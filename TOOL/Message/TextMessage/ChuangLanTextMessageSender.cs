using System;
using TOOL.Message.TextMessage.ChuangLan;

namespace TOOL.Message.TextMessage
{
   public  class ChuangLanTextMessageSender:TextMessageSender
    {
       public override bool Send()
       {
            //TODO:发送短信提示
            var request = new ChuangLanRequest
            {
                Mobile = ConfigReader.GetSettingValueByKey("CHUANGLAN.MOBILE.MONITOR"),
                Content = ConfigReader.GetSettingValueByKey("CHUANGLAN.CONTENT.MONITOR")
            };
            try
            {
                IMessageSender messageSender = new ChuangLanMessageSender(request);
                var success= messageSender.Send();
            }
            catch (Exception ex)
            {
                //AddLocLog.AddLog(1, 4, Request.UserHostAddress, "通道状态检测", ex.ToString());
            }
            return true;
        }
    }
}
