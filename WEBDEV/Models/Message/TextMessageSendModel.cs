using System;
using System.Collections.Generic;
using TOOL;

namespace WEBDEV.Models.Message
{
    public class TextMessageSendModel
    {
        public TextMessageSendModel()
        {
            WhichDay = "";
            Histories = new List<DateTime>();
        }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 发送日期,格式:yyyyMMdd
        /// </summary>
        public string WhichDay { get; private set; }
        public List<DateTime> Histories { get; set; }

        /// <summary>
        /// 获取当日的发送次数
        /// </summary>
        public int RequestCount
        {
            get { return Histories.Count; }
        }

        /// <summary>
        /// 发送了一次短信
        /// </summary>
        public void Sent()
        {
            if (WhichDay != Today)
            {
                WhichDay = Today;
                ClearHistory();
            }
            Histories.Add(DateTime.Now);
        }

        private string Today
        {
            get { return DateTime.Now.ToString("yyyyMMdd"); }
        }

        public TextMessageValidateModel AllowSend
        {
            get
            {
                var message = new TextMessageValidateModel();
                var maxRequest = 5;
                try
                {
                    maxRequest = Convert.ToInt32(ConfigReader.GetSettingValueByKey("MESSAGE.MAXREQUEST"));
                }
                catch { }
                if (Today != WhichDay || RequestCount <= maxRequest)
                {
                    message.AllowSend = true;
                    return message;
                }
                message.AllowSend = false;
                message.Message = string.Format("当前号码[{0}]已超过了每日请求次数限制", Phone);
                return message;
            }
        }
        /// <summary>
        /// 清空短信发送历史记录
        /// </summary>
        private void ClearHistory()
        {
            Histories.Clear();
        }
    }
}