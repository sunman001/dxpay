using DxPay.LogManager.LogFactory.ApiLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMALI
{
    public partial class Xxb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int pid = 0;
            StreamReader reader = new StreamReader(Request.InputStream);
            String jsonData = reader.ReadToEnd();//获取数据
            try
            {
                RootObject obj = new RootObject();
                obj = JMP.TOOL.JsonHelper.Deserialize<RootObject>(jsonData);
                pid = !string.IsNullOrEmpty(Request.QueryString["pid"]) ? int.Parse(Request.QueryString["pid"].ToString()) : 0;
                if (pid > 0)
                {
                    JMALI.notice.notice notic = new notice.notice();
                    string key = notic.SelectKey(pid);//获取通道key值
                    string json = JMP.TOOL.JsonHelper.Serialize(obj.info);
                    Dictionary<string, string> dic = JMP.TOOL.JsonHelper.DataRowJSON(json);
                    string signstr = JMP.TOOL.UrlStr.AzGetStr(dic) + "&key=" + key;
                    string signstring = JMP.TOOL.MD5.md5strGet(signstr, true).ToLower();
                    if (obj != null && signstring == obj.sign)
                    {
                        if (obj.resultCode == "20000" && obj.info.pay_status == "TRADE_SUCCESS")
                        {
                            DateTime gmt_payment = DateTime.Now;
                            string message =notic.PubNotice(obj.info.pay_order, decimal.Parse(obj.info.pay_fee), gmt_payment, obj.info.transid, obj.info.transid,pid, "小小贝支付宝wap通知接口错误", jsonData);
                            if (message == "ok")
                            {
                                Response.Write("SUCCESS");
                            }
                            else
                            {
                                Response.Write("FAILURE");
                            }
                        }
                        else
                        {
                            Response.Write("FAILURE");
                        }
                    }
                    else
                    {
                        Response.Write("FAILURE");
                    }
                }
                else
                {
                    Response.Write("FAILURE");
                    PayApiDetailErrorLogger.UpstreamNotifyErrorLog("获取到的参数Pid错误：" + pid + "小小贝支付宝wap通知接口错误", channelId: pid);
                }

            }
            catch (Exception ex)
            {
                PayApiDetailErrorLogger.UpstreamNotifyErrorLog("获取到的参数：" + jsonData + ",错误信息：" + ex, summary: "小小贝支付宝wap通知接口错误", channelId: pid);
                Response.Write("FAILURE");
            }

        }



        public class Info
        {
            public string pay_status { get; set; }
            public string pay_order { get; set; }

            public string transid { get; set; }
            public string pay_fee { get; set; }
        }

        public class RootObject
        {
            public Info info { get; set; }
            public string resultCode { get; set; }
            public string sign { get; set; }
            public string signtype { get; set; }
        }
    }
}