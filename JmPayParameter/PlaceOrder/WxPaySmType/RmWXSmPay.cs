using DxPay.LogManager.LogFactory.ApiLog;
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

namespace JmPayParameter.PlaceOrder.WxPaySmType
{
    /// <summary>
    /// 融梦微信扫码支付
    /// </summary>
    public class RmWXSmPay
    {
        /// <summary>
        /// 融梦微信扫码支付通道主入口
        /// </summary>
        /// <param name="paymode">平台类型（1：安卓，2：ios，3：H5）</param>
        /// <param name="apptype">应用类型子id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格（单位元）</param>
        /// <param name="orderid">订单id</param>
        /// <param name="infoTime">缓存时间</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        public  InnerResponse RmWxsmPayInfo(int paymode, int apptype, string code, string goodsname, decimal price, int orderid, int infoTime, int appid)
        {
            InnerResponse inn = new InnerResponse();
            if (paymode == 3)
            {
                inn = RmWxSmH5(apptype, code, price, orderid, goodsname, infoTime, appid);
            }
            else
            {
                inn = inn.ToResponse(ErrorCode.Code9987);
            }
            return inn;
        }

        /// <summary>
        /// 融梦微信扫支付
        /// </summary>
        /// <param name="apptype">应用类型id</param>
        /// <param name="code">订单号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private  InnerResponse RmWxSmH5(int apptype, string code, decimal price, int orderid, string goodsname, int infoTime, int appid)
        {

            InnerResponse inn = new InnerResponse();
            int pay_id = 0;//支付渠道id
            try
            {
                string userid = "";//融梦商户id
                string userkey = "";//融梦key
                string partnerId = "";//cpid

                decimal minmun = 0;
                decimal maximum = 0;
                string RmWxSmH5 = "RmWxSmH5" + appid;//组装缓存key值
                #region 融梦微信扫码支付账号信息
                try
                {
                    DataTable dt = new DataTable();
                    JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                    if (JMP.TOOL.CacheHelper.IsCache(RmWxSmH5))
                    {
                        dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(RmWxSmH5);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                            userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的融梦微信扫码id
                            partnerId = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的融梦微信扫码cpid
                            userkey = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的融梦微信扫码key
                            pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                            minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        }
                        else
                        {
                            dt = bll.SelectPay("RMWXSM", apptype, appid);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                int row = new Random().Next(0, dt.Rows.Count);
                                string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                                userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取融梦id
                                partnerId = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的融梦微信扫码cpid
                                userkey = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的融梦微信扫码key
                                pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                                minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                                maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                                JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, RmWxSmH5, infoTime);//存入缓存
                            }
                            else
                            {
                                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",获取缓存失败后，从数据库未查询到相关信息！", summary: "融梦微信扫码接口错误", channelId: pay_id);
                                inn = inn.ToResponse(ErrorCode.Code106);
                                return inn;
                            }
                        }
                    }
                    else
                    {
                        dt = bll.SelectPay("RMWXSM", apptype, appid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                            userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取融梦id
                            partnerId = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的融梦微信扫码cpid
                            userkey = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的融梦微信扫码key
                            pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                            minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, RmWxSmH5, infoTime);//存入缓存
                        }
                        else
                        {
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",从数据库未查询到相关信息！", summary: "融梦微信扫码接口错误", channelId: pay_id);
                            inn = inn.ToResponse(ErrorCode.Code106);
                            return inn;
                        }
                    }
                }
                catch (Exception e)
                {
                    string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog(bcxx, summary: "融梦微信扫码支付接口错误应用类型ID：" + apptype, channelId: pay_id);
                    inn = inn.ToResponse(ErrorCode.Code106);
                    return inn;
                }
                #endregion
                if (!UpdateOrde.OrdeUpdateInfo(orderid, pay_id))
                {
                    inn = inn.ToResponse(ErrorCode.Code101);
                    return inn;
                }
                if (!JudgeMoney.JudgeMinimum(price, minmun))
                {
                    inn = inn.ToResponse(ErrorCode.Code8990);
                    return inn;
                }
                if (!JudgeMoney.JudgeMaximum(price, maximum))
                {
                    inn = inn.ToResponse(ErrorCode.Code8989);
                    return inn;
                }
                Dictionary<string, string> list = new Dictionary<string, string>();
                list.Add("appId", userid);//产品id
                list.Add("partnerId", partnerId);//cpid
                list.Add("channelOrderId", code);//订单编号
                list.Add("body", goodsname);//商品名称
                list.Add("totalFee", (Convert.ToInt32(price * 100)).ToString());//金额（单位：分）
                list.Add("payType", "1400");//支付类型
                list.Add("timeStamp", JMP.TOOL.WeekDateTime.GetMilis);//时间戳
                string signstr = "appId=" + list["appId"] + "&timeStamp=" + list["timeStamp"] + "&totalFee=" + list["totalFee"] + "&key=" + userkey;
                string sign = JMP.TOOL.MD5.md5strGet(signstr, true).ToUpper();
                list.Add("notifyUrl", ConfigurationManager.AppSettings["rmWxsmNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步通知地址
                list.Add("sign", sign);//签名
                string yrl = ConfigurationManager.AppSettings["rmWxsmPostUrl"].ToString() + "?" + JMP.TOOL.UrlStr.AzGetStrnotnull(list);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(yrl);
                request.Timeout = 3000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string jmpay = "";
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8))
                {
                    jmpay = reader.ReadToEnd();
                }

                if (!string.IsNullOrEmpty(jmpay))
                {
                    Dictionary<string, object> jsonstr = JMP.TOOL.JsonHelper.DataRowFromJSON(jmpay);
                    if (jsonstr != null && jsonstr.Count > 0 && jsonstr.ContainsKey("payParam") && jsonstr["return_code"].ToString() == "0")
                    {
                        string exda = ((Dictionary<string, object>)jsonstr["payParam"])["code_img_url"].ToString();
                        string imageurl = System.Web.HttpUtility.UrlDecode(exda).Replace("https://pay.swiftpass.cn/pay/qrcode?uuid=", "");
                        string qurl = imageurl + "," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ",2";//组装二维码地址
                        string ImgQRcode = ConfigurationManager.AppSettings["ImgQRcode"].ToString() + "?QRcode=" + JMP.TOOL.Encrypt.IndexEncrypt(qurl);//二维码图片展示地址
                        string codeurl = ConfigurationManager.AppSettings["QRcode"].ToString() + "?QRcode=" + JMP.TOOL.Encrypt.IndexEncrypt(qurl);//二维码展示地址
                        inn = inn.ToResponse(ErrorCode.Code100);
                        inn.ExtraData = new { ImgQRcode = ImgQRcode, codeurl = codeurl };//http提交方式;

                    }
                    else
                    {
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("融梦微信扫码支付出错，获取到的回传参数：" + jmpay, summary: "融梦微信扫码支付接口错误", channelId: pay_id);
                        inn = inn.ToResponse(ErrorCode.Code104);
                    }
                }
                else
                {
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("融梦微信扫码支付出错，获取到的回传参数：" + jmpay, summary: "融梦微信扫码支付接口错误", channelId: pay_id);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "融梦微信扫码接口错误信息", channelId: pay_id);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }
    }
}
