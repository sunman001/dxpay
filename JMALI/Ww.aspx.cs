using DxPay.LogManager.LogFactory.ApiLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMALI
{
    public partial class Ww : System.Web.UI.Page
    {
        /// <summary>
        /// 微唯网络通知接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string jsonstr = "";
            int pid = 0;
            Dictionary<string, string> jsonlist = JMP.TOOL.UrlStr.GetRequestfrom(HttpContext.Current, "微唯支付通知接口");
            if (jsonlist != null)
            {
                jsonstr = JMP.TOOL.JsonHelper.DictJsonstr(jsonlist);//把获取的参数转换成字符串
            }
            try
            {
                pid = !string.IsNullOrEmpty(Request.QueryString["pid"]) ? int.Parse(Request.QueryString["pid"].ToString()) : 0;
                if (pid > 0)
                {
                    //订单号码
                    string order_id = jsonlist["order_id"];
                    //系统订单
                    string orderNo = jsonlist["orderNo"];
                    //支付金额
                    decimal money = decimal.Parse((decimal.Parse(jsonlist["money"]) / 100).ToString());
                    //商务号
                    string mch = jsonlist["mch"];
                    //支付类型
                    string pay_type = jsonlist["pay_type"];
                    //支付状态
                    string status = jsonlist["status"];
                    //签名
                    string sign = jsonlist["sign"];
                    //时间戳
                    string time = jsonlist["time"];
                    //获取通道key值
                    JMALI.notice.notice notic = new notice.notice();
                    string key = notic.SelectKey(pid);
                    //组装签名
                    string md5 = order_id + orderNo + jsonlist["money"] + mch + pay_type + time + JMP.TOOL.MD5.md5strGet(key, true).ToLower();
                    string signstr = JMP.TOOL.MD5.md5strGet(md5, true).ToLower();
                    if (signstr == sign && status == "1")
                    {
                        string message =notic.PubNotice(order_id, money, JMP.TOOL.WeekDateTime.GetTime(time), orderNo, orderNo, pid,"微唯网络通知接口", jsonstr);
                        if (message == "ok")
                        {
                            Response.Write("SUCCESS");
                        }
                        else
                        {
                            Response.Write("ERROR");
                        }
                    }
                    else
                    {
                        Response.Write("ERROR");
                    }

                }
                else
                {
                    Response.Write("ERROR");
                }
            }
            catch (Exception ex)
            {
                PayApiDetailErrorLogger.UpstreamNotifyErrorLog("获取到的参数：" + jsonstr + ",错误信息：" + ex, summary: "微唯网络通知接口", channelId: pid);
                Response.Write("ERROR");
            }
        }
    }
}