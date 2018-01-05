using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    /// <summary>
    /// 排班表
    /// </summary>
    public class jmp_scheduling
    {

        /// <summary>
        /// id
        /// </summary>
        [EntityTracker(Label = "主键", Description = "主键")]
        public int id { get; set; }

        /// <summary>
        /// 值班开始日期
        /// </summary>
        [EntityTracker(Label = "值班开始日期", Description = "值班开始日期")]
        public DateTime watchstartdate { get; set; }

        /// <summary>
        /// 值班结束日期
        /// </summary>
        [EntityTracker(Label = "值班结束日期", Description = "值班结束日期")]
        public DateTime watchenddate { get; set; }

        /// <summary>
        /// 值班人ID
        /// </summary>
        [EntityTracker(Label = "值班人ID", Description = "值班人ID")]
        public int watchid { get; set; }
        /// <summary>
        /// 值班类型 1技术， 2 运营
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [EntityTracker(Label = "创建时间", Description = "创建时间")]
        public DateTime createdon { get; set; }

        /// <summary>
        /// 创建者ID
        /// </summary>
        [EntityTracker(Label = "创建者ID", Description = "创建者ID")]
        public int createdbyid { get; set; }

        /// <summary>
        /// 值班人姓名
        /// </summary>
        [EntityTracker(Label = "值班人姓名", Description = "值班人姓名", Ignore =true)]
        public string u_realname { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        [EntityTracker(Label = "创建人姓名", Description = "创建人姓名", Ignore = true)]
        public string createdby { get; set; }

        /// <summary>
        /// 值班人手机号码
        /// </summary>
        [EntityTracker(Label = "值班人手机号码", Description = "值班人手机号码", Ignore = true)]
        public string u_mobilenumber { get; set; }

        [EntityTracker(Label = "值班人邮箱地址", Description = "值班人邮箱地址", Ignore = true)]
        public string u_emailaddress { get; set; }
        [EntityTracker(Label = "值班人qq", Description = "值班人qq", Ignore = true)]
        public string u_qq { get; set; }
    }
}
