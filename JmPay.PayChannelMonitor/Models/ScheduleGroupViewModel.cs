using JmPay.PayChannelMonitor.Util;

namespace JmPay.PayChannelMonitor.Models
{
    public class ScheduleGroupViewModel
    {
        /*
         * x.Name,
                x.Code,
                x.IntervalValue,
                x.IntervalUnit,
                x.MessageTemplate
                IsDeleted=scheduleGroup.IsDeleted,
                        IsEnabled=scheduleGroup.IsEnabled,
         */
        public string Name { get; set; }
        public string Code { get; set; }
        public int IntervalValue { get; set; }
        public string IntervalUnit { get; set; }
        public string MessageTemplate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsEnabled { get; set; }
        public ScheduleStatus Status { get; set; }
        
    }
}
