using System;
using System.Linq;
using FluentScheduler;
using JMNOTICE.CustomEvent;
using JMNOTICE.Util;
using JMNOTICE.Util.ListBoxExt;
using JMNOTICE.Util.LogManager;

namespace JMNOTICE.ScheduleManager
{
    /// <summary>
    /// 正在添加计划的委托
    /// </summary>
    /// <param name="level">消息级别</param>
    /// <param name="message">消息内容</param>
    public delegate void ScheduleAddingHandler(Level level, string message);
    /// <summary>
    /// 所有计划创建完成后的委托
    /// </summary>
    /// <param name="sender">实体对象</param>
    /// <param name="e">事件参数</param>
    public delegate void AllScheduleAddedCompletedHandler(object sender, JobberEvent e);
    /// <summary>
    /// 创建计划出错时的委托
    /// </summary>
    /// <param name="message">错误消息</param>
    public delegate void AddingScheduleErrorHandler(string message);
    public class Jobber
    {
        public event ScheduleAddingHandler OnScheduleAdding;
        public event AllScheduleAddedCompletedHandler OnScheduleAddedCompleted;
        public event AddingScheduleErrorHandler OnAddingScheduleError;
        public Jobber()
        {
            JobManager.Initialize(new Registry());
        }

        /// <summary>
        /// 添加计划
        /// </summary>
        /// <param name="task">通知任务配置实体</param>
        public void AddSchedule(TaskCron task)
        {
            var scheduleName = "SCH_ORDER_NOTIFY_" + task.NotifyTimes;
            RemoveSchedule(scheduleName);
            try
            {
                JobManager.AddJob(() =>
                {
                    if (!GlobalConfig.AllowReadDataFromDatabase)
                    {
                        //ScheduleAdding(Level.Error, "客户端已被禁用");
                        return;
                    }
                    //ScheduleAdding(Level.Info, string.Format("正在添加任务[{0}],周期[{1}]秒,是否从队列中删除数据[{2}]", scheduleName, task.Interval, task.IsDeleteData));
                    try
                    {
                        OrderNotifyTask notify;
                        var dataLoader = new DataLoader(); //new DataLoader();
                        if (!task.IsDeleteData)
                        {
                            notify = new OrderNotifyCommonTask(dataLoader, task);
                        }
                        else
                        {
                            notify = new OrderNotifyTaskWithDelete(dataLoader, task);
                        }
                        notify.OnDoingWork += Notify_OnDoingWork;
                        notify.RunJob();
                    }
                    catch (Exception ex)
                    {
                        var message = string.Format("执行订单通知任务时出错,原因:{0}", ex);
                        AddingScheduleError(message);
                        new DbLogger().Write(message);
                    }

                    //ScheduleAdding(Level.Info, string.Format("任务[{0}]添加成功,当前任务队列总任务[{1}]个.", scheduleName, GetAllScheduleCount));
                }, t => t.WithName(scheduleName).NonReentrant().ToRunNow().AndEvery(task.Interval).Seconds());
            }
            catch (Exception ex)
            {
                AddingScheduleError(string.Format("添加任务[{0}]时失败,原因:{1}", scheduleName, ex));
                //ScheduleAdding(Level.Error, string.Format("添加任务[{0}]时失败,原因:{1}", scheduleName, ex));
            }
        }
        /// <summary>
        /// 删除计划
        /// </summary>
        /// <param name="scheduleName">计划名称</param>
        public void RemoveSchedule(string scheduleName)
        {
            JobManager.RemoveJob(scheduleName);
        }

        public void RemoveAllSchedules()
        {
            var sches = JobManager.AllSchedules;
            foreach (var sch in sches)
            {
                RemoveSchedule(sch.Name);
            }
        }

        public int GetAllScheduleCount
        {
            get { return JobManager.AllSchedules.Count(); }
        }

        public void AddSchedules(TaskCronConfig tasks)
        {
            foreach (var task in tasks.TaskCrons)
            {
                AddSchedule(task);
            }
            ScheduleAddedCompleted(new JobberEvent
            {
                ScheduleCount = GetAllScheduleCount
            });
        }

        private void Notify_OnDoingWork(Level level, string message)
        {
            try
            {
                ScheduleAdding(level, message);
            }
            catch { }
        }

        protected virtual void ScheduleAdding(Level level, string message)
        {
            try
            {
                if (OnScheduleAdding != null)
                {
                    OnScheduleAdding.Invoke(level, message);
                }
            }
            catch
            {
            }
        }

        protected virtual void ScheduleAddedCompleted(JobberEvent e)
        {
            try
            {
                if (OnScheduleAddedCompleted != null)
                {
                    OnScheduleAddedCompleted.Invoke(this, e);
                }
            }
            catch
            {
            }
        }

        protected virtual void AddingScheduleError(string message)
        {
            try { 
            if (OnAddingScheduleError != null)
            {
                OnAddingScheduleError.Invoke(message);
            }
            }
            catch
            {
            }
        }
    }
}
