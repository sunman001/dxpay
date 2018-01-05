using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    /// <summary>
    /// 投诉每日汇总表
    /// </summary>
    public class CsComplainArchive
    {
        /// <summary>
        /// Id
        /// </summary>
        [EntityTracker(Label = "Id", Description = "Id")]
        public int Id { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        [EntityTracker(Label = "应用ID", Description = "应用ID")]
        public int AppId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [EntityTracker(Label = "用户ID", Description = "用户ID")]
        public int UserId { get; set; }

        /// <summary>
        /// 投诉类型ID
        /// </summary>
        [EntityTracker(Label = "投诉类型ID", Description = "投诉类型ID")]
        public int ComplainTypeId { get; set; }

        /// <summary>
        /// 分组合计
        /// </summary>
        [EntityTracker(Label = "分组合计", Description = "分组合计")]
        public int Amount { get; set; }
        /// <summary>
        /// 归档日期
        /// </summary>
        [EntityTracker(Label = "归档日期", Description = "归档日期")]
        public DateTime ArchiveDay { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [EntityTracker(Label = "创建日期", Description = "创建日期")]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// 开发者名称
        /// </summary>
        public string u_realname { get; set; }

        /// <summary>
        /// 开发者所属商务类型(1:商务 2：代理商)
        /// </summary>
        public int relation_type { get; set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        public string a_name { get; set; }
        public Decimal sumcount { get; set; }
        /// <summary>
        /// 平均响应时间
        /// </summary>
        public int AvgHandleTime { get; set; }


    }
}