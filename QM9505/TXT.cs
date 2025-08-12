using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QM9505
{
    public class TXT
    {
        private object threadLock;
        public TXT()
        {
            threadLock = new object();
        }

        #region 从TXT读取数据

        public string[] ReadTXT(string path1)
        {
            lock (threadLock)
            {
                string path = path1 + ".txt";

                // 创建泛型列表
                List<string> list = new List<string>();
                // 打开数据文件 E:\data.txt逐行读入
                if (File.Exists(path))
                {
                    StreamReader rd = File.OpenText(path);
                    string line;
                    while ((line = rd.ReadLine()) != null)
                    {
                        string[] data = line.Split(",".ToCharArray());//按逗号将数据分割成一个数组
                        list.AddRange(data);
                    }
                    // 关闭文件
                    rd.Close();

                    // 将泛型列表转换成数组
                }
                else
                {
                    //list.AddRange(listintial());

                }
                return list.ToArray();
            }
        }

        #endregion

        #region 从TXT读取数据，删除最后结尾的一个逗号
        public string[] ReadTXT1(string path1)
        {
            lock (threadLock)
            {
                string path = path1 + ".txt";

                // 创建泛型列表
                List<string> list = new List<string>();
                // 打开数据文件 E:\data.txt逐行读入
                if (File.Exists(path))
                {
                    StreamReader rd = File.OpenText(path);
                    string line = rd.ReadLine();
                    string line1 = line.Substring(0, line.Length - 1);// 删除最后结尾的一个逗号
                    //while (line1 != null)
                    //{
                    string[] data = line1.Split(",".ToCharArray());//按逗号将数据分割成一个数组
                    list.AddRange(data);
                    //}
                    // 关闭文件
                    rd.Close();
                }
                else
                {
                    //list.AddRange(listintial());

                }
                // 将泛型列表转换成数组
                return list.ToArray();
            }
        }

        #endregion

        #region 向Tray中TXT写入一行固定数据
        public void WriteTxt(string[] log, string path1,string list)
        {
            lock (threadLock)
            {
                int listNum = Convert.ToInt32(list);
                string str = null;
                string path = path1 + ".txt";
                FileStream fs = new FileStream(path, FileMode.Create);
                StreamWriter wr = null;
                wr = new StreamWriter(fs);
                int length = log.Length / listNum;

                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < listNum; j++)
                    {
                        if (!(log[j] == null))
                        {
                            if (j < listNum - 1)
                            {
                                str += log[j + i * listNum] + ",";
                            }
                            else
                            {
                                str += log[j + i * listNum];
                            }
                        }
                    }
                    wr.WriteLine(str);
                    str = null;
                }
                wr.Flush();
                wr.Close();
            }
        }
        #endregion

        #region 向Tray中TXT写入可变数据
        public void WriteTxt(string[] log, string path1)
        {
            lock (threadLock)
            {
                string str = null;
                string path = path1 + ".txt";
                FileStream fs = new FileStream(path, FileMode.Create);
                StreamWriter wr = null;
                wr = new StreamWriter(fs);

                for (int i = 0; i < log.Length; i++)
                {
                    if (!(log[i] == null))
                    {
                        if (i < log.Length - 1)
                        {
                            str += log[i] + ",";
                        }
                        else
                        {
                            str += log[i];
                        }
                    }
                }
                wr.WriteLine(str);
                wr.Flush();
                wr.Close();
            }
        }
        #endregion

        #region 读取TXT文件名

        public string[] ReadFileName(string path1, string path2)
        {
            lock (threadLock)
            {
                string path = @"D:\" + path1 + "\\" + path2;
                string[] name = Directory.GetFiles(path);
                return name;
            }
        }

        #endregion

        #region 写入TXT文件名

        public void WriteFileName(string path1, string path2, string path3)
        {
            lock (threadLock)
            {
                string path = @"D:\" + path1 + "\\";
                string resultData = path + path2 + @"\" + path3 + ".txt";
                foreach (string file in Directory.GetFileSystemEntries(path))
                {
                    if (!File.Exists(file))
                    {
                        FileStream fs = new FileStream(resultData, FileMode.Create);
                        fs.Flush();
                        fs.Close();
                    }
                }
            }
        }

        #endregion

        #region 删除txt文档
        public void DeleteTXT(string path1, string path2, string name)
        {
            lock (threadLock)
            {
                string path = @"D:\" + path1 + "\\" + path2;
                foreach (string file in Directory.GetFileSystemEntries(path))
                {
                    if (File.Exists(file))
                    {
                        string mamefile = Path.GetFileNameWithoutExtension(file);
                        if (mamefile.StartsWith(name))
                        {
                            File.Delete(file);
                        }
                    }
                }
            }

        }

        #endregion    

        #region 读取TXT数据到DataGridView方法
        public void ReadTxtToDataGridMethod(DataGridView dataGridView, string[] str)
        {
            lock (threadLock)
            {
                for (int i = 0; i < (int)Variable.RowNum; i++)
                {
                    string[] ss = str.Skip(i * 8).Take(8).ToArray();

                    for (int j = 0; j < (int)Variable.ListNum; j++)
                    {
                        if (ss[j] == "00")
                        {
                            dataGridView.Rows[i].Cells[j].Style.BackColor = Color.Green;
                        }
                        else if (ss[j] == "10")
                        {
                            dataGridView.Rows[i].Cells[j].Style.BackColor = Color.White;
                        }
                        else
                        {
                            dataGridView.Rows[i].Cells[j].Style.BackColor = Color.Red;
                        }
                    }
                }
            }

        }



        #endregion

        #region 读取TXT数据到DataGridView3方法
        public void ReadTxtToDataGridMethod1(DataGridView dataGridView, string[] str)
        {
            lock (threadLock)
            {
                for (int i = 0; i < (int)Variable.RowNum; i++)
                {
                    string[] ss = str.Skip(i * 8).Take(8).ToArray();

                    for (int j = 0; j < (int)Variable.ListNum; j++)
                    {
                        if (ss[j] == "00")
                        {
                            dataGridView.Rows[i].Cells[j].Style.BackColor = Color.LightSkyBlue;
                        }
                        else if (ss[j] == "01")
                        {
                            dataGridView.Rows[i].Cells[j].Style.BackColor = Color.Pink;
                        }
                    }
                }
            }

        }



        #endregion

        #region 创建文件夹

        /// <summary>
        /// 创建年月
        /// </summary>
        /// <returns></returns>
        public string FileName()
        {
            lock (threadLock)
            {
                //文件名字 年月
                string path1 = @"D:\QM9505Repot\";
                string path2 = System.DateTime.Now.ToString("yyyyMM");
                string fileName = Path.Combine(path1, path2);
                //string fileName1 = Path.Combine(fileName, path3);
                //如果不存在这个文件
                if (!Directory.Exists(fileName))
                {
                    Directory.CreateDirectory(fileName);//创建文件
                }
                string path3 = FileName1(fileName);
                return path3;
            }
        }

        /// <summary>
        /// 创建日
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string FileName1(string fileName)
        {
            lock (threadLock)
            {
                //文件名字 日
                string path1 = fileName;
                string path2 = System.DateTime.Now.ToString("dd");
                string path3 = Path.Combine(path1, path2);

                //如果不存在这个文件
                if (!Directory.Exists(path3))
                {
                    Directory.CreateDirectory(path3);//创建文件
                }

                return path3;
            }
        }

        #endregion

        #region 创建报表TXT
        public void RecordDataToTxt()
        {
            lock (threadLock)
            {
                string fileName = FileName();
                string path1 = DateTime.Now.ToString("yyyyMMddHHmmss");
                string path2 = Variable.BatchNum;
                string resultData = fileName + "\\" + path1 + "_" + path2 + ".txt";
                FileStream fs = new FileStream(resultData, FileMode.Create);
                StreamWriter wr = null;
                wr = new StreamWriter(fs);
                wr.WriteLine("设备生产报表");
                wr.WriteLine("——————————————————————————————————————————");

                wr.WriteLine("******************************************");
                wr.WriteLine("生产信息:");
                wr.WriteLine("******************************************");
                wr.WriteLine(string.Format("{0}{1}", alignmentStrFunc("机台标识:", 20), alignmentStrFunc("QM9505", 20)));
                wr.WriteLine(string.Format("{0}{1}", alignmentStrFunc("操作员:", 21), alignmentStrFunc(Variable.OP, 20)));
                wr.WriteLine(string.Format("{0}{1}", alignmentStrFunc("批号:", 22), alignmentStrFunc(Variable.BatchNum, 20)));
                wr.WriteLine(string.Format("{0}{1}", alignmentStrFunc("工单号:", 21), alignmentStrFunc(Variable.OrderNum, 20)));
                wr.WriteLine(string.Format("{0}{1}", alignmentStrFunc("PO号:", 22), alignmentStrFunc(Variable.PONum, 20)));
                wr.WriteLine(string.Format("{0}{1}", alignmentStrFunc("档案名称:", 20), alignmentStrFunc(Variable.FileName, 20)));
                wr.WriteLine(string.Format("{0}{1}", alignmentStrFunc("开始时间:", 20), alignmentStrFunc(Variable.startTime, 20)));
                wr.WriteLine(string.Format("{0}{1}", alignmentStrFunc("结束时间:", 20), alignmentStrFunc(Variable.endTime, 20)));
                wr.WriteLine(string.Format("{0}{1}", alignmentStrFunc("运行时间:", 20), alignmentStrFunc(Variable.runTime, 20)));
                wr.WriteLine(string.Format("{0}{1}", alignmentStrFunc("停止时间:", 20), alignmentStrFunc(Variable.stopTime, 20)));
                wr.WriteLine(string.Format("{0}{1}", alignmentStrFunc("报警时间:", 20), alignmentStrFunc(Variable.alarmTime, 20)));
                wr.WriteLine(string.Format("{0}{1}", alignmentStrFunc("jamRate:", 20), alignmentStrFunc(Variable.jamRate, 20)));
                wr.WriteLine(string.Format("{0}{1}", alignmentStrFunc("UPH:", 22), alignmentStrFunc(Variable.UPH.ToString(), 20)));
                wr.WriteLine(string.Format("{0}{1}", alignmentStrFunc("运行模式:", 20), alignmentStrFunc(Variable.runModel, 20)));

                wr.WriteLine("******************************************");
                wr.WriteLine("产量信息统计:");
                wr.WriteLine("******************************************");
                wr.WriteLine(string.Format("{0}{1}", alignmentStrFunc("输入数量:", 20), alignmentStrFunc(Variable.inChipNum.ToString(), 20)));
                wr.WriteLine(string.Format("{0}{1}", alignmentStrFunc("输出数量:", 20), alignmentStrFunc(Variable.outChipNum.ToString(), 20)));
                wr.WriteLine(string.Format("{0}{1}", alignmentStrFunc("良品数量:", 20), alignmentStrFunc(Variable.OKChipNum.ToString(), 20)));
                wr.WriteLine(string.Format("{0}{1}", alignmentStrFunc("良品率:", 21), alignmentStrFunc(Variable.Yield.ToString() + "%", 20)));

                wr.Flush();
                wr.Close();
            }

        }
        #endregion

        #region 历史报警创建文件夹

        /// <summary>
        /// 历史报警创建年月
        /// </summary>
        /// <returns></returns>
        public string FileNameHistory()
        {
            lock (threadLock)
            {
                //文件名字 年月
                string path1 = @"D:\HistoryLog\";
                string path2 = System.DateTime.Now.ToString("yyyyMM");
                string fileName = Path.Combine(path1, path2);
                //string fileName1 = Path.Combine(fileName, path3);
                //如果不存在这个文件
                if (!Directory.Exists(fileName))
                {
                    Directory.CreateDirectory(fileName);//创建文件
                }
                string path3 = FileName1History(fileName);
                return path3;
            }
        }

        /// <summary>
        /// 历史报警创建日
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string FileName1History(string fileName)
        {
            lock (threadLock)
            {
                //文件名字 日
                string path1 = fileName;
                string path2 = System.DateTime.Now.ToString("dd");
                string path3 = Path.Combine(path1, path2);

                //如果不存在这个文件
                if (!Directory.Exists(path3))
                {
                    Directory.CreateDirectory(path3);//创建文件
                }

                return path3;
            }
        }

        #endregion              

        #region 保存发送测试数据
        public void writeSendTxt(string set)
        {
            try
            {
                string time = DateTime.Now.ToString("yyyyMMdd");
                string path = @"D:\Send\" + time + ".txt";
                //判断文件是否存在，没有则创建。
                if (!System.IO.File.Exists(path))
                {
                    FileStream stream = System.IO.File.Create(path);
                    stream.Close();
                    stream.Dispose();
                }
                //写入日志
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(set);
                }
                long size = 0;
                //获取文件大小
                using (FileStream file = System.IO.File.OpenRead(path))
                {
                    size = file.Length;//文件大小。byte
                }

                //判断日志文件大于2M，自动删除。
                if (size > (1024 * 4 * 512))
                {
                    System.IO.File.Delete(path);
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(new StackTrace(new StackFrame(true)), new StackFrame(), ex);
            }
        }

        #endregion

        #region 保存接受测试数据

        /// <summary>
        /// 将文本内容写入至TXT文档里面
        /// </summary>
        /// <param name="Alarm"></param>
        public void writeReceiveTxt(string get)
        {
            try
            {
                string time = DateTime.Now.ToString("yyyyMMdd");
                string path = @"D:\Receive\" + time + ".txt";
                //判断文件是否存在，没有则创建。
                if (!System.IO.File.Exists(path))
                {
                    FileStream stream = System.IO.File.Create(path);
                    stream.Close();
                    stream.Dispose();
                }
                //写入日志
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(get);
                }
                long size = 0;
                //获取文件大小
                using (FileStream file = System.IO.File.OpenRead(path))
                {
                    size = file.Length;//文件大小。byte
                }

                //判断日志文件大于2M，自动删除。
                if (size > (1024 * 4 * 512))
                {
                    System.IO.File.Delete(path);
                }
            }
            catch (Exception ex)
            {
                Log.SaveError(new StackTrace(new StackFrame(true)), new StackFrame(), ex);
            }
        }

        #endregion

        #region 格式
        ///<summary>
        ///生成固定长度的空格字符串
        ///</summary>
        ///<paramname="length"></param>
        ///<returns></returns>
        private string SpaceStrFunc(int length)
        {
            string strReturn = string.Empty;
            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    strReturn += " ";
                }
            }
            return strReturn;
        }

        ///<summary>
        ///将字符串生转化为固定长度左对齐，右补空格
        ///</summary>
        ///<paramname="strTemp"></param>需要补齐的字符串
        ///<paramname="length"></param>补齐后的长度
        ///<returns></returns>
        private string alignmentStrFunc(string strTemp, int length)
        {
            byte[] byteStr = System.Text.Encoding.Default.GetBytes(strTemp.Trim());
            int iLength = byteStr.Length;
            int iNeed = length - iLength;
            byte[] spaceLen = Encoding.Default.GetBytes(" "); //一个空格的长度
            iNeed = iNeed / spaceLen.Length;
            string spaceString = SpaceStrFunc(iNeed);
            return strTemp + spaceString;
        }
        #endregion

        #region 计算总数方法
        public void Num(string str)
        {
            lock (threadLock)
            {
                //读取Map文件
                string st = Application.StartupPath + @"\Map\" + str + @"\tray";
                string[] ReadMap = ReadTXT(st);

                string strPhoto = Application.StartupPath + @"\Data\Photo\Down\tray";
                string[] ReadPhoto = ReadTXT(strPhoto);

                for (int i = 0; i < ReadPhoto.Length; i++)
                {
                    if (ReadPhoto[i] == "10")
                    {
                        ReadMap[i] = "03";//去掉拍照空位
                    }
                }

                if (Variable.Num != "")
                {
                    SelectTotalNum(ReadMap);//计算总数
                    SelectOKNum(ReadMap);//计算OK
                    Variable.Num = "";
                }

            }
        }
        #endregion

        #region 计算总数

        public void SelectTotalNum(string[] ReadNum)
        {
            lock (threadLock)
            {
                for (int i = 0; i < ReadNum.Length; i++)
                {
                    if (ReadNum[i] != "03")
                    {
                        Variable.TotalNum += 1;
                    }
                }
            }
        }
        #endregion

        #region 计算OK数

        public void SelectOKNum(string[] ReadNum)
        {
            lock (threadLock)
            {
                for (int i = 0; i < ReadNum.Length; i++)
                {
                    if (ReadNum[i] == "00")
                    {
                        Variable.OKNum += 1;
                    }
                }
            }
        }
        #endregion

        //     string Msg = "要显示的信息";

        //     /// 显示字符串包含空格的总长度
        //     int strLength = 200;         

        //     /// 左补空格，相当于是右对齐
        //     Msg = Msg.PadLeft(strLength);

        //     /// 右补空格，相当于是左对齐
        //     Msg = Msg.PadRight(strLength);



    }
}
