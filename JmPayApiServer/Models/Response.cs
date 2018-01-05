using JmPayParameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JmPayApiServer.Models
{
    /// <summary>
    /// 返回信息实体
    /// </summary>
    public class Response
    {
        public Response()
        {
            ErrorCode = 101;
            Message = "请求失败";
        }
        /// <summary>
        /// 提示语
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 标示码
        /// </summary>
        public int ErrorCode { get; set; }
    }
    /// <summary>
    /// 操作成功时追加支付信息
    /// </summary>
    public class SuccessResponse : Response
    {
        /// <summary>
        /// 附加信息
        /// </summary>
        public object ExtraData { get; set; }

    }
    /// <summary>
    /// 查询接口成功时需要返回的参数
    /// </summary>
    public class QuerySuccessResponse : Response
    {
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string trade_no { get; set; }
        /// <summary>
        /// 平台订单号
        /// </summary>
        public string trade_code { get; set; }
        /// <summary>
        /// 支付类型
        /// </summary>
        public int trade_type { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public string trade_price { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public string trade_time { get; set; }
        /// <summary>
        /// 支付状态
        /// </summary>
        public int o_state { get; set; }

    }

    /// <summary>
    /// 操作失败
    /// </summary>
    public class FailResponse : Response
    {

    }
    /// <summary>
    /// 获取返回码
    /// </summary>
    public static class ResponseExtension
    {
        /// <summary>
        /// 支付接口返回参数方法
        /// </summary>
        /// <param name="response">接受参数实体</param>
        /// <param name="errorCode">参数接口枚举</param>
        /// <returns></returns>
        public static Response ToResponse(this Response response, ErrorCode errorCode)
        {
            response.ErrorCode = errorCode.GetValue();
            response.Message = errorCode.GetDescription();
            return response;
        }
        /// <summary>
        /// 初始化接口返回参数方法
        /// </summary>
        /// <param name="response">接受参数实体</param>
        /// <param name="errorCode">参数接口枚举</param>
        /// <returns></returns>
        public static Response InfoToResponse(this Response response, InfoErrorCode errorCode)
        {
            response.ErrorCode = errorCode.InfoGetValue();
            response.Message = errorCode.InfoGetDescription();
            return response;
        }
        /// <summary>
        /// 查询接口返回参数方法
        /// </summary>
        /// <param name="response">接受参数实体</param>
        /// <param name="errorCode">参数接口枚举</param>
        /// <returns></returns>
        public static Response QueryToResponse(this Response response, QueryErrorCode errorCode)
        {
            response.ErrorCode = errorCode.QueryGetValue();
            response.Message = errorCode.QueryGetDescription();
            return response;
        }
    }
}