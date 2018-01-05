using System.Collections.Generic;
using JMP.MDL;
using System;
using DxPay.Infrastructure;

namespace DxPay.Services
{
    public interface IOperatingsystemService : IService<jmp_operatingsystem>
    {
        IEnumerable<jmp_operatingsystem> FindPagedList(int userid, string orderBy, string stime, string etime, string a_name);
        IEnumerable<jmp_operatingsystem> FindPagedListByBp(int userid, string orderBy, string stime, string etime, string a_name);
        jmp_operatingsystem FindBytime(string start, string end);
    }
}
