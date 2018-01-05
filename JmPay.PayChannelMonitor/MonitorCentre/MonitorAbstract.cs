using System;
using System.Collections.Generic;
using System.Linq;
using JmPay.PayChannelMonitor.DataLoader;
using JmPay.PayChannelMonitor.Util;
using JMP.MDL;
using TOOL.EnumUtil;
using TOOL.Message.MessageSender;

namespace JmPay.PayChannelMonitor.MonitorCentre
{
    /// <summary>
    /// 监控抽象类
    /// </summary>
    public abstract class MonitorAbstract<T>
    {
        /// <summary>
        /// 事件
        /// </summary>
        public event Action<string, Level?> OnDoingWork;

        /// <summary>
        /// 可用的消息接收者
        /// </summary>
        protected List<T> AllowSenders;

        /// <summary>
        /// 监控器名称
        /// </summary>
        protected abstract string MonitroName { get; }

        /// <summary>
        /// 消息接收者,多个以逗号分隔
        /// </summary>
        protected string _receivers;

        /// <summary>
        /// 语音模版ID
        /// </summary>
        protected long TelTemp { get; set; }

        /// <summary>
        /// 变量内容
        /// </summary>
        public abstract string ContextParm { get; set; }


        protected virtual void DoingWork(string message, Level? level = null)
        {
            if (OnDoingWork != null)
            {
                OnDoingWork.Invoke(string.Format("任务[{0}]{1}",MonitroName, message), level);
            }
        }

        /// <summary>
        /// 计划分组实体对象
        /// </summary>
        protected jmp_notificaiton_group ScheduleGroup;

        protected MonitorAbstract(ScheduleGroupCode groupCode)
        {
            ScheduleGroup = new JMP.BLL.jmp_notificaiton_group().GetModelList("IsDeleted=0 AND IsEnabled=1 AND Code='" + groupCode + "'").FirstOrDefault();
            if (ScheduleGroup == null)
            {
                throw new NullReferenceException("计划分组未创建或已被禁用");
            }
            TelTemp = ScheduleGroup.AudioTelTempId;
            AllowSendTextMessage = ScheduleGroup.SendMode.CheckAllowSendTextMessage();
            AllowSendAudioMessage = ScheduleGroup.SendMode.CheckAllowSendAudioMessage();
        }
        /// <summary>
        /// 是否允许继续执行
        /// </summary>
        protected bool AllowContinue = true;

        /// <summary>
        /// 是否可以发送短信消息
        /// </summary>
        protected bool AllowSendTextMessage;
        /// <summary>
        /// 是否可以发送语音消息
        /// </summary>
        protected bool AllowSendAudioMessage;

        /// <summary>
        /// 开始任务
        /// </summary>
        protected virtual void Start()
        {
            AllowContinue = true;
            DoingWork("开始执行...");
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        protected abstract void LoadData(IDataLoader dataLoader);
        /// <summary>
        /// 处理数据
        /// </summary>
        protected abstract void Process();
        /// <summary>
        /// 持久化
        /// </summary>
        protected abstract void Persistent();
        /// <summary>
        /// 过滤消息接收对象
        /// </summary>
        protected abstract void Filter();

        protected virtual void BuildMessageContent()
        {

        }

        /// <summary>
        /// 检查是否可以继续执行
        /// </summary>
        private void CheckAllowContinue()
        {
            if (AllowSenders == null || AllowSenders.Count <= 0)
            {
                AllowContinue = false;
                DoingWork("已中止,没有可用的接收对象", Level.Error);
                return;
            }
            if (ScheduleGroup == null)
            {
                AllowContinue = false;
                DoingWork("已中止,任务分组为空", Level.Error);
                return;
            }
            if (!ScheduleGroup.IsAllowSendMessage)
            {
                AllowContinue = false;
                DoingWork("已中止,短信发送已关闭", Level.Error);
            }
        }

        /// <summary>
        /// 文本消息发送器
        /// </summary>
        protected abstract void SendTextMessage();

        /// <summary>
        /// 文本消息发送器属性
        /// </summary>
        public TextMessageSenderAbstract TextMessageSender { get; set; }

        /// <summary>
        /// 语音消息发送器
        /// </summary>
        protected abstract void SendAudioMessage();

        /// <summary>
        /// 语音消息发送器属性
        /// </summary>
        public AudioMessageSenderAbstract AudioMessageSender { get; set; }

        /// <summary>
        /// 完成任务
        /// </summary>
        protected virtual void Completed()
        {
            DoingWork("执行完成.");
        }

        /// <summary>
        /// 执行监控任务
        /// </summary>
        /// <param name="dataLoader">数据加载器</param>
        public void DoMonitorTask(IDataLoader dataLoader)
        {
            Start();
            LoadData(dataLoader);

            if (!AllowContinue)
            {
                DoingWork("已退出[准备处理数据],没有可用数据...", Level.Warning);
                return;
            }

            Process();

            if (!AllowContinue)
            {
                DoingWork("已退出[准备过滤数据],没有可用数据...", Level.Warning);
                return;
            }

            Filter();

            if (!AllowContinue)
            {
                DoingWork("已退出[准备持久化数据],没有可用数据...", Level.Warning);
                return;
            }

            CheckAllowContinue();

            Persistent();

            if (!AllowContinue)
            {
                DoingWork("已退出[准备发送消息],没有可用数据...", Level.Warning);
                return;
            }
            if (AllowSendTextMessage)
            {
                DoingWork(string.Format("正在发送短信消息,接收对象[{0}]...", _receivers), Level.Info);
                try
                {
                    SendTextMessage();
                }
                catch (Exception ex)
                {
                    DoingWork(string.Format("发送短信消息时出错,原因:{0}...", ex), Level.Error);
                }

            }

            if (AllowSendAudioMessage && AllowContinue)
            {
                DoingWork(string.Format("正在发送语音消息,接收对象[{0}]...", _receivers), Level.Info);
                try
                {
                    SendAudioMessage();
                }
                catch (Exception ex)
                {
                    DoingWork(string.Format("发送语音消息时出错,原因:{0}...", ex), Level.Error);
                }
            }

            Completed();
        }
    }
}
