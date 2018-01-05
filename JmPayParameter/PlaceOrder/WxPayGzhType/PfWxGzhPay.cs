using DxPay.LogManager.LogFactory.ApiLog;
using JmPayParameter.Models;
using JmPayParameter.PayChannel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JmPayParameter.PlaceOrder.WxPayGzhType
{

    public  class PfWxGzhPay
    {
        /// <summary>
        /// 浦发银行微信公众号支付通道主入口
        /// </summary>
        /// <param name="paymode">平台类型（1：安卓，2：ios，3：H5）</param>
        /// <param name="orderid">订单id</param>
        /// <returns></returns>
        public  InnerResponse PfWxGzhPayInfo(int paymode, int orderid)
        {
            InnerResponse inn = new InnerResponse();
            if (paymode == 3)
            {
                inn = PfWxGzhH5(orderid);
            }
            else
            {
                inn = inn.ToResponse(ErrorCode.Code9987);
            }
            return inn;
        }
        /// <summary>
        /// 浦发银行微信公众号支付H5调用模式
        /// </summary>
        /// <param name="paymode">平台类型（1：安卓，2：ios，3：H5）</param>
        /// <param name="orderid">订单id</param>
        /// <returns></returns>
        private  InnerResponse PfWxGzhH5(int orderid)
        {
            InnerResponse inn = new InnerResponse();
            inn = inn.ToResponse(ErrorCode.Code100);
            inn.ExtraData = ConfigurationManager.AppSettings["wftwxgzhget"].ToString() + "/PfWxGzh" + orderid + ".html";
            inn.IsJump = true;
            return inn;
        }
    }
}
