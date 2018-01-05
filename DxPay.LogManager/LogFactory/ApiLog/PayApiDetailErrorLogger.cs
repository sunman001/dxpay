using DxPay.LogManager.ApiLogManager.Platform.PayServer;
using JMP.TOOL;
using TOOL.EnumUtil;

namespace DxPay.LogManager.LogFactory.ApiLog
{
    public class PayApiDetailErrorLogger
    {
        private static readonly IApiLogger Logger = new PayApiDetailErrorFactory().CreateLogger();
        /// <summary>
        /// 上游支付错误写日志方法
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="channelId">通道ID</param>
        /// <param name="location">报错位置</param>
        /// <param name="summary">错误摘要[可选]</param>
        public static void UpstreamPaymentErrorLog(string message, int channelId, string summary = "上游支付错误", string location = "")
        {
            Logger.UpstreamPaymentErrorLog(0, message, RequestHelper.GetClientIp(),channelId,(int)EnumForLogForApi.ErrorType.Unknown, location, summary);
        }

        /// <summary>
        /// 下游用户错误写日志方法
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="appId">应用ID</param>
        /// <param name="errorType">下游错误类型[1:订单号重复,2:重复发起支付,3:其他]</param>
        /// <param name="location">报错位置</param>
        /// <param name="summary">错误摘要[可选]</param>
        public static void DownstreamErrorLog(string message, int appId, EnumForLogForApi.ErrorType errorType, string summary = "下游用户错误", string location = "")
        {
            Logger.DownstreamErrorLog(0, message, RequestHelper.GetClientIp(), appId, errorType, location, summary);
        }

        /// <summary>
        /// 上游通知异常写日志方法
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="channelId">通道ID</param>
        /// <param name="location">报错位置</param>
        /// <param name="summary">错误摘要[可选]</param>
        public static void UpstreamNotifyErrorLog(string message, int channelId, string summary = "上游通知异常", string location = "")
        {
            Logger.UpstreamNotifyErrorLog(0, message, RequestHelper.GetClientIp(), channelId, (int)EnumForLogForApi.ErrorType.Unknown, location, summary);
        }

    }
}