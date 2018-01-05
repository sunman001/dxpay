using System.Collections.Generic;
using System.Data;

namespace JMP.BLL
{
    /// <summary>
    /// 通用查询
    /// </summary>
    public class BllCommonQuery
    {
        private readonly DAL.DalCommonQuery _dal = new DAL.DalCommonQuery();

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="order">排序字段</param>
        /// <param name="currPage">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public List<T> GetLists<T>(string sql, string order, int currPage, int pageSize, out int pageCount) where T: class
        {
            return _dal.GetLists<T>(sql, order, currPage, pageSize, out pageCount);
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。(基础数据库:dx_base)
        /// </summary>
        /// <param name="sqlStringList">多条SQL语句</param>		
        public int ExecuteSqlTranForBaseDatabase(List<string> sqlStringList)
        {
            return _dal.ExecuteSqlTranForBaseDatabase(sqlStringList);
        }

        /// <summary>
        /// 批量写入数据到指定数据库
        /// </summary>
        /// <typeparam name="T">泛型实体对象</typeparam>
        /// <param name="tableName">表名</param>
        /// <param name="list">泛型数据集合</param>
        /// <param name="batchSize">每次批量写入的行数(默认值:5000)</param>
        /// <returns></returns>
        public int BulkInsert<T>(string tableName, IEnumerable<T> list, int batchSize = 5000)
        {
            return DBA.DbHelperSQL.BulkInsert(tableName, list, batchSize);
        }

        /// <summary>
        /// 执行存储过程(基础数据库:jumipay_base)
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <returns></returns>
        public DataSet ExecuteProc(string procName)
        {
            return _dal.ExecuteProc(procName);
        }

    }
}
