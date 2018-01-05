using DxPay.Domain;
using DxPay.Infrastructure;
using System.Data;

namespace DxPay.Repositories.Inter
{
    /// <summary>
    /// 开发者结算详情接口
    /// </summary>
    public interface ICoSettlementDeveloperAppDetailsRepository
    {
        /// <summary>
        /// 查询开发者结算详情数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="pageIndex">当前页索引(从0开始)</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        IPagedList<CoSettlementDeveloperAppDetails> FindPagedList(string @where, string orderBy, int pageIndex = 0, int pageSize = 20);

        /// <summary>
        /// 根据条件查询结算详情
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        DataSet FindPagedModel(string @where);



        /// <summary>
        /// 根据条件查询商务平台首页开发者结算数据（首页统计）
        /// </summary>
        /// <param name="id">商务ID</param>
        /// <param name="date">日期</param>
        /// <param name="start">状态</param>
        /// <returns></returns>
        JMP.MDL.CoSettlementDeveloperAppDetails GetModelKFZ(int id, string date, int start);


        /// <summary>
        /// 根据条件查询代理商平台首页开发者结算数据（首页统计）
        /// </summary>
        /// <param name="id">代理商ID</param>
        /// <param name="date">日期</param>
        /// <param name="start">状态</param>
        /// <returns></returns>
        JMP.MDL.CoSettlementDeveloperAppDetails GetModelKFZAgent(int id, string date, int start);

    }
}
