using System;

namespace JMP.Model
{
    /// <summary>
    /// 监控分钟设置详情表
    /// </summary>
    public class JmpMonitorMinuteDetails
    {
        [EntityTracker(Label = "id", Description = "id")]
        public int Id { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        [EntityTracker(Label = "应用ID", Description = "应用ID")]
        public int AppId { get; set; }

        /// <summary>
        /// 应用监控类型[0:支付成功率(支付成功数/总支付数),1:xx分钟内无订单,2:金额成功率(成功支付金额/总支付金额)]
        /// </summary>
        [EntityTracker(Label = "应用监控类型", Description = "应用监控类型")]
        public int MonitorType { get; set; }

        /// <summary>
        /// 哪个小时
        /// </summary>
        [EntityTracker(Label = "哪个小时", Description = "哪个小时")]
        public int WhichHour { get; set; }

        /// <summary>
        /// 分钟数
        /// </summary>
        [EntityTracker(Label = "分钟数", Description = "分钟数")]
        public int Minutes { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [EntityTracker(Label = "创建时间", Description = "创建时间")]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// 创建者ID
        /// </summary>
        [EntityTracker(Label = "创建者ID", Description = "创建者ID")]
        public int CreatedById { get; set; }

        /// <summary>
        /// 创建者姓名
        /// </summary>
        [EntityTracker(Label = "创建者姓名", Description = "创建者姓名")]
        public string CreatedByName { get; set; }
    }
}
