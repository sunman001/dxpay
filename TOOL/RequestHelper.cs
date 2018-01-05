/************聚米支付平台__获取网页基本信息************/
//描述：
//功能：
//开发者：陶涛
//开发时间: 2016.03.04
/************聚米支付平台__获取网页基本信息************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace JMP.TOOL
{
    public class RequestHelper
    {
        public static string GetScriptNameQueryString
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["QUERY_STRING"].ToString();
            }
        }

        /// <summary>
        /// 获取页面（/admin/index）
        /// </summary>
        public static string GetScriptName
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"].ToString();
            }
        }

        /// <summary>  
        /// 获取客户端Ip  
        /// </summary>  
        /// <returns></returns>  
        public static string GetClientIp()
        {
            String clientIP = "";
            if (System.Web.HttpContext.Current != null)
            {
                clientIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(clientIP) || (clientIP.ToLower() == "unknown"))
                {
                    clientIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_REAL_IP"];
                    if (string.IsNullOrEmpty(clientIP))
                    {
                        clientIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    }
                }
                else
                {
                    clientIP = clientIP.Split(',')[0];
                }
            }
            return clientIP;
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="strInput">字符</param>
        /// <param name="intLen">长度</param>
        /// <returns></returns>
        public static string cutString(string strInput, int intLen)
        {
            if (string.IsNullOrEmpty(strInput))
            {
                return "";
            }
            strInput = strInput.Trim();
            byte[] myByte = System.Text.Encoding.Default.GetBytes(strInput);
            if (myByte.Length > intLen)
            {
                //截取操作
                string resultStr = "";
                for (int i = 0; i < strInput.Length; i++)
                {
                    byte[] tempByte = System.Text.Encoding.Default.GetBytes(resultStr);
                    if (tempByte.Length < intLen - 4)
                    {
                        resultStr += strInput.Substring(i, 1);
                    }
                    else
                    {
                        break;
                    }
                }
                return resultStr + " ...";
            }
            else
            {
                return strInput;
            }
        }

        /// <summary>
        /// HTTP提交表单(POST)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string HttpPost(string url, string parameters)
        {
            var req = System.Net.WebRequest.Create(url);
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            var bytes = Encoding.UTF8.GetBytes(parameters);
            req.ContentLength = bytes.Length;
            var os = req.GetRequestStream();
            os.Write(bytes, 0, bytes.Length);
            os.Close();
            var resp = req.GetResponse();
            var sr = new System.IO.StreamReader(resp.GetResponseStream());
            sr.Close();
            return sr.ReadToEnd().Trim();
        }
    }
}
