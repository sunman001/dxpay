using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JMP.TOOL
{
    /// <summary>
    /// 订单查询方法
    /// </summary>
    public class SelectOrder
    {
        /// <summary>
        /// 订单查询公共方法（字符串传参）
        /// </summary>
        /// <param name="strlist">键值集合</param>
        /// <param name="url">查询地址</param>
        /// <returns></returns>
        public static string jsonstr(Dictionary<string, string> strlist, string url)
        {
            string postString = JMP.TOOL.JsonHelper.DictJsonstr(strlist);
            byte[] postData = Encoding.UTF8.GetBytes(postString);//编码，尤其是汉字，事先要看下抓取网页的编码方式  
            WebClient webClient = new WebClient();
            byte[] responseData = webClient.UploadData(url, "POST", postData);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码  
            if (!string.IsNullOrEmpty(srcString))
            {
                return srcString;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 订单查询公共方法（data数组传参数）
        /// </summary>
        /// <param name="PostVars">键值集合</param>
        /// <param name="url">提交地址</param>
        /// <returns></returns>
        public static string postfromstr(NameValueCollection PostVars, string url)
        {
            WebClient webClient = new WebClient();
            byte[] responseData = webClient.UploadValues(url, "POST", PostVars);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码  
            if (!string.IsNullOrEmpty(srcString))
            {
                return srcString;
            }
            else
            {
                return "";
            }
        }
    }
}
