using System;
using System.Collections.Generic;

namespace JmPay.PayChannelMonitor.MessageSendCollection
{
    /// <summary>
    /// 无订单短信通知者收集器
    /// </summary>
    public class NoOrderAppMessageSendCollection
    {
        /// <summary>
        /// 每个监控对象两次发送短信的最短间隔时间
        /// </summary>
        private readonly int _intervalBetweenSendMessage = 5;
        /// <summary>
        /// 无订单短信通知者收集器
        /// </summary>
        /// <param name="intervalBetweenSendMessage">每个监控对象两次发送短信的最短间隔时间(默认:5分钟)</param>
        public NoOrderAppMessageSendCollection(int intervalBetweenSendMessage)
        {
            _intervalBetweenSendMessage = intervalBetweenSendMessage;
            NoOrderAppMessageSenders = new List<NoOrderAppMessageSender>();
        }
        public List<NoOrderAppMessageSender> NoOrderAppMessageSenders { get; set; }

        public void AddOrUpdate(NoOrderAppMessageSender sender)
        {
            if (!Exists(sender.Id, sender.MonitorTypeId))
            {
                sender.LatestSendMessageDate = DateTime.Now;
                NoOrderAppMessageSenders.Add(sender);
            }
            else
            {
                Update(sender);
            }
        }

        private bool Exists(int id, int monitorType)
        {
            return NoOrderAppMessageSenders.Exists(x => x.Id == id && x.MonitorTypeId == monitorType);
        }

        public NoOrderAppMessageSender Find(int id, int monitorType)
        {
            var item = NoOrderAppMessageSenders.Find(x => x.Id == id && x.MonitorTypeId == monitorType);
            return item;
        }

        private void Update(NoOrderAppMessageSender sender)
        {
            if (sender == null)
            {
                return;
            }
            var item = Find(sender.Id, sender.MonitorTypeId);
            if (item == null)
            {
                return;
            }
            item.LatestSendMessageDate = DateTime.Now;
        }

        public bool AllowSend(NoOrderAppMessageSender sender)
        {
            var item = Find(sender.Id, sender.MonitorTypeId);
            if (item == null)
            {
                return true;
            }
            if (item.LatestSendMessageDate == null)
            {
                return true;
            }
            if (((DateTime.Now - item.LatestSendMessageDate).Value.TotalMilliseconds / 1000) < _intervalBetweenSendMessage * 60)
            {
                return false;
            }

            return true;
        }
    }
}
