using System.Collections.Generic;
using JMP.MDL;
using System;
using DxPay.Infrastructure;

namespace DxPay.Services
{
    public interface IStatisticsService : IService<jmp_statistics>
    {
        IEnumerable<jmp_statistics> FindPagedListByAgent(int userid,string orderBy, string stime, string etime, string a_name);
        jmp_statistics FindBytime(string start, string end);
        IEnumerable<jmp_statistics> FindPagedListByBp(int userid, string orderBy, string stime, string etime, string a_name);

    }
}
