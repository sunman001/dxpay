using System.Collections.Generic;
using JMP.MDL;
using System;
using DxPay.Infrastructure;

namespace DxPay.Services
{
    public interface IAppService : IService<jmp_app>
    {
      

       /// <summary>
       /// 根据条件查询数据分页
       /// </summary>
       /// <param name="userid"> 当前登录人Id</param>
       /// <param name="calsstype"> 1代表代理商 2 代表商务</param>
       /// <param name="orderBy"> 排序条件</param>
       /// <param name="platformid"></param>
       /// <param name="auditstate"></param>
       /// <param name="sea_name"></param>
       /// <param name="type"></param>
       /// <param name="SelectState"></param>
       /// <param name="searchDesc"></param>
       /// <param name="parameters"></param>
       /// <param name="pageIndex"></param>
       /// <param name="pageSize"></param>
       /// <returns></returns>
        IPagedList<jmp_app> FindPagedListBySql( int userid, int calsstype, string orderBy, int platformid, int auditstate, string sea_name, int type, int SelectState, int searchDesc, object parameters = null, int pageIndex = 0, int pageSize = 20);

        IEnumerable<jmp_app> FindListByUserId(int classtype, int userid, string orderBy);
    }
}
