using DxPay.LogManager.Models;

namespace DxPay.LogManager.LogManager.Platform.Administrator
{
    public class AdministratorGlobalFactory :IPlatformLogFactory
    {
        public ILogger CreateLogger()
        {
            return new AdministratorGlobalErrorLogger();
        }
    }
}
