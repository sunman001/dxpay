namespace DxPay.Bp.Util.Logger
{
    /// <summary>
    /// 日志管理器
    /// </summary>
    public class LogManager
    {
        /// <summary>
        /// 获取操作日志管理器
        /// </summary>
        public static ILog GetOperateLogger
        {
            get
            {
                return new OperateLogger();
            }
        }

        /// <summary>
        /// 获取访问日志管理器
        /// </summary>
        public static ILog VisitLogger
        {
            get
            {
                return new VisitLogger();
            }
        }
    }
}