using System;
using System.Collections.Generic;
using JmPay.PayChannelMonitor.Scheduler;

namespace JmPay.PayChannelMonitor.Util
{
    public class GlobalConfig
    {
        private static List<JMP.MDL.jmp_notificaiton_group> _schedulerGroup;

        /// <summary>
        /// 应用支付成功率异常监控周期(分钟)
        /// </summary>
        public static int INTERVAL_APP_PAY_SUCCESS_ABNORMAL = 5;
        /// <summary>
        /// 订单支付异常监控周期(分钟)
        /// </summary>
        public static int INTERVAL_ORDER_PAY_ABNORMAL = 5;
        /// <summary>
        /// 无订单异常监控周期(秒)
        /// </summary>
        public static int INTERVAL_NO_ORDER_APP = 10;
        /// <summary>
        /// 无订单监控任务短信发送频率(分钟)
        /// </summary>
        public static int TIMESPAN_NO_ORDER_APP_SEND_MESSAGE = 5;
        /// <summary>
        /// 正在运行的任务分组集合
        /// </summary>
        public static List<string> RUNNIGN_SCHEDULE_GROUP_LIST = new List<string>();

        /// <summary>
        /// 定时任务分组集合
        /// </summary>
        public static List<JMP.MDL.jmp_notificaiton_group> SchedulerGroup
        {
            get { return _schedulerGroup; }
        }

        /// <summary>
        /// 任务字典
        /// </summary>
        private static Dictionary<string, Action> _scheduleDict;
        /// <summary>
        /// 任务字典
        /// </summary>
        public static Dictionary<string, Action> ScheduleDict
        {
            get { return _scheduleDict; }
            set { _scheduleDict = value; }
        }

        /// <summary>
        /// 初始化全局变量
        /// </summary>
        public static void InitGlobalConfig(ScheduleDictionary scheduleDictionary)
        {
            _scheduleDict = scheduleDictionary.ScheduleDict;
            LoadSchedulerGroup();
        }
        /// <summary>
        /// 从数据库加载定时任务分组集合
        /// </summary>
        private static void LoadSchedulerGroup()
        {
            _schedulerGroup = new List<JMP.MDL.jmp_notificaiton_group>();
            _schedulerGroup = new JMP.BLL.jmp_notificaiton_group().GetModelList("IsDeleted=0");
        }

    }
}
