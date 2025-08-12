using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace QM9505
{
    public class RobotTcpServer
    {
        public static Thread AcceptSocketThread;//创建监听连接的线程
        public static Thread threadReceive;//接收客户端发送消息的线程

        public static TcpListener tcpListener;//监听套接字 TcpLestener与TcpClient类 在命名空间 using System.Net.Sockets下 
        public static TcpClient tcpClient;//服务端与客户端建立连接 
        public static NetworkStream newworkStream;//利用NetworkStream对象与远程主机发送数据或接收数据

        #region 开始监听
        public static bool StartListening()
        {
            bool flag = false;
            try
            {
                IPAddress ip = IPAddress.Parse(Variable.serverIP2);     //服务器IP地址
                tcpListener = new TcpListener(ip, Convert.ToInt32(Variable.serverport2));     //服务器端口号
                tcpListener.Start();     //开始侦听传入的连接请求。
                AcceptSocketThread = new Thread(new ThreadStart(StartListen));    //定义接收客户端连接的线程
                AcceptSocketThread.IsBackground = true;   //设置为后台线程
                AcceptSocketThread.Start();       //启动线程
                Variable.Server2Connect = true;
                flag = true;
            }
            catch
            {
                flag = false;
                //MessageBox.Show("Robot连接失败");
            }
            return flag;
        }

        #endregion

        #region 停止监控
        public static void StopListening()
        {
            if (tcpListener != null)
            {
                if (tcpClient != null)
                {
                    if (tcpClient.Connected) tcpClient.Close();  //如果有客户端连接，就关闭连接
                    if (threadReceive.ThreadState != ThreadState.Aborted)
                    {
                        threadReceive.Abort();    //结束接收客户端数据线程
                    }
                }
                tcpListener.Stop();     //停止端口监听
                if (AcceptSocketThread.ThreadState != ThreadState.Aborted)
                {
                    AcceptSocketThread.Abort();    //结束消息监听线程
                }

                GC.Collect();    //清理内存

                Variable.Server2Connect = false;
            }
        }
        #endregion

        #region 等待客户端的连接
        public static void StartListen()
        {
            try
            {
                while (true)
                {
                    tcpClient = tcpListener.AcceptTcpClient();  //等待客户端的连接
                    newworkStream = tcpClient.GetStream();      //利用TcpClient对象GetStream方法得到网络流
                    string strIp = tcpClient.Client.RemoteEndPoint.ToString();    //获取远程主机的ip地址和端口号
                    string[] array = strIp.Split(':');                   //分割字符串
                    Variable.Server2Connect = true;
                    Variable.clientIP2 = array[0];       //显示客户端IP
                    Variable.clientport2 = array[1];   //显示客户端端口号

                    threadReceive = new Thread(new ThreadStart(Receive));   //定义接收客户端数据的线程
                    threadReceive.IsBackground = true;    //设置为后台线程
                    threadReceive.Start();                //启动线程
                   
                }
            }
            catch (Exception ex)
            {
                //Log.SaveError(new StackTrace(new StackFrame(true)), new StackFrame(), ex);
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
                    newworkStream.Write(buffer, 0, buffer.Length);    //服务器向客户端发送消息
                    MessageLog("发送代码为:"+ str);
                }
                else
                {
                    //MessageBox.Show("机械手客户端Unconnected");
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
                        Variable.Server2Connect = false;
                        break;//退出循环
                    }
                    else
                    {
                        //将字节数组转化成字符串
                        string RecMessage = Encoding.Default.GetString(buffer, 0, count).Trim('\0');    //从缓冲区中读取消息
                        //显示信息
                        Variable.RobotRecMessage = RecMessage;
                        MessageLog("接受数据为:" + RecMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                //Log.SaveError(new StackTrace(new StackFrame(true)), new StackFrame(), ex);
            }
        }
        #endregion

        #region 记录发送接收数据

        public static void MessageLog(string message)
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
            catch (Exception ex)
            {
                //Log.SaveError(new StackTrace(new StackFrame(true)), new StackFrame(), ex);
            }
        }

        #endregion




    }
}
