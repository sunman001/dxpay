using System;

namespace JmPay.PayChannelMonitor.Scheduler
{
    public delegate void BeforeMessageSendHandler(string message);
    public delegate void MessageSendingHandler(string message);
    public delegate void AfterMessageSendHandler(string message);
    public class SendMessageScheduler
    {
        public event BeforeMessageSendHandler BeforeMessageSend;
        public event BeforeMessageSendHandler MessageSending;
        public event BeforeMessageSendHandler AfterMessageSend;

        protected virtual void OnBeforeMessageSend(string message)
        {
            if(BeforeMessageSend!=null)
                BeforeMessageSend.Invoke(message);
        }

        protected virtual void OnMessageSending(string message)
        {
            if (MessageSending != null)
                MessageSending.Invoke(message);
        }

        protected virtual void OnAfterMessageSend(string message)
        {
            if (AfterMessageSend != null)
                AfterMessageSend.Invoke(message);
        }

        public void Run()
        {
            OnBeforeMessageSend("开始处理订单异常的核查任务...");
            /*var orderAbnormalTask = new OrderAbnormalTask();
            OnMessageSending("正在处理订单异常的核查任务...");
            try
            {
                orderAbnormalTask.SendMessage();
            }
            catch (Exception ex)
            {
                OnMessageSending(string.Format("处理订单异常的核查任务时出错,原因:{0}...", ex.Message));
            }
            OnAfterMessageSend(string.Format("订单异常的核查任务已完成,成功处理{0}条记录.", orderAbnormalTask.SuccessCount));
            OnAfterMessageSend("订单的核查任务已完成.");*/
        }
    }
}