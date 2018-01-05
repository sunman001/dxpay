using System.Text;
using DxPay.Domain;
using DxPay.Infrastructure;
using DxPay.Infrastructure.Extensions;
using DxPay.Repositories.Inter;
using DxPay.Dba.Extensions;
using System.Data;
using JMP.DBA;

namespace DxPay.Repositories.Impl
{
    public class CoSettlementDeveloperAppDetailsRepository : ReportRepositoryBase, ICoSettlementDeveloperAppDetailsRepository
    {
        public IPagedList<CoSettlementDeveloperAppDetails> FindPagedList(string @where, string orderBy, int pageIndex = 0, int pageSize = 20)
        {
            /*
             ;WITH TG AS(
	SELECT 0 AS [Id],NULL AS [SettlementDay],0 AS [DeveloperId],'--' AS [DeveloperName],0 AS [AppId],'--' AS [AppName],0 AS [PayModeId],'--' AS [PayModeName],SUM([OrderCount]) AS OrderCount,SUM([TotalAmount]) AS TotalAmount,SUM([OriginServiceFee]) AS OriginServiceFee,SUM([ServiceFee]) AS ServiceFee,AVG([ServiceFeeRatio]) AS ServiceFeeRatio,0 AS [IsSpecialApproval],0 AS [SpecialApprovalServiceFee],0 AS [SpecialApprovalFeeRatio],SUM([PortFee]) AS PortFee,AVG([PortFeeRatio]) AS PortFeeRatio,NULL AS [CreatedOn],0 AS RowNum
	FROM dx_total.dbo.CoSettlementDeveloperAppDetails
), 
T0 AS(
	SELECT *,RowNum=ROW_NUMBER() OVER (ORDER BY Id DESC) FROM dx_total.dbo.CoSettlementDeveloperAppDetails AS CD
	UNION 
	SELECT * FROM TG
)
,T1 AS(
	SELECT MAX(RowNum) AS RowNumTotalCount FROM T0
)
SELECT * FROM T0,T1 WHERE T0.RowNum BETWEEN 0 AND 20
             */
            var sql = new StringBuilder();
            sql.AppendFormat(@";WITH TG AS(
	SELECT 0 AS [Id],NULL AS [SettlementDay],0 AS [DeveloperId],'--' AS [DeveloperName],0 AS [AppId],'--' AS [AppName],0 AS [PayModeId],'--' AS [PayModeName],SUM([OrderCount]) AS OrderCount,SUM([TotalAmount]) AS TotalAmount,SUM([OriginServiceFee]) AS OriginServiceFee,SUM([ServiceFee]) AS ServiceFee,AVG([ServiceFeeRatio]) AS ServiceFeeRatio,0 AS [IsSpecialApproval],SUM(SpecialApprovalServiceFee) AS [SpecialApprovalServiceFee],AVG([SpecialApprovalFeeRatio]) AS [SpecialApprovalFeeRatio],SUM([PortFee]) AS PortFee,AVG([PortFeeRatio]) AS PortFeeRatio,NULL AS [CreatedOn],0 AS RowNum
	FROM {0}.dbo.CoSettlementDeveloperAppDetails
    {1}
), 
T0 AS(
	SELECT *,RowNum=ROW_NUMBER() OVER (ORDER BY {2}) FROM {0}.dbo.CoSettlementDeveloperAppDetails AS CD
    {1}
)
,T1 AS(
	SELECT ISNULL(MAX(RowNum),0) AS RowNumTotalCount FROM T0
),T2 AS
(
SELECT * FROM TG
)
SELECT * FROM T0,T1 WHERE T0.RowNum BETWEEN {3} AND {4} UNION SELECT * FROM T2，T1 ", ReportDbName, @where.PrependWhere(), orderBy, (pageIndex * pageSize) + 1, (pageIndex + 1) * pageSize);
            return ReadPagedList<CoSettlementDeveloperAppDetails>(sql.ToString(), pageIndex, pageSize);
        }


        /// <summary>
        /// 根据条件查询账单管理结算详情（账单管理）
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public DataSet FindPagedModel(string @where)
        {
            var sql = new StringBuilder();
            sql.AppendFormat(@"select PayModeName,PortFeeRatio,ServiceFeeRatio,isnull(SUM(TotalAmount),0) as TotalAmount, isnull(SUM(PortFee),0) as PortFee, isnull(SUM(OrderCount),0) as OrderCount,
isnull(SUM(ServiceFee), 0) as ServiceFee,  from {0}.[dbo].[CoSettlementDeveloperAppDetails] {1} group by PayModeName,PortFeeRatio,ServiceFeeRatio",
ReportDbName, @where.PrependWhere());


            return DbHelperSQLTotal.Query(sql.ToString());
        }


