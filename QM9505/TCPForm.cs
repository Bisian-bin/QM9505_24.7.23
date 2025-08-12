using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QM9505
{
    public partial class TCPForm : Form
    {
        public delegate void ConnectServer();
        public event ConnectServer connectServer;

        TXT txt = new TXT();
        Thread refreshThread;
        Function function = new Function();
        public int formNum;

        public TCPForm()
        {
            InitializeComponent();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            formNum += 1;
            if (formNum > 1)
            {
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

        #region 窗体加载
        private void TCPForm_Load(object sender, EventArgs e)
        {
            if (Variable.Server1Connect)
            {
                photoConnect.Enabled = false;      //设置停止监听按钮启用状态
                photoUnconnect.Enabled = true;      //设置停止监听按钮启用状态
            }
            else
            {
                photoConnect.Enabled = true;    //设置启动监听按钮启用状态
                photoUnconnect.Enabled = false;      //设置停止监听按钮启用状态
            }

            if (Variable.Server2Connect)
            {
                robotConnect.Enabled = false;      //设置停止监听按钮启用状态
                robotUnconnect.Enabled = true;      //设置停止监听按钮启用状态
            }
            else
            {
                robotConnect.Enabled = true;    //设置启动监听按钮启用状态
                robotUnconnect.Enabled = false;      //设置停止监听按钮启用状态
            }

            if (Variable.Server3Connect)
            {
                qrConnect.Enabled = false;      //设置停止监听按钮启用状态
                qrUnconnect.Enabled = true;      //设置停止监听按钮启用状态
            }
            else
            {
                qrConnect.Enabled = true;    //设置启动监听按钮启用状态
                qrUnconnect.Enabled = false;      //设置停止监听按钮启用状态
            }

            //开启刷新线程
            refreshThread = new Thread(refurbish);
            refreshThread.IsBackground = true;
            refreshThread.Start();
        }

        #endregion

        #region 刷新
        public void refurbish()
        {
            while (true)
            {
                if (Variable.PhotoRecMessage != "" && Variable.PhotoRecMessage != null)
                {
                    photoMsgRec.AppendText(Variable.PhotoRecMessage + "\r\n");
                    Variable.PhotoRecMessage = "";
                }

                if (Variable.RobotRecMessage != "" && Variable.RobotRecMessage != null)
                {
                    robotMsgRec.AppendText(Variable.RobotRecMessage + "\r\n");
                    Variable.RobotRecMessage = "";
                }

                if (Variable.QRRecMessage != "" && Variable.QRRecMessage != null)
                {
                    qrMsgRec.AppendText(Variable.QRRecMessage + "\r\n");
                    Variable.QRRecMessage = "";
                }

                if (Variable.Server1Connect)
                {
                    photoClientIP.Text = "客户端IP：" + Variable.clientIP1;       //显示客户端IP
                    photoClientPort.Text = "客户端Port：" + Variable.clientport1;   //显示客户端端口号
                    photostate.Text = "状态：Connect";    //显示连接状态
                }
                else
                {
                    photoClientIP.Text = "客户端IP：";        //清空显示
                    photoClientPort.Text = "客户端Port：";    //清空显示
                    photostate.Text = "状态：Unconnected";    //状态显示
                    Variable.clientIP1 = "";       //显示客户端IP
                    Variable.clientport1 = "";    //显示客户端端口号
                }

                if (Variable.Server2Connect)
                {
                    robotClientIP.Text = "客户端IP：" + Variable.clientIP2;       //显示客户端IP
                    robotClientPort.Text = "客户端Port：" + Variable.clientport2;   //显示客户端端口号
                    robotstate.Text = "状态：Connect";    //显示连接状态
                }
                else
                {
                    robotClientIP.Text = "客户端IP：";        //清空显示
                    robotClientPort.Text = "客户端Port：";    //清空显示
                    robotstate.Text = "状态：Unconnected";    //状态显示
                    Variable.clientIP2 = "";       //显示客户端IP
                    Variable.clientport2 = "";    //显示客户端端口号
                }
                if (Variable.Server3Connect)
                {
                    qrClientIP.Text = "客户端IP：" + Variable.clientIP3;       //显示客户端IP
                    qrClientPort.Text = "客户端Port：" + Variable.clientport3;   //显示客户端端口号
                    qrstate.Text = "状态：Connect";    //显示连接状态
                }
                else
                {
                    qrClientIP.Text = "客户端IP：";        //清空显示
                    qrClientPort.Text = "客户端Port：";    //清空显示
                    qrstate.Text = "状态：Unconnected";    //状态显示
                    Variable.clientIP3 = "";       //显示客户端IP
                    Variable.clientport3 = "";    //显示客户端端口号
                }

                robotRecord.Text = Variable.messageRecord[0];

                Thread.Sleep(10);
            }
        }
        #endregion

        #region 通信测试

        #region 相机
        //相机服务器开启
        private void photoConnect_Click(object sender, EventArgs e)
        {
            try
            {
                bool b1 = PhotoTcpServer.StartListening();
                if (b1)
                {

                    photoMsgRec.AppendText(DateTime.Now + "开始监听！" + "\r\n");    //消息提示
                    photoConnect.Enabled = false;    //设置启动监听按钮启用状态
                    photoUnconnect.Enabled = true;      //设置停止监听按钮启用状态
                }
                else
                {
                    photoMsgRec.AppendText("启动监听失败！\r\n");    //消息提示
                }
            }
            catch
            {
                photoMsgRec.AppendText("启动监听失败！\r\n");    //消息提示
            }
        }

        //相机服务器断开
        private void photoUnconnect_Click(object sender, EventArgs e)
        {
            PhotoTcpServer.StopListening();
            photoMsgRec.AppendText(DateTime.Now + "停止监听！" + "\r\n");    //消息提示
            photoConnect.Enabled = true;    //设置启动监听按钮启用状态
            photoUnconnect.Enabled = false;      //设置停止监听按钮启用状态
        }

        //相机发送
        private void btnPhotoMegSend_Click(object sender, EventArgs e)
        {
            Variable.PhotoRecMessage = "";
            PhotoTcpServer.MessageSend(photoMegSend.Text);
            photoMegSend.Text = "";
        }

        //相机清空消息
        private void photoDataClean_Click(object sender, EventArgs e)
        {
            photoMsgRec.Text = "";    //清空消息显示
        }
        #endregion

        #region 机械手

        //机械手服务器开启
        private void robotConnect_Click(object sender, EventArgs e)
        {
            try
            {
                bool b1 = RobotTcpServer.StartListening();
                if (b1)
                {

                    robotMsgRec.AppendText(DateTime.Now + "开始监听！" + "\r\n");    //消息提示
                    robotConnect.Enabled = false;    //设置启动监听按钮启用状态
                    robotUnconnect.Enabled = true;      //设置停止监听按钮启用状态
                }
                else
                {
                    robotMsgRec.AppendText("启动监听失败！\r\n");    //消息提示
                }
            }
            catch
            {
                robotMsgRec.AppendText("启动监听失败！\r\n");    //消息提示
            }
        }

        //机械手服务器关闭
        private void robotUnconnect_Click(object sender, EventArgs e)
        {
            RobotTcpServer.StopListening();
            robotMsgRec.AppendText(DateTime.Now + "停止监听！" + "\r\n");    //消息提示
            robotConnect.Enabled = true;    //设置启动监听按钮启用状态
            robotUnconnect.Enabled = false;      //设置停止监听按钮启用状态
        }

        //机械手发送
        private void btnRobotMegSend_Click(object sender, EventArgs e)
        {
            Variable.RobotRecOK = false;
            Variable.RobotResetNG = false;
            Variable.RobotRecMessage = "";
            RobotTcpServer.MessageSend(robotMegSend.Text);
            if (robotMegSend.Text=="999")
            {
                Variable.RobotHomeStartStep = 999;
            }
            else
            {
                Variable.RobotTCPAutoStep = 1000;
            } 
            robotMegSend.Text = "";
        }

        //机械手清空消息
        private void robotDataClean_Click(object sender, EventArgs e)
        {
            robotMsgRec.Text = "";    //清空消息显示
        }



        #endregion

        #region QR

        //QR客户端开启
        private void qrConnect_Click(object sender, EventArgs e)
        {
            try
            {
                bool b1 = QRTcpClient.ConnectServer();
                if (b1)
                {
                    qrMsgRec.AppendText(DateTime.Now + "开始连接！" + "\r\n");    //消息提示
                    qrConnect.Enabled = false;    //设置启动按钮启用状态
                    qrUnconnect.Enabled = true;      //设置停止按钮启用状态
                }
                else
                {
                    qrMsgRec.AppendText("启动连接失败！\r\n");    //消息提示
                }
            }
            catch
            {
                qrMsgRec.AppendText("启动连接失败！\r\n");    //消息提示
            }
        }

        //QR客户端关闭
        private void qrUnconnect_Click(object sender, EventArgs e)
        {
            QRTcpClient.StopConnect();
            qrMsgRec.AppendText(DateTime.Now + "停止连接！" + "\r\n");    //消息提示
            qrConnect.Enabled = true;    //设置启动按钮启用状态
            qrUnconnect.Enabled = false;      //设置停止按钮启用状态
        }

        //QR客户端发送
        private void btnQRMegSend_Click(object sender, EventArgs e)
        {
            Variable.QRRecMessage = "";
            QRTcpClient.MessageSend(qrMegSend.Text);
            qrMegSend.Text = "";
        }

        //QR客户端清空消息
        private void qrDataClean_Click(object sender, EventArgs e)
        {
            qrMsgRec.Text = "";    //清空消息显示
        }

        #endregion

        #endregion

        #region 机械手复位

        private void btnTobotResst_Click(object sender, EventArgs e)
        {
            int RobotRestStep = 10;

            if (RobotRestStep == 10)
            {
                //判断机械手在不在自动状态
                if (Variable.XStatus[25])//机械手自动模式
                {
                    RobotRestStep = 20;
                    btnTobotResst.BackColor = Color.Yellow;
                }
                else
                {
                    MessageBox.Show("机械手不在自动模式下");
                }
            }

            if (RobotRestStep == 20)
            {
                if (Variable.XStatus[26])//判断是否有急停报警
                {
                    function.OutYON(15);//机械手急停复位
                    Thread.Sleep(500);
                    function.OutYOFF(15);
                }
                RobotRestStep = 30;
            }

            if (RobotRestStep == 30)//判断是否有急停报警
            {
                Thread.Sleep(500);
                if (!Variable.XStatus[26])
                {
                    RobotRestStep = 40;
                }
                else
                {
                    MessageBox.Show("机械手急停报警");
                }
            }

            if (RobotRestStep == 40)//报警消除
            {
                if (Variable.XStatus[22])//报警未消除
                {
                    function.OutYON(16);
                    Thread.Sleep(500);
                    function.OutYOFF(16);
                }
                RobotRestStep = 50;
            }

            if (RobotRestStep == 50)
            {
                Thread.Sleep(500);
                if (!Variable.XStatus[22])
                {
                    RobotRestStep = 60;
                }
                else
                {
                    MessageBox.Show("机械手报警未消除");
                }
            }

            if (RobotRestStep == 60)//上电
            {
                if (!Variable.XStatus[27])
                {
                    function.OutYON(13);//机械手上电
                    Thread.Sleep(500);
                    function.OutYOFF(13);
                }
                RobotRestStep = 70;
            }

            if (RobotRestStep == 70)//判断是否上电
            {
                Thread.Sleep(500);
                if (Variable.XStatus[27])
                {
                    RobotRestStep = 75;
                }
                else
                {
                    MessageBox.Show("机械手未上电");
                }
            }

            if (RobotRestStep == 75)//停止
            {
                function.OutYON(17);//机械手停止
                Thread.Sleep(500);
                function.OutYOFF(17);
                RobotRestStep = 80;
            }

            if (RobotRestStep == 80)//机械手复位启动
            {
                function.OutYON(14);//机械手复位启动
                Thread.Sleep(500);
                function.OutYOFF(14);
                RobotRestStep = 0;
                btnTobotResst.BackColor = Color.Transparent;
            }
        }












        #endregion




    }
}
