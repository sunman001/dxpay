using JMP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.MDL
{
    //提款表
    public class jmp_pays
    {
        /// <summary>
        /// 提款ID（主键）
        /// </summary>
        [EntityTracker(Label = "提款ID（主键）", Description = "提款ID（主键）")]
        public int p_id { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        [EntityTracker(Label = "申请时间", Description = "申请时间")]
        public DateTime p_applytime { get; set; }

        /// <summary>
        /// 提款金额
        /// </summary>
        [EntityTracker(Label = "提款金额", Description = "提款金额")]
        public decimal p_money { get; set; }

        /// <summary>
        /// 关联账单
        /// </summary>
        [EntityTracker(Label = "关联账单", Description = "关联账单")]
        public int p_bill_id { get; set; }

        /// <summary>
        /// 开发者id
        /// </summary>
        [EntityTracker(Label = "开发者id", Description = "开发者id")]
        public int p_userid { get; set; }

        /// <summary>
        /// 审核状态（0：等待审核,1：审核通过，-1审核未通过）
        /// </summary>
        [EntityTracker(Label = "审核状态", Description = "审核状态")]
        public int p_state { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        [EntityTracker(Label = "审核人", Description = "审核人")]
        public string p_auditor { get; set; }

        /// <summary>
        /// 提款批次号
        /// </summary>
        [EntityTracker(Label = "提款批次号", Description = "提款批次号")]
        public string p_batchnumber { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        [EntityTracker(Label = "审核时间", Description = "审核时间")]
        public DateTime? p_date { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [EntityTracker(Label = "备注", Description = "备注")]
        public string p_remarks { get; set; }

        /// <summary>
        /// 账单日期
        /// </summary>
        [EntityTracker(Label = "账单日期", Description = "账单日期", Ignore = true)]
        public DateTime SettlementDay { get; set; }

        /// <summary>
        /// 结算金额
        /// </summary>
        [EntityTracker(Label = "结算金额", Description = "结算金额", Ignore = true)]
        public decimal KFZIncome { get; set; }

        /// <summary>
        /// 已提总金额
        /// </summary>
        public decimal p_moneys { get; set; }
     
    }
}
