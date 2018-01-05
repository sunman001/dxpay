using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JmPayParameter.Models
{
    /// <summary>
    /// H5和收银台模式第二次请求实体
    /// </summary>
    public class PayBankModels
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// 签名验证
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string goodsname { get; set; }
        /// <summary>
        /// 风控配置表id
        /// </summary>
        public int apptype { get; set; }
        /// <summary>
        /// 关联平台（1:安卓，2:苹果，3:H5）
        /// </summary>
        public int paymode { get; set; }
        /// <summary>
        /// 支付类型（“0”：为收银台模式，其他为H5模式）
        /// </summary>
        public string paytype { get; set; }

    }
}
