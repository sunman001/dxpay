using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DxPay.LogManager.LogFactory.ApiLog;
using System.Xml.Serialization;
using JMP.TOOL;

namespace JMALI
{
    public partial class PF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)

        {
            string jsonstr = "";
            int pid = 0;
            StreamReader reader = new StreamReader(Request.InputStream);
            String xmlData = reader.ReadToEnd();//获取xml数据
            PAY_NODIFY obj = new PAY_NODIFY();
            jsonstr = xmlData.ToString();
            try
            {
                pid = !string.IsNullOrEmpty(Request.QueryString["pid"]) ? int.Parse(Request.QueryString["pid"].ToString()) : 0;
                obj = xmlhelper.Deserialize<PAY_NODIFY>(xmlData);
            }
            catch
            {
                PayApiDetailErrorLogger.UpstreamNotifyErrorLog("解析xml格式出错,获取到的xml数据：" + xmlData.ToString(), summary: "浦发银行通知接口错误", channelId: pid);

                obj = JsonHelper.Deserializes<PAY_NODIFY>(xmlData.Replace(" ", ""));
            }
            try
            {

                string signstr = string.IsNullOrEmpty(Request["sign"]) ? "" : Request["sign"];
                
                if (obj != null && !string.IsNullOrEmpty(signstr) && pid > 0)
                {
                    if (obj.STATE == "1")
                    {
                        JMALI.notice.notice notic = new notice.notice();
                        string key = notic.SelectKey(pid);//获取通道key值

                        decimal price = decimal.Parse((decimal.Parse(obj.AMT) / 100).ToString());//支付金额（单位分转换成元）
                        string mch_id = obj.BUSI_ID;//商户号（浦发账号）
                        string code = obj.CHARGE_CODE;//商户订单号（我们平台编号）
                        string timestamp = signstr.Split(':')[1];
                        string signs = timestamp + key + xmlData.Replace(" ", "").Replace("\t", "").Replace("\r\n", "").Replace("\n", "").Replace("\\", "");
                        string sign = JMP.TOOL.MD5.md5strGet(signs, true).ToLower() + ":" + timestamp;
                        if (signstr == sign)
                        {
                            DateTime gmt_payment = DateTime.Now;//交易时间
                            string message =notic.PubNotice(code, price, gmt_payment, obj.CHARGE_DOWN_CODE, obj.CHARGE_DOWN_CODE, pid,"浦发支付宝通知接口", jsonstr);
                            if (message == "ok")
                            {
                                Response.StatusCode = 200;
                                Response.Write("200");
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
                PayApiDetailErrorLogger.UpstreamNotifyErrorLog("获取到的参数：" + jsonstr + ",错误信息：" + ex, summary: "浦发支付宝通知接口", channelId: pid);
                Response.Write("fail");
            }
        }


        [XmlRoot(ElementName = "TradeFundBill")]
        public class TradeFundBill
        {
            [XmlElement(ElementName = "amount")]
            public string Amount { get; set; }
            [XmlElement(ElementName = "fund_channel")]
            public string Fund_channel { get; set; }
        }

        [XmlRoot(ElementName = "fund_bill_list")]
        public class Fund_bill_list
        {
            [XmlElement(ElementName = "TradeFundBill")]
            public TradeFundBill TradeFundBill { get; set; }
        }

        [XmlRoot(ElementName = "ali")]
        public class Ali
        {
            [XmlElement(ElementName = "buyer_logon_id")]
            public string Buyer_logon_id { get; set; }
            [XmlElement(ElementName = "buyer_user_id")]
            public string Buyer_user_id { get; set; }
            [XmlElement(ElementName = "fund_bill_list")]
            public Fund_bill_list Fund_bill_list { get; set; }
            [XmlElement(ElementName = "out_trade_no")]
            public string Out_trade_no { get; set; }
            [XmlElement(ElementName = "total_fee")]
            public string Total_fee { get; set; }
            [XmlElement(ElementName = "trade_no")]
            public string Trade_no { get; set; }
            [XmlElement(ElementName = "trade_status")]
            public string Trade_status { get; set; }
        }

        [XmlRoot(ElementName = "TRADEFUNDBILL")]
        public class TRADEFUNDBILL
        {
            [XmlElement(ElementName = "AMOUNT")]
            public string AMOUNT { get; set; }
            [XmlElement(ElementName = "FUND_CHANNEL")]
            public string FUND_CHANNEL { get; set; }
        }

        [XmlRoot(ElementName = "FUND_BILL_LIST")]
        public class FUND_BILL_LIST
        {
            [XmlElement(ElementName = "TRADEFUNDBILL")]
            public TRADEFUNDBILL TRADEFUNDBILL { get; set; }
        }

        [XmlRoot(ElementName = "PAY_NODIFY")]
        public class PAY_NODIFY
        {
            [XmlElement(ElementName = "AMT")]
            public string AMT { get; set; }
            [XmlElement(ElementName = "ali")]
            public Ali Ali { get; set; }
            [XmlElement(ElementName = "BEGIN_TIME")]
            public string BEGIN_TIME { get; set; }
            [XmlElement(ElementName = "BUSI_ID")]
            public string BUSI_ID { get; set; }
            [XmlElement(ElementName = "buyerId")]
            public string BuyerId { get; set; }
            [XmlElement(ElementName = "CHANNEL_TYPE")]
            public string CHANNEL_TYPE { get; set; }
            [XmlElement(ElementName = "CHARGE_CODE")]
            public string CHARGE_CODE { get; set; }
            [XmlElement(ElementName = "CHARGE_DOWN_CODE")]
            public string CHARGE_DOWN_CODE { get; set; }
            [XmlElement(ElementName = "CHARGE_THIRD_CODE")]
            public string CHARGE_THIRD_CODE { get; set; }
            [XmlElement(ElementName = "DEV_ID")]
            public string DEV_ID { get; set; }
            [XmlElement(ElementName = "END_TIME")]
            public string END_TIME { get; set; }
            [XmlElement(ElementName = "FUND_BILL_LIST")]
            public FUND_BILL_LIST FUND_BILL_LIST { get; set; }
            [XmlElement(ElementName = "OPER_ID")]
            public string OPER_ID { get; set; }
            [XmlElement(ElementName = "PAY_SUBJECT")]
            public string PAY_SUBJECT { get; set; }
            [XmlElement(ElementName = "STATE")]
            public string STATE { get; set; }
        }

    }


}