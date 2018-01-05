using System.Text;
using JMP.MDL;
using System.Collections.Generic;

namespace DxPay.Repositories
{
    public class TrendsRepository : ReportRepositoryBase, ITrendsRepository
    {
        public IEnumerable<jmp_trends> FindPagedListBySql(string where, string orderBy)
        {
            var sql = new StringBuilder();
            sql.AppendFormat(@" select SUM(a.t_newcount) as t_newcount, SUM(a.t_activecount) as t_activecount, CONVERT(varchar(10), a.t_time, 120) as t_time from {2}.dbo.jmp_trends a  left join  {1}.dbo.jmp_app b on b.a_id = a.t_app_id  left join  {1}.dbo.jmp_user c on c.u_id = b.a_user_id  where {0} group by a.t_time  order by a.t_time ", where, BaseDbName, ReportDbName);
            return sql.ToString().QueryList<jmp_trends>();
        }

        public IEnumerable<jmp_trends> FindPagedListByBp(string where,  string bpWhere,string agentWhere,  string orderBy)
        {
            var sql = new StringBuilder();
            sql.AppendFormat(@" select SUM(a.t_newcount) as t_newcount, SUM(a.t_activecount) as t_activecount, CONVERT(varchar(10), a.t_time, 120) as t_time from 
dx_total.dbo.jmp_trends a  left join {3}.dbo.jmp_app b on b.a_id = a.t_app_id 
inner  join (select aa.u_id,aa.u_realname,aa.DisplayName,aa.relation_type from (
select u_id,u_realname,'' as DisplayName,relation_type from  jmp_user  where {1}
union all 
(
select a.u_id,a.u_realname,c.DisplayName,a.relation_type from  {3}.dbo.jmp_user a 
left join  {3}.dbo.CoAgent c on c.Id=a.relation_person_id
where {2}
)
)  aa 
group by aa.u_id,aa.u_realname,aa.DisplayName,aa.relation_type
)  users  on users.u_id = b.a_user_id  
 where {0}
  group by a.t_time  order by a.t_time  ", where, bpWhere,agentWhere, BaseDbName, ReportDbName);
            return sql.ToString().QueryList<jmp_trends>();
        }

    }

}
