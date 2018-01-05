using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using swiftpass.utils;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Xml;
using System.Text;
using JMP.TOOL;
using DxPay.LogManager.LogFactory.ApiLog;

namespace JMALI
{
    public partial class WFTNOTICE : System.Web.UI.Page
    {
        private ClientResponseHandler resHandler = new ClientResponseHandler();
        private Dictionary<string, string> cfg = new Dictionary<string, string>(1);
        /// <summary>
        /// 威富通通知程序接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.RequestType == "POST")
            {
                int pid = int.Parse(Request.QueryString["pid"].ToString());
                callback(pid);
            }
            else
            {
                Response.Write("非法访问！");
            }

        }
        public void callback(int pid)
        {
            //加载配置数据
            this.cfg = Utils.Load_CfgInterfaceId(pid);
            //初始化数据
            using (StreamReader sr = new StreamReader(Request.InputStream))
            {
                this.resHandler.setContent(sr.ReadToEnd());
                this.resHandler.setKey(this.cfg["key"]);

                Hashtable resParam = this.resHandler.getAllParameters();
                if (this.resHandler.isTenpaySign())
                {
                    if (int.Parse(resParam["status"].ToString()) == 0 && int.Parse(resParam["result_code"].ToString()) == 0)
                    {
                        //Utils.writeFile("接口回调", resParam); //通知返回参数写入result.txt文本文件。

                        //商户订单号

                        string out_trade_no = resParam["out_trade_no"].ToString();

                        //微付通交易号

                        string trade_no = resParam["transaction_id"].ToString();

                        //买家账号
                        string buyer_email = resParam["out_transaction_id"].ToString();

                        //买家付款时间
                        DateTime gmt_payment = DateTime.ParseExact(resParam["time_end"].ToString(), "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                        //付款金额(单位分转换成元)
                        JMALI.notice.notice notic = new notice.notice();
                        decimal o_price = decimal.Parse((decimal.Parse(resParam["total_fee"].ToString()) / 100).ToString("f2"));
                        string message =notic.PubNotice(out_trade_no, o_price, gmt_payment, trade_no, buyer_email, pid, "威富通通知程序接口", JMP.TOOL.JsonHelper.Serialize(resParam));
                        if (message == "ok")
                        {
                            Response.Write("success");
                        }
                        else
                        {
                            Response.Write("fail");
                        }
                    }
                }
                else
                {
                    PayApiDetailErrorLogger.UpstreamNotifyErrorLog("获取到的参数：" + resParam, summary: "威富通通知接口错误", channelId: pid);
                    Response.Write("fail");
                }

            }
        }
    }
}