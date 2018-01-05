using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JmPayParameter
{
    /// <summary>
    /// 查询接口错误提示码
    /// </summary>
    public enum QueryErrorCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Code100 = 100,
        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Code101 = 101,

        /// <summary>
        /// 非法请求
        /// </summary>
        [Description("非法请求")]
        Code9999 = 9999,
        /// <summary>
        /// 参数bizcode有误
        /// </summary>
        [Description("参数bizcode有误")]
        Code9998 = 9998,
        /// <summary>
        /// 参数code有误
        /// </summary>
        [Description("参数code有误")]
        Code9997 = 9997,
        /// <summary>
        /// 参数timestamp有误
        /// </summary>
        [Description("参数timestamp有误")]
        Code9996 = 9996,
        /// <summary>
        /// 请求超时
        /// </summary>
        [Description("请求超时")]
        Code9995 = 9995,
        /// <summary>
        /// 应用无效或未审核
        /// </summary>
        [Description("应用无效或未审核")]
        Code9994 = 9994,
        /// <summary>
        /// 签名验证失败
        /// </summary>
        [Description("签名验证失败")]
        Code9993 = 9993,
        /// <summary>
        /// 查询次数频繁
        /// </summary>
        [Description("查询次数频繁")]
        Code9992 = 9992,
        /// <summary>
        /// 参数bizcode或code有误
        /// </summary>
        [Description("参数bizcode或code有误")]
        Code9991 = 9991,
    }
    /// <summary>
    /// 返回枚举帮助接口
    /// </summary>
    public static class QueryEnumHelper
    {
        /// <summary>
        /// 返回编码方法
        /// </summary>
        /// <param name="errorCode"></param>
        /// <returns>返回一个int类型的Code值</returns>
        public static int QueryGetValue(this QueryErrorCode errorCode)
        {
            var name = Enum.GetName(typeof(QueryErrorCode), errorCode);
            var val = (int)Enum.Parse(typeof(QueryErrorCode), name);
            return val;
        }
        /// <summary>
        /// 返回提示方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerationValue"></param>
        /// <returns></returns>
        public static string QueryGetDescription<T>(this T enumerationValue) where T : struct
        {
            var type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("泛型参数必须为枚举类型");
            }
            var memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo.Length <= 0) return enumerationValue.ToString();
            var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attrs.Length > 0 ? ((DescriptionAttribute)attrs[0]).Description : enumerationValue.ToString();
        }
    }
}
