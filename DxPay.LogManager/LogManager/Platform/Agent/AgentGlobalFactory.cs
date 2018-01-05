using DxPay.LogManager.Models;

namespace DxPay.LogManager.LogManager.Platform.Agent
{
    public class AgentGlobalFactory : IPlatformLogFactory
    {
        public ILogger CreateLogger()
        {
            return new AgentGlobalErrorLogger();
        }
    }
}
