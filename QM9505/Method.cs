using System;
using System.Collections.Generic;
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
    public class Method
    {
        private object threadLock;
        public Method()
        {
            threadLock = new object();
        }

        /// <summary>
        /// 延时毫秒
        /// </summary>
        /// <param name="time">毫秒(999)</param>
        /// <returns></returns>
        public static void Delay(int time)
        {
            DateTime datetime = DateTime.Now;
            int s;
            do
            {
                TimeSpan timespan = DateTime.Now - datetime;
                s = timespan.Milliseconds;
                Application.DoEvents();
            } while (s < time);

        }
        /// <summary>
        /// 延时秒
        /// </summary>
        /// <param name="secound">秒(59)</param>
        public static void Delay_Second(int secound)
        {
            DateTime datetime = DateTime.Now;
            int s;
            do
            {
                TimeSpan timespan = DateTime.Now - datetime;
                s = timespan.Seconds;
                Application.DoEvents();
            } while (s < secound);

        }

        #region 将整数转换二进制
        /// <summary>
        /// 将整数转换二进制
        /// </summary>
        /// <param name="data">整数值</param>
        /// <param name="a">截取二进制位置</param>
        /// <returns></returns>
        public string IntToBin(int data, int a)
        {
            lock (threadLock)
            {
                string cnt;//记录转换过后二进制值
                cnt = Convert.ToString(data, 2);
                if (cnt.Length < 16)
                {
                    int c = cnt.Length;
                    for (int b = 0; b < (16 - c); b++)
                    {
                        cnt = "0" + cnt;
                    }
                }
                return cnt.Substring(15 - a, 1);
            }
        }

        public string IntToBin1(int data, int a)
        {
            lock (threadLock)
            {
                string cnt;//记录转换过后二进制值
                cnt = Convert.ToString(data, 2);
                if (cnt.Length < 16)
                {
                    int c = cnt.Length;
                    for (int b = 0; b < (16 - c); b++)
                    {
                        cnt = "0" + cnt;
                    }
                }
                return cnt.Substring(15 - a, 1);
            }
        }

        public string IntToBin2(int data, int a)
        {
            lock (threadLock)
            {
                string cnt;//记录转换过后二进制值
                cnt = Convert.ToString(data, 2);
                if (cnt.Length < 16)
                {
                    int c = cnt.Length;
                    for (int b = 0; b < (16 - c); b++)
                    {
                        cnt = "0" + cnt;
                    }
                }
                return cnt.Substring(15 - a, 1);
            }
        }

        public string IntToBin3(int data, int a)
        {
            lock (threadLock)
            {
                string cnt;//记录转换过后二进制值
                cnt = Convert.ToString(data, 2);
                if (cnt.Length < 16)
                {
                    int c = cnt.Length;
                    for (int b = 0; b < (16 - c); b++)
                    {
                        cnt = "0" + cnt;
                    }
                }
                return cnt.Substring(15 - a, 1);
            }
        }

        public string IntToBin10(int data, int a)
        {
            lock (threadLock)
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
        }

        #endregion









    }
}
