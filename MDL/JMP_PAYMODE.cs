using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    //支付方式表
    public class jmp_paymode
    {

        /// <summary>
        /// 支付类型ID
        /// </summary>
        [EntityTracker(Label = "支付类型ID", Description = "支付类型ID")]
        public int p_id { get; set; }
        /// <summary>
        /// 支付名称
        /// </summary>
        [EntityTracker(Label = "支付名称", Description = "支付名称")]
        public string p_name { get; set; }
        /// <summary>
        /// 第三方通道手续费
        /// </summary>
        [EntityTracker(Label = "第三方手续费", Description = "第三方手续费")]
        public decimal p_rate { get; set; }
        /// <summary>
        /// 是否启用（1正常，0：冻结）
        /// </summary>
        [EntityTracker(Label = "是否启用", Description = "是否启用")]
        public int p_state { get; set; }
        /// <summary>
        /// 是否锁定(1:正常，0：为锁定)
        /// </summary>、
        [EntityTracker(Label = "是否锁定", Description = "是否锁定")]
        public int p_islocked { get; set; }

    }
}