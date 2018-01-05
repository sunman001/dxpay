using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMP.MDL;
using System.Data;

namespace DxPay.Repositories
{
    public interface IAppCountRepository: IRepository<jmp_appcount>
    {
        /// <summary>
        /// 根据uid查询商务首页曲线图统计图像报表
        /// </summary>
        /// <param name="uid">商务ID</param>
        /// <param name="startTime">开始日期</param>
        /// <param name="endTime">结束日期</param>
        /// <param name="startTimeAdy">三日数据开始日期</param>
        /// <param name="endTimeAdy">三日数据结束日期</param>
        /// <returns></returns>
        DataSet FindPagedListSqlBp(int uid, string startTime, string endTime, string startTimeAdy, string endTimeAdy);

        /// <summary>
        /// 根据商务ID查询交易金额和交易笔数
        /// </summary>
        /// <param name="t_time">日期</param>
        /// <param name="u_id">用户ID</param>
        /// <returns></returns>
        jmp_appcount DataAppcountsqlBp(string t_time, int u_id);


        /// <summary>
        /// 代理商首页曲线图统计图像报表
        /// </summary>
        /// <param name="uid">代理商ID</param>
        /// <param name="startTime">开始日期</param>
        /// <param name="endTime">结束日期</param>
        /// <param name="startTimeAdy">三日数据开始日期</param>
        /// <param name="endTimeAdy">三日数据结束日期</param>
        /// <returns></returns>
        DataSet FindPagedListSqlAgent(int uid, string startTime, string endTime, string startTimeAdy, string endTimeAdy);


        /// <summary>
        /// 根据代理商ID查询交易金额和交易笔数
        /// </summary>
        /// <param name="t_time">日期</param>
        /// <param name="u_id">用户ID</param>
        /// <returns></returns>
        jmp_appcount DataAppcountsqlAgent(string t_time, int u_id);
    }
}
