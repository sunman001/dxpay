using System;

namespace DxPay.Domain
{
    /// <summary>
    /// [结算]按应用分组的开发者每日结算详情数据表
    /// </summary>
    public class CoSettlementDeveloperAppDetails
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
        /// 支付方式ID
        /// </summary>
        public int PayModeId { get; set; }
        /// <summary>
        /// 支付方式名称
        /// </summary>
        public string PayModeName { get; set; }
        /// <summary>
        /// 总订单数
        /// </summary>
        public int OrderCount { get; set; }
        /// <summary>
        /// 总流水
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 本应支付的服务费(未扣除特批的服务费)
        /// </summary>
        public decimal OriginServiceFee { get; set; }
        /// <summary>
        /// 实际的服务费(扣除了特批的服务费)
        /// </summary>
        public decimal ServiceFee { get; set; }
        /// <summary>
        /// 服务费费率
        /// </summary>
        public decimal ServiceFeeRatio { get; set; }
        /// <summary>
        /// 是否是特批
        /// </summary>
        public bool IsSpecialApproval { get; set; }
        /// <summary>
        /// 特批服务费
        /// </summary>
        public decimal SpecialApprovalServiceFee { get; set; }
        /// <summary>
        /// 特批服务费率
        /// </summary>
        public decimal SpecialApprovalFeeRatio { get; set; }
        /// <summary>
        /// 接口费
        /// </summary>
        public decimal PortFee { get; set; }
        /// <summary>
        /// 接口费率
        /// </summary>
        public decimal PortFeeRatio { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOn { get; set; }

    }
}