using DxPay.LogManager.ApiLogManager.Platform.PayServer;
using JMP.TOOL;

namespace DxPay.LogManager.LogFactory.ApiLog
{
    public class PayApiGlobalErrorLogger
    {
        public static void Log(string message, string location = "", string summary = "服务器错误")
        {
            var logger = new PayServerGlobalFactory().CreateLogger();

            logger.Log(0, message, RequestHelper.GetClientIp(), location, summary);
        }
    }
}