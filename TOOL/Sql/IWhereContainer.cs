using System.Collections.Generic;

namespace TOOL.Sql
{
    public interface IWhereContainer
    {
        /// <summary>
        /// 向集合中追加查询条件
        /// </summary>
        /// <param name="whereClause">查询条件字符串</param>
        void Append(string whereClause);
        /// <summary>
        /// 获取当容器中所有的查询条件集合
        /// </summary>
        List<string> Wheres { get; }
    }
}
