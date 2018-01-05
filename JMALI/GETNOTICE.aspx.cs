/************聚米支付平台__支付宝支付通知************/
//描述：支付宝支付通知
//功能：支付宝支付通知回调页面
//开发者：胡玉溪
//开发时间: 2016.03.18
/************聚米支付平台__支付宝支付通知************/
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Collections.Generic;
using Alipay;
using DxPay.LogManager.LogFactory.ApiLog;
using JMALI;
using JMP.TOOL;
using JMALI.notice;

namespace JMPAY
{
    public partial class GETNOTICE : System.Web.UI.Page
    {
        /// <summary>
        /// 支付宝官网通知程序接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            SortedDictionary<string, string> sPara = GetRequestPost();
            string jsonstr = "";
            int pid = 0;
            Dictionary<string, string> jsonlist = JMP.TOOL.UrlStr.GetRequestfrom(HttpContext.Current, "支付宝官网通知接口");
            if (jsonlist != null)
            {
                jsonstr = JMP.TOOL.JsonHelper.DictJsonstr(jsonlist);//把获取的参数转换成字符串
            }
            try
            {
                pid = !string.IsNullOrEmpty(Request.QueryString["pid"]) ? int.Parse(Request.QueryString["pid"].ToString()) : 0;
                Notify aliNotify = new Notify(pid);
                bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);
                if (verifyResult && pid > 0 && jsonlist["trade_status"] == "TRADE_SUCCESS")
                {
                    //买家付款时间
                    DateTime gmt_payment = DateTime.Parse(jsonlist["gmt_payment"]);
                    //订单金额（单位：元）
                    decimal price = jsonlist.ContainsKey("total_fee") ? decimal.Parse(jsonlist["total_fee"]) : decimal.Parse(jsonlist["total_amount"]);//支付金额（单位元）
                    string buyer_email = jsonlist.ContainsKey("buyer_email") ? jsonlist["buyer_email"] : jsonlist["buyer_id"];
                    JMALI.notice.notice notic = new notice();
                    string message = notic.PubNotice(jsonlist["out_trade_no"], price, gmt_payment, jsonlist["trade_no"], buyer_email, pid, "支付宝官网通知接口", jsonstr);
                    if (message == "ok")
                    {
                        Response.Write("success");
                    }
                    else
                    {
                        Response.Write("fail");
                    }
                }
                else
                {
                    Response.Write("fail");
                }
            }
            catch (Exception ex)
            {
                PayApiDetailErrorLogger.UpstreamNotifyErrorLog("获取到的参数：" + jsonstr + ",错误信息：" + ex, summary: "支付宝官网通知接口", channelId: pid);
                Response.Write("fail");
            }
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            return sArray;
        }

    }
}