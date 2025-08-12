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

namespace QM9505.ControlForm
{
    public partial class GPSForm : Form
    {
        Thread DrawLineThread;
        TXT myTXT = new TXT();
        public int formNum = 0;

        public GPSForm()
        {
            InitializeComponent();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            formNum += 1;
            if (formNum > 1)
            {
                //if (DrawLineThread != null)
                //{
                //    DrawLineThread.Abort();
                //    DrawLineThread = null;
                //}
            }

            base.OnVisibleChanged(e);
            if (!IsHandleCreated)
            {
                this.Close();
            }
        }

        private void GPSForm_Load(object sender, EventArgs e)
        {
            //DrawLine
            DrawLineThread = new Thread(DrawLine);
            DrawLineThread.IsBackground = true;
            DrawLineThread.Start();
        }

        #region 清空白夜班Txt赋10
        public void TimeTxtClear(string path)
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

        #region 画竖线
        public void DrawLine()
        {
            while (true)
            {
                //创建Graphics对象
                Graphics GPS = panel2.CreateGraphics();
                //设置抗锯齿
                GPS.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                //获取当前年月日
                int year = Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2));
                int month = Convert.ToInt32(DateTime.Now.Month.ToString().PadLeft(2, '0'));
                int day = Convert.ToInt32(DateTime.Now.Day.ToString().PadLeft(2, '0'));
                int hour = Convert.ToInt32(DateTime.Now.Hour.ToString().PadLeft(2, '0'));
                int min = Convert.ToInt32(DateTime.Now.Minute.ToString().PadLeft(2, '0'));

                #region 白夜班数组清空

                //白班
                if ((hour * 100 + min) >= (7 * 100 + 30) && (hour * 100 + min) < (7 * 100 + 35))
                {
                    TimeTxtClear(Application.StartupPath + @"\Data\DayTime\Time");
                }

                //夜班
                if ((hour * 100 + min) >= (19 * 100 + 30) && (hour * 100 + min) < (19 * 100 + 35))
                {
                    TimeTxtClear(Application.StartupPath + @"\Data\NightTime\Time");
                }
                #endregion

                Variable.DayTime = myTXT.ReadTXT(Application.StartupPath + @"\Data\DayTime\Time");
                Variable.NightTime = myTXT.ReadTXT(Application.StartupPath + @"\Data\NightTime\Time");

                #region 白班数组

