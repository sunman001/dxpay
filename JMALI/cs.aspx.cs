using Alipay;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using DxPay.LogManager.LogFactory.ApiLog;
namespace JMALI
{
    public partial class cs : System.Web.UI.Page
    {
        public string massage = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string url = ConfigurationManager.AppSettings["RequestAddress"].ToString();//请求地址
                string appkey = "";//应用key
                string appid = "";//商品id
                string orderidbh = DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random().Next(111111, 666666).ToString(); //订单号
                int sel = string.IsNullOrEmpty(Request["sel"]) ? 0 : int.Parse(Request["sel"]);//支付类型
                int Pattern = string.IsNullOrEmpty(Request["Pattern"]) ? 0 : int.Parse(Request["Pattern"]);//测试模式
                string o_price = Request["o_price"];//价格
                string timestamp = JMP.TOOL.WeekDateTime.GetMilis;//
                decimal price = !string.IsNullOrEmpty(o_price) ? decimal.Parse(o_price) : decimal.Parse("0.01");//价格


                if (Pattern == 1)
                {
                    appkey = ConfigurationManager.AppSettings["appkey"].ToString();//应用key(商务直客模式)
                    appid = ConfigurationManager.AppSettings["appid"].ToString();//商品id（商务直客模式）
                }
                else if (Pattern == 2)
                {
                    appkey = ConfigurationManager.AppSettings["agentappkey"].ToString();//应用key(代理商模式)
                    appid = ConfigurationManager.AppSettings["agentappid"].ToString();//商品id（代理商模式）
                }
                else if (Pattern == 3)
                {
                    appkey = string.IsNullOrEmpty(Request["appkey"]) ? "" : Request["appkey"].Trim();
                    appid = string.IsNullOrEmpty(Request["appid"]) ? "" : Request["appid"].Trim();
                }
                if (sel == 5)
                {
                    appkey = ConfigurationManager.AppSettings["azappkey"].ToString();//应用key(安卓模式)
                    appid = ConfigurationManager.AppSettings["azappid"].ToString();//商品id（安卓模式）
                }
                string num = price + orderidbh + timestamp + appkey;
                string sign = JMP.TOOL.MD5.md5strGet(num, true).ToUpper();
                if (price > 0 && sel > 0)
                {
                    if (sel == 9)
                    {
                        sel = 0;
                    }
                    Dictionary<string, string> list = new Dictionary<string, string>();
                    list.Add("bizcode", orderidbh);
                    list.Add("appid", appid);
                    list.Add("paytype", sel.ToString());
                    list.Add("goodsname", "测试商品");
                    list.Add("price", price.ToString());
                    list.Add("privateinfo", "测试dome");
                    list.Add("timestamp", timestamp);
                    list.Add("termkey", "1234567890");
                    list.Add("sign", sign);
                    url = url + "?" + JMP.TOOL.UrlStr.getstr(list);
                    if (sel == 0)
                    {
                        //  retjson = new { ErrorCode = 100, Message = "成功", ExtraData = url };
                        Response.Redirect(url);
                    }
                    else
                    {
                        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);    //创建一个请求示例
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();  //获取响应，即发送请求
                        Stream responseStream = response.GetResponseStream();
                        StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                        string html = streamReader.ReadToEnd();
                        //Dictionary<string, object> jsonstr = JMP.TOOL.JsonHelper.DataRowFromJSON(html);
                        mes json = JMP.TOOL.JsonHelper.Deserializes<mes>(html);
                        if (json.ErrorCode == 100 && !String.IsNullOrEmpty(json.ExtraData.ToString()))
                        {
                            if (sel == 6 || sel == 7)
                            {
                                string exda = ((Dictionary<string, object>)json.ExtraData)["ImgQRcode"].ToString();
                                Response.Redirect(exda);
                            }
                            else
                            {
                                if (json.ExtraData.ToString().StartsWith("http://") || json.ExtraData.ToString().StartsWith("https://") || (sel == 4 && json.ExtraData.ToString().StartsWith("weixin://")))
                                {
                                    Response.Redirect(json.ExtraData.ToString());
                                }
                                else
                                {
                                    massage = html;
                                }
                            }

                        }
                        else
                        {
                            massage = html;
                        }
                    }
                }
                else
                {
                    massage = "无效参数";
                }
            }
        }
    }

    internal class mes
    {
        /// <summary>
        /// 提示编码
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// 提示语
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 成功后返回的代码
        /// </summary>
        public object ExtraData { get; set; }


    }
}