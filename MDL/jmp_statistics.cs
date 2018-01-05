using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMP.Model;

namespace JMP.MDL
{
    ///<summary>
    ///手机品牌统计
    ///</summary>
    public class jmp_statistics
    {

        /// <summary>
        /// id主键
        /// </summary>		
        [EntityTracker(Label = "id主键", Description = "id主键")]
        public int s_id { get; set; }

        /// <summary>
        /// 手机品牌
        /// </summary>	
        [EntityTracker(Label = "手机品牌", Description = "手机品牌")]
        public string s_brand { get; set; }

        /// <summary>
        /// 关联应用表id
        /// </summary>		
        [EntityTracker(Label = "关联应用表id", Description = "关联应用表id")]
        public int s_app_id { get; set; }

        /// <summary>
        /// 统计总数
        /// </summary>		
        [EntityTracker(Label = "统计总数", Description = "统计总数")]
        public int s_count { get; set; }

        /// <summary>
        /// 统计日期
        /// </summary>		
        [EntityTracker(Label = "统计日期", Description = "统计日期")]
        public DateTime s_time { get; set; }

    }
}
