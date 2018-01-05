using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    /// <summary>
    /// 值班者工单统计
    /// </summary>
   public class jmp_workorderReport
    {
        /// <summary>
        /// 工单总量
        /// </summary>
       public int countworkorder { get; set; }

        /// <summary>
        /// 工单成功量
        /// </summary>
        public int sucessworkorder { get; set; }

        /// <summary>
        /// 平均响应时间
        /// </summary>
        public float branch { get; set; }

        /// <summary>
        /// 平均评分
        /// </summary>
        public float socre { get; set; }

        /// <summary>
        /// 值班者
        /// </summary>
        public string u_realname { get; set; }
    }
}
