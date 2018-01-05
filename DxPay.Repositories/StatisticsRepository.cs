using System;
using JMP.MDL;
using DxPay.Infrastructure.Dba;
using System.Collections.Generic;
using System.Text;
using DxPay.Dba.Extensions;

namespace DxPay.Repositories
{
    public class StatisticsRepository : GenericRepository<jmp_statistics>, IStatisticsRepository
    {
        /// <summary>
        /// 根据时间查询统计数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public jmp_statistics FindBytime(string where)
        {
            var sql = new StringBuilder();
            sql.AppendFormat(@"select sum(s_count) as s_count  from {1}.dbo.jmp_statistics  where {0}", where, ReportDbName);
            var ds = DbHelperSql.Query(sql.ToString());
            return ds.Tables[0].ToEntity<jmp_statistics>();
        }
        public IEnumerable<jmp_statistics> FindListBySql(string where, string orderBy)
        {
            var sql = new StringBuilder();
            sql.AppendFormat(@"  select top 10 a.s_brand,sum(a.s_count)as s_count from  {2}.dbo.jmp_statistics  a left join  {1}.dbo.jmp_app b on b.a_id=a.s_app_id left join  {1}.dbo.jmp_user c on c.u_id=b.a_user_id  where {0} group by a.s_brand  order by s_count desc ", where, BaseDbName, ReportDbName);
            return sql.ToString().QueryList<jmp_statistics>();
        }

        public IEnumerable<jmp_statistics> FindListBySql(string where, string bpWhere, string agentWhere, string orderBy)
        {
            var sql = new StringBuilder();
            sql.AppendFormat(@"  select top 10 a.s_brand,sum(a.s_count)as s_count from  {2}.dbo.jmp_statistics  a left join  {1}.dbo.jmp_app b on b.a_id=a.s_app_id inner  join (select aa.u_id,aa.u_realname,aa.DisplayName,aa.relation_type from (
            select u_id,u_realname,'' as DisplayName,relation_type from   {1}.dbo.jmp_user  where {3}
union all 
(
select a.u_id,a.u_realname,c.DisplayName,a.relation_type from   {1}.dbo.jmp_user a 
left join {1}.dbo.CoAgent c on c.Id=a.relation_person_id
where {4}
)
)  aa 
group by aa.u_id,aa.u_realname,aa.DisplayName,aa.relation_type
)  users on users.u_id=b.a_user_id  where {0} group by a.s_brand  order by s_count desc ", where, BaseDbName, ReportDbName, bpWhere, agentWhere);
            return sql.ToString().QueryList<jmp_statistics>();
        }
    }

}
