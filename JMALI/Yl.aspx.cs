using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DxPay.LogManager.LogFactory.ApiLog;
using System.IO;

namespace JMALI
{
    public partial class Yl : System.Web.UI.Page
    {
        /// <summary>
        /// 优乐通知接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string jsonstr = "";
            int pid = 0;
            StreamReader reader = new StreamReader(Request.InputStream);
            String xmlData = reader.ReadToEnd();//获取xml数据
            Dictionary<string, string> jsonlist = new Dictionary<string, string>();
            try
            {
                pid = !string.IsNullOrEmpty(Request.QueryString["pid"]) ? int.Parse(Request.QueryString["pid"].ToString()) : 0;
                jsonlist = JMP.TOOL.xmlhelper.FromXmls(xmlData);       
            }
            catch
            {
                PayApiDetailErrorLogger.UpstreamNotifyErrorLog("解析xml格式出错,获取到的xml数据：" + xmlData.ToString(), summary: "优乐通知接口错误", channelId: pid);
                jsonlist = JMP.TOOL.xmlhelper.FromXmls(xmlData.Replace(" ", ""));
            }
            if (jsonlist != null)
            {
                jsonstr = JMP.TOOL.JsonHelper.DictJsonstr(jsonlist);//把获取的参数转换成字符串
            }
            try
            {
                
                if (jsonlist.Count > 0 && pid > 0)
                {
                    if (jsonlist["return_code"] == "SUCCESS" && jsonlist["result_code"] == "SUCCESS")
                    {
                        JMALI.notice.notice notic = new notice.notice();
                        string key = notic.SelectKey(pid);//获取通道key值

                        decimal price = decimal.Parse((decimal.Parse(jsonlist["total_fee"]) / 100).ToString());//支付金额（单位分转换成元）
                        string mch_id = jsonlist["mch_id"];//商户号（优乐账号）
                        string code = jsonlist["out_trade_no"];//商户订单号（我们平台编号）
                        string sign = JMP.TOOL.MD5.md5strGet(mch_id + code + key, true).ToUpper();//组装签名验证
                        if (jsonlist["attach"] == sign)
                        {
                            DateTime gmt_payment = DateTime.ParseExact(jsonlist["time_end"], "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture); ;//交易时间
                            string message =notic.PubNotice(code, price, gmt_payment, jsonlist["transaction_id"], jsonlist["transaction_id"], pid,"优乐支付接口", jsonstr);
                            if (message == "ok")
                            {
                                Response.Write("success");
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
                else
                {
                    Response.Write("fail");
                }
            }
            catch (Exception ex)
            {
                PayApiDetailErrorLogger.UpstreamNotifyErrorLog("获取到的参数：" + jsonstr + ",错误信息：" + ex, summary: "优乐通知接口错误", channelId: pid);
                Response.Write("fail");
            }
        }
    }
}