using DxPay.Domain;
using DxPay.Infrastructure;
using System.Data;

namespace DxPay.Services.Inter
{
    public interface ICoSettlementDeveloperAppDetailsService
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
        IPagedList<CoSettlementDeveloperAppDetails> FindAll(string orderBy = "Id DESC", string settlementDayFrom = "", string settlementDayTo = "", int pageIndex = 0, int pageSize = 20);

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
        IPagedList<CoSettlementDeveloperAppDetails> FindPagedListByDeveloperId(int developerId,string orderBy = "Id DESC", string settlementDayFrom = "", string settlementDayTo = "", int pageIndex = 0, int pageSize = 20);
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
        IPagedList<CoSettlementDeveloperAppDetails> FindPagedListByDeveloperName(string developerName, string orderBy = "Id DESC", string settlementDayFrom = "", string settlementDayTo = "", int pageIndex = 0, int pageSize = 20);

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
        IPagedList<CoSettlementDeveloperAppDetails> FindPagedListByAppId(int appId, string orderBy = "Id DESC", string settlementDayFrom = "", string settlementDayTo = "", int pageIndex = 0, int pageSize = 20);
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
        IPagedList<CoSettlementDeveloperAppDetails> FindPagedListByAppName(string appName, string orderBy = "Id DESC", string settlementDayFrom = "", string settlementDayTo = "", int pageIndex = 0, int pageSize = 20);

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
        IPagedList<CoSettlementDeveloperAppDetails> FindPagedListByPayModeId(int payModeId, string orderBy = "Id DESC", string settlementDayFrom = "", string settlementDayTo = "", int pageIndex = 0, int pageSize = 20);
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
        IPagedList<CoSettlementDeveloperAppDetails> FindPagedListByPayModeName(string payModeName, string orderBy = "Id DESC", string settlementDayFrom = "", string settlementDayTo = "", int pageIndex = 0, int pageSize = 20);

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
        IPagedList<CoSettlementDeveloperAppDetails> FindAll(int developerId=0,string developerName="",int appId=0,string appName="",int payModeId=0,string payModeName = "",string orderBy = "Id DESC", string settlementDayFrom = "", string settlementDayTo = "", int pageIndex = 0, int pageSize = 20);

        /// <summary>
        /// 根据条件查询结算详情
        /// </summary>
        /// <param name="id">开发者ID</param>
        /// <param name="date">结算日期</param>
        /// <returns></returns>
        DataSet FindPagedListByDeveloperModel(int id, string date);

        /// <summary>
        /// 商务平台首页统计
        /// </summary>
        /// <param name="id">商务ID</param>
        /// <param name="date">查询日期</param>
        /// <param name="start">状态判断（昨日，月，上月）</param>
        /// <returns></returns>
        JMP.MDL.CoSettlementDeveloperAppDetails FindPagedListByDeveloperKFZ(int id, string date, int start);

        /// <summary>
        /// 代理商平台首页统计
        /// </summary>
        /// <param name="id">代理商ID</param>
        /// <param name="date">查询日期</param>
        /// <param name="start">状态判断（昨日，月，上月）</param>
        /// <returns></returns>
        JMP.MDL.CoSettlementDeveloperAppDetails FindPagedListByDeveloperKFZAgent(int id, string date, int start);

    }
}
