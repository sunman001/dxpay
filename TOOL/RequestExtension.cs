using System;
using System.Web;

namespace TOOL
{
    public static class RequestExtension
    {
        #region GET
        #region 获取[GET]请求参数

        /// <summary>
        /// 获取[GET]请求参数
        /// </summary>
        /// <typeparam name="T">转换后的泛型类型</typeparam>
        /// <param name="request">HTTP请求</param>
        /// <param name="paramenterName">参数名称</param>
        /// <param name="defaultValue">参数为空或者转换失败时的默认值</param>
        /// <param name="throwOnError">获取失败时是否允许抛出异常</param>
        /// <returns></returns>
        public static T HttpGetParaByName<T>(this HttpRequestBase request, string paramenterName, T defaultValue, bool throwOnError = false) where T : IConvertible
        {
            if (string.IsNullOrEmpty(paramenterName) || HttpContext.Current == null || request == null)
            {
                return defaultValue;
            }
            if (request.QueryString[paramenterName] == null)
            {
                return defaultValue;
            }
            try
            {
                return (T)Convert.ChangeType(request.QueryString[paramenterName], typeof(T));
            }
            catch
            {
                if (throwOnError)
                {
                    throw;
                }
                return defaultValue;
            }
        }
        #endregion

        #region 获取[GET]请求参数

        /// <summary>
        /// 获取[GET]请求参数
        /// </summary>
        /// <typeparam name="T">转换后的泛型类型</typeparam>
        /// <param name="request">HTTP请求</param>
        /// <param name="paramenterName">参数名称</param>
        /// <param name="converter">数据转换器委托</param>
        /// <param name="defaultValue">参数为空或者转换失败时的默认值</param>
        /// <param name="throwOnError">获取失败时是否允许抛出异常</param>
        /// <returns></returns>
        public static T HttpGetParaByName<T>(this HttpRequestBase request, string paramenterName, Func<string, T> converter, T defaultValue, bool throwOnError = false) where T : IConvertible
        {
            if (converter == null)
                throw new ArgumentNullException("Converter", "转换器参数不能为空");

            var value = request.UnSafeHttpGetParaByName(paramenterName);

            if (string.IsNullOrEmpty(value) && throwOnError == false)
            {
                return defaultValue;
            }

            try
            {
                return converter(value);
            }
            catch (Exception)
            {
                if (throwOnError)
                    throw;
                return defaultValue;
            }
        }

        /// <summary>
        /// 不验证的获取根据参数名称获取参数值的方法
        /// </summary>
        /// <param name="request">HTTP请求</param>
        /// <param name="name">参数名称</param>
        /// <returns></returns>
        public static string UnSafeHttpGetParaByName(this HttpRequestBase request, string name)
        {
            return request[name];
        }

        #endregion

        #endregion
        #region POST
        #region 获取[POST]请求参数

        /// <summary>
        /// 获取[POST]请求参数
        /// </summary>
        /// <typeparam name="T">转换后的泛型类型</typeparam>
        /// <param name="request">HTTP请求</param>
        /// <param name="paramenterName">参数名称</param>
        /// <param name="defaultValue">参数为空或者转换失败时的默认值</param>
        /// <param name="throwOnError">获取失败时是否允许抛出异常</param>
        /// <returns></returns>
        public static T HttpPostParaByName<T>(this HttpRequestBase request, string paramenterName, T defaultValue, bool throwOnError = false) where T : IConvertible
        {
            if (string.IsNullOrEmpty(paramenterName) || HttpContext.Current == null || request == null)
            {
                return defaultValue;
            }
            if (request[paramenterName] == null)
            {
                return defaultValue;
            }
            try
            {
                return (T)Convert.ChangeType(request[paramenterName], typeof(T));
            }
            catch
            {
                if (throwOnError)
                {
                    throw;
                }
                return defaultValue;
            }
        }
        #endregion
        #endregion
    }
}
