using JMP.MDL;
using System;
using DxPay.Infrastructure;
using System.Collections.Generic;

namespace DxPay.Repositories
{
    public interface IUserRepository : IRepository<jmp_user>
    {
       /// <summary>
       /// 根据sql语句查询数据
       /// </summary>
       /// <param name="userid"> 当前登录Id</param>
       /// <param name="orderBy"> 排序</param>
       /// <param name="where"> sql</param>
       /// <param name="parameters">参数</param>
       /// <param name="pageIndex">当前页</param>
       /// <param name="pageSize">每页显示数量</param>
       /// <returns></returns>
        IPagedList<jmp_user> FindPagedListBySql( string orderBy, string sql , object parameters = null, int pageIndex = 0, int pageSize = 20);

        IEnumerable<jmp_user> FindListBySql(string @where, string orderBy);

    }
}
