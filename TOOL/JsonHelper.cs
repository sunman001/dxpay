/************聚米支付平台__公共转换json格式数据方法************/
//描述：公共转换json格式数据方
//功能：公共转换json格式数据方
//开发者：秦际攀
//开发时间: 2016.03.21
/************聚米支付平台__公共转换json格式数据方************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JMP.TOOL
{
    public class JsonHelper
    {
        /// <summary>
        /// 把json字符串转成对象（支付接口，终端属性接口使用）
        /// </summary>
        /// <typeparam name="T">对象<peparam>
        /// <param name="data">json字符串</param> 
        public static T Deserializes<T>(string data)
        {
            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer json = new System.Web.Script.Serialization.JavaScriptSerializer();
                return json.Deserialize<T>(data);
            }
            catch (Exception e)
            {
                if (e.InnerException is Ex)
                {
                    //int ErrorCode = ((Ex)e).ErrorCode;
                    throw new JMP.TOOL.Ex((e.InnerException).Message);
                }
                else
                {
                    return default(T);
                }
            }

        }
        /// <summary>
        /// 接口转换把json字符串转成对象
        /// </summary>
        /// <typeparam name="T">对象<peparam>
        /// <param name="data">json字符串</param> 
        public static T Deserialize<T>(string data)
        {
            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer json = new System.Web.Script.Serialization.JavaScriptSerializer();
                return json.Deserialize<T>(data);
            }
            catch
            {
                return default(T);
            }
        }
        /// <summary>
        /// 把对象转成json字符串
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static string Serialize(object o)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Web.Script.Serialization.JavaScriptSerializer json = new System.Web.Script.Serialization.JavaScriptSerializer();
            json.Serialize(o, sb);
            return sb.ToString();
        }
        /// <summary> 
        /// 将JSON文本转换成Dictionary键值集合
        /// </summary> 
        /// <param name="jsonText">JSON文本</param> 
        /// <returns>Dictionary键值集合</returns> 
        public static Dictionary<string, object> DataRowFromJSON(string jsonText)
        {
            return Deserialize<Dictionary<string, object>>(jsonText);
        }

        /// <summary> 
        /// 将JSON文本转换成Dictionary键值集合
        /// </summary> 
        /// <param name="jsonText">JSON文本</param> 
        /// <returns>Dictionary键值集合</returns> 
        public static Dictionary<string, string> DataRowJSON(string jsonText)
        {
            return Deserialize<Dictionary<string, string>>(jsonText);
        }
        /// <summary>
        /// 将Dictionary键值集合转换成json格式
        /// </summary>
        /// <param name="sb">Dictionary键值集合</param>
        /// <param name="ts">是否需要组装二级json格式，如果需要则传入需要组装二级json字段的名字(如键值集合中的key值为a需要把a组装成二级json格式，那么就传入a结果{"b": "b","a": { "a": "a"}}注意a的value需要自己组装成json格式或者字符串 )</param>
        /// <returns></returns>
        public static string DictJsonstr(Dictionary<string, string> sb, string ts = null)
        {
            string result ="";
            try
            {
           
            if (sb != null)
            {
                    result = "{";
                result += string.Join(",", sb.Select(x => !string.IsNullOrEmpty(ts) && ts == x.Key ? "\"" + x.Key + "\":" + x.Value : "\"" + x.Key + "\":\"" + x.Value + "\""));
                result += "}";
            }
            }
            catch (Exception ex)
            {
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress,  "json转换数据错误", "将键值集合转换为json出错！错误信息：" + ex.ToString());//写入报错日志
            }
            return result;
        }
    }
}
