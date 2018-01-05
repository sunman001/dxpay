using DxPay.LogManager.LogFactory.ApiLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMALI
{
    /// <summary>
    /// 现在支付接口通知程序
    /// </summary>
    public partial class XZ : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string jsonstr = "";
            int pid = 0;
            jsonstr = JMP.TOOL.UrlStr.PostInput(HttpContext.Current, "现在通知接口");//获取通知参数（post数据流）
            jsonstr = HttpUtility.UrlDecode(jsonstr);
            try
            {
                string json = jsonstr.Replace("=", "\":\"").Replace("&", "\",\"");
                json = "{\"" + json + "\"}";
                Dictionary<string, string> jsonlist = JMP.TOOL.JsonHelper.Deserialize<Dictionary<string, string>>(json);
                pid = !string.IsNullOrEmpty(Request.QueryString["pid"]) ? int.Parse(Request.QueryString["pid"].ToString()) : 0;
                if (pid > 0 && jsonlist.Count > 0 && jsonlist["transStatus"] == "A001")
                {
                    //获取通道key值
                    JMALI.notice.notice notic = new notice.notice();
                    string key = notic.SelectKey(pid);
                    // string key = "1FZMAlAplOTamX6OARDVV8hrswhbGEVg";
                    Dictionary<string, string> list = jsonlist.Where(x => x.Key != "signature").ToDictionary(x => x.Key, x => x.Value);
                    string signstr = JMP.TOOL.UrlStr.AzGetStr(list) + "&" + JMP.TOOL.MD5.md5strGet(key, true).ToLower();
                    string sign = JMP.TOOL.MD5.md5strGet(signstr, true).ToLower();
                    if (sign == jsonlist["signature"])
                    {
                        //交易金额(单位：分)
                        DateTime gmt_payment = DateTime.ParseExact(jsonlist["payTime"].ToString(), "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                        decimal o_price = decimal.Parse((decimal.Parse(jsonlist["oriMhtOrderAmt"]) / 100).ToString("f2"));//支付金额（单位分转换成元）
                        string message =notic.PubNotice(jsonlist["mhtOrderNo"], o_price, gmt_payment, jsonlist["nowPayOrderNo"], jsonlist["channelOrderNo"], pid, "现在通知接口", jsonstr);
                        if (message == "ok")
                        {
                            Response.Write("success=Y");
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
                PayApiDetailErrorLogger.UpstreamNotifyErrorLog("获取到的参数：" + jsonstr + ",错误信息：" + ex, summary: "现在通知接口", channelId: pid);
                Response.Write("fail");
            }
        }
    }
}