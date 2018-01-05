using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.WebPages;

namespace WEBGW.Util
{
    /// <summary>
    /// 自定义的移动设备检测类
    /// </summary>
    public class CustomMobileDisplayMode : DefaultDisplayMode
    {
        /// <summary>
        /// 移动端设备集合
        /// </summary>
        private static readonly List<string> MobileDevices = new List<string> { "Opera Mobi", "Mobile" };
        public CustomMobileDisplayMode() : base("Mobile")
        {
            ContextCondition = context => IsMobile(context.GetOverriddenUserAgent());
        }
        /// <summary>
        /// 判断是否为移动设备
        /// </summary>
        /// <param name="userAgent">UserAgent</param>
        /// <returns></returns>
        public static bool IsMobile(string userAgent)
        {
            return MobileDevices.Select(excluded => userAgent.IndexOf(excluded, StringComparison.InvariantCultureIgnoreCase)).Any(index => index >= 0);
        }
    }
}