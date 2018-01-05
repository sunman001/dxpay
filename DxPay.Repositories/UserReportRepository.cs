using System.Text;
using DxPay.Domain;
using DxPay.Infrastructure;
using DxPay.Infrastructure.Extensions;
using JMP.MDL;
using DxPay.Repositories;
using System;

namespace DxPay.Repositories
{
    public class UserReportRepository : ReportRepositoryBase, IUserReportRepository
    {
        public IPagedList<jmp_user_report> FindPagedListByBpToday(string where, string bpwhere, string agentWhere, string orderBy, object parameters = null, int pageIndex = 1, int pageSize = 20)
        {
            var sql = new StringBuilder();
            sql.AppendFormat(@"WITH TG AS(
select a_appname,a_appid,
isnull(SUM(a_equipment),0) as a_equipment,getdate() as a_time,
isnull(SUM(a_success),0) a_success,
isnull(SUM(a_notpay),0) a_notpay,
isnull(SUM(a_alipay),0) a_alipay,
isnull(SUM(a_wechat),0) a_wechat,
isnull(SUM(a_qqwallet),0) a_qqwallet,
isnull(SUM(a_count),0) a_count,
isnull(SUM(a_curr),0) a_curr,
isnull(SUM(a_successratio),0) a_successratio,
isnull(SUM(a_arpur),0) a_arpur,
isnull(SUM(a_request),0) a_request,
isnull(sum(a_unionpay),0) a_unionpay,users.u_realname ,users.DisplayName,users.relation_type 
from {3}.dbo.jmp_appcount a 
inner  join (select aa.u_id,aa.u_realname,aa.DisplayName,aa.relation_type from (
select u_id,u_realname,'' as DisplayName,relation_type from  {2}.dbo.jmp_user  where {0}
union all 
(
select a.u_id,a.u_realname,c.DisplayName,a.relation_type from {2}.dbo.jmp_user a 
left join {2}.dbo.CoAgent c on c.Id=a.relation_person_id
where {1}
)
)  aa 
group by aa.u_id,aa.u_realname,aa.DisplayName,aa.relation_type
)  users on a.a_uerid=users.u_id  {4}

 group by a_appname,users.u_realname,a_appid,users.DisplayName,users.relation_type  
 ), T1 AS(
	SELECT * , ROW_NUMBER() OVER ({5}) AS RowNum FROM TG
 ),
 T2 AS(
	SELECT MAX(RowNum) AS RowNumTotalCount FROM T1
),
T3 AS(
	select  '合计' as  a_appname, 0 as a_appid, 
	isnull(SUM(a_equipment),0) a_equipment, 
	''as a_time,
	isnull(SUM(a_success),0) a_success
	,isnull(SUM(a_notpay),0)  a_notpay,
	isnull(SUM(a_alipay),0)a_alipay,
	isnull(SUM(a_wechat),0) a_wechat,
    isnull(SUM(a_qqwallet),0) a_qqwallet,
	isnull(SUM(a_count),0) a_count,
	isnull(SUM(a_curr),0) a_curr,
	isnull(SUM(a_successratio),0) a_successratio,
	isnull(SUM(a_arpur),0) a_arpur,
	isnull(SUM(a_request),0) a_request,
	isnull(sum(a_unionpay),0) a_unionpay,
	'---'as u_realname , '---' as DisplayName, 0 as relation_type ,0 AS RowNum from TG
)
SELECT * FROM T1,T2 WHERE T1.RowNum BETWEEN {6} AND {7} 
UNION SELECT * FROM T3,T2 where RowNumTotalCount>0", bpwhere, agentWhere, BaseDbName, ReportDbName, where.PrependWhere(), orderBy, JMP.TOOL.HtmlPage.pageIndex(pageIndex, pageSize), JMP.TOOL.HtmlPage.pageSize(pageIndex, pageSize));

            return ReadPagedList<jmp_user_report>(sql.ToString(), pageIndex, pageSize);
        }

