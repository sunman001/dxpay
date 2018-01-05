using System.Collections.Generic;
using System.Linq;

namespace WEB.Extensions
{
    public static class PayExtension
    {
        /// <summary>
        /// 转换支付状态(将数字转化为中文)    
        /// </summary>
        /// <param name="payState">判断参数</param>
        /// <returns></returns>
        public static string ConvertPayState(this int payState)
        {
            string result;
            switch (payState)
            {
                case 1:
                    result = "支付成功";
                    break;
                case -1:
                    result = "支付失败";
                    break;
                default:
                    result = "等待支付";
                    break;
            }
            return result;
        }

        /// <summary>
        /// 转换通知状态(将数字转化为中文)
        /// </summary>
        /// <param name="noticeState"></param>
        /// <param name="orderState"></param>
        /// <returns></returns>
        public static string ConvertNoticeState(this int noticeState, int orderState)
        {
            var result = "";
            if (noticeState == 1)
            {
                result = "通知成功";
            }
            else if (noticeState == -1)
            {
                result = "通知失败";
            }
            else if (noticeState == 0)
            {
                result = orderState == 1 ? "等待通知" : "--";
            }
            else if (noticeState == 2)
            {
                result = "正在通知";
            }
            return result;
        }

        public static string ConvertRelatedPlatformToString(this string platform)
        {
            if (string.IsNullOrEmpty(platform))
            {
                return "";
            }
            var dict = new Dictionary<string, string>
            {
                {"1","安卓" },
                {"2","苹果" },
                {"3","H5" }
            };
            var sp = platform.Split(',');
            var str = sp.Select(s => dict[s]).ToList();
            return string.Join(",", str);
        }
    }
}