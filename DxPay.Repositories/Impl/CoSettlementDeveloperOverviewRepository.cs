using System.Text;
using DxPay.Dba.Extensions;
using DxPay.Domain;
using DxPay.Infrastructure;
using DxPay.Infrastructure.Dba;
using DxPay.Infrastructure.Extensions;
using DxPay.Repositories.Inter;
using JMP.DBA;
using System.Data;

namespace DxPay.Repositories.Impl
{
    /// <summary>
    /// 开发者结算总揽实现
    /// </summary>
    public class CoSettlementDeveloperOverviewRepository : ReportRepositoryBase, ICoSettlementDeveloperOverviewRepository
    {
        /// <summary>
        /// 查询开发者结算总揽数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="pageIndex">当前页索引(从0开始)</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public IPagedList<CoSettlementDeveloperOverview> FindPagedList(string @where, string orderBy, int pageIndex = 0, int pageSize = 20)
        {
            var sql = new StringBuilder();
            sql.AppendFormat(@";WITH TG AS(
	SELECT 0 AS Id,0 AS DeveloperId,'合计' AS DeveloperName,NULL AS SettlementDay,NULL AS CreatedOn,SUM(TotalAmount) AS TotalAmount,SUM(ServiceFee) AS ServiceFee,AVG(ServiceFeeRatio) AS ServiceFeeRatio
	,SUM(BpPushMoney) AS BpPushMoney,AVG(BpPushMoneyRatio) AS BpPushMoneyRatio,SUM(AgentPushMoney) AS AgentPushMoney,AVG(AgentPushMoneyRatio) AS AgentPushMoneyRatio
	,SUM(PortFee) AS PortFee,SUM(CostFee) AS CostFee
	,0 AS RowNum
	FROM {0}.dbo.CoSettlementDeveloperOverview AS CO 
    {1}
),
T0 AS(
	SELECT *,RowNum=ROW_NUMBER() OVER (ORDER BY {2}) FROM {0}.dbo.CoSettlementDeveloperOverview AS CO 
    {1}
),
T1 AS(
	SELECT ISNULL(MAX(RowNum),0) AS RowNumTotalCount FROM T0
),
T2 AS(
 SELECT * FROM TG
)
SELECT * FROM T0,T1 WHERE T0.RowNum BETWEEN {3} AND {4} UNION SELECT * FROM T2,T1", ReportDbName, where.PrependWhere(), orderBy, (pageIndex * pageSize) + 1, (pageIndex + 1) * pageSize);

            return ReadPagedList<CoSettlementDeveloperOverview>(sql.ToString(), pageIndex, pageSize);
        }


