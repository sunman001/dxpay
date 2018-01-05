using DxPay.Dba.Extensions;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace DxPay.Infrastructure.Extensions
{
    /// <summary>
    /// SQL Server 参数静态扩展类
    /// </summary>
    public static class SqlParameterExtension
    {
        /// <summary>
        /// 追加参数的静态扩展方法
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="parameters">SQL 参数数组</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public static SqlParameter[] AppendParameters<T>(this SqlParameter[] parameters, object value) where T : class
        {
            if (parameters == null)
            {
                return null;
            }
            var type = typeof(T);
            if (type == null)
            {
                throw new NullReferenceException();
            }
            var primaryKey = type.GetPrimaryKey();
            var sqlParameters = parameters.Concat(new[] { new SqlParameter("@" + primaryKey, value) }).ToArray();
            return sqlParameters;
        }
    }
}
