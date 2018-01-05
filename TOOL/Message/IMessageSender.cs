namespace TOOL.Message
{
    /// <summary>
    /// 消息发送接口(邮件,短信等)
    /// </summary>
    public interface IMessageSender
    {
        /// <summary>
        /// 发送消息的方法
        /// </summary>
        /// <returns></returns>
        bool Send();
    }
}