        /// <summary>
        /// 根据条件查询商务下开发者结算数据(详情)
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public DataSet FindPagedListBpUid(int developerId, int searchType, string where)
        {
            var sql = new StringBuilder();

            if (searchType == 1)
            {
                sql.AppendFormat(@"select * from {0}.dbo.CoSettlementDeveloperOverview total 
inner join {1}.dbo.jmp_user users on total.DeveloperId=users.u_id 
where users.relation_type=1 
and users.relation_person_id={2} {3} ", ReportDbName, BaseDbName, developerId, where);

            }
            else
            {
                sql.AppendFormat(@"select  SettlementDay,DisplayName,BpPushMoneyRatio,ISNULL(SUM(TotalAmount),0) as TotalAmount,ISNULL(SUM(ServiceFee),0) as ServiceFee,
ISNULL(SUM(BpPushMoney),0) as BpPushMoney
 from  {0}.dbo.CoSettlementDeveloperOverview total
inner join(
select a.u_id,DisplayName
from {1}.dbo.jmp_user a 
left join  {1}.dbo.CoAgent c on c.Id=a.relation_person_id
where  a.relation_type=2 and c.OwnerId={2}
) as user_bp on total.DeveloperId=user_bp.u_id {3}
group by SettlementDay,DisplayName,BpPushMoneyRatio
", ReportDbName, BaseDbName, developerId, where);

            }

            return DbHelperSQLTotal.Query(sql.ToString());

        }


        /// <summary>
        /// 根据条件查询商务下开发者结算数据（合计）
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageIndex">当前页索引(从0开始)</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public IPagedList<JMP.MDL.CoSettlementDeveloperOverview> FindPagedListBpUid_Statistics(int developerId, string where, string orderBy, int pageIndex = 0, int pageSize = 20)
        {
            var sql = new StringBuilder();

            sql.AppendFormat(@"
WITH TG AS(
select  SettlementDay,''as DeveloperName,relation_type,ISNULL(SUM(TotalAmount),0) as TotalAmount,ISNULL(SUM(ServiceFee),0) as ServiceFee,
ISNULL(SUM(BpPushMoney),0) as BpPushMoney
from  {0}.dbo.CoSettlementDeveloperOverview total
inner join(
select a.u_id,relation_type
from {1}.dbo.jmp_user a 
left join  {1}.dbo.CoAgent c on c.Id=a.relation_person_id
where  a.relation_type=2 and c.OwnerId={2}
) as user_bp on total.DeveloperId=user_bp.u_id {3}
group by SettlementDay,relation_type
union all 
select SettlementDay,'' as DeveloperName,relation_type,ISNULL(SUM(TotalAmount),0) as TotalAmount,ISNULL(SUM(ServiceFee),0) as ServiceFee,
ISNULL(SUM(BpPushMoney),0) as BpPushMoney
from  {0}.dbo.CoSettlementDeveloperOverview total 
inner join {1}.dbo.jmp_user users on total.DeveloperId=users.u_id 
where users.relation_type=1 
and users.relation_person_id={2}{3}
  group by SettlementDay,relation_type
),
T1 AS (
 SELECT * , ROW_NUMBER() OVER (order by {4}) AS RowNum FROM TG
),T2 AS(
	SELECT ISNULL(MAX(RowNum),0) AS RowNumTotalCount FROM T1
),T3 AS(
select GETDATE() as SettlementDay,'合计'as DeveloperName,0 as relation_type,ISNULL(SUM(TotalAmount),0) as TotalAmount,ISNULL(SUM(ServiceFee),0) as ServiceFee,
ISNULL(SUM(BpPushMoney),0) as BpPushMoney, 0 as RowNum
 from TG
)

SELECT * FROM T1,T2 WHERE T1.RowNum BETWEEN {5} AND {6} UNION SELECT * FROM T3,T2", ReportDbName, BaseDbName, developerId, where, orderBy, JMP.TOOL.HtmlPage.pageIndex(pageIndex, pageSize), JMP.TOOL.HtmlPage.pageSize(pageIndex, pageSize));

            return ReadPagedList<JMP.MDL.CoSettlementDeveloperOverview>(sql.ToString(), pageIndex, pageSize);

        }


        /// <summary>
        /// 根据条件查询代理商下开发者结算数据(详情)
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public DataSet FindPagedListAgentUid(int developerId, string where)
        {
            var sql = new StringBuilder();

            sql.AppendFormat(@"select * from {0}.dbo.CoSettlementDeveloperOverview total
inner join(
select a.u_id from  
{1}.dbo.jmp_user a 
left join  {1}.dbo.CoAgent c on c.Id=a.relation_person_id
where  a.relation_type=2 and c.Id={2}
) as user_bp on total.DeveloperId=user_bp.u_id {3}", ReportDbName, BaseDbName, developerId, where);

            return DbHelperSQLTotal.Query(sql.ToString());

        }


        /// <summary>
        /// 根据条件查询代理商下开发者结算数据(合计)
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageIndex">当前页索引(从0开始)</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public IPagedList<JMP.MDL.CoSettlementDeveloperOverview> FindPagedListAgentUid_Statistics(int developerId, string where, string orderBy, int pageIndex = 0, int pageSize = 20)
        {
            var sql = new StringBuilder();

            sql.AppendFormat(@"WITH TG AS(
select SettlementDay,'' as DeveloperName,ISNULL(SUM(TotalAmount),0) as TotalAmount,ISNULL(SUM(ServiceFee),0) as ServiceFee,
ISNULL(SUM(AgentPushMoney),0) as AgentPushMoney
from  {0}.dbo.CoSettlementDeveloperOverview total
inner join(
select a.u_id from  
{1}.dbo.jmp_user a 
left join  {1}.dbo.CoAgent c on c.Id=a.relation_person_id
where  a.relation_type=2 and c.Id={2}
) as user_bp on total.DeveloperId=user_bp.u_id {3} group by SettlementDay

),
T1 AS (
SELECT * , ROW_NUMBER() OVER (order by {4}) AS RowNum FROM TG
),T2 AS(
	SELECT ISNULL(MAX(RowNum),0) AS RowNumTotalCount FROM T1
),T3 AS(
 select null as SettlementDay,'合计'as DeveloperName,ISNULL(SUM(TotalAmount),0) as TotalAmount,ISNULL(SUM(ServiceFee),0) as ServiceFee,
ISNULL(SUM(AgentPushMoney),0) as AgentPushMoney, 0 as RowNum
 from TG
)
SELECT * FROM T1,T2 WHERE T1.RowNum BETWEEN {5} AND {6} UNION SELECT * FROM T3,T2", ReportDbName, BaseDbName, developerId, where, orderBy, JMP.TOOL.HtmlPage.pageIndex(pageIndex, pageSize), JMP.TOOL.HtmlPage.pageSize(pageIndex, pageSize));

            return ReadPagedList<JMP.MDL.CoSettlementDeveloperOverview>(sql.ToString(), pageIndex, pageSize);

        }

    }


}
