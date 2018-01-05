using System;

namespace JmPay.PayChannelMonitor.MessageSendCollection
{
    /// <summary>
    /// 无订单短信通知者
    /// </summary>
    public class NoOrderAppMessageSender
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 监控类型ID
        /// </summary>
        public int MonitorTypeId { get; set; }
        /// <summary>
        /// 监控类型名称
        /// </summary>
        public string MonitorTypeDispay { get; set; }
        /// <summary>
        /// 最近发送时间
        /// </summary>
        public DateTime? LatestSendMessageDate { get; set; }
    }
}
