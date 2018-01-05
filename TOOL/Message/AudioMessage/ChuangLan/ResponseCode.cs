using System.Collections.Generic;

namespace TOOL.Message.AudioMessage.ChuangLan
{
    /// <summary>
    /// 创蓝的返回状态码及说明
    /// </summary>
    public class ResponseCode
    {
        private static readonly Dictionary<string, string> ResponseCodeDict = new Dictionary<string, string>
        {
            {"1","发送成功" },
            {"2","自定义模版发送成功" },
            {"1000","用户验证参数错误" },
            {"1001","用户不存在" },
            {"1002","密码错误" },
            {"1003","key错误" },
            {"1004","参数错误" },
            {"1005","参数为空" },
            {"1006","contextparm参数为空" },
            {"1007","身份验证失败" },
            {"1008","主叫号码不正确（强显号码）" },
            {"1009","主叫号码不正确" },
            {"1012","文件格式错误" },
            {"1013","文件超时" }
        };

        /// <summary>
        /// 根据错误码获取对应的中文解释
        /// </summary>
        /// <param name="errorCode">错误码</param>
        /// <returns></returns>
        public static string FindConnotationByStatusCode(string errorCode)
        {
            try
            {
                return ResponseCodeDict[errorCode];
            }
            catch
            {
                return "";
            }
           
        }
    }
}
