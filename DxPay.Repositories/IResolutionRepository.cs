using JMP.MDL;
using System;
using System.Collections.Generic;

namespace DxPay.Repositories
{
    //手机操作系统
    public interface IResolutionRepository : IRepository<jmp_resolution>
    {
        jmp_resolution FindBytime(string @where);

        IEnumerable<jmp_resolution> FindListBySql(string @where, string orderBy);
        IEnumerable<jmp_resolution> FindListBySql(string @where,string@bpWhere,string@agentWhere, string orderBy);
    }
}
