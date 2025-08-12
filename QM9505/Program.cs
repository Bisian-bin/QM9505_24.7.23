using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QM9505
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                BindExceptionHandler();//绑定程序中的异常处理   

                #region 应用程序的主入口点

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(MainForm.Instance);

                #endregion

            }
            catch (Exception ex)
            {
                string str = GetExceptionMsg(ex, string.Empty);
                LogUnhandledException(str);
                MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>        
        /// 绑定程序中的异常处理        
        /// </summary>        
        private static void BindExceptionHandler()
        {
            //设置应用程序处理异常方式：ThreadException处理           
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常            
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //处理未捕获的异常           
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }
        /// <summary>       
        /// 处理UI线程异常       
        /// </summary>       
        /// <param name="sender"></param>       
        /// <param name="e"></param>       
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.Exception, e.ToString());
            LogUnhandledException(str);
            MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>       
        /// 处理未捕获的异常       
        /// </summary>        
        /// <param name="sender"></param>       
        /// <param name="e"></param>       
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
            LogUnhandledException(str);
            MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 异常写入日志
        /// </summary>
        /// <param name="msg"></param>
        static void LogUnhandledException(string msg)
        {
            StreamWriter sw = new StreamWriter(Application.StartupPath + "\\ErrorRecord.txt", true);
            sw.WriteLine(msg);
            sw.Close();
        }

        /// <summary>
        /// 生成自定义异常消息
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();
        }

    }
}
