using JMP.MDL;
using System;
using System.Collections.Generic;

namespace DxPay.Repositories
{
    //手机操作系统
    public interface IOperatorRepository : IRepository<jmp_operator>
    {
        jmp_operator FindBytime(string @where);

        IEnumerable<jmp_operator> FindListBySql(string @where, string orderBy);

        IEnumerable<jmp_operator> FindListBySql(string @where, string bpWhere,string agentWhere, string orderBy);
    }
}
