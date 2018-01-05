using System.Collections.Generic;
using JMP.MDL;
using System;
using DxPay.Infrastructure;

namespace DxPay.Services
{
    public interface IUserReportService 
    {
        /// <summary>
        /// 查询应用今日报表(代理商)
        /// </summary>
        /// <param name="userid">当前登录人ID</param>
        /// <param name="orderBy">排序字段</param>
        /// <param name="rtype">查询类型</param>
        /// <param name="stime"></param>
        /// <param name="etime"></param>
        /// <param name="time"></param>
        /// <param name="num"></param>
        /// <param name="types"></param>
        /// <param name="searchKey"></param>
        /// <param name="sort"></param>
        /// <param name="searchTotal"></param>
        /// <param name="parameters"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedList<jmp_user_report> FindPagedListByAgentToday(int userid, string orderBy, string  rtype, string stime, string etime, string time ,int num, string types, string searchKey, int sort, string searchTotal ,object parameters = null, int pageIndex = 0, int pageSize = 20);

        /// <summary>
        /// 查询总的应用报表（代理商）
        /// </summary>
        /// <param name="userid">当前登录人ID</param>
        /// <param name="calsstype">平台（0商务1 代理商）</param>
        /// <param name="orderBy">排序字段</param>
        /// <param name="rtype">查询类型</param>
        /// <param name="stime"></param>
        /// <param name="etime"></param>
        /// <param name="time"></param>
        /// <param name="num"></param>
        /// <param name="types"></param>
        /// <param name="searchKey"></param>
        /// <param name="sort"></param>
        /// <param name="searchTotal"></param>
        /// <param name="parameters"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedList<jmp_user_report> FindPagedListByAgentTotal(int userid, string orderBy, string rtype, string stime, string etime, string time, int num, string types, string searchKey, int sort, string searchTotal, object parameters = null, int pageIndex = 0, int pageSize = 20);

        /// <summary>
        /// 查询总的应用报表（商务）
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="orderBy"></param>
        /// <param name="rtype"></param>
        /// <param name="stime"></param>
        /// <param name="etime"></param>
        /// <param name="time"></param>
        /// <param name="num"></param>
        /// <param name="types"></param>
        /// <param name="searchKey"></param>
        /// <param name="sort"></param>
        /// <param name="searchTotal"></param>
        /// <param name="parameters"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedList<jmp_user_report> FindPagedListByBpTotal(int userid, int relationtype,  string orderBy, string rtype, string stime, string etime, string time, int num, string types, string searchKey, int sort, string searchTotal, object parameters = null, int pageIndex = 0, int pageSize = 20);

        /// <summary>
        /// 查询今日应用报表（商务）
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="orderBy"></param>
        /// <param name="rtype"></param>
        /// <param name="stime"></param>
        /// <param name="etime"></param>
        /// <param name="time"></param>
        /// <param name="num"></param>
        /// <param name="types"></param>
        /// <param name="searchKey"></param>
        /// <param name="sort"></param>
        /// <param name="searchTotal"></param>
        /// <param name="parameters"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedList<jmp_user_report> FindPagedListByBpToday(int userid, int relationtype, string orderBy, string rtype, string stime, string etime, string time, int num, string types, string searchKey, int sort, string searchTotal, object parameters = null, int pageIndex = 0, int pageSize = 20);



    }
}
