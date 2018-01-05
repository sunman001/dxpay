using DxPay.Domain.Statistics;
using DxPay.Infrastructure;

namespace DxPay.Services
{
    public interface ICoSettlementChannelCostService
    {
        /// <summary>
        /// 按开发者和应用分组统计每个应用的成本
        /// </summary>
        /// <param name="developerId">开发者ID</param>
        /// <param name="appId">应用ID</param>
        /// <param name="settlementDayFrom">账单日期开始时间</param>
        /// <param name="settlementDayTo">账单日期结束时间</param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        IPagedList<CoAppCost> FindAppCostsGroupByDeveloperIdAndAppId(int developerId, int appId, string settlementDayFrom, string settlementDayTo, string orderBy = "", int pageIndex = 0, int pageSize = 20);
        /// <summary>
        /// 按开发者分组统计每个应用的成本
        /// </summary>
        /// <param name="developerId">开发者ID</param>
        /// <param name="appId">应用ID</param>
        /// <param name="settlementDayFrom">账单日期开始时间</param>
        /// <param name="settlementDayTo">账单日期结束时间</param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        IPagedList<CoAppCost> FindAppCostsGroupByDeveloperId(int developerId, int appId, string settlementDayFrom, string settlementDayTo,string orderBy="", int pageIndex = 0, int pageSize = 20);

        /// <summary>
        /// 按应用分组统计应用成本
        /// </summary>
        /// <param name="developerId">开发者ID</param>
        /// <param name="appId">应用ID</param>
        /// <param name="settlementDayFrom">账单日期开始时间</param>
        /// <param name="settlementDayTo">账单日期结束时间</param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        IPagedList<CoAppCost> FindAppCostsGroupByAppId(int developerId,int appId, string settlementDayFrom, string settlementDayTo, string orderBy = "", int pageIndex = 0, int pageSize = 20);

        /// <summary>
        /// 按通道分组统计每个通道的成本
        /// </summary>
        /// <param name="developerId">开发者ID</param>
        /// <param name="channelId">通道ID</param>
        /// <param name="settlementDayFrom">账单日期开始时间</param>
        /// <param name="settlementDayTo">账单日期结束时间</param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">分页大小</param>
        IPagedList<CoChannelCost> FindChannelCostsGroupByDeveloperIdAndChannelId(int developerId, int channelId, string settlementDayFrom, string settlementDayTo, string orderBy = "", int pageIndex = 0, int pageSize = 20);

        /// <summary>
        /// 按开发者分组统计每个通道的成本
        /// </summary>
        /// <param name="developerId">开发者ID</param>
        /// <param name="channelId">通道ID</param>
        /// <param name="settlementDayFrom">账单日期开始时间</param>
        /// <param name="settlementDayTo">账单日期结束时间</param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">分页大小</param>
        IPagedList<CoChannelCost> FindChannelCostsGroupByDeveloperId(int developerId, int channelId, string settlementDayFrom, string settlementDayTo, string orderBy = "", int pageIndex = 0, int pageSize = 20);

        /// <summary>
        /// 按通道分组统计每个通道的成本
        /// </summary>
        /// <param name="developerId">开发者ID</param>
        /// <param name="channelId">通道ID</param>
        /// <param name="settlementDayFrom">账单日期开始时间</param>
        /// <param name="settlementDayTo">账单日期结束时间</param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageSize">分页大小</param>
        IPagedList<CoChannelCost> FindChannelCostsGroupByChannelId(int developerId,int channelId, string settlementDayFrom, string settlementDayTo, string orderBy = "", int pageIndex = 0, int pageSize = 20);
    }
}
