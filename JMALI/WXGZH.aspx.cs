using JMP.TOOL;
using swiftpass.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using DxPay.LogManager.LogFactory.ApiLog;

namespace JMALI
{
    public partial class WXGZH : System.Web.UI.Page
    {
        /// <summary>
        /// 威富通微信公众号支付
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            int oid = !string.IsNullOrEmpty(Request["pid"]) ? Convert.ToInt32(Request["pid"].ToString()) : 0;

            if (oid > 0)
            {
                //获取缓存
                try
                {
                    string appid = "";
                    string appms = "";
                    string code = !string.IsNullOrEmpty(Request["code"]) ? Request["code"] : "";
                    if (!String.IsNullOrEmpty(code))
                    {
                        //第二次
                        JMP.MDL.jmp_order morder = new JMP.BLL.jmp_order().SelectOrderGoodsName(oid, "jmp_order");
                        string ddjj = Get_paystr(morder.o_interface_id.ToString());
                        appid = ddjj.ToString().Split(',')[2];
                        appms = ddjj.ToString().Split(',')[3];
                        string openid = "";
                        string URL = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + appid + "&secret=" + appms + "&code=" + code + "&grant_type=authorization_code";
                        Encoding encoding = Encoding.UTF8;
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                        request.Timeout = 3000;
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8))
                        {
                            string jmpay = reader.ReadToEnd();
                            //解析json对象
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            Dictionary<string, object> json = (Dictionary<string, object>)serializer.DeserializeObject(jmpay);
                            object value = null;
                            json.TryGetValue("openid", out value);
                            openid = value.ToString();
                        }
                        string str = "";
                        ClientResponseHandler resHandler = new ClientResponseHandler();
                        PayHttpClient pay = new PayHttpClient();
                        RequestHandler reqHandler = new RequestHandler(null);
                        Dictionary<string, string> cfg = Utils.Load_CfgInterfaceId(morder.o_interface_id);
                        reqHandler.setGateUrl(cfg["req_url"].ToString());
                        reqHandler.setKey(cfg["key"].ToString());
                        reqHandler.setParameter("out_trade_no", morder.o_code);//我们的订单号
                        reqHandler.setParameter("body", morder.o_goodsname);//商品描述
                        // reqHandler.setParameter("attach", string.IsNullOrEmpty(morder.o_privateinfo) ? "404" : morder.o_privateinfo);//附加信息
                        reqHandler.setParameter("total_fee", (Convert.ToInt32(morder.o_price * 100)).ToString());//价格
                        reqHandler.setParameter("mch_create_ip", HttpContext.Current.Request.UserHostAddress);//终端IP 
                        reqHandler.setParameter("service", "pay.weixin.jspay");
                        reqHandler.setParameter("mch_id", cfg["mch_id"].ToString());
                        reqHandler.setParameter("version", cfg["version"].ToString());
                        reqHandler.setParameter("notify_url", ConfigurationManager.AppSettings["notifyurl"].ToString().Replace("{0}", morder.o_interface_id.ToString()));//回掉地址
                        reqHandler.setParameter("callback_url", ConfigurationManager.AppSettings["callbackurl"].ToString().Replace("{0}", oid.ToString()));//同步回掉地址
                        reqHandler.setParameter("sub_openid", openid);//获取openid
                        reqHandler.setParameter("nonce_str", Utils.random());//随机字符串
                        reqHandler.setParameter("charset", "UTF-8");
                        reqHandler.setParameter("sign_type", "MD5");
                        reqHandler.setParameter("is_raw", "1");//原生JS
                        #region 判断是否需要禁用信用卡
                        //JMP.MDL.jmp_app moapp = new JMP.MDL.jmp_app();
                        //int tid = 0;
                        //string hc = "gzhpdxykjy" + morder.o_app_id;
                        //if (JMP.TOOL.CacheHelper.IsCache(hc))//判读是否存在缓存
                        //{
                        //    moapp = JMP.TOOL.CacheHelper.GetCaChe<JMP.MDL.jmp_app>(hc);//获取缓存
                        //    if (moapp != null)
                        //    {
                        //        tid = moapp.a_apptype_id > 0 ? moapp.a_apptype_id : 0;
                        //        if (tid == 0)
                        //        {
                        //            JMP.BLL.jmp_app blapp = new JMP.BLL.jmp_app();
                        //            moapp = blapp.SelectId(morder.o_app_id);
                        //            JMP.TOOL.CacheHelper.CacheObjectLocak<JMP.MDL.jmp_app>(moapp, hc, 5);//存入缓存 
                        //        }
                        //    }
                        //    else
                        //    {
                        //        JMP.BLL.jmp_app blapp = new JMP.BLL.jmp_app();
                        //        moapp = blapp.SelectId(morder.o_app_id);
                        //        tid = moapp.a_apptype_id > 0 ? moapp.a_apptype_id : 0;
                        //        JMP.TOOL.CacheHelper.CacheObjectLocak<JMP.MDL.jmp_app>(moapp, hc, 5);//存入缓存
                        //    }
                        //}
                        //else
                        //{
                        //    JMP.BLL.jmp_app blapp = new JMP.BLL.jmp_app();
                        //    moapp = blapp.SelectId(morder.o_app_id);
                        //    tid = moapp.a_apptype_id > 0 ? moapp.a_apptype_id : 0;
                        //    JMP.TOOL.CacheHelper.CacheObjectLocak<JMP.MDL.jmp_app>(moapp, hc, 5);//存入缓存
                        //}
                        //if (tid == 71)
                        //{
                        //    reqHandler.setParameter("limit_credit_pay", "1");//是否限制信用卡（1：限制，0：不限制）
                        //}
                        #endregion
                        reqHandler.createSign();
                        string datawft = Utils.toXml(reqHandler.getAllParameters());
                        Dictionary<string, string> reqContent = new Dictionary<string, string>();
                        reqContent.Add("url", reqHandler.getGateUrl());
                        reqContent.Add("data", datawft);
                        pay.setReqContent(reqContent);
                        if (pay.call())
                        {
                            resHandler.setContent(pay.getResContent());
                            resHandler.setKey(cfg["key"].ToString());
                            Hashtable param = resHandler.getAllParameters();
                            Dictionary<string, string> dic = JMP.TOOL.UrlStr.hastable(param);
                            string wftmsg = JMP.TOOL.JsonHelper.DictJsonstr(dic);
                            if (resHandler.isTenpaySign())
                            {
                                if (int.Parse(param["status"].ToString()) == 0 && int.Parse(param["result_code"].ToString()) == 0)
                                {
                                    string pay_info = dic["pay_info"];
                                    if (!string.IsNullOrEmpty(pay_info))
                                    {
                                        Dictionary<string, object> List = new Dictionary<string, object>();
                                        List = JMP.TOOL.JsonHelper.DataRowFromJSON(pay_info);
                                        try
                                        {
                                            string chengstr = "<script type=\"text/javascript\">function onBridgeReady(){WeixinJSBridge.invoke( 'getBrandWCPayRequest', {\"appId\": \"" + List["appId"] + "\", \"timeStamp\": \"" + List["timeStamp"] + "\", \"nonceStr\": \"" + List["nonceStr"] + "\",\"package\":\"" + List["package"] + "\",\"signType\": \"MD5\",\"paySign\": \"" + List["paySign"] + "\" },function(res) {if (res.err_msg ==\"get_brand_wcpay_request:ok\") {  window.location.href=\"" + List["callback_url"] + "\" }else{ alert(res.err_msg) } });}if (typeof WeixinJSBridge == \"undefined\"){if (document.addEventListener){document.addEventListener('WeixinJSBridgeReady', onBridgeReady, false);}else if (document.attachEvent){document.attachEvent('WeixinJSBridgeReady', onBridgeReady); document.attachEvent('onWeixinJSBridgeReady', onBridgeReady);} }else{onBridgeReady();}</script> ";
                                            Response.Write(chengstr);
                                        }
                                        catch
                                        {
                                            string wftzfsbxin = "威富通公众号支付失败信息，错误信息：" + wftmsg;
                                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息openid:" + openid + "：" + wftzfsbxin, summary: "威富通公众号接口错误信息2", channelId: oid);
                                            str = "{\"Message\":\"支付通道异常\",\"ErrorCode\":104}";
                                        }
                                    }
                                    else
                                    {
                                        string wftzfsbxin = "威富通公众号支付失败信息，错误信息：" + wftmsg;
                                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息openid:" + openid + "：" + wftzfsbxin, summary: "威富通公众号接口错误信息4", channelId: oid);
                                        str = "{\"Message\":\"支付通道异常\",\"ErrorCode\":104}";
                                    }
                                }
                                else
                                {
                                    string wftzfsbxin = "威富通公众号支付失败信息，错误信息：" + wftmsg;
                                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息openid:" + openid + "：" + wftzfsbxin, summary: "威富公众号通接口错误信息3", channelId: oid);
                                    str = "{\"Message\":\"支付通道异常\",\"ErrorCode\":104}";
                                }
                            }
                            else
                            {
                                str = "{\"Message\":\"支付通道异常\",\"ErrorCode\":104}";
                                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息openid:" + openid, summary: "威富公众号通接口错误信息4", channelId: oid);
                            }
                        }
                        else
                        {
                            str = "{\"Message\":\"支付通道异常\",\"ErrorCode\":104}";
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息openid:" + openid, summary: "威富公众号通接口错误信息5", channelId: oid);
                        }
                        Response.Write(str);

                    }
                    else
                    {
                        JMP.MDL.jmp_app mo = new JMP.MDL.jmp_app();
                        JMP.BLL.jmp_app blls = new JMP.BLL.jmp_app();
                        mo = JMP.TOOL.MdlList.ToModel<JMP.MDL.jmp_app>(blls.GetList(" a_id=(SELECT o_app_id FROM jmp_order WHERE o_id=" + oid + ")  ").Tables[0]);
                        if (mo != null)
                        {
                            Dictionary<string, string> cfg = Utils.loadCfgWxgzh(mo.a_rid, mo.a_id);
                            JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
                            if (bll.UpdatePay(oid, int.Parse(cfg["pay_id"].ToString())))
                            {
                                string ddjj = Get_paystr(cfg["pay_id"].ToString());
                                appid = ddjj.ToString().Split(',')[2];
                                appms = ddjj.ToString().Split(',')[3];
                                string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + appid + "&redirect_uri=" + ConfigurationManager.AppSettings["redirecturi"].ToString() + oid + ".html&response_type=code&scope=snsapi_base#wechat_redirect";
                                Response.Redirect(url, false);

                            }
                        }
                        else
                        {
                            string str = "{\"Message\":\"支付接口异常\",\"ErrorCode\":102}";
                            Response.Write(str);
                        }
                    }
                }
                catch (Exception ex)
                {
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + ex.ToString() + "订单表id：" + oid, summary: "威富公众号通接口错误信息", channelId: oid);
                    Response.Write("非法访问！");
                }
            }
            else
            {
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：第一步判断，订单表id：" + oid, summary: "威富公众号通接口错误信息", channelId: oid);
                Response.Write("非法访问！");
            }
        }

        /// <summary>
        /// 获取支付字符串
        /// </summary>
        /// <param name="pid"> 支付通道ID</param>
        /// <returns></returns>
        string Get_paystr(string pid)
        {
            string wxgghzfhc = "wxgghzfhc" + pid.ToString();
            if (JMP.TOOL.CacheHelper.IsCache(wxgghzfhc))
            {
                object ddjj = JMP.TOOL.CacheHelper.GetCaChe(wxgghzfhc);
                return ddjj.ToString();
            }
            else
            {

                object ddjj = JMP.DBA.DbHelperSQL.GetSingle("SELECT l_str FROM jmp_interface WHERE l_id=" + pid + "");
                if (ddjj != null)
                {
                    JMP.TOOL.CacheHelper.CacheObject(wxgghzfhc, ddjj, 5);//存入缓存
                    return ddjj.ToString();
                }
                else
                {
                    return "";
                }
            }

        }
    }
}