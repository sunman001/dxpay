using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB.Util
{
    /// <summary>
    /// 支付通道配置静态扩展类型
    /// </summary>
    public static class PaymentTypeConfigExtension
    {
        public static string JsonToString(this string json)
        {
            string tostring = "";
            List<WEB.Models.PaymentTypeConfig> list = JMP.TOOL.JsonHelper.Deserialize<List<WEB.Models.PaymentTypeConfig>>(json);
            return list.Count <= 0 ? "" : string.Join(",", list.Select(x => string.Format("{0}:{1}", x.label, x.value)));
        }
    }
}