using DxPay.Infrastructure;
using JMP.MDL;
using System;
using System.Collections.Generic;
using System.Data;

namespace DxPay.Repositories
{
    public interface ITrendsRepository
    {

      /// <summary>
      /// 分页查询
      /// </summary>
      /// <param name="where"></param>
      /// <param name="orderBy"></param>
      /// <returns></returns>
        IEnumerable<jmp_trends> FindPagedListBySql(string @where, string orderBy);

        IEnumerable<jmp_trends> FindPagedListByBp(string @where,  string @bpWhere,string @agentWhere,string orderBy);


    }
}
