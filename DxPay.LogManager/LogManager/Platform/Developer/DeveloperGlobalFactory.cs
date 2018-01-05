using DxPay.LogManager.Models;

namespace DxPay.LogManager.LogManager.Platform.Developer
{
    public class DeveloperGlobalFactory : IPlatformLogFactory
    {
        public ILogger CreateLogger()
        {
            return new DeveloperGlobalErrorLogger();
        }
    }
}
