using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace JMP.TOOL
{
    /// <summary>
    /// post提交xml数据公共方法
    /// </summary>
   public class postxmlhelper
    {
        /// <summary>
        /// post提交xml数据
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="xml">xml数据</param>
        /// <returns>返回结果信息</returns>
        public static string postxml(string url, string xml)
        {

            /***************************************************************
                 * 下面设置HttpWebRequest的相关属性
                 * ************************************************************/
            HttpWebResponse response = null;
            Stream reqStream = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
            request.Method = "POST";
            request.Timeout = 3000;
            //设置POST的数据类型和长度
            request.ContentType = "text/xml";
            byte[] data = System.Text.Encoding.UTF8.GetBytes(xml);
            request.ContentLength = data.Length;
            bool isUseCert = true;

            //往服务器写入数据
            reqStream = request.GetRequestStream();
            reqStream.Write(data, 0, data.Length);
            reqStream.Close();

            //获取服务端返回
            response = (HttpWebResponse)request.GetResponse();

            //获取服务端返回数据
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string result = sr.ReadToEnd().Trim();
            sr.Close();
            return result;
        }
        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
