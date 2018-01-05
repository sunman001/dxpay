using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DxPay.LogManager.LogFactory.ApiLog;
using System.Configuration;

namespace JMALI
{
    public partial class Wp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string jsonstr = "";
            int pdpid = 0;
            Dictionary<string, string> jsonlist = JMP.TOOL.UrlStr.GetRequestfrom(HttpContext.Current, "微派支付通知接口");
            if (jsonlist != null)
            {
                jsonstr = JMP.TOOL.JsonHelper.DictJsonstr(jsonlist);//把获取的参数转换成字符串
            }
            try
            {

                int pid = !string.IsNullOrEmpty(Request.QueryString["pid"]) ? int.Parse(Request.QueryString["pid"].ToString()) : 0;
                int alpayid = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["alpayid"].ToString()) ? int.Parse(ConfigurationManager.AppSettings["alpayid"].ToString()) : 0;
                //判断
                if (jsonlist["synType"] == "wxpay")
                {
                    pdpid = pid;
                }
                else
                {
                    pdpid = alpayid;
                }
                if (pdpid > 0)
                {
                    //获取通道key值
                    JMALI.notice.notice notic = new notice.notice();
                    string key = notic.SelectKey(pdpid);
                    Dictionary<string, string> dic = jsonlist.Where(x => x.Key != "sign").ToDictionary(x => x.Key, s => s.Value);
                    //签名
                    string md5 = JMP.TOOL.UrlStr.AzGetStr(dic) + key;
                    string signstr = JMP.TOOL.MD5.md5strGet(md5, true).ToUpper();
                    DateTime gmt_payment = DateTime.ParseExact(jsonlist["time"].ToString(), "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                    //判断
                    if (signstr == jsonlist["sign"].ToString() && jsonlist["status"].ToString() == "success")
                    {
                        string message =notic.PubNotice(jsonlist["cpparam"], decimal.Parse(jsonlist["price"]), gmt_payment, jsonlist["orderNo"], jsonlist["orderNo"], pdpid, "微派网络通知接口", jsonstr);
                        if (message == "ok")
                        {
                            Response.Write("success");
                        }
                        else
                        {
                            Response.Write("error");
                        }
                    }
                }
                else
                {
                    Response.Write("error");
                }
            }
            catch (Exception ex)
            {
                PayApiDetailErrorLogger.UpstreamNotifyErrorLog("获取到的参数：" + jsonstr + ",错误信息：" + ex, summary: "微派网络通知接口", channelId: pdpid);
                Response.Write("error");
            }
        }
    }
}