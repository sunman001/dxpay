namespace DxPay.Agent.Util.Logger
{
    /// <summary>
    /// 日志接口
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// 写日志方法
        /// </summary>
        /// <param name="summary">日志摘要</param>
        /// <param name="message">日志详情</param>
        void Log(string summary,string message);
    }
}
