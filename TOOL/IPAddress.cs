/***********聚米支付平台__根据Ip获取身份、城市************/
//描述：根据Ip获取身份、城市
//功能：根据Ip获取身份、城市
//开发者：秦际攀
//开发时间: 2016.03.08
/************聚米支付平台__根据Ip获取身份、城市************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace JMP.TOOL
{
    public class IPAddress
    {
        /// <summary>
        /// 根据IP获取省市
        /// </summary>
        /// <param name="IP">ip地址</param>
        /// <returns>返回一个数组（0：省份，1：城市）</returns>
        public static string GetAddressByIp(string IP)
        {
            //string ip = "115.193.217.249";
            string a = "";
            if (IP == "::1" || string.IsNullOrEmpty(IP))
            {
                a = "";
            }
            else
            {
                //string PostUrl = "http://int.dpool.sina.com.cn/iplookup/iplookup.php?ip=" + IP;
                string res = GetDataByPost(IP);//该条请求返回的数据为：res=1\t115.193.210.0\t115.194.201.255\t中国\t浙江\t杭州\t电信
                string[] arr = getAreaInfoList(res);
                if (arr != null)
                {
                    a = arr[0] + "-" + arr[1];
                }
                else
                {
                    a = "";
                }
            }
            return a;
        }
        /// <summary>
        /// Post请求数据
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <returns></returns>
        public static string GetDataByPost(string ip)
        {
            try
            {
                string url = "http://int.dpool.sina.com.cn/iplookup/iplookup.php?ip=" + ip;//地址  

                //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                //string s = "anything";
                //byte[] requestBytes = System.Text.Encoding.ASCII.GetBytes(s);
                //req.Method = "POST";
                //req.ContentType = "application/x-www-form-urlencoded";
                //req.ContentLength = requestBytes.Length;
                //Stream requestStream = req.GetRequestStream();
                //requestStream.Write(requestBytes, 0, requestBytes.Length);
                //requestStream.Close();
                //HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                //StreamReader sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.Default);
                //string backstr = sr.ReadToEnd();
                //sr.Close();
                //res.Close();
                //return backstr;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 3000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default))
                {
                    string jmpay = reader.ReadToEnd();
                    return jmpay;
                }
            }
            catch
            {

                return "";
            }


        }
        /// <summary>
        /// 处理所要的数据
        /// </summary>
        /// <param name="ip">根据ip信息获取省份、城市</param>
        /// <returns></returns>
        public static string[] getAreaInfoList(string ipData)
        {
            //1\t115.193.210.0\t115.194.201.255\t中国\t浙江\t杭州\t电信
            string[] areaArr = new string[10];
            string[] newAreaArr = new string[2];
            try
            {
                //取所要的数据，这里只取省市
                areaArr = ipData.Split('\t');
                newAreaArr[0] = areaArr[4];//省
                newAreaArr[1] = areaArr[5];//市
            }
            catch (Exception e)
            {
                newAreaArr = null;
                // TODO: handle exception
            }
            return newAreaArr;
        }
    }
}
