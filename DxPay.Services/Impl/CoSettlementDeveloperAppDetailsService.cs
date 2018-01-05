using DxPay.Domain;
using DxPay.Infrastructure;
using DxPay.Repositories.Inter;
using DxPay.Services.Inter;
using System.Data;

namespace DxPay.Services.Impl
{
    public class CoSettlementDeveloperAppDetailsService : ICoSettlementDeveloperAppDetailsService
    {
        private readonly ICoSettlementDeveloperAppDetailsRepository _repository;

        public CoSettlementDeveloperAppDetailsService(ICoSettlementDeveloperAppDetailsRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// 查询所有数据(分页)
        /// </summary>
        /// <param name="orderBy">排序条件</param>
        /// <param name="settlementDayFrom">账单开始日期</param>
        /// <param name="settlementDayTo">账单结束日期</param>
        /// <param name="pageIndex">当前页索引(从0开始)</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public IPagedList<CoSettlementDeveloperAppDetails> FindAll(string orderBy = "Id DESC", string settlementDayFrom = "", string settlementDayTo = "",
            int pageIndex = 0, int pageSize = 20)
        {
            var whereBuilder = WhereBuilderFactory.Create();
            if (!string.IsNullOrEmpty(settlementDayFrom))
            {
                whereBuilder.Append(string.Format("SettlementDay>='{0}'", settlementDayFrom));
            }
            if (!string.IsNullOrEmpty(settlementDayTo))
            {
                whereBuilder.Append(string.Format("SettlementDay<='{0}'", settlementDayTo));
            }
            return _repository.FindPagedList(whereBuilder.ToWhereString(), orderBy, pageIndex, pageSize);
        }

        /// <summary>
        /// 查询指定开发者的结算详情数据(分页)
        /// </summary>
        /// <param name="developerId">开发者ID</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="settlementDayFrom">账单开始日期</param>
        /// <param name="settlementDayTo">账单结束日期</param>
        /// <param name="pageIndex">当前页索引(从0开始)</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public IPagedList<CoSettlementDeveloperAppDetails> FindPagedListByDeveloperId(int developerId, string orderBy = "Id DESC", string settlementDayFrom = "",
            string settlementDayTo = "", int pageIndex = 0, int pageSize = 20)
        {
            var whereBuilder = WhereBuilderFactory.Create();
            if (developerId > 0)
            {
                whereBuilder.Append(string.Format("DeveloperId={0}", developerId));
            }
            if (!string.IsNullOrEmpty(settlementDayFrom))
            {
                whereBuilder.Append(string.Format("SettlementDay>='{0}'", settlementDayFrom));
            }
            if (!string.IsNullOrEmpty(settlementDayTo))
            {
                whereBuilder.Append(string.Format("SettlementDay<='{0}'", settlementDayTo));
            }
            return _repository.FindPagedList(whereBuilder.ToWhereString(), orderBy, pageIndex, pageSize);
        }

