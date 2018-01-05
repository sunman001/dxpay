using JMP.TOOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Globalization;
using DxPay.LogManager.LogFactory.ApiLog;

namespace JMALI
{
    public partial class NY : System.Web.UI.Page
    {
        /// <summary>
        /// 南粤异步通知接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string jsonstr = "";
            int pid = 0;
            Dictionary<string, string> jsonlist = JMP.TOOL.UrlStr.GetRequestJson(HttpContext.Current, "南粤通知接口");
            if (jsonlist != null)
            {
                jsonstr = JMP.TOOL.JsonHelper.DictJsonstr(jsonlist);//把获取的参数转换成字符串
            }
            try
            {
                pid = !string.IsNullOrEmpty(Request.QueryString["pid"]) ? int.Parse(Request.QueryString["pid"].ToString()) : 0;
                if (pid > 0 && jsonlist.Count > 0)
                {
                    //买家付款时间
                    DateTime gmt_payment = DateTime.ParseExact(jsonlist["transTime"].ToString(), "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                    //交易金额(单位：元)
                    decimal o_price = decimal.Parse(jsonlist["amount"]);
                    //获取通道key值
                    JMALI.notice.notice notic =new notice.notice();
                    string key = notic.SelectKey(pid);
                    //组装签名
                    Dictionary<string, string> list = jsonlist.Where(x => x.Key != "sign").ToDictionary(x => x.Key, x => x.Value);
                    string md5 = JMP.TOOL.UrlStr.AzGetStr(list) + "&key=" + key;
                    string md5str = JMP.TOOL.MD5.md5strGet(md5, true);
                    if (md5str == jsonlist["sign"] && jsonlist["status"] == "02")
                    {
                        string message = notic.PubNotice(jsonlist["outTradeNo"], o_price, gmt_payment, jsonlist["outChannelNo"], jsonlist["outChannelNo"],pid, "南粤通知接口", jsonstr);
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
                PayApiDetailErrorLogger.UpstreamNotifyErrorLog("获取到的参数：" + jsonstr + ",错误信息：" + ex, summary: "南粤通知接口错误", channelId: pid);
                Response.Write("fail");
            }
        }
    }
}