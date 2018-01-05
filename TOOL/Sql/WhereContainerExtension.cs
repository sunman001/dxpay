using System;

namespace TOOL.Sql
{
    public static class WhereContainerExtension
    {
        /// <summary>
        /// 将查询条件集合转换成查询条件语句字符串(用 AND 连接)
        /// </summary>
        /// <param name="container">储存查询条件的容器</param>
        /// <param name="addPrefixWhere">是否在输出的查询条件字符串前加上 WHERE 前缀,默认:是</param>
        /// <returns></returns>
        public static string ToWhereString(this IWhereContainer container, bool addPrefixWhere = true)
        {
            if (container == null)
            {
                throw new ArgumentNullException("没有可用的查询条件");
            }
            var whereClause = string.Join(" AND ", container.Wheres);
            if (addPrefixWhere && container.Wheres.Count > 0)
            {
                whereClause = " WHERE " + whereClause;
            }
            return whereClause;
        }
    }
}
