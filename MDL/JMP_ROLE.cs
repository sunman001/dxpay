using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    //角色表
    public class jmp_role
    {
        /// <summary>
        /// 角色标识
        /// </summary>	
        [EntityTracker(Label = "角色标识", Description = "角色标识")]
        public int r_id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>		
        [EntityTracker(Label = "角色名称", Description = "角色名称")]
        public string r_name { get; set; }

        /// <summary>
        /// 角色值
        /// </summary>	
        [EntityTracker(Label = "角色值", Description = "角色值")]
        public string r_value { get; set; }

        /// <summary>
        /// 状态(0:冻结，1：正常)
        /// </summary>	
        [EntityTracker(Label = "状态", Description = "状态")]
        public int r_state { get; set; }

        /// <summary>
        /// 角色类型（0：后台角色，1：前台角色）
        /// </summary>
        [EntityTracker(Label = "角色类型", Description = "角色类型")]
        public int r_type { get; set; }

    }
}