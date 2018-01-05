﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    ///<summary>
    ///手机操作系统统计
    ///</summary>
    public class jmp_operatingsystem
    {

        /// <summary>
        /// id主键
        /// </summary>	
        [EntityTracker(Label = "id主键", Description = "id主键")]
        public int o_id { get; set; }
        /// <summary>
        /// 手机操作系统
        /// </summary>	
        [EntityTracker(Label = "手机操作系统", Description = "手机操作系统")]
        public string o_system { get; set; }
        /// <summary>
        /// 关联应用表id
        /// </summary>	
        [EntityTracker(Label = "关联应用表id", Description = "关联应用表id")]
        public int o_app_id { get; set; }
        /// <summary>
        /// 统计总数
        /// </summary>	
        [EntityTracker(Label = "统计总数", Description = "统计总数")]
        public int o_count { get; set; }
        /// <summary>
        /// 统计日期
        /// </summary>	
        [EntityTracker(Label = "统计日期", Description = "统计日期")]
        public DateTime o_time { get; set; }

    }
}

