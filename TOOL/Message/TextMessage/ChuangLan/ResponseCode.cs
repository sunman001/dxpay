using System.Collections.Generic;

namespace TOOL.Message.TextMessage.ChuangLan
{
    /// <summary>
    /// 创蓝的返回状态码及说明
    /// </summary>
    public class ResponseCode
    {
        private static readonly Dictionary<string, string> ResponseCodeDict = new Dictionary<string, string>
        {
            {"0","提交成功" },
            {"101","无此用户" },
            {"102","密码错误" },
            {"103","提交过快（提交速度超过流速限制）" },
            {"104","系统忙（因平台侧原因，暂时无法处理提交的短信）" },
            {"105","敏感短信（短信内容包含敏感词）" },
            {"106","消息长度错" },
            {"107","包含错误的手机号码" },
            {"108","手机号码个数错（群发>50000或<=0;单发>200或<=0）" },
            {"109","无发送额度（该用户可用短信数已使用完）" },
            {"110","不在发送时间内" },
            {"111","超出该账户当月发送额度限制" },
            {"112","无此产品，用户没有订购该产品" },
            {"113","extno格式错（非数字或者长度不对）" },
            {"115","自动审核驳回" },
            {"116","签名不合法，未带签名（用户必须带签名的前提下）" },
            {"117","IP地址认证错,请求调用的IP地址不是系统登记的IP地址" },
            {"118","用户没有相应的发送权限" },
            {"119","用户已过期" },
            {"120","测试内容不是白名单" }
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
