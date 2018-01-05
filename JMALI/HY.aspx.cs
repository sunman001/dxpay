using JMP.TOOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DxPay.LogManager.LogFactory.ApiLog;

namespace JMALI
{
    public partial class HY : System.Web.UI.Page
    {
        /// <summary>
        /// 汇元通知接口程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            int pid = 0;
            string jsonstr = "";
            Dictionary<string, string> jsonlist = JMP.TOOL.UrlStr.GetRequestGet(HttpContext.Current, "汇元通知接口");
            if (jsonlist != null)
            {
                jsonstr = JMP.TOOL.JsonHelper.DictJsonstr(jsonlist);//把获取的参数转换成字符串
            }
            try
            {
                pid = !string.IsNullOrEmpty(Request.QueryString["pid"]) ? int.Parse(Request.QueryString["pid"].ToString()) : 0;
                if (pid > 0 && jsonlist.Count > 0)
                {
                    //订单编号
                    string userpara = jsonlist["agent_bill_id"].ToString().Trim();
                    //签名
                    string sign = jsonlist["sign"].ToString().Trim();
                    //第三方流水号                         
                    string trade_no = jsonlist["jnet_bill_no"].ToString();
                    //交易状态
                    string trade_status = jsonlist["result"].ToString();
                    //买家账号
                    string buyer_email = trade_no;
                    //买家付款时间
                    DateTime gmt_payment = DateTime.Now;
                    //交易金额(单位：元)
                    decimal o_price = decimal.Parse(Request.QueryString["pay_amt"]);
                    var zf = new JMP.BLL.jmp_interface();
                    JMALI.notice.notice notic =new notice.notice();
                    string key = notic.SelectKey(pid);//获取通道key值
                    string md5 = "result=" + trade_status + "&agent_id=" + jsonlist["agent_id"] + "&jnet_bill_no=" + trade_no + "&agent_bill_id=" + userpara + "&pay_type=" + Request.QueryString["pay_type"] + "&pay_amt=" + jsonlist["pay_amt"] + "&remark=" + jsonlist["remark"] + "&key=" + key;
                    string md5str = JMP.TOOL.MD5.md5strGet(md5, true).ToLower();
                    if (md5str == sign && trade_status == "1")
                    {
                        string message = notic.PubNotice(userpara, o_price, gmt_payment, trade_no, buyer_email, pid, "汇元通知接口", jsonstr);
                        if (message == "ok")
                        {
                            Response.Write("ok");
                        }
                        else
                        {
                            Response.Write("error");
                        }
                    }
                    else
                    {
                        Response.Write("error");
                    }
                }
                else
                {
                    Response.Write("error");
                }
            }
            catch (Exception ex)
            {
                PayApiDetailErrorLogger.UpstreamNotifyErrorLog("获取到的参数：" + jsonstr + ",错误信息：" + ex, summary: "汇元通知接口", channelId: pid);
                Response.Write("error");
            }
        }
    }
}