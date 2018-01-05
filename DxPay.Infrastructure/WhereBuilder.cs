using System.Collections.Generic;

namespace DxPay.Infrastructure
{
    /// <summary>
    /// WHERE子句生成器
    /// </summary>
    public class WhereBuilder :IWhereBuilder
   {
        /// <summary>
        /// 暂存多条查询条件的集合
        /// </summary>
       private readonly List<string> _whereList;
        public WhereBuilder()
        {
            _whereList = new List<string>();
        }

        /// <summary>
        /// 向暂存集合中追加查询条件
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public List<string> Append(string @where)
        {
            _whereList.Add(@where);
            return WhereList;
        }

        /// <summary>
        /// 返回查询条件集合
        /// </summary>
       public List<string> WhereList
       {
           get { return _whereList; }
       }

        /// <summary>
        /// 转换成AND连接的查询字符串
        /// </summary>
        /// <returns></returns>
       public string ToWhereString()
       {
           return string.Join(" AND ", _whereList);
       }
   }
}
