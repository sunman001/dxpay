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
    public partial class Zq : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string jsonstr = "";
            int pid = 0;
            //var nameValue = JMP.TOOL.UrlStr.GetRequestfrom(HttpContext.Current, "掌趣支付通知接口");
            //Dictionary<string, string> jsonlist = nameValue.Cast<string>().ToDictionary(x => x, x => nameValue[x]);

            Dictionary<string, string> jsonlist = JMP.TOOL.UrlStr.GetRequestfrom(HttpContext.Current, "掌趣支付通知接口");
            if (jsonlist != null)
            {
                jsonstr = JMP.TOOL.JsonHelper.DictJsonstr(jsonlist);//把获取的参数转换成字符串
            }
            try
            {
                pid = !string.IsNullOrEmpty(Request.QueryString["pid"]) ? int.Parse(Request.QueryString["pid"].ToString()) : 0;
                if (pid > 0)
                {
                    //状态码
                    string reCode = jsonlist["reCode"];
                    //商户编号
                    string merchantNo = jsonlist["merchantNo"];
                    //商户订单号
                    string merchantOrderno = jsonlist["merchantOrderno"];
                    //订单状态
                    string result = jsonlist["result"];
                    //支付类型
                    string payType = jsonlist["payType"];
                    //商品名称
                    string memberGoods = HttpUtility.UrlDecode(jsonlist["memberGoods"]);
                    //成功金额
                    decimal amount = decimal.Parse(jsonlist["amount"]); //单位:元，保留两位小数
                    //签名数据
                    string hmac = jsonlist["hmac"];
                    //获取通道key值
                    JMALI.notice.notice notic = new notice.notice();
                    string key = notic.SelectKey(pid);
                    //组装签名
                    string md5 = reCode + merchantNo + merchantOrderno + result + payType + memberGoods + amount;
                    //签名
                    string signstr = JMP.TOOL.Digest.HmacSign(md5, key);
                    if (signstr == hmac && reCode == "1" && result == "SUCCESS")
                    {
                        DateTime time_payment = DateTime.Now;
                        string message =notic.PubNotice(merchantOrderno, amount, time_payment, merchantOrderno, merchantOrderno, pid, "掌趣支付通知接口", jsonstr);
                        if (message == "ok")
                        {
                            Response.Write("SUCCESS");
                        }
                        else
                        {
                            Response.Write("FAIL");
                        }
                    }
                    else
                    {
                        Response.Write("FAIL");
                    }

                }
                else
                {
                    Response.Write("FAIL");
                }
            }
            catch (Exception ex)
            {
                PayApiDetailErrorLogger.UpstreamNotifyErrorLog("获取到的参数：" + jsonstr + ",错误信息：" + ex, summary: "掌趣支付通知接口", channelId: pid);
                Response.Write("FAIL");
            }
        }
    }
}