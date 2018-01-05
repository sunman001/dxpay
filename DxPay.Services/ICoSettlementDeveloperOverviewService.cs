using DxPay.Domain;
using DxPay.Infrastructure;
using System.Data;

namespace DxPay.Services
{
    public interface ICoSettlementDeveloperOverviewService
    {
        /// <summary>
        /// 查询所有数据(分页)
        /// </summary>
        /// <param name="orderBy">排序条件</param>
        /// <param name="settlementDayFrom">账单开始日期</param>
        /// <param name="settlementDayTo">账单结束日期</param>
        /// <param name="pageIndex">当前页索引(从0开始)</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        IPagedList<CoSettlementDeveloperOverview> FindAll(string orderBy = "Id DESC", string settlementDayFrom = "", string settlementDayTo = "", int pageIndex = 0, int pageSize = 20);
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
        IPagedList<CoSettlementDeveloperOverview> FindPagedListByDeveloperId(int developerId, string orderBy = "Id DESC", string settlementDayFrom = "", string settlementDayTo = "", int pageIndex = 0, int pageSize = 20);
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
        IPagedList<CoSettlementDeveloperOverview> FindPagedListByDeveloperName(string developerName, string orderBy = "Id DESC", string settlementDayFrom = "", string settlementDayTo = "", int pageIndex = 0, int pageSize = 20);

        /// <summary>
        /// 根据商务ID查询账单管理（详情）
        /// </summary>
        /// <param name="developerId">商务ID</param>
        /// <param name="searchType">类型</param>
        /// <param name="settlementDayFrom">账单日期</param>
        /// <param name="pageIndex">当前页索引(从0开始)</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        DataSet FindPagedListByDeveloperBpUId(int developerId, int searchType,  string settlementDayFrom = "");


        /// <summary>
        /// 根据商务ID查询账单管理（合计）
        /// </summary>
        /// <param name="developerId">商务ID</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="settlementDayFrom">账单开始日期</param>
        /// <param name="settlementDayTo">账单结束日期</param>
        /// <param name="pageIndex">当前页索引(从0开始)</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        IPagedList<JMP.MDL.CoSettlementDeveloperOverview> CoSettlementDeveloperOverview_Bp(int developerId, string orderBy = "SettlementDay  desc", string settlementDayFrom = "", string settlementDayTo = "", int pageIndex = 0, int pageSize = 20);

        /// <summary>
        /// 代理商ID查询账单管理
        /// </summary>
        /// <param name="developerId">商务ID</param>
        /// <param name="settlementDayFrom">账单日期</param>
        /// <returns></returns>
        DataSet FindPagedListByDeveloperAgentUId(int developerId, string settlementDayFrom = "");


        /// <summary>
        /// 代理商ID查询账单管理（合计）
        /// </summary>
        /// <param name="developerId">商务ID</param>
        /// <param name="settlementDayFrom">账单日期</param>
        /// <returns></returns>
        IPagedList<JMP.MDL.CoSettlementDeveloperOverview> CoSettlementDeveloperOverview_Agent(int developerId,string orderBy = "SettlementDay  desc", string settlementDayFrom = "", string settlementDayTo = "", int pageIndex = 0, int pageSize = 20);

    }
}
