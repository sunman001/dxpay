using TOOL.EnumUtil;

namespace DxPay.LogManager.ApiLogManager.Logger
{
    /// <summary>
    /// 运营平台错误日志类
    /// </summary>
    public class PayNotifyGlobalErrorLogger : ApiGlobalErrorLogModel
    {
        protected override void SetClient()
        {
            base.LogForApi.ClientId = (int)DxApiClient.PayNotify;
            base.LogForApi.ClientName = DxApiClient.PayNotify.GetDescription();
        }
    }
}
