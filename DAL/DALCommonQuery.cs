using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using JMP.DBA;

namespace JMP.DAL
{
    /// <summary>
    /// 通用查询
    /// </summary>
    public class DalCommonQuery
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sqls">查询语句</param>
        /// <param name="order">排序字段</param>
        /// <param name="pageIndexs">当前页</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCount">总条数</param>
        /// <returns></returns>
        public List<T> GetLists<T>(string sqls, string order, int pageIndexs, int pageSize, out int pageCount)
            where T : class
        {
            var sql = string.Format(sqls);
            var con = new SqlConnection(DbHelperSQL.connectionString);
            var da = new SqlDataAdapter("SqlPager", con) { SelectCommand = { CommandType = CommandType.StoredProcedure } };
            da.SelectCommand.Parameters.Add(new SqlParameter("@Sql", sql));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Order", order));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageIndex", pageIndexs));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageSize", pageSize));
            da.SelectCommand.Parameters.Add("@TotalCount", SqlDbType.Int);
            da.SelectCommand.Parameters["@TotalCount"].Direction = ParameterDirection.Output;
            var ds = new DataSet();
            da.Fill(ds);
            pageCount = Convert.ToInt32(da.SelectCommand.Parameters["@TotalCount"].Value);
            da.Dispose();
            return DbHelperSQL.ToList<T>(ds.Tables[0]);
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。(基础数据库:dx_base)
        /// </summary>
        /// <param name="sqlStringList">多条SQL语句</param>		
        public int ExecuteSqlTranForBaseDatabase(List<string> sqlStringList)
        {
            return DbHelperSQL.ExecuteSqlTran(sqlStringList);
        }

        /// <summary>
        /// 执行存储过程(基础数据库:jumipay_base)
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <returns></returns>
        public DataSet ExecuteProc(string procName)
        {
            return DbHelperSQL.Query(procName);
        }
    }
}
