using System;
using TOOL.Message;
using TOOL.Message.MessageSender;
using TOOL.Message.TextMessage.ChuangLan;

namespace JmPay.PayChannelMonitor.MessageSenderCentre
{
    /// <summary>
    /// 应用监控[XX分钟支付成功率]消息发送
    /// </summary>
    public class AppPaySuccessRatioMessageSender : TextMessageSenderAbstract
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="receivers">消息接收者</param>
        /// <param name="message">消息内容</param>
        /// <returns></returns>
        public override bool SendMessage(string receivers, string message)
        {

            var request = new ChuangLanRequest
            {
                Mobile = receivers,
                Content = message
            };
            try
            {
                IMessageSender messageSender = new ChuangLanMessageSender(request);
                messageSender.Send();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
    }
}
