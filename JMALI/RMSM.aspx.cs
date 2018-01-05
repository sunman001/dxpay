using DxPay.LogManager.LogFactory.ApiLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMALI
{
    public partial class RMSM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string jsonstr = "";
            int pid = 0;
            Dictionary<string, string> jsonlist = JMP.TOOL.UrlStr.GetRequestGet(HttpContext.Current, "融梦通知接口");
            if (jsonlist != null)
            {
                jsonstr = JMP.TOOL.JsonHelper.DictJsonstr(jsonlist);//把获取的参数转换成字符串
            }
            try
            {
                pid = !string.IsNullOrEmpty(Request.QueryString["pid"]) ? int.Parse(Request.QueryString["pid"].ToString()) : 0;
                if (pid > 0 && jsonlist.Count > 0 && jsonlist["return_code"] == "0")
                {
                    //买家付款时间
                    DateTime gmt_payment = DateTime.Now;
                    //交易金额(单位：分)
                    decimal o_price = decimal.Parse((decimal.Parse(jsonlist["totalFee"]) / 100).ToString("f2"));//支付金额（单位分转换成元）
                    //获取通道key值
                    JMALI.notice.notice notic = new notice.notice();
                    string key = notic.SelecRmHdKey(pid);
                    //组装签名
                    string sign = "channelOrderId=" + jsonlist["channelOrderId"] + "&key=" + key + "&orderId=" + jsonlist["orderId"] + "&timeStamp=" + jsonlist["timeStamp"] + "&totalFee=" + jsonlist["totalFee"];
                    string md5str = JMP.TOOL.MD5.md5strGet(sign, true).ToLower();
                    if (md5str == jsonlist["sign"])
                    {
                        string message =notic.PubNotice(jsonlist["channelOrderId"], o_price, gmt_payment, jsonlist["orderId"], jsonlist["orderId"],pid,"融梦通知接口", jsonstr);
                        if (message == "ok")
                        {
                            Response.Write("SUCCESS");
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
                else
                {
                    Response.Write("fail");
                }
            }
            catch (Exception ex)
            {
                PayApiDetailErrorLogger.UpstreamNotifyErrorLog("获取到的参数：" + jsonstr + ",错误信息：" + ex, summary: "融梦通知接口", channelId: pid);
                Response.Write("fail");
            }
        }
    }
}