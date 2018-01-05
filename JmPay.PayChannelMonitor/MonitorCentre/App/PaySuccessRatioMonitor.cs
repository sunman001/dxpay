using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using JmPay.PayChannelMonitor.DataLoader;
using JmPay.PayChannelMonitor.Models;
using JmPay.PayChannelMonitor.SenderFilter;
using JMP.DBA;
using TOOL.EnumUtil;

namespace JmPay.PayChannelMonitor.MonitorCentre.App
{
    /// <summary>
    /// 通道XX分钟无订单的监控
    /// </summary>
    public class PaySuccessRatioMonitor : MonitorAbstract<AppPaySuccessRatioSinceLatest>
    {
        /// <summary>
        /// 存储过程名称
        /// </summary>
        private readonly string _procName;

        private List<AppPaySuccessRatioSinceLatest> _data;

        private string _message;
        private readonly string _groupName;

        public PaySuccessRatioMonitor(string procName, ScheduleGroupCode groupCode) : base(groupCode)
        {
            _procName = procName;
            _groupName = groupCode.GetDescription();
        }

        protected override string MonitroName { get { return _groupName; } }
        public override string ContextParm { get; set; }

        protected override void SendTextMessage()
        {
            TextMessageSender.SendMessage(_receivers, _message);
        }

        protected override void SendAudioMessage()
        {
            var response = AudioMessageSender.SendMessage(TelTemp, _receivers, _message, ContextParm);
            DoingWork(string.Format("语音消息发送完成,响应代码:{0},消息:{1}", response.Code, response.Msg));
        }

        protected override void LoadData(IDataLoader dataLoader)
        {
            var ds = (DataSet)dataLoader.Load(_procName);
            _data = DbHelperSQL.ConvertToList<AppPaySuccessRatioSinceLatest>(ds.Tables[0]).ToList();
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
                    app_id = o.AppId,
                    app_name = o.AppName,
                    created_on = DateTime.Now,
                    is_send_message = 1,
                    message_send_time = DateTime.Now,
                    message = string.Format("应用[{0}:{1}]支付成功率异常,当前{2}分钟内的成功率为:{3},设定的阀值为:{4},请核实!", o.AppId, o.AppName, o.Minutes, o.SuccessRatio, o.ThresholdSuccessRatio)
                });
            }
            DoingWork("数据持久化完成,准备筛选...");
        }

        protected override void Filter()
        {
            var filter = new ForAppPaySuccessRatioFilter(_data);
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
            if (ScheduleGroup.MessageTemplate == null)
            {
                if (AllowSenders.Count <= 5)
                {
                    _message = string.Format("支付平台应用:[{0}] {1}个应用在设定时间内的支付成功率异常,请核实！", string.Join(",", AllowSenders.Select(x => string.Format("{0}:{1}", x.AppId, x.AppName))), AllowSenders.Count);
                }
                else if (AllowSenders.Count > 5)
                {
                    _message = string.Format("支付平台应用:[{0}]...等{1}个应用在设定时间内的支付成功率异常,请核实！", string.Join(",", AllowSenders.Take(5).Select(x => x.AppName)), AllowSenders.Count);
                }
            }
            else
            {
                _message = ScheduleGroup.MessageTemplate.Replace("{app_name}", string.Join(",", AllowSenders.Take(5).Select(x => x.AppName)))
                    .Replace("{app_count}", AllowSenders.Count.ToString());
            }
            ContextParm = "name:应用,item:当前有" + AllowSenders.Count + "个";
        }
    }
}
