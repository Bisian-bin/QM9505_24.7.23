using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QM9505
{
    public partial class IOForm : Form
    {
        INIHelper inIHelper = new INIHelper();
        DataGrid dataGrid = new DataGrid();
        Function function = new Function();
        Motion motion = new Motion();
        TXT myTXT = new TXT();
        public Thread IORefresh;
        PictureBox[] picbox_X = new PictureBox[448];
        PictureBox[] picbox_Y = new PictureBox[448];
        PictureBox[] picbox_Axis = new PictureBox[64];
        PictureBox[] picbox_Axisx = new PictureBox[64];
        public string picStatus_X;
        public string picStatus_Y;
        public string picStatus_Axis;
        public string picStatus_Axisx;
        public int formNum = 0;
        public int ioStep = 0;

        public IOForm()
        {
            InitializeComponent();
            //tabPage3.Parent = null;
            //tabPage6.Parent = null;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            ioStep = 0;
            formNum += 1;
            if (formNum > 1)
            {
                if (IORefresh != null)
                {
                    IORefresh.Abort();
                    IORefresh = null;
                }
            }

            base.OnVisibleChanged(e);
            if (!IsHandleCreated)
            {
                this.Close();
            }
        }

        #region 窗体加载
        private void IOForm_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;//线程间操作

            //初始化DataGridView
            dataGrid.NotChangeListRow(dataGridIN1);
            DataGridViewInit(dataGridIN1, 0);
            AddControl_X(dataGridIN1, 0);
            ReadIN(dataGridIN1, "_In");

            DataGridViewInit(dataGridIN2, 12);
            DataGridViewInit(dataGridIN3, 24);
            DataGridViewInit(dataGridIN4, 36);
            DataGridViewInit(dataGridIN5, 48);
            DataGridViewInit(dataGridIN6, 60);
            DataGridViewInit(dataGridIN7, 72);
            DataGridViewInit(dataGridOut1, 0);
            DataGridViewInit(dataGridOut2, 12);
            DataGridViewInit(dataGridOut3, 24);
            DataGridViewInit(dataGridOut4, 36);
            DataGridViewInit(dataGridOut5, 48);
            DataGridViewInit(dataGridOut6, 60);
            DataGridViewInit(dataGridOut7, 72);
            DataGridViewInit(dataGridAxis1, 0);

            #region 初始化控件

            //for (int i = 0; i < 64; i++)
            //{
            //    picStatus_X = "Xlight" + (i).ToString();
            //    picbox_X[i] = (PictureBox)(this.Controls.Find(picStatus_X, true)[0]);
            //}

            //for (int i = 0; i < 64; i++)
            //{
            //    picStatus_Y = "Ylight" + (i).ToString();
            //    picbox_Y[i] = (PictureBox)(this.Controls.Find(picStatus_Y, true)[0]);
            //}

            #endregion

            #region 开启线程

            IORefresh = new Thread(IORsh);//开始后，开新线程执行此方法
            IORefresh.IsBackground = true;
            IORefresh.Start();


            #endregion

        }

        #endregion

        #region 窗体关闭
        private void IOForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IORefresh != null)
            {
                IORefresh.Abort();
                IORefresh = null;
            }
        }

        #endregion

        #region tabpage切换
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (TabControl1.SelectedIndex)
            {
                case 0:
                    {
                        IORefresh.Suspend();
                        dataGrid.NotChangeListRow(dataGridIN1);
                        AddControl_X(dataGridIN1, 0);
                        ReadIN(dataGridIN1, "_In");
                        IORefresh.Resume();
                        break;
                    }
                case 1:
                    {
                        IORefresh.Suspend();
                        dataGrid.NotChangeListRow(dataGridIN2);
                        AddControl_X(dataGridIN2, 64);
                        ReadIN(dataGridIN2, "_In");
                        IORefresh.Resume();
                        break;
                    }
                case 2:
                    {
                        IORefresh.Suspend();
                        dataGrid.NotChangeListRow(dataGridIN3);
                        AddControl_X(dataGridIN3, 128);
                        ReadIN(dataGridIN3, "_In");
                        IORefresh.Resume();
                        break;
                    }
                case 3:
                    {
                        IORefresh.Suspend();
                        dataGrid.NotChangeListRow(dataGridIN3);
                        AddControl_X(dataGridIN4, 192);
                        ReadIN(dataGridIN4, "_In");
                        IORefresh.Resume();
                        break;
                    }
                case 4:
                    {
                        IORefresh.Suspend();
                        dataGrid.NotChangeListRow(dataGridIN3);
                        AddControl_X(dataGridIN5, 256);
                        ReadIN(dataGridIN5, "_In");
                        IORefresh.Resume();
                        break;
                    }
                case 5:
                    {
                        IORefresh.Suspend();
                        dataGrid.NotChangeListRow(dataGridIN3);
                        AddControl_X(dataGridIN6, 320);
                        ReadIN(dataGridIN6, "_In");
                        IORefresh.Resume();
                        break;
                    }
                case 6:
                    {
                        IORefresh.Suspend();
                        dataGrid.NotChangeListRow(dataGridIN3);
                        AddControl_X(dataGridIN7, 384);
                        ReadIN(dataGridIN7, "_In");
                        IORefresh.Resume();
                        break;
                    }
                case 7:
                    {
                        IORefresh.Suspend();
                        dataGrid.NotChangeListRow(dataGridOut1);
                        AddControl_Y(dataGridOut1, 0);
                        ReadOUT(dataGridOut1, "_out");
                        IORefresh.Resume();
                        break;
                    }
                case 8:
                    {
                        IORefresh.Suspend();
                        dataGrid.NotChangeListRow(dataGridOut2);
                        AddControl_Y(dataGridOut2, 64);
                        ReadOUT(dataGridOut2, "_out");
                        IORefresh.Resume();
                        break;
                    }
                case 9:
                    {
                        IORefresh.Suspend();
                        dataGrid.NotChangeListRow(dataGridOut3);
                        AddControl_Y(dataGridOut3, 128);
                        ReadOUT(dataGridOut3, "_out");
                        IORefresh.Resume();
                        break;
                    }
                case 10:
                    {
                        IORefresh.Suspend();
                        dataGrid.NotChangeListRow(dataGridOut3);
                        AddControl_Y(dataGridOut4, 192);
                        ReadOUT(dataGridOut4, "_out");
                        IORefresh.Resume();
                        break;
                    }
                case 11:
                    {
                        IORefresh.Suspend();
                        dataGrid.NotChangeListRow(dataGridOut3);
                        AddControl_Y(dataGridOut5, 256);
                        ReadOUT(dataGridOut5, "_out");
                        IORefresh.Resume();
                        break;
                    }
                case 12:
                    {
                        IORefresh.Suspend();
                        dataGrid.NotChangeListRow(dataGridOut3);
                        AddControl_Y(dataGridOut6, 320);
                        ReadOUT(dataGridOut6, "_out");
                        IORefresh.Resume();
                        break;
                    }
                case 13:
                    {
                        IORefresh.Suspend();
                        dataGrid.NotChangeListRow(dataGridOut3);
                        AddControl_Y(dataGridOut7, 384);
                        ReadOUT(dataGridOut7, "_out");
                        IORefresh.Resume();
                        break;
                    }
                case 14:
                    {
                        IORefresh.Suspend();
                        dataGrid.NotChangeListRow(dataGridAxis1);
                        AddControl_Axis(dataGridAxis1, "Axis", 0, 0);
                        ReadAxis(dataGridAxis1, "Home");
                        ReadAxis(dataGridAxis1, "Plimit");
                        ReadAxis(dataGridAxis1, "Nlimit");
                        ReadAxis(dataGridAxis1, "Alarm");
                        IORefresh.Resume();
                        break;
                    }
            }
        }
        #endregion

        #region 初始化DataGridView
        public void DataGridViewInit(DataGridView dataGridView, int num)
        {
            //添加列
            for (int i = 0; i < 12; i++)
            {
                if (i == 0 || i == 3 || i == 6 || i == 9)
                {
                    dataGridView.Columns.Add((i + num).ToString(), "状态");
                }
                else if (i == 1 || i == 4 || i == 7 || i == 10)
                {
                    dataGridView.Columns.Add((i + num).ToString(), "索引");
                }
                else if (i == 2 || i == 5 || i == 8 || i == 11)
                {
                    dataGridView.Columns.Add((i + num).ToString(), "内容");
                }
            }

            //添加行
            for (int i = 0; i < 16; i++)
            {
                dataGridView.Rows.Add();
            }
            //修改高度
            for (int i = 0; i < 16; i++)
            {
                dataGridView.Rows[i].Height = 55;
            }
            //修改宽度
            for (int i = 0; i < 12; i++)
            {
                if (i == 0 || i == 3 || i == 6 || i == 9)
                {
                    dataGridView.Columns[i].Width = 80;
                }
                else if (i == 1 || i == 4 || i == 7 || i == 10)
                {
                    dataGridView.Columns[i].Width = 80;
                }
                else if (i == 2 || i == 5 || i == 8 || i == 11)
                {
                    dataGridView.Columns[i].Width = 250;
                }
            }

            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//列标题居中
            dataGridView.AllowUserToAddRows = false;//取消第一行
            dataGridView.RowHeadersVisible = false;//取消第一列
            dataGridView.ClearSelection(); //取消默认选中  
        }

        #endregion

        #region 添加X控件
        public void AddControl_X(DataGridView dataGridView, int num)
        {
            //状态
            PictureBox[] pic = new PictureBox[64];
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 16; i++)
                {
                    pic[i] = new PictureBox();
                    pic[i].Name = "Xlight" + (j * 16 + i + num).ToString().PadLeft(3, '0');
                    dataGridView.Controls.Add(pic[i]);
                    Rectangle rect = dataGridView.GetCellDisplayRectangle(j * 3, i, false);
                    pic[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                    pic[i].Location = new Point(rect.Left + 3, rect.Top + 3);

                }
            }

            //索引
            Label[] lab = new Label[64];
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 16; i++)
                {
                    lab[i] = new Label();
                    lab[i].Text = "X" + (j * 16 + i + num).ToString().PadLeft(3, '0');
                    lab[i].AutoSize = false;
                    lab[i].TextAlign = ContentAlignment.MiddleCenter;
                    lab[i].BackColor = Color.LightGoldenrodYellow;
                    dataGridView.Controls.Add(lab[i]);
                    Rectangle rect = dataGridView.GetCellDisplayRectangle(j * 3 + 1, i, false);
                    lab[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                    lab[i].Location = new Point(rect.Left + 3, rect.Top + 3);

                }
            }

            //内容
            Label[] labcon = new Label[64];
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 16; i++)
                {
                    labcon[i] = new Label();
                    labcon[i].Name = "X" + (j * 16 + i + num).ToString().PadLeft(3, '0') + "_In";
                    labcon[i].Text = "123";
                    labcon[i].AutoSize = false;
                    labcon[i].TextAlign = ContentAlignment.MiddleCenter;
                    labcon[i].BackColor = Color.LightGoldenrodYellow;
                    dataGridView.Controls.Add(labcon[i]);
                    Rectangle rect = dataGridView.GetCellDisplayRectangle(j * 3 + 2, i, false);
                    labcon[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                    labcon[i].Location = new Point(rect.Left + 3, rect.Top + 3);

                }
            }
        }

        #endregion

        #region 添加Y控件
        public void AddControl_Y(DataGridView dataGridView, int num)
        {
            //状态
            PictureBox[] pic = new PictureBox[64];
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 16; i++)
                {
                    pic[i] = new PictureBox();
                    pic[i].Name = "Ylight" + (j * 16 + i + num).ToString().PadLeft(3, '0');
                    dataGridView.Controls.Add(pic[i]);
                    Rectangle rect = dataGridView.GetCellDisplayRectangle(j * 3, i, false);
                    pic[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                    pic[i].Location = new Point(rect.Left + 3, rect.Top + 3);

                }
            }

            //索引
            Label[] lab = new Label[64];
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 16; i++)
                {
                    lab[i] = new Label();
                    lab[i].Text = "Y" + (j * 16 + i + num).ToString().PadLeft(3, '0');
                    lab[i].AutoSize = false;
                    lab[i].TextAlign = ContentAlignment.MiddleCenter;
                    lab[i].BackColor = Color.LightGoldenrodYellow;
                    dataGridView.Controls.Add(lab[i]);
                    Rectangle rect = dataGridView.GetCellDisplayRectangle(j * 3 + 1, i, false);
                    lab[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                    lab[i].Location = new Point(rect.Left + 3, rect.Top + 3);

                }
            }

            //输出按钮
            Button[] btn = new Button[64];
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 16; i++)
                {
                    btn[i] = new Button();
                    btn[i].Name = "Y" + (j * 16 + i + num).ToString().PadLeft(3, '0') + "_out";
                    btn[i].Text = "123";
                    btn[i].AutoSize = false;
                    btn[i].TextAlign = ContentAlignment.MiddleCenter;
                    btn[i].BackColor = Color.LightGoldenrodYellow;
                    dataGridView.Controls.Add(btn[i]);
                    Rectangle rect = dataGridView.GetCellDisplayRectangle(j * 3 + 2, i, false);
                    btn[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                    btn[i].Location = new Point(rect.Left + 3, rect.Top + 3);
                    btn[i].Click += new EventHandler(OutBtn_Click);

                }
            }
        }

        #endregion

        #region 添加轴控件
        public void AddControl_Axis(DataGridView dataGridView, string str, int statu, int num)
        {
            //状态
            PictureBox[] pic = new PictureBox[64];
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 16; i++)
                {
                    pic[i] = new PictureBox();
                    pic[i].Name = str + (j * 16 + i + statu).ToString().PadLeft(3, '0');
                    dataGridView.Controls.Add(pic[i]);
                    Rectangle rect = dataGridView.GetCellDisplayRectangle(j * 3, i, false);
                    pic[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                    pic[i].Location = new Point(rect.Left + 3, rect.Top + 3);
                }
            }

            //索引
            Label[] lab = new Label[64];
            for (int j = 0; j < 4; j++)
            {
                switch (j)
                {
                    case 0:
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                lab[i] = new Label();
                                lab[i].Text = (i + 1 + num).ToString().PadLeft(2, '0') + "Home";
                                lab[i].AutoSize = false;
                                lab[i].TextAlign = ContentAlignment.MiddleCenter;
                                lab[i].BackColor = Color.LightGoldenrodYellow;
                                dataGridView.Controls.Add(lab[i]);
                                Rectangle rect = dataGridView.GetCellDisplayRectangle(j * 3 + 1, i, false);
                                lab[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                                lab[i].Location = new Point(rect.Left + 3, rect.Top + 3);
                            }
                            break;
                        }
                    case 1:
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                lab[i] = new Label();
                                lab[i].Text = (i + 1 + num).ToString().PadLeft(2, '0') + "Plimt";
                                lab[i].AutoSize = false;
                                lab[i].TextAlign = ContentAlignment.MiddleCenter;
                                lab[i].BackColor = Color.LightGoldenrodYellow;
                                dataGridView.Controls.Add(lab[i]);
                                Rectangle rect = dataGridView.GetCellDisplayRectangle(j * 3 + 1, i, false);
                                lab[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                                lab[i].Location = new Point(rect.Left + 3, rect.Top + 3);
                            }
                            break;
                        }
                    case 2:
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                lab[i] = new Label();
                                lab[i].Text = (i + 1 + num).ToString().PadLeft(2, '0') + "Nlimt";
                                lab[i].AutoSize = false;
                                lab[i].TextAlign = ContentAlignment.MiddleCenter;
                                lab[i].BackColor = Color.LightGoldenrodYellow;
                                dataGridView.Controls.Add(lab[i]);
                                Rectangle rect = dataGridView.GetCellDisplayRectangle(j * 3 + 1, i, false);
                                lab[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                                lab[i].Location = new Point(rect.Left + 3, rect.Top + 3);
                            }
                            break;
                        }
                    case 3:
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                lab[i] = new Label();
                                lab[i].Text = (i + 1 + num).ToString().PadLeft(2, '0') + "Alarm";
                                lab[i].AutoSize = false;
                                lab[i].TextAlign = ContentAlignment.MiddleCenter;
                                lab[i].BackColor = Color.LightGoldenrodYellow;
                                dataGridView.Controls.Add(lab[i]);
                                Rectangle rect = dataGridView.GetCellDisplayRectangle(j * 3 + 1, i, false);
                                lab[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                                lab[i].Location = new Point(rect.Left + 3, rect.Top + 3);
                            }
                            break;
                        }
                }
            }

            //内容
            Label[] labcon = new Label[64];
            for (int j = 0; j < 4; j++)
            {
                switch (j)
                {
                    case 0:
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                labcon[i] = new Label();
                                labcon[i].Name = (i + 1 + num).ToString().PadLeft(2, '0') + "Home";
                                labcon[i].Text = "123";
                                labcon[i].AutoSize = false;
                                labcon[i].TextAlign = ContentAlignment.MiddleCenter;
                                labcon[i].BackColor = Color.LightGoldenrodYellow;
                                dataGridView.Controls.Add(labcon[i]);
                                Rectangle rect = dataGridView.GetCellDisplayRectangle(j * 3 + 2, i, false);
                                labcon[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                                labcon[i].Location = new Point(rect.Left + 3, rect.Top + 3);

                            }
                            break;
                        }
                    case 1:
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                labcon[i] = new Label();
                                labcon[i].Name = (i + 1 + num).ToString().PadLeft(2, '0') + "Plimit";
                                labcon[i].Text = "123";
                                labcon[i].AutoSize = false;
                                labcon[i].TextAlign = ContentAlignment.MiddleCenter;
                                labcon[i].BackColor = Color.LightGoldenrodYellow;
                                dataGridView.Controls.Add(labcon[i]);
                                Rectangle rect = dataGridView.GetCellDisplayRectangle(j * 3 + 2, i, false);
                                labcon[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                                labcon[i].Location = new Point(rect.Left + 3, rect.Top + 3);

                            }
                            break;
                        }
                    case 2:
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                labcon[i] = new Label();
                                labcon[i].Name = (i + 1 + num).ToString().PadLeft(2, '0') + "Nlimit";
                                labcon[i].Text = "123";
                                labcon[i].AutoSize = false;
                                labcon[i].TextAlign = ContentAlignment.MiddleCenter;
                                labcon[i].BackColor = Color.LightGoldenrodYellow;
                                dataGridView.Controls.Add(labcon[i]);
                                Rectangle rect = dataGridView.GetCellDisplayRectangle(j * 3 + 2, i, false);
                                labcon[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                                labcon[i].Location = new Point(rect.Left + 3, rect.Top + 3);

                            }
                            break;
                        }
                    case 3:
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                labcon[i] = new Label();
                                labcon[i].Name = (i + 1 + num).ToString().PadLeft(2, '0') + "Alarm";
                                labcon[i].Text = "123";
                                labcon[i].AutoSize = false;
                                labcon[i].TextAlign = ContentAlignment.MiddleCenter;
                                labcon[i].BackColor = Color.LightGoldenrodYellow;
                                dataGridView.Controls.Add(labcon[i]);
                                Rectangle rect = dataGridView.GetCellDisplayRectangle(j * 3 + 2, i, false);
                                labcon[i].Size = new Size(rect.Width - 6, rect.Height - 6);
                                labcon[i].Location = new Point(rect.Left + 3, rect.Top + 3);

                            }
                            break;
                        }
                }
            }
        }

        #endregion

        #region 输出按钮点击
        public void OutBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                int a = Convert.ToInt32(btn.Name.Substring(1, 3));//获取按钮值
                if (btn.BackColor == Color.LightGoldenrodYellow)//根据颜色控制输出
                {
                    if (a < 10)
                    {
                        mc.GTN_SetDoBit(1, mc.MC_GPO, (short)(a + 1), 0);//主卡输出
                        btn.BackColor = Color.LightGreen;
                    }
                    else if (a >= 10 && a < 20)
                    {
                        mc.GTN_SetDoBit(2, mc.MC_GPO, (short)(a - 10 + 1), 0);//主卡输出
                        btn.BackColor = Color.LightGreen;
                    }
                    else if (a >= 20 && a < 100)
                    {
                        mc.GTN_SetExtDoBit(1, (short)(a - 20 + 1), 1);//模块
                        btn.BackColor = Color.LightGreen;
                    }
                    else if (a >= 100 && a < 420)
                    {
                        //mc.GTN_SetExtDoBit(1, (short)(a - 20 + 1), 1);//模块
                        //btn.BackColor = Color.LightGreen;
                        if ((a >= 100 && a < 116)|| (a >= 132 && a < 148) || (a >= 164 && a < 180) || (a >= 196 && a < 212) || (a >= 228 && a < 244) || (a >= 260 && a < 276) || (a >= 292 && a < 308) || (a >= 324 && a < 340) || (a >= 356 && a < 372) || (a >= 388 && a < 404))
                        {
                            int data1 = (a - 106) % 16;
                            int data2 = (a - 102) % 16;
                            int data3 = (a - 104) % 16;
                            if (data1 == 0 || data2 == 0 || data3 == 0)
                            {
                                if (data1 == 0)
                                {
                                    if (Variable.XStatus[a + 12] && Variable.XStatus[a + 16])
                                    {
                                        mc.GTN_SetExtDoBit(1, (short)(a - 20 + 1), 1);//模块
                                        btn.BackColor = Color.LightGreen;
                                    }
                                    else
                                    {
                                        MessageBox.Show("测试模组上顶气缸不在下位，请确认！");
                                    }
                                }
                                if (data2 == 0)
                                {
                                    if (Variable.XStatus[a + 24])
                                    {
                                        mc.GTN_SetExtDoBit(1, (short)(a - 20 + 1), 1);//模块
                                        btn.BackColor = Color.LightGreen;
                                    }
                                    else
                                    {
                                        MessageBox.Show("测试模组推Tray气缸不在原位，请确认！");
                                    }
                                }
                                if (data3 == 0)
                                {
                                    if (Variable.XStatus[a + 22])
                                    {
                                        mc.GTN_SetExtDoBit(1, (short)(a - 20 + 1), 1);//模块
                                        btn.BackColor = Color.LightGreen;
                                    }
                                    else
                                    {
                                        MessageBox.Show("测试模组推Tray气缸不在原位，请确认！");
                                    }
                                }
                            }
                            else
                            {
                                mc.GTN_SetExtDoBit(1, (short)(a - 20 + 1), 1);//模块
                                btn.BackColor = Color.LightGreen;
                            }
                        }
                        else
                        {
                            int data1 = (a - 106) % 16;
                            int data2 = (a - 102) % 16;
                            int data3 = (a - 104) % 16;
                            if (data1 == 0 || data2 == 0 || data3 == 0)
                            {
                                if (data1 == 0)
                                {
                                    if (Variable.XStatus[a + 10] && Variable.XStatus[a + 14])
                                    {
                                        mc.GTN_SetExtDoBit(1, (short)(a - 20 + 1), 1);//模块
                                        btn.BackColor = Color.LightGreen;
                                    }
                                    else
                                    {
                                        MessageBox.Show("测试模组上顶气缸不在下位，请确认！");
                                    }
                                }
                                if (data2 == 0)
                                {
                                    if (Variable.XStatus[a + 22])
                                    {
                                        mc.GTN_SetExtDoBit(1, (short)(a - 20 + 1), 1);//模块
                                        btn.BackColor = Color.LightGreen;
                                    }
                                    else
                                    {
                                        MessageBox.Show("测试模组推Tray气缸不在原位，请确认！");
                                    }
                                }
                                if (data3 == 0)
                                {
                                    if (Variable.XStatus[a + 20])
                                    {
                                        mc.GTN_SetExtDoBit(1, (short)(a - 20 + 1), 1);//模块
                                        btn.BackColor = Color.LightGreen;
                                    }
                                    else
                                    {
                                        MessageBox.Show("测试模组推Tray气缸不在原位，请确认！");
                                    }
                                }
                            }
                            else
                            {
                                mc.GTN_SetExtDoBit(1, (short)(a - 20 + 1), 1);//模块
                                btn.BackColor = Color.LightGreen;
                            }
                        }
                    }
                }
                else if (btn.BackColor == Color.LightGreen)//根据颜色控制输出
                {
                    if (a < 10)
                    {
                        mc.GTN_SetDoBit(1, mc.MC_GPO, (short)(a + 1), 1);//主卡1#输出
                        btn.BackColor = Color.LightGoldenrodYellow;
                    }
                    else if (a >= 10 && a < 20)
                    {
                        mc.GTN_SetDoBit(2, mc.MC_GPO, (short)(a - 10 + 1), 1);//主卡输出
                        btn.BackColor = Color.LightGoldenrodYellow;
                    }
                    else if (a >= 20 && a < 420)
                    {
                        mc.GTN_SetExtDoBit(1, (short)(a - 20 + 1), 0);//模块
                        btn.BackColor = Color.LightGoldenrodYellow;
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("输出点击按钮事件异常" + ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            }
        }
        #endregion

        #region IO文字显示

        public void ReadIN(DataGridView dataGridView, string name)
        {

            foreach (Control c in dataGridView.Controls)
            {
                if (c.Name.Contains(name))
                {
                    string index = c.Name.Substring(0, 4);

                    c.Text = inIHelper.IOReadContentValue("IO", index);
                }
            }
        }

        public void ReadOUT(DataGridView dataGridView, string name)
        {

            foreach (Control c in dataGridView.Controls)
            {
                if (c.Name.Contains(name))
                {
                    string index = c.Name.Substring(0, 4);

                    c.Text = inIHelper.IOReadContentValue("IO", index);
                }
            }
        }

        public void ReadAxis(DataGridView dataGridView, string name)
        {

            foreach (Control c in dataGridView.Controls)
            {
                if (c.Name.Contains(name))
                {
                    string index = c.Name;

                    c.Text = inIHelper.IOReadContentValue("IO", index);
                }
            }
        }


        #endregion

        #region 更新IO指示灯
        public void UpdateColor(DataGridView dataGridView, string name, int num, int state)
        {
            foreach (var c in dataGridView.Controls)
            {
                if (c is Label)
                {
                    Label lab = (Label)c;
                    if (lab.Name == name + num.ToString())
                    {
                        if (state == 1)
                        {
                            lab.BackColor = Color.Green;
                        }
                        else
                        {
                            lab.BackColor = Color.LightGray;
                        }
                    }
                }
            }
        }
        #endregion

        #region 刷新
        public void IORsh()
        {
            while (true)
            {
                if (this.IsDisposed)
                {
                    return;
                }

                #region IO界面刷新
                try
                {
                    switch (TabControl1.SelectedIndex)
                    {
                        case 0:
                            {
                                //输入刷新
                                for (int i = 0; i < 64; i++)
                                {
                                    picStatus_X = "Xlight" + (i).ToString().PadLeft(3, '0');
                                    picbox_X[i] = (PictureBox)(this.Controls.Find(picStatus_X, true)[0]);
                                }

                                for (int i = 0; i < 64; i++)
                                {
                                    if (Variable.XStatus[i])
                                    {
                                        picbox_X[i].BackColor = Color.Green;
                                    }
                                    else
                                    {
                                        picbox_X[i].BackColor = Color.LightGray;
                                    }
                                }
                                break;
                            }
                        case 1:
                            {
                                //输入刷新
                                for (int i = 64; i < 128; i++)
                                {
                                    picStatus_X = "Xlight" + (i).ToString().PadLeft(3, '0');
                                    picbox_X[i] = (PictureBox)(this.Controls.Find(picStatus_X, true)[0]);
                                }

                                for (int i = 64; i < 128; i++)
                                {
                                    if (Variable.XStatus[i])
                                    {
                                        picbox_X[i].BackColor = Color.Green;
                                    }
                                    else
                                    {
                                        picbox_X[i].BackColor = Color.LightGray;
                                    }
                                }
                                break;
                            }
                        case 2:
                            {
                                //输入刷新
                                for (int i = 128; i < 192; i++)
                                {
                                    picStatus_X = "Xlight" + (i).ToString().PadLeft(3, '0');
                                    picbox_X[i] = (PictureBox)(this.Controls.Find(picStatus_X, true)[0]);
                                }

                                for (int i = 128; i < 192; i++)
                                {
                                    if (Variable.XStatus[i])
                                    {
                                        picbox_X[i].BackColor = Color.Green;
                                    }
                                    else
                                    {
                                        picbox_X[i].BackColor = Color.LightGray;
                                    }
                                }
                                break;
                            }
                        case 3:
                            {
                                //输入刷新
                                for (int i = 192; i < 256; i++)
                                {
                                    picStatus_X = "Xlight" + (i).ToString().PadLeft(3, '0');
                                    picbox_X[i] = (PictureBox)(this.Controls.Find(picStatus_X, true)[0]);
                                }

                                for (int i = 192; i < 256; i++)
                                {
                                    if (Variable.XStatus[i])
                                    {
                                        picbox_X[i].BackColor = Color.Green;
                                    }
                                    else
                                    {
                                        picbox_X[i].BackColor = Color.LightGray;
                                    }
                                }
                                break;
                            }
                        case 4:
                            {
                                //输入刷新
                                for (int i = 256; i < 320; i++)
                                {
                                    picStatus_X = "Xlight" + (i).ToString().PadLeft(3, '0');
                                    picbox_X[i] = (PictureBox)(this.Controls.Find(picStatus_X, true)[0]);
                                }

                                for (int i = 256; i < 320; i++)
                                {
                                    if (Variable.XStatus[i])
                                    {
                                        picbox_X[i].BackColor = Color.Green;
                                    }
                                    else
                                    {
                                        picbox_X[i].BackColor = Color.LightGray;
                                    }
                                }
                                break;
                            }
                        case 5:
                            {
                                //输入刷新
                                for (int i = 320; i < 384; i++)
                                {
                                    picStatus_X = "Xlight" + (i).ToString().PadLeft(3, '0');
                                    picbox_X[i] = (PictureBox)(this.Controls.Find(picStatus_X, true)[0]);
                                }

                                for (int i = 320; i < 384; i++)
                                {
                                    if (Variable.XStatus[i])
                                    {
                                        picbox_X[i].BackColor = Color.Green;
                                    }
                                    else
                                    {
                                        picbox_X[i].BackColor = Color.LightGray;
                                    }
                                }
                                break;
                            }
                        case 6:
                            {
                                //输入刷新
                                for (int i = 384; i < 448; i++)
                                {
                                    picStatus_X = "Xlight" + (i).ToString().PadLeft(3, '0');
                                    picbox_X[i] = (PictureBox)(this.Controls.Find(picStatus_X, true)[0]);
                                }

                                for (int i = 384; i < 448; i++)
                                {
                                    if (Variable.XStatus[i])
                                    {
                                        picbox_X[i].BackColor = Color.Green;
                                    }
                                    else
                                    {
                                        picbox_X[i].BackColor = Color.LightGray;
                                    }
                                }
                                break;
                            }
                        case 7:
                            {
                                //输出刷新
                                for (int i = 0; i < 64; i++)
                                {
                                    picStatus_Y = "Ylight" + (i).ToString().PadLeft(3, '0');
                                    picbox_Y[i] = (PictureBox)(this.Controls.Find(picStatus_Y, true)[0]);
                                }

                                for (int i = 0; i < 64; i++)
                                {
                                    if (Variable.YStatus[i])
                                    {
                                        picbox_Y[i].BackColor = Color.Green;
                                    }
                                    else
                                    {
                                        picbox_Y[i].BackColor = Color.LightGray;
                                    }
                                }
                                break;
                            }
                        case 8:
                            {
                                //输出刷新
                                for (int i = 64; i < 128; i++)
                                {
                                    picStatus_Y = "Ylight" + (i).ToString().PadLeft(3, '0');
                                    picbox_Y[i] = (PictureBox)(this.Controls.Find(picStatus_Y, true)[0]);
                                }

                                for (int i = 64; i < 128; i++)
                                {
                                    if (Variable.YStatus[i])
                                    {
                                        picbox_Y[i].BackColor = Color.Green;
                                    }
                                    else
                                    {
                                        picbox_Y[i].BackColor = Color.LightGray;
                                    }
                                }
                                break;
                            }
                        case 9:
                            {
                                //输出刷新
                                for (int i = 128; i < 192; i++)
                                {
                                    picStatus_Y = "Ylight" + (i).ToString().PadLeft(3, '0');
                                    picbox_Y[i] = (PictureBox)(this.Controls.Find(picStatus_Y, true)[0]);
                                }

                                for (int i = 128; i < 192; i++)
                                {
                                    if (Variable.YStatus[i])
                                    {
                                        picbox_Y[i].BackColor = Color.Green;
                                    }
                                    else
                                    {
                                        picbox_Y[i].BackColor = Color.LightGray;
                                    }
                                }
                                break;
                            }
                        case 10:
                            {
                                //输出刷新
                                for (int i = 192; i < 256; i++)
                                {
                                    picStatus_Y = "Ylight" + (i).ToString().PadLeft(3, '0');
                                    picbox_Y[i] = (PictureBox)(this.Controls.Find(picStatus_Y, true)[0]);
                                }

                                for (int i = 192; i < 256; i++)
                                {
                                    if (Variable.YStatus[i])
                                    {
                                        picbox_Y[i].BackColor = Color.Green;
                                    }
                                    else
                                    {
                                        picbox_Y[i].BackColor = Color.LightGray;
                                    }
                                }
                                break;
                            }
                        case 11:
                            {
                                //输出刷新
                                for (int i = 256; i < 320; i++)
                                {
                                    picStatus_Y = "Ylight" + (i).ToString().PadLeft(3, '0');
                                    picbox_Y[i] = (PictureBox)(this.Controls.Find(picStatus_Y, true)[0]);
                                }

                                for (int i = 256; i < 320; i++)
                                {
                                    if (Variable.YStatus[i])
                                    {
                                        picbox_Y[i].BackColor = Color.Green;
                                    }
                                    else
                                    {
                                        picbox_Y[i].BackColor = Color.LightGray;
                                    }
                                }
                                break;
                            }
                        case 12:
                            {
                                //输出刷新
                                for (int i = 320; i < 384; i++)
                                {
                                    picStatus_Y = "Ylight" + (i).ToString().PadLeft(3, '0');
                                    picbox_Y[i] = (PictureBox)(this.Controls.Find(picStatus_Y, true)[0]);
                                }

                                for (int i = 320; i < 384; i++)
                                {
                                    if (Variable.YStatus[i])
                                    {
                                        picbox_Y[i].BackColor = Color.Green;
                                    }
                                    else
                                    {
                                        picbox_Y[i].BackColor = Color.LightGray;
                                    }
                                }
                                break;
                            }
                        case 13:
                            {
                                //输出刷新
                                for (int i = 384; i < 448; i++)
                                {
                                    picStatus_Y = "Ylight" + (i).ToString().PadLeft(3, '0');
                                    picbox_Y[i] = (PictureBox)(this.Controls.Find(picStatus_Y, true)[0]);
                                }

                                for (int i = 384; i < 448; i++)
                                {
                                    if (Variable.YStatus[i])
                                    {
                                        picbox_Y[i].BackColor = Color.Green;
                                    }
                                    else
                                    {
                                        picbox_Y[i].BackColor = Color.LightGray;
                                    }
                                }
                                break;
                            }
                        case 14:
                            {
                                //轴刷新
                                for (int i = 0; i < 64; i++)
                                {
                                    picStatus_Axis = "Axis" + (i).ToString().PadLeft(3, '0');
                                    picbox_Axis[i] = (PictureBox)(this.Controls.Find(picStatus_Axis, true)[0]);
                                }
                                //Home
                                for (int i = 0; i < 16; i++)//16
                                {
                                    if (IntToBin16(Variable.Home1, i) == "1")
                                    {
                                        picbox_Axis[i].BackColor = Color.Green;
                                    }
                                    else
                                    {
                                        picbox_Axis[i].BackColor = Color.LightGray;
                                    }
                                }

                                //Plimit
                                for (int i = 16; i < 32; i++)//32
                                {
                                    if (IntToBin16(Variable.Plimit1, i - 16) == "1")
                                    {
                                        picbox_Axis[i].BackColor = Color.Green;
                                    }
                                    else
                                    {
                                        picbox_Axis[i].BackColor = Color.LightGray;
                                    }
                                }

                                //Nlimit
                                for (int i = 32; i < 48; i++)//48
                                {
                                    if (IntToBin16(Variable.Nlimit1, i - 32) == "1")
                                    {
                                        picbox_Axis[i].BackColor = Color.Green;
                                    }
                                    else
                                    {
                                        picbox_Axis[i].BackColor = Color.LightGray;
                                    }
                                }

                                //Alarm
                                for (int i = 48; i < 64; i++)//64
                                {
                                    if (IntToBin16(Variable.Alarm1, i - 48) == "1")
                                    {
                                        picbox_Axis[i].BackColor = Color.Green;
                                    }
                                    else
                                    {
                                        picbox_Axis[i].BackColor = Color.LightGray;
                                    }
                                }

                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Log.SaveError(new StackTrace(new StackFrame(true)), new StackFrame(), ex);
                }
                #endregion

                #region 步序监控
                try
                {
                    labRestStep0.Text = Variable.RestStep.ToString();
                    labRestStep1.Text = Variable.INAutoReadyRestStep.ToString();
                    labRestStep2.Text = Variable.INAutoEmptyRestStep.ToString();
                    labRestStep3.Text = Variable.OutAutoOKRestStep.ToString();
                    labRestStep4.Text = Variable.OutAutoFillRestStep.ToString();
                    labRestStep5.Text = Variable.OutAutoNGRestStep.ToString();
                    labRestStep6.Text = Variable.RobotAutoRestStep.ToString();

                    labAutoStep0.Text = Variable.AutoStep.ToString();
                    labAutoStep1.Text = Variable.INAutoReady1Step.ToString();
                    labAutoStep2.Text = Variable.INAutoReadyStep.ToString();
                    labAutoStep3.Text = Variable.INAutoEmptyStartStep.ToString();
                    labAutoStep4.Text = Variable.OutAutoOKStartStep.ToString();
                    labAutoStep5.Text = Variable.OutAutoFillStartStep.ToString();
                    labAutoStep6.Text = Variable.OutAutoNGStartStep.ToString();
                    labAutoStep7.Text = Variable.RobotAutoStartStep.ToString();
                    labAutoStep8.Text = Variable.ModelSetStep.ToString();
                    labAutoStep9.Text = Variable.ModelGetStep.ToString();
                }
                catch (Exception ex)
                {
                    Log.SaveError(new StackTrace(new StackFrame(true)), new StackFrame(), ex);
                }

                #endregion

                Thread.Sleep(10);
            }
        }
        #endregion

        #region 将整数转换二进制

        public string IntToBin32(int data, int a)
        {
            string cnt;//记录转换过后二进制值
            byte[] IO_input = new byte[32];

            for (int i = 0; i < 32; i++)
            {
                IO_input[i] = (byte)(((data & 1) + 1) % 2);
                data = data >> 1;//n变成n向右移一位的那个数
            }
            cnt = IO_input[a].ToString();
            return cnt;
        }

        public string IntToBin16(int data, int a)
        {
            string cnt;//记录转换过后二进制值
            byte[] IO_input = new byte[16];

            for (int i = 0; i < 16; i++)
            {
                IO_input[i] = (byte)(((data & 1) + 1) % 2);
                data = data >> 1;//n变成n向右移一位的那个数
            }
            cnt = IO_input[a].ToString();
            return cnt;
        }














        #endregion


    }
}
