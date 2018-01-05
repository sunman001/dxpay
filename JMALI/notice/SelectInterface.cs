using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JMALI.notice
{
    /// <summary>
    /// 查询接口实体
    /// </summary>
    public class SelectInterface
    {
        /// <summary>
        /// 商户编号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 商户key
        /// </summary>
        public string UserKey { get; set; }
        /// <summary>
        /// 支付通道id
        /// </summary>
        public int PayId { get; set; }
        /// <summary>
        /// 微信appid
        /// </summary>
        public string wxappid { get; set; }
        /// <summary>
        /// 单笔最小支付金额
        /// </summary>
        public decimal minmun { get; set; }
        /// <summary>
        /// 单笔最大支付金额
        /// </summary>
        public decimal maximum { get; set; }

        /// <summary>
        /// 中信银行子商户账号
        /// </summary>
        public string UserIdZ { get; set; }

        /// <summary>
        /// 支付方式（途贝微信Wap用，特殊情况）
        /// </summary>
        public string UserPayType { get; set; }

        /// <summary>
        /// 跳转域名
        /// </summary>
        public string RequestUrl { get; set; }
        /// <summary>
        /// 同步跳转配置域名
        /// </summary>
        public string GotoURL { get; set; }
        /// <summary>
        /// 异步跳转配置域名
        /// </summary>
        public string ReturnUrl { get; set; }

    }
}