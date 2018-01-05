namespace PayForAnother.Logger
{
    /// <summary>
    /// 日志管理器
    /// </summary>
    public class LogManager
    {
        /// <summary>
        /// 获取操作日志管理器
        /// </summary>
        public static ILog GetPayForAnotherLogger
        {
            get
            {
                return new PayForAnotherLogger();
            }
        }

      
    }
}