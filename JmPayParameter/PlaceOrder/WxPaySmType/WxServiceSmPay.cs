using JmPayParameter.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxPay.LogManager.LogFactory.ApiLog;
using System.Data;
using JmPayParameter.PayChannel;

namespace JmPayParameter.PlaceOrder.WxPaySmType
{
    /// <summary>
    /// 微信服务商扫码支付模式
    /// </summary>
    public class WxServiceSmPay
    {
        /// <summary>
        /// 微信扫码主通道
        /// </summary>
        /// <param name="paymode">平台类型（1：安卓，2：ios，3：H5）</param>
        /// <param name="apptype">应用类型子id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">价格</param>
        /// <param name="orderid">订单id</param>
        /// <param name="infoTime">缓存时间</param>
        /// <param name="appid">应用id</param>
        /// <param name="ip">ip地址</param>
        /// <returns></returns>
        public InnerResponse WxServiceSmPayInfo(int paymode, int apptype, string code, string goodsname, decimal price, int orderid, int infoTime, int appid, string ip)
        {
            InnerResponse inn = new InnerResponse();
            if (paymode == 3)
            {
                inn = WxServiceSmH5(apptype, code, price, orderid, goodsname, infoTime, appid, ip);
            }
            else
            {
                inn = inn.ToResponse(ErrorCode.Code9987);
            }
            return inn;
        }

        /// <summary>
        /// 微信官方扫码支付
        /// </summary>
        /// <param name="apptype">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="price">商品价格</param>
        /// <param name="orderid">订单id</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse WxServiceSmH5(int apptype, string code, decimal price, int orderid, string goodsname, int infoTime, int appid, string ip)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {
                string WxServiceSmH5 = "WxServiceSmH5" + appid;//组装缓存key值

                SeIn = SelectInfo(WxServiceSmH5, apptype, appid, infoTime);
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
                Dictionary<string, string> List = new Dictionary<string, string>();
                List.Add("appid", SeIn.wxappid);//微信appid
                List.Add("mch_id", SeIn.UserId);//商户号
                List.Add("sub_mch_id", SeIn.UserIdZ);//子商户号
                List.Add("nonce_str", code);//随机字符串
                List.Add("body", goodsname);//商品名称
                List.Add("out_trade_no", code);//商户订单号
                List.Add("total_fee", (Convert.ToInt32(price * 100)).ToString());//支付金额（单位：分）
                List.Add("spbill_create_ip", ip);//ip地址
                int overtime = int.Parse(ConfigurationManager.AppSettings["overtime"].ToString());
                List.Add("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//交易起始时间
                List.Add("time_expire", DateTime.Now.AddSeconds(overtime).ToString("yyyyMMddHHmmss"));//交易结束时间
                List.Add("notify_url", ConfigurationManager.AppSettings["WxTokenUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//异步通知地址
                List.Add("trade_type", "NATIVE");//交易类型 NATIVE 微信扫码 JSAPI公众号
                string signstr = JMP.TOOL.UrlStr.AzGetStr(List) + "&key=" + SeIn.UserKey;
                string md5str = JMP.TOOL.MD5.md5strGet(signstr, true).ToUpper();
                List.Add("sign", md5str);//签名
                string PostXmlStr = JMP.TOOL.xmlhelper.ToXml(List);
                string url = ConfigurationManager.AppSettings["WxPayUrl"].ToString();// 请求地址
                string Respon = JMP.TOOL.postxmlhelper.postxml(url, PostXmlStr);
                Dictionary<string, object> dictionary = JMP.TOOL.xmlhelper.FromXml(Respon);
                if (dictionary.Count > 0 && dictionary["return_code"].ToString() == "SUCCESS" && dictionary.ContainsKey("code_url"))
                {
                    //string qcode = dictionary["code_url"].ToString();
                    string qurl = dictionary["code_url"].ToString() + "," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ",2";//组装二维码地址
                    string ImgQRcode = ConfigurationManager.AppSettings["ImgQRcode"].ToString() + "?QRcode=" + JMP.TOOL.Encrypt.IndexEncrypt(qurl);//二维码图片展示地址
                    string codeurl = ConfigurationManager.AppSettings["QRcode"].ToString() + "?QRcode=" + JMP.TOOL.Encrypt.IndexEncrypt(qurl);//二维码展示地址
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = new { ImgQRcode = ImgQRcode, codeurl = codeurl };//http提交方式;

                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "微信官方扫码接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }
        #region 获取微信官方扫码支付账户信息
        /// <summary>
        /// 查询微信官方扫码支付账户信息
        /// </summary>
        /// <param name="cache">缓存值</param>
        /// <param name="appid">应用id</param>
        /// <param name="apptype">风控配置表id</param>
        /// <returns></returns>
        private SelectInterface SelectInfo(string cache, int apptype, int appid, int infoTimes)
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
                        SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取微信商户号
                        SeIn.wxappid = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取微信appid
                        SeIn.UserIdZ = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//获取微信秘钥
                        SeIn.UserKey = paypz[3].Replace("\r", "").Replace("\n", "").Trim();//获取微信秘钥
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());//获取支付通道id
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                    }
                    else
                    {
                        dt = bll.SelectPay("WXService", apptype, appid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                            SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取微信商户号
                            SeIn.wxappid = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取微信appid
                            SeIn.UserIdZ = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//获取微信秘钥
                            SeIn.UserKey = paypz[3].Replace("\r", "").Replace("\n", "").Trim();//获取微信秘钥
                            SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());//获取支付通道id
                            SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                        }
                        else
                        {
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",获取缓存失败后，从数据库未查询到相关信息！", summary: "微信官方扫码支付接口错误", channelId: SeIn.PayId);
                        }
                    }
                }
                else
                {
                    dt = bll.SelectPay("WXService", apptype, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取微信商户号
                        SeIn.wxappid = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取微信appid
                        SeIn.UserIdZ = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//获取微信秘钥
                        SeIn.UserKey = paypz[3].Replace("\r", "").Replace("\n", "").Trim();//获取微信秘钥
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());//获取支付通道id
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                    }
                    else
                    {
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",直接从数据库未查询到相关信息！", summary: "微信官方扫码支付接口错误", channelId: SeIn.PayId);
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog(bcxx, summary: "微信官方扫码支付接口错误应用ID：" + appid, channelId: SeIn.PayId);//写入报错日志
            }
            return SeIn;
        }
        #endregion
    }
}
