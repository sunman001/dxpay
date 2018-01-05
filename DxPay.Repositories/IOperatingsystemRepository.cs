using JMP.MDL;
using System;
using System.Collections.Generic;

namespace DxPay.Repositories
{
    //手机操作系统
    public interface IOperatingsystemRepository : IRepository<jmp_operatingsystem>
    {
        jmp_operatingsystem FindBytime(string @where);

        IEnumerable<jmp_operatingsystem> FindListBySql(string @where, string orderBy);
        IEnumerable<jmp_operatingsystem> FindListBySql(string @where, string bpWhere, string agentWhere, string orderBy);
    }
}
