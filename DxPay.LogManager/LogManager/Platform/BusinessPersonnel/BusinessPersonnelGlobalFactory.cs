using DxPay.LogManager.Models;

namespace DxPay.LogManager.LogManager.Platform.BusinessPersonnel
{
    public class BusinessPersonnelGlobalFactory : IPlatformLogFactory
    {
        public ILogger CreateLogger()
        {
            return new BusinessPersonnelGlobalErrorLogger();
        }
    }
}
