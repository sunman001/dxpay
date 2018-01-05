using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    /// <summary>
    /// 工单交流表
    /// </summary>
    public class jmp_exchange
    {
        /// <summary>
        /// id
        /// </summary>
        [EntityTracker(Label = "主键", Description = "主键")]
        public int id { get; set; }

        /// <summary>
        /// 工单表id
        /// </summary>
        [EntityTracker(Label = "工单表id", Description = "工单表id")]
        public int workorderid { get; set; }

        /// <summary>
        /// 交流人ID
        /// </summary>
        [EntityTracker(Label = "交流人ID", Description = "交流人ID")]
        public int handlerid { get; set; }

        /// <summary>
        /// 交流时间
        /// </summary>
        [EntityTracker(Label = "交流时间", Description = "交流时间")]
        public DateTime handledate { get; set; }

        /// <summary>
        /// 交流描述
        /// </summary>
        [EntityTracker(Label = "交流描述", Description = "交流描述")]
        public string handleresultdescription { get; set; }

        //值班人
        [EntityTracker(Label = "值班人", Description = "值班人",Ignore =true)]
        public string name { get; set; }

        //提交人
        [EntityTracker(Label = "提交人", Description = "提交人", Ignore = true)]
        public string creartbyname { get; set; }
    }
}
