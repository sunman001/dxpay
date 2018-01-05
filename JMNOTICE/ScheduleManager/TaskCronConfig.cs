using System;
using System.Collections.Generic;

namespace JMNOTICE.ScheduleManager
{
    public delegate void ConvertErrorHandler(string message);
    /// <summary>
    /// 订单通知配置集合实体类
    /// </summary>
    public class TaskCronConfig
    {
        public event ConvertErrorHandler OnConvertError;
        private readonly string _taskCronConfigString;
        public TaskCronConfig(string taskCronConfigString)
        {
            _taskCronConfigString = taskCronConfigString;
        }
        /// <summary>
        /// 订单通知配置集合
        /// </summary>
        public List<TaskCron> TaskCrons { get; set; }

        /// <summary>
        /// 返回当前配置的总任务数
        /// </summary>
        public int TotalTask {
            get { return TaskCrons.Count; }
        }

        public void ConvertTaskCron()
        {
            if (string.IsNullOrEmpty(_taskCronConfigString))
            {
                return;
            }
            TaskCrons = new List<TaskCron>();

            //通知次数.间隔时间(秒).是否需要删除通知数据;通知次数.间隔时间(秒).是否需要删除通知数据
            /* 0.1.0;1.5.0;2.10.0;3.30.0;4.60.0;5.300.0;6.600.0;7.1800.0;8.3600.1;10.1.1;11.1.1;12.1.1
            */
            var configTaskList = _taskCronConfigString.Split(new[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var task in configTaskList)
            {
                if (string.IsNullOrEmpty(task))
                {
                    ConvertError("任务配置选项为空!!!");
                    continue;
                }
                var config = task.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                if (config.Length != 3)
                {
                    ConvertError(string.Format("解析任务[{0}]配置选项格式错误,正确格式形如:[通知次数.间隔时间(秒).是否需要删除通知数据],多个任务以分号(;)或逗号(,)隔开",task));
                    continue;
                }
                var cron = new TaskCron
                {
                    NotifyTimes = Convert.ToInt32(config[0]),
                    Interval = Convert.ToInt32(config[1]),
                    IsDeleteData = config[2].Trim() == "1"
                };
                TaskCrons.Add(cron);
            }

        }

        protected virtual void ConvertError(string message)
        {
            if (OnConvertError != null)
            {
                OnConvertError.Invoke(message);
            }
        }
    }
}
