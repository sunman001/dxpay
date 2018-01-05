using System;

namespace JMP.MDL
{
    /// <summary>
    /// 支付项目接口相关全局错误日志记录表
    /// </summary>
    public class LogForApi
    {
        /// <summary>
        /// 日志ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 平台ID
        /// </summary>
        public int ClientId { get; set; }
        /// <summary>
        /// 平台名称
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 日志类别：1 错误 2 警告 3 消息
        /// </summary>
        public int TypeValue { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 摘要信息
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 详情信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 日志时间
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// 平台类型ID[1:上游支付错误,2:下游用户错误,3:上游通知异常],默认值:0
        /// </summary>
        public int PlatformId { get; set; }
        /// <summary>
        /// 关联ID[与字段PlatformId相关,如果是上游，则是通道ID;如果下游，则是应用ID]
        /// </summary>
        public int RelatedId { get; set; }
        /// <summary>
        /// 下游错误类型[1:订单号重复,2:重复发起支付,3:其他]
        /// </summary>
        public int ErrorTypeId { get; set; }
    }
}