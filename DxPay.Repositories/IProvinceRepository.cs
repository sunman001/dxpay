using JMP.MDL;
using System;
using System.Collections.Generic;

namespace DxPay.Repositories
{
    //手机操作系统
    public interface IProvinceRepository : IRepository<jmp_province>
    {
        jmp_province FindBytime(string @where);

        IEnumerable<jmp_province> FindListBySql(string @where, string orderBy);

        IEnumerable<jmp_province> FindListBySql(string @where, string @bpWhere,string @agentWhere, string orderBy);
    }
}
