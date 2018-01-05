using System;

namespace DxPay.Domain
{
    /// <summary>
    /// [结算]按通道和应用分组的开发者成本详情统计表
    /// </summary>
    public class CoSettlementChannelCost
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 账单日期
        /// </summary>
        public DateTime SettlementDay { get; set; }
        /// <summary>
        /// 开发者ID
        /// </summary>
        public int DeveloperId { get; set; }
        /// <summary>
        /// 开发者名称
        /// </summary>
        public string DeveloperName { get; set; }
        /// <summary>
        /// 应用ID
        /// </summary>
        public int AppId { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        public string AppName { get; set; }
        /// <summary>
        /// 通道ID
        /// </summary>
        public int ChannelId { get; set; }
        /// <summary>
        /// 通道名称
        /// </summary>
        public string ChannelName { get; set; }
        /// <summary>
        /// 总订单数
        /// </summary>
        public int OrderCount { get; set; }
        /// <summary>
        /// 总流水
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 成本费率
        /// </summary>
        public decimal CostRatio { get; set; }
        /// <summary>
        /// 成本费
        /// </summary>
        public decimal CostFee { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedOn { get; set; }

    }
}