using DxPay.LogManager.LogFactory.ApiLog;
using JmPayParameter.Models;
using JmPayParameter.PayChannel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace JmPayParameter.PlaceOrder.QQPayType
{
    public class YlQQPay
    {
        /// <summary>
        /// 优乐QQwap支付通道主入口
        /// </summary>
        /// <param name="paymode">平台类型（1：安卓，2：ios，3：H5）</param>
        /// <param name="apptype">应用类型子id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格（单位元）</param>
        /// <param name="orderid">订单id</param>
        /// <param name="ip">ip地址</param>
        /// <param name="infoTime">查询接口信息缓存时间</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        public InnerResponse ylQQPayInfo(int paymode, int apptype, string code, string goodsname, decimal price, int orderid, string ip, int infoTimes, int appid)
        {
            InnerResponse inn = new InnerResponse();
            switch (paymode)
            {
                case 1://安卓方式
                    inn = YLAZ(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                case 2://ios方式
                    inn = YLIos(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                case 3://H5支付方式
                    inn = YLh5(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                default:
                    inn = inn.ToResponse(ErrorCode.Code9987);
                    break;
            }
            return inn;
        }
        /// <summary>
        /// 优乐QQwap支付接口H5模式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="ip">ip地址</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse YLh5(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {
                string YLh5 = "YLh5" + appid;//组装缓存key值

                SeIn = SelectUserInfo(YLh5, apptype, appid, infoTimes);
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
                list.Add("mch_id", SeIn.UserId);//商户号
                list.Add("nonce_str", code);//随机字符串
                list.Add("body", goodsname);//商品描述
                list.Add("out_trade_no", code);//商户订单号
                list.Add("total_fee", (Convert.ToInt32(price * 100)).ToString());//总金额（单位分）
                list.Add("spbill_create_ip",ip); //终端 IP
                list.Add("notify_url", ConfigurationManager.AppSettings["YlWxWapNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//通知地址
                list.Add("return_url", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", orderid.ToString()));//页面回调地址
                list.Add("trade_type", "trade.qqpay.native");//交易类型
                string attach = JMP.TOOL.MD5.md5strGet(SeIn.UserId + code + SeIn.UserKey, true).ToUpper();
                list.Add("attach", attach);//附加信息
                string sign = JMP.TOOL.UrlStr.AzGetStr(list) + "&key=" + SeIn.UserKey;
                string md5 = JMP.TOOL.MD5.md5strGet(sign, true).ToUpper();
                list.Add("sign", md5);//签名
                string url = ConfigurationManager.AppSettings["YlWxWapPostUrl"].ToString();//请求地址
                string xml = JMP.TOOL.xmlhelper.ToXml(list);
                string srcString = JMP.TOOL.postxmlhelper.postxml(url, xml);
                Dictionary<string, object> dict = JMP.TOOL.xmlhelper.FromXml(srcString);

                if (dict["return_code"].ToString() == "SUCCESS" && dict["result_code"].ToString() == "SUCCESS")
                {
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = dict["code_url"].ToString();//http提交方式;
                    inn.IsJump = true;
                }
                else
                {
                    string error = "优乐QQwap支付错误代码：" + srcString + ",商户号：" + SeIn.UserId;
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + error, summary: "优乐QQwap支付接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "优乐QQwap接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }

            return inn;
        }


        /// <summary>
        /// 优乐QQwap支付接口ios模式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="ip">ip地址</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse YLIos(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {
                string YLIos = "YLIos" + appid;//组装缓存key值

                SeIn = SelectUserInfo(YLIos, apptype, appid, infoTimes);
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
                list.Add("mch_id", SeIn.UserId);//商户号
                list.Add("nonce_str", code);//随机字符串
                list.Add("body", goodsname);//商品描述
                list.Add("out_trade_no", code);//商户订单号
                list.Add("total_fee", (Convert.ToInt32(price * 100)).ToString());//总金额（单位分）
                list.Add("spbill_create_ip", ip); //终端 IP
                list.Add("notify_url", ConfigurationManager.AppSettings["YlWxWapNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//通知地址
                list.Add("return_url", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", orderid.ToString()));//页面回调地址
                list.Add("trade_type", "trade.qqpay.native");//交易类型
                string attach = JMP.TOOL.MD5.md5strGet(SeIn.UserId + code + SeIn.UserKey, true).ToUpper();
                list.Add("attach", attach);//附加信息
                string sign = JMP.TOOL.UrlStr.AzGetStr(list) + "&key=" + SeIn.UserKey;
                string md5 = JMP.TOOL.MD5.md5strGet(sign, true).ToUpper();
                list.Add("sign", md5);//签名
                string url = ConfigurationManager.AppSettings["YlWxWapPostUrl"].ToString();//请求地址
                string xml = JMP.TOOL.xmlhelper.ToXml(list);
                string srcString = JMP.TOOL.postxmlhelper.postxml(url, xml);
                Dictionary<string, object> dict = JMP.TOOL.xmlhelper.FromXml(srcString);

                if (dict["result_code"].ToString() == "SUCCESS" && dict["return_code"].ToString() == "SUCCESS")
                {
                    string wxpay = "{\"data\":\"" + dict["code_url"].ToString() + "\",\"PaymentType\":\"8\",\"SubType\":\"4\",\"IsH5\":\"1\"}";
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(wxpay, ConfigurationManager.AppSettings["encryption"].ToString());
                }
                else
                {
                    string error = "优乐QQwap支付错误代码：" + srcString + ",商户号：" + SeIn.UserId;
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + error, summary: "优乐QQwap支付接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "优乐QQwap接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }

        /// <summary>
        /// 优乐QQwap支付接口安卓模式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="ip">ip地址</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse YLAZ(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {
                string YLAZ = "YLAZ" + appid;//组装缓存key值

                SeIn = SelectUserInfo(YLAZ, apptype, appid, infoTimes);
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
                list.Add("mch_id", SeIn.UserId);//商户号
                list.Add("nonce_str", code);//随机字符串
                list.Add("body", goodsname);//商品描述
                list.Add("detail", "app_name=" + goodsname + "&bundle_id=com.tencent.wzryIOS");//商品详情
                list.Add("out_trade_no", code);//商户订单号
                list.Add("total_fee", (Convert.ToInt32(price * 100)).ToString());//总金额（单位分）
                list.Add("spbill_create_ip",ip); //终端 IP
                list.Add("notify_url", ConfigurationManager.AppSettings["YlWxWapNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//通知地址
                list.Add("return_url", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", orderid.ToString()));//页面回调地址
                list.Add("trade_type", "trade.qqpay.native");//交易类型
                string attach = JMP.TOOL.MD5.md5strGet(SeIn.UserId + code + SeIn.UserKey, true).ToUpper();
                list.Add("attach", attach);//附加信息
                string sign = JMP.TOOL.UrlStr.AzGetStr(list) + "&key=" + SeIn.UserKey;
                string md5 = JMP.TOOL.MD5.md5strGet(sign, true).ToUpper();
                list.Add("sign", md5);//签名
                string url = ConfigurationManager.AppSettings["YlWxWapPostUrl"].ToString();//请求地址
                string xml = JMP.TOOL.xmlhelper.ToXml(list);
                string srcString = JMP.TOOL.postxmlhelper.postxml(url, xml);
                Dictionary<string, object> dict = JMP.TOOL.xmlhelper.FromXml(srcString);

                if (dict["result_code"].ToString() == "SUCCESS" && dict["return_code"].ToString() == "SUCCESS")
                {
                    string wxpay = "{\"data\":\"" + dict["code_url"].ToString() + "\",\"PaymentType\":\"8\",\"SubType\":\"4\",\"IsH5\":\"1\"}";
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(wxpay, ConfigurationManager.AppSettings["encryption"].ToString());
                }
                else
                {
                    string error = "优乐QQwap支付错误代码：" + srcString + ",商户号：" + SeIn.UserId;
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + error, summary: "优乐QQwap支付接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "优乐QQwap接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }

        /// <summary>
        /// 获取优乐账号信息
        /// </summary>
        /// <param name="cache">缓存key</param>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="appid">应用id</param>
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
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的汇元QQwap支付账号
                        SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的汇元QQwap支付key
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                    }
                    else
                    {

                        dt = bll.SelectPay("YLQQWAP", apptype, appid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                            SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取汇元QQwap支付账号
                            SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取汇元QQwap支付key
                            SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                            SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                        }
                        else
                        {
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",获取缓存失败后，从数据库未查询到相关信息！", summary: "优乐QQwap支付支付接口错误", channelId: SeIn.PayId);
                        }
                    }
                }
                else
                {
                    dt = bll.SelectPay("YLQQWAP", apptype, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取汇元QQwap支付账号
                        SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取汇元QQwap支付key
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                    }
                    else
                    {
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",从数据库未查询到相关信息！", summary: "优乐QQwap支付支付接口错误", channelId: SeIn.PayId);
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog(bcxx, summary: "优乐QQwap支付支付接口错误应用类型ID：" + apptype, channelId: SeIn.PayId);
            }
            return SeIn;
        }
    }
}
