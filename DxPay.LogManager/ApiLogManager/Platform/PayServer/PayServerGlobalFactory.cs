using DxPay.LogManager.ApiLogManager.Logger;

namespace DxPay.LogManager.ApiLogManager.Platform.PayServer
{
    public class PayServerGlobalFactory : IPlatformLogFactory
    {
        public ILogger CreateLogger()
        {
            return new PayServerGlobalErrorLogger();
        }
    }
}
