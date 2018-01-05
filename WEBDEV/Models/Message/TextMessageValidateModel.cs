namespace WEBDEV.Models.Message
{
    public class TextMessageValidateModel
    {
        public TextMessageValidateModel()
        {
            Message = "";
        }
        /// <summary>
        /// 是否允许发送
        /// </summary>
        public bool AllowSend { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
    }
}