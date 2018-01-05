using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    ///<summary>
    ///官网投诉表
    ///</summary>
    public class jmp_gwcomplaint
    {

        /// <summary>
        /// 主键
        /// </summary>
        [EntityTracker(Label = "主键", Description = "主键")]
        public int id { get; set; }

        /// <summary>
        /// 投诉姓名
        /// </summary>
        [EntityTracker(Label = "投诉姓名", Description = "投诉姓名")]
        public string name { get; set; }

        /// <summary>
        /// 投诉手机号码
        /// </summary>
        [EntityTracker(Label = "投诉手机号码", Description = "投诉手机号码")]
        public string telephone { get; set; }


        /// <summary>
        /// 投诉内容
        /// </summary>
        [EntityTracker(Label = "投诉内容", Description = "投诉内容")]
        public string reason { get; set; }

        /// <summary>
        /// 0未处理 1 已处理
        /// </summary>	
        [EntityTracker(Label = "状态", Description = "状态")]
        public int  state { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        [EntityTracker(Label = "处理时间", Description = "处理时间")]
        public DateTime? cltime { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        [EntityTracker(Label = "提交时间", Description = "提交时间")]
        public DateTime tjtime { get; set; }
        /// <summary>

        /// <summary>
        /// 处理人
        /// </summary>	
        [EntityTracker(Label = "处理人", Description = "处理人")]
        public string cluser { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        [EntityTracker(Label = "处理结果", Description = "处理结果")]
        public string result { get; set; }

        /// <summary>
        /// 投诉说明
        /// </summary>
        [EntityTracker(Label = "投诉说明", Description = "投诉说明")]
        public string remarks { get; set; }
    }
}