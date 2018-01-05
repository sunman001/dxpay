using System;
using System.Configuration;
using TOOL.Message.AudioMessage.ChuangLan;

namespace TOOL.Message.MessageSender
{
    /// <summary>
    /// 创蓝语音消息发送类
    /// </summary>
    public class ChuangLanAudioMessageSender : AudioMessageSenderAbstract
    {
        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="teltemp">语音模版ID</param>
        /// <param name="receivers">消息接收对象,多个以逗号分隔</param>
        /// <param name="message">语音消息内容</param>
        /// <param name="contextparm">变量内容[若模板中未使用变量，则不传该参数。多个内容直接用英文状态下逗号”,”隔开.例如：num:1002,time:半小时]</param>
        /// <returns></returns>
        public override ResponseModel SendMessage(long teltemp, string receivers, string message, string contextparm)
        {
            var request = new RequestPayload
            {
                callingline = receivers,
                company = ConfigurationManager.AppSettings["CHUANGLAN.AUDIO.RESOURCE.company"],
                contextparm = contextparm,
                keytime = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                sex = 2,
                telno = ConfigurationManager.AppSettings["CHUANGLAN.AUDIO.RESOURCE.telno"],
                teltemp = long.Parse(teltemp.ToString())
            };
            request.key = Util.GetKeyString(ConfigurationManager.AppSettings["CHUANGLAN.AUDIO.RESOURCE.key"], ConfigurationManager.AppSettings["CHUANGLAN.AUDIO.RESOURCE.secret"], request.keytime);
            try
            {
                var messageSender = new AudioMessage.ChuangLan.ChuangLanAudioMessageSender(request);
                messageSender.Send();
                return messageSender.Response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
