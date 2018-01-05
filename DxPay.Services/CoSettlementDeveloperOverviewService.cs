using DxPay.Domain;
using DxPay.Infrastructure;
using DxPay.Repositories;
using DxPay.Repositories.Inter;
using System.Data;

namespace DxPay.Services
{
    public class CoSettlementDeveloperOverviewService : ICoSettlementDeveloperOverviewService
    {
        private readonly ICoSettlementDeveloperOverviewRepository _repository;

        public CoSettlementDeveloperOverviewService(ICoSettlementDeveloperOverviewRepository repository)
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
        public IPagedList<CoSettlementDeveloperOverview> FindAll(string orderBy = "Id DESC", string settlementDayFrom = "", string settlementDayTo = "",
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
        /// 根据开发者ID查询
        /// </summary>
        /// <param name="developerId">开发者ID</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="settlementDayFrom">账单开始日期</param>
        /// <param name="settlementDayTo">账单结束日期</param>
        /// <param name="pageIndex">当前页索引(从0开始)</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public IPagedList<CoSettlementDeveloperOverview> FindPagedListByDeveloperId(int developerId, string orderBy = "Id DESC", string settlementDayFrom = "",
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
        /// 根据开发者姓名查询
        /// </summary>
        /// <param name="developerName">开发者姓名</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="settlementDayFrom">账单开始日期</param>
        /// <param name="settlementDayTo">账单结束日期</param>
        /// <param name="pageIndex">当前页索引(从0开始)</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>

        public IPagedList<CoSettlementDeveloperOverview> FindPagedListByDeveloperName(string developerName, string orderBy = "Id DESC", string settlementDayFrom = "", string settlementDayTo = "", int pageIndex = 0, int pageSize = 20)
        {
            var whereBuilder = WhereBuilderFactory.Create();
            if (!string.IsNullOrEmpty(developerName))
            {
                whereBuilder.Append(string.Format("DeveloperName='{0}'", developerName.Replace("'", "''")));
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
        /// 根据商务ID查询账单管理（详情）
        /// </summary>
        /// <param name="developerId">商务ID</param>
        /// <param name="searchType">类型</param>
        /// <param name="settlementDayFrom">账单开始</param>
        /// <returns></returns>
        public DataSet FindPagedListByDeveloperBpUId(int developerId, int searchType, string settlementDayFrom = "")
        {
            string where = "";

            if (!string.IsNullOrEmpty(settlementDayFrom))
            {
                where += " and SettlementDay>='" + settlementDayFrom + "' and SettlementDay<='" + settlementDayFrom + "'";

            }

            return _repository.FindPagedListBpUid(developerId, searchType, where);
        }


        /// <summary>
        /// 根据商务ID查询账单管理(合计)
        /// </summary>
        /// <param name="developerId">商务ID</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="settlementDayFrom">账单开始日期</param>
        /// <param name="settlementDayTo">账单结束日期</param>
        /// <param name="pageIndex">当前页索引(从0开始)</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public IPagedList<JMP.MDL.CoSettlementDeveloperOverview> CoSettlementDeveloperOverview_Bp(int developerId, string orderBy = "SettlementDay desc", string settlementDayFrom = "",
            string settlementDayTo = "", int pageIndex = 0, int pageSize = 20)
        {
            string where = "";


            if (!string.IsNullOrEmpty(settlementDayFrom))
            {
                where += " and SettlementDay>='" + settlementDayFrom + "'";

            }
            if (!string.IsNullOrEmpty(settlementDayTo))
            {
                where += " and SettlementDay<='" + settlementDayTo + "'";
            }

            return _repository.FindPagedListBpUid_Statistics(developerId, where, orderBy, pageIndex, pageSize);
        }


        /// <summary>
        /// 根据代理商ID查询账单管理
        /// </summary>
        /// <param name="developerId">代理商ID</param>
        /// <param name="settlementDayFrom">账单日期</param>
        /// <returns></returns>
        public DataSet FindPagedListByDeveloperAgentUId(int developerId, string settlementDayFrom = "")
        {

            string where = "";

            if (!string.IsNullOrEmpty(settlementDayFrom))
            {
                where += " and SettlementDay>='" + settlementDayFrom + "' and SettlementDay<='" + settlementDayFrom + "'";

            }

            return _repository.FindPagedListAgentUid(developerId, where);
        }


        /// <summary>
        /// 根据代理商ID查询账单管理(合计)
        /// </summary>
        /// <param name="developerId">代理商ID</param>
        /// <param name="settlementDayFrom">账单开始日期</param>
        /// <param name="settlementDayTo">账单结束日期</param>
        /// <param name="DeveloperName">开发者名称</param>
        /// <param name="pageIndex">当前页索引(从0开始)</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public IPagedList<JMP.MDL.CoSettlementDeveloperOverview> CoSettlementDeveloperOverview_Agent(int developerId, string orderBy = "SettlementDay  desc", string settlementDayFrom = "",
            string settlementDayTo = "", int pageIndex = 0, int pageSize = 20)
        {

            string where = "";

            if (!string.IsNullOrEmpty(settlementDayFrom))
            {
                where += " and SettlementDay>='" + settlementDayFrom + "'";

            }
            if (!string.IsNullOrEmpty(settlementDayTo))
            {
                where += " and SettlementDay<='" + settlementDayTo + "'";
            }

            return _repository.FindPagedListAgentUid_Statistics(developerId, where, orderBy, pageIndex, pageSize);
        }


    }
}
