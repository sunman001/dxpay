using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using JMP.TOOL;

namespace TOOL.Message.AudioMessage.ChuangLan
{
    public class Util
    {
        public static string BaseUrl = ConfigurationManager.AppSettings["CHUANGLAN.AUDIO.BASE.URL"];
        public static string ResouceUrl = ConfigurationManager.AppSettings["CHUANGLAN.AUDIO.RESOURCE.URL"];
        public ResponseModel DoRequest(RequestPayload sender)
        {
            //if (sender != null)
            //{
            //    sender.callingline = HttpUtility.UrlEncode(sender.callingline);
            //    sender.company = HttpUtility.UrlEncode(sender.company);
            //    sender.contextparm = HttpUtility.UrlEncode(sender.contextparm);
            //    sender.key = HttpUtility.UrlEncode(sender.key);
            //}
            var responseModel = new ResponseModel();
            try
            {
                var audio = new ChuangLanAudioJson
                {
                    userinfo = sender
                };
                var userinfo = JsonHelper.Serialize(audio);
                var encoding = Encoding.GetEncoding("GBK");
                userinfo = HttpUtility.UrlEncode(userinfo,encoding);
                var res = BaseUrl + "/" + ResouceUrl + "?userinfo=" + userinfo;
                var myRequest = (HttpWebRequest)WebRequest.Create(res);
                myRequest.ContentType = "text/html; charset=GBK";
                var myResponse = (HttpWebResponse)myRequest.GetResponse();
                responseModel.Status = myResponse.StatusCode;

                if (myResponse.StatusCode == HttpStatusCode.OK)
                {
                    var reader = new StreamReader(myResponse.GetResponseStream(), Encoding.GetEncoding("GBK"));
                    responseModel = JsonHelper.Deserialize<ResponseModel>(reader.ReadToEnd());
                    responseModel.Success = true;
                }
                else
                {
                    //访问失败
                }
                return responseModel;
            }
            catch (Exception ex)
            {
                responseModel.Msg = ex.ToString();
                return responseModel;
            }
        }

        /// <summary>
        /// 获取MD5加密后的字符串
        /// </summary>
        /// <param name="privateKey">密钥KEY</param>
        /// <param name="secret">密码</param>
        /// <param name="keytime">时间戳</param>
        /// <returns></returns>
        public static string GetKeyString(string privateKey, string secret, string keytime)
        {
            return MD5.md5strGet(privateKey + secret + keytime, true).ToLower();
        }

        public string Gb2312ToUtf8(string gb2312String)
        {
            Encoding fromEncoding = Encoding.GetEncoding("gb2312");
            Encoding toEncoding = Encoding.UTF8;
            return EncodingConvert(gb2312String, fromEncoding, toEncoding);
        }

        public string Utf8ToGb2312(string utf8String)
        {
            Encoding fromEncoding = Encoding.UTF8;
            Encoding toEncoding = Encoding.GetEncoding("gb2312");
            return EncodingConvert(utf8String, fromEncoding, toEncoding);
        }

        public string Utf8ToGbk(string input)
        {
            //string strSource = utf8String;
            //byte[] array2 = Encoding.GetEncoding("gbk").GetBytes(strSource);

            ////get string from gbk
            //string str = Encoding.GetEncoding("gbk").GetString(array2);
            //return str;

            return Encoding.GetEncoding("GBK").GetString(Encoding.Default.GetBytes(input));
        }

        public string EncodingConvert(string fromString, Encoding fromEncoding, Encoding toEncoding)
        {
            byte[] fromBytes = fromEncoding.GetBytes(fromString);
            byte[] toBytes = Encoding.Convert(fromEncoding, toEncoding, fromBytes);

            string toString = toEncoding.GetString(toBytes);
            return toString;
        }
    }
}
