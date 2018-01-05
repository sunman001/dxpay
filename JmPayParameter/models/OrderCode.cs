using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JmPayParameter.Models
{
    /// <summary>
    /// 收银台和H5模式第二次接收参数实体
    /// </summary>
    public class OrderCode
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 有效请求时间
        /// </summary>
        public DateTime EffectiveTime { get; set; }
        /// <summary>
        /// 支付类型（0：收银台模式，1:支付宝，2：微信，3：银联，4:微信公众号，5:微信APP,6:微信扫码，7：支付宝扫码,8：QQ钱包wap）
        /// </summary>
        public int paytype { get; set; }
    }
}
