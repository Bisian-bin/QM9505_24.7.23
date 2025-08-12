using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QM9505
{
    public partial class POPForm : Form
    {
        Access access = new Access();
        public POPForm()
        {
            InitializeComponent();
        }

        private void POPForm_Load(object sender, EventArgs e)
        {
            if (Variable.OPsure)
            {
                label1.Visible = true;
                textBox1.Visible = true;
                label2.Visible = true;
                textBox2.Visible = true;
            }
            else
            {
                label1.Visible = false;
                textBox1.Visible = false;
                label2.Visible = false;
                textBox2.Visible = false;
            }
        }

        #region 确定
        private void btnSure_Click(object sender, EventArgs e)
        {
            if (Variable.OPsure)
            {
                if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
                {
                    bool b = OPToJudge(textBox1.Text, textBox2.Text);
                    if (b)
                    {
                        if (Variable.step != "" && Variable.cancelStep != 0 && Variable.sureStep != 0)
                        {
                            switch (Variable.step)
                            {
                                case "Variable.AutoStep":
                                    {
                                        Variable.AutoStep = Convert.ToInt32(Variable.sureStep);
                                        break;
                                    }
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
                        access.RecordAccess(LogType.Alarm1, "OP号:" + textBox1.Text + "操作--" + LabelX1.Text);

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
                        timerScan.Stop();
                        if (pictureBox.Image != null)
                        {
                            pictureBox.Image.Dispose();
                        }

                        textBox1.Text = "";
                        textBox2.Text = "";
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("当前OP号没有权限操作此报警，请确认！");
                    }
                }
                else
                {
                    MessageBox.Show("请输入OP账号和密码，在消弹窗！");
                }
            }
            else
            {
                if (Variable.step != "" && Variable.cancelStep != 0 && Variable.sureStep != 0)
                {
                    switch (Variable.step)
                    {
                        case "Variable.AutoStep":
                            {
                                Variable.AutoStep = Convert.ToInt32(Variable.sureStep);
                                break;
                            }
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
                timerScan.Stop();
                if (pictureBox.Image != null)
                {
                    pictureBox.Image.Dispose();
                }

                textBox1.Text = "";
                textBox2.Text = "";
                this.Close();
            }
        }

        #endregion

        #region 取消/跳过
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (Variable.OPsure)
            {
                if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
                {
                    bool b = OPToJudge(textBox1.Text, textBox2.Text);
                    if (b)
                    {
                        if (Variable.step != "" && Variable.cancelStep != 0 && Variable.sureStep != 0)
                        {
                            switch (Variable.step)
                            {
                                case "Variable.AutoStep":
                                    {
                                        Variable.AutoStep = Convert.ToInt32(Variable.cancelStep);
                                        break;
                                    }
                                case "Variable.INAutoReady1Step":
                                    {
                                        Variable.INAutoReady1Step = Convert.ToInt32(Variable.cancelStep);
                                        break;
                                    }
                                case "Variable.INAutoReadyStep":
                                    {
                                        Variable.INAutoReadyStep = Convert.ToInt32(Variable.cancelStep);
                                        break;
                                    }
                                case "Variable.INAutoEmptyStartStep":
                                    {
                                        Variable.INAutoEmptyStartStep = Convert.ToInt32(Variable.cancelStep);
                                        break;
                                    }
                                case "Variable.OutAutoOKStartStep":
                                    {
                                        Variable.OutAutoOKStartStep = Convert.ToInt32(Variable.cancelStep);
                                        break;
                                    }
                                case "Variable.OutAutoFillStartStep":
                                    {
                                        Variable.OutAutoFillStartStep = Convert.ToInt32(Variable.cancelStep);
                                        break;
                                    }
                                case "Variable.OutAutoNGStartStep":
                                    {
                                        Variable.OutAutoNGStartStep = Convert.ToInt32(Variable.cancelStep);
                                        break;
                                    }
                                case "Variable.RobotAutoStartStep":
                                    {
                                        Variable.RobotAutoStartStep = Convert.ToInt32(Variable.cancelStep);
                                        break;
                                    }
                                case "Variable.ModelSetStep":
                                    {
                                        Variable.ModelSetStep = Convert.ToInt32(Variable.cancelStep);
                                        break;
                                    }
                                case "Variable.ModelGetStep":
                                    {
                                        Variable.ModelGetStep = Convert.ToInt32(Variable.cancelStep);
                                        break;
                                    }
                            }
                        }

                        //写入数据库
                        access.RecordAccess(LogType.Alarm1, "OP号:" + textBox1.Text + "操作--" + LabelX1.Text);

                        Variable.OPsure = false;
                        Variable.OPNum = "";
                        Variable.OPPass = "";

                        Variable.POPFlag = false;
                        Variable.btnReset = true;
                        Variable.step = "";
                        Variable.cancelStep = 0;
                        Variable.sureStep = 0;
                        timerScan.Stop();
                        if (pictureBox.Image != null)
                        {
                            pictureBox.Image.Dispose();
                        }

                        textBox1.Text = "";
                        textBox2.Text = "";
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("当前OP号没有权限操作此报警，请确认！");
                    }
                }
                else
                {
                    MessageBox.Show("请输入OP账号和密码，在消弹窗！");
                }
            }
            else
            {
                if (Variable.step != "" && Variable.cancelStep != 0 && Variable.sureStep != 0)
                {
                    switch (Variable.step)
                    {
                        case "Variable.AutoStep":
                            {
                                Variable.AutoStep = Convert.ToInt32(Variable.cancelStep);
                                break;
                            }
                        case "Variable.INAutoReady1Step":
                            {
                                Variable.INAutoReady1Step = Convert.ToInt32(Variable.cancelStep);
                                break;
                            }
                        case "Variable.INAutoReadyStep":
                            {
                                Variable.INAutoReadyStep = Convert.ToInt32(Variable.cancelStep);
                                break;
                            }
                        case "Variable.INAutoEmptyStartStep":
                            {
                                Variable.INAutoEmptyStartStep = Convert.ToInt32(Variable.cancelStep);
                                break;
                            }
                        case "Variable.OutAutoOKStartStep":
                            {
                                Variable.OutAutoOKStartStep = Convert.ToInt32(Variable.cancelStep);
                                break;
                            }
                        case "Variable.OutAutoFillStartStep":
                            {
                                Variable.OutAutoFillStartStep = Convert.ToInt32(Variable.cancelStep);
                                break;
                            }
                        case "Variable.OutAutoNGStartStep":
                            {
                                Variable.OutAutoNGStartStep = Convert.ToInt32(Variable.cancelStep);
                                break;
                            }
                        case "Variable.RobotAutoStartStep":
                            {
                                Variable.RobotAutoStartStep = Convert.ToInt32(Variable.cancelStep);
                                break;
                            }
                        case "Variable.ModelSetStep":
                            {
                                Variable.ModelSetStep = Convert.ToInt32(Variable.cancelStep);
                                break;
                            }
                        case "Variable.ModelGetStep":
                            {
                                Variable.ModelGetStep = Convert.ToInt32(Variable.cancelStep);
                                break;
                            }
                    }
                }

                Variable.POPFlag = false;
                Variable.btnReset = true;
                Variable.step = "";
                Variable.cancelStep = 0;
                Variable.sureStep = 0;
                timerScan.Stop();
                if (pictureBox.Image != null)
                {
                    pictureBox.Image.Dispose();
                }

                textBox1.Text = "";
                textBox2.Text = "";
                this.Close();
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

        private void timerScan_Tick(object sender, EventArgs e)
        {
            Variable.OPNum = textBox1.Text;
            Variable.OPPass = textBox2.Text;
            if (!Variable.POPFlag)//((Variable.PauseButton == true || Variable.btnStop == true) &&
            {
                Variable.AlarmFlag = false;
                textBox1.Text = "";
                textBox2.Text = "";
                //Variable.POPFlag = false;
                timerScan.Stop();
                if (pictureBox.Image != null)
                {
                    pictureBox.Image.Dispose();
                }
                this.Close();
            }
        }


    }
}
