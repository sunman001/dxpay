using System;
using System.IO;
using System.Net;
using System.Text;

namespace JMNOTICE.Util
{
    public class HttpHelper
    {
        public static string OpenReadWithHttps(string url,int q_uersid)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 3000;
                request.ServicePoint.ConnectionLimit = 100;
                var response = (HttpWebResponse)request.GetResponse();

                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    var hd = reader.ReadToEnd().Trim();
                    if (hd.Length > 10)
                    {
                        if (hd.Length > 100)
                        {
                            JMP.TOOL.AddLocLog.AddUserLog(q_uersid, 5, "", "通知失败：返回值：" + hd.Substring(2000), "通知地址：" + url);//写入报错日志
                        }
                        else
                        {
                            JMP.TOOL.AddLocLog.AddUserLog(q_uersid, 5, "", "通知失败：返回值：" + hd, "通知地址：" + url);//写入报错日志
                        }

                        return "fail";
                    }
                    if (hd.ToLower() != "success")
                    {
                        JMP.TOOL.AddLocLog.AddUserLog(q_uersid, 5, "", "通知失败：返回值：" + hd, "通知地址：" + url);//写入报错日志
                    }

                    return hd;
                }

            }
            catch (Exception e)
            {
                JMP.TOOL.AddLocLog.AddUserLog(q_uersid, 5, "", "通知地址错误:" + e.Message, "通知地址：" + url);//写入报错日志
                return "fail";
            }

        }
    }
}
