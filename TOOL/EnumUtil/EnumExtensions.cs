using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TOOL.EnumUtil
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 根据枚举值获取对应的枚举名称
        /// </summary>
        /// <typeparam name="T">泛型枚举</typeparam>
        /// <param name="t">泛型枚举</param>
        /// <param name="value">枚举值</param>
        /// <returns></returns>
        public static string GetEnumNameByValue<T>(this T t, int value) where T : struct
        {
            var type = t.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("不是可用的枚举类型");
            }
            var e = Enum.GetName(typeof(T), value);
            return e;
        }

        /// <summary>
        /// 根据枚举值获取对应的枚举名称
        /// </summary>
        /// <typeparam name="T">泛型枚举</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns></returns>
        public static string GetEnumNameByValue<T>(this int value) where T : struct
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new ArgumentException("不是可用的枚举类型");
            }
            var e = Enum.GetName(typeof(T), value);
            return e;
        }

        /// <summary>
        /// 根据枚举值获取对应的枚举描述
        /// </summary>
        /// <typeparam name="T">泛型枚举</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns></returns>
        public static string GetEnumDescByValue<T>(this int value) where T : struct
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new ArgumentException("不是可用的枚举类型");
            }
            var t = ToEnum<T>(value);
            return t.GetDescription();
        }

        /// <summary>
        /// 转换成指定枚举
        /// </summary>
        /// <typeparam name="T">泛型枚举对象</typeparam>
        /// <param name="param">枚举值</param>
        /// <returns></returns>
        public static T ToEnum<T>(this int param) where T : struct
        {
            var info = typeof(T);
            if (info.IsEnum)
            {
                T result = (T)Enum.Parse(typeof(T), param.ToString(), true);
                return result;
            }

            return default(T);
        }

        /// <summary>
        /// 获取枚举的描述
        /// </summary>
        /// <typeparam name="T">泛型枚举</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns></returns>
        public static string GetDescription<T>(this T value) where T : struct
        {
            var type = value.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("不是可用的枚举类型");
            }
            var memberInfo = type.GetMember(value.ToString());
            if (memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return value.ToString();
        }


        /// <summary>
        /// 将枚举转换成字典
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="intValueAsKey">为true时取枚举的数字值作为字典的键,否则取枚举的字符串名作为字典的键,默认:true</param>
        /// <returns></returns>
        public static Dictionary<object, string> ToDictionary<T>(this Type source, bool intValueAsKey = true) where T : struct, IConvertible
        {
            if (!source.IsEnum || typeof(T) != source)
            {
                throw new InvalidEnumArgumentException("T is not System.Enum");
            }
            var dict = new Dictionary<object, string>();
            foreach (var e in Enum.GetValues(typeof(T)))
            {
                var fi = e.GetType().GetField(e.ToString());
                var attrs = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                var desc = attrs.Length > 0 ? attrs[0].Description : e.ToString();
                if (intValueAsKey)
                {
                    dict[(int)e] = desc;
                }
                else
                {
                    dict[e.ToString()] = desc;
                }
            }

            return dict;
        }

        /// <summary>
        /// 将枚举转换成键值对集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="intValueAsKey">为true时取枚举的数字值作为字典的键,否则取枚举的字符串名作为字典的键,默认:true</param>
        /// <returns></returns>
        public static List<KeyValuePair<object, string>> ToEnumList<T>(bool intValueAsKey = true)
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new InvalidEnumArgumentException("T is not System.Enum");
            }
            var enumList = new List<KeyValuePair<object, string>>();


            foreach (var e in Enum.GetValues(typeof(T)))
            {
                var fi = e.GetType().GetField(e.ToString());
                var attrs = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                var desc = attrs.Length > 0 ? attrs[0].Description : e.ToString();
                enumList.Add(intValueAsKey
                    ? new KeyValuePair<object, string>((int)e, desc)
                    : new KeyValuePair<object, string>(e.ToString(), desc));
            }

            /*
            var enumList = (from object e in Enum.GetValues(typeof(T))
                            let fi = e.GetType().GetField(e.ToString())
                            let attrs = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false)
                            let desc = attrs.Length > 0 ? attrs[0].Description : e.ToString()
                            select new KeyValuePair<object, string>(intValueAsKey? (int)e : e.ToString(), desc)).ToList();
                            */
            return enumList;
        }
    }
}