        /// <summary>
        /// 查询指定开发者的结算详情数据(分页)
        /// </summary>
        /// <param name="developerName">开发者ID</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="settlementDayFrom">账单开始日期</param>
        /// <param name="settlementDayTo">账单结束日期</param>
        /// <param name="pageIndex">当前页索引(从0开始)</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public IPagedList<CoSettlementDeveloperAppDetails> FindPagedListByDeveloperName(string developerName, string orderBy = "Id DESC", string settlementDayFrom = "",
            string settlementDayTo = "", int pageIndex = 0, int pageSize = 20)
        {
            var whereBuilder = WhereBuilderFactory.Create();
            if (!string.IsNullOrEmpty(developerName))
            {
                whereBuilder.Append(string.Format("DeveloperName LIKE '%{0}%'", developerName.Replace("'", "''")));
            }
            if (!string.IsNullOrEmpty(settlementDayFrom))
            {
                whereBuilder.Append(string.Format("SettlementDay>='{0}'", settlementDayFrom));
            }
            if (!string.IsNullOrEmpty(settlementDayTo))
            {
                whereBuilder.Append(string.Format("SettlementDay<='{0}'", settlementDayTo));
            }
            return _repository.FindPagedList(whereBuilder.ToWhereString(), orderBy, pageIndex, pageSize);
        }

        /// <summary>
        /// 查询指定应用ID的结算详情数据(分页)
        /// </summary>
        /// <param name="appId">应用ID</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="settlementDayFrom">账单开始日期</param>
        /// <param name="settlementDayTo">账单结束日期</param>
        /// <param name="pageIndex">当前页索引(从0开始)</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public IPagedList<CoSettlementDeveloperAppDetails> FindPagedListByAppId(int appId, string orderBy = "Id DESC", string settlementDayFrom = "",
            string settlementDayTo = "", int pageIndex = 0, int pageSize = 20)
        {
            var whereBuilder = WhereBuilderFactory.Create();
            if (appId > 0)
            {
                whereBuilder.Append(string.Format("AppId>{0}", appId));
            }
            if (!string.IsNullOrEmpty(settlementDayFrom))
            {
                whereBuilder.Append(string.Format("SettlementDay>='{0}'", settlementDayFrom));
            }
            if (!string.IsNullOrEmpty(settlementDayTo))
            {
                whereBuilder.Append(string.Format("SettlementDay<='{0}'", settlementDayTo));
            }
            return _repository.FindPagedList(whereBuilder.ToWhereString(), orderBy, pageIndex, pageSize);
        }

        /// <summary>
        /// 查询指定应用名称的结算详情数据(分页)
        /// </summary>
        /// <param name="appName">应用名称</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="settlementDayFrom">账单开始日期</param>
        /// <param name="settlementDayTo">账单结束日期</param>
        /// <param name="pageIndex">当前页索引(从0开始)</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public IPagedList<CoSettlementDeveloperAppDetails> FindPagedListByAppName(string appName, string orderBy = "Id DESC", string settlementDayFrom = "",
            string settlementDayTo = "", int pageIndex = 0, int pageSize = 20)
        {
            var whereBuilder = WhereBuilderFactory.Create();
            if (!string.IsNullOrEmpty(appName))
            {
                whereBuilder.Append(string.Format("AppName LIKE '%{0}%'", appName.Replace("'", "''")));
            }
            if (!string.IsNullOrEmpty(settlementDayFrom))
            {
                whereBuilder.Append(string.Format("SettlementDay>='{0}'", settlementDayFrom));
            }
            if (!string.IsNullOrEmpty(settlementDayTo))
            {
                whereBuilder.Append(string.Format("SettlementDay<='{0}'", settlementDayTo));
            }
            return _repository.FindPagedList(whereBuilder.ToWhereString(), orderBy, pageIndex, pageSize);
        }

        /// <summary>
        /// 查询指定支付方式ID的结算详情数据(分页)
        /// </summary>
        /// <param name="payModeId">支付方式ID</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="settlementDayFrom">账单开始日期</param>
        /// <param name="settlementDayTo">账单结束日期</param>
        /// <param name="pageIndex">当前页索引(从0开始)</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public IPagedList<CoSettlementDeveloperAppDetails> FindPagedListByPayModeId(int payModeId, string orderBy = "Id DESC", string settlementDayFrom = "",
            string settlementDayTo = "", int pageIndex = 0, int pageSize = 20)
        {
            var whereBuilder = WhereBuilderFactory.Create();
            if (payModeId > 0)
            {
                whereBuilder.Append(string.Format("PayModeId>{0}", payModeId));
            }
            if (!string.IsNullOrEmpty(settlementDayFrom))
            {
                whereBuilder.Append(string.Format("SettlementDay>='{0}'", settlementDayFrom));
            }
            if (!string.IsNullOrEmpty(settlementDayTo))
            {
                whereBuilder.Append(string.Format("SettlementDay<='{0}'", settlementDayTo));
            }
            return _repository.FindPagedList(whereBuilder.ToWhereString(), orderBy, pageIndex, pageSize);
        }

        /// <summary>
        /// 查询指定支付方式名称的结算详情数据(分页)
        /// </summary>
        /// <param name="payModeName">支付方式名称</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="settlementDayFrom">账单开始日期</param>
        /// <param name="settlementDayTo">账单结束日期</param>
        /// <param name="pageIndex">当前页索引(从0开始)</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public IPagedList<CoSettlementDeveloperAppDetails> FindPagedListByPayModeName(string payModeName, string orderBy = "Id DESC", string settlementDayFrom = "",
            string settlementDayTo = "", int pageIndex = 0, int pageSize = 20)
        {
            var whereBuilder = WhereBuilderFactory.Create();
            if (!string.IsNullOrEmpty(payModeName))
            {
                whereBuilder.Append(string.Format("PayModeName LIKE '%{0}%'", payModeName.Replace("'", "''")));
            }
            if (!string.IsNullOrEmpty(settlementDayFrom))
            {
                whereBuilder.Append(string.Format("SettlementDay>='{0}'", settlementDayFrom));
            }
            if (!string.IsNullOrEmpty(settlementDayTo))
            {
                whereBuilder.Append(string.Format("SettlementDay<='{0}'", settlementDayTo));
            }
            return _repository.FindPagedList(whereBuilder.ToWhereString(), orderBy, pageIndex, pageSize);
        }


        /// <summary>
        /// 查询所有数据(分页)
        /// </summary>
        /// <param name="developerId">开发者ID</param>
        /// <param name="developerName">开发者名称</param>
        /// <param name="appId">应用ID</param>
        /// <param name="appName">应用名称</param>
        /// <param name="payModeId">支付方式ID</param>
        /// <param name="payModeName">支付方式名称</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="settlementDayFrom">账单开始日期</param>
        /// <param name="settlementDayTo">账单结束日期</param>
        /// <param name="pageIndex">当前页索引(从0开始)</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public IPagedList<CoSettlementDeveloperAppDetails> FindAll(int developerId = 0, string developerName = "", int appId = 0, string appName = "",
            int payModeId = 0, string payModeName = "", string orderBy = "Id DESC", string settlementDayFrom = "",
            string settlementDayTo = "", int pageIndex = 0, int pageSize = 20)
        {
            var whereBuilder = WhereBuilderFactory.Create();
            if (developerId > 0)
            {
                whereBuilder.Append(string.Format("DeveloperId>{0}", developerId));
            }
            if (!string.IsNullOrEmpty(developerName))
            {
                whereBuilder.Append(string.Format("DeveloperName LIKE '%{0}%'", developerName.Replace("'", "''")));
            }
            if (appId > 0)
            {
                whereBuilder.Append(string.Format("AppId>{0}", appId));
            }
            if (!string.IsNullOrEmpty(appName))
            {
                whereBuilder.Append(string.Format("AppName LIKE '%{0}%'", appName.Replace("'", "''")));
            }
            if (payModeId > 0)
            {
                whereBuilder.Append(string.Format("PayModeId>{0}", payModeId));
            }
            if (!string.IsNullOrEmpty(payModeName))
            {
                whereBuilder.Append(string.Format("PayModeName LIKE '%{0}%'", payModeName.Replace("'", "''")));
            }
            if (!string.IsNullOrEmpty(settlementDayFrom))
            {
                whereBuilder.Append(string.Format("SettlementDay>='{0}'", settlementDayFrom));
            }
            if (!string.IsNullOrEmpty(settlementDayTo))
            {
                whereBuilder.Append(string.Format("SettlementDay<='{0}'", settlementDayTo));
            }
            return _repository.FindPagedList(whereBuilder.ToWhereString(), orderBy, pageIndex, pageSize);
        }

        /// <summary>
        /// 根据条件查询结算详情
        /// </summary>
        /// <param name="id">开发者ID</param>
        /// <param name="date">结算日期</param>
        /// <returns></returns>
        public DataSet FindPagedListByDeveloperModel(int id, string date)
        {
            var whereBuilder = WhereBuilderFactory.Create();

            if (id > 0)
            {
                whereBuilder.Append(string.Format("DeveloperId={0}", id));
            }
            if (!string.IsNullOrEmpty(date))
            {
                whereBuilder.Append(string.Format("SettlementDay ='{0}'", date));
            }

            return _repository.FindPagedModel(whereBuilder.ToWhereString());
        }


        /// <summary>
        /// 商务平台首页统计
        /// </summary>
        /// <param name="id">商务ID</param>
        /// <param name="date">查询日期</param>
        /// <param name="start">状态判断（昨日，月，上月）</param>
        /// <returns></returns>
        public JMP.MDL.CoSettlementDeveloperAppDetails FindPagedListByDeveloperKFZ(int id, string date, int start)
        {

            return _repository.GetModelKFZ(id, date, start);
        }


        /// <summary>
        /// 代理商平台首页统计
        /// </summary>
        /// <param name="id">代理商ID</param>
        /// <param name="date">查询日期</param>
        /// <param name="start">状态判断（昨日，月，上月）</param>
        /// <returns></returns>
        public JMP.MDL.CoSettlementDeveloperAppDetails FindPagedListByDeveloperKFZAgent(int id, string date, int start)
        {

            return _repository.GetModelKFZAgent(id, date, start);
        }

    }
}
