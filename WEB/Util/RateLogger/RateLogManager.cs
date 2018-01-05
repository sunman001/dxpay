namespace WEB.Util.RateLogger
{
    /// <summary>
    /// 日志管理器
    /// </summary>
    public class RateLogManager
    {
        /// <summary>
        /// 获取操作日志管理器
        /// </summary>
        public static IRateLog GetOperateLogger
        {
            get
            {
                return new RateOperateLogger();
            }
        }

       
    }
}