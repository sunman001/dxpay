using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    /// <summary>
    /// 代付通道
    /// </summary>
    public class PayChannel
    {
        /// <summary>
        /// 主键
        /// </summary>
        [EntityTracker(Label = "主键", Description = "主键")]
        public int Id { get; set; }

        /// <summary>
        /// 通道名称
        /// </summary>
        [EntityTracker(Label = "通道名称", Description = "通道名称")]
        public string ChannelName { get; set; }

        /// <summary>
        /// 通道标识
        /// </summary>
        [EntityTracker(Label = "通道标识", Description = "通道标识")]
        public string ChannelIdentifier { get; set; }

        /// <summary>
        /// 添加人姓名
        /// </summary>
        [EntityTracker(Label = "添加人姓名", Description = "添加人姓名")]
        public string Append { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [EntityTracker(Label = "添加时间", Description = "添加时间")]
        public DateTime Appendtime { get; set; }

    }
}