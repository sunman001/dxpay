using JMP.Model;
using System;

namespace JMP.MDL
{
   public  class jmp_AppChannelReport
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 通道ID
        /// </summary>
        public int ChannelId { get; set; }
        /// <summary>
        /// 通道名称
        /// </summary>
        public string ChannelName { get; set; }
        /// <summary>
        /// 支付类型
        /// </summary>
        public string PayTypeName { get; set; }
        /// <summary>
        /// 应用ID
        /// </summary>
        public int AppId { get; set; }
        /// <summary>
        /// 统计金额
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 未支付量
        /// </summary>
        public decimal Notpay { get; set; }
        /// <summary>
        /// 成功量
        /// </summary>
        public decimal Success { get; set; }
        /// <summary>
        /// 成功付费率
        /// </summary>
        public decimal Successratio { get; set; }
        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// 汇总日期
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
