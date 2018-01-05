using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    ///<summary>
    ///缺失用户报表统计
    ///</summary>
    public class jmp_defect
    {

        /// <summary>
        /// 主键id
        /// </summary>		
        [EntityTracker(Label = "主键id", Description = "主键id")]
        public int d_id { get; set; }
        /// <summary>
        /// 统计类型（0：新增，1：活跃）
        /// </summary>	
        [EntityTracker(Label = "统计类型", Description = "统计类型")]
        public int d_type { get; set; }
        /// <summary>
        /// 关联应用id
        /// </summary>	
        [EntityTracker(Label = "关联应用id", Description = "关联应用id")]
        public int d_aapid { get; set; }
        /// <summary>
        /// 流失用户总数
        /// </summary>	
        [EntityTracker(Label = "流失用户总数", Description = "流失用户总数")]
        public int d_losscount { get; set; }
        /// <summary>
        /// 流失用户比例
        /// </summary>	
        [EntityTracker(Label = "流失用户比例", Description = "流失用户比例")]
        public decimal d_lossproportion { get; set; }
        /// <summary>
        /// 用户总数
        /// </summary>	
        [EntityTracker(Label = "用户总数", Description = "用户总数")]
        public int d_usercount { get; set; }
        /// <summary>
        /// 数据类型（0：连续7天，1：连续14天，2：连续30天）
        /// </summary>	
        [EntityTracker(Label = "数据类型", Description = "数据类型")]
        public int d_datatype { get; set; }
        /// <summary>
        /// 统计日期
        /// </summary>
        [EntityTracker(Label = "统计日期", Description = "统计日期")]
        public DateTime d_time { get; set; }

    }
}

