using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JMP.TOOL
{
    public class PostJsonRequest
    {
        /// <summary>
        /// c#post请求提交json数据格式
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="json">请求json格式参数</param>
        /// <returns></returns>
        public static string GetHtmlByJson(string url, string json = "")
        {
            var result = string.Empty;
            if (!string.IsNullOrEmpty(json))
            {
                try
                {
                    var request = WebRequest.Create(url) as HttpWebRequest;
                    request.ContentType = "text/json";
                    request.Method = "post";
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                        var response = (HttpWebResponse)request.GetResponse();
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
                catch (WebException ex)
                {
                    // 出错处理  
                    return "";
                }
            }
            return result;
        }
    }
}
