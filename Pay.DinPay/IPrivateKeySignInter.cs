namespace Pay.DinPay
{
    /// <summary>
    /// 私钥签名接口
    /// </summary>
   public  interface IPrivateKeySignInter
    {
        /// <summary>
        /// 需要验证的签名参数
        /// </summary>
        /// <param name="signSrc"></param>
        /// <returns></returns>
        string Sign(string signSrc);
    }
}