using Alipay;
using DxPay.LogManager.LogFactory.ApiLog;
using JMP.TOOL;
using JmPayParameter.Models;
using JmPayParameter.PayChannel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JmPayParameter.PlaceOrder.AlpayType
{
    /// <summary>
    /// 支付宝扫码封装
    /// </summary>
    public class IAlPaySmPacking
    {
        /// <summary>
        /// 支付宝官方扫码封装wap支付通道入口
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
        public InnerResponse IAlPaySmPackingInfo(int paymode, int apptype, string code, string goodsname, decimal price, int orderid, string ip, int infoTimes, int appid)
        {
            InnerResponse inn = new InnerResponse();
            switch (paymode)
            {
                case 1://安卓方式
                    inn = IAlPaySmPackingAZ(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                case 2://ios方式
                    inn = IAlPaySmPackingIos(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                case 3://H5方式
                    inn = IAlPaySmPackingH5(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                default:
                    inn = inn.ToResponse(ErrorCode.Code9987);
                    break;
            }
            return inn;
        }

        /// <summary>
        /// 支付宝官方扫码封装wapH5通道
        /// </summary>
        /// <param name="apptype">应用类型子id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格（单位元）</param>
        /// <param name="orderid">订单id</param>
        /// <param name="ip">ip地址</param>
        /// <param name="infoTime">查询接口信息缓存时间</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse IAlPaySmPackingH5(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();

            SelectInterface SeIn = new SelectInterface();

            try
            {
                string IAlPaySmPackingH5 = "IAlPaySmPackingH5" + appid;//组装缓存key值
                SeIn = SelectUserInfo(IAlPaySmPackingH5, apptype, appid, infoTimes);
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

                //公共请求参数
                Dictionary<string, string> List = new Dictionary<string, string>();
                List.Add("app_id", SeIn.UserId);//支付宝应用ID
                List.Add("method", "alipay.trade.precreate");//接口名称（请求类型）
                List.Add("charset", "utf-8");//请求使用的编码格式
                List.Add("sign_type", "RSA");//商户生成签名字符串所使用的签名算法类型，目前支持RSA2和RSA，推荐使用RSA2
                List.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));//发送请求的时间
                List.Add("version", "1.0"); //调用的接口版本
                List.Add("notify_url", ConfigurationManager.AppSettings["TokenUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//异步通知地址。
                //请求参数
                Dictionary<string, string> Orlist = new Dictionary<string, string>();
                Orlist.Add("out_trade_no", code);//商户订单号
                Orlist.Add("total_amount", price.ToString()); //订单总金额，单位为元，精确到小数点后两位，
                Orlist.Add("subject", goodsname);//订单标题
                Orlist.Add("body", goodsname);//对交易或商品的描述
                string overtime = (int.Parse(ConfigurationManager.AppSettings["overtime"].ToString()) / 60) + "m";
                Orlist.Add("timeout_express", overtime);//该笔订单允许的最晚付款时间，逾期将关闭交易。

                string biz_content = JsonHelper.DictJsonstr(Orlist);
                List.Add("biz_content", biz_content);

                string SignStr = UrlStr.AzGetStr(List);
                //签名
                string Sign = RSAFromPkcs8.sign(SignStr, SeIn.UserKey, "utf-8");
                //签名 注释：get请求时必须采用url编码方式（HttpUtility.UrlEncode）
                List.Add("sign", HttpUtility.UrlEncode(Sign));
                //请求地址
                string url = ConfigurationManager.AppSettings["AliPaySmUrl"].ToString();
                string Urlstr = url + UrlStr.AzGetStr(List);

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(Urlstr);    //创建一个请求示例
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();  //获取响应，即发送请求
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                string srcString = streamReader.ReadToEnd();

                Root root = new Root();
                //对返回参数转换格式
                root = JsonHelper.Deserializes<Root>(srcString);

                if (root != null && root.alipay_trade_precreate_response.code == "10000" && root.alipay_trade_precreate_response.msg == "Success")
                {
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = root.alipay_trade_precreate_response.qr_code;//http提交方式;
                    inn.IsJump = true;
                }
                else
                {
                    string error = "支付宝官方扫码封装wap支付错误代码：" + srcString + ",商户号：" + SeIn.UserId;
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + error, summary: "支付宝官方扫码封装wap支付接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }


            }
            catch (Exception ex)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + ex.ToString(), summary: "支付宝官方扫码封装wap接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }

            return inn;
        }


        /// <summary>
        /// 支付宝官方扫码封装wap安卓通道
        /// </summary>
        /// <param name="apptype">应用类型子id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格（单位元）</param>
        /// <param name="orderid">订单id</param>
        /// <param name="ip">ip地址</param>
        /// <param name="infoTime">查询接口信息缓存时间</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse IAlPaySmPackingAZ(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();

            SelectInterface SeIn = new SelectInterface();

            try
            {
                string IAlPaySmPackingAZ = "IAlPaySmPackingAZ" + appid;//组装缓存key值
                SeIn = SelectUserInfo(IAlPaySmPackingAZ, apptype, appid, infoTimes);
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

                //公共请求参数
                Dictionary<string, string> List = new Dictionary<string, string>();
                List.Add("app_id", SeIn.UserId);//支付宝应用ID
                List.Add("method", "alipay.trade.precreate");//接口名称（请求类型）
                List.Add("charset", "utf-8");//请求使用的编码格式
                List.Add("sign_type", "RSA");//商户生成签名字符串所使用的签名算法类型，目前支持RSA2和RSA，推荐使用RSA2
                List.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));//发送请求的时间
                List.Add("version", "1.0"); //调用的接口版本
                List.Add("notify_url", ConfigurationManager.AppSettings["TokenUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//异步通知地址。
                //请求参数
                Dictionary<string, string> Orlist = new Dictionary<string, string>();
                Orlist.Add("out_trade_no", code);//商户订单号
                Orlist.Add("total_amount", price.ToString()); //订单总金额，单位为元，精确到小数点后两位，
                Orlist.Add("subject", goodsname);//订单标题
                Orlist.Add("body", goodsname);//对交易或商品的描述
                string overtime = (int.Parse(ConfigurationManager.AppSettings["overtime"].ToString()) / 60) + "m";
                Orlist.Add("timeout_express", overtime);//该笔订单允许的最晚付款时间，逾期将关闭交易。

                string biz_content = JsonHelper.DictJsonstr(Orlist);
                List.Add("biz_content", biz_content);

                string SignStr = UrlStr.AzGetStr(List);
                //签名
                string Sign = RSAFromPkcs8.sign(SignStr, SeIn.UserKey, "utf-8");
                //签名 注释：get请求时必须采用url编码方式（HttpUtility.UrlEncode）
                List.Add("sign", HttpUtility.UrlEncode(Sign));
                //请求地址
                string url = ConfigurationManager.AppSettings["AliPaySmUrl"].ToString();
                string Urlstr = url + UrlStr.AzGetStr(List);

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(Urlstr);    //创建一个请求示例
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();  //获取响应，即发送请求
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                string srcString = streamReader.ReadToEnd();

                Root root = new Root();
                //对返回参数转换格式
                root = JsonHelper.Deserializes<Root>(srcString);

                if (root != null && root.alipay_trade_precreate_response.code == "10000" && root.alipay_trade_precreate_response.msg == "Success")
                {
                    string Alpay = "{\"data\":\"" + root.alipay_trade_precreate_response.qr_code + "\",\"PaymentType\":\"1\",\"SubType\":\"8\",\"IsH5\":\"1\"}";
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(Alpay, ConfigurationManager.AppSettings["encryption"].ToString());
                }
                else
                {
                    string error = "支付宝官方扫码封装wap支付错误代码：" + srcString + ",商户号：" + SeIn.UserId;
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + error, summary: "支付宝官方扫码封装wap支付接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }


            }
            catch (Exception ex)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + ex.ToString(), summary: "支付宝官方扫码封装wap接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }

            return inn;
        }

        /// <summary>
        /// 支付宝官方扫码封装wap IOS通道
        /// </summary>
        /// <param name="apptype">应用类型子id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格（单位元）</param>
        /// <param name="orderid">订单id</param>
        /// <param name="ip">ip地址</param>
        /// <param name="infoTime">查询接口信息缓存时间</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse IAlPaySmPackingIos(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();

            SelectInterface SeIn = new SelectInterface();

            try
            {
                string IAlPaySmPackingIos = "IAlPaySmPackingIos" + appid;//组装缓存key值
                SeIn = SelectUserInfo(IAlPaySmPackingIos, apptype, appid, infoTimes);
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

                //公共请求参数
                Dictionary<string, string> List = new Dictionary<string, string>();
                List.Add("app_id", SeIn.UserId);//支付宝应用ID
                List.Add("method", "alipay.trade.precreate");//接口名称（请求类型）
                List.Add("charset", "utf-8");//请求使用的编码格式
                List.Add("sign_type", "RSA");//商户生成签名字符串所使用的签名算法类型，目前支持RSA2和RSA，推荐使用RSA2
                List.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));//发送请求的时间
                List.Add("version", "1.0"); //调用的接口版本
                List.Add("notify_url", ConfigurationManager.AppSettings["TokenUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//异步通知地址。
                //请求参数
                Dictionary<string, string> Orlist = new Dictionary<string, string>();
                Orlist.Add("out_trade_no", code);//商户订单号
                Orlist.Add("total_amount", price.ToString()); //订单总金额，单位为元，精确到小数点后两位，
                Orlist.Add("subject", goodsname);//订单标题
                Orlist.Add("body", goodsname);//对交易或商品的描述
                string overtime = (int.Parse(ConfigurationManager.AppSettings["overtime"].ToString()) / 60) + "m";
                Orlist.Add("timeout_express", overtime);//该笔订单允许的最晚付款时间，逾期将关闭交易。

                string biz_content = JsonHelper.DictJsonstr(Orlist);
                List.Add("biz_content", biz_content);

                string SignStr = UrlStr.AzGetStr(List);
                //签名
                string Sign = RSAFromPkcs8.sign(SignStr, SeIn.UserKey, "utf-8");
                //签名 注释：get请求时必须采用url编码方式（HttpUtility.UrlEncode）
                List.Add("sign", HttpUtility.UrlEncode(Sign));
                //请求地址
                string url = ConfigurationManager.AppSettings["AliPaySmUrl"].ToString();
                string Urlstr = url + UrlStr.AzGetStr(List);

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(Urlstr);    //创建一个请求示例
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();  //获取响应，即发送请求
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                string srcString = streamReader.ReadToEnd();

                Root root = new Root();
                //对返回参数转换格式
                root = JsonHelper.Deserializes<Root>(srcString);

                if (root != null && root.alipay_trade_precreate_response.code == "10000" && root.alipay_trade_precreate_response.msg == "Success")
                {
                    string Alpay = "{\"data\":\"" + root.alipay_trade_precreate_response.qr_code + "\",\"PaymentType\":\"1\",\"SubType\":\"8\",\"IsH5\":\"1\"}";
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(Alpay, ConfigurationManager.AppSettings["encryption"].ToString());
                }
                else
                {
                    string error = "支付宝官方扫码封装wap支付错误代码：" + srcString + ",商户号：" + SeIn.UserId;
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + error, summary: "支付宝官方扫码封装wap支付接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }


            }
            catch (Exception ex)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + ex.ToString(), summary: "支付宝官方扫码封装wap接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }

            return inn;
        }

        /// <summary>
        /// 获取支付宝官方扫码封装wap账号信息
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
                        SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的支付宝扫码支付账号
                        SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的支付宝扫码支付key
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额

                    }
                    else
                    {

                        dt = bll.SelectPay("ZFBSMPACK", apptype, appid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                            SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取支付宝扫码支付账号
                            SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取支付宝扫码支付key
                            SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                            SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                        }
                        else
                        {
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",获取缓存未成功后在次查询数据！", summary: "支付宝官方扫码封装wap支付支付接口错误", channelId: SeIn.PayId);
                        }
                    }
                }
                else
                {
                    dt = bll.SelectPay("ZFBSMPACK", apptype, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取支付宝扫码支付账号
                        SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取支付宝扫码支付key
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                    }
                    else
                    {
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",在数据库为查询到数据", summary: "支付宝官方扫码封装wap支付接口错误", channelId: SeIn.PayId);
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog(bcxx, summary: "支付宝官方扫码封装wap支付支付接口错误应用类型ID：" + apptype, channelId: SeIn.PayId);
            }
            return SeIn;
        }

        #region 返回参数
        private class Alipay_trade_precreate_response
        {
            /// <summary>
            /// 
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string msg { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string out_trade_no { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string qr_code { get; set; }
        }

        private class Root
        {
            /// <summary>
            /// 
            /// </summary>
            public Alipay_trade_precreate_response alipay_trade_precreate_response { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string sign { get; set; }
        }
        #endregion
    }
}
