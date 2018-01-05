using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    ///<summary>
    ///留存用户统计
    ///</summary>
    public class jmp_keep
    {

        /// <summary>
        /// 主键id
        /// </summary>	
        [EntityTracker(Label = "支付类型名称", Description = "支付类型名称")]
        public int k_id { get; set; }
        /// <summary>
        /// 关联应用表id
        /// </summary>	
        [EntityTracker(Label = "关联应用表id", Description = "关联应用表id")]
        public int k_app_id { get; set; }
        /// <summary>
        /// 所属类型(0：新增，1：活跃)
        /// </summary>		
        [EntityTracker(Label = "所属类型", Description = "所属类型")]
        public int k_type { get; set; }
        /// <summary>
        /// 用户数量
        /// </summary>
        [EntityTracker(Label = "用户数量", Description = "用户数量")]
        public int k_usercount { get; set; }
        /// <summary>
        /// 留存1天
        /// </summary>	
        [EntityTracker(Label = "留存1天", Description = "留存1天")]
        public decimal k_day1 { get; set; }
        /// <summary>
        /// 留存2天
        /// </summary>
        [EntityTracker(Label = "留存2天", Description = "留存2天")]
        public decimal k_day2 { get; set; }
        /// <summary>
        /// 留存3天
        /// </summary>	
        [EntityTracker(Label = "留存3天", Description = "留存3天")]
        public decimal k_day3 { get; set; }
        /// <summary>
        /// 留存4天
        /// </summary>
        [EntityTracker(Label = "留存4天", Description = "留存4天")]
        public decimal k_day4 { get; set; }
        /// <summary>
        /// 留存5天
        /// </summary>	
        [EntityTracker(Label = "留存5天", Description = "留存5天")]
        public decimal k_day5 { get; set; }
        /// <summary>
        /// 留存6天
        /// </summary>	
        [EntityTracker(Label = "留存6天", Description = "留存6天")]
        public decimal k_day6 { get; set; }
        /// <summary>
        /// 留存7天
        /// </summary>		
        [EntityTracker(Label = "留存7天", Description = "留存7天")]
        public decimal k_day7 { get; set; }
        /// <summary>
        /// 留存14天
        /// </summary>	
        [EntityTracker(Label = "留存14天", Description = "留存14天")]
        public decimal k_day14 { get; set; }
        /// <summary>
        /// 留存30天
        /// </summary>	
        [EntityTracker(Label = "留存30天", Description = "留存30天")]
        public decimal k_day30 { get; set; }
        /// <summary>
        /// 统计日期
        /// </summary>
        [EntityTracker(Label = "统计日期", Description = "统计日期")]
        public DateTime k_time { get; set; }

    }
}

