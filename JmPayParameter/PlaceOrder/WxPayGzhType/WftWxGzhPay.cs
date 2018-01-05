using System.Configuration;

namespace JmPayParameter.PlaceOrder.WxPayGzhType
{
    /// <summary>
    /// 威富通微信公众号支付接口
    /// </summary>
    public  class WftWxGzhPay
    {
        /// <summary>
        /// 威富通微信wap支付通道主入口
        /// </summary>
        /// <param name="paymode">平台类型（1：安卓，2：ios，3：H5）</param>
        /// <param name="orderid">订单id</param>
        /// <returns></returns>
        public  InnerResponse WftWxGzhPayInfo(int paymode, int orderid)
        {
            InnerResponse inn = new InnerResponse();
            if (paymode == 3)
            {
                inn = PayWftGzhH5(orderid);
            }
            else
            {
                inn = inn.ToResponse(ErrorCode.Code9987);
            }
            return inn;
        }
        /// <summary>
        /// 威富通公众号支付通道H5调用方式
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        private  InnerResponse PayWftGzhH5(int orderid)
        {
            InnerResponse inn = new InnerResponse();
            inn = inn.ToResponse(ErrorCode.Code100);
            inn.ExtraData = ConfigurationManager.AppSettings["wftwxgzhget"].ToString() + "/wxggh" + orderid + ".html";
            inn.IsJump = true;
            return inn;
        }
    }
}