        /// <summary>
        /// 根据条件查询商务平台首页开发者结算数据（首页统计）
        /// </summary>
        /// <param name="id">商务ID</param>
        /// <param name="date">日期</param>
        /// <param name="start">状态</param>
        /// <returns></returns>
        public JMP.MDL.CoSettlementDeveloperAppDetails GetModelKFZ(int id, string date, int start)
        {
            string time = "";
            if (start == 0)
            {
                time = " and SettlementDay='" + date + "'";
            }
            else
            {
                time = " and CONVERT(varchar(7),SettlementDay,120)='" + date + "'";
            }

            var sql = new StringBuilder();

            sql.AppendFormat(@"select ISNULL(SUM(OrderCount),0) as OrderCount,ISNULL(SUM(TotalAmount),0) as TotalAmount, 
ISNULL(SUM(BpPushMoney),0)  as BpPushMoney
from(
select ISNULL(SUM(OrderCount),0) as OrderCount,0 as TotalAmount, 
0 as BpPushMoney
from {0}.[dbo].[CoSettlementDeveloperAppDetails] a
inner join
 (select u_id from {1}.dbo.jmp_user  where relation_type=1 and relation_person_id={2}
union all 
select a.u_id from {1}.dbo.jmp_user a 
left join  {1}.dbo.CoAgent b on b.Id=a.relation_person_id
  where  a.relation_type=2 and b.OwnerId={2}
)as b on a.DeveloperId=b.u_id {3}

UNION 
select 0 as OrderCount,ISNULL(SUM(a.TotalAmount),0) as TotalAmount, 
ISNULL(SUM(a.BpPushMoney),0)  as BpPushMoney
from {0}.[dbo].[CoSettlementDeveloperOverview] a
inner join
 (select u_id from {1}.dbo.jmp_user  where relation_type=1 and relation_person_id={2}
union all 
select a.u_id from {1}.dbo.jmp_user a 
left join  {1}.dbo.CoAgent b on b.Id=a.relation_person_id
  where  a.relation_type=2 and b.OwnerId={2}
)as b on a.DeveloperId=b.u_id {3}
) as a", ReportDbName, BaseDbName, id, time);

            var ds = DbHelperSQLTotal.Query(sql.ToString());
            return ds.Tables[0].ToEntity<JMP.MDL.CoSettlementDeveloperAppDetails>();

        }


        /// <summary>
        /// 根据条件查询代理商平台首页开发者结算数据（首页统计）
        /// </summary>
        /// <param name="id">代理商ID</param>
        /// <param name="date">日期</param>
        /// <param name="start">状态</param>
        /// <returns></returns>
        public JMP.MDL.CoSettlementDeveloperAppDetails GetModelKFZAgent(int id, string date, int start)
        {
            string time = "";
            if (start == 0)
            {
                time = " and SettlementDay='" + date + "'";
            }
            else
            {
                time = " and CONVERT(varchar(7),SettlementDay,120)='" + date + "'";
            }

            var sql = new StringBuilder();

            sql.AppendFormat(@"
select ISNULL(SUM(OrderCount),0) as OrderCount,ISNULL(SUM(TotalAmount),0) as TotalAmount, 
ISNULL(SUM(AgentPushMoney),0)  as AgentPushMoney
from(
select ISNULL(SUM(OrderCount),0) as OrderCount,0 as TotalAmount, 
0 as AgentPushMoney
from {0}.[dbo].[CoSettlementDeveloperAppDetails] a
inner join
 (
select a.u_id from {1}.dbo.jmp_user a 
left join  {1}.dbo.CoAgent b on b.Id=a.relation_person_id
  where  a.relation_type=2 and b.id={2}
)as b on a.DeveloperId=b.u_id {3}
union
select 0 as OrderCount,ISNULL(SUM(a.TotalAmount),0) as TotalAmount, 
ISNULL(SUM(a.AgentPushMoney),0)  as AgentPushMoney
from {0}.[dbo].[CoSettlementDeveloperOverview] a
inner join
 (
select a.u_id from {1}.dbo.jmp_user a 
left join  {1}.dbo.CoAgent b on b.Id=a.relation_person_id
  where  a.relation_type=2 and b.id={2}
)as b on a.DeveloperId=b.u_id {3}

) as a", ReportDbName, BaseDbName, id, time);

            var ds = DbHelperSQLTotal.Query(sql.ToString());
            return ds.Tables[0].ToEntity<JMP.MDL.CoSettlementDeveloperAppDetails>();

        }
    }
}
