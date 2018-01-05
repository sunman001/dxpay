using DxPay.LogManager.LogFactory.ApiLog;
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

namespace JmPayParameter.PlaceOrder.WxPayType
{
    /// <summary>
    /// 掌趣微信wap主通道
    /// </summary>
    public class ZqWxPay
    {
        /// <summary>
        /// 掌趣微信wap支付通道主入口
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
        public InnerResponse ZqWxPayInfo(int paymode, int apptype, string code, string goodsname, decimal price, int orderid, string ip, int infoTimes, int appid)
        {
            InnerResponse inn = new InnerResponse();
            switch (paymode)
            {
                case 1://安卓方式
                    inn = ZqWxWaPAz(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                case 2://ios方式
                    inn = ZqWxWaPIOS(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                case 3://H5支付方式
                    inn = ZqWxWaPH5(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                default:
                    inn = inn.ToResponse(ErrorCode.Code9987);
                    break;
            }
            return inn;
        }


        #region 掌趣微信wap支付

        /// <summary>
        /// 掌趣微信wap支付h5调用模式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse ZqWxWaPH5(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {
                string ZqWxWaPH5jkhc = "ZqWxWaPH5jkhc" + appid;//组装缓存key值

                SeIn = SelectUserInfo(ZqWxWaPH5jkhc, apptype, appid, infoTimes);
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

                Dictionary<string, string> Palist = new Dictionary<string, string>();

                Palist.Add("merchantNo", SeIn.UserId);//商户编号
                Palist.Add("merchantOrderno", code);//商户订单号
                Palist.Add("requestAmount", price.ToString());//订单金额(元)
                Palist.Add("noticeSysaddress", ConfigurationManager.AppSettings["ZqPayNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//异步通知地址
                Palist.Add("noticeWebaddress", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", orderid.ToString()));//页面通知地址
                Palist.Add("memberNo", code);//用户id
                Palist.Add("memberGoods", goodsname);//商品名称
                Palist.Add("payType", "WXWAP");//支付类型
                //组装签名字符串
                string sign = Palist["merchantNo"].ToString() + Palist["merchantOrderno"].ToString() + Palist["requestAmount"].ToString() + Palist["noticeSysaddress"].ToString() + Palist["noticeWebaddress"].ToString() + Palist["memberNo"].ToString() + Palist["memberGoods"].ToString() + Palist["payType"].ToString();
                //签名
                string signstr = JMP.TOOL.Digest.HmacSign(sign, SeIn.UserKey);

                Palist.Add("hmac", signstr);//签名

                string urlstr = JMP.TOOL.UrlStr.AzGetStr(Palist);
                string url = ConfigurationManager.AppSettings["ZqWxWapPayUrl"].ToString() + urlstr;//请求地址
                //发起请求
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string jmpay = "";
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8))
                {
                    jmpay = reader.ReadToEnd();
                }
                Dictionary<string, object> dict = JMP.TOOL.JsonHelper.DataRowFromJSON(jmpay);
                if (dict["code"].ToString() == "000")
                {
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = dict["payUrl"].ToString();//http提交方式;
                    inn.IsJump = true;
                }
                else
                {
                    string ErrorMessage = "掌趣微信wap支付错误代码：" + jmpay + ",商户号：" + SeIn.UserId;
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + ErrorMessage, summary: "掌趣微信wap支付接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }


            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "掌趣微信wap接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }

        /// <summary>
        /// 掌趣微信wap支付安卓调用模式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse ZqWxWaPAz(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {
                string WpWxWaPAzjkhc = "WpWxWaPAzjkhc" + appid;//组装缓存key值

                SeIn = SelectUserInfo(WpWxWaPAzjkhc, apptype, appid, infoTimes);
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

                Dictionary<string, string> Palist = new Dictionary<string, string>();

                Palist.Add("merchantNo", SeIn.UserId);//商户编号
                Palist.Add("merchantOrderno", code);//商户订单号
                Palist.Add("requestAmount", price.ToString());//订单金额(元)
                Palist.Add("noticeSysaddress", ConfigurationManager.AppSettings["ZqPayNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//异步通知地址
                Palist.Add("noticeWebaddress", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", orderid.ToString()));//页面通知地址
                Palist.Add("memberNo", code);//用户id
                Palist.Add("memberGoods", goodsname);//商品名称
                Palist.Add("payType", "WXWAP");//支付类型
                //组装签名字符串
                string sign = Palist["merchantNo"].ToString() + Palist["merchantOrderno"].ToString() + Palist["requestAmount"].ToString() + Palist["noticeSysaddress"].ToString() + Palist["noticeWebaddress"].ToString() + Palist["memberNo"].ToString() + Palist["memberGoods"].ToString() + Palist["payType"].ToString();
                //签名
                string signstr = JMP.TOOL.Digest.HmacSign(sign, SeIn.UserKey);

                Palist.Add("hmac", signstr);//签名

                string urlstr = JMP.TOOL.UrlStr.AzGetStr(Palist);
                string url = ConfigurationManager.AppSettings["ZqWxWapPayUrl"].ToString() + urlstr;//请求地址
                //发起请求
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string jmpay = "";
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8))
                {
                    jmpay = reader.ReadToEnd();
                }

                Dictionary<string, object> dict = JMP.TOOL.JsonHelper.DataRowFromJSON(jmpay);

                if (dict["code"].ToString() == "000")
                {
                    string wxpay = "{\"data\":\"" + dict["payUrl"].ToString() + "\",\"PaymentType\":\"2\",\"SubType\":\"8\",\"IsH5\":\"1\"}";
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(wxpay, ConfigurationManager.AppSettings["encryption"].ToString());
                }
                else
                {
                    string ErrorMessage = "掌趣微信wap支付错误代码：" + jmpay + ",商户号：" + SeIn.UserId;
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + ErrorMessage, summary: "掌趣微信wap支付接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "掌趣微信wap接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }

        /// <summary>
        /// 掌趣微信wap支付苹果调用模式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse ZqWxWaPIOS(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {
                string WpWxWaPIOSjkhc = "WpWxWaPIOSjkhc" + appid;//组装缓存key值

                SeIn = SelectUserInfo(WpWxWaPIOSjkhc, apptype, appid, infoTimes);
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

                Dictionary<string, string> Palist = new Dictionary<string, string>();

                Palist.Add("merchantNo", SeIn.UserId);//商户编号
                Palist.Add("merchantOrderno", code);//商户订单号
                Palist.Add("requestAmount", price.ToString());//订单金额(元)
                Palist.Add("noticeSysaddress", ConfigurationManager.AppSettings["ZqPayNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//异步通知地址
                Palist.Add("noticeWebaddress", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", orderid.ToString()));//页面通知地址
                Palist.Add("memberNo", code);//用户id
                Palist.Add("memberGoods", goodsname);//商品名称
                Palist.Add("payType", "WXWAP");//支付类型
                //组装签名字符串
                string sign = Palist["merchantNo"].ToString() + Palist["merchantOrderno"].ToString() + Palist["requestAmount"].ToString() + Palist["noticeSysaddress"].ToString() + Palist["noticeWebaddress"].ToString() + Palist["memberNo"].ToString() + Palist["memberGoods"].ToString() + Palist["payType"].ToString();
                //签名
                string signstr = JMP.TOOL.Digest.HmacSign(sign, SeIn.UserKey);

                Palist.Add("hmac", signstr);//签名

                string urlstr = JMP.TOOL.UrlStr.AzGetStr(Palist);
                string url = ConfigurationManager.AppSettings["ZqWxWapPayUrl"].ToString() + urlstr;//请求地址
                //发起请求
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string jmpay = "";
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8))
                {
                    jmpay = reader.ReadToEnd();
                }

                Dictionary<string, object> dict = JMP.TOOL.JsonHelper.DataRowFromJSON(jmpay);

                if (dict["code"].ToString() == "000")
                {
                    string wxpay = "{\"data\":\"" + dict["payUrl"].ToString() + "\",\"PaymentType\":\"2\",\"SubType\":\"8\",\"IsH5\":\"1\"}";
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(wxpay, ConfigurationManager.AppSettings["encryption"].ToString());
                }
                else
                {
                    string ErrorMessage = "掌趣微信wap支付错误代码：" + jmpay + ",商户号：" + SeIn.UserId;
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + ErrorMessage, summary: "掌趣微信wap支付接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "掌趣微信wap接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }

        /// <summary>
        /// 获取掌趣账号信息
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
                        SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的掌趣微信wap支付账号
                        SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的掌趣微信wap支付key
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                    }
                    else
                    {

                        dt = bll.SelectPay("ZQWXWAP", apptype, appid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                            SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取掌趣微信wap支付账号
                            SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取掌趣微信wap支付key
                            SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                            SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                        }
                        else
                        {
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",获取缓存未成功后在次查询数据！", summary: "掌趣微信wap支付支付接口错误", channelId: SeIn.PayId);
                        }
                    }
                }
                else
                {
                    dt = bll.SelectPay("ZQWXWAP", apptype, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取掌趣微信wap支付账号
                        SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取掌趣微信wap支付key
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                    }
                    else
                    {
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",在数据库为查询到数据", summary: "掌趣微信wap支付支付接口错误", channelId: SeIn.PayId);
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog(bcxx, summary: "掌趣微信wap支付支付接口错误应用类型ID：" + apptype, channelId: SeIn.PayId);
            }
            return SeIn;
        }


        #endregion
    }
}
