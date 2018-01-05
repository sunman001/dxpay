using System;

namespace DxPay.Domain.Statistics
{
    /// <summary>
    /// 按应用分组的成本统计
    /// </summary>
    public class CoAppCost
    {
        public string SettlementDay { get; set; }
        public int DeveloperId { get; set; }
        public string DeveloperName { get; set; }
        public int AppId { get; set; }
        public string AppName { get; set; }
        public int OrderCount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal CostFee { get; set; }
        public decimal CostRatio { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
