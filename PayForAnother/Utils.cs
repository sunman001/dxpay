using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PayForAnother.Logger;

namespace PayForAnother
{
    /// <summary>
    /// 代付
    /// </summary>
    public class Utils
    {
        private static readonly ILogWriter Logger = LogWriterManager.GetPayForAnotherLogger;
        //签名
        public static string signData(string merId, string plain, string priKeyPath)
        {
            byte[] StrRes = Encoding.GetEncoding("gbk").GetBytes(plain);
            plain = Convert.ToBase64String(StrRes);

            NetPay a = new NetPay();
            //设置密钥文件地址
            a.buildKey(merId, 0, priKeyPath);

            // 对一段字符串的签名
            return a.Sign(plain);

        }


        //验签
        public static string checkData(string plain, string ChkValue, string pubKeyPath)
        {
            byte[] StrRes = Encoding.GetEncoding("gbk").GetBytes(plain);
            plain = Convert.ToBase64String(StrRes);

            NetPay a = new NetPay();
            //设置密钥文件地址
            a.buildKey("999999999999999", 0, pubKeyPath);

            // 对一段字符串的签名
            if (a.verifyAuthToken(plain, ChkValue))
            {
                return "0";
            }
            else
            {
                return "-118";
            }
        }

        //提交数据
        public static string postData(string str, string url)
        {
            try
            {
                byte[] data = System.Text.Encoding.GetEncoding("gbk").GetBytes(str);
                //   准备请求
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(@url);
                req.Method = "Post";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = data.Length;
                Stream stream = req.GetRequestStream();
                //   发送数据   
                stream.Write(data, 0, data.Length);
                stream.Close();

                HttpWebResponse rep = (HttpWebResponse)req.GetResponse();
                Stream receiveStream = rep.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("gbk");
                StreamReader readStream = new StreamReader(receiveStream, encode);

                Char[] read = new Char[256];
                int count = readStream.Read(read, 0, 256);
                StringBuilder sb = new StringBuilder("");
                while (count > 0)
                {
                    String readstr = new String(read, 0, count);
                    sb.Append(readstr);
                    count = readStream.Read(read, 0, 256);
                }

                rep.Close();
                readStream.Close();

                return sb.ToString();

            }
            catch (Exception ex)
            {
                Logger.PayForAnotherLog("代付异常", ex.Message);
                return "";

            }
        }
    }
}
