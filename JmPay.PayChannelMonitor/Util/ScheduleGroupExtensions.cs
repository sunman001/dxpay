namespace JmPay.PayChannelMonitor.Util
{
    public static class ScheduleGroupExtensions
    {
        /// <summary>
        /// 检查是否允许发送短信消息
        /// </summary>
        /// <param name="sendMode"></param>
        /// <returns></returns>
        public static bool CheckAllowSendTextMessage(this string sendMode)
        {
            if (string.IsNullOrEmpty(sendMode))
            {
                return true;
            }
            return !string.IsNullOrEmpty(sendMode) && sendMode.Contains("短信");
        }

        /// <summary>
        /// 检查是否允许发送语音消息
        /// </summary>
        /// <param name="sendMode"></param>
        /// <returns></returns>
        public static bool CheckAllowSendAudioMessage(this string sendMode)
        {
            return !string.IsNullOrEmpty(sendMode) && sendMode.Contains("语音");
        }
    }
}
