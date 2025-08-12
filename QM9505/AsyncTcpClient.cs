using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QM9505
{
    public class AsyncTcpClient
    {
        TcpClient tcpClient;
        byte[] ReadBytes = new byte[1024];
        bool isTryingToCon = false;
        bool IsClose = false;

        #region 连接服务器
        public void ConnectServer()
        {
            //防止多个事例去重复连接
            if (isTryingToCon)
            {
                return;
            }
            try
            {
                Variable.Server3Connect = false;
                if (tcpClient != null)
                {
                    tcpClient.Close();
                }

                tcpClient = new TcpClient();
                isTryingToCon = true;
                //开始异步
                tcpClient.BeginConnect(IPAddress.Parse(Variable.serverIP3), Convert.ToInt32(Variable.serverport3), Connectionjudgment, null);
                Thread.Sleep(200);
                string strIp = tcpClient.Client.LocalEndPoint.ToString();    //获取本地的ip地址和端口号
                string[] array = strIp.Split(':');                   //分割字符串
                Variable.clientIP3 = array[0];       //显示客户端IP
                Variable.clientport3 = array[1];   //显示客户端端口号
                Variable.Server3Connect = true;
            }
            catch (Exception ex)
            {
                Log.SaveError(new StackTrace(new StackFrame(true)), new StackFrame(), ex);
            }
        }

        #endregion

        #region 连接判断
        void Connectionjudgment(IAsyncResult ar)
        {
            if (IsClose)
            {
                return;
            }
            if (tcpClient.Connected == false)
            {
                tcpClient.Close();
                tcpClient = new System.Net.Sockets.TcpClient();
                //尝试重连。。。。。。
                tcpClient.BeginConnect(IPAddress.Parse(Variable.serverIP3), Convert.ToInt32(Variable.serverport3), Connectionjudgment, null);
            }
            else
            {
                //连接上了
                isTryingToCon = false;
                Variable.Server3Connect = true;
                //结束异步连接
                tcpClient.EndConnect(ar);
                //读取数据
                tcpClient.GetStream().BeginRead(ReadBytes, 0, ReadBytes.Length, ReceiveCallBack, null);
            }
        }

        #endregion

        #region 断开连接
        public void Close()
        {
            IsClose = true;
            if (tcpClient != null && tcpClient.Client.Connected)
            {
                tcpClient.Close();
            }
            if (!tcpClient.Client.Connected)
            {
                tcpClient.Close();//断开挂起的异步连接
            }

            Variable.Server3Connect = false;
        }

        #endregion

        #region 发送消息
        public void SendMsg(string msg)
        {
            try
            {
                //开始异步发送
                byte[] msgBytes = Encoding.UTF8.GetBytes(msg);
                tcpClient.GetStream().BeginWrite(msgBytes, 0, msgBytes.Length, (ar) =>
                {
                    tcpClient.GetStream().EndWrite(ar);//结束异步发送
                }, null);
            }
            catch 
            {
                //尝试重连。。。。。。
                ConnectServer();
            }
        }

        #endregion

        #region 接受消息
        void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                int len = tcpClient.GetStream().EndRead(ar);//结束异步读取
                if (len > 0)
                {
                    string RecMessage = Encoding.UTF8.GetString(ReadBytes, 0, len);
                    //显示信息
                    Variable.QRRecMessage = RecMessage;

                    //重置数据,防止旧数据残留
                    ReadBytes = new byte[1024];
                    tcpClient.GetStream().BeginRead(ReadBytes, 0, ReadBytes.Length, ReceiveCallBack, null);
                }
                else
                {
                    isTryingToCon = false;
                    //尝试重连。。。。。。
                    ConnectServer();
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
