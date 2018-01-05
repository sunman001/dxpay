using System;
using System.Collections.Generic;
using System.Linq;
using FluentScheduler;
using JmPay.PayChannelMonitor.CustomEvent;
using JmPay.PayChannelMonitor.Util;
using JmPay.PayChannelMonitor.Util.LogManager;
using TOOL;
using TOOL.EnumUtil;

namespace JmPay.PayChannelMonitor.Scheduler
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
        /// <param name="scheduleName"></param>
        /// <param name="intervalValue"></param>
        /// <param name="intervalUnit"></param>
        /// <param name="action"></param>
        public void AddSchedule(string scheduleName,int intervalValue,IntervalUnit intervalUnit,Action action)
        {
            if (intervalValue < 1)
            {
                intervalValue = 1;
            }
            if (!GlobalConfig.RUNNIGN_SCHEDULE_GROUP_LIST.Contains(scheduleName))
            {
                ScheduleAdding(Level.Warning, string.Format("任务[{0}]已被暂停", scheduleName));
                return;
            }
            RemoveSchedule(scheduleName);
            try
            {
                JobManager.AddJob(() =>
                {
                    try
                    {
                        action();
                    }
                    catch (Exception ex)
                    {
                        var message = string.Format("执行任务[{0}]时出错,原因:{1}", scheduleName,ex);
                        AddingScheduleError(message);
                        new DbLogger().Write(message);
                    }
                }, t =>
                {
                    var schedule= t.WithName(scheduleName).NonReentrant().ToRunNow();
                    switch (intervalUnit)
                    {
                        case IntervalUnit.Second:
                            schedule.AndEvery(intervalValue).Seconds();
                            break;
                        case IntervalUnit.Minute:
                            schedule.AndEvery(intervalValue).Minutes();
                            break;
                        case IntervalUnit.Hour:
                            schedule.AndEvery(intervalValue).Hours();
                            break;
                        case IntervalUnit.Day:
                            schedule.AndEvery(intervalValue).Days();
                            break;
                        case IntervalUnit.Month:
                            schedule.AndEvery(intervalValue).Months();
                            break;
                        case IntervalUnit.Year:
                            schedule.AndEvery(intervalValue).Years();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("IntervalUnit","该任务没有匹配的任务执行周期单位");
                    }
                });
                ScheduleAddedCompleted(new JobberEvent
                {
                    ScheduleCount = GetAllScheduleCount
                });
            }
            catch (Exception ex)
            {
                AddingScheduleError(string.Format("添加任务[{0}]时失败,原因:{1}", scheduleName, ex));
            }
        }
        /// <summary>
        /// 删除计划
        /// </summary>
        /// <param name="scheduleName">计划名称</param>
        public void RemoveSchedule(string scheduleName)
        {
            JobManager.RemoveJob(scheduleName);
            ScheduleAddedCompleted(new JobberEvent
            {
                ScheduleCount = GetAllScheduleCount
            });
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

        public void AddSchedules(List<JMP.MDL.jmp_notificaiton_group> groups)
        {
            foreach (var group in groups)
            {
                if (group.IsDeleted)
                {
                    ScheduleAdding(Level.Warning, string.Format("任务[{0}]已被删除",group.Name));
                    continue;
                }
                if (group.IsDeleted)
                {
                    ScheduleAdding(Level.Warning, string.Format("任务[{0}]不可用", group.Name));
                    continue;
                }
                var intervalUnit = (IntervalUnit) Enum.Parse(typeof(IntervalUnit), group.IntervalUnit.FirstCharToUpper());
                if (!GlobalConfig.ScheduleDict.ContainsKey(group.Code))
                {
                    ScheduleAdding(Level.Warning, string.Format("任务[{0}]没有在数据库中注册", group.Name));
                    continue;
                }
                AddSchedule(group.Code,group.IntervalValue,intervalUnit,() =>
                {
                    var action=GlobalConfig.ScheduleDict[group.Code];
                    if (action != null)
                    {
                        action.Invoke();
                    }
                });
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
            try
            {
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
