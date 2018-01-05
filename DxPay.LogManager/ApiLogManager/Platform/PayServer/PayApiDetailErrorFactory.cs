using DxPay.LogManager.ApiLogManager.Logger;

namespace DxPay.LogManager.ApiLogManager.Platform.PayServer
{
    public class PayApiDetailErrorFactory : IPayApiDetailLogFactory
    {
        public IApiLogger CreateLogger()
        {
            return new PayApiDetailErrorLogger();
        }
    }
}
