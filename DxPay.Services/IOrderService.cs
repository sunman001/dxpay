using System.Collections.Generic;
using JMP.MDL;
using System;
using DxPay.Infrastructure;

namespace DxPay.Services
{
    public interface IOrderService : IService<jmp_order>
    {
        /// <summary>
        /// 查询订单明细（代理商）
        /// </summary>
        /// <param name="userid"> 当前登录人ID</param>
        /// <param name="orderBy"> 排序条件</param>
        /// <param name="searchType">查询类型</param>
        /// <param name="searchname">查询类容</param>
        /// <param name="stime">开始时间</param>
        /// <param name="etime">结束时间</param>
        /// <param name="paymode">支付类型</param>
        /// <param name="paymentstate">支付状态</param>
        /// <param name="noticestate">通知状态</param>
        /// <param name="parameters">参数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <returns></returns>
        IPagedList<jmp_order> FindPagedListByAgent(int userid,string orderBy, int searchType, string searchname, string stime, string etime,int paymode, string paymentstate, string noticestate,object parameters = null, int pageIndex = 0, int pageSize = 20);

        /// <summary>
        /// 查询订单明细（商务）
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="orderBy"></param>
        /// <param name="searchType"></param>
        /// <param name="searchname"></param>
        /// <param name="stime"></param>
        /// <param name="etime"></param>
        /// <param name="paymode"></param>
        /// <param name="paymentstate"></param>
        /// <param name="noticestate"></param>
        /// <param name="parameters"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IPagedList<jmp_order> FindPagedListByBP(int userid, int relationtype, string orderBy, int searchType, string searchname, string stime, string etime, int paymode, string paymentstate, string noticestate, object parameters = null, int pageIndex = 0, int pageSize = 20);
    }
}
