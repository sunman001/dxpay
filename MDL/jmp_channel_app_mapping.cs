using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    /// <summary>
    /// [通道池]通道池-应用映射表
    /// </summary>
    public class jmp_channel_app_mapping
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
        public int ChannelId { get; set; }
        /// <summary>
        /// 应用ID
        /// </summary>
        [EntityTracker(Label = "应用ID", Description = "应用ID")]
        public int AppId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [EntityTracker(Label = "创建时间", Description = "创建时间")]
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// 创建者ID
        /// </summary>
        [EntityTracker(Label = "创建者ID", Description = "创建者ID")]
        public int CreatedByUerId { get; set; }
        /// <summary>
        /// 创建者名称
        /// </summary>
        [EntityTracker(Label = "创建者名称", Description = "创建者名称")]
        public string CreatedByUserName { get; set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        [EntityTracker(Label = "应用名称", Description = "应用名称", Ignore = true)]
        public string a_name { get; set; }

        /// <summary>
        /// 通道池名称
        /// </summary>
        [EntityTracker(Label = "通道池名称", Description = "通道池名称", Ignore = true)]
        public string PoolName { get; set; }


    }
}