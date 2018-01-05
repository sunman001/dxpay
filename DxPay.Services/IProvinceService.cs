using System.Collections.Generic;
using JMP.MDL;
using System;
using DxPay.Infrastructure;

namespace DxPay.Services
{
    public interface IProvinceService : IService<jmp_province>
    {
        
        IEnumerable<jmp_province> FindPagedList(int userid,  string orderBy, string stime, string etime, string a_name);

        IEnumerable<jmp_province> FindPagedListByBp(int userid, string orderBy, string stime, string etime, string a_name);


        /// <summary>
        /// 根据时间查询
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns></returns>
        jmp_province FindBytime(string start, string end);
    }
}
