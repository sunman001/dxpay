using JMP.TOOL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DxPay.LogManager.LogFactory.ApiLog;

namespace JMALI
{
    /// <summary>
    /// 兴业银行通知接口
    /// </summary>
    public partial class xyyh : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string jsonstr = "";
            int pid = 0;
            StreamReader reader = new StreamReader(Request.InputStream);
            String xmlData = reader.ReadToEnd();//获取xml数据
            Dictionary<string, string> jsonlist = JMP.TOOL.xmlhelper.FromXmls(xmlData);
            if (jsonlist != null)
            {
                jsonstr = JMP.TOOL.JsonHelper.DictJsonstr(jsonlist);//把获取的参数转换成字符串
            }
            try
            {
                pid = !string.IsNullOrEmpty(Request.QueryString["pid"]) ? int.Parse(Request.QueryString["pid"].ToString()) : 0;
                if (jsonlist.Count > 0 && pid > 0)
                {
                    if (jsonlist["return_code"] == "SUCCESS")
                    {
                        JMALI.notice.notice notic = new notice.notice();
                        string key = notic.SelectKey(pid);//获取通道key值
                        Dictionary<string, string> list = jsonlist.Where(x => x.Key != "sign").ToDictionary(x => x.Key, x => x.Value);
                        string md5 = JMP.TOOL.UrlStr.AzGetStr(list) + "&key=" + key; ;
                        string sing = JMP.TOOL.MD5.md5strGet(md5, true);
                        if (jsonlist["sign"] == sing && jsonlist["result_code"] == "SUCCESS")
                        {
                            decimal o_price = decimal.Parse((decimal.Parse(jsonlist["total_fee"]) / 100).ToString("f2"));//支付金额（单位分转换成元）
                            DateTime gmt_payment = DateTime.ParseExact(jsonlist["time_end"], "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture); ;//交易时间
                            string message = notic.PubNotice(jsonlist["out_trade_no"], o_price, gmt_payment, jsonlist["transaction_id"], jsonlist["transaction_id"], pid, "兴业银行", jsonstr);
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
                else
                {
                    Response.Write("FAIL");
                }
            }
            catch (Exception ex)
            {
                PayApiDetailErrorLogger.UpstreamNotifyErrorLog("获取到的参数：" + jsonstr + ",错误信息：" + ex, summary: "兴业银行通知接口错误", channelId: pid);
                Response.Write("FAIL");
            }
        }
    }
}