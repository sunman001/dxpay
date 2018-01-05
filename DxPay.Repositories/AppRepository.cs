using System;
using JMP.MDL;
using DxPay.Infrastructure.Dba;
using System.Data.SqlClient;
using System.Data;
using DxPay.Dba.Extensions;
using DxPay.Infrastructure;
using System.Collections.Generic;
using System.Text;

namespace DxPay.Repositories
{
    public class AppRepository : GenericRepository<jmp_app>, IAppRepository
    {
        public IEnumerable<jmp_app> FindListByUserId( int classtype,int userid, string orderBy)
        {
            var sql = new StringBuilder();
            if(classtype==0)
            {
                sql.AppendFormat(@" select a.* from jmp_app a left join  jmp_user b on  a.a_user_id=b.u_id where relation_type=1 and relation_person_id='" + userid+"' {0}" ,orderBy);
            }
            else
            {
                sql.AppendFormat(@" select a.* from jmp_app a left join  jmp_user b on  a.a_user_id=b.u_id where relation_type=2 and relation_person_id='" + userid + "'{0}",orderBy);
            }
            return sql.ToString().QueryList<jmp_app>();
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
        IPagedList<jmp_app> IAppRepository.FindPagedListBySql(int userid, int type, string orderBy, string where, object parameters, int pageIndex, int pageSize)
        {
            var sql = "select a.a_id, a.a_auditstate, a.a_secretkey, a.a_time, a.a_user_id, a.a_name, a.a_platform_id, a.a_paymode_id, a.a_apptype_id,a.a_auditor, a.a_notifyurl,a.a_showurl, a.a_key, a.a_state,b.u_email,b.u_realname,(select t_name from jmp_apptype where t_id = (select t_topid from jmp_apptype where t_id = a.a_apptype_id )) as t_name,c.p_name from JMP_APP a  left join JMP_USER b on a.a_user_id = b.u_id  left join jmp_platform c on a.a_platform_id = c.p_id    where 1 = 1   and b.relation_type = '" + type+"' and b.relation_person_id = '"+userid+"'";
            sql = sql + where;
           // pageIndex += 1;
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
            var list = ds.Tables[0].ToList<jmp_app>();
            var totalCount = Convert.ToInt32(da.SelectCommand.Parameters["@TotalCount"].Value);
            var items = new PagedList<jmp_app>(list, pageIndex, pageSize, totalCount);
            da.Dispose();
            return items;
        }

       

    }

}
