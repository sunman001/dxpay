using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JmPayParameter
{
    /// <summary>
    /// 返回实体信息
    /// </summary>
    public class InnerResponse
    {
        public InnerResponse()
        {
            ErrorCode = 101;
            Message = "请求失败";
            Success = false;
        }
        public string Message { get; set; }
        public int ErrorCode { get; set; }
        public bool Success { get; set; }
        /// <summary>
        /// 附加信息
        /// </summary>
        public object ExtraData { get; set; }
        /// <summary>
        /// 是否为跳转
        /// </summary>
        public bool IsJump { get; set; }
    }
    /// <summary>
    /// 设置返回参数
    /// </summary>
    public static class ResponseExtension
    {
        /// <summary>
        /// 支付接口接受返回参数
        /// </summary>
        /// <param name="response"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static InnerResponse ToResponse(this InnerResponse response, ErrorCode errorCode)
        {
            response.ErrorCode = errorCode.GetValue();
            response.Message = errorCode.GetDescription();
            response.Success = false;
            if (errorCode == ErrorCode.Code100)
            {
                response.Success = true;
            }
            return response;
        }
        /// <summary>
        /// 初始化接口返回参数方法
        /// </summary>
        /// <param name="response">接受参数实体</param>
        /// <param name="errorCode">参数接口枚举</param>
        /// <returns></returns>
        public static InnerResponse InfoToResponse(this InnerResponse response, InfoErrorCode errorCode)
        {
            response.ErrorCode = errorCode.InfoGetValue();
            response.Message = errorCode.InfoGetDescription();
            response.Success = false;
            if (errorCode == InfoErrorCode.Code100)
            {
                response.Success = true;
            }
            return response;
        }
    
    }
}
