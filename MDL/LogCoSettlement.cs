using System;

namespace JMP.MDL
{
    /// <summary>
    /// 结算日志详情记录表
    /// </summary>
    public class LogCoSettlement
    {
        /// <summary>
        /// 日志ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 日志类别： -1失败 1 成功
        /// </summary>
        public int TypeId { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 简短说明
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 附加信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 日志时间
        /// </summary>
        public DateTime CreatedOn { get; set; }

    }
}