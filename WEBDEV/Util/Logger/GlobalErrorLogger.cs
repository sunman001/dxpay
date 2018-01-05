using DxPay.LogManager.LogManager.Platform.Developer;
using JMP.TOOL;

namespace WEBDEV.Util.Logger
{
    /// <summary>
    /// 全局错误日志器
    /// </summary>
    public class GlobalErrorLogger
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="location">错误定位</param>
        /// <param name="summary">摘要</param>
        public static void Log(string message, string location = "", string summary = "服务器错误")
        {
            var logger = new DeveloperGlobalFactory().CreateLogger();

            logger.Log(UserInfo.Uid, message, RequestHelper.GetClientIp(), location, summary);
        }
    }
}