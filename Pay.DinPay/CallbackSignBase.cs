namespace Pay.DinPay
{
    /// <summary>
    /// 订单支付成功后的回调验证(抽象基类)
    /// </summary>
    public abstract class CallbackSign
    {
        /// <summary>
        /// 验证公钥签名
        /// </summary>
        /// <returns></returns>
        public abstract bool Verify();
    }
}
