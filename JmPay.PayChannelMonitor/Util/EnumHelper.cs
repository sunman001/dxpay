using System.ComponentModel;

namespace JmPay.PayChannelMonitor.Util
{
    /// <summary>
    /// 消息级别枚举
    /// </summary>
    public enum Level
    {
        Critical = 0,
        Error = 1,
        Warning = 2,
        Info = 3,
        Verbose = 4,
        Debug = 5,
        Default = 6
    }

    public enum ScheduleStatus
    {
        [Description("挂起")]
        Pending,
        [Description("运行")]
        Running
    }
}
