using JMNOTICE.Models;
using JMNOTICE.Util;
using JMNOTICE.Util.ListBoxExt;
using JMNOTICE.Util.LogManager;
using JMP.DBA;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace JMNOTICE.ScheduleManager
{
    /// <summary>
    /// 订单通知过程中的委托
    /// </summary>
    /// <param name="level"></param>
    /// <param name="message"></param>
    public delegate void DoingWorkHandler(Level level, string message);

    /// <summary>
    /// 订单通知抽象类
    /// </summary>
    public abstract class OrderNotifyTask
    {
        /// <summary>
        /// 并发锁
        /// </summary>
        private readonly object _lock = new object();
        /// <summary>
        /// 订单通知过程中的事件
        /// </summary>
        public event DoingWorkHandler OnDoingWork;
        /// <summary>
        /// 需要通知的订单队列
        /// </summary>
        private Queue<OrderNotifyQueueListModel> _queueNotifies;
        /// <summary>
        /// 订单通知数据加载器接口
        /// </summary>
        private readonly IDataLoader<OrderNotifyQueueListModel> _dataLoader;
        /// <summary>
        /// 当前通知任务的配置选项,包含了通知次数,任务间隔周期,任务完成后是否从通知队列表中删除数据三个属性
        /// </summary>
        private readonly TaskCron _taskCron;

        public int NotifyCount
        {
            get { return _queueNotifies.Count; }
        }

        /// <summary>
        /// 通知次数
        /// </summary>
        public int NotifyTimes
        {
            get { return _taskCron.NotifyTimes; }
        }

        protected OrderNotifyTask(IDataLoader<OrderNotifyQueueListModel> dataLoader, TaskCron taskCron)
        {
            _taskCron = taskCron;
            if (_taskCron == null)
            {
                throw new ArgumentNullException("Task Cron", "没有通知任务的配置选项");
            }
            _queueNotifies = new Queue<OrderNotifyQueueListModel>();
            _dataLoader = dataLoader;
        }
        /// <summary>
        /// 启动作业
        /// </summary>
        public void RunJob()
        {
            try
            {
                try
                {
                    GlobalConfig.TaskExecuteCount++;
                }
                catch
                {
                }
                LoadData();
                if (NotifyCount <= 0)
                {
                    //DoingWork(Level.Info, string.Format("没有需要通知的订单,当前任务[第{0}次通知],返回...", NotifyTimes));
                    return;
                }
                if (IsLoopDoWork)
                {
                    DoWorkLoop();
                }
                else
                {
                    DoWorkOnce();
                }
            }
            catch (Exception ex)
            {
                DoingWork(Level.Error, string.Format("当前任务[第{0}次通知],运行订单通知作业时出错,原因:{1}", NotifyTimes, ex));
            }
        }

        private async void DoWorkOnce()
        {
            var maxThreadCount = GlobalConfig.MAX_THREAD_COUNT;
            if (NotifyCount < GlobalConfig.MAX_THREAD_COUNT)
            {
                maxThreadCount = NotifyCount;
            }
            try
            {
                await Task.Run(() =>
                {
                    Parallel.For(0, NotifyCount, new ParallelOptions { MaxDegreeOfParallelism = maxThreadCount }, index =>
                        {
                            try
                            {
                                //DoingWork(Level.Info, string.Format("正在执行订单通知任务,线程[{0}]...", index));
                                DoWork(index);
                            }
                            catch (Exception ex)
                            {
                                DoingWork(Level.Error, string.Format("执行订单通知任务线程出错,原因:{0}", ex));
                            }
                        });

                }).ContinueWith(t =>
                {
                    DoingWork(Level.Info, string.Format("订单通知任务(第{0}次通知)已完成.", NotifyTimes));
                });
            }
            catch (Exception ex)
            {
                var message = string.Format("执行订单通知任务时异常,原因:{0}", ex);
                DoingWork(Level.Error, message);
            }
        }

        private async void DoWorkLoop()
        {
            var maxThreadCount = GlobalConfig.MAX_THREAD_COUNT;
            if (NotifyCount < GlobalConfig.MAX_THREAD_COUNT)
            {
                maxThreadCount = NotifyCount;
            }
            try
            {
                do
                {
                    await Task.Run(() =>
                    {
                        Parallel.For(0, NotifyCount, new ParallelOptions { MaxDegreeOfParallelism = maxThreadCount }, index =>
                         {
                             try
                             {
                                 //DoingWork(Level.Info, string.Format("正在执行订单通知任务,线程[{0}]...", index));
                                 DoWork(index);
                             }
                             catch (Exception ex)
                             {
                                 DoingWork(Level.Error, string.Format("执行订单通知任务线程出错,原因:{0}", ex));
                             }
                         });

                    }).ContinueWith(t =>
                    {
                        LoadData();
                    });
                } while (NotifyCount > 0);
                DoingWork(Level.Info, string.Format("订单通知任务(第{0}次通知)已完成.", NotifyTimes));
            }
            catch (Exception ex)
            {
                DoingWork(Level.Error, string.Format("执行订单通知任务时异常,原因:{0}", ex));
            }
        }

        /// <summary>
        /// 根据定时任务的间隔周期判断本次任务是否循环加载数据并完成通知操作
        /// </summary>
        protected bool IsLoopDoWork
        {
            get { return _taskCron.Interval > GlobalConfig.LOOP_LOAD_DATA_THRESHOLD; }
        }

        /// <summary>
        /// 加载指定次数的需要通知的订单集合
        /// </summary>
        private void LoadData()
        {
            //DoingWork(Level.Info, string.Format("正在加载订单通知次数为[{0}]次的数据...", NotifyTimes));
            _queueNotifies = new Queue<OrderNotifyQueueListModel>();
            lock (_lock)
            {
                _queueNotifies = _dataLoader.LoadSpecifiedTimesData(NotifyTimes, GlobalConfig.EACH_TIME_SELECT_TOP_COUNT);
                //DoingWork(Level.Info, string.Format("订单ID:[{0}]", string.Join(",", _queueNotifies.Select(x => x.q_id))));
            }
            //DoingWork(Level.Info, string.Format("订单通知次数为[{0}]次的数据加载完成,共[{1}]条.", NotifyTimes, NotifyCount));
        }

        /// <summary>
        /// 单个订单通知任务
        /// </summary>
        private void DoWork(int index)
        {
            //DoingWork(Level.Info, string.Format("订单ID:[{0}]", string.Join(",", _queueNotifies.Select(x => x.q_id))));
            while (NotifyCount > 0)
            {
                OrderNotifyQueueListModel notify;
                lock (_lock)
                {
                    notify = _queueNotifies.Dequeue();
                }
                if (notify == null) continue;
                GlobalPara.Request();
                //DoingWork(Level.Info, string.Format("正在执行订单通知[{0}]的通知任务...", notify.q_id));
                DoingWork(Level.Info, string.Format("正在执行订单通知任务,线程[{0}],交易码[{1}]...", index, notify.trade_code));
                var success = SendNotify(notify);

                if (Success(success))
                {
                    GlobalPara.RequestSuccess();
                }

                AfterSendNotify(notify, success);
                DoingWork(Level.Info, string.Format("订单通知[{0}]的任务已执行完成,结果为:[{1}]", notify.q_id, success));
            }
        }

        private bool Success(string success)
        {
            return success.Trim().ToLower() == "success";
        }

        private string SendNotify(OrderNotifyQueueListModel notify)
        {
            var mark = notify.q_address.Contains("?") ? "&" : "?";
            var url = notify.q_address + mark + "trade_md5=" +
                      JMP.TOOL.MD5.md5strGet(notify.trade_no + notify.trade_code + notify.trade_price + notify.q_sign,
                          true) + "&trade_type=" + notify.trade_type +
                      "&trade_price=" + notify.trade_price + "&trade_paycode=" + notify.trade_paycode + "&trade_code=" +
                      notify.trade_code + "&trade_no=" + notify.trade_no + "&trade_privateinfo=" +
                      System.Web.HttpUtility.UrlEncode(notify.q_privateinfo, System.Text.Encoding.UTF8) + "&trade_sign=" +
                      JMP.TOOL.DESEncrypt.Encrypt(DateTime.Now.ToString(CultureInfo.InvariantCulture) + "," +
                                                    notify.trade_no + "," + notify.q_sign, "hyx") +
                      "&trade_status=TRADE_SUCCESS&trade_time=" + DateTime.Parse(notify.trade_time.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            var qUersid = notify.q_uersid;
            var success = HttpHelper.OpenReadWithHttps(url, qUersid);

            //message = "正在通知：" + q_id + "," + q_address + "     " + success;
            //sl++;
            return success;
        }

        /// <summary>
        /// 订单通知后的操作
        /// </summary>
        /// <param name="notify">订单通知对象</param>
        /// <param name="success">通知是否成功</param>
        public virtual void AfterSendNotify(OrderNotifyQueueListModel notify, string success)
        {
            var notifySuccess = success == "success" ? 1 : -1;
            var updateDataSuccess = DbHelperSQL.ExecuteSql("update jmp_order set o_noticestate=" + notifySuccess + ",o_noticetimes=GETDATE(),o_times=" + (notify.q_times + 1) + " where o_id=" + notify.q_o_id) > 0;
            //如果下游返回异步通知成功
            if (Success(success))
            {
                //如果更新数据通知状态失败
                if (!updateDataSuccess)
                {
                    //更新队列表
                    DbHelperSQL.ExecuteSql("update " + notify.q_tablename + " set o_noticestate=1,o_noticetimes=GETDATE(),o_times=" +
                                           (notify.q_times + 1) + " where o_code='" + notify.trade_code + "'");
                }
                if (notifySuccess == 1)
                {
                    DbHelperSQL.ExecuteSql("delete from jmp_queuelist where q_id=" + notify.q_id);
                }
                else
                {
                    //JMP.TOOL.AddLocLog.AddUserLog(0, 5, "", string.Format("异常移除队列数据,订单号:{0}",notify.trade_code), "通知程序异常");//写入报错日志
                    JMP.TOOL.AddLocLog.AddUserLog(0, 5, "", string.Format("异常移除队列数据,订单号:{0},通知次数:{1},通知结果:{2},归档表:{3}", notify.trade_code, notify.q_times + 1, success, notify.q_tablename), "通知程序异常");//写入报错日志
                }
            }
            else
            {
                DbHelperSQL.ExecuteSql("update jmp_queuelist set q_noticestate=0,q_noticetimes=GETDATE() where q_id=" + notify.q_id);
                //更新队列表
                if (!updateDataSuccess)
                {
                    DbHelperSQL.ExecuteSql("update " + notify.q_tablename + " set o_noticestate=" + notifySuccess + ",o_noticetimes=GETDATE(),o_times=" + (notify.q_times + 1) + " where o_code='" + notify.trade_code + "'");
                }
            }
        }

        protected virtual void DoingWork(Level level, string message)
        {
            try
            {
                if (OnDoingWork != null)
                {
                    if (level == Level.Error)
                    {
                        new DbLogger().Write(message);
                    }
                    OnDoingWork.Invoke(level, string.Format("[{0}]:{1}", NotifyTimes, message));
                }
            }
            catch
            {
            }
        }
    }
}
