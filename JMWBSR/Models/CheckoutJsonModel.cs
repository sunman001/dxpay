namespace JMWBSR.Models
{
    /// <summary>
    /// 收银台传递参数
    /// </summary>
    public class CheckoutJsonModel
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
        /// 支付类型
        /// </summary>
        public int paymode { get; set; }
        /// <summary>
        /// 签名验证
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string goodsname { get; set; }
        /// <summary>
        /// 应用类型id
        /// </summary>
        public int tid { get; set; }
        /// <summary>
        /// 关联平台（1:安卓，2:苹果，3:H5）
        /// </summary>
        public int paytype { get; set; }
        /// <summary>
        /// 支持的支付类型,以逗号隔开
        /// </summary>
        public string spotpaytype { get; set; }
    }
}