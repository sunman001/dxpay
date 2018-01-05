using DxPay.LogManager.LogFactory.ApiLog;
using JmPayParameter.Models;
using JmPayParameter.PayChannel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JmPayParameter.PlaceOrder.AlpayType
{

    public class XdAliPay
    {
        /// <summary>
        /// 现在支付宝wap支付通道主入口
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
        public InnerResponse XdZfbPayInfo(int paymode, int apptype, string code, string goodsname, decimal price, int orderid, string ip, int infoTimes, int appid)
        {
            InnerResponse inn = new InnerResponse();
            switch (paymode)
            {
                case 1://安卓方式
                    inn = XdZfbWaPAz(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                case 2://ios方式
                    inn = XdZfbWaPIOS(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                case 3://H5支付方式
                    inn = XdZfbWaPH5(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                default:
                    inn = inn.ToResponse(ErrorCode.Code9987);
                    break;
            }
            return inn;
        }
        #region 现在支付宝wap支付
        /// <summary>
        /// 现在支付宝wap支付h5调用模式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse XdZfbWaPH5(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {
                string XdZfbWaPAz = "XdZfbWaPAz" + appid;//组装缓存key值

                SeIn = SelectUserInfo(XdZfbWaPAz, apptype, appid, infoTimes);
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
                Palist.Add("funcode", "WP001");//功能码
                Palist.Add("version", "1.0.0");//接口版本号
                Palist.Add("appId", SeIn.UserId);//商户应用唯一标识
                Palist.Add("mhtOrderNo", code);//商户订单号
                Palist.Add("mhtOrderName", goodsname); //商户商品名称
                Palist.Add("mhtOrderType", "01");//商户交易类型
                Palist.Add("mhtCurrencyType", "156");//商户订单币种类型
                Palist.Add("mhtOrderAmt", (Convert.ToInt32(price * 100)).ToString());//商户订单交易金额(单位：分)
                Palist.Add("mhtOrderDetail", goodsname);//商户订单详情
                Palist.Add("mhtOrderStartTime", DateTime.Now.ToString("yyyyMMddHHmmss"));//下单时间
                Palist.Add("notifyUrl", ConfigurationManager.AppSettings["XdNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//异步通知
                Palist.Add("frontNotifyUrl", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", orderid.ToString()));//同步通知
                Palist.Add("mhtCharset", "UTF-8");//编码格式
                Palist.Add("deviceType", "0601");//设备类型
                Palist.Add("payChannelType", "12");//支付类型 银联：11 支付宝：12 微信：13
                Palist.Add("outputType", "1");//输出格式
                Palist.Add("mhtSignType", "MD5");//签名方式
                Palist.Add("mhtOrderTimeOut", ConfigurationManager.AppSettings["overtime"].ToString());//商户订单超时时间
                string signstr = JMP.TOOL.UrlStr.GetStrAzNv(Palist) + "&" + JMP.TOOL.MD5.md5strGet(SeIn.UserKey, true).ToLower();
                string sign = JMP.TOOL.MD5.md5strGet(signstr, true).ToLower();
                Palist.Add("mhtSignature", sign);//签名
                string url = ConfigurationManager.AppSettings["XdPostUrl"].ToString();//请求地址
                WebClient webClient = new WebClient();
                byte[] responseData = webClient.UploadValues(url, "POST", Palist);//得到返回字符流  
                string srcString = Encoding.UTF8.GetString(responseData);//解码 
                string json = srcString.Replace("=", "\":\"").Replace("&", "\",\"");
                json = "{\"" + json + "\"}";
                Dictionary<string, object> dic = JMP.TOOL.JsonHelper.DataRowFromJSON(json);
                if (dic["responseCode"].ToString() == "A001" && json.Contains("tn"))
                {
                    string strurl = HttpUtility.UrlDecode(dic["tn"].ToString());
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = strurl;//http提交方式;
                    inn.IsJump = true;
                }
                else
                {
                    string responseMsg = HttpUtility.UrlDecode(dic["responseMsg"].ToString());
                    string mesage = "现在支付请求失败，错误代码：" + srcString + ",错误信息：" + json + "转换后的错误提示码:" + responseMsg + ",商户号：" + SeIn.UserId;
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + mesage, summary: "现在支付宝wap接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "现在支付宝wap接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }
        /// <summary>
        /// 现在支付宝wap支付安卓调用模式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse XdZfbWaPAz(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {
                string XdZfbWaPAz = "XdZfbWaPAz" + appid;//组装缓存key值

                SeIn = SelectUserInfo(XdZfbWaPAz, apptype, appid, infoTimes);
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
                Palist.Add("funcode", "WP001");//功能码
                Palist.Add("version", "1.0.0");//接口版本号
                Palist.Add("appId", SeIn.UserId);//商户应用唯一标识
                Palist.Add("mhtOrderNo", code);//商户订单号
                Palist.Add("mhtOrderName", goodsname); //商户商品名称
                Palist.Add("mhtOrderType", "01");//商户交易类型
                Palist.Add("mhtCurrencyType", "156");//商户订单币种类型
                Palist.Add("mhtOrderAmt", (Convert.ToInt32(price * 100)).ToString());//商户订单交易金额(单位：分)
                Palist.Add("mhtOrderDetail", goodsname);//商户订单详情
                Palist.Add("mhtOrderStartTime", DateTime.Now.ToString("yyyyMMddHHmmss"));//下单时间
                Palist.Add("notifyUrl", ConfigurationManager.AppSettings["XdNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//异步通知
                Palist.Add("frontNotifyUrl", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", orderid.ToString()));//同步通知
                Palist.Add("mhtCharset", "UTF-8");//编码格式
                Palist.Add("deviceType", "0601");//设备类型
                Palist.Add("payChannelType", "12");//支付类型 银联：11 支付宝：12 微信：13
                Palist.Add("outputType", "1");//输出格式
                Palist.Add("mhtSignType", "MD5");//签名方式
                Palist.Add("mhtOrderTimeOut", ConfigurationManager.AppSettings["overtime"].ToString());//商户订单超时时间
                string signstr = JMP.TOOL.UrlStr.GetStrAzNv(Palist) + "&" + JMP.TOOL.MD5.md5strGet(SeIn.UserKey, true).ToLower();
                string sign = JMP.TOOL.MD5.md5strGet(signstr, true).ToLower();
                Palist.Add("mhtSignature", sign);//签名
                string url = ConfigurationManager.AppSettings["XdPostUrl"].ToString();//请求地址
                WebClient webClient = new WebClient();
                byte[] responseData = webClient.UploadValues(url, "POST", Palist);//得到返回字符流  
                string srcString = Encoding.UTF8.GetString(responseData);//解码 
                string json = srcString.Replace("=", "\":\"").Replace("&", "\",\"");
                json = "{\"" + json + "\"}";
                Dictionary<string, object> dic = JMP.TOOL.JsonHelper.DataRowFromJSON(json);
                if (dic["responseCode"].ToString() == "A001" && json.Contains("tn"))
                {
                    string strurl = HttpUtility.UrlDecode(dic["tn"].ToString());
                    string wxpay = "{\"data\":\"" + strurl + "\",\"PaymentType\":\"1\",\"SubType\":\"6\",\"IsH5\":\"1\"}";
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(wxpay, ConfigurationManager.AppSettings["encryption"].ToString());
                }
                else
                {
                    string responseMsg = HttpUtility.UrlDecode(dic["responseMsg"].ToString());
                    string mesage = "现在支付请求失败，错误代码：" + srcString + ",错误信息：" + json + "转换后的错误提示码:" + responseMsg + ",商户号：" + SeIn.UserId;
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + mesage, summary: "现在支付宝wap接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "现在支付宝wap接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }

        /// <summary>
        /// 现在支付宝wap支付苹果调用模式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse XdZfbWaPIOS(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {
                string XdZfbWaPIOS = "XdZfbWaPIOS" + appid;//组装缓存key值

                SeIn = SelectUserInfo(XdZfbWaPIOS, apptype, appid, infoTimes);
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
                Palist.Add("funcode", "WP001");//功能码
                Palist.Add("version", "1.0.0");//接口版本号
                Palist.Add("appId", SeIn.UserId);//商户应用唯一标识
                Palist.Add("mhtOrderNo", code);//商户订单号
                Palist.Add("mhtOrderName", goodsname); //商户商品名称
                Palist.Add("mhtOrderType", "01");//商户交易类型
                Palist.Add("mhtCurrencyType", "156");//商户订单币种类型
                Palist.Add("mhtOrderAmt", (Convert.ToInt32(price * 100)).ToString());//商户订单交易金额(单位：分)
                Palist.Add("mhtOrderDetail", goodsname);//商户订单详情
                Palist.Add("mhtOrderStartTime", DateTime.Now.ToString("yyyyMMddHHmmss"));//下单时间
                Palist.Add("notifyUrl", ConfigurationManager.AppSettings["XdNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//异步通知
                Palist.Add("frontNotifyUrl", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", orderid.ToString()));//同步通知
                Palist.Add("mhtCharset", "UTF-8");//编码格式
                Palist.Add("deviceType", "0601");//设备类型
                Palist.Add("payChannelType", "12");//支付类型 银联：11 支付宝：12 微信：13
                Palist.Add("outputType", "1");//输出格式
                Palist.Add("mhtSignType", "MD5");//签名方式
                Palist.Add("mhtOrderTimeOut", ConfigurationManager.AppSettings["overtime"].ToString());//商户订单超时时间
                string signstr = JMP.TOOL.UrlStr.GetStrAzNv(Palist) + "&" + JMP.TOOL.MD5.md5strGet(SeIn.UserKey, true).ToLower();
                string sign = JMP.TOOL.MD5.md5strGet(signstr, true).ToLower();
                Palist.Add("mhtSignature", sign);//签名
                string url = ConfigurationManager.AppSettings["XdPostUrl"].ToString();//请求地址
                WebClient webClient = new WebClient();
                byte[] responseData = webClient.UploadValues(url, "POST", Palist);//得到返回字符流  
                string srcString = Encoding.UTF8.GetString(responseData);//解码 
                string json = srcString.Replace("=", "\":\"").Replace("&", "\",\"");
                json = "{\"" + json + "\"}";
                Dictionary<string, object> dic = JMP.TOOL.JsonHelper.DataRowFromJSON(json);
                if (dic["responseCode"].ToString() == "A001" && json.Contains("tn"))
                {
                    string strurl = HttpUtility.UrlDecode(dic["tn"].ToString());
                    string wxpay = "{\"data\":\"" + strurl + "\",\"PaymentType\":\"1\",\"SubType\":\"6\",\"IsH5\":\"1\"}";
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(wxpay, ConfigurationManager.AppSettings["encryption"].ToString());
                }
                else
                {
                    string responseMsg = HttpUtility.UrlDecode(dic["responseMsg"].ToString());
                    string mesage = "现在支付请求失败，错误代码：" + srcString + ",错误信息：" + json + "转换后的错误提示码:" + responseMsg + ",商户号：" + SeIn.UserId;
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + mesage, summary: "现在支付宝wap接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "现在支付宝wap接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }
        /// <summary>
        /// 获取现在网账号信息
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
                        SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的现在支付宝wap支付账号
                        SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的现在支付宝wap支付key
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                    }
                    else
                    {
                        dt = bll.SelectPay("XDZFB", apptype, appid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                            SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取现在支付宝wap支付账号
                            SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取现在支付宝wap支付key
                            SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                            SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                        }
                        else
                        {
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",获取缓存未成功后在次查询数据！", summary: "现在支付宝wap支付支付接口错误", channelId: SeIn.PayId);
                        }
                    }
                }
                else
                {
                    dt = bll.SelectPay("XDZFB", apptype, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取现在支付宝wap支付账号
                        SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取现在支付宝wap支付key
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                    }
                    else
                    {
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",在数据库为查询到数据", summary: "现在支付宝wap支付支付接口错误", channelId: SeIn.PayId);
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog(bcxx, summary: "现在支付宝wap支付支付接口错误应用类型ID：" + apptype, channelId: SeIn.PayId);
            }
            return SeIn;
        }


        #endregion
    }
}
