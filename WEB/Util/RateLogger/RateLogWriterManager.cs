namespace WEB.Util.RateLogger
{
    /// <summary>
    /// 日志管理器
    /// </summary>
    public class RateLogWriterManager
    {
        /// <summary>
        /// 获取操作日志管理器
        /// </summary>
        public static IRateLogWriter GetOperateLogger
        {
            get
            {
                return new RateLogWriter();
            }
        }
    }
}