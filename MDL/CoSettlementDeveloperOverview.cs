using System;
using JMP.Model;

namespace JMP.MDL
{
    /// <summary>
    /// 开发者每日结算详情表
    /// </summary>
    public class CoSettlementDeveloperOverview
    {
        /// <summary>
        /// 主键
        /// </summary>
        [EntityTracker(Label = "主键", Description = "主键")]
        public int Id { get; set; }
        /// <summary>
        /// 开发者ID
        /// </summary>
        [EntityTracker(Label = "开发者ID", Description = "开发者ID")]
        public int DeveloperId { get; set; }
        /// <summary>
        /// 开发者姓名
        /// </summary>
        [EntityTracker(Label = "开发者姓名", Description = "开发者姓名")]
        public string DeveloperName { get; set; }
        /// <summary>
        /// 账单日期
        /// </summary>
        [EntityTracker(Label = "账单日期", Description = "账单日期")]
        public DateTime SettlementDay { get; set; }
        /// <summary>
        /// 生成日期
        /// </summary>
        [EntityTracker(Label = "生成日期", Description = "生成日期")]
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// 总流水金额
        /// </summary>
        [EntityTracker(Label = "总流水金额", Description = "总流水金额")]
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 服务费
        /// </summary>
        [EntityTracker(Label = "服务费", Description = "服务费")]
        public decimal ServiceFee { get; set; }
        /// <summary>
        /// 服务费费率
        /// </summary>
        [EntityTracker(Label = "服务费费率", Description = "服务费费率")]
        public decimal ServiceFeeRatio { get; set; }
        /// <summary>
        /// 商务提成金额
        /// </summary>
        [EntityTracker(Label = "商务提成", Description = "商务提成")]
        public decimal BpPushMoney { get; set; }
        /// <summary>
        /// 商务提成比例
        /// </summary>
        [EntityTracker(Label = "商务提成比例", Description = "商务提成比例")]
        public decimal BpPushMoneyRatio { get; set; }
        /// <summary>
        /// 代理商提成金额
        /// </summary>
        [EntityTracker(Label = "代理商提成金额", Description = "代理商提成金额")]
        public decimal AgentPushMoney { get; set; }
        /// <summary>
        /// 代理商提成比例
        /// </summary>
        [EntityTracker(Label = "代理商提成比例", Description = "代理商提成比例")]
        public decimal AgentPushMoneyRatio { get; set; }
        /// <summary>
        /// 接口费
        /// </summary>
        [EntityTracker(Label = "接口费", Description = "接口费")]
        public decimal PortFee { get; set; }
        /// <summary>
        /// 成本费
        /// </summary>
        [EntityTracker(Label = "成本费", Description = "成本费")]
        public decimal CostFee { get; set; }

        /// <summary>
        /// 收入金额（流水-服务费-接口费）
        /// </summary>
        [EntityTracker(Label = "收入金额", Description = "收入金额", Ignore = true)]
        public decimal KFZIncome { get; set; }

        /// <summary>
        /// 代理商姓名
        /// </summary>
        [EntityTracker(Label = "代理商姓名", Description = "代理商姓名", Ignore = true)]
        public string DisplayName { get; set; }

        /// <summary>
        /// 类型(1,商务，2代理商)
        /// </summary>
        [EntityTracker(Label = "类型", Description = "类型", Ignore = true)]
        public int relation_type { get; set; }

        /// <summary>
        /// 代理商ID
        /// </summary>
        [EntityTracker(Label = "代理商ID", Description = "代理商ID", Ignore = true)]
        public int agentid { get; set; }

        /// <summary>
        /// 商务ID
        /// </summary>
        [EntityTracker(Label = "商务ID", Description = "商务ID", Ignore = true)]
        public int bpid { get; set; }

        /// <summary>
        /// 商务姓名
        /// </summary>
        [EntityTracker(Label = "商务姓名", Description = "商务姓名", Ignore = true)]
        public string bpname { get; set; }

        /// <summary>
        /// 已提款金额
        /// </summary>
        [EntityTracker(Label = "已提款金额", Description = "已提款金额", Ignore = true)]
        public decimal p_money { get; set; }

        /// <summary>
        /// 已付金额
        /// </summary>
        [EntityTracker(Label = "已付金额", Description = "已付金额", Ignore = true)]
        public decimal yf_money { get; set; }


        /// <summary>
        /// 可提金额
        /// </summary>
        [EntityTracker(Label = "可提金额", Description = "可提金额", Ignore = true)]
        public decimal ketiMoney { get; set; }


        [EntityTracker(Label = "原始金额（未退款的）", Description = "原始金额（未退款的）", Ignore = true)]
        public decimal OriginalTotalAmount { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        [EntityTracker(Label = "退款金额", Description = "退款金额", Ignore = true)]
        public decimal RefundAmount { get; set; }

        /// <summary>
        /// 冻结金额
        /// </summary>
        [EntityTracker(Label = "冻结金额", Description = "冻结金额", Ignore = true)]
        public decimal FrozenMoney { get; set; }
    }
}