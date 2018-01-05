using JMP.MDL;
using System;
using System.Collections.Generic;

namespace DxPay.Repositories
{
    //手机品牌
    public interface IStatisticsRepository : IRepository<jmp_statistics>
    {
        jmp_statistics FindBytime(string @where);

        IEnumerable<jmp_statistics> FindListBySql(string @where, string orderBy);
        IEnumerable<jmp_statistics> FindListBySql(string @where, string @bpWhere,string @agentWhere, string orderBy);
    }
}
