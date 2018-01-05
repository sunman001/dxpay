using System;

namespace DxPay.Domain
{
    /// <summary>
    /// 开发者每日结算详情表
    /// </summary>
    public class CoSettlementDeveloperOverview
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 开发者ID
        /// </summary>
        public int DeveloperId { get; set; }
        /// <summary>
        /// 开发者姓名
        /// </summary>
        public string DeveloperName { get; set; }
        /// <summary>
        /// 账单日期
        /// </summary>
        public DateTime SettlementDay { get; set; }
        /// <summary>
        /// 生成日期
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// 总流水金额
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 服务费
        /// </summary>
        public decimal ServiceFee { get; set; }
        /// <summary>
        /// 服务费费率
        /// </summary>
        public decimal ServiceFeeRatio { get; set; }
        /// <summary>
        /// 商务提成
        /// </summary>
        public decimal BpPushMoney { get; set; }
        /// <summary>
        /// 商务提成比例
        /// </summary>
        public decimal BpPushMoneyRatio { get; set; }
        /// <summary>
        /// 代理商提成金额
        /// </summary>
        public decimal AgentPushMoney { get; set; }
        /// <summary>
        /// 代理商提成比例
        /// </summary>
        public decimal AgentPushMoneyRatio { get; set; }
        /// <summary>
        /// 接口费
        /// </summary>
        public decimal PortFee { get; set; }
        /// <summary>
        /// 成本费
        /// </summary>
        public decimal CostFee { get; set; }

    }
}