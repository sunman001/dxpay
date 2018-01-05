using System.Collections.Generic;
using JMP.MDL;
using System;
using DxPay.Infrastructure;

namespace DxPay.Services
{
    public interface IUserService : IService<jmp_user>
    {
        /// <summary>
        /// 根据条件查询分页数据
        /// </summary>
        /// <param name="orderBy">排序字段,不允许为空</param>
        /// <param name="where">查询条件</param>
        /// <param name="parameters">查询条件参数对象</param>
        /// <param name="pageIndex">当前页面索引</param>
        /// <param name="pageSize">分布大小</param>
        /// <returns></returns>
        IPagedList<jmp_user> FindPagedListBySql(string orderBy,string sql, object parameters = null, int pageIndex = 0, int pageSize = 20);

        IEnumerable<jmp_user> FindListBySql(string @where, string orderBy);
    }
}
