using System.Collections.Generic;

namespace DxPay.Infrastructure
{
    /// <summary>
    /// WHERE生成器接口
    /// </summary>
    public interface IWhereBuilder
    {
        /// <summary>
        /// 向暂存集合中追加查询条件
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        List<string> Append(string where);
        /// <summary>
        /// 返回查询条件集合
        /// </summary>
        List<string> WhereList { get; }
        /// <summary>
        /// 转换成AND连接的查询字符串
        /// </summary>
        /// <returns></returns>
        string ToWhereString();
    }
}
