using System;
using JMP.Model;

namespace JMP.MDL
{
    /// <summary>
    /// [结算]按应用分组的开发者每日结算详情数据表
    /// </summary>
    public class CoSettlementDeveloperAppDetails
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        [EntityTracker(Label = "自增ID", Description = "自增ID")]
        public int Id { get; set; }
        /// <summary>
        /// 账单日期
        /// </summary>
        [EntityTracker(Label = "账单日期", Description = "账单日期")]
        public DateTime SettlementDay { get; set; }
        /// <summary>
        /// 开发者ID
        /// </summary>
        [EntityTracker(Label = "开发者ID", Description = "开发者ID")]
        public int DeveloperId { get; set; }
        /// <summary>
        /// 开发者名称
        /// </summary>
        [EntityTracker(Label = "开发者名称", Description = "开发者名称")]
        public string DeveloperName { get; set; }
        /// <summary>
        /// 应用ID
        /// </summary>
        [EntityTracker(Label = "应用ID", Description = "应用ID")]
        public int AppId { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        [EntityTracker(Label = "应用名称", Description = "应用名称")]
        public string AppName { get; set; }
        /// <summary>
        /// 支付方式ID
        /// </summary>
        [EntityTracker(Label = "支付方式ID", Description = "支付方式ID")]
        public int PayModeId { get; set; }
        /// <summary>
        /// 支付方式名称
        /// </summary>
        [EntityTracker(Label = "支付方式名称", Description = "支付方式名称")]
        public string PayModeName { get; set; }
        /// <summary>
        /// 总订单数
        /// </summary>
        [EntityTracker(Label = "总订单数", Description = "总订单数")]
        public int OrderCount { get; set; }
        /// <summary>
        /// 总流水
        /// </summary>
        [EntityTracker(Label = "总流水", Description = "总流水")]
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 本应支付的服务费(未扣除特批的服务费)
        /// </summary>
        [EntityTracker(Label = "本应支付的服务费", Description = "本应支付的服务费")]
        public decimal OriginServiceFee { get; set; }
        /// <summary>
        /// 实际的服务费(扣除了特批的服务费)
        /// </summary>
        [EntityTracker(Label = "实际的服务费", Description = "实际的服务费")]
        public decimal ServiceFee { get; set; }
        /// <summary>
        /// 服务费费率
        /// </summary>
        [EntityTracker(Label = "服务费费率", Description = "服务费费率")]
        public decimal ServiceFeeRatio { get; set; }
        /// <summary>
        /// 是否是特批
        /// </summary>
        [EntityTracker(Label = "是否是特批", Description = "是否是特批")]
        public bool IsSpecialApproval { get; set; }
        /// <summary>
        /// 特批服务费
        /// </summary>
        [EntityTracker(Label = "特批服务费", Description = "特批服务费")]
        public decimal SpecialApprovalServiceFee { get; set; }
        /// <summary>
        /// 特批服务费率
        /// </summary>
        [EntityTracker(Label = "特批服务费率", Description = "特批服务费率")]
        public decimal SpecialApprovalFeeRatio { get; set; }
        /// <summary>
        /// 接口费
        /// </summary>
        [EntityTracker(Label = "接口费", Description = "接口费")]
        public decimal PortFee { get; set; }
        /// <summary>
        /// 接口费率
        /// </summary>
        [EntityTracker(Label = "接口费率", Description = "接口费率")]
        public decimal PortFeeRatio { get; set; }
        /// <summary>
        /// 默认接口费率[读取支付类型配置表中的接口费率]
        /// </summary>
        [EntityTracker(Label = "默认接口费率", Description = "默认接口费率")]
        public decimal DefaultPortFeeRatio { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [EntityTracker(Label = "创建时间", Description = "创建时间")]
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// 通道ID
        /// </summary>
        [EntityTracker(Label = "通道ID", Description = "通道ID")]
        public int ChannelId { get; set; }
        /// <summary>
        /// 通道名称
        /// </summary>
        [EntityTracker(Label = "通道名称", Description = "通道名称")]
        public string ChannelName { get; set; }
        /// <summary>
        /// 通道成本费
        /// </summary>
        [EntityTracker(Label = "通道成本费", Description = "通道成本费")]
        public decimal ChannelCostFee { get; set; }
        /// <summary>
        /// 通道成本费率
        /// </summary>
        [EntityTracker(Label = "通道成本费率", Description = "通道成本费率")]
        public decimal ChannelCostRatio { get; set; }
        /// <summary>
        /// 通道退款金额
        /// </summary>
        [EntityTracker(Label = "通道退款金额", Description = "通道退款金额")]
        public decimal ChannelRefundAmount { get; set; }

        /// <summary>
        /// 收入金额（流水-服务费-接口费）
        /// </summary>
        [EntityTracker(Label = "收入金额", Description = "收入金额", Ignore = true)]
        public decimal KFZIncome { get; set; }

        /// <summary>
        ///商务提成金额
        /// </summary>
        [EntityTracker(Label = "商务提成金额", Description = "商务提成金额", Ignore = true)]
        public decimal BpPushMoney { get; set; }

        /// <summary>
        /// 代理商提成金额
        /// </summary>
        [EntityTracker(Label = "代理商提成金额", Description = "代理商提成金额", Ignore = true)]
        public decimal AgentPushMoney { get; set; }

        /// <summary>
        /// 关联平台（苹果，安卓，H5）
        /// </summary>
        [EntityTracker(Label = "关联平台", Description = "关联平台", Ignore = true)]
        public int a_platform_id { get; set; }

    }
}