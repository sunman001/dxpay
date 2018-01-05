using System;
using System.Windows.Forms;

namespace JMNOTICE
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
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                Application.ThreadException += Application_ThreadException;
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                //Application.Run(new MIAN());

                Application.Run(new FrmNoticeMonitor());
            }
            catch (Exception ex)
            {
                JMP.TOOL.AddLocLog.AddLog(1, 5, "----", "订单通知程序错误[入口]", string.Format("订单通知程序错误,原因:{0}", ex));
            }
        }
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            try
            {
                JMP.TOOL.AddLocLog.AddLog(1, 5, "----", "订单通知程序错误[线程异常]", string.Format("订单通知程序错误,原因:{0}", e.Exception));
                Environment.Exit(0);
            }
            catch
            {
                Environment.Exit(0);
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                JMP.TOOL.AddLocLog.AddLog(1, 5, "----", "订单通知程序错误[未处理异常]", string.Format("订单通知程序错误,原因:{0}", e.ExceptionObject));
                Environment.Exit(0);
            }
            catch
            {
                Environment.Exit(0);
            }
        }
    }
}
