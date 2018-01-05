using JMP.TOOL;
using Pay.DinPay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DxPay.LogManager.LogFactory.ApiLog;

namespace JMALI
{
    public partial class ZFNOTICE : System.Web.UI.Page
    {
        /// <summary>
        /// 智付通知接口程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string jsonstr = "";
            int pid = 0;
            Dictionary<string, string> jsonlist = UrlStr.GetRequestfrom(HttpContext.Current, "智付通知接口");
            if (jsonlist != null)
            {
                jsonstr = JsonHelper.DictJsonstr(jsonlist);//把获取的参数转换成字符串
            }
            try
            {
                pid = !string.IsNullOrEmpty(Request.QueryString["pid"]) ? int.Parse(Request.QueryString["pid"]) : 0;
                var callbackVerify = new CallbackVerify();
                var form = Request.Form.AllKeys.ToDictionary(key => key, key => Request.Form[key]);
                if (pid > 0 && jsonlist.Count > 0)
                {
                    //商户订单号
                    string out_trade_no = jsonlist["order_no"].Trim();
                    //智付交易号
                    string trade_no = jsonlist["trade_no"].Trim();
                    //交易状态
                    string trade_status = jsonlist["trade_status"].Trim();
                    //买家账号
                    string buyer_email = jsonlist["merchant_code"].Trim();
                    //买家付款时间
                    DateTime gmt_payment = Convert.ToDateTime(jsonlist["trade_time"].Trim());
                    //实际支付金额
                    decimal o_price = decimal.Parse(jsonlist["order_amount"].Trim());
                    if (callbackVerify.Verify(form) && jsonlist["trade_status"].Trim() == "SUCCESS")
                    {
                        JMALI.notice.notice notic = new notice.notice();
                        string message = notic.PubNotice(out_trade_no, o_price, gmt_payment, trade_no, buyer_email, pid,"智付通知接口", jsonstr);
                        if (message == "ok")
                        {
                            Response.Write("SUCCESS");
                        }
                        else
                        {
                            Response.Write("FAILED");
                        }
                    }
                    else
                    {
                        Response.Write("FAILED");
                    }
                }
                else
                {
                    Response.Write("FAILED");
                }
            }
            catch (Exception ex)
            {
                PayApiDetailErrorLogger.UpstreamNotifyErrorLog("获取到的参数：" + jsonstr + ",错误信息：" + ex, summary: "智付通知接口错误", channelId: pid);
                Response.Write("FAILED");
            }

        }
    }
}