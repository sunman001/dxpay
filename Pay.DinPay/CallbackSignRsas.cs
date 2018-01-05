namespace Pay.DinPay
{
    /// <summary>
    /// 智付回调验证(RSA-S)
    /// </summary>
    public class CallbackSignRsas : CallbackSign
    {
        private readonly string _signStr;
        private readonly string _dinPaySign;

        /// <summary>
        /// 智付RSA-S签名回调验证
        /// </summary>
        /// <param name="signStr">需要验证的回传参数串</param>
        /// <param name="dinPaySign">智付返回的签名</param>
        public CallbackSignRsas(string signStr, string dinPaySign)
        {
            _signStr = signStr;
            _dinPaySign = dinPaySign;
        }
        /// <summary>
        /// 验证公钥签名
        /// </summary>
        /// <returns></returns>
        public override bool Verify()
        {
            //使用智付公钥对返回的数据验签
            var dinpayPubKey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCDCDYzzgMzQQNXhJHMTe8O3VO7QBCYFhUEnwTzG0aVdaYxpkEoTTA3igaj21Ha4OLAf9EX/Sb0/0uyiqzsGomAvd5uR23QKQEnSXO5vHs1NXg4kMC6ZD+8ZhbpoOBCuWgn374RAlY7k8LC+I5J84qwR6NxXozpMCLkx+hoauHrEQIDAQAB";
            //将智付公钥转换成C#专用格式
            var util = new Util();
            dinpayPubKey = util.RsaPublicKeyJava2DotNet(dinpayPubKey);
            //验签
            var result = util.ValidateRsaSign(_signStr, dinpayPubKey, _dinPaySign);
            return result;
        }
    }
}
