using TOOL;

namespace WEB.Util
{
    public class SystemConfigReader
    {
        private static readonly JMP.BLL.jmp_system SystemBll = new JMP.BLL.jmp_system();
        /// <summary>
        /// 是否允许拨打响应转发的语音消息
        /// </summary>
        public static bool CustomerResponseAllowSendAudioMessage
        {
            get
            {
                var config= SystemBll.FindByName(SystemConfig.KeyForCustomerResponseAllowSendAudioMessage);
                return config.s_value == "true";
            }
        }
    }
}