using JMWBSR.Models;

namespace JMWBSR.Extensions
{
    public static class WeiChatDetectorExtension
    {
        /// <summary>
        /// 判断当前的支付类型是否可用
        /// </summary>
        /// <param name="payment">支付类型对象</param>
        /// <param name="isInWeiChatBrowser">当前请求是否从微信中发起</param>
        /// <param name="isMobile">是否是移动设备</param>
        /// <returns></returns>
        public static bool PaymentEnableDetect(this PaymentMode payment,bool isInWeiChatBrowser,bool isMobile)
        {
            if (payment == null)
            {
                return false;
            }
            //如果不检测微信,直接返回true
            if (!payment.CheckWeiChat)
            {
                return true;
            }
            
            if (payment.CheckMobileDevice && !isMobile)
            {
                payment.Description = "当前浏览器不支持该支付方式";
                return false;
            }
            if (isInWeiChatBrowser) return true;
            payment.Description = "该支付方式只支持微信内部使用";
            return false;
        }
    }
}