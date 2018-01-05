namespace DxPay.Infrastructure.Extensions
{
    /// <summary>
    /// SQL WHERE查询生成器静态扩展类
    /// </summary>
    public static class SqlWhereBuilderExtension
    {
        /// <summary>
        /// 向生成器中追加查询条件
        /// </summary>
        /// <param name="wheres">生成器对象</param>
        /// <param name="anotherWhere">新增的查询条件</param>
        /// <returns></returns>
        public static IWhereBuilder Append(this IWhereBuilder wheres, string anotherWhere)
        {
            wheres.Append(anotherWhere);
            return wheres;
        }

        /// <summary>
        /// 生成WHERE查询语句字符串
        /// </summary>
        /// <param name="where">WHERE查询条件</param>
        /// <returns></returns>
        public static string PrependWhere(this string @where)
        {
            if (!string.IsNullOrEmpty(@where))
            {
                where = " WHERE " + where;
            }
            return where;
        }
    }
}
