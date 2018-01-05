using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    /// <summary>
    /// 风险等级表
    /// </summary>
    public class jmp_risklevelallocation
    {
        /// <summary>
        /// 主键自增
        /// </summary>
        [EntityTracker(Label = "主键", Description = "主键")]
        public int r_id { get; set; }
        /// <summary>
        /// 应用类型父级id
        /// </summary>
        [EntityTracker(Label = "应用类型父级id", Description = "应用类型父级id")]
        public int r_apptypeid { get; set; }
        /// <summary>
        /// 关联风险等级表id
        /// </summary>
        [EntityTracker(Label = "关联风险等级表id", Description = "关联风险等级表id")]
        public int r_risklevel { get; set; }
        /// <summary>
        /// 状态（0：正常，1冻结）
        /// </summary>
        [EntityTracker(Label = "状态", Description = "状态")]
        public int r_state { get; set; }
        /// <summary>
        /// 应用类型名称
        /// </summary>
        public string t_name { get; set; }
        /// <summary>
        /// 风险等级名称
        /// </summary>
        public string r_name { get; set; }
    }
}