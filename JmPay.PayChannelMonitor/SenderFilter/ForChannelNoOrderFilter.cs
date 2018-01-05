using System.Collections.Generic;
using JmPay.PayChannelMonitor.MessageSendCollection;
using JmPay.PayChannelMonitor.Models;

namespace JmPay.PayChannelMonitor.SenderFilter
{
    /// <summary>
    /// 应用监控[无订单监控]短信发送者筛选器
    /// </summary>
    public class ForChannelNoOrderFilter : ISenderFilter
    {
        private readonly List<MonitorForChannelNoOrderSinceLatest> _noOrderApps;

        /// <summary>
        /// 允许发送的接收者集合
        /// </summary>
        private List<MonitorForChannelNoOrderSinceLatest> _allowList;

        public ForChannelNoOrderFilter(List<MonitorForChannelNoOrderSinceLatest> noOrderApps)
        {
            _noOrderApps = noOrderApps;
        }

        public void DoFilter()
        {
            if (_noOrderApps == null || _noOrderApps.Count <= 0)
            {
                return ;
            }
            _allowList = new List<MonitorForChannelNoOrderSinceLatest>();
            foreach (var item in _noOrderApps)
            {
                var sender = new NoOrderAppMessageSender
                {
                    Id = item.ChannelId,
                    Name = item.ChannelName,
                    MonitorTypeId = item.MonitorType,
                    MonitorTypeDispay = ""
                };
                var allowSend = NoOrderSenderSingleton.Instance.GetCollection.AllowSend(sender);
                if (allowSend)
                {
                    _allowList.Add(item);
                    NoOrderSenderSingleton.Instance.GetCollection.AddOrUpdate(sender);
                }
            }
        }
        /// <summary>
        /// 允许发送的接收者集合
        /// </summary>
        public List<MonitorForChannelNoOrderSinceLatest> AllowList { get { return _allowList; } }
    }
}
