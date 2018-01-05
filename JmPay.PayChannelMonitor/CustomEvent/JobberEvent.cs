using System;

namespace JmPay.PayChannelMonitor.CustomEvent
{
    public class JobberEvent :EventArgs
    {
        /// <summary>
        /// 当前JobManager中所有的计划数
        /// </summary>
        public int ScheduleCount { get; set; }
    }
}
