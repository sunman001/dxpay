using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JMP.TOOL
{
    /// <summary>
    /// 组装url参数字符串公共方法
    /// </summary>
    public class UrlStr
    {
        /// <summary>
        /// 根据Dictionary键值集合组装url
        /// </summary>
        /// <param name="sb">Dictionary键值集合</param>
        /// <returns>返回string字符串（格式：a=a&b=b&c=c）</returns>
        public static string getstr(Dictionary<string, string> sb)
        {
            string str = string.Empty;
            str = string.Join("&", sb.Select(x => x.Key + "=" + x.Value));
            return str;
        }
        /// <summary>
        /// 根据Dictionary键值集合组装url字符串按首字母循序排序
        /// </summary>
        /// <param name="st">Dictionary键值集合</param>
        /// <returns>返回string字符串（格式：a=a&b=b&c=c）</returns>
        public static string AzGetStr(Dictionary<string, string> st)
        {
            string str = string.Empty;
            var newDict = st.Where(x => !string.IsNullOrEmpty(x.Value)).OrderBy(x => x.Key).ToDictionary(k => k.Key, d => d.Value);
            str = string.Join("&", newDict.Select(x => string.Format("{0}={1}", x.Key, x.Value)));
            return str;
        }
        /// <summary>
        /// 根据Dictionary键值集合组装url字符串按首字母循序排序(为空的数据一起参加签名)
        /// </summary>
        /// <param name="st">Dictionary键值集合</param>
        /// <returns>返回string字符串（格式：a=a&b=b&c=c）</returns>
        public static string AzGetStrnotnull(Dictionary<string, string> st)
        {
            string str = string.Empty;
            var newDict = st.OrderBy(x => x.Key).ToDictionary(k => k.Key, d => d.Value);
            str = string.Join("&", newDict.Select(x => string.Format("{0}={1}", x.Key, x.Value)));
            return str;
        }
        /// <summary>
        /// 根据Dictionary键值集合组装url字符串按首字母循序排序
        /// </summary>
        /// <param name="st">Dictionary键值集合</param>
        /// <returns>返回string字符串（格式：a=ab=bc=c）</returns>
        public static string AzGetStrs(Dictionary<string, string> st)
        {
            string str = string.Empty;
            var newDict = st.Where(x => !string.IsNullOrEmpty(x.Value)).OrderBy(x => x.Key).ToDictionary(k => k.Key, d => d.Value);
            str = string.Join("", newDict.Select(x => string.Format("{0}={1}", x.Key, x.Value)));
            return str;
        }
        /// <summary>
        /// 根据NameValueCollection键值集合组装url字符串
        /// </summary>
        /// <param name="st">NameValueCollection键值集合</param>
        /// <returns>返回返回string字符串（格式：a=a&b=b&c=c）</returns>
        public static string GetStrNV(System.Collections.Specialized.NameValueCollection nv)
        {
            string str = string.Empty;
            for (int i = 0; i < nv.Count; i++)
            {
                str += nv.GetKey(i) + "=" + nv.Get(i) + "&";
            }
            str = str.Substring(0, str.Length - 1);
            return str;
        }
        /// <summary>
        /// 将NameValueCollection键值集合按字母a到z的顺便数组url字符串
        /// </summary>
        /// <param name="nv"></param>
        /// <returns></returns>
        public static string GetStrAzNv(System.Collections.Specialized.NameValueCollection nv)
        {
            string str = string.Empty;
            if (nv.Count > 0)
            {
                Dictionary<string, string> list = nv.Cast<string>().ToDictionary(x => x, x => nv[x]);
                str = AzGetStr(list);
            }
            return str;
        }

        /// <summary>
        /// 将键值集合转换成get url字符串
        /// </summary>
        /// <param name="Palist">键值集合</param>
        /// <param name="key">需要排除的key值</param>
        /// <returns>返回返回string字符串（格式：a=a&b=b&c=c）</returns>
        public static string GetStrNvNotKey(System.Collections.Specialized.NameValueCollection Palist, string key)
        {

            string str = "";
            if (string.IsNullOrEmpty(key))
            {
                for (int i = 0; i < Palist.Count; i++)
                {
                    str += Palist.GetKey(i) + "=" + Palist.Get(i) + "&";

                }
            }
            else
            {
                for (int k = 0; k < Palist.Count; k++)
                {
                    if (Palist.GetKey(k) != key)
                    {
                        str += Palist.GetKey(k) + "=" + Palist.Get(k) + "&";
                    }
                }
            }
            str = str.Substring(0, str.Length - 1);

            return str;
        }
        /// <summary>
        /// 获取from表单提交信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns>返回一个键值集合</returns>
        public static Dictionary<string, string> GetRequestfrom(HttpContext context, string name)
        {
            try
            {
                int i = 0;
                Dictionary<string, string> sArray = new Dictionary<string, string>();
                NameValueCollection coll;
                //Load Form variables into NameValueCollection variable.
                coll = context.Request.Form;

                // Get names of all forms into a string array.
                String[] requestItem = coll.AllKeys;
                for (i = 0; i < requestItem.Length; i++)
                {
                    sArray.Add(requestItem[i], context.Request.Form[requestItem[i]]);
                }
                return sArray;
            }
            catch (Exception ex)
            {
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, name + "通知接口错误", "获取参数json参数失败！报错信息：" + ex.ToString());//写入报错日志
                return null;
            }

        }

        /// <summary>
        /// 获取get提交信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns>返回一个键值集合</returns>
        public static Dictionary<string, string> GetRequestGet(HttpContext context, string name)
        {
            try
            {
                int i = 0;
                Dictionary<string, string> sArray = new Dictionary<string, string>();
                NameValueCollection coll;
                //Load Form variables into NameValueCollection variable.
                coll = context.Request.QueryString;

                // Get names of all forms into a string array.
                String[] requestItem = coll.AllKeys;
                for (i = 0; i < requestItem.Length; i++)
                {
                    sArray.Add(requestItem[i], context.Request.QueryString[requestItem[i]]);
                }
                return sArray;
            }
            catch (Exception ex)
            {
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, name + "通知接口错误", "获取参数json参数失败！报错信息：" + ex.ToString());//写入报错日志
                return null;
            }
        }
        /// <summary>
        /// 获取post提交信息过来的json数据
        /// </summary>
        /// <param name="context">http请求信息</param>
        /// <param name="name">接口名称</param>
        /// <returns>返回一个键值集合</returns>
        public static Dictionary<string, string> GetRequestJson(HttpContext context, string name)
        {
            try
            {
                string jsonstr = "";
                using (var wr = new StreamReader(context.Request.InputStream))
                {
                    jsonstr = wr.ReadToEnd();
                }
                try
                {
                    Dictionary<string, string> sArray = new Dictionary<string, string>();
                    sArray = JMP.TOOL.JsonHelper.Deserialize<Dictionary<string, string>>(jsonstr);
                    return sArray;
                }
                catch (Exception ex)
                {
                    AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, name + "通知接口错误", "获取参数json参数转换失败！报错信息：" + ex.ToString() + ",传入参数：" + jsonstr);//写入报错日志
                    return null;
                }
            }
            catch (Exception ex)
            {
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, name + "通知接口错误", "获取参数json参数失败！报错信息：" + ex.ToString());//写入报错日志
                return null;
            }
        }
        /// <summary>
        /// 将建哈希Hashtable转换成键值集合
        /// </summary>
        /// <param name="has">Hashtable</param>
        /// <returns>返回Dictionary键值集合</returns>
        public static Dictionary<string, string> hastable(Hashtable has)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (has.Count > 0)
            {
                try
                {
                    dic = has.Cast<DictionaryEntry>().ToDictionary(x => x.Key.ToString(), v => v.Value.ToString());
                }
                catch
                {
                    dic = new Dictionary<string, string>();
                    return dic;
                }

            }
            return dic;

        }
        /// <summary>
        /// 获取post提交的数据流
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string PostInput(HttpContext context, string name)
        {
            try
            {
                System.IO.Stream s = context.Request.InputStream;
                int count = 0;
                byte[] buffer = new byte[1024];
                StringBuilder builder = new StringBuilder();
                while ((count = s.Read(buffer, 0, 1024)) > 0)
                {
                    builder.Append(Encoding.UTF8.GetString(buffer, 0, count));
                }
                s.Flush();
                s.Close();
                s.Dispose();
                return builder.ToString();
            }
            catch (Exception ex)
            {
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, name + "通知接口错误", "获取参数json参数失败！报错信息：" + ex.ToString());//写入报错日志
                return "";
            }
        }



    }
}
