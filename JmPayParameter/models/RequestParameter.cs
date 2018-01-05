using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JmPayApiServer.Models
{
    /// <summary>
    /// 下单请求参数
    /// </summary>
    public class RequestParameter
    {
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string bizcode { get; set; }
        /// <summary>
        /// 应用id
        /// </summary>
        public int appid { get; set; }
        /// <summary>
        /// 关联终端唯一KEY
        /// </summary>
        public string termkey { get; set; }
        /// <summary>
        /// 异步通知地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 同步地址
        /// </summary>
        public string showaddress { get; set; }
        /// <summary>
        /// 支付类型（0：收银台模式，1:支付宝，2：微信，3：银联，4:微信公众号，5:微信APP,6:微信扫码，7：支付宝扫码,8：QQ钱包wap）
        /// </summary>
        public int paytype { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// 商户私有信息
        /// </summary>
        public string privateinfo { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string goodsname { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string timestamp { get; set; }
        /// <summary>
        /// 数据签名（md5（price+ bizcode+timestamp+appkey））
        /// </summary>
        public string sign { get; set; }
    }
}