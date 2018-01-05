using DxPay.Infrastructure;
using JMP.MDL;
using System;
using System.Data;

namespace DxPay.Repositories
{
    public interface IUserReportRepository 
    {

       /// <summary>
       /// 今日应用报表(代理商)
       /// </summary>
       /// <param name="where"></param>
       /// <param name="orderBy"></param>
       /// <param name="parameters"></param>
       /// <param name="pageIndex"></param>
       /// <param name="pageSize"></param>
       /// <returns></returns>
        IPagedList<jmp_user_report> FindPagedListByToday(string @where, string orderBy,object parameters = null, int pageIndex = 0, int pageSize = 20);

        /// <summary>
        /// 总的应用报表（代理商）
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="parameters"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedList<jmp_user_report> FindPagedListByTotal(string @where, string orderBy, object parameters = null, int pageIndex = 0, int pageSize = 20);

        /// <summary>
        /// 总的应用报表（商务）
        /// </summary>
        /// <param name="where"></param>
        /// <param name="bpwhere"></param>
        /// <param name="agentWhere"></param>
        /// <param name="orderBy"></param>
        /// <param name="parameters"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedList<jmp_user_report> FindPagedListByBpTotal(string @where, string @bpwhere,string@agentWhere,  string orderBy, object parameters = null, int pageIndex = 0, int pageSize = 20);

        /// <summary>
        /// 今日应用报表（商务）
        /// </summary>
        /// <param name="where"></param>
        /// <param name="bpwhere"></param>
        /// <param name="agentWhere"></param>
        /// <param name="orderBy"></param>
        /// <param name="parameters"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedList<jmp_user_report> FindPagedListByBpToday(string @where, string @bpwhere, string @agentWhere, string orderBy, object parameters = null, int pageIndex = 0, int pageSize = 20);

    }
}
