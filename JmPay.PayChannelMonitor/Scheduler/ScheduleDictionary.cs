using System;
using System.Collections.Generic;
using JmPay.PayChannelMonitor.DataLoader;
using JmPay.PayChannelMonitor.MonitorCentre.App;
using JmPay.PayChannelMonitor.MonitorCentre.Task;
using JmPay.PayChannelMonitor.Util;
using TOOL.EnumUtil;
using TOOL.Message.MessageSender;

namespace JmPay.PayChannelMonitor.Scheduler
{
    /// <summary>
    /// 任务字典类
    /// </summary>
    public class ScheduleDictionary
    {

        /// <summary>
        /// 任务字典
        /// </summary>
        private readonly Dictionary<string, Action> _scheduleDict;
        /// <summary>
        /// 任务字典类构造函数
        /// </summary>
        public ScheduleDictionary()
        {
            _scheduleDict = new Dictionary<string, Action>();
        }

        public event Action<string, Level?> OnDoingJob;

        /// <summary>
        /// 任务字典
        /// </summary>
        public Dictionary<string, Action> ScheduleDict
        {
            get { return _scheduleDict; }
        }
        /// <summary>
        /// 初始化任务字典
        /// </summary>
        public void InitDict()
        {
            _scheduleDict.Add(ScheduleGroupCode.SCHE_AUDITOR_MESSAGE_SCHEDULER_MONITOR.ToString(), () =>
            {
                try
                {
                    var monitor = new OrderAbnormalTask(ScheduleGroupCode.SCHE_AUDITOR_MESSAGE_SCHEDULER_MONITOR);
                    monitor.OnDoingWork += DoingJob;
                    monitor.AudioMessageSender = new ChuangLanAudioMessageSender();
                    monitor.TextMessageSender = new ChuangLanTextMessageSender();
                    monitor.DoMonitorTask(new OrderAbnormalLoader());
                }
                catch (Exception ex)
                {
                    DoingJob(string.Format("任务出错,原因:{0}", ex), Level.Error);
                }
            });
            _scheduleDict.Add(ScheduleGroupCode.PROC_MONITOR_APP_PAY_SUCCESS_RATIO.ToString(), () =>
            {
                var monitor = new PaySuccessRatioMonitor(
                    ProcNameCollection.MonitorForAppPaySuccessRatioProcName
                    , ScheduleGroupCode.PROC_MONITOR_APP_PAY_SUCCESS_RATIO);
                monitor.OnDoingWork += DoingJob;
                monitor.AudioMessageSender = new ChuangLanAudioMessageSender();
                monitor.TextMessageSender = new ChuangLanTextMessageSender();
                monitor.DoMonitorTask(new AppMonitorPaySuccessRatioDataLoader());
            });
            _scheduleDict.Add(ScheduleGroupCode.PROC_GET_NO_ORDERS_APP_FROM_TIMESPAN.ToString(), () =>
            {
                var monitor = new NoOrderMonitor(
                    ProcNameCollection.MonitorForAppNoOrderProcName
                    , ScheduleGroupCode.PROC_GET_NO_ORDERS_APP_FROM_TIMESPAN);
                monitor.OnDoingWork += DoingJob;
                monitor.TextMessageSender = new ChuangLanTextMessageSender();
                monitor.AudioMessageSender = new ChuangLanAudioMessageSender();
                monitor.DoMonitorTask(new AppMonitorNoOrderDataLoader());
            });
            _scheduleDict.Add(ScheduleGroupCode.PROC_MONITOR_APP_AMOUNT_SUCCESS_RATIO.ToString(), () =>
            {
                var monitor = new AmountSuccessRatioMonitor(
                    ProcNameCollection.MonitorForAppAmountSuccessRatioProcName
                    , ScheduleGroupCode.PROC_MONITOR_APP_AMOUNT_SUCCESS_RATIO);
                monitor.OnDoingWork += DoingJob;
                monitor.TextMessageSender = new ChuangLanTextMessageSender();
                monitor.AudioMessageSender = new ChuangLanAudioMessageSender();
                monitor.DoMonitorTask(new AppMonitorPayAmountDataLoader());
            });

            _scheduleDict.Add(ScheduleGroupCode.PROC_MONITOR_CHANNEL_NO_ORDER_WITH_MINUTES.ToString(), () =>
            {
                var monitor = new MonitorCentre.Channel.NoOrderMonitor(
                    ProcNameCollection.MonitorForChannelOnOrderProcName
                    , ScheduleGroupCode.PROC_MONITOR_CHANNEL_NO_ORDER_WITH_MINUTES);
                monitor.OnDoingWork += DoingJob;
                monitor.TextMessageSender = new ChuangLanTextMessageSender();
                monitor.AudioMessageSender = new ChuangLanAudioMessageSender();
                monitor.DoMonitorTask(new ChannelMonitorNoOrderDataLoader());
            });

            _scheduleDict.Add(ScheduleGroupCode.SCHE_MONITOR_WORK_ORDER_REMIND.ToString(), () =>
            {
                try
                {
                    var monitor = new WorkOrderRemindTask(ScheduleGroupCode.SCHE_MONITOR_WORK_ORDER_REMIND);
                    monitor.OnDoingWork += DoingJob;
                    monitor.AudioMessageSender = new ChuangLanAudioMessageSender();
                    monitor.TextMessageSender = new ChuangLanTextMessageSender();
                    monitor.DoMonitorTask(new WorkOrderRemindLoader());
                }
                catch (Exception ex)
                {
                    DoingJob(string.Format("任务出错,原因:{0}", ex), Level.Error);
                }
            });

        }

        private void Monitor_OnDoingWork(string message)
        {
            DoingJob(message);
        }

        protected virtual void DoingJob(string mesage, Level? level = null)
        {
            if (OnDoingJob != null)
            {
                OnDoingJob.Invoke(mesage, level);
            }
        }
    }
}