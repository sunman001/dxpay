namespace TOOL.Message.AudioMessage.ChuangLan
{
    public class ChuangLanAudioJson
    {
        public ChuangLanAudioJson()
        {
            userinfo=new RequestPayload();
        }
        public RequestPayload userinfo { get; set; }   
    }
    public class RequestPayload
    {
        /// <summary>
        /// 帐号名
        /// </summary>
        public string company { get; set; }
        /// <summary>
        /// 请联系商务，得到该模板id
        /// </summary>
        public long teltemp { get; set; }
        /// <summary>
        /// 若模板中未使用变量，则不传该参数。多个内容直接用英文状态下逗号”,”隔开.例如：num:1002,time:半小时
        /// </summary>
        public string contextparm { get; set; }
        /// <summary>
        /// 显示号码，请联系商务
        /// </summary>
        public string telno { get; set; }
        /// <summary>
        /// 接收语音消息的号码,多个以逗号分隔
        /// </summary>
        public string callingline { get; set; }
        /// <summary>
        /// 当前时间戳或随机数，用于加密
        /// </summary>
        public string keytime { get; set; }
        /// <summary>
        /// 加密方式为MD5({key}+{passwd}+{keytime})，key为用户密钥，passwd为扣费账号密码
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// 1 男声 2 女声 3 自定义（请联系商务）
        /// </summary>
        public int sex { get; set; }
    }
}
