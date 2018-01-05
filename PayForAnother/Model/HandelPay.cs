using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayForAnother.Model
{
  public   class HandelPay
    { 
        /// <summary>
        /// 批次号
        /// </summary>
        public string batchnumber { get; set; }
        /// <summary>
        /// 商户ID
        /// </summary>
        public string MerchantNumber { get; set; }

        /// <summary>
        /// 私钥
        /// </summary>
        public string PrivateKey { get; set; }

        /// <summary>
        /// 公钥
        /// </summary>
        public string PublicKey { get; set; }
    }
}
