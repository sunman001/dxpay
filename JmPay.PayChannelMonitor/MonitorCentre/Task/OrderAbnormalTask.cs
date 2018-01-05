using System.Collections.Generic;
using System.Linq;
using JmPay.PayChannelMonitor.DataLoader;
using TOOL.EnumUtil;

namespace JmPay.PayChannelMonitor.MonitorCentre.Task
{
    public class OrderAbnormalTask : MonitorAbstract<JMP.MDL.jmp_order_audit>
    {
        public OrderAbnormalTask(ScheduleGroupCode groupCode) : base(groupCode)
        {
            _groupName = groupCode.GetDescription();
        }

        private IEnumerable<JMP.MDL.jmp_order_audit> _data;
        private readonly string _groupName;
        private string _message;

        protected override string MonitroName { get { return _groupName; } }
        public override string ContextParm { get; set; }
        protected override void LoadData(IDataLoader dataLoader)
        {
            _data = dataLoader.Load() as List<JMP.MDL.jmp_order_audit>;

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
            _receivers = ScheduleGroup.NotifyMobileList;
            if (ScheduleGroup.MessageTemplate == null)
            {
                if (auditors <= 5)
                {
                    _message = string.Format("支付平台订单[{0}] {1}个订单支付异常,请核实！", string.Join(",", _data.Select(x => x.order_code)), auditors);
                }
                else if (auditors > 5)
                {
                    _message = string.Format("支付平台订单[{0}]...等{1}个订单支付异常,请核实！", string.Join(",", _data.Select(x => x.order_code)), auditors);
                }
            }
            else
            {
                _message =
                    ScheduleGroup.MessageTemplate.Replace("{app_name}", string.Join(",", _data.Select(x => x.order_code)))
                        .Replace("{app_count}", auditors.ToString());
            }
        }

        protected override void SendTextMessage()
        {
            TextMessageSender.SendMessage(_receivers,_message);
        }

        protected override void SendAudioMessage()
        {
            AudioMessageSender.SendMessage(TelTemp, _receivers, _message, "");
        }

        protected override void Completed()
        {
            var bll=new JMP.BLL.jmp_order_audit();
            bll.SetSentMessage();
            base.Completed();
        }
    }
}
