using System;

namespace Pay.DinPay
{
    /// <summary>
    /// 商家号私钥RSA-S签名类
    /// </summary>
    public class PrivateKeySignRsas : IPrivateKeySignInter
    {
        private string _merchantPrivateKey;
        
        public PrivateKeySignRsas(string merchantPrivateKey)
        {
            if (string.IsNullOrEmpty(merchantPrivateKey))
            {
                throw new ArgumentNullException("商家号私钥为空");
            }
            _merchantPrivateKey = merchantPrivateKey;

        }
        /// <summary>
        /// 根据私钥获取对应签名
        /// </summary>
        /// <param name="signSrc">需要验证的签名参数</param>
        /// <returns></returns>
        public string Sign(string signSrc)
        {
            var util = new Util();
            //私钥转换成C#专用私钥
            _merchantPrivateKey = util.RsaPrivateKeyJava2DotNet(_merchantPrivateKey);
            //签名
            var signData = util.RsaSign(signSrc, _merchantPrivateKey);
            return signData;
        }
    }
}
