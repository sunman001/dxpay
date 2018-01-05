using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayForAnother.Model
{
    /// <summary>
    /// 代付参数
    /// </summary>
   public  class ChinaPayParameter
    {
        /// <summary>
        /// 商户编号
        /// </summary>
        public string merId { get; set; }
        /// <summary>
        /// 商户流水号
        /// </summary>
        public string merSeqId { get; set; }
        /// <summary>
        /// 收款账号
        /// </summary>
        public string cardNo { get; set; }
        /// <summary>
        /// 收款人姓名
        /// </summary>
        public string usrName { get; set; }

        /// <summary>
        /// 金额,人民币，单位分,整数
        /// </summary>
        public string  transAmt { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string prov { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 开户银行
        /// </summary>
        public string openBank { get; set; }

        /// <summary>
        /// 开户行支行
        /// </summary>
        public string subBank { get; set; }

        /// <summary>
        /// 用途
        /// </summary>
        public string purpose { get; set; }

        /// <summary>
        /// 私钥
        /// </summary>
        public string priKeyPath { get; set; }

        /// <summary>
        /// 付款标志
        /// </summary>
        public string flags { get; set; }
    }
}
