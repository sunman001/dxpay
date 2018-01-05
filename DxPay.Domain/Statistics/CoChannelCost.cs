using System;

namespace DxPay.Domain.Statistics
{
    /// <summary>
    /// 按通道分组的成本统计
    /// </summary>
    public class CoChannelCost
    {
        public string SettlementDay { get; set; }
        public int DeveloperId { get; set; }
        public string DeveloperName { get; set; }
        public int ChannelId { get; set; }
        public string ChannelName { get; set; }
        public int OrderCount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal CostFee { get; set; }
        public decimal CostRatio { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
