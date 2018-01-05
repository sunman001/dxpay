using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace JMP.TOOL
{
    /// <summary>
    /// xml操作公共方法
    /// </summary>
    public class xmlhelper
    {
        /// <summary>
        /// 将Dictionary键值集合转换成xml文件
        /// </summary>
        /// <param name="m_values">Dictionary键值集合</param>
        /// <returns>返回一个string类型的xml格式数据</returns>
        public static string ToXml(Dictionary<string, string> m_values)
        {
            //数据为空时不能转化为xml格式
            if (m_values.Count > 0)
            {
                string xml = "<xml>";
                foreach (KeyValuePair<string, string> pair in m_values)
                {
                    //字段值不能为null，会影响后续流程
                    if (pair.Value == null)
                    {
                        //Log.Error(this.GetType().ToString(), "WxPayData内部含有值为null的字段!");
                    }

                    if (pair.Value.GetType() == typeof(int))
                    {
                        xml += "<" + pair.Key + ">" + pair.Value + "</" + pair.Key + ">";
                    }
                    else if (pair.Value.GetType() == typeof(string))
                    {
                        xml += "<" + pair.Key + ">" + "<![CDATA[" + pair.Value + "]]></" + pair.Key + ">";
                    }
                    else//除了string和int类型不能含有其他数据类型
                    {
                        // Log.Error(this.GetType().ToString(), "WxPayData字段数据类型错误!");
                    }
                }
                xml += "</xml>";
                return xml;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 将xml文件转换为Dictionary键值集合
        /// </summary>
        /// <param name="xml">xml数据</param>
        /// <returns>返回Dictionary键值集合</returns>
        public static Dictionary<string, object> FromXml(string xml)
        {
            if (!string.IsNullOrEmpty(xml))
            {


                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);
                XmlNode xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
                XmlNodeList nodes = xmlNode.ChildNodes;
                Dictionary<string, object> m_values = new Dictionary<string, object>();
                foreach (XmlNode xn in nodes)
                {
                    XmlElement xe = (XmlElement)xn;
                    m_values[xe.Name] = xe.InnerText;//获取xml的键值对到WxPayData内部的数据中
                }
                return m_values;
            }
            else
            {
                return null;
            }
        }

        public static Dictionary<string, string> FromXmls(string xml)
        {
            if (!string.IsNullOrEmpty(xml))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);
                XmlNode xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
                XmlNodeList nodes = xmlNode.ChildNodes;
                Dictionary<string, string> m_values = new Dictionary<string, string>();
                foreach (XmlNode xn in nodes)
                {
                    XmlElement xe = (XmlElement)xn;
                    m_values[xe.Name] = xe.InnerText;//获取xml的键值对到WxPayData内部的数据中
                }
                return m_values;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 将xml文件反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string xml)
        {

            if (string.IsNullOrEmpty(xml))
            {
                return default(T);
            }

            var serializer = new XmlSerializer(typeof(T));

            var settings = new XmlReaderSettings();
            // No settings need modifying here

            using (var textReader = new StringReader(xml))
            {
                using (var xmlReader = XmlReader.Create(textReader, settings))
                {
                    return (T)serializer.Deserialize(xmlReader);
                }
            }
        }
    }
}
