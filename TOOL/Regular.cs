using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JMP.TOOL
{
    /// <summary>
    /// 正则表达式验证类
    /// </summary>
    public static class Regular
    {
        /// <summary>
        /// 验证基础方法
        /// </summary>
        /// <param name="value">验证参数</param>
        /// <param name="isRestrict">需要验证的正则表达式</param>
        /// <returns></returns>
        public static bool IsMatch(this string value, string isRestrict)
        {

            return System.Text.RegularExpressions.Regex.IsMatch(value, isRestrict);

        }
        /// <summary>
        /// 验证数据是否为空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        #region IsEmail(是否邮箱)
        /// <summary>
        /// 是否邮箱
        /// </summary>
        /// <param name="value">邮箱地址</param>
        /// <param name="isRestrict">是否按严格模式验证</param>
        /// <returns></returns>
        public static bool IsEmail(string value, bool isRestrict = false)
        {
            if (value.IsEmpty())
            {
                return false;
            }
            string pattern = isRestrict
                ? @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"
                : @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";

            return value.IsMatch(pattern);
        }
        /// <summary>
        /// 是否合法的手机号码
        /// </summary>
        /// <param name="value">手机号码</param>
        /// <returns></returns>
        public static bool IsPhoneNumber(string value)
        {
            if (value.IsEmpty())
            {
                return false;
            }
            return value.IsMatch(@"^(0|86|17951)?(13[0-9]|15[012356789]|18[0-9]|14[57]|17[678])[0-9]{8}$");
        }
        /// <summary>
        /// 是否身份证号码
        /// </summary>
        /// <param name="value">身份证</param>
        /// <returns></returns>
        public static bool IsIdCard(string value)
        {
            if (value.IsEmpty())
            {
                return false;
            }
            if (value.Length == 15)
            {
                return value.IsMatch(@"^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$");
            }
            return value.Length == 0x12 &&
                   value.IsMatch(@"^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])((\d{4})|\d{3}[Xx])$");
        }
        /// <summary>
         /// 是否Url地址（统一资源定位）
         /// </summary>
         /// <param name="value">url地址</param>
         /// <returns></returns>
        public static bool IsUrl(string value)
        {
            if (value.IsEmpty())
            {
                return false;
            }
            return
                value.IsMatch(
                    @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }
        /// <summary>
        /// 是否Double类型
        /// </summary>
        /// <param name="value">小数</param>
        /// <returns></returns>
        public static bool IsDouble(string value)
        {
            if (value.IsEmpty())
            {
                return false;
            }
            return value.IsMatch(@"^\d[.]?\d?$");
        }
        /// <summary>
        /// 验证最多只能保留两位小数
        /// </summary>
        /// <param name="value">小数</param>
        /// <returns></returns>
        public static bool IsDem(string value)
        {
            if (value.IsEmpty())
            {
                return false;
            }
            return value.IsMatch(@"^\d{1,}(\.\d{1,2})?$");
        }
        #endregion

        /// <summary>
        /// 判断QQ钱包是否返回支付连接
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string IshiQQPay(string value)
        {
            string result = "";
            try
            {
                string pattern = "<input type=\"hidden\" name=\"hiQQPay\" id=\"hiQQPay\" value[=\\s\"\']+([^\"\']*)[\"\']?";
                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                //使用正则表达式匹配字符串，仅返回一次匹配结果
                Match m = regex.Match(value);
                result = m.ToString();

            }
            catch (Exception)
            {

                result = "";
            }
           
            return result;
        }

        /// <summary>
        /// 判断QQ钱包是否返回支付连接
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string IshiQQPayUrl(string value)
        {
            string result = "";
            try
            {
                string pattern = "value[=\\s\"\']+([^\"\']*)[\"\']?";
                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                //使用正则表达式匹配字符串，仅返回一次匹配结果
                Match m = regex.Match(value);
                result = m.ToString();

            }
            catch (Exception)
            {

                result = "";
            }

            return result;
        }


    }
}
