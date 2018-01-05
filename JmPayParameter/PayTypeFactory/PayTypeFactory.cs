using JmPayParameter.PayType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JmPayParameter.PayTypeFactory
{

    public class PayTypeFactory
    {
        /// <summary>
        /// 根据支付类型获取支付通道
        /// </summary>
        /// <param name="payType"></param>
        /// <returns></returns>
        public  PayTypeAbstract Create(int payType)
        {
            PayTypeAbstract payTypeInstance;
            switch (payType)
            {
                case 1://支付宝
                    payTypeInstance = new PayTypeForAliPay();
                    break;
                case 2://微信
                    payTypeInstance = new PayWxWap();
                    break;
                case 3://银联
                    payTypeInstance = new UnionPay();
                    break;
                case 4://微信公众号
                    payTypeInstance = new PayWxGzh();
                    break;
                case 5://微信appid
                    payTypeInstance = new PayWxApp();
                    break;
                case 6://微信扫码
                    payTypeInstance = new PayWxSm();
                    break;
                case 7://支付宝扫码
                    payTypeInstance = new PayAlPaySm();
                    break;
                case 8://QQ钱包wap
                    payTypeInstance = new PayQQWap();
                    break;
                default:
                    throw new Exc { Response = new InnerResponse { ErrorCode = ErrorCode.Code107.GetValue(), Message = ErrorCode.Code107.GetDescription() } };
                    //break;

            }
            return payTypeInstance;
        }
    }
}
