using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QM9505
{
    public partial class MainForm : Form
    {
        Function function = new Function();
        Motion motion = new Motion();
        ParameterForm parameterForm = new ParameterForm();
        TCPForm tcpForm = new TCPForm();
        RecordForm recordForm = new RecordForm();
        ManualForm manualForm = new ManualForm();
        AlarmForm alarmForm = new AlarmForm();
        IOForm ioForm = new IOForm();
        UserForm userForm = new UserForm();
        Access access = new Access();
        Thread IoThread;


        #region 禁用窗体的关闭按钮
        ////禁用窗体的关闭按钮
        //private const int CP_NOCLOSE_BUTTON = 0x200;
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams myCp = base.CreateParams;
        //        myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
        //        return myCp;
        //    }
        //}
        #endregion 

        public MainForm()
        {
            InitializeComponent();
        }

        private static MainForm _instance;

        public static MainForm Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                    _instance = new MainForm();
                return _instance;
            }
        }

        #region 窗体加载
        private void MainForm_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;//检查线程间非法交
            this.WindowState = FormWindowState.Maximized;
            this.MaximizeBox = true;

            Variable.MachineState = Variable.MachineStatus.Stop;
            Variable.userEnter = Variable.UserEnter.User;

            //直接读取屏幕分辨率，
            this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            //这一条必须加，不加还是会把任务栏档住。具体原因我也不知道。
            this.MinimumSize = new Size(100, 100);

            //隐藏TabControl选项栏
            this.TabControl.Region = new Region(new RectangleF(this.MainTabPage.Left, this.MainTabPage.Top, this.MainTabPage.Width, this.MainTabPage.Height));

            #region 控制卡初始化

            short rtn = motion.OpenCardInit("gtn_core1.cfg", "gtn_core2.cfg");//初始化控制卡
            if (rtn != 0)
            {
                MessageBox.Show("初始化运动控制器失败！");
                //写入数据库
                access.RecordAccess(LogType.Operate, "初始化运动控制器失败！");
            }

            #endregion

            #region 开启线程

            //刷新IO
            IoThread = new Thread(function.ReadIO);
            IoThread.IsBackground = true;
            IoThread.Start();
            #endregion

            //加载主窗体
            LoadMainForm(new UserForm());

        }

        #endregion

        #region 窗体关闭

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.Yes != MessageBox.Show("程序正在使用中,确认退出?", "请确认", MessageBoxButtons.YesNo))
            {
                e.Cancel = true;
            }
            else
            {
                //写入数据库
                access.RecordAccess(LogType.Operate, "软件运行结束，即将关闭");
               
                //关闭相机程序
                StopProcess("ZJ-Vision");

                #region 关闭线程

                if (IoThread != null)
                {
                    IoThread.Abort();
                    IoThread = null;
                }

                #endregion

                Thread.Sleep(1000);

                //通信关闭
                try
                {
                    Temperature1.Close();
                    Temperature2.Close();
                }
                catch (Exception ex)
                {
                    Log.SaveError(new StackTrace(new StackFrame(true)), new StackFrame(), ex);
                }

                //停止所有轴
                motion.AxisStopAll();

                //停止步进
                function.OutYOFF(25);
                function.OutYOFF(26);
                function.OutYOFF(27);
                function.OutYOFF(28);

                //测试断电
                for (int i = 0; i < 14; i++)
                {
                    function.OutYOFF(58 + i * 3);
                }

                //三色灯关闭
                this.function.SetRedLampOff();
                this.function.SetGreenLampOff();
                this.function.SetYellowLampOff();
                this.function.SetBuzzerOff();

                //释放内存
                ClearMemory();

                //程序结束干净
                System.Environment.Exit(0);

            }
        }

        #endregion

        #region 页面切换
        public void LoadMainForm(object form)
        {
            TabControl.SelectTab(0);
            if (this.MainPanel.Controls.Count > 0)
            {
                this.MainPanel.Controls.RemoveAt(0);
            }

            Form f = form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.MainPanel.Controls.Add(f);
            this.MainPanel.Tag = f;
            f.Show();
        }
        public void LoadSubForm(object form)
        {
            TabControl.SelectTab(1);
            if (this.SubPanel.Controls.Count > 0)
            {
                this.SubPanel.Controls.RemoveAt(0);
            }

            Form f = form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.SubPanel.Controls.Add(f);
            this.SubPanel.Tag = f;
            f.Show();
        }

        private void 主界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseForm();

            if (this.SubPanel.Controls.Count > 0)
            {
                this.SubPanel.Controls.RemoveAt(0);
            }

            TabControl.SelectTab(0);
        }

        private void IO界面ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Variable.MachineState != Variable.MachineStatus.Running)
            {
                CloseForm();

                foreach (Control c in SubPanel.Controls)
                {
                    SubPanel.Controls.Remove(c);
                    c.Dispose();
                }
                LoadSubForm(new IOForm());
            }
        }

        private void 调试界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Variable.MachineState != Variable.MachineStatus.Running)
            {
                CloseForm();

                foreach (Control c in SubPanel.Controls)
                {
                    SubPanel.Controls.Remove(c);
                    c.Dispose();
                }
                LoadSubForm(new ManualForm());
            }
        }

        private void 参数界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Variable.MachineState != Variable.MachineStatus.Running)
            {
                CloseForm();

                foreach (Control c in SubPanel.Controls)
                {
                    SubPanel.Controls.Remove(c);
                    c.Dispose();
                }
                LoadSubForm(new ParameterForm());
            }
        }

        private void 通信界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Variable.MachineState != Variable.MachineStatus.Running)
            {
                CloseForm();

                foreach (Control c in SubPanel.Controls)
                {
                    SubPanel.Controls.Remove(c);
                    c.Dispose();
                }
                LoadSubForm(new TCPForm());
            }
        }

        private void 数据记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Variable.MachineState != Variable.MachineStatus.Running)
            {
                CloseForm();

                foreach (Control c in SubPanel.Controls)
                {
                    SubPanel.Controls.Remove(c);
                    c.Dispose();
                }
                LoadSubForm(new RecordForm());
            }
        }

        private void SummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Variable.MachineState != Variable.MachineStatus.Running)
            {
                CloseForm();

                foreach (Control c in SubPanel.Controls)
                {
                    SubPanel.Controls.Remove(c);
                    c.Dispose();
                }
                LoadSubForm(new AlarmForm());
            }
        }

        private void 权限登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.StartPosition = FormStartPosition.CenterScreen;
            loginForm.ShowDialog();
        }

        #endregion

        #region 释放内存
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);

        /// <summary>
        /// 释放内存
        /// </summary>
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        #endregion

        #region 存储异常
        public static void EX(string str)
        {
            try
            {
                FileStream fs = new FileStream(ConfigurationManager.ConnectionStrings["AbnormalCapture"].ConnectionString, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(DateTime.Now);
                sw.WriteLine(str);
                sw.Close();
                fs.Close();

            }
            catch (Exception ex)
            {
                Log.SaveError(new StackTrace(new StackFrame(true)), new StackFrame(), ex);
            }
        }
        #endregion

        #region 关闭窗体
        public void CloseForm()
        {
            ioForm.Close();
            ioForm.Dispose();
            manualForm.Close();
            manualForm.Dispose();
            parameterForm.Close();
            parameterForm.Dispose();
            tcpForm.Close();
            tcpForm.Dispose();
            recordForm.Close();
            recordForm.Dispose();
            alarmForm.Close();
            alarmForm.Dispose();

            while (SubPanel.Controls.Count > 0)
            {
                Control ct = SubPanel.Controls[0];
                SubPanel.Controls.Remove(ct);
                ct.Dispose();
                ct = null;
            }

            //foreach (Control item in this.SubPanel.Controls)
            //{
            //    if (item is Form)
            //    {
            //        ((Form)item).Close();
            //    }
            //}
        }





        #endregion

        #region 关闭安装程序

        public static void StopProcess(string processName)
        {
            try
            {
                System.Diagnostics.Process[] myprocess = System.Diagnostics.Process.GetProcessesByName(processName);
                if (myprocess.Count() > 0)
                {
                    myprocess[0].Kill();
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(new StackTrace(new StackFrame(true)), new StackFrame(), ex);
            }
        }

        #endregion


       
    }
}
