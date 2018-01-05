using JMP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.MDL
{
    /// <summary>
    /// 根据时间段设置通道池每次调用支付通道的数量
    /// </summary>
    public class jmp_channel_amount_config
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        [EntityTracker(Label = "自增ID", Description = "自增ID")]
        public int Id { get; set; }
        /// <summary>
        /// 通道池ID
        /// </summary>
        [EntityTracker(Label = "通道池ID", Description = "通道池ID")]
        public int ChannelPoolId { get; set; }
        /// <summary>
        /// 小时
        /// </summary>
        [EntityTracker(Label = "小时", Description = "小时")]
        public int WhichHour { get; set; }
        /// <summary>
        /// 调用量
        /// </summary>
        [EntityTracker(Label = "调用量", Description = "调用量")]
        public int Amount { get; set; }
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
        /// 创建者名称
        /// </summary>
        [EntityTracker(Label = "创建者名称", Description = "创建者名称")]
        public string CreatedByUserName { get; set; }

    }
}
