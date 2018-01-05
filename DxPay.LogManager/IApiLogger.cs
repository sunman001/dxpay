using TOOL.EnumUtil;

namespace DxPay.LogManager
{
    /// <summary>
    /// 对全局接口的写日志接口
    /// </summary>
    public interface IApiLogger
    {
        /// <summary>
        /// 上游支付错误写日志方法
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="message">错误消息</param>
        /// <param name="channelId">通道ID</param>
        /// <param name="errorType">下游错误类型[1:订单号重复,2:重复发起支付,3:其他]</param>
        /// <param name="location">报错位置</param>
        /// <param name="summary">错误摘要[可选]</param>
        /// <param name="ipAddress">客户端IP地址</param>
        void UpstreamPaymentErrorLog(int userId, string message, string ipAddress, int channelId,
            EnumForLogForApi.ErrorType errorType, string location = "", string summary = "");

        /// <summary>
        /// 下游用户错误写日志方法
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="message">错误消息</param>
        /// <param name="channelId">通道ID</param>
        /// <param name="errorType">下游错误类型[1:订单号重复,2:重复发起支付,3:其他]</param>
        /// <param name="location">报错位置</param>
        /// <param name="summary">错误摘要[可选]</param>
        /// <param name="ipAddress">客户端IP地址</param>
        void DownstreamErrorLog(int userId, string message, string ipAddress, int channelId,
            EnumForLogForApi.ErrorType errorType, string location = "", string summary = "");

        /// <summary>
        /// 上游通知异常写日志方法
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="message">错误消息</param>
        /// <param name="ipAddress">客户端IP地址</param>
        /// <param name="appId">应用ID</param>
        /// <param name="errorType">下游错误类型[1:订单号重复,2:重复发起支付,3:其他]</param>
        /// <param name="location">报错位置</param>
        /// <param name="summary">错误摘要[可选]</param>
        void UpstreamNotifyErrorLog(int userId, string message, string ipAddress, int appId,
            EnumForLogForApi.ErrorType errorType, string location = "", string summary = "");
    }
}
