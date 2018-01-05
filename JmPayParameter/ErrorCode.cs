using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JmPayParameter
{
    /// <summary>
    /// 支付接口返回错误码枚举
    /// </summary>
    public enum ErrorCode
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
        /// 支付接口异常
        /// </summary>
        [Description("支付接口异常")]
        Code102 = 102,

        /// <summary>
        /// 参数有误
        /// </summary>
        [Description("参数有误")]
        Code103 = 103,
        /// <summary>
        ///支付通道异常
        /// </summary>
        [Description("支付通道异常")]
        Code104 = 104,
        /// <summary>
        /// 支付通道已关闭
        /// </summary>
        [Description("支付通道已关闭")]
        Code105 = 105,
        /// <summary>
        /// 支付通道未配置
        /// </summary>
        [Description("支付通道未配置")]
        Code106 = 106,
        /// <summary>
        /// 支付通有误
        /// </summary>
        [Description("支付通有误")]
        Code107 = 107,
        /// <summary>
        /// 非法请求
        /// </summary>
        [Description("非法请求")]
        Code9999 = 9999,
        /// <summary>
        /// 应用无效或未审核
        /// </summary>
        [Description("应用无效或未审核")]
        Code9998 = 9998,

        /// <summary>
        /// 参数bizcode有误
        /// </summary>
        [Description("参数bizcode有误")]
        Code9997 = 9997,
        /// <summary>
        /// 参数termkey有误
        /// </summary>
        [Description("参数termkey有误")]
        Code9996 = 9996,
        /// <summary>
        /// 参数address有误
        /// </summary>
        [Description("参数address有误")]
        Code9995 = 9995,
        /// <summary>
        /// 参数showaddress有误
        /// </summary>
        [Description("参数showaddress有误")]
        Code9994 = 9994,
        /// <summary>
        /// 参数goodsname有误
        /// </summary>
        [Description("参数goodsname有误")]
        Code9993 = 9993,

        /// <summary>
        /// 参数price有误
        /// </summary>
        [Description("参数price有误")]
        Code9992 = 9992,
        /// <summary>
        /// 参数privateinfo有误
        /// </summary>
        [Description("参数privateinfo有误")]
        Code9991 = 9991,
        /// <summary>
        /// 参数paytype有误
        /// </summary>
        [Description("参数paytype有误")]
        Code9990 = 9990,
        /// <summary>
        /// 签名验证失败
        /// </summary>
        [Description("签名验证失败")]
        Code9989 = 9989,
        /// <summary>
        /// 订单校验失败
        /// </summary>
        [Description("订单校验失败")]
        Code9988 = 9988,
        /// <summary>
        /// 支付平台有误
        /// </summary>
        [Description("支付平台有误")]
        Code9987 = 9987,
        /// <summary>
        /// 参数price有误
        /// </summary>
        [Description("参数price有误")]
        Code9986 = 9986,
        /// <summary>
        /// 参数timestamp有误
        /// </summary>
        [Description("参数timestamp有误")]
        Code9985 = 9985,
        /// <summary>
        /// 订单超时
        /// </summary>
        [Description("订单超时")]
        Code9984 = 9984,

        /// <summary>
        /// 参数code有误
        /// </summary>
        [Description("参数code有误")]
        Code8999 = 8999,
        /// <summary>
        /// 参数sign有误
        /// </summary>
        [Description("参数sign有误")]
        Code8998 = 8998,

        /// <summary>
        /// 参数price有误
        /// </summary>
        [Description("参数price有误")]
        Code8997 = 8997,
        /// <summary>
        /// 参数goodsname有误
        /// </summary>
        [Description("参数goodsname有误")]
        Code8996 = 8996,
        /// <summary>
        /// 参数apptype有误
        /// </summary>
        [Description("参数apptype有误")]
        Code8995 = 8995,
        /// <summary>
        /// 参数paymode有误
        /// </summary>
        [Description("参数paymode有误")]
        Code8994 = 8994,
        /// <summary>
        /// 参数paytype有误
        /// </summary>
        [Description("参数paymode有误")]
        Code8993 = 8993,
        /// <summary>
        /// 订单已关闭
        /// </summary>
        [Description("订单已关闭")]
        Code8992 = 8992,
        /// <summary>
        /// 订单已处理，不能重复提交！
        /// </summary>
        [Description("订单已处理，不能重复提交")]
        Code8991 = 8991,
        /// <summary>
        /// 订单金额不能小于单笔最小支付金额
        /// </summary>
        [Description("订单金额不能小于单笔最小支付金额")]
        Code8990 = 8990,
        /// <summary>
        /// 订单金额不能大于单笔最大支付金额
        /// </summary>
        [Description("订单金额不能大于单笔最大支付金额")]
        Code8989 = 8989,
        /// <summary>
        /// 存在重复调起支付请求
        /// </summary>
        [Description("存在重复调起支付请求")]
        Code8988 = 8988,
        /// <summary>
        /// 汇率必须大于零
        /// </summary>
        [Description("费率必须大于零")]
        Code8987 = 8987,
    }
    /// <summary>
    /// 返回枚举帮助接口
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 返回编码方法
        /// </summary>
        /// <param name="errorCode"></param>
        /// <returns>返回一个int类型的Code值</returns>
        public static int GetValue(this ErrorCode errorCode)
        {
            var name = Enum.GetName(typeof(ErrorCode), errorCode);
            var val = (int)Enum.Parse(typeof(ErrorCode), name);
            return val;
        }
        /// <summary>
        /// 返回提示方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerationValue"></param>
        /// <returns></returns>
        public static string GetDescription<T>(this T enumerationValue) where T : struct
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
