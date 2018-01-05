using DxPay.Domain;
using DxPay.Infrastructure;
using System.Data;

namespace DxPay.Repositories.Inter
{
    /// <summary>
    /// 开发者结算总揽接口
    /// </summary>
    public interface ICoSettlementDeveloperOverviewRepository
    {
        /// <summary>
        /// 查询开发者结算总揽数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="pageIndex">当前页索引(从0开始)</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        IPagedList<CoSettlementDeveloperOverview> FindPagedList(string @where, string orderBy, int pageIndex = 0, int pageSize = 20);

        /// <summary>
        /// 根据条件查询商务下开发者结算数据(详情)
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        DataSet FindPagedListBpUid(int developerId, int searchType, string where);

        /// <summary>
        /// 根据条件查询商务下开发者结算数据（合计）
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageIndex">当前页索引(从0开始)</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        IPagedList<JMP.MDL.CoSettlementDeveloperOverview> FindPagedListBpUid_Statistics(int developerId, string where, string orderBy, int pageIndex = 0, int pageSize = 20);


        /// <summary>
        /// 根据条件查询代理商下开发者结算数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        DataSet FindPagedListAgentUid(int developerId, string where);


        /// 根据条件查询代理商下开发者结算数据（合计）
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageIndex">当前页索引(从0开始)</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        IPagedList<JMP.MDL.CoSettlementDeveloperOverview> FindPagedListAgentUid_Statistics(int developerId, string where, string orderBy, int pageIndex = 0, int pageSize = 20);


    }
}
