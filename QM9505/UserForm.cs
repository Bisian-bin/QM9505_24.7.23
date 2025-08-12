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
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using QM9505.TrayForm;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections;
using QM9505.TraySetForm;
using QM9505.ControlForm;


namespace QM9505
{
    public partial class UserForm : Form
    {
        #region 变量
        public CancellationToken token;
        public ManualResetEvent resetEvent = new ManualResetEvent(true);
        ManualForm manualForm = new ManualForm();
        ParameterForm parameterForm = new ParameterForm();
        INIHelper inIHelper = new INIHelper();
        TCPForm tcpForm = new TCPForm();
        Motion motion = new Motion();
        Helping helping = new Helping();
        Function function = new Function();
        IOForm ioForm = new IOForm();
        TXT myTXT = new TXT();
        InfoForm inform = new InfoForm();
        Temperature1 tem1 = new Temperature1();
        Temperature2 tem2 = new Temperature2();
        INIHelper iniHelper = new INIHelper();
        public static string path = Application.StartupPath + "\\currentInfo.ini";

        public static bool restStartFlag;
        public static bool restOverFlag;
        DataGrid dataGrid = new DataGrid();
        Access access = new Access();
        POPForm pop = new POPForm();
        Stopwatch runstopwatch = new Stopwatch();
        Stopwatch stopstopwatch = new Stopwatch();
        Stopwatch alarmstopwatch = new Stopwatch();
        Stopwatch[] watch = new Stopwatch[40];
        public static long[] second = new long[40];
        public static int[] takeNum = new int[40];
        public static int takeModNum;


        Tray1Form tray1Form = new Tray1Form();
        Tray2Form tray2Form = new Tray2Form();
        Tray3Form tray3Form = new Tray3Form();
        Tray4Form tray4Form = new Tray4Form();
        Tray5Form tray5Form = new Tray5Form();
        Tray6Form tray6Form = new Tray6Form();
        Tray7Form tray7Form = new Tray7Form();
        Tray8Form tray8Form = new Tray8Form();
        Tray9Form tray9Form = new Tray9Form();
        Tray10Form tray10Form = new Tray10Form();

        Tray1SetForm tray1SetForm = new Tray1SetForm();
        Tray2SetForm tray2SetForm = new Tray2SetForm();
        Tray3SetForm tray3SetForm = new Tray3SetForm();
        Tray4SetForm tray4SetForm = new Tray4SetForm();
        Tray5SetForm tray5SetForm = new Tray5SetForm();
        Tray6SetForm tray6SetForm = new Tray6SetForm();
        Tray7SetForm tray7SetForm = new Tray7SetForm();
        Tray8SetForm tray8SetForm = new Tray8SetForm();
        Tray9SetForm tray9SetForm = new Tray9SetForm();
        Tray10SetForm tray10SetForm = new Tray10SetForm();

        GPSForm gpsForm = new GPSForm();

        Thread RestThread;
        Thread refresh;
        Thread ButtonRefresh;
        Thread INAutoReadyRestThread;
        Thread INAutoEmptyRestThread;
        Thread OutAutoOKRestThread;
        Thread OutAutoFillRestThread;
        Thread OutAutoNGRestThread;
        Thread RobotAutoRestThread;
        Thread ModelAutoRestThread;

        Thread AutoThread;
        Thread INAutoReady1Thread;
        Thread INAutoReadyStartThread;
        Thread INAutoEmptyStartThread;
        Thread OutAutoOKStartThread;
        Thread OutAutoFillStartThread;
        Thread OutAutoNGStartThread;
        Thread RobotAutoStartThread;
        Thread TcpServerStartThread;
        Thread TempThread;
        Thread LampScanThread;
        Thread TimeRecord;

        /*增加队列*/
        Queue<double> recordTime1 = new Queue<double>(40);
        Queue<double> recordTime2 = new Queue<double>(40);
        Queue<double> recordTime3 = new Queue<double>(40);
        Queue<double> recordTime4 = new Queue<double>(40);
        Queue<double> Serie1_1 = new Queue<double>(40);
        Queue<double> Serie1_2 = new Queue<double>(40);
        Queue<double> Serie1_3 = new Queue<double>(40);
        Queue<double> Serie1_4 = new Queue<double>(40);
        Queue<double> Serie1_5 = new Queue<double>(40);
        Queue<double> Serie1_6 = new Queue<double>(40);
        Queue<double> Serie1_7 = new Queue<double>(40);
        Queue<double> Serie1_8 = new Queue<double>(40);
        Queue<double> Serie1_9 = new Queue<double>(40);
        Queue<double> Serie1_10 = new Queue<double>(40);
        Queue<double> Serie1_11 = new Queue<double>(40);
        Queue<double> Serie1_12 = new Queue<double>(40);
        Queue<double> Serie1_13 = new Queue<double>(40);
        Queue<double> Serie1_14 = new Queue<double>(40);
        Queue<double> Serie1_15 = new Queue<double>(40);
        Queue<double> Serie1_16 = new Queue<double>(40);
        Queue<double> Serie1_17 = new Queue<double>(40);
        Queue<double> Serie1_18 = new Queue<double>(40);
        Queue<double> Serie1_19 = new Queue<double>(40);
        Queue<double> Serie1_20 = new Queue<double>(40);

        Queue<double> Serie2_1 = new Queue<double>(40);
        Queue<double> Serie2_2 = new Queue<double>(40);
        Queue<double> Serie2_3 = new Queue<double>(40);
        Queue<double> Serie2_4 = new Queue<double>(40);
        Queue<double> Serie2_5 = new Queue<double>(40);
        Queue<double> Serie2_6 = new Queue<double>(40);
        Queue<double> Serie2_7 = new Queue<double>(40);
        Queue<double> Serie2_8 = new Queue<double>(40);
        Queue<double> Serie2_9 = new Queue<double>(40);
        Queue<double> Serie2_10 = new Queue<double>(40);
        Queue<double> Serie2_11 = new Queue<double>(40);
        Queue<double> Serie2_12 = new Queue<double>(40);
        Queue<double> Serie2_13 = new Queue<double>(40);
        Queue<double> Serie2_14 = new Queue<double>(40);
        Queue<double> Serie2_15 = new Queue<double>(40);
        Queue<double> Serie2_16 = new Queue<double>(40);
        Queue<double> Serie2_17 = new Queue<double>(40);
        Queue<double> Serie2_18 = new Queue<double>(40);
        Queue<double> Serie2_19 = new Queue<double>(40);
        Queue<double> Serie2_20 = new Queue<double>(40);

        Random rand = new Random();

        public static bool RobotAlarmFlag = false;
        public static bool INReadyNull1Flag = false;
        public static bool INReadyNull2Flag = false;
        public static bool RobotPause = false;
        public static long runSecond;
        public static long Second1;
        public static long stopSecond;
        public static long Second2;
        public static long alarmSecond;
        public static long Second3;
        public static long uphSecond;
        public static int lightFlag;
        public static bool buzzingFlag;
        public static bool stopRobotFlag;
        public static int alarmNum;
        public static int QRCount;
        public static string mes_OPNO;
        public static string mes_CURQTY;

        #endregion

        #region 禁用窗体的关闭按钮
        //禁用窗体的关闭按钮
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                myCp.ExStyle |= 0x02000000;
                return myCp;
            }
        }

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams myCp = base.CreateParams;
        //        myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
        //        return myCp;
        //    }
        //}

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;
        //        return cp;
        //    }
        //}
        #endregion

        #region 程序主入口
        public UserForm()
        {
            InitializeComponent();
            //OrderNum.SelectAll();//选中文本框中的所有文本
            //OrderNum.Focus();//为控件设置输入焦点

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
        }

        #endregion

        #region 窗体加载
        private void UserForm_Load(object sender, EventArgs e)
        {

            Control.CheckForIllegalCrossThreadCalls = false;//检查线程间非法交
            this.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "V1.0";
            this.WindowState = FormWindowState.Maximized;
            this.MaximizeBox = true;
            //降低CPU
            helping.Cracker();

            //写入数据库
            access.RecordAccess(LogType.Operate, "软件开始运行");

            //初始化数组
            DayNightArray();

            //档案加载
            string path = @"D:\参数\";
            string[] files = Directory.GetFiles(path, "*.bin");
            foreach (string file in files)
            {
                string[] split = file.Split(new Char[] { '\\' });
                ModelCombo.Items.Add(split[split.Length - 1]);
            }

            for (int i = 0; i < 40; i++)
            {
                watch[i] = new Stopwatch();
            }

            #region 参数初始化

            //加载参数
            parameterForm.LoadParameter();
            parameterForm.LoadPoint(Application.StartupPath + "\\Point.ini");
            parameterForm.LoadParameter(Application.StartupPath + "\\Parameter.ini");
            Variable.FileName = iniHelper.getIni("PGM", "FileName", "", Application.StartupPath + "//parameter.ini");

            bool modelFlag = false;
            for (int i = 0; i < ModelCombo.Items.Count; i++)
            {
                if (ModelCombo.Items[i].ToString() == Variable.FileName)
                {
                    modelFlag = true;
                }
            }
            if (modelFlag)
            {
                //加载参数    
                string path1 = @"D:\参数\" + Variable.FileName;
                parameterForm.LoadParameter(path1);
                ModelCombo.Text = Variable.FileName;
            }
            else
            {
                //加载参数    
                string path1 = @"D:\参数\" + ModelCombo.Items[0].ToString();
                parameterForm.LoadParameter(path1);
                ModelCombo.Text = Variable.FileName = ModelCombo.Items[0].ToString();
            }

            //报警参数
            for (int i = 0; i < 300; i++)
            {
                Variable.AlarmContent[i] = iniHelper.AlarmReadContentValue("ALARM", i.ToString());
            }

            #endregion

            #region DataGridView初始化

            dataGrid.IniLeftLoadTrayW(EmptyDataGrid, Variable.RowNum, Variable.ListNum);
            dataGrid.IniLeftLoadTrayW(LoadDataGrid, Variable.RowNum, Variable.ListNum);
            dataGrid.IniLeftLoadTrayW(OKDataGrid, Variable.RowNum, Variable.ListNum);
            dataGrid.IniLeftLoadTrayW(FillDataGrid, Variable.RowNum, Variable.ListNum);
            dataGrid.IniLeftLoadTrayW(NGDataGrid, Variable.RowNum, Variable.ListNum);

            dataGrid.InitializeData(TemperDataGrid, 5, 11);
            dataGrid.Initialize(infoDataGrid, 8, 4);

            #endregion

            #region 加载子窗体
            //加载测试机显示
            CloseForm();
            foreach (Control c in SubPanel.Controls)
            {
                SubPanel.Controls.Remove(c);
                c.Dispose();
            }
            LoadSubForm(new Tray1Form());

            #endregion

            #region 温控器链接在线

            bool sc1 = false;
            Temperature1.ComPort(ConfigurationManager.ConnectionStrings["TemperatureCOM1"].ConnectionString);
            sc1 = Temperature1.Open();
            if (sc1)
            {

            }
            else
            {
                MessageBox.Show("温控器1链接失败");
            }

            bool sc2 = false;
            Temperature2.ComPort(ConfigurationManager.ConnectionStrings["TemperatureCOM2"].ConnectionString);
            sc2 = Temperature2.Open();
            if (sc2)
            {

            }
            else
            {
                MessageBox.Show("温控器1链接失败");
            }
            #endregion

            #region 开启QR通讯
            // bool b1 = QRTcpClient.ConnectServer();
            // if (!b1)
            //{
            //    MessageBox.Show("开启QR连接失败");
            //}

            #endregion

            #region 开启相机通讯
            bool b2 = PhotoTcpServer.StartListening();
            if (!b2)
            {
                MessageBox.Show("开启相机连接失败");
            }
            #endregion

            #region 开启Robot通讯
            bool b3 = RobotTcpServer.StartListening();
            if (!b3)
            {
                MessageBox.Show("开启Robot连接失败");
            }
            #endregion

            #region 打开相机应用程序
            try
            {
                OpenOtherEXEMethod("ZJ-Vision.exe", @"D:\ZJ-Vision 2.0.0.10");
            }
            catch
            {
                MessageBox.Show("打开视觉程序失败");
            }
            #endregion

            #region 柱状图

            ChartShow();
            cht1.Titles[1].Text = "探针次数设定：" + Variable.ProbeSet[0].ToString();

            #endregion

            #region 开启线程

            //开启刷新线程
            refresh = new Thread(RefreshT);
            refresh.IsBackground = true;
            refresh.Start();

            //按钮刷新线程
            ButtonRefresh = new Thread(ButtonRef);
            ButtonRefresh.IsBackground = true;
            ButtonRefresh.Start();

            //温控器
            TempThread = new Thread(Temp);
            TempThread.IsBackground = true;
            TempThread.Start();

            //总复位
            RestThread = new Thread(Rest);
            RestThread.IsBackground = true;
            RestThread.Start();

            //上料待机复位
            INAutoReadyRestThread = new Thread(INAutoReadyRest);
            INAutoReadyRestThread.IsBackground = true;
            INAutoReadyRestThread.Start();

            //上料空Tray复位
            INAutoEmptyRestThread = new Thread(INAutoEmptyRest);
            INAutoEmptyRestThread.IsBackground = true;
            INAutoEmptyRestThread.Start();

            //下料OK复位
            OutAutoOKRestThread = new Thread(OutAutoOKRest);
            OutAutoOKRestThread.IsBackground = true;
            OutAutoOKRestThread.Start();

            //下料补料复位
            OutAutoFillRestThread = new Thread(OutAutoFillRest);
            OutAutoFillRestThread.IsBackground = true;
            OutAutoFillRestThread.Start();

            //下料NG复位
            OutAutoNGRestThread = new Thread(OutAutoNGRest);
            OutAutoNGRestThread.IsBackground = true;
            OutAutoNGRestThread.Start();

            //下料Robot复位
            RobotAutoRestThread = new Thread(RobotAutoRest);
            RobotAutoRestThread.IsBackground = true;
            RobotAutoRestThread.Start();

            //单机复位
            ModelAutoRestThread = new Thread(ModelAutoRest);
            ModelAutoRestThread.IsBackground = true;
            ModelAutoRestThread.Start();

            //自动流程
            AutoThread = new Thread(Auto);
            AutoThread.IsBackground = true;
            AutoThread.Start();

            //上料待测流程
            INAutoReadyStartThread = new Thread(INAutoReadyStart);
            INAutoReadyStartThread.IsBackground = true;
            INAutoReadyStartThread.Start();

            //上料分料流程
            INAutoReady1Thread = new Thread(INAutoReady1Start);
            INAutoReady1Thread.IsBackground = true;
            INAutoReady1Thread.Start();


            //上料空Tray流程
            INAutoEmptyStartThread = new Thread(INAutoEmptyStart);
            INAutoEmptyStartThread.IsBackground = true;
            INAutoEmptyStartThread.Start();


            //下料OK流程
            OutAutoOKStartThread = new Thread(OutAutoOKStart);
            OutAutoOKStartThread.IsBackground = true;
            OutAutoOKStartThread.Start();


            //下料补料流程
            OutAutoFillStartThread = new Thread(OutAutoFillStart);
            OutAutoFillStartThread.IsBackground = true;
            OutAutoFillStartThread.Start();

            //下料NG流程
            OutAutoNGStartThread = new Thread(OutAutoNGStart);
            OutAutoNGStartThread.IsBackground = true;
            OutAutoNGStartThread.Start();

            //下料Robot
            RobotAutoStartThread = new Thread(RobotAutoStart);
            RobotAutoStartThread.IsBackground = true;
            RobotAutoStartThread.Start();

            //TCP
            TcpServerStartThread = new Thread(TcpServerStart);
            TcpServerStartThread.IsBackground = true;
            TcpServerStartThread.Start();

            //延迟加热
            DelayHeat();

            //三色灯扫描
            LampScanThread = new Thread(LampScan);
            LampScanThread.IsBackground = true;
            LampScanThread.Start();

            //计时
            TimeRecord = new Thread(TimeCount);
            TimeRecord.IsBackground = true;
            TimeRecord.Start();

            #endregion

            InitChart();
            Initseries();
            Charttimer.Interval = 2000;//2秒

            ReadQRArray();
            ReadOPArray();

            MessageBox.Show("机台开始前请先归零！");

        }

        #endregion

        #region **********系统线程**********
        public void ButtonRef()
        {
            while (true)
            {
                #region 轴卡报错

                //轴卡报错
                Variable.ServerAlarm = AxisAlarmTrue();
                Variable.PLimitAlarm = AxisPAlarmTrue();
                Variable.NLimitAlarm = AxisNAlarmTrue();

                if ((Variable.ServerAlarm || Variable.PLimitAlarm || Variable.NLimitAlarm))// && Variable.MachineState == Variable.MachineStatus.Running)
                {
                    //motion.AxisStopAll();
                    Variable.RunEnable = false;
                    Variable.RestEnable = false;
                    Variable.MachineState = Variable.MachineStatus.Alarm;
                }

                #endregion

                #region 程序报警

                //程序报警
                Variable.CommonAlarm1 = AlarmTrue();

                Variable.CommonAlarm2 = Variable.QRAlarm || Variable.PhotoAlarm || Variable.UpAxisAlarm || Variable.DownAxisAlarm || Variable.RobotAlarm || Variable.ModelAlarm || Variable.ProbeAlarm || Variable.AlarmFlag;

                if ((Variable.CommonAlarm1 || Variable.CommonAlarm2) && (Variable.MachineState == Variable.MachineStatus.Running || Variable.MachineState == Variable.MachineStatus.zero))
                {
                    Variable.RunEnable = false;
                    Variable.RestEnable = false;
                    Variable.MachineState = Variable.MachineStatus.Alarm;
                    motion.AxisStopAll();
                }
                #endregion

                #region 急停按钮

                //急停按钮
                if (Variable.EMG == true)
                {
                    StopRest();
                    StopAuto();
                    Variable.RestEnable = false;
                    Variable.MachineState = Variable.MachineStatus.Stop;
                    motion.AxisStopAll();
                    //写入数据库
                    access.RecordAccess(LogType.Operate, "急停按钮被点击，机台急停中");
                }
                #endregion

                #region 启动按钮

                //启动按钮
                if (((Variable.StartButton == true || Variable.btnStart == true) && Variable.MachineState == Variable.MachineStatus.StandBy && restOverFlag) || ((Variable.StartButton == true || Variable.btnStart == true) && Variable.MachineState == Variable.MachineStatus.Pause))
                {
                    Form POPFormIsOpenOrNot = Application.OpenForms["POPForm"];
                    if ((POPFormIsOpenOrNot == null) || (POPFormIsOpenOrNot.IsDisposed))//如果没有创建过或者窗体已经被释放
                    {
                        Variable.AlarmFlag = false;
                        Variable.MachineState = Variable.MachineStatus.Running;
                        Variable.RunEnable = true;
                        if (Variable.AutoStep < 10 && restOverFlag)
                        {
                            Variable.AutoStep = 10;
                        }
                        //写入数据库
                        access.RecordAccess(LogType.Operate, "启动按钮被点击，机台自动运行");
                    }
                    else
                    {
                        MessageBox.Show("有报警窗体未关闭，请关闭再运行");
                    }
                }
                #endregion

                #region 暂停按钮

                //暂停按钮
                if (((Variable.PauseButton == true || Variable.btnStop == true) && (Variable.MachineState == Variable.MachineStatus.Running || Variable.MachineState == Variable.MachineStatus.StandBy || Variable.MachineState == Variable.MachineStatus.Reset)) || ((Variable.PauseButton == true || Variable.btnStop == true) && Variable.MachineState == Variable.MachineStatus.Alarm &&
                    !Variable.ServerAlarm && !Variable.PLimitAlarm && !Variable.NLimitAlarm && !Variable.CommonAlarm1 && !Variable.CommonAlarm2))
                {
                    Variable.btnStop = false;
                    Variable.RunEnable = false;
                    Variable.MachineState = Variable.MachineStatus.Pause;
                    motion.AxisStopAll();
                    //this.listAlarm.Items.Clear();
                    //写入数据库
                    access.RecordAccess(LogType.Operate, "暂停按钮被点击，机台暂停中");
                }
                #endregion

                #region 报警复位按钮

                //报警复位按钮消蜂鸣
                if ((Variable.AlarmClrButton == true || Variable.btnReset == true) && (Variable.MachineState == Variable.MachineStatus.Alarm || Variable.MachineState == Variable.MachineStatus.Emg))
                {
                    Variable.MachineState = Variable.MachineStatus.Reset;
                    //写入数据库
                    access.RecordAccess(LogType.Operate, "报警复位按钮被点击，机台报警复位中");
                }
                #endregion

                #region 归零按钮

                //归零按钮
                if ((Variable.ZeroButton == true || Variable.btnZero == true) && Variable.MachineState != Variable.MachineStatus.Running)
                {
                    if (MessageBox.Show("机台归零?选择'是'归零，选择'否'放弃！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.Yes)
                    {
                        //    if (MessageBox.Show("机台归零?选择'是'归零，选择'否'放弃！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //{
                        for (int i = 0; i < 27; i++)
                        {
                            Variable.ServoAlarmHappen[i] = false;
                        }
                        Variable.btnZero = false;
                        Variable.MachineState = Variable.MachineStatus.zero;
                        StopRest();
                        StopAuto();
                        //listAlarm.Items.Clear();
                        restStartFlag = true;
                        Variable.RestEnable = true;
                        restOverFlag = false;
                        Variable.RestStep = 10;
                        //写入数据库
                        access.RecordAccess(LogType.Operate, "归零按钮被点击，机台归零中");
                    }
                    else
                    {
                        Variable.btnZero = false;
                        Variable.btnZero = false;
                    }
                }
                #endregion

                #region 停止归零

                //停止归零
                if ((Variable.PauseButton == true || Variable.btnStop == true) && Variable.MachineState == Variable.MachineStatus.zero)
                {
                    Variable.MachineState = Variable.MachineStatus.Stop;
                    Variable.RestEnable = false;
                    stopRobotFlag = true;
                    motion.AxisStopAll();
                    //listAlarm.Items.Clear();
                    StopRest();
                    //写入数据库
                    access.RecordAccess(LogType.Operate, "停止按钮被点击，机台停止归零中");

                }
                #endregion

                #region 清料按钮

                //清空机台按钮
                if ((Variable.OneCycleButton || Variable.btnCleanOne) && Variable.MachineState == Variable.MachineStatus.Pause)
                {
                    if (MessageBox.Show("请确认是否清料?选择'是'确定，选择'否'放弃！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Variable.CleanOne = true;
                        Variable.CleanOut = false;
                        //写入数据库
                        access.RecordAccess(LogType.Operate, "清料按钮被点击，机台清料中");
                    }
                    else
                    {
                        Variable.btnCleanOne = false;
                    }
                }
                #endregion

                #region 结批按钮

                //结批按钮
                if ((Variable.CleanOutButton || Variable.btnCleanOut) && Variable.MachineState != Variable.MachineStatus.Running)
                {
                    if (MessageBox.Show("请确认是否结批?选择'是'确定，选择'否'放弃！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Variable.CleanOut = true;
                        Variable.CleanOne = false;
                        //写入数据库
                        access.RecordAccess(LogType.Operate, "结批按钮被点击，机台结批中");
                    }
                    else
                    {
                        Variable.btnCleanOut = false;
                    }
                }
                #endregion

                #region 暂停消弹窗

                //暂停消弹窗
                if ((Variable.PauseButton == true || Variable.btnStop == true) && Variable.POPFlag)
                {
                    if (Variable.OPsure)
                    {
                        if (!string.IsNullOrEmpty(Variable.OPNum) && !string.IsNullOrEmpty(Variable.OPPass))
                        {
                            bool b = OPToJudge(Variable.OPNum, Variable.OPPass);
                            if (b)
                            {
                                if (Variable.step != "" && Variable.cancelStep != 0 && Variable.sureStep != 0)
                                {
                                    switch (Variable.step)
                                    {
                                        case "Variable.INAutoReady1Step":
                                            {
                                                Variable.INAutoReady1Step = Convert.ToInt32(Variable.sureStep);
                                                break;
                                            }
                                        case "Variable.INAutoReadyStep":
                                            {
                                                Variable.INAutoReadyStep = Convert.ToInt32(Variable.sureStep);
                                                break;
                                            }
                                        case "Variable.INAutoEmptyStartStep":
                                            {
                                                Variable.INAutoEmptyStartStep = Convert.ToInt32(Variable.sureStep);
                                                break;
                                            }
                                        case "Variable.OutAutoOKStartStep":
                                            {
                                                Variable.OutAutoOKStartStep = Convert.ToInt32(Variable.sureStep);
                                                break;
                                            }
                                        case "Variable.OutAutoFillStartStep":
                                            {
                                                Variable.OutAutoFillStartStep = Convert.ToInt32(Variable.sureStep);
                                                break;
                                            }
                                        case "Variable.OutAutoNGStartStep":
                                            {
                                                Variable.OutAutoNGStartStep = Convert.ToInt32(Variable.sureStep);
                                                break;
                                            }
                                        case "Variable.RobotAutoStartStep":
                                            {
                                                Variable.RobotAutoStartStep = Convert.ToInt32(Variable.sureStep);
                                                break;
                                            }
                                        case "Variable.ModelSetStep":
                                            {
                                                Variable.ModelSetStep = Convert.ToInt32(Variable.sureStep);
                                                break;
                                            }
                                        case "Variable.ModelGetStep":
                                            {
                                                Variable.ModelGetStep = Convert.ToInt32(Variable.sureStep);
                                                break;
                                            }
                                    }
                                }

                                //写入数据库
                                access.RecordAccess(LogType.Alarm1, "OP号:" + textBox1.Text + "操作--" + Variable.content);

                                Variable.OPsure = false;
                                Variable.OPNum = "";
                                Variable.OPPass = "";

                                if (Variable.CleanOutFlag)
                                {
                                    Variable.CleanOutFlag = false;
                                    Variable.MachineState = Variable.MachineStatus.Stop;
                                }
                                Variable.POPFlag = false;
                                Variable.btnReset = true;
                                Variable.step = "";
                                Variable.cancelStep = 0;
                                Variable.sureStep = 0;

                                access.RecordAccess(LogType.Operate, "停止按钮被点击，消报警弹窗");
                            }
                            else
                            {
                                MessageBox.Show("当前OP号没有权限操作此报警，请确认！");
                            }
                        }
                        else
                        {
                            MessageBox.Show("请输入OP号，在消弹窗！");
                        }
                    }
                    else
                    {
                        if (Variable.step != "" && Variable.cancelStep != 0 && Variable.sureStep != 0)
                        {
                            switch (Variable.step)
                            {
                                case "Variable.INAutoReady1Step":
                                    {
                                        Variable.INAutoReady1Step = Convert.ToInt32(Variable.sureStep);
                                        break;
                                    }
                                case "Variable.INAutoReadyStep":
                                    {
                                        Variable.INAutoReadyStep = Convert.ToInt32(Variable.sureStep);
                                        break;
                                    }
                                case "Variable.INAutoEmptyStartStep":
                                    {
                                        Variable.INAutoEmptyStartStep = Convert.ToInt32(Variable.sureStep);
                                        break;
                                    }
                                case "Variable.OutAutoOKStartStep":
                                    {
                                        Variable.OutAutoOKStartStep = Convert.ToInt32(Variable.sureStep);
                                        break;
                                    }
                                case "Variable.OutAutoFillStartStep":
                                    {
                                        Variable.OutAutoFillStartStep = Convert.ToInt32(Variable.sureStep);
                                        break;
                                    }
                                case "Variable.OutAutoNGStartStep":
                                    {
                                        Variable.OutAutoNGStartStep = Convert.ToInt32(Variable.sureStep);
                                        break;
                                    }
                                case "Variable.RobotAutoStartStep":
                                    {
                                        Variable.RobotAutoStartStep = Convert.ToInt32(Variable.sureStep);
                                        break;
                                    }
                                case "Variable.ModelSetStep":
                                    {
                                        Variable.ModelSetStep = Convert.ToInt32(Variable.sureStep);
                                        break;
                                    }
                                case "Variable.ModelGetStep":
                                    {
                                        Variable.ModelGetStep = Convert.ToInt32(Variable.sureStep);
                                        break;
                                    }
                            }
                        }

                        if (Variable.CleanOutFlag)
                        {
                            Variable.CleanOutFlag = false;
                            Variable.MachineState = Variable.MachineStatus.Stop;
                        }
                        Variable.POPFlag = false;
                        Variable.btnReset = true;
                        Variable.step = "";
                        Variable.cancelStep = 0;
                        Variable.sureStep = 0;
                        Variable.OPNum = "";
                        Variable.OPPass = "";

                        access.RecordAccess(LogType.Operate, "停止按钮被点击，消报警弹窗");
                    }

                }

                #endregion

                Thread.Sleep(1);
            }
        }

        #region UI界面按钮
        //开始按钮
        private void btnStart_MouseDown(object sender, MouseEventArgs e)
        {
            Variable.btnStart = true;
            Thread.Sleep(500);
            Variable.btnStart = false;
            Application.DoEvents();
        }

        private void btnStart_MouseUp(object sender, MouseEventArgs e)
        {
            Variable.btnStart = false;
            Application.DoEvents();
        }

        //停止按钮
        private void btnStop_MouseDown(object sender, MouseEventArgs e)
        {
            Variable.btnStop = true;
            Thread.Sleep(500);
            Variable.btnStop = false;
            Application.DoEvents();
        }

        private void btnStop_MouseUp(object sender, MouseEventArgs e)
        {
            Variable.btnStop = false;
            Application.DoEvents();
        }

        //复位按钮
        private void btnReset_MouseDown(object sender, MouseEventArgs e)
        {
            Variable.btnReset = true;
            //Thread.Sleep(500);
            //Variable.btnReset = false;
            Application.DoEvents();
        }

        private void btnReset_MouseUp(object sender, MouseEventArgs e)
        {
            Variable.btnReset = false;
            Application.DoEvents();
        }

        //清料按钮
        private void btnCleanOne_MouseDown(object sender, MouseEventArgs e)
        {
            Variable.btnCleanOne = true;
            Application.DoEvents();
        }

        private void btnCleanOne_MouseUp(object sender, MouseEventArgs e)
        {
            Variable.btnCleanOne = false;
            Application.DoEvents();
        }

        //结批按钮
        private void btnCleanOut_MouseDown(object sender, MouseEventArgs e)
        {
            Variable.btnCleanOut = true;
            Application.DoEvents();
        }

        private void btnCleanOut_MouseUp(object sender, MouseEventArgs e)
        {
            Variable.btnCleanOut = false;
            Application.DoEvents();
        }

        //回原点按钮
        private void btnZero_MouseDown(object sender, MouseEventArgs e)
        {
            Variable.btnZero = true;
            Thread.Sleep(500);
            Variable.btnZero = false;
            Application.DoEvents();
        }

        private void btnZero_MouseUp(object sender, MouseEventArgs e)
        {
            Variable.btnZero = false;
            Application.DoEvents();
        }

        #endregion

        #region 停止复位
        public void StopRest()
        {
            Variable.RestStep = 0;
            Variable.INAutoReadyRestStep = 0;
            Variable.INAutoEmptyRestStep = 0;
            Variable.OutAutoOKRestStep = 0;
            Variable.OutAutoFillRestStep = 0;
            Variable.OutAutoNGRestStep = 0;
            Variable.RobotAutoRestStep = 0;

        }
        #endregion

        #region 停止自动
        public void StopAuto()
        {
            Variable.AutoStep = 0;
            Variable.INAutoReady1Step = 0;
            Variable.INAutoReadyStep = 0;
            Variable.INAutoEmptyStartStep = 0;
            Variable.OutAutoOKStartStep = 0;
            Variable.OutAutoFillStartStep = 0;
            Variable.OutAutoNGStartStep = 0;
            Variable.RobotHomeStartStep = 0;
            Variable.RobotAutoStartStep = 0;
            Variable.RobotTCPAutoStep = 0;
            Variable.PhotoTCPAutoStep = 0;

            Variable.RobotSetStep = 0;
            Variable.RobotSet = 0;
            Variable.RobotGet = 0;
            Variable.RobotGetStep = 0;
            Variable.ModelSetStep = 0;
            Variable.ModelGetStep = 0;
            Variable.TcpServerStartStep = 0;

        }

        #endregion

        #endregion

        #region **********复位流程**********

        #region 复位流程
        public void Rest()
        {
            while (true)
            {
                switch (Variable.RestStep)
                {
                    case 10:
                        {
                            if (Variable.RestEnable == true)
                            {
                                if (Variable.PGMcheck)
                                {
                                    //断电加载参数
                                    Variable.endTime = iniHelper.getIni("PGM", "endTime", "", path);//2023/05/23 21:26:40
                                    if (Variable.endTime == null || Variable.endTime == "")
                                    {
                                        Variable.endTime = "2023/05/23 21:26:40";
                                    }

                                    string path1 = @"D:\test program\Test Program.txt";
                                    FileInfo fileInfo = new FileInfo(path1);
                                    // 获取或设置最后一次修改文件夹或文件夹的时间
                                    DateTime lastWriteTime = fileInfo.LastWriteTime;//2023/6/13 18:49:28
                                    DateTime endTime = Convert.ToDateTime(Variable.endTime);//2023/05/23 21:26:40
                                    int compNum = DateTime.Compare(lastWriteTime, endTime);
                                    if (compNum >= 0)//lastWriteTime>= endTime
                                    {
                                        Variable.RestStep = 15;
                                    }
                                    else
                                    {
                                        MessageBox.Show("未用PGM下载程序，请确认程序是否正确，重新下载PGM！");
                                    }
                                }
                                else
                                {
                                    Variable.RestStep = 15;
                                }
                            }
                            break;
                        }
                    case 15:
                        {
                            if (Variable.RestEnable == true)
                            {
                                if (restStartFlag)//需要复位
                                {
                                    this.listAlarm.Items.Clear();
                                    //清除数据
                                    ClearInfoData();
                                    ClearRecordData();

                                    //阵列初始化
                                    ArrayCount();

                                    mc.GTN_ClrSts(1, 1, 12);  //核1、2清除轴状态
                                    mc.GTN_ClrSts(2, 1, 12);
                                    motion.MCAxisOnAll(1, 12);//轴使能
                                    motion.MCAxisOnAll(2, 12);
                                    Thread.Sleep(1000);

                                    Variable.CleanOne = false;
                                    Variable.CleanOut = false;

                                    Array.Clear(Variable.AxisPTime, 0, Variable.AxisPTime.Length);
                                    Array.Clear(Variable.AxisNTime, 0, Variable.AxisNTime.Length);
                                    Array.Clear(Variable.ServoAlarmHappen, 0, Variable.ServoAlarmHappen.Length);
                                    Array.Clear(Variable.PlimitAlarmHappen, 0, Variable.PlimitAlarmHappen.Length);
                                    Array.Clear(Variable.NlimitAlarmHappen, 0, Variable.NlimitAlarmHappen.Length);
                                    Array.Clear(Variable.AlarmHappen, 0, Variable.AlarmHappen.Length);
                                    Array.Clear(Variable.OnTime, 0, Variable.OnTime.Length);
                                    Array.Clear(Variable.OffTime, 0, Variable.OffTime.Length);
                                    Array.Clear(Variable.OnEnable, 0, Variable.OnEnable.Length);
                                    Array.Clear(Variable.OffEnable, 0, Variable.OffEnable.Length);
                                    Array.Clear(Variable.PicBox, 0, 10);
                                    Array.Clear(Variable.AxisAlarmTime, 0, Variable.AxisAlarmTime.Length);

                                    Variable.PhotoAlarm = false;
                                    Variable.UpAxisAlarm = false;
                                    Variable.DownAxisAlarm = false;
                                    Variable.RobotAlarm = false;
                                    Variable.QRAlarm = false;
                                    Variable.ModelAlarm = false;
                                    Variable.AlarmFlag = false;

                                    Variable.btnReset = false;
                                    Variable.CleanOutFlag = false;
                                    Variable.ProbeAlarm = false;
                                    RobotAlarmFlag = false;
                                    INReadyNull1Flag = false;
                                    INReadyNull2Flag = false;
                                    Variable.RobotRecOK = false;
                                    Variable.RobotResetNG = false;
                                    Variable.INAutoReady1flag = false;
                                    Variable.INAutoReady2flag = false;
                                    Variable.UpNullTray = 0;
                                    Variable.UpNullTrayFull = false;
                                    Variable.UpNullTrayOK = false;
                                    Variable.UpReady1TrayOK = false;
                                    Variable.UpReady1TrayFlag = false;
                                    Variable.UpReady2TrayOK = false;
                                    Variable.UpReady2TrayFlag = false;
                                    Variable.UpReadyTrayEmpty = 0;
                                    Variable.UpReadyTrayOK = false;
                                    Variable.DownOKTrayFull = 0;
                                    Variable.DownOKTrayWait = 0;
                                    Variable.DownOKTrayFullOK = false;
                                    Variable.DownOKTrayNG = 0;
                                    Variable.DownGetTray = false;
                                    Variable.DownReadyTrayFullOK = false;
                                    Variable.DownReadyTrayOK = 0;
                                    Variable.OutFillTrayFlag = false;
                                    Variable.DownReadyEmpty = false;
                                    Variable.DownReadyOK = 0;
                                    Variable.DownNGTray = 0;
                                    Variable.DownNGTrayFull = false;
                                    Variable.DownNGTrayOK = false;
                                    Variable.RobotUpGetTray = false;
                                    Variable.RobotToFileUp = false;
                                    Variable.RobotSetFlag = false;
                                    Variable.RobotGetFlag = false;
                                    Variable.RobotDownGetTray = false;
                                    Variable.RobotSetTray = false;
                                    Variable.RobotSetTrayOK = false;
                                    Variable.RobotSafePoint = false;
                                    Variable.RobotGeting = false;
                                    Variable.RobotSeting = false;
                                    Variable.messageRecord[0] = "";
                                    stopRobotFlag = false;
                                    Variable.XAlarmTime[1] = 0;
                                    Variable.XAlarmTime[2] = 0;
                                    Variable.XAlarmTime[3] = 0;
                                    Variable.XAlarmTime[4] = 0;
                                    Variable.info = false;

                                    Variable.RestStep = 20;
                                }
                            }
                            break;
                        }
                    case 20:
                        {
                            if (Variable.RestEnable == true)
                            {
                                StopPower(); //测试板断电
                                if (!Variable.HotModel)
                                {
                                    HotClose();  //加热关闭
                                }

                                Variable.RestStep = 30;
                            }
                            break;
                        }

                    case 30:
                        {
                            if (Variable.RestEnable == true)
                            {
                                //单机屏蔽复位
                                for (int i = 0; i < 40; i++)
                                {
                                    Variable.ModelState[i] = 0;
                                }

                                // 屏蔽机台
                                for (int i = 0; i < 40; i++)
                                {
                                    if (!Variable.modelCheck[i])
                                    {
                                        Variable.ModelState[i] = 10;
                                    }
                                }
                                //取料
                                for (int i = 0; i < 40; i++)
                                {
                                    takeNum[i] = 10;
                                }

                                function.OutYOFF(19); //告诉机械手可以继续动作

                                Variable.RestStep = 40;
                            }
                            break;
                        }
                    case 40:
                        {
                            if (Variable.RestEnable == true)
                            {
                                for (int i = 0; i < 40; i++)
                                {
                                    DeleteFinishTXT((i + 1).ToString());//删除finish文档
                                    //DeleteTrayTXT((i+1).ToString());//删除Tray文档
                                    DeleteTestTXT((i + 1).ToString());//删除Test文档
                                    DeletealarmTXT((i + 1).ToString());//删除alarm文档
                                    DeleteResetOKTXT((i + 1).ToString());//删除ResetOK文档
                                    DeleteResetTXT((i + 1).ToString());//删除Reset文档
                                    DeleteParameterTXT((i + 1).ToString());//删除Parameter文档
                                }

                                Variable.RestStep = 50;
                            }
                            break;
                        }
                    case 50:
                        {
                            if (Variable.RestEnable == true)
                            {
                                Variable.INAutoReadyRestStep = 0;
                                Variable.INAutoEmptyRestStep = 0;
                                Variable.OutAutoOKRestStep = 0;
                                Variable.OutAutoFillRestStep = 0;
                                Variable.OutAutoNGRestStep = 0;
                                Variable.RobotAutoRestStep = 0;
                                Variable.AutoStep = 0;
                                Variable.INAutoReady1Step = 0;
                                Variable.INAutoReadyStep = 0;
                                Variable.INAutoEmptyStartStep = 0;
                                Variable.OutAutoOKStartStep = 0;
                                Variable.OutAutoFillStartStep = 0;
                                Variable.OutAutoNGStartStep = 0;
                                Variable.RobotHomeStartStep = 0;
                                Variable.RobotAutoStartStep = 0;
                                Variable.RobotTCPAutoStep = 0;
                                Variable.RobotSetStep = 0;
                                Variable.RobotSet = 0;
                                Variable.RobotGet = 0;
                                Variable.RobotGetStep = 0;
                                Variable.ModelSetStep = 0;
                                Variable.ModelGetStep = 0;
                                Variable.TcpServerStartStep = 0;

                                Variable.RestStep = 60;
                            }
                            break;
                        }

                    case 60:
                        {
                            if (Variable.RestEnable == true)
                            {
                                Variable.INAutoReadyRestStep = 10;
                                Variable.INAutoEmptyRestStep = 10;
                                Variable.OutAutoOKRestStep = 10;
                                Variable.OutAutoFillRestStep = 10;
                                Variable.OutAutoNGRestStep = 10;
                                Variable.RobotAutoRestStep = 10;
                                Variable.ModelAutoRestStep = 10;

                                Variable.RestStep = 70;
                            }
                            break;
                        }
                    case 70:
                        {
                            if (Variable.RestEnable == true)
                            {
                                if (Variable.INAutoReadyRestStep == 200 && Variable.INAutoEmptyRestStep == 200 && Variable.OutAutoOKRestStep == 200 && Variable.OutAutoFillRestStep == 200 && Variable.OutAutoNGRestStep == 200 && Variable.RobotAutoRestStep == 200 && Variable.ModelAutoRestStep == 200)
                                {
                                    Variable.RestStep = 80;
                                }
                            }
                            break;
                        }

                    case 80:
                        {
                            if (Variable.RestEnable == true)
                            {
                                for (int i = 0; i < 10; i++)
                                {
                                    if (Variable.XStatus[117 + i * 32])
                                    {
                                        ListBoxTxt((i + 1).ToString() + "#上层内Tray盘未取走，请取走");
                                    }
                                    if (Variable.XStatus[121 + i * 32])
                                    {
                                        ListBoxTxt((i + 1).ToString() + "#上层外Tray盘未取走，请取走");
                                    }
                                    if (Variable.XStatus[131 + i * 32])
                                    {
                                        ListBoxTxt((i + 1).ToString() + "#下层内Tray盘未取走，请取走");
                                    }
                                    if (Variable.XStatus[135 + i * 32])
                                    {
                                        ListBoxTxt((i + 1).ToString() + "#下层外Tray盘未取走，请取走");
                                    }
                                }

                                if (Variable.XStatus[43])
                                {
                                    ListBoxTxt("上料待测1Tray盘未取走，请取走");
                                }
                                else if (Variable.XStatus[51])
                                {
                                    ListBoxTxt("上料待测3Tray盘未取走，请取走");
                                }
                                else if (Variable.XStatus[32])
                                {
                                    ListBoxTxt("上料空Tray盘未取走，请取走");
                                }
                                else if (Variable.XStatus[64])
                                {
                                    ListBoxTxt("下料良品Tray盘未取走，请取走");
                                }
                                else if (Variable.XStatus[78])
                                {
                                    ListBoxTxt("下料补料Tray盘未取走，请取走");
                                }
                                else if (Variable.XStatus[86])
                                {
                                    ListBoxTxt("下料NGTray盘未取走，请取走");
                                }
                                else
                                {
                                    Variable.RestStep = 90;
                                }
                            }
                            break;
                        }

                    case 90:
                        {
                            if (Variable.RestEnable == true)
                            {
                                TxtClear1(Application.StartupPath + @"\Data\UpEmpty\tray");//料盘赋空值
                                TxtClear1(Application.StartupPath + @"\Data\UpReady\tray");//料盘赋空值
                                TxtClear1(Application.StartupPath + @"\Data\DownOK\tray");//料盘赋空值
                                TxtClear1(Application.StartupPath + @"\Data\DownReadyOK\tray");//料盘赋空值
                                TxtClear1(Application.StartupPath + @"\Data\DownNG\tray");//料盘赋空值

                                for (int i = 0; i < 40; i++)
                                {
                                    TxtClear1(Application.StartupPath + @"\Map\" + (i + 1).ToString() + "\\tray");//料盘赋空值
                                    TxtClear0(Application.StartupPath + @"\Data\ModelUP\" + (i + 1).ToString() + "\\tray");//料盘赋空值
                                    //TxtClear0(Application.StartupPath + @"\Data\TCPModel\" + (i + 1).ToString() + "\\tray");//料盘赋空值
                                }


                                //初始化探针数组
                                for (int i = 0; i < 40; i++)
                                {
                                    for (int j = 0; j < 152; j++)
                                    {
                                        Variable.siteNGCount[i, j] = 0;
                                    }
                                }

                                Variable.RestStep = 100;
                            }
                            break;
                        }


                    case 100://初始化单机
                        {
                            if (Variable.RestEnable == true)
                            {
                                //写入参数
                                //str[0]批号，str[1]档案，str[2]测试时间，str[3]测试等待时间，str[4]测试超时，str[5]VCC电压，str[6]VCQ电压，str[7]VCQ选择，str[8]链接模式
                                string[] str = new string[14];
                                if (Variable.PGMcheck)
                                {
                                    string path = @"D:\test program\Test Program.txt";
                                    string[] lines = File.ReadAllLines(path);//"A29B01LB01"
                                    if (lines[1].Length > 7)
                                    {
                                        string path1 = lines[2];
                                        string path2 = @"D:\AP\BI\" + path1 + ".txt";
                                        if (File.Exists(path2))//存在
                                        {
                                            string[] strPGM = File.ReadAllLines(path2);

                                            if (strPGM.Length >= 12)
                                            {
                                                //str[0] = Variable.BatchNum;
                                                infoDataGrid.Rows[1].Cells[1].Value = lines[1];
                                                str[0] = lines[1];
                                                str[1] = strPGM[0];
                                                str[2] = strPGM[1];
                                                str[3] = strPGM[2];
                                                str[4] = strPGM[3];
                                                str[5] = strPGM[4];
                                                str[6] = strPGM[5];
                                                str[7] = strPGM[6];
                                                str[8] = strPGM[7];
                                                str[9] = Variable.testYield.ToString();
                                                str[10] = strPGM[8];
                                                str[11] = strPGM[9];
                                                str[11] = strPGM[10];
                                                str[12] = strPGM[11];
                                                // str[13] = strPGM[12];

                                                //BatchNum.Text = strPGM[0];
                                                Variable.ModelName = strPGM[0];
                                                Variable.TestTime = Convert.ToDouble(strPGM[1]);
                                                Variable.TestWaitTime = Convert.ToDouble(strPGM[2]);
                                                Variable.TestOutTime = Convert.ToDouble(strPGM[3]);
                                                Variable.VCCTestVol = Convert.ToDouble(strPGM[4]);
                                                Variable.VCQTestVol = Convert.ToDouble(strPGM[5]);
                                                Variable.temper = Convert.ToDouble(strPGM[8]);
                                                Variable.upTemper = Convert.ToDouble(strPGM[9]);
                                                Variable.TempUpLimit = Convert.ToDouble(strPGM[10]);
                                                Variable.TempDownLimit = Convert.ToDouble(strPGM[11]);

                                                if (strPGM[6] == "1")
                                                {
                                                    Variable.VCQcheck = true;
                                                }
                                                else
                                                {
                                                    Variable.VCQcheck = false;
                                                }
                                                if (strPGM[7] == "1")
                                                {
                                                    Variable.OnlineModcheck = true;
                                                }
                                                else
                                                {
                                                    Variable.OnlineModcheck = false;
                                                }
                                                if (strPGM[12] == "1")
                                                {
                                                    // HotModcheckBox.Checked = true;
                                                    Variable.HotModel = true;
                                                }
                                                else
                                                {
                                                    //HotModcheckBox.Checked = false;
                                                    Variable.HotModel = false;
                                                }


                                                //温度写入
                                                Variable.temWriteFlag = true;
                                                do
                                                {
                                                    if (!Variable.RestEnable)
                                                    {
                                                        break;
                                                    }
                                                } while (Variable.temReadFlag);
                                                Thread.Sleep(2000);
                                                btnUpTemper(Convert.ToInt32(strPGM[9]));
                                                btnTemper(Convert.ToInt32(strPGM[8]));
                                                btnUpLimitTemper(3);
                                                btnDownLimitTemper(3);
                                                Variable.temWriteFlag = false;

                                                string path10 = @"D:\参数\" + Variable.FileName;
                                                inIHelper.writeIni("PGM", "TempUpLimit", Variable.TempUpLimit.ToString(), path10);
                                                inIHelper.writeIni("PGM", "TempDownLimit", Variable.TempDownLimit.ToString(), path10);
                                                inIHelper.writeIni("PGM", "temper", Variable.temper.ToString(), path10);
                                                inIHelper.writeIni("PGM", "Uptemper", Variable.upTemper.ToString(), path10);
                                                inIHelper.writeIni("PGM", "HotModcheckBox", Variable.HotModel.ToString(), path10);

                                            }
                                            else
                                            {
                                                MessageBox.Show("D:\\AP下的参数数据不全");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("D:\\AP下不存在PGM下载的参数");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Test Program.txt 不存在烧录文件");
                                    }
                                }
                                else
                                {
                                    str[0] = Variable.BatchNum;
                                    str[1] = Variable.ModelName = Variable.FileName;
                                    str[2] = Variable.TestTime.ToString();
                                    str[3] = Variable.TestWaitTime.ToString();
                                    str[4] = Variable.TestOutTime.ToString();
                                    str[5] = Variable.VCCTestVol.ToString();
                                    str[6] = Variable.VCQTestVol.ToString();
                                    if (Variable.VCQcheck)
                                    {
                                        str[7] = "1";
                                    }
                                    else
                                    {
                                        str[7] = "0";
                                    }
                                    if (Variable.OnlineModcheck)
                                    {
                                        str[8] = "1";
                                    }
                                    else
                                    {
                                        str[8] = "0";
                                    }
                                    str[9] = Variable.temper.ToString();
                                    str[10] = Variable.upTemper.ToString();
                                    str[11] = Variable.TempUpLimit.ToString();
                                    str[12] = Variable.TempDownLimit.ToString();
                                    str[13] = Variable.testYield.ToString();
                                }
                                System.IO.File.WriteAllLines(Application.StartupPath + @"\Map\Parameter.txt", str);

                                for (int i = 0; i < 40; i++)
                                {
                                    //Thread.Sleep(500);
                                    string strPath0 = @"D:\Map\" + (i + 1).ToString();//共享文件夹
                                    string strPath1 = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\Map";//Test本地文件夹
                                    string destFn = "Parameter.txt";//要读取Handler共享文件夹下文件的名称
                                    string destFnNew = "Parameter.txt";//要读取Test文件夹下文件的名称

                                    string FullPath = strPath0 + "\\" + destFn;//共享
                                    string newPath = strPath1 + "\\" + destFnNew;//本地
                                    if (File.Exists(newPath))
                                    {
                                        if (File.Exists(FullPath))
                                        {
                                            File.Delete(FullPath);
                                        }
                                        File.Copy(newPath, FullPath);
                                    }
                                    else
                                    {

                                    }
                                }
                                Variable.RestStep = 110;

                            }
                            break;
                        }

                    case 110://发送Reset文档
                        {
                            if (Variable.RestEnable == true)
                            {
                                for (int i = 0; i < 40; i++)
                                {
                                    WriteResetTXT((i + 1).ToString());
                                }

                                Thread.Sleep(10000);
                                Variable.RestStep = 120;
                            }
                            break;
                        }
                    case 120://判断是否都有ResetOK文档
                        {
                            if (Variable.RestEnable == true)
                            {
                                bool flag = false;
                                int data = 0;
                                for (int i = 0; i < 40; i++)
                                {
                                    string strPath0 = @"D:\Map\" + (i + 1).ToString();//共享文件夹
                                    string destFn = "ResetOK.txt";//要读取Handler共享文件夹下文件的名称
                                    string FullPath = strPath0 + "\\" + destFn;//共享
                                    if (File.Exists(FullPath))
                                    {
                                        flag = true;
                                        Variable.PicBox[i / 4] = true;
                                    }
                                    else
                                    {
                                        flag = false;
                                        data = i / 4 + 1;
                                        Variable.PicBox[i / 4] = false;
                                        //break;
                                    }
                                    if (flag)
                                    {
                                        Variable.RestStep = 130;
                                    }
                                    else
                                    {
                                        ListBoxTxt(data.ToString() + "号测试机复位失败");
                                        if (MessageBox.Show(data.ToString() + "号测试机复位失败" + "选择'是'重试，选择'否'放弃！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            Variable.RestStep = 100;
                                            break;
                                        }
                                        else
                                        {
                                            Variable.RestStep = 100;
                                            break;
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    case 130:
                        {
                            if (Variable.RestEnable == true)
                            {
                                DataGridViewInitialize();
                                restStartFlag = false;
                                restOverFlag = true;
                                Variable.MachineState = Variable.MachineStatus.StandBy;
                                Variable.RestStep = 0;
                                this.listAlarm.Items.Clear();
                                ListBoxTxt("机台初始化完成");
                                function.ReturnZeroOff();
                            }
                            break;
                        }
                }
                Thread.Sleep(1);
            }
        }
        #endregion

        #region 上料空盘复位流程

        public void INAutoEmptyRest()
        {
            while (true)
            {
                switch (Variable.INAutoEmptyRestStep)
                {
                    case 10:
                        {
                            if (Variable.RestEnable == true)
                            {
                                function.OutYOFF(20);//支撑气缸回
                                Variable.INAutoEmptyRestStep = 20;
                            }
                            break;
                        }
                    case 20:
                        {
                            if (Variable.RestEnable == true)
                            {
                                if (Variable.XStatus[34])
                                {
                                    Axis5GoHome();//上料空Tray上顶Z轴回原点
                                    Variable.INAutoEmptyRestStep = 30;
                                }

                            }
                            break;
                        }

                    case 30:
                        {
                            if (Variable.RestEnable == true)
                            {
                                double pos = Variable.AxisPos[5, 0];
                                Axis5SetMove(pos); //轴5待测上顶1Z轴回待机位
                                if (Variable.AIMpos[5] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[5] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.INAutoEmptyRestStep = 40;
                                }

                            }
                            break;
                        }

                    case 40:
                        {
                            if (Variable.RestEnable == true)
                            {
                                if (Variable.INAutoReadyRestStep >= 30)
                                {
                                    Axis2GoHome(); //轴2上料空TrayY轴回原点
                                    Thread.Sleep(200);
                                    Variable.INAutoEmptyRestStep = 50;

                                }
                            }
                            break;
                        }

                    case 50:
                        {
                            if (Variable.RestEnable == true)
                            {
                                double pos = Variable.AxisPos[2, 0];
                                Axis2SetMove(pos); //轴2上料空TrayY轴移动待机位
                                if (Variable.AIMpos[2] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[2] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.INAutoEmptyRestStep = 60;
                                }

                            }
                            break;
                        }

                    case 60:
                        {
                            if (Variable.RestEnable == true)
                            {
                                function.OutYOFF(22);//工位3上顶气缸下降
                                function.OutYOFF(23);//运动平台夹紧气缸松开

                                Variable.INAutoEmptyRestStep = 70;
                            }
                            break;
                        }

                    case 70:
                        {
                            if (Variable.RestEnable == true)
                            {
                                if (Variable.XStatus[40] && !Variable.XStatus[42])
                                {
                                    Variable.INAutoEmptyRestStep = 200;
                                }
                            }
                            break;
                        }

                }
                Thread.Sleep(1);
            }

        }

        #endregion

        #region 上料待测复位流程
        public void INAutoReadyRest()
        {
            while (true)
            {
                switch (Variable.INAutoReadyRestStep)
                {
                    case 10:
                        {
                            if (Variable.RestEnable == true)
                            {
                                function.OutYOFF(33);//吸嘴1气缸上升
                                function.OutYOFF(34);//吸嘴2气缸上升
                                function.OutYOFF(25);//工位2侧顶气缸回位
                                function.OutYOFF(26);//工位2上下气缸下降
                                Thread.Sleep(200);
                                if (Variable.XStatus[48] && Variable.XStatus[49] && Variable.XStatus[59] && Variable.XStatus[61])
                                {
                                    Variable.INAutoReadyRestStep = 15;
                                }
                            }
                            break;
                        }

                    case 15:
                        {
                            if (Variable.RestEnable == true)
                            {
                                Axis4GoHome();//分料Z轴回原点
                                Variable.INAutoReadyRestStep = 20;
                            }
                            break;
                        }

                    case 20:
                        {
                            if (Variable.RestEnable == true)
                            {
                                double pos = Variable.AxisPos[4, 0];
                                Axis4SetMove(pos);//分料Z轴回待机
                                if (Variable.AIMpos[4] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.INAutoReadyRestStep = 30;
                                }
                            }
                            break;
                        }

                    case 30:
                        {
                            if (Variable.RestEnable == true)
                            {
                                Axis1GoHome();//分料X轴回原点
                                Variable.INAutoReadyRestStep = 40;
                            }
                            break;
                        }

                    case 40:
                        {
                            if (Variable.RestEnable == true)
                            {
                                if (Variable.XStatus[49])
                                {
                                    if (Variable.AIMpos[4] <= Math.Round(Variable.AxisPos[4, 0] + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(Variable.AxisPos[4, 0] - 0.1, 2) && Variable.XStatus[59] && Variable.XStatus[61])
                                    {
                                        double pos = Variable.AxisPos[1, 0];
                                        Axis1SetMove(pos); ;//分料X轴回待机
                                        if (Variable.AIMpos[1] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[1] >= Math.Round(pos - 0.1, 2))
                                        {
                                            Variable.XAlarmTime[1] = 0;
                                            Variable.XAlarmTime[4] = 0;
                                            Variable.INAutoReadyRestStep = 70;
                                        }
                                    }
                                    else
                                    {
                                        Variable.XAlarmTime[1] += 1;
                                        //Variable.UpAxisAlarm = true;
                                        //ListBoxTxt("上料吸嘴Z轴不在待机位或上料吸嘴气缸不在上位，请确认！");
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[4] += 1;
                                    //Variable.AlarmHappen[49] = true;
                                    //ListBoxTxt("上料待测Tray工位2上顶气缸不在下位，请确认！");
                                }
                            }
                            break;
                        }

                    case 70:
                        {
                            if (Variable.RestEnable == true)
                            {
                                function.OutYOFF(24);//支撑气缸1回
                                function.OutYOFF(27);//支撑气缸3回
                                Variable.INAutoReadyRestStep = 80;
                            }
                            break;
                        }
                    case 80:
                        {
                            if (Variable.RestEnable == true)
                            {
                                if (Variable.XStatus[45] && Variable.XStatus[53])
                                {
                                    Axis6GoHome();//待测上顶1Z轴回原点
                                    Variable.INAutoReadyRestStep = 90;
                                }

                            }
                            break;
                        }

                    case 90:
                        {
                            if (Variable.RestEnable == true)
                            {
                                double pos = Variable.AxisPos[6, 0];
                                Axis6SetMove(pos); //待测上顶1Z轴回待机位
                                if (Variable.AIMpos[6] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[6] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.INAutoReadyRestStep = 100;
                                }
                            }
                            break;
                        }

                    case 100:
                        {
                            if (Variable.RestEnable == true)
                            {
                                if (Variable.XStatus[53])
                                {
                                    Axis7GoHome();//待测上顶2Z轴回原点
                                    Variable.INAutoReadyRestStep = 110;
                                }

                            }
                            break;
                        }

                    case 110:
                        {
                            if (Variable.RestEnable == true)
                            {
                                double pos = Variable.AxisPos[7, 0];
                                Axis7SetMove(pos); //待测上顶2Z轴回待机位
                                if (Variable.AIMpos[7] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[7] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.INAutoReadyRestStep = 111;
                                }
                            }
                            break;
                        }

                    case 111:
                        {
                            if (Variable.RestEnable == true)
                            {
                                Axis3GoHome(); //轴3待测Y轴回原点
                                Thread.Sleep(200);
                                Variable.INAutoReadyRestStep = 112;

                            }
                            break;
                        }

                    case 112:
                        {
                            if (Variable.RestEnable == true)
                            {
                                double pos = Variable.AxisPos[3, 0];
                                Axis3SetMove(pos); //轴3上料待测Y轴移动待机位
                                if (Variable.AIMpos[3] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[3] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.INAutoReadyRestStep = 120;
                                }

                            }
                            break;
                        }

                    case 120:
                        {
                            if (Variable.RestEnable == true)
                            {
                                function.OutYOFF(29);//吸嘴1真空关闭
                                function.OutYOFF(30);//吸嘴2真空关闭
                                function.OutYOFF(31);//吸嘴1破真空关闭
                                function.OutYOFF(32);//吸嘴2破真空关闭
                                function.OutYOFF(28);//运动平台夹紧气缸松开
                                Variable.INAutoReadyRestStep = 130;
                            }
                            break;
                        }

                    case 130:
                        {
                            if (Variable.RestEnable == true)
                            {
                                if (!Variable.XStatus[56] && !Variable.XStatus[57] && !Variable.XStatus[58])
                                {
                                    Variable.UpReady1TrayOK = false;
                                    Variable.UpReady2TrayOK = false;
                                    Variable.INAutoReadyRestStep = 200;
                                }
                            }
                            break;
                        }
                }
                Thread.Sleep(1);
            }

        }

        #endregion

        #region 下料良品复位流程

        public void OutAutoOKRest()
        {
            while (true)
            {
                switch (Variable.OutAutoOKRestStep)
                {
                    case 10:
                        {
                            if (Variable.RestEnable == true)
                            {
                                function.OutYOFF(66);//吸嘴1气缸上升
                                function.OutYOFF(67);//吸嘴2气缸上升
                                function.OutYOFF(52);//工位1上顶气缸回位
                                function.OutYOFF(53);//工位2侧顶气缸回位
                                function.OutYOFF(54);//工位2上顶气缸回位
                                Thread.Sleep(100);
                                if (Variable.XStatus[68] && Variable.XStatus[71] && Variable.XStatus[72])
                                {
                                    Variable.OutAutoOKRestStep = 15;
                                }
                            }
                            break;
                        }

                    case 15:
                        {
                            if (Variable.RestEnable == true)
                            {
                                Axis13GoHome();//分料Z轴回原点
                                Variable.OutAutoOKRestStep = 20;
                            }
                            break;
                        }

                    case 20:
                        {
                            if (Variable.RestEnable == true)
                            {
                                double pos = Variable.AxisPos[13, 0];
                                Axis13SetMove(pos);//分料Z轴回待机
                                if (Variable.AIMpos[13] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.OutAutoOKRestStep = 30;
                                }
                            }
                            break;
                        }

                    case 30:
                        {
                            if (Variable.RestEnable == true)
                            {
                                Axis9GoHome();//分料X轴回原点
                                Thread.Sleep(200);
                                Variable.OutAutoOKRestStep = 40;
                            }
                            break;
                        }

                    case 40:
                        {
                            if (Variable.RestEnable == true)
                            {
                                if (Variable.XStatus[72])
                                {
                                    if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                    {
                                        double pos = Variable.AxisPos[9, 0];
                                        Axis9SetMove(pos);//分料X轴回待机
                                        if (Variable.AIMpos[9] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[9] >= Math.Round(pos - 0.1, 2))
                                        {
                                            Variable.XAlarmTime[2] = 0;
                                            Variable.XAlarmTime[3] = 0;
                                            Variable.OutAutoOKRestStep = 50;
                                        }
                                    }
                                    else
                                    {
                                        Variable.XAlarmTime[2] += 1;
                                        //Variable.DownAxisAlarm = true;
                                        //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[3] += 1;
                                    //Variable.AlarmHappen[72] = true;
                                    //ListBoxTxt("下料良品Tray盘工位2上顶气缸不在下位，请确认！");
                                }
                            }
                            break;
                        }

                    case 50:
                        {
                            if (Variable.RestEnable == true)
                            {
                                Axis10GoHome(); //轴10下料良品Y轴回原点
                                Thread.Sleep(200);
                                Variable.OutAutoOKRestStep = 60;

                            }
                            break;
                        }

                    case 60:
                        {
                            if (Variable.RestEnable == true)
                            {
                                double pos = Variable.AxisPos[10, 0];
                                Axis10SetMove(pos); //轴1下料良品Y轴移动待机位
                                if (Variable.AIMpos[10] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[10] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.OutAutoOKRestStep = 70;
                                }
                            }
                            break;
                        }

                    case 70:
                        {
                            if (Variable.RestEnable == true)
                            {
                                function.OutYOFF(62);//吸嘴1真空关闭
                                function.OutYOFF(63);//吸嘴2真空关闭
                                function.OutYOFF(64);//吸嘴1破真空关闭
                                function.OutYOFF(65);//吸嘴2破真空关闭
                                function.OutYOFF(55);//运动平台夹紧气缸松开

                                Variable.OutAutoOKRestStep = 80;
                            }
                            break;
                        }

                    case 80:
                        {
                            if (Variable.RestEnable == true)
                            {
                                if (!Variable.XStatus[96] && !Variable.XStatus[97] && !Variable.XStatus[77])
                                {
                                    Variable.OutAutoOKRestStep = 200;
                                }
                            }
                            break;
                        }
                }
                Thread.Sleep(1);
            }

        }

        #endregion

        #region 下料补料复位流程

        public void OutAutoFillRest()
        {
            while (true)
            {
                switch (Variable.OutAutoFillRestStep)
                {
                    case 10:
                        {
                            if (Variable.RestEnable == true)
                            {
                                function.OutYOFF(69);//移载夹爪气缸松开                               
                                Variable.OutAutoFillRestStep = 20;
                            }
                            break;
                        }

                    case 20:
                        {
                            if (Variable.RestEnable == true)
                            {
                                if (Variable.XStatus[104])
                                {
                                    function.OutYOFF(68);//移载上下气缸上升
                                    Variable.OutAutoFillRestStep = 30;
                                }

                            }
                            break;
                        }

                    case 30:
                        {
                            if (Variable.RestEnable == true)
                            {
                                if (Variable.XStatus[102])
                                {
                                    Axis15GoHome();//移载X轴回原点
                                    Thread.Sleep(200);
                                    Variable.OutAutoFillRestStep = 40;
                                }

                            }
                            break;
                        }

                    case 40:
                        {
                            if (Variable.RestEnable == true)
                            {
                                double pos = Variable.AxisPos[15, 0];
                                Axis15SetMove(pos);//移载X轴回待机
                                if (Variable.AIMpos[15] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[15] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.OutAutoFillRestStep = 50;
                                }
                            }
                            break;
                        }

                    case 50:
                        {
                            if (Variable.RestEnable == true)
                            {
                                Axis11GoHome(); //轴11下料备品Y轴回原点
                                Thread.Sleep(200);
                                Variable.OutAutoFillRestStep = 60;
                            }
                            break;
                        }

                    case 60:
                        {
                            if (Variable.RestEnable == true)
                            {
                                double pos = Variable.AxisPos[11, 0];
                                Axis11SetMove(pos); //轴11下料备品Y轴移动待机位
                                if (Variable.AIMpos[11] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[11] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.OutAutoFillRestStep = 70;
                                }
                            }
                            break;
                        }

                    case 70:
                        {
                            if (Variable.RestEnable == true)
                            {
                                function.OutYOFF(56);//工位1上顶气缸回位
                                function.OutYOFF(57);//工位2侧顶气缸回位
                                function.OutYOFF(58);//运动平台夹紧气缸松开
                                Variable.OutAutoFillRestStep = 80;
                            }
                            break;
                        }

                    case 80:
                        {
                            if (Variable.RestEnable == true)
                            {
                                if (Variable.XStatus[81] && Variable.XStatus[82] && !Variable.XStatus[85])
                                {
                                    Variable.OutAutoFillRestStep = 200;
                                }
                            }
                            break;
                        }
                }
                Thread.Sleep(1);
            }

        }

        #endregion

        #region 下料NG复位流程
        public void OutAutoNGRest()
        {
            while (true)
            {
                switch (Variable.OutAutoNGRestStep)
                {
                    case 10:
                        {
                            if (Variable.RestEnable == true)
                            {
                                function.OutYOFF(59);//支撑气缸回
                                Variable.OutAutoNGRestStep = 20;
                            }
                            break;
                        }
                    case 20:
                        {
                            if (Variable.RestEnable == true)
                            {
                                if (Variable.XStatus[88])
                                {
                                    Axis14GoHome();//下料NG空Tray上顶Z轴回原点
                                    Thread.Sleep(200);
                                    Variable.OutAutoNGRestStep = 30;
                                }

                            }
                            break;
                        }

                    case 30:
                        {
                            if (Variable.RestEnable == true)
                            {
                                double pos = Variable.AxisPos[14, 0];
                                Axis14SetMove(pos); //下料空Tray上顶Z轴回待机位
                                if (Variable.AIMpos[14] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[14] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.OutAutoNGRestStep = 40;
                                }
                            }
                            break;
                        }

                    case 40:
                        {
                            if (Variable.RestEnable == true)
                            {
                                if (Variable.OutAutoOKRestStep >= 30)
                                {
                                    Axis12GoHome(); //轴12下料NG盘Y轴回原点
                                    Thread.Sleep(200);
                                    Variable.OutAutoNGRestStep = 50;
                                }
                            }
                            break;
                        }

                    case 50:
                        {
                            if (Variable.RestEnable == true)
                            {
                                double pos = Variable.AxisPos[12, 0];
                                Axis12SetMove(pos); //轴12下料NG盘Y轴移动待机位
                                if (Variable.AIMpos[12] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[12] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.OutAutoNGRestStep = 60;
                                }
                            }
                            break;
                        }

                    case 60:
                        {
                            if (Variable.RestEnable == true)
                            {
                                function.OutYOFF(60);//工位3上顶气缸回位
                                function.OutYOFF(61);//运动平台夹紧气缸松开

                                Variable.OutAutoNGRestStep = 70;
                            }
                            break;
                        }

                    case 70:
                        {
                            if (Variable.RestEnable == true)
                            {
                                if (Variable.XStatus[93] && !Variable.XStatus[95])
                                {
                                    Variable.OutAutoNGRestStep = 200;
                                }
                            }
                            break;
                        }

                }
                Thread.Sleep(1);
            }

        }

        #endregion

        #region Robot复位流程
        public void RobotAutoRest()
        {
            while (true)
            {
                switch (Variable.RobotAutoRestStep)
                {

                    case 10://判断机械手在不在自动状态
                        {
                            if (Variable.RestEnable == true)
                            {
                                if (Variable.XStatus[25])//机械手自动模式
                                {
                                    Variable.RobotAutoRestStep = 20;
                                }
                                else
                                {
                                    ListBoxTxt("机械手不在自动模式下");
                                    //Variable.RobotAutoRestStep = 0;
                                }
                                //Variable.RobotAutoRestStep = 200;
                            }
                            break;
                        }
                    case 20:
                        {
                            if (Variable.RestEnable == true)
                            {
                                if (Variable.XStatus[26])//判断是否有急停报警
                                {
                                    function.OutYON(15);//机械手急停复位
                                    Thread.Sleep(1000);
                                    function.OutYOFF(15);
                                }

                                Variable.RobotAutoRestStep = 30;
                            }
                            break;
                        }
                    case 30://判断是否有急停报警
                        {
                            if (Variable.RestEnable == true)
                            {
                                Thread.Sleep(1000);
                                if (!Variable.XStatus[26])
                                {
                                    Variable.RobotAutoRestStep = 40;
                                }
                                else
                                {
                                    ListBoxTxt("机械手急停报警");
                                    //Variable.RobotAutoRestStep = 0;
                                }
                            }
                            break;
                        }
                    case 40://报警消除
                        {
                            if (Variable.RestEnable == true)
                            {
                                if (Variable.XStatus[22])//报警未消除
                                {
                                    function.OutYON(16);
                                    Thread.Sleep(1000);
                                    function.OutYOFF(16);
                                }
                                Variable.RobotAutoRestStep = 50;
                            }
                            break;
                        }
                    case 50:
                        {
                            if (Variable.RestEnable == true)
                            {
                                Thread.Sleep(1000);
                                if (!Variable.XStatus[22])
                                {
                                    Variable.RobotAutoRestStep = 60;
                                }
                                else
                                {
                                    ListBoxTxt("机械手报警未消除");
                                    //Variable.RobotAutoRestStep = 0;
                                }
                            }
                            break;
                        }
                    case 60://上电
                        {
                            if (Variable.RestEnable == true)
                            {
                                if (!Variable.XStatus[27])
                                {
                                    function.OutYON(13);//机械手上电
                                    Thread.Sleep(1000);
                                    function.OutYOFF(13);
                                }
                                Variable.RobotAutoRestStep = 70;
                            }
                            break;
                        }
                    case 70://判断是否上电
                        {
                            if (Variable.RestEnable == true)
                            {
                                Thread.Sleep(1000);
                                if (Variable.XStatus[27])
                                {
                                    Variable.RobotAutoRestStep = 75;
                                }
                                else
                                {
                                    ListBoxTxt("机械手未上电");
                                    //Variable.RobotAutoRestStep = 0;
                                }
                            }
                            break;
                        }

                    case 75://停止
                        {
                            if (Variable.RestEnable == true)
                            {
                                function.OutYON(17);//机械手停止
                                Thread.Sleep(1000);
                                function.OutYOFF(17);
                                Variable.RobotAutoRestStep = 80;
                            }
                            break;
                        }

                    case 80://机械手复位启动
                        {
                            if (Variable.RestEnable == true)
                            {
                                function.OutYON(14);//机械手复位启动
                                Thread.Sleep(1000);
                                function.OutYOFF(14);
                                Variable.RobotAutoRestStep = 90;
                            }
                            break;
                        }

                    case 90:
                        {
                            Variable.RobotRecOK = false;
                            Variable.RobotSendStr = "999";//告知机械手回安全位
                            Variable.messageRecord[0] = "机械手回原点中，代码为:999";
                            Variable.RobotHomeStartStep = 999;
                            Variable.RobotAutoRestStep = 100;

                            break;
                        }

                    case 100:
                        {
                            if (Variable.RestEnable == true)
                            {
                                Thread.Sleep(2000);
                                if (Variable.RobotRecOK || Variable.RobotResetNG)//机械手回安全位
                                {
                                    Variable.messageRecord[0] = "";
                                    Variable.RobotAutoRestStep = 105;
                                }
                                else
                                {
                                    ListBoxTxt("机械手和PC通信失败");
                                    //Variable.RobotAutoRestStep = 0;
                                }
                            }
                            break;
                        }

                    case 105:
                        {
                            if (Variable.RestEnable == true)
                            {
                                if (Variable.RobotRecOK && !Variable.RobotResetNG)
                                {
                                    Variable.RobotAutoRestStep = 110;
                                }
                                else if (Variable.RobotResetNG)
                                {
                                    function.OutYON(10);//机械手夹爪打开
                                    Thread.Sleep(500);
                                    function.OutYOFF(10);
                                    Variable.RobotAutoRestStep = 106;
                                }
                            }
                            break;
                        }

                    case 106:
                        {
                            if (Variable.RestEnable == true)
                            {
                                if (MessageBox.Show("确认机械手夹爪上Tray盘是否取走?选择'是'已取走，选择'否'未取走！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    Variable.RobotAutoRestStep = 110;
                                }

                            }
                            break;
                        }

                    case 110:
                        {
                            if (Variable.RestEnable == true)
                            {
                                Axis16GoHome(); //轴16机械手轴回原点
                                Variable.RobotAutoRestStep = 120;
                            }
                            break;
                        }

                    case 120:
                        {
                            if (Variable.RestEnable == true)
                            {
                                double pos = Variable.AxisPos[16, 0];
                                Axis16SetMove(pos); //轴16机械手轴移动待机位
                                if (Variable.AIMpos[16] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[16] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.RobotAutoRestStep = 130;
                                }
                            }
                            break;
                        }
                    case 130:
                        {
                            if (Variable.RestEnable == true)
                            {
                                Variable.RobotAutoRestStep = 200;
                            }
                            break;
                        }

                }
                Thread.Sleep(1);
            }

        }

        #endregion

        #region 单机复位
        public void ModelAutoRest()
        {
            while (true)
            {
                switch (Variable.ModelAutoRestStep)
                {
                    case 10:
                        {
                            if (Variable.RestEnable == true)
                            {
                                for (int i = 0; i < 20; i++)
                                {
                                    function.OutYOFF(108 + i * 16);//测试板断电
                                    function.OutYOFF(110 + i * 16);//测试板断电  
                                }
                                Variable.ModelAutoRestStep = 20;
                            }
                            break;
                        }
                    case 20:
                        {
                            if (Variable.RestEnable == true)
                            {
                                for (int i = 0; i < 20; i++)
                                {
                                    function.OutYOFF(109 + i * 16);//加热关闭
                                    function.OutYOFF(111 + i * 16);//加热关闭  
                                }
                                Variable.ModelAutoRestStep = 30;
                            }
                            break;
                        }
                    case 30:
                        {
                            if (Variable.RestEnable == true)
                            {
                                for (int i = 0; i < 20; i++)
                                {
                                    function.OutYOFF(102 + i * 16);//上顶1气缸上升  
                                    function.OutYON(101 + i * 16);//上顶1气缸下降

                                    function.OutYOFF(104 + i * 16);//上顶2气缸上升  
                                    function.OutYON(103 + i * 16);//上顶2气缸下降    
                                }
                                bool flag = false;
                                for (int i = 0; i < 10; i++)
                                {
                                    if (Variable.XStatus[118 + i * 32] && Variable.XStatus[122 + i * 32] && Variable.XStatus[132 + i * 32] && Variable.XStatus[136 + i * 32])
                                    {
                                        flag = true;
                                    }
                                    else
                                    {
                                        flag = false;
                                        break;
                                    }
                                }
                                if (flag)
                                {
                                    Variable.ModelAutoRestStep = 40;
                                }
                            }
                            break;
                        }

                    case 40:
                        {
                            if (Variable.RestEnable == true)
                            {
                                for (int i = 0; i < 20; i++)
                                {
                                    function.OutYOFF(105 + i * 16);//侧定位气缸回       
                                }

                                bool flag = false;
                                for (int i = 0; i < 10; i++)
                                {
                                    if (!Variable.XStatus[124 + i * 32] && !Variable.XStatus[125 + i * 32] && !Variable.XStatus[138 + i * 32] && !Variable.XStatus[139 + i * 32])
                                    {
                                        flag = true;
                                    }
                                    else
                                    {
                                        flag = false;
                                        break;
                                    }
                                }
                                if (flag)
                                {
                                    Variable.ModelAutoRestStep = 50;
                                }
                            }
                            break;
                        }

                    case 50:
                        {
                            if (Variable.RestEnable == true)
                            {
                                for (int i = 0; i < 20; i++)
                                {
                                    function.OutYOFF(106 + i * 16);//推Tray气缸回       
                                }

                                bool flag = false;
                                for (int i = 0; i < 10; i++)
                                {
                                    if (Variable.XStatus[126 + i * 32] && Variable.XStatus[140 + i * 32])
                                    {
                                        flag = true;
                                    }
                                    else
                                    {
                                        flag = false;
                                        break;
                                    }
                                }
                                if (flag)
                                {
                                    Variable.ModelAutoRestStep = 200;
                                }
                            }
                            break;
                        }
                }
                Thread.Sleep(1);
            }
        }

        #endregion

        #endregion

        #region **********自动流程**********

        //this.Dispatcher.Invoke(new Action(() => { }));
        //this.Dispatcher.Invoke(new Action(delegate { }));
        //System.Drawing.Color color = this.Dispatcher.Invoke(new Func<System.Drawing.Color>(() => {
        //return LeftFly2GridView.Rows[i].Cells[0].Style.BackColor;
        //}));

        #region 自动流程
        public void Auto()
        {
            while (true)
            {
                switch (Variable.AutoStep)
                {
                    case 10:
                        {
                            if (Variable.RunEnable == true)
                            {
                                Variable.startTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");//开始时间
                                Variable.BatchNum = "";
                                Variable.inTrayNumSet = "";
                                mes_OPNO = "";
                                mes_CURQTY = "";
                                if (Variable.HotModel)
                                {
                                    HotOpen();//加热打开
                                    Variable.AutoStep = 20;
                                }
                                else
                                {
                                    Variable.AutoStep = 20;
                                }
                            }
                            break;
                        }
                    case 20://弹窗输入Lot号和盘数
                        {
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.MEScheck)
                                {
                                    if (!Variable.info)
                                    {
                                        Variable.info = true;
                                        inform.ShowDialog();
                                    }

                                    if (!string.IsNullOrEmpty(Variable.BatchNum) && Convert.ToInt32(Variable.inTrayNumSet) > 0)
                                    {
                                        string path = @"D:\test program\Test Program.txt";
                                        string[] lines = File.ReadAllLines(path);//"A29B01LB01"
                                        if (lines[1].Substring(0, 7) == Variable.BatchNum.Substring(0, 7))
                                        {
                                            Variable.AutoStep = 30;
                                        }
                                        else
                                        {
                                            MessageBox.Show("请检查产品批次是否相同!");
                                        }
                                    }
                                }
                                else
                                {
                                    Variable.AutoStep = 60;
                                }
                            }
                            break;
                        }
                    case 30:
                        {
                            if (Variable.RunEnable == true)
                            {
                                bool flag = MesCall(Variable.BatchNum);
                                if (flag)
                                {
                                    Variable.AutoStep = 40;
                                }
                                else
                                {
                                    Variable.AutoStep = 20;
                                    MessageBox.Show("请检查产品数量，重新输入产品数量!");
                                }
                            }
                            break;
                        }
                    case 40://进站类别
                        {
                            if (Variable.RunEnable == true)
                            {
                                if (!string.IsNullOrEmpty(mes_OPNO) && !string.IsNullOrEmpty(mes_CURQTY))
                                {
                                    if (mes_OPNO.Substring(mes_OPNO.Length - 1, 1) == "0" || mes_OPNO.Substring(mes_OPNO.Length - 1, 1) == "1")
                                    {
                                        Variable.AutoStep = 50;
                                    }
                                    else
                                    {
                                        Variable.AutoStep = 20;
                                        MessageBox.Show("检查产品是否进站Burn in!");
                                    }
                                }
                                else
                                {
                                    Variable.AutoStep = 20;
                                    MessageBox.Show("检查产品是否进站Burn in!");
                                }
                            }
                            break;
                        }
                    case 50://盘数
                        {
                            if (Variable.RunEnable == true)
                            {
                                if (Convert.ToDouble(Variable.inTrayNumSet) < ((Convert.ToDouble(mes_CURQTY) / 152) + 3) && Convert.ToDouble(Variable.inTrayNumSet) > ((Convert.ToDouble(mes_CURQTY) / 152) - 3))
                                {
                                    Variable.AutoStep = 60;
                                }
                                else
                                {
                                    Variable.AutoStep = 20;
                                    MessageBox.Show("Tray盘数量设定错误!");
                                }
                            }
                            break;
                        }
                    case 60:
                        {
                            if (Variable.RunEnable == true)
                            {
                                Variable.INAutoReady1Step = 10;
                                Variable.INAutoReadyStep = 10;
                                Variable.INAutoEmptyStartStep = 10;
                                Variable.OutAutoOKStartStep = 10;
                                Variable.OutAutoFillStartStep = 10;
                                Variable.OutAutoNGStartStep = 10;
                                Variable.RobotAutoStartStep = 10;

                                Variable.AutoStep = 200;
                            }
                            break;
                        }
                }
                Thread.Sleep(1);
            }

        }
        #endregion

        #region 上料空盘自动流程
        public void INAutoEmptyStart()
        {
            while (true)
            {
                switch (Variable.INAutoEmptyStartStep)
                {
                    //***************上料上空Tray盘***************

                    case 10:
                        {
                            RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "10-->上料空Tray工位1是否有Tray");
                            if (Variable.RunEnable == true)//工位1有无料
                            {
                                if (!Variable.XStatus[32])
                                {
                                    Variable.INAutoEmptyStartStep = 20;//有料
                                }
                                else
                                {
                                    ListBoxTxt("上料空Tray工位1有Tray盘,请取走");
                                    Variable.INAutoEmptyStartStep = 15;
                                    RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "15-->上料空Tray工位1有Tray盘,请取走");
                                }
                            }
                            break;
                        }

                    case 20:
                        {
                            RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "20-->上料空Tray料盘准备OK");
                            if (Variable.RunEnable == true)
                            {
                                if (!Variable.UpNullTrayOK)
                                {
                                    //Variable.UpNullTrayOK = false;//料盘准备OK
                                    double pos = Variable.AxisPos[5, 1];
                                    Axis5SetMove(pos); //轴5上料空Tray上顶Z轴上顶
                                    if (Variable.AIMpos[5] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[5] >= Math.Round(pos - 0.1, 2))
                                    {
                                        Variable.AxisAlarmTime[5] = 0;
                                        Variable.INAutoEmptyStartStep = 30;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[5] += 1;
                                    }
                                }
                            }
                            break;
                        }

                    case 30:
                        {
                            RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "30-->轴2空TrayY移动到取空Tray位");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[2, 1];
                                Axis2SetMove(pos); //轴2空TrayY移动到取空Tray位
                                if (Variable.AIMpos[2] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[2] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[2] = 0;
                                    Variable.INAutoEmptyStartStep = 35;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[2] += 1;
                                }
                            }
                            break;
                        }

                    case 35:
                        {
                            RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "35-->上料空Tray支撑气缸出");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(20);//支撑气缸出
                                if (Variable.XStatus[35])
                                {
                                    Variable.INAutoEmptyStartStep = 40;
                                }
                            }
                            break;
                        }
                    case 40:
                        {
                            RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "40-->轴15上料空Tray上顶Z轴下降");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[5, 2];
                                Axis5SetMove(pos); //轴15上料空Tray上顶Z轴下降
                                if (Variable.AIMpos[5] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[5] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[5] = 0;
                                    Variable.INAutoEmptyStartStep = 50;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[5] += 1;
                                }
                            }
                            break;
                        }

                    case 50:
                        {
                            RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "50-->支撑气缸回");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(20);//支撑气缸回
                                if (Variable.XStatus[34])
                                {
                                    Thread.Sleep(200);
                                    Variable.INAutoEmptyStartStep = 55;
                                }
                            }
                            break;
                        }

                    case 55:
                        {
                            RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "55-->轴15上料空Tray上顶Z轴回待机位");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[5, 0];
                                Axis5SetMove(pos); //轴15上料空Tray上顶Z轴回待机位
                                if (Variable.AIMpos[5] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[5] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[5] = 0;
                                    Variable.INAutoEmptyStartStep = 60;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[5] += 1;
                                }
                            }
                            break;
                        }

                    case 60:
                        {
                            RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "60-->上料空Tray工位1有无盘感应");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[32])//有无料感应
                                {
                                    Variable.INAutoEmptyStartStep = 80;
                                }
                                else
                                {
                                    Variable.INAutoEmptyStartStep = 70;
                                }
                            }
                            break;
                        }

                    case 70:
                        {
                            RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "70-->上料空Tray工位1没有Tray盘");
                            if (Variable.RunEnable == true)
                            {
                                ListBoxTxt("上料空Tray工位1没有Tray盘");
                                Variable.INAutoEmptyStartStep = 75;
                                RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "75-->上料空Tray工位1没有Tray盘");
                            }
                            break;
                        }

                    case 80:
                        {
                            RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "80-->轴15上料空Tray上顶Z轴回待机位");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[5, 0];
                                Axis5SetMove(pos); //轴15上料空Tray上顶Z轴回待机位
                                if (Variable.AIMpos[5] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[5] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[5] = 0;
                                    Variable.INAutoEmptyStartStep = 90;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[5] += 1;
                                }
                            }
                            break;
                        }

                    case 90:
                        {
                            RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "90-->上料空Tray轨道平台夹紧气缸夹紧");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(23);//轨道平台夹紧气缸夹紧
                                if (Variable.XStatus[42])
                                {
                                    Variable.INAutoEmptyStartStep = 100;
                                }
                            }
                            break;
                        }

                    case 100:
                        {
                            RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "100-->上料空TrayY移动到待机位");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[2, 0];
                                Axis2SetMove(pos); //空TrayY移动到待机位
                                if (Variable.AIMpos[2] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[2] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[2] = 0;
                                    Variable.INAutoEmptyStartStep = 110;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[2] += 1;
                                }
                            }
                            break;
                        }

                    case 110:
                        {
                            RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "110-->上料空Tray轨道平台夹紧气缸松开");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(23);//轨道平台夹紧气缸松开
                                if (!Variable.XStatus[42])
                                {
                                    Thread.Sleep(100);
                                    Variable.INAutoEmptyStartStep = 120;
                                }
                            }
                            break;
                        }

                    case 120:
                        {
                            RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "120-->上料空Tray轨道平台夹紧气缸夹紧");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(23);//轨道平台夹紧气缸夹紧
                                if (Variable.XStatus[42])
                                {
                                    Thread.Sleep(100);
                                    Variable.INAutoEmptyStartStep = 130;
                                }
                            }
                            break;
                        }

                    case 130:
                        {
                            RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "130-->上料空TrayY移动到开始位");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[2, 3];
                                Axis2SetMove(pos); //空TrayY移动到开始位
                                if (Variable.AIMpos[2] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[2] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[2] = 0;
                                    Variable.INAutoEmptyStartStep = 150;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[2] += 1;
                                }
                            }
                            break;
                        }

                    //***************上料空Tray盘准备好，等待放NG料***************

                    case 150:
                        {
                            RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "150-->上料空Tray料盘准备OK");
                            if (Variable.RunEnable == true)
                            {
                                Variable.UpNullTrayOK = true;//料盘准备OK
                                Variable.UpNullTrayFull = false;
                                TxtClear1(Application.StartupPath + @"\Data\UpEmpty\tray");//料盘赋空值
                                Variable.INAutoEmptyStartStep = 160;
                            }
                            break;
                        }
                    case 160:
                        {
                            RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "160-->判断上料空Tray是否放满料");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.UpNullTrayFull)//空料盘放满料
                                {
                                    Variable.INAutoEmptyStartStep = 170;
                                }
                                if (Variable.OutAutoOKStartStep == 1000 && (Variable.CleanOne || Variable.CleanOut))//结批中
                                {
                                    Variable.INAutoEmptyStartStep = 170;
                                }
                            }
                            break;
                        }

                    //***************上料空Tray盘放满NG料***************

                    case 170:
                        {
                            RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "170-->轴2空TrayY移动到放满Tray位");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[2, 2];
                                Axis2SetMove(pos); //轴2空TrayY移动到放满Tray位
                                if (Variable.AIMpos[2] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[2] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[2] = 0;
                                    Variable.INAutoEmptyStartStep = 180;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[2] += 1;
                                }
                            }
                            break;
                        }

                    case 180:
                        {
                            RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "180-->上料空Tray轨道平台夹紧气缸松开");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(23);//轨道平台夹紧气缸松开
                                if (!Variable.XStatus[42])
                                {
                                    Variable.INAutoEmptyStartStep = 190;
                                }
                            }
                            break;
                        }

                    case 190:
                        {
                            RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "190-->上料空Tray工位3上顶气缸上顶");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(22);//工位3上顶气缸上顶
                                if (Variable.XStatus[39])
                                {
                                    Thread.Sleep(200);
                                    Variable.INAutoEmptyStartStep = 200;
                                }
                            }
                            break;
                        }

                    case 200:
                        {
                            RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "200-->上料空Tray工位3上顶气缸下降");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(22);//工位3上顶气缸下降
                                if (Variable.XStatus[40])
                                {
                                    Variable.INAutoEmptyStartStep = 210;
                                }
                            }
                            break;
                        }

                    case 210:
                        {
                            RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "210-->上料空Tray工位3是否有盘感应");
                            if (Variable.RunEnable == true)
                            {
                                if (!Variable.XStatus[37])
                                {
                                    Variable.INAutoEmptyStartStep = 211;
                                }
                                else
                                {
                                    ListBoxTxt("上料空Tray工位3有料感应，请取走！");
                                    Variable.INAutoEmptyStartStep = 212;
                                    RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "212-->上料空Tray工位3有料感应，请取走！");
                                }
                            }
                            break;
                        }
                    case 211:
                        {
                            RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "211-->上料空Tray轴2空TrayY移动到待机位");
                            if (Variable.RunEnable == true)
                            {
                                Variable.UpNullTrayOK = false;
                                double pos = Variable.AxisPos[2, 0];
                                Axis2SetMove(pos); //轴2空TrayY移动到待机位
                                if (Variable.AIMpos[2] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[2] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[2] = 0;
                                    Variable.INAutoEmptyStartStep = 215;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[2] += 1;
                                }
                            }
                            break;
                        }
                    case 215:
                        {
                            RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "215-->上料空Tray是否结批");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.OutAutoOKStartStep == 1000 && (Variable.CleanOne || Variable.CleanOut))//结批中
                                {
                                    Variable.INAutoEmptyStartStep = 500;
                                }
                                else
                                {
                                    Variable.INAutoEmptyStartStep = 220;
                                }
                            }
                            break;
                        }
                    case 220:
                        {
                            RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "220-->上料空Tray工位3Tray盘是否已满");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[38])
                                {
                                    ListBoxTxt("上料空Tray工位3Tray盘已满，请取走");
                                    Variable.INAutoEmptyStartStep = 225;
                                    RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "225-->上料空Tray工位3Tray盘已满，请取走");
                                }
                                else
                                {
                                    Variable.INAutoEmptyStartStep = 10;
                                }
                            }
                            break;
                        }

                    case 230:
                        {
                            RSDAlarmINAutoEmptyStep(Variable.AutoStepMsg[1] = "230-->上料空Tray返回第一步");
                            if (Variable.RunEnable == true)
                            {
                                if ((Variable.AlarmClrButton || Variable.btnReset == true))
                                {
                                    Variable.INAutoEmptyStartStep = 10;
                                }
                            }
                            break;
                        }
                }
                Thread.Sleep(1);
            }
        }
        #endregion

        #region 上料待测分料流程
        public void INAutoReady1Start()
        {
            while (true)
            {
                switch (Variable.INAutoReady1Step)
                {
                    case 10:
                        {
                            RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "10-->上料待测Tray工位1Tray盘是否超过数量");
                            if (Variable.RunEnable == true)
                            {
                                if (!Variable.XStatus[44])//工位1满盘
                                {
                                    Variable.INAutoReady1Step = 15;
                                }
                                else
                                {
                                    ListBoxTxt("上料待测Tray工位1Tray盘超过数量,请取走");
                                    Variable.INAutoReady1Step = 11;
                                    RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "11-->上料待测Tray工位1Tray盘超过数量,请取走");
                                }
                            }
                            break;
                        }

                    case 15:
                        {
                            RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "15-->上料待测Tray工位1有无料");
                            if (Variable.RunEnable == true)
                            {
                                if (!Variable.XStatus[43])//工位1无料
                                {
                                    Variable.INAutoReady1Step = 20;//无料 
                                }
                                else
                                {
                                    ListBoxTxt("上料待测Tray工位1有Tray盘,请取走");
                                    Variable.INAutoReady1Step = 16;
                                    RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "16-->上料待测Tray工位1有Tray盘,请取走");
                                }
                            }
                            break;
                        }

                    case 20:
                        {
                            RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "20-->上料待测轴3待测Y移动到工位1");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.UpReady1TrayOK == false)
                                {
                                    if (!Variable.CleanOne)
                                    {
                                        double pos = Variable.AxisPos[3, 1];
                                        Axis3SetMove(pos); //轴3待测Y移动到工位1
                                        if (Variable.AIMpos[3] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[3] >= Math.Round(pos - 0.1, 2))
                                        {
                                            Variable.AxisAlarmTime[3] = 0;
                                            Variable.INAutoReady1Step = 30;
                                        }
                                        else
                                        {
                                            Variable.AxisAlarmTime[3] += 1;
                                        }
                                    }
                                }
                            }
                            break;
                        }

                    case 30:
                        {
                            RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "30-->上料待测轴6待测上顶1Z轴上顶");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.UpReady1TrayOK == false)
                                {
                                    double pos = Variable.AxisPos[6, 1];
                                    Axis6SetMove(pos); //轴6待测上顶1Z轴上顶
                                    if (Variable.AIMpos[6] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[6] >= Math.Round(pos - 0.1, 2))
                                    {
                                        Variable.AxisAlarmTime[6] = 0;
                                        Variable.INAutoReady1Step = 35;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[6] += 1;
                                    }
                                }
                            }
                            break;
                        }

                    case 35:
                        {
                            RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "35-->上料待测支撑气缸出");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(24);//支撑气缸出
                                if (Variable.XStatus[46])
                                {
                                    Thread.Sleep(100);
                                    Variable.INAutoReady1Step = 40;
                                }
                            }
                            break;
                        }

                    case 40:
                        {
                            RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "40-->上料待测轴13待测上顶1Z轴下降");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[6, 2];
                                Axis6SetMove(pos); //轴13待测上顶1Z轴下降
                                if (Variable.AIMpos[6] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[6] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[6] = 0;
                                    Variable.INAutoReady1Step = 50;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[6] += 1;
                                }
                            }
                            break;
                        }
                    case 50:
                        {
                            RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "50-->上料待测支撑气缸回");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(24);//支撑气缸回
                                if (Variable.XStatus[45])
                                {
                                    Thread.Sleep(100);
                                    Variable.INAutoReady1Step = 55;
                                }
                            }
                            break;
                        }

                    case 55:
                        {
                            RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "55-->上料待测轴6待测上顶1Z轴回待机位");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[6, 0];
                                Axis6SetMove(pos); //轴6待测上顶1Z轴回待机位
                                if (Variable.AIMpos[6] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[6] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[6] = 0;
                                    Variable.INAutoReady1Step = 60;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[6] += 1;
                                }
                            }
                            break;
                        }

                    case 60:
                        {
                            RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "60-->上料待测Tray工位1有无盘感应");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[43])//有无料感应
                                {
                                    Variable.UpReady1TrayOK = true;//料盘准备OK
                                    INReadyNull1Flag = false;
                                    Variable.INAutoReady1Step = 70;
                                }
                                else
                                {
                                    INReadyNull1Flag = true;
                                    ListBoxTxt("上料待测Tray工位1没有Tray盘");
                                    Variable.INAutoReady1Step = 100;
                                }
                            }
                            break;
                        }

                    case 70:
                        {
                            RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "70-->上料待测Tray工位1有Tray盘");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.UpReady1TrayOK == false)
                                {
                                    Variable.INAutoReady1Step = 10;
                                }
                            }
                            break;
                        }
                    case 100:
                        {
                            RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "70-->上料待测Tray工位1是否满盘");
                            if (Variable.RunEnable == true)
                            {
                                if (!Variable.XStatus[44])//工位1满盘
                                {
                                    Variable.INAutoReady1Step = 110;
                                }
                                else
                                {
                                    ListBoxTxt("上料待测Tray工位3Tray盘超过数量,请取走");
                                    Variable.INAutoReady1Step = 105;
                                    RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "105-->上料待测Tray工位3Tray盘超过数量,请取走");
                                }
                            }
                            break;
                        }
                    case 110:
                        {
                            RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "110-->上料待测Tray工位3是否有Tray盘");
                            if (Variable.RunEnable == true)
                            {
                                if (!Variable.XStatus[51])//工位3无料
                                {
                                    Variable.INAutoReady1Step = 120;//无料
                                }
                                else
                                {
                                    ListBoxTxt("上料待测Tray工位3有Tray盘,请取走");
                                    Variable.INAutoReady1Step = 115;
                                    RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "115-->上料待测Tray工位3有Tray盘,请取走");
                                }
                            }
                            break;
                        }

                    case 120:
                        {
                            RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "120-->上料待测轴9待测Y移动到工位3");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.UpReady2TrayOK == false)
                                {
                                    if (!Variable.CleanOne)
                                    {
                                        double pos = Variable.AxisPos[3, 2];
                                        Axis3SetMove(pos); //轴9待测Y移动到工位3
                                        if (Variable.AIMpos[3] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[3] >= Math.Round(pos - 0.1, 2))
                                        {
                                            Variable.AxisAlarmTime[3] = 0;
                                            Variable.INAutoReady1Step = 130;
                                        }
                                        else
                                        {
                                            Variable.AxisAlarmTime[3] += 1;
                                        }
                                    }
                                }
                            }
                            break;
                        }

                    case 130:
                        {
                            RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "130-->上料待测轴7待测上顶1Z轴上顶");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.UpReady2TrayOK == false)
                                {
                                    double pos = Variable.AxisPos[7, 1];
                                    Axis7SetMove(pos); //轴7待测上顶1Z轴上顶
                                    if (Variable.AIMpos[7] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[7] >= Math.Round(pos - 0.1, 2))
                                    {
                                        Variable.AxisAlarmTime[7] = 0;
                                        Variable.INAutoReady1Step = 135;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[7] += 1;
                                    }
                                }
                            }
                            break;
                        }

                    case 135:
                        {
                            RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "135-->上料待测支撑气缸出");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(27);//支撑气缸出
                                if (Variable.XStatus[54])
                                {
                                    Thread.Sleep(100);
                                    Variable.INAutoReady1Step = 140;
                                }
                            }
                            break;
                        }
                    case 140:
                        {
                            RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "140-->上料待测轴14待测上顶1Z轴下降");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[7, 2];
                                Axis7SetMove(pos); //轴14待测上顶1Z轴下降
                                if (Variable.AIMpos[7] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[7] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[7] = 0;
                                    Variable.INAutoReady1Step = 150;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[7] += 1;
                                }
                            }
                            break;
                        }
                    case 150:
                        {
                            RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "150-->上料待测支撑气缸回");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(27);//支撑气缸回
                                if (Variable.XStatus[53])
                                {
                                    Thread.Sleep(100);
                                    Variable.INAutoReady1Step = 155;
                                }
                            }
                            break;
                        }

                    case 155:
                        {
                            RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "155-->上料待测轴14待测上顶2Z轴回待机位");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[7, 0];
                                Axis7SetMove(pos); //轴14待测上顶2Z轴回待机位
                                if (Variable.AIMpos[7] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[7] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[7] = 0;
                                    Variable.INAutoReady1Step = 160;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[7] += 1;
                                }
                            }
                            break;
                        }

                    case 160:
                        {
                            RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "160-->上料待测Tray工位3有无盘感应");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[51])//有无料感应
                                {
                                    Variable.UpReady2TrayOK = true;//料盘准备OK
                                    INReadyNull2Flag = false;
                                    Variable.INAutoReady1Step = 170;
                                }
                                else
                                {
                                    INReadyNull2Flag = true;
                                    ListBoxTxt("上料待测Tray工位3没有Tray盘");
                                    Variable.INAutoReady1Step = 180;
                                }
                            }
                            break;
                        }

                    case 170:
                        {
                            RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "170-->上料待测Tray工位3有Tray盘");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.UpReady2TrayOK == false)
                                {
                                    Variable.INAutoReady1Step = 180;
                                }
                            }
                            break;
                        }

                    case 180:
                        {
                            RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "180-->上料待测Tray工位1和工位3是否有Tray盘");
                            if (Variable.RunEnable == true)
                            {
                                if (INReadyNull1Flag && INReadyNull2Flag)
                                {
                                    //判断是否结批
                                    if (Variable.CleanOne || Variable.CleanOut)
                                    {
                                        Variable.INAutoReady1Step = 500;
                                        RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "185-->上料待测Tray结批完成");
                                    }
                                    else
                                    {
                                        ListBoxTxt("上料待测Tray工位1和工位3都没有Tray盘，请放置");
                                        Variable.INAutoReady1Step = 185;
                                        RSDAlarmINAutoReady1Step(Variable.AutoStepMsg[2] = "185-->上料待测Tray工位1和工位3都没有Tray盘，请放置");
                                    }
                                }
                                else
                                {
                                    Variable.INAutoReady1Step = 10;
                                }
                            }
                            break;
                        }
                }
                Thread.Sleep(1);
            }

        }
        #endregion

        #region 上料待测自动流程
        public void INAutoReadyStart()
        {
            while (true)
            {
                switch (Variable.INAutoReadyStep)
                {
                    //***************上料待测Y轴判断去工位1或3取Tray盘***************
                    case 10:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "10-->判断上料待测工位1和工位1有无Tray");
                            if (Variable.RunEnable == true)//工位1，3有无料
                            {
                                if ((Variable.UpReady1TrayOK && Variable.XStatus[43]) || (Variable.UpReady2TrayOK && Variable.XStatus[51]))
                                {
                                    Variable.INAutoReadyStep = 20;
                                }
                                //清空机台或结批
                                else if ((Variable.CleanOne || Variable.CleanOut) && Variable.INAutoReady1Step == 500)
                                {
                                    Variable.INAutoReadyStep = 800;
                                }
                            }
                            break;
                        }

                    //***************QR扫码***************
                    case 20:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "20-->判断是否扫Tray盘二维码");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.TrayQRCheck)
                                {
                                    QRCount = 0;
                                    Variable.INAutoReadyStep = 21;
                                }
                                else
                                {
                                    Variable.INAutoReadyStep = 60;
                                }
                            }
                            break;
                        }
                    case 21:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "21-->上料待测轨道平台夹紧气缸夹紧");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(28);//轨道平台夹紧气缸夹紧
                                if (Variable.XStatus[56])
                                {
                                    Variable.INAutoReadyStep = 22;
                                }
                            }
                            break;
                        }

                    case 22:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "22-->上料待测轴3待测Y移动到扫码位");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[4] <= Math.Round(Variable.AxisPos[4, 0] + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(Variable.AxisPos[4, 0] - 0.1, 2) && Variable.XStatus[59] && Variable.XStatus[61])
                                {
                                    double pos = Variable.AxisPos[3, 5];
                                    Axis3SetMove(pos); //轴3待测Y移动到扫码位
                                    if (Variable.AIMpos[3] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[3] >= Math.Round(pos - 0.1, 2))
                                    {
                                        QRCount = 0;
                                        Variable.AxisAlarmTime[3] = 0;
                                        Variable.XAlarmTime[1] = 0;
                                        Variable.QRRecMessage = "";
                                        Variable.INAutoReadyStep = 30;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[3] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[1] += 1;
                                    //Variable.UpAxisAlarm = true;
                                    //ListBoxTxt("上料吸嘴Z轴不在待机位或上料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }
                    case 30:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "30-->触发扫码");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.TrayQRCheck)
                                {
                                    QRCount++;
                                    Variable.QRTCPAutoStep = 101;//触发扫码
                                    Variable.INAutoReadyStep = 40;
                                }
                                else
                                {
                                    Variable.INAutoReadyStep = 65;
                                }
                            }
                            break;
                        }
                    case 40:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "40-->判断扫码是否OK");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.QRTCPAutoStep == 0)
                                {
                                    //while (Variable.PhotoRecMessage.Length < 2)
                                    //{
                                    //    Application.DoEvents();
                                    //    Thread.Sleep(1);
                                    //}
                                    Thread.Sleep((int)Variable.QRDelay);
                                    int rec = StringManipulation(Variable.QRRecMessage);

                                    if (rec == 1 && Variable.TestResult)//拍照结果OK
                                    {
                                        this.Invoke(new Action(() => { labQR.Text = Variable.QRRecMessage; }));
                                        Variable.INAutoReadyStep = 50;
                                    }
                                    else
                                    {
                                        if (QRCount >= Variable.QRTime)
                                        {
                                            Variable.INAutoReadyStep = 41;
                                        }
                                        else
                                        {
                                            Variable.INAutoReadyStep = 30;
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    case 41://判断扫码次数
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "41-->判断扫码次数");
                            if (Variable.RunEnable == true)
                            {
                                if (QRCount < Variable.QRTime)
                                {
                                    Variable.INAutoReadyStep = 30;
                                }
                                else
                                {
                                    ListBoxTxt("扫码NG，请确认Tray盘");
                                    Variable.INAutoReadyStep = 45;
                                    RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "45-->扫码NG，请确认Tray盘");
                                }
                            }
                            break;
                        }
                    case 50://配对
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "50-->QR码与数据库配对");
                            if (Variable.RunEnable == true)
                            {
                                bool flag = QRToJudge(Variable.QRRecMessage);
                                if (flag)
                                {
                                    Variable.INAutoReadyStep = 60;//配对成功
                                }
                                else
                                {
                                    ListBoxTxt("该Tray盘在数据库没有信息，请确认Tray盘！");
                                    Variable.INAutoReadyStep = 55;
                                    RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "55-->该Tray盘在数据库没有信息，请确认Tray盘！");
                                }
                            }
                            break;
                        }
                    case 56://配对NG
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "56-->QR码与数据库配对NG");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[4] <= Math.Round(Variable.AxisPos[4, 0] + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(Variable.AxisPos[4, 0] - 0.1, 2) && Variable.XStatus[59] && Variable.XStatus[61])
                                {
                                    double pos = Variable.AxisPos[3, 4];
                                    Axis3SetMove(pos); //轴3待测Y移动到机械手取料位
                                    if (Variable.AIMpos[3] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[3] >= Math.Round(pos - 0.1, 2))
                                    {
                                        Variable.AxisAlarmTime[3] = 0;
                                        Variable.XAlarmTime[1] = 0;
                                        Variable.INAutoReadyStep = 57;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[3] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[1] += 1;
                                    //Variable.UpAxisAlarm = true;
                                    //ListBoxTxt("上料吸嘴Z轴不在待机位或上料吸嘴气缸不在上位，请确认！");
                                }
                            }

                            break;
                        }
                    case 57:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "57-->请取走该Tray盘");
                            if (Variable.RunEnable == true)
                            {
                                if (MessageBox.Show("请取走该Tray盘", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    Variable.UpReady1TrayOK = false;
                                    Variable.UpReady2TrayOK = false;
                                    Variable.INAutoReadyStep = 10;
                                }
                            }
                            break;
                        }

                    case 60://配对
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "50-->QR码与数据库配对");
                            if (Variable.RunEnable == true)
                            {
                                //13X11.5X0.8-0001

                                if (Variable.QRRecMessage.Substring(0, 11) == Variable.trayCombo)
                                {
                                    Variable.INAutoReadyStep = 65;//配对成功
                                }
                                else
                                {
                                    ListBoxTxt("Tray盘尺寸与设定不符合，请确认Tray盘！");
                                    Variable.INAutoReadyStep = 61;
                                    RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "61-->Tray盘尺寸与设定不符合，请确认Tray盘！");
                                }
                            }
                            break;
                        }

                    case 65:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "60-->上料分料X轴回待机位");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[49])
                                {
                                    if (Variable.AIMpos[4] <= Math.Round(Variable.AxisPos[4, 0] + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(Variable.AxisPos[4, 0] - 0.1, 2) && Variable.XStatus[59] && Variable.XStatus[61])
                                    {
                                        double pos = Variable.AxisPos[1, 0];
                                        Axis1SetMove(pos); ;//分料X轴回待机位
                                        if (Variable.AIMpos[1] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[1] >= Math.Round(pos - 0.1, 2))
                                        {
                                            Variable.AxisAlarmTime[1] = 0;
                                            Variable.XAlarmTime[1] = 0;
                                            Variable.XAlarmTime[4] = 0;
                                            Variable.INAutoReadyStep = 70;
                                        }
                                        else
                                        {
                                            Variable.AxisAlarmTime[1] += 1;
                                        }
                                    }
                                    else
                                    {
                                        Variable.XAlarmTime[1] += 1;
                                        //Variable.UpAxisAlarm = true;
                                        //ListBoxTxt("上料吸嘴Z轴不在待机位或上料吸嘴气缸不在上位，请确认！");
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[4] += 1;
                                    // Variable.AlarmHappen[49] = true;
                                    // ListBoxTxt("上料待测Tray工位2上顶气缸不在下位，请确认！");
                                }
                            }
                            break;
                        }
                    //***************上料待测Y轴判断取Tray盘去工位2***************
                    case 70:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "70-->上料待测轨道平台夹紧气缸夹紧");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(28);//轨道平台夹紧气缸夹紧
                                if (Variable.XStatus[56])
                                {
                                    Variable.INAutoReadyStep = 80;
                                }
                            }
                            break;
                        }

                    case 80:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "80-->上料待测轴3待测Y移动到待机位");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[4] <= Math.Round(Variable.AxisPos[4, 0] + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(Variable.AxisPos[4, 0] - 0.1, 2) && Variable.XStatus[59] && Variable.XStatus[61])
                                {
                                    double pos = Variable.AxisPos[3, 0];
                                    Axis3SetMove(pos); //轴3待测Y移动到待机位
                                    if (Variable.AIMpos[3] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[3] >= Math.Round(pos - 0.1, 2))
                                    {
                                        Variable.AxisAlarmTime[3] = 0;
                                        Variable.XAlarmTime[1] = 0;
                                        Variable.INAutoReadyStep = 81;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[3] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[1] += 1;
                                    //Variable.UpAxisAlarm = true;
                                    //ListBoxTxt("上料吸嘴Z轴不在待机位或上料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }

                    case 81:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "81-->上料待测轨道平台夹紧气缸松开");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(28);//轨道平台夹紧气缸松开
                                if (!Variable.XStatus[56])
                                {
                                    Variable.INAutoReadyStep = 82;
                                }
                            }
                            break;
                        }

                    case 82:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "82-->上料待测侧顶气缸伸出");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(25);//侧顶气缸伸出
                                if (!Variable.XStatus[48])
                                {
                                    Thread.Sleep(500);
                                    Variable.INAutoReadyStep = 83;
                                }
                            }
                            break;
                        }

                    case 83:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "83-->上料待测轨道平台夹紧气缸夹紧");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(28);//轨道平台夹紧气缸夹紧
                                if (Variable.XStatus[56])
                                {
                                    Thread.Sleep(100);
                                    Variable.INAutoReadyStep = 84;
                                }
                            }
                            break;
                        }

                    case 84:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "84-->上料待测侧顶气缸回退");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(25);//侧顶气缸回退
                                if (Variable.XStatus[48])
                                {
                                    Variable.INAutoReadyStep = 85;
                                }
                            }
                            break;
                        }

                    case 85:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "85-->机械手搜索到需要上料的模组");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.siteShieldCheck)
                                {
                                    if (Variable.RobotSetFlag && (takeNum[takeModNum] == 10 || takeNum[takeModNum] == 1))//机械手搜索到需要上料的模组
                                    {
                                        takeNum[takeModNum] = 0;
                                        Variable.INAutoReadyStep = 90;
                                    }
                                }
                                else
                                {
                                    if (Variable.RobotSetFlag)//机械手搜索到需要上料的模组
                                    {
                                        takeNum[takeModNum] = 0;
                                        Variable.INAutoReadyStep = 500;
                                    }
                                }
                            }
                            break;
                        }

                    //***************上料待测Y轴判断是否取料***************

                    case 90://取料
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "90-->上料待测Y轴判断是否取料");
                            if (Variable.RunEnable == true)
                            {
                                Variable.UpReadyTrayEmpty = ReadTxtJudge(Application.StartupPath + @"\Data\UpReady\tray");//搜索是否有NG料，0是OK，不等于0是NG，10是空位
                                if (Variable.UpReadyTrayEmpty != 200)//取NG料位
                                {
                                    if (Variable.XStatus[57])//A吸嘴有料
                                    {
                                        if (Variable.XStatus[58])//B吸嘴有料
                                        {
                                            Variable.INAutoReadyStep = 300;//放NG料
                                        }
                                        else
                                        {
                                            Variable.INAutoReadyStep = 160;//B吸嘴吸NG料
                                        }
                                    }
                                    else
                                    {
                                        Variable.INAutoReadyStep = 100;//A吸嘴吸NG料

                                    }
                                }
                                else//无NG料位
                                {
                                    if (Variable.XStatus[57] || Variable.XStatus[58])
                                    {
                                        Variable.INAutoReadyStep = 300;//放NG料
                                    }
                                    else
                                    {
                                        Variable.INAutoReadyStep = 500;//机械手取盘
                                    }
                                }
                            }
                            break;
                        }

                    case 100://A吸嘴吸NG料
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "100-->上料待测A吸嘴吸NG料");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[4] <= Math.Round(Variable.AxisPos[4, 0] + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(Variable.AxisPos[4, 0] - 0.1, 2) && Variable.XStatus[59] && Variable.XStatus[61])
                                {
                                    double pos1 = Variable.UpXReadyTrayPositionA[Variable.UpReadyTrayEmpty];
                                    double pos3 = Variable.UpYReadyTrayPositionA[Variable.UpReadyTrayEmpty];
                                    UpReadyLineMove(pos1, pos3); //待测XY轴移动坐标 

                                    //Thread.Sleep(100);
                                    if (Variable.AIMpos[1] <= Math.Round(pos1 + 0.1, 2) && Variable.AIMpos[1] >= Math.Round(pos1 - 0.1, 2) && Variable.AIMpos[3] <= Math.Round(pos3 + 0.1, 2) && Variable.AIMpos[3] >= Math.Round(pos3 - 0.1, 2))
                                    {
                                        UpdateTxtUpReadyTrayEmpty(Application.StartupPath + @"\Data\UpReady\tray");
                                        Variable.AxisAlarmTime[1] = 0;
                                        Variable.AxisAlarmTime[3] = 0;
                                        Variable.XAlarmTime[1] = 0;
                                        Variable.INAutoReadyStep = 110;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[1] += 1;
                                        Variable.AxisAlarmTime[3] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[1] += 1;
                                    //Variable.UpAxisAlarm = true;
                                    //ListBoxTxt("上料吸嘴Z轴不在待机位或上料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }

                    case 110:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "110-->上料待测吸嘴1真空打开");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(29);//吸嘴1真空打开                                
                                Variable.INAutoReadyStep = 120;
                            }
                            break;
                        }

                    case 120:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "120-->上料待测吸嘴1上下气缸下降");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(33);//吸嘴1上下气缸下降
                                if (Variable.XStatus[60])
                                {
                                    Variable.INAutoReadyStep = 130;
                                }
                            }
                            break;
                        }

                    case 130:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "130-->上料待测轴4分料Z轴移动坐标吸料");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[4, 1] + Variable.offset[0];//上料Z轴A吸嘴吸料补偿
                                Axis4SetMove(pos); //轴4分料Z轴移动坐标吸料
                                if (Variable.AIMpos[4] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(pos - 0.1, 2))
                                {
                                    Thread.Sleep((int)Variable.UpAabsorb);
                                    Variable.AxisAlarmTime[4] = 0;
                                    Variable.INAutoReadyStep = 140;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[4] += 1;
                                }
                            }
                            break;
                        }

                    case 140:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "140-->上料待测吸嘴1上下气缸上升");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(33);//吸嘴1上下气缸上升
                                if (Variable.XStatus[59])
                                {
                                    Variable.INAutoReadyStep = 150;
                                }
                            }
                            break;
                        }

                    case 150:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "150-->上料待测轴4分料Z轴待机位坐标");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[4, 0];
                                Axis4SetMove(pos); //轴4分料Z轴待机位坐标
                                if (Variable.AIMpos[4] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[4] = 0;
                                    Variable.INAutoReadyStep = 155;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[4] += 1;
                                }
                            }
                            break;
                        }

                    case 155://检测吸真空
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "155-->上料待测A吸嘴检测吸真空");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[57])
                                {
                                    Variable.INAutoReadyStep = 90;
                                }
                                else
                                {
                                    ListBoxTxt("A吸嘴吸NG料真空异常，请确认！");
                                    Variable.INAutoReadyStep = 156;
                                    RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "156-->上料待测A吸嘴吸NG料真空异常，请确认！");
                                }
                            }
                            break;
                        }

                    case 160://B吸嘴吸NG料
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "160-->上料待测B吸嘴吸NG料");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[4] <= Math.Round(Variable.AxisPos[4, 0] + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(Variable.AxisPos[4, 0] - 0.1, 2) && Variable.XStatus[59] && Variable.XStatus[61])
                                {
                                    double pos1 = Variable.UpXReadyTrayPositionB[Variable.UpReadyTrayEmpty];
                                    double pos3 = Variable.UpYReadyTrayPositionB[Variable.UpReadyTrayEmpty];
                                    UpReadyLineMove(pos1, pos3); //待测XY轴移动坐标 
                                    //Thread.Sleep(100);
                                    if (Variable.AIMpos[1] <= Math.Round(pos1 + 0.1, 2) && Variable.AIMpos[1] >= Math.Round(pos1 - 0.1, 2) && Variable.AIMpos[3] <= Math.Round(pos3 + 0.1, 2) && Variable.AIMpos[3] >= Math.Round(pos3 - 0.1, 2))
                                    {
                                        UpdateTxtUpReadyTrayEmpty(Application.StartupPath + @"\Data\UpReady\tray");
                                        Variable.AxisAlarmTime[1] = 0;
                                        Variable.AxisAlarmTime[3] = 0;
                                        Variable.XAlarmTime[1] = 0;
                                        Variable.INAutoReadyStep = 170;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[1] += 1;
                                        Variable.AxisAlarmTime[3] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[1] += 1;
                                    //Variable.UpAxisAlarm = true;
                                    //ListBoxTxt("上料吸嘴Z轴不在待机位或上料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }

                    case 170:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "170-->上料待测吸嘴2吸真空打开");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(30);//吸嘴2吸真空打开                             
                                Variable.INAutoReadyStep = 180;
                            }
                            break;
                        }

                    case 180:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "180-->上料待测吸嘴2上下气缸下降");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(34);//吸嘴2上下气缸下降
                                if (Variable.XStatus[62])
                                {
                                    Variable.INAutoReadyStep = 190;
                                }
                            }
                            break;
                        }

                    case 190:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "190-->上料待测轴12分料Z轴移动坐标吸料");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[4, 1] + Variable.offset[0];//上料Z轴B吸嘴吸料补偿
                                Axis4SetMove(pos); //轴12分料Z轴移动坐标吸料
                                if (Variable.AIMpos[4] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(pos - 0.1, 2))
                                {
                                    Thread.Sleep((int)Variable.UpBabsorb);
                                    Variable.AxisAlarmTime[4] = 0;
                                    Variable.INAutoReadyStep = 200;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[4] += 1;
                                }
                            }
                            break;
                        }

                    case 200:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "200-->上料待测吸嘴2上下气缸上升");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(34);//吸嘴2上下气缸上升
                                if (Variable.XStatus[61])
                                {
                                    Variable.INAutoReadyStep = 210;
                                }
                            }
                            break;
                        }

                    case 210:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "210-->上料待测轴4分料Z轴待机位坐标");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[4, 0];
                                Axis4SetMove(pos); //轴4分料Z轴待机位坐标
                                if (Variable.AIMpos[4] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[4] = 0;
                                    Variable.INAutoReadyStep = 220;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[4] += 1;
                                }
                            }
                            break;
                        }
                    case 220:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "215-->上料待测B吸嘴吸真空检测");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[58])
                                {
                                    Variable.INAutoReadyStep = 90;
                                }
                                else
                                {
                                    ListBoxTxt("B吸嘴吸NG料真空异常，请确认！");
                                    Variable.INAutoReadyStep = 215;
                                    RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "216-->上料待测B吸嘴吸NG料真空异常，请确认！");
                                }
                            }
                            break;
                        }

                    //***************上料待测Y轴去上料空Tray盘放料***************

                    case 300:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "300-->上料待测Y轴去上料空Tray盘放料");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.UpNullTrayOK)//有空盘
                                {
                                    Variable.UpNullTray = ReadTxtJudgeOK(Application.StartupPath + @"\Data\UpEmpty\tray");
                                    if (Variable.UpNullTray != 200)//空Tray盘有空位
                                    {
                                        if (Variable.XStatus[57])//A吸嘴有料
                                        {
                                            Variable.INAutoReadyStep = 310;//A吸嘴放NG料
                                        }
                                        else
                                        {
                                            if (Variable.XStatus[58])//B吸嘴有料
                                            {
                                                Variable.INAutoReadyStep = 380;//B吸嘴放NG料
                                            }
                                            else
                                            {
                                                Thread.Sleep(100);
                                                Variable.INAutoReadyStep = 90;//取料
                                            }
                                        }
                                    }
                                    else//空Tray盘已满，上空Tray盘
                                    {
                                        Variable.UpNullTrayFull = true;
                                    }
                                }
                            }
                            break;
                        }

                    case 310://A吸嘴放NGTray位
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "310-->上料空TrayA吸嘴放NGTray位");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[4] <= Math.Round(Variable.AxisPos[4, 0] + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(Variable.AxisPos[4, 0] - 0.1, 2) && Variable.XStatus[59] && Variable.XStatus[61])
                                {
                                    double pos1 = Variable.UpXNullTrayPositionA[Variable.UpNullTray];
                                    double pos2 = Variable.UpYNullTrayPositionA[Variable.UpNullTray];
                                    UpEmptyLineMove(pos1, pos2); //空TrayXY轴移动坐标 

                                    //Thread.Sleep(100);
                                    if (Variable.AIMpos[1] <= Math.Round(pos1 + 0.1, 2) && Variable.AIMpos[1] >= Math.Round(pos1 - 0.1, 2) && Variable.AIMpos[2] <= Math.Round(pos2 + 0.1, 2) && Variable.AIMpos[2] >= Math.Round(pos2 - 0.1, 2))
                                    {
                                        UpdateTxtUpNullTray(Application.StartupPath + @"\Data\UpEmpty\tray");
                                        Variable.AxisAlarmTime[1] = 0;
                                        Variable.AxisAlarmTime[2] = 0;
                                        Variable.XAlarmTime[1] = 0;
                                        Variable.INAutoReadyStep = 320;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[1] += 1;
                                        Variable.AxisAlarmTime[2] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[1] += 1;
                                    //Variable.UpAxisAlarm = true;
                                    //ListBoxTxt("上料吸嘴Z轴不在待机位或上料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }

                    case 320:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "320-->上料空Tray吸嘴1上下气缸下降");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(33);//吸嘴1上下气缸下降
                                if (Variable.XStatus[60])
                                {
                                    Variable.INAutoReadyStep = 330;
                                }
                            }
                            break;
                        }

                    case 330:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "330-->上料空Tray轴12分料Z轴移动坐标放料");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[4, 2] + Variable.offset[0];//上料Z轴A吸嘴放料补偿
                                Axis4SetMove(pos); //轴12分料Z轴移动坐标放料
                                if (Variable.AIMpos[4] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[4] = 0;
                                    Variable.INAutoReadyStep = 340;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[4] += 1;
                                }
                            }
                            break;
                        }

                    case 340:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "340-->上料空Tray吸嘴1真空关闭");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(29);//吸嘴1真空关闭
                                function.OutYON(31);//吸嘴1破真空打开
                                Thread.Sleep((int)Variable.UpAbroken);
                                function.OutYOFF(31);//吸嘴1破真空关闭
                                Variable.INAutoReadyStep = 350;
                            }
                            break;
                        }

                    case 350:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "350-->上料空Tray吸嘴1上下气缸上升");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(33);//吸嘴1上下气缸上升
                                if (Variable.XStatus[59])
                                {
                                    Variable.INAutoReadyStep = 360;
                                }
                            }
                            break;
                        }

                    case 360:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "360-->上料空Tray轴4分料Z轴待机位坐标");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[4, 0];
                                Axis4SetMove(pos); //轴4分料Z轴待机位坐标
                                if (Variable.AIMpos[4] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[4] = 0;
                                    Variable.INAutoReadyStep = 300;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[4] += 1;
                                }
                            }
                            break;
                        }

                    case 380://B吸嘴放NGTray位
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "380-->上料空TrayB吸嘴放NGTray位");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[4] <= Math.Round(Variable.AxisPos[4, 0] + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(Variable.AxisPos[4, 0] - 0.1, 2) && Variable.XStatus[59] && Variable.XStatus[61])
                                {
                                    double pos1 = Variable.UpXNullTrayPositionB[Variable.UpNullTray];
                                    double pos2 = Variable.UpYNullTrayPositionB[Variable.UpNullTray];
                                    UpEmptyLineMove(pos1, pos2); //空TrayXY轴移动坐标 

                                    //Thread.Sleep(100);
                                    if (Variable.AIMpos[1] <= Math.Round(pos1 + 0.1, 2) && Variable.AIMpos[1] >= Math.Round(pos1 - 0.1, 2) && Variable.AIMpos[2] <= Math.Round(pos2 + 0.1, 2) && Variable.AIMpos[2] >= Math.Round(pos2 - 0.1, 2))
                                    {
                                        UpdateTxtUpNullTray(Application.StartupPath + @"\Data\UpEmpty\tray");
                                        Variable.AxisAlarmTime[1] = 0;
                                        Variable.AxisAlarmTime[2] = 0;
                                        Variable.XAlarmTime[1] = 0;
                                        Variable.INAutoReadyStep = 390;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[1] += 1;
                                        Variable.AxisAlarmTime[2] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[1] += 1;
                                    //Variable.UpAxisAlarm = true;
                                    //ListBoxTxt("上料吸嘴Z轴不在待机位或上料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }

                    case 390:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "390-->上料空Tray吸嘴2上下气缸下降");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(34);//吸嘴2上下气缸下降
                                if (Variable.XStatus[62])
                                {
                                    Variable.INAutoReadyStep = 400;
                                }
                            }
                            break;
                        }

                    case 400:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "400-->上料空Tray轴12分料Z轴移动坐标放料");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[4, 2] + Variable.offset[0];//上料Z轴B吸嘴放料补偿
                                Axis4SetMove(pos); //轴12分料Z轴移动坐标放料
                                if (Variable.AIMpos[4] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[4] = 0;
                                    Variable.INAutoReadyStep = 410;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[4] += 1;
                                }
                            }
                            break;
                        }

                    case 410:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "410-->上料空Tray吸嘴2真空关闭");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(30);//吸嘴2真空关闭
                                function.OutYON(32);//吸嘴2破真空打开
                                Thread.Sleep((int)Variable.UpBbroken);
                                function.OutYOFF(32);//吸嘴2破真空关闭
                                Variable.INAutoReadyStep = 420;
                            }
                            break;
                        }

                    case 420:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "420-->上料空Tray吸嘴2上下气缸上升");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(34);//吸嘴2上下气缸上升
                                if (Variable.XStatus[61])
                                {
                                    Variable.INAutoReadyStep = 430;
                                }
                            }
                            break;
                        }

                    case 430:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "430-->上料空Tray轴12分料Z轴待机位坐标");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[4, 0];
                                Axis4SetMove(pos); //轴12分料Z轴待机位坐标
                                if (Variable.AIMpos[4] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[4] = 0;
                                    Variable.INAutoReadyStep = 300;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[4] += 1;
                                }
                            }
                            break;
                        }

                    //***************告知机械手取料***************

                    case 500:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "500-->上料分料X轴移动待机位");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[49])
                                {
                                    if (Variable.AIMpos[4] <= Math.Round(Variable.AxisPos[4, 0] + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(Variable.AxisPos[4, 0] - 0.1, 2) && Variable.XStatus[59] && Variable.XStatus[61])
                                    {
                                        double pos = Variable.AxisPos[1, 0];
                                        Axis1SetMove(pos); //轴1上料分料X轴移动待机位
                                        if (Variable.AIMpos[1] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[1] >= Math.Round(pos - 0.1, 2))
                                        {
                                            Variable.AxisAlarmTime[1] = 0;
                                            Variable.XAlarmTime[1] = 0;
                                            Variable.XAlarmTime[4] = 0;
                                            Variable.INAutoReadyStep = 505;
                                        }
                                        else
                                        {
                                            Variable.AxisAlarmTime[1] += 1;
                                        }
                                    }
                                    else
                                    {
                                        Variable.XAlarmTime[1] += 1;
                                        //Variable.UpAxisAlarm = true;
                                        //ListBoxTxt("上料吸嘴Z轴不在待机位或上料吸嘴气缸不在上位，请确认！");
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[4] += 1;
                                    // Variable.AlarmHappen[49] = true;
                                    // ListBoxTxt("上料待测Tray工位2上顶气缸不在下位，请确认！");
                                }
                            }
                            break;
                        }
                    case 505:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "505-->上料待测轴9上料待测Y轴移动机械手取料位");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[4] <= Math.Round(Variable.AxisPos[4, 0] + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(Variable.AxisPos[4, 0] - 0.1, 2) && Variable.XStatus[59] && Variable.XStatus[61])
                                {
                                    double pos = Variable.AxisPos[3, 4];
                                    Axis3SetMove(pos); //轴9上料待测Y轴移动机械手取料位
                                    if (Variable.AIMpos[3] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[3] >= Math.Round(pos - 0.1, 2))
                                    {
                                        Variable.AxisAlarmTime[3] = 0;
                                        Variable.XAlarmTime[1] = 0;
                                        Variable.INAutoReadyStep = 510;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[3] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[1] += 1;
                                    //Variable.UpAxisAlarm = true;
                                    //ListBoxTxt("上料吸嘴Z轴不在待机位或上料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }
                    case 510:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "510-->上料待测轨道平台夹紧气缸松开");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(28);//轨道平台夹紧气缸松开
                                if (!Variable.XStatus[56])
                                {
                                    Variable.INAutoReadyStep = 520;
                                }
                            }
                            break;
                        }
                    case 520:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "520-->上料待测工位2上下气缸上升");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(26);//工位2上下气缸上升
                                if (Variable.XStatus[50])
                                {
                                    Thread.Sleep(200);
                                    Variable.INAutoReadyStep = 525;
                                }
                            }
                            break;
                        }
                    case 525:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "525-->上料待测工位2Tray上顶Tray是否感应到");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[47])
                                {
                                    Thread.Sleep(100);
                                    Variable.UpReadyTrayOK = true;
                                    Variable.INAutoReadyStep = 530;
                                }
                                else if (!Variable.XStatus[47])
                                {
                                    ListBoxTxt("上料待测工位2Tray上顶Tray未感应到");
                                    Variable.INAutoReadyStep = 526;
                                    RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "526-->上料待测工位2Tray上顶Tray未感应到");
                                }
                            }
                            break;
                        }
                    case 530:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "530-->上料待测工位2机械手已取走Tray");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.RobotUpGetTray)
                                {
                                    Variable.inTrayNum++;
                                    Variable.inTrayNumRecord++;
                                    Variable.INAutoReadyStep = 550;
                                }
                            }
                            break;
                        }
                    case 550:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "550-->上料待测工位2上下气缸下降");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(26);//工位2上下气缸下降
                                if (Variable.XStatus[49])
                                {
                                    Variable.UpReadyTrayOK = false;
                                    Variable.RobotUpGetTray = false;
                                    Variable.INAutoReadyStep = 551;
                                }
                            }
                            break;
                        }

                    case 551://判断Tray盘数量
                        {
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.MEScheck)
                                {
                                    if (Variable.inTrayNum + 5 >= Convert.ToInt32(Variable.inTrayNumSet) + Variable.inTrayNumT)
                                    {
                                        Variable.BatchNum = "";
                                        Variable.inTrayNumSet = "";
                                        mes_OPNO = "";
                                        mes_CURQTY = "";
                                        Variable.INAutoReadyStep = 552;
                                    }
                                    else
                                    {
                                        Variable.INAutoReadyStep = 560;
                                    }
                                }
                                else
                                {
                                    Variable.INAutoReadyStep = 560;
                                }
                            }
                            break;
                        }
                    case 552://弹窗输入Lot号和盘数
                        {
                            if (Variable.RunEnable == true)
                            {
                                if (!Variable.info)
                                {
                                    Variable.info = true;
                                    inform.ShowDialog();
                                }
                                if (!string.IsNullOrEmpty(Variable.BatchNum) && Convert.ToInt32(Variable.inTrayNumSet) > 0)
                                {
                                    string path = @"D:\test program\Test Program.txt";
                                    string[] lines = File.ReadAllLines(path);//"A29B01LB01"
                                    if (lines[1].Substring(0, 7) == Variable.BatchNum.Substring(0, 7))
                                    {
                                        Variable.INAutoReadyStep = 553;
                                    }
                                    else
                                    {
                                        MessageBox.Show("请检查产品批次是否相同!");
                                    }
                                }
                            }
                            break;
                        }
                    case 553:
                        {
                            if (Variable.RunEnable == true)
                            {
                                bool flag = MesCall(Variable.BatchNum);
                                if (flag)
                                {
                                    Variable.INAutoReadyStep = 554;
                                }
                                else
                                {
                                    Variable.INAutoReadyStep = 552;
                                    MessageBox.Show("请检查产品数量，重新输入产品数量!");
                                }
                            }
                            break;
                        }
                    case 554://进站类别
                        {
                            if (Variable.RunEnable == true)
                            {
                                if (!string.IsNullOrEmpty(mes_OPNO) && !string.IsNullOrEmpty(mes_CURQTY))
                                {
                                    if (mes_OPNO.Substring(mes_OPNO.Length - 1, 1) == "0" || mes_OPNO.Substring(mes_OPNO.Length - 1, 1) == "1")
                                    {
                                        Variable.INAutoReadyStep = 555;
                                    }
                                    else
                                    {
                                        Variable.INAutoReadyStep = 552;
                                        MessageBox.Show("检查产品是否进站Burn in!");
                                    }
                                }
                                else
                                {
                                    Variable.INAutoReadyStep = 552;
                                    MessageBox.Show("检查产品是否进站Burn in!");
                                }
                            }
                            break;
                        }
                    case 555://盘数
                        {
                            if (Variable.RunEnable == true)
                            {
                                if (Convert.ToDouble(Variable.inTrayNumSet) < ((Convert.ToDouble(mes_CURQTY) / 152) + 3) && Convert.ToDouble(Variable.inTrayNumSet) > ((Convert.ToDouble(mes_CURQTY) / 152) - 3))
                                {
                                    Variable.inTrayNumT = Variable.inTrayNum;
                                    Variable.INAutoReadyStep = 560;
                                }
                                else
                                {
                                    Variable.INAutoReadyStep = 552;
                                    MessageBox.Show("Tray盘数量设定错误!");
                                }
                            }
                            break;
                        }
                    case 560:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "560-->上料待测是否清空机台或结批");
                            if (Variable.RunEnable == true)
                            {
                                //清空机台或结批
                                if (Variable.CleanOne || Variable.INAutoReady1Step == 500)
                                {
                                    Variable.INAutoReadyStep = 800;
                                }
                                else
                                {
                                    Variable.INAutoReadyStep = 570;
                                }
                            }
                            break;
                        }

                    case 570:
                        {
                            RSDAlarmINAutoReadyStep(Variable.AutoStepMsg[3] = "570-->上料待测判断工位1或工位2有料");
                            if (Variable.RunEnable == true)
                            {
                                //Axis9SetMove(Variable.AxisUpYReadyTray_WaitPoint); //轴9上料待测Y轴移动待机位
                                if (Variable.UpReady1TrayOK == true)
                                {
                                    Variable.UpReady1TrayOK = false;
                                    Thread.Sleep(100);
                                    Variable.INAutoReadyStep = 10;
                                }
                                if (Variable.UpReady2TrayOK == true)
                                {
                                    Variable.UpReady2TrayOK = false;
                                    Thread.Sleep(100);
                                    Variable.INAutoReadyStep = 10;
                                }
                            }
                            break;
                        }
                }
                Thread.Sleep(10);
            }
        }
        #endregion


        #region 下料良品自动流程
        public void OutAutoOKStart()
        {
            while (true)
            {
                switch (Variable.OutAutoOKStartStep)
                {

                    //***************判断机械手是否放料***************
                    case 10:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "10-->判断工位2有无料,工位3有无料");
                            if (Variable.RunEnable == true)
                            {
                                if (!Variable.XStatus[69] && !Variable.XStatus[74])//判断工位2有无料,工位3有无料
                                {
                                    Variable.OutAutoOKStartStep = 15;//无料                                  
                                }
                                else if (Variable.XStatus[69])
                                {
                                    Variable.OutAutoOKStartStep = 200;//有料
                                }
                                else if (Variable.XStatus[74])
                                {
                                    Variable.OutAutoOKStartStep = 70;//判断补料有无料
                                }
                            }
                            break;
                        }
                    case 15:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "15-->下料良品工位2上顶气缸上升");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(54);//下料良品工位2上顶气缸上升
                                if (Variable.XStatus[73])
                                {
                                    Thread.Sleep(200);
                                    Variable.OutAutoOKStartStep = 20;
                                }
                            }
                            break;
                        }

                    case 20:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "20-->下料良品轴10下料良品Y轴移动到机械手放料位");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                {
                                    double pos = Variable.AxisPos[10, 4];
                                    Axis10SetMove(pos); //轴10下料良品Y轴移动到机械手放料位
                                    if (Variable.AIMpos[10] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[10] >= Math.Round(pos - 0.1, 2))
                                    {
                                        Variable.AxisAlarmTime[10] = 0;
                                        Variable.XAlarmTime[2] = 0;
                                        Variable.OutAutoOKStartStep = 30;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[10] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[2] += 1;
                                    //Variable.DownAxisAlarm = true;
                                    //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }
                    case 30:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "30-->下料良品判断X轴是否在安全位");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[9] >= Math.Round(Variable.AxisPos[9, 2], 2))
                                {
                                    Variable.OutAutoOKStartStep = 40;
                                }
                                else
                                {
                                    if (Variable.OutAutoFillStartStep >= 40 && Variable.OutAutoFillStartStep <= 290)
                                    {
                                        Variable.OutAutoOKStartStep = 40;
                                    }
                                    else
                                    {
                                        Variable.OutAutoOKStartStep = 35;
                                    }
                                }
                            }
                            break;
                        }

                    case 35:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "35-->下料良品轴1下料良品Y轴移动到机械手放料位");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                {
                                    double pos = Variable.AxisPos[10, 4];
                                    Axis10SetMove(pos); //轴1下料良品Y轴移动到机械手放料位
                                    if (Variable.AIMpos[10] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[10] >= Math.Round(pos - 0.1, 2))
                                    {
                                        Variable.AxisAlarmTime[10] = 0;
                                        Variable.XAlarmTime[2] = 0;
                                        Variable.OutAutoOKStartStep = 40;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[10] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[2] += 1;
                                    //Variable.DownAxisAlarm = true;
                                    //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }
                    case 40://等待机械手放料
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "40-->下料良品等待机械手放料");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[73] && Variable.AIMpos[9] >= Math.Round(Variable.AxisPos[9, 0] - 0.1, 2))//告知机械手放料
                                {
                                    Variable.DownGetTray = true;
                                    Variable.RobotDownGetTray = false;
                                    Variable.OutAutoOKStartStep = 45;
                                }
                            }
                            break;
                        }

                    case 45:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "45-->下料良品机械手放料OK");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.RobotDownGetTray)//机械手放料OK
                                {
                                    Variable.DownGetTray = false;
                                    Variable.outTrayNum++;
                                    Variable.OutAutoOKStartStep = 50;
                                }
                            }
                            break;
                        }

                    case 50:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "50-->下料良品工位2上顶气缸下降");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(54);//下料良品工位2上顶气缸下降
                                if (Variable.XStatus[72])
                                {
                                    Thread.Sleep(200);
                                    Variable.OutAutoOKStartStep = 55;
                                }
                            }
                            break;
                        }

                    case 55:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "55-->下料良品Tray盘工位2侧顶气缸出");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(53);//良品Tray盘工位2侧顶气缸出
                                Thread.Sleep(200);
                                Variable.OutAutoOKStartStep = 56;
                            }
                            break;
                        }
                    case 56:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "56-->下料良品轴1下料良品Y轴移动到机械手待机位");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                {
                                    double pos = Variable.AxisPos[10, 0];
                                    Axis10SetMove(pos); //轴1下料良品Y轴移动到机械手待机位
                                    if (Variable.AIMpos[10] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[10] >= Math.Round(pos - 0.1, 2))
                                    {
                                        Variable.AxisAlarmTime[10] = 0;
                                        Variable.XAlarmTime[2] = 0;
                                        Variable.OutAutoOKStartStep = 57;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[10] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[2] += 1;
                                    //Variable.DownAxisAlarm = true;
                                    //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }
                    case 57:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "57-->下料良品Tray盘工位2侧顶气缸回");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(53);//良品Tray盘工位2侧顶气缸回
                                Thread.Sleep(200);
                                Variable.OutAutoOKStartStep = 58;
                            }
                            break;
                        }
                    case 58:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "58-->下料良品是否需要拍照");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.PhotoCheck)//需要拍照
                                {
                                    PhotoResultToDown();//相机数据与模块数据集合传给下料站
                                }
                                else
                                {
                                    string path = Application.StartupPath + @"\Data\Photo\Down\tray";
                                    string[] Readstr = new string[152];
                                    for (int i = 0; i < 152; i++)
                                    {
                                        Readstr[i] = "00";
                                    }
                                    //向TXT写入数据
                                    myTXT.WriteTxt(Readstr, path);
                                }
                                Thread.Sleep(200);
                                Variable.OutAutoOKStartStep = 60;
                            }
                            break;
                        }

                    case 60:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "60-->下料良品放置Tray盘OK");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[69] && Variable.XStatus[72])//Tray盘OK
                                {
                                    Variable.OutAutoOKStartStep = 65;
                                }
                                else if (!Variable.XStatus[69])
                                {
                                    ListBoxTxt("下料良品工位2未感应到Tray，请确认！");
                                    Variable.OutAutoOKStartStep = 62;
                                    RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "62-->下料良品工位2未感应到Tray，请确认！");
                                }
                                else if (!Variable.XStatus[72])
                                {
                                    ListBoxAlarm("下料良品工位2上顶气缸不在下位");
                                    Variable.OutAutoOKStartStep = 63;
                                    RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "63-->下料良品工位2上顶气缸不在下位");
                                }
                            }
                            break;
                        }

                    case 65://判断OK盘NG是否超过一半
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "65-->下料良品判断OK盘NG是否超过一半");
                            if (Variable.RunEnable == true)
                            {
                                int nums = NGCount();
                                if (nums > 75)
                                {
                                    Variable.OutAutoOKStartStep = 180;//不补料
                                    Variable.OKTrayNGCountFlag = true;//OK盘NG超过一半
                                }
                                else
                                {
                                    Variable.OutAutoOKStartStep = 70;//取NG位
                                }
                            }
                            break;
                        }

                    //***************判断补料位有没有盘***************
                    case 70:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "70-->下料良品判断补料位有没有盘");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.OutFillTrayFlag || Variable.XStatus[83])//判断补料位有没有料盘
                                {
                                    Variable.OutAutoOKStartStep = 180;//有
                                }
                                else
                                {
                                    Variable.OutAutoOKStartStep = 75;//没有
                                }

                            }
                            break;
                        }
                    case 75:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "75-->下料良品夹紧气缸夹紧");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(55); //夹紧气缸夹紧
                                if (Variable.XStatus[77])
                                {
                                    Thread.Sleep(100);
                                    Variable.OutAutoOKStartStep = 80;
                                }
                            }
                            break;
                        }
                    case 80:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "80-->下料良品轴1下料良品Y轴移动到OKTray备料位");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                {
                                    double pos9 = Variable.AxisPos[9, 0];
                                    double pos10 = Variable.AxisPos[10, 2];
                                    Axis9SetMove(pos9); ;//分料X轴回待机
                                    Axis10SetMove(pos10); //轴1下料良品Y轴移动到OKTray备料位 
                                    if (Variable.AIMpos[9] <= Math.Round(pos9 + 0.1, 2) && Variable.AIMpos[9] >= Math.Round(pos9 - 0.1, 2) && Variable.AIMpos[10] <= Math.Round(pos10 + 0.1, 2) && Variable.AIMpos[10] >= Math.Round(pos10 - 0.1, 2))
                                    {
                                        Variable.AxisAlarmTime[9] = 0;
                                        Variable.AxisAlarmTime[10] = 0;
                                        Variable.XAlarmTime[2] = 0;
                                        Variable.OutAutoOKStartStep = 85;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[9] += 1;
                                        Variable.AxisAlarmTime[10] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[2] += 1;
                                    //Variable.DownAxisAlarm = true;
                                    //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }
                    case 85:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "85-->下料良品轴15下料移TrayX轴移动到OKTray备料位");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[15, 1];
                                Axis15SetMove(pos); //轴15下料移TrayX轴移动到OKTray备料位
                                if (Variable.AIMpos[15] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[15] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[15] = 0;
                                    Variable.OutAutoOKStartStep = 90;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[15] += 1;
                                }
                            }
                            break;
                        }
                    case 90:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "90-->下料良品下料良品Tray盘工位3有无感应");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[74])//有料
                                {
                                    function.OutYOFF(55); //轨道夹紧气缸松开
                                    if (!Variable.XStatus[77])
                                    {
                                        Thread.Sleep(100);
                                        Variable.OutAutoOKStartStep = 100;
                                    }
                                }
                                else
                                {
                                    ListBoxTxt("下料良品Tray盘工位3未感应到产品");
                                    Variable.OutAutoOKStartStep = 95;
                                    RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "95-->下料良品Tray盘工位3未感应到产品");
                                }
                            }
                            break;
                        }
                    case 100:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "100-->下料良品移Tray夹爪上下缸下降");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(68); //移Tray夹爪上下缸下降
                                if (Variable.XStatus[103])
                                {
                                    Variable.OutAutoOKStartStep = 110;
                                }
                            }
                            break;
                        }

                    case 110:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "110-->下料良品移Tray夹爪缸夹紧");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(69); //移Tray夹爪缸夹紧
                                if (Variable.XStatus[105])
                                {
                                    Variable.OutAutoOKStartStep = 120;
                                }
                            }
                            break;
                        }

                    case 120:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "120-->下料良品移Tray夹爪上下缸上升");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(68); //移Tray夹爪上下缸上升
                                if (Variable.XStatus[102])
                                {
                                    Variable.OutAutoOKStartStep = 130;
                                }
                            }
                            break;
                        }

                    case 130:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "130-->下料良品轴15下料移TrayX轴移动到分料OKTray备料位");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[15, 2];
                                Axis15SetMove(pos); //轴15下料移TrayX轴移动到分料OKTray备料位
                                if (Variable.AIMpos[15] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[15] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[15] = 0;
                                    Variable.OutAutoOKStartStep = 135;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[15] += 1;
                                }
                            }
                            break;
                        }

                    case 135:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "135-->下料良品轴11下料补料Y轴移动到OKTray备料位");
                            if (Variable.RunEnable == true)
                            {

                                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                {
                                    double pos = Variable.AxisPos[11, 1];
                                    Axis11SetMove(pos); //轴11下料补料Y轴移动到OKTray备料位
                                    if (Variable.AIMpos[11] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[11] >= Math.Round(pos - 0.1, 2))
                                    {
                                        Variable.AxisAlarmTime[11] = 0;
                                        Variable.XAlarmTime[2] = 0;
                                        Variable.OutAutoOKStartStep = 140;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[11] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[2] += 1;
                                    //Variable.DownAxisAlarm = true;
                                    //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }

                    case 140:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "140-->下料良品移Tray夹爪上下缸下降");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(68);//移Tray夹爪上下缸下降
                                if (Variable.XStatus[103])
                                {
                                    Variable.OutAutoOKStartStep = 150;
                                }
                            }
                            break;
                        }

                    case 150:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "150-->下料良品移Tray夹爪缸松开");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(69); //移Tray夹爪缸松开
                                if (Variable.XStatus[104])
                                {
                                    Variable.OutAutoOKStartStep = 160;
                                }
                            }
                            break;
                        }

                    case 160:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "160-->下料良品移Tray夹爪上下缸上升");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(68);//移Tray夹爪上下缸上升
                                string[] Readstr = myTXT.ReadTXT(Application.StartupPath + @"\Data\DownOK\tray");
                                myTXT.WriteTxt(Readstr, Application.StartupPath + @"\Data\DownReadyOK\tray");//读取DownOK文本到DownOKWait中
                                if (Variable.XStatus[102])
                                {
                                    Variable.OutAutoOKStartStep = 170;
                                }
                            }
                            break;
                        }

                    case 170:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "170-->下料良品轴15下料移TrayX轴移动到待机位");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[15, 0];
                                Axis15SetMove(pos); //轴15下料移TrayX轴移动到待机位
                                if (Variable.AIMpos[15] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[15] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[15] = 0;
                                    Variable.OutAutoOKStartStep = 10;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[15] += 1;
                                }
                            }
                            break;
                        }

                    //***************良品Tray移到工位2***************
                    case 180:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "180-->下料良品轴1下料良品Y轴移动到待机位");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                {
                                    double pos = Variable.AxisPos[10, 0];
                                    Axis10SetMove(pos); //轴1下料良品Y轴移动到待机位
                                    if (Variable.AIMpos[10] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[10] >= Math.Round(pos - 0.1, 2))
                                    {
                                        Variable.AxisAlarmTime[10] = 0;
                                        Variable.XAlarmTime[2] = 0;
                                        Variable.OutAutoOKStartStep = 181;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[10] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[2] += 1;
                                    //Variable.DownAxisAlarm = true;
                                    //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }

                    case 181:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "181-->下料良品轨道夹紧气缸松开");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(55); //轨道夹紧气缸松开
                                if (!Variable.XStatus[77])
                                {
                                    Thread.Sleep(100);
                                    Variable.OutAutoOKStartStep = 182;
                                }
                            }
                            break;
                        }

                    case 182:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "182-->下料良品侧顶气缸伸出");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(53);//侧顶气缸伸出
                                if (!Variable.XStatus[71])
                                {
                                    Thread.Sleep(500);
                                    Variable.OutAutoOKStartStep = 183;
                                }
                            }
                            break;
                        }

                    case 183:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "183-->下料良品轨道夹紧气缸夹紧");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(55); //轨道夹紧气缸夹紧
                                if (Variable.XStatus[77])
                                {
                                    Thread.Sleep(100);
                                    Variable.OutAutoOKStartStep = 184;
                                }
                            }
                            break;
                        }

                    case 184:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "184-->下料良品侧顶气缸缩回");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(53); //侧顶气缸缩回
                                Thread.Sleep(100);
                                Variable.OKTrayNGCountFlag = false;//OK盘NG超过一半
                                Variable.OutAutoOKStartStep = 190;
                            }
                            break;
                        }

                    case 190://判断OK盘NG是否超过一半
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "190-->下料良品判断OK盘NG是否超过一半");
                            if (Variable.RunEnable == true)
                            {
                                int nums = NGCount();
                                if (nums > 75)
                                {
                                    Variable.OutAutoOKStartStep = 800;//不补料
                                    Variable.OKTrayNGCountFlag = true;//OK盘NG超过一半
                                }
                                else
                                {
                                    Variable.OutAutoOKStartStep = 200;//取NG位 
                                }
                            }
                            break;
                        }


                    //***************判断良品盘是否有NG料***************

                    case 200://取良品盘NG料
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "200-->下料良品取良品盘NG料");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.DownReadyTrayFullOK == true)//备料盘都是OK品
                                {
                                    Variable.DownOKTrayNG = ReadTxtJudge(Application.StartupPath + @"\Data\DownOK\tray");//搜索是否有NG料，0是OK，不等于0是NG，10是空位
                                    if (Variable.DownOKTrayNG != 200)//有NG料
                                    {
                                        if (Variable.XStatus[96])//A吸嘴有料
                                        {
                                            if (Variable.XStatus[97])//B吸嘴有料
                                            {
                                                Variable.OutAutoOKStartStep = 330;//去放料
                                            }
                                            else
                                            {
                                                Variable.OutAutoOKStartStep = 270;//B吸嘴去吸料
                                            }
                                        }
                                        else
                                        {
                                            Variable.OutAutoOKStartStep = 210;//A吸嘴去吸料
                                        }

                                    }
                                    else //没有NG料
                                    {
                                        if (Variable.XStatus[96] || Variable.XStatus[97])//A吸嘴或B吸嘴有料
                                        {
                                            Variable.OutAutoOKStartStep = 330;//去放料
                                        }
                                        else
                                        {
                                            //Variable.OutAutoOKStartStep = 500; //补OK料到良品盘
                                            Variable.OutAutoOKStartStep = 480; //判断是否补OK料到良品盘
                                        }
                                    }
                                }
                            }
                            break;
                        }

                    case 210://A吸嘴取NG料
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "210-->下料良品A吸嘴取NG料");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[72])
                                {
                                    if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                    {
                                        double pos9 = Variable.DownXOKTrayPositionA[Variable.DownOKTrayNG];
                                        double pos10 = Variable.DownYOKTrayPositionA[Variable.DownOKTrayNG];
                                        DownOKLineMove(pos9, pos10);//良品轴取OK料盘里NG料
                                        if (Variable.AIMpos[9] <= Math.Round(pos9 + 0.1, 2) && Variable.AIMpos[9] >= Math.Round(pos9 - 0.1, 2) && Variable.AIMpos[10] <= Math.Round(pos10 + 0.1, 2) && Variable.AIMpos[10] >= Math.Round(pos10 - 0.1, 2))
                                        {
                                            UpdateTxtDownOKTrayNG(Application.StartupPath + @"\Data\DownOK\tray");
                                            Variable.AxisAlarmTime[9] = 0;
                                            Variable.AxisAlarmTime[10] = 0;
                                            Variable.XAlarmTime[2] = 0;
                                            Variable.XAlarmTime[3] = 0;
                                            Variable.OutAutoOKStartStep = 220;
                                        }
                                        else
                                        {
                                            Variable.AxisAlarmTime[9] += 1;
                                            Variable.AxisAlarmTime[10] += 1;
                                        }
                                    }
                                    else
                                    {
                                        Variable.XAlarmTime[2] += 1;
                                        //Variable.DownAxisAlarm = true;
                                        //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[3] += 1;
                                    //Variable.AlarmHappen[72] = true;
                                    //ListBoxTxt("下料良品Tray盘工位2上顶气缸不在下位，请确认！");
                                }
                            }
                            break;
                        }

                    case 220:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "220-->下料良品A吸嘴真空打开");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(62);//A吸嘴真空打开
                                Variable.OutAutoOKStartStep = 230;
                            }
                            break;
                        }

                    case 230:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "230-->下料良品A吸嘴气缸下降");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(66); //A吸嘴气缸下降
                                if (Variable.XStatus[99])
                                {
                                    Variable.OutAutoOKStartStep = 240;
                                }
                            }
                            break;
                        }

                    case 240:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "240-->下料良品轴13Z轴下降");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[13, 1] + Variable.offset[0];//良品轴，下料Z轴A吸嘴取NG料补偿
                                Axis13SetMove(pos); //轴13Z轴下降
                                if (Variable.AIMpos[13] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(pos - 0.1, 2))
                                {
                                    Thread.Sleep((int)Variable.DownAabsorb);
                                    Variable.AxisAlarmTime[13] = 0;
                                    Variable.OutAutoOKStartStep = 250;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[13] += 1;
                                }
                            }
                            break;
                        }
                    case 250:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "250-->下料良品A吸嘴气缸上升");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(66);//A吸嘴气缸上升
                                if (Variable.XStatus[98])
                                {
                                    Variable.OutAutoOKStartStep = 255;
                                }
                            }
                            break;
                        }
                    case 255:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "255-->下料良品轴13Z轴上升");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[13, 0];
                                Axis13SetMove(pos); //轴13Z轴上升
                                if (Variable.AIMpos[13] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[13] = 0;
                                    Variable.OutAutoOKStartStep = 260;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[13] += 1;
                                }
                            }
                            break;
                        }
                    case 260:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "260-->下料良品A吸嘴真空检测");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[96])
                                {
                                    Variable.OutAutoOKStartStep = 200;
                                }
                                else
                                {
                                    ListBoxTxt("A吸嘴吸NG料真空异常，请确认！");
                                    Variable.OutAutoOKStartStep = 265;
                                }
                            }
                            break;
                        }

                    case 270://B吸嘴取NG料
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "270-->下料良品B吸嘴取NG料");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[72])
                                {
                                    if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                    {
                                        double pos9 = Variable.DownXOKTrayPositionB[Variable.DownOKTrayNG];
                                        double pos10 = Variable.DownYOKTrayPositionB[Variable.DownOKTrayNG];
                                        DownOKLineMove(pos9, pos10);//良品轴取OK料盘里NG料
                                        if (Variable.AIMpos[9] <= Math.Round(pos9 + 0.1, 2) && Variable.AIMpos[9] >= Math.Round(pos9 - 0.1, 2) && Variable.AIMpos[10] <= Math.Round(pos10 + 0.1, 2) && Variable.AIMpos[10] >= Math.Round(pos10 - 0.1, 2))
                                        {
                                            UpdateTxtDownOKTrayNG(Application.StartupPath + @"\Data\DownOK\tray");
                                            Variable.AxisAlarmTime[9] = 0;
                                            Variable.AxisAlarmTime[10] = 0;
                                            Variable.XAlarmTime[2] = 0;
                                            Variable.XAlarmTime[3] = 0;
                                            Variable.OutAutoOKStartStep = 280;
                                        }
                                        else
                                        {
                                            Variable.AxisAlarmTime[9] += 1;
                                            Variable.AxisAlarmTime[10] += 1;
                                        }
                                    }
                                    else
                                    {
                                        Variable.XAlarmTime[2] += 1;
                                        //Variable.DownAxisAlarm = true;
                                        //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[3] += 1;
                                    //Variable.AlarmHappen[72] = true;
                                    //ListBoxTxt("下料良品Tray盘工位2上顶气缸不在下位，请确认！");
                                }
                            }
                            break;
                        }

                    case 280:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "280-->下料良品B吸嘴真空打开");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(63); //B吸嘴真空打开
                                Variable.OutAutoOKStartStep = 290;
                            }
                            break;
                        }

                    case 290:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "290-->下料良品B吸嘴气缸下降");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(67); //B吸嘴气缸下降
                                if (Variable.XStatus[101])
                                {
                                    Variable.OutAutoOKStartStep = 295;
                                }
                            }
                            break;
                        }
                    case 295:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "295-->下料良品轴13Z轴下降");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[13, 1] + Variable.offset[0];//良品轴，下料Z轴B吸嘴取NG料补偿
                                Axis13SetMove(pos); //轴7Z轴下降
                                if (Variable.AIMpos[13] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(pos - 0.1, 2))
                                {
                                    Thread.Sleep((int)Variable.DownBabsorb);
                                    Variable.AxisAlarmTime[13] = 0;
                                    Variable.OutAutoOKStartStep = 300;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[13] += 1;
                                }
                            }
                            break;
                        }

                    case 300:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "300-->下料良品B吸嘴上下缸上升");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(67); //B吸嘴上下缸上升
                                if (Variable.XStatus[100])
                                {
                                    Variable.OutAutoOKStartStep = 310;
                                }
                            }
                            break;
                        }
                    case 310:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "310-->下料良品轴13Z轴上升");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[13, 0];
                                Axis13SetMove(pos); //轴13Z轴上升
                                if (Variable.AIMpos[13] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[13] = 0;
                                    Variable.OutAutoOKStartStep = 320;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[13] += 1;
                                }
                            }
                            break;
                        }
                    case 320:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "320-->下料良品B吸嘴真空检测");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[97])
                                {
                                    Variable.OutAutoOKStartStep = 200;
                                }
                                else
                                {
                                    ListBoxTxt("B吸嘴吸NG料真空异常，请确认！");
                                    Variable.OutAutoOKStartStep = 325;
                                }
                            }
                            break;
                        }

                    //***************去NGTray盘位放料***************

                    case 330://去NG位放料
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "330-->下料良品去NG位放料");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.DownNGTrayOK == true)
                                {
                                    Variable.DownNGTray = ReadTxtJudgeOK(Application.StartupPath + @"\Data\DownNG\tray");//搜索是否有NG料，0是无料，1是有料
                                    if (Variable.DownNGTray != 200)//空Tray盘有空位
                                    {
                                        if (Variable.XStatus[96])//A吸嘴有料
                                        {
                                            Variable.OutAutoOKStartStep = 340;//A吸嘴放NG料
                                        }
                                        else
                                        {
                                            if (Variable.XStatus[97])//B吸嘴有料
                                            {
                                                Variable.OutAutoOKStartStep = 410;//B吸嘴放NG料
                                            }
                                            else
                                            {
                                                //Thread.Sleep(100);
                                                Variable.OutAutoOKStartStep = 200;//取料
                                            }
                                        }
                                    }
                                    else //空Tray盘没有空位
                                    {
                                        Variable.DownNGTrayFull = true;

                                    }
                                }
                            }
                            break;
                        }

                    case 340://A吸嘴放NG料
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "340-->下料良品A吸嘴放NG料");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                {
                                    double pos9 = Variable.DownXNGTrayPositionA[Variable.DownNGTray];
                                    double pos12 = Variable.DownYNGTrayPositionA[Variable.DownNGTray];
                                    DownNGLineMove(pos9, pos12);//NG轴去NG料盘里放NG料

                                    //Thread.Sleep(100);
                                    if (Variable.AIMpos[9] <= Math.Round(pos9 + 0.1, 2) && Variable.AIMpos[9] >= Math.Round(pos9 - 0.1, 2) && Variable.AIMpos[12] <= Math.Round(pos12 + 0.1, 2) && Variable.AIMpos[12] >= Math.Round(pos12 - 0.1, 2))
                                    {
                                        UpdateTxtDownNGTray(Application.StartupPath + @"\Data\DownNG\tray");
                                        Variable.AxisAlarmTime[9] = 0;
                                        Variable.AxisAlarmTime[12] = 0;
                                        Variable.XAlarmTime[2] = 0;
                                        Variable.OutAutoOKStartStep = 350;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[9] += 1;
                                        Variable.AxisAlarmTime[12] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[2] += 1;
                                    //Variable.DownAxisAlarm = true;
                                    //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }

                    case 350:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "350-->下料良品A吸嘴气缸下降");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(66);//A吸嘴气缸下降
                                if (Variable.XStatus[99])
                                {
                                    Variable.OutAutoOKStartStep = 360;
                                }
                            }
                            break;
                        }

                    case 360:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "360-->下料良品轴13Z轴移动到NG取料位");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[13, 3] + Variable.offset[0];//NG位，下料Z轴A吸嘴放料补偿
                                Axis13SetMove(pos); //轴13Z轴移动到NG取料位
                                if (Variable.AIMpos[13] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[13] = 0;
                                    Variable.OutAutoOKStartStep = 370;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[13] += 1;
                                }
                            }
                            break;
                        }

                    case 370:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "370-->下料良品A吸嘴真空关闭");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(62); //A吸嘴真空关闭
                                function.OutYON(64);//A吸嘴破真空打开
                                Thread.Sleep((int)Variable.DownAbroken);
                                function.OutYOFF(64);//A吸嘴破真空关闭
                                Variable.OutAutoOKStartStep = 380;
                            }
                            break;
                        }

                    case 380:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "380-->下料良品A吸嘴气缸上升");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(66);//A吸嘴气缸上升
                                if (Variable.XStatus[98])
                                {
                                    Variable.OutAutoOKStartStep = 390;
                                }
                            }
                            break;
                        }

                    case 390:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "390-->下料良品轴13Z轴移动到待机位");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[13, 0];
                                Axis13SetMove(pos); //轴13Z轴移动到待机位
                                if (Variable.AIMpos[13] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[13] = 0;
                                    Variable.OutAutoOKStartStep = 330;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[13] += 1;
                                }
                            }
                            break;
                        }

                    case 410://B吸嘴放NG料
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "410-->下料良品B吸嘴放NG料");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                {
                                    double pos9 = Variable.DownXNGTrayPositionB[Variable.DownNGTray];
                                    double pos12 = Variable.DownYNGTrayPositionB[Variable.DownNGTray];
                                    DownNGLineMove(pos9, pos12);//NG轴去NG料盘里放NG料
                                    if (Variable.AIMpos[9] <= Math.Round(pos9 + 0.1, 2) && Variable.AIMpos[9] >= Math.Round(pos9 - 0.1, 2) && Variable.AIMpos[12] <= Math.Round(pos12 + 0.1, 2) && Variable.AIMpos[12] >= Math.Round(pos12 - 0.1, 2))
                                    {
                                        UpdateTxtDownNGTray(Application.StartupPath + @"\Data\DownNG\tray");
                                        Variable.AxisAlarmTime[9] = 0;
                                        Variable.AxisAlarmTime[12] = 0;
                                        Variable.XAlarmTime[2] = 0;
                                        Variable.OutAutoOKStartStep = 420;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[9] += 1;
                                        Variable.AxisAlarmTime[12] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[2] += 1;
                                    //Variable.DownAxisAlarm = true;
                                    //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }

                    case 420:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "420-->下料良品B吸嘴气缸下降");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(67); //B吸嘴气缸下降
                                if (Variable.XStatus[101])
                                {
                                    Variable.OutAutoOKStartStep = 430;
                                }
                            }
                            break;
                        }

                    case 430:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "430-->下料良品轴13Z轴移动到NG取料位");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[13, 3] + Variable.offset[0];//NG位，下料Z轴B吸嘴放料补偿
                                Axis13SetMove(pos); //轴13Z轴移动到NG取料位
                                if (Variable.AIMpos[13] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[13] = 0;
                                    Variable.OutAutoOKStartStep = 440;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[13] += 1;
                                }
                            }
                            break;
                        }

                    case 440:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "440-->下料良品B吸嘴真空关闭");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(63); //B吸嘴真空关闭
                                function.OutYON(65); //B吸嘴破真空打开
                                Thread.Sleep((int)Variable.DownBbroken);
                                function.OutYOFF(65); //B吸嘴破真空关闭
                                Variable.OutAutoOKStartStep = 450;
                            }
                            break;
                        }
                    case 450:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "450-->下料良品B吸嘴气缸上升");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(67);//B吸嘴气缸上升
                                if (Variable.XStatus[100])
                                {
                                    Variable.OutAutoOKStartStep = 460;
                                }
                            }
                            break;
                        }

                    case 460:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "460-->下料良品轴13Z轴移动到待机位");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[13, 0];
                                Axis13SetMove(pos); //轴13Z轴移动到待机位
                                if (Variable.AIMpos[13] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[13] = 0;
                                    Variable.OutAutoOKStartStep = 330;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[13] += 1;
                                }
                            }
                            break;
                        }

                    //***************判断是否需要补OK料到良品盘***************

                    case 480://判断OK盘空位是否超过一半
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "480-->下料良品判断OK盘空位是否超过一半");
                            if (Variable.RunEnable == true)
                            {
                                int nums = DownNGCount();
                                if (nums > Variable.DownNGCount)
                                {
                                    Variable.OutAutoOKStartStep = 800;//不补料
                                }
                                else
                                {
                                    Variable.OutAutoOKStartStep = 500;//补料
                                }
                            }
                            break;
                        }

                    case 500://吸嘴取OK料
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "500-->下料良品吸嘴取OK料");
                            if (Variable.RunEnable == true)
                            {
                                Variable.DownOKTrayWait = ReadTxtJudgeOK(Application.StartupPath + @"\Data\DownOKWait\tray");//搜索OK盘空位料
                                Variable.DownReadyOK = ReadTxtJudgeReadyOK(Application.StartupPath + @"\Data\DownReadyOK\tray");//搜索备料盘OK料

                                if (Variable.DownOKTrayWait != 200 && Variable.DownReadyOK != 200)// 下料良品Tray盘没放满,下料OK补料工位有料
                                {
                                    if (Variable.XStatus[96])//A吸嘴有料
                                    {
                                        if (Variable.XStatus[97])//B吸嘴有料
                                        {
                                            Variable.OutAutoOKStartStep = 610;//去良品盘放料
                                        }
                                        else
                                        {
                                            Variable.OutAutoOKStartStep = 560;//B吸嘴去吸料
                                        }
                                    }
                                    else
                                    {
                                        Variable.OutAutoOKStartStep = 505;//A吸嘴去吸料
                                    }
                                }
                                else if (Variable.DownOKTrayWait != 200 && Variable.DownReadyOK == 200)// 下料良品Tray盘没放满,下料补料工位无料
                                {
                                    if (Variable.XStatus[96] || Variable.XStatus[97])
                                    {
                                        Variable.OutAutoOKStartStep = 610;//去良品盘放料
                                        Variable.DownReadyEmpty = true;//OK备品料盘已取空
                                    }
                                    else
                                    {
                                        Variable.DownReadyEmpty = true;//OK备品料盘已取空
                                        Variable.OutAutoOKStartStep = 501;//OK盘移到备用位
                                    }
                                }

                                else if (Variable.DownOKTrayWait == 200 && Variable.DownReadyOK != 200)// 下料良品Tray盘放满,下料OK补料工位有料
                                {
                                    if (Variable.XStatus[96] || Variable.XStatus[97])
                                    {
                                        Variable.OutAutoOKStartStep = 610;//去良品盘放料
                                    }
                                    else
                                    {
                                        Variable.OutAutoOKStartStep = 800;//料盘OK品已放满
                                    }

                                }
                                else if (Variable.DownOKTrayWait == 200 && Variable.DownReadyOK == 200)// 下料良品Tray盘放满,下料OK补料工位无料
                                {
                                    if (Variable.XStatus[96] || Variable.XStatus[97])
                                    {
                                        Variable.OutAutoOKStartStep = 610;//去良品盘放料
                                        Variable.DownReadyEmpty = true;//OK备品料盘已取空
                                    }
                                    else
                                    {
                                        Variable.DownReadyEmpty = true;//OK备品料盘已取空
                                        Variable.OutAutoOKStartStep = 501;//OK盘移到备用位
                                    }
                                }
                            }
                            break;
                        }

                    case 501:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "501-->下料良品分料X轴回待机");
                            if (Variable.RunEnable == true)
                            {
                                if (!Variable.DownReadyEmpty || Variable.OutAutoFillStartStep == 10)
                                {
                                    if (Variable.XStatus[72])
                                    {
                                        if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                        {
                                            double pos = Variable.AxisPos[9, 0];
                                            Axis9SetMove(pos);//分料X轴回待机
                                            if (Variable.AIMpos[9] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[9] >= Math.Round(pos - 0.1, 2))
                                            {
                                                Variable.AxisAlarmTime[9] = 0;
                                                Variable.XAlarmTime[2] = 0;
                                                Variable.XAlarmTime[3] = 0;
                                                Variable.OutAutoOKStartStep = 70;
                                            }
                                            else
                                            {
                                                Variable.AxisAlarmTime[9] += 1;
                                            }
                                        }
                                        else
                                        {
                                            Variable.XAlarmTime[2] += 1;
                                            //Variable.DownAxisAlarm = true;
                                            //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                        }
                                    }
                                    else
                                    {
                                        Variable.XAlarmTime[3] += 1;
                                        //Variable.AlarmHappen[72] = true;
                                        //ListBoxTxt("下料良品Tray盘工位2上顶气缸不在下位，请确认！");
                                    }
                                }
                            }
                            break;
                        }

                    case 505://A吸嘴取OK料
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "505-->下料良品A吸嘴取OK料");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                {
                                    double pos9 = Variable.DownXReadyPositionA[Variable.DownReadyOK];
                                    double pos11 = Variable.DownYReadyPositionA[Variable.DownReadyOK];
                                    DownFillLineMove(pos9, pos11); //备料轴到备料盘里取OK料
                                    if (Variable.AIMpos[9] <= Math.Round(pos9 + 0.1, 2) && Variable.AIMpos[9] >= Math.Round(pos9 - 0.1, 2) && Variable.AIMpos[11] <= Math.Round(pos11 + 0.1, 2) && Variable.AIMpos[11] >= Math.Round(pos11 - 0.1, 2))
                                    {
                                        UpdateTxtDownOKTrayWait(Application.StartupPath + @"\Data\DownOKWait\tray");//更新OK盘
                                        UpdateTxtDownReadyOK(Application.StartupPath + @"\Data\DownReadyOK\tray");//更新备料OK盘
                                        Variable.AxisAlarmTime[9] = 0;
                                        Variable.AxisAlarmTime[10] = 0;
                                        Variable.XAlarmTime[2] = 0;
                                        Variable.OutAutoOKStartStep = 510;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[9] += 1;
                                        Variable.AxisAlarmTime[10] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[2] += 1;
                                    //Variable.DownAxisAlarm = true;
                                    //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }

                    case 510:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "510-->下料良品A吸嘴真空打开");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(62); //A吸嘴真空打开
                                Variable.OutAutoOKStartStep = 520;
                            }
                            break;
                        }
                    case 520:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "520-->下料良品A吸嘴分料上下缸下降");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(66); //A吸嘴分料上下缸下降
                                if (Variable.XStatus[99])
                                {
                                    Variable.OutAutoOKStartStep = 530;
                                }
                            }
                            break;
                        }

                    case 530:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "530-->下料良品轴13Z轴下降");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[13, 2] + Variable.offset[0];//补料盘，下料Z轴A吸嘴取料补偿
                                Axis13SetMove(pos); //轴13Z轴下降
                                if (Variable.AIMpos[13] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(pos - 0.1, 2))
                                {
                                    Thread.Sleep((int)Variable.DownAabsorb);
                                    Variable.AxisAlarmTime[13] = 0;
                                    Variable.OutAutoOKStartStep = 540;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[13] += 1;
                                }
                            }
                            break;
                        }

                    case 540:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "540-->下料良品A吸嘴分料上下缸上升");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(66);//A吸嘴分料上下缸上升
                                if (Variable.XStatus[98])
                                {
                                    Variable.OutAutoOKStartStep = 545;
                                }
                            }
                            break;
                        }
                    case 545:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "545-->下料良品轴13下分料Z轴上升");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[13, 0];
                                Axis13SetMove(pos); //轴13下分料Z轴上升
                                if (Variable.AIMpos[13] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[13] = 0;
                                    Variable.OutAutoOKStartStep = 550;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[13] += 1;
                                }
                            }
                            break;
                        }
                    case 550:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "550-->下料良品A吸嘴真空检测");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[96])
                                {
                                    Variable.OutAutoOKStartStep = 500;
                                }
                                else
                                {
                                    ListBoxTxt("A吸嘴吸OK料真空异常，请确认！");
                                    Variable.OutAutoOKStartStep = 555;
                                    RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "555-->下料良品A吸嘴吸OK料真空异常");
                                }
                            }
                            break;
                        }

                    case 560://B吸嘴取OK料
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "560-->下料良品B吸嘴取OK料");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                {
                                    double pos9 = Variable.DownXReadyPositionB[Variable.DownReadyOK];
                                    double pos11 = Variable.DownYReadyPositionB[Variable.DownReadyOK];
                                    DownFillLineMove(pos9, pos11); //备料轴到备料盘里取OK料
                                    if (Variable.AIMpos[9] <= Math.Round(pos9 + 0.1, 2) && Variable.AIMpos[9] >= Math.Round(pos9 - 0.1, 2) && Variable.AIMpos[11] <= Math.Round(pos11 + 0.1, 2) && Variable.AIMpos[11] >= Math.Round(pos11 - 0.1, 2))
                                    {
                                        UpdateTxtDownOKTrayWait(Application.StartupPath + @"\Data\DownOKWait\tray");//更新OK盘
                                        UpdateTxtDownReadyOK(Application.StartupPath + @"\Data\DownReadyOK\tray");//更新备料OK盘
                                        Variable.AxisAlarmTime[9] = 0;
                                        Variable.AxisAlarmTime[10] = 0;
                                        Variable.XAlarmTime[2] = 0;
                                        Variable.OutAutoOKStartStep = 570;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[9] += 1;
                                        Variable.AxisAlarmTime[10] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[2] += 1;
                                    //Variable.DownAxisAlarm = true;
                                    //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }

                    case 570:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "570-->下料良品B吸嘴真空打开");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(63); //B吸嘴真空打开
                                Variable.OutAutoOKStartStep = 580;
                            }
                            break;
                        }

                    case 580:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "580-->下料良品B吸嘴上下缸下降");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(67); //B吸嘴上下缸下降
                                if (Variable.XStatus[101])
                                {
                                    Variable.OutAutoOKStartStep = 585;
                                }
                            }
                            break;
                        }

                    case 585:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "585-->下料良品轴13Z轴下降");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[13, 2] + Variable.offset[0];//补料盘，下料Z轴B吸嘴取料补偿
                                Axis13SetMove(pos); //轴13Z轴下降
                                if (Variable.AIMpos[13] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(pos - 0.1, 2))
                                {
                                    Thread.Sleep((int)Variable.DownBabsorb);
                                    Variable.AxisAlarmTime[13] = 0;
                                    Variable.OutAutoOKStartStep = 590;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[13] += 1;
                                }
                            }
                            break;
                        }

                    case 590:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "590-->下料良品B吸嘴上下缸上升");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(67); //B吸嘴上下缸上升
                                if (Variable.XStatus[100])
                                {
                                    Variable.OutAutoOKStartStep = 595;
                                }
                            }
                            break;
                        }
                    case 595:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "595-->下料良品轴13下分料Z轴上升");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[13, 0];
                                Axis13SetMove(pos); //轴13下分料Z轴上升
                                if (Variable.AIMpos[13] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[13] = 0;
                                    Variable.OutAutoOKStartStep = 600;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[13] += 1;
                                }
                            }
                            break;
                        }
                    case 600:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "600-->下料良品B吸嘴真空检测");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[97])
                                {
                                    Variable.OutAutoOKStartStep = 500;
                                }
                                else
                                {
                                    ListBoxTxt("B吸嘴吸OK料真空异常，请确认！");
                                    Variable.OutAutoOKStartStep = 605;
                                    RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "605-->下料良品B吸嘴吸OK料真空异常");
                                }
                            }
                            break;
                        }

                    //***************去良品盘放料***************

                    case 610://去良品盘放料
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "610-->下料良品去良品盘放料");
                            if (Variable.RunEnable == true)
                            {
                                Variable.DownOKTrayFull = ReadTxtJudgeOK(Application.StartupPath + @"\Data\DownOK\tray");//搜索OK盘空位料

                                if (Variable.DownOKTrayFull != 200)//良品Tray盘有空位
                                {
                                    if (Variable.XStatus[96])//A吸嘴有料
                                    {
                                        Variable.OutAutoOKStartStep = 630;//A吸嘴放OK料
                                    }
                                    else
                                    {
                                        if (Variable.XStatus[97])//B吸嘴有料
                                        {
                                            Variable.OutAutoOKStartStep = 700;//B吸嘴放OK料
                                        }
                                        else
                                        {
                                            Variable.OutAutoOKStartStep = 500;//取料
                                        }
                                    }
                                }
                                else//良品Tray盘已满，收Tray盘
                                {
                                    Variable.OutAutoOKStartStep = 800;//收OK Tray盘
                                }
                            }
                            break;
                        }

                    case 630://A吸嘴放OK料
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "630-->下料良品A吸嘴放OK料");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[72])
                                {
                                    if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                    {
                                        double pos9 = Variable.DownXOKTrayPositionA[Variable.DownOKTrayFull];
                                        double pos10 = Variable.DownYOKTrayPositionA[Variable.DownOKTrayFull];
                                        DownOKLineMove(pos9, pos10); //良品轴到OK盘放OK料
                                        if (Variable.AIMpos[9] <= Math.Round(pos9 + 0.1, 2) && Variable.AIMpos[9] >= Math.Round(pos9 - 0.1, 2) && Variable.AIMpos[10] <= Math.Round(pos10 + 0.1, 2) && Variable.AIMpos[10] >= Math.Round(pos10 - 0.1, 2))
                                        {
                                            UpdateTxtDownOKTrayFull(Application.StartupPath + @"\Data\DownOK\tray");
                                            //Thread.Sleep(200);
                                            string[] Readstr = myTXT.ReadTXT(Application.StartupPath + @"\Data\DownOK\tray");
                                            myTXT.WriteTxt(Readstr, Application.StartupPath + @"\Data\DownOKWait\tray");//读取DownOK文本到DownOKWait中
                                            Variable.AxisAlarmTime[9] = 0;
                                            Variable.AxisAlarmTime[10] = 0;
                                            Variable.XAlarmTime[2] = 0;
                                            Variable.XAlarmTime[3] = 0;
                                            Variable.OutAutoOKStartStep = 640;
                                        }
                                        else
                                        {
                                            Variable.AxisAlarmTime[9] += 1;
                                            Variable.AxisAlarmTime[10] += 1;
                                        }
                                    }
                                    else
                                    {
                                        Variable.XAlarmTime[2] += 1;
                                        //Variable.DownAxisAlarm = true;
                                        //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[3] += 1;
                                    //Variable.AlarmHappen[72] = true;
                                    //ListBoxTxt("下料良品Tray盘工位2上顶气缸不在下位，请确认！");
                                }
                            }
                            break;
                        }

                    case 640:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "640-->下料良品A吸嘴气缸下降");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(66); //A吸嘴气缸下降
                                if (Variable.XStatus[99])
                                {
                                    Variable.OutAutoOKStartStep = 650;
                                }
                            }
                            break;
                        }

                    case 650:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "650-->下料良品轴13分料Z轴移动到良品OK放料位");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[13, 1] + Variable.offset[0];//良品位，上料Z轴A吸嘴放料补偿
                                Axis13SetMove(pos); //轴13分料Z轴移动到良品OK放料位
                                if (Variable.AIMpos[13] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[13] = 0;
                                    Variable.OutAutoOKStartStep = 660;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[13] += 1;
                                }
                            }
                            break;
                        }

                    case 660:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "660-->下料良品A吸嘴真空关闭");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(62); //A吸嘴真空关闭
                                function.OutYON(64); //A吸嘴破真空打开
                                Thread.Sleep((int)Variable.DownAbroken);
                                function.OutYOFF(64);//A吸嘴破真空关闭
                                Variable.OutAutoOKStartStep = 670;
                            }
                            break;
                        }

                    case 670:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "670-->下料良品A吸嘴气缸上升");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(66);//A吸嘴气缸上升
                                if (Variable.XStatus[98])
                                {
                                    Variable.OutAutoOKStartStep = 680;
                                }
                            }
                            break;
                        }

                    case 680:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "680-->下料良品轴13Z轴移动到待机位");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[13, 0];
                                Axis13SetMove(pos); //轴13Z轴移动到待机位
                                if (Variable.AIMpos[13] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[13] = 0;
                                    Variable.OutAutoOKStartStep = 610;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[13] += 1;
                                }
                            }
                            break;
                        }

                    case 700://B吸嘴放OK料
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "700-->下料良品B吸嘴放OK料");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[72])
                                {
                                    if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                    {
                                        double pos9 = Variable.DownXOKTrayPositionB[Variable.DownOKTrayFull];
                                        double pos10 = Variable.DownYOKTrayPositionB[Variable.DownOKTrayFull];
                                        DownOKLineMove(pos9, pos10); //良品轴到OK盘放OK料
                                        if (Variable.AIMpos[9] <= Math.Round(pos9 + 0.1, 2) && Variable.AIMpos[9] >= Math.Round(pos9 - 0.1, 2) && Variable.AIMpos[10] <= Math.Round(pos10 + 0.1, 2) && Variable.AIMpos[10] >= Math.Round(pos10 - 0.1, 2))
                                        {
                                            UpdateTxtDownOKTrayFull(Application.StartupPath + @"\Data\DownOK\tray");
                                            //Thread.Sleep(200);
                                            string[] Readstr = myTXT.ReadTXT(Application.StartupPath + @"\Data\DownOK\tray");
                                            myTXT.WriteTxt(Readstr, Application.StartupPath + @"\Data\DownOKWait\tray");//读取DownOK文本到DownOKWait中
                                            Variable.AxisAlarmTime[9] = 0;
                                            Variable.AxisAlarmTime[10] = 0;
                                            Variable.XAlarmTime[2] = 0;
                                            Variable.XAlarmTime[3] = 0;
                                            Variable.OutAutoOKStartStep = 710;
                                        }
                                        else
                                        {
                                            Variable.AxisAlarmTime[9] += 1;
                                            Variable.AxisAlarmTime[10] += 1;
                                        }
                                    }
                                    else
                                    {
                                        Variable.XAlarmTime[2] += 1;
                                        //Variable.DownAxisAlarm = true;
                                        //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[3] += 1;
                                    //Variable.AlarmHappen[72] = true;
                                    //ListBoxTxt("下料良品Tray盘工位2上顶气缸不在下位，请确认！");
                                }
                            }
                            break;
                        }

                    case 710:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "710-->下料良品B吸嘴气缸下降");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(67); //B吸嘴气缸下降
                                if (Variable.XStatus[101])
                                {
                                    Variable.OutAutoOKStartStep = 720;
                                }
                            }
                            break;
                        }

                    case 720:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "720-->下料良品轴13分料Z轴移动到NG取料位");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[13, 1] + Variable.offset[0];//良品位，下料Z轴A吸嘴放料补偿
                                Axis13SetMove(pos); //轴13分料Z轴移动到NG取料位
                                if (Variable.AIMpos[13] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[13] = 0;
                                    Variable.OutAutoOKStartStep = 730;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[13] += 1;
                                }
                            }
                            break;
                        }

                    case 730:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "730-->下料良品B吸嘴真空关闭");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(63); //B吸嘴真空关闭
                                function.OutYON(65); //B吸嘴破真空打开
                                Thread.Sleep((int)Variable.DownBbroken);
                                function.OutYOFF(65); //B吸嘴破真空关闭
                                Variable.OutAutoOKStartStep = 740;
                            }
                            break;
                        }

                    case 740:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "740-->下料良品B吸嘴气缸上升");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(67); //B吸嘴气缸上升
                                if (Variable.XStatus[100])
                                {
                                    Variable.OutAutoOKStartStep = 750;
                                }
                            }
                            break;
                        }

                    case 750:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "750-->下料良品轴13分料Z轴移动到待机位");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[13, 0];
                                Axis13SetMove(pos); //轴13分料Z轴移动到待机位
                                if (Variable.AIMpos[13] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[13] = 0;
                                    Variable.OutAutoOKStartStep = 610;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[13] += 1;
                                }
                            }
                            break;
                        }

                    //***************OK满料满盘收取***************

                    case 800:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "800-->下料良品OK满料满盘收取");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[72])
                                {
                                    if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                    {
                                        double pos9 = Variable.AxisPos[9, 0];
                                        double pos10 = Variable.AxisPos[10, 3];
                                        Axis9SetMove(pos9); //轴9移动到待机位
                                        Axis10SetMove(pos10); //轴10良品Y轴移动到满OKTray位
                                        if (Variable.AIMpos[9] <= Math.Round(pos9 + 0.1, 2) && Variable.AIMpos[9] >= Math.Round(pos9 - 0.1, 2) && Variable.AIMpos[10] <= Math.Round(pos10 + 0.1, 2) && Variable.AIMpos[10] >= Math.Round(pos10 - 0.1, 2))
                                        {
                                            Variable.AxisAlarmTime[9] = 0;
                                            Variable.AxisAlarmTime[10] = 0;
                                            Variable.XAlarmTime[2] = 0;
                                            Variable.XAlarmTime[3] = 0;
                                            Variable.OutAutoOKStartStep = 810;
                                        }
                                        else
                                        {
                                            Variable.AxisAlarmTime[9] += 1;
                                            Variable.AxisAlarmTime[10] += 1;
                                        }
                                    }
                                    else
                                    {
                                        Variable.XAlarmTime[2] += 1;
                                        //Variable.DownAxisAlarm = true;
                                        //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[3] += 1;
                                    //Variable.AlarmHappen[72] = true;
                                    //ListBoxTxt("下料良品Tray盘工位2上顶气缸不在下位，请确认！");
                                }
                            }
                            break;
                        }

                    case 810:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "810-->下料良品夹紧气缸松开");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(55);//夹紧气缸松开
                                if (!Variable.XStatus[77])
                                {
                                    Variable.OutAutoOKStartStep = 820;
                                }
                            }
                            break;
                        }

                    case 820:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "820-->下料良品Tray盘工位1有无盘感应");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[64])
                                {
                                    Variable.OutAutoOKStartStep = 826;
                                }
                                else
                                {
                                    ListBoxTxt("下料良品Tray未到工位1，请确认");
                                    Variable.OutAutoOKStartStep = 825;
                                    RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "825-->下料良品Tray未到工位1");
                                }
                            }
                            break;
                        }
                    case 826:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "826-->下料良品上顶气缸上升");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(52);//上顶气缸上升
                                if (Variable.XStatus[67])
                                {
                                    Thread.Sleep(500);
                                    Variable.OutAutoOKStartStep = 830;
                                }
                            }
                            break;
                        }

                    case 830:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "830-->下料良品上顶气缸下降");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(52); //上顶气缸下降
                                if (Variable.XStatus[68])
                                {
                                    Variable.OutAutoOKStartStep = 840;
                                }
                            }
                            break;
                        }

                    case 840:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "840-->下料良品轴10良品Y轴移动到待机位");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                {
                                    double pos = Variable.AxisPos[10, 0];
                                    Axis10SetMove(pos);//轴10良品Y轴移动到待机位
                                    if (Variable.AIMpos[10] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[10] >= Math.Round(pos - 0.1, 2))
                                    {
                                        Variable.AxisAlarmTime[10] = 0;
                                        Variable.XAlarmTime[2] = 0;
                                        Variable.OutAutoOKStartStep = 860;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[10] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[2] += 1;
                                    //Variable.DownAxisAlarm = true;
                                    //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }

                    case 860:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "860-->下料良品判断测试结束");
                            if (Variable.RunEnable == true)
                            {
                                bool data = true;
                                for (int i = 0; i < 20; i++)
                                {
                                    if (Variable.ModelState[i] != 10 && Variable.ModelState[i] != 0)
                                    {
                                        data = false;
                                        break;
                                    }
                                }

                                bool data1 = true;
                                for (int i = 0; i < 20; i++)
                                {
                                    if (ModelIsNull((i + 1).ToString()))//有test
                                    {
                                        data1 = false;
                                        break;
                                    }
                                }

                                bool data3 = true;
                                for (int i = 0; i < 20; i++)
                                {
                                    if (ModelIsNull1((i + 1).ToString()))//有test
                                    {
                                        data3 = false;
                                        break;
                                    }
                                }

                                //判断探针是否都没有
                                bool data4 = false;
                                for (int i = 0; i < 10; i++)
                                {
                                    if (Variable.XStatus[117 + i * 32])
                                    {
                                        data4 = true;
                                        break;
                                    }
                                    if (Variable.XStatus[120 + i * 32])
                                    {
                                        data4 = true;
                                        break;
                                    }
                                    if (Variable.XStatus[131 + i * 32])
                                    {
                                        data4 = true;
                                        break;
                                    }
                                    if (Variable.XStatus[135 + i * 32])
                                    {
                                        data4 = true;
                                        break;
                                    }
                                }

                                Thread.Sleep(5000);
                                //判断测试结束
                                if (Variable.XStatus[49] && !Variable.XStatus[56] && Variable.RobotGet == 0 && data && data1 && data3 && !data4 && (Variable.CleanOut || Variable.CleanOne) && Variable.INAutoReadyStep == 800 && Variable.XStatus[24] && Variable.inTrayNum == Variable.outTrayNum)
                                {
                                    Variable.OutAutoOKStartStep = 1000;
                                }
                                else
                                {
                                    if (Variable.inTrayNum == Variable.outTrayNum)
                                    {
                                        Variable.OutAutoOKStartStep = 1000;
                                    }
                                    else
                                    {
                                        Variable.OutAutoOKStartStep = 870;
                                    }
                                }
                            }
                            break;
                        }

                    case 870:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "870-->下料良品Tray工位1Tray盘是否已满");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[65])
                                {
                                    ListBoxTxt("下料良品Tray工位1Tray盘已满，请取走");
                                    Variable.OutAutoOKStartStep = 875;
                                    RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "875-->下料良品Tray工位1Tray盘已满");
                                }
                                else
                                {
                                    Variable.OutAutoOKStartStep = 900;
                                }
                            }
                            break;
                        }

                    case 900:
                        {
                            RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "900-->下料良品此盘NG数量是否过多");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.OKTrayNGCountFlag)//OK盘NG超过一半
                                {
                                    if (Variable.CheckDownOK)
                                    {
                                        Variable.OutAutoOKStartStep = 950;
                                        RSDAlarmOutAutoOKStartStep(Variable.AutoStepMsg[4] = "950-->下料良品此盘NG数量过多");
                                    }
                                    else
                                    {
                                        Variable.OutAutoOKStartStep = 10;
                                    }
                                }
                                else
                                {
                                    Variable.OutAutoOKStartStep = 10;
                                }
                            }
                            break;
                        }
                }
                Thread.Sleep(1);
            }

        }
        #endregion=-

        #region 下料补料自动流程
        public void OutAutoFillStart()
        {
            while (true)
            {
                switch (Variable.OutAutoFillStartStep)
                {

                    //***************判断是否取Tray盘***************
                    case 10:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "10-->判断工位3有料，工位2无料");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[83] && Variable.OutFillTrayFlag == false)//判断工位3有料，工位2无料
                                {
                                    Variable.OutAutoFillStartStep = 20;//无料
                                }
                                else if (Variable.OutFillTrayFlag == true)
                                {
                                    Variable.OutAutoFillStartStep = 40;//有料
                                }
                            }
                            break;
                        }

                    case 20:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "20-->下料补料Y轴移动到OKTray备料位");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[102])//移Tray夹爪上位
                                {
                                    if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                    {
                                        double pos = Variable.AxisPos[11, 1];
                                        Axis11SetMove(pos); //轴11下料补料Y轴移动到OKTray备料位
                                        if (Variable.AIMpos[11] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[11] >= Math.Round(pos - 0.1, 2))
                                        {
                                            Variable.AxisAlarmTime[11] = 0;
                                            Variable.XAlarmTime[2] = 0;
                                            Variable.OutAutoFillStartStep = 25;
                                        }
                                        else
                                        {
                                            Variable.AxisAlarmTime[11] += 1;
                                        }
                                    }
                                    else
                                    {
                                        Variable.XAlarmTime[2] += 1;
                                        //Variable.DownAxisAlarm = true;
                                        //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                    }
                                }
                            }
                            break;
                        }
                    case 25:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "25-->下料补料轨道夹紧气缸夹紧");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(58);//轨道夹紧气缸夹紧
                                if (Variable.XStatus[85])
                                {
                                    Variable.OutAutoFillStartStep = 30;
                                }
                            }
                            break;
                        }

                    //***************备品OKTray移到工位2***************
                    case 30:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "30-->下料备品Y轴移动到待机位");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                {
                                    double pos = Variable.AxisPos[11, 0];
                                    Axis11SetMove(pos); //轴11下料备品Y轴移动到待机位
                                    if (Variable.AIMpos[11] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[11] >= Math.Round(pos - 0.1, 2))
                                    {
                                        Variable.AxisAlarmTime[11] = 0;
                                        Variable.XAlarmTime[2] = 0;
                                        Variable.OutAutoFillStartStep = 31;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[11] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[2] += 1;
                                    //Variable.DownAxisAlarm = true;
                                    //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }

                    case 31:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "31-->判断下料补料Tray盘工位3是否有盘");
                            if (Variable.RunEnable == true)
                            {
                                if (!Variable.XStatus[83])//下料补料Tray盘工位3有盘
                                {
                                    Variable.OutAutoFillStartStep = 32;
                                }
                                else
                                {
                                    ListBoxTxt("下料补料Tray盘工位3Tray未取走,请确认！");
                                    Variable.OutAutoFillStartStep = 33;
                                    RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "33-->下料补料Tray盘工位3Tray未取走,请确认");
                                }
                            }
                            break;
                        }
                    case 32:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "32-->下料补料轨道夹紧气缸松开");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(58); //轨道夹紧气缸松开
                                if (!Variable.XStatus[85])
                                {
                                    Thread.Sleep(100);
                                    Variable.OutAutoFillStartStep = 35;
                                }
                            }
                            break;
                        }
                    case 35:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "35-->下料补料侧顶气缸伸出");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(57); //侧顶气缸伸出
                                if (!Variable.XStatus[82])
                                {
                                    Thread.Sleep(500);
                                    Variable.OutAutoFillStartStep = 36;
                                }
                            }
                            break;
                        }

                    case 36:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "36-->下料补料轨道夹紧气缸夹紧");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(58); //轨道夹紧气缸夹紧
                                if (Variable.XStatus[85])
                                {
                                    Thread.Sleep(100);
                                    Variable.OutAutoFillStartStep = 37;
                                }
                            }
                            break;
                        }

                    case 37:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "37-->下料补料侧顶气缸缩回");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(57); //侧顶气缸缩回
                                if (Variable.XStatus[82])
                                {
                                    Thread.Sleep(100);
                                    Variable.OutAutoFillStartStep = 38;
                                }
                            }
                            break;
                        }

                    case 38:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "38-->下料补料工位2有料标志位");
                            if (Variable.RunEnable == true)
                            {
                                Variable.OutFillTrayFlag = true;//补料工位2有料标志位                                    
                                Variable.OutAutoFillStartStep = 40;
                            }
                            break;
                        }

                    //***************判断是否有NG料***************

                    case 40://有NG料
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "40-->下料补料判断是否有NG料");
                            if (Variable.RunEnable == true)
                            {
                                Variable.DownReadyTrayOK = ReadTxtJudge(Application.StartupPath + @"\Data\DownReadyOK\tray");//搜索是否有NG料，0是OK，不等于0是NG，10是空位
                                if (Variable.DownReadyTrayOK != 200)//有NG料
                                {
                                    if (Variable.XStatus[96])//A吸嘴有料
                                    {
                                        if (Variable.XStatus[97])//B吸嘴有料
                                        {
                                            Variable.OutAutoFillStartStep = 150;//去放料
                                        }
                                        else
                                        {
                                            Variable.OutAutoFillStartStep = 100;//B吸嘴去吸料
                                        }

                                    }
                                    else
                                    {
                                        Variable.OutAutoFillStartStep = 50;//A吸嘴去吸料
                                    }

                                }
                                else//无NG料
                                {
                                    if (Variable.XStatus[96] || Variable.XStatus[97])
                                    {
                                        Variable.OutAutoFillStartStep = 150;//去放料
                                    }
                                    else
                                    {
                                        Variable.OutAutoFillStartStep = 300;//等待补OK品
                                    }
                                }
                            }
                            break;
                        }

                    case 50://A吸嘴取NG料
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "50-->下料补料A吸嘴取NG料");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                {
                                    double pos9 = Variable.DownXReadyPositionA[Variable.DownReadyTrayOK];
                                    double pos11 = Variable.DownYReadyPositionA[Variable.DownReadyTrayOK];
                                    DownFillLineMove(pos9, pos11); //备料轴到备料盘取NG品

                                    if (Variable.AIMpos[9] <= Math.Round(pos9 + 0.1, 2) && Variable.AIMpos[9] >= Math.Round(pos9 - 0.1, 2) && Variable.AIMpos[11] <= Math.Round(pos11 + 0.1, 2) && Variable.AIMpos[11] >= Math.Round(pos11 - 0.1, 2))
                                    {
                                        UpdateTxtDownReadyTrayOK(Application.StartupPath + @"\Data\DownReadyOK\tray");
                                        Variable.AxisAlarmTime[9] = 0;
                                        Variable.AxisAlarmTime[11] = 0;
                                        Variable.XAlarmTime[2] = 0;
                                        Variable.OutAutoFillStartStep = 60;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[9] += 1;
                                        Variable.AxisAlarmTime[11] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[2] += 1;
                                    //Variable.DownAxisAlarm = true;
                                    //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }
                    case 60:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "60-->下料补料A吸嘴真空打开");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(62); //A吸嘴真空打开                               
                                Variable.OutAutoFillStartStep = 70;
                            }
                            break;
                        }
                    case 70:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "70-->下料补料A吸嘴上下缸下降");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(66); //A吸嘴上下缸下降
                                if (Variable.XStatus[99])
                                {
                                    Variable.OutAutoFillStartStep = 80;
                                }
                            }
                            break;
                        }

                    case 80:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "80-->下料补料分料Z轴下降");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[13, 2] + Variable.offset[0];//备料位，下料Z轴A吸嘴取料补偿
                                Axis13SetMove(pos); //轴13分料Z轴下降
                                if (Variable.AIMpos[13] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(pos - 0.1, 2))
                                {
                                    Thread.Sleep((int)Variable.DownAabsorb);
                                    Variable.AxisAlarmTime[13] = 0;
                                    Variable.OutAutoFillStartStep = 90;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[13] += 1;
                                }
                            }
                            break;
                        }

                    case 90:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "90-->下料补料A吸嘴上下缸上升");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(66); //A吸嘴上下缸上升
                                if (Variable.XStatus[98])
                                {
                                    Variable.OutAutoFillStartStep = 91;
                                }
                            }
                            break;
                        }
                    case 91:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "91-->下料补料分料Z轴上升");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[13, 0];
                                Axis13SetMove(pos); //轴13分料Z轴上升
                                if (Variable.AIMpos[13] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[13] = 0;
                                    Variable.OutAutoFillStartStep = 95;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[13] += 1;
                                }
                            }
                            break;
                        }
                    case 95:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "95-->判断下料补料A吸嘴吸NG料真空是否OK");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[96])
                                {
                                    Variable.OutAutoFillStartStep = 40;
                                }
                                else
                                {
                                    ListBoxTxt("A吸嘴吸NG料真空异常，请确认！");
                                    Variable.OutAutoFillStartStep = 96;
                                    RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "96-->判断下料补料A吸嘴吸NG料真空异常");
                                }
                            }
                            break;
                        }

                    case 100://B吸嘴取NG料
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "100-->判断下料补料B吸嘴取NG料");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                {
                                    double pos9 = Variable.DownXReadyPositionB[Variable.DownReadyTrayOK];
                                    double pos11 = Variable.DownYReadyPositionB[Variable.DownReadyTrayOK];
                                    DownFillLineMove(pos9, pos11); //备料轴到备料盘取NG品
                                    if (Variable.AIMpos[9] <= Math.Round(pos9 + 0.1, 2) && Variable.AIMpos[9] >= Math.Round(pos9 - 0.1, 2) && Variable.AIMpos[11] <= Math.Round(pos11 + 0.1, 2) && Variable.AIMpos[11] >= Math.Round(pos11 - 0.1, 2))
                                    {
                                        UpdateTxtDownReadyTrayOK(Application.StartupPath + @"\Data\DownReadyOK\tray");
                                        Variable.AxisAlarmTime[9] = 0;
                                        Variable.AxisAlarmTime[11] = 0;
                                        Variable.XAlarmTime[2] = 0;
                                        Variable.OutAutoFillStartStep = 110;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[9] += 1;
                                        Variable.AxisAlarmTime[11] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[2] += 1;
                                    //Variable.DownAxisAlarm = true;
                                    //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }
                    case 110:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "110-->下料补料B吸嘴真空打开");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(63); //B吸嘴真空打开                               
                                Variable.OutAutoFillStartStep = 120;
                            }
                            break;
                        }
                    case 120:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "120-->下料补料B吸嘴上下缸下降");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(67); //B吸嘴上下缸下降
                                if (Variable.XStatus[101])
                                {
                                    Variable.OutAutoFillStartStep = 125;
                                }
                            }
                            break;
                        }

                    case 125:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "125-->下料补料分料Z轴B吸嘴吸料补偿");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[13, 2] + Variable.offset[0];//备料位，下料Z轴B吸嘴吸料补偿
                                Axis13SetMove(pos); //轴13分料Z轴B吸嘴吸料补偿
                                if (Variable.AIMpos[13] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(pos - 0.1, 2))
                                {
                                    Thread.Sleep((int)Variable.DownBabsorb);
                                    Variable.AxisAlarmTime[13] = 0;
                                    Variable.OutAutoFillStartStep = 130;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[13] += 1;
                                }
                            }
                            break;
                        }

                    case 130:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "130-->下料补料B吸嘴上下缸上升");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(67); //B吸嘴上下缸上升
                                if (Variable.XStatus[100])
                                {
                                    Variable.OutAutoFillStartStep = 135;
                                }
                            }
                            break;
                        }
                    case 135:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "135-->下料补料分料Z轴上升");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[13, 0];
                                Axis13SetMove(pos); //轴13分料Z轴上升
                                if (Variable.AIMpos[13] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[13] = 0;
                                    Variable.OutAutoFillStartStep = 140;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[13] += 1;
                                }
                            }
                            break;
                        }
                    case 140:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "140-->判断下料补料B吸嘴吸NG料真空是否OK");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[97])
                                {
                                    Variable.OutAutoFillStartStep = 40;
                                }
                                else
                                {
                                    ListBoxTxt("B吸嘴吸NG料真空异常，请确认！");
                                    Variable.OutAutoFillStartStep = 145;
                                }
                            }
                            break;
                        }

                    case 150://去NG放料
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "150-->判断下料补料吸嘴放料");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.DownNGTrayOK == true)
                                {

                                    Variable.DownNGTray = ReadTxtJudgeOK(Application.StartupPath + @"\Data\DownNG\tray");//搜索是否有NG料，0是OK，不等于0是NG，10是空位
                                    if (Variable.DownNGTray != 200)//NGTray盘有空位
                                    {
                                        if (Variable.XStatus[96])//A吸嘴有料
                                        {
                                            Variable.OutAutoFillStartStep = 160;//A吸嘴放NG料
                                        }
                                        else
                                        {
                                            if (Variable.XStatus[97])//B吸嘴有料
                                            {
                                                Variable.OutAutoFillStartStep = 230;//B吸嘴放NG料
                                            }
                                            else
                                            {
                                                Variable.OutAutoFillStartStep = 40;//取料
                                            }
                                        }
                                    }
                                    else//NGTray盘已满，收NGTray盘
                                    {
                                        Variable.DownNGTrayFull = true;
                                    }
                                }
                            }
                            break;
                        }

                    case 160://A吸嘴放NG料
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "160-->下料补料A吸嘴放NG料");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                {
                                    double pos9 = Variable.DownXNGTrayPositionA[Variable.DownNGTray];
                                    double pos12 = Variable.DownYNGTrayPositionA[Variable.DownNGTray];
                                    DownNGLineMove(pos9, pos12); //NG轴去NG盘放NG品
                                    if (Variable.AIMpos[9] <= Math.Round(pos9 + 0.1, 2) && Variable.AIMpos[9] >= Math.Round(pos9 - 0.1, 2) && Variable.AIMpos[12] <= Math.Round(pos12 + 0.1, 2) && Variable.AIMpos[12] >= Math.Round(pos12 - 0.1, 2))
                                    {
                                        UpdateTxtDownNGTray(Application.StartupPath + @"\Data\DownNG\tray");
                                        Variable.AxisAlarmTime[9] = 0;
                                        Variable.AxisAlarmTime[12] = 0;
                                        Variable.XAlarmTime[2] = 0;
                                        Variable.OutAutoFillStartStep = 170;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[9] += 1;
                                        Variable.AxisAlarmTime[12] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[2] += 1;
                                    //Variable.DownAxisAlarm = true;
                                    //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }

                    case 170:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "170-->下料补料A吸嘴气缸下降");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(66);//A吸嘴气缸下降
                                if (Variable.XStatus[99])
                                {
                                    Variable.OutAutoFillStartStep = 180;
                                }
                            }
                            break;
                        }

                    case 180:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "180-->下料补料Z轴移动到NG取料位");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[13, 3] + Variable.offset[0];//NG位，下料Z轴A吸嘴放料补偿
                                Axis13SetMove(pos); //轴13Z轴移动到NG取料位
                                if (Variable.AIMpos[13] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[13] = 0;
                                    Variable.OutAutoFillStartStep = 190;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[13] += 1;
                                }
                            }
                            break;
                        }

                    case 190:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "190-->下料补料A吸嘴真空关闭");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(62); //A吸嘴真空关闭
                                function.OutYON(64); //A吸嘴破真空打开
                                Thread.Sleep((int)Variable.DownAbroken);
                                function.OutYOFF(64); //A吸嘴破真空关闭
                                Variable.OutAutoFillStartStep = 200;
                            }
                            break;
                        }

                    case 200:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "200-->下料补料A吸嘴气缸上升");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(66); //A吸嘴气缸上升
                                if (Variable.XStatus[98])
                                {
                                    Variable.OutAutoFillStartStep = 210;
                                }
                            }
                            break;
                        }

                    case 210:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "210-->下料补料Z轴移动到待机位");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[13, 0];
                                Axis13SetMove(pos); //轴7Z轴移动到待机位
                                if (Variable.AIMpos[13] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[13] = 0;
                                    Variable.OutAutoFillStartStep = 150;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[13] += 1;
                                }
                            }
                            break;
                        }

                    case 230://B吸嘴放NG料
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "230-->下料补料B吸嘴放NG料");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                {
                                    double pos9 = Variable.DownXNGTrayPositionB[Variable.DownNGTray];
                                    double pos12 = Variable.DownYNGTrayPositionB[Variable.DownNGTray];
                                    DownNGLineMove(pos9, pos12); //NG轴去NG盘放NG品
                                    if (Variable.AIMpos[9] <= Math.Round(pos9 + 0.1, 2) && Variable.AIMpos[9] >= Math.Round(pos9 - 0.1, 2) && Variable.AIMpos[12] <= Math.Round(pos12 + 0.1, 2) && Variable.AIMpos[12] >= Math.Round(pos12 - 0.1, 2))
                                    {
                                        UpdateTxtDownNGTray(Application.StartupPath + @"\Data\DownNG\tray");
                                        Variable.AxisAlarmTime[9] = 0;
                                        Variable.AxisAlarmTime[12] = 0;
                                        Variable.XAlarmTime[2] = 0;
                                        Variable.OutAutoFillStartStep = 240;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[9] += 1;
                                        Variable.AxisAlarmTime[12] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[2] += 1;
                                    //Variable.DownAxisAlarm = true;
                                    //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }

                    case 240:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "240-->下料补料B吸嘴气缸下降");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(67); //B吸嘴气缸下降
                                if (Variable.XStatus[101])
                                {
                                    Variable.OutAutoFillStartStep = 250;
                                }
                            }
                            break;
                        }

                    case 250:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "250-->下料补料Z轴移动到NG取料位");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[13, 3] + Variable.offset[0];
                                Axis13SetMove(pos); //轴13Z轴移动到NG取料位
                                if (Variable.AIMpos[13] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[13] = 0;
                                    Variable.OutAutoFillStartStep = 260;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[13] += 1;
                                }
                            }
                            break;
                        }

                    case 260:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "260-->下料补料B吸嘴真空关闭");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(63); //B吸嘴真空关闭
                                function.OutYON(65); //B吸嘴破真空打开
                                Thread.Sleep((int)Variable.DownBbroken);
                                function.OutYOFF(65); //B吸嘴破真空关闭
                                Variable.OutAutoFillStartStep = 270;
                            }
                            break;
                        }

                    case 270:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "270-->下料补料B吸嘴气缸上升");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(67); //B吸嘴气缸上升
                                if (Variable.XStatus[100])
                                {
                                    Variable.OutAutoFillStartStep = 280;
                                }
                            }
                            break;
                        }

                    case 280:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "280-->下料补料Z轴移动到待机位");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[13, 0];
                                Axis13SetMove(pos); //轴13Z轴移动到待机位
                                if (Variable.AIMpos[13] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[13] = 0;
                                    Variable.OutAutoFillStartStep = 150;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[13] += 1;
                                }
                            }
                            break;
                        }

                    //***************补料盘都是OK品，等待良品盘备用***************
                    case 300:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "300-->下料补料盘都是OK品，等待良品盘备用");
                            if (Variable.RunEnable == true)
                            {
                                //if (Variable.XStatus[72])
                                //{
                                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                {
                                    double pos = Variable.AxisPos[9, 0];
                                    Axis9SetMove(pos); //轴9X轴移动到待机位
                                    if (Variable.AIMpos[9] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[9] >= Math.Round(pos - 0.1, 2))
                                    {
                                        Variable.AxisAlarmTime[9] = 0;
                                        Variable.XAlarmTime[2] = 0;
                                        Variable.XAlarmTime[3] = 0;
                                        Variable.OutAutoFillStartStep = 305;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[9] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[2] += 1;
                                    //Variable.DownAxisAlarm = true;
                                    //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                }
                                //}
                                //else
                                //{
                                //    Variable.XAlarmTime[3] += 1;
                                //    //Variable.AlarmHappen[72] = true;
                                //    //ListBoxTxt("下料良品Tray盘工位2上顶气缸不在下位，请确认！");
                                //}
                            }
                            break;
                        }
                    case 305:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "305-->下料补料备品盘产品都位OK");
                            if (Variable.RunEnable == true)
                            {
                                Variable.DownReadyTrayFullOK = true;//备品盘产品都位OK
                                Variable.OutAutoFillStartStep = 310;
                            }
                            break;
                        }

                    case 310:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "310-->下料补料判断是否结批");
                            if (Variable.RunEnable == true)
                            {
                                //判断探针是否都没有
                                bool data4 = false;
                                for (int i = 0; i < 10; i++)
                                {
                                    if (Variable.XStatus[117 + i * 32])
                                    {
                                        data4 = true;
                                        break;
                                    }
                                    if (Variable.XStatus[120 + i * 32])
                                    {
                                        data4 = true;
                                        break;
                                    }
                                    if (Variable.XStatus[131 + i * 32])
                                    {
                                        data4 = true;
                                        break;
                                    }
                                    if (Variable.XStatus[135 + i * 32])
                                    {
                                        data4 = true;
                                        break;
                                    }
                                }

                                if (Variable.DownReadyEmpty)//备品盘取空
                                {
                                    Variable.DownReadyTrayFullOK = false;//备品盘产品都位OK
                                    Variable.OutAutoFillStartStep = 400;
                                }
                                else if (Variable.OutAutoOKStartStep == 1000 && (Variable.CleanOne || Variable.CleanOut))//结批中
                                {
                                    Variable.DownReadyTrayFullOK = false;//备品盘产品都位OK
                                    Variable.OutAutoFillStartStep = 400;
                                }
                                //判断测试结束
                                else if (Variable.OutAutoOKStartStep == 45 && !data4 && Variable.RobotGet == 0 && (Variable.CleanOut || Variable.CleanOne))
                                {
                                    Variable.DownReadyTrayFullOK = false;//备品盘产品都位OK
                                    Variable.OutAutoOKStartStep = 1000;
                                    Variable.OutAutoFillStartStep = 400;
                                }
                            }
                            break;
                        }

                    //***************补料盘已取完去放空Tray盘***************

                    case 400:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "400-->下料补料盘已取完去放空Tray盘");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                {
                                    Variable.OutFillTrayFlag = false;//清空料盘标志
                                    double pos = Variable.AxisPos[11, 3];
                                    Axis11SetMove(pos); //轴11下料补料轴移动到空Tray位
                                    if (Variable.AIMpos[11] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[11] >= Math.Round(pos - 0.1, 2))
                                    {
                                        Variable.AxisAlarmTime[11] = 0;
                                        Variable.XAlarmTime[2] = 0;
                                        Variable.OutAutoFillStartStep = 410;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[11] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[2] += 1;
                                    //Variable.DownAxisAlarm = true;
                                    //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }

                    case 410:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "410-->下料补料轨道夹紧气缸松开");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(58);//轨道夹紧气缸松开
                                if (!Variable.XStatus[85])
                                {
                                    Variable.OutAutoFillStartStep = 420;
                                }
                            }
                            break;
                        }

                    case 420:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "420-->下料补料空Tray工位1是否有盘");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[78])//下料补料空Tray工位1有盘
                                {
                                    Variable.OutAutoFillStartStep = 421;
                                }
                                else
                                {
                                    ListBoxTxt("下料补料空Tray未到工位1，请确认");
                                    Variable.OutAutoFillStartStep = 425;
                                    RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "425-->下料补料空Tray未到工位1，请确认");
                                }
                            }
                            break;
                        }
                    case 421:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "421-->下料补料工位1上顶气缸上升");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(56);//工位1上顶气缸上升
                                if (Variable.XStatus[80])
                                {
                                    Thread.Sleep(100);
                                    Variable.OutAutoFillStartStep = 430;
                                }
                            }
                            break;
                        }

                    case 430:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "430-->下料补料工位1上顶气缸下降");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(56);//工位1上顶气缸下降
                                if (Variable.XStatus[81])
                                {
                                    Variable.OutAutoFillStartStep = 440;
                                }
                            }
                            break;
                        }

                    case 440:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "440-->下料补料Y轴移动到待机位");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                {
                                    double pos = Variable.AxisPos[11, 0];
                                    Axis11SetMove(pos); //轴11移动到待机位
                                    if (Variable.AIMpos[11] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[11] >= Math.Round(pos - 0.1, 2))
                                    {
                                        Variable.DownReadyEmpty = false;//OK备品料盘已取空
                                        if (!Variable.DownReadyEmpty)
                                        {
                                            Variable.AxisAlarmTime[11] = 0;
                                            Variable.XAlarmTime[2] = 0;
                                            Variable.OutAutoFillStartStep = 450;
                                        }
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[11] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[2] += 1;
                                    //Variable.DownAxisAlarm = true;
                                    //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }
                    case 450:
                        {
                            RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "450-->下料补料工位1Tray盘是否已满");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[79])
                                {
                                    ListBoxTxt("下料补料工位1Tray盘已满，请取走");
                                    Variable.OutAutoFillStartStep = 455;
                                    RSDAlarmOutAutoFillStartStep(Variable.AutoStepMsg[5] = "455-->下料补料工位1Tray盘已满");
                                }
                                else
                                {
                                    Variable.OutAutoFillStartStep = 10;
                                }
                            }
                            break;
                        }
                }
                Thread.Sleep(1);
            }

        }
        #endregion

        #region 下料NG自动流程
        public void OutAutoNGStart()
        {
            while (true)
            {
                switch (Variable.OutAutoNGStartStep)
                {
                    //***************判断是否取空Tray盘***************
                    case 10:
                        {
                            RSDAlarmOutAutoNGStartStep(Variable.AutoStepMsg[6] = "10-->判断下料NGTray工位1是否有Tray盘");
                            if (Variable.RunEnable == true)
                            {
                                if (!Variable.XStatus[86])//判断工位1有无料
                                {
                                    Variable.OutAutoNGStartStep = 20;
                                }
                                else
                                {
                                    ListBoxTxt("下料NGTray工位1有Tray盘,请取走");
                                    Variable.OutAutoNGStartStep = 15;
                                    RSDAlarmOutAutoNGStartStep(Variable.AutoStepMsg[6] = "15-->下料NGTray工位1有Tray盘,请取走");
                                }
                            }
                            break;
                        }
                    case 20:
                        {
                            RSDAlarmOutAutoNGStartStep(Variable.AutoStepMsg[6] = "20-->下料NGTrayY移动到取空Tray位");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                {
                                    double pos = Variable.AxisPos[12, 1];
                                    Axis12SetMove(pos); //轴12NGTrayY移动到取空Tray位
                                    if (Variable.AIMpos[12] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[12] >= Math.Round(pos - 0.1, 2))
                                    {
                                        Variable.AxisAlarmTime[12] = 0;
                                        Variable.XAlarmTime[2] = 0;
                                        Variable.OutAutoNGStartStep = 25;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[12] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[2] += 1;
                                    //Variable.DownAxisAlarm = true;
                                    //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }
                    case 25:
                        {
                            RSDAlarmOutAutoNGStartStep(Variable.AutoStepMsg[6] = "25-->下料NGTray上顶Z轴上顶");
                            if (Variable.RunEnable == true)
                            {
                                Variable.DownNGTrayOK = false;//料盘准备OK
                                double pos = Variable.AxisPos[14, 1];
                                Axis14SetMove(pos); //轴14下料NG上顶Z轴上顶
                                if (Variable.AIMpos[14] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[14] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[14] = 0;
                                    Variable.OutAutoNGStartStep = 30;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[14] += 1;
                                }
                            }
                            break;
                        }

                    case 30:
                        {
                            RSDAlarmOutAutoNGStartStep(Variable.AutoStepMsg[6] = "30-->下料NGTray支撑气缸出");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(59);//支撑气缸出
                                if (Variable.XStatus[89])
                                {
                                    Thread.Sleep(300);
                                    Variable.OutAutoNGStartStep = 40;
                                }
                            }
                            break;
                        }
                    case 40:
                        {
                            RSDAlarmOutAutoNGStartStep(Variable.AutoStepMsg[6] = "40-->下料NGTray上顶Z轴下降");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[14, 2];
                                Axis14SetMove(pos); //轴14下料NG上顶Z轴下降
                                if (Variable.AIMpos[14] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[14] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[14] = 0;
                                    Variable.OutAutoNGStartStep = 50;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[14] += 1;
                                }
                            }
                            break;
                        }
                    case 50:
                        {
                            RSDAlarmOutAutoNGStartStep(Variable.AutoStepMsg[6] = "50-->下料NGTray支撑气缸回");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(59);//支撑气缸回
                                if (Variable.XStatus[88])
                                {
                                    Thread.Sleep(200);
                                    Variable.OutAutoNGStartStep = 55;
                                }
                            }
                            break;
                        }

                    case 55:
                        {
                            RSDAlarmOutAutoNGStartStep(Variable.AutoStepMsg[6] = "55-->下料NGTray上顶轴回待机位");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[14, 0];
                                Axis14SetMove(pos); //轴14下料NG上顶轴回待机位
                                if (Variable.AIMpos[14] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[14] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[14] = 0;
                                    Variable.OutAutoNGStartStep = 60;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[14] += 1;
                                }
                            }
                            break;
                        }

                    case 60:
                        {
                            RSDAlarmOutAutoNGStartStep(Variable.AutoStepMsg[6] = "60-->下料NG空Tray工位1有盘感应");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[86])//判断有无料感应
                                {
                                    Variable.OutAutoNGStartStep = 70;//有料
                                }
                                else
                                {
                                    //Variable.OutAutoNGStartStep = 10;//有料
                                    ListBoxTxt("下料NG空Tray工位1没有Tray盘");
                                    Variable.OutAutoNGStartStep = 65;
                                    RSDAlarmOutAutoNGStartStep(Variable.AutoStepMsg[6] = "65-->下料NG空Tray工位1没有Tray盘");
                                }
                            }
                            break;
                        }

                    case 70:
                        {
                            RSDAlarmOutAutoNGStartStep(Variable.AutoStepMsg[6] = "70-->下料NG空Tray上顶轴回待机位");
                            if (Variable.RunEnable == true)
                            {
                                double pos = Variable.AxisPos[14, 0];
                                Axis14SetMove(pos); //轴14下料NG上顶轴回待机位
                                if (Variable.AIMpos[14] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[14] >= Math.Round(pos - 0.1, 2))
                                {
                                    Variable.AxisAlarmTime[14] = 0;
                                    Variable.OutAutoNGStartStep = 90;
                                }
                                else
                                {
                                    Variable.AxisAlarmTime[14] += 1;
                                }
                            }
                            break;
                        }

                    case 90:
                        {
                            RSDAlarmOutAutoNGStartStep(Variable.AutoStepMsg[6] = "90-->下料NGTray轨道平台夹紧气缸夹紧");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(61);//轨道平台夹紧气缸夹紧
                                if (Variable.XStatus[95])
                                {
                                    Variable.OutAutoNGStartStep = 100;
                                }
                            }
                            break;
                        }

                    case 100:
                        {
                            RSDAlarmOutAutoNGStartStep(Variable.AutoStepMsg[6] = "100-->下料NGTrayY移动到开始位");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                {
                                    double pos = Variable.AxisPos[12, 2];
                                    Axis12SetMove(pos); //轴12NGTrayY移动到开始位
                                    if (Variable.AIMpos[12] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[12] >= Math.Round(pos - 0.1, 2))
                                    {
                                        Variable.AxisAlarmTime[12] = 0;
                                        Variable.XAlarmTime[2] = 0;
                                        Variable.OutAutoNGStartStep = 110;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[12] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[2] += 1;
                                    //Variable.DownAxisAlarm = true;
                                    //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }

                    //***************空Tray盘在工位2等待放NG品***************

                    case 110:
                        {
                            RSDAlarmOutAutoNGStartStep(Variable.AutoStepMsg[6] = "110-->下料NG空Tray盘在工位2等待放NG品");
                            if (Variable.RunEnable == true)
                            {
                                Variable.DownNGTrayOK = true;//料盘准备OK
                                TxtClear1(Application.StartupPath + @"\Data\DownNG\tray");//料盘赋空值
                                Variable.DownNGTrayFull = false;//清空料盘放满标志
                                Variable.OutAutoNGStartStep = 120;
                            }
                            break;
                        }
                    case 120:
                        {
                            RSDAlarmOutAutoNGStartStep(Variable.AutoStepMsg[6] = "120-->下料NG空Tray盘是否已放满");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.DownNGTrayFull == true)//料盘已放满
                                {
                                    Variable.DownNGTrayOK = false;//料盘准备OK
                                    Variable.OutAutoNGStartStep = 170;
                                }
                                else if (Variable.OutAutoOKStartStep == 1000 && (Variable.CleanOne || Variable.CleanOut))//结批中
                                {
                                    Variable.DownNGTrayOK = false;//料盘准备OK
                                    Variable.OutAutoNGStartStep = 170;
                                }

                            }
                            break;
                        }

                    //***************空Tray盘已放满料，去收满盘位***************

                    case 170://空盘放满料
                        {
                            RSDAlarmOutAutoNGStartStep(Variable.AutoStepMsg[6] = "170-->下料NG空TrayY移动到放满Tray位");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                {
                                    double pos = Variable.AxisPos[12, 3];
                                    Axis12SetMove(pos); //轴3空TrayY移动到放满Tray位
                                    if (Variable.AIMpos[12] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[12] >= Math.Round(pos - 0.1, 2))
                                    {
                                        Variable.AxisAlarmTime[12] = 0;
                                        Variable.XAlarmTime[2] = 0;
                                        Variable.OutAutoNGStartStep = 180;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[12] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[2] += 1;
                                    //Variable.DownAxisAlarm = true;
                                    //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }

                    case 180:
                        {
                            RSDAlarmOutAutoNGStartStep(Variable.AutoStepMsg[6] = "180-->下料NG空Tray轨道平台夹紧气缸松开");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(61);//轨道平台夹紧气缸松开
                                if (!Variable.XStatus[95])
                                {
                                    Variable.OutAutoNGStartStep = 190;
                                }
                            }
                            break;
                        }

                    case 190:
                        {
                            RSDAlarmOutAutoNGStartStep(Variable.AutoStepMsg[6] = "190-->下料NG空Tray工位3是否有Tray");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[90])//下料NGTray盘工位3有
                                {
                                    Variable.OutAutoNGStartStep = 191;
                                }
                                else
                                {
                                    ListBoxTxt("下料NGTray盘工位3Tray未感应到");
                                    Variable.OutAutoNGStartStep = 195;
                                }
                            }
                            break;
                        }
                    case 191:
                        {
                            RSDAlarmOutAutoNGStartStep(Variable.AutoStepMsg[6] = "191-->下料NG空Tray工位3上顶气缸上顶");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYON(60);//工位3上顶气缸上顶
                                if (Variable.XStatus[92])
                                {
                                    Thread.Sleep(200);
                                    Variable.OutAutoNGStartStep = 200;
                                }
                            }
                            break;
                        }

                    case 200:
                        {
                            RSDAlarmOutAutoNGStartStep(Variable.AutoStepMsg[6] = "200-->下料NG空Tray工位3上顶气缸下降");
                            if (Variable.RunEnable == true)
                            {
                                function.OutYOFF(60);//工位3上顶气缸下降
                                if (Variable.XStatus[93])
                                {
                                    Variable.OutAutoNGStartStep = 210;
                                }
                            }
                            break;
                        }

                    case 210:
                        {
                            RSDAlarmOutAutoNGStartStep(Variable.AutoStepMsg[6] = "210-->下料NG空TrayY移动到待机位");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2) && Variable.XStatus[98] && Variable.XStatus[100])
                                {
                                    double pos = Variable.AxisPos[12, 0];
                                    Axis12SetMove(pos); //轴12空TrayY移动到待机位
                                    if (Variable.AIMpos[12] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[12] >= Math.Round(pos - 0.1, 2))
                                    {
                                        Variable.AxisAlarmTime[12] = 0;
                                        Variable.XAlarmTime[2] = 0;
                                        Variable.OutAutoNGStartStep = 215;
                                    }
                                    else
                                    {
                                        Variable.AxisAlarmTime[12] += 1;
                                    }
                                }
                                else
                                {
                                    Variable.XAlarmTime[2] += 1;
                                    //Variable.DownAxisAlarm = true;
                                    //ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                                }
                            }
                            break;
                        }

                    case 215:
                        {
                            RSDAlarmOutAutoNGStartStep(Variable.AutoStepMsg[6] = "215-->下料NG空Tray判断是否结批");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.OutAutoOKStartStep == 1000 && (Variable.CleanOne || Variable.CleanOut))//结批中
                                {
                                    Variable.OutAutoNGStartStep = 500;
                                }
                                else
                                {
                                    Variable.OutAutoNGStartStep = 220;
                                }
                            }
                            break;
                        }

                    case 220:
                        {
                            RSDAlarmOutAutoNGStartStep(Variable.AutoStepMsg[6] = "220-->下料空Tray工位3Tray盘是否已满");
                            if (Variable.RunEnable == true)
                            {
                                if (Variable.XStatus[91])
                                {
                                    ListBoxTxt("下料空Tray工位3Tray盘已满，请取走");
                                    Variable.OutAutoNGStartStep = 225;
                                    RSDAlarmOutAutoNGStartStep(Variable.AutoStepMsg[6] = "225-->下料空Tray工位3Tray盘已满，请取走");
                                }
                                else
                                {
                                    Variable.OutAutoNGStartStep = 10;
                                }
                            }
                            break;
                        }
                }
                Thread.Sleep(1);
            }

        }
        #endregion

        #region Robot自动流程
        public void RobotAutoStart()
        {
            while (true)
            {
                #region 判断机械手放料还是取料

                switch (Variable.RobotAutoStartStep)
                {
                    case 10://模块取料流程判断
                        {
                            RSDAlarmRobotAutoStartStep(Variable.AutoStepMsg[7] = "10-->判断第几个模块没有放产品");
                            if (Variable.RunEnable == true)
                            {
                                Variable.RobotSet = ModelNoOK();//判断第几个模块没有放产品
                                //Variable.RobotSetFlag = false;
                                if (Variable.RobotSet > 0)
                                {
                                    if (Variable.RobotSetFlag)//传文件给上料机  
                                    {
                                        Variable.RobotAutoStartStep = 30;
                                    }
                                    else
                                    {
                                        Variable.RobotAutoStartStep = 20;
                                    }
                                }
                                else
                                {
                                    Variable.RobotAutoStartStep = 50;//去取料 
                                }
                            }
                            break;
                        }

                    case 20://传文件给上料机
                        {
                            RSDAlarmRobotAutoStartStep(Variable.AutoStepMsg[7] = "20-->传文件给上料机");
                            if (Variable.RunEnable == true)
                            {
                                SetFileToUP(Convert.ToString(Variable.RobotSet - 20));
                                takeModNum = Variable.RobotSet - 21;
                                Variable.RobotSetFlag = true;
                                Variable.RobotAutoStartStep = 30;
                            }
                            break;
                        }

                    case 30://判断上料机有没有准备好产品
                        {
                            RSDAlarmRobotAutoStartStep(Variable.AutoStepMsg[7] = "30-->判断上料机有没有准备好产品");
                            if (Variable.RunEnable == true)
                            {
                                if (!Variable.RobotSeting && !Variable.RobotGeting)//不在取放料中
                                {
                                    if (Variable.UpReadyTrayOK && Variable.RobotSetFlag)//上料机准备OK
                                    {
                                        Variable.RobotSetStep = Variable.RobotSet;
                                        Variable.ModelSetStep = 10;
                                    }
                                    else
                                    {
                                        Variable.RobotAutoStartStep = 50;//去取料 
                                    }
                                }
                                else
                                {
                                    Variable.RobotAutoStartStep = 50;//去取料 
                                }
                            }
                            break;
                        }

                    case 50://判断模块料有没有老化OK，是否去下料机放料
                        {
                            RSDAlarmRobotAutoStartStep(Variable.AutoStepMsg[7] = "50-->判断模块料有没有老化OK，是否去下料机放料");
                            if (Variable.RunEnable == true)
                            {
                                Variable.RobotGet = ModelIsOK();
                                //Variable.RobotGetFlag = false;
                                if (Variable.RobotGet > 0)
                                {
                                    Variable.RobotAutoStartStep = 60;
                                }
                                else
                                {
                                    Variable.RobotAutoStartStep = 10;//产品老化没完成，去放料
                                }
                            }
                            break;
                        }

                    case 60://判断下料机有没有准备好产品
                        {
                            RSDAlarmRobotAutoStartStep(Variable.AutoStepMsg[7] = "60-->判断下料机有没有准备好产品");
                            if (Variable.RunEnable == true)
                            {
                                if (!Variable.RobotSeting && !Variable.RobotGeting)//不在取放料中
                                {
                                    Variable.RobotSet = ModelNoOK();//判断第几个模块没有放产品
                                    Variable.RobotGet = ModelIsOK();

                                    if (Variable.INAutoReady1Step == 500 && Variable.INAutoReadyStep == 800 && (Variable.CleanOne || Variable.CleanOut))
                                    {
                                        Variable.RobotAutoStartStep = 70;
                                    }
                                    else
                                    {
                                        if (Variable.RobotSet > 0 && Variable.RobotGetStep == 0)
                                        {
                                            Variable.RobotAutoStartStep = 10;//产品老化没完成，去放料
                                        }
                                        else if (Variable.RobotGet > 0 && Variable.RobotSetStep == 0)
                                        {
                                            Variable.RobotGetStep = Variable.RobotGet;
                                            Variable.ModelGetStep = 10;
                                        }
                                    }
                                }
                                else
                                {
                                    Variable.RobotAutoStartStep = 10;//产品老化没完成，去放料
                                }
                            }
                            break;
                        }
                    case 70://结批
                        {
                            RSDAlarmRobotAutoStartStep(Variable.AutoStepMsg[7] = "70-->判断是否结批");
                            if (Variable.RunEnable == true)
                            {
                                if (!Variable.RobotSeting && !Variable.RobotGeting)//不在取放料中
                                {

                                    Variable.RobotSet = ModelNoOK();//判断第几个模块没有放产品

                                    Variable.RobotGet = ModelIsOK();

                                    if (Variable.RobotGet > 0 && Variable.RobotSetStep == 0)
                                    {
                                        Variable.RobotGetStep = Variable.RobotGet;
                                        Variable.ModelGetStep = 10;
                                    }
                                    else if (Variable.RobotSet > 0 && Variable.RobotGetStep == 0)
                                    {
                                        Variable.RobotAutoStartStep = 10;//产品老化没完成，去放料
                                    }
                                }
                                else
                                {
                                    Variable.RobotAutoStartStep = 10;//产品老化没完成，去放料
                                }
                            }
                            break;
                        }
                }

                #endregion

                #region 上层模块内放料

                if (Variable.RobotSetStep > 20 && ((Variable.RobotSetStep - 21) % 4) == 0)//case 21:模块1动作
                {
                    int model = (Variable.RobotSetStep - 21) / 4;//模块号
                    if (Variable.ModelState[model * 4] == 0 && (Variable.ModelState[model * 4 + 1] == 0 || Variable.ModelState[model * 4 + 1] == 10))//外部没有放板子或外部屏蔽
                    {
                        switch (Variable.ModelSetStep)
                        {
                            case 10:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "10-->" + model.ToString() + "上层模块内放料_上料待测Tray盘已准备OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.UpReadyTrayOK)
                                        {
                                            Variable.messageRecord[0] = "";
                                            Variable.RobotSeting = true;

                                            function.OutYOFF(105 + model * 32); //上层老化侧顶气缸回
                                            if (!Variable.XStatus[124 + model * 32] && !Variable.XStatus[125 + model * 32])
                                            {
                                                Variable.ModelSetStep = 15;
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 15:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "15-->" + model.ToString() + "上层模块内放料_内外检测Tray和侧顶气缸感应器是否满足条件");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (!Variable.XStatus[124 + model * 32] && !Variable.XStatus[125 + model * 32] && !Variable.XStatus[117 + model * 32] && !Variable.XStatus[121 + model * 32])//上层内Tray到位信号
                                        {
                                            Variable.ModelSetStep = 20;
                                        }
                                        else if (Variable.XStatus[124 + model * 32] && Variable.XStatus[125 + model * 32])
                                        {
                                            ListBoxTxt((model + 1).ToString() + "#上层老化机侧顶气缸伸出，请检查！");
                                            alarmNum = model;
                                            Variable.ModelSetStep = 16;
                                            RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "16-->" + model.ToString() + "上层模块内放料_上层老化机侧顶气缸伸出，请检查");
                                        }
                                        else if (Variable.XStatus[117 + model * 32])
                                        {
                                            ListBoxTxt((model + 1).ToString() + "#上层老化机内Tray盘未取走，请检查上层内Tray到位信号");
                                            alarmNum = model;
                                            Variable.ModelSetStep = 17;
                                            RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "17-->" + model.ToString() + "上层模块内放料_上层老化机内Tray盘未取走，请检查");
                                        }
                                        else if (Variable.XStatus[121 + model * 32])
                                        {
                                            ListBoxTxt((model + 1).ToString() + "#上层老化机外Tray盘未取走，请检查上层外Tray到位信号");
                                            alarmNum = model;
                                            Variable.ModelSetStep = 18;
                                            RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "18-->" + model.ToString() + "上层模块内放料_上层老化机外Tray盘未取走，请检查");
                                        }
                                    }
                                    break;
                                }
                            case 20:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "20-->" + model.ToString() + "上层模块内放料_轴16机械手X轴移动到取料位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)
                                        {
                                            double pos = Variable.AxisPos[16, 2];
                                            Axis16SetMove(pos); //轴16机械手X轴移动到取料位
                                            if (Variable.AIMpos[16] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[16] >= Math.Round(pos - 0.1, 2))
                                            {
                                                Variable.RobotUpGetTray = false;
                                                Variable.AxisAlarmTime[16] = 0;
                                                Variable.ModelSetStep = 30;
                                            }
                                            else
                                            {
                                                Variable.AxisAlarmTime[16] += 1;
                                            }
                                        }
                                    }
                                    break;
                                }

                            case 30:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "30-->" + model.ToString() + "上层模块内放料_机械手取料中，代码为:001");
                                    if (Variable.UpReadyTrayOK)//告知机械手取料
                                    {
                                        Variable.RobotRecOK = false;
                                        Variable.RobotSendStr = "001";
                                        Variable.messageRecord[0] = "机械手取料中，代码为:001";
                                        Variable.RobotTCPAutoStep = 1000;
                                        Variable.ModelSetStep = 40;
                                    }
                                    break;
                                }

                            case 40:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "40-->" + model.ToString() + "上层模块内放料_机械手取料OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)//机械手取料OK
                                        {
                                            Variable.messageRecord[0] = "";
                                            Variable.RobotUpGetTray = true;
                                            Variable.ModelSetStep = 50;
                                        }
                                    }
                                    break;
                                }
                            case 50:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "50-->" + model.ToString() + "上层模块内放料_轴16机械手X轴移动到拍照位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)
                                        {
                                            double pos = Variable.AxisPos[16, 1];
                                            Axis16SetMove(pos); //轴16机械手X轴移动到拍照位
                                            if (Variable.AIMpos[16] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[16] >= Math.Round(pos - 0.1, 2))
                                            {
                                                Variable.AxisAlarmTime[16] = 0;
                                                Variable.ModelSetStep = 60;
                                            }
                                            else
                                            {
                                                Variable.AxisAlarmTime[16] += 1;
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 60:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "60-->" + model.ToString() + "上层模块内放料_机械手拍照中，代码为:003");
                                    Variable.RobotRecOK = false;
                                    Variable.RobotSendStr = "003";//告知机械手到上料拍照位置1
                                    Variable.messageRecord[0] = "机械手拍照中，代码为:003";
                                    Variable.RobotTCPAutoStep = 1000;
                                    Variable.ModelSetStep = 70;
                                    break;
                                }

                            case 70:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "70-->" + model.ToString() + "上层模块内放料_机械手拍照位置1OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)//机械手拍照位置1OK
                                        {
                                            Variable.messageRecord[0] = "";
                                            Thread.Sleep(500);
                                            Variable.ModelSetStep = 80;
                                        }
                                    }
                                    break;
                                }
                            case 80:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "80-->" + model.ToString() + "上层模块内放料_判断是否触发相机拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoCheck)
                                        {
                                            Variable.PhotoTCPAutoStep = 101;//触发相机拍照
                                            Variable.ModelSetStep = 90;
                                        }
                                        else
                                        {
                                            PhotoDataOK(Variable.PhotoData1);//放弃拍照
                                            Variable.ModelSetStep = 100;
                                        }
                                    }
                                    break;
                                }
                            case 90:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "90-->" + model.ToString() + "上层模块内放料_判断相机拍照结果是否OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoTCPAutoStep == 0)
                                        {
                                            //while (Variable.PhotoRecMessage.Length < 2)
                                            //{
                                            //    Application.DoEvents();
                                            //    Thread.Sleep(1);
                                            //}
                                            Thread.Sleep((int)Variable.photoDelay);
                                            int rec = UPStringManipulation1(Variable.PhotoRecMessage);

                                            if (rec == 1 && Variable.TestResult)//拍照结果OK
                                            {
                                                Variable.ModelSetStep = 100;
                                            }
                                            else
                                            {
                                                ListBoxTxt("拍照NG，请确认Tray盘");
                                                Variable.ModelSetStep = 95;
                                                RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "95-->" + model.ToString() + "上层模块内放料_拍照NG，请确认Tray盘");
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 96://放弃拍照
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "96-->" + model.ToString() + "上层模块内放料_拍照NG，放弃拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        PhotoDataOK(Variable.PhotoData1);
                                        Variable.ModelSetStep = 100;
                                    }
                                    break;
                                }
                            case 100:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "100-->" + model.ToString() + "上层模块内放料_机械手拍照中，代码为:004");
                                    Variable.RobotRecOK = false;
                                    Variable.RobotSendStr = "004";//告知机械手到上料拍照位置2
                                    Variable.messageRecord[0] = "机械手拍照中，代码为:004";
                                    Variable.RobotTCPAutoStep = 1000;
                                    Variable.ModelSetStep = 110;
                                    break;
                                }

                            case 110:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "110-->" + model.ToString() + "上层模块内放料_机械手拍照位置2 OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)//机械手拍照位置2 OK
                                        {
                                            Variable.messageRecord[0] = "";
                                            Thread.Sleep(1000);
                                            Variable.ModelSetStep = 120;
                                        }
                                    }
                                    break;
                                }
                            case 120:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "120-->" + model.ToString() + "上层模块内放料_判断是否触发相机拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoCheck)
                                        {
                                            Variable.PhotoTCPAutoStep = 102;//触发相机拍照
                                            Variable.ModelSetStep = 130;
                                        }
                                        else
                                        {
                                            PhotoDataOK(Variable.PhotoData2);//放弃拍照
                                            Variable.ModelSetStep = 140;
                                        }
                                    }
                                    break;
                                }
                            case 130:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "130-->" + model.ToString() + "上层模块内放料_判断相机拍照结果是否OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoTCPAutoStep == 0)
                                        {
                                            //while (Variable.PhotoRecMessage.Length < 2)
                                            //{
                                            //    Application.DoEvents();
                                            //    Thread.Sleep(1);
                                            //}
                                            Thread.Sleep((int)Variable.photoDelay);
                                            int rec = UPStringManipulation2(Variable.PhotoRecMessage);

                                            if (rec == 1 && Variable.TestResult)//拍照结果OK
                                            {
                                                Variable.ModelSetStep = 140;
                                            }
                                            else
                                            {
                                                ListBoxTxt("拍照NG，请确认Tray盘");
                                                Variable.ModelSetStep = 135;
                                                RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "135-->" + model.ToString() + "上层模块内放料_拍照NG，请确认Tray盘");
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 136://放弃拍照
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "136-->" + model.ToString() + "上层模块内放料_拍照NG，放弃拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        PhotoDataOK(Variable.PhotoData2);
                                        Variable.ModelSetStep = 140;
                                    }
                                    break;
                                }
                            case 140:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "140-->" + model.ToString() + "上层模块内放料_保存相机数据到TXT");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手在安全位
                                        {
                                            Variable.PhotoData = Variable.PhotoData1.Concat(Variable.PhotoData2).ToArray();
                                            //保存相机数据到TXT
                                            myTXT.WriteTxt(Variable.PhotoData, Application.StartupPath + @"\Data\Photo\Up\" + (model * 4 + 1).ToString() + "\\tray");
                                            //保存相机数据到TXT供测试模组计算良率
                                            myTXT.WriteTxt(Variable.PhotoData, @"D:\Map\Photo\" + (model * 4 + 1).ToString() + "\\tray");
                                            //计算投入数量
                                            InNum((model * 4 + 1).ToString());

                                            Variable.ModelSetStep = 141;
                                        }
                                    }
                                    break;
                                }
                            case 141:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "141-->" + model.ToString() + "上层模块内放料_轴16机械手轴移动到Model放料");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手在安全位
                                        {
                                            double pos = Variable.AxisPos[16, model + 4];
                                            Axis16SetMove(pos); //轴16机械手轴移动到Model放料
                                            if (Variable.AIMpos[16] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[16] >= Math.Round(pos - 0.1, 2))
                                            {
                                                Variable.ModelSetStep = 146;
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 146:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "146-->" + model.ToString() + "上层模块内放料_判断内外上顶气缸是否在下位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.XStatus[118 + model * 32] && Variable.XStatus[122 + model * 32])
                                        {
                                            Variable.ModelSetStep = 150;
                                        }
                                        else if (!Variable.XStatus[118 + model * 32])
                                        {
                                            ListBoxTxt((model + 1).ToString() + "#上老化机内上顶气缸未在下位");
                                            alarmNum = model;
                                            Variable.ModelSetStep = 143;
                                            RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "143-->" + model.ToString() + "上层模块内放料_上老化机内上顶气缸未在下位");
                                        }
                                        else if (!Variable.XStatus[122 + model * 32])
                                        {
                                            ListBoxTxt((model + 1).ToString() + "#上老化机外上顶气缸未在下位");
                                            alarmNum = model;
                                            Variable.ModelSetStep = 144;
                                            RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "144-->" + model.ToString() + "上层模块内放料_上老化机外上顶气缸未在下位");
                                        }
                                    }
                                    break;
                                }
                            case 150:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "150-->" + model.ToString() + "上层模块内放料_告知机械手到Model放料");
                                    Variable.RobotRecOK = false;
                                    Variable.RobotSendStr = (101 + model * 4).ToString();//告知机械手到Model放料
                                    Variable.messageRecord[0] = "机械手" + (model + 1).ToString() + "号模组放料中，代码为:" + (101 + model * 4).ToString();
                                    Variable.RobotTCPAutoStep = 1000;
                                    Variable.ModelSetStep = 160;
                                    break;
                                }

                            case 160:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "160-->" + model.ToString() + "上层模块内放料_判断上老化机内Tray盘是否到位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)
                                        {
                                            Variable.messageRecord[0] = "";
                                            if (Variable.XStatus[117 + model * 32])
                                            {
                                                Variable.ModelSetStep = 170;
                                            }
                                            else
                                            {
                                                ListBoxTxt((model + 1).ToString() + "#上老化机内Tray盘未到位");
                                                alarmNum = model;
                                                Variable.ModelSetStep = 165;
                                                RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "165-->" + model.ToString() + "上层模块内放料_上老化机内Tray盘未到位");
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 170:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "170-->" + model.ToString() + "上层模块内放料_加热标志复位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)
                                        {
                                            Variable.TempOKFlag[model * 4] = false;
                                            Variable.ModelSetStep = 180;
                                        }
                                    }
                                    break;
                                }
                            case 180:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "180-->" + model.ToString() + "上层模块内放料_延时加热，气缸上升，发送测试指令给单机");
                                    if (Variable.RunEnable == true)
                                    {
                                        Variable.TempOKFlag[model * 4] = true;//延时加热，气缸上升，发送测试指令给单机
                                        Variable.ModelSetStep = 190;
                                    }
                                    break;
                                }
                            case 190:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "190-->" + model.ToString() + "上层模块内放料_读取UpReady数据到ModelUP");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手在安全位
                                        {
                                            UpReadyTOModelUP((model * 4 + 1).ToString());//读取UpReady数据到ModelUP
                                            Variable.ModelState[model * 4] = 1;
                                            Variable.RobotSetFlag = false;
                                            Variable.RobotSeting = false;
                                            Variable.RobotSetStep = 0;
                                            Variable.ModelSetStep = 0;
                                        }
                                    }
                                    break;
                                }
                        }
                    }
                }

                #endregion

                #region 上层模块外放料

                if (Variable.RobotSetStep > 20 && ((Variable.RobotSetStep - 21) % 4) == 1)//case 21:模块1动作
                {
                    int model = (Variable.RobotSetStep - 22) / 4;//模块号
                    if (Variable.ModelState[model * 4 + 1] == 0 && (Variable.ModelState[model * 4] != 0 || Variable.ModelState[model * 4] == 10))//内部已经放板子或内部屏蔽
                    {
                        switch (Variable.ModelSetStep)
                        {
                            case 10:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "10-->" + model.ToString() + "上层模块外放料_上料待测Tray盘已准备OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.UpReadyTrayOK)
                                        {
                                            Variable.messageRecord[0] = "";
                                            Variable.RobotSeting = true;
                                            Variable.ModelSetStep = 15;
                                        }
                                    }
                                    break;
                                }
                            case 15:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "15-->" + model.ToString() + "上层模块外放料_外检测Tray感应器是否满足条件");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (!Variable.XStatus[121 + model * 32])//上层外Tray到位信号
                                        {
                                            Variable.ModelSetStep = 20;
                                        }
                                        else
                                        {
                                            ListBoxTxt((model + 1).ToString() + "#上层老化机外Tray盘未取走，请检查上层外Tray到位信号");
                                            alarmNum = model;
                                            Variable.ModelSetStep = 18;
                                            RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "18-->" + model.ToString() + "上层模块外放料_上层老化机外Tray盘未取走，请检查");
                                        }
                                    }
                                    break;
                                }
                            case 20:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "20-->" + model.ToString() + "上层模块外放料_轴16机械手X轴移动到取料位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手X轴移动到放料位
                                        {
                                            double pos = Variable.AxisPos[16, 2];
                                            Axis16SetMove(pos); //轴16机械手X轴移动到取料位
                                            if (Variable.AIMpos[16] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[16] >= Math.Round(pos - 0.1, 2))
                                            {
                                                Variable.RobotUpGetTray = false;
                                                Variable.AxisAlarmTime[16] = 0;
                                                Variable.ModelSetStep = 30;
                                            }
                                            else
                                            {
                                                Variable.AxisAlarmTime[16] += 1;
                                            }
                                        }
                                    }
                                    break;
                                }

                            case 30:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "30-->" + model.ToString() + "上层模块外放料_机械手取料中，代码为:001");
                                    if (Variable.UpReadyTrayOK)//告知机械手取料
                                    {
                                        Variable.RobotRecOK = false;
                                        Variable.RobotSendStr = "001";
                                        Variable.messageRecord[0] = "机械手取料中，代码为:001";
                                        Variable.RobotTCPAutoStep = 1000;
                                        Variable.ModelSetStep = 40;
                                    }
                                    break;
                                }

                            case 40:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "40-->" + model.ToString() + "上层模块外放料_机械手取料OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)//机械手取料OK
                                        {
                                            Variable.messageRecord[0] = "";
                                            Variable.RobotUpGetTray = true;
                                            Variable.ModelSetStep = 50;
                                        }
                                    }
                                    break;
                                }
                            case 50:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "50-->" + model.ToString() + "上层模块外放料_轴16机械手X轴移动到拍照位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)
                                        {
                                            double pos = Variable.AxisPos[16, 1];
                                            Axis16SetMove(pos); //轴16机械手X轴移动到拍照位
                                            if (Variable.AIMpos[16] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[16] >= Math.Round(pos - 0.1, 2))
                                            {
                                                Variable.AxisAlarmTime[16] = 0;
                                                Variable.ModelSetStep = 60;
                                            }
                                            else
                                            {
                                                Variable.AxisAlarmTime[16] += 1;
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 60:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "60-->" + model.ToString() + "上层模块外放料_机械手拍照中，代码为:003");
                                    Variable.RobotRecOK = false;
                                    Variable.RobotSendStr = "003";//告知机械手到上料拍照位置1
                                    Variable.messageRecord[0] = "机械手拍照中，代码为:003";
                                    Variable.RobotTCPAutoStep = 1000;
                                    Variable.ModelSetStep = 70;
                                    break;
                                }

                            case 70:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "70-->" + model.ToString() + "上层模块外放料_机械手拍照位置1OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)//机械手拍照位置1OK
                                        {
                                            Variable.messageRecord[0] = "";
                                            Thread.Sleep(500);
                                            Variable.ModelSetStep = 80;
                                        }
                                    }
                                    break;
                                }
                            case 80:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "80-->" + model.ToString() + "上层模块外放料_判断是否触发相机拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoCheck)
                                        {
                                            Variable.PhotoTCPAutoStep = 101;//触发相机拍照
                                            Variable.ModelSetStep = 90;
                                        }
                                        else
                                        {
                                            PhotoDataOK(Variable.PhotoData1);//放弃拍照
                                            Variable.ModelSetStep = 100;
                                        }
                                    }
                                    break;
                                }
                            case 90:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "90-->" + model.ToString() + "上层模块外放料_判断相机拍照结果是否OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoTCPAutoStep == 0)
                                        {
                                            //while (Variable.PhotoRecMessage.Length < 2)
                                            //{
                                            //    Application.DoEvents();
                                            //    Thread.Sleep(1);
                                            //}
                                            Thread.Sleep((int)Variable.photoDelay);
                                            int rec = UPStringManipulation1(Variable.PhotoRecMessage);

                                            if (rec == 1 && Variable.TestResult)//拍照结果OK
                                            {
                                                Variable.ModelSetStep = 100;
                                            }
                                            else
                                            {
                                                ListBoxTxt("拍照NG，请确认Tray盘");
                                                Variable.ModelSetStep = 95;
                                                RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "95-->" + model.ToString() + "上层模块外放料_拍照NG，请确认Tray盘");
                                            }
                                        }
                                    }
                                    break;
                                }

                            case 96://放弃拍照
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "96-->" + model.ToString() + "上层模块外放料_拍照NG，放弃拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        PhotoDataOK(Variable.PhotoData1);
                                        Variable.ModelSetStep = 100;
                                    }
                                    break;
                                }

                            case 100:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "100-->" + model.ToString() + "上层模块外放料_机械手拍照中，代码为:004");
                                    Variable.RobotRecOK = false;
                                    Variable.RobotSendStr = "004";//告知机械手到上料拍照位置2
                                    Variable.messageRecord[0] = "机械手拍照中，代码为:004";
                                    Variable.RobotTCPAutoStep = 1000;
                                    Variable.ModelSetStep = 110;
                                    break;
                                }

                            case 110:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "110-->" + model.ToString() + "上层模块外放料_机械手拍照位置2 OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)//机械手拍照位置2 OK
                                        {
                                            Variable.messageRecord[0] = "";
                                            Thread.Sleep(1000);
                                            Variable.ModelSetStep = 120;
                                        }
                                    }
                                    break;
                                }
                            case 120:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "120-->" + model.ToString() + "上层模块外放料_判断是否触发相机拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoCheck)
                                        {
                                            Variable.PhotoTCPAutoStep = 102;//触发相机拍照
                                            Variable.ModelSetStep = 130;
                                        }
                                        else
                                        {
                                            PhotoDataOK(Variable.PhotoData2);//放弃拍照
                                            Variable.ModelSetStep = 140;
                                        }
                                    }
                                    break;
                                }
                            case 130:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "130-->" + model.ToString() + "上层模块外放料_判断相机拍照结果是否OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoTCPAutoStep == 0)
                                        {
                                            //while (Variable.PhotoRecMessage.Length < 2)
                                            //{
                                            //    Application.DoEvents();
                                            //    Thread.Sleep(1);
                                            //}
                                            Thread.Sleep((int)Variable.photoDelay);
                                            int rec = UPStringManipulation2(Variable.PhotoRecMessage);

                                            if (rec == 1 && Variable.TestResult)//拍照结果OK
                                            {
                                                Variable.ModelSetStep = 140;
                                            }
                                            else
                                            {
                                                ListBoxTxt("拍照NG，请确认Tray盘");
                                                Variable.ModelSetStep = 135;
                                                RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "135-->" + model.ToString() + "上层模块外放料_拍照NG，请确认Tray盘");
                                            }
                                        }
                                    }
                                    break;
                                }

                            case 136://放弃拍照
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "136-->" + model.ToString() + "上层模块外放料_拍照NG，放弃拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        PhotoDataOK(Variable.PhotoData2);
                                        Variable.ModelSetStep = 140;
                                    }
                                    break;
                                }
                            case 140:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "140-->" + model.ToString() + "上层模块外放料_保存相机数据到TXT");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手在安全位
                                        {
                                            Variable.PhotoData = Variable.PhotoData1.Concat(Variable.PhotoData2).ToArray();
                                            //保存相机数据到TXT
                                            myTXT.WriteTxt(Variable.PhotoData, Application.StartupPath + @"\Data\Photo\Up\" + (model * 4 + 2).ToString() + "\\tray");
                                            //保存相机数据到TXT供测试模组计算良率
                                            myTXT.WriteTxt(Variable.PhotoData, @"D:\Map\Photo\" + (model * 4 + 2).ToString() + "\\tray");
                                            //计算投入数量
                                            InNum((model * 4 + 2).ToString());

                                            Variable.ModelSetStep = 141;
                                        }
                                    }
                                    break;
                                }
                            case 141:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "141-->" + model.ToString() + "上层模块外放料_轴16机械手轴移动到Model放料");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手在安全位
                                        {
                                            double pos = Variable.AxisPos[16, model + 4];
                                            Axis16SetMove(pos); //轴16机械手轴移动到Model放料
                                            if (Variable.AIMpos[16] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[16] >= Math.Round(pos - 0.1, 2))
                                            {
                                                Variable.ModelSetStep = 145;
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 145:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "145-->" + model.ToString() + "上层模块外放料_侧定位气缸出");
                                    if (Variable.RunEnable == true)
                                    {
                                        function.OutYON(105 + model * 32);//侧定位气缸出
                                        if (Variable.XStatus[124 + model * 32] && Variable.XStatus[125 + model * 32])
                                        {
                                            Variable.ModelSetStep = 146;
                                        }
                                    }
                                    break;
                                }
                            case 146:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "146-->" + model.ToString() + "上层模块外放料_判断外上顶气缸是否在下位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.XStatus[122 + model * 32])
                                        {
                                            Variable.ModelSetStep = 150;
                                        }
                                        else if (!Variable.XStatus[122 + model * 32])
                                        {
                                            ListBoxTxt((model + 1).ToString() + "#上老化机外上顶气缸未在下位");
                                            alarmNum = model;
                                            Variable.ModelSetStep = 144;
                                            RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "144-->" + model.ToString() + "上层模块外放料_上老化机外上顶气缸未在下位");
                                        }
                                    }
                                    break;
                                }
                            case 150:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "150-->" + model.ToString() + "上层模块外放料_告知机械手到Model放料");
                                    Variable.RobotRecOK = false;
                                    Variable.RobotSendStr = (102 + model * 4).ToString();//告知机械手到Model放料
                                    Variable.messageRecord[0] = "机械手" + (model + 1).ToString() + "号模组放料中，代码为:" + (102 + model * 4).ToString();
                                    Variable.RobotTCPAutoStep = 1000;
                                    Variable.ModelSetStep = 160;
                                    break;
                                }

                            case 160:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "160-->" + model.ToString() + "上层模块外放料_判断上老化机外Tray盘是否到位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)
                                        {
                                            Variable.messageRecord[0] = "";
                                            if (Variable.XStatus[121 + model * 32])
                                            {
                                                Variable.ModelSetStep = 170;
                                            }
                                            else
                                            {
                                                ListBoxTxt((model + 1).ToString() + "#上老化机外Tray盘未到位");
                                                alarmNum = model;
                                                Variable.ModelSetStep = 166;
                                                RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "166-->" + model.ToString() + "上层模块外放料_上老化机外Tray盘未到位");
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 170:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "170-->" + model.ToString() + "上层模块外放料_加热标志复位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)
                                        {
                                            Variable.TempOKFlag[model * 4 + 1] = false;
                                            Variable.ModelSetStep = 180;
                                        }
                                    }
                                    break;
                                }
                            case 180:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "180-->" + model.ToString() + "上层模块外放料_延时加热，气缸上升，发送测试指令给单机");
                                    if (Variable.RunEnable == true)
                                    {
                                        Variable.TempOKFlag[model * 4 + 1] = true;//延时加热，气缸上升，发送测试指令给单机
                                        Variable.ModelSetStep = 190;
                                    }
                                    break;
                                }
                            case 190:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "190-->" + model.ToString() + "上层模块外放料_读取UpReady数据到ModelUP");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手在安全位
                                        {
                                            UpReadyTOModelUP((model * 4 + 2).ToString());//读取UpReady数据到ModelUP
                                            Variable.ModelState[model * 4 + 1] = 1;
                                            Variable.RobotSetFlag = false;
                                            Variable.RobotSeting = false;
                                            Variable.RobotSetStep = 0;
                                            Variable.ModelSetStep = 0;
                                        }
                                    }
                                    break;
                                }
                        }
                    }
                }

                #endregion

                #region 下层模块内放料

                if (Variable.RobotSetStep > 20 && ((Variable.RobotSetStep - 21) % 4) == 2)//case 21:模块1动作
                {
                    int model = (Variable.RobotSetStep - 23) / 4;//模块号
                    if (Variable.ModelState[model * 4 + 2] == 0 && (Variable.ModelState[model * 4 + 3] == 0 || Variable.ModelState[model * 4 + 3] == 10))//外部没有放板子或外部屏蔽
                    {
                        switch (Variable.ModelSetStep)
                        {
                            case 10:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "10-->" + model.ToString() + "下层模块内放料_上料待测Tray盘已准备OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.UpReadyTrayOK)
                                        {
                                            Variable.messageRecord[0] = "";
                                            Variable.RobotSeting = true;
                                            function.OutYOFF(121 + model * 32); ;//下层老化侧顶气缸回
                                            if (!Variable.XStatus[138 + model * 32] && !Variable.XStatus[139 + model * 32])
                                            {
                                                Variable.ModelSetStep = 15;
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 15:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "15-->" + model.ToString() + "下层模块内放料_内外检测Tray和侧顶气缸感应器是否满足条件");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (!Variable.XStatus[138 + model * 32] && !Variable.XStatus[139 + model * 32] && !Variable.XStatus[131 + model * 32] && !Variable.XStatus[135 + model * 32])//上层内Tray到位信号
                                        {
                                            Variable.ModelSetStep = 20;
                                        }
                                        else if (Variable.XStatus[138 + model * 32] && Variable.XStatus[139 + model * 32])
                                        {
                                            ListBoxTxt((model + 1).ToString() + "#下层老化机侧顶气缸伸出，请检查！");
                                            alarmNum = model;
                                            Variable.ModelSetStep = 12;
                                            RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "12-->" + model.ToString() + "下层模块内放料_下层老化机侧顶气缸伸出，请检查");
                                        }
                                        else if (Variable.XStatus[131 + model * 32])
                                        {
                                            ListBoxTxt((model + 1).ToString() + "#下层老化机内Tray盘未取走，请检查下层内Tray到位信号");
                                            alarmNum = model;
                                            Variable.ModelSetStep = 13;
                                            RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "13-->" + model.ToString() + "下层模块内放料_下层老化机内Tray盘未取走，请检查");
                                        }
                                        else if (Variable.XStatus[135 + model * 32])
                                        {
                                            ListBoxTxt((model + 1).ToString() + "#下层老化机外Tray盘未取走，请检查下层外Tray到位信号");
                                            alarmNum = model;
                                            Variable.ModelSetStep = 14;
                                            RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "14-->" + model.ToString() + "下层模块内放料_下层老化机外Tray盘未取走，请检查");
                                        }
                                    }
                                    break;
                                }
                            case 20:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "20-->" + model.ToString() + "下层模块内放料_轴16机械手X轴移动到取料位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手X轴移动到放料位
                                        {
                                            double pos = Variable.AxisPos[16, 2];
                                            Axis16SetMove(pos); //轴16机械手X轴移动到取料位
                                            if (Variable.AIMpos[16] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[16] >= Math.Round(pos - 0.1, 2))
                                            {
                                                Variable.RobotUpGetTray = false;
                                                Variable.AxisAlarmTime[16] = 0;
                                                Variable.ModelSetStep = 30;
                                            }
                                            else
                                            {
                                                Variable.AxisAlarmTime[16] += 1;
                                            }
                                        }
                                    }
                                    break;
                                }

                            case 30:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "30-->" + model.ToString() + "下层模块内放料_机械手取料中，代码为:001");
                                    if (Variable.UpReadyTrayOK)//告知机械手取料
                                    {
                                        Variable.RobotRecOK = false;
                                        Variable.RobotSendStr = "001";
                                        Variable.messageRecord[0] = "机械手取料中，代码为:001";
                                        Variable.RobotTCPAutoStep = 1000;
                                        Variable.ModelSetStep = 40;
                                    }

                                    break;
                                }

                            case 40:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "40-->" + model.ToString() + "下层模块内放料_机械手取料OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)//机械手取料OK
                                        {
                                            Variable.messageRecord[0] = "";
                                            Variable.RobotUpGetTray = true;
                                            Variable.ModelSetStep = 50;
                                        }
                                    }
                                    break;
                                }
                            case 50:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "50-->" + model.ToString() + "下层模块内放料_轴16机械手X轴移动到拍照位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)
                                        {
                                            double pos = Variable.AxisPos[16, 1];
                                            Axis16SetMove(pos); //轴16机械手X轴移动到拍照位
                                            if (Variable.AIMpos[16] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[16] >= Math.Round(pos - 0.1, 2))
                                            {
                                                Variable.AxisAlarmTime[16] = 0;
                                                Variable.ModelSetStep = 60;
                                            }
                                            else
                                            {
                                                Variable.AxisAlarmTime[16] += 1;
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 60:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "60-->" + model.ToString() + "下层模块内放料_机械手拍照中，代码为:003");
                                    Variable.RobotRecOK = false;
                                    Variable.RobotSendStr = "003";//告知机械手到上料拍照位置1
                                    Variable.messageRecord[0] = "机械手拍照中，代码为:003";
                                    Variable.RobotTCPAutoStep = 1000;
                                    Variable.ModelSetStep = 70;
                                    break;
                                }

                            case 70:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "70-->" + model.ToString() + "下层模块内放料_机械手拍照位置1OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)//机械手拍照位置1OK
                                        {
                                            Variable.messageRecord[0] = "";
                                            Thread.Sleep(500);
                                            Variable.ModelSetStep = 80;
                                        }
                                    }
                                    break;
                                }
                            case 80:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "80-->" + model.ToString() + "下层模块内放料_判断是否触发相机拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoCheck)
                                        {
                                            Variable.PhotoTCPAutoStep = 101;//触发相机拍照
                                            Variable.ModelSetStep = 90;
                                        }
                                        else
                                        {
                                            PhotoDataOK(Variable.PhotoData1);//放弃拍照
                                            Variable.ModelSetStep = 100;
                                        }
                                    }
                                    break;
                                }
                            case 90:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "90-->" + model.ToString() + "下层模块内放料_判断相机拍照结果是否OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoTCPAutoStep == 0)
                                        {
                                            //while (Variable.PhotoRecMessage.Length < 2)
                                            //{
                                            //    Application.DoEvents();
                                            //    Thread.Sleep(1);
                                            //}
                                            Thread.Sleep((int)Variable.photoDelay);
                                            int rec = UPStringManipulation1(Variable.PhotoRecMessage);

                                            if (rec == 1 && Variable.TestResult)//拍照结果OK
                                            {
                                                Variable.ModelSetStep = 100;
                                            }
                                            else
                                            {
                                                ListBoxTxt("拍照NG，请确认Tray盘");
                                                Variable.ModelSetStep = 95;
                                                RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "95-->" + model.ToString() + "下层模块内放料_拍照NG，请确认Tray盘");
                                            }
                                        }
                                    }
                                    break;
                                }

                            case 96://放弃拍照
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "96-->" + model.ToString() + "下层模块内放料_拍照NG，放弃拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        PhotoDataOK(Variable.PhotoData1);
                                        Variable.ModelSetStep = 100;
                                    }
                                    break;
                                }

                            case 100:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "100-->" + model.ToString() + "下层模块内放料_机械手拍照中，代码为:004");
                                    Variable.RobotRecOK = false;
                                    Variable.RobotSendStr = "004";//告知机械手到上料拍照位置2
                                    Variable.messageRecord[0] = "机械手拍照中，代码为:004";
                                    Variable.RobotTCPAutoStep = 1000;
                                    Variable.ModelSetStep = 110;
                                    break;
                                }

                            case 110:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "110-->" + model.ToString() + "下层模块内放料_机械手拍照位置2 OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)//机械手拍照位置2 OK
                                        {
                                            Variable.messageRecord[0] = "";
                                            Thread.Sleep(1000);
                                            Variable.ModelSetStep = 120;
                                        }
                                    }
                                    break;
                                }
                            case 120:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "120-->" + model.ToString() + "下层模块内放料_判断是否触发相机拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoCheck)
                                        {
                                            Variable.PhotoTCPAutoStep = 102;//触发相机拍照

                                            Variable.ModelSetStep = 130;
                                        }
                                        else
                                        {
                                            PhotoDataOK(Variable.PhotoData2);//放弃拍照
                                            Variable.ModelSetStep = 140;
                                        }
                                    }
                                    break;
                                }
                            case 130:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "130-->" + model.ToString() + "下层模块内放料_判断相机拍照结果是否OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoTCPAutoStep == 0)
                                        {
                                            //while (Variable.PhotoRecMessage.Length < 2)
                                            //{
                                            //    Application.DoEvents();
                                            //    Thread.Sleep(1);
                                            //}
                                            Thread.Sleep((int)Variable.photoDelay);
                                            int rec = UPStringManipulation2(Variable.PhotoRecMessage);

                                            if (rec == 1 && Variable.TestResult)//拍照结果OK
                                            {
                                                Variable.ModelSetStep = 140;
                                            }
                                            else
                                            {
                                                ListBoxTxt("拍照NG，请确认Tray盘");
                                                Variable.ModelSetStep = 135;
                                                RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "135-->" + model.ToString() + "下层模块内放料_拍照NG，请确认Tray盘");
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 136://放弃拍照
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "136-->" + model.ToString() + "下层模块内放料_拍照NG，放弃拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        PhotoDataOK(Variable.PhotoData2);
                                        Variable.ModelSetStep = 140;
                                    }
                                    break;
                                }
                            case 140:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "140-->" + model.ToString() + "下层模块内放料_保存相机数据到TXT");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手在安全位
                                        {
                                            Variable.PhotoData = Variable.PhotoData1.Concat(Variable.PhotoData2).ToArray();
                                            //保存相机数据到TXT
                                            myTXT.WriteTxt(Variable.PhotoData, Application.StartupPath + @"\Data\Photo\Up\" + (model * 4 + 3).ToString() + "\\tray");
                                            //保存相机数据到TXT供测试模组计算良率
                                            myTXT.WriteTxt(Variable.PhotoData, @"D:\Map\Photo\" + (model * 4 + 3).ToString() + "\\tray");
                                            //计算投入数量
                                            InNum((model * 4 + 3).ToString());

                                            Variable.ModelSetStep = 141;
                                        }
                                    }
                                    break;
                                }
                            case 141:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "141-->" + model.ToString() + "下层模块内放料_轴16机械手轴移动到Model放料");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手在安全位
                                        {
                                            double pos = Variable.AxisPos[16, model + 4];
                                            Axis16SetMove(pos); //轴16机械手轴移动到Model放料
                                            if (Variable.AIMpos[16] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[16] >= Math.Round(pos - 0.1, 2))
                                            {
                                                Variable.ModelSetStep = 146;
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 146:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "146-->" + model.ToString() + "下层模块内放料_判断内外上顶气缸是否在下位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.XStatus[132 + model * 32] && Variable.XStatus[136 + model * 32])
                                        {
                                            Variable.ModelSetStep = 150;
                                        }
                                        else if (!Variable.XStatus[132 + model * 32])
                                        {
                                            ListBoxTxt((model + 1).ToString() + "#下老化机内上顶气缸未在下位");
                                            alarmNum = model;
                                            Variable.ModelSetStep = 147;
                                            RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "147-->" + model.ToString() + "下层模块内放料_下老化机内上顶气缸未在下位");
                                        }
                                        else if (!Variable.XStatus[136 + model * 32])
                                        {
                                            ListBoxTxt((model + 1).ToString() + "#下老化机外上顶气缸未在下位");
                                            alarmNum = model;
                                            Variable.ModelSetStep = 148;
                                            RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "148-->" + model.ToString() + "下层模块内放料_下老化机外上顶气缸未在下位");
                                        }
                                    }
                                    break;
                                }
                            case 150:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "150-->" + model.ToString() + "下层模块内放料_告知机械手到Model放料");
                                    Variable.RobotRecOK = false;
                                    Variable.RobotSendStr = (103 + model * 4).ToString();//告知机械手到Model放料
                                    Variable.messageRecord[0] = "机械手" + (model + 1).ToString() + "号模组放料中，代码为:" + (103 + model * 4).ToString();
                                    Variable.RobotTCPAutoStep = 1000;
                                    Variable.ModelSetStep = 160;
                                    break;
                                }

                            case 160:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "160-->" + model.ToString() + "下层模块内放料_判断下老化机内Tray盘是否到位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)
                                        {
                                            Variable.messageRecord[0] = "";
                                            if (Variable.XStatus[131 + model * 32])//Tray到位
                                            {
                                                Variable.ModelSetStep = 170;
                                            }
                                            else
                                            {
                                                ListBoxTxt((model + 1).ToString() + "#下老化机内Tray盘未到位");
                                                alarmNum = model;
                                                Variable.ModelSetStep = 167;
                                                RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "167-->" + model.ToString() + "下层模块内放料_下老化机内Tray盘未到位");
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 170:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "170-->" + model.ToString() + "下层模块内放料_加热标志复位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)
                                        {
                                            Variable.TempOKFlag[model * 4 + 2] = false;
                                            Variable.ModelSetStep = 180;
                                        }
                                    }
                                    break;
                                }
                            case 180:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "180-->" + model.ToString() + "下层模块内放料_延时加热，气缸上升，发送测试指令给单机");
                                    if (Variable.RunEnable == true)
                                    {
                                        Variable.TempOKFlag[model * 4 + 2] = true;//延时加热，气缸上升，发送测试指令给单机
                                        Variable.ModelSetStep = 190;
                                    }
                                    break;
                                }
                            case 190:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "190-->" + model.ToString() + "下层模块内放料_读取UpReady数据到ModelUP");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手在安全位
                                        {
                                            UpReadyTOModelUP((model * 4 + 3).ToString());//读取UpReady数据到ModelUP
                                            Variable.ModelState[model * 4 + 2] = 1;
                                            Variable.RobotSetFlag = false;
                                            Variable.RobotSeting = false;
                                            Variable.RobotSetStep = 0;
                                            Variable.ModelSetStep = 0;
                                        }
                                    }
                                    break;
                                }
                        }
                    }
                }

                #endregion

                #region 下层模块外放料

                if (Variable.RobotSetStep > 20 && ((Variable.RobotSetStep - 21) % 4) == 3)//case 21:模块1动作
                {
                    int model = (Variable.RobotSetStep - 24) / 4;//模块号
                    if (Variable.ModelState[model * 4 + 3] == 0 && (Variable.ModelState[model * 4 + 2] != 0 || Variable.ModelState[model * 4 + 2] == 10))//内部已经放板子或内部屏蔽
                    {
                        switch (Variable.ModelSetStep)
                        {
                            case 10:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "10-->" + model.ToString() + "下层模块外放料_上料待测Tray盘已准备OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.UpReadyTrayOK)
                                        {
                                            Variable.messageRecord[0] = "";
                                            Variable.RobotSeting = true;
                                            Variable.ModelSetStep = 15;
                                        }
                                    }
                                    break;
                                }
                            case 15:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "15-->" + model.ToString() + "下层模块外放料_外检测Tray感应器是否满足条件");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (!Variable.XStatus[135 + model * 32])//上层外Tray到位信号
                                        {
                                            Variable.ModelSetStep = 20;
                                        }
                                        else
                                        {
                                            ListBoxTxt((model + 1).ToString() + "#下层老化机外Tray盘未取走，请检查下层外Tray到位信号");
                                            alarmNum = model;
                                            Variable.ModelSetStep = 14;
                                            RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "14-->" + model.ToString() + "下层模块外放料_下层老化机外Tray盘未取走，请检查");
                                        }
                                    }
                                    break;
                                }
                            case 20:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "20-->" + model.ToString() + "下层模块外放料_轴16机械手X轴移动到取料位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手X轴移动到放料位
                                        {
                                            double pos = Variable.AxisPos[16, 2];
                                            Axis16SetMove(pos); //轴16机械手X轴移动到取料位
                                            if (Variable.AIMpos[16] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[16] >= Math.Round(pos - 0.1, 2))
                                            {
                                                Variable.RobotUpGetTray = false;
                                                Variable.AxisAlarmTime[16] = 0;
                                                Variable.ModelSetStep = 30;
                                            }
                                            else
                                            {
                                                Variable.AxisAlarmTime[16] += 1;
                                            }
                                        }
                                    }
                                    break;
                                }

                            case 30:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "30-->" + model.ToString() + "下层模块外放料_机械手取料中，代码为:001");
                                    if (Variable.UpReadyTrayOK)//告知机械手取料
                                    {
                                        Variable.RobotRecOK = false;
                                        Variable.RobotSendStr = "001";
                                        Variable.messageRecord[0] = "机械手取料中，代码为:001";
                                        Variable.RobotTCPAutoStep = 1000;
                                        Variable.ModelSetStep = 40;
                                    }

                                    break;
                                }

                            case 40:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "40-->" + model.ToString() + "下层模块外放料_机械手取料OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)//机械手取料OK
                                        {
                                            Variable.messageRecord[0] = "";
                                            Variable.RobotUpGetTray = true;
                                            Variable.ModelSetStep = 50;
                                        }
                                    }
                                    break;
                                }
                            case 50:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "50-->" + model.ToString() + "下层模块外放料_轴16机械手X轴移动到拍照位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)
                                        {
                                            double pos = Variable.AxisPos[16, 1];
                                            Axis16SetMove(pos); //轴16机械手X轴移动到拍照位
                                            if (Variable.AIMpos[16] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[16] >= Math.Round(pos - 0.1, 2))
                                            {
                                                Variable.AxisAlarmTime[16] = 0;
                                                Variable.ModelSetStep = 60;
                                            }
                                            else
                                            {
                                                Variable.AxisAlarmTime[16] += 1;
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 60:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "60-->" + model.ToString() + "下层模块外放料_机械手拍照中，代码为:003");
                                    Variable.RobotRecOK = false;
                                    Variable.RobotSendStr = "003";//告知机械手到上料拍照位置1
                                    Variable.messageRecord[0] = "机械手拍照中，代码为:003";
                                    Variable.RobotTCPAutoStep = 1000;
                                    Variable.ModelSetStep = 70;
                                    break;
                                }

                            case 70:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "70-->" + model.ToString() + "下层模块外放料_机械手拍照位置1OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)//机械手拍照位置1OK
                                        {
                                            Variable.messageRecord[0] = "";
                                            Thread.Sleep(500);
                                            Variable.ModelSetStep = 80;
                                        }
                                    }
                                    break;
                                }
                            case 80:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "80-->" + model.ToString() + "下层模块外放料_判断是否触发相机拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoCheck)
                                        {
                                            Variable.PhotoTCPAutoStep = 101;//触发相机拍照
                                            Variable.ModelSetStep = 90;
                                        }
                                        else
                                        {
                                            PhotoDataOK(Variable.PhotoData1);//放弃拍照
                                            Variable.ModelSetStep = 100;
                                        }
                                    }
                                    break;
                                }
                            case 90:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "90-->" + model.ToString() + "下层模块外放料_判断相机拍照结果是否OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoTCPAutoStep == 0)
                                        {
                                            //while (Variable.PhotoRecMessage.Length < 2)
                                            //{
                                            //    Application.DoEvents();
                                            //    Thread.Sleep(1);
                                            //}
                                            Thread.Sleep((int)Variable.photoDelay);
                                            int rec = UPStringManipulation1(Variable.PhotoRecMessage);

                                            if (rec == 1 && Variable.TestResult)//拍照结果OK
                                            {
                                                Variable.ModelSetStep = 100;
                                            }
                                            else
                                            {
                                                ListBoxTxt("拍照NG，请确认Tray盘");
                                                Variable.ModelSetStep = 95;
                                                RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "95-->" + model.ToString() + "下层模块外放料_拍照NG，请确认Tray盘");
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 96://放弃拍照
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "96-->" + model.ToString() + "下层模块外放料_拍照NG，放弃拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        PhotoDataOK(Variable.PhotoData1);
                                        Variable.ModelSetStep = 100;
                                    }
                                    break;
                                }
                            case 100:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "100-->" + model.ToString() + "下层模块外放料_机械手拍照中，代码为:004");
                                    Variable.RobotRecOK = false;
                                    Variable.RobotSendStr = "004";//告知机械手到上料拍照位置2
                                    Variable.messageRecord[0] = "机械手拍照中，代码为:004";
                                    Variable.RobotTCPAutoStep = 1000;
                                    Variable.ModelSetStep = 110;
                                    break;
                                }

                            case 110:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "110-->" + model.ToString() + "下层模块外放料_机械手拍照位置2 OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)//机械手拍照位置2 OK
                                        {
                                            Variable.messageRecord[0] = "";
                                            Thread.Sleep(1000);
                                            Variable.ModelSetStep = 120;
                                        }
                                    }
                                    break;
                                }
                            case 120:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "120-->" + model.ToString() + "下层模块外放料_判断是否触发相机拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoCheck)
                                        {
                                            Variable.PhotoTCPAutoStep = 102;//触发相机拍照
                                            Variable.ModelSetStep = 130;
                                        }
                                        else
                                        {
                                            PhotoDataOK(Variable.PhotoData2);//放弃拍照
                                            Variable.ModelSetStep = 140;
                                        }
                                    }
                                    break;
                                }
                            case 130:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "130-->" + model.ToString() + "下层模块外放料_判断相机拍照结果是否OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoTCPAutoStep == 0)
                                        {
                                            //while (Variable.PhotoRecMessage.Length < 2)
                                            //{
                                            //    Application.DoEvents();
                                            //    Thread.Sleep(1);
                                            //}
                                            Thread.Sleep((int)Variable.photoDelay);
                                            int rec = UPStringManipulation2(Variable.PhotoRecMessage);

                                            if (rec == 1 && Variable.TestResult)//拍照结果OK
                                            {
                                                Variable.ModelSetStep = 140;
                                            }
                                            else
                                            {
                                                ListBoxTxt("拍照NG，请确认Tray盘");
                                                Variable.ModelSetStep = 135;
                                                RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "135-->" + model.ToString() + "下层模块外放料_拍照NG，请确认Tray盘");
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 136://放弃拍照
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "136-->" + model.ToString() + "下层模块外放料_拍照NG，放弃拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        PhotoDataOK(Variable.PhotoData2);
                                        Variable.ModelSetStep = 140;
                                    }
                                    break;
                                }
                            case 140:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "140-->" + model.ToString() + "下层模块外放料_保存相机数据到TXT");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手在安全位
                                        {
                                            Variable.PhotoData = Variable.PhotoData1.Concat(Variable.PhotoData2).ToArray();
                                            //保存相机数据到TXT
                                            myTXT.WriteTxt(Variable.PhotoData, Application.StartupPath + @"\Data\Photo\Up\" + (model * 4 + 4).ToString() + "\\tray");
                                            //保存相机数据到TXT供测试模组计算良率
                                            myTXT.WriteTxt(Variable.PhotoData, @"D:\Map\Photo\" + (model * 4 + 4).ToString() + "\\tray");
                                            //计算投入数量
                                            InNum((model * 4 + 4).ToString());

                                            Variable.ModelSetStep = 141;
                                        }
                                    }
                                    break;
                                }
                            case 141:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "141-->" + model.ToString() + "下层模块外放料_轴16机械手轴移动到Model放料");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手在安全位
                                        {
                                            double pos = Variable.AxisPos[16, model + 4];
                                            Axis16SetMove(pos); //轴16机械手轴移动到Model放料
                                            if (Variable.AIMpos[16] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[16] >= Math.Round(pos - 0.1, 2))
                                            {
                                                Variable.ModelSetStep = 145;
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 145:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "145-->" + model.ToString() + "下层模块外放料_侧定位气缸出");
                                    if (Variable.RunEnable == true)
                                    {
                                        function.OutYON(121 + model * 32);//侧定位气缸出
                                        if (Variable.XStatus[138 + model * 32] && Variable.XStatus[139 + model * 32])
                                        {
                                            Variable.ModelSetStep = 146;
                                        }
                                    }
                                    break;
                                }
                            case 146:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "146-->" + model.ToString() + "下层模块外放料_判断内外上顶气缸是否在下位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.XStatus[136 + model * 32])
                                        {
                                            Variable.ModelSetStep = 150;
                                        }
                                        else if (!Variable.XStatus[136 + model * 32])
                                        {
                                            ListBoxTxt((model + 1).ToString() + "#下老化机外上顶气缸未在下位");
                                            alarmNum = model;
                                            Variable.ModelSetStep = 148;
                                            RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "144-->" + model.ToString() + "下层模块外放料_下老化机外上顶气缸未在下位");
                                        }
                                    }
                                    break;
                                }
                            case 150:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "150-->" + model.ToString() + "下层模块外放料_告知机械手到Model放料");
                                    Variable.RobotRecOK = false;
                                    Variable.RobotSendStr = (104 + model * 4).ToString();//告知机械手到Model放料
                                    Variable.messageRecord[0] = "机械手" + (model + 1).ToString() + "号模组放料中，代码为:" + (104 + model * 4).ToString();
                                    Variable.RobotTCPAutoStep = 1000;
                                    Variable.ModelSetStep = 160;
                                    break;
                                }

                            case 160:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "160-->" + model.ToString() + "下层模块外放料_判断下老化机内Tray盘是否到位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)
                                        {
                                            Variable.messageRecord[0] = "";
                                            if (Variable.XStatus[135 + model * 32])//Tray到位
                                            {
                                                Variable.ModelSetStep = 170;
                                            }
                                            else
                                            {
                                                ListBoxTxt((model + 1).ToString() + "#下老化机外Tray盘未到位");
                                                alarmNum = model;
                                                Variable.ModelSetStep = 168;
                                                RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "168-->" + model.ToString() + "下层模块外放料_下老化机内Tray盘未到位");
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 170:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "170-->" + model.ToString() + "下层模块外放料_加热标志复位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)
                                        {
                                            Variable.TempOKFlag[model * 4 + 3] = false;
                                            Variable.ModelSetStep = 180;
                                        }
                                    }
                                    break;
                                }
                            case 180:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "180-->" + model.ToString() + "下层模块外放料_延时加热，气缸上升，发送测试指令给单机");
                                    if (Variable.RunEnable == true)
                                    {
                                        Variable.TempOKFlag[model * 4 + 3] = true;//延时加热，气缸上升，发送测试指令给单机
                                        Variable.ModelSetStep = 190;
                                    }
                                    break;
                                }
                            case 190:
                                {
                                    RSDAlarmRobotAutoPutStep(Variable.AutoStepMsg[8] = "190-->" + model.ToString() + "下层模块外放料_读取UpReady数据到ModelUP");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手在安全位
                                        {
                                            UpReadyTOModelUP((model * 4 + 4).ToString());//读取UpReady数据到ModelUP
                                            Variable.ModelState[model * 4 + 3] = 1;
                                            Variable.RobotSetFlag = false;
                                            Variable.RobotSeting = false;
                                            Variable.RobotSetStep = 0;
                                            Variable.ModelSetStep = 0;
                                        }
                                    }
                                    break;
                                }
                        }
                    }
                }

                #endregion


                #region 上层模块内取料

                if (Variable.RobotGetStep > 60 && ((Variable.RobotGetStep - 61) % 4) == 0)//case 61:模块1动作
                {
                    int model = (Variable.RobotGetStep - 61) / 4;//模块号
                    if (Variable.ModelState[model * 4] == 3 && (Variable.ModelState[model * 4 + 1] == 0 || Variable.ModelState[model * 4 + 1] == 10))//外部没有放板子或外部屏蔽
                    {
                        switch (Variable.ModelGetStep)
                        {
                            case 10:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "10-->" + model.ToString() + "上层模块内取料_取料流程标志置位");
                                    if (Variable.RunEnable == true)
                                    {
                                        Variable.messageRecord[0] = "";
                                        Variable.RobotGeting = true;
                                        Variable.ModelGetStep = 15;
                                    }
                                    break;
                                }
                            case 15:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "15-->" + model.ToString() + "上层模块内取料_上层老化机外Tray盘是否取走");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (!Variable.XStatus[121 + model * 32])//上层2Tray到位信号
                                        {
                                            Variable.ModelGetStep = 20;
                                        }
                                        else
                                        {
                                            ListBoxTxt((model + 1).ToString() + "#上层老化机外Tray盘未取走，请检查上层外Tray到位信号");
                                            alarmNum = model;
                                            Variable.ModelGetStep = 16;
                                            RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "16-->" + model.ToString() + "上层模块内取料_上层老化机外Tray盘未取走，请检查");
                                        }
                                    }
                                    break;
                                }
                            case 20:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "20-->" + model.ToString() + "上层模块内取料_轴16机械手轴移动到Model1取料");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手在安全位
                                        {
                                            double pos = Variable.AxisPos[16, model + 4];
                                            Axis16SetMove(pos); //轴16机械手轴移动到Model1取料
                                            if (Variable.AIMpos[16] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[16] >= Math.Round(pos - 0.1, 2))
                                            {
                                                Variable.AxisAlarmTime[16] = 0;
                                                Variable.ModelGetStep = 30;
                                            }
                                            else
                                            {
                                                Variable.AxisAlarmTime[16] += 1;
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 30:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "30-->" + model.ToString() + "上层模块内取料_上层内老化测试板断电");
                                    if (Variable.RunEnable == true)
                                    {
                                        function.OutYOFF(108 + model * 32);//上层内老化测试板断电
                                        Thread.Sleep(100);
                                        Variable.ModelGetStep = 40;
                                    }
                                    break;
                                }
                            case 40:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "40-->" + model.ToString() + "上层模块内取料_上层内老化上下气缸下降");
                                    if (Variable.RunEnable == true)
                                    {
                                        function.OutYOFF(102 + model * 32); //上层内老化上下气缸上升
                                        function.OutYON(101 + model * 32); //上层内老化上下气缸下降
                                        Thread.Sleep(100);
                                        Variable.ProbeNum[model * 4] += 1;
                                        ProbRefresh();
                                        if (Variable.XStatus[118 + model * 32])
                                        {
                                            Variable.ModelGetStep = 41;
                                        }
                                    }
                                    break;
                                }
                            case 41:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "41-->" + model.ToString() + "上层模块内取料_上层老化侧顶气缸回");
                                    if (Variable.RunEnable == true)
                                    {
                                        function.OutYOFF(105 + model * 32); //上层老化侧顶气缸回
                                        if (!Variable.XStatus[124 + model * 32] && !Variable.XStatus[125 + model * 32])
                                        {
                                            Variable.ModelGetStep = 42;
                                        }
                                    }
                                    break;
                                }
                            case 42:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "42-->" + model.ToString() + "上层模块内取料_告知机械手到Model取料");
                                    Variable.RobotRecOK = false;
                                    Variable.RobotSendStr = (141 + model * 4).ToString();//告知机械手到Model取料
                                    Variable.messageRecord[0] = "机械手" + (model + 1).ToString() + "号模组取料中，代码为:" + (141 + model * 4).ToString();
                                    Variable.RobotTCPAutoStep = 1000;
                                    Variable.ModelGetStep = 43;
                                    break;
                                }
                            case 43://等待机械手到位，推Tray气缸出
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "43-->" + model.ToString() + "等待机械手到位，推Tray气缸出");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.XStatus[28])
                                        {
                                            Variable.ModelGetStep = 44;
                                        }
                                    }
                                    break;
                                }
                            case 44: //上层老化推Tray气缸出
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "44-->" + model.ToString() + "上层老化推Tray气缸出");
                                    if (Variable.RunEnable == true)
                                    {
                                        function.OutYON(106 + model * 32); //上层老化推Tray气缸出
                                        if (Variable.XStatus[127 + model * 32])
                                        {
                                            Thread.Sleep(500);
                                            Variable.ModelGetStep = 45;
                                        }
                                    }
                                    break;
                                }
                            case 45://上层老化推Tray气缸回
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "45-->" + model.ToString() + "上层老化推Tray气缸回");
                                    if (Variable.RunEnable == true)
                                    {
                                        function.OutYOFF(106 + model * 32); //上层老化推Tray气缸回
                                        if (Variable.XStatus[126 + model * 32])
                                        {
                                            Variable.ModelGetStep = 46;
                                        }
                                    }
                                    break;
                                }
                            case 46://告诉机械手可以继续动作
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "46-->" + model.ToString() + "告诉机械手可以继续动作");
                                    if (Variable.RunEnable == true)
                                    {
                                        function.OutYON(19); //告诉机械手可以继续动作
                                        if (!Variable.XStatus[28])
                                        {
                                            Variable.ModelGetStep = 60;
                                        }

                                    }
                                    break;
                                }
                            //case 50:
                            //    {
                            //        RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "50-->" + model.ToString() + "上层模块内取料_告知机械手到Model取料");
                            //        Variable.RobotRecOK = false;
                            //        Variable.RobotSendStr = (141 + model * 4).ToString();//告知机械手到Model取料
                            //        Variable.messageRecord[0] = "机械手" + (model + 1).ToString() + "号模组取料中，代码为:" + (141 + model * 4).ToString();
                            //        Variable.RobotTCPAutoStep = 1000;
                            //        Variable.ModelGetStep = 60;
                            //        break;
                            //    }

                            case 60:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "60-->" + model.ToString() + "上层模块内取料_上层老化机内Tray盘是否取走");
                                    if (Variable.RunEnable == true)
                                    {
                                        function.OutYOFF(19); //告诉机械手可以继续动作
                                        if (Variable.RobotRecOK)
                                        {

                                            Variable.messageRecord[0] = "";
                                            if (!Variable.XStatus[117 + model * 32])
                                            {
                                                Variable.ModelGetStep = 90;
                                            }
                                            else
                                            {
                                                ListBoxTxt((model + 1).ToString() + "#上层老化机内Tray盘未取走");
                                                alarmNum = model;
                                                Variable.ModelGetStep = 65;
                                                RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "65-->" + model.ToString() + "上层模块内取料_上层老化机内Tray盘未取走");
                                            }
                                        }
                                    }
                                    break;
                                }

                            case 90:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "90-->" + model.ToString() + "上层模块内取料_轴16机械手X轴移动到拍照位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)
                                        {
                                            double pos = Variable.AxisPos[16, 1];
                                            Axis16SetMove(pos); //轴16机械手X轴移动到拍照位
                                            if (Variable.AIMpos[16] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[16] >= Math.Round(pos - 0.1, 2))
                                            {
                                                Variable.AxisAlarmTime[16] = 0;
                                                Variable.ModelGetStep = 100;
                                            }
                                            else
                                            {
                                                Variable.AxisAlarmTime[16] += 1;
                                            }
                                        }
                                    }
                                    break;
                                }

                            case 100:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "100-->" + model.ToString() + "上层模块内取料_机械手拍照中，代码为:003");
                                    Variable.RobotRecOK = false;
                                    Variable.RobotSendStr = "003";//告知机械手到上料拍照位置1
                                    Variable.messageRecord[0] = "机械手拍照中，代码为:003";
                                    Variable.RobotTCPAutoStep = 1000;
                                    Variable.ModelGetStep = 110;
                                    break;
                                }

                            case 110:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "110-->" + model.ToString() + "上层模块内取料_机械手拍照位置1OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)//机械手拍照位置1OK
                                        {
                                            Variable.messageRecord[0] = "";
                                            Thread.Sleep(500);
                                            Variable.ModelGetStep = 120;
                                        }
                                    }
                                    break;
                                }
                            case 120:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "120-->" + model.ToString() + "上层模块内取料_判断是否触发相机拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoCheck)
                                        {
                                            Variable.PhotoTCPAutoStep = 101;//触发相机拍照
                                            Variable.ModelGetStep = 130;
                                        }
                                        else
                                        {
                                            Variable.ModelGetStep = 140;
                                        }
                                    }
                                    break;
                                }
                            case 130:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "130-->" + model.ToString() + "上层模块内取料_判断相机拍照是否OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoTCPAutoStep == 0)
                                        {
                                            //while (Variable.PhotoRecMessage.Length < 2)
                                            //{
                                            //    Application.DoEvents();
                                            //    Thread.Sleep(1);
                                            //}
                                            Thread.Sleep((int)Variable.photoDelay);
                                            int rec = DownStringManipulation1(Variable.PhotoRecMessage);

                                            if (rec == 1 && Variable.TestResult)//拍照结果OK
                                            {
                                                Variable.ModelGetStep = 140;
                                            }
                                            else
                                            {
                                                ListBoxTxt("拍照NG，请确认Tray盘");
                                                Variable.ModelGetStep = 135;
                                                RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "135-->" + model.ToString() + "上层模块内取料_拍照NG，请确认Tray盘");
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 136://放弃拍照
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "136-->" + model.ToString() + "上层模块内取料_拍照NG，放弃拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        PhotoDataOK(Variable.PhotoData1);
                                        Variable.ModelGetStep = 140;
                                    }
                                    break;
                                }
                            case 140:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "140-->" + model.ToString() + "上层模块内取料_机械手拍照中，代码为:004");
                                    Variable.RobotRecOK = false;
                                    Variable.RobotSendStr = "004";//告知机械手到上料拍照位置2
                                    Variable.messageRecord[0] = "机械手拍照中，代码为:004";
                                    Variable.RobotTCPAutoStep = 1000;
                                    Variable.ModelGetStep = 150;
                                    break;
                                }

                            case 150:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "150-->" + model.ToString() + "上层模块内取料_机械手拍照位置2OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)//机械手拍照位置2OK
                                        {
                                            Variable.messageRecord[0] = "";
                                            Thread.Sleep(1000);
                                            Variable.ModelGetStep = 160;
                                        }
                                    }
                                    break;
                                }
                            case 160:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "160-->" + model.ToString() + "上层模块内取料_判断是否触发相机拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoCheck)
                                        {
                                            Variable.PhotoTCPAutoStep = 102;//触发相机拍照
                                            Variable.ModelGetStep = 170;
                                        }
                                        else
                                        {
                                            Variable.ModelGetStep = 180;
                                        }
                                    }
                                    break;
                                }
                            case 170:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "170-->" + model.ToString() + "上层模块内取料_判断相机拍照是否OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoTCPAutoStep == 0)
                                        {
                                            //while (Variable.PhotoRecMessage.Length < 2)
                                            //{
                                            //    Application.DoEvents();
                                            //    Thread.Sleep(1);
                                            //}
                                            Thread.Sleep((int)Variable.photoDelay);
                                            int rec = DownStringManipulation2(Variable.PhotoRecMessage);

                                            if (rec == 1 && Variable.TestResult)//拍照结果OK
                                            {
                                                Variable.ModelGetStep = 180;
                                            }
                                            else
                                            {
                                                ListBoxTxt("拍照NG，请确认Tray盘");
                                                Variable.ModelGetStep = 175;
                                                RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "175-->" + model.ToString() + "上层模块内取料_拍照NG，请确认Tray盘");
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 176://放弃拍照
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "176-->" + model.ToString() + "上层模块内取料_拍照NG，放弃拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        PhotoDataOK(Variable.PhotoData2);
                                        Variable.ModelGetStep = 180;
                                    }
                                    break;
                                }
                            case 180:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "180-->" + model.ToString() + "上层模块内取料_相机数据写入TXT");
                                    if (Variable.RunEnable == true)
                                    {
                                        Variable.PhotoData = Variable.PhotoData1.Concat(Variable.PhotoData2).ToArray();
                                        //向TXT写入数据
                                        myTXT.WriteTxt(Variable.PhotoData, Application.StartupPath + @"\Data\Photo\Down\tray");
                                        Thread.Sleep(100);
                                        Variable.ModelGetStep = 185;
                                    }
                                    break;
                                }
                            case 185:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "185-->" + model.ToString() + "上层模块内取料_判断端口是不是NG");
                                    if (Variable.RunEnable == true)
                                    {
                                        JudgeNGCount(model * 4 + 1);//判断端口是不是NG
                                        SaveNGCount(model * 4 + 1);//保存端口是不是NG
                                        Variable.RobotSetFlag = false;
                                        takeNum[model * 4] = 1;
                                        Variable.ModelGetStep = 190;
                                    }
                                    break;
                                }
                            case 190:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "190-->" + model.ToString() + "上层模块内取料_相机与模组比较");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoCheck)
                                        {
                                            int n = ModelToPhoto((model * 4 + 1).ToString());//相机与模组比较
                                            if (n == 1)
                                            {
                                                Variable.ModelGetStep = 200;
                                            }
                                            else
                                            {
                                                //ListBoxTxt("料盘上缺料，检查是否掉料或模组粘料");
                                                Variable.PhotoAlarm = true;
                                                Thread.Sleep(200);
                                                Variable.TrayNum = (model * 4 + 1).ToString();
                                                Variable.ModelGetStep = 195;
                                                RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "195-->" + model.ToString() + "上层模块内取料_料盘上缺料，检查是否掉料或模组粘料");
                                            }
                                        }
                                        else
                                        {
                                            Variable.ModelGetStep = 200;
                                        }
                                    }
                                    break;
                                }

                            case 200:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "200-->" + model.ToString() + "上层模块内取料_轴16机械手X轴移动到下料位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手X轴移动到下料位
                                        {
                                            double pos = Variable.AxisPos[16, 3];
                                            Axis16SetMove(pos); //轴16机械手X轴移动到下料位
                                            if (Variable.AIMpos[16] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[16] >= Math.Round(pos - 0.1, 2))
                                            {
                                                Variable.AxisAlarmTime[16] = 0;
                                                Variable.ModelGetStep = 210;
                                            }
                                            else
                                            {
                                                Variable.AxisAlarmTime[16] += 1;
                                            }
                                        }
                                    }
                                    break;
                                }

                            case 210:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "210-->" + model.ToString() + "上层模块内取料_机械手放料中，代码为:007");
                                    if (Variable.DownGetTray)//告知机械手放料
                                    {
                                        Variable.RobotRecOK = false;
                                        Variable.RobotSendStr = "007";
                                        Variable.messageRecord[0] = "机械手放料中，代码为:007";
                                        Variable.RobotTCPAutoStep = 1000;
                                        Variable.ModelGetStep = 220;
                                    }
                                    break;
                                }

                            case 220:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "220-->" + model.ToString() + "上层模块内取料_械手放料位OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)
                                        {
                                            Variable.messageRecord[0] = "";
                                            Variable.ModelGetStep = 230;
                                        }
                                    }
                                    break;
                                }
                            case 230:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "230-->" + model.ToString() + "上层模块内取料_读取Map文件传给下料机");
                                    if (Variable.RunEnable == true)
                                    {
                                        string st = @"D:\Map\" + (model * 4 + 1).ToString() + "\\tray";
                                        string[] Readstr = myTXT.ReadTXT1(st);
                                        string[] Readstr1 = new string[152];
                                        if (Readstr.Length == 0)
                                        {
                                            for (int i = 0; i < 152; i++)
                                            {
                                                Readstr1[i] = "20";
                                            }
                                            myTXT.WriteTxt(Readstr1, Application.StartupPath + @"\Map\" + (model * 4 + 1).ToString() + "\\tray");//读取D:\map\1\tray文本到 Map\tray中
                                        }
                                        else
                                        {
                                            myTXT.WriteTxt(Readstr, Application.StartupPath + @"\Map\" + (model * 4 + 1).ToString() + "\\tray");//读取D:\map\1\tray文本到 Map\tray中
                                        }

                                        Thread.Sleep(500);
                                        GetMapToDown((model * 4 + 1).ToString());//读取Map文件传给下料机
                                        Thread.Sleep(500);
                                        ModelResultToDown((model * 4 + 1).ToString());//根据上料模组数据找出空位发送给下料机
                                        DeleteFinishTXT((model * 4 + 1).ToString());//删除finish文档
                                        DeleteTrayTXT((model * 4 + 1).ToString());//删除Tray文档
                                        Variable.RobotDownGetTray = true;

                                        Variable.ModelGetStep = 240;
                                    }
                                    break;
                                }

                            case 240:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "230-->" + model.ToString() + "上层模块内取料_计算产出总数");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手在安全位
                                        {
                                            if (!Variable.DownGetTray)
                                            {
                                                OutNum(model * 4 + 1);//计算产出总数
                                                Variable.RobotGeting = false;
                                                Variable.RobotGet = 0;
                                                Variable.RobotGetStep = 0;
                                                Variable.ModelState[model * 4] = 0;//模组状态=>0:空
                                                HideTest();//判断是否有屏蔽的机台

                                                Variable.ModelGetStep = 0;
                                            }
                                        }
                                    }
                                    break;
                                }
                        }
                    }
                }

                #endregion

                #region 上层模块外取料

                if (Variable.RobotGetStep > 60 && ((Variable.RobotGetStep - 61) % 4) == 1)//case 61:模块1动作
                {
                    int model = (Variable.RobotGetStep - 62) / 4;//模块号
                    if (Variable.ModelState[model * 4 + 1] == 3 && (Variable.ModelState[model * 4] == 3 || Variable.ModelState[model * 4] == 0 || Variable.ModelState[model * 4] == 10))//内部已经放板子或内部屏蔽
                    {
                        switch (Variable.ModelGetStep)
                        {
                            case 10:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "10-->" + model.ToString() + "上层模块外取料_取料流程标志置位");
                                    if (Variable.RunEnable == true)
                                    {
                                        Variable.messageRecord[0] = "";
                                        Variable.RobotGeting = true;
                                        Variable.ModelGetStep = 20;
                                    }
                                    break;
                                }

                            case 20:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "20-->" + model.ToString() + "上层模块外取料_轴16机械手轴移动到Model1取料");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手在安全位
                                        {
                                            double pos = Variable.AxisPos[16, model + 4];
                                            Axis16SetMove(pos); //轴16机械手轴移动到Model1取料
                                            if (Variable.AIMpos[16] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[16] >= Math.Round(pos - 0.1, 2))
                                            {
                                                Variable.AxisAlarmTime[16] = 0;
                                                Variable.ModelGetStep = 30;
                                            }
                                            else
                                            {
                                                Variable.AxisAlarmTime[16] += 1;
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 30:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "30-->" + model.ToString() + "上层模块外取料_上层外老化测试板断电");
                                    if (Variable.RunEnable == true)
                                    {
                                        function.OutYOFF(110 + model * 32);//上层外老化测试板断电
                                        Thread.Sleep(100);
                                        Variable.ModelGetStep = 40;
                                    }
                                    break;
                                }
                            case 40:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "40-->" + model.ToString() + "上层模块外取料_上层外老化上下气缸下降");
                                    if (Variable.RunEnable == true)
                                    {
                                        function.OutYOFF(104 + model * 32); ;//上层外老化上下气缸上升
                                        function.OutYON(103 + model * 32); ;//上层外老化上下气缸下降
                                        Thread.Sleep(100);
                                        Variable.ProbeNum[model * 4 + 1] += 1;
                                        ProbRefresh();
                                        Variable.ModelGetStep = 50;
                                    }
                                    break;
                                }
                            case 50:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "50-->" + model.ToString() + "上层模块外取料_告知机械手到Model取料");
                                    if (Variable.XStatus[122 + model * 32])
                                    {
                                        Variable.RobotRecOK = false;
                                        Variable.RobotSendStr = (142 + model * 4).ToString();//告知机械手到Model取料
                                        Variable.messageRecord[0] = "机械手" + (model + 1).ToString() + "号模组取料中，代码为:" + (142 + model * 4).ToString();
                                        Variable.RobotTCPAutoStep = 1000;
                                        Variable.ModelGetStep = 60;
                                    }
                                    break;
                                }

                            case 60:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "60-->" + model.ToString() + "上层模块外取料_上层老化机外Tray盘是否取走");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)
                                        {
                                            Variable.messageRecord[0] = "";
                                            if (!Variable.XStatus[121 + model * 32])
                                            {
                                                Variable.ModelGetStep = 90;
                                            }
                                            else
                                            {
                                                ListBoxTxt((model + 1).ToString() + "#上层老化机外Tray盘未取走");
                                                alarmNum = model;
                                                Variable.ModelGetStep = 66;
                                                RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "66-->" + model.ToString() + "上层模块外取料_上层老化机外Tray盘未取走");
                                            }
                                        }
                                    }
                                    break;
                                }

                            case 90:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "90-->" + model.ToString() + "上层模块外取料_轴16机械手X轴移动到拍照位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)
                                        {
                                            double pos = Variable.AxisPos[16, 1];
                                            Axis16SetMove(pos); //轴16机械手X轴移动到拍照位
                                            if (Variable.AIMpos[16] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[16] >= Math.Round(pos - 0.1, 2))
                                            {
                                                Variable.AxisAlarmTime[16] = 0;
                                                Variable.ModelGetStep = 100;
                                            }
                                            else
                                            {
                                                Variable.AxisAlarmTime[16] += 1;
                                            }
                                        }
                                    }
                                    break;
                                }

                            case 100:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "100-->" + model.ToString() + "上层模块外取料_机械手拍照中，代码为:003");
                                    Variable.RobotRecOK = false;
                                    Variable.RobotSendStr = "003";//告知机械手到上料拍照位置1
                                    Variable.messageRecord[0] = "机械手拍照中，代码为:003";
                                    Variable.RobotTCPAutoStep = 1000;
                                    Variable.ModelGetStep = 110;
                                    break;
                                }

                            case 110:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "110-->" + model.ToString() + "上层模块外取料_机械手拍照位置1OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)//机械手拍照位置1OK
                                        {
                                            Variable.messageRecord[0] = "";
                                            Thread.Sleep(500);
                                            Variable.ModelGetStep = 120;
                                        }
                                    }
                                    break;
                                }
                            case 120:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "120-->" + model.ToString() + "上层模块外取料_判断是否触发相机拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoCheck)
                                        {
                                            Variable.PhotoTCPAutoStep = 101;//触发相机拍照
                                            Variable.ModelGetStep = 130;
                                        }
                                        else
                                        {
                                            Variable.ModelGetStep = 140;
                                        }
                                    }
                                    break;
                                }
                            case 130:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "130-->" + model.ToString() + "上层模块外取料_判断相机拍照是否OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoTCPAutoStep == 0)
                                        {
                                            //while (Variable.PhotoRecMessage.Length < 2)
                                            //{
                                            //    Application.DoEvents();
                                            //    Thread.Sleep(1);
                                            //}
                                            Thread.Sleep((int)Variable.photoDelay);
                                            int rec = DownStringManipulation1(Variable.PhotoRecMessage);

                                            if (rec == 1 && Variable.TestResult)//拍照结果OK
                                            {
                                                Variable.ModelGetStep = 140;
                                            }
                                            else
                                            {
                                                ListBoxTxt("拍照NG，请确认Tray盘");
                                                Variable.ModelGetStep = 135;
                                                RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "135-->" + model.ToString() + "上层模块外取料_拍照NG，请确认Tray盘");
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 136://放弃拍照
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "136-->" + model.ToString() + "上层模块外取料_拍照NG，放弃拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        PhotoDataOK(Variable.PhotoData1);
                                        Variable.ModelGetStep = 140;
                                    }
                                    break;
                                }
                            case 140:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "140-->" + model.ToString() + "上层模块外取料_机械手拍照中，代码为:004");
                                    Variable.RobotRecOK = false;
                                    Variable.RobotSendStr = "004";//告知机械手到上料拍照位置2
                                    Variable.messageRecord[0] = "机械手拍照中，代码为:004";
                                    Variable.RobotTCPAutoStep = 1000;
                                    Variable.ModelGetStep = 150;
                                    break;
                                }

                            case 150:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "150-->" + model.ToString() + "上层模块外取料_机械手拍照位置2OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)//机械手拍照位置2OK
                                        {
                                            Variable.messageRecord[0] = "";
                                            Thread.Sleep(1000);
                                            Variable.ModelGetStep = 160;
                                        }
                                    }
                                    break;
                                }
                            case 160:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "160-->" + model.ToString() + "上层模块外取料_判断是否触发相机拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoCheck)
                                        {
                                            Variable.PhotoTCPAutoStep = 102;//触发相机拍照
                                            Variable.ModelGetStep = 170;
                                        }
                                        else
                                        {
                                            Variable.ModelGetStep = 180;
                                        }
                                    }
                                    break;
                                }
                            case 170:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "170-->" + model.ToString() + "上层模块外取料_判断相机拍照是否OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoTCPAutoStep == 0)
                                        {
                                            //while (Variable.PhotoRecMessage.Length < 2)
                                            //{
                                            //    Application.DoEvents();
                                            //    Thread.Sleep(1);
                                            //}
                                            Thread.Sleep((int)Variable.photoDelay);
                                            int rec = DownStringManipulation2(Variable.PhotoRecMessage);

                                            if (rec == 1 && Variable.TestResult)//拍照结果OK
                                            {
                                                Variable.ModelGetStep = 180;
                                            }
                                            else
                                            {
                                                ListBoxTxt("拍照NG，请确认Tray盘");
                                                Variable.ModelGetStep = 175;
                                                RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "175-->" + model.ToString() + "上层模块外取料_拍照NG，请确认Tray盘");
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 176://放弃拍照
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "176-->" + model.ToString() + "上层模块外取料_拍照NG，放弃拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        PhotoDataOK(Variable.PhotoData2);
                                        Variable.ModelGetStep = 180;
                                    }
                                    break;
                                }
                            case 180:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "180-->" + model.ToString() + "上层模块外取料_相机数据写入TXT");
                                    if (Variable.RunEnable == true)
                                    {
                                        Variable.PhotoData = Variable.PhotoData1.Concat(Variable.PhotoData2).ToArray();
                                        //向TXT写入数据
                                        myTXT.WriteTxt(Variable.PhotoData, Application.StartupPath + @"\Data\Photo\Down\tray");
                                        Thread.Sleep(100);
                                        Variable.ModelGetStep = 185;
                                    }
                                    break;
                                }
                            case 185:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "185-->" + model.ToString() + "上层模块外取料_判断端口是不是NG");
                                    if (Variable.RunEnable == true)
                                    {
                                        JudgeNGCount(model * 4 + 2);//判断端口是不是NG
                                        SaveNGCount(model * 4 + 2);//储存端口是不是NG
                                        Variable.RobotSetFlag = false;
                                        takeNum[model * 4 + 1] = 1;
                                        Variable.ModelGetStep = 190;
                                    }
                                    break;
                                }
                            case 190:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "190-->" + model.ToString() + "上层模块外取料_相机与模组比较");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoCheck)
                                        {
                                            int n = ModelToPhoto((model * 4 + 2).ToString());//相机与模组比较
                                            if (n == 1)
                                            {
                                                Variable.ModelGetStep = 200;
                                            }
                                            else
                                            {
                                                //ListBoxTxt("料盘上缺料，检查是否掉料或模组粘料");
                                                Variable.PhotoAlarm = true;
                                                Thread.Sleep(200);
                                                Variable.TrayNum = (model * 4 + 2).ToString();
                                                Variable.ModelGetStep = 195;
                                                RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "195-->" + model.ToString() + "上层模块外取料_料盘上缺料，检查是否掉料或模组粘料");
                                            }
                                        }
                                        else
                                        {
                                            Variable.ModelGetStep = 200;
                                        }
                                    }
                                    break;
                                }

                            case 200:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "200-->" + model.ToString() + "上层模块外取料_轴16机械手X轴移动到下料位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手X轴移动到下料位
                                        {
                                            double pos = Variable.AxisPos[16, 3];
                                            Axis16SetMove(pos); //轴16机械手X轴移动到下料位
                                            if (Variable.AIMpos[16] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[16] >= Math.Round(pos - 0.1, 2))
                                            {
                                                Variable.AxisAlarmTime[16] = 0;
                                                Variable.ModelGetStep = 210;
                                            }
                                            else
                                            {
                                                Variable.AxisAlarmTime[16] += 1;
                                            }
                                        }
                                    }
                                    break;
                                }

                            case 210:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "210-->" + model.ToString() + "上层模块外取料_机械手放料中，代码为:007");
                                    if (Variable.DownGetTray)//告知机械手放料
                                    {
                                        Variable.RobotRecOK = false;
                                        Variable.RobotSendStr = "007";
                                        Variable.messageRecord[0] = "机械手放料中，代码为:007";
                                        Variable.RobotTCPAutoStep = 1000;
                                        Variable.ModelGetStep = 220;
                                    }
                                    break;
                                }

                            case 220:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "220-->" + model.ToString() + "上层模块外取料_械手放料位OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)
                                        {
                                            Variable.messageRecord[0] = "";
                                            Variable.ModelGetStep = 230;
                                        }
                                    }
                                    break;
                                }
                            case 230:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "230-->" + model.ToString() + "上层模块外取料_读取Map文件传给下料机");
                                    if (Variable.RunEnable == true)
                                    {
                                        string st = @"D:\Map\" + (model * 4 + 2).ToString() + "\\tray";
                                        string[] Readstr = myTXT.ReadTXT1(st);
                                        string[] Readstr1 = new string[152];
                                        if (Readstr.Length == 0)
                                        {
                                            for (int i = 0; i < 152; i++)
                                            {
                                                Readstr1[i] = "20";
                                            }
                                            myTXT.WriteTxt(Readstr1, Application.StartupPath + @"\Map\" + (model * 4 + 2).ToString() + "\\tray");//读取D:\map\1\tray文本到 Map\tray中
                                        }
                                        else
                                        {
                                            myTXT.WriteTxt(Readstr, Application.StartupPath + @"\Map\" + (model * 4 + 2).ToString() + "\\tray");//读取D:\map\1\tray文本到 Map\tray中
                                        }

                                        Thread.Sleep(500);
                                        GetMapToDown((model * 4 + 2).ToString());//读取Map文件传给下料机
                                        Thread.Sleep(500);
                                        ModelResultToDown((model * 4 + 2).ToString());//根据上料模组数据找出空位发送给下料机
                                        DeleteFinishTXT((model * 4 + 2).ToString());//删除finish文档
                                        DeleteTrayTXT((model * 4 + 2).ToString());//删除Tray文档 
                                        Variable.RobotDownGetTray = true;

                                        Variable.ModelGetStep = 240;

                                    }
                                    break;
                                }

                            case 240:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "230-->" + model.ToString() + "上层模块外取料_计算产出总数");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手在安全位
                                        {
                                            if (!Variable.DownGetTray)
                                            {
                                                OutNum(model * 4 + 2);//计算产出总数
                                                Variable.RobotGeting = false;
                                                Variable.RobotGet = 0;
                                                Variable.RobotGetStep = 0;
                                                Variable.ModelState[model * 4 + 1] = 0;//模组状态=>0:空
                                                HideTest();//判断是否有屏蔽的机台

                                                Variable.ModelGetStep = 0;
                                            }
                                        }
                                    }
                                    break;
                                }
                        }
                    }
                }

                #endregion

                #region 下层模块内取料

                if (Variable.RobotGetStep > 60 && ((Variable.RobotGetStep - 61) % 4) == 2)//case 61:模块1动作
                {
                    int model = (Variable.RobotGetStep - 63) / 4;//模块号
                    if (Variable.ModelState[model * 4 + 2] == 3 && (Variable.ModelState[model * 4 + 3] == 0 || Variable.ModelState[model * 4 + 3] == 10))//外部没有放板子或外部屏蔽
                    {
                        switch (Variable.ModelGetStep)
                        {
                            case 10:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "10-->" + model.ToString() + "下层模块内取料_取料流程标志置位");
                                    if (Variable.RunEnable == true)
                                    {
                                        Variable.messageRecord[0] = "";
                                        Variable.RobotGeting = true;
                                        Variable.ModelGetStep = 15;
                                    }
                                    break;
                                }
                            case 15:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "15-->" + model.ToString() + "下层模块内取料_下层老化机外Tray盘是否取走");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (!Variable.XStatus[135 + model * 32])//下层2Tray到位信号
                                        {
                                            Variable.ModelGetStep = 20;
                                        }
                                        else
                                        {
                                            ListBoxTxt((model + 1).ToString() + "#下层老化机外Tray盘未取走，请检查下层外Tray到位信号");
                                            alarmNum = model;
                                            Variable.ModelGetStep = 17;
                                            RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "17-->" + model.ToString() + "下层模块内取料_下层老化机外Tray盘未取走，请检查");
                                        }
                                    }
                                    break;
                                }
                            case 20:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "20-->" + model.ToString() + "下层模块内取料_轴16机械手轴移动到Model1取料");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手在安全位
                                        {
                                            double pos = Variable.AxisPos[16, model + 4];
                                            Axis16SetMove(pos); //轴16机械手轴移动到Model1取料
                                            if (Variable.AIMpos[16] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[16] >= Math.Round(pos - 0.1, 2))
                                            {
                                                Variable.AxisAlarmTime[16] = 0;
                                                Variable.ModelGetStep = 30;
                                            }
                                            else
                                            {
                                                Variable.AxisAlarmTime[16] += 1;
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 30:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "30-->" + model.ToString() + "下层模块内取料_下层内老化测试板断电");
                                    if (Variable.RunEnable == true)
                                    {
                                        function.OutYOFF(124 + model * 32);//下层内老化测试板断电
                                        Thread.Sleep(100);
                                        Variable.ModelGetStep = 40;
                                    }
                                    break;
                                }
                            case 40:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "40-->" + model.ToString() + "下层模块内取料_下层内老化上下气缸下降");
                                    if (Variable.RunEnable == true)
                                    {
                                        function.OutYOFF(118 + model * 32); ;//下层内老化上下气缸上升
                                        function.OutYON(117 + model * 32); ;//下层内老化上下气缸下降
                                        Thread.Sleep(100);
                                        Variable.ProbeNum[model * 4 + 2] += 1;
                                        ProbRefresh();
                                        if (Variable.XStatus[132 + model * 32])
                                        {
                                            Variable.ModelGetStep = 41;
                                        }
                                    }
                                    break;
                                }
                            case 41://下层老化侧顶气缸回
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "41-->" + model.ToString() + "下层老化侧顶气缸回");
                                    if (Variable.RunEnable == true)
                                    {
                                        function.OutYOFF(121 + model * 32); ;//下层老化侧顶气缸回
                                        if (!Variable.XStatus[138 + model * 32] && !Variable.XStatus[139 + model * 32])
                                        {
                                            Variable.ModelGetStep = 42;
                                        }
                                    }
                                    break;
                                }
                            case 42:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "42-->" + model.ToString() + "下层模块内取料_告知机械手到Model取料");
                                    Variable.RobotRecOK = false;
                                    Variable.RobotSendStr = (143 + model * 4).ToString();//告知机械手到Model取料
                                    Variable.messageRecord[0] = "机械手" + (model + 1).ToString() + "号模组取料中，代码为:" + (143 + model * 4).ToString();
                                    Variable.RobotTCPAutoStep = 1000;
                                    Variable.ModelGetStep = 43;
                                    break;
                                }
                            case 43://等待机械手到位，推Tray气缸出
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "43-->" + model.ToString() + "等待机械手到位，推Tray气缸出");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.XStatus[28])
                                        {
                                            Variable.ModelGetStep = 44;
                                        }
                                    }
                                    break;
                                }
                            case 44://下层老化推Tray气缸出
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "44-->" + model.ToString() + "下层模块内取料_下层老化推Tray气缸出");
                                    if (Variable.RunEnable == true)
                                    {
                                        function.OutYON(122 + model * 32); //下层老化推Tray气缸出
                                        if (Variable.XStatus[141 + model * 32])
                                        {
                                            Thread.Sleep(500);
                                            Variable.ModelGetStep = 45;
                                        }
                                    }
                                    break;
                                }
                            case 45://下层老化推Tray气缸回
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "45-->" + model.ToString() + "下层老化推Tray气缸回");
                                    if (Variable.RunEnable == true)
                                    {
                                        function.OutYOFF(122 + model * 32); //下层老化推Tray气缸回
                                        if (Variable.XStatus[140 + model * 32])
                                        {
                                            Variable.ModelGetStep = 46;
                                        }
                                    }
                                    break;
                                }
                            case 46://告诉机械手可以继续动作
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "46-->" + model.ToString() + "告诉机械手可以继续动作");
                                    if (Variable.RunEnable == true)
                                    {
                                        function.OutYON(19); //告诉机械手可以继续动作
                                        if (!Variable.XStatus[28])
                                        {
                                            Variable.ModelGetStep = 60;
                                        }
                                    }
                                    break;
                                }
                            //case 50:
                            //    {
                            //        RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "42-->" + model.ToString() + "下层模块内取料_告知机械手到Model取料");
                            //        Variable.RobotRecOK = false;
                            //        Variable.RobotSendStr = (143 + model * 4).ToString();//告知机械手到Model取料
                            //        Variable.messageRecord[0] = "机械手" + (model + 1).ToString() + "号模组取料中，代码为:" + (143 + model * 4).ToString();
                            //        Variable.RobotTCPAutoStep = 1000;
                            //        Variable.ModelGetStep = 60;
                            //        break;
                            //    }
                            case 60:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "60-->" + model.ToString() + "下层模块内取料_下层老化机内Tray盘是否取走");
                                    if (Variable.RunEnable == true)
                                    {
                                        function.OutYOFF(19); //告诉机械手可以继续动作
                                        if (Variable.RobotRecOK)
                                        {

                                            Variable.messageRecord[0] = "";
                                            if (!Variable.XStatus[131 + model * 32])
                                            {
                                                Variable.ModelGetStep = 90;
                                            }
                                            else
                                            {
                                                ListBoxTxt((model + 1).ToString() + "#下层老化机内Tray盘未取走");
                                                alarmNum = model;
                                                Variable.ModelGetStep = 67;
                                                RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "67-->" + model.ToString() + "下层模块内取料_下层老化机内Tray盘未取走");
                                            }
                                        }
                                    }
                                    break;
                                }

                            case 90:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "90-->" + model.ToString() + "下层模块内取料_轴16机械手X轴移动到拍照位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)
                                        {
                                            double pos = Variable.AxisPos[16, 1];
                                            Axis16SetMove(pos); //轴16机械手X轴移动到拍照位
                                            if (Variable.AIMpos[16] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[16] >= Math.Round(pos - 0.1, 2))
                                            {
                                                Variable.AxisAlarmTime[16] = 0;
                                                Variable.ModelGetStep = 100;
                                            }
                                            else
                                            {
                                                Variable.AxisAlarmTime[16] += 1;
                                            }
                                        }
                                    }
                                    break;
                                }

                            case 100:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "100-->" + model.ToString() + "下层模块内取料_机械手拍照中，代码为:003");
                                    Variable.RobotRecOK = false;
                                    Variable.RobotSendStr = "003";//告知机械手到上料拍照位置1
                                    Variable.messageRecord[0] = "机械手拍照中，代码为:003";
                                    Variable.RobotTCPAutoStep = 1000;
                                    Variable.ModelGetStep = 110;
                                    break;
                                }

                            case 110:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "110-->" + model.ToString() + "下层模块内取料_机械手拍照位置1OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)//机械手拍照位置1OK
                                        {
                                            Variable.messageRecord[0] = "";
                                            Thread.Sleep(500);
                                            Variable.ModelGetStep = 120;
                                        }
                                    }
                                    break;
                                }
                            case 120:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "120-->" + model.ToString() + "下层模块内取料_判断是否触发相机拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoCheck)
                                        {
                                            Variable.PhotoTCPAutoStep = 101;//触发相机拍照
                                            Variable.ModelGetStep = 130;
                                        }
                                        else
                                        {
                                            Variable.ModelGetStep = 140;
                                        }
                                    }
                                    break;
                                }
                            case 130:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "130-->" + model.ToString() + "下层模块内取料_判断相机拍照是否OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoTCPAutoStep == 0)
                                        {
                                            //while (Variable.PhotoRecMessage.Length < 2)
                                            //{
                                            //    Application.DoEvents();
                                            //    Thread.Sleep(1);
                                            //}
                                            Thread.Sleep((int)Variable.photoDelay);
                                            int rec = DownStringManipulation1(Variable.PhotoRecMessage);

                                            if (rec == 1 && Variable.TestResult)//拍照结果OK
                                            {
                                                Variable.ModelGetStep = 140;
                                            }
                                            else
                                            {
                                                ListBoxTxt("拍照NG，请确认Tray盘");
                                                Variable.ModelGetStep = 135;
                                                RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "135-->" + model.ToString() + "下层模块内取料_拍照NG，请确认Tray盘");
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 136://放弃拍照
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "136-->" + model.ToString() + "下层模块内取料_拍照NG，放弃拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        PhotoDataOK(Variable.PhotoData1);
                                        Variable.ModelGetStep = 140;
                                    }
                                    break;
                                }
                            case 140:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "140-->" + model.ToString() + "下层模块内取料_机械手拍照中，代码为:004");
                                    Variable.RobotRecOK = false;
                                    Variable.RobotSendStr = "004";//告知机械手到上料拍照位置2
                                    Variable.messageRecord[0] = "机械手拍照中，代码为:004";
                                    Variable.RobotTCPAutoStep = 1000;
                                    Variable.ModelGetStep = 150;
                                    break;
                                }

                            case 150:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "150-->" + model.ToString() + "下层模块内取料_机械手拍照位置2OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)//机械手拍照位置2OK
                                        {
                                            Variable.messageRecord[0] = "";
                                            Thread.Sleep(1000);
                                            Variable.ModelGetStep = 160;
                                        }
                                    }
                                    break;
                                }
                            case 160:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "160-->" + model.ToString() + "下层模块内取料_判断是否触发相机拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoCheck)
                                        {
                                            Variable.PhotoTCPAutoStep = 102;//触发相机拍照
                                            Variable.ModelGetStep = 170;
                                        }
                                        else
                                        {
                                            Variable.ModelGetStep = 180;
                                        }
                                    }
                                    break;
                                }
                            case 170:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "170-->" + model.ToString() + "下层模块内取料_判断相机拍照是否OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoTCPAutoStep == 0)
                                        {
                                            //while (Variable.PhotoRecMessage.Length < 2)
                                            //{
                                            //    Application.DoEvents();
                                            //    Thread.Sleep(1);
                                            //}
                                            Thread.Sleep((int)Variable.photoDelay);
                                            int rec = DownStringManipulation2(Variable.PhotoRecMessage);

                                            if (rec == 1 && Variable.TestResult)//拍照结果OK
                                            {
                                                Variable.ModelGetStep = 180;
                                            }
                                            else
                                            {
                                                ListBoxTxt("拍照NG，请确认Tray盘");
                                                Variable.ModelGetStep = 175;
                                                RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "175-->" + model.ToString() + "下层模块内取料_拍照NG，请确认Tray盘");
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 176://放弃拍照
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "176-->" + model.ToString() + "下层模块内取料_拍照NG，放弃拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        PhotoDataOK(Variable.PhotoData2);
                                        Variable.ModelGetStep = 180;
                                    }
                                    break;
                                }
                            case 180:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "180-->" + model.ToString() + "下层模块内取料_相机数据写入TXT");
                                    if (Variable.RunEnable == true)
                                    {
                                        Variable.PhotoData = Variable.PhotoData1.Concat(Variable.PhotoData2).ToArray();
                                        //向TXT写入数据
                                        myTXT.WriteTxt(Variable.PhotoData, Application.StartupPath + @"\Data\Photo\Down\tray");
                                        Thread.Sleep(100);
                                        Variable.ModelGetStep = 185;
                                    }
                                    break;
                                }
                            case 185:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "185-->" + model.ToString() + "下层模块内取料_判断端口是不是NG");
                                    if (Variable.RunEnable == true)
                                    {
                                        JudgeNGCount(model * 4 + 3);//判断端口是不是NG
                                        SaveNGCount(model * 4 + 3);//储存端口是不是NG
                                        Variable.RobotSetFlag = false;
                                        takeNum[model * 4 + 2] = 1;
                                        Variable.ModelGetStep = 190;
                                    }
                                    break;
                                }
                            case 190:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "190-->" + model.ToString() + "下层模块内取料_相机与模组比较");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoCheck)
                                        {
                                            int n = ModelToPhoto((model * 4 + 3).ToString());//相机与模组比较
                                            if (n == 1)
                                            {
                                                Variable.ModelGetStep = 200;
                                            }
                                            else
                                            {
                                                //ListBoxTxt("料盘上缺料，检查是否掉料或模组粘料");
                                                Variable.PhotoAlarm = true;
                                                Thread.Sleep(200);
                                                Variable.TrayNum = (model * 4 + 3).ToString();
                                                Variable.ModelGetStep = 195;
                                                RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "195-->" + model.ToString() + "下层模块内取料_料盘上缺料，检查是否掉料或模组粘料");
                                            }
                                        }
                                        else
                                        {
                                            Variable.ModelGetStep = 200;
                                        }
                                    }
                                    break;
                                }

                            case 200:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "200-->" + model.ToString() + "下层模块内取料_轴16机械手X轴移动到下料位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手X轴移动到下料位
                                        {
                                            double pos = Variable.AxisPos[16, 3];
                                            Axis16SetMove(pos); //轴16机械手X轴移动到下料位
                                            if (Variable.AIMpos[16] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[16] >= Math.Round(pos - 0.1, 2))
                                            {
                                                Variable.AxisAlarmTime[16] = 0;
                                                Variable.ModelGetStep = 210;
                                            }
                                            else
                                            {
                                                Variable.AxisAlarmTime[16] += 1;
                                            }
                                        }
                                    }
                                    break;
                                }

                            case 210:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "210-->" + model.ToString() + "下层模块内取料_机械手放料中，代码为:007");
                                    if (Variable.DownGetTray)//告知机械手放料
                                    {
                                        Variable.RobotRecOK = false;
                                        Variable.RobotSendStr = "007";
                                        Variable.messageRecord[0] = "机械手放料中，代码为:007";
                                        Variable.RobotTCPAutoStep = 1000;
                                        Variable.ModelGetStep = 220;
                                    }
                                    break;
                                }

                            case 220:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "220-->" + model.ToString() + "下层模块内取料_械手放料位OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)
                                        {
                                            Variable.messageRecord[0] = "";
                                            Variable.ModelGetStep = 230;
                                        }
                                    }
                                    break;
                                }
                            case 230:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "230-->" + model.ToString() + "下层模块内取料_读取Map文件传给下料机");
                                    if (Variable.RunEnable == true)
                                    {
                                        string st = @"D:\Map\" + (model * 4 + 3).ToString() + "\\tray";
                                        string[] Readstr = myTXT.ReadTXT1(st);
                                        string[] Readstr1 = new string[152];
                                        if (Readstr.Length == 0)
                                        {
                                            for (int i = 0; i < 152; i++)
                                            {
                                                Readstr1[i] = "20";
                                            }
                                            myTXT.WriteTxt(Readstr1, Application.StartupPath + @"\Map\" + (model * 4 + 3).ToString() + "\\tray");//读取D:\map\1\tray文本到 Map\tray中
                                        }
                                        else
                                        {
                                            myTXT.WriteTxt(Readstr, Application.StartupPath + @"\Map\" + (model * 4 + 3).ToString() + "\\tray");//读取D:\map\1\tray文本到 Map\tray中
                                        }

                                        Thread.Sleep(500);
                                        GetMapToDown((model * 4 + 3).ToString());//读取Map文件传给下料机
                                        Thread.Sleep(500);
                                        ModelResultToDown((model * 4 + 3).ToString());//根据上料模组数据找出空位发送给下料机
                                        DeleteFinishTXT((model * 4 + 3).ToString());//删除finish文档
                                        DeleteTrayTXT((model * 4 + 3).ToString());//删除Tray文档
                                        Variable.RobotDownGetTray = true;

                                        Variable.ModelGetStep = 240;
                                    }
                                    break;
                                }

                            case 240:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "230-->" + model.ToString() + "下层模块内取料_计算产出总数");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手在安全位
                                        {
                                            if (!Variable.DownGetTray)
                                            {
                                                OutNum(model * 4 + 3);//计算产出总数
                                                Variable.RobotGeting = false;
                                                Variable.RobotGet = 0;
                                                Variable.RobotGetStep = 0;
                                                Variable.ModelState[model * 4 + 2] = 0;//模组状态=>0:空
                                                HideTest();//判断是否有屏蔽的机台

                                                Variable.ModelGetStep = 0;
                                            }
                                        }
                                    }
                                    break;
                                }
                        }
                    }
                }

                #endregion

                #region 下层模块外取料

                if (Variable.RobotGetStep > 60 && ((Variable.RobotGetStep - 61) % 4) == 3)//case 61:模块1动作
                {
                    int model = (Variable.RobotGetStep - 64) / 4;//模块号
                    if (Variable.ModelState[model * 4 + 3] == 3 && (Variable.ModelState[model * 4 + 2] == 3 || Variable.ModelState[model * 4 + 2] == 0 || Variable.ModelState[model * 4 + 2] == 10))//内部已经放板子或内部屏蔽
                    {
                        switch (Variable.ModelGetStep)
                        {
                            case 10:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "10-->" + model.ToString() + "下层模块外取料_取料流程标志置位");
                                    if (Variable.RunEnable == true)
                                    {
                                        Variable.messageRecord[0] = "";
                                        Variable.RobotGeting = true;
                                        Variable.ModelGetStep = 20;
                                    }
                                    break;
                                }

                            case 20:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "20-->" + model.ToString() + "下层模块外取料_轴16机械手轴移动到Model1取料");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手在安全位
                                        {
                                            double pos = Variable.AxisPos[16, model + 4];
                                            Axis16SetMove(pos); //轴16机械手轴移动到Model1取料
                                            if (Variable.AIMpos[16] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[16] >= Math.Round(pos - 0.1, 2))
                                            {
                                                Variable.AxisAlarmTime[16] = 0;
                                                Variable.ModelGetStep = 30;
                                            }
                                            else
                                            {
                                                Variable.AxisAlarmTime[16] += 1;
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 30:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "30-->" + model.ToString() + "下层模块外取料_下层外老化测试板断电");
                                    if (Variable.RunEnable == true)
                                    {
                                        function.OutYOFF(126 + model * 32);//下层外老化测试板断电
                                        Thread.Sleep(100);
                                        Variable.ModelGetStep = 40;
                                    }
                                    break;
                                }
                            case 40:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "40-->" + model.ToString() + "下层模块外取料_下层外老化上下气缸下降");
                                    if (Variable.RunEnable == true)
                                    {
                                        function.OutYOFF(120 + model * 32); ;//下层外老化上下气缸上升
                                        function.OutYON(119 + model * 32); ;//下层外老化上下气缸下降
                                        Thread.Sleep(100);
                                        Variable.ProbeNum[model * 4 + 3] += 1;
                                        ProbRefresh();
                                        Variable.ModelGetStep = 50;
                                    }
                                    break;
                                }
                            case 50:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "50-->" + model.ToString() + "下层模块外取料_告知机械手到Model取料");
                                    if (Variable.XStatus[136 + model * 32])
                                    {
                                        Variable.RobotRecOK = false;
                                        Variable.RobotSendStr = (144 + model * 4).ToString();//告知机械手到Model取料
                                        Variable.messageRecord[0] = "机械手" + (model + 1).ToString() + "号模组取料中，代码为:" + (144 + model * 4).ToString();
                                        Variable.RobotTCPAutoStep = 1000;
                                        Variable.ModelGetStep = 60;
                                    }
                                    break;
                                }

                            case 60:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "60-->" + model.ToString() + "下层模块外取料_下层老化机外Tray盘是否取走");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)
                                        {
                                            Variable.messageRecord[0] = "";
                                            if (!Variable.XStatus[135 + model * 32])
                                            {
                                                Variable.ModelGetStep = 90;
                                            }
                                            else
                                            {
                                                ListBoxTxt((model + 1).ToString() + "#下层老化机外Tray盘未取走");
                                                alarmNum = model;
                                                Variable.ModelGetStep = 68;
                                                RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "68-->" + model.ToString() + "下层模块外取料_下层老化机外Tray盘未取走");
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 90:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "90-->" + model.ToString() + "下层模块外取料_轴16机械手X轴移动到拍照位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)
                                        {
                                            double pos = Variable.AxisPos[16, 1];
                                            Axis16SetMove(pos); //轴16机械手X轴移动到拍照位
                                            if (Variable.AIMpos[16] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[16] >= Math.Round(pos - 0.1, 2))
                                            {
                                                Variable.AxisAlarmTime[16] = 0;
                                                Variable.ModelGetStep = 100;
                                            }
                                            else
                                            {
                                                Variable.AxisAlarmTime[16] += 1;
                                            }
                                        }
                                    }
                                    break;
                                }

                            case 100:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "100-->" + model.ToString() + "下层模块外取料_机械手拍照中，代码为:003");
                                    Variable.RobotRecOK = false;
                                    Variable.RobotSendStr = "003";//告知机械手到上料拍照位置1
                                    Variable.messageRecord[0] = "机械手拍照中，代码为:003";
                                    Variable.RobotTCPAutoStep = 1000;
                                    Variable.ModelGetStep = 110;
                                    break;
                                }

                            case 110:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "110-->" + model.ToString() + "下层模块外取料_机械手拍照位置1OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)//机械手拍照位置1OK
                                        {
                                            Variable.messageRecord[0] = "";
                                            Thread.Sleep(500);
                                            Variable.ModelGetStep = 120;
                                        }
                                    }
                                    break;
                                }
                            case 120:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "120-->" + model.ToString() + "下层模块外取料_判断是否触发相机拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoCheck)
                                        {
                                            Variable.PhotoTCPAutoStep = 101;//触发相机拍照
                                            Variable.ModelGetStep = 130;
                                        }
                                        else
                                        {
                                            Variable.ModelGetStep = 140;
                                        }
                                    }
                                    break;
                                }
                            case 130:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "130-->" + model.ToString() + "下层模块外取料_判断相机拍照是否OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoTCPAutoStep == 0)
                                        {
                                            //while (Variable.PhotoRecMessage.Length < 2)
                                            //{
                                            //    Application.DoEvents();
                                            //    Thread.Sleep(1);
                                            //}
                                            Thread.Sleep((int)Variable.photoDelay);
                                            int rec = DownStringManipulation1(Variable.PhotoRecMessage);

                                            if (rec == 1 && Variable.TestResult)//拍照结果OK
                                            {
                                                Variable.ModelGetStep = 140;
                                            }
                                            else
                                            {
                                                ListBoxTxt("拍照NG，请确认Tray盘");
                                                Variable.ModelGetStep = 135;
                                                RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "135-->" + model.ToString() + "下层模块外取料_拍照NG，请确认Tray盘");
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 136://放弃拍照
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "136-->" + model.ToString() + "下层模块外取料_拍照NG，放弃拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        PhotoDataOK(Variable.PhotoData1);
                                        Variable.ModelGetStep = 140;
                                    }
                                    break;
                                }
                            case 140:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "140-->" + model.ToString() + "下层模块外取料_机械手拍照中，代码为:004");
                                    Variable.RobotRecOK = false;
                                    Variable.RobotSendStr = "004";//告知机械手到上料拍照位置2
                                    Variable.messageRecord[0] = "机械手拍照中，代码为:004";
                                    Variable.RobotTCPAutoStep = 1000;
                                    Variable.ModelGetStep = 150;
                                    break;
                                }

                            case 150:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "150-->" + model.ToString() + "下层模块外取料_机械手拍照位置2OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)//机械手拍照位置2OK
                                        {
                                            Variable.messageRecord[0] = "";
                                            Thread.Sleep(1000);
                                            Variable.ModelGetStep = 160;
                                        }
                                    }
                                    break;
                                }
                            case 160:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "160-->" + model.ToString() + "下层模块外取料_判断是否触发相机拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoCheck)
                                        {
                                            Variable.PhotoTCPAutoStep = 102;//触发相机拍照
                                            Variable.ModelGetStep = 170;
                                        }
                                        else
                                        {
                                            Variable.ModelGetStep = 180;
                                        }
                                    }
                                    break;
                                }
                            case 170:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "170-->" + model.ToString() + "下层模块外取料_判断相机拍照是否OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoTCPAutoStep == 0)
                                        {
                                            //while (Variable.PhotoRecMessage.Length < 2)
                                            //{
                                            //    Application.DoEvents();
                                            //    Thread.Sleep(1);
                                            //}
                                            Thread.Sleep((int)Variable.photoDelay);
                                            int rec = DownStringManipulation2(Variable.PhotoRecMessage);

                                            if (rec == 1 && Variable.TestResult)//拍照结果OK
                                            {
                                                Variable.ModelGetStep = 180;
                                            }
                                            else
                                            {
                                                ListBoxTxt("拍照NG，请确认Tray盘");
                                                Variable.ModelGetStep = 175;
                                                RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "175-->" + model.ToString() + "下层模块外取料_拍照NG，请确认Tray盘");
                                            }
                                        }
                                    }
                                    break;
                                }
                            case 176://放弃拍照
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "176-->" + model.ToString() + "下层模块外取料_拍照NG，放弃拍照");
                                    if (Variable.RunEnable == true)
                                    {
                                        PhotoDataOK(Variable.PhotoData2);
                                        Variable.ModelGetStep = 180;
                                    }
                                    break;
                                }
                            case 180:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "180-->" + model.ToString() + "下层模块外取料_相机数据写入TXT");
                                    if (Variable.RunEnable == true)
                                    {
                                        Variable.PhotoData = Variable.PhotoData1.Concat(Variable.PhotoData2).ToArray();
                                        //向TXT写入数据
                                        myTXT.WriteTxt(Variable.PhotoData, Application.StartupPath + @"\Data\Photo\Down\tray");
                                        Thread.Sleep(100);
                                        Variable.ModelGetStep = 185;
                                    }
                                    break;
                                }
                            case 185:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "185-->" + model.ToString() + "下层模块外取料_判断端口是不是NG");
                                    if (Variable.RunEnable == true)
                                    {
                                        JudgeNGCount(model * 4 + 4);//判断端口是不是NG
                                        SaveNGCount(model * 4 + 4);//储存端口是不是NG
                                        Variable.RobotSetFlag = false;
                                        takeNum[model * 4 + 3] = 1;
                                        Variable.ModelGetStep = 190;
                                    }
                                    break;
                                }
                            case 190:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "190-->" + model.ToString() + "下层模块外取料_相机与模组比较");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.PhotoCheck)
                                        {
                                            int n = ModelToPhoto((model * 4 + 4).ToString());//相机与模组比较
                                            if (n == 1)
                                            {
                                                Variable.ModelGetStep = 200;
                                            }
                                            else
                                            {
                                                //ListBoxTxt("料盘上缺料，检查是否掉料或模组粘料");
                                                Variable.PhotoAlarm = true;
                                                Thread.Sleep(200);
                                                Variable.TrayNum = (model * 4 + 4).ToString();
                                                Variable.ModelGetStep = 195;
                                                RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "195-->" + model.ToString() + "下层模块外取料_料盘上缺料，检查是否掉料或模组粘料");
                                            }
                                        }
                                        else
                                        {
                                            Variable.ModelGetStep = 200;
                                        }
                                    }
                                    break;
                                }

                            case 200:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "200-->" + model.ToString() + "下层模块外取料_轴16机械手X轴移动到下料位");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手X轴移动到下料位
                                        {
                                            double pos = Variable.AxisPos[16, 3];
                                            Axis16SetMove(pos); //轴8机械手X轴移动到下料位
                                            if (Variable.AIMpos[16] <= Math.Round(pos + 0.1, 2) && Variable.AIMpos[16] >= Math.Round(pos - 0.1, 2))
                                            {
                                                Variable.AxisAlarmTime[16] = 0;
                                                Variable.ModelGetStep = 210;
                                            }
                                            else
                                            {
                                                Variable.AxisAlarmTime[16] += 1;
                                            }
                                        }
                                    }
                                    break;
                                }

                            case 210:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "210-->" + model.ToString() + "下层模块外取料_机械手放料中，代码为:007");
                                    if (Variable.DownGetTray)//告知机械手放料
                                    {
                                        Variable.RobotRecOK = false;
                                        Variable.RobotSendStr = "007";
                                        Variable.messageRecord[0] = "机械手放料中，代码为:007";
                                        Variable.RobotTCPAutoStep = 1000;
                                        Variable.ModelGetStep = 220;
                                    }
                                    break;
                                }

                            case 220:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "220-->" + model.ToString() + "下层模块外取料_械手放料位OK");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotRecOK)
                                        {
                                            Variable.messageRecord[0] = "";
                                            Variable.ModelGetStep = 230;
                                        }
                                    }
                                    break;
                                }
                            case 230:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "230-->" + model.ToString() + "下层模块外取料_读取Map文件传给下料机");
                                    if (Variable.RunEnable == true)
                                    {
                                        string st = @"D:\Map\" + (model * 4 + 4).ToString() + "\\tray";
                                        string[] Readstr = myTXT.ReadTXT1(st);
                                        string[] Readstr1 = new string[152];
                                        if (Readstr.Length == 0)
                                        {
                                            for (int i = 0; i < 152; i++)
                                            {
                                                Readstr1[i] = "20";
                                            }
                                            myTXT.WriteTxt(Readstr1, Application.StartupPath + @"\Map\" + (model * 4 + 4).ToString() + "\\tray");//读取D:\map\1\tray文本到 Map\tray中
                                        }
                                        else
                                        {
                                            myTXT.WriteTxt(Readstr, Application.StartupPath + @"\Map\" + (model * 4 + 4).ToString() + "\\tray");//读取D:\map\1\tray文本到 Map\tray中
                                        }

                                        Thread.Sleep(500);
                                        GetMapToDown((model * 4 + 4).ToString());//读取Map文件传给下料机
                                        Thread.Sleep(500);
                                        ModelResultToDown((model * 4 + 4).ToString());//根据上料模组数据找出空位发送给下料机
                                        DeleteFinishTXT((model * 4 + 4).ToString());//删除finish文档
                                        DeleteTrayTXT((model * 4 + 4).ToString());//删除Tray文档
                                        Variable.RobotDownGetTray = true;

                                        Variable.ModelGetStep = 240;
                                    }
                                    break;
                                }

                            case 240:
                                {
                                    RSDAlarmRobotAutoTakeStep(Variable.AutoStepMsg[9] = "230-->" + model.ToString() + "下层模块外取料_计算产出总数");
                                    if (Variable.RunEnable == true)
                                    {
                                        if (Variable.RobotSafePoint)//机械手在安全位
                                        {
                                            if (!Variable.DownGetTray)
                                            {
                                                OutNum(model * 4 + 4);//计算产出总数
                                                Variable.RobotGeting = false;
                                                Variable.RobotGet = 0;
                                                Variable.RobotGetStep = 0;
                                                Variable.ModelState[model * 4 + 3] = 0;//模组状态=>0:空
                                                HideTest();//判断是否有屏蔽的机台

                                                Variable.ModelGetStep = 0;
                                            }
                                        }
                                    }
                                    break;
                                }
                        }
                    }
                }

                #endregion

                Thread.Sleep(1);
            }
        }
        #endregion

        #region TCP通讯流程
        public void TcpServerStart()
        {
            while (true)
            {
                #region Robot回原点

                //Robot回原点
                switch (Variable.RobotHomeStartStep)
                {
                    case 999:
                        {
                            Variable.RobotRecMessage = "";
                            RobotTcpServer.MessageSend(Variable.RobotSendStr);

                            Variable.RobotRecOK = false;
                            Variable.RobotResetNG = false;
                            Thread.Sleep(100);
                            Variable.RobotHomeStartStep = 201;
                            break;
                        }

                    case 201:
                        {
                            if (Variable.RobotRecMessage == "busy" + Variable.RobotSendStr)
                            {
                                Variable.RobotHomeStartStep = 202;
                            }
                            if (Variable.RobotRecMessage == "end" + Variable.RobotSendStr)
                            {
                                Variable.RobotSendStr = "";
                                Variable.RobotHomeStartStep = 203;
                            }
                            if (Variable.RobotRecMessage == "end" + "998")
                            {
                                Variable.RobotSendStr = "";
                                Variable.RobotHomeStartStep = 204;
                            }
                            break;
                        }

                    case 202:
                        {
                            if (Variable.RobotRecMessage == "end" + Variable.RobotSendStr)
                            {
                                Variable.RobotSendStr = "";
                                Variable.RobotHomeStartStep = 203;
                            }

                            if (Variable.RobotRecMessage == "end" + "998")
                            {
                                Variable.RobotSendStr = "";
                                Variable.RobotHomeStartStep = 204;
                            }
                            break;
                        }


                    case 203:
                        {
                            Variable.RobotRecOK = true;
                            Variable.RobotHomeStartStep = 0;
                            break;
                        }

                    case 204:
                        {
                            Variable.RobotRecOK = true;
                            Variable.RobotResetNG = true;
                            Variable.RobotHomeStartStep = 0;
                            break;
                        }

                }

                #endregion

                #region Robot自动流程

                //Robot自动流程
                switch (Variable.RobotTCPAutoStep)
                {
                    case 1000:
                        {
                            Variable.RobotRecMessage = "";
                            Variable.RobotRecOK = false;
                            Variable.RobotTCPAutoStep = 1100;
                            break;
                        }

                    case 1100:
                        {
                            RobotTcpServer.MessageSend(Variable.RobotSendStr);
                            Textadd(textBox1, Variable.RobotSendStr);
                            Variable.RobotTCPAutoStep = 101;
                            break;
                        }

                    case 101:
                        {
                            if (Variable.RobotRecMessage == "busy" + Variable.RobotSendStr)
                            {
                                Variable.RobotTCPAutoStep = 102;
                            }
                            if (Variable.RobotRecMessage == "end" + Variable.RobotSendStr)
                            {
                                Variable.RobotSendStr = "";
                                Variable.RobotTCPAutoStep = 103;
                            }
                            break;
                        }

                    case 102:
                        {
                            if (Variable.RobotRecMessage == "end" + Variable.RobotSendStr)
                            {
                                Variable.RobotSendStr = "";
                                Variable.RobotTCPAutoStep = 103;
                            }
                            break;
                        }

                    case 103:
                        {
                            Variable.RobotRecOK = true;
                            Variable.RobotTCPAutoStep = 0;
                            break;
                        }
                }

                #endregion

                #region Photo自动流程

                //Photo自动流程
                switch (Variable.PhotoTCPAutoStep)
                {
                    case 101:
                        {
                            Variable.TestResult = false;
                            Variable.PhotoRecMessage = "";
                            Thread.Sleep(100);
                            PhotoTcpServer.MessageSend(Variable.TakePhoto1);//触发相机拍照
                            Variable.PhotoTCPAutoStep = 103;
                            break;
                        }

                    case 102:
                        {
                            Variable.TestResult = false;
                            Variable.PhotoRecMessage = "";
                            Thread.Sleep(100);
                            PhotoTcpServer.MessageSend(Variable.TakePhoto2);//触发相机拍照
                            Variable.PhotoTCPAutoStep = 103;
                            break;
                        }

                    case 103:
                        {
                            Variable.PhotoTCPAutoStep = 0;
                            break;
                        }

                }

                #endregion

                #region QR

                //QR自动流程
                switch (Variable.QRTCPAutoStep)
                {
                    case 101:
                        {
                            Variable.TestResult = false;
                            Variable.QRRecMessage = "";
                            Thread.Sleep(100);
                            QRTcpClient.MessageSend(Variable.TakeQR);//触发相机拍照
                            Variable.QRTCPAutoStep = 103;
                            break;
                        }

                    case 102:
                        {
                            Variable.TestResult = false;
                            Variable.QRRecMessage = "";
                            Thread.Sleep(100);
                            QRTcpClient.MessageSend(Variable.TakeQR);//触发相机拍照
                            Variable.QRTCPAutoStep = 103;
                            break;
                        }

                    case 103:
                        {
                            Variable.QRTCPAutoStep = 0;
                            break;
                        }
                }

                #endregion

                Thread.Sleep(1);
            }
        }
        #endregion

        #endregion

        #region **********刷新线程**********
        public void RefreshT()
        {
            while (true)
            {
                //this.Invoke(new Action(() => {  }));

                if (Variable.Num != "" && Variable.Num != null)
                {
                    myTXT.Num(Variable.Num);//计算总数
                }

                #region 生产数据UI显示

                //生产信息
                infoDataGrid.Rows[0].Cells[1].Value = Variable.OP;//操作员
                infoDataGrid.Rows[1].Cells[1].Value = Variable.BatchNum;//批号
                infoDataGrid.Rows[2].Cells[1].Value = Variable.OrderNum;//工单号
                infoDataGrid.Rows[3].Cells[1].Value = Variable.PONum;//PO号  
                infoDataGrid.Rows[4].Cells[1].Value = Variable.startTime;//批开始时间
                infoDataGrid.Rows[5].Cells[1].Value = Variable.runTime;//运行时间
                infoDataGrid.Rows[6].Cells[1].Value = Variable.stopTime;//停止时间
                infoDataGrid.Rows[7].Cells[1].Value = Variable.alarmTime;//加热时间

                infoDataGrid.Rows[0].Cells[3].Value = Variable.inChipNum;//投入总数
                infoDataGrid.Rows[1].Cells[3].Value = Variable.outChipNum;//产出总数
                infoDataGrid.Rows[2].Cells[3].Value = Variable.OKChipNum;//良品数
                infoDataGrid.Rows[3].Cells[3].Value = Variable.Yield.ToString("0.00") + "%";//总良率
                infoDataGrid.Rows[4].Cells[3].Value = Variable.UPH;//UPH
                infoDataGrid.Rows[5].Cells[3].Value = Variable.jamRate;//报警率(M/N)
                infoDataGrid.Rows[6].Cells[3].Value = Variable.inTrayNum;//入盘数
                infoDataGrid.Rows[7].Cells[3].Value = Variable.outTrayNum;//出盘数

                #endregion

                #region Tray信息UI显示

                //LeftProductNum.Text = Variable.LeftProductNum.ToString();
                //LeftTrayNum.Text = Variable.LeftTrayNum.ToString();
                //BIBBordProductNum.Text = Variable.BIBBordProductNum.ToString();
                //BIBBordNum.Text = Variable.BIBBordNum.ToString();
                //RightProductNum.Text = Variable.RightProductNum.ToString();
                //RightTrayNum.Text = Variable.RightTrayNum.ToString();

                //BINAProductNum.Text = Variable.BinAreaNum[0].ToString();
                //BINATrayNum.Text = Variable.BinAreaTrayNum[0].ToString();
                //BINBProductNum.Text = Variable.BinAreaNum[1].ToString();
                //BINBTrayNum.Text = Variable.BinAreaTrayNum[1].ToString();
                //BINCProductNum.Text = Variable.BinAreaNum[2].ToString();
                //BINCTrayNum.Text = Variable.BinAreaTrayNum[2].ToString();
                //BINDProductNum.Text = Variable.BinAreaNum[3].ToString();
                //BINDTrayNum.Text = Variable.BinAreaTrayNum[3].ToString();

                #endregion

                #region 数量统计

                //良率
                if (Variable.OKNum == 0 && Variable.TotalNum == 0)
                {
                    Variable.Yield = 0;
                }
                else
                {
                    Variable.Yield = Math.Round((Variable.OKNum / Variable.TotalNum) * 100, 2); //计算良率
                }
                Variable.jamRate = Variable.alarmCount.ToString() + "/" + Variable.inChipNum.ToString();

                #endregion

                #region 阵列初始化

                if (Variable.saveFlag || Variable.modeNameFlag)
                {
                    //加载参数
                    parameterForm.LoadPoint(Application.StartupPath + "\\Point.ini");
                    parameterForm.LoadParameter(Application.StartupPath + "\\parameter.ini");

                    //计算阵列
                    ArrayCount();
                    Variable.saveFlag = false;
                    Variable.modeNameFlag = false;

                    HotOpen();
                }

                #endregion

                #region 结批完显示
                if (Variable.OutAutoOKStartStep == 1000 && Variable.OutAutoFillStartStep == 10 && Variable.OutAutoNGStartStep == 500 && Variable.INAutoEmptyStartStep == 500 && Variable.INAutoReadyStep == 800 && (Variable.CleanOut || Variable.CleanOne))
                {
                    //结束时间
                    Variable.endTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");//开始时间
                    myTXT.RecordDataToTxt();//输出报表
                    //断电保存参数方法
                    SaveCurrenParameter();
                    HotClose();  //加热关闭
                    Variable.RunEnable = false;
                    Variable.CleanOut = false;
                    Variable.CleanOne = false;

                    Variable.MachineState = Variable.MachineStatus.Alarm;
                    Variable.CleanOutFlag = true;
                    ListBoxTxt("结批完成，请复位重新开始");
                    //MessageBox.Show("结批完成，请确认");
                }
                #endregion

                #region 轴速度设定
                //速度设定
                Variable.AxisRealRunVel[1] = Variable.SpeedCom / 100 * Variable.AxisRunVel[1];
                Variable.AxisRealRunVel[2] = Variable.SpeedCom / 100 * Variable.AxisRunVel[2];
                Variable.AxisRealRunVel[3] = Variable.SpeedCom / 100 * Variable.AxisRunVel[3];
                Variable.AxisRealRunVel[4] = Variable.SpeedCom / 100 * Variable.AxisRunVel[4];
                Variable.AxisRealRunVel[5] = Variable.SpeedCom / 100 * Variable.AxisRunVel[5];
                Variable.AxisRealRunVel[6] = Variable.SpeedCom / 100 * Variable.AxisRunVel[6];
                Variable.AxisRealRunVel[7] = Variable.SpeedCom / 100 * Variable.AxisRunVel[7];
                Variable.AxisRealRunVel[8] = Variable.SpeedCom / 100 * Variable.AxisRunVel[8];
                Variable.AxisRealRunVel[9] = Variable.SpeedCom / 100 * Variable.AxisRunVel[9];
                Variable.AxisRealRunVel[10] = Variable.SpeedCom / 100 * Variable.AxisRunVel[10];
                Variable.AxisRealRunVel[11] = Variable.SpeedCom / 100 * Variable.AxisRunVel[11];
                Variable.AxisRealRunVel[12] = Variable.SpeedCom / 100 * Variable.AxisRunVel[12];
                Variable.AxisRealRunVel[13] = Variable.SpeedCom / 100 * Variable.AxisRunVel[13];
                Variable.AxisRealRunVel[14] = Variable.SpeedCom / 100 * Variable.AxisRunVel[14];
                Variable.AxisRealRunVel[15] = Variable.SpeedCom / 100 * Variable.AxisRunVel[15];
                Variable.AxisRealRunVel[16] = Variable.SpeedCom / 100 * Variable.AxisRunVel[16];

                #endregion

                #region 暂停轴
                if (Variable.MachineState == Variable.MachineStatus.Pause && !Variable.ManualViewFlag)
                {
                    motion.AxisStopAll();
                }

                #endregion

                #region 档案

                //档案加载
                if (Variable.parameterSaveFlag)
                {
                    //ModelCombo.Items.Clear();
                    string path = @"D:\参数\";
                    string[] files = Directory.GetFiles(path, "*.bin");
                    foreach (string file in files)
                    {
                        string[] split = file.Split(new Char[] { '\\' });
                        if (!ModelCombo.Items.Contains(split[split.Length - 1]))
                        {
                            ModelCombo.Items.Add(split[split.Length - 1]);
                        }
                    }
                    Variable.parameterSaveFlag = false;
                }

                if (Variable.PGMcheck)
                {
                    PGMname.Text = Variable.ModelName;
                }

                //档案名称刷新
                if (Variable.LoadparameterFlag)
                {
                    ModelCombo.SelectedItem = Variable.FileName;
                    Variable.LoadparameterFlag = false;
                }

                #endregion

                #region 机械手暂停

                if (Variable.MachineState == Variable.MachineStatus.Alarm || Variable.MachineState == Variable.MachineStatus.Pause || stopRobotFlag)
                {
                    RobotPause = false;
                    function.OutYON(17);//机械手暂停
                    Thread.Sleep(500);
                    function.OutYOFF(17);
                    RobotPause = true;
                    stopRobotFlag = false;
                    ListBoxTxt("机械手已暂停");
                }

                if (Variable.MachineState == Variable.MachineStatus.Running && RobotPause)
                {
                    function.OutYON(13);//机械手上电
                    Thread.Sleep(500);
                    function.OutYOFF(13);
                    function.OutYON(18);//机械手启动
                    Thread.Sleep(1000);
                    function.OutYOFF(18);
                    RobotPause = false;

                    if (!RobotPause)
                    {
                        this.listAlarm.Items.Clear();
                    }
                }

                #endregion

                #region 机械手报警

                //机械手报警
                if (Variable.RobotAlarm)
                {
                    Thread.Sleep(500);
                    function.OutYON(17);//机械手暂停
                    Thread.Sleep(500);
                    function.OutYOFF(17);
                    Variable.RobotRecMessage = "";
                }

                //机械手安全位
                if (Variable.XStatus[24])
                {
                    Variable.RobotSafePoint = true;
                }
                else
                {
                    Variable.RobotSafePoint = true;
                    //Variable.RobotSafePoint = false;
                }

                #region 机械手报警复位

                //报警复位
                if ((Variable.AlarmClrButton == true || Variable.btnReset == true) && Variable.RobotAlarm)//复位按钮
                {
                    Variable.RobotAlarm = false;
                    RobotAlarmFlag = true;

                    function.RestLampOff();
                    Variable.MachineState = Variable.MachineStatus.Reset;
                    function.StopLampOn();
                }
                if ((Variable.StartButton == true || Variable.btnStart == true) && Variable.RobotAlarm == false && RobotAlarmFlag == true)
                {
                    function.OutYON(18);//机械手启动
                    Thread.Sleep(500);
                    function.OutYOFF(18);
                    RobotAlarmFlag = false;
                }



                #endregion

                //机械手碰撞报警
                if (Variable.XStatus[21] || (!Variable.XStatus[23] && Variable.RunEnable))
                {
                    Thread.Sleep(2000);
                    if (Variable.XStatus[23])
                    {

                    }
                    else
                    {
                        ListBoxTxt("机械手碰撞报警");
                        Variable.RobotAlarm = true;
                    }

                }
                //else
                //{
                //    Variable.Robot5Alarm.Happen = false;
                //}

                #endregion

                #region 判断温度是否到达
                for (int i = 0; i < 10; i++)
                {
                    if (!Variable.XStatus[116 + i * 32])
                    {
                        Variable.tempOK[0 + i * 2] = true;
                    }
                    else
                    {
                        Variable.tempOK[0 + i * 2] = false;
                    }

                    if (!Variable.XStatus[120 + i * 32])
                    {
                        Variable.tempOK[1 + i * 2] = true;
                    }
                    else
                    {
                        Variable.tempOK[1 + i * 2] = false;
                    }

                    if (!Variable.XStatus[130 + i * 32])
                    {
                        Variable.tempOK[2 + i * 2] = true;
                    }
                    else
                    {
                        Variable.tempOK[2 + i * 2] = false;
                    }

                    if (!Variable.XStatus[134 + i * 32])
                    {
                        Variable.tempOK[3 + i * 2] = true;
                    }
                    else
                    {
                        Variable.tempOK[3 + i * 2] = false;
                    }
                }
                #endregion

                #region 测试端口屏蔽

                //端口屏蔽选择
                if (!Variable.siteShieldCheck)
                {
                    Variable.siteNGSet = 100000;
                }
                #endregion

                #region 测试机台返回TestOK

                foreach (Control c in groupBox13.Controls)
                {
                    if (c.Name.Contains("pictureBox"))
                    {
                        int index = int.Parse(c.Name.Substring((c.Name.Length - 2), 2));
                        if (Variable.PicBox[index - 1])
                        {
                            c.BackColor = Color.Green;
                        }
                        else
                        {
                            c.BackColor = Color.Red;
                        }
                    }
                }

                #endregion

                #region 权限

                if (Variable.userEnter == Variable.UserEnter.Manufacturer || Variable.userEnter == Variable.UserEnter.Administrator)
                {
                    SubPanel1.Enabled = true;
                }
                else
                {
                    SubPanel1.Enabled = false;
                }

                #endregion

                #region 显示相机，加热状态

                if (Variable.PhotoCheck)
                {
                    labelPhoto.BackColor = Color.Green;
                    labelPhoto.Text = "启用";
                }
                else
                {
                    labelPhoto.BackColor = Color.FromArgb(255, 240, 240, 240);
                    labelPhoto.Text = "屏蔽";
                }

                if (Variable.HotModel)
                {
                    labelHight.BackColor = Color.Green;
                    labelHight.Text = "启用";
                }
                else
                {
                    labelHight.BackColor = Color.FromArgb(255, 240, 240, 240);
                    labelHight.Text = "屏蔽";
                }
                #endregion

                //刷新UIDataView
                ReadTxtToDataGrid();


                //if (Variable.inTrayNumRecord >= Convert.ToInt32(Variable.inTrayNumSet) && Convert.ToInt32(Variable.inTrayNumSet)!=0 && Variable.MachineState == Variable.MachineStatus.Running && !Variable.info)
                //{
                //    Variable.info = true;
                //    inform.ShowDialog();
                //}

                Thread.Sleep(1);
            }
        }

        #endregion

        #region **********报警线程**********

        #region 报警内容
        private void AlarmScan1_Tick(object sender, EventArgs e)
        {
            #region 离子风扇报警
            //if (Variable.ionFanCheck)
            //{
            //    for (int i = 0; i < 6; i++)
            //    {
            //        if (!Variable.XStatus[128 + i])
            //        {
            //            Variable.AlarmHappen[128 + i] = true;
            //            Down("X0", LogType.Alarm, "X" + (128 + i).ToString() + "离子风扇" + (i + 1).ToString() + "报警!", "", 0, 0);
            //        }
            //    }
            //}
            #endregion

            #region 门屏蔽与否

            //上料
            if (Variable.UpDoorCheck)
            {
                //上料前门未关
                if (!Variable.XStatus[6] && (Variable.RunEnable || Variable.MachineState == Variable.MachineStatus.zero))
                {
                    Variable.AlarmHappen[6] = true;
                    ListBoxTxt("X6-上料前门被打开，请关门!");
                    Down("X6", LogType.Alarm1, "X6-上料前门被打开，请关门!", "", 0, 0);
                }


                //上料后门未关
                if (!Variable.XStatus[7] && (Variable.RunEnable || Variable.MachineState == Variable.MachineStatus.zero))
                {
                    Variable.AlarmHappen[7] = true;
                    ListBoxTxt("X7-上料后门被打开，请关门!");
                    Down("X7", LogType.Alarm1, "X7-上料后门被打开，请关门!", "", 0, 0);
                }

            }

            //下料
            if (Variable.DownDoorCheck)
            {
                //光栅
                if (Variable.XStatus[110] && (Variable.RunEnable || Variable.MachineState == Variable.MachineStatus.zero))
                {
                    Variable.AlarmHappen[110] = true;
                    ListBoxTxt("X110-光栅被遮住，请勿遮挡!");
                    Down("X0", LogType.Alarm1, "X110-光栅被遮住，请勿遮挡!", "", 0, 0);
                }

                //下料前门未关
                if (!Variable.XStatus[106] && (Variable.RunEnable || Variable.MachineState == Variable.MachineStatus.zero))
                {
                    Variable.AlarmHappen[106] = true;
                    ListBoxTxt("X106-下料前门被打开，请关门!");
                    Down("X106", LogType.Alarm1, "X106-下料前门被打开，请关门!", "", 0, 0);
                }

                //下料后门未关
                if (!Variable.XStatus[107] && (Variable.RunEnable || Variable.MachineState == Variable.MachineStatus.zero))
                {
                    Variable.AlarmHappen[107] = true;
                    ListBoxTxt("X107-下料后门被打开，请关门!");
                    Down("X107", LogType.Alarm1, "X107-下料后门被打开，请关门!", "", 0, 0);
                }

            }

            //机械手门
            //机械手上料前门禁
            if (!Variable.XStatus[8] && (Variable.RunEnable || Variable.MachineState == Variable.MachineStatus.zero))
            {
                Variable.AlarmHappen[8] = true;
                ListBoxTxt("X8-机械手上料前门被打开，请关门!");
                Down("X8", LogType.Alarm1, "X8-机械手上料前门被打开，请关门!", "", 0, 0);
            }

            //机械手上料后门禁
            if (!Variable.XStatus[9] && (Variable.RunEnable || Variable.MachineState == Variable.MachineStatus.zero))
            {
                Variable.AlarmHappen[9] = true;
                ListBoxTxt("X9-机械手上料后门被打开，请关门!");
                Down("X9", LogType.Alarm1, "X9-机械手上料后门被打开，请关门!", "", 0, 0);
            }

            //机械手下料前门禁
            if (!Variable.XStatus[108] && (Variable.RunEnable || Variable.MachineState == Variable.MachineStatus.zero))
            {
                Variable.AlarmHappen[108] = true;
                ListBoxTxt("X108-机械手下料前门被打开，请关门!");
                Down("X108", LogType.Alarm1, "X108-机械手下料前门被打开，请关门!", "", 0, 0);
            }

            //机械手下料后门禁
            if (!Variable.XStatus[109] && (Variable.RunEnable || Variable.MachineState == Variable.MachineStatus.zero))
            {
                Variable.AlarmHappen[109] = true;
                ListBoxTxt("X109-机械手下料后门被打开，请关门!");
                Down("X109", LogType.Alarm1, "X109-机械手下料后门被打开，请关门!", "", 0, 0);
            }

            //老化柜门未关
            if (Variable.ModelDoorCheck)
            {
                for (int i = 0; i < 10; i++)
                {
                    //老化柜门
                    if (!Variable.XStatus[112 + i * 32] && (Variable.RunEnable || Variable.MachineState == Variable.MachineStatus.zero))
                    {
                        Variable.AlarmHappen[112 + i * 32] = true;
                        ListBoxTxt((i + 1).ToString() + "#老化柜门被打开，请关门!");
                        Down("X112", LogType.Alarm1, (i + 1).ToString() + "#老化柜门被打开，请关门!", "", 0, 0);
                    }

                    //光栅
                    if (!Variable.XStatus[113 + i * 32] && (Variable.RunEnable || Variable.MachineState == Variable.MachineStatus.zero))
                    {
                        Variable.AlarmHappen[113 + i * 32] = true;
                        ListBoxTxt((i + 1).ToString() + "#老化柜光栅被遮住，请勿遮挡!");
                        Down("X113", LogType.Alarm1, (i + 1).ToString() + "#老化柜光栅被遮住，请勿遮挡!", "", 0, 0);
                    }
                }
            }

            #endregion

            #region 伺服报警

            for (int i = 0; i < 16; i++)
            {
                //伺服驱动器报警
                if (Variable.MachineState == Variable.MachineStatus.Running)
                {
                    if (Variable.ServoAlarmHappen[i + 1])
                    {
                        ListBoxTxt("轴" + (i + 1).ToString() + "伺服控制器报警!，请先单轴归零回待机位，再运行！");
                        Down("轴" + (i + 1).ToString(), LogType.Alarm, "轴" + (i + 1).ToString() + "伺服控制器报警!，请先单轴归零回待机位，再运行！", "", 0, 0);
                    }
                }

                if (i < 13)
                {
                    if (IntToBin(Variable.Alarm1, i) == "1")
                    {
                        Variable.ServoAlarmHappen[i + 1] = true;
                        ListBoxTxt("轴" + (i + 1).ToString() + "伺服控制器报警!");
                        Down("轴" + (i + 1).ToString(), LogType.Alarm, "轴" + (i + 1).ToString() + "伺服控制器报警!", "", 0, 0);
                    }
                    else
                    {
                        //Variable.ServoAlarmHappen[i + 1] = false;
                    }
                }
                else
                {
                    if (IntToBin(Variable.Alarm2, i - 12) == "1")
                    {
                        Variable.ServoAlarmHappen[i + 1] = true;
                        ListBoxTxt("轴" + (i + 1).ToString() + "伺服控制器报警!");
                        Down("轴" + (i + 1).ToString(), LogType.Alarm, "轴" + (i + 1).ToString() + "伺服控制器报警!", "", 0, 0);
                    }
                    else
                    {
                        //Variable.ServoAlarmHappen[i + 1] = false;
                    }
                }

                //正极限报警
                if (i < 13)
                {
                    if (IntToBin(Variable.Plimit1, i) == "1")
                    {
                        if (++Variable.AxisPTime[i + 1] >= 50)
                        {
                            Variable.PlimitAlarmHappen[i + 1] = true;
                            ListBoxTxt("轴" + (i + 1).ToString() + "伺服正极性报警!");
                            Down("轴" + (i + 1).ToString(), LogType.Alarm, "轴" + (i + 1).ToString() + "伺服正极性报警!", "", 0, 0);
                        }
                    }
                    else
                    {
                        Variable.PlimitAlarmHappen[i + 1] = false;
                        Variable.AxisPTime[i + 1] = 0;
                    }
                }
                else
                {
                    if (IntToBin(Variable.Plimit2, i - 12) == "1")
                    {
                        if (++Variable.AxisPTime[i + 1] >= 50)
                        {
                            Variable.PlimitAlarmHappen[i + 1] = true;
                            ListBoxTxt("轴" + (i + 1).ToString() + "伺服正极性报警!");
                            Down("轴" + (i + 1).ToString(), LogType.Alarm, "轴" + (i + 1).ToString() + "伺服正极性报警!", "", 0, 0);
                        }
                    }
                    else
                    {
                        Variable.PlimitAlarmHappen[i + 1] = false;
                        Variable.AxisPTime[i + 1] = 0;
                    }
                }

                //负极限报警
                if (i < 13)
                {
                    if (IntToBin(Variable.Nlimit1, i) == "1")
                    {
                        if (++Variable.AxisNTime[i + 1] >= 50)
                        {
                            Variable.NlimitAlarmHappen[i + 1] = true;
                            ListBoxTxt("轴" + (i + 1).ToString() + "伺服负极性报警!");
                            Down("轴" + (i + 1).ToString(), LogType.Alarm, "轴" + (i + 1).ToString() + "伺服负极限报警!", "", 0, 0);
                        }
                    }
                    else
                    {
                        Variable.NlimitAlarmHappen[i + 1] = false;
                        Variable.AxisNTime[i + 1] = 0;
                    }
                }
                else
                {
                    if (IntToBin(Variable.Nlimit2, i - 12) == "1")
                    {
                        if (++Variable.AxisNTime[i + 1] >= 50)
                        {
                            Variable.NlimitAlarmHappen[i + 1] = true;
                            ListBoxTxt("轴" + (i + 1).ToString() + "伺服负极性报警!");
                            Down("轴" + (i + 1).ToString(), LogType.Alarm, "轴" + (i + 1).ToString() + "伺服负极限报警!", "", 0, 0);
                        }
                    }
                    else
                    {
                        Variable.NlimitAlarmHappen[i + 1] = false;
                        Variable.AxisNTime[i + 1] = 0;
                    }
                }
            }

            #endregion

            #region 机械手报警
            //机械手报警

            if (Variable.RobotRecMessage == "A,ERR,01,B")
            {
                Thread.Sleep(500);
                Variable.RobotAlarm = true;
                ListBoxTxt("机械手Tray盘感应异常");
                Down("X0", LogType.Alarm, "机械手Tray盘感应异常，请确认!", "", 0, 0);
            }

            if (Variable.RobotRecMessage == "A,ERR,03,B")
            {
                Thread.Sleep(500);
                Variable.RobotAlarm = true;
                ListBoxTxt("机械手夹紧气缸回退异常");
                Down("X0", LogType.Alarm, "机械手夹紧气缸回退异常，请确认!", "", 0, 0);
            }

            if (Variable.RobotRecMessage == "A,ERR,02,B")
            {
                Thread.Sleep(500);
                Variable.RobotAlarm = true;
                ListBoxTxt("机械手夹紧气缸夹紧异常");
                Down("X0", LogType.Alarm, "机械手夹紧气缸夹紧异常，请确认!", "", 0, 0);
            }

            if (Variable.RobotRecMessage == "A,ERR,B")
            {
                Thread.Sleep(500);
                Variable.RobotAlarm = true;
                ListBoxTxt("机械手接受指令错误");
                Down("X0", LogType.Alarm, "机械手接受指令错误，请确认!", "", 0, 0);
            }
            #endregion

            #region 气缸报警

            if (Variable.MachineState == Variable.MachineStatus.Running || Variable.MachineState == Variable.MachineStatus.zero)
            {
                //正压报警
                if (!Variable.XStatus[11])
                {
                    if (++Variable.OnTime[11] >= 50)
                    {
                        Variable.AlarmHappen[11] = true;
                        ListBoxTxt("X11-正压报警!");
                        Down("X11", LogType.Alarm, "X11-正压报警!", "", 0, 0);
                    }
                }
                else
                {

                }

                ////负压报警
                //if (!Variable.XStatus[12])
                //{
                //    if (++Variable.OnTime[12] >= 10)
                //    {
                //        Variable.AlarmHappen[12] = true;
                //        ListBoxTxt("X12-负压报警!");
                //        Down("X0", LogType.Alarm, "X12-负压报警!", "", 0, 0);
                //    }
                //}
                //else
                //{

                //}

                #region 上料模组

                //上料空Tray工位1支撑气缸出
                if (Variable.OnEnable[20] && !Variable.XStatus[35])
                {
                    if (++Variable.OnTime[35] >= 50)
                    {
                        Variable.AlarmHappen[35] = true;
                        ListBoxTxt("X35-上料空Tray工位1支撑气缸出异常，请确认!");
                        Down("X35", LogType.Alarm, "X35-上料空Tray工位1支撑气缸出异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //上料空Tray工位1支撑气缸回
                if (Variable.OffEnable[20] && !Variable.XStatus[34])
                {
                    if (++Variable.OffTime[34] >= 50)
                    {
                        Variable.AlarmHappen[34] = true;
                        ListBoxTxt("X34-上料空Tray工位1支撑气缸回异常，请确认!");
                        Down("X34", LogType.Alarm, "X34-上料空Tray工位1支撑气缸回异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //上料空Tray盘工位3上顶气缸上升
                if (Variable.OnEnable[22] && !Variable.XStatus[39])
                {
                    if (++Variable.OnTime[39] >= 50)
                    {
                        Variable.AlarmHappen[39] = true;
                        ListBoxTxt("X39-上料空Tray盘工位3上顶气缸上升异常，请确认!");
                        Down("X39", LogType.Alarm, "X39-上料空Tray盘工位3上顶气缸上升异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //上料空Tray盘工位3上顶气缸下降
                if (Variable.OffEnable[22] && !Variable.XStatus[40])
                {
                    if (++Variable.OffTime[40] >= 50)
                    {
                        Variable.AlarmHappen[40] = true;
                        ListBoxTxt("X40-上料空Tray盘工位3上顶气缸下降异常，请确认!");
                        Down("X40", LogType.Alarm, "X40-上料空Tray盘工位3上顶气缸下降异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //上料空Tray轨道运动平台夹紧气缸夹紧
                if (Variable.OnEnable[23] && !Variable.XStatus[42])
                {
                    if (++Variable.OnTime[42] >= 50)
                    {
                        Variable.AlarmHappen[42] = true;
                        ListBoxTxt("X42-上料空Tray轨道运动平台夹紧气缸夹紧异常，请确认!");
                        Down("X42", LogType.Alarm, "X42-上料空Tray轨道运动平台夹紧气缸夹紧异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //上料空Tray轨道运动平台夹紧气缸松开
                if (Variable.OffEnable[23] && Variable.XStatus[42])
                {
                    if (++Variable.OffTime[42] >= 50)
                    {
                        Variable.AlarmHappen[42] = true;
                        ListBoxTxt("X42-上料空Tray轨道运动平台夹紧气缸松开异常，请确认!");
                        Down("X42", LogType.Alarm, "X42-上料空Tray轨道运动平台夹紧气缸松开异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //上料分料吸嘴1吸真空打开
                if (Variable.OnEnable[29] && !Variable.XStatus[57])
                {
                    if (++Variable.OnTime[57] >= 50)
                    {
                        Variable.AlarmHappen[57] = true;
                        ListBoxTxt("X57-上料分料吸嘴1吸真空打开异常，请确认!");
                        Down("X57", LogType.Alarm1, "X57-上料分料吸嘴1吸真空打开异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //上料分料吸嘴2吸真空打开
                if (Variable.OnEnable[30] && !Variable.XStatus[58])
                {
                    if (++Variable.OnTime[58] >= 50)
                    {
                        Variable.AlarmHappen[58] = true;
                        ListBoxTxt("X58-上料分料吸嘴2吸真空打开异常，请确认!");
                        Down("X58", LogType.Alarm1, "X58-上料分料吸嘴2吸真空打开异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //上料分料吸嘴1气缸出
                if (Variable.OnEnable[33] && !Variable.XStatus[60])
                {
                    if (++Variable.OnTime[60] >= 50)
                    {
                        Variable.AlarmHappen[60] = true;
                        ListBoxTxt("X60-上料分料吸嘴1气缸出异常，请确认!");
                        Down("X60", LogType.Alarm, "X60-上料分料吸嘴1气缸出异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //上料分料吸嘴1气缸回
                if (Variable.OffEnable[33] && !Variable.XStatus[59])
                {
                    if (++Variable.OffTime[59] >= 50)
                    {
                        Variable.AlarmHappen[59] = true;
                        ListBoxTxt("X59-上料分料吸嘴1气缸回异常，请确认!");
                        Down("X59", LogType.Alarm, "X59-上料分料吸嘴1气缸回异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //上料分料吸嘴2气缸出
                if (Variable.OnEnable[34] && !Variable.XStatus[62])
                {
                    if (++Variable.OnTime[62] >= 50)
                    {
                        Variable.AlarmHappen[62] = true;
                        ListBoxTxt("X62-上料分料吸嘴2气缸出异常，请确认!");
                        Down("X62", LogType.Alarm, "X62-上料分料吸嘴2气缸出异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //上料分料吸嘴2气缸回
                if (Variable.OffEnable[34] && !Variable.XStatus[61])
                {
                    if (++Variable.OffTime[61] >= 50)
                    {
                        Variable.AlarmHappen[61] = true;
                        ListBoxTxt("X61-上料分料吸嘴2气缸回异常，请确认!");
                        Down("X61", LogType.Alarm, "X61-上料分料吸嘴2气缸回异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //上料待测Tray盘工位1支撑气缸出
                if (Variable.OnEnable[24] && !Variable.XStatus[46])
                {
                    if (++Variable.OnTime[46] >= 50)
                    {
                        Variable.AlarmHappen[46] = true;
                        ListBoxTxt("X46-上料待测Tray盘工位1支撑气缸出异常，请确认!");
                        Down("X46", LogType.Alarm, "X46-上料待测Tray盘工位1支撑气缸出异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //上料待测Tray盘工位1支撑气缸回
                if (Variable.OffEnable[24] && !Variable.XStatus[45])
                {
                    if (++Variable.OffTime[45] >= 50)
                    {
                        Variable.AlarmHappen[45] = true;
                        ListBoxTxt("X45-上料待测Tray盘工位1支撑气缸回异常，请确认!");
                        Down("X45", LogType.Alarm, "X45-上料待测Tray盘工位1支撑气缸回异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //上料待测Tray盘工位2侧顶气缸顶出
                if (Variable.OnEnable[25] && Variable.XStatus[48])
                {
                    if (++Variable.OnTime[48] >= 50)
                    {
                        Variable.AlarmHappen[48] = true;
                        ListBoxTxt("X48-上料待测Tray盘工位2侧顶气缸顶出异常，请确认!");
                        Down("X48", LogType.Alarm, "X48-上料待测Tray盘工位2侧顶气缸顶出异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //上料待测Tray盘工位2侧顶气缸顶回
                if (Variable.OffEnable[25] && !Variable.XStatus[48])
                {
                    if (++Variable.OffTime[48] >= 50)
                    {
                        Variable.AlarmHappen[48] = true;
                        ListBoxTxt("X48-上料待测Tray盘工位2侧顶气缸顶回异常，请确认!");
                        Down("X48", LogType.Alarm, "X48-上料待测Tray盘工位2侧顶气缸顶回异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //上料待测Tray盘工位2上下气缸上升
                if (Variable.OnEnable[26] && !Variable.XStatus[50])
                {
                    if (++Variable.OnTime[50] >= 120)
                    {
                        Variable.AlarmHappen[50] = true;
                        ListBoxTxt("X50-上料待测Tray盘工位2上下气缸上升异常，请确认!");
                        Down("X50", LogType.Alarm, "X50-上料待测Tray盘工位2上下气缸上升异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //上料待测Tray盘工位2上下气缸下降
                if (Variable.OffEnable[26] && !Variable.XStatus[49])
                {
                    if (++Variable.OffTime[49] >= 120)
                    {
                        Variable.AlarmHappen[49] = true;
                        ListBoxTxt("X49-上料待测Tray盘工位2上下气缸下降异常，请确认!");
                        Down("X49", LogType.Alarm, "X49-上料待测Tray盘工位2上下气缸下降异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //上料待测Tray盘工位3支撑气缸出
                if (Variable.OnEnable[27] && !Variable.XStatus[54])
                {
                    if (++Variable.OnTime[54] >= 50)
                    {
                        Variable.AlarmHappen[54] = true;
                        ListBoxTxt("X54-上料待测Tray盘工位3支撑气缸出异常，请确认!");
                        Down("X54", LogType.Alarm, "X54-上料待测Tray盘工位3支撑气缸出异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //上料待测Tray盘工位3支撑气缸回
                if (Variable.OffEnable[27] && !Variable.XStatus[53])
                {
                    if (++Variable.OffTime[53] >= 50)
                    {
                        Variable.AlarmHappen[53] = true;
                        ListBoxTxt("X53-上料待测Tray盘工位3支撑气缸回异常，请确认!");
                        Down("X53", LogType.Alarm, "X53-上料待测Tray盘工位3支撑气缸回异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //上料待测Tray盘轨道运动平台夹紧气缸夹紧
                if (Variable.OnEnable[28] && !Variable.XStatus[56])
                {
                    if (++Variable.OnTime[56] >= 50)
                    {
                        Variable.AlarmHappen[56] = true;
                        ListBoxTxt("X56-上料待测Tray盘轨道运动平台夹紧气缸夹紧异常，请确认!");
                        Down("X56", LogType.Alarm, "X56-上料待测Tray盘轨道运动平台夹紧气缸夹紧异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //上料待测Tray盘轨道运动平台夹紧气缸松开
                if (Variable.OffEnable[2] && Variable.XStatus[56])
                {
                    if (++Variable.OffTime[56] >= 50)
                    {
                        Variable.AlarmHappen[56] = true;
                        ListBoxTxt("X56-上料待测Tray盘轨道运动平台夹紧气缸松开异常，请确认!");
                        Down("X56", LogType.Alarm, "X56-上料待测Tray盘轨道运动平台夹紧气缸松开异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }
                #endregion

                #region 下料模组

                //下料良品Tray盘工位1上顶气缸上升
                if (Variable.OnEnable[52] && !Variable.XStatus[67])
                {
                    if (++Variable.OnTime[67] >= 50)
                    {
                        Variable.AlarmHappen[67] = true;
                        ListBoxTxt("X67-下料良品Tray盘工位1上顶气缸上升异常，请确认!");
                        Down("X67", LogType.Alarm, "X67-下料良品Tray盘工位1上顶气缸上升异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //下料良品Tray盘工位1上顶气缸下降
                if (Variable.OffEnable[52] && !Variable.XStatus[68])
                {
                    if (++Variable.OffTime[68] >= 50)
                    {
                        Variable.AlarmHappen[68] = true;
                        ListBoxTxt("X68-下料良品Tray盘工位1上顶气缸下降异常，请确认!");
                        Down("X68", LogType.Alarm, "X68-下料良品Tray盘工位1上顶气缸下降异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }


                //下料良品Tray盘工位2侧顶气缸出
                if (Variable.OnEnable[53] && Variable.XStatus[71])
                {
                    if (++Variable.OnTime[71] >= 50)
                    {
                        Variable.AlarmHappen[71] = true;
                        ListBoxTxt("X71-下料良品Tray盘工位2侧顶气缸出异常，请确认!");
                        Down("X71", LogType.Alarm, "X71-下料良品Tray盘工位2侧顶气缸出异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //下料良品Tray盘工位2侧顶气缸回
                if (Variable.OffEnable[53] && !Variable.XStatus[71])
                {
                    if (++Variable.OffTime[71] >= 50)
                    {
                        Variable.AlarmHappen[71] = true;
                        ListBoxTxt("X71-下料良品Tray盘工位2侧顶气缸回异常，请确认!");
                        Down("X71", LogType.Alarm, "X71-下料良品Tray盘工位2侧顶气缸回异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //下料良品Tray盘工位2上顶气缸上升
                if (Variable.OnEnable[54] && !Variable.XStatus[73])
                {
                    if (++Variable.OnTime[73] >= 100)
                    {
                        Variable.AlarmHappen[73] = true;
                        ListBoxTxt("X73-下料良品Tray盘工位2上顶气缸上升异常，请确认!");
                        Down("X73", LogType.Alarm, "X73-下料良品Tray盘工位2上顶气缸上升异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //下料良品Tray盘工位2上顶气缸下降
                if (Variable.OffEnable[54] && !Variable.XStatus[72])
                {
                    if (++Variable.OffTime[72] >= 120)
                    {
                        Variable.AlarmHappen[72] = true;
                        ListBoxTxt("X72-下料良品Tray盘工位2上顶气缸下降异常，请确认!");
                        Down("X72", LogType.Alarm, "X72-下料良品Tray盘工位2上顶气缸下降异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //下料良品轨道运动平台夹紧气缸夹紧
                if (Variable.OnEnable[55] && !Variable.XStatus[77])
                {
                    if (++Variable.OnTime[77] >= 50)
                    {
                        Variable.AlarmHappen[77] = true;
                        ListBoxTxt("X77-下料良品轨道运动平台夹紧气缸夹紧异常，请确认!");
                        Down("X77", LogType.Alarm, "X77-下料良品轨道运动平台夹紧气缸夹紧异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //下料良品轨道运动平台夹紧气缸松开
                if (Variable.OffEnable[55] && Variable.XStatus[77])
                {
                    if (++Variable.OffTime[77] >= 50)
                    {
                        Variable.AlarmHappen[77] = true;
                        ListBoxTxt("X77-下料良品轨道运动平台夹紧气缸夹紧异常，请确认!");
                        Down("X77", LogType.Alarm, "X77-下料良品轨道运动平台夹紧气缸夹紧异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //下料补料空Tray工位1上顶气缸上升
                if (Variable.OnEnable[56] && !Variable.XStatus[80])
                {
                    if (++Variable.OnTime[80] >= 50)
                    {
                        Variable.AlarmHappen[80] = true;
                        ListBoxTxt("X80-下料补料空Tray工位1上顶气缸上升异常，请确认!");
                        Down("X80", LogType.Alarm, "X80-下料补料空Tray工位1上顶气缸上升异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //下料补料空Tray工位1上顶气缸下降
                if (Variable.OffEnable[56] && !Variable.XStatus[81])
                {
                    if (++Variable.OffTime[81] >= 50)
                    {
                        Variable.AlarmHappen[81] = true;
                        ListBoxTxt("X81-下料补料空Tray工位1上顶气缸下降异常，请确认!");
                        Down("X81", LogType.Alarm, "X81-下料补料空Tray工位1上顶气缸下降异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //下料补料Tray盘工位2侧顶气缸顶出
                if (Variable.OnEnable[57] && Variable.XStatus[82])
                {
                    if (++Variable.OnTime[82] >= 50)
                    {
                        Variable.AlarmHappen[82] = true;
                        ListBoxTxt("X82-下料补料Tray盘工位2侧顶气缸顶出异常，请确认!");
                        Down("X82", LogType.Alarm, "X82-下料补料Tray盘工位2侧顶气缸顶出异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //下料补料Tray盘工位2侧顶气缸回
                if (Variable.OffEnable[57] && !Variable.XStatus[82])
                {
                    if (++Variable.OffTime[82] >= 50)
                    {
                        Variable.AlarmHappen[82] = true;
                        ListBoxTxt("X82-下料补料Tray盘工位2侧顶气缸回异常，请确认!");
                        Down("X82", LogType.Alarm, "X82-下料补料Tray盘工位2侧顶气缸回异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //下料补料轨道运动平台夹紧气缸夹紧
                if (Variable.OnEnable[58] && !Variable.XStatus[85])
                {
                    if (++Variable.OnTime[85] >= 50)
                    {
                        Variable.AlarmHappen[85] = true;
                        ListBoxTxt("X85-下料补料轨道运动平台夹紧气缸夹紧异常，请确认!");
                        Down("X85", LogType.Alarm, "X85-下料补料轨道运动平台夹紧气缸夹紧异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //下料补料轨道运动平台夹紧气缸松开
                if (Variable.OffEnable[58] && Variable.XStatus[85])
                {
                    if (++Variable.OffTime[85] >= 50)
                    {
                        Variable.AlarmHappen[85] = true;
                        ListBoxTxt("X85-下料补料轨道运动平台夹紧气缸松开异常，请确认!");
                        Down("X85", LogType.Alarm, "X85-下料补料轨道运动平台夹紧气缸松开异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //下料NGTray工位1支撑气缸出
                if (Variable.OnEnable[59] && !Variable.XStatus[89])
                {
                    if (++Variable.OnTime[89] >= 50)
                    {
                        Variable.AlarmHappen[89] = true;
                        ListBoxTxt("X89-下料NGTray工位1支撑气缸出异常，请确认!");
                        Down("X89", LogType.Alarm, "X89-下料NGTray工位1支撑气缸出异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //下料NGTray工位1支撑气缸回
                if (Variable.OffEnable[59] && !Variable.XStatus[88])
                {
                    if (++Variable.OffTime[88] >= 50)
                    {
                        Variable.AlarmHappen[88] = true;
                        ListBoxTxt("X88-下料NGTray工位1支撑气缸回异常，请确认!");
                        Down("X88", LogType.Alarm, "X88-下料NGTray工位1支撑气缸回异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //下料NGTray盘工位3上顶气缸上升
                if (Variable.OnEnable[60] && !Variable.XStatus[92])
                {
                    if (++Variable.OnTime[92] >= 50)
                    {
                        Variable.AlarmHappen[92] = true;
                        ListBoxTxt("X92-下料NGTray盘工位3上顶气缸上升异常，请确认!");
                        Down("X92", LogType.Alarm, "X92-下料NGTray盘工位3上顶气缸上升异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //下料NGTray盘工位3上顶气缸下降
                if (Variable.OffEnable[60] && !Variable.XStatus[93])
                {
                    if (++Variable.OffTime[93] >= 50)
                    {
                        Variable.AlarmHappen[93] = true;
                        ListBoxTxt("X93-下料NGTray盘工位3上顶气缸下降异常，请确认!");
                        Down("X93", LogType.Alarm, "X93-下料NGTray盘工位3上顶气缸下降异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //下料NG轨道运动平台夹紧气缸夹紧
                if (Variable.OnEnable[61] && !Variable.XStatus[95])
                {
                    if (++Variable.OnTime[95] >= 50)
                    {
                        Variable.AlarmHappen[95] = true;
                        ListBoxTxt("X95-下料NG轨道运动平台夹紧气缸夹紧异常，请确认!");
                        Down("X95", LogType.Alarm, "X95-下料NG轨道运动平台夹紧气缸夹紧异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //下料NG轨道运动平台夹紧气缸松开
                if (Variable.OffEnable[61] && Variable.XStatus[95])
                {
                    if (++Variable.OffTime[95] >= 50)
                    {
                        Variable.AlarmHappen[95] = true;
                        ListBoxTxt("X95-下料NG轨道运动平台夹紧气缸松开异常，请确认!");
                        Down("X95", LogType.Alarm, "X95-下料NG轨道运动平台夹紧气缸松开异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //移Tray夹爪上下气缸下降
                if (Variable.OnEnable[68] && !Variable.XStatus[103])
                {
                    if (++Variable.OnTime[103] >= 50)
                    {
                        Variable.AlarmHappen[103] = true;
                        ListBoxTxt("X103-移Tray夹爪上下气缸下降异常，请确认!");
                        Down("X103", LogType.Alarm, "X103-移Tray夹爪上下气缸下降异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //移Tray夹爪上下气缸上升
                if (Variable.OffEnable[68] && !Variable.XStatus[102])
                {
                    if (++Variable.OffTime[102] >= 50)
                    {
                        Variable.AlarmHappen[102] = true;
                        ListBoxTxt("X102-移Tray夹爪上下气缸上升异常，请确认!");
                        Down("X102", LogType.Alarm, "X102-移Tray夹爪上下气缸上升异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //移Tray夹爪气缸夹紧
                if (Variable.OnEnable[69] && !Variable.XStatus[105])
                {
                    if (++Variable.OnTime[105] >= 50)
                    {
                        Variable.AlarmHappen[105] = true;
                        ListBoxTxt("X105-移Tray夹爪气缸夹紧异常，请确认!");
                        Down("X105", LogType.Alarm, "X105-移Tray夹爪气缸夹紧异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //移Tray夹爪气缸松开
                if (Variable.OffEnable[69] && !Variable.XStatus[104])
                {
                    if (++Variable.OffTime[104] >= 50)
                    {
                        Variable.AlarmHappen[104] = true;
                        ListBoxTxt("X104-移Tray夹爪气缸松开异常，请确认!");
                        Down("X104", LogType.Alarm, "X104-移Tray夹爪气缸松开异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                ////下料分料吸嘴1吸真空
                //if (Variable.OnEnable[62] && !Variable.XStatus[96])
                //{
                //    if (++Variable.OnTime[96] >= 50)
                //    {
                //        Variable.AlarmHappen[96] = true;
                //        ListBoxTxt("X96-下料分料吸嘴1吸真空异常，请确认!");
                //        Down("X96", LogType.Alarm, "X96-下料分料吸嘴1吸真空异常，请确认!", "", 0, 0);
                //    }
                //}
                //else
                //{
                //}

                ////下料分料吸嘴2吸真空
                //if (Variable.OnEnable[63] && !Variable.XStatus[97])
                //{
                //    if (++Variable.OnTime[97] >= 50)
                //    {
                //        Variable.AlarmHappen[97] = true;
                //        ListBoxTxt("X97-下料分料吸嘴2吸真空异常，请确认!");
                //        Down("X97", LogType.Alarm, "X97-下料分料吸嘴2吸真空异常，请确认!", "", 0, 0);
                //    }
                //}
                //else
                //{
                //}

                //下料分料吸嘴1气缸回
                if (Variable.OffEnable[66] && !Variable.XStatus[98])
                {
                    if (++Variable.OnTime[98] >= 50)
                    {
                        Variable.AlarmHappen[98] = true;
                        ListBoxTxt("X98-下料分料吸嘴1气缸回异常，请确认!");
                        Down("X98", LogType.Alarm, "X98-下料分料吸嘴1气缸回异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //下料分料吸嘴1气缸出
                if (Variable.OnEnable[66] && !Variable.XStatus[99])
                {
                    if (++Variable.OffTime[99] >= 50)
                    {
                        Variable.AlarmHappen[99] = true;
                        ListBoxTxt("X99-下料分料吸嘴1气缸出异常，请确认!");
                        Down("X99", LogType.Alarm, "X99-下料分料吸嘴1气缸出异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //下料分料吸嘴2气缸回
                if (Variable.OffEnable[67] && !Variable.XStatus[100])
                {
                    if (++Variable.OnTime[100] >= 50)
                    {
                        Variable.AlarmHappen[100] = true;
                        ListBoxTxt("X100-下料分料吸嘴2气缸回异常，请确认!");
                        Down("X100", LogType.Alarm, "X100-下料分料吸嘴2气缸回异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }

                //下料分料吸嘴2气缸出
                if (Variable.OnEnable[67] && !Variable.XStatus[101])
                {
                    if (++Variable.OffTime[101] >= 50)
                    {
                        Variable.AlarmHappen[101] = true;
                        ListBoxTxt("X101-下料分料吸嘴2气缸出异常，请确认!");
                        Down("X101", LogType.Alarm, "X101-下料分料吸嘴2气缸出异常，请确认!", "", 0, 0);
                    }
                }
                else
                {
                }
                #endregion

                #region 老化机

                #region 上层

                for (int i = 0; i < 10; i++)
                {
                    //上层平台定位气缸回
                    if (Variable.OnEnable[100 + i * 32] && !Variable.XStatus[115 + i * 32])
                    {
                        if (++Variable.OnTime[115 + i * 32] >= 50)
                        {
                            Variable.AlarmHappen[115 + i * 32] = true;
                            ListBoxTxt((115 + i * 32).ToString() + "-" + (i + 1).ToString() + "#上层平台定位气缸回异常，请确认!");
                            Down("X115", LogType.Alarm, (115 + i * 32).ToString() + "-" + (i + 1).ToString() + "#上层平台定位气缸回异常，请确认!", "", 0, 0);
                        }
                    }
                    else
                    {
                    }

                    //上层平台定位气缸出
                    if (Variable.OffEnable[100 + i * 32] && Variable.XStatus[115 + i * 32])
                    {
                        if (++Variable.OffTime[115 + i * 32] >= 50)
                        {
                            Variable.AlarmHappen[115 + i * 32] = true;
                            ListBoxTxt((115 + i * 32).ToString() + "-" + (i + 1).ToString() + "#上层平台定位气缸出异常，请确认!");
                            Down("X115", LogType.Alarm, (115 + i * 32).ToString() + "-" + (i + 1).ToString() + "#上层平台定位气缸出异常，请确认!", "", 0, 0);
                        }
                    }
                    else
                    {
                    }

                    //上层老化机上顶气缸1上升
                    if (Variable.OnEnable[102 + i * 32] && !Variable.XStatus[119 + i * 32])
                    {
                        if (++Variable.OnTime[119 + i * 32] >= 50)
                        {
                            function.OutYOFF(101 + i * 32);//上层1老化上下气缸下降关闭
                            function.OutYOFF(102 + i * 32);//上层1老化上下气缸上升关闭

                            Variable.AlarmHappen[119 + i * 32] = true;
                            Variable.AireAlarm[i * 4] = true;
                            ListBoxTxt((119 + i * 32).ToString() + "-" + (i + 1).ToString() + "#上层老化机上顶气缸内上升异常，请确认!");
                            Down("X119", LogType.Alarm, (119 + i * 32).ToString() + "-" + (i + 1).ToString() + "#上层老化机上顶气缸内上升异常，请确认!", "", 0, 0);
                        }
                    }
                    else
                    {
                    }

                    //上层老化机上顶气缸1下降
                    if (Variable.OnEnable[101 + i * 32] && !Variable.XStatus[118 + i * 32])
                    {
                        if (++Variable.OffTime[118 + i * 32] >= 50)
                        {
                            Variable.AlarmHappen[118 + i * 32] = true;
                            ListBoxTxt((118 + i * 32).ToString() + "-" + (i + 1).ToString() + "#上层老化机上顶气缸内下降异常，请确认!");
                            Down("X118", LogType.Alarm, (118 + i * 32).ToString() + "-" + (i + 1).ToString() + "#上层老化机上顶气缸内下降异常，请确认!", "", 0, 0);
                        }
                    }
                    else
                    {
                    }
                    //上层老化机上顶气缸2上升
                    if (Variable.OnEnable[104 + i * 32] && !Variable.XStatus[123 + i * 32])
                    {
                        if (++Variable.OnTime[123 + i * 32] >= 50)
                        {
                            function.OutYOFF(103 + i * 32);//上层2老化上下气缸下降关闭
                            function.OutYOFF(104 + i * 32);//上层2老化上下气缸上升关闭

                            Variable.AlarmHappen[123 + i * 32] = true;
                            Variable.AireAlarm[i * 4 + 1] = true;
                            ListBoxTxt((123 + i * 32).ToString() + "-" + (i + 1).ToString() + "#上层老化机上顶气缸外上升异常，请确认!");
                            Down("X123", LogType.Alarm, (123 + i * 32).ToString() + "-" + (i + 1).ToString() + "#上层老化机上顶气缸外上升异常，请确认!", "", 0, 0);
                        }
                    }
                    else
                    {
                    }

                    //上层老化机上顶气缸2下降
                    if (Variable.OnEnable[103 + i * 32] && !Variable.XStatus[122 + i * 32])
                    {
                        if (++Variable.OffTime[122 + i * 32] >= 50)
                        {
                            Variable.AlarmHappen[122 + i * 32] = true;
                            ListBoxTxt((122 + i * 32).ToString() + "-" + (i + 1).ToString() + "#上层老化机上顶气缸外下降异常，请确认!");
                            Down("X122", LogType.Alarm, (122 + i * 32).ToString() + "-" + (i + 1).ToString() + "#上层老化机上顶气缸外下降异常，请确认!", "", 0, 0);
                        }
                    }
                    else
                    {
                    }
                    //上层老化机侧定位气缸出
                    if (Variable.OnEnable[105 + i * 32] && (!Variable.XStatus[124 + i * 32] || !Variable.XStatus[125 + i * 32]))
                    {
                        if (++Variable.OnTime[124 + i * 32] >= 50)
                        {
                            Variable.AlarmHappen[124 + i * 32] = true;
                            ListBoxTxt((124 + i * 32).ToString() + "-" + (i + 1).ToString() + "#上层老化机侧定位气缸出异常，请确认!");
                            Down("X124", LogType.Alarm, (124 + i * 32).ToString() + "-" + (i + 1).ToString() + "#上层老化机侧定位气缸出异常，请确认!", "", 0, 0);
                        }
                    }
                    else
                    {
                    }

                    //上层老化机侧定位气缸回
                    if (Variable.OffEnable[105 + i * 32] && (Variable.XStatus[124 + i * 32] || Variable.XStatus[125 + i * 32]))
                    {
                        if (++Variable.OffTime[124 + i * 32] >= 50)
                        {
                            Variable.AlarmHappen[124 + i * 32] = true;
                            ListBoxTxt((124 + i * 32).ToString() + "-" + (i + 1).ToString() + "#上层老化机侧定位气缸回异常，请确认!");
                            Down("X124", LogType.Alarm, (124 + i * 32).ToString() + "-" + (i + 1).ToString() + "#上层老化机侧定位气缸回异常，请确认!", "", 0, 0);
                        }
                    }
                    else
                    {
                    }
                    //上层老化机推Tray气缸回
                    if (Variable.OffEnable[106 + i * 32] && !Variable.XStatus[126 + i * 32])
                    {
                        if (++Variable.OnTime[126 + i * 32] >= 50)
                        {
                            Variable.AlarmHappen[126 + i * 32] = true;
                            ListBoxTxt((126 + i * 32).ToString() + "-" + (i + 1).ToString() + "#上层老化机推Tray气缸回异常，请确认!");
                            Down("X126", LogType.Alarm, (126 + i * 32).ToString() + "-" + (i + 1).ToString() + "#上层老化机推Tray气缸回异常，请确认!", "", 0, 0);
                        }
                    }
                    else
                    {
                    }

                    //上层老化机推Tray气缸出
                    if (Variable.OnEnable[106 + i * 32] && !Variable.XStatus[127 + i * 32])
                    {
                        if (++Variable.OffTime[127 + i * 32] >= 50)
                        {
                            Variable.AlarmHappen[127 + i * 32] = true;
                            ListBoxTxt((127 + i * 32).ToString() + "-" + (i + 1).ToString() + "#上层老化机推Tray气缸出异常，请确认!");
                            Down("X127", LogType.Alarm, (127 + i * 32).ToString() + "-" + (i + 1).ToString() + "#上层老化机推Tray气缸出异常，请确认!", "", 0, 0);
                        }
                    }
                    else
                    {
                    }
                    //上层老化机测试板加热1有效
                    if (Variable.ModelState[i * 4 + 0] == 1)
                    {
                        if ((Variable.OnEnable[109 + i * 32] && Variable.XStatus[116 + i * 32]) || Variable.TempOKFlag[i * 4 + 0])
                        {
                            if (++Variable.OnTime[116 + i * 32] >= 18000)
                            {
                                Variable.AlarmHappen[116 + i * 32] = true;
                                ListBoxTxt((116 + i * 32).ToString() + "-" + (i + 1).ToString() + "#上层老化机测试板加热内异常，请确认!");
                                Down("X116", LogType.Alarm, (116 + i * 32).ToString() + "-" + (i + 1).ToString() + "#上层老化机测试板加热内异常，请确认!", "", 0, 0);
                            }
                        }
                        else
                        {
                        }
                    }

                    //上层老化机测试板加热2有效
                    if (Variable.ModelState[i * 4 + 1] == 1)
                    {
                        if ((Variable.OnEnable[111 + i * 32] && Variable.XStatus[120 + i * 32]) || Variable.TempOKFlag[i * 4 + 1])
                        {
                            if (++Variable.OnTime[120 + i * 32] >= 18000)
                            {
                                Variable.AlarmHappen[120 + i * 32] = true;
                                ListBoxTxt((120 + i * 32).ToString() + "-" + (i + 1).ToString() + "#上层老化机测试板加热外异常，请确认!");
                                Down("X120", LogType.Alarm, (120 + i * 32).ToString() + "-" + (i + 1).ToString() + "#上层老化机测试板加热外异常，请确认!", "", 0, 0);
                            }
                        }
                        else
                        {
                        }
                    }
                }
                #endregion

                #region 下层

                for (int i = 0; i < 10; i++)
                {
                    //下层平台定位气缸回
                    if (Variable.OnEnable[116 + i * 32] && !Variable.XStatus[129 + i * 32])
                    {
                        if (++Variable.OnTime[129 + i * 32] >= 50)
                        {
                            Variable.AlarmHappen[129 + i * 32] = true;
                            ListBoxTxt((129 + i * 32).ToString() + "-" + (i + 1).ToString() + "#下层平台定位气缸回异常，请确认!");
                            Down("X129", LogType.Alarm, (129 + i * 32).ToString() + "-" + (i + 1).ToString() + "#下层平台定位气缸回异常，请确认!", "", 0, 0);
                        }
                    }
                    else
                    {
                    }

                    //下层平台定位气缸出
                    if (Variable.OffEnable[116 + i * 32] && Variable.XStatus[129 + i * 32])
                    {
                        if (++Variable.OffTime[129 + i * 32] >= 50)
                        {
                            Variable.AlarmHappen[129 + i * 32] = true;
                            ListBoxTxt((129 + i * 32).ToString() + "-" + (i + 1).ToString() + "#下层平台定位气缸出异常，请确认!");
                            Down("X129", LogType.Alarm, (129 + i * 32).ToString() + "-" + (i + 1).ToString() + "#下层平台定位气缸出异常，请确认!", "", 0, 0);
                        }
                    }
                    else
                    {
                    }

                    //下层老化机上顶气缸1上升
                    if (Variable.OnEnable[118 + i * 32] && !Variable.XStatus[133 + i * 32])
                    {
                        if (++Variable.OnTime[133 + i * 32] >= 50)
                        {
                            function.OutYOFF(117 + i * 32);//下层1老化上下气缸下降关闭
                            function.OutYOFF(118 + i * 32);//下层1老化上下气缸上升关闭

                            Variable.AlarmHappen[133 + i * 32] = true;
                            Variable.AireAlarm[i * 4 + 2] = true;
                            ListBoxTxt((133 + i * 32).ToString() + "-" + (i + 1).ToString() + "#下层老化机上顶气缸内上升异常，请确认!");
                            Down("X133", LogType.Alarm, (133 + i * 32).ToString() + "-" + (i + 1).ToString() + "#下层老化机上顶气缸内上升异常，请确认!", "", 0, 0);
                        }
                    }
                    else
                    {
                    }

                    //下层老化机上顶气缸1下降
                    if (Variable.OnEnable[117 + i * 32] && !Variable.XStatus[132 + i * 32])
                    {
                        if (++Variable.OffTime[132 + i * 32] >= 50)
                        {
                            Variable.AlarmHappen[132 + i * 32] = true;
                            ListBoxTxt((132 + i * 32).ToString() + "-" + (i + 1).ToString() + "#下层老化机上顶气缸内下降异常，请确认!");
                            Down("X132", LogType.Alarm, (132 + i * 32).ToString() + "-" + (i + 1).ToString() + "#下层老化机上顶气缸内下降异常，请确认!", "", 0, 0);
                        }
                    }
                    else
                    {
                    }
                    //下层老化机上顶气缸2上升
                    if (Variable.OnEnable[120 + i * 32] && !Variable.XStatus[137 + i * 32])
                    {
                        if (++Variable.OnTime[137 + i * 32] >= 50)
                        {
                            function.OutYOFF(119 + i * 32);//下层2老化上下气缸下降关闭
                            function.OutYOFF(120 + i * 32);//下层2老化上下气缸上升关闭

                            Variable.AlarmHappen[137 + i * 32] = true;
                            Variable.AireAlarm[i * 4 + 3] = true;
                            ListBoxTxt((137 + i * 32).ToString() + "-" + (i + 1).ToString() + "#下层老化机上顶气缸外上升异常，请确认!");
                            Down("X137", LogType.Alarm, (137 + i * 32).ToString() + "-" + (i + 1).ToString() + "#下层老化机上顶气缸外上升异常，请确认!", "", 0, 0);
                        }
                    }
                    else
                    {
                    }

                    //下层老化机上顶气缸2下降
                    if (Variable.OnEnable[119 + i * 32] && !Variable.XStatus[136 + i * 32])
                    {
                        if (++Variable.OffTime[136 + i * 32] >= 50)
                        {
                            Variable.AlarmHappen[136 + i * 32] = true;
                            ListBoxTxt((136 + i * 32).ToString() + "-" + (i + 1).ToString() + "#下层老化机上顶气缸外下降异常，请确认!");
                            Down("X136", LogType.Alarm, (136 + i * 32).ToString() + "-" + (i + 1).ToString() + "#下层老化机上顶气缸外下降异常，请确认!", "", 0, 0);
                        }
                    }
                    else
                    {
                    }
                    //下层老化机侧定位气缸出
                    if (Variable.OnEnable[121 + i * 32] && (!Variable.XStatus[138 + i * 32] || !Variable.XStatus[139 + i * 32]))
                    {
                        if (++Variable.OnTime[138 + i * 32] >= 50)
                        {
                            Variable.AlarmHappen[138 + i * 32] = true;
                            ListBoxTxt((138 + i * 32).ToString() + "-" + (i + 1).ToString() + "#下层老化机侧定位气缸出异常，请确认!");
                            Down("X138", LogType.Alarm, (138 + i * 32).ToString() + "-" + (i + 1).ToString() + "#下层老化机侧定位气缸出异常，请确认!", "", 0, 0);
                        }
                    }
                    else
                    {
                    }

                    //下层老化机侧定位气缸回
                    if (Variable.OffEnable[121 + i * 32] && (Variable.XStatus[138 + i * 32] || Variable.XStatus[139 + i * 32]))
                    {
                        if (++Variable.OffTime[138 + i * 32] >= 50)
                        {
                            Variable.AlarmHappen[138 + i * 32] = true;
                            ListBoxTxt((138 + i * 32).ToString() + "-" + (i + 1).ToString() + "#下层老化机侧定位气缸回异常，请确认!");
                            Down("X138", LogType.Alarm, (138 + i * 32).ToString() + "-" + (i + 1).ToString() + "#下层老化机侧定位气缸回异常，请确认!", "", 0, 0);
                        }
                    }
                    else
                    {
                    }
                    //下层老化机推Tray气缸回
                    if (Variable.OffEnable[122 + i * 32] && !Variable.XStatus[140 + i * 32])
                    {
                        if (++Variable.OnTime[140 + i * 32] >= 50)
                        {
                            Variable.AlarmHappen[140 + i * 32] = true;
                            ListBoxTxt((140 + i * 32).ToString() + "-" + (i + 1).ToString() + "#下层老化机推Tray气缸回异常，请确认!");
                            Down("X140", LogType.Alarm, (140 + i * 32).ToString() + "-" + (i + 1).ToString() + "#下层老化机推Tray气缸回异常，请确认!", "", 0, 0);
                        }
                    }
                    else
                    {
                    }

                    //下层老化机推Tray气缸出
                    if (Variable.OnEnable[122 + i * 32] && !Variable.XStatus[141 + i * 32])
                    {
                        if (++Variable.OffTime[141 + i * 32] >= 50)
                        {
                            Variable.AlarmHappen[141 + i * 32] = true;
                            ListBoxTxt((141 + i * 32).ToString() + "-" + (i + 1).ToString() + "#下层老化机推Tray气缸出异常，请确认!");
                            Down("X141", LogType.Alarm, (141 + i * 32).ToString() + "-" + (i + 1).ToString() + "#下层老化机推Tray气缸出异常，请确认!", "", 0, 0);
                        }
                    }
                    else
                    {
                    }
                    //下层老化机测试板加热1有效
                    if (Variable.ModelState[i * 4 + 2] == 1)
                    {
                        if ((Variable.OnEnable[125 + i * 32] && Variable.XStatus[130 + i * 32]) || Variable.TempOKFlag[i * 4 + 2])
                        {
                            if (++Variable.OnTime[130 + i * 32] >= 18000)
                            {
                                Variable.AlarmHappen[130 + i * 32] = true;
                                ListBoxTxt((130 + i * 32).ToString() + "-" + (i + 1).ToString() + "#下层老化机测试板加热内异常，请确认!");
                                Down("X130", LogType.Alarm, (130 + i * 32).ToString() + "-" + (i + 1).ToString() + "#下层老化机测试板加热内异常，请确认!", "", 0, 0);
                            }
                        }
                        else
                        {
                        }
                    }

                    //下层老化机测试板加热2有效
                    if (Variable.ModelState[i * 4 + 3] == 1)
                    {
                        if ((Variable.OnEnable[127 + i * 32] && Variable.XStatus[134 + i * 32]) || Variable.TempOKFlag[i * 4 + 3])
                        {
                            if (++Variable.OnTime[134 + i * 32] >= 18000)
                            {
                                Variable.AlarmHappen[134 + i * 32] = true;
                                ListBoxTxt((134 + i * 32).ToString() + "-" + (i + 1).ToString() + "#下层老化机测试板加热外异常，请确认!");
                                Down("X134", LogType.Alarm, (134 + i * 32).ToString() + "-" + (i + 1).ToString() + "#下层老化机测试板加热外异常，请确认!", "", 0, 0);
                            }
                        }
                        else
                        {
                        }
                    }
                }
                #endregion

                #endregion

                #region 气缸上升没到位报警气缸上下都断开，运行时气缸再上升

                if (Variable.MachineState == Variable.MachineStatus.Running)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        if (Variable.AireAlarm[i])//有报警
                        {
                            if (i % 4 == 0)
                            {
                                function.OutYOFF(101 + (i / 4) * 32);//上层1老化上下气缸下降关闭
                                function.OutYON(102 + (i / 4) * 32);//上层1老化上下气缸上升
                                Variable.AireAlarm[i] = false;
                            }
                            if (i % 4 == 1)
                            {
                                function.OutYOFF(103 + (i / 4) * 32);//上层2老化上下气缸下降关闭
                                function.OutYON(104 + (i / 4) * 32);//上层2老化上下气缸上升
                                Variable.AireAlarm[i] = false;
                            }
                            if (i % 4 == 2)
                            {
                                function.OutYOFF(117 + (i / 4) * 32);//下层1老化上下气缸下降关闭
                                function.OutYON(118 + (i / 4) * 32);//下层1老化上下气缸上升
                                Variable.AireAlarm[i] = false;
                            }
                            if (i % 4 == 3)
                            {
                                function.OutYOFF(119 + (i / 4) * 32);//下层2老化上下气缸下降关闭
                                function.OutYON(120 + (i / 4) * 32);//下层2老化上下气缸上升
                                Variable.AireAlarm[i] = false;
                            }
                        }
                    }
                }

                #endregion
            }
            #endregion

            #region 单机报警

            if (Variable.RunEnable == true && !Variable.ModelAlarm)
            {
                bool flag = false;
                int data = 0;
                for (int i = 0; i < 40; i++)
                {
                    string strPath0 = @"D:\Map\" + (i + 1).ToString();//共享文件夹
                    string destFn = "alarm.txt";//要读取Handler共享文件夹下文件的名称
                    string FullPath = strPath0 + "\\" + destFn;//共享
                    if (File.Exists(FullPath))
                    {
                        flag = true;
                        data = i;
                    }
                    else
                    {
                        flag = false;
                    }

                    if (flag)
                    {
                        try
                        {
                            string path = @"D:\Map\" + (data + 1).ToString() + @"\alarm";
                            string[] Readstr = myTXT.ReadTXT(path);
                            if (Readstr[0] == "1")
                            {
                                Variable.ModelAlarm = true;
                                ListBoxTxt((data / 4 + 1).ToString() + "号测试机写入VCQ电压失败，请确认！");
                                Down("X0", LogType.Alarm, (data / 4 + 1).ToString() + "号测试机写入VCQ电压失败，请确认！", "", 0, 0);
                            }
                            else if (Readstr[0] == "2")
                            {
                                Variable.ModelAlarm = true;
                                ListBoxTxt((data / 4 + 1).ToString() + "号测试机写入VCC电压失败，请确认！");
                                Down("X0", LogType.Alarm, (data / 4 + 1).ToString() + "号测试机写入VCC电压失败，请确认！", "", 0, 0);
                            }
                            else if (Readstr[0] == "3")
                            {
                                Variable.ModelAlarm = true;
                                ListBoxTxt((data / 4 + 1).ToString() + "号测试机良率低于设定值，请确认！");
                                Down("X0", LogType.Alarm, (data / 4 + 1).ToString() + "号测试机良率低于设定值，请确认！", "", 0, 0);
                            }
                            if (Variable.ModelAlarm)
                            {
                                string[] str = new string[1] { "0" };
                                string path1 = @"D:\Map\" + (data + 1).ToString() + @"\alarm";
                                myTXT.WriteTxt(str, path1);
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.SaveError(new StackTrace(new StackFrame(true)), new StackFrame(), ex);
                        }
                    }
                }
            }
            #endregion

            #region 其他报警

            //Robot
            //上层
            if (Variable.ModelSetStep == 16)
            {
                if (++Variable.OnTime[124] >= 2)
                {
                    Variable.AlarmHappen[124 + alarmNum * 32] = true;
                    ListBoxTxt((alarmNum + 1).ToString() + "#上层老化机侧顶气缸伸出，请检查！");
                    Down("X124", LogType.Alarm, (124 + alarmNum * 32).ToString() + "-" + (alarmNum + 1).ToString() + "#上层老化机侧顶气缸伸出，请检查！", "Variable.ModelSetStep", 15, 15);
                }
            }

            if (Variable.ModelSetStep == 17)
            {
                if (++Variable.OnTime[117] >= 2)
                {
                    Variable.AlarmHappen[117 + alarmNum * 32] = true;
                    ListBoxTxt((alarmNum + 1).ToString() + "#上层老化机内Tray盘未取走，请检查上层内Tray到位信号");
                    Down("X117", LogType.Alarm, (117 + alarmNum * 32).ToString() + "-" + (alarmNum + 1).ToString() + "#上层老化机内Tray盘未取走，请检查上层内Tray到位信号", "Variable.ModelSetStep", 15, 15);
                }
            }

            if (Variable.ModelSetStep == 18)
            {
                if (++Variable.OnTime[121] >= 2)
                {
                    Variable.AlarmHappen[121 + alarmNum * 32] = true;
                    ListBoxTxt((alarmNum + 1).ToString() + "#上层老化机外Tray盘未取走，请检查上层外Tray到位信号");
                    Down("X121", LogType.Alarm, (121 + alarmNum * 32).ToString() + "-" + (alarmNum + 1).ToString() + "#上层老化机外Tray盘未取走，请检查上层外Tray到位信号", "Variable.ModelSetStep", 15, 15);
                }
            }

            if (Variable.ModelSetStep == 165)
            {
                if (++Variable.OnTime[117] >= 2)
                {
                    Variable.AlarmHappen[117 + alarmNum * 32] = true;
                    ListBoxTxt((alarmNum + 1).ToString() + "#上老化机内Tray盘未到位");
                    Down("X117", LogType.Alarm, (117 + alarmNum * 32).ToString() + "-" + (alarmNum + 1).ToString() + "#上老化机内Tray盘未到位", "Variable.ModelSetStep", 160, 160);
                }
            }

            if (Variable.ModelSetStep == 166)
            {
                if (++Variable.OnTime[121] >= 2)
                {
                    Variable.AlarmHappen[121 + alarmNum * 32] = true;
                    ListBoxTxt((alarmNum + 1).ToString() + "#上老化机外Tray盘未到位");
                    Down("X121", LogType.Alarm, (121 + alarmNum * 32).ToString() + "-" + (alarmNum + 1).ToString() + "#上老化机外Tray盘未到位", "Variable.ModelSetStep", 160, 160);
                }
            }

            if (Variable.ModelSetStep == 143)
            {
                if (++Variable.OnTime[118] >= 2)
                {
                    Variable.AlarmHappen[118 + alarmNum * 32] = true;
                    ListBoxTxt((alarmNum + 1).ToString() + "#上老化机内上顶气缸未在下位");
                    Down("X118", LogType.Alarm, (118 + alarmNum * 32).ToString() + "-" + (alarmNum + 1).ToString() + "#上老化机内上顶气缸未在下位", "Variable.ModelSetStep", 146, 146);
                }
            }

            if (Variable.ModelSetStep == 144)
            {
                if (++Variable.OnTime[122] >= 2)
                {
                    Variable.AlarmHappen[122 + alarmNum * 32] = true;
                    ListBoxTxt((alarmNum + 1).ToString() + "#上老化机外上顶气缸未在下位");
                    Down("X122", LogType.Alarm, (122 + alarmNum * 32).ToString() + "-" + (alarmNum + 1).ToString() + "#上老化机内上顶气缸未在下位", "Variable.ModelSetStep", 146, 146);
                }
            }

            //下层
            if (Variable.ModelSetStep == 12)
            {
                if (++Variable.OnTime[138] >= 2)
                {
                    Variable.AlarmHappen[138 + alarmNum * 32] = true;
                    ListBoxTxt((alarmNum + 1).ToString() + "#下层老化机侧顶气缸伸出，请检查！");
                    Down("X138", LogType.Alarm, (138 + alarmNum * 32).ToString() + "-" + (alarmNum + 1).ToString() + "#下层老化机侧顶气缸伸出，请检查！", "Variable.ModelSetStep", 15, 15);
                }
            }

            if (Variable.ModelSetStep == 13)
            {
                if (++Variable.OnTime[131] >= 2)
                {
                    Variable.AlarmHappen[131 + alarmNum * 32] = true;
                    ListBoxTxt((alarmNum + 1).ToString() + "#下层老化机内Tray盘未取走，请检查上层内Tray到位信号");
                    Down("X131", LogType.Alarm, (131 + alarmNum * 32).ToString() + "-" + (alarmNum + 1).ToString() + "#下层老化机内Tray盘未取走，请检查上层内Tray到位信号", "Variable.ModelSetStep", 15, 15);
                }
            }

            if (Variable.ModelSetStep == 14)
            {
                if (++Variable.OnTime[135] >= 2)
                {
                    Variable.AlarmHappen[135 + alarmNum * 32] = true;
                    ListBoxTxt((alarmNum + 1).ToString() + "#下层老化机外Tray盘未取走，请检查上层外Tray到位信号");
                    Down("X135", LogType.Alarm, (135 + alarmNum * 32).ToString() + "-" + (alarmNum + 1).ToString() + "#下层老化机外Tray盘未取走，请检查上层外Tray到位信号", "Variable.ModelSetStep", 15, 15);
                }
            }

            if (Variable.ModelSetStep == 167)
            {
                if (++Variable.OnTime[131] >= 2)
                {
                    Variable.AlarmHappen[131 + alarmNum * 32] = true;
                    ListBoxTxt((alarmNum + 1).ToString() + "#下老化机内Tray盘未到位");
                    Down("X131", LogType.Alarm, (131 + alarmNum * 32).ToString() + "-" + (alarmNum + 1).ToString() + "#下老化机内Tray盘未到位", "Variable.ModelSetStep", 160, 160);
                }
            }

            if (Variable.ModelSetStep == 168)
            {
                if (++Variable.OnTime[135] >= 2)
                {
                    Variable.AlarmHappen[135 + alarmNum * 32] = true;
                    ListBoxTxt((alarmNum + 1).ToString() + "#下老化机外Tray盘未到位");
                    Down("X135", LogType.Alarm, (135 + alarmNum * 32).ToString() + "-" + (alarmNum + 1).ToString() + "#下老化机外Tray盘未到位", "Variable.ModelSetStep", 160, 160);
                }
            }

            if (Variable.ModelSetStep == 147)
            {
                if (++Variable.OnTime[132] >= 2)
                {
                    Variable.AlarmHappen[132 + alarmNum * 32] = true;
                    ListBoxTxt((alarmNum + 1).ToString() + "#下老化机内上顶气缸未在下位");
                    Down("X132", LogType.Alarm, (132 + alarmNum * 32).ToString() + "-" + (alarmNum + 1).ToString() + "#下老化机内上顶气缸未在下位", "Variable.ModelSetStep", 146, 146);
                }
            }

            if (Variable.ModelSetStep == 148)
            {
                if (++Variable.OnTime[136] >= 2)
                {
                    Variable.AlarmHappen[136 + alarmNum * 32] = true;
                    ListBoxTxt((alarmNum + 1).ToString() + "#下老化机外上顶气缸未在下位");
                    Down("X136", LogType.Alarm, (136 + alarmNum * 32).ToString() + "-" + (alarmNum + 1).ToString() + "#下老化机外上顶气缸未在下位", "Variable.ModelSetStep", 146, 146);
                }
            }

            //上层
            if (Variable.ModelGetStep == 16)
            {
                if (++Variable.OnTime[121] >= 2)
                {
                    Variable.AlarmHappen[121 + alarmNum * 32] = true;
                    ListBoxTxt((alarmNum + 1).ToString() + "#上层老化机外Tray盘未取走，请检查上层外Tray到位信号");
                    Down("X121", LogType.Alarm, (121 + alarmNum * 32).ToString() + "-" + (alarmNum + 1).ToString() + "#上层老化机外Tray盘未取走，请检查上层外Tray到位信号", "Variable.ModelGetStep", 15, 15);
                }
            }

            if (Variable.ModelGetStep == 65)
            {
                if (++Variable.OnTime[117] >= 2)
                {
                    Variable.AlarmHappen[117 + alarmNum * 32] = true;
                    ListBoxTxt((alarmNum + 1).ToString() + "#上层老化机内Tray盘未取走");
                    Down("X117", LogType.Alarm, (117 + alarmNum * 32).ToString() + "-" + (alarmNum + 1).ToString() + "#上层老化机内Tray盘未取走", "Variable.ModelGetStep", 60, 60);
                }
            }

            if (Variable.ModelGetStep == 66)
            {
                if (++Variable.OnTime[121] >= 2)
                {
                    Variable.AlarmHappen[121 + alarmNum * 32] = true;
                    ListBoxTxt((alarmNum + 1).ToString() + "#上层老化机外Tray盘未取走");
                    Down("X121", LogType.Alarm, (121 + alarmNum * 32).ToString() + "-" + (alarmNum + 1).ToString() + "#上层老化机外Tray盘未取走", "Variable.ModelGetStep", 60, 60);
                }
            }
            //下层
            if (Variable.ModelGetStep == 17)
            {
                if (++Variable.OnTime[135] >= 2)
                {
                    Variable.AlarmHappen[135 + alarmNum * 32] = true;
                    ListBoxTxt((alarmNum + 1).ToString() + "#下层老化机外Tray盘未取走，请检查下层外Tray到位信号");
                    Down("X135", LogType.Alarm, (135 + alarmNum * 32).ToString() + "-" + (alarmNum + 1).ToString() + "#下层老化机外Tray盘未取走，请检查下层外Tray到位信号", "Variable.ModelGetStep", 15, 15);
                }
            }

            if (Variable.ModelGetStep == 67)
            {
                if (++Variable.OnTime[131] >= 2)
                {
                    Variable.AlarmHappen[131 + alarmNum * 32] = true;
                    ListBoxTxt((alarmNum + 1).ToString() + "#下层老化机内Tray盘未取走");
                    Down("X131", LogType.Alarm, (131 + alarmNum * 32).ToString() + "-" + (alarmNum + 1).ToString() + "#下层老化机内Tray盘未取走", "Variable.ModelGetStep", 60, 60);
                }
            }

            if (Variable.ModelGetStep == 68)
            {
                if (++Variable.OnTime[135] >= 2)
                {
                    Variable.AlarmHappen[135 + alarmNum * 32] = true;
                    ListBoxTxt((alarmNum + 1).ToString() + "#下层老化机外Tray盘未取走");
                    Down("X135", LogType.Alarm, (135 + alarmNum * 32).ToString() + "-" + (alarmNum + 1).ToString() + "#下层老化机外Tray盘未取走", "Variable.ModelGetStep", 60, 60);
                }
            }

            //缺料
            if (Variable.ModelGetStep == 195)
            {
                if (++Variable.OnTime[124] >= 2)
                {
                    Variable.PhotoAlarm = true;
                    ListBoxTxt("料盘上缺料，检查是否掉料或模组粘料!");
                    Down("X0", LogType.Alarm1, "料盘上缺料，检查是否掉料或模组粘料!", "Variable.ModelGetStep", 200, 190);
                }
            }


            //拍照
            if (Variable.ModelSetStep == 95)
            {
                if (++Variable.OnTime[124] >= 2)
                {
                    Variable.PhotoAlarm = true;
                    ListBoxTxt("拍照NG，请确认Tray盘!");
                    Down("X0", LogType.Alarm1, "拍照NG，请确认Tray盘!", "Variable.ModelSetStep", 96, 80);
                }
            }

            if (Variable.ModelSetStep == 135)
            {
                if (++Variable.OnTime[124] >= 2)
                {
                    Variable.PhotoAlarm = true;
                    ListBoxTxt("拍照NG，请确认Tray盘!");
                    Down("X0", LogType.Alarm1, "拍照NG，请确认Tray盘!", "Variable.ModelSetStep", 136, 120);
                }
            }

            if (Variable.ModelGetStep == 135)
            {
                if (++Variable.OnTime[124] >= 2)
                {
                    Variable.PhotoAlarm = true;
                    ListBoxTxt("拍照NG，请确认Tray盘!");
                    Down("X0", LogType.Alarm1, "拍照NG，请确认Tray盘!", "Variable.ModelGetStep", 136, 120);
                }
            }

            if (Variable.ModelGetStep == 175)
            {
                if (++Variable.OnTime[124] >= 2)
                {
                    Variable.PhotoAlarm = true;
                    ListBoxTxt("拍照NG，请确认Tray盘!");
                    Down("X0", LogType.Alarm1, "拍照NG，请确认Tray盘!", "Variable.ModelGetStep", 176, 160);
                }
            }

            //扫码
            if (Variable.INAutoReadyStep == 45)
            {
                if (++Variable.OnTime[124] >= 2)
                {
                    Variable.QRAlarm = true;
                    ListBoxTxt("扫码NG，请确认Tray盘!");
                    Down("X0", LogType.Alarm1, "扫码NG，请确认Tray盘!", "Variable.INAutoReadyStep", 60, 30);
                }
            }

            if (Variable.INAutoReadyStep == 55)
            {
                if (++Variable.OnTime[124] >= 2)
                {
                    Variable.QRAlarm = true;
                    ListBoxTxt("该Tray盘在数据库没有信息，请确认Tray盘！");
                    Down("X0", LogType.Alarm, "该Tray盘在数据库没有信息，请确认Tray盘！", "Variable.INAutoReadyStep", 56, 30);
                }
            }

            if (Variable.INAutoReadyStep == 61)
            {
                if (++Variable.OnTime[124] >= 2)
                {
                    Variable.QRAlarm = true;
                    ListBoxTxt("Tray盘尺寸与设定不符合，请确认Tray盘！");
                    Down("X0", LogType.Alarm, "Tray盘尺寸与设定不符合，请确认Tray盘！", "Variable.INAutoReadyStep", 56, 30);
                }
            }

            //上料空Tray
            if (Variable.INAutoEmptyStartStep == 15)
            {
                if (++Variable.OnTime[32] >= 2)
                {
                    Variable.AlarmHappen[32] = true;
                    ListBoxTxt("X32-上料空Tray工位1有Tray盘,请取走!");
                    Down("X32", LogType.Alarm, "X32-上料空Tray工位1有Tray盘,请取走!", "Variable.INAutoEmptyStartStep", 10, 10);
                }
            }

            if (Variable.INAutoEmptyStartStep == 75)
            {
                if (++Variable.OnTime[32] >= 2)
                {
                    Variable.AlarmHappen[32] = true;
                    ListBoxTxt("X32-上料空Tray工位1没有Tray盘,请确认!");
                    Down("X32", LogType.Alarm, "X32-上料空Tray工位1没有Tray盘,请确认!", "Variable.INAutoEmptyStartStep", 10, 10);
                }
            }

            if (Variable.INAutoEmptyStartStep == 225)
            {
                if (++Variable.OnTime[38] >= 2)
                {
                    Variable.AlarmHappen[38] = true;
                    ListBoxTxt("X38-上料空Tray工位3Tray盘已满，请取走!");
                    Down("X38", LogType.Alarm, "X38-上料空Tray工位3Tray盘已满，请取走!", "Variable.INAutoEmptyStartStep", 230, 230);
                }
            }
            if (Variable.INAutoEmptyStartStep == 212)
            {
                if (++Variable.OnTime[37] >= 2)
                {
                    Variable.AlarmHappen[37] = true;
                    ListBoxTxt("X37-上料空Tray工位3有料感应，请取走！");
                    Down("X37", LogType.Alarm, "X37-上料空Tray工位3有料感应，请取走！", "Variable.INAutoEmptyStartStep", 210, 210);
                }
            }

            //上料待测Tray
            if (Variable.INAutoReady1Step == 11)
            {
                if (++Variable.OnTime[44] >= 2)
                {
                    Variable.AlarmHappen[44] = true;
                    ListBoxTxt("X44-上料待测Tray工位1Tray盘超过数量,请取走!");
                    Down("X44", LogType.Alarm, "X44-上料待测Tray工位1Tray盘超过数量,请取走!", "Variable.INAutoReady1Step", 10, 10);
                }
            }

            if (Variable.INAutoReady1Step == 16)
            {
                if (++Variable.OnTime[124] >= 2)
                {
                    Variable.AlarmHappen[124] = true;
                    ListBoxTxt("X43-上料待测Tray工位1有Tray盘,请取走!");
                    Down("X43", LogType.Alarm, "X43-上料待测Tray工位1有Tray盘,请取走!", "Variable.INAutoReady1Step", 15, 15);
                }
            }

            if (Variable.INAutoReady1Step == 105)
            {
                if (++Variable.OnTime[44] >= 2)
                {
                    Variable.AlarmHappen[44] = true;
                    ListBoxTxt("X44-上料待测Tray工位3Tray盘超过数量,请取走!");
                    Down("X44", LogType.Alarm, "X44-上料待测Tray工位3Tray盘超过数量,请取走!", "Variable.INAutoReady1Step", 100, 100);
                }
            }

            if (Variable.INAutoReady1Step == 115)
            {
                if (++Variable.OnTime[51] >= 2)
                {
                    Variable.AlarmHappen[51] = true;
                    ListBoxTxt("X44-上料待测Tray工位3有Tray盘,请取走!");
                    Down("X44", LogType.Alarm, "X44-上料待测Tray工位3有Tray盘,请取走!", "Variable.INAutoReady1Step", 110, 110);
                }
            }

            if (Variable.INAutoReady1Step == 185)
            {
                if (++Variable.OnTime[51] >= 2)
                {
                    Variable.AlarmHappen[51] = true;
                    ListBoxTxt("X51-上料待测Tray工位1和工位3都没有Tray盘，请放置!");
                    Down("X51", LogType.Alarm, "X51-上料待测Tray工位1和工位3都没有Tray盘，请放置!", "Variable.INAutoReady1Step", 10, 10);
                }
            }

            if (Variable.INAutoReadyStep == 156)
            {
                if (++Variable.OnTime[57] >= 2)
                {
                    Variable.AlarmHappen[57] = true;
                    ListBoxTxt("X57-A吸嘴吸NG料真空异常，请确认！");
                    Down("X57", LogType.Alarm1, "X57-A吸嘴吸NG料真空异常，请确认！", "Variable.INAutoReadyStep", 90, 110);
                }
            }

            if (Variable.INAutoReadyStep == 215)
            {
                if (++Variable.OnTime[58] >= 2)
                {
                    Variable.AlarmHappen[58] = true;
                    ListBoxTxt("X58-B吸嘴吸NG料真空异常，请确认！");
                    Down("X58", LogType.Alarm1, "X58-B吸嘴吸NG料真空异常，请确认！", "Variable.INAutoReadyStep", 90, 170);
                }
            }

            if (Variable.INAutoReadyStep == 526)
            {
                if (++Variable.OnTime[47] >= 2)
                {
                    Variable.AlarmHappen[47] = true;
                    ListBoxTxt("X47-上料待测工位2Tray上顶Tray未感应到！");
                    Down("X47", LogType.Alarm, "X47-上料待测工位2Tray上顶Tray未感应到！", "Variable.INAutoReadyStep", 525, 525);
                }
            }

            //下料良品
            if (Variable.OutAutoOKStartStep == 950)
            {
                if (++Variable.OnTime[599] >= 2)
                {
                    Variable.AlarmHappen[599] = true;
                    ListBoxTxt("此盘NG数量过多,请取走!");
                    Down("X0", LogType.Message, "此盘NG数量过多,请取走!", "Variable.OutAutoOKStartStep", 10, 10);
                }
            }

            if (Variable.OutAutoOKStartStep == 62)
            {
                if (++Variable.OnTime[69] >= 2)
                {
                    Variable.AlarmHappen[69] = true;
                    ListBoxTxt("X69-下料良品工位2未感应到Tray，请确认！");
                    Down("X69", LogType.Alarm, "X69-下料良品工位2未感应到Tray，请确认！", "Variable.OutAutoOKStartStep", 60, 60);
                }
            }

            if (Variable.OutAutoOKStartStep == 63)
            {
                if (++Variable.OnTime[72] >= 2)
                {
                    Variable.AlarmHappen[72] = true;
                    ListBoxTxt("X72-下料良品工位2上顶气缸不在下位！");
                    Down("X72", LogType.Alarm, "X72-下料良品工位2上顶气缸不在下位！", "Variable.OutAutoOKStartStep", 60, 60);
                }
            }

            if (Variable.OutAutoOKStartStep == 95)
            {
                if (++Variable.OnTime[74] >= 2)
                {
                    Variable.AlarmHappen[74] = true;
                    ListBoxTxt("X74-下料良品Tray盘工位3未感应到产品！");
                    Down("X74", LogType.Alarm, "X74-下料良品Tray盘工位3未感应到产品！", "Variable.OutAutoOKStartStep", 90, 90);
                }
            }

            if (Variable.OutAutoOKStartStep == 265)
            {
                if (++Variable.OnTime[96] >= 2)
                {
                    Variable.AlarmHappen[96] = true;
                    ListBoxTxt("X96-A吸嘴吸NG料真空异常，请确认！");
                    Down("X96", LogType.Alarm1, "X96-A吸嘴吸NG料真空异常，请确认！", "Variable.OutAutoOKStartStep", 200, 220);
                }
            }

            if (Variable.OutAutoOKStartStep == 325)
            {
                if (++Variable.OnTime[97] >= 2)
                {
                    Variable.AlarmHappen[97] = true;
                    ListBoxTxt("X97-B吸嘴吸NG料真空异常，请确认！");
                    Down("X97", LogType.Alarm1, "X97-B吸嘴吸NG料真空异常，请确认！", "Variable.OutAutoOKStartStep", 200, 280);
                }
            }

            if (Variable.OutAutoOKStartStep == 555)
            {
                if (++Variable.OnTime[96] >= 2)
                {
                    Variable.AlarmHappen[96] = true;
                    ListBoxTxt("X96-A吸嘴吸NG料真空异常，请确认！");
                    Down("X96", LogType.Alarm1, "X96-A吸嘴吸NG料真空异常，请确认！", "Variable.OutAutoOKStartStep", 505, 510);
                }
            }

            if (Variable.OutAutoOKStartStep == 605)
            {
                if (++Variable.OnTime[97] >= 2)
                {
                    Variable.AlarmHappen[97] = true;
                    ListBoxTxt("X97-B吸嘴吸NG料真空异常，请确认！");
                    Down("X97", LogType.Alarm1, "X97-B吸嘴吸NG料真空异常，请确认！", "Variable.OutAutoOKStartStep", 560, 570);
                }
            }

            if (Variable.OutAutoOKStartStep == 825)
            {
                if (++Variable.OnTime[64] >= 2)
                {
                    Variable.AlarmHappen[64] = true;
                    ListBoxTxt("X64-下料良品Tray未到工位1，请确认！");
                    Down("X64", LogType.Alarm, "X64-下料良品Tray未到工位1，请确认！", "Variable.OutAutoOKStartStep", 820, 820);
                }
            }

            if (Variable.OutAutoOKStartStep == 875)
            {
                if (++Variable.OnTime[65] >= 2)
                {
                    Variable.AlarmHappen[65] = true;
                    ListBoxTxt("X65-下料良品Tray工位1Tray盘已满，请取走！");
                    Down("X65", LogType.Alarm, "X65-下料良品Tray工位1Tray盘已满，请取走！", "Variable.OutAutoOKStartStep", 900, 870);
                }
            }

            //下料补料
            if (Variable.OutAutoFillStartStep == 33)
            {
                if (++Variable.OnTime[83] >= 2)
                {
                    Variable.AlarmHappen[83] = true;
                    ListBoxTxt("X83-下料补料Tray盘工位3Tray未取走,请确认！");
                    Down("X83", LogType.Alarm, "X83-下料补料Tray盘工位3Tray未取走,请确认！", "Variable.OutAutoFillStartStep", 31, 31);
                }
            }

            if (Variable.OutAutoFillStartStep == 96)
            {
                if (++Variable.OnTime[96] >= 2)
                {
                    Variable.AlarmHappen[96] = true;
                    ListBoxTxt("X96-A吸嘴吸NG料真空异常，请确认！");
                    Down("X96", LogType.Alarm1, "X96-A吸嘴吸NG料真空异常，请确认！", "Variable.OutAutoFillStartStep", 40, 60);
                }
            }

            if (Variable.OutAutoFillStartStep == 145)
            {
                if (++Variable.OnTime[97] >= 2)
                {
                    Variable.AlarmHappen[97] = true;
                    ListBoxTxt("X97-B吸嘴吸NG料真空异常，请确认！");
                    Down("X97", LogType.Alarm1, "X97-B吸嘴吸NG料真空异常，请确认！", "Variable.OutAutoFillStartStep", 40, 110);
                }
            }

            if (Variable.OutAutoFillStartStep == 425)
            {
                if (++Variable.OnTime[78] >= 2)
                {
                    Variable.AlarmHappen[78] = true;
                    ListBoxTxt("X78-下料补料空Tray未到工位1，请确认！");
                    Down("X78", LogType.Alarm, "X78-下料补料空Tray未到工位1，请确认！", "Variable.OutAutoFillStartStep", 430, 420);
                }
            }

            if (Variable.OutAutoFillStartStep == 455)
            {
                if (++Variable.OnTime[79] >= 2)
                {
                    Variable.AlarmHappen[79] = true;
                    ListBoxTxt("X79-下料补料空Tray未到工位1，请确认！");
                    Down("X79", LogType.Alarm, "X79-下料补料空Tray未到工位1，请确认！", "Variable.OutAutoFillStartStep", 10, 450);
                }
            }

            //下料NG
            if (Variable.OutAutoNGStartStep == 15)
            {
                if (++Variable.OnTime[86] >= 2)
                {
                    Variable.AlarmHappen[86] = true;
                    ListBoxTxt("X86-下料NGTray工位1有Tray盘,请取走！");
                    Down("X86", LogType.Alarm, "X86-下料NGTray工位1有Tray盘,请取走！", "Variable.OutAutoNGStartStep", 10, 10);
                }
            }

            if (Variable.OutAutoNGStartStep == 65)
            {
                if (++Variable.OnTime[86] >= 2)
                {
                    Variable.AlarmHappen[86] = true;
                    ListBoxTxt("X86-下料NG空Tray工位1没有Tray盘！");
                    Down("X86", LogType.Alarm, "X86-下料NG空Tray工位1没有Tray盘！", "Variable.OutAutoNGStartStep", 10, 60);
                }
            }

            if (Variable.OutAutoNGStartStep == 195)
            {
                if (++Variable.OnTime[90] >= 2)
                {
                    Variable.AlarmHappen[90] = true;
                    ListBoxTxt("X90-下料NGTray盘工位3Tray未感应到！");
                    Down("X90", LogType.Alarm, "X90-下料NGTray盘工位3Tray未感应到！", "Variable.OutAutoNGStartStep", 200, 190);
                }
            }

            if (Variable.OutAutoNGStartStep == 225)
            {
                if (++Variable.OnTime[91] >= 2)
                {
                    Variable.AlarmHappen[91] = true;
                    ListBoxTxt("X91-下料空Tray工位3Tray盘已满，请取走！");
                    Down("X91", LogType.Alarm, "X91-下料空Tray工位3Tray盘已满，请取走！", "Variable.OutAutoNGStartStep", 10, 220);
                }
            }

            //安全位判断
            if (Variable.UpAxisAlarm)
            {
                ListBoxTxt("上料吸嘴Z轴不在待机位，请确认！");
                Down("X0", LogType.Alarm, "上料吸嘴Z轴不在待机位，请确认！", "", 0, 0);
            }

            if (Variable.AlarmHappen[59])
            {
                ListBoxTxt("上料吸嘴气缸不在上位，请确认！");
                Down("X0", LogType.Alarm, "上料吸嘴气缸不在上位，请确认！", "", 0, 0);
            }

            if (Variable.AlarmHappen[49])
            {
                ListBoxTxt("上料待测Tray工位2上顶气缸不在下位，请确认！");
                Down("X0", LogType.Alarm, "上料待测Tray工位2上顶气缸不在下位，请确认！", "", 0, 0);
            }

            if (Variable.DownAxisAlarm)
            {
                ListBoxTxt("下料吸嘴Z轴不在待机位，请确认！");
                Down("X0", LogType.Alarm, "下料吸嘴Z轴不在待机位，请确认！", "", 0, 0);
            }

            if (Variable.AlarmHappen[98])
            {
                ListBoxTxt("下料吸嘴气缸不在上位，请确认！");
                Down("X0", LogType.Alarm, "下料吸嘴气缸不在上位，请确认！", "", 0, 0);
            }

            if (Variable.AlarmHappen[72])
            {
                ListBoxTxt("下料良品Tray盘工位2上顶气缸不在下位，请确认！");
                Down("X0", LogType.Alarm, "下料良品Tray盘工位2上顶气缸不在下位，请确认！", "", 0, 0);
            }

            if (Variable.AlarmHappen[102])
            {
                ListBoxTxt("移Tray夹爪上下气缸不在上位，请确认！");
                Down("X0", LogType.Alarm, "移Tray夹爪上下气缸不在上位，请确认！", "", 0, 0);
            }

            //探针次数达到设定值
            for (int i = 0; i < 40; i++)
            {
                if (Variable.ProbeNum[i] > Variable.ProbeSet[i])
                {
                    Variable.ProbeAlarm = true;
                    if (i % 4 == 0)
                    {
                        ListBoxTxt("测试机上层内探针次数达到设定值，请确认！");
                        Down("X0", LogType.Alarm, "测试机上层内探针次数达到设定值，请确认！", "", 0, 0);
                    }
                    else if (i % 4 == 1)
                    {
                        ListBoxTxt("测试机上层外探针次数达到设定值，请确认！");
                        Down("X0", LogType.Alarm, "测试机上层外探针次数达到设定值，请确认！", "", 0, 0);
                    }
                    else if (i % 4 == 2)
                    {
                        ListBoxTxt("测试机下层内探针次数达到设定值，请确认！");
                        Down("X0", LogType.Alarm, "测试机下层内探针次数达到设定值，请确认！", "", 0, 0);
                    }
                    else if (i % 4 == 3)
                    {
                        ListBoxTxt("测试机下层外探针次数达到设定值，请确认！");
                        Down("X0", LogType.Alarm, "测试机下层外探针次数达到设定值，请确认！", "", 0, 0);
                    }
                }

            }

            if (Variable.MachineState == Variable.MachineStatus.Running)
            {
                if (Variable.XAlarmTime[1] > Variable.XAlarmDelay)
                {
                    ListBoxTxt("上料待测Tray工位2上顶气缸不在下位，请确认！");
                    Down("X0", LogType.Alarm, "上料待测Tray工位2上顶气缸不在下位，请确认！", "", 0, 0);
                }
            }
            if (Variable.MachineState == Variable.MachineStatus.Running)
            {
                if (Variable.XAlarmTime[2] > Variable.XAlarmDelay)
                {
                    ListBoxTxt("下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！");
                    Down("X0", LogType.Alarm, "下料吸嘴Z轴不在待机位或下料吸嘴气缸不在上位，请确认！", "", 0, 0);
                }
            }
            if (Variable.MachineState == Variable.MachineStatus.Running)
            {
                if (Variable.XAlarmTime[3] > Variable.XAlarmDelay)
                {
                    ListBoxTxt("下料良品Tray盘工位2上顶气缸不在下位，请确认！");
                    Down("X0", LogType.Alarm, "下料良品Tray盘工位2上顶气缸不在下位，请确认！", "", 0, 0);
                }
            }
            if (Variable.MachineState == Variable.MachineStatus.Running)
            {
                if (Variable.XAlarmTime[4] > Variable.XAlarmDelay)
                {
                    ListBoxTxt("上料待测Tray工位2上顶气缸不在下位，请确认！");
                    Down("X0", LogType.Alarm, "上料待测Tray工位2上顶气缸不在下位，请确认！", "", 0, 0);
                }
            }
            //结批
            if (Variable.CleanOutFlag)
            {
                Down("X0", LogType.Message, "结批完成，请取走所有Tray盘，复位重新开始", "", 0, 0);
            }
            #endregion

            #region 轴未运动到位
            for (int i = 0; i < 16; i++)
            {
                //伺服驱动器报警
                if (Variable.MachineState == Variable.MachineStatus.Running)
                {
                    if (Variable.AxisAlarmTime[i] > Variable.axisAlarmDelay)
                    {
                        ListBoxTxt("轴" + (i + 1).ToString() + "伺服未运动到位，请确认！");
                        Down("轴" + (i + 1).ToString(), LogType.Alarm, "轴" + (i + 1).ToString() + "伺服未运动到位，请确认！", "", 0, 0);
                    }
                }
            }
            #endregion

            #region 报警复位

            //报警复位
            if (Variable.btnReset || Variable.AlarmClrButton || Variable.PauseButton == true || Variable.btnStop == true)//复位按钮
            {
                //核1、2清除轴状态
                mc.GTN_ClrSts(1, 1, 12);  //核1、2清除轴状态
                mc.GTN_ClrSts(2, 1, 12);

                for (int i = 0; i < 26; i++)
                {
                    Variable.ServoAlarmHappen[i + 1] = false;
                    Variable.PlimitAlarmHappen[i + 1] = false;
                    Variable.NlimitAlarmHappen[i + 1] = false;
                    Variable.AxisPTime[i] = 0;
                    Variable.AxisNTime[i] = 0;
                }

                Variable.PhotoAlarm = false;
                Variable.UpAxisAlarm = false;
                Variable.DownAxisAlarm = false;
                Variable.RobotAlarm = false;
                Variable.ModelAlarm = false;
                Variable.ProbeAlarm = false;
                Variable.QRAlarm = false;
                //Variable.AlarmFlag = false;
                stopRobotFlag = false;
                Variable.CleanOutFlag = false;

                //报警集合初始化
                AlarmListReset();
                listAlarm.Items.Clear();

                Variable.btnReset = false;
            }

            #endregion


        }

        #endregion

        #region 判断Listbox有没有重复的内容
        public bool ListBoxAlarm(string Alm)//true:没有重复
        {
            bool arm = true;
            if (Alm != null && Alm != "")
            {
                for (int i = 0; i < listAlarm.Items.Count; i++)
                {
                    //string[] str = listAlarm.Items[i].ToString().Split(' ');
                    if (listAlarm.Items[i].ToString() == Alm)
                    {
                        arm = false;
                        break;
                    }
                    else
                    {
                        arm = true;
                    }
                }
            }
            return arm;
        }

        public void RSDAlarmINAutoEmptyStep(string alarmMess)//true:没有重复
        {
            if (Variable.StepMsg[1] != alarmMess)
            {
                Log.SaveLog(LogType.INAutoEmptyStartStep, alarmMess);
                Variable.StepMsg[1] = alarmMess;
            }
        }
        public void RSDAlarmINAutoReady1Step(string alarmMess)//true:没有重复
        {
            if (Variable.StepMsg[2] != alarmMess)
            {
                Log.SaveLog(LogType.INAutoReady1Step, alarmMess);
                Variable.StepMsg[2] = alarmMess;
            }
        }
        public void RSDAlarmINAutoReadyStep(string alarmMess)//true:没有重复
        {
            if (Variable.StepMsg[3] != alarmMess)
            {
                Log.SaveLog(LogType.INAutoReadyStep, alarmMess);
                Variable.StepMsg[3] = alarmMess;
            }
        }
        public void RSDAlarmOutAutoOKStartStep(string alarmMess)//true:没有重复
        {
            if (Variable.StepMsg[4] != alarmMess)
            {
                Log.SaveLog(LogType.OutAutoOKStartStep, alarmMess);
                Variable.StepMsg[4] = alarmMess;
            }
        }
        public void RSDAlarmOutAutoFillStartStep(string alarmMess)//true:没有重复
        {
            if (Variable.StepMsg[5] != alarmMess)
            {
                Log.SaveLog(LogType.OutAutoFillStartStep, alarmMess);
                Variable.StepMsg[5] = alarmMess;
            }
        }
        public void RSDAlarmOutAutoNGStartStep(string alarmMess)//true:没有重复
        {
            if (Variable.StepMsg[6] != alarmMess)
            {
                Log.SaveLog(LogType.OutAutoNGStartStep, alarmMess);
                Variable.StepMsg[6] = alarmMess;
            }
        }
        public void RSDAlarmRobotAutoStartStep(string alarmMess)//true:没有重复
        {
            if (Variable.StepMsg[7] != alarmMess)
            {
                Log.SaveLog(LogType.RobotAutoStartStep, alarmMess);
                Variable.StepMsg[7] = alarmMess;
            }
        }
        public void RSDAlarmRobotAutoPutStep(string alarmMess)//true:没有重复
        {
            if (Variable.StepMsg[8] != alarmMess)
            {
                Log.SaveLog(LogType.RobotAutoPutStep, alarmMess);
                Variable.StepMsg[8] = alarmMess;
            }
        }
        public void RSDAlarmRobotAutoTakeStep(string alarmMess)//true:没有重复
        {
            if (Variable.StepMsg[9] != alarmMess)
            {
                Log.SaveLog(LogType.RobotAutoTakeStep, alarmMess);
                Variable.StepMsg[9] = alarmMess;
            }
        }
        #endregion

        #region Listbox增加没重复的内容
        public void ListBoxTxt(string Alm)
        {
            if (ListBoxAlarm(Alm) == true && Alm != null)
            {
                listAlarm.Items.Add(this.GetDisplayString(Alm));
            }
        }
        #endregion

        #region 报警显示内容
        public string GetDisplayString(string str)
        {
            //string timestring = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "   " + str ;
            string timestring = str;
            return timestring;
        }

        #endregion

        #region 报警集合初始化

        public void AlarmListReset()
        {
            for (int i = 0; i < 600; i++)
            {
                Variable.AlarmHappen[i] = false;
                //Variable.OnEnable[i] = false;
                //Variable.OffEnable[i] = false;
                Variable.OnTime[i] = 0;
                Variable.OffTime[i] = 0;
                if (i < 27)
                {
                    Variable.AxisAlarmTime[i] = 0;
                }
            }
        }
        #endregion

        #region 判断有没有报警True
        public bool AlarmTrue()
        {
            bool b = false;
            try
            {
                for (int i = 0; i < Variable.AlarmHappen.Length; i++)
                {
                    if (Variable.AlarmHappen[i])
                    {
                        b = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(new StackTrace(new StackFrame(true)), new StackFrame(), ex);
            }
            return b;
        }

        #endregion


        #region 判断轴控制器有没有报警True
        public bool AxisAlarmTrue()
        {
            bool b = false;
            try
            {
                for (int i = 1; i < Variable.ServoAlarmHappen.Length; i++)
                {
                    if (Variable.ServoAlarmHappen[i])
                    {
                        b = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(new StackTrace(new StackFrame(true)), new StackFrame(), ex);
            }
            return b;
        }

        #endregion

        #region 判断轴正极限有没有报警True
        public bool AxisPAlarmTrue()
        {
            bool b = false;
            try
            {
                for (int i = 0; i < Variable.PlimitAlarmHappen.Length; i++)
                {
                    if (Variable.PlimitAlarmHappen[i])
                    {
                        b = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(new StackTrace(new StackFrame(true)), new StackFrame(), ex);
            }
            return b;
        }

        #endregion

        #region 判断轴负极限有没有报警True
        public bool AxisNAlarmTrue()
        {
            bool b = false;
            try
            {
                for (int i = 0; i < Variable.NlimitAlarmHappen.Length; i++)
                {
                    if (Variable.NlimitAlarmHappen[i])
                    {
                        b = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(new StackTrace(new StackFrame(true)), new StackFrame(), ex);
            }
            return b;
        }

        #endregion

        #region 报警弹出
        public void Down(string X, Enum logType, string content, string step, int cancelStep, int sureStep)
        {
            if (!Variable.AlarmFlag)
            {
                if (logType.ToString() != "Operate")
                {
                    if (logType.ToString() == "Alarm1")
                    {
                        Thread.Sleep(200);
                        //Form POPFormIsOpenOrNot = Application.OpenForms["POPForm"];
                        //if ((POPFormIsOpenOrNot == null) || (POPFormIsOpenOrNot.IsDisposed))//如果没有创建过或者窗体已经被释放
                        //{
                        pop = new POPForm();
                        pop.StartPosition = FormStartPosition.CenterScreen;
                        //pop.Show();
                        pop.pictureBox.Image = Image.FromFile(Application.StartupPath + "\\ico\\" + X + ".jpg");
                        pop.LabelX1.Text = content;
                        Variable.AlarmFlag = true;
                        Variable.OPsure = true;
                        Variable.content = content;

                        Variable.step = step;
                        Variable.cancelStep = cancelStep;
                        Variable.sureStep = sureStep;

                        Variable.POPFlag = true;
                        pop.timerScan.Enabled = true;
                        pop.Visible = false;
                        //pop.Close();
                        //pop.ShowDialog(this);
                        pop.Show();

                        //}
                        //else
                        //{
                        //    POPFormIsOpenOrNot.Activate();
                        //    POPFormIsOpenOrNot.WindowState = FormWindowState.Normal;
                        //}
                    }
                    else
                    {
                        Thread.Sleep(200);
                        //Form POPFormIsOpenOrNot = Application.OpenForms["POPForm"];
                        //if ((POPFormIsOpenOrNot == null) || (POPFormIsOpenOrNot.IsDisposed))//如果没有创建过或者窗体已经被释放
                        //{
                        pop = new POPForm();
                        pop.StartPosition = FormStartPosition.CenterScreen;
                        //pop.Show();
                        pop.pictureBox.Image = Image.FromFile(Application.StartupPath + "\\ico\\" + X + ".jpg");
                        pop.LabelX1.Text = content;
                        Variable.AlarmFlag = true;

                        Variable.step = step;
                        Variable.cancelStep = cancelStep;
                        Variable.sureStep = sureStep;

                        Variable.POPFlag = true;
                        pop.timerScan.Enabled = true;
                        pop.Visible = false;
                        //pop.Close();
                        //pop.ShowDialog(this);
                        pop.Show();

                        //}
                        //else
                        //{
                        //    POPFormIsOpenOrNot.Activate();
                        //    POPFormIsOpenOrNot.WindowState = FormWindowState.Normal;
                        //}

                        //写入数据库
                        access.RecordAccess(logType, content);
                    }

                    //报警次数计数
                    if ((logType.ToString() == "Alarm" || logType.ToString() == "Alarm1") && content != Variable.strVar[0])
                    {
                        Variable.strVar[0] = content;
                        Variable.alarmCount += 1;
                    }
                }
                else
                {
                    //写入数据库
                    access.RecordAccess(logType, content);
                }

            }
        }

        #endregion

        #endregion

        #region **********延迟加热流程**********
        public void DelayHeat()
        {
            DelayHeatUp1();
            DelayHeatUp2();
            DelayHeatDown1();
            DelayHeatDown2();
            DelayTime();
        }

        #region 上层内延迟加热
        public void DelayHeatUp1()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        return;
                    }
                    resetEvent.WaitOne();

                    for (int i = 0; i < 10; i++)
                    {
                        if (Variable.TempOKFlag[i * 4 + 0])//上层1温度达标
                        {
                            if (Variable.MachineState == Variable.MachineStatus.Running)
                            {
                                if (Variable.HotModel)
                                {
                                    double data = Convert.ToDouble(Variable.TemperData[i * 4 + 0]);//读取温度
                                    if (data >= Variable.TempDownLimit && data <= Variable.TempUpLimit)
                                    {
                                        if (!Variable.FistPower)
                                        {
                                            if (Variable.tempDelay[i * 4 + 0] > Variable.tempSetDelay)
                                            {
                                                function.OutYOFF(101 + i * 32);//上层1老化上下气缸下降
                                                Thread.Sleep(1000);
                                                function.OutYON(102 + i * 32);//上层1老化上下气缸上升
                                                Thread.Sleep(Variable.airSetDelay);
                                                if (Variable.XStatus[119 + i * 32])
                                                {
                                                    function.OutYON(108 + i * 32);//上层1老化测试板通电
                                                    //
                                                    WriteTestTXT((i * 4 + 1).ToString());//告知老化机测试
                                                    Variable.ModelState[i * 4] = 2;//老化机测试中
                                                    Variable.TempOKFlag[i * 4 + 0] = false;
                                                    Variable.tempDelay[i * 4 + 0] = 0;
                                                }
                                            }
                                        }

                                        if (Variable.FistPower)
                                        {
                                            function.OutYON(108 + i * 32);//上层1老化测试板通电  

                                            if (Variable.tempDelay[i * 4 + 0] > Variable.tempSetDelay)
                                            {
                                                function.OutYOFF(101 + i * 32);//上层1老化上下气缸下降
                                                Thread.Sleep(1000);
                                                function.OutYON(102 + i * 32);//上层1老化上下气缸上升
                                                Thread.Sleep(Variable.airSetDelay);
                                                if (Variable.XStatus[119 + i * 32])
                                                {
                                                    WriteTestTXT((i * 4 + 1).ToString());//告知老化机测试
                                                    Variable.ModelState[i * 4] = 2;//老化机测试中
                                                    Variable.TempOKFlag[i * 4 + 0] = false;
                                                    Variable.tempDelay[i * 4 + 0] = 0;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (!Variable.FistPower)
                                    {
                                        if (Variable.tempDelay[i * 4 + 0] > Variable.tempSetDelay)
                                        {
                                            function.OutYOFF(101 + i * 32);//上层1老化上下气缸下降
                                            Thread.Sleep(1000);
                                            function.OutYON(102 + i * 32);//上层1老化上下气缸上升
                                            Thread.Sleep(Variable.airSetDelay);
                                            if (Variable.XStatus[119 + i * 32])
                                            {
                                                function.OutYON(108 + i * 32);//1#上老化测试板通电  

                                                WriteTestTXT((i * 4 + 1).ToString());//告知老化机测试
                                                Variable.ModelState[i * 4] = 2;//老化机测试中
                                                Variable.TempOKFlag[i * 4 + 0] = false;
                                                Variable.tempDelay[i * 4 + 0] = 0;
                                            }
                                        }
                                    }

                                    if (Variable.FistPower)
                                    {
                                        function.OutYON(108 + i * 32);//1#上老化测试板通电  

                                        if (Variable.tempDelay[i * 4 + 0] > Variable.tempSetDelay)
                                        {
                                            function.OutYOFF(101 + i * 32);//上层1上老化上下气缸下降
                                            Thread.Sleep(1000);
                                            function.OutYON(102 + i * 32);//上层1上老化上下气缸上升
                                            Thread.Sleep(Variable.airSetDelay);
                                            if (Variable.XStatus[119 + i * 32])
                                            {
                                                WriteTestTXT((i * 4 + 1).ToString());//告知老化机测试
                                                Variable.ModelState[i * 4] = 2;//老化机测试中
                                                Variable.TempOKFlag[i * 4 + 0] = false;
                                                Variable.tempDelay[i * 4 + 0] = 0;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    Thread.Sleep(1);
                }
            });
        }
        #endregion

        #region 上层外延迟加热
        public void DelayHeatUp2()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        return;
                    }
                    resetEvent.WaitOne();

                    for (int i = 0; i < 10; i++)
                    {
                        if (Variable.TempOKFlag[i * 4 + 1])//上层1温度达标
                        {
                            if (Variable.MachineState == Variable.MachineStatus.Running)
                            {
                                if (Variable.HotModel)
                                {
                                    double data = Convert.ToDouble(Variable.TemperData[i * 4 + 1]);//读取温度
                                    if (data >= Variable.TempDownLimit && data <= Variable.TempUpLimit)
                                    {
                                        if (!Variable.FistPower)
                                        {
                                            if (Variable.tempDelay[i * 4 + 1] > Variable.tempSetDelay)
                                            {
                                                function.OutYOFF(103 + i * 32);//上层2老化上下气缸下降
                                                Thread.Sleep(1000);
                                                function.OutYON(104 + i * 32);//上层2老化上下气缸上升
                                                Thread.Sleep(Variable.airSetDelay);
                                                if (Variable.XStatus[123 + i * 32])
                                                {
                                                    function.OutYON(110 + i * 32);//上层2老化测试板通电   

                                                    WriteTestTXT((i * 4 + 2).ToString());//告知老化机测试
                                                    Variable.ModelState[i * 4 + 1] = 2;//老化机测试中
                                                    Variable.TempOKFlag[i * 4 + 1] = false;
                                                    Variable.tempDelay[i * 4 + 1] = 0;
                                                }
                                            }
                                        }

                                        if (Variable.FistPower)
                                        {
                                            function.OutYON(110 + i * 32);//上层2老化测试板通电  

                                            if (Variable.tempDelay[i * 4 + 1] > Variable.tempSetDelay)
                                            {
                                                function.OutYOFF(103 + i * 32);//上层2老化上下气缸下降
                                                Thread.Sleep(1000);
                                                function.OutYON(104 + i * 32);//上层2老化上下气缸上升
                                                Thread.Sleep(Variable.airSetDelay);
                                                if (Variable.XStatus[123 + i * 32])
                                                {
                                                    WriteTestTXT((i * 4 + 2).ToString());//告知老化机测试
                                                    Variable.ModelState[i * 4 + 1] = 2;//老化机测试中
                                                    Variable.TempOKFlag[i * 4 + 1] = false;
                                                    Variable.tempDelay[i * 4 + 1] = 0;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (!Variable.FistPower)
                                    {
                                        if (Variable.tempDelay[i * 4 + 1] > Variable.tempSetDelay)
                                        {
                                            function.OutYOFF(103 + i * 32);//上层2老化上下气缸下降
                                            Thread.Sleep(1000);
                                            function.OutYON(104 + i * 32);//上层2老化上下气缸上升
                                            Thread.Sleep(Variable.airSetDelay);
                                            if (Variable.XStatus[123 + i * 32])
                                            {
                                                function.OutYON(110 + i * 32);//上层2老化测试板通电  

                                                WriteTestTXT((i * 4 + 2).ToString());//告知老化机测试
                                                Variable.ModelState[i * 4 + 1] = 2;//老化机测试中
                                                Variable.TempOKFlag[i * 4 + 1] = false;
                                                Variable.tempDelay[i * 4 + 1] = 0;
                                            }
                                        }
                                    }

                                    if (Variable.FistPower)
                                    {
                                        function.OutYON(110 + i * 32);//上层2老化测试板通电 

                                        if (Variable.tempDelay[i * 4 + 1] > Variable.tempSetDelay)
                                        {
                                            function.OutYOFF(103 + i * 32);//上层2上老化上下气缸下降
                                            Thread.Sleep(1000);
                                            function.OutYON(104 + i * 32);//上层2上老化上下气缸上升
                                            Thread.Sleep(Variable.airSetDelay);
                                            if (Variable.XStatus[123 + i * 32])
                                            {
                                                WriteTestTXT((i * 4 + 2).ToString());//告知老化机测试
                                                Variable.ModelState[i * 4 + 1] = 2;//老化机测试中
                                                Variable.TempOKFlag[i * 4 + 1] = false;
                                                Variable.tempDelay[i * 4 + 1] = 0;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    Thread.Sleep(1);
                }
            });
        }
        #endregion

        #region 下层内延迟加热
        public void DelayHeatDown1()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        return;
                    }
                    resetEvent.WaitOne();

                    for (int i = 0; i < 10; i++)
                    {
                        if (Variable.TempOKFlag[i * 4 + 2])//下层1温度达标
                        {
                            if (Variable.MachineState == Variable.MachineStatus.Running)
                            {
                                if (Variable.HotModel)
                                {
                                    double data = Convert.ToDouble(Variable.TemperData[i * 4 + 2]);//读取温度
                                    if (data >= Variable.TempDownLimit && data <= Variable.TempUpLimit)
                                    {
                                        if (!Variable.FistPower)
                                        {
                                            if (Variable.tempDelay[i * 4 + 2] > Variable.tempSetDelay)
                                            {
                                                function.OutYOFF(117 + i * 32);//下层1老化上下气缸下降
                                                Thread.Sleep(1000);
                                                function.OutYON(118 + i * 32);//下层1老化上下气缸上升
                                                Thread.Sleep(Variable.airSetDelay);
                                                if (Variable.XStatus[133 + i * 32])
                                                {
                                                    function.OutYON(124 + i * 32);//下层1老化测试板通电 

                                                    WriteTestTXT((i * 4 + 3).ToString());//告知老化机测试
                                                    Variable.ModelState[i * 4 + 2] = 2;//老化机测试中
                                                    Variable.TempOKFlag[i * 4 + 2] = false;
                                                    Variable.tempDelay[i * 4 + 2] = 0;
                                                }
                                            }
                                        }

                                        if (Variable.FistPower)
                                        {
                                            function.OutYON(124 + i * 32);//下层1老化测试板通电 

                                            if (Variable.tempDelay[i * 4 + 2] > Variable.tempSetDelay)
                                            {
                                                function.OutYOFF(117 + i * 32);//下层1老化上下气缸下降
                                                Thread.Sleep(1000);
                                                function.OutYON(118 + i * 32);//下层1老化上下气缸上升
                                                Thread.Sleep(Variable.airSetDelay);
                                                if (Variable.XStatus[133 + i * 32])
                                                {
                                                    WriteTestTXT((i * 4 + 3).ToString());//告知老化机测试
                                                    Variable.ModelState[i * 4 + 2] = 2;//老化机测试中
                                                    Variable.TempOKFlag[i * 4 + 2] = false;
                                                    Variable.tempDelay[i * 4 + 2] = 0;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (!Variable.FistPower)
                                    {
                                        if (Variable.tempDelay[i * 4 + 2] > Variable.tempSetDelay)
                                        {
                                            function.OutYOFF(117 + i * 32);//下层1老化上下气缸下降
                                            Thread.Sleep(1000);
                                            function.OutYON(118 + i * 32);//下层1老化上下气缸上升
                                            Thread.Sleep(Variable.airSetDelay);
                                            if (Variable.XStatus[133 + i * 32])
                                            {
                                                function.OutYON(124 + i * 32);//下层1老化测试板通电

                                                WriteTestTXT((i * 4 + 3).ToString());//告知老化机测试
                                                Variable.ModelState[i * 4 + 2] = 2;//老化机测试中
                                                Variable.TempOKFlag[i * 4 + 2] = false;
                                                Variable.tempDelay[i * 4 + 2] = 0;
                                            }
                                        }
                                    }

                                    if (Variable.FistPower)
                                    {
                                        function.OutYON(124 + i * 32);//下层1老化测试板通电   

                                        if (Variable.tempDelay[i * 4 + 2] > Variable.tempSetDelay)
                                        {
                                            function.OutYOFF(117 + i * 32);//下层1上老化上下气缸下降
                                            Thread.Sleep(1000);
                                            function.OutYON(118 + i * 32);//下层1上老化上下气缸上升
                                            Thread.Sleep(Variable.airSetDelay);
                                            if (Variable.XStatus[133 + i * 32])
                                            {
                                                WriteTestTXT((i * 4 + 3).ToString());//告知老化机测试
                                                Variable.ModelState[i * 4 + 2] = 2;//老化机测试中
                                                Variable.TempOKFlag[i * 4 + 2] = false;
                                                Variable.tempDelay[i * 4 + 2] = 0;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    Thread.Sleep(1);
                }
            });
        }
        #endregion

        #region 下层外延迟加热
        public void DelayHeatDown2()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        return;
                    }
                    resetEvent.WaitOne();

                    for (int i = 0; i < 10; i++)
                    {
                        if (Variable.TempOKFlag[i * 4 + 3])//下层2温度达标
                        {
                            if (Variable.MachineState == Variable.MachineStatus.Running)
                            {
                                if (Variable.HotModel)
                                {
                                    double data = Convert.ToDouble(Variable.TemperData[i * 4 + 3]);//读取温度
                                    if (data >= Variable.TempDownLimit && data <= Variable.TempUpLimit)
                                    {
                                        if (!Variable.FistPower)
                                        {
                                            if (Variable.tempDelay[i * 4 + 3] > Variable.tempSetDelay)
                                            {
                                                function.OutYOFF(119 + i * 32);//下层2老化上下气缸下降
                                                Thread.Sleep(1000);
                                                function.OutYON(120 + i * 32);//下层2老化上下气缸上升
                                                Thread.Sleep(Variable.airSetDelay);
                                                if (Variable.XStatus[137 + i * 32])
                                                {
                                                    function.OutYON(126 + i * 32);//下层2老化测试板通电 

                                                    WriteTestTXT((i * 4 + 4).ToString());//告知老化机测试
                                                    Variable.ModelState[i * 4 + 3] = 2;//老化机测试中
                                                    Variable.TempOKFlag[i * 4 + 3] = false;
                                                    Variable.tempDelay[i * 4 + 3] = 0;
                                                }
                                            }
                                        }

                                        if (Variable.FistPower)
                                        {
                                            function.OutYON(126 + i * 32);//下层2老化测试板通电

                                            if (Variable.tempDelay[i * 4 + 3] > Variable.tempSetDelay)
                                            {
                                                function.OutYOFF(119 + i * 32);//下层2老化上下气缸下降
                                                Thread.Sleep(1000);
                                                function.OutYON(120 + i * 32);//下层2老化上下气缸上升
                                                Thread.Sleep(Variable.airSetDelay);
                                                if (Variable.XStatus[137 + i * 32])
                                                {
                                                    WriteTestTXT((i * 4 + 4).ToString());//告知老化机测试
                                                    Variable.ModelState[i * 4 + 3] = 2;//老化机测试中
                                                    Variable.TempOKFlag[i * 4 + 3] = false;
                                                    Variable.tempDelay[i * 4 + 3] = 0;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (!Variable.FistPower)
                                    {
                                        if (Variable.tempDelay[i * 4 + 3] > Variable.tempSetDelay)
                                        {
                                            function.OutYOFF(119 + i * 32);//下层2老化上下气缸下降
                                            Thread.Sleep(1000);
                                            function.OutYON(120 + i * 32);//下层2老化上下气缸上升
                                            Thread.Sleep(Variable.airSetDelay);
                                            if (Variable.XStatus[137 + i * 32])
                                            {
                                                function.OutYON(126 + i * 32);//下层2老化测试板通电 

                                                WriteTestTXT((i * 4 + 4).ToString());//告知老化机测试
                                                Variable.ModelState[i * 4 + 3] = 2;//老化机测试中
                                                Variable.TempOKFlag[i * 4 + 3] = false;
                                                Variable.tempDelay[i * 4 + 3] = 0;
                                            }
                                        }
                                    }

                                    if (Variable.FistPower)
                                    {
                                        function.OutYON(126 + i * 32);//下层2老化测试板通电  

                                        if (Variable.tempDelay[i * 4 + 3] > Variable.tempSetDelay)
                                        {
                                            function.OutYOFF(119 + i * 32);//下层2上老化上下气缸下降
                                            Thread.Sleep(1000);
                                            function.OutYON(120 + i * 32);//下层2上老化上下气缸上升
                                            Thread.Sleep(Variable.airSetDelay);
                                            if (Variable.XStatus[137 + i * 32])
                                            {
                                                WriteTestTXT((i * 4 + 4).ToString());//告知老化机测试
                                                Variable.ModelState[i * 4 + 3] = 2;//老化机测试中
                                                Variable.TempOKFlag[i * 4 + 3] = false;
                                                Variable.tempDelay[i * 4 + 3] = 0;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    Thread.Sleep(1);
                }
            });
        }
        #endregion

        #region 延时
        public void DelayTime()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        if (Variable.TempOKFlag[i])
                        {
                            //Variable.tempDelay[i] += 1;
                            watch[i].Start();
                        }
                        else
                        {
                            watch[i].Stop();
                        }

                        Variable.tempDelay[i] = second[i] = watch[i].ElapsedMilliseconds;// / 1000;
                    }

                    for (int i = 0; i < 40; i++)
                    {
                        if (!Variable.TempOKFlag[i])
                        {
                            watch[i].Stop();// 停止
                            watch[i].Reset();// 重新计时
                        }
                    }

                    Thread.Sleep(1);
                }
            });
        }

        #endregion

        #endregion

        #region **********温度读取显示线程**********

        public void Temp()
        {
            while (true)
            {
                if (!Variable.formOpenFlag)
                {
                    if (!Variable.temWriteFlag && Variable.RestStep != 100)
                    {
                        Variable.temReadFlag = true;
                        #region 温度读取

                        bool sc1 = false;
                        bool sc2 = false;
                        sc1 = Temperature1.Open();
                        sc2 = Temperature2.Open();
                        if (sc1 && sc2)
                        {
                            //温度读取
                            for (int i = 0; i < 10; i++)
                            {
                                if (i < 5)
                                {
                                    Variable.TemperData[i * 4 + 0] = TempRead1((i * 4 + 1).ToString("X2"));
                                    TemperDataGrid.Rows[0].Cells[i + 1].Value = Variable.TemperData[i * 4 + 0].ToString();
                                    Variable.TemperData[i * 4 + 1] = TempRead1((i * 4 + 2).ToString("X2"));
                                    TemperDataGrid.Rows[1].Cells[i + 1].Value = Variable.TemperData[i * 4 + 1].ToString();
                                    Variable.TemperData[i * 4 + 2] = TempRead1((i * 4 + 3).ToString("X2"));
                                    TemperDataGrid.Rows[3].Cells[i + 1].Value = Variable.TemperData[i * 4 + 2].ToString();
                                    Variable.TemperData[i * 4 + 3] = TempRead1((i * 4 + 4).ToString("X2"));
                                    TemperDataGrid.Rows[4].Cells[i + 1].Value = Variable.TemperData[i * 4 + 3].ToString();
                                }
                                else
                                {
                                    Variable.TemperData[i * 4 + 0] = TempRead2(((i - 5) * 4 + 1).ToString("X2"));
                                    TemperDataGrid.Rows[0].Cells[i + 1].Value = Variable.TemperData[i * 4 + 0].ToString();
                                    Variable.TemperData[i * 4 + 1] = TempRead2(((i - 5) * 4 + 2).ToString("X2"));
                                    TemperDataGrid.Rows[1].Cells[i + 1].Value = Variable.TemperData[i * 4 + 1].ToString();
                                    Variable.TemperData[i * 4 + 2] = TempRead2(((i - 5) * 4 + 3).ToString("X2"));
                                    TemperDataGrid.Rows[3].Cells[i + 1].Value = Variable.TemperData[i * 4 + 2].ToString();
                                    Variable.TemperData[i * 4 + 3] = TempRead2(((i - 5) * 4 + 4).ToString("X2"));
                                    TemperDataGrid.Rows[4].Cells[i + 1].Value = Variable.TemperData[i * 4 + 3].ToString();
                                }
                            }
                        }

                        #endregion

                        #region 温度状态显示

                        if (Variable.HotModel)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                if (!Variable.XStatus[116 + i * 32] && Convert.ToDouble(TemperDataGrid.Rows[0].Cells[i + 1].Value) <= Variable.TempUpLimit && Convert.ToDouble(TemperDataGrid.Rows[0].Cells[i + 1].Value) >= Variable.TempDownLimit)
                                {
                                    TemperDataGrid.Rows[0].Cells[i + 1].Style.BackColor = Color.Green;
                                }
                                else
                                {
                                    TemperDataGrid.Rows[0].Cells[i + 1].Style.BackColor = Color.Red;
                                }

                                if (!Variable.XStatus[120 + i * 32] && Convert.ToDouble(TemperDataGrid.Rows[1].Cells[i + 1].Value) <= Variable.TempUpLimit && Convert.ToDouble(TemperDataGrid.Rows[1].Cells[i + 1].Value) >= Variable.TempDownLimit)
                                {
                                    TemperDataGrid.Rows[1].Cells[i + 1].Style.BackColor = Color.Green;
                                }
                                else
                                {
                                    TemperDataGrid.Rows[1].Cells[i + 1].Style.BackColor = Color.Red;
                                }

                                if (!Variable.XStatus[130 + i * 32] && Convert.ToDouble(TemperDataGrid.Rows[3].Cells[i + 1].Value) <= Variable.TempUpLimit && Convert.ToDouble(TemperDataGrid.Rows[3].Cells[i + 1].Value) >= Variable.TempDownLimit)
                                {
                                    TemperDataGrid.Rows[3].Cells[i + 1].Style.BackColor = Color.Green;
                                }
                                else
                                {
                                    TemperDataGrid.Rows[3].Cells[i + 1].Style.BackColor = Color.Red;
                                }

                                if (!Variable.XStatus[134 + i * 32] && Convert.ToDouble(TemperDataGrid.Rows[4].Cells[i + 1].Value) <= Variable.TempUpLimit && Convert.ToDouble(TemperDataGrid.Rows[4].Cells[i + 1].Value) >= Variable.TempDownLimit)
                                {
                                    TemperDataGrid.Rows[4].Cells[i + 1].Style.BackColor = Color.Green;
                                }
                                else
                                {
                                    TemperDataGrid.Rows[4].Cells[i + 1].Style.BackColor = Color.Red;
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                if (i != 2)
                                {
                                    for (int j = 0; j < 11; j++)
                                    {
                                        TemperDataGrid.Rows[i].Cells[j].Style.BackColor = Color.LightSkyBlue;
                                    }
                                }
                            }
                        }

                        #endregion
                        Variable.temReadFlag = false;
                    }
                }
                Thread.Sleep(10);
            }
        }
        #endregion

        #region UPH计算,运行时间，停止时间，报警时间
        public void TimeCount()
        {
            while (true)
            {
                //UPH
                if (Variable.MachineState == Variable.MachineStatus.Running)
                {
                    //计算UPH
                    if (Variable.inChipNum != 0 && uphSecond % 1 == 0 && uphSecond != 0)
                    {
                        Variable.UPH = Convert.ToInt32((Variable.outChipNum / uphSecond) * 3600);
                    }
                }

                //运行时间
                if (Variable.MachineState == Variable.MachineStatus.Running)
                {
                    runstopwatch.Start();
                }
                else
                {
                    runstopwatch.Stop();
                }

                Second1 = runstopwatch.ElapsedMilliseconds / 1000;
                uphSecond = runSecond + Second1;
                runTimeShow(runSecond + Second1);//转化时间

                //停止时间
                if (Variable.MachineState == Variable.MachineStatus.Pause || Variable.MachineState == Variable.MachineStatus.Alarm)
                {
                    stopstopwatch.Start();
                }
                else
                {
                    stopstopwatch.Stop();
                }
                Second2 = stopstopwatch.ElapsedMilliseconds / 1000;
                stopTimeShow(stopSecond + Second2);//转化时间

                //报警时间
                if (Variable.MachineState == Variable.MachineStatus.Alarm)
                {
                    alarmstopwatch.Start();
                }
                else
                {
                    alarmstopwatch.Stop();
                }
                Second3 = alarmstopwatch.ElapsedMilliseconds / 1000;
                alarmTimeShow(alarmSecond + Second3);//转化时间

                Thread.Sleep(1);
            }
        }
        private void UPHtimer_Tick(object sender, EventArgs e)
        {
            //if (Variable.MachineState == Variable.MachineStatus.Running)
            //{
            //    //计算UPH
            //    if ((Variable.inChipNum != 0) && runSecond % 1 == 0)
            //    {
            //        Variable.UPH = Convert.ToInt32((Variable.inChipNum / runSecond) * 3600);
            //    }
            //}

            //if (Variable.MachineState == Variable.MachineStatus.Running)
            //{
            //    runSecond += 1;//累计计数
            //    runTimeShow(runSecond);//转化时间

            //}
            //if (Variable.MachineState == Variable.MachineStatus.Pause || Variable.MachineState == Variable.MachineStatus.Alarm)
            //{
            //    stopSecond += 1;//累计计数
            //    stopTimeShow(stopSecond);//转化时间
            //}

            //if (Variable.MachineState == Variable.MachineStatus.Alarm)
            //{
            //    alarmSecond += 1;//累计计数
            //    alarmTimeShow(alarmSecond);//转化时间
            //}
        }

        public void runTimeShow(long seconds)
        {
            //显示work时间
            DateTime data = DateTime.Now.Date;
            DateTime now = data.AddSeconds(seconds);//将整数转化为时间
            string[] data1 = now.ToString().Split(' ');
            Variable.runTime = data1[1];//使用时间
        }

        public void stopTimeShow(long seconds)
        {
            //显示work时间
            DateTime data = DateTime.Now.Date;
            DateTime now = data.AddSeconds(seconds);//将整数转化为时间
            string[] data1 = now.ToString().Split(' ');
            Variable.stopTime = data1[1];//使用时间
        }
        public void alarmTimeShow(long seconds)
        {
            //显示work时间
            DateTime data = DateTime.Now.Date;
            DateTime now = data.AddSeconds(seconds);//将整数转化为时间
            string[] data1 = now.ToString().Split(' ');
            Variable.alarmTime = data1[1];//使用时间
        }
        #endregion

        #region 三色灯按钮灯扫描
        public void LampScan()
        {
            while (true)
            {
                #region 音乐蜂鸣
                //音乐1：YON(132)结批
                //音乐2：YON(133)气缸报警
                //音乐3：YON(134)料没放好报警
                //音乐4：YON(135)正极限报警
                //音乐5：YON(136)门禁，急停 轴报警 温度

                //if (Variable.BuzzerCheck == true && !buzzingFlag)
                //{
                //    ////音乐1：YON(10)结批
                //    if (Variable.CleanOutFlag)
                //    {
                //        function.OutYON(10);
                //    }

                //    //音乐2：YON(11)气缸报警
                //    if ((Variable.CommonAlarm1 || Variable.CommonAlarm2) && Variable.MachineState == Variable.MachineStatus.Alarm && !buzzingFlag)
                //    {
                //        function.OutYON(11);
                //    }

                //    //音乐3：YON(12)料没放好报警
                //    if ((Variable.XStatus[40] || Variable.XStatus[43] || Variable.XStatus[72] || Variable.XStatus[75]) && Variable.MachineState == Variable.MachineStatus.Alarm && !buzzingFlag)
                //    {
                //        function.OutYON(12);
                //    }

                //    //音乐5：YON(13)正极限报警
                //    if ((Variable.PLimitAlarm || Variable.NLimitAlarm) && Variable.MachineState == Variable.MachineStatus.Alarm && !buzzingFlag)
                //    {
                //        function.OutYON(13);
                //    }

                //    //音乐4：YON(14)门禁，急停 轴报警 温度
                //    if ((Variable.ServerAlarm || Variable.EMG == true || Variable.XStatus[5] || Variable.XStatus[6] || Variable.XStatus[7] || Variable.XStatus[8]) && Variable.MachineState == Variable.MachineStatus.Alarm && !buzzingFlag)
                //    {
                //        function.OutYON(14);
                //    }

                //    //音乐停止
                //    if ((Variable.PauseButton == true || Variable.btnStop == true || Variable.btnReset || Variable.AlarmClrButton) && !buzzingFlag)
                //    {
                //        function.OutYOFF(10);
                //        function.OutYOFF(11);
                //        function.OutYOFF(12);
                //        function.OutYOFF(13);
                //        function.OutYOFF(14);

                //        buzzingFlag = true;
                //        Variable.CleanOutFlag = false;
                //    }

                //    if (Variable.MachineState != Variable.MachineStatus.Alarm)
                //    {
                //        buzzingFlag = false;
                //    }
                //}

                #endregion

                #region 三色灯按钮灯
                //待机
                if (Variable.MachineState == Variable.MachineStatus.Stop)
                {
                    function.SetGreenLampOff();//绿灯灭
                    function.SetYellowLampOn();//黄灯亮
                    function.SetRedLampOff();//红灯灭
                    function.SetBuzzerOff();//蜂鸣器灭
                    function.StartLampOff();//启动灯
                    function.StopLampOff();//停止灯
                    function.RestLampOff();//复位灯
                    function.CleanOutOff();//Clean Out灯

                    statelab.Text = "待机";
                    statelab.BackColor = Color.Yellow;
                    btnStart.Enabled = false;//开始按钮
                    btnReset.Enabled = false;//复位按钮
                    btnStop.Enabled = false;//停止按钮
                    btnCleanOut.Enabled = false;//结批按钮
                    btnCleanOne.Enabled = false;//单循环按钮
                    btnZero.Enabled = true;//归零按钮
                    if (lightFlag == 0)
                    {
                        function.ReturnZeroOn();//归零灯
                        lightFlag = 1;
                    }
                    else
                    {
                        function.ReturnZeroOff();//归零灯
                        lightFlag = 0;
                    }
                }
                //开始
                else if (Variable.MachineState == Variable.MachineStatus.Running)
                {

                    function.SetGreenLampOn();//绿灯亮
                    function.SetYellowLampOff();//黄灯灭
                    function.SetBuzzerOff();//蜂鸣器灭
                    function.SetRedLampOff();//红灯灭
                    function.StartLampOff();//启动灯               
                    function.RestLampOff();//复位灯
                    function.CleanOutOff();//Clean Out灯
                    function.ReturnZeroOff();//归零灯

                    btnStart.Enabled = false;
                    btnStop.Enabled = true;
                    btnReset.Enabled = false;
                    btnCleanOne.Enabled = false;
                    btnCleanOut.Enabled = false;
                    btnZero.Enabled = false;

                    if (!Variable.CleanOut && !Variable.CleanOne)
                    {
                        statelab.Text = "运行";
                    }
                    else if (Variable.CleanOut)
                    {
                        statelab.Text = "结批中";
                    }
                    else if (Variable.CleanOne)
                    {
                        statelab.Text = "清料中";
                    }
                    statelab.BackColor = Color.Green;
                    if (lightFlag == 0)
                    {
                        function.StopLampOn();//停止灯
                        lightFlag = 1;
                    }
                    else
                    {
                        function.StopLampOff();//停止灯
                        lightFlag = 0;
                    }
                }
                //复位
                else if (Variable.MachineState == Variable.MachineStatus.Reset)
                {
                    function.SetRedLampOn();//红灯亮
                    function.RestLampOff();//复位灯灭
                    function.SetBuzzerOff();//蜂鸣器灭
                    btnStop.Enabled = true;
                    if (lightFlag == 0)
                    {
                        function.StopLampOn();//停止灯
                        lightFlag = 1;
                    }
                    else
                    {
                        function.StopLampOff();//停止灯
                        lightFlag = 0;
                    }
                }
                //停止
                else if (Variable.MachineState == Variable.MachineStatus.Pause)
                {
                    function.SetGreenLampOff();//绿灯灭
                    function.SetYellowLampOn();//黄灯亮
                    function.SetBuzzerOff();//蜂鸣器灭
                    function.SetRedLampOff();//红灯灭
                    statelab.Text = "停止";
                    statelab.BackColor = Color.Yellow;

                    btnStart.Enabled = true;
                    btnStop.Enabled = false;
                    btnReset.Enabled = true;
                    btnCleanOne.Enabled = true;
                    btnCleanOut.Enabled = true;
                    btnZero.Enabled = true;

                    if (lightFlag == 0)
                    {
                        function.ReturnZeroOn();//归零灯
                        function.StartLampOn();//启动灯
                        function.RestLampOn();//复位灯
                        function.CleanOutOff();//Clean Out灯
                        lightFlag = 1;
                    }
                    else
                    {
                        function.ReturnZeroOff();//归零灯
                        function.StartLampOff();//启动灯
                        function.RestLampOff();//复位灯
                        function.CleanOutOff();//Clean Out灯
                        lightFlag = 0;
                    }
                }
                //报警
                else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                {
                    function.SetYellowLampOff();//黄灯灭
                    function.SetGreenLampOff();//绿灯灭
                    function.StartLampOff();//启动灯
                    function.ReturnZeroOff();//归零灯
                    function.OneCycleOff();//OneCycle
                    statelab.Text = "报警";
                    statelab.BackColor = Color.Red;
                    btnReset.Enabled = true;
                    if (lightFlag == 0)
                    {
                        function.StopLampOn();//停止灯
                        function.RestLampOn();//复位灯
                        function.SetRedLampOn();//红灯
                        if (Variable.BuzzerCheck)
                        {
                            function.SetBuzzerOn();//蜂鸣器
                        }
                        lightFlag = 1;
                    }
                    else
                    {
                        function.StopLampOff();//停止灯
                        function.RestLampOff();//复位灯 
                        function.SetRedLampOff();//红灯
                        function.SetBuzzerOff();//蜂鸣器
                        lightFlag = 0;
                    }
                }
                //急停
                else if (Variable.MachineState == Variable.MachineStatus.Emg)
                {
                    function.SetYellowLampOff();//黄灯灭
                    function.SetGreenLampOff();//绿灯灭
                    function.StartLampOff();//启动灯
                    function.RestLampOff();//复位灯
                    function.ReturnZeroOff();//归零灯
                    function.OneCycleOff();//OneCycle
                    statelab.Text = "急停中";
                    statelab.BackColor = Color.Red;
                    btnStop.Enabled = true;
                    btnReset.Enabled = true;
                    if (lightFlag == 0)
                    {
                        function.StopLampOn();//停止灯
                        function.SetRedLampOn();//红灯
                        if (Variable.BuzzerCheck)
                        {
                            function.SetBuzzerOn();//蜂鸣器
                        }
                        lightFlag = 1;
                    }
                    else
                    {
                        function.StopLampOff();//停止灯
                        function.SetRedLampOff();//红灯
                        function.SetBuzzerOff();//蜂鸣器
                        lightFlag = 0;
                    }
                }
                //归零完成
                else if (Variable.MachineState == Variable.MachineStatus.StandBy)
                {
                    function.StartLampOff();//启动灭
                    function.StopLampOff();//停止灭
                    function.RestLampOff();//复位灭
                    function.OneCycleOff();//OneCycle灭
                    function.CleanOutOff();//CleanOut灭
                    function.SetGreenLampOff();//绿灯灭
                    function.SetYellowLampOn();//黄灯亮
                    function.SetBuzzerOff();//蜂鸣器灭
                    function.SetRedLampOff();//红灯灭
                    statelab.Text = "归零OK";
                    statelab.BackColor = Color.Yellow;
                    if (lightFlag == 0)
                    {
                        function.StopLampOn();//停止灯
                        lightFlag = 1;
                    }
                    else
                    {
                        function.StopLampOff();//停止灯
                        lightFlag = 0;
                    }
                }
                //归零
                else if (Variable.MachineState == Variable.MachineStatus.zero)
                {
                    function.StartLampOn();//启动亮
                    function.StopLampOn();//停止亮
                    function.RestLampOn();//复位亮
                    function.CleanOutOn();//CleanOut亮
                    function.ReturnZeroOn();//归零亮
                    function.SetRedLampOn();//红灯亮
                    function.SetYellowLampOn();//黄灯亮
                    function.SetGreenLampOn();//绿灯亮

                    btnStart.Enabled = false;
                    btnStop.Enabled = true;
                    btnReset.Enabled = false;
                    btnCleanOne.Enabled = false;
                    btnCleanOut.Enabled = false;
                    btnZero.Enabled = false;

                    statelab.Text = "归零中";
                    statelab.BackColor = Color.Green;
                }
                //结批
                else if (Variable.CleanOut && Variable.MachineState == Variable.MachineStatus.Running)
                {
                    btnCleanOut.Enabled = false;

                    statelab.Text = "结批中";
                    statelab.BackColor = Color.Green;
                }
                //清料
                else if (Variable.CleanOne && Variable.MachineState == Variable.MachineStatus.Running)
                {
                    btnCleanOne.Enabled = false;

                    statelab.Text = "清料中";
                    statelab.BackColor = Color.Green;
                }

                #endregion

                Thread.Sleep(500);
            }
        }
        #endregion

        #region 计算投入总数
        public void InNum(string str)
        {
            //读取Photo文件
            string strPhoto = Application.StartupPath + @"\Data\Photo\Up\" + str + @"\tray";
            string[] ReadPhoto = myTXT.ReadTXT(strPhoto);

            for (int i = 0; i < ReadPhoto.Length; i++)
            {
                //计算投入总数
                if (ReadPhoto[i] != "10")
                {
                    Variable.inChipNum++;
                }
            }
        }
        #endregion

        #region 计算产出总数,良率
        public void OutNum(int num)
        {
            double totalnum = 0;
            double OKnum = 0;
            //读取Map文件
            string st = Application.StartupPath + @"\Map\" + num.ToString() + @"\tray";
            string[] ReadMap = myTXT.ReadTXT(st);

            string strPhoto = Application.StartupPath + @"\Data\Photo\Down\tray";
            string[] ReadPhoto = myTXT.ReadTXT(strPhoto);

            for (int i = 0; i < ReadPhoto.Length; i++)
            {
                if (ReadPhoto[i] == "10")
                {
                    ReadMap[i] = "03";//去掉拍照空位
                }
            }

            for (int i = 0; i < ReadMap.Length; i++)
            {
                //计算产出总数
                if (ReadMap[i] != "03")
                {
                    Variable.outChipNum++;
                    totalnum++;
                }

                //计算OK数
                if (ReadMap[i] == "00")
                {
                    Variable.OKChipNum++;
                    OKnum++;
                }

                Variable.YieldMode[num - 1] = Convert.ToString(Math.Round((OKnum / totalnum) * 100, 2)) + "%";
            }
        }
        #endregion

        #region 清除数据
        public void ClearInfoData()
        {
            //生产信息
            Variable.OP = "";//操作员
            Variable.BatchNum = "";//批号
            Variable.OrderNum = "";//工单号
            Variable.PONum = "";//PO号  
            infoDataGrid.Rows[0].Cells[1].Value = "";//操作员
            infoDataGrid.Rows[1].Cells[1].Value = "";//批号
            infoDataGrid.Rows[2].Cells[1].Value = "";//工单号
            infoDataGrid.Rows[3].Cells[1].Value = "";//PO号 
        }

        public void ClearRecordData()
        {
            //生产信息
            Variable.startTime = "0";//批开始时间
            Variable.runTime = "0";//运行时间
            Variable.stopTime = "0";//停止时间
            Variable.alarmTime = "0";//报警时间
            runSecond = 0;
            stopSecond = 0;
            alarmSecond = 0;

            Variable.inTrayNum = 0;//入盘数
            Variable.inTrayNumT = 0;//入盘数
            Variable.outTrayNum = 0;//出盘数
            Variable.inChipNum = 0;//投入总数
            Variable.outChipNum = 0;//产出总数
            Variable.OKChipNum = 0;//良品数
            Variable.Yield = 0;//总良率
            Variable.UPH = 0;//UPH
            //Variable.inNum = 0;//生产速度(/h)
            //Variable.inNum = 0;//测试时间(ms)
            Variable.jamRate = "0";//报警率(M/N)
            Variable.alarmCount = 0;
            Variable.inTrayNumRecord = 0;

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

        #region 记录异常日志
        public void WriteMessageToLog(string message)
        {
            try
            {
                string path = Application.StartupPath + "\\Trace.log";
                if (!System.IO.File.Exists(path))
                {
                    System.IO.File.Create(path).Close();
                }
                StreamWriter sw = System.IO.File.AppendText(path);
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + Application.StartupPath + " " + message);
                sw.Flush();
                sw.Close();
            }
            catch (Exception e)
            {
                string path = Application.StartupPath + "\\Trace.log";
                if (!System.IO.File.Exists(path))
                {
                    System.IO.File.Create(path).Close();
                }
                StreamWriter sw = System.IO.File.AppendText(path);
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + Application.StartupPath + e.Message);
                sw.Flush();
                sw.Close();
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

        #region 阵列计算
        public void ArrayCount()
        {
            #region 阵列坐标清空

            //上料空Tray盘坐标计算清空
            Variable.UpYNullTrayPositionA.Clear();
            Variable.UpYNullTrayPositionB.Clear();

            //上料待测Tray盘坐标计算清空
            Variable.UpYReadyTrayPositionA.Clear();
            Variable.UpYReadyTrayPositionB.Clear();

            //下料良品Tray盘坐标计算清空
            Variable.DownYOKTrayPositionA.Clear();
            Variable.DownYOKTrayPositionB.Clear();

            //下料OK备品Tray盘坐标计算清空
            Variable.DownYReadyPositionA.Clear();
            Variable.DownYReadyPositionB.Clear();

            //下料NGTray盘坐标计算清空
            Variable.DownYNGTrayPositionA.Clear();
            Variable.DownYNGTrayPositionB.Clear();

            //上料空Tray盘坐标计算清空
            Variable.UpXNullTrayPositionA.Clear();
            Variable.UpXNullTrayPositionB.Clear();

            //上料待测Tray盘坐标计算清空
            Variable.UpXReadyTrayPositionA.Clear();
            Variable.UpXReadyTrayPositionB.Clear();

            //下料良品Tray盘坐标计算清空
            Variable.DownXOKTrayPositionA.Clear();
            Variable.DownXOKTrayPositionB.Clear();

            //下料OK备品Tray盘坐标计算清空
            Variable.DownXReadyPositionA.Clear();
            Variable.DownXReadyPositionB.Clear();

            //下料NGTray盘坐标计算清空
            Variable.DownXNGTrayPositionA.Clear();
            Variable.DownXNGTrayPositionB.Clear();


            #endregion

            #region 阵列坐标计算

            for (int j = 0; j < Variable.RowNum; j++)//行循环-Y轴坐标
            {

                for (int i = 0; i < Variable.ListNum; i++)//列循环-X轴坐标
                {
                    ///A吸嘴X坐标
                    //上料空Tray盘坐标计算
                    Variable.UpXNullTrayPositionA.Add((Variable.AxisPos[1, 2] + Variable.offset[1] + i * Variable.ListSpacing));

                    //上料待测Tray盘坐标计算
                    Variable.UpXReadyTrayPositionA.Add((Variable.AxisPos[1, 1] + Variable.offset[7] + i * Variable.ListSpacing));

                    //下料良品Tray盘坐标计算
                    Variable.DownXOKTrayPositionA.Add((Variable.AxisPos[9, 1] + Variable.offset[13] + i * Variable.ListSpacing));

                    //下料OK备品Tray盘坐标计算
                    Variable.DownXReadyPositionA.Add((Variable.AxisPos[9, 2] + Variable.offset[21] + i * Variable.ListSpacing));

                    //下料NGTray盘坐标计算
                    Variable.DownXNGTrayPositionA.Add((Variable.AxisPos[9, 3] + Variable.offset[27] + i * Variable.ListSpacing));


                    ///B吸嘴X坐标
                    //上料空Tray盘坐标计算
                    Variable.UpXNullTrayPositionB.Add((Variable.AxisPos[1, 2] - Variable.UpABSpacing + Variable.offset[3] + i * Variable.ListSpacing));

                    //上料待测Tray盘坐标计算
                    Variable.UpXReadyTrayPositionB.Add((Variable.AxisPos[1, 1] - Variable.UpABSpacing + Variable.offset[9] + i * Variable.ListSpacing));

                    //下料良品Tray盘坐标计算
                    Variable.DownXOKTrayPositionB.Add((Variable.AxisPos[9, 1] - Variable.DownABSpacing + Variable.offset[15] + i * Variable.ListSpacing));

                    //下料OK备品Tray盘坐标计算
                    Variable.DownXReadyPositionB.Add((Variable.AxisPos[9, 2] - Variable.DownABSpacing + Variable.offset[23] + i * Variable.ListSpacing));

                    //下料NGTray盘坐标计算
                    Variable.DownXNGTrayPositionB.Add((Variable.AxisPos[9, 3] - Variable.DownABSpacing + Variable.offset[29] + i * Variable.ListSpacing));



                    ///A吸嘴Y坐标
                    //上料空Tray盘坐标计算
                    Variable.UpYNullTrayPositionA.Add((Variable.AxisPos[2, 3] + Variable.offset[2] - j * Variable.RowSpacing));

                    //上料待测Tray盘坐标计算
                    Variable.UpYReadyTrayPositionA.Add((Variable.AxisPos[3, 3] + Variable.offset[8] - j * Variable.RowSpacing));

                    //下料良品Tray盘坐标计算
                    Variable.DownYOKTrayPositionA.Add((Variable.AxisPos[10, 1] + Variable.offset[14] - j * Variable.RowSpacing));

                    //下料OK备品Tray盘坐标计算
                    Variable.DownYReadyPositionA.Add((Variable.AxisPos[11, 2] + Variable.offset[22] - j * Variable.RowSpacing));

                    //下料NGTray盘坐标计算
                    Variable.DownYNGTrayPositionA.Add((Variable.AxisPos[12, 2] + Variable.offset[28] - j * Variable.RowSpacing));


                    ///B吸嘴Y坐标
                    //上料空Tray盘坐标计算
                    Variable.UpYNullTrayPositionB.Add((Variable.AxisPos[2, 3] + Variable.offset[4] - j * Variable.RowSpacing));

                    //上料待测Tray盘坐标计算
                    Variable.UpYReadyTrayPositionB.Add((Variable.AxisPos[3, 3] + Variable.offset[10] - j * Variable.RowSpacing));

                    //下料良品Tray盘坐标计算
                    Variable.DownYOKTrayPositionB.Add((Variable.AxisPos[10, 1] + Variable.offset[16] - j * Variable.RowSpacing));

                    //下料OK备品Tray盘坐标计算
                    Variable.DownYReadyPositionB.Add((Variable.AxisPos[11, 2] + Variable.offset[24] - j * Variable.RowSpacing));

                    //下料NGTray盘坐标计算
                    Variable.DownYNGTrayPositionB.Add((Variable.AxisPos[12, 2] + Variable.offset[30] - j * Variable.RowSpacing));
                }

            }

            #endregion

        }

        #endregion

        #region 回原点

        //轴1回原点
        public void Axis1GoHome()
        {
            if (Variable.XStatus[49])
            {
                if (Variable.XStatus[59] && Variable.XStatus[61])
                {
                    if (Variable.AIMpos[4] <= Math.Round(Variable.AxisPos[4, 0] + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(Variable.AxisPos[4, 0] - 0.1, 2))
                    {
                        short homeMode = 11;// 回原点模式
                        short moveDir = -1;//负方向回零
                        short indexDir = 1;
                        //double Vel = Variable.AxisRealRunVel[1] * Variable.AxisPulse[1] / (Variable.AxisPitch[1] * 1000);
                        double velHigh = Variable.AxisHmoeVelHight[1] * Variable.AxisPulse[1] / (Variable.AxisPitch[1] * 1000);
                        double velLow = Variable.AxisHmoeVelLow[1] * Variable.AxisPulse[1] / (Variable.AxisPitch[1] * 1000);
                        int escapeStep = 200;
                        motion.SmartHome1(1, 1, homeMode, moveDir, indexDir, velHigh, velLow, escapeStep);
                    }
                    else
                    {
                        Variable.UpAxisAlarm = true;
                        ListBoxTxt("上料吸嘴Z轴不在待机位，请确认！");
                    }
                }
                else
                {
                    Variable.AlarmHappen[59] = true;
                    ListBoxTxt("上料吸嘴气缸不在上位，请确认！");
                }
            }
            else
            {
                Variable.AlarmHappen[49] = true;
                ListBoxTxt("上料待测Tray工位2上顶气缸不在下位，请确认！");
            }
        }

        //轴2回原点
        public void Axis2GoHome()
        {
            if (Variable.XStatus[59] && Variable.XStatus[61])
            {
                if (Variable.AIMpos[4] <= Math.Round(Variable.AxisPos[4, 0] + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(Variable.AxisPos[4, 0] - 0.1, 2))
                {
                    short homeMode = 11;// 回原点模式
                    short moveDir = 1;//正方向回零
                    short indexDir = 1;
                    double velHigh = Variable.AxisHmoeVelHight[2] * Variable.AxisPulse[2] / (Variable.AxisPitch[2] * 1000);
                    double velLow = Variable.AxisHmoeVelLow[2] * Variable.AxisPulse[2] / (Variable.AxisPitch[2] * 1000);
                    int escapeStep = 200;
                    motion.SmartHome1(1, 2, homeMode, moveDir, indexDir, velHigh, velLow, escapeStep);
                }
                else
                {
                    Variable.UpAxisAlarm = true;
                    ListBoxTxt("上料吸嘴Z轴不在待机位，请确认！");
                }
            }
            else
            {
                Variable.AlarmHappen[59] = true;
                ListBoxTxt("上料吸嘴气缸不在上位，请确认！");
            }
        }

        //轴3回原点
        public void Axis3GoHome()
        {
            if (Variable.XStatus[59] && Variable.XStatus[61])
            {
                if (Variable.AIMpos[4] <= Math.Round(Variable.AxisPos[4, 0] + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(Variable.AxisPos[4, 0] - 0.1, 2))
                {
                    short homeMode = 11;// 回原点模式
                    short moveDir = 1;//正方向回零
                    short indexDir = 1;
                    double velHigh = Variable.AxisHmoeVelHight[3] * Variable.AxisPulse[3] / (Variable.AxisPitch[3] * 1000);
                    double velLow = Variable.AxisHmoeVelLow[3] * Variable.AxisPulse[3] / (Variable.AxisPitch[3] * 1000);
                    int escapeStep = 200;
                    motion.SmartHome1(1, 3, homeMode, moveDir, indexDir, velHigh, velLow, escapeStep);
                }
                else
                {
                    Variable.UpAxisAlarm = true;
                    ListBoxTxt("上料吸嘴Z轴不在待机位，请确认！");
                }
            }
            else
            {
                Variable.AlarmHappen[59] = true;
                ListBoxTxt("上料吸嘴气缸不在上位，请确认！");
            }
        }

        //轴4回原点
        public void Axis4GoHome()
        {
            short homeMode = 10;// 回原点模式
            short moveDir = -1;//负方向回零
            short indexDir = 1;
            double velHigh = Variable.AxisHmoeVelHight[4] * Variable.AxisPulse[4] / (Variable.AxisPitch[4] * 1000);
            double velLow = Variable.AxisHmoeVelLow[4] * Variable.AxisPulse[4] / (Variable.AxisPitch[4] * 1000);
            int escapeStep = 200;

            motion.SmartHome1(1, 4, homeMode, moveDir, indexDir, velHigh, velLow, escapeStep);
        }

        //轴5回原点
        public void Axis5GoHome()
        {
            short homeMode = 10;// 回原点模式
            short moveDir = 1;//正方向回零
            short indexDir = 1;
            double velHigh = Variable.AxisHmoeVelHight[5] * Variable.AxisPulse[5] / (Variable.AxisPitch[5] * 1000);
            double velLow = Variable.AxisHmoeVelLow[5] * Variable.AxisPulse[5] / (Variable.AxisPitch[5] * 1000);
            int escapeStep = 200;

            motion.SmartHome1(1, 5, homeMode, moveDir, indexDir, velHigh, velLow, escapeStep);
        }

        //轴6回原点
        public void Axis6GoHome()
        {
            short homeMode = 10;// 回原点模式
            short moveDir = 1;//正方向回零
            short indexDir = 1;
            double velHigh = Variable.AxisHmoeVelHight[6] * Variable.AxisPulse[6] / (Variable.AxisPitch[6] * 1000);
            double velLow = Variable.AxisHmoeVelLow[6] * Variable.AxisPulse[6] / (Variable.AxisPitch[6] * 1000);
            int escapeStep = 200;

            motion.SmartHome1(1, 6, homeMode, moveDir, indexDir, velHigh, velLow, escapeStep);
        }

        //轴7回原点
        public void Axis7GoHome()
        {
            short homeMode = 10;// 回原点模式
            short moveDir = 1;//正方向回零
            short indexDir = 1;
            double velHigh = Variable.AxisHmoeVelHight[7] * Variable.AxisPulse[7] / (Variable.AxisPitch[7] * 1000);
            double velLow = Variable.AxisHmoeVelLow[7] * Variable.AxisPulse[7] / (Variable.AxisPitch[7] * 1000);
            int escapeStep = 200;

            motion.SmartHome1(1, 7, homeMode, moveDir, indexDir, velHigh, velLow, escapeStep);
        }

        //轴8回原点
        public void Axis8GoHome()
        {
            short homeMode = 11;// 回原点模式
            short moveDir = 1;//正方向回零
            short indexDir = 1;
            double velHigh = Variable.AxisHmoeVelHight[8] * Variable.AxisPulse[8] / (Variable.AxisPitch[8] * 1000);
            double velLow = Variable.AxisHmoeVelLow[8] * Variable.AxisPulse[8] / (Variable.AxisPitch[8] * 1000);
            int escapeStep = 200;

            motion.SmartHome1(1, 8, homeMode, moveDir, indexDir, velHigh, velLow, escapeStep);
        }

        //轴9回原点
        public void Axis9GoHome()
        {
            if (Variable.XStatus[72])
            {
                if (Variable.XStatus[98] && Variable.XStatus[100])
                {
                    if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2))
                    {
                        short homeMode = 11;// 回原点模式
                        short moveDir = -1;//负方向回零
                        short indexDir = 1;
                        double velHigh = Variable.AxisHmoeVelHight[9] * Variable.AxisPulse[9] / (Variable.AxisPitch[9] * 1000);
                        double velLow = Variable.AxisHmoeVelLow[9] * Variable.AxisPulse[9] / (Variable.AxisPitch[9] * 1000);
                        int escapeStep = 200;
                        motion.SmartHome1(1, 9, homeMode, moveDir, indexDir, velHigh, velLow, escapeStep);
                    }
                    else
                    {
                        Variable.DownAxisAlarm = true;
                        ListBoxTxt("下料吸嘴Z轴不在待机位，请确认！");
                    }
                }
                else
                {
                    Variable.AlarmHappen[98] = true;
                    ListBoxTxt("下料吸嘴气缸不在上位，请确认！");
                }
            }
            else
            {
                Variable.AlarmHappen[72] = true;
                ListBoxTxt("下料良品Tray盘工位2上顶气缸不在下位，请确认！");
            }
        }

        //轴10回原点
        public void Axis10GoHome()
        {
            if (Variable.XStatus[98] && Variable.XStatus[100])
            {
                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2))
                {
                    short homeMode = 11;// 回原点模式
                    short moveDir = 1;//正方向回零
                    short indexDir = 1;
                    double velHigh = Variable.AxisHmoeVelHight[10] * Variable.AxisPulse[10] / (Variable.AxisPitch[10] * 1000);
                    double velLow = Variable.AxisHmoeVelLow[10] * Variable.AxisPulse[10] / (Variable.AxisPitch[10] * 1000);
                    int escapeStep = 200;
                    motion.SmartHome1(1, 10, homeMode, moveDir, indexDir, velHigh, velLow, escapeStep);
                }
                else
                {
                    Variable.DownAxisAlarm = true;
                    ListBoxTxt("下料吸嘴Z轴不在待机位，请确认！");
                }
            }
            else
            {
                Variable.AlarmHappen[98] = true;
                ListBoxTxt("下料吸嘴气缸不在上位，请确认！");
            }
        }

        //轴11回原点
        public void Axis11GoHome()
        {
            if (Variable.XStatus[98] && Variable.XStatus[100])
            {
                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2))
                {
                    short homeMode = 11;// 回原点模式
                    short moveDir = 1;//正方向回零
                    short indexDir = 1;
                    double velHigh = Variable.AxisHmoeVelHight[11] * Variable.AxisPulse[11] / (Variable.AxisPitch[11] * 1000);
                    double velLow = Variable.AxisHmoeVelLow[11] * Variable.AxisPulse[11] / (Variable.AxisPitch[11] * 1000);
                    int escapeStep = 200;
                    motion.SmartHome1(1, 11, homeMode, moveDir, indexDir, velHigh, velLow, escapeStep);
                }
                else
                {
                    Variable.DownAxisAlarm = true;
                    ListBoxTxt("下料吸嘴Z轴不在待机位，请确认！");
                }
            }
            else
            {
                Variable.AlarmHappen[98] = true;
                ListBoxTxt("下料吸嘴气缸不在上位，请确认！");
            }
        }

        //轴12回原点
        public void Axis12GoHome()
        {
            if (Variable.XStatus[98] && Variable.XStatus[100])
            {
                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2))
                {
                    short homeMode = 11;// 回原点模式
                    short moveDir = 1;//正方向回零
                    short indexDir = 1;
                    double velHigh = Variable.AxisHmoeVelHight[12] * Variable.AxisPulse[12] / (Variable.AxisPitch[12] * 1000);
                    double velLow = Variable.AxisHmoeVelLow[12] * Variable.AxisPulse[12] / (Variable.AxisPitch[12] * 1000);
                    int escapeStep = 200;
                    motion.SmartHome1(1, 12, homeMode, moveDir, indexDir, velHigh, velLow, escapeStep);
                }
                else
                {
                    Variable.DownAxisAlarm = true;
                    ListBoxTxt("下料吸嘴Z轴不在待机位，请确认！");
                }
            }
            else
            {
                Variable.AlarmHappen[98] = true;
                ListBoxTxt("下料吸嘴气缸不在上位，请确认！");
            }
        }

        //轴13回原点
        public void Axis13GoHome()
        {
            short homeMode = 10;// 回原点模式
            short moveDir = -1;//负方向回零
            short indexDir = 1;
            double velHigh = Variable.AxisHmoeVelHight[13] * Variable.AxisPulse[13] / (Variable.AxisPitch[13] * 1000);
            double velLow = Variable.AxisHmoeVelLow[13] * Variable.AxisPulse[13] / (Variable.AxisPitch[13] * 1000);
            int escapeStep = 200;

            motion.SmartHome2(2, 1, homeMode, moveDir, indexDir, velHigh, velLow, escapeStep);
        }

        //轴14回原点
        public void Axis14GoHome()
        {
            short homeMode = 10;// 回原点模式
            short moveDir = 1;//正方向回零
            short indexDir = 1;
            double velHigh = Variable.AxisHmoeVelHight[14] * Variable.AxisPulse[14] / (Variable.AxisPitch[14] * 1000);
            double velLow = Variable.AxisHmoeVelLow[14] * Variable.AxisPulse[14] / (Variable.AxisPitch[14] * 1000);
            int escapeStep = 200;

            motion.SmartHome2(2, 2, homeMode, moveDir, indexDir, velHigh, velLow, escapeStep);
        }

        //轴15回原点
        public void Axis15GoHome()
        {
            if (Variable.XStatus[102])
            {
                short homeMode = 11;// 回原点模式
                short moveDir = -1;//负方向回零
                short indexDir = 1;
                double velHigh = Variable.AxisHmoeVelHight[15] * Variable.AxisPulse[15] / (Variable.AxisPitch[15] * 1000);
                double velLow = Variable.AxisHmoeVelLow[15] * Variable.AxisPulse[15] / (Variable.AxisPitch[15] * 1000);
                int escapeStep = 200;

                motion.SmartHome2(2, 3, homeMode, moveDir, indexDir, velHigh, velLow, escapeStep);
            }
            else
            {
                Variable.AlarmHappen[102] = true;
                ListBoxTxt("移Tray夹爪上下气缸不在上位，请确认！");
            }
        }

        //轴16回原点
        public void Axis16GoHome()
        {
            short homeMode = 11;// 回原点模式
            short moveDir = -1;//负方向回零
            short indexDir = 1;
            double velHigh = Variable.AxisHmoeVelHight[16] * Variable.AxisPulse[16] / (Variable.AxisPitch[16] * 1000);
            double velLow = Variable.AxisHmoeVelLow[16] * Variable.AxisPulse[16] / (Variable.AxisPitch[16] * 1000);
            int escapeStep = 200;

            motion.SmartHome2(2, 4, homeMode, moveDir, indexDir, velHigh, velLow, escapeStep);
        }

        #endregion

        #region 定长运动

        public void Axis1SetMove(double point)
        {
            if (Variable.XStatus[49])
            {
                if (Variable.XStatus[59] && Variable.XStatus[61])
                {
                    if (Variable.AIMpos[4] <= Math.Round(Variable.AxisPos[4, 0] + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(Variable.AxisPos[4, 0] - 0.1, 2))
                    {
                        mc.GTN_ClrSts(1, 1, 1);
                        double Vel = Variable.AxisRealRunVel[1] * Variable.AxisPulse[1] / (Variable.AxisPitch[1] * 1000);
                        motion.P2PAbs(1, 1, Vel, Variable.AxisTacc[1], Variable.AxisTdec[1], Variable.AxisStartVel[1], Variable.AxisSmoothTime[1], (int)(point / Variable.AxisPitch[1] * Variable.AxisPulse[1]));
                    }
                    else
                    {
                        Variable.UpAxisAlarm = true;
                        ListBoxTxt("上料吸嘴Z轴不在待机位，请确认！");
                    }
                }
                else
                {
                    Variable.AlarmHappen[59] = true;
                    ListBoxTxt("上料吸嘴气缸不在上位，请确认！");
                }
            }
            else
            {
                Variable.AlarmHappen[49] = true;
                ListBoxTxt("上料待测Tray工位2上顶气缸不在下位，请确认！");
            }
        }

        public void Axis2SetMove(double point)
        {
            if (Variable.XStatus[59] && Variable.XStatus[61])
            {
                if (Variable.AIMpos[4] <= Math.Round(Variable.AxisPos[4, 0] + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(Variable.AxisPos[4, 0] - 0.1, 2))
                {
                    mc.GTN_ClrSts(1, 2, 1);
                    double Vel = Variable.AxisRealRunVel[2] * Variable.AxisPulse[2] / (Variable.AxisPitch[2] * 1000);
                    motion.P2PAbs(1, 2, Vel, Variable.AxisTacc[2], Variable.AxisTdec[2], Variable.AxisStartVel[2], Variable.AxisSmoothTime[2], (int)(point / Variable.AxisPitch[2] * Variable.AxisPulse[2]));
                }
                else
                {
                    Variable.UpAxisAlarm = true;
                    ListBoxTxt("上料吸嘴Z轴不在待机位，请确认！");
                }
            }
            else
            {
                Variable.AlarmHappen[59] = true;
                ListBoxTxt("上料吸嘴气缸不在上位，请确认！");
            }
        }

        public void Axis3SetMove(double point)
        {
            if (Variable.XStatus[59] && Variable.XStatus[61])
            {
                if (Variable.AIMpos[4] <= Math.Round(Variable.AxisPos[4, 0] + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(Variable.AxisPos[4, 0] - 0.1, 2))
                {
                    mc.GTN_ClrSts(1, 3, 1);
                    double Vel = Variable.AxisRealRunVel[3] * Variable.AxisPulse[3] / (Variable.AxisPitch[3] * 1000);
                    motion.P2PAbs(1, 3, Vel, Variable.AxisTacc[3], Variable.AxisTdec[3], Variable.AxisStartVel[3], Variable.AxisSmoothTime[3], (int)(point / Variable.AxisPitch[3] * Variable.AxisPulse[3]));
                }
                else
                {
                    Variable.UpAxisAlarm = true;
                    ListBoxTxt("上料吸嘴Z轴不在待机位，请确认！");
                }
            }
            else
            {
                Variable.AlarmHappen[59] = true;
                ListBoxTxt("上料吸嘴气缸不在上位，请确认！");
            }
        }

        public void Axis4SetMove(double point)
        {
            mc.GTN_ClrSts(1, 4, 1);
            double Vel = Variable.AxisRealRunVel[4] * Variable.AxisPulse[4] / (Variable.AxisPitch[4] * 1000);
            motion.P2PAbs(1, 4, Vel, Variable.AxisTacc[4], Variable.AxisTdec[4], Variable.AxisStartVel[4], Variable.AxisSmoothTime[4], (int)(point / Variable.AxisPitch[4] * Variable.AxisPulse[4]));
        }

        public void Axis5SetMove(double point)
        {
            mc.GTN_ClrSts(1, 5, 1);
            double Vel = Variable.AxisRealRunVel[5] * Variable.AxisPulse[5] / (Variable.AxisPitch[5] * 1000);
            motion.P2PAbs(1, 5, Vel, Variable.AxisTacc[5], Variable.AxisTdec[5], Variable.AxisStartVel[5], Variable.AxisSmoothTime[5], (int)(point / Variable.AxisPitch[5] * Variable.AxisPulse[5]));
        }

        public void Axis6SetMove(double point)
        {
            mc.GTN_ClrSts(1, 6, 1);
            double Vel = Variable.AxisRealRunVel[6] * Variable.AxisPulse[6] / (Variable.AxisPitch[6] * 1000);
            motion.P2PAbs(1, 6, Vel, Variable.AxisTacc[6], Variable.AxisTdec[6], Variable.AxisStartVel[6], Variable.AxisSmoothTime[6], (int)(point / Variable.AxisPitch[6] * Variable.AxisPulse[6]));
        }

        public void Axis7SetMove(double point)
        {
            mc.GTN_ClrSts(1, 7, 1);
            double Vel = Variable.AxisRealRunVel[7] * Variable.AxisPulse[7] / (Variable.AxisPitch[7] * 1000);
            motion.P2PAbs(1, 7, Vel, Variable.AxisTacc[7], Variable.AxisTdec[7], Variable.AxisStartVel[7], Variable.AxisSmoothTime[7], (int)(point / Variable.AxisPitch[7] * Variable.AxisPulse[7]));
        }

        public void Axis8SetMove(double point)
        {
            mc.GTN_ClrSts(1, 8, 1);
            double Vel = Variable.AxisRealRunVel[8] * Variable.AxisPulse[8] / (Variable.AxisPitch[8] * 1000);
            motion.P2PAbs(1, 8, Vel, Variable.AxisTacc[8], Variable.AxisTdec[8], Variable.AxisStartVel[8], Variable.AxisSmoothTime[8], (int)(point / Variable.AxisPitch[8] * Variable.AxisPulse[8]));
        }

        public void Axis9SetMove(double point)
        {
            //if (Variable.XStatus[72])
            //{
            if (Variable.XStatus[98] && Variable.XStatus[100])
            {
                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2))
                {
                    mc.GTN_ClrSts(1, 9, 1);
                    double Vel = Variable.AxisRealRunVel[9] * Variable.AxisPulse[9] / (Variable.AxisPitch[9] * 1000);
                    motion.P2PAbs(1, 9, Vel, Variable.AxisTacc[9], Variable.AxisTdec[9], Variable.AxisStartVel[9], Variable.AxisSmoothTime[9], (int)(point / Variable.AxisPitch[9] * Variable.AxisPulse[9]));
                }
                else
                {
                    Variable.DownAxisAlarm = true;
                    ListBoxTxt("下料吸嘴Z轴不在待机位，请确认！");
                }
            }
            else
            {
                Variable.AlarmHappen[98] = true;
                ListBoxTxt("下料吸嘴气缸不在上位，请确认！");
            }
            //}
            //else
            //{
            //    Variable.AlarmHappen[72] = true;
            //    ListBoxTxt("下料良品Tray盘工位2上顶气缸不在下位，请确认！");
            //}
        }

        public void Axis10SetMove(double point)
        {
            //if (Variable.XStatus[98] && Variable.XStatus[100])
            //{
            //if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2))
            //{
            mc.GTN_ClrSts(1, 10, 1);
            double Vel = Variable.AxisRealRunVel[10] * Variable.AxisPulse[10] / (Variable.AxisPitch[10] * 1000);
            motion.P2PAbs(1, 10, Vel, Variable.AxisTacc[10], Variable.AxisTdec[10], Variable.AxisStartVel[10], Variable.AxisSmoothTime[10], (int)(point / Variable.AxisPitch[10] * Variable.AxisPulse[10]));
            //}
            //else
            //{
            //    Variable.DownAxisAlarm = true;
            //    ListBoxTxt("下料吸嘴Z轴不在待机位，请确认！");
            //}
            //}
            //else
            //{
            //    Variable.AlarmHappen[98] = true;
            //    ListBoxTxt("下料吸嘴气缸不在上位，请确认！");
            //}
        }

        public void Axis11SetMove(double point)
        {
            if (Variable.XStatus[98] && Variable.XStatus[100])
            {
                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2))
                {
                    mc.GTN_ClrSts(1, 11, 1);
                    double Vel = Variable.AxisRealRunVel[11] * Variable.AxisPulse[11] / (Variable.AxisPitch[11] * 1000);
                    motion.P2PAbs(1, 11, Vel, Variable.AxisTacc[11], Variable.AxisTdec[11], Variable.AxisStartVel[11], Variable.AxisSmoothTime[11], (int)(point / Variable.AxisPitch[11] * Variable.AxisPulse[11]));
                }
                else
                {
                    Variable.DownAxisAlarm = true;
                    ListBoxTxt("下料吸嘴Z轴不在待机位，请确认！");
                }
            }
            else
            {
                Variable.AlarmHappen[98] = true;
                ListBoxTxt("下料吸嘴气缸不在上位，请确认！");
            }
        }

        public void Axis12SetMove(double point)
        {
            if (Variable.XStatus[98] && Variable.XStatus[100])
            {
                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2))
                {
                    mc.GTN_ClrSts(1, 12, 1);
                    double Vel = Variable.AxisRealRunVel[12] * Variable.AxisPulse[12] / (Variable.AxisPitch[12] * 1000);
                    motion.P2PAbs(1, 12, Vel, Variable.AxisTacc[12], Variable.AxisTdec[12], Variable.AxisStartVel[12], Variable.AxisSmoothTime[12], (int)(point / Variable.AxisPitch[12] * Variable.AxisPulse[12]));
                }
                else
                {
                    Variable.DownAxisAlarm = true;
                    ListBoxTxt("下料吸嘴Z轴不在待机位，请确认！");
                }
            }
            else
            {
                Variable.AlarmHappen[98] = true;
                ListBoxTxt("下料吸嘴气缸不在上位，请确认！");
            }
        }

        public void Axis13SetMove(double point)
        {
            mc.GTN_ClrSts(2, 1, 1);
            double Vel = Variable.AxisRealRunVel[13] * Variable.AxisPulse[13] / (Variable.AxisPitch[13] * 1000);
            motion.P2PAbs(2, 1, Vel, Variable.AxisTacc[13], Variable.AxisTdec[13], Variable.AxisStartVel[13], Variable.AxisSmoothTime[13], (int)(point / Variable.AxisPitch[13] * Variable.AxisPulse[13]));
        }

        public void Axis14SetMove(double point)
        {
            mc.GTN_ClrSts(2, 2, 1);
            double Vel = Variable.AxisRealRunVel[14] * Variable.AxisPulse[14] / (Variable.AxisPitch[14] * 1000);
            motion.P2PAbs(2, 2, Vel, Variable.AxisTacc[14], Variable.AxisTdec[14], Variable.AxisStartVel[14], Variable.AxisSmoothTime[14], (int)(point / Variable.AxisPitch[14] * Variable.AxisPulse[14]));
        }

        public void Axis15SetMove(double point)
        {
            if (Variable.XStatus[102])
            {
                mc.GTN_ClrSts(2, 3, 1);
                double Vel = Variable.AxisRealRunVel[15] * Variable.AxisPulse[15] / (Variable.AxisPitch[15] * 1000);
                motion.P2PAbs(2, 3, Vel, Variable.AxisTacc[15], Variable.AxisTdec[15], Variable.AxisStartVel[15], Variable.AxisSmoothTime[15], (int)(point / Variable.AxisPitch[15] * Variable.AxisPulse[15]));
            }
            else
            {
                Variable.AlarmHappen[102] = true;
                ListBoxTxt("移Tray夹爪上下气缸不在上位，请确认！");
            }
        }

        public void Axis16SetMove(double point)
        {
            mc.GTN_ClrSts(2, 4, 1);
            double Vel = Variable.AxisRealRunVel[16] * Variable.AxisPulse[16] / (Variable.AxisPitch[16] * 1000);
            motion.P2PAbs(2, 4, Vel, Variable.AxisTacc[16], Variable.AxisTdec[16], Variable.AxisStartVel[16], Variable.AxisSmoothTime[16], (int)(point / Variable.AxisPitch[16] * Variable.AxisPulse[16]));
        }

        #endregion

        #region 直线插补

        //上料待测轴插补
        public void UpReadyLineMove(double Xpos, double Ypos)
        {
            if (Variable.XStatus[49])
            {
                if (Variable.XStatus[59] && Variable.XStatus[61])
                {
                    if (Variable.AIMpos[4] <= Math.Round(Variable.AxisPos[4, 0] + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(Variable.AxisPos[4, 0] - 0.1, 2))
                    {
                        short[] axisNo = new short[] { 0, 2 };
                        short axisNo1 = 0;
                        short axisNo2 = 2;
                        double Vel = Variable.AxisRealRunVel[1] * Variable.AxisPulse[1] / (Variable.AxisPitch[1] * 1000);
                        motion.Line1Move1(axisNo, axisNo1, axisNo2, Vel, Variable.AxisTacc[1], (int)(Xpos / Variable.AxisPitch[1] * Variable.AxisPulse[1]), (int)(Ypos / Variable.AxisPitch[3] * Variable.AxisPulse[3]));
                    }
                    else
                    {
                        Variable.UpAxisAlarm = true;
                        ListBoxTxt("上料吸嘴Z轴不在待机位，请确认！");
                    }
                }
                else
                {
                    Variable.AlarmHappen[59] = true;
                    ListBoxTxt("上料吸嘴气缸不在上位，请确认！");
                }
            }
            else
            {
                Variable.AlarmHappen[49] = true;
                ListBoxTxt("上料待测Tray工位2上顶气缸不在下位，请确认！");
            }
        }

        //上料空盘轴插补
        public void UpEmptyLineMove(double Xpos, double Ypos)
        {
            if (Variable.XStatus[49])
            {
                if (Variable.XStatus[59] && Variable.XStatus[61])
                {
                    if (Variable.AIMpos[4] <= Math.Round(Variable.AxisPos[4, 0] + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(Variable.AxisPos[4, 0] - 0.1, 2))
                    {
                        short[] axisNo = new short[] { 0, 1 };
                        short axisNo1 = 0;
                        short axisNo2 = 1;
                        double Vel = Variable.AxisRealRunVel[1] * Variable.AxisPulse[1] / (Variable.AxisPitch[1] * 1000);
                        motion.Line1Move1(axisNo, axisNo1, axisNo2, Vel, Variable.AxisTacc[1], (int)(Xpos / Variable.AxisPitch[1] * Variable.AxisPulse[1]), (int)(Ypos / Variable.AxisPitch[2] * Variable.AxisPulse[2]));
                    }
                    else
                    {
                        Variable.UpAxisAlarm = true;
                        ListBoxTxt("上料吸嘴Z轴不在待机位，请确认！");
                    }
                }
                else
                {
                    Variable.AlarmHappen[59] = true;
                    ListBoxTxt("上料吸嘴气缸不在上位，请确认！");
                }
            }
            else
            {
                Variable.AlarmHappen[49] = true;
                ListBoxTxt("上料待测Tray工位2上顶气缸不在下位，请确认！");
            }
        }

        //下料良品轴插补
        public void DownOKLineMove(double Xpos, double Ypos)
        {
            if (Variable.XStatus[72])
            {
                if (Variable.XStatus[98] && Variable.XStatus[100])
                {
                    if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2))
                    {
                        short[] axisNo = new short[] { 0, 1 };
                        short axisNo1 = 0;
                        short axisNo2 = 1;
                        double Vel = Variable.AxisRealRunVel[9] * Variable.AxisPulse[9] / (Variable.AxisPitch[9] * 1000);
                        motion.Line1Move2(axisNo, axisNo1, axisNo2, Vel, Variable.AxisTacc[9], (int)(Xpos / Variable.AxisPitch[9] * Variable.AxisPulse[9]), (int)(Ypos / Variable.AxisPitch[10] * Variable.AxisPulse[10]));
                    }
                    else
                    {
                        Variable.DownAxisAlarm = true;
                        ListBoxTxt("下料吸嘴Z轴不在待机位，请确认！");
                    }
                }
                else
                {
                    Variable.AlarmHappen[98] = true;
                    ListBoxTxt("下料吸嘴气缸不在上位，请确认！");
                }
            }
            else
            {
                Variable.AlarmHappen[72] = true;
                ListBoxTxt("下料良品Tray盘工位2上顶气缸不在下位，请确认！");
            }
        }

        //下料补料轴插补
        public void DownFillLineMove(double Xpos, double Ypos)
        {
            //if (Variable.XStatus[72])
            //{
            if (Variable.XStatus[98] && Variable.XStatus[100])
            {
                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2))
                {
                    short[] axisNo = new short[] { 0, 2 };
                    short axisNo1 = 0;
                    short axisNo2 = 2;
                    double Vel = Variable.AxisRealRunVel[9] * Variable.AxisPulse[9] / (Variable.AxisPitch[9] * 1000);
                    motion.Line1Move2(axisNo, axisNo1, axisNo2, Vel, Variable.AxisTacc[9], (int)(Xpos / Variable.AxisPitch[9] * Variable.AxisPulse[9]), (int)(Ypos / Variable.AxisPitch[11] * Variable.AxisPulse[11]));
                }
                else
                {
                    Variable.DownAxisAlarm = true;
                    ListBoxTxt("下料吸嘴Z轴不在待机位，请确认！");
                }
            }
            else
            {
                Variable.AlarmHappen[98] = true;
                ListBoxTxt("下料吸嘴气缸不在上位，请确认！");
            }
            //}
            //else
            //{
            //    Variable.AlarmHappen[72] = true;
            //    ListBoxTxt("下料良品Tray盘工位2上顶气缸不在下位，请确认！");
            //}
        }

        //下料NG轴插补
        public void DownNGLineMove(double Xpos, double Ypos)
        {
            //if (Variable.XStatus[72])
            //{
            if (Variable.XStatus[98] && Variable.XStatus[100])
            {
                if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2))
                {
                    short[] axisNo = new short[] { 0, 3 };
                    short axisNo1 = 0;
                    short axisNo2 = 3;
                    double Vel = Variable.AxisRealRunVel[9] * Variable.AxisPulse[9] / (Variable.AxisPitch[9] * 1000);
                    motion.Line1Move2(axisNo, axisNo1, axisNo2, Vel, Variable.AxisTacc[9], (int)(Xpos / Variable.AxisPitch[9] * Variable.AxisPulse[9]), (int)(Ypos / Variable.AxisPitch[12] * Variable.AxisPulse[12]));
                }
                else
                {
                    Variable.DownAxisAlarm = true;
                    ListBoxTxt("下料吸嘴Z轴不在待机位，请确认！");
                }
            }
            else
            {
                Variable.AlarmHappen[98] = true;
                ListBoxTxt("下料吸嘴气缸不在上位，请确认！");
            }
            //}
            //else
            //{
            //    Variable.AlarmHappen[72] = true;
            //    ListBoxTxt("下料良品Tray盘工位2上顶气缸不在下位，请确认！");
            //}
        }

        #endregion

        #region 档案加载
        private void btnLoad_Click(object sender, EventArgs e)
        {
            //加载参数  
            parameterForm.LoadPoint(Application.StartupPath + "\\Point.ini");
            string path = @"D:\参数\" + ModelCombo.Text;
            parameterForm.LoadParameter(path);
            iniHelper.writeIni("PGM", "FileName", Variable.FileName, Application.StartupPath + "//parameter.ini");

            Variable.modeNameFlag = true;
        }
        #endregion

        #region 将秒转换成HMS
        private string sec_to_hms(long duration)
        {
            TimeSpan ts = new TimeSpan(0, 0, Convert.ToInt32(duration));
            string str = "";
            if (ts.Hours > 0)
            {
                str = String.Format("{0:00}", ts.Hours) + ":" + String.Format("{0:00}", ts.Minutes) + ":" + String.Format("{0:00}", ts.Seconds);
            }
            if (ts.Hours == 0 && ts.Minutes > 0)
            {
                str = "00:" + String.Format("{0:00}", ts.Minutes) + ":" + String.Format("{0:00}", ts.Seconds);
            }
            if (ts.Hours == 0 && ts.Minutes == 0)
            {
                str = "00:00:" + String.Format("{0:00}", ts.Seconds);
            }
            return str;
        }
        #endregion

        #region 设置窗体调用一次
        //设置窗体只打开一次方法
        //以下两句为调用方法
        //POPForm f2 = GenericSingleton<POPForm>.CreateInstrance();
        //f2.Show();
        public class GenericSingleton<T> where T : Form, new()
        {
            private static T t = null;
            public static T CreateInstrance()
            {
                if (t == null || t.IsDisposed)
                {
                    t = new T();
                }
                else
                {
                    t.Activate(); //如果已经打开过就让其获得焦点  
                    t.WindowState = FormWindowState.Normal;//使Form恢复正常窗体大小
                }
                return t;
            }
        }
        #endregion

        #region 将整数转换二进制
        public string IntToBin(int data, int a)
        {
            string cnt = "0";//记录转换过后二进制值
            try
            {
                cnt = Convert.ToString(data, 2);
                if (cnt.Length < 32)
                {
                    int c = cnt.Length;
                    for (int b = 0; b < (32 - c); b++)
                    {
                        cnt = "0" + cnt;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(new StackTrace(new StackFrame(true)), new StackFrame(), ex);
            }
            return cnt.Substring(31 - a, 1);
        }

        #endregion

        #region 页面切换

        //测试模组显示
        public void LoadSubForm(object form)
        {
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

        private void 测试模组1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseForm();
            foreach (Control c in SubPanel.Controls)
            {
                SubPanel.Controls.Remove(c);
                c.Dispose();
            }
            LoadSubForm(new Tray1Form());
        }

        private void 测试模组2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseForm();
            foreach (Control c in SubPanel.Controls)
            {
                SubPanel.Controls.Remove(c);
                c.Dispose();
            }
            LoadSubForm(new Tray2Form());
        }

        private void 测试模组3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseForm();
            foreach (Control c in SubPanel.Controls)
            {
                SubPanel.Controls.Remove(c);
                c.Dispose();
            }
            LoadSubForm(new Tray3Form());
        }

        private void 测试模组4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseForm();
            foreach (Control c in SubPanel.Controls)
            {
                SubPanel.Controls.Remove(c);
                c.Dispose();
            }
            LoadSubForm(new Tray4Form());
        }

        private void 测试模组5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseForm();
            foreach (Control c in SubPanel.Controls)
            {
                SubPanel.Controls.Remove(c);
                c.Dispose();
            }
            LoadSubForm(new Tray5Form());
        }

        private void 测试模组6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseForm();
            foreach (Control c in SubPanel.Controls)
            {
                SubPanel.Controls.Remove(c);
                c.Dispose();
            }
            LoadSubForm(new Tray6Form());
        }

        private void 测试模组7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseForm();
            foreach (Control c in SubPanel.Controls)
            {
                SubPanel.Controls.Remove(c);
                c.Dispose();
            }
            LoadSubForm(new Tray7Form());
        }

        private void 测试模组8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseForm();
            foreach (Control c in SubPanel.Controls)
            {
                SubPanel.Controls.Remove(c);
                c.Dispose();
            }
            LoadSubForm(new Tray8Form());
        }

        private void 测试模组9ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseForm();
            foreach (Control c in SubPanel.Controls)
            {
                SubPanel.Controls.Remove(c);
                c.Dispose();
            }
            LoadSubForm(new Tray9Form());
        }

        private void 测试模组10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseForm();
            foreach (Control c in SubPanel.Controls)
            {
                SubPanel.Controls.Remove(c);
                c.Dispose();
            }
            LoadSubForm(new Tray10Form());
        }

        //测试模组设置
        public void LoadSubForm1(object form)
        {
            if (this.SubPanel1.Controls.Count > 0)
            {
                this.SubPanel1.Controls.RemoveAt(0);
            }

            Form f = form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.SubPanel1.Controls.Add(f);
            this.SubPanel1.Tag = f;
            f.Show();
        }
        private void 测试模组1ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CloseForm();
            foreach (Control c in SubPanel1.Controls)
            {
                SubPanel1.Controls.Remove(c);
                c.Dispose();
            }
            LoadSubForm1(new Tray1SetForm());
        }

        private void 测试模组2ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CloseForm();
            foreach (Control c in SubPanel1.Controls)
            {
                SubPanel1.Controls.Remove(c);
                c.Dispose();
            }
            LoadSubForm1(new Tray2SetForm());
        }

        private void 测试模组3ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CloseForm();
            foreach (Control c in SubPanel1.Controls)
            {
                SubPanel1.Controls.Remove(c);
                c.Dispose();
            }
            LoadSubForm1(new Tray3SetForm());
        }

        private void 测试模组4ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CloseForm();
            foreach (Control c in SubPanel1.Controls)
            {
                SubPanel1.Controls.Remove(c);
                c.Dispose();
            }
            LoadSubForm1(new Tray4SetForm());
        }

        private void 测试模组5ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CloseForm();
            foreach (Control c in SubPanel1.Controls)
            {
                SubPanel1.Controls.Remove(c);
                c.Dispose();
            }
            LoadSubForm1(new Tray5SetForm());
        }

        private void 测试模组6ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CloseForm();
            foreach (Control c in SubPanel1.Controls)
            {
                SubPanel1.Controls.Remove(c);
                c.Dispose();
            }
            LoadSubForm1(new Tray6SetForm());
        }

        private void 测试模组7ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CloseForm();
            foreach (Control c in SubPanel1.Controls)
            {
                SubPanel1.Controls.Remove(c);
                c.Dispose();
            }
            LoadSubForm1(new Tray7SetForm());
        }

        private void 测试模组8ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CloseForm();
            foreach (Control c in SubPanel1.Controls)
            {
                SubPanel1.Controls.Remove(c);
                c.Dispose();
            }
            LoadSubForm1(new Tray8SetForm());
        }

        private void 测试模组9ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CloseForm();
            foreach (Control c in SubPanel1.Controls)
            {
                SubPanel1.Controls.Remove(c);
                c.Dispose();
            }
            LoadSubForm1(new Tray9SetForm());
        }

        private void 测试模组10ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CloseForm();
            foreach (Control c in SubPanel1.Controls)
            {
                SubPanel1.Controls.Remove(c);
                c.Dispose();
            }
            LoadSubForm1(new Tray10SetForm());
        }
        #endregion

        #region 关闭Tray窗体
        public void CloseForm()
        {
            tray1Form.Close();
            tray1Form.Dispose();
            tray2Form.Close();
            tray2Form.Dispose();
            tray3Form.Close();
            tray3Form.Dispose();
            tray4Form.Close();
            tray4Form.Dispose();
            tray5Form.Close();
            tray5Form.Dispose();
            tray6Form.Close();
            tray6Form.Dispose();
            tray7Form.Close();
            tray7Form.Dispose();
            tray8Form.Close();
            tray8Form.Dispose();
            tray9Form.Close();
            tray9Form.Dispose();
            tray10Form.Close();
            tray10Form.Dispose();

            tray1SetForm.Close();
            tray1SetForm.Dispose();
            tray2SetForm.Close();
            tray2SetForm.Dispose();
            tray3SetForm.Close();
            tray3SetForm.Dispose();
            tray4SetForm.Close();
            tray4SetForm.Dispose();
            tray5SetForm.Close();
            tray5SetForm.Dispose();
            tray6SetForm.Close();
            tray6SetForm.Dispose();
            tray7SetForm.Close();
            tray7SetForm.Dispose();
            tray8SetForm.Close();
            tray8SetForm.Dispose();
            tray9SetForm.Close();
            tray9SetForm.Dispose();
            tray10SetForm.Close();
            tray10SetForm.Dispose();

            while (SubPanel.Controls.Count > 0)
            {
                Control ct = SubPanel.Controls[0];
                SubPanel.Controls.Remove(ct);
                ct.Dispose();
                ct = null;
            }
        }





        #endregion

        #region 测试模组是否都完成
        public bool ModelIsNull(string num)
        {
            bool txt = false;
            //@"D:\Map\2\finish.txt"
            string path = @"D:\Map\" + num + @"\test.txt";
            if (File.Exists(path))
            {
                txt = true;
            }
            else
            {
                txt = false;
            }
            return txt;
        }

        public bool ModelIsNull1(string num)
        {
            bool txt = false;
            //@"D:\Map\2\finish.txt"
            string path = @"D:\Map\" + num + @"\finish.txt";
            if (File.Exists(path))
            {
                txt = true;
            }
            else
            {
                txt = false;
            }
            return txt;
        }
        #endregion

        #region 判断10个模组哪一个模组没有放产品
        public int ModelNoOK()
        {
            //int ModelData = 0;
            //for (int i = 0; i < 40; i++)
            //{
            //    if (Variable.ModelProductionIsOK[i] == false)
            //    {
            //        ModelData = i + 21;
            //        break;
            //    }

            //}
            //return ModelData;

            int ModelNum = 0;
            bool a = false;//内
            bool b = false;//外
            for (int i = 0; i < 40; i++)
            {
                // Variable.ModelState  10个模组状态=>0:空，1:已放料，2:测试中，3:测试OK，10:屏蔽

                if (i % 2 == 0)//内部
                {
                    a = Variable.ModelState[i] == 0;
                    b = Variable.ModelState[i + 1] == 0 || Variable.ModelState[i + 1] == 10;
                    if (a && b)//判断外部没有放料或屏蔽
                    {
                        ModelNum = i + 21;
                        break;
                    }
                }
                else//外部
                {
                    a = Variable.ModelState[i - 1] == 1 || Variable.ModelState[i - 1] == 2 || Variable.ModelState[i - 1] == 10;
                    b = Variable.ModelState[i] == 0;
                    if (a && b)//判断内部放料或屏蔽
                    {
                        ModelNum = i + 21;
                        break;
                    }
                }

            }
            return ModelNum;

            //int ModelNum = 0;
            //for (int i = 0; i < 40; i++)
            //{
            //    if (i % 2 == 0)//内部
            //    {
            //        if (Variable.ModelState[i] == 0 && (Variable.ModelState[i + 1] == 0 || !Variable.modelCheck[i + 1]))//判断外部没有放料或屏蔽
            //        {
            //            ModelNum = i + 21;
            //            break;
            //        }
            //    }
            //    else//外部
            //    {
            //        if (Variable.ModelState[i] == 0 && (Variable.ModelState[i - 1] != 3 || !Variable.modelCheck[i - 1]))//判断内部放料或屏蔽
            //        {
            //            ModelNum = i + 21;
            //            break;
            //        }
            //    }

            //}
            //return ModelNum;
        }

        #endregion

        #region 读取UpReady数据到ModelUP
        public void UpReadyTOModelUP(string str)
        {
            //将取完的上料数据UpReady保存到对应的ModelUP中
            string st = Application.StartupPath + @"\Data\UpReady\tray";
            string[] Readstr = myTXT.ReadTXT(st);
            myTXT.WriteTxt(Readstr, Application.StartupPath + @"\Data\ModelUP\" + str + @"\tray");
        }

        #endregion

        #region 模块传文件给上料机

        public void SetFileToUP(string str)
        {
            string st = @"\Data\TCPModel\" + str + @"\tray";
            string[] Readstr = myTXT.ReadTXT(Application.StartupPath + st);
            myTXT.WriteTxt(Readstr, Application.StartupPath + @"\Data\UpReady\tray");//读取Model文本到UpReady中
        }
        #endregion

        #region 判断10个模组哪一个模组老化OK

        public int ModelIsOK()
        {
            //int ModelData = 0;
            //bool txt = false;
            //for (int i = 0; i < 40; i++)
            //{
            //    txt = GetFile(Convert.ToString(i + 1));
            //    if (txt == true)
            //    {
            //        ModelData = i + 61;
            //        break;
            //    }
            //}
            //return ModelData;

            int ModelNum = 0;
            bool a = false;//内
            bool b = false;//外
            bool txt1 = false;
            bool txt2 = false;
            for (int i = 0; i < 40; i++)
            {
                // Variable.ModelState  10个模组状态=>0:空，1:已放料，2:测试中，3:测试OK，10:屏蔽
                if (i % 2 == 0)//内部
                {
                    txt1 = GetFile(Convert.ToString(i + 1));
                    a = txt1 && Variable.ModelState[i] == 3;
                    b = Variable.ModelState[i + 1] == 0 || Variable.ModelState[i + 1] == 10;
                    if (a && b)
                    {
                        ModelNum = i + 61;
                        break;
                    }
                }
                else//外部
                {
                    txt1 = GetFile(Convert.ToString(i - 1 + 1));
                    txt2 = GetFile(Convert.ToString(i + 1));
                    a = (txt1 && Variable.ModelState[i - 1] == 3) || (!txt1 && (Variable.ModelState[i - 1] == 0 || Variable.ModelState[i - 1] == 10));
                    b = txt2 && Variable.ModelState[i] == 3;
                    if (a && b)
                    {
                        ModelNum = i + 61;
                        break;
                    }
                }

                //if (i % 2 == 0)//内部
                //{
                //    txt1 = GetFile(Convert.ToString(i + 1));
                //    if (txt1 && Variable.ModelState[i] == 3 && ((Variable.ModelState[i + 1] == 0 || Variable.ModelState[i + 1] == 10) || !Variable.modelCheck[i + 1]))
                //    {
                //        ModelNum = i + 61;
                //        break;
                //    }
                //}
                //else//外部
                //{
                //    txt1 = GetFile(Convert.ToString(i));
                //    txt2 = GetFile(Convert.ToString(i + 1));
                //    if ((txt1 || !Variable.modelCheck[i - 1]) && txt2 && Variable.ModelState[i] == 3 && (Variable.ModelState[i - 1] == 3 || !Variable.modelCheck[i - 1]))
                //    {
                //        ModelNum = i + 61;
                //        break;
                //    }
                //}
            }
            return ModelNum;
        }

        public bool GetFile(string num)
        {
            bool txt = false;
            //@"D:\Map\2\finish.txt"
            string path = @"D:\Map\" + num + @"\finish.txt";
            if (File.Exists(path))
            {
                txt = true;
                Variable.ModelState[Convert.ToInt32(num) - 1] = 3;
            }
            else
            {
                txt = false;
            }
            return txt;
        }

        #endregion

        #region 读取Map文件传给下料机
        public void GetMapToDown(string str)
        {
            //读取Map文件
            string st = Application.StartupPath + @"\Map\" + str + @"\tray";
            string[] Readstr = myTXT.ReadTXT1(st);

            //传给下料机
            Array.Reverse(Readstr);//反转
            myTXT.WriteTxt(Readstr, Application.StartupPath + @"\Data\DownOK\tray");//读取Model文本到DownOK中
            myTXT.WriteTxt(Readstr, Application.StartupPath + @"\Data\DownOKWait\tray");//读取Model文本到DownOKWait中

            //存入历史文件
            string path1 = @"D:\History\" + str;
            string path2 = Variable.BatchNum + "_" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
            string fileName = Path.Combine(path1, path2);
            string[] saveStr = new string[Readstr.Length + 1];
            for (int i = 0; i < saveStr.Length; i++)
            {
                if (i < saveStr.Length - 1)
                {
                    saveStr[i] = Readstr[i];
                }
                else
                {
                    saveStr[i] = Variable.YieldMode[Convert.ToInt32(str) - 1];//良率
                }
            }
            myTXT.WriteTxt(saveStr, fileName);
        }
        #endregion

        #region 判断OK盘空位是否超过一半
        public int DownNGCount()
        {
            int nums = 0;
            //从TXT读取数据
            string path = Application.StartupPath + @"\Data\DownOK\tray";
            string[] Readstr = myTXT.ReadTXT(path);
            for (int i = 0; i < Readstr.Length; i++)
            {
                if (Readstr[i] == "10")//10：空
                {
                    nums += 1;
                }
            }

            return nums;
        }
        #endregion

        #region 判断OK盘NG是否超过一半
        public int NGCount()
        {
            int nums = 0;
            //从TXT读取数据
            string path = Application.StartupPath + @"\Data\DownOK\tray";
            string[] Readstr = myTXT.ReadTXT(path);
            for (int i = 0; i < Readstr.Length; i++)
            {
                if (Readstr[i] != "00")
                {
                    nums += 1;
                }
            }

            return nums;
        }
        #endregion

        #region 加热打开

        public void HotOpen()
        {
            for (int i = 0; i < 10; i++)
            {
                if (Variable.modelCheck[i * 4 + 0])
                {
                    function.OutYON(109 + i * 32);//加热打开
                }
                else
                {
                    function.OutYOFF(109 + i * 32);//加热关闭
                }

                if (Variable.modelCheck[i * 4 + 1])
                {
                    function.OutYON(111 + i * 32);//加热打开
                }
                else
                {
                    function.OutYOFF(111 + i * 32);//加热关闭
                }

                if (Variable.modelCheck[i * 4 + 2])
                {
                    function.OutYON(125 + i * 32);//加热打开
                }
                else
                {
                    function.OutYOFF(125 + i * 32);//加热关闭
                }

                if (Variable.modelCheck[i * 4 + 3])
                {
                    function.OutYON(127 + i * 32);//加热打开
                }
                else
                {
                    function.OutYOFF(127 + i * 32);//加热关闭
                }
            }
        }

        #endregion

        #region 加热关闭

        public void HotClose()
        {
            for (int i = 0; i < 10; i++)
            {
                function.OutYOFF(109 + i * 32);//加热关闭     
                function.OutYOFF(111 + i * 32);//加热关闭    
                function.OutYOFF(125 + i * 32);//加热关闭  
                function.OutYOFF(127 + i * 32);//加热关闭            
            }
        }

        #endregion

        #region PCB板断电

        public void StopPower()
        {
            for (int i = 0; i < 10; i++)
            {
                function.OutYOFF(108 + i * 32);//1#上内断电
                function.OutYOFF(110 + i * 32);//1#上外断电
                function.OutYOFF(124 + i * 32);//1#下内断电
                function.OutYOFF(126 + i * 32);//1#下外断电
            }
        }
        #endregion

        #region 根据从TXT读出的数据判断是NG

        public int ReadTxtJudge(string path)
        {
            int nums = 200;
            //从TXT读取数据
            //string[] str = myTXT.ReadTXT("Map", @"1\test");
            string[] Readstr = myTXT.ReadTXT(path);
            for (int i = 0; i < Readstr.Length; i++)
            {
                if (Readstr[i] != "00" && Readstr[i] != "10")//00：OK，10：空位
                {
                    nums = Convert.ToInt32(i);
                    break;
                }
            }

            return nums;

        }
        #endregion

        #region 根据从TXT读出的数据判断是NG和空位

        public int ReadTxtJudgeOK(string path)
        {
            int nums = 200;
            //从TXT读取数据
            //string[] str = myTXT.ReadTXT("Map", @"1\test");
            string[] Readstr = myTXT.ReadTXT(path);
            for (int i = 0; i < Readstr.Length; i++)
            {
                if (Readstr[i] != "00")//00：OK，10：空位
                {
                    nums = Convert.ToInt32(i);
                    break;
                }
            }

            return nums;

        }
        #endregion

        #region 根据从TXT读出的数据判断是OK

        public int ReadTxtJudgeReadyOK(string path)
        {
            int nums = 200;
            //从TXT读取数据
            //string[] str = myTXT.ReadTXT("Map", @"1\test");
            string[] Readstr = myTXT.ReadTXT(path);
            for (int i = 0; i < Readstr.Length; i++)
            {
                if (Readstr[i] == "00")//00：OK
                {
                    nums = Convert.ToInt32(i);
                    break;
                }
            }

            return nums;

        }
        #endregion


        #region 更新UpNullTray数据写入到TXT中

        public void UpdateTxtUpNullTray(string path)
        {
            //从TXT读取数据
            //string[] str = myTXT.ReadTXT("Map", @"1\test");
            string[] Readstr = myTXT.ReadTXT(path);
            Readstr[Variable.UpNullTray] = "00";
            //向TXT写入数据
            myTXT.WriteTxt(Readstr, path);

        }
        #endregion

        #region 更新UpReadyTrayEmpty数据写入到TXT中

        public void UpdateTxtUpReadyTrayEmpty(string path)
        {
            //从TXT读取数据
            //string[] str = myTXT.ReadTXT("Map", @"1\test");
            string[] Readstr = myTXT.ReadTXT(path);
            Readstr[Variable.UpReadyTrayEmpty] = "10";
            //向TXT写入数据
            myTXT.WriteTxt(Readstr, path);

        }
        #endregion

        #region 更新DownOKTrayNG数据写入到TXT中

        public void UpdateTxtDownOKTrayNG(string path)
        {
            //从TXT读取数据
            //string[] str = myTXT.ReadTXT("Map", @"1\test");
            string[] Readstr = myTXT.ReadTXT(path);
            Readstr[Variable.DownOKTrayNG] = "10";
            //向TXT写入数据
            myTXT.WriteTxt(Readstr, path);

        }
        #endregion

        #region 更新DownOKTrayFull数据写入到TXT中

        public void UpdateTxtDownOKTrayFull(string path)
        {
            //从TXT读取数据
            //string[] str = myTXT.ReadTXT("Map", @"1\test");
            string[] Readstr = myTXT.ReadTXT(path);
            Readstr[Variable.DownOKTrayFull] = "00";
            //向TXT写入数据
            myTXT.WriteTxt(Readstr, path);

        }
        #endregion

        #region 更新DownOKTrayWait数据写入到TXT中

        public void UpdateTxtDownOKTrayWait(string path)
        {
            //从TXT读取数据
            //string[] str = myTXT.ReadTXT("Map", @"1\test");
            string[] Readstr = myTXT.ReadTXT(path);
            Readstr[Variable.DownOKTrayWait] = "00";
            //向TXT写入数据
            myTXT.WriteTxt(Readstr, path);

        }
        #endregion

        #region 更新DownReadyOK数据写入到TXT中

        public void UpdateTxtDownReadyOK(string path)
        {
            //从TXT读取数据
            //string[] str = myTXT.ReadTXT("Map", @"1\test");
            string[] Readstr = myTXT.ReadTXT(path);
            Readstr[Variable.DownReadyOK] = "10";
            //向TXT写入数据
            myTXT.WriteTxt(Readstr, path);

        }
        #endregion

        #region 更新DownReadyTrayOK数据写入到TXT中

        public void UpdateTxtDownReadyTrayOK(string path)
        {
            //从TXT读取数据
            //string[] str = myTXT.ReadTXT("Map", @"1\test");
            string[] Readstr = myTXT.ReadTXT(path);
            Readstr[Variable.DownReadyTrayOK] = "10";
            //向TXT写入数据
            myTXT.WriteTxt(Readstr, path);

        }
        #endregion

        #region 更新DownNGTray数据写入到TXT中

        public void UpdateTxtDownNGTray(string path)
        {
            //从TXT读取数据
            //string[] str = myTXT.ReadTXT("Map", @"1\test");
            string[] Readstr = myTXT.ReadTXT(path);
            Readstr[Variable.DownNGTray] = "00";
            //向TXT写入数据
            myTXT.WriteTxt(Readstr, path);

        }
        #endregion


        #region 清空Txt赋0

        public void TxtClear0(string path)
        {
            string[] Readstr = myTXT.ReadTXT(path);
            for (int i = 0; i < Readstr.Length; i++)
            {
                Readstr[i] = "00";
            }
            //向TXT写入数据
            myTXT.WriteTxt(Readstr, path);
        }
        #endregion

        #region 清空Txt赋10

        public void TxtClear1(string path)
        {
            string[] Readstr = myTXT.ReadTXT(path);
            for (int i = 0; i < Readstr.Length; i++)
            {
                Readstr[i] = "10";
            }
            //向TXT写入数据
            myTXT.WriteTxt(Readstr, path);
        }
        #endregion

        #region 提取txt名
        public string[] GetTxtname(string num1, string num2)
        {
            TXT myTXT = new TXT();
            string[] name = myTXT.ReadFileName(num1, num2);
            string[] getname = new string[name.Length];
            if (name.Length > 0)
            {
                for (int i = 0; i < name.Length; i++)
                {

                    getname[i] = name[i].Substring(9, name[i].Length - 13);

                }
            }
            return getname;
        }

        #endregion

        #region 写入test.TXT文件名
        public void WriteTestTXT(string num)
        {
            //string path = Convert.ToString(Convert.ToInt32(num));
            //写入TXT文件名
            myTXT.WriteFileName("Map", num, "test");
        }

        #endregion

        #region 写入Reset.TXT文件名
        public void WriteResetTXT(string num)
        {
            //string path = Convert.ToString(Convert.ToInt32(num));
            //写入TXT文件名
            myTXT.WriteFileName("Map", num, "Reset");
        }

        #endregion

        #region 写入Parameter.TXT文件名
        public void WriteResetOKTXT(string num)
        {
            //string path = Convert.ToString(Convert.ToInt32(num));
            //写入TXT文件名
            myTXT.WriteFileName("Map", num, "ResetOK");
        }

        #endregion

        #region 删除finishTXT文档
        public void DeleteFinishTXT(string num)
        {
            string path = Convert.ToString(Convert.ToInt32(num));
            //删除txt文档
            myTXT.DeleteTXT("Map", path, "finish");
        }
        #endregion

        #region 删除alarmTXT文档
        public void DeletealarmTXT(string num)
        {
            string path = Convert.ToString(Convert.ToInt32(num));
            //删除txt文档
            myTXT.DeleteTXT("Map", path, "alarm");
        }
        #endregion

        #region 删除ResetOKTXT文档
        public void DeleteResetOKTXT(string num)
        {
            string path = Convert.ToString(Convert.ToInt32(num));
            //删除txt文档
            myTXT.DeleteTXT("Map", path, "ResetOK");
        }
        #endregion

        #region 删除TrayTXT文档
        public void DeleteTrayTXT(string num)
        {
            string path = Convert.ToString(Convert.ToInt32(num));
            //删除txt文档
            myTXT.DeleteTXT("Map", path, "tray");
        }
        #endregion

        #region 删除testTXT文档
        public void DeleteTestTXT(string num)
        {
            string path = Convert.ToString(Convert.ToInt32(num));
            //删除txt文档
            myTXT.DeleteTXT("Map", path, "test");
        }
        #endregion

        #region 删除finishTXT文档
        public void DeleteParameterTXT(string num)
        {
            string path = Convert.ToString(Convert.ToInt32(num));
            //删除txt文档
            myTXT.DeleteTXT("Map", path, "Parameter");
        }
        #endregion

        #region 删除ResetOKTXT文档
        public void DeleteResetTXT(string num)
        {
            string path = Convert.ToString(Convert.ToInt32(num));
            //删除txt文档
            myTXT.DeleteTXT("Map", path, "Reset");
        }
        #endregion

        #region DataGridView初始化
        public void DataGridViewInitialize()
        {
            dataGrid.IniLeftLoadTrayW(EmptyDataGrid, Variable.RowNum, Variable.ListNum);
            dataGrid.IniLeftLoadTrayW(LoadDataGrid, Variable.RowNum, Variable.ListNum);
            dataGrid.IniLeftLoadTrayW(OKDataGrid, Variable.RowNum, Variable.ListNum);
            dataGrid.IniLeftLoadTrayW(FillDataGrid, Variable.RowNum, Variable.ListNum);
            dataGrid.IniLeftLoadTrayW(NGDataGrid, Variable.RowNum, Variable.ListNum);
        }
        #endregion

        #region 读取TXT文本到DataGridView

        public void ReadTxtToDataGrid()
        {
            //EmptyDataGrid
            string[] strEmptyDataGrid = myTXT.ReadTXT(Application.StartupPath + @"\Data\UpEmpty\tray");
            if (strEmptyDataGrid.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(EmptyDataGrid, strEmptyDataGrid);
            }

            //LoadDataGrid
            string[] strLoadDataGrid = myTXT.ReadTXT(Application.StartupPath + @"\Data\UpReady\tray");
            if (strLoadDataGrid.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(LoadDataGrid, strLoadDataGrid);
            }

            //Pass1DataGrid
            string[] strPass1DataGrid = myTXT.ReadTXT(Application.StartupPath + @"\Data\DownOK\tray");
            if (strPass1DataGrid.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(OKDataGrid, strPass1DataGrid);
            }

            //Pass2DataGrid
            string[] strPass2DataGrid = myTXT.ReadTXT(Application.StartupPath + @"\Data\DownReadyOK\tray");
            if (strPass2DataGrid.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(FillDataGrid, strPass2DataGrid);
            }

            //FailDataGrid
            string[] strFailDataGrid = myTXT.ReadTXT(Application.StartupPath + @"\Data\DownNG\tray");
            if (strFailDataGrid.Length == 152)
            {
                myTXT.ReadTxtToDataGridMethod(NGDataGrid, strFailDataGrid);
            }

            ////窗体1
            //Form POPFormIsOpenOrNot1 = Application.OpenForms["Tray1Form"];
            //if ((POPFormIsOpenOrNot1 == null) || (POPFormIsOpenOrNot1.IsDisposed))//如果没有创建过或者窗体已经被释放
            //{

            //}
            //else
            //{
            //    tray1Form.timer1.Enabled = true;
            //    tray1Form.timer1.Start();
            //}

        }

        #endregion

        #region 相机数据字符串处理

        // TCP反馈字符串处理
        private int UPStringManipulation1(string recMessage)
        {
            int n = 0;
            try
            {
                if (recMessage.Length > 5)
                {
                    string[] recList = recMessage.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);//new string[] { " ", " " }  0是OK，1是NG，10是空位
                    string str = recList[0];
                    if (str == "100")
                    {
                        n = 1;
                    }
                    else
                    {
                        n = 0;
                    }
                    string[] recList1 = recList.Skip(1).Take(152).ToArray();
                    recList1.CopyTo(Variable.PhotoData1, 0);//将数据传给拍照1

                    for (int i = 0; i < recList1.Length; i++)
                    {
                        if (recList1[i] == "01")
                        {
                            Variable.TestResult = false;
                            break;
                        }
                        else
                        {
                            Variable.TestResult = true;
                        }
                    }

                    //向TXT写入数据
                    //myTXT.WriteTxt(recList1, Application.StartupPath + @"\Data\Photo\Up\tray");
                }
                else
                {
                    ListBoxTxt("相机数据传输异常");//MessageBox.Show("相机数据传输异常");
                }
            }
            catch (Exception)
            {
                ListBoxTxt("相机数据传输异常");//MessageBox.Show("相机数据传输异常");
            }
            return n;
        }

        private int UPStringManipulation2(string recMessage)
        {
            int n = 0;
            try
            {
                if (recMessage.Length > 5)
                {
                    string[] recList = recMessage.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);//new string[] { " ", " " }  0是OK，1是NG，10是空位
                    string str = recList[0];
                    if (str == "101")
                    {
                        n = 1;
                    }
                    else
                    {
                        n = 0;
                    }
                    string[] recList1 = recList.Skip(1).Take(152).ToArray();
                    recList1.CopyTo(Variable.PhotoData2, 0);//将数据传给拍照2

                    for (int i = 0; i < recList1.Length; i++)
                    {
                        if (recList1[i] == "01")
                        {
                            Variable.TestResult = false;
                            break;
                        }
                        else
                        {
                            Variable.TestResult = true;
                        }
                    }

                    //向TXT写入数据
                    //myTXT.WriteTxt(recList1, Application.StartupPath + @"\Data\Photo\Up\tray");
                }
                else
                {
                    ListBoxTxt("相机数据传输异常");//MessageBox.Show("相机数据传输异常");
                }
            }
            catch (Exception)
            {
                ListBoxTxt("相机数据传输异常");//MessageBox.Show("相机数据传输异常");
            }
            return n;
        }

        private int DownStringManipulation1(string recMessage)
        {
            int n = 0;
            try
            {
                if (recMessage.Length > 5)
                {
                    string[] recList = recMessage.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);//new string[] { " ", " " }  0是OK，1是NG，10是空位
                    string str = recList[0];
                    if (str == "100")
                    {
                        n = 1;
                    }
                    else
                    {
                        n = 0;
                    }
                    string[] recList1 = recList.Skip(1).Take(152).ToArray();
                    recList1.CopyTo(Variable.PhotoData1, 0);//将数据传给拍照1

                    for (int i = 0; i < recList1.Length; i++)
                    {
                        if (recList1[i] == "01")
                        {
                            Variable.TestResult = false;
                            break;
                        }
                        else
                        {
                            Variable.TestResult = true;
                        }
                    }

                    //向TXT写入数据
                    //myTXT.WriteTxt(recList1, Application.StartupPath + @"\Data\Photo\Down\tray");

                }
                else
                {
                    ListBoxTxt("相机数据传输异常");//MessageBox.Show("相机数据传输异常");
                }
            }
            catch (Exception)
            {
                ListBoxTxt("相机数据传输异常");//MessageBox.Show("相机数据传输异常");
            }
            return n;
        }

        private int DownStringManipulation2(string recMessage)
        {
            int n = 0;
            try
            {
                if (recMessage.Length > 5)
                {
                    string[] recList = recMessage.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);//new string[] { " ", " " }  0是OK，1是NG，10是空位
                    string str = recList[0];
                    if (str == "101")
                    {
                        n = 1;
                    }
                    else
                    {
                        n = 0;
                    }
                    string[] recList1 = recList.Skip(1).Take(152).ToArray();
                    recList1.CopyTo(Variable.PhotoData2, 0);//将数据传给拍照2

                    for (int i = 0; i < recList1.Length; i++)
                    {
                        if (recList1[i] == "01")
                        {
                            Variable.TestResult = false;
                            break;
                        }
                        else
                        {
                            Variable.TestResult = true;
                        }
                    }

                    //向TXT写入数据
                    //myTXT.WriteTxt(recList1, Application.StartupPath + @"\Data\Photo\Down\tray");

                }
                else
                {
                    ListBoxTxt("相机数据传输异常");//MessageBox.Show("相机数据传输异常");
                }
            }
            catch (Exception)
            {
                ListBoxTxt("相机数据传输异常");//MessageBox.Show("相机数据传输异常");
            }
            return n;
        }

        private int StringManipulation(string recMessage)
        {
            int n = 0;
            try
            {
                if (recMessage.Length > 5)
                {
                    string[] recList = recMessage.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);//new string[] { " ", " " }  0是OK，1是NG，10是空位

                    if (recList[0].Length >= 9)
                    {
                        n = 1;
                    }
                    else
                    {
                        n = 0;
                    }

                    if (recList[1].Length > 0)
                    {
                        Variable.TestResult = true;
                        Variable.QRRecMessage = recMessage;
                    }
                    else
                    {
                        Variable.TestResult = false;
                    }
                }
                else
                {
                    ListBoxTxt("QR数据传输异常");//MessageBox.Show("QR数据传输异常");
                }
            }
            catch (Exception)
            {
                ListBoxTxt("QR数据传输异常");//MessageBox.Show("QR数据传输异常");
            }
            return n;
        }

        #endregion

        #region 根据相机拍照数据找出空位发送给下料机
        public void PhotoResultToDown()
        {
            string strDown = Application.StartupPath + @"\Data\DownOK\tray";
            string strPhoto = Application.StartupPath + @"\Data\Photo\Down\tray";

            string[] ReadDown = myTXT.ReadTXT(strDown);

            string[] ReadPhoto = myTXT.ReadTXT(strPhoto);
            Array.Reverse(ReadPhoto);//反转

            for (int i = 0; i < ReadDown.Length; i++)
            {
                if (ReadPhoto[i] == "10")
                {
                    ReadDown[i] = "10";
                }
            }

            myTXT.WriteTxt(ReadDown, Application.StartupPath + @"\Data\DownOK\tray");//读取Model文本到DownOK中
            myTXT.WriteTxt(ReadDown, Application.StartupPath + @"\Data\DownOKWait\tray");//读取Model文本到DownOKWait中
        }

        #endregion

        #region 根据上料模组数据找出空位发送给下料机
        public void ModelResultToDown(string str)
        {
            //读取stDownOK,stModel文件
            string stDownOK = Application.StartupPath + @"\Data\DownOK\tray";
            string stModel = Application.StartupPath + @"\Data\ModelUP\" + str + @"\tray";

            string[] ReadstrDownOK = myTXT.ReadTXT(stDownOK);
            string[] ReadstrModel = myTXT.ReadTXT(stModel);
            Array.Reverse(ReadstrModel);//反转
            for (int i = 0; i < ReadstrModel.Length; i++)
            {
                if (ReadstrModel[i] == "10")
                {
                    ReadstrDownOK[i] = "10";
                }
            }

            myTXT.WriteTxt(ReadstrDownOK, Application.StartupPath + @"\Data\DownOK\tray");//读取Model文本到DownOK中
            myTXT.WriteTxt(ReadstrDownOK, Application.StartupPath + @"\Data\DownOKWait\tray");//读取Model文本到DownOKWait中
        }

        #endregion

        #region 相机与模块比较是否缺料

        public int ModelToPhoto(string str)
        {
            int n = 1;
            string strPhotoUp = Application.StartupPath + @"\Data\Photo\Up\" + str + @"\tray";
            string strPhotoDown = Application.StartupPath + @"\Data\Photo\Down\tray";
            string[] ReadPhotoUp = myTXT.ReadTXT(strPhotoUp);
            string[] ReadPhotoDown = myTXT.ReadTXT(strPhotoDown);

            for (int i = 0; i < ReadPhotoUp.Length; i++)
            {
                if (ReadPhotoDown[i] == "10")
                {
                    if (ReadPhotoUp[i] == "10")
                    {
                        n = 1;
                    }
                    else
                    {
                        n = 0;//缺料
                        break;
                    }
                }
            }
            return n;
        }
        #endregion

        #region 放弃拍照，相机数据赋值00，OK
        public void PhotoDataOK(string[] str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                str[i] = "00";
            }
        }
        #endregion

        #region 打开安装程序
        /// <summary>        
        /// 打开自己开发的程序        
        /// </summary>        
        /// <param name="fileName">文件名称（比如C-MES.exe）</param>        
        /// <param name="filePath">文件所在路径（比如G:\SoftWare\DMMES）</param>        
        public static void OpenOtherEXEMethod(string fileName, string filePath)
        {
            if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(filePath))
            {                //开启一个新process                
                System.Diagnostics.ProcessStartInfo p = null;
                System.Diagnostics.Process proc;
                p = new System.Diagnostics.ProcessStartInfo(fileName);
                p.WorkingDirectory = filePath;//设置此外部程序所在windows目录                
                proc = System.Diagnostics.Process.Start(p);//调用外部程序            
            }
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

        #region Txt增加显示

        public void Textadd(TextBox textbox, string Messg)
        {
            textbox.Text += Messg + "\r\n";
        }

        #endregion

        #region 计算分钟时间差

        public void MinutesTimeDiffer()
        {
            DateTime dt1 = DateTime.Now;
            DateTime dt2 = Convert.ToDateTime(Variable.startTime); //开始加热时间            
            //DateTime dt2 = Convert.ToDateTime("2021/6/29 23:22:25"); //开始加热时间
            TimeSpan ts = dt1.Subtract(dt2);
            double hours = Math.Round(ts.TotalMinutes, 1);
            Variable.HotTime = Convert.ToString(hours);
        }

        #endregion

        #region 判断端口是不是NG
        public void JudgeNGCount(int num)
        {
            string path1 = @"D:\Map\" + num.ToString() + @"\tray";
            string[] Readstr1 = myTXT.ReadTXT1(path1);
            string path2 = Application.StartupPath + @"\Data\Photo\Down\tray";
            string[] Readstr2 = myTXT.ReadTXT(path2);

            for (int i = 0; i < Readstr1.Length; i++)
            {
                if (Readstr1[i] == "02" && Readstr2[i] != "10")
                {
                    Variable.siteNGCount[num - 1, i] += 1;
                }
            }
        }

        #endregion

        #region 保存端口是不是NG
        public void SaveNGCount(int num)
        {
            //写入ModelUP上料文件
            string path = Application.StartupPath + @"\Data\TCPModel\" + num.ToString() + @"\tray";
            string[] Readstr = myTXT.ReadTXT(path);

            for (int i = 0; i < Readstr.Length; i++)
            {
                if (Variable.siteNGCount[num - 1, i] >= (int)Variable.siteNGSet)
                {
                    Readstr[i] = "01";
                }
            }

            //向TXT写入数据
            myTXT.WriteTxt(Readstr, path);
        }

        #endregion

        #region 温控器温度读取
        public double TempRead1(string num)//string s = TempRead("01");
        {
            double d = 0;
            Variable.Temp = "00";
            //地址:01,02,03,04,05,06,07,08,09,0A,0B,0C,0D,0E,0F,10,11,12,13.14
            //string str = num + "0347000002";//"010347000002"
            string str = num + "0310000002";//"010310000002"
            char[] startch = { ':' };
            char[] endch = { '\0', '\0', '\r', '\n' };
            char[] lrcch = tem1.LRC(str).ToCharArray();//校验码生成字符数组
            lrcch.CopyTo(endch, 0);//把校验码数组嵌入到endch中
            char[] datach = str.ToCharArray();//发送内容生成字符数组
            char[] ch3 = new char[datach.Length + 5];
            datach.CopyTo(ch3, 1);//组成命令字符数组
            endch.CopyTo(ch3, datach.Length + 1);//组成命令字符数组
            startch.CopyTo(ch3, 0);//组成命令字符数组
            byte[] byt = new byte[ch3.Length];//把命令字符数组转换为字节数组
            for (int i = 0; i < ch3.Length; i++)
            {
                byt[i] = Convert.ToByte(ch3[i]);
            }

            tem1.SendData(byt);
            Thread.Sleep(100);
            try
            {
                if (Variable.Temp != "0")
                {
                    d = Math.Round(Convert.ToDouble(Variable.Temp) / 10, 1);
                }
                else
                {
                    d = Convert.ToDouble(Variable.Temp);
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(new StackTrace(new StackFrame(true)), new StackFrame(), ex);
            }

            return d;
        }

        public double TempRead2(string num)//string s = TempRead("01");
        {
            double d = 0;
            Variable.Temp = "00";
            //地址:01,02,03,04,05,06,07,08,09,0A,0B,0C,0D,0E,0F,10,11,12,13.14
            //string str = num + "0347000002";//"010347000002"
            string str = num + "0310000002";//"010310000002"
            char[] startch = { ':' };
            char[] endch = { '\0', '\0', '\r', '\n' };
            char[] lrcch = tem2.LRC(str).ToCharArray();//校验码生成字符数组
            lrcch.CopyTo(endch, 0);//把校验码数组嵌入到endch中
            char[] datach = str.ToCharArray();//发送内容生成字符数组
            char[] ch3 = new char[datach.Length + 5];
            datach.CopyTo(ch3, 1);//组成命令字符数组
            endch.CopyTo(ch3, datach.Length + 1);//组成命令字符数组
            startch.CopyTo(ch3, 0);//组成命令字符数组
            byte[] byt = new byte[ch3.Length];//把命令字符数组转换为字节数组
            for (int i = 0; i < ch3.Length; i++)
            {
                byt[i] = Convert.ToByte(ch3[i]);
            }

            tem2.SendData(byt);
            Thread.Sleep(100);
            try
            {
                if (Variable.Temp != "0")
                {
                    d = Math.Round(Convert.ToDouble(Variable.Temp) / 10, 1);
                }
                else
                {
                    d = Convert.ToDouble(Variable.Temp);
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(new StackTrace(new StackFrame(true)), new StackFrame(), ex);
            }

            return d;
        }

        #endregion

        #region 初始化白班夜班数组
        public void DayNightArray()
        {
            for (int i = 0; i < 720; i++)
            {
                Variable.DayTime[i] = "10";
                Variable.NightTime[i] = "10";
            }
            myTXT.WriteTxt(Variable.DayTime, Application.StartupPath + @"\Data\DayTime\Time");
            myTXT.WriteTxt(Variable.NightTime, Application.StartupPath + @"\Data\NightTime\Time");
        }
        #endregion

        #region OK区选中行和列颜色发生变化

        /// <summary>
        /// OK区选中行和列颜色发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void OKDataGrid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (Variable.userEnter == Variable.UserEnter.Administrator || Variable.userEnter == Variable.UserEnter.Manufacturer)
            {
                int MyCount = OKDataGrid.GetCellCount(DataGridViewElementStates.Selected);//被选中单元格数
                if (MyCount > 0)
                {
                    if (OKDataGrid.AreAllCellsSelected(true))//判断是否选中多个
                    {
                        for (int a = 0; a < Variable.RowNum; a++)
                        {
                            for (int b = 0; b < Variable.ListNum; b++)
                            {
                                if (OKDataGrid.Rows[a].Cells[b].Style.BackColor == Color.White)
                                {
                                    OKDataGrid.Rows[a].Cells[b].Style.BackColor = Color.Red;
                                }
                                else
                                {
                                    OKDataGrid.Rows[a].Cells[b].Style.BackColor = Color.White;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < MyCount; i++)
                        {
                            if (OKDataGrid.SelectedCells[i].Style.BackColor == Color.White)
                            {
                                OKDataGrid.SelectedCells[i].Style.BackColor = Color.Red;
                            }
                            else
                            {
                                OKDataGrid.SelectedCells[i].Style.BackColor = Color.White;
                            }
                        }
                    }
                    OKDataGrid.CurrentCell = null;
                }

                string strDown = Application.StartupPath + @"\Data\DownOK\tray";
                string[] ReadDown = myTXT.ReadTXT(strDown);

                for (int a = 0; a < Variable.RowNum; a++)
                {
                    for (int b = 0; b < Variable.ListNum; b++)
                    {
                        if (OKDataGrid.Rows[a].Cells[b].Style.BackColor == Color.White)
                        {
                            ReadDown[a * Convert.ToInt32(Variable.ListNum) + b] = "10";
                        }
                        else if (OKDataGrid.Rows[a].Cells[b].Style.BackColor == Color.Green)
                        {
                            ReadDown[a * Convert.ToInt32(Variable.ListNum) + b] = "00";
                        }
                        else if (OKDataGrid.Rows[a].Cells[b].Style.BackColor == Color.Red)
                        {
                            ReadDown[a * Convert.ToInt32(Variable.ListNum) + b] = "02";
                        }
                    }
                }
                Thread.Sleep(100);
                myTXT.WriteTxt(ReadDown, Application.StartupPath + @"\Data\DownOK\tray");//读取Model文本到DownOK中
                Thread.Sleep(100);
                myTXT.WriteTxt(ReadDown, Application.StartupPath + @"\Data\DownOKWait\tray");//读取Model文本到DownOKWait中

            }
        }

        #endregion

        #region 探针柱状图
        public void ChartShow()
        {
            //标题
            cht1.Titles.Add("探针寿命数据分析");
            cht1.Titles[0].ForeColor = Color.Black;
            cht1.Titles[0].Font = new Font("宋体", 15f, FontStyle.Regular);
            cht1.Titles[0].Alignment = ContentAlignment.TopCenter;

            //右上角标题
            cht1.Titles.Add("探针次数设定：0");
            //cht1.Titles.Add("合计：25414 宗");
            //cht1.Titles.Add("合计：25414 宗");
            //cht1.Titles.Add("合计：25414 宗");
            cht1.Titles[1].ForeColor = Color.Black;
            cht1.Titles[1].Font = new Font("宋体", 10f, FontStyle.Regular);

            //设置标题位于右上角
            //cht1.Titles[1].Alignment = ContentAlignment.TopRight;
            //cht1.Titles[2].Alignment = ContentAlignment.TopRight;
            //cht1.Titles[3].Alignment = ContentAlignment.TopRight;
            //cht1.Titles[4].Alignment = ContentAlignment.TopRight;

            //控件背景
            cht1.BackColor = Color.LightBlue;

            //图表区背景
            cht1.ChartAreas[0].BackColor = Color.Transparent;
            cht1.ChartAreas[0].BorderColor = Color.Transparent;

            //X轴标签间距
            cht1.ChartAreas[0].AxisX.Interval = 1;
            cht1.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;
            cht1.ChartAreas[0].AxisX.LabelStyle.Angle = -45;   //下方标签显示的角度
            cht1.ChartAreas[0].AxisX.TitleFont = new Font("宋体", 10f, FontStyle.Regular);
            cht1.ChartAreas[0].AxisX.TitleForeColor = Color.Black;

            //X坐标轴颜色
            cht1.ChartAreas[0].AxisX.LineColor = ColorTranslator.FromHtml("#38587a"); ;
            cht1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
            cht1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("宋体", 10f, FontStyle.Regular);

            //X坐标轴标题
            cht1.ChartAreas[0].AxisX.Title = "次数(次)";
            cht1.ChartAreas[0].AxisX.TitleFont = new Font("宋体", 10f, FontStyle.Regular);
            cht1.ChartAreas[0].AxisX.TitleForeColor = Color.Black;
            cht1.ChartAreas[0].AxisX.TextOrientation = TextOrientation.Horizontal;
            cht1.ChartAreas[0].AxisX.ToolTip = "次数(次)";

            //X轴网络线条
            cht1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            cht1.ChartAreas[0].AxisX.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

            //Y坐标轴颜色
            cht1.ChartAreas[0].AxisY.LineColor = ColorTranslator.FromHtml("#38587a");
            cht1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;
            cht1.ChartAreas[0].AxisY.LabelStyle.Font = new Font("宋体", 10f, FontStyle.Regular);

            //Y坐标轴标题
            cht1.ChartAreas[0].AxisY.Title = "次数(次)";
            cht1.ChartAreas[0].AxisY.TitleFont = new Font("宋体", 10f, FontStyle.Regular);
            cht1.ChartAreas[0].AxisY.TitleForeColor = Color.Black;
            cht1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            cht1.ChartAreas[0].AxisY.ToolTip = "次数(次)";

            //Y轴网格线条
            cht1.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            cht1.ChartAreas[0].AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

            cht1.ChartAreas[0].AxisY2.LineColor = Color.Transparent;
            cht1.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;
            string now_time = System.DateTime.Now.ToString("d");
            Legend legend = new Legend(now_time);
            legend.Title = "legendTitle";

            cht1.ChartAreas[0].AxisY.Maximum = 100000;//设定y轴的最大值
            cht1.Series[0].XValueType = ChartValueType.String;  //设置X轴上的值类型
            cht1.Series[0].Label = "#VAL";                //设置显示X Y的值    
            cht1.Series[0].LabelForeColor = Color.Black;
            cht1.Series[0].ToolTip = "#VALX:#VAL";     //鼠标移动到对应点显示数值
            cht1.Series[0].ChartType = SeriesChartType.Column;    //图类型(折线)


            cht1.Series[0].Color = Color.Lime;
            cht1.Series[0].LegendText = legend.Name;
            cht1.Series[0].IsValueShownAsLabel = true;
            cht1.Series[0].LabelForeColor = Color.Black;
            cht1.Series[0].CustomProperties = "DrawingStyle = Cylinder";
            cht1.Legends.Add(legend);
            cht1.Legends[0].Position.Auto = false;

            ArrayList Totalnum = new ArrayList();
            ArrayList Probename = new ArrayList();
            Totalnum.Clear();
            Probename.Clear();
            for (int i = 0; i < 40; i++)
            {
                Totalnum.Add(Variable.ProbeNum[i]);
            }

            for (int i = 0; i < 10; i++)
            {
                Probename.Add((i + 1).ToString() + "#上层内探针数");
                Probename.Add((i + 1).ToString() + "#上层外探针数");
                Probename.Add((i + 1).ToString() + "#下层内探针数");
                Probename.Add((i + 1).ToString() + "#下层外探针数");
            }

            //绑定数据
            cht1.Series[0].Points.DataBindXY(Probename, Totalnum);
            cht1.Series[0].Points[0].Color = Color.Black;
            cht1.Series[0].Palette = ChartColorPalette.Bright;
        }

        #endregion

        #region 探针次数刷新
        public void ProbRefresh()
        {
            try
            {
                cht1.Titles[1].Text = "探针次数设定：" + Variable.ProbeSet[0].ToString();

                //绑定数据
                ArrayList Totalnum = new ArrayList();
                ArrayList Probename = new ArrayList();
                Totalnum.Clear();
                Probename.Clear();
                for (int i = 0; i < 40; i++)
                {
                    Totalnum.Add(Variable.ProbeNum[i]);
                }

                for (int i = 0; i < 10; i++)
                {
                    Probename.Add((i + 1).ToString() + "#上层内探针数");
                    Probename.Add((i + 1).ToString() + "#上层外探针数");
                    Probename.Add((i + 1).ToString() + "#下层内探针数");
                    Probename.Add((i + 1).ToString() + "#下层外探针数");
                }

                //绑定数据
                cht1.Series[0].Points.DataBindXY(Probename, Totalnum);
                cht1.Series[0].Points[0].Color = Color.Black;
                cht1.Series[0].Palette = ChartColorPalette.Bright;
            }
            catch (Exception ex)
            {
                Log.SaveError(new StackTrace(new StackFrame(true)), new StackFrame(), ex);
            }
        }

        #endregion

        #region 初始化chart

        //以下按照先绘制chartArea、然后再绘制Series的步骤画图
        //绘制chartArea
        public void InitChart()
        {
            /*chart1*/
            //chartArea背景颜色
            chart1.BackColor = Color.Azure;

            //X轴设置
            chart1.ChartAreas[0].AxisX.Title = "时间";
            chart1.ChartAreas[0].AxisX.TitleAlignment = StringAlignment.Near;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;//不显示竖着的分割线
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss"; //X轴显示的时间格式，HH为大写时是24小时制，hh小写时是12小时制
            chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;//如果是时间类型的数据，间隔方式可以是秒、分、时
            chart1.ChartAreas[0].AxisX.Interval = DateTime.Parse("00:00:30").Second;//间隔为2秒

            //Y轴设置
            chart1.ChartAreas[0].AxisY.Title = "温度";
            chart1.ChartAreas[0].AxisY.TitleAlignment = StringAlignment.Center;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = true;//显示横着的分割线
            chart1.ChartAreas[0].AxisY.Minimum = 20;
            chart1.ChartAreas[0].AxisY.Maximum = 100;
            chart1.ChartAreas[0].AxisY.Interval = 5;

            /*chart2*/
            //chartArea背景颜色
            chart2.BackColor = Color.Azure;

            //X轴设置
            chart2.ChartAreas[0].AxisX.Title = "时间";
            chart2.ChartAreas[0].AxisX.TitleAlignment = StringAlignment.Near;
            chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = false;//不显示竖着的分割线
            chart2.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss"; //X轴显示的时间格式，HH为大写时是24小时制，hh小写时是12小时制
            chart2.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Seconds;//如果是时间类型的数据，间隔方式可以是秒、分、时
            chart2.ChartAreas[0].AxisX.Interval = DateTime.Parse("00:00:30").Second;//间隔为2秒

            //Y轴设置
            chart2.ChartAreas[0].AxisY.Title = "温度";
            chart2.ChartAreas[0].AxisY.TitleAlignment = StringAlignment.Center;
            chart2.ChartAreas[0].AxisY.MajorGrid.Enabled = true;//显示横着的分割线
            chart2.ChartAreas[0].AxisY.Minimum = 20;
            chart2.ChartAreas[0].AxisY.Maximum = 100;
            chart2.ChartAreas[0].AxisY.Interval = 5;

        }

        //绘制Series
        public void Initseries()
        {
            for (int i = 0; i < 20; i++)
            {
                /*chart1*/
                //清空原来数据缓存
                chart1.Series[i].Points.Clear();

                //Series绘制
                //chart1.Series[0].LegendText = "模组1";
                chart1.Series[i].ChartType = SeriesChartType.Spline;
                chart1.Series[i].XValueType = ChartValueType.DateTime;
                //chart1.Series[i].IsValueShownAsLabel = true;//显示数据点的值
                //chart1.Series[i].MarkerStyle = MarkerStyle.Diamond;//显示标记,菱形

                /*chart2*/
                //清空原来数据缓存
                chart2.Series[i].Points.Clear();

                //Series绘制
                //chart2.Series[0].LegendText = "模组1";
                chart2.Series[i].ChartType = SeriesChartType.Spline;
                chart2.Series[i].XValueType = ChartValueType.DateTime;
                //chart2.Series[i].IsValueShownAsLabel = true;//显示数据点的值
                //chart2.Series[i].MarkerStyle = MarkerStyle.Diamond;//显示标记,菱形

            }
        }
        #endregion

        #region 更新数据
        private void UpdateChartData1()
        {
            //Chart1
            if (Serie1_1.Count > 60)
            {
                Serie1_1.Dequeue();
                recordTime1.Dequeue();
            }
            else
            {
                Serie1_1.Enqueue(Variable.TemperData[0]);
                recordTime1.Enqueue(DateTime.Now.ToOADate());
            }

            if (Serie1_2.Count > 60)
            {
                Serie1_2.Dequeue();
            }
            else
            {
                Serie1_2.Enqueue(Variable.TemperData[1]);
            }

            if (Serie1_3.Count > 60)
            {
                Serie1_3.Dequeue();
            }
            else
            {
                Serie1_3.Enqueue(Variable.TemperData[2]);
            }

            if (Serie1_4.Count > 60)
            {
                Serie1_4.Dequeue();
            }
            else
            {
                Serie1_4.Enqueue(Variable.TemperData[3]);
            }

            if (Serie1_5.Count > 60)
            {
                Serie1_5.Dequeue();
            }
            else
            {
                Serie1_5.Enqueue(Variable.TemperData[4]);
            }

            if (Serie1_6.Count > 60)
            {
                Serie1_6.Dequeue();
            }
            else
            {
                Serie1_6.Enqueue(Variable.TemperData[5]);
            }

            if (Serie1_7.Count > 60)
            {
                Serie1_7.Dequeue();
            }
            else
            {
                Serie1_7.Enqueue(Variable.TemperData[6]);
            }

            if (Serie1_8.Count > 60)
            {
                Serie1_8.Dequeue();
            }
            else
            {
                Serie1_8.Enqueue(Variable.TemperData[7]);
            }

            if (Serie1_9.Count > 60)
            {
                Serie1_9.Dequeue();
            }
            else
            {
                Serie1_9.Enqueue(Variable.TemperData[8]);
            }

            if (Serie1_10.Count > 60)
            {
                Serie1_10.Dequeue();
            }
            else
            {
                Serie1_10.Enqueue(Variable.TemperData[9]);
            }
            if (Serie1_11.Count > 60)
            {
                Serie1_11.Dequeue();
            }
            else
            {
                Serie1_11.Enqueue(Variable.TemperData[10]);
            }

            if (Serie1_12.Count > 60)
            {
                Serie1_12.Dequeue();
            }
            else
            {
                Serie1_12.Enqueue(Variable.TemperData[11]);
            }

            if (Serie1_13.Count > 60)
            {
                Serie1_13.Dequeue();
            }
            else
            {
                Serie1_13.Enqueue(Variable.TemperData[12]);
            }

            if (Serie1_14.Count > 60)
            {
                Serie1_14.Dequeue();
            }
            else
            {
                Serie1_14.Enqueue(Variable.TemperData[13]);
            }

            if (Serie1_15.Count > 60)
            {
                Serie1_15.Dequeue();
            }
            else
            {
                Serie1_15.Enqueue(Variable.TemperData[14]);
            }

            if (Serie1_16.Count > 60)
            {
                Serie1_16.Dequeue();
            }
            else
            {
                Serie1_16.Enqueue(Variable.TemperData[15]);
            }

            if (Serie1_17.Count > 60)
            {
                Serie1_17.Dequeue();
            }
            else
            {
                Serie1_17.Enqueue(Variable.TemperData[16]);
            }

            if (Serie1_18.Count > 60)
            {
                Serie1_18.Dequeue();
            }
            else
            {
                Serie1_18.Enqueue(Variable.TemperData[17]);
            }

            if (Serie1_19.Count > 60)
            {
                Serie1_19.Dequeue();
            }
            else
            {
                Serie1_19.Enqueue(Variable.TemperData[18]);
            }

            if (Serie1_20.Count > 60)
            {
                Serie1_20.Dequeue();
            }
            else
            {
                Serie1_20.Enqueue(Variable.TemperData[19]);
            }

            //Chart2
            if (Serie2_1.Count > 60)
            {
                Serie2_1.Dequeue();
                recordTime2.Dequeue();
            }
            else
            {
                Serie2_1.Enqueue(Variable.TemperData[20]);
                recordTime2.Enqueue(DateTime.Now.ToOADate());
            }

            if (Serie2_2.Count > 60)
            {
                Serie2_2.Dequeue();
            }
            else
            {
                Serie2_2.Enqueue(Variable.TemperData[21]);
            }

            if (Serie2_3.Count > 60)
            {
                Serie2_3.Dequeue();
            }
            else
            {
                Serie2_3.Enqueue(Variable.TemperData[22]);
            }

            if (Serie2_4.Count > 60)
            {
                Serie2_4.Dequeue();
            }
            else
            {
                Serie2_4.Enqueue(Variable.TemperData[23]);
            }

            if (Serie2_5.Count > 60)
            {
                Serie2_5.Dequeue();
            }
            else
            {
                Serie2_5.Enqueue(Variable.TemperData[24]);
            }

            if (Serie2_6.Count > 60)
            {
                Serie2_6.Dequeue();
            }
            else
            {
                Serie2_6.Enqueue(Variable.TemperData[25]);
            }

            if (Serie2_7.Count > 60)
            {
                Serie2_7.Dequeue();
            }
            else
            {
                Serie2_7.Enqueue(Variable.TemperData[26]);
            }

            if (Serie2_8.Count > 60)
            {
                Serie2_8.Dequeue();
            }
            else
            {
                Serie2_8.Enqueue(Variable.TemperData[27]);
            }

            if (Serie2_9.Count > 60)
            {
                Serie2_9.Dequeue();
            }
            else
            {
                Serie2_9.Enqueue(Variable.TemperData[28]);
            }

            if (Serie2_10.Count > 60)
            {
                Serie2_10.Dequeue();
            }
            else
            {
                Serie2_10.Enqueue(Variable.TemperData[29]);
            }

            if (Serie2_11.Count > 60)
            {
                Serie2_11.Dequeue();
            }
            else
            {
                Serie2_11.Enqueue(Variable.TemperData[30]);
            }

            if (Serie2_12.Count > 60)
            {
                Serie2_12.Dequeue();
            }
            else
            {
                Serie2_12.Enqueue(Variable.TemperData[31]);
            }

            if (Serie2_13.Count > 60)
            {
                Serie2_13.Dequeue();
            }
            else
            {
                Serie2_13.Enqueue(Variable.TemperData[32]);
            }

            if (Serie2_14.Count > 60)
            {
                Serie2_14.Dequeue();
            }
            else
            {
                Serie2_14.Enqueue(Variable.TemperData[33]);
            }

            if (Serie2_15.Count > 60)
            {
                Serie2_15.Dequeue();
            }
            else
            {
                Serie2_15.Enqueue(Variable.TemperData[34]);
            }

            if (Serie2_16.Count > 60)
            {
                Serie2_16.Dequeue();
            }
            else
            {
                Serie2_16.Enqueue(Variable.TemperData[35]);
            }

            if (Serie2_17.Count > 60)
            {
                Serie2_17.Dequeue();
            }
            else
            {
                Serie2_17.Enqueue(Variable.TemperData[36]);
            }

            if (Serie2_18.Count > 60)
            {
                Serie2_18.Dequeue();
            }
            else
            {
                Serie2_18.Enqueue(Variable.TemperData[37]);
            }

            if (Serie2_19.Count > 60)
            {
                Serie2_19.Dequeue();
            }
            else
            {
                Serie2_19.Enqueue(Variable.TemperData[38]);
            }

            if (Serie2_20.Count > 60)
            {
                Serie2_20.Dequeue();
            }
            else
            {
                Serie2_20.Enqueue(Variable.TemperData[39]);
            }
        }
        #endregion

        #region Chart刷新
        private void Charttimer_Tick(object sender, EventArgs e)
        {
            //获取实时数据最近的60个数据
            UpdateChartData1();

            /*绘制Chart1*/
            //重新绘制曲折线图
            this.chart1.Series[0].Points.DataBindXY(recordTime1, Serie1_1);
            this.chart1.Series[1].Points.DataBindXY(recordTime1, Serie1_2);
            this.chart1.Series[2].Points.DataBindXY(recordTime1, Serie1_3);
            this.chart1.Series[3].Points.DataBindXY(recordTime1, Serie1_4);
            this.chart1.Series[4].Points.DataBindXY(recordTime1, Serie1_5);
            this.chart1.Series[5].Points.DataBindXY(recordTime1, Serie1_6);
            this.chart1.Series[6].Points.DataBindXY(recordTime1, Serie1_7);
            this.chart1.Series[7].Points.DataBindXY(recordTime1, Serie1_8);
            this.chart1.Series[8].Points.DataBindXY(recordTime1, Serie1_9);
            this.chart1.Series[9].Points.DataBindXY(recordTime1, Serie1_10);
            this.chart1.Series[10].Points.DataBindXY(recordTime1, Serie1_11);
            this.chart1.Series[11].Points.DataBindXY(recordTime1, Serie1_12);
            this.chart1.Series[12].Points.DataBindXY(recordTime1, Serie1_13);
            this.chart1.Series[13].Points.DataBindXY(recordTime1, Serie1_14);
            this.chart1.Series[14].Points.DataBindXY(recordTime1, Serie1_15);
            this.chart1.Series[15].Points.DataBindXY(recordTime1, Serie1_16);
            this.chart1.Series[16].Points.DataBindXY(recordTime1, Serie1_17);
            this.chart1.Series[17].Points.DataBindXY(recordTime1, Serie1_18);
            this.chart1.Series[18].Points.DataBindXY(recordTime1, Serie1_19);
            this.chart1.Series[19].Points.DataBindXY(recordTime1, Serie1_20);

            /*绘制Chart2*/
            //重新绘制曲折线图
            this.chart2.Series[0].Points.DataBindXY(recordTime2, Serie2_1);
            this.chart2.Series[1].Points.DataBindXY(recordTime2, Serie2_2);
            this.chart2.Series[2].Points.DataBindXY(recordTime2, Serie2_3);
            this.chart2.Series[3].Points.DataBindXY(recordTime2, Serie2_4);
            this.chart2.Series[4].Points.DataBindXY(recordTime2, Serie2_5);
            this.chart2.Series[5].Points.DataBindXY(recordTime2, Serie2_6);
            this.chart2.Series[6].Points.DataBindXY(recordTime2, Serie2_7);
            this.chart2.Series[7].Points.DataBindXY(recordTime2, Serie2_8);
            this.chart2.Series[8].Points.DataBindXY(recordTime2, Serie2_9);
            this.chart2.Series[9].Points.DataBindXY(recordTime2, Serie2_10);
            this.chart2.Series[10].Points.DataBindXY(recordTime2, Serie2_11);
            this.chart2.Series[11].Points.DataBindXY(recordTime2, Serie2_12);
            this.chart2.Series[12].Points.DataBindXY(recordTime2, Serie2_13);
            this.chart2.Series[13].Points.DataBindXY(recordTime2, Serie2_14);
            this.chart2.Series[14].Points.DataBindXY(recordTime2, Serie2_15);
            this.chart2.Series[15].Points.DataBindXY(recordTime2, Serie2_16);
            this.chart2.Series[16].Points.DataBindXY(recordTime2, Serie2_17);
            this.chart2.Series[17].Points.DataBindXY(recordTime2, Serie2_18);
            this.chart2.Series[18].Points.DataBindXY(recordTime2, Serie2_19);
            this.chart2.Series[19].Points.DataBindXY(recordTime2, Serie2_20);
        }

        #endregion

        #region 档案名称刷新
        private void ModelCombo_MouseMove(object sender, MouseEventArgs e)
        {
            ModelCombo.Items.Clear();
            //档案加载
            string path = @"D:\参数\";
            string[] files = Directory.GetFiles(path, "*.bin");
            foreach (string file in files)
            {
                string[] split = file.Split(new Char[] { '\\' });
                ModelCombo.Items.Add(split[split.Length - 1]);
            }
        }
        #endregion

        #region QR配对
        public bool QRToJudge(string str)
        {
            bool flag = false;
            for (int i = 0; i < Variable.qrRecord.Count; i++)
            {
                if (str == Variable.qrRecord[i])
                {
                    flag = true;
                    break;
                }
                else
                {
                    flag = false;
                }
            }
            return flag;
        }
        #endregion

        #region 运行中判断需不需要屏蔽机台
        public void HideTest()
        {
            // 屏蔽机台
            //Variable.ModelState  10个模组状态 => 0:空，1:已放料，2:测试中，3:测试OK，10:屏蔽
            for (int i = 0; i < 40; i++)
            {
                if (Variable.ModelState[i] == 0 && Variable.modelCheck[i])
                {
                    Variable.ModelState[i] = 0;
                    Variable.modelCheck[i] = true;//选择
                }
                else if (Variable.ModelState[i] == 0 && !Variable.modelCheck[i])
                {
                    Variable.ModelState[i] = 10;
                    Variable.modelCheck[i] = false;//屏蔽
                }
            }
        }
        #endregion

        #region 判断excel是否被打开
        public bool IsFileInUse(string fileName)
        {
            bool inUse = true;
            if (File.Exists(fileName))
            {
                FileStream fs = null;
                try
                {
                    fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                    inUse = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message.ToString());
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                    }
                }
                return inUse;           //true表示正在使用,false没有使用
            }
            else
            {
                return false;           //文件不存在则一定没有被使用
            }
        }
        #endregion

        #region tab切换
        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl2.SelectedIndex == 0)
            {
                //加载测试机显示
                CloseForm();
                foreach (Control c in SubPanel.Controls)
                {
                    SubPanel.Controls.Remove(c);
                    c.Dispose();
                }
                LoadSubForm(new Tray1Form());
            }
            else if (tabControl2.SelectedIndex == 1)
            {
                //加载测试机设置
                CloseForm();
                foreach (Control c in SubPanel1.Controls)
                {
                    SubPanel1.Controls.Remove(c);
                    c.Dispose();
                }
                LoadSubForm1(new Tray1SetForm());
            }
        }
        #endregion

        #region GPS界面切换
        bool flag;
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2 && !flag)
            {
                gpsForm.Close();
                gpsForm.Dispose();

                foreach (Control c in panel2.Controls)
                {
                    panel2.Controls.Remove(c);
                    c.Dispose();
                }
                LoadSubForm3(new GPSForm());
                flag = true;
            }
        }

        public void LoadSubForm3(object form)
        {
            if (this.panel2.Controls.Count > 0)
            {
                this.panel2.Controls.RemoveAt(0);
            }

            Form f = form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(f);
            this.panel2.Tag = f;
            f.Show();
        }
        #endregion

        #region 断电保存参数方法
        public void SaveCurrenParameter()
        {
            //生产信息
            iniHelper.writeIni("PGM", "OP", Variable.OP, path);
            iniHelper.writeIni("PGM", "BatchNum", Variable.BatchNum, path);
            iniHelper.writeIni("PGM", "OrderNum", Variable.OrderNum, path);
            iniHelper.writeIni("PGM", "PONum", Variable.PONum, path);
            iniHelper.writeIni("PGM", "startTime", Variable.startTime, path);
            iniHelper.writeIni("PGM", "runTime", Variable.runTime, path);
            iniHelper.writeIni("PGM", "stopTime", Variable.stopTime, path);
            iniHelper.writeIni("PGM", "alarmTime", Variable.alarmTime, path);
            iniHelper.writeIni("PGM", "endTime", Variable.endTime, path);
            iniHelper.writeIni("PGM", "runSecond", (runSecond + Second1).ToString(), path);
            iniHelper.writeIni("PGM", "stopSecond", (stopSecond + Second2).ToString(), path);
            iniHelper.writeIni("PGM", "alarmSecond", (alarmSecond + Second3).ToString(), path);
            iniHelper.writeIni("PGM", "alarmCount", Variable.alarmCount.ToString(), path);
            iniHelper.writeIni("PGM", "jamRate", Variable.jamRate, path);
            iniHelper.writeIni("PGM", "UPH", Variable.UPH.ToString(), path);
            iniHelper.writeIni("PGM", "inTrayNum", Variable.inTrayNum.ToString(), path);
            iniHelper.writeIni("PGM", "outTrayNum", Variable.outTrayNum.ToString(), path);
            iniHelper.writeIni("PGM", "inChipNum", Variable.inChipNum.ToString(), path);
            iniHelper.writeIni("PGM", "outChipNum", Variable.outChipNum.ToString(), path);
            iniHelper.writeIni("PGM", "OKChipNum", Variable.OKChipNum.ToString(), path);
            iniHelper.writeIni("PGM", "OP", Variable.OP.ToString(), path);
            iniHelper.writeIni("PGM", "FileName", Variable.FileName, path);

        }

        #endregion

        #region 断电加载参数方法
        public void LoadParameter()
        {
            //生产信息
            Variable.OP = iniHelper.getIni("PGM", "OP", "", path);
            Variable.BatchNum = iniHelper.getIni("PGM", "BatchNum", "", path);
            Variable.OrderNum = iniHelper.getIni("PGM", "OrderNum", "", path);
            Variable.PONum = iniHelper.getIni("PGM", "PONum", "", path);
            Variable.startTime = iniHelper.getIni("PGM", "startTime", "", path);
            Variable.runTime = iniHelper.getIni("PGM", "runTime", "", path);
            Variable.stopTime = iniHelper.getIni("PGM", "stopTime", "", path);
            Variable.alarmTime = iniHelper.getIni("PGM", "alarmTime", "", path);
            Variable.endTime = iniHelper.getIni("PGM", "endTime", "", path);
            runSecond = Convert.ToInt64(iniHelper.getIni("PGM", "runSecond", "", path));
            stopSecond = Convert.ToInt64(iniHelper.getIni("PGM", "stopSecond", "", path));
            alarmSecond = Convert.ToInt64(iniHelper.getIni("PGM", "alarmSecond", "", path));
            Variable.alarmCount = Convert.ToInt64(iniHelper.getIni("PGM", "alarmCount", "", path));
            Variable.jamRate = iniHelper.getIni("PGM", "jamRate", "", path);
            Variable.UPH = Convert.ToDouble(iniHelper.getIni("PGM", "UPH", "", path));
            Variable.inTrayNum = Convert.ToDouble(iniHelper.getIni("PGM", "inTrayNum", "", path));
            Variable.outTrayNum = Convert.ToDouble(iniHelper.getIni("PGM", "outTrayNum", "", path));
            Variable.inChipNum = Convert.ToDouble(iniHelper.getIni("PGM", "inChipNum", "", path));
            Variable.outChipNum = Convert.ToDouble(iniHelper.getIni("PGM", "outChipNum", "", path));
            Variable.OKChipNum = Convert.ToDouble(iniHelper.getIni("PGM", "OKChipNum", "", path));
        }
        #endregion

        #region 温度设定
        private void btnTemper(int data)
        {
            Variable.temWriteFlag = true;
            Thread.Sleep(100);
            //地址:01,02,03,04,05,06,07,08,09,0A,0B,0C,0D,0E,0F,10,11,12,13.14
            //TempWrite("01", Convert.ToInt16(temper.Value * 10));
            for (int i = 0; i < 20; i++)
            {
                TempWrite1((i + 1).ToString("X2"), Convert.ToInt16(data * 10));
                Thread.Sleep(200);
                TempWrite2((i + 1).ToString("X2"), Convert.ToInt16(data * 10));
                Thread.Sleep(200);
            }
            Variable.temWriteFlag = false;
        }
        #endregion

        #region 最高温度设定
        private void btnUpTemper(int data)
        {
            Variable.temWriteFlag = true;
            Thread.Sleep(100);
            //地址:01,02,03,04,05,06,07,08,09,0A,0B,0C,0D,0E,0F,10,11,12,13.14
            //UpTempWrite("01", Convert.ToInt16(Uptemper.Value * 10));
            for (int i = 0; i < 20; i++)
            {
                UpTempWrite1((i + 1).ToString("X2"), Convert.ToInt16(data * 10));
                Thread.Sleep(200);
                UpTempWrite2((i + 1).ToString("X2"), Convert.ToInt16(data * 10));
                Thread.Sleep(200);
            }
            Variable.temWriteFlag = false;
        }
        #endregion

        #region 上限温度设定
        private void btnUpLimitTemper(int data)
        {
            Variable.temWriteFlag = true;
            Thread.Sleep(100);
            //地址:01,02,03,04,05,06,07,08,09,0A,0B,0C,0D,0E,0F,10,11,12,13.14
            //UpTempWrite("01", Convert.ToInt16(Uptemper.Value * 10));
            for (int i = 0; i < 20; i++)
            {
                UplimitWrite1((i + 1).ToString("X2"), Convert.ToInt16(data * 10));
                Thread.Sleep(200);
                UplimitWrite2((i + 1).ToString("X2"), Convert.ToInt16(data * 10));
                Thread.Sleep(200);
            }
            Variable.temWriteFlag = false;
        }
        #endregion

        #region 下限温度设定
        private void btnDownLimitTemper(int data)
        {
            Variable.temWriteFlag = true;
            Thread.Sleep(100);
            //地址:01,02,03,04,05,06,07,08,09,0A,0B,0C,0D,0E,0F,10,11,12,13.14
            //UpTempWrite("01", Convert.ToInt16(Uptemper.Value * 10));
            for (int i = 0; i < 20; i++)
            {
                DownlimitWrite1((i + 1).ToString("X2"), Convert.ToInt16(data * 10));
                Thread.Sleep(200);
                DownlimitWrite2((i + 1).ToString("X2"), Convert.ToInt16(data * 10));
                Thread.Sleep(200);
            }
            Variable.temWriteFlag = false;
        }
        #endregion

        #region 温度写入方法

        #region 温度写入

        public void TempWrite1(string num, int data)//TempWrite("01", 110);
        {
            //地址:01,02,03,04,05,06,07,08,09,0A,0B,0C,0D,0E,0F,10,11,12,13.14
            string s = data.ToString("X4");
            //string str = num + "064701" + s;//"0106470103E8"
            string str = num + "061001" + s;//"0106100103E8"
            char[] startch = { ':' };
            char[] endch = { '\0', '\0', '\r', '\n' };
            char[] lrcch = tem1.LRC(str).ToCharArray();//校验码生成字符数组
            lrcch.CopyTo(endch, 0);//把校验码数组嵌入到endch中
            char[] datach = str.ToCharArray();//发送内容生成字符数组
            char[] ch3 = new char[datach.Length + 5];
            datach.CopyTo(ch3, 1);//组成命令字符数组
            endch.CopyTo(ch3, datach.Length + 1);//组成命令字符数组
            startch.CopyTo(ch3, 0);//组成命令字符数组
            byte[] byt = new byte[ch3.Length];//把命令字符数组转换为字节数组
            for (int i = 0; i < ch3.Length; i++)
            {
                byt[i] = Convert.ToByte(ch3[i]);
            }
            tem1.SendData(byt);
        }
        public void TempWrite2(string num, int data)//TempWrite("01", 110);
        {
            //地址:01,02,03,04,05,06,07,08,09,0A,0B,0C,0D,0E,0F,10,11,12,13.14
            string s = data.ToString("X4");
            //string str = num + "064701" + s;//"0106470103E8"
            string str = num + "061001" + s;//"0106100103E8"
            char[] startch = { ':' };
            char[] endch = { '\0', '\0', '\r', '\n' };
            char[] lrcch = tem2.LRC(str).ToCharArray();//校验码生成字符数组
            lrcch.CopyTo(endch, 0);//把校验码数组嵌入到endch中
            char[] datach = str.ToCharArray();//发送内容生成字符数组
            char[] ch3 = new char[datach.Length + 5];
            datach.CopyTo(ch3, 1);//组成命令字符数组
            endch.CopyTo(ch3, datach.Length + 1);//组成命令字符数组
            startch.CopyTo(ch3, 0);//组成命令字符数组
            byte[] byt = new byte[ch3.Length];//把命令字符数组转换为字节数组
            for (int i = 0; i < ch3.Length; i++)
            {
                byt[i] = Convert.ToByte(ch3[i]);
            }
            tem2.SendData(byt);
        }
        #endregion

        #region 最高温度写入
        public void UpTempWrite1(string num, int data)//TempWrite("01", 110);
        {
            //地址:01,02,03,04,05,06,07,08,09,0A,0B,0C,0D,0E,0F,10,11,12,13.14
            string s = data.ToString("X4");
            //string str = num + "064706" + s;//"0106470603E8"
            string str = num + "061002" + s;//"0106100203E8"
            char[] startch = { ':' };
            char[] endch = { '\0', '\0', '\r', '\n' };
            char[] lrcch = tem1.LRC(str).ToCharArray();//校验码生成字符数组
            lrcch.CopyTo(endch, 0);//把校验码数组嵌入到endch中
            char[] datach = str.ToCharArray();//发送内容生成字符数组
            char[] ch3 = new char[datach.Length + 5];
            datach.CopyTo(ch3, 1);//组成命令字符数组
            endch.CopyTo(ch3, datach.Length + 1);//组成命令字符数组
            startch.CopyTo(ch3, 0);//组成命令字符数组
            byte[] byt = new byte[ch3.Length];//把命令字符数组转换为字节数组
            for (int i = 0; i < ch3.Length; i++)
            {
                byt[i] = Convert.ToByte(ch3[i]);
            }
            tem1.SendData(byt);
        }

        public void UpTempWrite2(string num, int data)//TempWrite("01", 110);
        {
            //地址:01,02,03,04,05,06,07,08,09,0A,0B,0C,0D,0E,0F,10,11,12,13.14
            string s = data.ToString("X4");
            //string str = num + "064706" + s;//"0106470603E8"
            string str = num + "061002" + s;//"0106100203E8"
            char[] startch = { ':' };
            char[] endch = { '\0', '\0', '\r', '\n' };
            char[] lrcch = tem2.LRC(str).ToCharArray();//校验码生成字符数组
            lrcch.CopyTo(endch, 0);//把校验码数组嵌入到endch中
            char[] datach = str.ToCharArray();//发送内容生成字符数组
            char[] ch3 = new char[datach.Length + 5];
            datach.CopyTo(ch3, 1);//组成命令字符数组
            endch.CopyTo(ch3, datach.Length + 1);//组成命令字符数组
            startch.CopyTo(ch3, 0);//组成命令字符数组
            byte[] byt = new byte[ch3.Length];//把命令字符数组转换为字节数组
            for (int i = 0; i < ch3.Length; i++)
            {
                byt[i] = Convert.ToByte(ch3[i]);
            }
            tem2.SendData(byt);
        }
        #endregion

        #region 上限温度写入

        public void UplimitWrite1(string num, int data)//TempWrite("01", 110);
        {
            //地址:01,02,03,04,05,06,07,08,09,0A,0B,0C,0D,0E,0F,10,11,12,13.14
            string s = data.ToString("X4");
            string str = num + "061024" + s;//"0106470103E8"
            char[] startch = { ':' };
            char[] endch = { '\0', '\0', '\r', '\n' };
            char[] lrcch = tem1.LRC(str).ToCharArray();//校验码生成字符数组
            lrcch.CopyTo(endch, 0);//把校验码数组嵌入到endch中
            char[] datach = str.ToCharArray();//发送内容生成字符数组
            char[] ch3 = new char[datach.Length + 5];
            datach.CopyTo(ch3, 1);//组成命令字符数组
            endch.CopyTo(ch3, datach.Length + 1);//组成命令字符数组
            startch.CopyTo(ch3, 0);//组成命令字符数组
            byte[] byt = new byte[ch3.Length];//把命令字符数组转换为字节数组
            for (int i = 0; i < ch3.Length; i++)
            {
                byt[i] = Convert.ToByte(ch3[i]);
            }
            tem1.SendData(byt);
        }

        public void UplimitWrite2(string num, int data)//TempWrite("01", 110);
        {
            //地址:01,02,03,04,05,06,07,08,09,0A,0B,0C,0D,0E,0F,10,11,12,13.14
            string s = data.ToString("X4");
            string str = num + "061024" + s;//"0106470103E8"
            char[] startch = { ':' };
            char[] endch = { '\0', '\0', '\r', '\n' };
            char[] lrcch = tem2.LRC(str).ToCharArray();//校验码生成字符数组
            lrcch.CopyTo(endch, 0);//把校验码数组嵌入到endch中
            char[] datach = str.ToCharArray();//发送内容生成字符数组
            char[] ch3 = new char[datach.Length + 5];
            datach.CopyTo(ch3, 1);//组成命令字符数组
            endch.CopyTo(ch3, datach.Length + 1);//组成命令字符数组
            startch.CopyTo(ch3, 0);//组成命令字符数组
            byte[] byt = new byte[ch3.Length];//把命令字符数组转换为字节数组
            for (int i = 0; i < ch3.Length; i++)
            {
                byt[i] = Convert.ToByte(ch3[i]);
            }
            tem2.SendData(byt);
        }
        #endregion

        #region 下限温度写入

        public void DownlimitWrite1(string num, int data)//TempWrite("01", 110);
        {
            //地址:01,02,03,04,05,06,07,08,09,0A,0B,0C,0D,0E,0F,10,11,12,13.14
            string s = data.ToString("X4");
            string str = num + "061025" + s;//"0106470103E8"
            char[] startch = { ':' };
            char[] endch = { '\0', '\0', '\r', '\n' };
            char[] lrcch = tem1.LRC(str).ToCharArray();//校验码生成字符数组
            lrcch.CopyTo(endch, 0);//把校验码数组嵌入到endch中
            char[] datach = str.ToCharArray();//发送内容生成字符数组
            char[] ch3 = new char[datach.Length + 5];
            datach.CopyTo(ch3, 1);//组成命令字符数组
            endch.CopyTo(ch3, datach.Length + 1);//组成命令字符数组
            startch.CopyTo(ch3, 0);//组成命令字符数组
            byte[] byt = new byte[ch3.Length];//把命令字符数组转换为字节数组
            for (int i = 0; i < ch3.Length; i++)
            {
                byt[i] = Convert.ToByte(ch3[i]);
            }
            tem1.SendData(byt);
        }

        public void DownlimitWrite2(string num, int data)//TempWrite("01", 110);
        {
            //地址:01,02,03,04,05,06,07,08,09,0A,0B,0C,0D,0E,0F,10,11,12,13.14
            string s = data.ToString("X4");
            string str = num + "061025" + s;//"0106470103E8"
            char[] startch = { ':' };
            char[] endch = { '\0', '\0', '\r', '\n' };
            char[] lrcch = tem2.LRC(str).ToCharArray();//校验码生成字符数组
            lrcch.CopyTo(endch, 0);//把校验码数组嵌入到endch中
            char[] datach = str.ToCharArray();//发送内容生成字符数组
            char[] ch3 = new char[datach.Length + 5];
            datach.CopyTo(ch3, 1);//组成命令字符数组
            endch.CopyTo(ch3, datach.Length + 1);//组成命令字符数组
            startch.CopyTo(ch3, 0);//组成命令字符数组
            byte[] byt = new byte[ch3.Length];//把命令字符数组转换为字节数组
            for (int i = 0; i < ch3.Length; i++)
            {
                byt[i] = Convert.ToByte(ch3[i]);
            }
            tem2.SendData(byt);
        }
        #endregion

        #endregion

        #region 读取QR数据到数组
        public void ReadQRArray()
        {
            string CONN = Access.GetSqlConnectionString1();
            OleDbConnection conn = new OleDbConnection(CONN);
            string cmdText = "select * from QRRecord";

            OleDbDataAdapter sda = new OleDbDataAdapter(cmdText, conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            DataTable dt = ds.Tables[0];

            Variable.qrRecord.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Variable.qrRecord.Add(dt.Rows[i][2].ToString());
            }

        }

        #endregion

        #region 读取OP数据到数组
        public void ReadOPArray()
        {
            string CONN = Access.GetSqlConnectionString();
            OleDbConnection conn = new OleDbConnection(CONN);
            string cmdText = "select * from OPRecord";

            OleDbDataAdapter sda = new OleDbDataAdapter(cmdText, conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            DataTable dt = ds.Tables[0];

            Variable.OPNumber.Clear();
            Variable.OPPassword.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Variable.OPNumber.Add(dt.Rows[i][2].ToString());
                Variable.OPPassword.Add(dt.Rows[i][3].ToString());
            }
        }

        #endregion

        #region OP配对
        public bool OPToJudge(string str1, string str2)
        {
            bool flag = false;
            for (int i = 0; i < Variable.OPNumber.Count; i++)
            {
                if (str1 == Variable.OPNumber[i] && str2 == Variable.OPPassword[i])
                {
                    flag = true;
                    break;
                }
                else
                {
                    flag = false;
                }
            }
            return flag;
        }
        #endregion

        #region MES

        public bool MesCall(string strLot)
        {
            //try
            //{
                //地址:http://10.8.33.9/MESWS_sz/wsWIP.asmx?wsdl
                //传参格式
                //str1 = "<request><parameter><lotno><name>lotno</name><type>String</type><value>"
                //str2 = "</value><desc></desc></lotno></parameter></request>"
                //inputgram = str1 + strLot + str2
                //mesWeb.ProvideLotInformation(inputgram)
                bool flag = false;
                string status = "";
                QM9505.ServiceWeb.wsWIPSoapClient mesWeb = new ServiceWeb.wsWIPSoapClient();
                string str1 = "<request><parameter><lotno><name>lotno</name><type>String</type><value>";
                string str2 = "</value><desc></desc></lotno></parameter></request>";
                string inputgram = str1 + strLot + str2;
                status = mesWeb.ProvideLotInformation(inputgram);
                if (status.Length > 0)
                {
                    string[] temmes = status.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < temmes.Length; i++)
                    {
                        if (temmes[i].Contains("<OPNO>"))
                        {
                            mes_OPNO = temmes[i].Split(new char[] { '<', '>' })[2];
                        }
                        if (temmes[i].Contains("<CURQTY>"))
                        {
                            mes_CURQTY = temmes[i].Split(new char[] { '<', '>' })[2];
                        }
                    }
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            //}
            //catch
            //{
            //    flag = false;
            //    MessageBox.Show("MES链接错误，检查网络!");
            //}
            return flag;
        }

        #endregion


        private void button1_Click(object sender, EventArgs e)
        {
            bool flag = MesCall("A46B0AFD0H");
            //string mesStation1 = "";
            //string mesStation2 = "";
            //string status = "<response><identity></identity><returnvalue><providelotinformation><name>ProvideLotInformation</name><type>DataSet</type><schema>\n<xs:schema id=\"NewDataSet\" xmlns=\"\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">\n  <xs:element name=\"NewDataSet\" msdata:IsDataSet=\"true\" msdata:UseCurrentLocale=\"true\">\n    <xs:complexType>\n      <xs:choice minOccurs=\"0\" maxOccurs=\"unbounded\">\n        <xs:element name=\"ProvideLotInformation\">\n          <xs:complexType>\n            <xs:sequence>\n              <xs:element name=\"OPNO\" type=\"xs:string\" minOccurs=\"0\" />\n              <xs:element name=\"EQUIPMENTNO\" type=\"xs:string\" minOccurs=\"0\" />\n              <xs:element name=\"EVENTTIME\" type=\"xs:dateTime\" minOccurs=\"0\" />\n              <xs:element name=\"CURQTY\" type=\"xs:decimal\" minOccurs=\"0\" />\n            </xs:sequence>\n          </xs:complexType>\n        </xs:element>\n      </xs:choice>\n    </xs:complexType>\n  </xs:element>\n</xs:schema></schema><value><NewDataSet>\n  <ProvideLotInformation>\n    <OPNO>BURN IN 1</OPNO>\n    <EQUIPMENTNO>FHQ0705</EQUIPMENTNO>\n    <EVENTTIME>2024-07-03T07:33:40+08:00</EVENTTIME>\n    <CURQTY>5735.0000</CURQTY>\n  </ProvideLotInformation>\n</NewDataSet></value><desc></desc></providelotinformation></returnvalue><result>success</result></response>";

            //string[] temmes = status.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            //for (int i = 0; i < temmes.Length; i++)
            //{
            //    if (temmes[i].Contains("<OPNO>"))
            //    {
            //        mesStation1 = temmes[i].Split(new char[] { '<', '>' })[2];
            //    }
            //    if (temmes[i].Contains("<CURQTY>"))
            //    {
            //        mesStation2 = temmes[i].Split(new char[] { '<', '>' })[2];
            //    }
            //}

        }




    }
}
