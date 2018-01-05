using DxPay.LogManager.LogManager.Platform.BusinessPersonnel;
using JMP.TOOL;

namespace DxPay.Bp.Util.Logger
{
    public class BpGlobalErrorLogger
    {
        public static void Log(string message, string location = "", string summary = "服务器错误")
        {
            var logger = new BusinessPersonnelGlobalFactory().CreateLogger();

            logger.Log(UserInfo.Uid, message, RequestHelper.GetClientIp(), location, summary);
        }
    }
}