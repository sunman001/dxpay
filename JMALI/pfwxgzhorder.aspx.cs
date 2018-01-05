using DxPay.LogManager.LogFactory.ApiLog;
using System;
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

namespace JMALI
{
    public partial class pfwxgzhorder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int oid = !string.IsNullOrEmpty(Request.QueryString["pid"]) ? Convert.ToInt32(Request.QueryString["pid"].ToString()) : 0; //订单表ID
                string code = !string.IsNullOrEmpty(Request.QueryString["code"]) ? Request.QueryString["code"] : "";
                string UserId = "";
                string UserKey = "";
                string appid = "";
                string appms = "";
                if (!String.IsNullOrEmpty(code) && oid > 0)
                {
                    string json = "";
                    string str = "";
                    string openid = "";
                    //System.Threading.Thread.Sleep(new Random().Next(100, 500));
                    JMP.MDL.jmp_order morder = new JMP.BLL.jmp_order().SelectOrderGoodsName(oid, "jmp_order");
                    if (JMP.TOOL.CacheHelper.IsCache(morder.o_code) == false)
                    {
                        string ddjj = Get_paystr(morder.o_interface_id.ToString());
                        UserId = ddjj.ToString().Split(',')[0];
                        UserKey = ddjj.ToString().Split(',')[1];
                        appid = ddjj.ToString().Split(',')[2];
                        appms = ddjj.ToString().Split(',')[3];

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
                            Dictionary<string, object> jsonstr = (Dictionary<string, object>)serializer.DeserializeObject(jmpay);
                            object value = null;
                            jsonstr.TryGetValue("openid", out value);
                            openid = value.ToString();
                        }
                        string xml = "<?xml version='1.0' encoding='utf-8' ?><ORDER_REQ><BUSI_ID>" + UserId + "</BUSI_ID><OPER_ID>oper01</OPER_ID><DEV_ID>dev01</DEV_ID><AMT>" + morder.o_price + "</AMT><CHANNEL_TYPE>2</CHANNEL_TYPE><TRADE_TYPE>JSAPI</TRADE_TYPE><subAppid>" + appid + "</subAppid><subOpenid>" + openid + "</subOpenid><PAY_SUBJECT>" + morder.o_goodsname + "</PAY_SUBJECT ><CHARGE_CODE>" + morder.o_code + "</CHARGE_CODE><NODIFY_URL>" + ConfigurationManager.AppSettings["pfalpayNotifyUrl"].ToString().Replace("{0}", morder.o_interface_id.ToString()) + "</NODIFY_URL></ORDER_REQ>";
                        string timestamp = JMP.TOOL.WeekDateTime.GetMilis;//时间戳
                        string signstr = timestamp + UserKey + xml.Replace(" ", "");
                        string sign = JMP.TOOL.MD5.md5strGet(signstr, true).ToLower() + ":" + timestamp; ;
                        string url = ConfigurationManager.AppSettings["pfalpayPostUrl"].ToString() + "?sign=" + sign + "&_type=json&busiCode=" + UserId;
                        json = JMP.TOOL.postxmlhelper.postxml(url, xml);
                        JMP.TOOL.CacheHelper.CacheObject(morder.o_code, json, 1);//存入缓存
                    }
                    else
                    {
                        json = JMP.TOOL.CacheHelper.GetCaChe<string>(morder.o_code);
                    }
                    RootObject obj = new RootObject();
                    obj = JMP.TOOL.JsonHelper.Deserializes<RootObject>(json);
                    if (obj != null && obj.ORDER_RESP.RESULT.CODE == "SUCCESS")
                    {
                        if (string.IsNullOrEmpty(morder.o_showaddress))
                        {
                            morder.o_showaddress = ConfigurationManager.AppSettings["succeed"].ToString();
                        }
                        string chengstr = "<script src=\"http://res.wx.qq.com/open/js/jweixin-1.2.0.js\"></script><script type=\"text/javascript\">function onBridgeReady(){WeixinJSBridge.invoke( 'getBrandWCPayRequest', {\"appId\": \"" + obj.ORDER_RESP.appId + "\", \"timeStamp\": \"" + obj.ORDER_RESP.timeStamp + "\", \"nonceStr\": \"" + obj.ORDER_RESP.nonceStr + "\",\"package\":\"" + obj.ORDER_RESP.packageData + "\",\"signType\": \"MD5\",\"paySign\": \"" + obj.ORDER_RESP.sign + "\" },function(res) {if (res.err_msg ==\"get_brand_wcpay_request:ok\") {  window.location.href=\"" + morder.o_showaddress + "\" }else{ alert(res.err_msg) } });}if (typeof WeixinJSBridge == \"undefined\"){if (document.addEventListener){document.addEventListener('WeixinJSBridgeReady', onBridgeReady, false);}else if (document.attachEvent){document.attachEvent('WeixinJSBridgeReady', onBridgeReady); document.attachEvent('onWeixinJSBridgeReady', onBridgeReady);} }else{onBridgeReady();}</script> ";
                        Response.Write(chengstr);
                    }
                    else
                    {
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("浦发银行公众号支付失败信息：" + json, channelId: oid);
                        str = "{\"Message\":\"支付通道异常\",\"ErrorCode\":104}";
                        Response.Write(str);
                    }
                }
                else
                {
                    Response.Write("非法访问！");
                }

            }
        }
        #region 接受回传参数实体
        private class RESULT
        {
            public string CODE { get; set; }
            public string INFO { get; set; }
        }
        private class ORDERRESP
        {
            public string BEGIN_TIME { get; set; }
            public string END_TIME { get; set; }
            public string CHARGE_CODE { get; set; }
            public string CHARGE_DOWN_CODE { get; set; }
            public RESULT RESULT { get; set; }
            public string BAR_CODE { get; set; }
            public string appId { get; set; }
            public string timeStamp { get; set; }
            public string nonceStr { get; set; }
            public string packageData { get; set; }
            public string signType { get; set; }
            public string sign { get; set; }
        }
        private class RootObject
        {
            public ORDERRESP ORDER_RESP { get; set; }
        }
        #endregion
        /// <summary>
        /// 获取支付字符串
        /// </summary>
        /// <param name="pid"> 支付通道ID</param>
        /// <returns></returns>
        string Get_paystr(string pid)
        {
            string pfwxgghzfhc = "pfwxgghzfhc" + pid.ToString();
            if (JMP.TOOL.CacheHelper.IsCache(pfwxgghzfhc))
            {
                object ddjj = JMP.TOOL.CacheHelper.GetCaChe(pfwxgghzfhc);
                return ddjj.ToString();
            }
            else
            {

                object ddjj = JMP.DBA.DbHelperSQL.GetSingle("SELECT l_str FROM jmp_interface WHERE l_id=" + pid + "");
                if (ddjj != null)
                {
                    JMP.TOOL.CacheHelper.CacheObject(pfwxgghzfhc, ddjj, 5);//存入缓存
                    return ddjj.ToString();
                }
                else
                {
                    return "";
                }
            }

        }

        /// <summary>
        /// 获取浦发账号信息 截取到通道id
        /// </summary>
        /// <param name="cache">缓存key</param>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private int SelectUserInfo(int apptype, int appid)
        {
            int PayId = 0;
            string cache = "pfyhwxgzhpay" + appid;
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
                        PayId = Int32.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype, channelId: PayId);
                    }
                }
                else
                {
                    dt = bll.SelectPay("PFWXGZH", apptype, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        PayId = Int32.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, int.Parse(ConfigurationManager.AppSettings["CacheTime"].ToString()));//存入缓存
                    }
                    else
                    {
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype, channelId: PayId);
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog(bcxx, summary: "浦发银行微信公众号支付接口错误", channelId: PayId);
            }
            return PayId;
        }
    }
}