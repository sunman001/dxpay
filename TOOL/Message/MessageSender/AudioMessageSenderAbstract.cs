using TOOL.Message.AudioMessage.ChuangLan;

namespace TOOL.Message.MessageSender
{
    /// <summary>
    /// 语音消息发送器抽象类
    /// </summary>
    public abstract class AudioMessageSenderAbstract
    {
        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="teltemp">语音模版ID</param>
        /// <param name="receivers">消息接收对象,多个以逗号分隔</param>
        /// <param name="message">语音消息内容</param>
        /// <param name="contextparm">变量内容</param>
        /// <returns></returns>
        public abstract ResponseModel SendMessage(long teltemp, string receivers, string message,string contextparm);
    }
}
