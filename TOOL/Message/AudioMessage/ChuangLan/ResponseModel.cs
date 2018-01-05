using System.Net;

namespace TOOL.Message.AudioMessage.ChuangLan
{
    /// <summary>
    /// 请求响应实体类
    /// </summary>
    public class ResponseModel
    {
        public ResponseModel()
        {
            Success = false;
        }
        /// <summary>
        /// 响应内容
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 返回状态
        /// </summary>
        public HttpStatusCode Status { get; set; }
        /// <summary>
        /// 访问是否成功
        /// </summary>
        public bool Success { get; set; }
    }
}
