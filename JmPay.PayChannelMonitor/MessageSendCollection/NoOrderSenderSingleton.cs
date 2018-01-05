using JmPay.PayChannelMonitor.Util;

namespace JmPay.PayChannelMonitor.MessageSendCollection
{
    /// <summary>
    /// 无订单监控短信发送者收集器(单例)
    /// </summary>
    public class NoOrderSenderSingleton
    {
        private static readonly NoOrderSenderSingleton _instance = new NoOrderSenderSingleton();

        private NoOrderSenderSingleton()
        {
            
        }

        public static NoOrderSenderSingleton Instance
        {
            get { return _instance; }
        }

        private NoOrderAppMessageSendCollection _messageSendCollection = new NoOrderAppMessageSendCollection(GlobalConfig.TIMESPAN_NO_ORDER_APP_SEND_MESSAGE);

        public NoOrderAppMessageSendCollection GetCollection
        {
            get { return _messageSendCollection; }
        }
    }
}
