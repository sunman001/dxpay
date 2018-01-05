using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMP.Model;

namespace JMP.MDL
{
    ///<summary>
    ///流量趋势汇总
    ///</summary>
    public class jmp_trends
    {

        /// <summary>
        /// 主键id
        /// </summary>	
        [EntityTracker(Label = "主键id", Description = "主键id")]
        public int t_id { get; set; }

        /// <summary>
        /// 关联应用表id
        /// </summary>	
        [EntityTracker(Label = "关联应用表id", Description = "关联应用表id")]
        public int t_app_id { get; set; }

        /// <summary>
        /// 每日新增总数
        /// </summary>
        [EntityTracker(Label = "每日新增总数", Description = "每日新增总数")]
        public int t_newcount { get; set; }

        /// <summary>
        /// 每日活跃总数
        /// </summary>	
        [EntityTracker(Label = "每日活跃总数", Description = "每日活跃总数")]
        public int t_activecount { get; set; }

        /// <summary>
        /// 汇总时间
        /// </summary>		
        [EntityTracker(Label = "汇总时间", Description = "汇总时间")]
        public DateTime t_time { get; set; }

    }
}
