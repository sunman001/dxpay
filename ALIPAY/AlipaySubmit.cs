using System.Web;
using System.Text;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Xml;

namespace Alipay
{
    public class Submit
    {
        #region 字段
        //支付宝网关地址（新）
        private string GATEWAY_NEW = "https://mapi.alipay.com/gateway.do?";
        //商户的私钥
        private string _private_key = "";
        //编码格式
        private string _input_charset = "";
        //签名方式
        private string _sign_type = "";

        private string _partner = "";
        #endregion

        public Submit(int tid, int appid)
        {
            Config cfg = new Config(tid, appid);
            _partner = cfg.partner;
            _private_key = cfg.private_key;
            _input_charset = cfg.input_charset.Trim().ToLower();
            _sign_type = cfg.sign_type.Trim().ToUpper();
        }

        /// <summary>
        /// 用于检测通道状态
        /// </summary>
        /// <param name="tid"></param>
        public Submit(int tid, ConfigMonitor cfg)
        {
            //var cfg = new ConfigMonitor(tid);
            _partner = cfg.partner;
            _private_key = cfg.private_key;
            _input_charset = cfg.input_charset.Trim().ToLower();
            _sign_type = cfg.sign_type.Trim().ToUpper();
        }

        /// <summary>
        /// 生成请求时的签名
        /// </summary>
        /// <param name="sPara">请求给支付宝的参数数组</param>
        /// <returns>签名结果</returns>
        private string BuildRequestMysign(Dictionary<string, string> sPara)
        {
            //把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
            string prestr = Core.CreateLinkString(sPara);

            //把最终的字符串签名，获得签名结果
            string mysign = "";
            switch (_sign_type)
            {
                case "RSA":
                    mysign = RSAFromPkcs8.sign(prestr, _private_key, _input_charset);
                    break;
                default:
                    mysign = "";
                    break;
            }

            return mysign;
        }

        /// <summary>
        /// 生成要请求给支付宝的参数数组
        /// </summary>
        /// <param name="sParaTemp">请求前的参数数组</param>
        /// <returns>要请求的参数数组</returns>
        private Dictionary<string, string> BuildRequestPara(SortedDictionary<string, string> sParaTemp)
        {
            //待签名请求参数数组
            Dictionary<string, string> sPara = new Dictionary<string, string>();
            //签名结果
            string mysign = "";

            //过滤签名参数数组
            sPara = Core.FilterPara(sParaTemp);

            //获得签名结果
            mysign = BuildRequestMysign(sPara);

            //签名结果与签名方式加入请求提交参数组中
            sPara.Add("sign", mysign);
            sPara.Add("sign_type", _sign_type);

            return sPara;
        }

        private Dictionary<string, string> BuildRequestParapp(SortedDictionary<string, string> sParaTemp)
        {
            //待签名请求参数数组
            Dictionary<string, string> sPara = new Dictionary<string, string>();
            //签名结果
            string mysign = "";

            //过滤签名参数数组
            sPara = Core.FilterPara(sParaTemp);

            //获得签名结果
            mysign = BuildRequestMysign(sPara);

            //签名结果与签名方式加入请求提交参数组中
            sPara.Add("sign", HttpUtility.UrlEncode(mysign));
            sPara.Add("sign_type", _sign_type);

            return sPara;
        }

        /// <summary>
        /// 生成要请求给支付宝的参数数组
        /// </summary>
        /// <param name="sParaTemp">请求前的参数数组</param>
        /// <param name="code">字符编码</param>
        /// <returns>要请求的参数数组字符串</returns>
        private string BuildRequestParaToString(SortedDictionary<string, string> sParaTemp, Encoding code)
        {
            //待签名请求参数数组
            Dictionary<string, string> sPara = new Dictionary<string, string>();
            sPara = BuildRequestPara(sParaTemp);

            //把参数组中所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串，并对参数值做urlencode
            string strRequestData = Core.CreateLinkStringUrlencode(sPara, code);

            return strRequestData;
        }

