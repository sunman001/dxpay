using JmPayParameter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxPay.LogManager.LogFactory.ApiLog;
using JmPayParameter.PayChannel;
using System.Configuration;

namespace JmPayParameter.PlaceOrder.QQPayType
{
    /// <summary>
    /// 途贝QQ钱包wap
    /// </summary>
    public class TbQQPay
    {
        /// <summary>
        /// 途贝QQ钱包wap支付通道主入口
        /// </summary>
        /// <param name="paymode">平台类型（1：安卓 2：IOS 3：H5）</param>
        /// <param name="apptype">风控配置表ID</param>
        /// <param name="code">订单号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格，单位元</param>
        /// <param name="orderid">订单ID</param>
        /// <param name="ip">IP地址</param>
        /// <param name="infoTimes">查询接口信息缓存时间</param>
        /// <param name="appid">应用ID</param>
        /// <returns></returns>
        public InnerResponse TbQQPayInfo(int paymode, int apptype, string code, string goodsname, decimal price, int orderid, string ip, int infoTimes, int appid)
        {
            InnerResponse inn = new InnerResponse();

            switch (paymode)
            {
                case 1://安卓
                    inn = TbQQWapAz(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                case 2://ios
                    inn = TbQQWapIOS(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                case 3://h5
                    inn = TbQQWapH5(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                default:
                    inn = inn.ToResponse(ErrorCode.Code9987);
                    break;
            }

            return inn;
        }

        /// <summary>
        /// 途贝QQwap支付H5调用模式
        /// </summary>
        /// <param name="apptype">风控配置表ID</param>
        /// <param name="code">订单号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格，单位元</param>
        /// <param name="orderid">订单ID</param>
        /// <param name="ip">IP地址</param>
        /// <param name="appid">应用ID</param>
        /// <param name="infoTimes">查询接口信息缓存时间</param>
        /// <returns></returns>
        private InnerResponse TbQQWapH5(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();

            try
            {
                string TbQQWapH5jkhc = "TbQQWapH5jkhc" + appid;//组装缓存key值

                SeIn = SelectUserInfo(TbQQWapH5jkhc, apptype, appid, infoTimes);
                if (SeIn == null || SeIn.PayId <= 0 || string.IsNullOrEmpty(SeIn.UserId) || string.IsNullOrEmpty(SeIn.UserKey))
                {
                    inn = inn.ToResponse(ErrorCode.Code106);
                    return inn;
                }
                if (!UpdateOrde.OrdeUpdateInfo(orderid, SeIn.PayId))
                {
                    inn = inn.ToResponse(ErrorCode.Code101);
                    return inn;
                }
                if (!JudgeMoney.JudgeMinimum(price, SeIn.minmun))
                {
                    inn = inn.ToResponse(ErrorCode.Code8990);
                    return inn;
                }
                if (!JudgeMoney.JudgeMaximum(price, SeIn.maximum))
                {
                    inn = inn.ToResponse(ErrorCode.Code8989);
                    return inn;
                }
                Dictionary<string, string> list = new Dictionary<string, string>();

                list.Add("mch_id", SeIn.UserId);//商户编号
                list.Add("nonce_str", code);//随机字符串
                list.Add("body", goodsname);//商品或支付单简要描述
                list.Add("out_trade_no", code);//订单号
                list.Add("total_fee", (Convert.ToInt32(price * 100)).ToString());//金额 单位分
                list.Add("spbill_create_ip", ip);//ip
                list.Add("notify_url", ConfigurationManager.AppSettings["TbpayNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//通知地址
                list.Add("return_url", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", orderid.ToString()));//页面回调地址
                list.Add("trade_type", "trade.qqpay.native");//交易类型

                string sign = JMP.TOOL.UrlStr.AzGetStr(list) + "&key=" + SeIn.UserKey;
                string md5 = JMP.TOOL.MD5.md5strGet(sign, true).ToUpper();

                list.Add("sign", md5);//签名
                string url = ConfigurationManager.AppSettings["TbPayUrl"].ToString();//请求地址
                string xml = JMP.TOOL.xmlhelper.ToXml(list);
                string srcString = JMP.TOOL.postxmlhelper.postxml(url, xml);
                Dictionary<string, object> dict = JMP.TOOL.xmlhelper.FromXml(srcString);

                if (dict["return_code"].ToString() == "SUCCESS" && dict["result_code"].ToString() == "SUCCESS")
                {
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = dict["prepay_url"].ToString();//http提交方式;
                    inn.IsJump = true;
                }
                else
                {
                    string error = "途贝微信Wap支付错误代码：" + srcString + ",商户号：" + SeIn.UserId;
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + error, summary: "途贝微信Wap支付接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "途贝微信wap接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }


            return inn;
        }

        /// <summary>
        /// 途贝QQwap支付安卓调用模式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <param name="infoTimes">查询接口信息缓存时间</param>
        /// <returns></returns>
        private InnerResponse TbQQWapAz(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {
                string TbQQWapAzjkhc = "TbQQWapAzjkhc" + appid;//组装缓存key值

                SeIn = SelectUserInfo(TbQQWapAzjkhc, apptype, appid, infoTimes);
                if (SeIn == null || SeIn.PayId <= 0 || string.IsNullOrEmpty(SeIn.UserId) || string.IsNullOrEmpty(SeIn.UserKey))
                {
                    inn = inn.ToResponse(ErrorCode.Code106);
                    return inn;
                }
                if (!UpdateOrde.OrdeUpdateInfo(orderid, SeIn.PayId))
                {
                    inn = inn.ToResponse(ErrorCode.Code101);
                    return inn;
                }
                if (!JudgeMoney.JudgeMinimum(price, SeIn.minmun))
                {
                    inn = inn.ToResponse(ErrorCode.Code8990);
                    return inn;
                }
                if (!JudgeMoney.JudgeMaximum(price, SeIn.maximum))
                {
                    inn = inn.ToResponse(ErrorCode.Code8989);
                    return inn;
                }
                Dictionary<string, string> list = new Dictionary<string, string>();

                list.Add("mch_id", SeIn.UserId);//商户编号
                list.Add("nonce_str", code);//随机字符串
                list.Add("body", goodsname);//商品或支付单简要描述
                list.Add("out_trade_no", code);//订单号
                list.Add("total_fee", (Convert.ToInt32(price * 100)).ToString());//金额 单位分
                list.Add("spbill_create_ip", ip);//ip
                list.Add("notify_url", ConfigurationManager.AppSettings["TbpayNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//通知地址
                list.Add("return_url", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", orderid.ToString()));//页面回调地址
                list.Add("trade_type", "trade.qqpay.native");//交易类型

                string sign = JMP.TOOL.UrlStr.AzGetStr(list) + "&key=" + SeIn.UserKey;
                string md5 = JMP.TOOL.MD5.md5strGet(sign, true).ToUpper();

                list.Add("sign", md5);//签名
                string url = ConfigurationManager.AppSettings["TbPayUrl"].ToString();//请求地址
                string xml = JMP.TOOL.xmlhelper.ToXml(list);
                string srcString = JMP.TOOL.postxmlhelper.postxml(url, xml);
                Dictionary<string, object> dict = JMP.TOOL.xmlhelper.FromXml(srcString);

                if (dict["return_code"].ToString() == "SUCCESS" && dict["result_code"].ToString() == "SUCCESS")
                {
                    string QQpay = "{\"data\":\"" + dict["prepay_url"] + "\",\"PaymentType\":\"8\",\"SubType\":\"3\",\"IsH5\":\"1\"}";
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(QQpay, ConfigurationManager.AppSettings["encryption"].ToString());
                }
                else
                {
                    string error = "途贝QQWap支付错误代码：" + srcString + ",商户号：" + SeIn.UserId;
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + error, summary: "途贝QQWap支付接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "途贝QQwap接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }

        /// <summary>
        /// 途贝QQwap支付苹果调用模式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <param name="infoTimes">查询接口信息缓存时间</param>
        /// <returns></returns>
        private InnerResponse TbQQWapIOS(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {
                string TbQQWapIOSjkhc = "TbQQWapIOSjkhc" + appid;//组装缓存key值

                SeIn = SelectUserInfo(TbQQWapIOSjkhc, apptype, appid, infoTimes);
                if (SeIn == null || SeIn.PayId <= 0 || string.IsNullOrEmpty(SeIn.UserId) || string.IsNullOrEmpty(SeIn.UserKey))
                {
                    inn = inn.ToResponse(ErrorCode.Code106);
                    return inn;
                }
                if (!UpdateOrde.OrdeUpdateInfo(orderid, SeIn.PayId))
                {
                    inn = inn.ToResponse(ErrorCode.Code101);
                    return inn;
                }
                if (!JudgeMoney.JudgeMinimum(price, SeIn.minmun))
                {
                    inn = inn.ToResponse(ErrorCode.Code8990);
                    return inn;
                }
                if (!JudgeMoney.JudgeMaximum(price, SeIn.maximum))
                {
                    inn = inn.ToResponse(ErrorCode.Code8989);
                    return inn;
                }
                Dictionary<string, string> list = new Dictionary<string, string>();

                list.Add("mch_id", SeIn.UserId);//商户编号
                list.Add("nonce_str", code);//随机字符串
                list.Add("body", goodsname);//商品或支付单简要描述
                list.Add("out_trade_no", code);//订单号
                list.Add("total_fee", (Convert.ToInt32(price * 100)).ToString());//金额 单位分
                list.Add("spbill_create_ip", ip);//ip
                list.Add("notify_url", ConfigurationManager.AppSettings["TbpayNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//通知地址
                list.Add("return_url", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", orderid.ToString()));//页面回调地址
                list.Add("trade_type", "trade.qqpay.native");//交易类型

                string sign = JMP.TOOL.UrlStr.AzGetStr(list) + "&key=" + SeIn.UserKey;
                string md5 = JMP.TOOL.MD5.md5strGet(sign, true).ToUpper();

                list.Add("sign", md5);//签名
                string url = ConfigurationManager.AppSettings["TbPayUrl"].ToString();//请求地址
                string xml = JMP.TOOL.xmlhelper.ToXml(list);
                string srcString = JMP.TOOL.postxmlhelper.postxml(url, xml);
                Dictionary<string, object> dict = JMP.TOOL.xmlhelper.FromXml(srcString);

                if (dict["return_code"].ToString() == "SUCCESS" && dict["result_code"].ToString() == "SUCCESS")
                {
                    string QQpay = "{\"data\":\"" + dict["prepay_url"] + "\",\"PaymentType\":\"8\",\"SubType\":\"3\",\"IsH5\":\"1\"}";
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(QQpay, ConfigurationManager.AppSettings["encryption"].ToString());
                }
                else
                {
                    string error = "途贝QQWap支付错误代码：" + srcString + ",商户号：" + SeIn.UserId;
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + error, summary: "途贝QQWap支付接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "途贝QQwap接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }

        /// <summary>
        /// 获取支付账号信息
        /// </summary>
        /// <param name="cache">缓存Key</param>
        /// <param name="apptype">风控配置表ID</param>
        /// <param name="appid">应用Id</param>
        /// <param name="infoTimes">查询接口信息缓存时间</param>
        /// <returns></returns>
        private SelectInterface SelectUserInfo(string cache, int apptype, int appid, int infoTimes)
        {
            SelectInterface SeIn = new SelectInterface();

            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(cache))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(cache);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] PayConfigure = dt.Rows[row]["l_str"].ToString().Split(',');
                        SeIn.UserId = PayConfigure[0].Replace("\r", "").Replace("\n", "").Trim();//支付商户账号
                        SeIn.UserKey = PayConfigure[1].Replace("\r", "").Replace("\n", "").Trim();//支付key
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                    }
                    else
                    {
                        dt = bll.SelectPay("TBQQWAP", apptype, appid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] PayConfigure = dt.Rows[row]["l_str"].ToString().Split(',');
                            SeIn.UserId = PayConfigure[0].Replace("\r", "").Replace("\n", "").Trim();//支付商户账号
                            SeIn.UserKey = PayConfigure[1].Replace("\r", "").Replace("\n", "").Trim();//支付key
                            SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                            SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                        }
                        else
                        {
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",获取缓存未成功后在次查询数据！", summary: "途贝QQwap支付支付接口错误", channelId: SeIn.PayId);
                        }
                    }

                }
                else
                {
                    dt = bll.SelectPay("TBQQWAP", apptype, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] PayConfigure = dt.Rows[row]["l_str"].ToString().Split(',');
                        SeIn.UserId = PayConfigure[0].Replace("\r", "").Replace("\n", "").Trim();//支付商户账号
                        SeIn.UserKey = PayConfigure[1].Replace("\r", "").Replace("\n", "").Trim();//支付key
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                    }
                    else
                    {
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",在数据库为查询到数据", summary: "途贝QQwap支付支付接口错误", channelId: SeIn.PayId);
                    }
                }

            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog(bcxx, summary: "途贝QQwap支付支付接口错误应用类型ID：" + apptype, channelId: SeIn.PayId);
            }

            return SeIn;
        }
    }

}
