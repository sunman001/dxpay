using System.Collections.Generic;
using System.IO;

namespace JmPay.PayChannelMonitor.Util.LogManager
{
    /// <summary>
    /// 文件日志类
    /// </summary>
    public class FileLogger : ILogger
    {
        /// <summary>
        /// 日志存放目录
        /// </summary>
        private readonly string _logDirectory ;
        /// <summary>
        /// 日志存放目录
        /// </summary>
        /// <param name="logDirectory">日志目录</param>
        public FileLogger(string logDirectory)
        {
            _logDirectory = logDirectory;
        }
        public void Write(string content)
        {
            File.AppendAllText(_logDirectory, content);
        }

        public void Write(List<string> lines)
        {
            File.AppendAllLines(_logDirectory, lines);
        }
    }
}
