using JMP.TOOL;
using JmPayParameter.PayChannel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DxPay.LogManager.LogFactory.ApiLog;
using JmPayParameter.Models;

namespace JmPayParameter.PlaceOrder.WxAppType
{
    /// <summary>
    /// 南粤微信appid支付接口通道
    /// </summary>
    public class NyPayApp
    {


        /// <summary>
        /// 南粤微信appid支付接口支付通道主入口
        /// </summary>
        /// <param name="paymode">平台类型（1：安卓，2：ios，3：H5）</param>
        /// <param name="appid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格（单位元）</param>
        /// <param name="orderid">订单id</param>
        /// <param name="ip">ip地址</param>
        /// <param name="apptype">风控配置表id</param>
        /// <returns></returns>
        public InnerResponse NyWxAppPayInfo(int paymode, int appid, string code, string goodsname, decimal price, int orderid, string ip, int infoTimes, int apptype)
        {
            InnerResponse inn = new InnerResponse();
            switch (paymode)
            {
                case 1://安卓方式
                    inn = NyAppidAz(appid, code, price, orderid, goodsname, apptype, infoTimes);
                    break;
                case 2://ios方式
                    inn = NyAppidIos(appid, code, price, orderid, goodsname, apptype, infoTimes);
                    break;
                default:
                    inn = inn.ToResponse(ErrorCode.Code9987);
                    break;
            }
            return inn;
        }

