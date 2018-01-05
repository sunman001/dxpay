using System;
using JMP.MDL;
using DxPay.Infrastructure.Dba;
using System.Data.SqlClient;
using System.Data;
using DxPay.Dba.Extensions;
using DxPay.Infrastructure;
using JMP.DBA;
using System.Collections.Generic;

namespace DxPay.Repositories
{
    public class OrderRepository : GenericRepository<jmp_order>, IOrderRepository
    {
        public IPagedList<jmp_order> FindPagedListByBP(string orderBy, string sql, string where, string bpwhere, string agentwhere, object parameters = null, int pageIndex = 0, int pageSize = 20)
        {
            SqlConnection con = new SqlConnection(DbHelperSQL.connectionString);
            SqlDataAdapter da = new SqlDataAdapter("BpOrderList", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add(new SqlParameter("@where", where));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Bpwhere", bpwhere));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Agentwhere", agentwhere));
            da.SelectCommand.Parameters.Add(new SqlParameter("@sql1", sql));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Order", orderBy));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageIndex", pageIndex));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageSize", pageSize));
            int count = 0;
            DataSet ds = new DataSet();
            da.Fill(ds);
            var list=new List<jmp_order>();
           
            if (ds.Tables.Count > 0)
            {
                list = ds.Tables[0].ToList<jmp_order>();
                try
                {
                    count = int.Parse(ds.Tables[0].Rows[0]["TotalCount"].ToString());
                }
                catch { }
            }
         
            var items = new PagedList<jmp_order>(list, pageIndex-1, pageSize,count);
            da.Dispose();
            return items;
        }

        public IPagedList<jmp_order> FindPagedListBySql(string orderBy, string sql, string where, string agentwhere, object parameters = null, int pageIndex = 0, int pageSize = 20)
        {
            SqlConnection con = new SqlConnection(DbHelperSQL.connectionString);
            SqlDataAdapter da = new SqlDataAdapter("AgentOrderList", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add(new SqlParameter("@where", where));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Agentwhere", agentwhere));
            da.SelectCommand.Parameters.Add(new SqlParameter("@sql1", sql));
            da.SelectCommand.Parameters.Add(new SqlParameter("@Order", orderBy));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageIndex", pageIndex));
            da.SelectCommand.Parameters.Add(new SqlParameter("@PageSize", pageSize));
            // da.SelectCommand.Parameters.Add("@TotalCount", SqlDbType.Int);
            // da.SelectCommand.Parameters["@TotalCount"].Direction = ParameterDirection.Output;
            int count = 0;
            DataSet ds = new DataSet();
            da.Fill(ds);
            var list = new List<jmp_order>();

            if (ds.Tables.Count > 0)
            {
                list = ds.Tables[0].ToList<jmp_order>();
                try
                {
                    if(list.Count>0)
                    {
                     count = int.Parse(ds.Tables[0].Rows[0]["TotalCount"].ToString());
                    }
                   
                }
                catch {
                    throw new Exception("table无数据");
                }
            }
            var items = new PagedList<jmp_order>(list, pageIndex-1, pageSize, count);
            da.Dispose();
             return items; 
        }
    }

}
