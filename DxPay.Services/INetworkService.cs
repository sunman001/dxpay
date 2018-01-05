using System.Collections.Generic;
using JMP.MDL;
using System;
using DxPay.Infrastructure;

namespace DxPay.Services
{
    public interface INetworkService : IService<jmp_network>
    {
        IEnumerable<jmp_network> FindPagedList(int userid, string orderBy, string stime, string etime, string a_name);
        IEnumerable<jmp_network> FindPagedListByBp(int userid, string orderBy, string stime, string etime, string a_name);

        jmp_network FindBytime(string start, string end);
    }
}
