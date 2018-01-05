using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    //提款表
    public class jmp_pay
    {

        /// <summary>
        /// 提款ID
        /// </summary>
        [EntityTracker(Label = "提款ID", Description = "提款ID")]
        public int p_id { get; set; }

        /// <summary>
        /// 流水号
        /// </summary>
        [EntityTracker(Label = "流水号", Description = "流水号")]
        public string p_tradeno { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        [EntityTracker(Label = "申请时间", Description = "申请时间")]
        public DateTime p_applytime { get; set; }

        /// <summary>
        /// 交易号
        /// </summary>
        [EntityTracker(Label = "交易号", Description = "交易号")]
        public string p_dealno { get; set; }

        /// <summary>
        /// 打款时间
        /// </summary>
        [EntityTracker(Label = "打款时间", Description = "打款时间")]
        public DateTime p_paytime { get; set; }

        /// <summary>
        /// 状态 : 0 等待 1 成功 -1 失败
        /// </summary>
        [EntityTracker(Label = "状态", Description = "状态")]
        public int p_state { get; set; }

        /// <summary>
        /// 提款金额
        /// </summary>
        [EntityTracker(Label = "提款金额", Description = "提款金额")]
        public decimal p_money { get; set; }

        /// <summary>
        /// 关联账单
        /// </summary>
        [EntityTracker(Label = "关联账单", Description = "关联账单")]
        public string p_bill_id { get; set; }
    }
}