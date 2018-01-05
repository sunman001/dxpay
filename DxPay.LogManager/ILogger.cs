namespace DxPay.LogManager
{
    /// <summary>
    /// 对客户端的写日志接口
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// 写日志方法
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="message">错误消息</param>
        /// <param name="location">报错位置</param>
        /// <param name="summary">错误摘要[可选]</param>
        /// <param name="ipAddress">客户端IP地址</param>
        void Log(int userId,string message, string ipAddress,string location="", string summary = "");
    }
}
