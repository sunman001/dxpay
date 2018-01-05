using DxPay.LogManager.LogManager.Platform.Developer;
using JMP.TOOL;

namespace WEB.Util.Logger
{
    public class DeveloperGlobalErrorLogger
    {
        public static void Log(string message, string location = "", string summary = "服务器错误")
        {
            var logger = new DeveloperGlobalFactory().CreateLogger();

            logger.Log(UserInfo.Uid, message, RequestHelper.GetClientIp(), location, summary);
        }
    }
}