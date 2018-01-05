/************聚米支付平台__接收支付信息实体类************/
//描述：接收支付信息实体类
//功能：接收支付信息实体类
//开发者：秦际攀
//开发时间: 2016.03.21
/************聚米支付平台__接收支付信息实体类************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMWBSR
{
    /// <summary>
    /// 接收支付信息实体类
    /// </summary>
    public class ModelZfJk
    {
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string o_bizcode { get; set; }
        /// <summary>
        /// 应用key
        /// </summary>
        public string o_appkey { get; set; }
        /// <summary>
        /// 关联终端唯一KEY
        /// </summary>
        public string o_term_key { get; set; }
        /// <summary>
        /// 通知地址
        /// </summary>
        public string o_address { get; set; }
        /// <summary>
        /// 支付类型
        /// </summary>
        public string o_paymode_id { get; set; }
        /// <summary>
        /// 商品id
        /// </summary>
        public int o_goods_id { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal o_price { get; set; }
        /// <summary>
        /// 商户私有信息
        /// </summary>
        public string o_privateinfo { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string o_goods_name { get; set; }
        /// <summary>
        /// 同步地址
        /// </summary>
        public string o_showaddress { get; set; }
        /// <summary>
        /// 用户ip地址
        /// </summary>
        public string o_ip { get; set; }

    }
}
