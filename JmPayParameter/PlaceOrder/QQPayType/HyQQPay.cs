using JmPayParameter.PayChannel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using DxPay.LogManager.LogFactory.ApiLog;
using JmPayParameter.Models;
using System.IO;
using System.Text.RegularExpressions;

namespace JmPayParameter.PlaceOrder.QQPayType
{
    /// <summary>
    /// 汇元QQwap支付主入口
    /// </summary>
    public class HyQQPay
    {
        /// <summary>
        /// 汇元QQwap支付通道主入口
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
        public InnerResponse HyQQPayInfo(int paymode, int apptype, string code, string goodsname, decimal price, int orderid, string ip, int infoTimes, int appid)
        {
            InnerResponse inn = new InnerResponse();
            switch (paymode)
            {
                case 1://安卓方式
                    inn = HyQQWaPAz(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                case 2://ios方式
                    inn = HyQQWaPIOS(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                case 3://H5支付方式
                    inn = HyQQWaPH5(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                default:
                    inn = inn.ToResponse(ErrorCode.Code9987);
                    break;
            }
            return inn;
        }
        #region 汇元QQwap支付
        /// <summary>
        /// 汇元QQwap支付h5调用模式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse HyQQWaPH5(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {
                string HyQQWaPH5jkhc = "HyQQWaPH5jkhc" + appid;//组装缓存key值

                SeIn = SelectUserInfo(HyQQWaPH5jkhc, apptype, appid, infoTimes);
                if (SeIn == null || SeIn.PayId <= 0 || string.IsNullOrEmpty(SeIn.UserId) || string.IsNullOrEmpty(SeIn.UserKey))
                {
                    inn = inn.ToResponse(ErrorCode.Code106);
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

                System.Collections.Specialized.NameValueCollection Palist = new System.Collections.Specialized.NameValueCollection();

                Palist.Add("version", "1");//当前接口版本号1
                Palist.Add("pay_type", "31"); //支付类型31，（数据类型：int）
                Palist.Add("agent_id", SeIn.UserId);//商户编号
                Palist.Add("agent_bill_id", code);//商户系统内部的订单号（要保证唯一）。长度最长50字符
                Palist.Add("pay_amt", price.ToString());//单位：元
                Palist.Add("user_ip", ip.Replace(".", "_"));//IP
                Palist.Add("agent_bill_time", DateTime.Now.ToString("yyyyMMddHHmmss"));//提交单据的时间
                string GotoUrlName = ConfigurationManager.AppSettings["GotoUrlName"];//同步跳转域名
                string RetunUrl = ConfigurationManager.AppSettings["RetunUrl"];//异步跳转域名
                if (!string.IsNullOrEmpty(SeIn.RequestUrl))
                {
                    Palist.Add("goods_name", goodsname);//商品名称 
                }
                else
                {
                    Palist.Add("goods_name", HttpUtility.UrlEncode(goodsname, Encoding.GetEncoding("gb2312")));//支付说明
                }

                if (!string.IsNullOrEmpty(SeIn.ReturnUrl))
                {
                    Palist.Add("notify_url", ConfigurationManager.AppSettings["HyNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()).Replace(RetunUrl, SeIn.ReturnUrl));//异步通知地址
                }
                else
                {
                    Palist.Add("notify_url", ConfigurationManager.AppSettings["HyNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//异步通知地址
                }

                if (!string.IsNullOrEmpty(SeIn.GotoURL))
                {
                    Palist.Add("return_url", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", orderid.ToString()).Replace(GotoUrlName, SeIn.GotoURL));//同步通知地址
                }
                else
                {
                    Palist.Add("return_url", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", orderid.ToString()));//同步通知地址
                }

                Palist.Add("timestamp", JMP.TOOL.WeekDateTime.GetMiliss);//时间戳13位
                Palist.Add("is_phone", "0");
                string md5sing = "version=" + Palist["version"] + "&agent_id=" + Palist["agent_id"] + "&agent_bill_id=" + Palist["agent_bill_id"] + "&agent_bill_time=" + Palist["agent_bill_time"] + "&pay_type=" + Palist["pay_type"] + "&pay_amt=" + Palist["pay_amt"] + "&notify_url=" + Palist["notify_url"] + "&return_url=" + Palist["return_url"] + "&user_ip=" + Palist["user_ip"] + "&key=" + SeIn.UserKey + "&timestamp=" + Palist["timestamp"];
                string md5str = JMP.TOOL.MD5.md5strGet(md5sing, true);
                Palist.Add("sign", md5str);//签名
                UpdateOrdes uporder = new UpdateOrdes();
                if (!uporder.OrdeUpdateInfo(orderid, SeIn.PayId, code))
                {
                    inn = inn.ToResponse(ErrorCode.Code101);
                    return inn;
                }
                //参数
                string data = "";
                string url = "";
                if (!string.IsNullOrEmpty(SeIn.RequestUrl))
                {
                    data = JMP.TOOL.UrlStr.GetStrNV(Palist) + "&url=" + ConfigurationManager.AppSettings["HyPOSTUrl"].ToString() + "&jmtype=HY";
                    // url = "http://192.168.1.54:52682/H5/Jump" + "?" + data;//请求地址
                    url = SeIn.RequestUrl.Contains("http") || SeIn.RequestUrl.Contains("https") ? SeIn.RequestUrl : "http://" + SeIn.RequestUrl;//请求地址
                    url = url + "/H5/Jump" + "?" + data;
                }
                else
                {
                    data = JMP.TOOL.UrlStr.GetStrNV(Palist);
                    url = ConfigurationManager.AppSettings["HyPOSTUrl"].ToString() + "?" + data;//请求地址
                }
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);    //创建一个请求示例
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();  //获取响应，即发送请求
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("GB2312"));
                string srcString = streamReader.ReadToEnd();

                if (!string.IsNullOrEmpty(srcString))
                {
                    //判断是否有返回支付连接
                    string QQPay = JMP.TOOL.Regular.IshiQQPay(srcString);
                    if (!string.IsNullOrEmpty(QQPay))
                    {
                        string QQPayUrl = JMP.TOOL.Regular.IshiQQPayUrl(QQPay);
                        if (!string.IsNullOrEmpty(QQPayUrl))
                        {
                            inn = inn.ToResponse(ErrorCode.Code100);
                            inn.ExtraData = QQPayUrl.Replace("value=", "").Replace("\"", "").Replace("amp;", "").Trim();//http提交方式;
                            inn.IsJump = true;
                        }
                        else
                        {
                            string error = "汇元QQWap支付正则表达式匹配URL错误代码：" + srcString + ",商户号：" + SeIn.UserId;
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + error, summary: "汇元QQWap支付接口错误信息", channelId: SeIn.PayId);
                            inn = inn.ToResponse(ErrorCode.Code104);
                        }
                    }
                    else
                    {
                        string error = "汇元QQWap支付正则表达式匹配支付地址是否存在错误代码：" + srcString + ",商户号：" + SeIn.UserId;
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + error, summary: "汇元QQWap支付接口错误信息", channelId: SeIn.PayId);
                        inn = inn.ToResponse(ErrorCode.Code104);

                    }

                }
                else
                {
                    string error = "汇元QQWap支付错误代码：" + srcString + ",商户号：" + SeIn.UserId;
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + error, summary: "汇元QQWap支付接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }

            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "汇元QQwap接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }
        /// <summary>
        /// 汇元QQwap支付安卓调用模式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse HyQQWaPAz(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {

                string HyQQWaPH5Azjkhc = "HyQQWaPH5Azjkhc" + appid;//组装缓存key值

                SeIn = SelectUserInfo(HyQQWaPH5Azjkhc, apptype, appid, infoTimes);
                if (SeIn == null || SeIn.PayId <= 0 || string.IsNullOrEmpty(SeIn.UserId) || string.IsNullOrEmpty(SeIn.UserKey))
                {
                    inn = inn.ToResponse(ErrorCode.Code106);
                    return inn;
                }
                UpdateOrdes uporder = new UpdateOrdes();
                if (!uporder.OrdeUpdateInfo(orderid, SeIn.PayId, code))
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

                System.Collections.Specialized.NameValueCollection Palist = new System.Collections.Specialized.NameValueCollection();

                Palist.Add("version", "1");//当前接口版本号1
                Palist.Add("pay_type", "31"); //支付类型31，（数据类型：int）
                Palist.Add("agent_id", SeIn.UserId);//商户编号
                Palist.Add("agent_bill_id", code);//商户系统内部的订单号（要保证唯一）。长度最长50字符
                Palist.Add("pay_amt", price.ToString());//单位：元

                Palist.Add("user_ip", ip.Replace(".", "_"));//IP
                Palist.Add("agent_bill_time", DateTime.Now.ToString("yyyyMMddHHmmss"));//提交单据的时间
                string GotoUrlName = ConfigurationManager.AppSettings["GotoUrlName"];//同步跳转域名
                string RetunUrl = ConfigurationManager.AppSettings["RetunUrl"];//异步跳转域名                                                                     
                // Palist.Add("goods_name", HttpUtility.UrlEncode(goodsname, Encoding.GetEncoding("GBK")));//商品名称
                if (!string.IsNullOrEmpty(SeIn.RequestUrl))
                {
                    Palist.Add("goods_name", goodsname);//商品名称 
                }
                else
                {
                    Palist.Add("goods_name", HttpUtility.UrlEncode(goodsname, Encoding.GetEncoding("gb2312")));//支付说明
                }

                if (!string.IsNullOrEmpty(SeIn.ReturnUrl))
                {
                    Palist.Add("notify_url", ConfigurationManager.AppSettings["HyNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()).Replace(RetunUrl, SeIn.ReturnUrl));//异步通知地址
                }
                else
                {
                    Palist.Add("notify_url", ConfigurationManager.AppSettings["HyNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//异步通知地址
                }

                if (!string.IsNullOrEmpty(SeIn.GotoURL))
                {
                    Palist.Add("return_url", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", orderid.ToString()).Replace(GotoUrlName, SeIn.GotoURL));//同步通知地址
                }
                else
                {
                    Palist.Add("return_url", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", orderid.ToString()));//同步通知地址
                }

                //Palist.Add("remark", HttpUtility.UrlEncode(goodsname, Encoding.GetEncoding("GBK")));//商户自定义 原样返回
                Palist.Add("timestamp", JMP.TOOL.WeekDateTime.GetMiliss);//时间戳13位

                string md5sing = "version=" + Palist["version"] + "&agent_id=" + Palist["agent_id"] + "&agent_bill_id=" + Palist["agent_bill_id"] + "&agent_bill_time=" + Palist["agent_bill_time"] + "&pay_type=" + Palist["pay_type"] + "&pay_amt=" + Palist["pay_amt"] + "&notify_url=" + Palist["notify_url"] + "&return_url=" + Palist["return_url"] + "&user_ip=" + Palist["user_ip"] + "&key=" + SeIn.UserKey + "&timestamp=" + Palist["timestamp"];

                string md5str = JMP.TOOL.MD5.md5strGet(md5sing, true);
                Palist.Add("sign", md5str);//签名

                //参数

                string data = "";
                string url = "";
                if (!string.IsNullOrEmpty(SeIn.RequestUrl))
                {
                    data = JMP.TOOL.UrlStr.GetStrNV(Palist) + "&url=" + ConfigurationManager.AppSettings["HyPOSTUrl"].ToString() + "&jmtype=HY";
                    url = SeIn.RequestUrl.Contains("http") || SeIn.RequestUrl.Contains("https") ? SeIn.RequestUrl : "http://" + SeIn.RequestUrl;//请求地址
                    url = url + "/H5/Jump" + "?" + data;
                }
                else
                {
                    data = JMP.TOOL.UrlStr.GetStrNV(Palist);
                    url = ConfigurationManager.AppSettings["HyPOSTUrl"].ToString() + "?" + data;//请求地址
                }
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);    //创建一个请求示例
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();  //获取响应，即发送请求
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("GB2312"));
                string srcString = streamReader.ReadToEnd();

                if (!string.IsNullOrEmpty(srcString))
                {
                    //判断是否有返回支付连接
                    string QQPay = JMP.TOOL.Regular.IshiQQPay(srcString);
                    if (!string.IsNullOrEmpty(QQPay))
                    {
                        string QQPayUrl = JMP.TOOL.Regular.IshiQQPayUrl(QQPay);
                        if (!string.IsNullOrEmpty(QQPayUrl))
                        {

                            string QQurl = QQPayUrl.Replace("value=", "").Replace("\"", "").Replace("amp;", "").Trim();
                            string QQpay = "{\"data\":\"" + QQurl + "\",\"PaymentType\":\"8\",\"SubType\":\"1\",\"IsH5\":\"1\"}";
                            inn = inn.ToResponse(ErrorCode.Code100);
                            inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(QQpay, ConfigurationManager.AppSettings["encryption"].ToString());

                        }
                        else
                        {
                            string error = "汇元QQWap支付正则表达式匹配URL错误代码：" + srcString + ",商户号：" + SeIn.UserId;
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + error, summary: "汇元QQWap支付接口错误信息", channelId: SeIn.PayId);
                            inn = inn.ToResponse(ErrorCode.Code104);
                        }
                    }
                    else
                    {
                        string error = "汇元QQWap支付正则表达式匹配支付地址是否存在错误代码：" + srcString + ",商户号：" + SeIn.UserId;
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + error, summary: "汇元QQWap支付接口错误信息", channelId: SeIn.PayId);
                        inn = inn.ToResponse(ErrorCode.Code104);

                    }

                }
                else
                {
                    string error = "汇元QQWap支付错误代码：" + srcString + ",商户号：" + SeIn.UserId;
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + error, summary: "汇元QQWap支付接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }

            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "汇元QQwap接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }

        /// <summary>
        /// 汇元QQwap支付苹果调用模式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse HyQQWaPIOS(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {
                string HyQQWaPIOSjkhc = "HyQQWaPIOSjkhc" + appid;//组装缓存key值

                SeIn = SelectUserInfo(HyQQWaPIOSjkhc, apptype, appid, infoTimes);
                if (SeIn == null || SeIn.PayId <= 0 || string.IsNullOrEmpty(SeIn.UserId) || string.IsNullOrEmpty(SeIn.UserKey))
                {
                    inn = inn.ToResponse(ErrorCode.Code106);
                    return inn;
                }
                UpdateOrdes uporder = new UpdateOrdes();
                if (!uporder.OrdeUpdateInfo(orderid, SeIn.PayId, code))
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
                System.Collections.Specialized.NameValueCollection Palist = new System.Collections.Specialized.NameValueCollection();

                Palist.Add("version", "1");//当前接口版本号1
                Palist.Add("pay_type", "31"); //支付类型31，（数据类型：int）
                Palist.Add("agent_id", SeIn.UserId);//商户编号
                Palist.Add("agent_bill_id", code);//商户系统内部的订单号（要保证唯一）。长度最长50字符
                Palist.Add("pay_amt", price.ToString());//单位：元

                Palist.Add("user_ip", ip.Replace(".", "_"));//IP
                Palist.Add("agent_bill_time", DateTime.Now.ToString("yyyyMMddHHmmss"));//提交单据的时间
                string GotoUrlName = ConfigurationManager.AppSettings["GotoUrlName"];//同步跳转域名
                string RetunUrl = ConfigurationManager.AppSettings["RetunUrl"];//异步跳转域名  
                // Palist.Add("goods_name", HttpUtility.UrlEncode(goodsname, Encoding.GetEncoding("GBK")));//商品名称
                if (!string.IsNullOrEmpty(SeIn.RequestUrl))
                {
                    Palist.Add("goods_name", goodsname);//商品名称
                }
                else
                {
                    Palist.Add("goods_name", HttpUtility.UrlEncode(goodsname, Encoding.GetEncoding("gb2312")));//支付说明
                }

                if (!string.IsNullOrEmpty(SeIn.ReturnUrl))
                {
                    Palist.Add("notify_url", ConfigurationManager.AppSettings["HyNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()).Replace(RetunUrl, SeIn.ReturnUrl));//异步通知地址
                }
                else
                {
                    Palist.Add("notify_url", ConfigurationManager.AppSettings["HyNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//异步通知地址
                }

                if (!string.IsNullOrEmpty(SeIn.GotoURL))
                {
                    Palist.Add("return_url", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", orderid.ToString()).Replace(GotoUrlName, SeIn.GotoURL));//同步通知地址
                }
                else
                {
                    Palist.Add("return_url", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", orderid.ToString()));//同步通知地址
                }
                //  Palist.Add("remark", HttpUtility.UrlEncode(goodsname, Encoding.GetEncoding("GBK")));//商户自定义 原样返回
                Palist.Add("timestamp", JMP.TOOL.WeekDateTime.GetMiliss);//时间戳13位

                string md5sing = "version=" + Palist["version"] + "&agent_id=" + Palist["agent_id"] + "&agent_bill_id=" + Palist["agent_bill_id"] + "&agent_bill_time=" + Palist["agent_bill_time"] + "&pay_type=" + Palist["pay_type"] + "&pay_amt=" + Palist["pay_amt"] + "&notify_url=" + Palist["notify_url"] + "&return_url=" + Palist["return_url"] + "&user_ip=" + Palist["user_ip"] + "&key=" + SeIn.UserKey + "&timestamp=" + Palist["timestamp"];

                string md5str = JMP.TOOL.MD5.md5strGet(md5sing, true);
                Palist.Add("sign", md5str);//签名
                //参数
                string data = "";
                string url = "";//请求地址
                if (!string.IsNullOrEmpty(SeIn.RequestUrl))
                {
                    data = JMP.TOOL.UrlStr.GetStrNV(Palist) + "&url=" + ConfigurationManager.AppSettings["HyPOSTUrl"].ToString() + "&jmtype=HY";
                    url = SeIn.RequestUrl.Contains("http") || SeIn.RequestUrl.Contains("https") ? SeIn.RequestUrl : "http://" + SeIn.RequestUrl;//请求地址
                    url = url + "/H5/Jump" + "?" + data;
                }
                else
                {
                    data = JMP.TOOL.UrlStr.GetStrNV(Palist);
                    url = ConfigurationManager.AppSettings["HyPOSTUrl"].ToString() + "?" + data;//请求地址
                }
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);    //创建一个请求示例
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();  //获取响应，即发送请求
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("GB2312"));
                string srcString = streamReader.ReadToEnd();

                if (!string.IsNullOrEmpty(srcString))
                {
                    //判断是否有返回支付连接
                    string QQPay = JMP.TOOL.Regular.IshiQQPay(srcString);
                    if (!string.IsNullOrEmpty(QQPay))
                    {
                        string QQPayUrl = JMP.TOOL.Regular.IshiQQPayUrl(QQPay);
                        if (!string.IsNullOrEmpty(QQPayUrl))
                        {
                            string QQurl = QQPayUrl.Replace("value=", "").Replace("\"", "").Replace("amp;", "").Trim();
                            string QQpay = "{\"data\":\"" + QQurl + "\",\"PaymentType\":\"8\",\"SubType\":\"1\",\"IsH5\":\"1\"}";
                            inn = inn.ToResponse(ErrorCode.Code100);
                            inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(QQpay, ConfigurationManager.AppSettings["encryption"].ToString());

                        }
                        else
                        {
                            string error = "汇元QQWap支付正则表达式匹配URL错误代码：" + srcString + ",商户号：" + SeIn.UserId;
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + error, summary: "汇元QQWap支付接口错误信息", channelId: SeIn.PayId);
                            inn = inn.ToResponse(ErrorCode.Code104);
                        }
                    }
                    else
                    {
                        string error = "汇元QQWap支付正则表达式匹配支付地址是否存在错误代码：" + srcString + ",商户号：" + SeIn.UserId;
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + error, summary: "汇元QQWap支付接口错误信息", channelId: SeIn.PayId);
                        inn = inn.ToResponse(ErrorCode.Code104);

                    }

                }
                else
                {
                    string error = "汇元QQWap支付错误代码：" + srcString + ",商户号：" + SeIn.UserId;
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + error, summary: "汇元QQWap支付接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }

            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "汇元QQwap接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }

        /// <summary>
        /// 获取汇元网账号信息
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
                        SeIn.RequestUrl = paypz.Length > 2 ? paypz[2].Replace("\r", "").Replace("\n", "").Trim() : null;
                        SeIn.GotoURL = paypz.Length > 3 ? paypz[3].Replace("\r", "").Replace("\n", "").Trim() : null;
                        SeIn.ReturnUrl = paypz.Length > 4 ? paypz[4].Replace("\r", "").Replace("\n", "").Trim() : null;
                    }
                    else
                    {
                        dt = bll.SelectPay("HYQQWAP", apptype, appid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                            SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取汇元QQwap支付账号
                            SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取汇元QQwap支付key
                            SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                            SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                            SeIn.RequestUrl = paypz.Length > 2 ? paypz[2].Replace("\r", "").Replace("\n", "").Trim() : null;
                            SeIn.GotoURL = paypz.Length > 3 ? paypz[3].Replace("\r", "").Replace("\n", "").Trim() : null;
                            SeIn.ReturnUrl = paypz.Length > 4 ? paypz[4].Replace("\r", "").Replace("\n", "").Trim() : null;
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                        }
                        else
                        {
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",获取缓存失败后，从数据库未查询到相关信息！", summary: "汇元QQwap支付支付接口错误", channelId: SeIn.PayId);
                        }
                    }
                }
                else
                {
                    dt = bll.SelectPay("HYQQWAP", apptype, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取汇元QQwap支付账号
                        SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取汇元QQwap支付key
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        SeIn.RequestUrl = paypz.Length > 2 ? paypz[2].Replace("\r", "").Replace("\n", "").Trim() : null;
                        SeIn.GotoURL = paypz.Length > 3 ? paypz[3].Replace("\r", "").Replace("\n", "").Trim() : null;
                        SeIn.ReturnUrl = paypz.Length > 4 ? paypz[4].Replace("\r", "").Replace("\n", "").Trim() : null;
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                    }
                    else
                    {
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",从数据库未查询到相关信息！", summary: "汇元QQwap支付支付接口错误", channelId: SeIn.PayId);
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog(bcxx, summary: "汇元QQwap支付支付接口错误应用类型ID：" + apptype, channelId: SeIn.PayId);
            }
            return SeIn;
        }


        #endregion
    }
}
