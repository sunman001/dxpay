using DxPay.Domain.Statistics;
using DxPay.Infrastructure;

namespace DxPay.Repositories
{
    public interface ICoSettlementChannelCostRepository
    {
        /// <summary>
        /// 按开发者和应用分组统计每个应用的成本
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        IPagedList<CoAppCost> FindAppCostsGroupByDeveloperIdAndAppId(string @where, string orderBy = "", int pageIndex = 0, int pageSize = 20);
        /// <summary>
        /// 按开发者分组统计每个应用的成本
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        IPagedList<CoAppCost> FindAppCostsGroupByDeveloperId(string @where, string orderBy = "", int pageIndex = 0, int pageSize = 20);

        /// <summary>
        /// 按应用分组统计应用成本
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        IPagedList<CoAppCost> FindAppCostsGroupByAppId(string @where, string orderBy = "", int pageIndex = 0, int pageSize = 20);
        /// <summary>
        /// 按开发者和通道分组统计每个通道的成本
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">分页大小</param>
        IPagedList<CoChannelCost> FindChannelCostsGroupByDeveloperIdAndChannelId(string @where, string orderBy = "", int pageIndex = 0, int pageSize = 20);
        /// <summary>
        /// 按开发者分组统计每个通道的成本
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">分页大小</param>
        IPagedList<CoChannelCost> FindChannelCostsGroupByDeveloperId(string @where, string orderBy = "", int pageIndex = 0, int pageSize = 20);

        /// <summary>
        /// 按通道分组统计每个通道的成本
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">分页大小</param>
        IPagedList<CoChannelCost> FindChannelCostsGroupByChannelId(string @where, string orderBy = "", int pageIndex = 0, int pageSize = 20);
    }
}
