using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMP.Model;

namespace JMP.MDL
{
    ///<summary>
    ///省份统计
    ///</summary>
    public class jmp_province
    {

        /// <summary>
        /// 主键
        /// </summary>	
        [EntityTracker(Label = "主键", Description = "主键")]
        public int p_id { get; set; }

        /// <summary>
        /// 省份
        /// </summary>	
        [EntityTracker(Label = "省份", Description = "省份")]
        public string p_province { get; set; }

        /// <summary>
        /// 应用id
        /// </summary>	
        [EntityTracker(Label = "应用id", Description = "应用id")]
        public int p_appid { get; set; }

        /// <summary>
        /// 统计总数
        /// </summary>		
        [EntityTracker(Label = "统计总数", Description = "统计总数")]
        public int p_count { get; set; }

        /// <summary>
        /// 统计日期
        /// </summary>		
        [EntityTracker(Label = "统计日期", Description = "统计日期")]
        public DateTime p_time { get; set; }

    }
}
