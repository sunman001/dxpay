using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace TOOL
{
    public static class StringHelper
    {
        /// <summary>
        /// 第一个字母大小
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FirstCharToUpper(this string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        #region 去掉HTML中的所有标签,只留下纯文本
        /// <summary>
        /// 去掉HTML中的所有标签,只留下纯文本
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>

        public static string CleanHtml(this string strHtml)
        {
            if (string.IsNullOrEmpty(strHtml)) return strHtml;
            //删除脚本
            //Regex.Replace(strHtml, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase)
            strHtml = Regex.Replace(strHtml, "(\\<script(.+?)\\</script\\>)|(\\<style(.+?)\\</style\\>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            //删除标签
            var r = new Regex(@"<\/?[^>]*>", RegexOptions.IgnoreCase);
            Match m;
            for (m = r.Match(strHtml); m.Success; m = m.NextMatch())
            {
                strHtml = strHtml.Replace(m.Groups[0].ToString(), "");
            }
            return strHtml.Trim();
        }

        #endregion

        #region 截取指定长度的字符串
        /// <summary>
        /// 截取指定长度的字符串
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <param name="strLength">要保留的字符串长度</param>
        /// <returns></returns>
        public static string CutStrLength(this string str, int strLength)
        {
            var strNew = str;
            if (string.IsNullOrEmpty(strNew)) return strNew;
            var strOriginalLength = strNew.Length;
            if (strOriginalLength > strLength)
            {
                strNew = strNew.Substring(0, strLength) + "...";
            }
            return strNew;
        }

        #endregion

        #region
        /// <summary>
        /// 截取指定长度的字符串
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <param name="strLength">要保留的字符串长度</param>
        /// <param name="endWithEllipsis">是或以省略号(...)结束</param>
        /// <returns></returns>
        public static string CutStrLength(this string str, int strLength, bool endWithEllipsis = true)
        {
            string strNew = str;
            if (!strNew.Equals(""))
            {
                int strOriginalLength = strNew.Length;
                if (strOriginalLength > strLength)
                {
                    strNew = strNew.Substring(0, strLength);
                    if (endWithEllipsis)
                    {
                        strNew += "...";
                    }
                }
            }
            return strNew;
        }

        #endregion

        public static string RemoveStyles(this string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return html;
            }
            var xmlDoc = XDocument.Parse(html);
            // Remove all inline styles
            xmlDoc.Descendants().Attributes("style").Remove();
            xmlDoc.Descendants().Attributes("class").Remove();

            // Remove all classes inserted by 3rd party, without removing our own lovely classes
            //foreach (var node in xmlDoc.Descendants())
            //{
            //    var classAttribute = node.Attributes("class").SingleOrDefault();
            //    if (classAttribute == null)
            //    {
            //        continue;
            //    }
            //    var classesThatShouldStay = classAttribute.Value.Split(' ').Where(className => !className.StartsWith("abc"));
            //    classAttribute.SetValue(string.Join(" ", classesThatShouldStay));

            //}

            return xmlDoc.ToString();
        }
    }
}
