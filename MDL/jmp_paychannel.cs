using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMP.Model;

namespace JMP.MDL
{
    ///<summary>
    ///支付渠道汇总
    ///</summary>
    public class jmp_paychannel
    {

        /// <summary>
        /// 主键
        /// </summary>	
        [EntityTracker(Label = "主键", Description = "主键")]
        public int id { get; set; }

        /// <summary>
        /// 支付渠道名称
        /// </summary>	
        [EntityTracker(Label = "支付渠道名称", Description = "支付渠道名称")]
        public string payname { get; set; }

        /// <summary>
        /// 支付渠道id
        /// </summary>	
        [EntityTracker(Label = "支付渠道id", Description = "支付渠道id")]
        public int payid { get; set; }

        /// <summary>
        /// 统计金额
        /// </summary>		
        [EntityTracker(Label = "统计金额", Description = "统计金额")]
        public decimal money { get; set; }

        /// <summary>
        /// 统计时间
        /// </summary>	
        [EntityTracker(Label = "统计时间", Description = "统计时间")]
        public DateTime datetimes { get; set; }

        /// <summary>
        /// 支付类型
        /// </summary>
        [EntityTracker(Label = "支付类型", Description = "支付类型")]
        public string paytype { get; set; }

        /// <summary>
        /// 成功量
        /// </summary>
        [EntityTracker(Label = "成功量", Description = "成功量")]
        public int success { get; set; }

        /// <summary>
        /// 付费成功率
        /// </summary>
        [EntityTracker(Label = "付费成功率", Description = "付费成功率")]
        public decimal successratio { get; set; }

        /// <summary>
        /// 未付量
        /// </summary>
        [EntityTracker(Label = "未付量", Description = "未付量")]
        public int notpay { get; set; }

        /// <summary>
        /// 投诉量
        /// </summary>
        [EntityTracker(Label = "投诉量", Description = "投诉量")]
        public int complaintcount { get; set; }

        /// <summary>
        /// 补单量
        /// </summary>
        [EntityTracker(Label = "补单量", Description = "补单量")]
        public int refundcount { get; set; }

        /// <summary>
        /// 投诉率
        /// </summary>
        [EntityTracker(Label = "投诉率", Description = "投诉率")]
        public decimal complaintl { get; set; }

        /// <summary>
        /// 补单率
        /// </summary>
        [EntityTracker(Label = "补单率", Description = "补单率")]
        public decimal refundl { get; set; }

        /// <summary>
        /// 成本费率
        /// </summary>
        [EntityTracker(Label = "成本费率", Description = "成本费率",Ignore =true)]
        public decimal ChannelCostRatio { get; set; }

        /// <summary>
        /// 成本金额
        /// </summary>
        [EntityTracker(Label = "成本金额", Description = "成本金额", Ignore = true)]
        public decimal ChannelCostFee { get; set; }

        /// <summary>
        /// 上游结算金额
        /// </summary>
        [EntityTracker(Label = "结算金额", Description = "结算金额", Ignore = true)]
        public decimal SettlementAmount { get; set; }
    }
}
