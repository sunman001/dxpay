using System;
using JMP.MDL;
using DxPay.Infrastructure.Dba;
using System.Data.SqlClient;
using System.Data;
using DxPay.Dba.Extensions;
using DxPay.Infrastructure;
using System.Collections.Generic;
using System.Text;
using JMP.DbName;

namespace DxPay.Repositories
{
    public class UserRepository : GenericRepository<jmp_user>, IUserRepository
    {
        public IEnumerable<jmp_user> FindListBySql(string where, string orderBy)
        {
            var sql = new StringBuilder();
            sql.AppendFormat(@"select * from  {1}.[dbo].[jmp_user]  where {0}  {2} ", where, BaseDbName, orderBy);
            return sql.ToString().QueryList<jmp_user>();
        }

        /// <summary>
        /// 根据条件查询分页数据
        /// </summary>
        /// <param name="orderBy">排序字段,不允许为空</param>
        /// <param name="where">查询条件</param>
        /// <param name="parameters">查询条件参数对象</param>
        /// <param name="pageIndex">当前页面索引</param>
        /// <param name="pageSize">分布大小</param>
        /// <returns></returns>
        IPagedList<jmp_user> IUserRepository.FindPagedListBySql( string orderBy, string sql, object parameters, int pageIndex, int pageSize)
        {
            SqlConnection con = new SqlConnection(DbHelperSql.connectionString);
            SqlDataAdapter da = new SqlDataAdapter("SqlPager", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add(new SqlParameter("@Sql", sql));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Order", orderBy));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageIndex", pageIndex));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageSize", pageSize));
            da.SelectCommand.Parameters.Add("@TotalCount", SqlDbType.Int);
            da.SelectCommand.Parameters["@TotalCount"].Direction = ParameterDirection.Output;
            DataSet ds = new DataSet();
            da.Fill(ds);
            var list = ds.Tables[0].ToList<jmp_user>();
            var totalCount = Convert.ToInt32(da.SelectCommand.Parameters["@TotalCount"].Value);
            var items = new PagedList<jmp_user>(list, pageIndex, pageSize, totalCount);
            da.Dispose();
            return items;
        }
       
    }

}
