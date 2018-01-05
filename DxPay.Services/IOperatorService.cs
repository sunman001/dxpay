using System.Collections.Generic;
using JMP.MDL;
using System;
using DxPay.Infrastructure;

namespace DxPay.Services
{
    public interface IOperatorService : IService<jmp_operator>
    {
        IEnumerable<jmp_operator> FindPagedList(int userid, string orderBy, string stime, string etime, string a_name);
        IEnumerable<jmp_operator> FindPagedListByBp(int userid, string orderBy, string stime, string etime, string a_name);
        jmp_operator FindBytime(string start, string end);
    }
}
