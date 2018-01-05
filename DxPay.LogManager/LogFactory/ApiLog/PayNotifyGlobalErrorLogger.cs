using DxPay.LogManager.ApiLogManager.Platform.PayNotify;
using JMP.TOOL;

namespace DxPay.LogManager.LogFactory.ApiLog
{
    public class PayNotifyGlobalErrorLogger
    {
        public static void Log(string message, string location = "", string summary = "服务器错误")
        {
            var logger = new PayNotifyGlobalFactory().CreateLogger();

            logger.Log(0, message, RequestHelper.GetClientIp(), location, summary);
        }
    }
}