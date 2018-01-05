using System;

namespace JmPay.PayChannelMonitor.Models
{
    /// <summary>
    /// 指定时间内无订单的应用的查询实体类
    /// </summary>
    public class NoOrdersAppSinceLatest
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        public int AppId { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        public string AppName { get; set; }
        /// <summary>
        /// 应用最近一次下单时间
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
