using System;

namespace JMP.Model
{
    /// <summary>
    /// 支付项目全局错误日志记录表
    /// </summary>
    public class DxGlobalLogError
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
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 日志类别：1 错误 2 警告
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

    }
}