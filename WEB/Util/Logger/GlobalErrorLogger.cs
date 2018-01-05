using DxPay.LogManager.LogManager.Platform.Administrator;
using JMP.TOOL;

namespace WEB.Util.Logger
{
    public class GlobalErrorLogger
    {
        public static void Log(string message, string location = "", string summary = "服务器错误")
        {
            var logger = new AdministratorGlobalFactory().CreateLogger();

            logger.Log(UserInfo.Uid, message, RequestHelper.GetClientIp(), location, summary);
        }
    }
}