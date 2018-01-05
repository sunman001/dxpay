using System;
using System.Windows.Forms;
using FluentScheduler;

namespace JmPay.PayChannelMonitor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
            JobManager.Initialize(new TaskRegistry());
        }
    }
}
