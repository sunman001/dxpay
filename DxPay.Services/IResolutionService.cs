using System.Collections.Generic;
using JMP.MDL;
using System;
using DxPay.Infrastructure;

namespace DxPay.Services
{
    public interface IResolutionService : IService<jmp_resolution>
    {
        IEnumerable<jmp_resolution> FindPagedList(int userid,  string orderBy, string stime, string etime, string a_name);
        IEnumerable<jmp_resolution> FindPagedListByBp(int userid, string orderBy, string stime, string etime, string a_name);
        jmp_resolution FindBytime(string start, string end);
    }
}
