using System;
using FluentScheduler;

namespace JmPay.PayChannelMonitor
{
    public class TaskRegistry : Registry
    {
        public TaskRegistry()
        {
            Schedule(() => { }).WithName("[five minutes]").ToRunOnceAt(DateTime.Now.AddMinutes(5)).AndEvery(5).Minutes();
        }
}
}
