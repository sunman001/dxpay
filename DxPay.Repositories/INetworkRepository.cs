using JMP.MDL;
using System;
using System.Collections.Generic;

namespace DxPay.Repositories
{
    //手机操作系统
    public interface INetworkRepository : IRepository<jmp_network>
    {
        jmp_network FindBytime(string @where);

        IEnumerable<jmp_network> FindListBySql(string @where, string orderBy);
        IEnumerable<jmp_network> FindListBySql(string @where, string @bpWhere,string @agentWhere,string orderBy);
    }
}