        #region 南粤app支付
        /// <summary>
        /// 南粤app支付安卓调用方式
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="price">商品价格</param>
        /// <param name="orderid">订单id</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="apptype">风控配置表id</param>
        /// <returns></returns>
        private InnerResponse NyAppidAz(int appid, string code, decimal price, int orderid, string goodsname, int apptype, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {
                string Nyappid = "Nyappid" + appid;//组装缓存key值

                SeIn = SelectInfo(Nyappid, apptype, appid, infoTimes);
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
                Dictionary<string, string> strlist = new Dictionary<string, string>();
                strlist.Add("tradeType", "cs.pay.submit");//交易类型
                strlist.Add("version", "1.3");//版本号
                strlist.Add("mchId", SeIn.UserId);//代理商号
                strlist.Add("channel", "wxApp");//支付渠道wxApp wxPub
                strlist.Add("body", goodsname);//商品描述
                strlist.Add("outTradeNo", code);//商户订单号
                strlist.Add("amount", price.ToString());//交易金额
                                                        //strlist.Add("description", JMP.TOOL.Encrypt.IndexEncrypt(code));//自定义信息
                strlist.Add("mobileAppId", SeIn.wxappid);//appid时需要传入
                strlist.Add("notifyUrl", ConfigurationManager.AppSettings["NyNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//异步通知
                string md5str = JMP.TOOL.UrlStr.AzGetStr(strlist) + "&key=" + SeIn.UserKey;
                string md5 = JMP.TOOL.MD5.md5strGet(md5str, true);
                strlist.Add("sign", md5);//签名
                string extra = "{\"mobileAppId\":\"" + SeIn.wxappid + "\",\"notifyUrl\":\"" + ConfigurationManager.AppSettings["NyNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()) + "\"}";
                strlist.Add("extra", extra);//扩展字段
                string postString = JMP.TOOL.JsonHelper.DictJsonstr(strlist, "extra");//这里即为传递的参数，可以用工具抓包分析，也可以自己分析，主要是form里面每一个name都要加进来  
                byte[] postData = Encoding.UTF8.GetBytes(postString);//编码，尤其是汉字，事先要看下抓取网页的编码方式  
                string url = ConfigurationManager.AppSettings["NYPOSTUrl"].ToString();//地址  
                WebClient webClient = new WebClient();
                byte[] responseData = webClient.UploadData(url, "POST", postData);//得到返回字符流  
                string srcString = Encoding.UTF8.GetString(responseData);//解码  
                Dictionary<string, object> jsonstr = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
                if (jsonstr.ContainsKey("returnCode") && jsonstr["resultCode"].ToString() == "0")
                {
                    string str = "{\"PaymentType\":\"5\",\"SubType\":\"2\"," + jsonstr["payCode"].ToString().Replace("{", "").Replace("}", "").Replace("package", "pkg") + ",\"IsH5\":\"0\"}";
                    //str = "{\"message\":\"成功\",\"result\":100,\"data\":" + str + "}";
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(str, ConfigurationManager.AppSettings["encryption"].ToString());
                }
                else
                {
                    string error = "南粤微信app接口错误信息：" + srcString + ",商户号：" + SeIn.UserId;
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + error, summary: "南粤微信app接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "南粤微信app接口错误信息", channelId: SeIn.PayId);

                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }
        /// <summary>
        /// 南粤app支付苹果调用方式
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="price">商品价格</param>
        /// <param name="orderid">订单id</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="apptype">风控配置表id</param>
        /// <returns></returns>
        private InnerResponse NyAppidIos(int appid, string code, decimal price, int orderid, string goodsname, int apptype, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {
                string Nyappid = "Nyappid" + appid;//组装缓存key值

                SeIn = SelectInfo(Nyappid, apptype, appid, infoTimes);
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
                Dictionary<string, string> strlist = new Dictionary<string, string>();
                strlist.Add("tradeType", "cs.pay.submit");//交易类型
                strlist.Add("version", "1.3");//版本号
                strlist.Add("mchId", SeIn.UserId);//代理商号
                strlist.Add("channel", "wxApp");//支付渠道wxApp wxPub
                strlist.Add("body", goodsname);//商品描述
                strlist.Add("outTradeNo", code);//商户订单号
                strlist.Add("amount", price.ToString());//交易金额
                                                        //strlist.Add("description", JMP.TOOL.DESEncrypt.Encrypt(code));//自定义信息
                strlist.Add("mobileAppId", SeIn.wxappid);//appid时需要传入
                strlist.Add("notifyUrl", ConfigurationManager.AppSettings["NyNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//异步通知
                string md5str = JMP.TOOL.UrlStr.AzGetStr(strlist) + "&key=" + SeIn.UserKey;
                string md5 = JMP.TOOL.MD5.md5strGet(md5str, true);
                strlist.Add("sign", md5);//签名
                string extra = "{\"mobileAppId\":\"" + SeIn.wxappid + "\",\"notifyUrl\":\"" + ConfigurationManager.AppSettings["NyNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()) + "\"}";
                strlist.Add("extra", extra);//扩展字段
                string postString = JMP.TOOL.JsonHelper.DictJsonstr(strlist, "extra");//这里即为传递的参数，可以用工具抓包分析，也可以自己分析，主要是form里面每一个name都要加进来  
                byte[] postData = Encoding.UTF8.GetBytes(postString);//编码，尤其是汉字，事先要看下抓取网页的编码方式  
                string url = ConfigurationManager.AppSettings["NYPOSTUrl"].ToString();//地址  
                WebClient webClient = new WebClient();
                byte[] responseData = webClient.UploadData(url, "POST", postData);//得到返回字符流  
                string srcString = Encoding.UTF8.GetString(responseData);//解码  
                Dictionary<string, object> jsonstr = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
                if (jsonstr.ContainsKey("returnCode") && jsonstr["resultCode"].ToString() == "0")
                {
                    string codes = JMP.TOOL.Encrypt.IndexEncrypt(code);
                    string str = "{\"code\":\"" + codes + "\",\"PaymentType\":\"5\",\"SubType\":\"2\"," + jsonstr["payCode"].ToString().Replace("{", "").Replace("}", "").Replace("package", "pkg") + ",\"IsH5\":\"0\"}";
                    //str = "{\"message\":\"成功\",\"result\":100,\"data\":" + str + "}";
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(str, ConfigurationManager.AppSettings["encryption"].ToString());
                }
                else
                {
                    string error = "南粤微信app接口错误信息：" + srcString + ",商户号：" + SeIn.UserId;
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + error, summary: "南粤微信app接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "南粤微信app接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }
        /// <summary>
        /// 查询南粤appid支付账户信息
        /// </summary>
        /// <param name="cache">缓存值</param>
        /// <param name="appid">应用id</param>
        /// <param name="apptype">风控配置表id</param>
        /// <returns></returns>
        private SelectInterface SelectInfo(string cache, int appid, int apptype, int infoTimes)
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
                        SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的南粤app账号id
                        SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的南粤appkey
                        SeIn.wxappid = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//获取缓存中的微信appid
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                    }
                    else
                    {
                        dt = bll.SelectPay("NYAPP", apptype, appid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                            SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取南粤app
                            SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取南粤appkey
                            SeIn.wxappid = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//获取缓存中的微信appid
                            SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                            SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                        }
                        else
                        {
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",获取缓存失败后，从数据库未查询到相关信息！", summary: "南粤app支付接口错误", channelId: SeIn.PayId);
                        }
                    }
                }
                else
                {
                    dt = bll.SelectPay("NYAPP", apptype, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取南粤app
                        SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取南粤appkey
                        SeIn.wxappid = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//获取缓存中的微信appid
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                    }
                    else
                    {
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",直接从数据库未查询到相关信息！", summary: "南粤app支付接口错误", channelId: SeIn.PayId);
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog(bcxx, summary: "南粤app支付接口错误应用ID：" + appid, channelId: SeIn.PayId);//写入报错日志
            }
            return SeIn;
        }
        #endregion
    }
}
