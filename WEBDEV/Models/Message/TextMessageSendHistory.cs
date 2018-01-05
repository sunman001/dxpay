using System.Collections.Generic;
using System.Linq;

namespace WEBDEV.Models.Message
{
    public class TextMessageSendHistory
    {
        public TextMessageSendHistory()
        {
            TextMessageSendModels = new List<TextMessageSendModel>();
        }
        protected List<TextMessageSendModel> TextMessageSendModels { get; set; }

        /// <summary>
        /// 记录短信发送历史
        /// </summary>
        /// <param name="phone"></param>
        public TextMessageValidateModel RecordSentMessage(string phone)
        {
            var message = new TextMessageValidateModel();
            var his = FindByPhone(phone);
            if ( his== null)
            {
                var send = new TextMessageSendModel
                {
                    Phone = phone
                };
                send.Sent();
                TextMessageSendModels.Add(send);
                message.AllowSend = true;
            }
            else
            {
                message = his.AllowSend;
                if (message.AllowSend)
                {
                    his.Sent();
                }                
            }
            if (TextMessageSendModels.Count > 50000)
            {
                //如果超过50000个手机,则清空历史记录
                TextMessageSendModels.Clear();
            }
            return message;
        }

        private bool PhoneExists(string phone)
        {
            return TextMessageSendModels.Exists(x => x.Phone == phone);
        }

        private TextMessageSendModel FindByPhone(string phone)
        {
            return TextMessageSendModels.FirstOrDefault(x=>x.Phone==phone);
        }
    }
}