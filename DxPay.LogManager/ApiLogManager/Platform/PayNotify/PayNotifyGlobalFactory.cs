using DxPay.LogManager.ApiLogManager.Logger;

namespace DxPay.LogManager.ApiLogManager.Platform.PayNotify
{
    public class PayNotifyGlobalFactory : IPlatformLogFactory
    {
        public ILogger CreateLogger()
        {
            return new PayNotifyGlobalErrorLogger();
        }
    }
}
