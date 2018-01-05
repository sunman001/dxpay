namespace TOOL.Message.AudioMessage.ChuangLan
{
    /// <summary>
    /// 创蓝返回的响应结果类
    /// </summary>
    public class ResponseResult
    {
        /// <summary>
        /// 响应时间
        /// </summary>
        public string ResponseTime { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public string StatusCode { get; set; }
        /// <summary>
        /// 消息ID
        /// </summary>
        public string MessageId { get; set; }
    }
}