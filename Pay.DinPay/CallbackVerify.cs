/**
*功能：智付异步后台通知
*场景：当订单支付完毕后智付服务器主动将该笔订单的支付成功数据回来后，实例化此类并调用Verify()方法进行签名验证
*版本：3.0
*日期：2016-09-23
*说明：
*需要收集智付回调表单的所有字段并整理成字典作为Verify()方法的参数
**/
using System.Collections.Generic;
using System.Linq;

namespace Pay.DinPay
{
    /// <summary>
    /// 订单成功后回调验证类
    /// </summary>
    public class CallbackVerify
    {
        /// <summary>
        /// 需要验证的字段
        /// </summary>
        private readonly List<string> _dict = new List<string> { "bank_seq_no", "extra_return_param", "interface_version", "merchant_code", "notify_id", "notify_type", "order_amount", "order_no", "order_time", "trade_no", "trade_status", "trade_time" };
        /// <summary>
        /// 验证签名的方法
        /// </summary>
        /// <param name="requestDict">表单字段字典</param>
        /// <returns></returns>
        public bool Verify(Dictionary<string, string> requestDict)
        {
            var lst = _dict.Where(requestDict.ContainsKey).ToDictionary(d => d, d => requestDict[d]);
            var str = string.Join("&", lst.OrderBy(x => x.Key).Select(x => string.Format("{0}={1}", x.Key, x.Value)));
            CallbackSign callbackSign = new CallbackSignRsas(str, requestDict["sign"]);
            var success = callbackSign.Verify();
            return success;
        }
    }
}
