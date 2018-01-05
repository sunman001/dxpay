using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    /// <summary>
    /// 通道过滤规则配置表
    /// </summary>
    public class jmp_channel_filter_config
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        [EntityTracker(Label = "自增ID", Description = "自增ID")]
        public int Id { get; set; }
        /// <summary>
        /// 过滤类型[0:成功率,1:请求率]
        /// </summary>
        [EntityTracker(Label = "过滤类型", Description = "过滤类型")]
        public int TypeId { get; set; }
        /// <summary>
        /// 过滤对象[0:全局通道过滤,1:指定通道过滤,2:指定通道池过滤]
        /// </summary>
        [EntityTracker(Label = "过滤对象", Description = "过滤对象")]
        public int TargetId { get; set; }
        /// <summary>
        /// 小时[0-23点]
        /// </summary>
        [EntityTracker(Label = "小时", Description = "小时")]
        public int WhichHour { get; set; }
        /// <summary>
        /// 阀值
        /// </summary>
        [EntityTracker(Label = "阀值", Description = "阀值")]
        public decimal Threshold { get; set; }
        /// <summary>
        /// 关联的ID[根据过滤对象可确定关联的ID是什么.如:过滤对象为--指定通道过滤,则关联的ID为通道的ID,如果过滤对象为--指定通道池,则关联的ID为通道池的ID]
        /// </summary>
        [EntityTracker(Label = "关联的ID", Description = "关联的ID")]
        public int RelatedId { get; set; }
        /// <summary>
        /// 单位天。如：今日通道超额后，根据设置的恢复时间间隔来恢复通道。如果设置的一天，那么将在次日凌晨恢复，如果是两天将在两天后次日凌晨恢复
        /// </summary>
        [EntityTracker(Label = "单位天", Description = "单位天")]
        public int IntervalOfRecover { get; set; }
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
        /// 创建者姓名
        /// </summary>
        [EntityTracker(Label = "创建者姓名", Description = "创建者姓名")]
        public string CreatedByUserName { get; set; }

       public string RelatedName { get; set; }

    }
}