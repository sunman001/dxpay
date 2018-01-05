using DxPay.Infrastructure;
using JMP.MDL;
using System;
namespace DxPay.Repositories
{
    public interface IOrderRepository : IRepository<jmp_order>
    {

        /// <summary>
        /// 根据sql语句查询数据(代理商平台)
        /// </summary>
        /// <param name="userid"> 当前登录Id</param>
        /// <param name="orderBy"> 排序</param>
        /// <param name="where"> sql</param>
        /// <param name="parameters">参数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <returns></returns>
        IPagedList<jmp_order> FindPagedListBySql(string orderBy, string sql, string where, string agentwhere, object parameters = null, int pageIndex = 0, int pageSize = 20);
       
        /// <summary>
        /// 查询订单明细（商务平台）
        /// </summary>
        /// <param name="orderBy">排序字段</param>
        /// <param name="sql">动态组装订单查询语句</param>
        /// <param name="where">where条件</param>
        /// <param name="bpwhere">商务过滤条件</param>
        /// <param name="agentwhere">代理商过滤条件</param>
        /// <param name="parameters">参数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <returns></returns>
        IPagedList<jmp_order> FindPagedListByBP(string orderBy, string sql, string where, string bpwhere,string agentwhere, object parameters = null, int pageIndex = 0, int pageSize = 20);

    }
}
