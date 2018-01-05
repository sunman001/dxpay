using JMP.MDL;
using System;
using System.Collections.Generic;

namespace DxPay.Repositories
{
    //手机型号统计
    public interface IModelnumbersRepository : IRepository<jmp_modelnumber>
    {
        jmp_modelnumber FindBytime(string @where);

        IEnumerable<jmp_modelnumber> FindListBySql(string @where, string orderBy);
        IEnumerable<jmp_modelnumber> FindListBySql(string @where,  string bpWhere,string agentWhere, string orderBy);
    }
}
