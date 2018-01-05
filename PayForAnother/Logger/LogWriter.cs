using TOOL.Extensions;

namespace PayForAnother.Logger
{
    public class LogWriter : ILogWriter
    {
        private void Write(string summary,string message)
        {
            LogManager.GetPayForAnotherLogger.Log(summary, message);
        }
       
        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="summary">日志摘要</param>
        /// <param name="message">日志内容</param>
        public void PayForAnotherLog(string summary, string message)
        {
            Write(summary, message);
        }
    }
}