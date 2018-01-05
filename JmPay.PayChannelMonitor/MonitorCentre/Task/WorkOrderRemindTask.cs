using System.Collections.Generic;
using System.Linq;
using JmPay.PayChannelMonitor.DataLoader;
using JmPay.PayChannelMonitor.Util;
using TOOL.EnumUtil;

namespace JmPay.PayChannelMonitor.MonitorCentre.Task
{
    /// <summary>
    /// 工单系统新工单提醒任务
    /// </summary>
    public class WorkOrderRemindTask : MonitorAbstract<JMP.MDL.jmp_workorder>
    {
        public WorkOrderRemindTask(ScheduleGroupCode groupCode) : base(groupCode)
        {
            _groupName = groupCode.GetDescription();
        }

        private IEnumerable<JMP.MDL.jmp_workorder> _data;
        private readonly string _groupName;
        private string _message;

        protected override string MonitroName { get { return _groupName; } }
        public override string ContextParm { get; set; }
        protected override void LoadData(IDataLoader dataLoader)
        {
            _data = dataLoader.Load() as List<JMP.MDL.jmp_workorder>;

        }

        protected override void Process()
        {
            DoingWork("跳过处理,准备持久化...");
        }

        protected override void Persistent()
        {
            DoingWork("跳过持久化...");
        }

        protected override void Filter()
        {
            AllowSenders = _data.ToList();
            BuildMessageContent();
        }

        protected override void BuildMessageContent()
        {
            var auditors = _data.Count();
            if (auditors <= 0)
            {
                AllowContinue = false;
                return;
            }
            //_receivers = ScheduleGroup.NotifyMobileList;
            LoadReceivers();
            _message = ScheduleGroup.MessageTemplate == null ? "工单系统有新的工单产生,请即时处理！" : ScheduleGroup.MessageTemplate.Replace("{app_name}", "").Replace("{app_count}", auditors.ToString());
        }
        /// <summary>
        /// 加载消息接收者
        /// </summary>
        private void LoadReceivers()
        {
            var scheBll = new JMP.BLL.jmp_scheduling();
            var watchers = scheBll.FindAllWatcherOfTheDay();
            if (watchers != null && watchers.Count > 0)
            {
                _receivers = string.Join(",", watchers.Select(x => x.MobileNumber));
            }
            if (string.IsNullOrEmpty(_receivers))
            {
                _receivers = ScheduleGroup.NotifyMobileList;
            }
        }

        protected override void SendTextMessage()
        {
            TextMessageSender.SendMessage(_receivers, _message);
        }

        protected override void SendAudioMessage()
        {
            var response = AudioMessageSender.SendMessage(TelTemp, _receivers, _message, "");
            if (!response.Success)
            {
                DoingWork("发送语音消息时出错,原因:" + response.Msg, Level.Error);
            }
        }

    }
}
