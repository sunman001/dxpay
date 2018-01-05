namespace TOOL.Message.TextMessage
{
    /// <summary>
    /// 短信发送基类
    /// </summary>
    public abstract class TextMessageSender : IMessageSender
    {
        /// <summary>
        /// 发送短信的抽象方法
        /// </summary>
        /// <returns></returns>
        public abstract bool Send();
    }
}
