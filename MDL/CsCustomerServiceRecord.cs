using JMP.Model;
using System;

namespace JMP.MDL
{
    /// <summary>
	/// 客服响应记录表
    /// </summary>
    public class CsCustomerServiceRecord
    {
        /// <summary>
        /// Id
        /// </summary>
        [EntityTracker(Label = "id", Description = "id")]
        public int Id { get; set; }

        /// <summary>
        /// 同一问题的ID
        /// </summary>
        [EntityTracker(Label = "同一问题的ID", Description = "同一问题的ID")]
        public string No { get; set; }
        /// <summary>
        /// 大类[0:运营,1:技术],默认值:0
        /// </summary>
        [EntityTracker(Label = "大类", Description = "大类")]
        public int MainCategory { get; set; }

        /// <summary>
        /// 小类[0:商户注册,1:结算相关,2:技术对接,3:通道配置,4:故障排查],默认值:0
        /// </summary>
        [EntityTracker(Label = "小类", Description = "小类")]
        public int SubCategory { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [EntityTracker(Label = "创建时间", Description = "创建时间")]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// 创建者ID
        /// </summary>
        [EntityTracker(Label = "创建者ID", Description = "创建者ID")]
        public int CreatedByUserId { get; set; }
        /// <summary>
        /// 开发者ID
        /// </summary>
        [EntityTracker(Label = "开发者ID", Description = "开发者ID")]
        public int DeveloperId { get; set; }

        /// <summary>
        /// 开发者邮箱
        /// </summary>
        [EntityTracker(Label = "开发者邮箱", Description = "开发者邮箱")]
        public string DeveloperEmail { get; set; }

        /// <summary>
        /// 提问时间[必填]
        /// </summary>
        [EntityTracker(Label = "提问时间", Description = "提问时间")]
        public DateTime? AskDate { get; set; }

        /// <summary>
        /// 提问截图路径[必填]
        /// </summary>
        [EntityTracker(Label = "提问截图路径", Description = "提问截图路径")]
        public string AskScreenshot { get; set; }

        /// <summary>
        /// 响应时间[必填]
        /// </summary>
        [EntityTracker(Label = "响应时间", Description = "响应时间")]
        public DateTime? ResponseDate { get; set; }

        /// <summary>
        /// 响应截图路径[必填]
        /// </summary>
        [EntityTracker(Label = "响应截图路径", Description = "响应截图路径")]
        public string ResponseScreenshot { get; set; }

        /// <summary>
        /// 处理详情(可追加),追加的内容带上[追加]的字样
        /// </summary>
        [EntityTracker(Label = "处理详情", Description = "处理详情")]
        public string HandleDetails { get; set; }

        /// <summary>
        /// 证据截图(可上传多张,多张以逗号隔开)
        /// </summary>
        [EntityTracker(Label = "证据截图", Description = "证据截图")]
        public string EvidenceScreenshot { get; set; }

        /// <summary>
        /// 处理完成时间
        /// </summary>
        [EntityTracker(Label = "处理完成时间", Description = "处理完成时间")]
        public DateTime? CompletedDate { get; set; }

        /// <summary>
        /// 处理人员ID
        /// </summary>
        [EntityTracker(Label = "处理人员ID", Description = "处理人员ID")]
        public int HandlerId { get; set; }

        /// <summary>
        /// 处理人员姓名
        /// </summary>
        [EntityTracker(Label = "处理人员姓名", Description = "处理人员姓名")]
        public string HandlerName { get; set; }

        /// <summary>
        /// 状态[-1:被打回,0:新建,1:处理中,2:处理完成,3:已关闭(评分后自动设成已关闭状态,已关闭的不能再修改和追加)]
        /// </summary>
        [EntityTracker(Label = "状态", Description = "状态")]
        public int Status { get; set; }

        /// <summary>
        /// 审核状态[0:未审核,1:已审核.默认值:0]
        /// </summary>
        [EntityTracker(Label = "审核状态", Description = "审核状态")]
        public bool AuditStatus { get; set; }

        /// <summary>
        /// 审核人ID
        /// </summary>
        [EntityTracker(Label = "审核人ID", Description = "审核人ID")]
        public int AuditByUserId { get; set; }

        /// <summary>
        /// 审核人姓名
        /// </summary>
        [EntityTracker(Label = "审核人姓名", Description = "审核人姓名")]
        public string AuditByUserName { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        [EntityTracker(Label = "审核时间", Description = "审核时间")]
        public DateTime? AuditDate { get; set; }

        /// <summary>
        /// 审核评级[0:未评级,1:优秀，2:良好，3:合格，4:不合格。在审核时系统根据响应时间自动评分]
        /// </summary>
        [EntityTracker(Label = "审核评级", Description = "审核评级")]
        public int Grade { get; set; }

        /// <summary>
        /// 当天值班人ID
        /// </summary>
        [EntityTracker(Label = "当天值班人ID", Description = "当天值班人ID")]
        public int WatchId { get; set; }

        /// <summary>
        /// 值班人姓名
        /// </summary>
        [EntityTracker(Label = "当天值班人ID", Description = "当天值班人ID", Ignore = true)]
        public string u_realname { get; set; }
        /// <summary>
        /// 处理评级[0:未评级,1:优秀，2:良好，3:合格，4:不合格.由审核人员选择等级]
        /// </summary>
        [EntityTracker(Label = "处理评级[0:未评级,1:优秀，2:良好，3:合格，4:不合格.由审核人员选择等级]", Description = "处理评级[0:未评级,1:优秀，2:良好，3:合格，4:不合格.由审核人员选择等级]")]
        public int HandelGrade { get; set; }
        /// <summary>
        /// 来源ID(父ID)
        /// </summary>
        [EntityTracker(Label = "来源ID(父ID)", Description = "来源ID(父ID)")]
        public int ParentId { get; set; }
        /// <summary>
        /// 是否通知值班人员
        /// </summary>
        [EntityTracker(Label = "是否通知值班人员", Description = "是否通知值班人员")]
        public bool NotifyWatcher { get; set; }
        /// <summary>
        /// 通知值班人员时间
        /// </summary>
        [EntityTracker(Label = "通知值班人员时间", Description = "通知值班人员时间")]
        public DateTime? NotifyDate { get; set; }

        /// <summary>
        /// 是否复制一条记录
        /// </summary>
        [EntityTracker(Label = "是否复制一条记录", Description = "是否复制一条记录", Ignore = true)]
        public bool IsCopy { get; set; }
    }
}
