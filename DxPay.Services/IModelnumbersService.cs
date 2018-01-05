using System.Collections.Generic;
using JMP.MDL;
using System;
using DxPay.Infrastructure;

namespace DxPay.Services
{
    public interface IModelnumbersService : IService<jmp_modelnumber>
    {
        IEnumerable<jmp_modelnumber> FindPagedList(int userid,  string orderBy, string stime, string etime, string a_name);

        IEnumerable<jmp_modelnumber> FindPagedListByBp(int userid, string orderBy, string stime, string etime, string a_name);
        jmp_modelnumber FindBytime(string start, string end);
    }
}
