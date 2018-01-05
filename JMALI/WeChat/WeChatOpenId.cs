using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace JMALI.WeChat
{
    /// <summary>
    /// 微信获取OpendId公用方法
    /// </summary>
    public class WeChatOpenId
    {
        /// <summary>
        /// 获取微信Openid
        /// </summary>
        /// <param name="appid">微信appid</param>
        /// <param name="AppSecret">微信appid秘钥</param>
        /// <param name="Code">微信返回会参数Code</param>
        /// <returns></returns>
        public string SelectOpendi(string appid, string AppSecret, string Code)
        {
            string Url = "https://api.weixin.qq.com/sns/oauth2/access_token?";
            //string URL = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + appid + "&secret=" + appms + "&code=" + code + "&grant_type=authorization_code";
            Dictionary<string, string> List = new Dictionary<string, string>();
            List.Add("appid", appid);
            List.Add("secret", AppSecret);
            List.Add("code", Code);
            List.Add("grant_type", "authorization_code");
            Url = Url + JMP.TOOL.UrlStr.AzGetStr(List);
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Timeout = 3000;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string openid = string.Empty;
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8))
            {
                string jmpay = reader.ReadToEnd();
                //解析json对象
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Dictionary<string, object> json = (Dictionary<string, object>)serializer.DeserializeObject(jmpay);
                object value = null;
                json.TryGetValue("openid", out value);
                openid = value.ToString();
            }
            return openid;
        }
        /// <summary>
        /// 获取支付配置支付串
        /// </summary>
        /// <param name="PayId">支付通道id</param>
        /// <param name="Cache">缓存时间</param>
        /// <returns></returns>
        public string GetPayStr(string PayId, string Cache)
        {
            string wxgghzfhc = "SelecPayStr" + Cache + PayId.ToString();
            int CacheTime = int.Parse(ConfigurationManager.AppSettings["CacheTime"].ToString());//获取缓存时间
            if (JMP.TOOL.CacheHelper.IsCache(wxgghzfhc))
            {
                object ddjj = JMP.TOOL.CacheHelper.GetCaChe(wxgghzfhc);
                if (string.IsNullOrEmpty(ddjj.ToString()))
                {
                    ddjj = JMP.DBA.DbHelperSQL.GetSingle("SELECT l_str FROM jmp_interface WHERE l_id=" + PayId + "");
                    if (ddjj != null)
                    {
                        JMP.TOOL.CacheHelper.CacheObject(wxgghzfhc, ddjj, CacheTime);//存入缓存
                        return ddjj.ToString();
                    }
                    else
                    {
                        return "";
                    }
                }
                return ddjj.ToString();
            }
            else
            {
                object ddjj = JMP.DBA.DbHelperSQL.GetSingle("SELECT l_str FROM jmp_interface WHERE l_id=" + PayId + "");
                if (ddjj != null)
                {
                    JMP.TOOL.CacheHelper.CacheObject(wxgghzfhc, ddjj, CacheTime);//存入缓存
                    return ddjj.ToString();
                }
                else
                {
                    return "";
                }
            }

        }
    }
}