                //白班
                if ((hour * 100 + min) >= (7 * 100 + 30) && (hour * 100 + min) < (8 * 100 + 30))
                {
                    if (min > 30)
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.DayTime[min - 31] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.DayTime[min - 31] = "01";
                        }
                        else
                        {
                            Variable.DayTime[min - 31] = "10";
                        }
                    }
                    else
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.DayTime[min + 29] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.DayTime[min + 29] = "01";
                        }
                        else
                        {
                            Variable.DayTime[min + 29] = "10";
                        }
                    }
                }
                if ((hour * 100 + min) >= (8 * 100 + 30) && (hour * 100 + min) < (9 * 100 + 30))
                {
                    if (min > 30)
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.DayTime[min - 31 + 60] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.DayTime[min - 31 + 60] = "01";
                        }
                        else
                        {
                            Variable.DayTime[min - 31 + 60] = "10";
                        }
                    }
                    else
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.DayTime[min + 29 + 60] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.DayTime[min + 29 + 60] = "01";
                        }
                        else
                        {
                            Variable.DayTime[min + 29 + 60] = "10";
                        }
                    }
                }
                if ((hour * 100 + min) >= (9 * 100 + 30) && (hour * 100 + min) < (10 * 100 + 30))
                {
                    if (min > 30)
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.DayTime[min - 31 + 120] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.DayTime[min - 31 + 120] = "01";
                        }
                        else
                        {
                            Variable.DayTime[min - 31 + 120] = "10";
                        }
                    }
                    else
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.DayTime[min + 29 + 120] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.DayTime[min + 29 + 120] = "01";
                        }
                        else
                        {
                            Variable.DayTime[min + 29 + 120] = "10";
                        }
                    }
                }
                if ((hour * 100 + min) >= (10 * 100 + 30) && (hour * 100 + min) < (11 * 100 + 30))
                {
                    if (min > 30)
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.DayTime[min - 31 + 180] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.DayTime[min - 31 + 180] = "01";
                        }
                        else
                        {
                            Variable.DayTime[min - 31 + 180] = "10";
                        }
                    }
                    else
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.DayTime[min + 29 + 180] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.DayTime[min + 29 + 180] = "01";
                        }
                        else
                        {
                            Variable.DayTime[min + 29 + 180] = "10";
                        }
                    }
                }
                if ((hour * 100 + min) >= (11 * 100 + 30) && (hour * 100 + min) < (12 * 100 + 30))
                {
                    if (min > 30)
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.DayTime[min - 31 + 240] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.DayTime[min - 31 + 240] = "01";
                        }
                        else
                        {
                            Variable.DayTime[min - 31 + 240] = "10";
                        }
                    }
                    else
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.DayTime[min + 29 + 240] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.DayTime[min + 29 + 240] = "01";
                        }
                        else
                        {
                            Variable.DayTime[min + 29 + 240] = "10";
                        }
                    }
                }
                if ((hour * 100 + min) >= (12 * 100 + 30) && (hour * 100 + min) < (13 * 100 + 30))
                {
                    if (min > 30)
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.DayTime[min - 31 + 300] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.DayTime[min - 31 + 300] = "01";
                        }
                        else
                        {
                            Variable.DayTime[min - 31 + 300] = "10";
                        }
                    }
                    else
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.DayTime[min + 29 + 300] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.DayTime[min + 29 + 300] = "01";
                        }
                        else
                        {
                            Variable.DayTime[min + 29 + 300] = "10";
                        }
                    }
                }
                if ((hour * 100 + min) >= (13 * 100 + 30) && (hour * 100 + min) < (14 * 100 + 30))
                {
                    if (min > 30)
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.DayTime[min - 31 + 360] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.DayTime[min - 31 + 360] = "01";
                        }
                        else
                        {
                            Variable.DayTime[min - 31 + 360] = "10";
                        }
                    }
                    else
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.DayTime[min + 29 + 360] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.DayTime[min + 29 + 360] = "01";
                        }
                        else
                        {
                            Variable.DayTime[min + 29 + 360] = "10";
                        }
                    }
                }
                if ((hour * 100 + min) >= (14 * 100 + 30) && (hour * 100 + min) < (15 * 100 + 30))
                {
                    if (min > 30)
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.DayTime[min - 31 + 420] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.DayTime[min - 31 + 420] = "01";
                        }
                        else
                        {
                            Variable.DayTime[min - 31 + 420] = "10";
                        }
                    }
                    else
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.DayTime[min + 29 + 420] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.DayTime[min + 29 + 420] = "01";
                        }
                        else
                        {
                            Variable.DayTime[min + 29 + 420] = "10";
                        }
                    }
                }
                if ((hour * 100 + min) >= (15 * 100 + 30) && (hour * 100 + min) < (16 * 100 + 30))
                {
                    if (min > 30)
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.DayTime[min - 31 + 480] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.DayTime[min - 31 + 480] = "01";
                        }
                        else
                        {
                            Variable.DayTime[min - 31 + 480] = "10";
                        }
                    }
                    else
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.DayTime[min + 29 + 480] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.DayTime[min + 29 + 480] = "01";
                        }
                        else
                        {
                            Variable.DayTime[min + 29 + 480] = "10";
                        }
                    }
                }
                if ((hour * 100 + min) >= (16 * 100 + 30) && (hour * 100 + min) < (17 * 100 + 30))
                {
                    if (min > 30)
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.DayTime[min - 31 + 540] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.DayTime[min - 31 + 540] = "01";
                        }
                        else
                        {
                            Variable.DayTime[min - 31 + 540] = "10";
                        }
                    }
                    else
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.DayTime[min + 29 + 540] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.DayTime[min + 29 + 540] = "01";
                        }
                        else
                        {
                            Variable.DayTime[min + 29 + 540] = "10";
                        }
                    }
                }
                if ((hour * 100 + min) >= (17 * 100 + 30) && (hour * 100 + min) < (18 * 100 + 30))
                {
                    if (min > 30)
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.DayTime[min - 31 + 600] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.DayTime[min - 31 + 600] = "01";
                        }
                        else
                        {
                            Variable.DayTime[min - 31 + 600] = "10";
                        }
                    }
                    else
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.DayTime[min + 29 + 600] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.DayTime[min + 29 + 600] = "01";
                        }
                        else
                        {
                            Variable.DayTime[min + 29 + 600] = "10";
                        }
                    }
                }
                if ((hour * 100 + min) >= (18 * 100 + 30) && (hour * 100 + min) < (19 * 100 + 30))
                {
                    if (min > 30)
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.DayTime[min - 31 + 660] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.DayTime[min - 31 + 660] = "01";
                        }
                        else
                        {
                            Variable.DayTime[min - 31 + 660] = "10";
                        }
                    }
                    else
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.DayTime[min + 29 + 660] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.DayTime[min + 29 + 660] = "01";
                        }
                        else
                        {
                            Variable.DayTime[min + 29 + 660] = "10";
                        }
                    }
                }

                #endregion

                #region 夜班数组

                //夜班
                if ((hour * 100 + min) >= (19 * 100 + 30) && (hour * 100 + min) < (20 * 100 + 30))
                {
                    if (min > 30)
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.NightTime[min - 31] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.NightTime[min - 31] = "01";
                        }
                        else
                        {
                            Variable.NightTime[min - 31] = "10";
                        }
                    }
                    else
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.NightTime[min + 29] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.NightTime[min + 29] = "01";
                        }
                        else
                        {
                            Variable.NightTime[min + 29] = "10";
                        }
                    }
                }
                if ((hour * 100 + min) >= (20 * 100 + 30) && (hour * 100 + min) < (21 * 100 + 30))
                {
                    if (min > 30)
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.NightTime[min - 31 + 60] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.NightTime[min - 31 + 60] = "01";
                        }
                        else
                        {
                            Variable.NightTime[min - 31 + 60] = "10";
                        }
                    }
                    else
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.NightTime[min + 29 + 60] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.NightTime[min + 29 + 60] = "01";
                        }
                        else
                        {
                            Variable.NightTime[min + 29 + 60] = "10";
                        }
                    }
                }
                if ((hour * 100 + min) >= (21 * 100 + 30) && (hour * 100 + min) < (22 * 100 + 30))
                {
                    if (min > 30)
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.NightTime[min - 31 + 120] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.NightTime[min - 31 + 120] = "01";
                        }
                        else
                        {
                            Variable.NightTime[min - 31 + 120] = "10";
                        }
                    }
                    else
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.NightTime[min + 29 + 120] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.NightTime[min + 29 + 120] = "01";
                        }
                        else
                        {
                            Variable.NightTime[min + 29 + 120] = "10";
                        }
                    }
                }
                if ((hour * 100 + min) >= (22 * 100 + 30) && (hour * 100 + min) < (23 * 100 + 30))
                {
                    if (min > 30)
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.NightTime[min - 31 + 180] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.NightTime[min - 31 + 180] = "01";
                        }
                        else
                        {
                            Variable.NightTime[min - 31 + 180] = "10";
                        }
                    }
                    else
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.NightTime[min + 29 + 180] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.NightTime[min + 29 + 180] = "01";
                        }
                        else
                        {
                            Variable.NightTime[min + 29 + 180] = "10";
                        }
                    }
                }
                if ((hour * 100 + min) >= (23 * 100 + 30) || (hour * 100 + min) < (00 * 100 + 30))
                {
                    if (min > 30)
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.NightTime[min - 31 + 240] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.NightTime[min - 31 + 240] = "01";
                        }
                        else
                        {
                            Variable.NightTime[min - 31 + 240] = "10";
                        }
                    }
                    else
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.NightTime[min + 29 + 240] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.NightTime[min + 29 + 240] = "01";
                        }
                        else
                        {
                            Variable.NightTime[min + 29 + 240] = "10";
                        }
                    }
                }
                if ((hour * 100 + min) >= (00 * 100 + 30) && (hour * 100 + min) < (1 * 100 + 30))
                {
                    if (min > 30)
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.NightTime[min - 31 + 300] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.NightTime[min - 31 + 300] = "01";
                        }
                        else
                        {
                            Variable.NightTime[min - 31 + 300] = "10";
                        }
                    }
                    else
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.NightTime[min + 29 + 300] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.NightTime[min + 29 + 300] = "01";
                        }
                        else
                        {
                            Variable.NightTime[min + 29 + 300] = "10";
                        }
                    }
                }
                if ((hour * 100 + min) >= (1 * 100 + 30) && (hour * 100 + min) < (2 * 100 + 30))
                {
                    if (min > 30)
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.NightTime[min - 31 + 360] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.NightTime[min - 31 + 360] = "01";
                        }
                        else
                        {
                            Variable.NightTime[min - 31 + 360] = "10";
                        }
                    }
                    else
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.NightTime[min + 29 + 360] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.NightTime[min + 29 + 360] = "01";
                        }
                        else
                        {
                            Variable.NightTime[min + 29 + 360] = "10";
                        }
                    }
                }
                if ((hour * 100 + min) >= (2 * 100 + 30) && (hour * 100 + min) < (3 * 100 + 30))
                {
                    if (min > 30)
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.NightTime[min - 31 + 420] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.NightTime[min - 31 + 420] = "01";
                        }
                        else
                        {
                            Variable.NightTime[min - 31 + 420] = "10";
                        }
                    }
                    else
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.NightTime[min + 29 + 420] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.NightTime[min + 29 + 420] = "01";
                        }
                        else
                        {
                            Variable.NightTime[min + 29 + 420] = "10";
                        }
                    }
                }
                if ((hour * 100 + min) >= (3 * 100 + 30) && (hour * 100 + min) < (4 * 100 + 30))
                {
                    if (min > 30)
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.NightTime[min - 31 + 480] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.NightTime[min - 31 + 480] = "01";
                        }
                        else
                        {
                            Variable.NightTime[min - 31 + 480] = "10";
                        }
                    }
                    else
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.NightTime[min + 29 + 480] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.NightTime[min + 29 + 480] = "01";
                        }
                        else
                        {
                            Variable.NightTime[min + 29 + 480] = "10";
                        }
                    }
                }
                if ((hour * 100 + min) >= (4 * 100 + 30) && (hour * 100 + min) < (5 * 100 + 30))
                {
                    if (min > 30)
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.NightTime[min - 31 + 540] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.NightTime[min - 31 + 540] = "01";
                        }
                        else
                        {
                            Variable.NightTime[min - 31 + 540] = "10";
                        }
                    }
                    else
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.NightTime[min + 29 + 540] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.NightTime[min + 29 + 540] = "01";
                        }
                        else
                        {
                            Variable.NightTime[min + 29 + 540] = "10";
                        }
                    }
                }
                if ((hour * 100 + min) >= (5 * 100 + 30) && (hour * 100 + min) < (6 * 100 + 30))
                {
                    if (min > 30)
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.NightTime[min - 31 + 600] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.NightTime[min - 31 + 600] = "01";
                        }
                        else
                        {
                            Variable.NightTime[min - 31 + 600] = "10";
                        }
                    }
                    else
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.NightTime[min + 29 + 600] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.NightTime[min + 29 + 600] = "01";
                        }
                        else
                        {
                            Variable.NightTime[min + 29 + 600] = "10";
                        }
                    }
                }
                if ((hour * 100 + min) >= (6 * 100 + 30) && (hour * 100 + min) < (7 * 100 + 30))
                {
                    if (min > 30)
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.NightTime[min - 31 + 660] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.NightTime[min - 31 + 660] = "01";
                        }
                        else
                        {
                            Variable.NightTime[min - 31 + 660] = "10";
                        }
                    }
                    else
                    {
                        if (Variable.MachineState == Variable.MachineStatus.Running)
                        {
                            Variable.NightTime[min + 29 + 660] = "00";

                        }
                        else if (Variable.MachineState == Variable.MachineStatus.Alarm)
                        {
                            Variable.NightTime[min + 29 + 660] = "01";
                        }
                        else
                        {
                            Variable.NightTime[min + 29 + 660] = "10";
                        }
                    }
                }

                #endregion

                #region 白班

                for (int i = 0; i < 720; i++)
                {
                    if (Variable.DayTime[i] == "00")
                    {
                        //创建绿色pen对象
                        Pen MyPen = new Pen(Color.Green, 1f);
                        //确定起点和终点
                        Point pt1 = new Point(120 + i, 110);
                        Point pt2 = new Point(120 + i, 170);
                        //使用DrawLine方法绘制直线
                        GPS.DrawLine(MyPen, pt1, pt2);
                    }
                    else if (Variable.DayTime[i] == "01")
                    {
                        //创建绿色pen对象
                        Pen MyPen = new Pen(Color.Red, 1f);
                        //确定起点和终点
                        Point pt1 = new Point(120 + i, 110);
                        Point pt2 = new Point(120 + i, 170);
                        //使用DrawLine方法绘制直线
                        GPS.DrawLine(MyPen, pt1, pt2);
                    }
                    else
                    {
                        //创建绿色pen对象
                        Pen MyPen = new Pen(Color.Orange, 1f);
                        //确定起点和终点
                        Point pt1 = new Point(120 + i, 110);
                        Point pt2 = new Point(120 + i, 170);
                        //使用DrawLine方法绘制直线
                        GPS.DrawLine(MyPen, pt1, pt2);
                    }

                }

                #endregion

                #region 夜班

                for (int i = 0; i < 720; i++)
                {
                    if (Variable.NightTime[i] == "00")
                    {
                        //创建绿色pen对象
                        Pen MyPen = new Pen(Color.Green, 1f);
                        //确定起点和终点
                        Point pt1 = new Point(120 + i, 325);
                        Point pt2 = new Point(120 + i, 385);
                        //使用DrawLine方法绘制直线
                        GPS.DrawLine(MyPen, pt1, pt2);
                    }
                    else if (Variable.NightTime[i] == "01")
                    {
                        //创建绿色pen对象
                        Pen MyPen = new Pen(Color.Red, 1f);
                        //确定起点和终点
                        Point pt1 = new Point(120 + i, 325);
                        Point pt2 = new Point(120 + i, 385);
                        //使用DrawLine方法绘制直线
                        GPS.DrawLine(MyPen, pt1, pt2);
                    }
                    else
                    {
                        //创建绿色pen对象
                        Pen MyPen = new Pen(Color.Orange, 1f);
                        //确定起点和终点
                        Point pt1 = new Point(120 + i, 325);
                        Point pt2 = new Point(120 + i, 385);
                        //使用DrawLine方法绘制直线
                        GPS.DrawLine(MyPen, pt1, pt2);
                    }

                }

                #endregion

                myTXT.WriteTxt(Variable.DayTime, Application.StartupPath + @"\Data\DayTime\Time");
                myTXT.WriteTxt(Variable.NightTime, Application.StartupPath + @"\Data\NightTime\Time");

                Thread.Sleep(1);
            }
        }

        #endregion


    }
}
