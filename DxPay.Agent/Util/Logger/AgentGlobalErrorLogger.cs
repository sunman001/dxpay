using DxPay.LogManager.LogManager.Platform.Agent;
using JMP.TOOL;

namespace DxPay.Agent.Util.Logger
{
    public class AgentGlobalErrorLogger
    {
        public static void Log(string message, string location = "", string summary = "服务器错误")
        {
            var logger = new AgentGlobalFactory().CreateLogger();

            logger.Log(UserInfo.Uid, message, RequestHelper.GetClientIp(), location, summary);
        }
    }
}