using JMP.MDL;
using System;
using DxPay.Infrastructure;
using System.Collections.Generic;

namespace DxPay.Repositories
{
    public interface IAppRepository : IRepository<jmp_app>
    {
        /// <summary>
        /// 根据sql语句查询数据
        /// </summary>
        /// <param name="userid"> 当前登录Id</param>
        ///  <param name="userid"> 类别（代理商1 商务0）</param>
        /// <param name="orderBy"> 排序</param>
        /// <param name="where"> 条件</param>
        /// <param name="parameters">参数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <returns></returns>
        IPagedList<jmp_app> FindPagedListBySql(int userid, int type, string orderBy, string @where = "", object parameters = null, int pageIndex = 0, int pageSize = 20);

        IEnumerable<jmp_app> FindListByUserId(int classtype, int userid, string orderBy);
    }
}
