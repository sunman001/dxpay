using JMP.MDL;
using System;
using TOOL.EnumUtil;

namespace DxPay.LogManager.ApiLogManager.Logger
{
    public abstract class PayApiDetailErrorLogBase : IApiLogger
    {
        protected readonly IApiLogWriter LogWriter;
        protected PayApiDetailErrorLogBase()
        {
            LogForApi = new LogForApi { TypeValue = 1 };
            LogWriter = new ApiSqlServerLogWriter();
            Init();
        }

        protected void Init()
        {
            SetClient();
        }
        /// <summary>
        /// 继承类需要设置不同的平台标识
        /// </summary>
        protected abstract void SetClient();
        protected LogForApi LogForApi { get; set; }

        #region Implementation of IApiLogger

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
        public void UpstreamPaymentErrorLog(int userId, string message, string ipAddress, int channelId, EnumForLogForApi.ErrorType errorType, string location = "", string summary = "")
        {
            LogForApi.Message = message;
            LogForApi.IpAddress = ipAddress;
            LogForApi.Location = location;
            LogForApi.Summary = summary;
            LogForApi.CreatedOn = DateTime.Now;
            LogForApi.PlatformId = (int)EnumForLogForApi.Platform.UpstreamPaymentError;
            LogForApi.ErrorTypeId = (int)errorType;
            LogForApi.RelatedId = channelId;
            LogWriter.Write(LogForApi);
        }

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
        public void DownstreamErrorLog(int userId, string message, string ipAddress, int channelId, EnumForLogForApi.ErrorType errorType, string location = "", string summary = "")
        {
            LogForApi.Message = message;
            LogForApi.IpAddress = ipAddress;
            LogForApi.Location = location;
            LogForApi.Summary = summary;
            LogForApi.CreatedOn = DateTime.Now;
            LogForApi.PlatformId = (int)EnumForLogForApi.Platform.DownstreamError;
            LogForApi.ErrorTypeId = (int)errorType;
            LogForApi.RelatedId = channelId;
            LogWriter.Write(LogForApi);
        }

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
        public void UpstreamNotifyErrorLog(int userId, string message, string ipAddress, int appId, EnumForLogForApi.ErrorType errorType, string location = "", string summary = "")
        {
            LogForApi.Message = message;
            LogForApi.IpAddress = ipAddress;
            LogForApi.Location = location;
            LogForApi.Summary = summary;
            LogForApi.CreatedOn = DateTime.Now;
            LogForApi.PlatformId = (int)EnumForLogForApi.Platform.UpsteamNotifyError;
            LogForApi.ErrorTypeId = (int)errorType;
            LogForApi.RelatedId = appId;
            LogWriter.Write(LogForApi);
        }

        #endregion
    }
}
