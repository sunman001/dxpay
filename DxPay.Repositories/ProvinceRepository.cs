using System;
using JMP.MDL;
using DxPay.Infrastructure.Dba;
using System.Collections.Generic;
using System.Text;
using DxPay.Dba.Extensions;

namespace DxPay.Repositories
{
    public class ProvinceRepository : GenericRepository<jmp_province>, IProvinceRepository
    {
        /// <summary>
        /// 根据时间查询统计数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public jmp_province FindBytime(string where)
        {
            var sql = new StringBuilder();
            sql.AppendFormat(@"select  sum(p_count)as p_count from  {1}.dbo.jmp_province where {0}", where, ReportDbName);
            var ds = DbHelperSql.Query(sql.ToString());
            return ds.Tables[0].ToEntity<jmp_province>();
        }
        public IEnumerable<jmp_province> FindListBySql(string where, string orderBy)
        {
            var sql = new StringBuilder();
            sql.AppendFormat(@"select top 10 a.p_province,sum(a.p_count) as p_count from  {2}.dbo.jmp_province a left join {1}.dbo.jmp_app b on b.a_id=a.p_appid left join  {1}.dbo.jmp_user c on c.u_id=b.a_user_id  where {0} group by a.p_province  order by p_count desc", where, BaseDbName, ReportDbName);
            return sql.ToString().QueryList<jmp_province>();
        }

        public IEnumerable<jmp_province> FindListBySql(string where, string bpWhere, string agentWhere, string orderBy)
        {
            var sql = new StringBuilder();
            sql.AppendFormat(@"select top 10 a.p_province,sum(a.p_count) as p_count from  {2}.dbo.jmp_province a left join {1}.dbo.jmp_app b on b.a_id=a.p_appid inner  join (select aa.u_id,aa.u_realname,aa.DisplayName,aa.relation_type from (
select u_id,u_realname,'' as DisplayName,relation_type from   {1}.dbo.jmp_user  where {3}
union all 
(
select a.u_id,a.u_realname,c.DisplayName,a.relation_type from  {1}.dbo.jmp_user a 
left join {1}.dbo.CoAgent c on c.Id=a.relation_person_id
where {4}
)
)  aa 
group by aa.u_id,aa.u_realname,aa.DisplayName,aa.relation_type
)  users on users.u_id=b.a_user_id  where {0} group by a.p_province  order by p_count desc", where, BaseDbName, ReportDbName,bpWhere,agentWhere);
            return sql.ToString().QueryList<jmp_province>();
        }
    }

}
