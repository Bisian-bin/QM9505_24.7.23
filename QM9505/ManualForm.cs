using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QM9505
{
    public partial class ManualForm : Form
    {
        INIHelper inIHelper = new INIHelper();
        DataGrid dataGrid = new DataGrid();
        Motion motion = new Motion();
        ParameterForm parameterForm = new ParameterForm();
        ComboBox[] comboxPoint = new ComboBox[16];
        public string comStatusPoint;
        ComboBox[] comboxVel = new ComboBox[16];
        public string comStatusVel;
        public Thread PointRefresh;
        public int formNum = 0;

        public ManualForm()
        {
            InitializeComponent();
            tabControl1.SelectedIndex = 1;
            tabPage2.Parent = null;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            formNum += 1;
            if (formNum > 1)
            {
                Variable.ManualViewFlag = false;
                if (PointRefresh != null)
                {
                    PointRefresh.Abort();
                    PointRefresh = null;
                }
            }

            base.OnVisibleChanged(e);
            if (!IsHandleCreated)
            {
                this.Close();
            }
        }

        #region 窗体加载
        private void ManualForm_Load(object sender, EventArgs e)
        {
            //初始化DataGridView
            dataGrid.NotChangeListRow(dataGridAxis1);
            DataGridViewInit(dataGridAxis1, 0);
            AddControl_Parameter(dataGridAxis1, 0);
            ReadAxis(dataGridAxis1, "AxisNum");
            ReadAxisVel(dataGridAxis1, "AxisVel");
            ReadAxisPoint(dataGridAxis1, "AxisPoint");

            for (int i = 0; i < 16; i++)
            {
                comStatusPoint = "AxisPoint" + (i + 1).ToString().PadLeft(2, '0');
                comboxPoint[i] = (ComboBox)(this.Controls.Find(comStatusPoint, true)[0]);

                comStatusVel = "AxisVel" + (i + 1).ToString().PadLeft(2, '0');
                comboxVel[i] = (ComboBox)(this.Controls.Find(comStatusVel, true)[0]);
            }

            dataGridAxis1.RowsDefaultCellStyle.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //dataGridAxis.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            tabControl1.SelectedIndex = 0;

            //加载参数
            parameterForm.LoadPoint(Application.StartupPath + "\\Point.ini");

            foreach (Control c in dataGridAxis1.Controls)
            {
                if (c.Name.Contains("Enable"))
                {
                    int picStatus;              //读取轴状态
                    uint pClock;                //时钟信号
                    string index = c.Name.Substring(6, c.Name.Length - 6);
                    short axis = (short)(Convert.ToUInt16(index) - 0);

                    if (axis < 13)
                    {
                        axis = (short)(Convert.ToUInt16(index) - 0);
                        mc.GTN_GetSts(1, axis, out picStatus, 1, out pClock);
                        if ((picStatus & 0x0200) == 0)//判断第9位是否为1
                        {
                            c.BackColor = Color.LightGray;
                        }
                        else
                        {
                            c.BackColor = Color.Green;
                        }
                    }
                    else
                    {
                        axis = (short)(Convert.ToUInt16(index) - 12);
                        mc.GTN_GetSts(2, axis, out picStatus, 1, out pClock);
                        if ((picStatus & 0x0200) == 0)//判断第9位是否为1
                        {
                            c.BackColor = Color.LightGray;
                        }
                        else
                        {
                            c.BackColor = Color.Green;
                        }
                    }
                }
            }

            PointRefresh = new Thread(PointRsh);//开始后，开新线程执行此方法
            PointRefresh.IsBackground = true;
            PointRefresh.Start();

            Variable.ManualViewFlag = true;
        }

        #endregion

        #region 初始化DataGridView
        public void DataGridViewInit(DataGridView dataGridView, int num)
        {
            //添加列
            dataGridView.Columns.Add((1 + num).ToString(), "轴号");
            dataGridView.Columns.Add((2 + num).ToString(), "轴名称");
            dataGridView.Columns.Add((3 + num).ToString(), "移动速度");
            dataGridView.Columns.Add((4 + num).ToString(), "当前坐标");
            dataGridView.Columns.Add((5 + num).ToString(), "点位选择");
            dataGridView.Columns.Add((6 + num).ToString(), "点位值");
            dataGridView.Columns.Add((7 + num).ToString(), "使能");
            dataGridView.Columns.Add((8 + num).ToString(), "回零");
            dataGridView.Columns.Add((9 + num).ToString(), "JOG+");
            dataGridView.Columns.Add((10 + num).ToString(), "JOG-");
            dataGridView.Columns.Add((11 + num).ToString(), "移动到点位");
            dataGridView.Columns.Add((12 + num).ToString(), "点位保存");

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
                else if (i == 2)
                {
                    dataGridView.Columns[i].Width = 90;
                }
                else if (i == 4)
                {
                    dataGridView.Columns[i].Width = 300;
                }
                else
                {
                    dataGridView.Columns[i].Width = 140;
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

            //移动速度
            ComboBox[] combVel = new ComboBox[16];

            for (int i = 0; i < 16; i++)
            {
                combVel[i] = new ComboBox();
                combVel[i].Name = "AxisVel" + (i + 1 + num).ToString().PadLeft(2, '0');
                combVel[i].Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                combVel[i].AutoSize = false;
                combVel[i].BackColor = Color.LightSteelBlue;
                dataGridView.Controls.Add(combVel[i]);
                Rectangle rect = dataGridView.GetCellDisplayRectangle(2, i, false);
                combVel[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                combVel[i].Location = new Point(rect.Left + 3, rect.Top + 3);

            }

            //点位选择
            ComboBox[] combPoint = new ComboBox[16];

            for (int i = 0; i < 16; i++)
            {
                combPoint[i] = new ComboBox();
                combPoint[i].Name = "AxisPoint" + (i + 1 + num).ToString().PadLeft(2, '0');
                combPoint[i].Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                combPoint[i].AutoSize = false;
                combPoint[i].BackColor = Color.LightSteelBlue;
                dataGridView.Controls.Add(combPoint[i]);
                Rectangle rect = dataGridView.GetCellDisplayRectangle(4, i, false);
                combPoint[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                combPoint[i].Location = new Point(rect.Left + 3, rect.Top + 3);

            }

            //使能按钮
            Button[] btn1 = new Button[16];
            for (int i = 0; i < 16; i++)
            {
                btn1[i] = new Button();
                btn1[i].Name = "Enable" + (i + 1 + num).ToString().PadLeft(2, '0');
                btn1[i].Text = "使能";
                btn1[i].AutoSize = false;
                btn1[i].TextAlign = ContentAlignment.MiddleCenter;
                btn1[i].BackColor = Color.LightGray;
                dataGridView.Controls.Add(btn1[i]);
                Rectangle rect = dataGridView.GetCellDisplayRectangle(6, i, false);
                btn1[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                btn1[i].Location = new Point(rect.Left + 3, rect.Top + 3);
                btn1[i].Click += new EventHandler(EnableBtn_Click);

            }

            //回零按钮
            Button[] btn2 = new Button[16];
            for (int i = 0; i < 16; i++)
            {
                btn2[i] = new Button();
                btn2[i].Name = "Home" + (i + 1 + num).ToString().PadLeft(2, '0');
                btn2[i].Text = "回零";
                btn2[i].AutoSize = false;
                btn2[i].TextAlign = ContentAlignment.MiddleCenter;
                btn2[i].BackColor = Color.LightGray;
                dataGridView.Controls.Add(btn2[i]);
                Rectangle rect = dataGridView.GetCellDisplayRectangle(7, i, false);
                btn2[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                btn2[i].Location = new Point(rect.Left + 3, rect.Top + 3);
                btn2[i].Click += new EventHandler(HomeBtn_Click);

            }

            //JOG+
            Button[] btn3 = new Button[16];
            for (int i = 0; i < 16; i++)
            {
                btn3[i] = new Button();
                btn3[i].Name = "JOGA" + (i + 1 + num).ToString().PadLeft(2, '0');
                btn3[i].Text = "JOG+";
                btn3[i].AutoSize = false;
                btn3[i].TextAlign = ContentAlignment.MiddleCenter;
                btn3[i].BackColor = Color.LightGray;
                dataGridView.Controls.Add(btn3[i]);
                Rectangle rect = dataGridView.GetCellDisplayRectangle(8, i, false);
                btn3[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                btn3[i].Location = new Point(rect.Left + 3, rect.Top + 3);
                btn3[i].MouseUp += new MouseEventHandler(JogBtnA_MouseUp);
                btn3[i].MouseDown += new MouseEventHandler(JogBtnA_MouseDown);

            }

            //JOG-
            Button[] btn4 = new Button[16];
            for (int i = 0; i < 16; i++)
            {
                btn4[i] = new Button();
                btn4[i].Name = "JOGB" + (i + 1 + num).ToString().PadLeft(2, '0');
                btn4[i].Text = "JOG-";
                btn4[i].AutoSize = false;
                btn4[i].TextAlign = ContentAlignment.MiddleCenter;
                btn4[i].BackColor = Color.LightGray;
                dataGridView.Controls.Add(btn4[i]);
                Rectangle rect = dataGridView.GetCellDisplayRectangle(9, i, false);
                btn4[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                btn4[i].Location = new Point(rect.Left + 3, rect.Top + 3);
                btn4[i].MouseUp += new MouseEventHandler(JogBtnB_MouseUp);
                btn4[i].MouseDown += new MouseEventHandler(JogBtnB_MouseDown);

            }

            //移动
            Button[] btn5 = new Button[16];
            for (int i = 0; i < 16; i++)
            {
                btn5[i] = new Button();
                btn5[i].Name = "Move" + (i + 1 + num).ToString().PadLeft(2, '0');
                btn5[i].Text = "移动";
                btn5[i].AutoSize = false;
                btn5[i].TextAlign = ContentAlignment.MiddleCenter;
                btn5[i].BackColor = Color.LightGray;
                dataGridView.Controls.Add(btn5[i]);
                Rectangle rect = dataGridView.GetCellDisplayRectangle(10, i, false);
                btn5[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                btn5[i].Location = new Point(rect.Left + 3, rect.Top + 3);
                btn5[i].Click += new EventHandler(MoveBtn_Click);

            }

            //保存
            Button[] btn6 = new Button[16];
            for (int i = 0; i < 16; i++)
            {
                btn6[i] = new Button();
                btn6[i].Name = "Save" + (i + 1 + num).ToString().PadLeft(2, '0');
                btn6[i].Text = "保存";
                btn6[i].AutoSize = false;
                btn6[i].TextAlign = ContentAlignment.MiddleCenter;
                btn6[i].BackColor = Color.LightGray;
                dataGridView.Controls.Add(btn6[i]);
                Rectangle rect = dataGridView.GetCellDisplayRectangle(11, i, false);
                btn6[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                btn6[i].Location = new Point(rect.Left + 3, rect.Top + 3);
                btn6[i].Click += new EventHandler(SaveBtn_Click);

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

        #region 读取Axis速度名称
        public void ReadAxisVel(DataGridView dataGridView, string name)
        {
            string[] str = new string[7] { "1", "5", "10", "20", "30", "40", "50" };
            foreach (Control c in dataGridView.Controls)
            {
                if (c.Name.Contains(name))
                {
                    ComboBox comb = c as ComboBox;
                    //if (c.Name != "AxisVel16")
                    //{
                    for (int i = 0; i < 7; i++)
                    {
                        if (str[i] != "" && str[i] != null)
                        {
                            comb.Items.Add(str[i]);
                            if (i == 1)
                            {
                                comb.SelectedItem = str[i];
                            }
                        }
                    }
                    //}
                }

            }
        }
        #endregion

        #region 读取Axis点位名称
        public void ReadAxisPoint(DataGridView dataGridView, string name)
        {

            foreach (Control c in dataGridView.Controls)
            {
                if (c.Name.Contains(name))
                {
                    string index = c.Name;
                    ComboBox comb = c as ComboBox;
                    for (int i = 0; i < 20; i++)
                    {
                        string str = inIHelper.AxisReadContentValue("Axis", index + "_" + (i + 1).ToString());
                        if (str != "" && str != null)
                        {
                            comb.Items.Add(str);
                            if (i == 0)
                            {
                                comb.SelectedItem = str;
                            }
                        }
                    }

                }
            }
        }
        #endregion

        #region 使能
        public void EnableBtn_Click(object sender, EventArgs e)
        {
            int picStatus;              //读取轴状态
            uint pClock;                //时钟信号
            Button btn = sender as Button;
            string name = btn.Name.Substring(6, btn.Name.Length - 6);
            short s = (short)(Convert.ToUInt16(name));
            if (s < 13)
            {
                short axis = (short)(Convert.ToUInt16(name) - 0);
                mc.GTN_GetSts(1, axis, out picStatus, 1, out pClock);
                if ((picStatus & 0x0200) == 0)//判断第9位是否为1
                {
                    mc.GTN_ClrSts(1, axis, 1);   //清除状态，若是当前驱动器报警是能无效，返回值1
                    mc.GTN_AxisOn(1, axis);      //单轴使能
                    btn.BackColor = Color.Green;
                }
                else
                {
                    mc.GTN_ClrSts(1, axis, 1);
                    mc.GTN_AxisOff(1, axis);        //单轴下使能
                    btn.BackColor = Color.LightGray;
                }
            }
            else
            {
                short axis = (short)(Convert.ToUInt16(name) - 12);
                mc.GTN_GetSts(2, axis, out picStatus, 1, out pClock);
                if ((picStatus & 0x0200) == 0)//判断第9位是否为1
                {
                    mc.GTN_ClrSts(2, axis, 1);   //清除状态，若是当前驱动器报警是能无效，返回值1
                    mc.GTN_AxisOn(2, axis);      //单轴使能
                    btn.BackColor = Color.Green;
                }
                else
                {
                    mc.GTN_ClrSts(2, axis, 1);
                    mc.GTN_AxisOff(2, axis);        //单轴下使能
                    btn.BackColor = Color.LightGray;
                }
            }

        }
        #endregion

        #region 回零

        //核1回原点
        private void AlignHomeBtn(object sender, short carID, short homeMode, short moveDir, short indexDir, double velHigh, double velLow, int escapeStep)
        {
            short axis;
            Button btn = sender as Button;
            //btn.Enabled = false;
            string name = btn.Name.Substring(4, btn.Name.Length - 4);//Home01
            short s = (short)(Convert.ToUInt16(name));
            if (s < 13)
            {
                axis = (short)(Convert.ToUInt16(name) - 0);
                if (btn.BackColor == Color.LightGray)
                {
                    btn.BackColor = Color.Green;
                    motion.SmartHome1(carID, axis, homeMode, moveDir, indexDir, velHigh, velLow, escapeStep);
                    btn.BackColor = Color.LightGray;
                    //btn.Enabled = true;
                    Variable.ServoAlarmHappen[axis] = false;
                }
                else
                {
                    btn.BackColor = Color.LightGray;
                    //停止回原点
                    mc.GTN_Stop(carID, 1 << (axis - 1), 0);
                }
            }
            else
            {
                axis = (short)(Convert.ToUInt16(name) - 12);
                if (btn.BackColor == Color.LightGray)
                {
                    btn.BackColor = Color.Green;
                    motion.SmartHome2(carID, axis, homeMode, moveDir, indexDir, velHigh, velLow, escapeStep);
                    btn.BackColor = Color.LightGray;
                    //btn.Enabled = true;
                    Variable.ServoAlarmHappen[axis] = false;
                }
                else
                {
                    btn.BackColor = Color.LightGray;
                    //停止回原点
                    mc.GTN_Stop(carID, 1 << (axis - 1), 0);
                }
            }

        }
        public void HomeBtn_Click(object sender, EventArgs e)
        {
            short carID;
            Button btn = sender as Button;
            string name = btn.Name.Substring(4, btn.Name.Length - 4);//Home01
            short axis = (short)(Convert.ToUInt16(name));

            // 回原点模式
            short homeMode = 17;//回原点模式
            short moveDir = 1;//正方向回零，1正2负
            short indexDir = 1;
            //double Vel = Variable.AxisRealRunVel[1] * Variable.AxisPulse[1] / (Variable.AxisPitch[1] * 1000);
            double velHigh = Variable.AxisHmoeVelHight[axis] * Variable.AxisPulse[axis] / (Variable.AxisPitch[axis] * 1000);
            double velLow = Variable.AxisHmoeVelLow[axis] * Variable.AxisPulse[axis] / (Variable.AxisPitch[axis] * 1000);
            int escapeStep = 200;//方向脱离距离
            if (axis == 2 || axis == 3 || axis == 10 || axis == 11 || axis == 12)//原点极限正方向回零
            {
                homeMode = 11;
                moveDir = 1;
                escapeStep = 200;
            }
            else if (axis == 1 || axis == 9 || axis == 15 || axis == 16)//原点极限负方向回零
            {
                homeMode = 11;
                moveDir = -1;
                escapeStep = 200;
            }
            else if (false)//原点正方向回零
            {
                homeMode = 20;
                moveDir = 1;
                escapeStep = 200;
            }
            else if (false)//原点负方向回零
            {
                homeMode = 20;
                moveDir = -1;
                escapeStep = 200;
            }
            else if (axis == 5 || axis == 6 || axis == 7 || axis == 14)//极限正方向回零
            {
                homeMode = 10;
                moveDir = 1;
                escapeStep = 200;
            }
            else if (axis == 4 || axis == 13)//极限负方向回零
            {
                homeMode = 10;
                moveDir = -1;
                escapeStep = 200;
            }

            //判断在安全位
            bool safetyFlag = IsSafety(axis);
            if (safetyFlag)
            {
                if (axis < 13)
                {
                    carID = 1;
                }
                else
                {
                    carID = 2;
                }
                Func<object> func = new Func<object>(() => { AlignHomeBtn(sender, carID, homeMode, moveDir, indexDir, velHigh, velLow, escapeStep); return sender; });
                AsyncCallback callback = new AsyncCallback((x) => { });
                func.BeginInvoke(callback, sender);

            }
        }
        #endregion

        #region JOG

        #region JOG+
        public void JogBtnA_MouseUp(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            string name = btn.Name.Substring(4, btn.Name.Length - 4);
            short s = (short)(Convert.ToUInt16(name));
            if (s < 13)
            {
                short axis = (short)(Convert.ToUInt16(name) - 0);
                motion.AxisStop(1, axis);
            }
            else
            {
                short axis = (short)(Convert.ToUInt16(name) - 12);
                motion.AxisStop(2, axis);
            }

        }

        public void JogBtnA_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            string name = btn.Name.Substring(4, btn.Name.Length - 4);
            short axisNum = (short)(Convert.ToUInt16(name));
            if (axisNum < 13)
            {
                short axis = (short)(Convert.ToUInt16(name) - 0);
                mc.GTN_ClrSts(1, axisNum, 1);//清除轴状态

                JOGPDown(1, axis, Convert.ToDouble(comboxVel[axis - 1].Text.Trim()) * Variable.AxisPulse[axis] / (Variable.AxisPitch[axis] * 1000), Variable.AxisTacc[axis], Variable.AxisTdec[axis], Variable.AxisSmoothCoefficient[axis]);
            }
            else
            {
                short axis = (short)(Convert.ToUInt16(name) - 12);
                mc.GTN_ClrSts(2, axis, 1);//清除轴状态

                JOGPDown(2, axis, Convert.ToDouble(comboxVel[axisNum - 1].Text.Trim()) * Variable.AxisPulse[axisNum] / (Variable.AxisPitch[axisNum] * 1000), Variable.AxisTacc[axisNum], Variable.AxisTdec[axisNum], Variable.AxisSmoothCoefficient[axisNum]);
            }
        }
        #endregion

        #region JOG-
        public void JogBtnB_MouseUp(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            string name = btn.Name.Substring(4, btn.Name.Length - 4);
            short s = (short)(Convert.ToUInt16(name));
            if (s < 13)
            {
                short axis = (short)(Convert.ToUInt16(name) - 0);
                motion.AxisStop(1, axis);
            }
            else
            {
                short axis = (short)(Convert.ToUInt16(name) - 12);
                motion.AxisStop(2, axis);
            }
        }

        public void JogBtnB_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            string name = btn.Name.Substring(4, btn.Name.Length - 4);
            short axisNum = (short)(Convert.ToUInt16(name));
            if (axisNum < 13)
            {
                short axis = (short)(Convert.ToUInt16(name) - 0);
                mc.GTN_ClrSts(1, axis, 1);//清除轴状态

                JOGNDown(1, axis, Convert.ToDouble(comboxVel[axis - 1].Text.Trim()) * Variable.AxisPulse[axis] / (Variable.AxisPitch[axis] * 1000), Variable.AxisTacc[axis], Variable.AxisTdec[axis], Variable.AxisSmoothCoefficient[axis]);
            }
            else
            {
                short axis = (short)(Convert.ToUInt16(name) - 12);
                mc.GTN_ClrSts(2, axis, 1);//清除轴状态

                JOGNDown(2, axis, Convert.ToDouble(comboxVel[axisNum - 1].Text.Trim()) * Variable.AxisPulse[axisNum] / (Variable.AxisPitch[axisNum] * 1000), Variable.AxisTacc[axisNum], Variable.AxisTdec[axisNum], Variable.AxisSmoothCoefficient[axisNum]);
            }
        }
        #endregion

        /* JOG运动 */
        /* 方向有速度的正负号决定 */

        //正转
        private void JOGPDown(short carID, short axis, double vel, double acc, double dec, short SmoothCoefficient)
        {
            double velJ = vel;//jog运动通过vel的符号来改变方向
            double accJ = acc;
            double decJ = dec;
            short SmoothCoefficientJ = SmoothCoefficient;//平滑系数 取值范围： [0, 1]
            mc.TJogPrm tJogPrm = new mc.TJogPrm { acc = accJ, dec = decJ, smooth = SmoothCoefficientJ };
            motion.JOG(carID, axis, tJogPrm, velJ);
        }

        //反转
        private void JOGNDown(short carID, short axis, double vel, double acc, double dec, short SmoothCoefficient)
        {
            double velJ = -vel;//jog运动通过vel的符号来改变方向
            double accJ = acc;
            double decJ = dec;
            short SmoothCoefficientJ = SmoothCoefficient;//平滑系数 取值范围： [0, 1]
            mc.TJogPrm tJogPrm = new mc.TJogPrm { acc = accJ, dec = decJ, smooth = SmoothCoefficientJ };
            motion.JOG(carID, axis, tJogPrm, velJ);
        }

        #endregion

        #region 移动到点位

        /* 绝对式点位运动 */
        private void MoveAbs(short carID, short axis, double vel, double posAbs, double acc, double dec, double velStart, short smoothtime)
        {
            double velA = vel;
            double accA = acc;
            double decA = dec;
            double velStartA = velStart;
            short smoothTimeA = smoothtime; //平滑时间 取值范围：0-50ms
            int pos = (int)posAbs;
            motion.P2PAbs(carID, axis, velA, accA, decA, velStartA, smoothTimeA, pos);
        }
        public void MoveBtn_Click(object sender, EventArgs e)
        {
            short carID;
            short axis;
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.BackColor = Color.Green;
            string name = btn.Name.Substring(4, btn.Name.Length - 4);
            short axisName = (short)(Convert.ToUInt16(name));
            if (axisName < 13)
            {
                carID = 1;
                axis = (short)(Convert.ToUInt16(name) - 0);
            }
            else
            {
                carID = 2;
                axis = (short)(Convert.ToUInt16(name) - 12);
            }
            double pos = motion.GetAxisPosition(axisName) / Variable.AxisPitch[axisName] * Variable.AxisPulse[axisName];
            double Vel = Convert.ToDouble(comboxVel[axisName - 1].Text.Trim()) * Variable.AxisPulse[axisName] / (Variable.AxisPitch[axisName] * 1000);
            for (int i = 0; i < 20; i++)
            {
                if (comboxPoint[axisName - 1].SelectedIndex == i)
                {
                    pos = Variable.AxisPos[axisName, i];
                }
            }
            pos = pos / Variable.AxisPitch[axisName] * Variable.AxisPulse[axisName];

            //判断在安全位
            bool safetyFlag = IsSafety(axisName);
            if (safetyFlag)
            {
                Func<object> func = new Func<object>(() => { MoveAbs(carID, axis, Vel, pos, Variable.AxisTacc[axisName], Variable.AxisTdec[axisName], Variable.AxisStartVel[axisName], 0); return sender; });
                AsyncCallback callback = new AsyncCallback((x) => { });
                func.BeginInvoke(callback, sender);
                btn.Enabled = true;
                btn.BackColor = Color.LightGray;
            }
        }

        #endregion

        #region 点位保存
        public void SavePoint(string path)
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    inIHelper.writeIni("PGM", "AxisPos" + (i + 1).ToString() + "/" + j.ToString(), Variable.AxisPos[i + 1, j].ToString(), path);
                }
            }
        }
        public void SaveBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认保存坐标吗?选择'是'保存坐标，选择'否'放弃保存！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Button btn = sender as Button;
                double pos = 0;
                btn.Enabled = false;
                btn.BackColor = Color.Green;
                string name = btn.Name.Substring(4, btn.Name.Length - 4);
                short axis = (short)(Convert.ToUInt16(name) - 0);
                if (axis < 13)
                {
                    pos = motion.GetAxisPosition(axis) / Variable.AxisPulse[axis] * Variable.AxisPitch[axis];
                }
                else
                {
                    pos = motion.GetAxisPosition1((short)(axis - 12)) / Variable.AxisPulse[axis] * Variable.AxisPitch[axis];
                }
                for (int i = 0; i < 20; i++)
                {
                    if (comboxPoint[axis - 1].SelectedIndex == i)
                    {
                        Variable.AxisPos[axis, i] = Math.Round(pos, 2);
                    }
                }
                btn.Enabled = true;
                btn.BackColor = Color.LightGray;
                SavePoint(Application.StartupPath + "\\Point.ini");
            }
        }

        #endregion

        #region 当前坐标刷新
        public void PointRsh()
        {
            while (true)
            {
                if (this.IsDisposed)
                {
                    return;
                }

                #region 急停

                if (Variable.EMG)
                {
                    foreach (var c in dataGridAxis1.Controls)
                    {
                        if (c is Button)
                        {
                            Button bt = (Button)c;
                            if (bt.Name.Length > 6)
                            {
                                if (bt.Name.Substring(0, 6) == "Enable")
                                {
                                    bt.BackColor = Color.LightGray;
                                }
                            }
                        }
                    }
                }

                #endregion

                #region 当前坐标刷新

                for (int i = 0; i < 16; i++)
                {
                    if (i != 7)
                    {
                        dataGridAxis1.Rows[i].Cells[3].Value = Variable.REApos[i + 1];
                    }
                    else
                    {
                        dataGridAxis1.Rows[i].Cells[3].Value = 0;
                    }

                }

                #endregion

                #region 点位坐标显示

                for (int i = 0; i < 16; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        if (i < 16)
                        {
                            if (tabControl1.SelectedIndex == 0)
                            {
                                if (comboxPoint[i].SelectedIndex == j)
                                {
                                    dataGridAxis1.Rows[i].Cells[5].Value = Convert.ToString(Variable.AxisPos[i + 1, j]);
                                }
                            }
                        }
                        else
                        {
                            if (tabControl1.SelectedIndex == 1)
                            {
                                if (comboxPoint[i].SelectedIndex == j)
                                {
                                    //dataGridAxis2.Rows[i - 16].Cells[5].Value = Convert.ToString(Variable.AxisPos[i + 1, j]);
                                }
                            }
                        }
                    }
                }


                #endregion

                #region 权限

                if (Variable.userEnter == Variable.UserEnter.Manufacturer || Variable.userEnter == Variable.UserEnter.Administrator)
                {
                    dataGridAxis1.Enabled = true;
                }
                else
                {
                    dataGridAxis1.Enabled = false;
                }

                #endregion

                Thread.Sleep(10);
            }
        }

        #endregion

        #region 点位改变事件
        private void comboBox_TextChanged(object sender, EventArgs e)
        {
            ComboBox com = sender as ComboBox;
            string name = com.Name.Substring(9, com.Name.Length - 9);//AxisPoint01
            int i = Convert.ToUInt16(name) - 1;
            if (i < 16)
            {
                if (comboxPoint[0] != null)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        if (comboxPoint[i].SelectedIndex == j)
                        {
                            dataGridAxis1.Rows[i].Cells[5].Value = Convert.ToString(Variable.AxisPos[i + 1, j]);
                        }
                    }
                }
            }
            else
            {
                if (comboxPoint[16] != null)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        if (comboxPoint[i].SelectedIndex == j)
                        {
                            //dataGridAxis2.Rows[i - 16].Cells[5].Value = Convert.ToString(Variable.AxisPos[i + 1, j]);
                        }
                    }
                }
            }
        }
        #endregion

        #region 轴移动是否在安全位
        public bool IsSafety(short axis)
        {
            bool flag = false;
            if (axis == 1)
            {
                if (Variable.XStatus[49])
                {
                    if (Variable.XStatus[59] && Variable.XStatus[61])
                    {
                        if (Variable.AIMpos[4] <= Math.Round(Variable.AxisPos[4, 0] + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(Variable.AxisPos[4, 0] - 0.1, 2))
                        {
                            flag = true;
                        }
                        else
                        {
                            MessageBox.Show("上料吸嘴Z轴不在待机位，请确认！");
                        }
                    }
                    else
                    {
                        MessageBox.Show("上料吸嘴气缸不在上位，请确认！");
                    }
                }
                else
                {
                    MessageBox.Show("上料待测Tray工位2上顶气缸不在下位，请确认！");
                }
            }
            else if (axis == 2 || axis == 3)
            {
                if (Variable.XStatus[59] && Variable.XStatus[61])
                {
                    if (Variable.AIMpos[4] <= Math.Round(Variable.AxisPos[4, 0] + 0.1, 2) && Variable.AIMpos[4] >= Math.Round(Variable.AxisPos[4, 0] - 0.1, 2))
                    {
                        flag = true;
                    }
                    else
                    {
                        MessageBox.Show("上料吸嘴Z轴不在待机位，请确认！");
                    }
                }
                else
                {
                    MessageBox.Show("上料吸嘴气缸不在上位，请确认！");
                }
            }
            else if (axis == 9)
            {
                if (Variable.XStatus[72])
                {
                    if (Variable.XStatus[98] && Variable.XStatus[100])
                    {
                        if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2))
                        {
                            flag = true;
                        }
                        else
                        {
                            MessageBox.Show("下料吸嘴Z轴不在待机位，请确认！");
                        }
                    }
                    else
                    {
                        MessageBox.Show("下料吸嘴气缸不在上位，请确认！");
                    }
                }
                else
                {
                    MessageBox.Show("下料良品Tray盘工位2上顶气缸不在下位，请确认！");
                }
            }
            else if (axis == 10 || axis == 11 || axis == 12)
            {
                if (Variable.XStatus[98] && Variable.XStatus[100])
                {
                    if (Variable.AIMpos[13] <= Math.Round(Variable.AxisPos[13, 0] + 0.1, 2) && Variable.AIMpos[13] >= Math.Round(Variable.AxisPos[13, 0] - 0.1, 2))
                    {
                        flag = true;
                    }
                    else
                    {
                        MessageBox.Show("下料吸嘴Z轴不在待机位，请确认！");
                    }
                }
                else
                {
                    MessageBox.Show("下料吸嘴气缸不在上位，请确认！");
                }
            }
            else if (axis == 15)
            {
                if (Variable.XStatus[102])
                {
                    flag = true;
                }
                else
                {
                    MessageBox.Show("移Tray夹爪上下气缸不在上位，请确认！");
                }

            }
            else if (axis == 16)
            {
                if (MessageBox.Show("请确认机械手在安全位?选择'是'确认，选择'否'放弃！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (Variable.RobotSafePoint)//机械手X轴移动到上料位
                    {
                        flag = true;
                    }
                    else
                    {
                        MessageBox.Show("机械手不在安全位，请确认！");
                    }
                }
            }
            else
            {
                flag = true;
            }
            return flag;
        }
        #endregion

        #region 去除符号

        public void SetTextBoxOnlyInt(Control ctrl)
        {
            foreach (Control item in ctrl.Controls)
            {
                if (item is TextBox)
                {
                    ((TextBox)item).KeyPress += textBox_KeyPress;
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




    }
}
