using System;
using DxPay.LogManager.LogFactory.ApiLog;
using System.Xml;
using System.Net;
using System.Text;
using System.Configuration;
using System.Collections.Generic;
using System.IO;

namespace JMALI
{
    public partial class demo : System.Web.UI.Page
    {

        public string ShowTimestamp = "";
        public string ShowSignstr = "";
        public string ShowSign = "";
        public string ShowUrl = "";
        public string ShowMassage = "";
        public string ShowParameter = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string bizcode = string.IsNullOrEmpty(Request["bizcode"]) ? "" : Request["bizcode"];//商户订单号
                int appid = string.IsNullOrEmpty(Request["appid"]) ? 0 : int.Parse(Request["appid"]);//应用编号
                string address = string.IsNullOrEmpty(Request["address"]) ? "" : Request["address"];//通知地址
                string showaddress = string.IsNullOrEmpty(Request["showaddress"]) ? "" : Request["showaddress"];//H5同步通知地址
                int paytype = string.IsNullOrEmpty(Request["paytype"]) ? 9 : int.Parse(Request["paytype"]);//支付类型
                string goodsname = string.IsNullOrEmpty(Request["goodsname"]) ? "" : Request["goodsname"];//商品名称
                decimal price = string.IsNullOrEmpty(Request["price"]) ? 0 : decimal.Parse(Request["price"]);//商品价格	
                string privateinfo = string.IsNullOrEmpty(Request["privateinfo"]) ? "" : Request["privateinfo"];//商户私有信息
                string appkey = string.IsNullOrEmpty(Request["appkey"]) ? "" : Request["appkey"];//appkey
                string termkey = string.IsNullOrEmpty(Request["termkey"]) ? "" : Request["termkey"];//termkey

                if (bizcode == "" || appid == 0 || goodsname == "" || price == 0 || appkey == "")
                {
                    ShowMassage = "请完善必填参数项";
                }
                else
                {
                    string timestamp = JMP.TOOL.WeekDateTime.GetMilis;//时间戳
                    ShowTimestamp = timestamp;

                    string url = ConfigurationManager.AppSettings["RequestAddress"].ToString();//请求地址

                    string signstr = price + bizcode + timestamp + appkey;//要签名的字符串
                    ShowSignstr = signstr;
                    string sign = JMP.TOOL.MD5.md5strGet(signstr, true).ToUpper();//生成的签名
                    ShowSign = sign;

                    ShowParameter = "<span style='color:red'>bizcode:</span>" + bizcode + "，<span style='color:red'>appid:</span>" + appid + "，<span style='color:red'>address:</span>" + address + "，<span style='color:red'>showaddress:</span>" + showaddress + "，<span style='color:red'>paytype:</span>" + paytype + "，<span style='color:red'>goodsname:</span>" + goodsname + "，<span style='color:red'>price:</span>" + price + "，<span style='color:red'>privateinfo:</span>" + privateinfo + "，<span style='color:red'>appkey:</span>" + appkey + "，<span style='color:red'>termkey:</span>" + termkey + "，<span style='color:red'>timestamp:</span>" + timestamp;

                    Dictionary<string, string> list = new Dictionary<string, string>();

                    list.Add("bizcode", bizcode);
                    list.Add("appid", appid.ToString());
                    list.Add("address", address);
                    list.Add("showaddress", showaddress);
                    list.Add("paytype", paytype.ToString());
                    list.Add("goodsname", goodsname);
                    list.Add("price", price.ToString());
                    list.Add("privateinfo", privateinfo);
                    list.Add("timestamp", timestamp);
                    list.Add("sign", sign);
                    list.Add("termkey", termkey);
                    //请求url
                    url = url + "?" + JMP.TOOL.UrlStr.getstr(list);
                    ShowUrl = url.Replace("&times", "&amp;times");


                }
            }
            else
            {
                ShowMassage = "参数无效";
            }
        }

    }
}