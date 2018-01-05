using System.Collections.Generic;
using JMP.MDL;
using System;
using DxPay.Infrastructure;

namespace DxPay.Services
{
    public interface ICoCooperationApplicationService : IService<CoCooperationApplication>
    {
        IEnumerable<CoCooperationApplication> FindByClause(int top, string orderBy, string @where = "");

        IPagedList<CoCooperationApplication> FindPagedList(string orderBy, string status, string sea_name,int type, int searchDesc, object parameters = null, int pageIndex = 0, int pageSize = 20);

        bool UpdateState( int id, int state, DateTime GrabbedDate, string GrabbedByName, int GrabbedById);

        IPagedList<CoCooperationApplication> FindPagedList(string orderBy, string status, string sea_name, int type, int searchDesc, int userid, object parameters = null, int pageIndex = 0, int pageSize = 20);

    }
}
