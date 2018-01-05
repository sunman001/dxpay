using System.Collections.Generic;

namespace TOOL.Sql
{
    /// <summary>
    /// 储存查询条件集合的容器
    /// </summary>
    public class WhereContainer :IWhereContainer
    {
        /// <summary>
        /// 查询条件集合
        /// </summary>
        private readonly List<string> _whereClaseList;
        /// <summary>
        /// 储存查询条件集合的容器
        /// </summary>
        public WhereContainer()
        {
            _whereClaseList = new List<string>();
        }

        /// <summary>
        /// 向集合中追加查询条件
        /// </summary>
        /// <param name="whereClause">查询条件字符串</param>
        public void Append(string whereClause)
        {
            _whereClaseList.Add(whereClause);
        }

        /// <summary>
        /// 获取当容器中所有的查询条件集合
        /// </summary>
        public List<string> Wheres
        {
            get { return _whereClaseList; }
        }
    }
}
