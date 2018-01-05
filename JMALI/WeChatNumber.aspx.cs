using DxPay.LogManager.LogFactory.ApiLog;
using JMALI.notice;
using JMALI.WeChat;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WxPayAPI;

namespace JMALI
{
    public partial class WeChatNumber : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int oid = !string.IsNullOrEmpty(Request["pid"]) ? Convert.ToInt32(Request["pid"].ToString()) : 0; //订单表ID
            try
            {
                if (oid > 0)
                {
                    string code = !string.IsNullOrEmpty(Request["code"]) ? Request["code"] : "";

                    if (!string.IsNullOrEmpty(code))
                    {

                        TwoJump(oid, code);
                    }
                    else
                    {
                        OnJump(oid);
                    }
                }
                else
                {
                    Response.Write("非法访问！");
                }
            }
            catch (Exception ex)
            {
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + ex.Message, summary: "微信官方公众号支付接口错误信息", channelId: oid);
                Response.Write("非法访问！");
            }
        }

        /// <summary>
        /// 微信公众号第一次跳转
        /// </summary>
        /// <param name="Oid">订单id</param>
        private void OnJump(int Oid)
        {
            try
            {
                string str = "";
                JMP.MDL.jmp_app mo = new JMP.MDL.jmp_app();
                JMP.BLL.jmp_app blls = new JMP.BLL.jmp_app();
                mo = JMP.TOOL.MdlList.ToModel<JMP.MDL.jmp_app>(blls.GetList(" a_id=(SELECT o_app_id FROM jmp_order WHERE o_id=" + Oid + ")  ").Tables[0]);
                if (mo != null)
                {
                    SelectInterface SeIn = new SelectInterface();
                    string cache = "wxgfgzh" + Oid;
                    SeIn = SelectInfo(cache, mo.a_rid, mo.a_id, int.Parse(ConfigurationManager.AppSettings["CacheTime"].ToString()));
                    if (SeIn == null || SeIn.PayId <= 0 || string.IsNullOrEmpty(SeIn.UserId) || string.IsNullOrEmpty(SeIn.UserKey))
                    {
                        str = "{\"Message\":\"支付通道未配置\",\"ErrorCode\":106}";
                        Response.Write(str);
                    }
                    JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
                    JMP.MDL.jmp_order morder = bll.SelectOrderGoodsName(Oid, "jmp_order");
                    if (morder.o_price < SeIn.minmun)
                    {
                        str = "{\"Message\":\"订单金额不能小于单笔最小支付金额\",\"ErrorCode\":8990}";
                        Response.Write(str);
                    }
                    if (morder.o_price > SeIn.maximum)
                    {
                        str = "{\"Message\":\"订单金额不能大于单笔最大支付金额\",\"ErrorCode\":8989}";
                        Response.Write(str);
                    }
                    if (bll.UpdatePay(Oid, SeIn.PayId))
                    {
                        string redirect_uri = ConfigurationManager.AppSettings["WxGzhRturnUrl"].ToString() + Oid + ".html";
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + "回调地址:" + redirect_uri, summary: "微信官方公众号第一次跳转支付接口错误信息", channelId: Oid);
                        string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + SeIn.wxappid + "&redirect_uri=" + redirect_uri + "&response_type=code&scope=snsapi_base#wechat_redirect";
                        Response.Redirect(url, false);

                    }
                }
                else
                {
                    str = "{\"Message\":\"支付接口异常\",\"ErrorCode\":102}";
                    Response.Write(str);
                }
            }
            catch (Exception ex)
            {
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + ex.Message, summary: "微信官方公众号第一次跳转支付接口错误信息", channelId: Oid);
                Response.Write("非法访问！");
            }

        }
        /// <summary>
        /// 微信公众号第二次跳转
        /// </summary>
        /// <param name="Oid">订单id</param>
        /// <param name="Code">微信回传的Code编码</param>
        private void TwoJump(int Oid, string Code)
        {
            try
            {
                String AppId = "";
                String AppSecret = "";
                String UserId = "";
                String UserKey = "";
                JMP.MDL.jmp_order morder = new JMP.BLL.jmp_order().SelectOrderGoodsName(Oid, "jmp_order");
                WeChatOpenId weChatOpenId = new WeChatOpenId();
                String PayStr = weChatOpenId.GetPayStr(morder.o_interface_id.ToString(), "WxGfGZH");
                UserId = PayStr.ToString().Split(',')[0];//商户号
                UserKey = PayStr.ToString().Split(',')[1];//api秘钥 
                AppId = PayStr.ToString().Split(',')[2];//微信appid
                AppSecret = PayStr.ToString().Split(',')[3];//微信app秘钥
                string openid = weChatOpenId.SelectOpendi(AppId, AppSecret, Code);
                if (!string.IsNullOrEmpty(openid))
                {
                    Dictionary<string, string> List = new Dictionary<string, string>();
                    List.Add("appid", AppId);//微信appid
                    List.Add("mch_id", UserId);//商户号
                    List.Add("nonce_str", morder.o_code);//随机字符串
                    List.Add("body", morder.o_goodsname);//商品名称
                    List.Add("out_trade_no", morder.o_code);//商户订单号
                    List.Add("total_fee", (Convert.ToInt32(morder.o_price * 100)).ToString());//支付金额（单位：分）
                    List.Add("spbill_create_ip", HttpContext.Current.Request.UserHostAddress);//ip地址
                    int overtime = int.Parse(ConfigurationManager.AppSettings["overtime"].ToString());
                    List.Add("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//交易起始时间
                    List.Add("time_expire", DateTime.Now.AddSeconds(overtime).ToString("yyyyMMddHHmmss"));//交易结束时间
                    List.Add("notify_url", ConfigurationManager.AppSettings["WxTokenUrl"].ToString().Replace("{0}", morder.o_interface_id.ToString()));//异步通知地址
                    List.Add("trade_type", "JSAPI");//交易类型 NATIVE 微信扫码 JSAPI公众号
                    List.Add("openid", openid);//微信openid
                    string signstr = JMP.TOOL.UrlStr.AzGetStr(List) + "&key=" + UserKey;
                    string md5str = JMP.TOOL.MD5.md5strGet(signstr, true).ToUpper();
                    List.Add("sign", md5str);//签名
                    string PostXmlStr = JMP.TOOL.xmlhelper.ToXml(List);
                    string url = ConfigurationManager.AppSettings["WxPayUrl"].ToString();// 请求地址
                    string Respon = JMP.TOOL.postxmlhelper.postxml(url, PostXmlStr);
                    Dictionary<string, object> dictionary = JMP.TOOL.xmlhelper.FromXml(Respon);
                    if (dictionary.Count > 0 && dictionary["return_code"].ToString() == "SUCCESS" && dictionary["return_msg"].ToString() == "OK")
                    {
                        CallJsApid(Oid, dictionary, UserKey, morder.o_showaddress);
                    }
                    else
                    {
                        string wftzfsbxin = "微信官方公众号支付失败信息，错误信息：" + Respon;
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息openid:" + openid + "：" + wftzfsbxin, summary: "微信官方公众号第二次跳转接口错误信息", channelId: Oid);
                        String str = "{\"Message\":\"支付通道异常\",\"ErrorCode\":104}";
                        Response.Write(str);
                    }

                }
                else
                {
                    Response.Write("非法访问！");
                }
            }
            catch (Exception ex)
            {
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + ex.Message, summary: "微信官方公众号支付接口错误信息", channelId: Oid);
                Response.Write("非法访问！");
            }

        }
        /// <summary>
        /// 调用微信jsapi放方法
        /// </summary>
        /// <param name="Oid">订单id</param>
        /// <param name="dictionary">微信回传的键值集合</param>
        /// <param name="UserKey">api秘钥</param>
        /// <param name="showaddress">同步地址</param>
        private void CallJsApid(int Oid, Dictionary<string, object> dictionary, string UserKey, string showaddress)
        {
            try
            {
                string timestamp = JMP.TOOL.WeekDateTime.GetMilis;//时间戳
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("appId", dictionary["appid"].ToString());
                dict.Add("timeStamp", timestamp);
                dict.Add("nonceStr", dictionary["nonce_str"].ToString());
                dict.Add("signType", "MD5");
                string package = @"prepay_id=" + dictionary["prepay_id"];
                dict.Add("package", package);
                string apimd5 = JMP.TOOL.UrlStr.AzGetStr(dict) + "&key=" + UserKey;
                string jspaimd5 = JMP.TOOL.MD5.md5strGet(apimd5, true).ToUpper();
                string chengstr = "<script type=\"text/javascript\">function onBridgeReady(){WeixinJSBridge.invoke( 'getBrandWCPayRequest', {\"appId\": \"" + dictionary["appid"] + "\", \"timeStamp\": \"" + timestamp + "\", \"nonceStr\": \"" + dictionary["nonce_str"] + "\",\"package\":\"" + package + "\",\"signType\": \"MD5\",\"paySign\": \"" + jspaimd5 + "\" },function(res) { if (res.err_msg ==\"get_brand_wcpay_request:ok\") {  window.location.href=\"" + showaddress + "\" }else{ alert(res.err_msg) } });}if (typeof WeixinJSBridge == \"undefined\"){if (document.addEventListener){document.addEventListener('WeixinJSBridgeReady', onBridgeReady, false);}else if (document.attachEvent){document.attachEvent('WeixinJSBridgeReady', onBridgeReady); document.attachEvent('onWeixinJSBridgeReady', onBridgeReady);} }else{onBridgeReady();}</script> ";
                Response.Write(chengstr);
            }
            catch (Exception ex)
            {
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + ex.Message, summary: "微信官方公众号支付接口调用jsApi时错误信息", channelId: Oid);
                String str = "{\"Message\":\"支付通道异常\",\"ErrorCode\":104}";
                Response.Write(str);
            }

        }
        #region 获取微信官方公众号支付账户信息
        /// <summary>
        /// 查询微信官方公众号支付账户信息
        /// </summary>
        /// <param name="cache">缓存值</param>
        /// <param name="appid">应用id</param>
        /// <param name="apptype">应用类型id</param>
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
                        SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取微信秘钥
                        SeIn.wxappid = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//获取微信appid
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());//获取支付通道id
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                    }
                    else
                    {
                        dt = bll.SelectPay("WXGFGZH", apptype, appid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                            SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取微信商户号
                            SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取微信秘钥
                            SeIn.wxappid = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//获取微信appid
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
                    dt = bll.SelectPay("WXGFGZH", apptype, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取微信商户号
                        SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取微信秘钥
                        SeIn.wxappid = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//获取微信appid
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