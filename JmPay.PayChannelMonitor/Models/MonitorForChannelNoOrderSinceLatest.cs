using System;

namespace JmPay.PayChannelMonitor.Models
{
    /// <summary>
    /// 通道监控[XX分钟内无订单]查询实体类
    /// </summary>
    public class MonitorForChannelNoOrderSinceLatest
    {
        /// <summary>
        /// 通道ID
        /// </summary>
        public int ChannelId { get; set; }
        /// <summary>
        /// 通道名称
        /// </summary>
        public string ChannelName { get; set; }
        /// <summary>
        /// 通道最近一次下单时间
        /// </summary>
        public DateTime LatestPayTime { get; set; }
        /// <summary>
        /// 自上次下单起,到现在的时间间隔(单位:秒)
        /// </summary>
        public int LatestOrderTimespan { get; set; }
        /// <summary>
        /// 哪一个小时
        /// </summary>
        public int WhichHour { get; set; }
        /// <summary>
        /// 分钟数
        /// </summary>
        public int Minutes { get; set; }
        /// <summary>
        /// 监控类型
        /// </summary>
        public int MonitorType { get; set; }
    }
}
