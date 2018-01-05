using JMP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace JMP.MDL
{
    ///<summary>
    ///调单设置
    ///</summary>
    public class jmp_dispatchorder
    {

        /// <summary>
        /// 主键
        /// </summary>	
        [EntityTracker(Label = "主键", Description = "主键")]
        public int d_id { get; set; }
        /// <summary>
        /// 应用类型id
        /// </summary>	
        [EntityTracker(Label = "应用类型id", Description = "应用类型id")]
        public int d_apptyeid { get; set; }
        /// <summary>
        /// 调单比例
        /// </summary>	
        [EntityTracker(Label = "调单比例", Description = "调单比例")]
        public decimal d_ratio { get; set; }
        /// <summary>
        /// 状态(0:正常，1冻结)
        /// </summary>
        [EntityTracker(Label = "状态", Description = "状态")]
        public int d_state { get; set; }
        /// <summary>
        /// 设置时间
        /// </summary>	
        [EntityTracker(Label = "设置时间", Description = "设置时间")]
        public DateTime d_datatime { get; set; }
        /// <summary>
        /// 应用类型名称
        /// </summary>
        [EntityTracker(Label = "应用类型名称", Description = "应用类型名称")]
        public string t_name { get; set; }

    }
}