        public IPagedList<jmp_user_report> FindPagedListByBpTotal(string where, string bpwhere, string agentWhere, string orderBy, object parameters = null, int pageIndex=1, int pageSize = 20)
        {
        
            var sql = new StringBuilder();
            sql.AppendFormat(@"
WITH TG AS(
select a_appname,a_appid,
isnull(SUM(a_equipment),0) as a_equipment,a_time,
isnull(SUM(a_success),0) a_success,
isnull(SUM(a_notpay),0) a_notpay,
isnull(SUM(a_alipay),0) a_alipay,
isnull(SUM(a_wechat),0) a_wechat,
isnull(SUM(a_qqwallet),0) a_qqwallet,
isnull(SUM(a_count),0) a_count,
isnull(SUM(a_curr),0) a_curr,
isnull(SUM(a_successratio),0) a_successratio,
isnull(SUM(a_arpur),0) a_arpur,
isnull(SUM(a_request),0) a_request,
isnull(sum(a_unionpay),0) a_unionpay,users.u_realname ,users.DisplayName,users.relation_type 
from {3}.dbo.jmp_appreport a 
inner  join (select aa.u_id,aa.u_realname,aa.DisplayName,aa.relation_type from (
select u_id,u_realname,'' as DisplayName,relation_type from  {2}.dbo.jmp_user  where {0}
union all 
(
select a.u_id,a.u_realname,c.DisplayName,a.relation_type from  {2}.dbo.jmp_user a 
left join  {2}.dbo.CoAgent c on c.Id=a.relation_person_id
where {1}
)
)  aa 
group by aa.u_id,aa.u_realname,aa.DisplayName,aa.relation_type
)  users on a.a_uerid=users.u_id  {4}

 group by a_appname,users.u_realname,a_appid,a_time,users.DisplayName,users.relation_type  
 ),
T1 AS 
(SELECT * , ROW_NUMBER() OVER (order by a_appid  desc) AS RowNum FROM TG )
,T2 AS(
	SELECT MAX(RowNum) AS RowNumTotalCount FROM T1
),
T3 AS (
select  '合计' as  a_appname, 0 as a_appid, 
isnull(SUM(a_equipment),0) a_equipment, 
''as a_time,
isnull(SUM(a_success),0) a_success
,isnull(SUM(a_notpay),0)  a_notpay,
isnull(SUM(a_alipay),0)a_alipay,
isnull(SUM(a_wechat),0) a_wechat,
isnull(SUM(a_qqwallet),0) a_qqwallet,
isnull(SUM(a_count),0) a_count,
isnull(SUM(a_curr),0) a_curr,
isnull(SUM(a_successratio),0) a_successratio,
isnull(SUM(a_arpur),0) a_arpur,
isnull(SUM(a_request),0) a_request,
isnull(sum(a_unionpay),0) a_unionpay,
'---'as u_realname , '---' as DisplayName, 0 as relation_type ,0 AS RowNum from TG
)
SELECT * FROM T1,T2 WHERE T1.RowNum BETWEEN {6} AND {7}
UNION SELECT * FROM T3,T2 where RowNumTotalCount>0 {5}
", bpwhere, agentWhere, BaseDbName, ReportDbName, where.PrependWhere(), orderBy, JMP.TOOL.HtmlPage.pageIndex(pageIndex, pageSize), JMP.TOOL.HtmlPage.pageSize(pageIndex, pageSize));
            return ReadPagedList<jmp_user_report>(sql.ToString(), pageIndex, pageSize);
        }

        /// <summary>
        /// 查询今日应用报表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="parameters"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<jmp_user_report> FindPagedListByToday(string where, string orderBy, object parameters = null, int pageIndex = 0, int pageSize = 20)
        {

            var sql = new StringBuilder();
            sql.AppendFormat(@"
WITH TG AS(
select a_appname,a_appid,
isnull(SUM(a_equipment),0) as a_equipment,getdate() as a_time,
isnull(SUM(a_success),0) a_success,
isnull(SUM(a_notpay),0) a_notpay,
isnull(SUM(a_alipay),0) a_alipay,
isnull(SUM(a_wechat),0) a_wechat,
isnull(SUM(a_qqwallet),0) a_qqwallet,
isnull(SUM(a_count),0) a_count,
isnull(SUM(a_curr),0) a_curr,
isnull(SUM(a_successratio),0) a_successratio,
isnull(SUM(a_arpur),0) a_arpur,
isnull(SUM(a_request),0) a_request,
isnull(sum(a_unionpay),0) a_unionpay,b.u_realname   
from {1}.dbo.jmp_appcount a left join {0}.dbo.jmp_user b on a.a_uerid=b.u_id {2}  group by a_appname,b.u_realname,a_appid),
T1 AS 
(SELECT * , ROW_NUMBER() OVER (order by a_appid  desc) AS RowNum FROM TG )
,T2 AS(
	SELECT MAX(RowNum) AS RowNumTotalCount FROM T1
),
T3 AS (select  '合计' as  a_appname, 0 as a_appid, 
isnull(SUM(a_equipment),0) a_equipment, 
''as a_time,
isnull(SUM(a_success),0) a_success
,isnull(SUM(a_notpay),0)  a_notpay,
isnull(SUM(a_alipay),0)a_alipay,
isnull(SUM(a_wechat),0) a_wechat,
isnull(SUM(a_qqwallet),0) a_qqwallet,
isnull(SUM(a_count),0) a_count,
isnull(SUM(a_curr),0) a_curr,
isnull(SUM(a_successratio),0) a_successratio,
isnull(SUM(a_arpur),0) a_arpur,
isnull(SUM(a_request),0) a_request,
isnull(sum(a_unionpay),0) a_unionpay,
'---'as u_realname ,0 AS RowNum from TG)
SELECT * FROM T1,T2 WHERE T1.RowNum BETWEEN {4} AND {5}
UNION SELECT * FROM T3,T2 where RowNumTotalCount>0 {3}
", BaseDbName, ReportDbName, where.PrependWhere(), orderBy, JMP.TOOL.HtmlPage.pageIndex(pageIndex, pageSize), JMP.TOOL.HtmlPage.pageSize(pageIndex, pageSize));
            return ReadPagedList<jmp_user_report>(sql.ToString(), pageIndex, pageSize);
        }

        /// <summary>
        /// 查询总的应用报表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="parameters"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<jmp_user_report> FindPagedListByTotal(string where, string orderBy, object parameters = null, int pageIndex = 0, int pageSize = 20)
        {
            var sql = new StringBuilder();
            sql.AppendFormat(@"
WITH TG AS(
select a_appname,a_appid,
isnull(SUM(a_equipment),0) as a_equipment,a_time,
isnull(SUM(a_success),0) a_success,
isnull(SUM(a_notpay),0) a_notpay,
isnull(SUM(a_alipay),0) a_alipay,
isnull(SUM(a_wechat),0) a_wechat,
isnull(SUM(a_qqwallet),0) a_qqwallet,
isnull(SUM(a_count),0) a_count,
isnull(SUM(a_curr),0) a_curr,
isnull(SUM(a_successratio),0) a_successratio,
isnull(SUM(a_arpur),0) a_arpur,
isnull(SUM(a_request),0) a_request,
isnull(sum(a_unionpay),0) a_unionpay,b.u_realname   
from {1}.dbo.jmp_appreport a left join {0}.dbo.jmp_user b on a.a_uerid=b.u_id {2}  group by a_appname,b.u_realname,a_appid,a_time),
T1 AS 
( SELECT * , ROW_NUMBER() OVER ({3}) AS RowNum FROM TG )
,T2 AS(
	SELECT MAX(RowNum) AS RowNumTotalCount FROM T1
),T3 AS (
select  '合计' as  a_appname, 0 as a_appid, 
isnull(SUM(a_equipment),0) a_equipment, 
''as a_time,
isnull(SUM(a_success),0) a_success
,isnull(SUM(a_notpay),0)  a_notpay,
isnull(SUM(a_alipay),0)a_alipay,
isnull(SUM(a_wechat),0) a_wechat,
isnull(SUM(a_qqwallet),0) a_qqwallet,
isnull(SUM(a_count),0) a_count,
isnull(SUM(a_curr),0) a_curr,
isnull(SUM(a_successratio),0) a_successratio,
isnull(SUM(a_arpur),0) a_arpur,
isnull(SUM(a_request),0) a_request,
isnull(sum(a_unionpay),0) a_unionpay,
'---'as u_realname ,0 AS RowNum from TG
)
SELECT * FROM T1,T2 WHERE T1.RowNum BETWEEN {4} AND {5}
UNION SELECT * FROM T3,T2 where RowNumTotalCount>0
", BaseDbName, ReportDbName, where.PrependWhere(), orderBy, JMP.TOOL.HtmlPage.pageIndex(pageIndex, pageSize), JMP.TOOL.HtmlPage.pageSize(pageIndex, pageSize));
            return ReadPagedList<jmp_user_report>(sql.ToString(), pageIndex, pageSize);
        }
    }

}
