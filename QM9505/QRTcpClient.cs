using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace QM9505
{
    class QRTcpClient
    {
        public static TcpClient tcpClient;//服务端与客户端建立连接 
        public static Thread threadReceive;//接收客户端发送消息的线程
        public static NetworkStream newworkStream;//利用NetworkStream对象与远程主机发送数据或接收数据

        #region 开始连接
        public static bool ConnectServer()
        {
            bool flag = false;
            try
            {
                tcpClient = new TcpClient(Variable.serverIP3, Convert.ToInt32(Variable.serverport3));
                newworkStream = tcpClient.GetStream();      //利用TcpClient对象GetStream方法得到网络流
                string strIp = tcpClient.Client.LocalEndPoint.ToString();    //获取本地的ip地址和端口号
                string[] array = strIp.Split(':');                   //分割字符串
                Variable.clientIP3 = array[0];       //显示客户端IP
                Variable.clientport3 = array[1];   //显示客户端端口号
                threadReceive = new Thread(new ThreadStart(Receive));   //定义接收服务器数据的线程
                threadReceive.IsBackground = true;    //设置为后台线程
                threadReceive.Start();                //启动线程

                Variable.Server3Connect = true;
                flag = true;
            }
            catch
            {
                StopConnect();
                flag = false;
                //MessageBox.Show("QR连接失败");
            }
            return flag;
        }

        #endregion

        #region 停止连接
        public static void StopConnect()
        {
            if (tcpClient != null)
            {
                if (tcpClient.Connected)
                {
                    tcpClient.Close();  //如果有客户端连接，就关闭连接
                }
                if (threadReceive.ThreadState != ThreadState.Aborted)  //判断线程是否启用，是否跨线程调用
                {
                    threadReceive.Abort();    //结束接收服务器数据线程
                }

                tcpClient = null;
                GC.Collect();    //清理内存

                Variable.Server3Connect = false;
            }
        }
        #endregion

        #region 发送消息
        public static void MessageSend(string str)
        {
            try
            {
                if (tcpClient != null && tcpClient.Connected == true)   //判断客户端是否连接
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(str);//将字符串转换为byte数组
                    newworkStream.Write(buffer, 0, buffer.Length);    //客户端向服务器发送消息
                }
                else
                {
                    MessageBox.Show("QR服务器Unconnected,正在尝试重连......");
                    StopConnect();
                    ConnectServer();
                }
            }
            catch (Exception ex)
            {
                //Log.SaveError(new StackTrace(new StackFrame(true)), new StackFrame(), ex);
            }
        }
        #endregion

        #region 接收消息
        public static void Receive()
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[tcpClient.ReceiveBufferSize];  //定义消息接收缓冲区
                    int count = newworkStream.Read(buffer, 0, buffer.Length);//实际接收到的有效字节数
                    if (count == 0)    //count=0 表示客户端关闭，要退出循环
                    {
                        Variable.Server3Connect = false;
                        break;    //退出循环
                    }
                    else
                    {
                        //将字节数组转化成字符串
                        string RecMessage = Encoding.Default.GetString(buffer, 0, count).Trim('\0');    //从缓冲区中读取消息
                        //显示信息
                        Variable.QRRecMessage = RecMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                //Log.SaveError(new StackTrace(new StackFrame(true)), new StackFrame(), ex);
            }
        }
        #endregion

        

    }
}
