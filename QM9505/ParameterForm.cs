using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QM9505
{
    public partial class ParameterForm : Form
    {
        private object threadLock;
        INIHelper inIHelper = new INIHelper();
        DataGrid dataGrid = new DataGrid();
        Thread refreshThread;
        public string txtStatus;
        TextBox[] txtbox = new TextBox[32];
        public string group;
        public GroupBox[] groupBox = new GroupBox[11];
        public string check;
        public CheckBox[] checkBox = new CheckBox[40];
        public int formNum = 0;
        Temperature1 tem1 = new Temperature1();
        Temperature2 tem2 = new Temperature2();
        TXT myTXT = new TXT();
        public int row;
        public int col;
        public int row1;
        public int col1;

        public ParameterForm()
        {
            InitializeComponent();
            threadLock = new object();
            tabPage5.Parent = null;
            dataGrid.InitializeSite(dataGridView2, 40, 3);
            //查询OP
            SearchOPData();
            OPGridView.Columns[0].Visible = false;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            formNum += 1;
            if (formNum > 1)
            {
                Variable.formOpenFlag = false;

                if (refreshThread != null)
                {
                    refreshThread.Abort();
                    refreshThread = null;
                }
            }

            base.OnVisibleChanged(e);
            if (!IsHandleCreated)
            {
                this.Close();
            }
        }

        #region 打开窗体
        private void ParameterForm_Load(object sender, EventArgs e)
        {
            //初始化DataGridView
            DataGridViewInit(dataGridAxis1, 0);
            DataGridViewInit(dataGridAxis2, 0);

            for (int i = 0; i < 32; i++)
            {
                txtStatus = "offset" + (i + 1).ToString().PadLeft(2, '0');
                txtbox[i] = (TextBox)(this.Controls.Find(txtStatus, true)[0]);
            }

            //加载参数
            LoadPoint(Application.StartupPath + "\\Point.ini");
            string path = @"D:\参数\" + Variable.FileName;
            LoadParameter(path);
            LoadParameter();


            //权限
            for (int i = 0; i < 11; i++)
            {
                group = "groupBox" + (i + 100).ToString();//.PadLeft(2, '0');
                groupBox[i] = (GroupBox)(this.Controls.Find(group, true)[0]);

            }

            //屏蔽
            for (int i = 0; i < 40; i++)
            {
                check = "ModelCheck" + (i + 1).ToString();//.PadLeft(2, '0');
                checkBox[i] = (CheckBox)(this.Controls.Find(check, true)[0]);
                checkBox[i].CheckedChanged += new System.EventHandler(ModelCheck_CheckedChanged);
            }

            for (int i = 0; i < 40; i++)
            {
                if (Variable.modelCheck[i])
                {
                    checkBox[i].Checked = true;
                }
                else
                {
                    checkBox[i].Checked = false;
                }
            }

            //查询QR
            SearchData();
            QRGridView.Columns[0].Visible = false;

            //查询OP
            SearchOPData();
            OPGridView.Columns[0].Visible = false;

            refreshThread = new Thread(Rsh);//开始后，开新线程执行此方法
            refreshThread.IsBackground = true;
            refreshThread.Start();

            //变量传给控件
            VariableToView();

            SetTextBoxOnlyInt();

            Variable.formOpenFlag = true;
        }

        #endregion

        #region tabpage切换
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 1:
                    {
                        dataGrid.NotChangeListRow(dataGridAxis1);
                        AddControl_Parameter(dataGridAxis1, 0);
                        ReadAxis(dataGridAxis1, "AxisNum");
                        ReadAxisParameter(dataGridAxis1);
                        dataGridAxis1.Columns[8].ReadOnly = true;
                        dataGridAxis1.Columns[9].ReadOnly = true;
                        dataGridAxis1.Columns[10].ReadOnly = true;
                        dataGridAxis1.Columns[11].ReadOnly = true;

                        dataGridAxis1.RowsDefaultCellStyle.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                        //dataGridAxis.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));


                        break;
                    }
            }
        }
        #endregion

        #region 初始化DataGridView
        public void DataGridViewInit(DataGridView dataGridView, int num)
        {
            //添加列
            dataGridView.Columns.Add((1 + num).ToString(), "轴号");
            dataGridView.Columns.Add((2 + num).ToString(), "轴名称");
            dataGridView.Columns.Add((3 + num).ToString(), "起始速度");
            dataGridView.Columns.Add((4 + num).ToString(), "运行速度");
            dataGridView.Columns.Add((5 + num).ToString(), "回零速度");
            dataGridView.Columns.Add((6 + num).ToString(), "爬行速度");
            dataGridView.Columns.Add((7 + num).ToString(), "加速度");
            dataGridView.Columns.Add((8 + num).ToString(), "减速度");
            dataGridView.Columns.Add((9 + num).ToString(), "平滑系数");
            dataGridView.Columns.Add((10 + num).ToString(), "平滑时间");
            dataGridView.Columns.Add((11 + num).ToString(), "导程");
            dataGridView.Columns.Add((12 + num).ToString(), "每转脉冲数");

            //添加行
            for (int i = 0; i < 16; i++)
            {
                dataGridView.Rows.Add();
            }
            //修改高度
            for (int i = 0; i < 16; i++)
            {
                dataGridView.Rows[i].Height = 54;
            }
            //修改宽度
            for (int i = 0; i < 12; i++)
            {
                if (i == 0)
                {
                    dataGridView.Columns[i].Width = 70;
                }
                else
                {
                    dataGridView.Columns[i].Width = 150;
                }
            }

            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//列标题居中
            dataGridView.AllowUserToAddRows = false;//取消第一行
            dataGridView.RowHeadersVisible = false;//取消第一列
            dataGridView.ClearSelection(); //取消默认选中  
        }

        #endregion

        #region 添加参数控件
        public void AddControl_Parameter(DataGridView dataGridView, int num)
        {
            //轴号
            Label[] labAxis = new Label[16];
            for (int i = 0; i < 16; i++)
            {
                labAxis[i] = new Label();
                labAxis[i].Text = (i + 1 + num).ToString().PadLeft(2, '0');
                labAxis[i].AutoSize = false;
                labAxis[i].TextAlign = ContentAlignment.MiddleCenter;
                labAxis[i].BackColor = Color.LightSteelBlue;
                dataGridView.Controls.Add(labAxis[i]);
                Rectangle rect = dataGridView.GetCellDisplayRectangle(0, i, false);
                labAxis[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                labAxis[i].Location = new Point(rect.Left + 3, rect.Top + 3);

            }


            //轴名称
            Label[] labname = new Label[16];

            for (int i = 0; i < 16; i++)
            {
                labname[i] = new Label();
                labname[i].Name = "AxisNum" + (i + 1 + num).ToString().PadLeft(2, '0');
                labname[i].AutoSize = false;
                labname[i].TextAlign = ContentAlignment.MiddleCenter;
                labname[i].BackColor = Color.LightSteelBlue;
                dataGridView.Controls.Add(labname[i]);
                Rectangle rect = dataGridView.GetCellDisplayRectangle(1, i, false);
                labname[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                labname[i].Location = new Point(rect.Left + 3, rect.Top + 3);

            }

        }

        #endregion

        #region 读取Axis名称
        public void ReadAxis(DataGridView dataGridView, string name)
        {

            foreach (Control c in dataGridView.Controls)
            {
                if (c.Name.Contains(name))
                {
                    string index = c.Name;
                    c.Text = inIHelper.AxisReadContentValue("Axis", index);
                }
            }
        }

        #endregion

        #region 添加平滑系数，平滑时间，导程，每转脉冲数
        public void ReadAxisParameter(DataGridView dataGridView)
        {
            for (int i = 0; i < 16; i++)
            {
                dataGridView.Rows[i].Cells[2].Value = Variable.AxisStartVel[i + 1];
                dataGridView.Rows[i].Cells[3].Value = Variable.AxisRunVel[i + 1];
                dataGridView.Rows[i].Cells[4].Value = Variable.AxisHmoeVelHight[i + 1];
                dataGridView.Rows[i].Cells[5].Value = Variable.AxisHmoeVelLow[i + 1];
                dataGridView.Rows[i].Cells[6].Value = Variable.AxisTacc[i + 1];
                dataGridView.Rows[i].Cells[7].Value = Variable.AxisTdec[i + 1];
                dataGridView.Rows[i].Cells[8].Value = Variable.AxisSmoothCoefficient[i + 1];
                dataGridView.Rows[i].Cells[9].Value = Variable.AxisSmoothTime[i + 1];
                dataGridView.Rows[i].Cells[10].Value = Variable.AxisPitch[i + 1];
                dataGridView.Rows[i].Cells[11].Value = Variable.AxisPulse[i + 1];
            }

        }
        #endregion

        #region 文本框不能为空
        public bool TextEmpty()
        {
            bool flag = false;
            foreach (Control c in this.Controls)
            {
                foreach (Control item1 in this.Controls)
                {
                    foreach (Control item2 in item1.Controls)
                    {
                        foreach (Control item3 in item2.Controls)
                        {
                            foreach (Control item4 in item3.Controls)
                            {
                                if (item4 is TextBox)
                                {
                                    if (item4.Text.Length == 0)
                                    {
                                        //MessageBox.Show("文本框不能为空,请输入0");
                                        flag = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return flag;
        }

        #endregion

        #region 文本框去除符号
        public void SetTextBoxOnlyInt()
        {
            GC.Collect();
            foreach (Control item1 in this.Controls)
            {
                foreach (Control item2 in item1.Controls)
                {
                    foreach (Control item3 in item2.Controls)
                    {
                        foreach (Control item4 in item3.Controls)
                        {
                            if (item4 is TextBox)
                            {
                                ((TextBox)item4).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
                            }
                        }
                    }
                }
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8 && e.KeyChar != (char)45 && e.KeyChar != (char)46)
            {
                e.Handled = true;
            }
        }

        #endregion

        #region 刷新
        public void Rsh()
        {
            while (true)
            {
                if (this.IsDisposed)
                {
                    return;
                }

                //屏蔽机台
                for (int i = 0; i < 40; i++)
                {
                    if (checkBox[i].Checked)
                    {
                        Variable.modelCheck[i] = true;
                    }
                    else
                    {
                        Variable.modelCheck[i] = false;
                    }
                }

                //权限
                if (Variable.userEnter == Variable.UserEnter.User)
                {
                    for (int i = 0; i < 11; i++)
                    {
                        groupBox[i].Enabled = false;
                    }
                }
                else
                {
                    for (int i = 0; i < 11; i++)
                    {
                        groupBox[i].Enabled = true;
                    }
                }

                //权限参数
                if (Variable.userEnter == Variable.UserEnter.Manufacturer || Variable.userEnter == Variable.UserEnter.Administrator)
                {
                    dataGridAxis1.Enabled = true;
                    dataGridAxis2.Enabled = true;
                }
                else
                {
                    dataGridAxis1.Enabled = false;
                    dataGridAxis2.Enabled = false;
                }

                Thread.Sleep(1);
            }
        }
        #endregion

        #region 加载参数

        /// <summary>
        /// 加载参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
            OpenFileDialog1.InitialDirectory = "D:/";
            OpenFileDialog1.Filter = "加载参数(*.bin)|*.bin";
            OpenFileDialog1.FilterIndex = 1;
            OpenFileDialog1.RestoreDirectory = true;
            OpenFileDialog1.FileName = "";
            OpenFileDialog1.ShowDialog();
            try
            {
                if (OpenFileDialog1.FileName == "")
                {
                    MessageBox.Show("加载文件名不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    string path = OpenFileDialog1.FileName;//保存文件地址名称
                    string[] split = path.Split(new Char[] { '\\' });
                    Variable.FileName = split[split.Length - 1];//档案名称

                    //加载点位
                    string point = Application.StartupPath + "\\Point.ini";
                    LoadPoint(point);

                    //加载参数
                    LoadParameter(path);
                    VariableToView();

                    MessageBox.Show("参数加载成功！");
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show("载入参数失败，异常信息如下：" + EX.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        #endregion

        #region 加载参数方法
        public void LoadParameter(string path)
        {
            lock (threadLock)
            {
                Variable.LoadparameterFlag = false;
                //档案名称
                Variable.FileName = inIHelper.getIni("PGM", "FileName", "", path);

                //功能参数
                string fistPower = inIHelper.getIni("GN", "FistPower", "", path);
                if (fistPower == "1")
                {
                    FistPower.Checked = true;
                    Variable.FistPower = true;
                }
                else
                {
                    FistPower.Checked = false;
                    Variable.FistPower = false;
                }

                string upDoor_Check = inIHelper.getIni("GN", "UpDoor_Check", "", path);
                if (upDoor_Check == "1")
                {
                    UpDoor_Check.Checked = true;
                    Variable.UpDoorCheck = true;
                }
                else
                {
                    UpDoor_Check.Checked = false;
                    Variable.UpDoorCheck = false;
                }

                string modelDoor_Check = inIHelper.getIni("GN", "ModelDoor_Check", "", path);
                if (modelDoor_Check == "1")
                {
                    ModelDoor_Check.Checked = true;
                    Variable.ModelDoorCheck = true;
                }
                else
                {
                    ModelDoor_Check.Checked = false;
                    Variable.ModelDoorCheck = false;
                }

                string downDoor_Check = inIHelper.getIni("GN", "DownDoor_Check", "", path);
                if (downDoor_Check == "1")
                {
                    DownDoor_Check.Checked = true;
                    Variable.DownDoorCheck = true;
                }
                else
                {
                    DownDoor_Check.Checked = false;
                    Variable.DownDoorCheck = false;
                }

                string hotModcheckBox = inIHelper.getIni("GN", "HotModcheckBox", "", path);
                if (hotModcheckBox == "1")
                {
                    HotModcheckBox.Checked = true;
                    Variable.HotModel = true;
                }
                else
                {
                    HotModcheckBox.Checked = false;
                    Variable.HotModel = false;
                }

                string photocheckBox = inIHelper.getIni("GN", "PhotocheckBox", "", path);
                if (photocheckBox == "1")
                {
                    PhotocheckBox.Checked = true;
                    Variable.PhotoCheck = true;
                }
                else
                {
                    PhotocheckBox.Checked = false;
                    Variable.PhotoCheck = false;
                }

                string needleHideCheck = inIHelper.getIni("GN", "siteShieldCheck", "", path);
                if (needleHideCheck == "1")
                {
                    siteShieldCheck.Checked = true;
                    Variable.siteShieldCheck = true;
                }
                else
                {
                    siteShieldCheck.Checked = false;
                    Variable.siteShieldCheck = false;
                }

                string needleMEScheck = inIHelper.getIni("GN", "MEScheck", "", path);
                if (needleMEScheck == "1")
                {
                    MEScheck.Checked = true;
                    Variable.MEScheck = true;
                }
                else
                {
                    MEScheck.Checked = false;
                    Variable.MEScheck = false;
                }

                string trayQRCheck = inIHelper.getIni("GN", "TrayQRCheck", "", path);
                if (trayQRCheck == "1")
                {
                    TrayQRCheck.Checked = true;
                    Variable.TrayQRCheck = true;
                }
                else
                {
                    TrayQRCheck.Checked = false;
                    Variable.TrayQRCheck = false;
                }

                string checkDownOK = inIHelper.getIni("GN", "CheckDownOK", "", path);
                if (checkDownOK == "1")
                {
                    CheckDownOK.Checked = true;
                    Variable.CheckDownOK = true;
                }
                else
                {
                    CheckDownOK.Checked = false;
                    Variable.CheckDownOK = false;
                }

                //屏蔽单机
                for (int i = 0; i < 40; i++)
                {
                    string hideMachine = inIHelper.getIni("GN", "modelCheck" + (i + 1).ToString(), "", path);
                    if (hideMachine == "1")
                    {
                        Variable.modelCheck[i] = true;
                    }
                    else
                    {
                        Variable.modelCheck[i] = false;
                    }
                }
                QRTime.Text = inIHelper.getIni("PGM", "QRTime", "", path);
                Variable.QRTime = Convert.ToDouble(QRTime.Text);
                DownNGCount.Text = inIHelper.getIni("PGM", "DownNGCount", "", path);
                Variable.DownNGCount = Convert.ToDouble(DownNGCount.Text);
                siteNGSet.Text = inIHelper.getIni("PGM", "siteNGSet", "", path);
                Variable.siteNGSet = Convert.ToDouble(siteNGSet.Text);
                trayCombo.Text = inIHelper.getIni("PGM", "trayCombo", "", path);
                Variable.trayCombo = trayCombo.Text;
                
                //Tray参数  
                TextBox_RowNum.Text = inIHelper.getIni("PGM", "TextBox_RowNum", "", path);
                TextBox_ListNum.Text = inIHelper.getIni("PGM", "TextBox_ListNum", "", path);
                TextBox_RowSpacing.Text = inIHelper.getIni("PGM", "TextBox_RowSpacing", "", path);
                TextBox_ListSpacing.Text = inIHelper.getIni("PGM", "TextBox_ListSpacing", "", path);
                UpABSpacing.Text = inIHelper.getIni("PGM", "UpABSpacing", "", path);
                DownABSpacing.Text = inIHelper.getIni("PGM", "DownABSpacing", "", path);

                Variable.RowNum = Convert.ToDouble(TextBox_RowNum.Text);
                Variable.ListNum = Convert.ToDouble(TextBox_ListNum.Text);
                Variable.RowSpacing = Convert.ToDouble(TextBox_RowSpacing.Text);
                Variable.ListSpacing = Convert.ToDouble(TextBox_ListSpacing.Text);
                Variable.UpABSpacing = Convert.ToDouble(UpABSpacing.Text);
                Variable.DownABSpacing = Convert.ToDouble(DownABSpacing.Text);

                //轴速度设定
                SpeedChoose.Text = inIHelper.getIni("PGM", "SpeedChoose", "", path);
                Variable.SpeedCom = Convert.ToDouble(SpeedChoose.Text);

                //吸嘴延时
                UpAabsorb.Text = inIHelper.getIni("PGM", "UpAabsorb", "", path);
                UpAbroken.Text = inIHelper.getIni("PGM", "UpAbroken", "", path);
                UpBabsorb.Text = inIHelper.getIni("PGM", "UpBabsorb", "", path);
                UpBbroken.Text = inIHelper.getIni("PGM", "UpBbroken", "", path);
                DownAabsorb.Text = inIHelper.getIni("PGM", "DownAabsorb", "", path);
                DownAbroken.Text = inIHelper.getIni("PGM", "DownAbroken", "", path);
                DownBabsorb.Text = inIHelper.getIni("PGM", "DownBabsorb", "", path);
                DownBbroken.Text = inIHelper.getIni("PGM", "DownBbroken", "", path);

                Variable.UpAabsorb = Convert.ToDouble(UpAabsorb.Text);
                Variable.UpAbroken = Convert.ToDouble(UpAbroken.Text);
                Variable.UpBabsorb = Convert.ToDouble(UpBabsorb.Text);
                Variable.UpBbroken = Convert.ToDouble(UpBbroken.Text);
                Variable.DownAabsorb = Convert.ToDouble(DownAabsorb.Text);
                Variable.DownAbroken = Convert.ToDouble(DownAbroken.Text);
                Variable.DownBabsorb = Convert.ToDouble(DownBabsorb.Text);
                Variable.DownBbroken = Convert.ToDouble(DownBbroken.Text);

                //温度设定

                if (Variable.PGMcheck)
                {
                    TempUpLimit.Text = Variable.TempUpLimit.ToString();
                    TempDownLimit.Text = Variable.TempDownLimit.ToString();
                    temper.Text = Variable.temper.ToString();
                    Uptemper.Text = Variable.upTemper.ToString();
                }
                else
                {
                    TempUpLimit.Text = inIHelper.getIni("PGM", "TempUpLimit", "", path);
                    TempDownLimit.Text = inIHelper.getIni("PGM", "TempDownLimit", "", path);
                    temper.Text = inIHelper.getIni("PGM", "temper", "", path);
                    Uptemper.Text = inIHelper.getIni("PGM", "Uptemper", "", path);

                    Variable.TempUpLimit = Convert.ToDouble(TempUpLimit.Text);
                    Variable.TempDownLimit = Convert.ToDouble(TempDownLimit.Text);
                    Variable.temper = Convert.ToDouble(temper.Text);
                    Variable.upTemper = Convert.ToDouble(Uptemper.Text);
                }

                //测试参数
                TestTime.Text = inIHelper.getIni("PGM", "TestTime", "", path);
                TestWaitTime.Text = inIHelper.getIni("PGM", "TestWaitTime", "", path);
                TestOutTime.Text = inIHelper.getIni("PGM", "TestOutTime", "", path);
                VCCTestVol.Text = inIHelper.getIni("PGM", "VCCTestVol", "", path);
                VCQTestVol.Text = inIHelper.getIni("PGM", "VCQTestVol", "", path);
                testYield.Text = inIHelper.getIni("PGM", "testYield", "", path);

                Variable.TestTime = Convert.ToDouble(TestTime.Text);
                Variable.TestWaitTime = Convert.ToDouble(TestWaitTime.Text);
                Variable.TestOutTime = Convert.ToDouble(TestOutTime.Text);
                Variable.VCCTestVol = Convert.ToDouble(VCCTestVol.Text);
                Variable.VCQTestVol = Convert.ToDouble(VCQTestVol.Text);
                Variable.testYield = Convert.ToDouble(testYield.Text);

                string onlineModcheck = inIHelper.getIni("PGM", "OnlineModcheck", "", path);
                if (onlineModcheck == "1")
                {
                    OnlineModcheck.Checked = true;
                    Variable.OnlineModcheck = true;
                }
                else
                {
                    OnlineModcheck.Checked = false;
                    Variable.OnlineModcheck = false;
                }

                string vCQcheck = inIHelper.getIni("PGM", "VCQcheck", "", path);
                if (vCQcheck == "1")
                {
                    VCQcheck.Checked = true;
                    Variable.VCQcheck = true;
                }
                else
                {
                    VCQcheck.Checked = false;
                    Variable.VCQcheck = false;
                }
            }
            Variable.LoadparameterFlag = true;
        }
        #endregion

        #region 加载点位方法

        public void LoadPoint(string path)
        {
            lock (threadLock)
            {
                for (int i = 0; i < 16; i++)
                {
                    Variable.AxisStartVel[i + 1] = Convert.ToDouble(inIHelper.getIni("PGM", "AxisStartVel" + (i + 1).ToString(), "", path));
                    Variable.AxisRunVel[i + 1] = Convert.ToDouble(inIHelper.getIni("PGM", "AxisRunVel" + (i + 1).ToString(), "", path));
                    Variable.AxisHmoeVelHight[i + 1] = Convert.ToDouble(inIHelper.getIni("PGM", "AxisHmoeVelHight" + (i + 1).ToString(), "", path));
                    Variable.AxisHmoeVelLow[i + 1] = Convert.ToDouble(inIHelper.getIni("PGM", "AxisHmoeVelLow" + (i + 1).ToString(), "", path));
                    Variable.AxisTacc[i + 1] = Convert.ToDouble(inIHelper.getIni("PGM", "AxisTacc" + (i + 1).ToString(), "", path));
                    Variable.AxisTdec[i + 1] = Convert.ToDouble(inIHelper.getIni("PGM", "AxisTdec" + (i + 1).ToString(), "", path));

                }

                //补偿信息
                for (int i = 0; i < 32; i++)
                {
                    Variable.offset[i + 1] = Convert.ToDouble(inIHelper.getIni("PGM", "Offset" + i.ToString(), "", path));
                }

                //加载点位参数
                for (int i = 0; i < 16; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        Variable.AxisPos[i + 1, j] = Convert.ToDouble(inIHelper.getIni("PGM", "AxisPos" + (i + 1).ToString() + "/" + j.ToString(), "", path));
                    }
                }

                string pGMcheck = inIHelper.getIni("PGM", "PGMcheck", "", path);
                if (pGMcheck == "1")
                {
                    PGMcheck.Checked = true;
                    Variable.PGMcheck = true;
                }
                else
                {
                    PGMcheck.Checked = false;
                    Variable.PGMcheck = false;
                }

                texttempSetDelay.Text = inIHelper.getIni("PGM", "texttempSetDelay", "", path);
                Variable.tempSetDelay = Convert.ToDouble(texttempSetDelay.Text);

                airDelay.Text = inIHelper.getIni("PGM", "airDelay", "", path);
                Variable.airSetDelay = Convert.ToInt32(airDelay.Text);

                photoDelay.Text = inIHelper.getIni("PGM", "photoDelay", "", path);
                Variable.photoDelay = Convert.ToInt32(photoDelay.Text);
            }
        }

        #endregion


        #region 保存参数

        /// <summary>
        /// 保存参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
            SaveFileDialog1.InitialDirectory = "D:/";
            SaveFileDialog1.Filter = "保存参数(*.bin)|*.bin";
            SaveFileDialog1.FilterIndex = 1;
            SaveFileDialog1.RestoreDirectory = true;
            SaveFileDialog1.FileName = "";
            SaveFileDialog1.ShowDialog();
            try
            {
                if (SaveFileDialog1.FileName == "")
                {
                    MessageBox.Show("保存文件名不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    bool b = TextEmpty();
                    if (!b)
                    {
                        string path = SaveFileDialog1.FileName;//保存文件地址名称
                        string[] split = path.Split(new Char[] { '\\' });
                        Variable.FileName = split[split.Length - 1];//档案名称                    
                        SaveParameter(path);

                        //保存点位
                        path = Application.StartupPath + "\\Point.ini";
                        SavePoint(path);
                        //加载点位
                        LoadPoint(path);

                        //保存参数
                        path = Application.StartupPath + "\\parameter.ini";
                        SaveParameter(path);
                        //加载参数
                        LoadParameter(path);
                        VariableToView();

                        Variable.saveFlag = true;

                        // 屏蔽机台
                        //Variable.ModelState  10个模组状态 => 0:空，1:已放料，2:测试中，3:测试OK，10:屏蔽
                        for (int i = 0; i < 40; i++)
                        {
                            if ((Variable.ModelState[i] == 0 || Variable.ModelState[i] == 10) && Variable.modelCheck[i])
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

                        MessageBox.Show("参数保存成功！");
                    }
                    else
                    {
                        MessageBox.Show("文本框不能为空,请输入0");
                    }
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show("保存参数失败，异常信息如下：" + EX.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
        }

        #endregion

        #region 保存参数方法
        public void SaveParameter(string path)
        {
            Variable.parameterSaveFlag = false;
            inIHelper.writeIni("PGM", "FileName", Variable.FileName, path);
            //功能参数
            if (FistPower.Checked == true)
            {
                inIHelper.writeIni("GN", "FistPower", "1", path);
            }
            else
            {
                inIHelper.writeIni("GN", "FistPower", "0", path);
            }

            if (UpDoor_Check.Checked == true)
            {
                inIHelper.writeIni("GN", "UpDoor_Check", "1", path);
            }
            else
            {
                inIHelper.writeIni("GN", "UpDoor_Check", "0", path);
            }

            if (ModelDoor_Check.Checked == true)
            {
                inIHelper.writeIni("GN", "ModelDoor_Check", "1", path);
            }
            else
            {
                inIHelper.writeIni("GN", "ModelDoor_Check", "0", path);
            }

            if (DownDoor_Check.Checked == true)
            {
                inIHelper.writeIni("GN", "DownDoor_Check", "1", path);
            }
            else
            {
                inIHelper.writeIni("GN", "DownDoor_Check", "0", path);
            }

            if (HotModcheckBox.Checked == true)
            {
                inIHelper.writeIni("GN", "HotModcheckBox", "1", path);
            }
            else
            {
                inIHelper.writeIni("GN", "HotModcheckBox", "0", path);
            }

            if (PhotocheckBox.Checked == true)
            {
                inIHelper.writeIni("GN", "PhotocheckBox", "1", path);
            }
            else
            {
                inIHelper.writeIni("GN", "PhotocheckBox", "0", path);
            }

            if (siteShieldCheck.Checked == true)
            {
                inIHelper.writeIni("GN", "siteShieldCheck", "1", path);
            }
            else
            {
                inIHelper.writeIni("GN", "siteShieldCheck", "0", path);
            }

            if (MEScheck.Checked == true)
            {
                inIHelper.writeIni("GN", "MEScheck", "1", path);
            }
            else
            {
                inIHelper.writeIni("GN", "MEScheck", "0", path);
            }

            if (TrayQRCheck.Checked == true)
            {
                inIHelper.writeIni("GN", "TrayQRCheck", "1", path);
            }
            else
            {
                inIHelper.writeIni("GN", "TrayQRCheck", "0", path);
            }

            if (CheckDownOK.Checked == true)
            {
                inIHelper.writeIni("GN", "CheckDownOK", "1", path);
            }
            else
            {
                inIHelper.writeIni("GN", "CheckDownOK", "0", path);
            }

            //屏蔽单机
            for (int i = 0; i < 40; i++)
            {
                if (checkBox[i].Checked)
                {
                    inIHelper.writeIni("GN", "modelCheck" + (i + 1).ToString(), "1", path);
                }
                else
                {
                    inIHelper.writeIni("GN", "modelCheck" + (i + 1).ToString(), "0", path);
                }
            }

            inIHelper.writeIni("PGM", "QRTime", QRTime.Text, path);
            inIHelper.writeIni("PGM", "DownNGCount", DownNGCount.Text, path);
            inIHelper.writeIni("PGM", "siteNGSet", siteNGSet.Text, path);
            inIHelper.writeIni("PGM", "trayCombo", trayCombo.Text, path);

            //Tray参数
            inIHelper.writeIni("PGM", "TextBox_RowNum", TextBox_RowNum.Text, path);
            inIHelper.writeIni("PGM", "TextBox_ListNum", TextBox_ListNum.Text, path);
            inIHelper.writeIni("PGM", "TextBox_RowSpacing", TextBox_RowSpacing.Text, path);
            inIHelper.writeIni("PGM", "TextBox_ListSpacing", TextBox_ListSpacing.Text, path);
            inIHelper.writeIni("PGM", "UpABSpacing", UpABSpacing.Text, path);
            inIHelper.writeIni("PGM", "DownABSpacing", DownABSpacing.Text, path);

            //轴速度设定
            inIHelper.writeIni("PGM", "SpeedChoose", SpeedChoose.Text, path);

            //吸嘴延时
            inIHelper.writeIni("PGM", "UpAabsorb", UpAabsorb.Text, path);
            inIHelper.writeIni("PGM", "UpAbroken", UpAbroken.Text, path);
            inIHelper.writeIni("PGM", "UpBabsorb", UpBabsorb.Text, path);
            inIHelper.writeIni("PGM", "UpBbroken", UpBbroken.Text, path);
            inIHelper.writeIni("PGM", "DownAabsorb", DownAabsorb.Text, path);
            inIHelper.writeIni("PGM", "DownAbroken", DownAbroken.Text, path);
            inIHelper.writeIni("PGM", "DownBabsorb", DownBabsorb.Text, path);
            inIHelper.writeIni("PGM", "DownBbroken", DownBbroken.Text, path);

            //温度设定
            inIHelper.writeIni("PGM", "TempUpLimit", TempUpLimit.Text, path);
            inIHelper.writeIni("PGM", "TempDownLimit", TempDownLimit.Text, path);
            inIHelper.writeIni("PGM", "temper", temper.Text, path);
            inIHelper.writeIni("PGM", "Uptemper", Uptemper.Text, path);

            //测试参数
            inIHelper.writeIni("PGM", "TestTime", TestTime.Text, path);
            inIHelper.writeIni("PGM", "TestWaitTime", TestWaitTime.Text, path);
            inIHelper.writeIni("PGM", "TestOutTime", TestOutTime.Text, path);
            inIHelper.writeIni("PGM", "VCCTestVol", VCCTestVol.Text, path);
            inIHelper.writeIni("PGM", "VCQTestVol", VCQTestVol.Text, path);
            inIHelper.writeIni("PGM", "testYield", testYield.Text, path);
            if (OnlineModcheck.Checked)
            {
                inIHelper.writeIni("PGM", "OnlineModcheck", "1", path);
            }
            else
            {
                inIHelper.writeIni("PGM", "OnlineModcheck", "0", path);
            }
            if (VCQcheck.Checked)
            {
                inIHelper.writeIni("PGM", "VCQcheck", "1", path);
            }
            else
            {
                inIHelper.writeIni("PGM", "VCQcheck", "0", path);
            }

            Variable.parameterSaveFlag = true;
        }

        #endregion

        #region 保存点位方法
        public void SavePoint(string path)
        {
            for (int i = 0; i < 16; i++)
            {
                if (i < 16)
                {
                    Variable.AxisStartVel[i + 1] = Convert.ToDouble(dataGridAxis1.Rows[i].Cells[2].Value);
                    Variable.AxisRunVel[i + 1] = Convert.ToDouble(dataGridAxis1.Rows[i].Cells[3].Value);
                    Variable.AxisHmoeVelHight[i + 1] = Convert.ToDouble(dataGridAxis1.Rows[i].Cells[4].Value);
                    Variable.AxisHmoeVelLow[i + 1] = Convert.ToDouble(dataGridAxis1.Rows[i].Cells[5].Value);
                    Variable.AxisTacc[i + 1] = Convert.ToDouble(dataGridAxis1.Rows[i].Cells[6].Value);
                    Variable.AxisTdec[i + 1] = Convert.ToDouble(dataGridAxis1.Rows[i].Cells[7].Value);
                }
                else
                {
                    Variable.AxisStartVel[i + 1] = Convert.ToDouble(dataGridAxis2.Rows[i - 16].Cells[2].Value);
                    Variable.AxisRunVel[i + 1] = Convert.ToDouble(dataGridAxis2.Rows[i - 16].Cells[3].Value);
                    Variable.AxisHmoeVelHight[i + 1] = Convert.ToDouble(dataGridAxis2.Rows[i - 16].Cells[4].Value);
                    Variable.AxisHmoeVelLow[i + 1] = Convert.ToDouble(dataGridAxis2.Rows[i - 16].Cells[5].Value);
                    Variable.AxisTacc[i + 1] = Convert.ToDouble(dataGridAxis2.Rows[i - 16].Cells[6].Value);
                    Variable.AxisTdec[i + 1] = Convert.ToDouble(dataGridAxis2.Rows[i - 16].Cells[7].Value);
                }
            }

            for (int i = 0; i < 16; i++)
            {
                inIHelper.writeIni("PGM", "AxisStartVel" + (i + 1).ToString(), Variable.AxisStartVel[i + 1].ToString(), path);
                inIHelper.writeIni("PGM", "AxisRunVel" + (i + 1).ToString(), Variable.AxisRunVel[i + 1].ToString(), path);
                inIHelper.writeIni("PGM", "AxisHmoeVelHight" + (i + 1).ToString(), Variable.AxisHmoeVelHight[i + 1].ToString(), path);
                inIHelper.writeIni("PGM", "AxisHmoeVelLow" + (i + 1).ToString(), Variable.AxisHmoeVelLow[i + 1].ToString(), path);
                inIHelper.writeIni("PGM", "AxisTacc" + (i + 1).ToString(), Variable.AxisTacc[i + 1].ToString(), path);
                inIHelper.writeIni("PGM", "AxisTdec" + (i + 1).ToString(), Variable.AxisTdec[i + 1].ToString(), path);

            }

            //补偿信息
            for (int i = 0; i < 32; i++)
            {
                inIHelper.writeIni("PGM", "Offset" + i.ToString(), txtbox[i].Text.Trim(), path);
            }

            //保存点位参数
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    inIHelper.writeIni("PGM", "AxisPos" + (i + 1).ToString() + "/" + j.ToString(), Variable.AxisPos[i + 1, j].ToString(), path);
                }
            }

            if (PGMcheck.Checked)
            {
                inIHelper.writeIni("PGM", "PGMcheck", "1", path);
            }
            else
            {
                inIHelper.writeIni("PGM", "PGMcheck", "0", path);
            }

            inIHelper.writeIni("PGM", "airDelay", airDelay.Text, path);
            inIHelper.writeIni("PGM", "texttempSetDelay", texttempSetDelay.Text, path);
            inIHelper.writeIni("PGM", "photoDelay", photoDelay.Text, path);
        }


        #endregion

        #region 变量值传给界面
        public void VariableToView()
        {
            //轴参数
            for (int i = 0; i < 16; i++)
            {
                dataGridAxis1.Rows[i].Cells[2].Value = Variable.AxisStartVel[i + 1];
                dataGridAxis1.Rows[i].Cells[3].Value = Variable.AxisRunVel[i + 1];
                dataGridAxis1.Rows[i].Cells[4].Value = Variable.AxisHmoeVelHight[i + 1];
                dataGridAxis1.Rows[i].Cells[5].Value = Variable.AxisHmoeVelLow[i + 1];
                dataGridAxis1.Rows[i].Cells[6].Value = Variable.AxisTacc[i + 1];
                dataGridAxis1.Rows[i].Cells[7].Value = Variable.AxisTdec[i + 1];
            }

            //补偿信息
            for (int i = 0; i < 32; i++)
            {
                txtbox[i].Text = Variable.offset[i + 1].ToString();
            }

            //屏蔽单机
            for (int i = 0; i < 40; i++)
            {
                if (Variable.modelCheck[i])
                {
                    checkBox[i].Checked = true;
                }
                else
                {
                    checkBox[i].Checked = false;
                }
            }

        }
        #endregion

        #region 温度设定
        private void btnTemper_Click(object sender, EventArgs e)
        {
            Variable.temWriteFlag = true;
            Thread.Sleep(100);
            //地址:01,02,03,04,05,06,07,08,09,0A,0B,0C,0D,0E,0F,10,11,12,13.14
            //TempWrite("01", Convert.ToInt16(temper.Value * 10));
            for (int i = 0; i < 20; i++)
            {
                TempWrite1((i + 1).ToString("X2"), Convert.ToInt16(temper.Value * 10));
                Thread.Sleep(200);
                TempWrite2((i + 1).ToString("X2"), Convert.ToInt16(temper.Value * 10));
                Thread.Sleep(200);
            }
            Variable.temWriteFlag = false;
        }
        #endregion

        #region 最高温度设定
        private void btnUpTemper_Click(object sender, EventArgs e)
        {
            Variable.temWriteFlag = true;
            Thread.Sleep(100);
            //地址:01,02,03,04,05,06,07,08,09,0A,0B,0C,0D,0E,0F,10,11,12,13.14
            //UpTempWrite("01", Convert.ToInt16(Uptemper.Value * 10));
            for (int i = 0; i < 20; i++)
            {
                UpTempWrite1((i + 1).ToString("X2"), Convert.ToInt16(Uptemper.Value * 10));
                Thread.Sleep(200);
                UpTempWrite2((i + 1).ToString("X2"), Convert.ToInt16(Uptemper.Value * 10));
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

        #endregion

        //******探针寿命******

        #region 加载参数

        /// <summary>
        /// 加载参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LoadParameter()
        {
            string Path = Application.StartupPath + "\\Preserve.ini";
            LoadPoint1(Path);
        }

        #endregion

        #region 加载点位方法
        public void LoadPoint1(string path)
        {
            lock (threadLock)
            {
                for (int i = 0; i < 40; i++)
                {
                    dataGridView2.Rows[i].Cells[1].Value = Convert.ToDouble(inIHelper.getIni("PGM", (i + 1).ToString(), "", path));
                    Variable.ProbeNum[i] = Convert.ToDouble(dataGridView2.Rows[i].Cells[1].Value);
                    dataGridView2.Rows[i].Cells[2].Value = Convert.ToDouble(inIHelper.getIni("PGM", (i + 101).ToString(), "", path));
                    Variable.ProbeSet[i] = Convert.ToDouble(dataGridView2.Rows[i].Cells[2].Value);
                }
            }
        }

        #endregion

        #region 保存参数

        /// <summary>
        /// 保存参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SaveParameter()
        {
            string path = Application.StartupPath + "\\Preserve.ini";
            SavePoint1(path);
        }

        #endregion

        #region 保存点位方法
        public void SaveSite(int num, string path)
        {
            lock (threadLock)
            {
                inIHelper.writeIni("PGM", (num + 1).ToString(), Variable.ProbeNum[num].ToString(), path);
            }
        }
        public void SavePoint1(string path)
        {
            lock (threadLock)
            {
                for (int i = 0; i < 40; i++)
                {
                    inIHelper.writeIni("PGM", (i + 1).ToString(), Variable.ProbeNum[i].ToString(), path);
                    inIHelper.writeIni("PGM", (i + 101).ToString(), dataGridView2.Rows[i].Cells[2].Value.ToString(), path);
                }
            }
        }

        #endregion

        #region 保存
        private void btnSave1_Click(object sender, EventArgs e)
        {
            SaveParameter();
            LoadParameter();
        }
        #endregion

        #region 清除
        private void btnClear_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows[row1].Cells[1].Value = "0";
            Variable.ProbeNum[row1] = 0;
            SaveParameter();
        }
        #endregion

        #region 全部清空
        private void btnAllClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 40; i++)
            {
                dataGridView2.Rows[i].Cells[1].Value = "0";
                Variable.ProbeNum[i] = 0;
                SaveParameter();
            }
        }
        #endregion

        //******Tray盘维护******

        #region 数据库搜索
        public void SearchData()
        {
            try
            {
                string CONN = Access.GetSqlConnectionString1();
                OleDbConnection conn = new OleDbConnection(CONN);
                conn.Open();
                string cmdText = "select * from QRRecord";
                OleDbDataAdapter sda = new OleDbDataAdapter(cmdText, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                QRGridView.DataSource = ds.Tables[0];
                dataGrid.AutoSizeColumn(QRGridView);
                QRGridView.Columns[0].Width = 50;
                QRGridView.Columns[1].Width = 50;
                QRGridView.RowHeadersVisible = false;//行头隐藏
                dataGrid.DataGridViewchangeColor(QRGridView, Color.White, Color.Blue);
                dataGrid.setColorColum(QRGridView, 0);
                conn.Close();
            }
            catch
            {
                MessageBox.Show("无资料查询!", "提示:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            QRRecord();
        }
        #endregion

        #region 新增
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string CONN = Access.GetSqlConnectionString1();
                using (OleDbConnection conn = new OleDbConnection(CONN))
                {
                    conn.Open();
                    using (OleDbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO QRRecord(Num,QR)" +
    "values(@s1,@s2)";
                        DataTable dt = QRGridView.DataSource as DataTable;
                        cmd.Parameters.AddWithValue("@s1", dt.Rows[dt.Rows.Count - 1][1]);
                        cmd.Parameters.AddWithValue("@s2", dt.Rows[dt.Rows.Count - 1][2]);
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            catch
            {
                MessageBox.Show("无资料查询!", "提示:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            SearchData();
            QRGridView.Columns[0].Visible = false;
        }
        #endregion

        #region 更新
        private void btnUpdata_Click(object sender, EventArgs e)
        {
            try
            {
                string CONN = Access.GetSqlConnectionString1();
                using (OleDbConnection conn = new OleDbConnection(CONN))
                {
                    conn.Open();
                    using (OleDbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "update QRRecord set Num=@s1,QR=@s2 where ID = @sv1";
                        DataTable dt = QRGridView.DataSource as DataTable;
                        cmd.Parameters.AddWithValue("s1", dt.Rows[row][1]);
                        cmd.Parameters.AddWithValue("s2", dt.Rows[row][2]);
                        if (col == 0)
                        {
                            cmd.Parameters.AddWithValue("sv1", dt.Rows[row][col]);
                        }
                        else if (col == 1)
                        {
                            cmd.Parameters.AddWithValue("sv1", dt.Rows[row][col - 1]);
                        }
                        else if (col == 2)
                        {
                            cmd.Parameters.AddWithValue("sv1", dt.Rows[row][col - 2]);
                        }
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            catch
            {
                MessageBox.Show("无资料查询!", "提示:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            SearchData();
            QRGridView.Columns[0].Visible = false;
        }
        #endregion

        #region 删除
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string CONN = Access.GetSqlConnectionString1();
                using (OleDbConnection conn = new OleDbConnection(CONN))
                {
                    conn.Open();
                    using (OleDbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "delete from QRRecord where ID = @s1";
                        DataTable dt = QRGridView.DataSource as DataTable;
                        if (col == 0)
                        {
                            cmd.Parameters.AddWithValue("s1", dt.Rows[row][col]);
                        }
                        else if (col == 1)
                        {
                            cmd.Parameters.AddWithValue("s1", dt.Rows[row][col - 1]);
                        }
                        else if (col == 2)
                        {
                            cmd.Parameters.AddWithValue("s1", dt.Rows[row][col - 2]);
                        }
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            catch
            {
                MessageBox.Show("无资料查询!", "提示:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            SearchData();
            QRGridView.Columns[0].Visible = false;
        }
        #endregion

        #region QR
        private void QRGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //获取行列坐标索引
            //方法一：
            row = e.RowIndex;
            col = e.ColumnIndex;
        }

        public void QRRecord()
        {
            Variable.qrRecord.Clear();
            for (int i = 0; i < QRGridView.Rows.Count - 1; i++)
            {
                Variable.qrRecord.Add(QRGridView.Rows[i].Cells[2].Value.ToString());
            }
        }
        #endregion

        //******OP盘维护******

        #region 数据库搜索
        public void SearchOPData()
        {
            try
            {
                string CONN = Access.GetSqlConnectionString();
                OleDbConnection conn = new OleDbConnection(CONN);
                conn.Open();
                string cmdText = "select * from OPRecord";
                OleDbDataAdapter sda = new OleDbDataAdapter(cmdText, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                OPGridView.DataSource = ds.Tables[0];
                //dataGrid.AutoSizeColumn(OPGridView);
                OPGridView.RowHeadersVisible = false;//行头隐藏
                dataGrid.DataGridViewchangeColor(OPGridView, Color.White, Color.Blue);
                dataGrid.setColorColum(OPGridView, 0);
                conn.Close();
            }
            catch
            {
                MessageBox.Show("无资料查询!", "提示:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            SearchOPNumber();
        }
        #endregion

        #region 新增
        private void btnOPAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(OPNumber.Text) && !string.IsNullOrEmpty(OPPassword.Text))
            {
                //写入ACCESS文件里面
                try
                {
                    string CONN = Access.GetSqlConnectionString();
                    using (OleDbConnection conn = new OleDbConnection(CONN))
                    {
                        conn.Open();
                        using (OleDbCommand cmd = conn.CreateCommand())
                        {

                            cmd.CommandText = "INSERT INTO OPRecord(Num,OPNumber,OPPassword)" +
        "values(@s1,@s2,@s3)";
                            DataTable dt = OPGridView.DataSource as DataTable;
                            cmd.Parameters.AddWithValue("@s1", (Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][1]) + 1).ToString());
                            cmd.Parameters.AddWithValue("@s2", OPNumber.Text);
                            cmd.Parameters.AddWithValue("@s3", OPPassword.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
            else
            {
                MessageBox.Show("OP账号和密码不能为空");
            }
            SearchOPData();
            OPGridView.Columns[0].Visible = false;
        }
        #endregion

        #region 更新
        private void btnOPUpdata_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(OPNumber.Text) && !string.IsNullOrEmpty(OPPassword.Text))
            {
                try
                {
                    string CONN = Access.GetSqlConnectionString();
                    using (OleDbConnection conn = new OleDbConnection(CONN))
                    {
                        conn.Open();
                        using (OleDbCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "update OPRecord set Num=@s1,OPNumber=@s2,OPPassword=@s3 where ID = @sv1";
                            DataTable dt = OPGridView.DataSource as DataTable;
                            cmd.Parameters.AddWithValue("s1", dt.Rows[row][1]);
                            cmd.Parameters.AddWithValue("s2", OPNumber.Text);
                            cmd.Parameters.AddWithValue("s2", OPPassword.Text);
                            if (col == 0)
                            {
                                cmd.Parameters.AddWithValue("sv1", dt.Rows[row][col]);
                            }
                            else if (col == 1)
                            {
                                cmd.Parameters.AddWithValue("sv1", dt.Rows[row][col - 1]);
                            }
                            else if (col == 2)
                            {
                                cmd.Parameters.AddWithValue("sv1", dt.Rows[row][col - 2]);
                            }
                            else if (col == 3)
                            {
                                cmd.Parameters.AddWithValue("sv1", dt.Rows[row][col - 3]);
                            }
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch
                {

                }
            }
            else
            {
                MessageBox.Show("OP账号和密码不能为空");
            }

            SearchOPData();
            OPGridView.Columns[0].Visible = false;
        }
        #endregion

        #region 删除
        private void btnOPDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string CONN = Access.GetSqlConnectionString();
                using (OleDbConnection conn = new OleDbConnection(CONN))
                {
                    conn.Open();
                    using (OleDbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "delete from OPRecord where ID = @s1";
                        DataTable dt = OPGridView.DataSource as DataTable;
                        if (col == 0)
                        {
                            cmd.Parameters.AddWithValue("s1", dt.Rows[row][col]);
                        }
                        else if (col == 1)
                        {
                            cmd.Parameters.AddWithValue("s1", dt.Rows[row][col - 1]);
                        }
                        else if (col == 2)
                        {
                            cmd.Parameters.AddWithValue("s1", dt.Rows[row][col - 2]);
                        }
                        else if (col == 3)
                        {
                            cmd.Parameters.AddWithValue("s1", dt.Rows[row][col - 3]);
                        }
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

            }
            SearchOPData();
            OPGridView.Columns[0].Visible = false;
        }
        #endregion

        #region dataGridView1单元格单击
        private void OPGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //获取行列坐标索引
            //方法一：
            row = e.RowIndex;
            col = e.ColumnIndex;

            DataTable dt = OPGridView.DataSource as DataTable;
            OPNumber.Text = dt.Rows[row][2].ToString();
            OPPassword.Text = dt.Rows[row][3].ToString();
        }

        public void SearchOPNumber()
        {
            Variable.OPNumber.Clear();
            Variable.OPPassword.Clear();
            for (int i = 0; i < OPGridView.Rows.Count; i++)
            {
                Variable.OPNumber.Add(OPGridView.Rows[i].Cells[2].Value.ToString());
                Variable.OPPassword.Add(OPGridView.Rows[i].Cells[3].Value.ToString());
            }
        }
        #endregion

        #region 模组选择
        private void ModelCheck_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 40; i++)
            {
                if (Variable.ModelState[i] == 0 && checkBox[i].Checked)
                {
                    checkBox[i].BackColor = Color.LightBlue;
                }
                else if (Variable.ModelState[i] == 1 && checkBox[i].Checked)
                {
                    checkBox[i].BackColor = Color.Blue;
                }
                else if (Variable.ModelState[i] == 2 && checkBox[i].Checked)
                {
                    checkBox[i].BackColor = Color.Gold;
                }
                else if (Variable.ModelState[i] == 3 && checkBox[i].Checked)
                {
                    checkBox[i].BackColor = Color.Green;
                }
                else if (Variable.ModelState[i] == 10 || !checkBox[i].Checked)
                {
                    checkBox[i].BackColor = Color.Pink;
                }
            }
        }
        #endregion

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            row1 = e.RowIndex;
            col1 = e.ColumnIndex;
        }

        private void OPGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 把第4列显示*号，*号的个数和实际数据的长度相同
            if (e.ColumnIndex == 3)
            {
                if (e.Value != null && e.Value.ToString().Length > 0)
                {
                    e.Value = new string('*', e.Value.ToString().Length);
                }
            }
        }

        private void UpDoor_Check_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
