namespace TOOL.Message.MessageSender
{
    public abstract class TextMessageSenderAbstract 
    {
        public abstract bool SendMessage(string receivers, string message);
    }
}
