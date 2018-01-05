using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    /// <summary>
    /// 支付通道配置json字符串对象
    /// </summary>
    public class PaymentTypeConfig
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string fieldName { get; set; }
        /// <summary>
        /// 字段值
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 字段显示名称
        /// </summary>
        public string label { get; set; }
    }

    public class PaymentTypeConfigExt: PaymentTypeConfig
    {
        public PaymentTypeConfigExt()
        {
            InputType = "text";
        }
        public string InputType { get; set; }
    }
}