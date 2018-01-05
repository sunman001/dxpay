using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JmPayParameter
{
    /// <summary>
    /// 初始化接口返回错误码枚举
    /// </summary>
    public enum InfoErrorCode
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
        /// 参数出错
        /// </summary>
        [Description("参数出错")]
        Code9998 = 9998,
        /// <summary>
        /// 参数t_key有误
        /// </summary>
        [Description("参数t_key有误")]
        Code9997 = 9997,
        /// <summary>
        /// 应用无效或未审核
        /// </summary>
        [Description("应用无效或未审核")]
        Code9996 = 9996,
        /// <summary>
        /// 参数t_mark有误
        /// </summary>
        [Description("参数t_mark有误")]
        Code9995 = 9995,
        /// <summary>
        /// 参数t_imsi有误
        /// </summary>
        [Description("参数t_imsi有误")]
        Code9994 = 9994,
        /// <summary>
        /// 参数t_brand有误
        /// </summary>
        [Description("参数t_brand有误")]
        Code9993 = 9993,
        /// <summary>
        /// 参数t_system有误
        /// </summary>
        [Description("参数t_system有误")]
        Code9992 = 9992,
        /// <summary>
        /// 参数t_hardware有误
        /// </summary>
        [Description("参数t_hardware有误")]
        Code9991 = 9991,
        /// <summary>
        ///参数t_sdkver有误
        /// </summary>
        [Description("参数t_sdkver有误")]
        Code9990 = 9990,
        /// <summary>
        ///参数t_screen有误
        /// </summary>
        [Description("参数t_screen有误")]
        Code9989 = 9989,
        /// <summary>
        ///参数t_network有误
        /// </summary>
        [Description("参数t_network有误")]
        Code9988 = 9988,
    }
    /// <summary>
    /// 返回枚举帮助接口
    /// </summary>
    public static class InfoEnumHelper
    {
        /// <summary>
        /// 返回编码方法
        /// </summary>
        /// <param name="errorCode"></param>
        /// <returns>返回一个int类型的Code值</returns>
        public static int InfoGetValue(this InfoErrorCode errorCode)
        {
            var name = Enum.GetName(typeof(InfoErrorCode), errorCode);
            var val = (int)Enum.Parse(typeof(InfoErrorCode), name);
            return val;
        }
        /// <summary>
        /// 返回提示方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerationValue"></param>
        /// <returns></returns>
        public static string InfoGetDescription<T>(this T enumerationValue) where T : struct
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
