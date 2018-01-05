using System.Collections.Generic;
using DxPay.Domain.Statistics;
using DxPay.Infrastructure;
using DxPay.Repositories;

namespace DxPay.Services
{
    public class CoSettlementChannelCostService : ICoSettlementChannelCostService
    {
        private readonly ICoSettlementChannelCostRepository _repository;

        public CoSettlementChannelCostService(ICoSettlementChannelCostRepository repository)
        {
            _repository = repository;
        }
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
        public IPagedList<CoAppCost> FindAppCostsGroupByDeveloperIdAndAppId(int developerId, int appId, string settlementDayFrom,
            string settlementDayTo, string orderBy = "", int pageIndex = 0, int pageSize = 20)
        {
            var where = new List<string>();
            if (developerId > 0)
            {
                where.Add(string.Format("CC.DeveloperId={0}", developerId));
            }
            if (appId > 0)
            {
                where.Add(string.Format("CC.AppId={0}", appId));
            }
            if (!string.IsNullOrEmpty(settlementDayFrom))
            {
                where.Add(string.Format("CC.SettlementDay>='{0}'", settlementDayFrom));
            }
            if (!string.IsNullOrEmpty(settlementDayTo))
            {
                where.Add(string.Format("CC.SettlementDay<='{0}'", settlementDayTo));
            }
            return _repository.FindAppCostsGroupByDeveloperIdAndAppId(string.Join(" AND ", where), orderBy,pageIndex,pageSize);
        }

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
        public IPagedList<CoAppCost> FindAppCostsGroupByDeveloperId(int developerId, int appId, string settlementDayFrom, string settlementDayTo,
            string orderBy = "", int pageIndex = 0, int pageSize = 20)
        {
            var where = new List<string>();
            if (developerId > 0)
            {
                where.Add(string.Format("CC.DeveloperId={0}", developerId));
            }
            if (appId > 0)
            {
                where.Add(string.Format("CC.AppId={0}", appId));
            }
            if (!string.IsNullOrEmpty(settlementDayFrom))
            {
                where.Add(string.Format("CC.SettlementDay>='{0}'", settlementDayFrom));
            }
            if (!string.IsNullOrEmpty(settlementDayTo))
            {
                where.Add(string.Format("CC.SettlementDay<='{0}'", settlementDayTo));
            }
            return _repository.FindAppCostsGroupByDeveloperId(string.Join(" AND ", where), orderBy, pageIndex, pageSize);
        }

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
        public IPagedList<CoAppCost> FindAppCostsGroupByAppId(int developerId, int appId, string settlementDayFrom, string settlementDayTo, string orderBy = "",
            int pageIndex = 0, int pageSize = 20)
        {
            var where = new List<string>();
            if (developerId > 0)
            {
                where.Add(string.Format("CC.DeveloperId={0}", developerId));
            }
            if (appId > 0)
            {
                where.Add(string.Format("CC.AppId={0}", appId));
            }
            if (!string.IsNullOrEmpty(settlementDayFrom))
            {
                where.Add(string.Format("CC.SettlementDay>='{0}'", settlementDayFrom));
            }
            if (!string.IsNullOrEmpty(settlementDayTo))
            {
                where.Add(string.Format("CC.SettlementDay<='{0}'", settlementDayTo));
            }
            return _repository.FindAppCostsGroupByAppId(string.Join(" AND ", where), orderBy, pageIndex, pageSize);
        }

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
        public IPagedList<CoChannelCost> FindChannelCostsGroupByDeveloperIdAndChannelId(int developerId, int channelId, string settlementDayFrom,
            string settlementDayTo, string orderBy = "", int pageIndex = 0, int pageSize = 20)
        {
            var where = new List<string>();
            if (developerId > 0)
            {
                where.Add(string.Format("CC.DeveloperId={0}", developerId));
            }
            if (channelId > 0)
            {
                where.Add(string.Format("CC.ChannelId={0}", channelId));
            }
            if (!string.IsNullOrEmpty(settlementDayFrom))
            {
                where.Add(string.Format("CC.SettlementDay>='{0}'", settlementDayFrom));
            }
            if (!string.IsNullOrEmpty(settlementDayTo))
            {
                where.Add(string.Format("CC.SettlementDay<='{0}'", settlementDayTo));
            }
            return _repository.FindChannelCostsGroupByDeveloperIdAndChannelId(string.Join(" AND ", where), orderBy, pageIndex, pageSize);
        }

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
        public IPagedList<CoChannelCost> FindChannelCostsGroupByDeveloperId(int developerId, int channelId, string settlementDayFrom,
            string settlementDayTo, string orderBy = "", int pageIndex = 0, int pageSize = 20)
        {
            var where = new List<string>();
            if (developerId > 0)
            {
                where.Add(string.Format("CC.DeveloperId={0}", developerId));
            }
            if (channelId > 0)
            {
                where.Add(string.Format("CC.ChannelId={0}", channelId));
            }
            if (!string.IsNullOrEmpty(settlementDayFrom))
            {
                where.Add(string.Format("CC.SettlementDay>='{0}'", settlementDayFrom));
            }
            if (!string.IsNullOrEmpty(settlementDayTo))
            {
                where.Add(string.Format("CC.SettlementDay<='{0}'", settlementDayTo));
            }
            return _repository.FindChannelCostsGroupByDeveloperIdAndChannelId(string.Join(" AND ", where), orderBy, pageIndex, pageSize);
        }

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
        public IPagedList<CoChannelCost> FindChannelCostsGroupByChannelId(int developerId, int channelId, string settlementDayFrom, string settlementDayTo,
            string orderBy = "", int pageIndex = 0, int pageSize = 20)
        {
            var where = new List<string>();
            if (developerId > 0)
            {
                where.Add(string.Format("CC.DeveloperId={0}", developerId));
            }
            if (channelId > 0)
            {
                where.Add(string.Format("CC.ChannelId={0}", channelId));
            }
            if (!string.IsNullOrEmpty(settlementDayFrom))
            {
                where.Add(string.Format("CC.SettlementDay>='{0}'", settlementDayFrom));
            }
            if (!string.IsNullOrEmpty(settlementDayTo))
            {
                where.Add(string.Format("CC.SettlementDay<='{0}'", settlementDayTo));
            }
            return _repository.FindChannelCostsGroupByDeveloperIdAndChannelId(string.Join(" AND ", where), orderBy, pageIndex, pageSize);
        }
    }
}
