using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JmPayParameter.Models
{
    /// <summary>
    /// 预下单接口实体
    /// </summary>
    public class PreOrdeModel
    {
        #region 封装实体
        /// <summary>
        /// 订单id
        /// </summary>
        public int orderid { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string goodsname { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// ip地址
        /// </summary>
        public string ip { get; set; }
        /// <summary>
        /// 应用key
        /// </summary>
        public string appkey { get; set; }
        /// <summary>
        /// 该应用已开通的支付通道
        /// </summary>
        public string ThispayType { get; set; }
        #endregion
    }
}
