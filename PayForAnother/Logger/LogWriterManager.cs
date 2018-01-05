namespace PayForAnother.Logger
{
    /// <summary>
    /// 日志管理器
    /// </summary>
    public class LogWriterManager
    {
        /// <summary>
        /// 获取操作日志管理器
        /// </summary>
        public static ILogWriter GetPayForAnotherLogger
        {
            get
            {
                return new LogWriter();
            }
        }
    }
}