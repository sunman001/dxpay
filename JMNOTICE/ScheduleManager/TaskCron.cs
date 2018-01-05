namespace JMNOTICE.ScheduleManager
{
    /// <summary>
    /// 订单通知任务的配置实体类
    /// </summary>
    public class TaskCron
    {
        /// <summary>
        /// 通知次数
        /// </summary>
        public int NotifyTimes { get; set; }
        /// <summary>
        /// 间隔时间(秒)
        /// </summary>
        public int Interval { get; set; }
        /// <summary>
        /// 是否需要删除通知数据
        /// </summary>
        public bool IsDeleteData { get; set; }
    }
}