        /// <summary>
        /// 建立请求，以表单HTML形式构造（默认）
        /// </summary>
        /// <param name="sParaTemp">请求参数数组</param>
        /// <param name="strMethod">提交方式。两个值可选：post、get</param>
        /// <param name="strButtonValue">确认按钮显示文字</param>
        /// <returns>提交表单HTML文本</returns>
        public string BuildRequest(SortedDictionary<string, string> sParaTemp, string strMethod, string strButtonValue)
        {
            //待请求参数数组
            Dictionary<string, string> dicPara = new Dictionary<string, string>();
            dicPara = BuildRequestPara(sParaTemp);

            StringBuilder sbHtml = new StringBuilder();

            sbHtml.Append("<form id='alipaysubmit' name='alipaysubmit' action='" + GATEWAY_NEW + "_input_charset=" + _input_charset + "' method='" + strMethod.ToLower().Trim() + "'>");

            foreach (KeyValuePair<string, string> temp in dicPara)
            {
                sbHtml.Append("<input type='hidden' name='" + temp.Key + "' value='" + temp.Value + "'/>");
            }

            //submit按钮控件请不要含有name属性
            sbHtml.Append("<input type='submit' value='" + strButtonValue + "' style='display:none;'></form>");

            sbHtml.Append("<script>document.forms['alipaysubmit'].submit();</script>");

            return sbHtml.ToString();
        }
        /// <summary>
        /// 已http get请求方式
        /// </summary>
        /// <param name="sParaTemp"></param>
        /// <returns></returns>
        public string BuildRequestHttp(SortedDictionary<string, string> sParaTemp)
        {
            string httpurl = string.Empty;
            //待请求参数数组
            Dictionary<string, string> dicPara = new Dictionary<string, string>();
            dicPara = BuildRequestPara(sParaTemp);
            httpurl = GATEWAY_NEW;
            foreach (KeyValuePair<string, string> temp in dicPara)
            {
                if (temp.Key == "sign")
                {
                    httpurl = httpurl + temp.Key + "=" + HttpUtility.UrlEncode(temp.Value) + "&";
                }
                else
                {
                    httpurl = httpurl + temp.Key + "=" + temp.Value + "&";
                }
            }
            httpurl = httpurl.Substring(0, httpurl.Length-1);
            return httpurl;

        }

        /// <summary>
        /// 建立请求，URL连接参数输出(扩展)
        /// </summary>
        /// <param name="sParaTemp">请求参数数组</param>
        /// <returns>提交表单HTML文本</returns>
        public string BuildRequestParameters(SortedDictionary<string, string> sParaTemp)
        {
            //待请求参数数组
            var dicPara = BuildRequestPara(sParaTemp);

            var lst = dicPara.Select(temp => string.Format("{0}={1}", temp.Key, temp.Value)).ToList();

            //var result = HttpPost(GATEWAY_NEW + "_input_charset=" + _input_charset, string.Join("&", lst));
            var result = Post(GATEWAY_NEW + "_input_charset=" + _input_charset, dicPara);

            return result;
        }


        private string Post(string url, Dictionary<string, string> dict)
        {

            using (var wb = new WebClient())
            {
                wb.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/27.0.1453.94 Safari/537.36");
                var data = new NameValueCollection();
                foreach (var d in dict)
                {
                    data[d.Key] = d.Value;
                }
                //data["username"] = "myUser";
                //data["password"] = "myPassword";

                var response = wb.UploadValues(url, "POST", data);
                var result = Encoding.UTF8.GetString(response);
                return result;
            }
        }

        /// <summary>
        /// HTTP提交表单(POST)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private string HttpPost(string url, string parameters)
        {
            var req = WebRequest.Create(url);
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            var bytes = Encoding.ASCII.GetBytes(parameters);
            req.ContentLength = bytes.Length;
            var os = req.GetRequestStream();
            os.Write(bytes, 0, bytes.Length);
            os.Close();
            var resp = req.GetResponse();
            var sr = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding("gb2312"));

            var result = sr.ReadToEnd().Trim();
            sr.Close();
            return result;
        }

        /// <summary>
        /// 建立请求，以安卓形式构造
        /// </summary>
        /// <param name="sParaTemp">请求参数数组</param>
        /// <param name="strMethod">提交方式。两个值可选：post、get</param>
        /// <param name="strButtonValue">确认按钮显示文字</param>
        /// <returns>提交表单HTML文本</returns>
        public string BuildRequest1(SortedDictionary<string, string> sParaTemp)
        {
            //待请求参数数组
            Dictionary<string, string> dicPara = new Dictionary<string, string>();
            dicPara = BuildRequestParapp(sParaTemp);

            StringBuilder sbHtml = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in dicPara)
            {
                sbHtml.Append("&" + temp.Key + "=" + temp.Value);
            }
            return sbHtml.ToString().Substring(1);
        }

        /// <summary>
        /// 用于防钓鱼，调用接口query_timestamp来获取时间戳的处理函数
        /// 注意：远程解析XML出错，与IIS服务器配置有关
        /// </summary>
        /// <returns>时间戳字符串</returns>
        public string Query_timestamp()
        {
            string url = GATEWAY_NEW + "service=query_timestamp&partner=" + _partner + "&_input_charset=" + _input_charset;
            string encrypt_key = "";

            XmlTextReader Reader = new XmlTextReader(url);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Reader);

            encrypt_key = xmlDoc.SelectSingleNode("/alipay/response/timestamp/encrypt_key").InnerText;

            return encrypt_key;
        }
    }
}