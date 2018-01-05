using System.Collections.Generic;
using JMP.MDL;
using System;
using DxPay.Infrastructure;

namespace DxPay.Services
{
    public interface ITrendsService
    {
         /// <summary>
         /// 查询分页数据(代理商)
         /// </summary>
         /// <param name="userid">当前登录人ID</param>
         /// <param name="calsstype">平台（0 商务 1 代理商）</param>
         /// <param name="orderBy">排序字段</param>
         /// <param name="stime">开始时间</param>
         /// <param name="etime">结束时间</param>
         /// <param name="parameters"></param>
         /// <param name="pageIndex"></param>
         /// <param name="pageSize"></param>
         /// <returns></returns>
        IEnumerable<jmp_trends> FindPagedListByAgent(int userid,string orderBy,string  stime, string  etime,string a_name);


        IEnumerable<jmp_trends> FindPagedListByBp(int userid, string orderBy, string stime, string etime, string a_name);
    }
}
