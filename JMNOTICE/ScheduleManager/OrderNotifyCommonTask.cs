using JMNOTICE.Models;
using JMNOTICE.Util;

namespace JMNOTICE.ScheduleManager
{
    /// <summary>
    /// 通用的订单通知类
    /// </summary>
    public class OrderNotifyCommonTask : OrderNotifyTask
    {
        public OrderNotifyCommonTask(IDataLoader<OrderNotifyQueueListModel> dataLoader, TaskCron taskCron) : base(dataLoader, taskCron)
        {
        }
    }
}
