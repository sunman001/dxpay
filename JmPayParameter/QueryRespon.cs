using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JmPayParameter
{
    /// <summary>
    /// 查询接口返回实体
    /// </summary>
    public class QueryRespon
    {
        public QueryRespon()
        {
            ErrorCode = 101;
            Message = "请求失败";
            Success = false;
        }
        /// <summary>
        /// 消息提示
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 参数编码
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// 成功或失败
        /// </summary>
        public bool Success { get; set; }

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
    /// 设置返回参数
    /// </summary>
    public static class     QueryResponseExtension
    {
        /// <summary>
        /// 查询接口返回参数方法
        /// </summary>
        /// <param name="query">接受参数实体</param>
        /// <param name="errorCode">参数接口枚举</param>
        /// <returns></returns>
        public static QueryRespon QueryResp(this QueryRespon query, QueryErrorCode error)
        {
            query.ErrorCode = error.QueryGetValue();
            query.Message = error.QueryGetDescription();
            if (error == QueryErrorCode.Code100)
            {
                query.Success = true;
            }
            return query;
        }
    }
}
