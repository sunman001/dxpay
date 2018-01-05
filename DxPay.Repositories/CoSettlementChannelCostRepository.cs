using System.Text;
using DxPay.Domain.Statistics;
using DxPay.Infrastructure;

namespace DxPay.Repositories
{
    public class CoSettlementChannelCostRepository : ReportRepositoryBase, ICoSettlementChannelCostRepository
    {

        /// <summary>
        /// 按开发者和应用分组统计每个应用的成本
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public IPagedList<CoAppCost> FindAppCostsGroupByDeveloperIdAndAppId(string @where, string orderBy = "", int pageIndex = 0,
            int pageSize = 20)
        {
            var sql = new StringBuilder();
            sql.AppendFormat(
                @"SELECT CC.SettlementDay,CONVERT(VARCHAR(10),CC.CreatedOn,120) AS CreatedOn,CC.AppId,CC.AppName,CC.DeveloperId,CC.DeveloperName
,SUM(CC.CostFee) AS CostFee,AVG(CC.CostRatio) AS CostRatio,SUM(CC.OrderCount) AS OrderCount,SUM(CC.TotalAmount) AS TotalAmount
FROM {0}.dbo.CoSettlementChannelCost AS CC ", ReportDbName);
            if (!string.IsNullOrEmpty(@where))
            {
                sql.AppendFormat(" WHERE {0}", @where);
            }
            sql.AppendFormat(@" GROUP BY CC.SettlementDay,CC.AppId,CC.AppName,CC.DeveloperId,CC.DeveloperName,CONVERT(VARCHAR(10),CC.CreatedOn,120) ");

            var list = ReadPagedList<CoAppCost>(sql.ToString(), orderBy, pageIndex, pageSize);
            return list;
        }
        /// <summary>
        /// 按开发者分组统计每个应用的成本
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public IPagedList<CoAppCost> FindAppCostsGroupByDeveloperId(string @where, string orderBy = "", int pageIndex = 0, int pageSize = 20)
        {
            var sql = new StringBuilder();
            sql.AppendFormat(
                @"SELECT CC.SettlementDay,CONVERT(VARCHAR(10),CC.CreatedOn,120) AS CreatedOn,CC.DeveloperId,CC.DeveloperName
,SUM(CC.CostFee) AS CostFee,AVG(CC.CostRatio) AS CostRatio,SUM(CC.OrderCount) AS OrderCount,SUM(CC.TotalAmount) AS TotalAmount
FROM {0}.dbo.CoSettlementChannelCost AS CC ", ReportDbName);
            if (!string.IsNullOrEmpty(@where))
            {
                sql.AppendFormat(" WHERE {0}", @where);
            }
            sql.AppendFormat(@" GROUP BY CC.SettlementDay,CC.DeveloperId,CC.DeveloperName,CONVERT(VARCHAR(10),CC.CreatedOn,120) ");

            var list = ReadPagedList<CoAppCost>(sql.ToString(), orderBy, pageIndex, pageSize);
            return list;
        }
        /// <summary>
        /// 按应用分组统计应用成本
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public IPagedList<CoAppCost> FindAppCostsGroupByAppId(string @where, string orderBy = "", int pageIndex = 0, int pageSize = 20)
        {
            var sql = new StringBuilder();
            sql.AppendFormat(
                @";WITH T0 AS(
	SELECT CC.SettlementDay,CONVERT(VARCHAR(10),CC.CreatedOn,120) AS CreatedOn,CC.AppId,CC.AppName
,SUM(CC.CostFee) AS CostFee,AVG(CC.CostRatio) AS CostRatio,SUM(CC.OrderCount) AS OrderCount,SUM(CC.TotalAmount) AS TotalAmount
FROM {0}.dbo.CoSettlementChannelCost AS CC", ReportDbName);
            if (!string.IsNullOrEmpty(@where))
            {
                sql.AppendFormat(" WHERE {0}", @where);
            }
            sql.AppendFormat(
                @" GROUP BY CC.SettlementDay,CC.AppId,CC.AppName,CONVERT(VARCHAR(10),CC.CreatedOn,120)
),
TG AS(
	SELECT T0.SettlementDay,T0.CreatedOn,0 AS AppId,'合计' AS AppName,SUM(T0.CostFee) AS CostFee,SUM(T0.CostRatio) AS CostRatio,SUM(T0.OrderCount) AS OrderCount,SUM(T0.TotalAmount) AS TotalAmount FROM T0 
	GROUP BY T0.SettlementDay,T0.CreatedOn
),
T1 AS (
	SELECT T0.*,RowNum=ROW_NUMBER() OVER (ORDER BY T0.{0}) FROM T0 
	UNION 
	SELECT TG.*,RowNum=0 FROM TG
),
T2 AS(
	SELECT MAX(RowNum) AS RowNumTotalCount FROM T1
)
SELECT * FROM T1,T2 WHERE T1.RowNum BETWEEN {1} AND {2}", orderBy, pageIndex * pageSize, (pageIndex + 1) * pageSize);

            var list = ReadPagedList<CoAppCost>(sql.ToString(), orderBy, pageIndex, pageSize);

            return list;
        }
        /// <summary>
        /// 按开发者和通道分组统计每个通道的成本
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">分页大小</param>
        public IPagedList<CoChannelCost> FindChannelCostsGroupByDeveloperIdAndChannelId(string @where, string orderBy = "", int pageIndex = 0,
            int pageSize = 20)
        {
            var sql = new StringBuilder();
            sql.AppendFormat(
                @"SELECT CC.SettlementDay,CONVERT(VARCHAR(10),CC.CreatedOn,120) AS CreatedOn,CC.ChannelId,CC.ChannelName,CC.DeveloperId,CC.DeveloperName
,SUM(CC.CostFee) AS CostFee,AVG(CC.CostRatio) AS CostRatio,SUM(CC.OrderCount) AS OrderCount,SUM(CC.TotalAmount) AS TotalAmount
FROM {0}.dbo.CoSettlementChannelCost AS CC ", ReportDbName);
            if (!string.IsNullOrEmpty(@where))
            {
                sql.AppendFormat(" WHERE {0}", @where);
            }
            sql.AppendFormat(@" GROUP BY CC.SettlementDay,CC.ChannelId,CC.ChannelName,CC.DeveloperId,CC.DeveloperName,CONVERT(VARCHAR(10),CC.CreatedOn,120) ");

            var list = ReadPagedList<CoChannelCost>(sql.ToString(), orderBy, pageIndex, pageSize);
            return list;
        }
        /// <summary>
        /// 按开发者分组统计每个通道的成本
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">分页大小</param>
        public IPagedList<CoChannelCost> FindChannelCostsGroupByDeveloperId(string @where, string orderBy = "", int pageIndex = 0, int pageSize = 20)
        {
            var sql = new StringBuilder();
            sql.AppendFormat(
                @"SELECT CC.SettlementDay,CONVERT(VARCHAR(10),CC.CreatedOn,120) AS CreatedOn,CC.DeveloperId,CC.DeveloperName
,SUM(CC.CostFee) AS CostFee,AVG(CC.CostRatio) AS CostRatio,SUM(CC.OrderCount) AS OrderCount,SUM(CC.TotalAmount) AS TotalAmount
FROM {0}.dbo.CoSettlementChannelCost AS CC ", ReportDbName);
            if (!string.IsNullOrEmpty(@where))
            {
                sql.AppendFormat(" WHERE {0}", @where);
            }
            sql.AppendFormat(@" GROUP BY CC.SettlementDay,CC.DeveloperId,CC.DeveloperName,CONVERT(VARCHAR(10),CC.CreatedOn,120) ");

            var list = ReadPagedList<CoChannelCost>(sql.ToString(), orderBy, pageIndex, pageSize);
            return list;
        }
        /// <summary>
        /// 按通道分组统计每个通道的成本
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">分页大小</param>
        public IPagedList<CoChannelCost> FindChannelCostsGroupByChannelId(string @where, string orderBy = "", int pageIndex = 0, int pageSize = 20)
        {
            var sql = new StringBuilder();
            sql.AppendFormat(
                @"SELECT CC.SettlementDay,CONVERT(VARCHAR(10),CC.CreatedOn,120) AS CreatedOn,CC.ChannelId,CC.ChannelName
,SUM(CC.CostFee) AS CostFee,AVG(CC.CostRatio) AS CostRatio,SUM(CC.OrderCount) AS OrderCount,SUM(CC.TotalAmount) AS TotalAmount
FROM {0}.dbo.CoSettlementChannelCost AS CC ", ReportDbName);
            if (!string.IsNullOrEmpty(@where))
            {
                sql.AppendFormat(" WHERE {0}", @where);
            }
            sql.AppendFormat(@" GROUP BY CC.SettlementDay,CC.ChannelId,CC.ChannelName,CONVERT(VARCHAR(10),CC.CreatedOn,120) ");

            var list = ReadPagedList<CoChannelCost>(sql.ToString(),orderBy, pageIndex, pageSize);
            return list;
        }
    }
}
