﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using JmPay.PayChannelMonitor.DataLoader;
using JmPay.PayChannelMonitor.Models;
using JmPay.PayChannelMonitor.SenderFilter;
using JMP.DBA;
using TOOL;
using TOOL.EnumUtil;

namespace JmPay.PayChannelMonitor.MonitorCentre.Channel
{
    /// <summary>
    /// 通道XX分钟无订单的监控
    /// </summary>
    public class NoOrderMonitor : MonitorAbstract<MonitorForChannelNoOrderSinceLatest>
    {
        /// <summary>
        /// 存储过程名称
        /// </summary>
        private readonly string _procName;

        private List<MonitorForChannelNoOrderSinceLatest> _data;

        private string _message;
        private readonly string _groupName;


        public NoOrderMonitor(string procName, ScheduleGroupCode groupCode) : base(groupCode)
        {
            _procName = procName;
            _groupName = groupCode.GetDescription();
        }

        protected override string MonitroName { get { return _groupName; } }
        public override string ContextParm { get; set; }

        protected override void LoadData(IDataLoader dataLoader)
        {
            var ds = (DataSet)dataLoader.Load(_procName);
            _data = DbHelperSQL.ConvertToList<MonitorForChannelNoOrderSinceLatest>(ds.Tables[0]).ToList();
            DoingWork(string.Format("数据加载完成,共{0}条数据,准备处理...", _data.Count));
            if (_data == null || _data.Count <= 0)
            {
                AllowContinue = false;
            }
        }

        protected override void Process()
        {
            DoingWork("跳过处理,准备持久化...");
        }

        protected override void Persistent()
        {
            foreach (var o in AllowSenders)
            {
                var bll = new JMP.BLL.jmp_app_request_audit();
                bll.Add(new JMP.MDL.jmp_app_request_audit
                {
                    app_id = o.ChannelId,
                    app_name = o.ChannelName,
                    created_on = DateTime.Now,
                    is_send_message = 1,
                    message_send_time = DateTime.Now,
                    type = 1,
                    message = string.Format("通道[{0}:{1}]已有{2}无订单产生,上次订单时间[{3}],请核实!", o.ChannelId, o.ChannelName, DateTime.Now.AddSeconds(-o.LatestOrderTimespan).TimeAgo(""), o.LatestPayTime)
                });
            }
            DoingWork("数据持久化完成,准备筛选...");
        }

        protected override void Filter()
        {
            var filter = new ForChannelNoOrderFilter(_data);
            filter.DoFilter();
            AllowSenders = filter.AllowList;
            if (AllowSenders.Count <= 0)
            {
                AllowContinue = false;
                return;
            }
            DoingWork(string.Format("数据筛选完成,共{0}条数据,准备构建消息内容...", _data.Count));
            BuildMessageContent();
        }

        protected override void BuildMessageContent()
        {
            base.BuildMessageContent();
            _receivers = ScheduleGroup.NotifyMobileList;
            var items = "";
            var count = 0;
            if (ScheduleGroup.MessageTemplate == null)
            {

                if (AllowSenders.Count <= 5)
                {
                    items = string.Join(",", AllowSenders.Select(x => string.Format("{0}:{1}", x.ChannelId, x.ChannelName)));
                    count = AllowSenders.Count;
                    _message = string.Format("支付平台通道:[{0}] {1}个通道在设定时间内无订单产生,请核实！", items, count);
                }
                else if (AllowSenders.Count > 5)
                {
                    items = string.Join(",", AllowSenders.Take(5).Select(x => x.ChannelName));
                    count = AllowSenders.Count;
                    _message = string.Format("支付平台通道:[{0}]...等{1}个通道在设定时间内无订单产生,请核实！", items, count);
                }
                ContextParm = "name:通道,item:" + count;
            }
            else
            {
                items = string.Join(",", AllowSenders.Take(5).Select(x => x.ChannelName));
                count = AllowSenders.Count;
                _message = ScheduleGroup.MessageTemplate.Replace("{app_name}", items)
                    .Replace("{app_count}", count.ToString());
                ContextParm = "name:通道,item:当前有" + count + "个";
            }
        }

        protected override void SendTextMessage()
        {
            TextMessageSender.SendMessage(_receivers, _message);
        }

        protected override void SendAudioMessage()
        {
            var response = AudioMessageSender.SendMessage(TelTemp, _receivers, _message, ContextParm);
            DoingWork(string.Format("语音消息发送完成,响应代码:{0},消息:{1}", response.Code, response.Msg));
        }
    }
}
