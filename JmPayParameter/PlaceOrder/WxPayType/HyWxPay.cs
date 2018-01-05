using JMP.TOOL;
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

namespace JmPayParameter.PlaceOrder.WxPayType
{
    /// <summary>
    /// 汇元微信wap支付
    /// </summary>
    public class HyWxPay
    {

        /// <summary>
        /// 汇元微信wap支付通道主入口
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
        public InnerResponse HyWxPayInfo(int paymode, int apptype, string code, string goodsname, decimal price, int orderid, string ip, int infoTimes, int appid)
        {
            InnerResponse inn = new InnerResponse();
            switch (paymode)
            {
                case 1://安卓方式
                    inn = HyWxWaPAz(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                case 2://ios方式
                    inn = HyWxWaPIOS(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                case 3://H5支付方式
                    inn = HyWxWaPH5(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                default:
                    //throw new Exc { Response = new InnerResponse { ErrorCode = ErrorCode.Code9987.GetValue() } };
                    inn = inn.ToResponse(ErrorCode.Code9987);
                    break;
            }
            return inn;
        }
        #region 汇元微信wap支付
        /// <summary>
        /// 汇元微信wap支付h5调用模式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse HyWxWaPH5(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {
                string HyWxWaPH5jkhc = "HyWxWaPH5jkhc" + appid;//组装缓存key值

                SeIn = SelectUserInfo(HyWxWaPH5jkhc, apptype, appid, infoTimes);
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
                System.Collections.Specialized.NameValueCollection Palist = new System.Collections.Specialized.NameValueCollection();
                Palist.Add("version", "1");//版本号
                Palist.Add("agent_id", SeIn.UserId);//商户编号
                Palist.Add("agent_bill_id", code);//订单号
                Palist.Add("agent_bill_time", DateTime.Now.ToString("yyyyMMddHHmmss"));//提交订单时间
                Palist.Add("pay_amt", price.ToString());//支付金额（单位：元）

                Palist.Add("user_ip", HttpContext.Current.Request.UserHostAddress.Replace('.', '_'));//ip地址
                Palist.Add("pay_type", "30");//支付类型
                Palist.Add("is_phone", "1");//微信wap支付传入1
                Palist.Add("is_frame", "0");//微信支付类型（0：wap，1：公众号）默认为1
                string meta_option = "{\"s\":\"WAP\",\"n\":\"测试\",\"id\":\"http://www.baidu.com\"}";
                string GotoUrlName = ConfigurationManager.AppSettings["GotoUrlName"];//同步跳转域名
                string RetunUrl = ConfigurationManager.AppSettings["RetunUrl"];//异步跳转域名
                if (!string.IsNullOrEmpty(SeIn.RequestUrl))
                {
                    Palist.Add("goods_name", goodsname);//商品名称
                    Palist.Add("meta_option", meta_option.Trim());
                }
                else
                {
                    Palist.Add("goods_name", HttpUtility.UrlEncode(goodsname, Encoding.GetEncoding("gb2312")));//商品名称
                    Palist.Add("meta_option", Convert.ToBase64String(System.Text.Encoding.GetEncoding("GBK").GetBytes(meta_option.Trim())));
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

                string md5sing = "version=" + Palist["version"] + "&agent_id=" + Palist["agent_id"] + "&agent_bill_id=" + Palist["agent_bill_id"] + "&agent_bill_time=" + Palist["agent_bill_time"] + "&pay_type=30&pay_amt=" + Palist["pay_amt"] + "&notify_url=" + Palist["notify_url"] + "&return_url=" + Palist["return_url"] + "&user_ip=" + Palist["user_ip"] + "&key=" + SeIn.UserKey;
                string md5str = JMP.TOOL.MD5.md5strGet(md5sing, true);
                Palist.Add("sign", md5str);//签名
                string url = "";//请求地址
                string data = "";
                // string strurl = url + "?" + JMP.TOOL.UrlStr.GetStrNV(Palist);
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
                inn = inn.ToResponse(ErrorCode.Code100);
                inn.ExtraData = url;//http提交方式;
                inn.IsJump = true;
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "汇元微信wap接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }
        /// <summary>
        /// 汇元微信wap支付安卓调用模式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse HyWxWaPAz(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {

                string HyWxWaPH5Azjkhc = "HyWxWaPH5Azjkhc" + appid;//组装缓存key值

                SeIn = SelectUserInfo(HyWxWaPH5Azjkhc, apptype, appid, infoTimes);
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
                System.Collections.Specialized.NameValueCollection Palist = new System.Collections.Specialized.NameValueCollection();
                Palist.Add("version", "1");//版本号
                Palist.Add("agent_id", SeIn.UserId);//商户编号
                Palist.Add("agent_bill_id", code);//订单号
                Palist.Add("agent_bill_time", DateTime.Now.ToString("yyyyMMddHHmmss"));//提交订单时间
                Palist.Add("pay_amt", price.ToString());//支付金额（单位：元）

                Palist.Add("return_url", "http://www.baidu.com");//同步通知地址
                Palist.Add("user_ip", HttpContext.Current.Request.UserHostAddress.Replace('.', '_'));//ip地址
                Palist.Add("pay_type", "30");//支付类型
                string meta_option = "[{\"s\":\"Android\",\"n\":\"测试\",\"id\":\"测试\"},{\"s\":\"IOS\",\"n\":\"\",\"id\":\"\"}]";
                string GotoUrlName = ConfigurationManager.AppSettings["GotoUrlName"];//同步跳转域名
                string RetunUrl = ConfigurationManager.AppSettings["RetunUrl"];//异步跳转域名
                if (!string.IsNullOrEmpty(SeIn.RequestUrl))
                {
                    Palist.Add("goods_note", goodsname);//支付说明 
                    Palist.Add("goods_name", goodsname);//商品名称
                    Palist.Add("meta_option", meta_option.Trim());
                }
                else
                {
                    Palist.Add("goods_note", HttpUtility.UrlEncode(goodsname, Encoding.GetEncoding("gb2312")));//支付说明 
                    Palist.Add("goods_name", HttpUtility.UrlEncode(goodsname, Encoding.GetEncoding("gb2312")));//商品名称
                    Palist.Add("meta_option", Convert.ToBase64String(System.Text.Encoding.GetEncoding("GBK").GetBytes(meta_option.Trim())));
                }

                if (!string.IsNullOrEmpty(SeIn.ReturnUrl))
                {
                    Palist.Add("notify_url", ConfigurationManager.AppSettings["HyNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()).Replace(RetunUrl, SeIn.ReturnUrl));//异步通知地址
                }
                else
                {
                    Palist.Add("notify_url", ConfigurationManager.AppSettings["HyNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//异步通知地址
                }

                Palist.Add("goods_num", "1");//产品数量
                                             //Palist.Add("remark", HttpUtility.UrlEncode(goodsname, Encoding.GetEncoding("gb2312")));//自定义参数
                string md5sing = "version=" + Palist["version"] + "&agent_id=" + Palist["agent_id"] + "&agent_bill_id=" + Palist["agent_bill_id"] + "&agent_bill_time=" + Palist["agent_bill_time"] + "&pay_type=30&pay_amt=" + Palist["pay_amt"] + "&notify_url=" + Palist["notify_url"] + "&user_ip=" + Palist["user_ip"] + "&key=" + SeIn.UserKey;
                string md5str = JMP.TOOL.MD5.md5strGet(md5sing, true);
                Palist.Add("sign", md5str);//签名
                //参数
                string data = "";
                string url = "";
                if (!string.IsNullOrEmpty(SeIn.RequestUrl))
                {
                    data = JMP.TOOL.UrlStr.GetStrNV(Palist) + "&url=" + ConfigurationManager.AppSettings["HywxsdkPOSTUrl"].ToString() + "&jmtype=HY";
                    url = SeIn.RequestUrl.Contains("http") || SeIn.RequestUrl.Contains("https") ? SeIn.RequestUrl : "http://" + SeIn.RequestUrl;//请求地址
                    url = url + "/H5/Jump" + "?" + data;
                }
                else
                {
                    data = JMP.TOOL.UrlStr.GetStrNV(Palist);
                    url = ConfigurationManager.AppSettings["HywxsdkPOSTUrl"].ToString() + "?" + data;//请求地址  
                }
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);    //创建一个请求示例
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();  //获取响应，即发送请求
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("GB2312"));
                string srcString = streamReader.ReadToEnd();


                //Dictionary<string, string> list = Palist.Cast<string>().ToDictionary(x => x, x => Palist[x]);
                //string aaa = JMP.TOOL.UrlStr.AzGetStr(list);
                //string url = ConfigurationManager.AppSettings["HywxsdkPOSTUrl"].ToString();//请求地址
                //WebClient webClient = new WebClient();
                //byte[] responseData = webClient.UploadValues(url, "POST", Palist);//得到返回字符流  
                //string srcString = Encoding.UTF8.GetString(responseData);//解码 

                if (srcString.Contains("token_id"))
                {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.LoadXml(srcString);
                    string token_id = xmldoc["token_id"].InnerText + "," + SeIn.UserId + "," + code + ",30";
                    string wxpay = "{\"token_id\":\"" + token_id + "\",\"PaymentType\":\"2\",\"SubType\":\"2\",\"IsH5\":\"0\"}";
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(wxpay, ConfigurationManager.AppSettings["encryption"].ToString());
                }
                else
                {
                    string error = "汇元微信wap接口错误代码：" + srcString + ",商户号：" + SeIn.UserId;
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + error, summary: "汇元微信wap接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "汇元微信wap接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }

        /// <summary>
        /// 汇元微信wap支付苹果调用模式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse HyWxWaPIOS(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {
                string HyWxWaPIOSjkhc = "HyWxWaPIOSjkhc" + appid;//组装缓存key值

                SeIn = SelectUserInfo(HyWxWaPIOSjkhc, apptype, appid, infoTimes);
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
                System.Collections.Specialized.NameValueCollection Palist = new System.Collections.Specialized.NameValueCollection();
                Palist.Add("version", "1");//版本号
                Palist.Add("agent_id", SeIn.UserId);//商户编号
                Palist.Add("agent_bill_id", code);//订单号
                Palist.Add("agent_bill_time", DateTime.Now.ToString("yyyyMMddHHmmss"));//提交订单时间
                Palist.Add("pay_amt", price.ToString());//支付金额（单位：元）

                Palist.Add("return_url", "https://www.baidu.com");//同步通知地址
                Palist.Add("user_ip", ip.Replace('.', '_'));//ip地址
                Palist.Add("pay_type", "30");//支付类型
                Palist.Add("goods_num", "1");//产品数量
                                             //Palist.Add("goods_note", HttpUtility.UrlEncode(goodsname, Encoding.GetEncoding("gb2312")));//支付说明
                                             // Palist.Add("goods_name", HttpUtility.UrlEncode(goodsname, Encoding.GetEncoding("gb2312")));//商品名称
                                             // Palist.Add("remark", HttpUtility.UrlEncode(goodsname, Encoding.GetEncoding("gb2312")));//自定义参数
                string meta_option = "[{\"s\":\"Android\",\"n\":\"\",\"id\":\"\"},{\"s\":\"IOS\",\"n\":\"测试\",\"id\":\"com.jurtevfdb.rykueryeqrg\"}]";
                string GotoUrlName = ConfigurationManager.AppSettings["GotoUrlName"];//同步跳转域名
                string RetunUrl = ConfigurationManager.AppSettings["RetunUrl"];//异步跳转域名
                if (!string.IsNullOrEmpty(SeIn.RequestUrl))
                {
                    Palist.Add("goods_note", goodsname);//支付说明 
                    Palist.Add("goods_name", goodsname);//商品名称
                    Palist.Add("meta_option", meta_option.Trim());
                }
                else
                {
                    Palist.Add("goods_note", HttpUtility.UrlEncode(goodsname, Encoding.GetEncoding("gb2312")));//支付说明 
                    Palist.Add("goods_name", HttpUtility.UrlEncode(goodsname, Encoding.GetEncoding("gb2312")));//商品名称
                    Palist.Add("meta_option", Convert.ToBase64String(System.Text.Encoding.GetEncoding("GBK").GetBytes(meta_option.Trim())));
                }

                if (!string.IsNullOrEmpty(SeIn.ReturnUrl))
                {
                    Palist.Add("notify_url", ConfigurationManager.AppSettings["HyNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()).Replace(RetunUrl, SeIn.ReturnUrl));//异步通知地址
                }
                else
                {
                    Palist.Add("notify_url", ConfigurationManager.AppSettings["HyNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//异步通知地址
                }

                //Palist.Add("meta_option", Convert.ToBase64String(System.Text.Encoding.GetEncoding("GBK").GetBytes(meta_option.Trim())));
                string md5sing = "version=" + Palist["version"] + "&agent_id=" + Palist["agent_id"] + "&agent_bill_id=" + Palist["agent_bill_id"] + "&agent_bill_time=" + Palist["agent_bill_time"] + "&pay_type=30&pay_amt=" + Palist["pay_amt"] + "&notify_url=" + Palist["notify_url"] + "&user_ip=" + Palist["user_ip"] + "&key=" + SeIn.UserKey;
                string md5str = JMP.TOOL.MD5.md5strGet(md5sing, true);
                Palist.Add("sign", md5str);//签名

                //参数
                // string data = JMP.TOOL.UrlStr.GetStrNV(Palist);
                //string url = ConfigurationManager.AppSettings["HywxsdkPOSTUrl"].ToString() + "?" + data;//请求地址
                string data = "";
                string url = "";
                if (!string.IsNullOrEmpty(SeIn.RequestUrl))
                {
                    data = JMP.TOOL.UrlStr.GetStrNV(Palist) + "&url=" + ConfigurationManager.AppSettings["HywxsdkPOSTUrl"].ToString() + "&jmtype=HY";
                    url = SeIn.RequestUrl.Contains("http") || SeIn.RequestUrl.Contains("https") ? SeIn.RequestUrl : "http://" + SeIn.RequestUrl;//请求地址
                    url = url + "/H5/Jump" + "?" + data;
                }
                else
                {
                    data = JMP.TOOL.UrlStr.GetStrNV(Palist);
                    url = ConfigurationManager.AppSettings["HywxsdkPOSTUrl"].ToString() + "?" + data;//请求地址  
                }
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);    //创建一个请求示例
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();  //获取响应，即发送请求
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("GB2312"));
                string srcString = streamReader.ReadToEnd();

                //string url = ConfigurationManager.AppSettings["HywxsdkPOSTUrl"].ToString();//请求地址
                //WebClient webClient = new WebClient();
                //byte[] responseData = webClient.UploadValues(url, "POST", Palist);//得到返回字符流  
                //string srcString = Encoding.UTF8.GetString(responseData);//解码 

                if (srcString.Contains("token_id"))
                {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.LoadXml(srcString);
                    string token_id = xmldoc["token_id"].InnerText + "," + SeIn.UserId + "," + code + ",30";
                    string wxpay = "{\"token_id\":\"" + token_id + "\",\"PaymentType\":\"2\",\"SubType\":\"2\",\"IsH5\":\"0\"}";
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(wxpay, ConfigurationManager.AppSettings["encryption"].ToString());
                }
                else
                {
                    string error = "汇元微信wap接口错误代码：" + srcString + ",商户号：" + SeIn.UserId;
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + error, summary: "汇元微信wap接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "汇元微信wap接口错误信息", channelId: SeIn.PayId);
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
                        SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的汇元微信wap支付账号
                        SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的汇元微信wap支付key
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        SeIn.RequestUrl = paypz.Length > 2 ? paypz[2].Replace("\r", "").Replace("\n", "").Trim() : null;
                        SeIn.GotoURL = paypz.Length > 3 ? paypz[3].Replace("\r", "").Replace("\n", "").Trim() : null;
                        SeIn.ReturnUrl = paypz.Length > 4 ? paypz[4].Replace("\r", "").Replace("\n", "").Trim() : null;
                    }
                    else
                    {
                        dt = bll.SelectPay("HYWX", apptype, appid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                            SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取汇元微信wap支付账号
                            SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取汇元微信wap支付key
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
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",获取缓存失败后，从数据库未查询到相关信息！", summary: "汇元微信wap支付支付接口错误", channelId: SeIn.PayId);
                        }
                    }
                }
                else
                {
                    dt = bll.SelectPay("HYWX", apptype, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取汇元微信wap支付账号
                        SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取汇元微信wap支付key
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
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",从数据库未查询到相关信息！", summary: "汇元微信wap支付支付接口错误", channelId: SeIn.PayId);
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog(bcxx, summary: "汇元微信wap支付支付接口错误应用类型ID：" + apptype, channelId: SeIn.PayId);
            }
            return SeIn;
        }


        #endregion
    }
}
