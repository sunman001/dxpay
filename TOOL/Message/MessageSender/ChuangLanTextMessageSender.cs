using System;
using TOOL.Message.TextMessage.ChuangLan;

namespace TOOL.Message.MessageSender
{
    /// <summary>
    /// 创蓝短信消息发送类
    /// </summary>
    public class ChuangLanTextMessageSender: TextMessageSenderAbstract
    {
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
