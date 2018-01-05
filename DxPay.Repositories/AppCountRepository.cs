using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMP.MDL;
using System.Data;
using JMP.DbName;
using JMP.DBA;
using DxPay.Infrastructure.Extensions;
using DxPay.Infrastructure.Dba;
using DxPay.Dba.Extensions;

namespace DxPay.Repositories
{
    public class AppCountRepository : GenericRepository<jmp_appcount>, IAppCountRepository
    {
        /// <summary>
        /// 商务首页曲线图表
        /// </summary>
        /// <param name="uid">商务ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="startTimeAdy">三日开始时间</param>
        /// <param name="endTimeAdy">三日结束时间</param>
        /// <returns></returns>
        public DataSet FindPagedListSqlBp(int uid, string startTime, string endTime, string startTimeAdy, string endTimeAdy)
        {
            var sql = new StringBuilder();

            sql.AppendFormat(@" select [hour].[Hours],isnull(sum(a_success),0) as a_success,isnull(sum(a_curr),0) as a_curr,isnull(sum(b_success),0) as b_success from 
(select convert(int, DATENAME(HOUR, dateadd(hour, number, CONVERT(varchar(12), getdate(), 111)))) as [Hours] from master.dbo.spt_values  where type = 'P' 
and number < DATENAME(HOUR, '{2}') + 1) as [hour] left join 
(
select  total.a_success,total.a_curr, total.b_success, total.[HOUR] from (
select SUM( a.a_success) as a_success,SUM(a.a_curr) as a_curr,0 as b_success,DATENAME(HOUR,a_datetime) as [HOUR] 
from {5}.dbo.jmp_appcount a
inner join  
(select  u_id  from  {6}.dbo.jmp_user  where relation_person_id={0} and  relation_type=1
union all 
select a.u_id from  {6}.dbo.jmp_user a 
left join  {6}.dbo.CoAgent b on b.Id=a.relation_person_id
where  a.relation_type=2 and b.OwnerId={0}
)as b on a.a_uerid=b.u_id and a.a_datetime >= '{1}' and a.a_datetime <= '{2}' group by a.a_datetime
) as total
union all 
select  average.a_success, average.a_curr, average.b_success, average.[HOUR] from (
select  0 as  a_success,0 as a_curr,convert(decimal(18,0),(SUM(a.a_success)/3)) as b_success ,DATENAME(HOUR,a_datetime) as [HOUR]
from {5}.dbo.jmp_appcount a
inner join  
(select  u_id  from {6}.dbo.jmp_user  where relation_person_id={0} and  relation_type=1
union all 
select a.u_id from {6}.dbo.jmp_user a 
left join  {6}.dbo.CoAgent b on b.Id=a.relation_person_id
where  a.relation_type=2 and b.OwnerId={0}
)as b on a.a_uerid=b.u_id and a.a_datetime >= '{3}' and a.a_datetime <= '{4}'  group by a.a_datetime
) as average 
) as total on [hour].[Hours]= total.[HOUR] group by [hour].[Hours] order by [hour].[Hours] asc
", uid, startTime, endTime, startTimeAdy, endTimeAdy, PubDbName.dbtotal, PubDbName.dbbase);

            // return sql.ToString().QueryList<jmp_appcount>();
            return DbHelperSQLTotal.Query(sql.ToString());
        }


        /// <summary>
        /// 商务首页根据用户ID查询交易金额和交易笔数
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public jmp_appcount DataAppcountsqlBp(string t_time, int u_id)
        {
            var sql = new StringBuilder();
            sql.AppendFormat(@"select isnull(SUM(a_curr),0) as a_curr,FLOOR(isnull(SUM(a_success),0)) as a_success 
from {0}.dbo.jmp_appcount as total
inner join
(select u_id from {1}.dbo.jmp_user  where relation_type=1 and relation_person_id={2}
union all 
select a.u_id from {1}.dbo.jmp_user a 
left join  {1}.dbo.CoAgent b on b.Id=a.relation_person_id
  where  a.relation_type=2 and b.OwnerId={2}
)as userId on total.a_uerid=userId.u_id  and total.a_datetime>='{3} 00:00:00' and total.a_datetime<='{3} 23:59:59'", PubDbName.dbtotal, PubDbName.dbbase, u_id, t_time);

            var ds = DbHelperSql.Query(sql.ToString());
            return ds.Tables[0].ToEntity<jmp_appcount>();
        }


        /// <summary>
        /// 代理商首页曲线图表
        /// </summary>
        /// <param name="uid">代理商ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="startTimeAdy">三日开始时间</param>
        /// <param name="endTimeAdy">三日结束时间</param>
        /// <returns></returns>
        public DataSet FindPagedListSqlAgent(int uid, string startTime, string endTime, string startTimeAdy, string endTimeAdy)
        {
            var sql = new StringBuilder();

            sql.AppendFormat(@" select [hour].[Hours],isnull(sum(a_success),0) as a_success,isnull(sum(a_curr),0) as a_curr,isnull(sum(b_success),0) as b_success from 
(select convert(int, DATENAME(HOUR, dateadd(hour, number, CONVERT(varchar(12), getdate(), 111)))) as [Hours] from master.dbo.spt_values  where type = 'P' 
and number < DATENAME(HOUR, '{2}') + 1) as [hour] left join 
(
select  total.a_success,total.a_curr, total.b_success, total.[HOUR] from (
select SUM( a.a_success) as a_success,SUM(a.a_curr) as a_curr,0 as b_success,DATENAME(HOUR,a_datetime) as [HOUR] 
from {5}.dbo.jmp_appcount a
inner join  
(
select a.u_id from  {6}.dbo.jmp_user a 
left join  {6}.dbo.CoAgent b on b.Id=a.relation_person_id
where  a.relation_type=2 and b.id={0}
)as b on a.a_uerid=b.u_id and a.a_datetime >= '{1}' and a.a_datetime <= '{2}' group by a.a_datetime
) as total
union all 
select  average.a_success, average.a_curr, average.b_success, average.[HOUR] from (
select  0 as  a_success,0 as a_curr,convert(decimal(18,0),(SUM(a.a_success)/3)) as b_success ,DATENAME(HOUR,a_datetime) as [HOUR]
from {5}.dbo.jmp_appcount a
inner join  
(
select a.u_id from {6}.dbo.jmp_user a 
left join  {6}.dbo.CoAgent b on b.Id=a.relation_person_id
where  a.relation_type=2 and b.id={0}
)as b on a.a_uerid=b.u_id and a.a_datetime >= '{3}' and a.a_datetime <= '{4}' group by a.a_datetime
) as average 
) as total on [hour].[Hours]= total.[HOUR] group by [hour].[Hours] order by [hour].[Hours] asc
", uid, startTime, endTime, startTimeAdy, endTimeAdy, PubDbName.dbtotal, PubDbName.dbbase);

            // return sql.ToString().QueryList<jmp_appcount>();
            return DbHelperSQLTotal.Query(sql.ToString());
        }

        /// <summary>
        /// 代理商首页根据用户ID查询交易金额和交易笔数
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public jmp_appcount DataAppcountsqlAgent(string t_time, int u_id)
        {
            var sql = new StringBuilder();
            sql.AppendFormat(@"select isnull(SUM(a_curr),0) as a_curr,FLOOR(isnull(SUM(a_success),0)) as a_success 
from {0}.dbo.jmp_appcount as total
inner join
(
select a.u_id from {1}.dbo.jmp_user a 
left join  {1}.dbo.CoAgent b on b.Id=a.relation_person_id
  where  a.relation_type=2 and b.id={2}
)as userId on total.a_uerid=userId.u_id  and total.a_datetime>='{3} 00:00:00' and total.a_datetime<='{3} 23:59:59'", PubDbName.dbtotal, PubDbName.dbbase, u_id, t_time);

            var ds = DbHelperSql.Query(sql.ToString());
            return ds.Tables[0].ToEntity<jmp_appcount>();
        }


    }
}
