using TOOL.EnumUtil;

namespace DxPay.LogManager.ApiLogManager.Logger
{
    /// <summary>
    /// 支付接口错误日志类
    /// </summary>
    public class PayApiDetailErrorLogger : PayApiDetailErrorLogBase
    {
        /// <summary>
        /// 支付接口错误日志类
        /// </summary>
        public PayApiDetailErrorLogger()
        {
            
        }

        protected override void SetClient()
        {
            base.LogForApi.ClientId = (int)DxApiClient.PayServer;
            base.LogForApi.ClientName = DxApiClient.PayServer.GetDescription();
        }
    }
}
