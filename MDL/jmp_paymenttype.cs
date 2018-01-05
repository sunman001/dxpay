using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    ///<summary>
    ///支付类型
    ///</summary>
    public class jmp_paymenttype
    {

        /// <summary>
        /// 支付类型id
        /// </summary>	
        [EntityTracker(Label = "支付类型id", Description = "支付类型id")]
        public int p_id { get; set; }

        /// <summary>
        /// 支付名称
        /// </summary>		
        [EntityTracker(Label = "支付名称", Description = "支付名称")]
        public string p_name { get; set; }

        /// <summary>
        /// 所属支付类型id
        /// </summary>	
        [EntityTracker(Label = "所属支付类型id", Description = "所属支付类型id")]
        public int p_type { get; set; }

        /// <summary>
        /// 支付通道标示（ZFB：支付宝，WX：微信，WFT：威富通）
        /// </summary>		
        [EntityTracker(Label = "支付通道标示", Description = "支付通道标示")]
        public string p_extend { get; set; }

        /// <summary>
        /// 支付类型名称
        /// </summary>
        [EntityTracker(Label = "支付类型名称", Ignore = true)]
        public string zflxname { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        [EntityTracker(Label = "优先级", Description = "优先级")]
        public int p_priority { get; set; }

        /// <summary>
        /// 是否被禁用（0：正常，1：冻结）
        /// </summary>
        [EntityTracker(Label = "是否被禁用", Description = "是否被禁用")]
        public int p_forbidden { get; set; }

        /// <summary>
        /// 关联平台
        /// </summary>
        [EntityTracker(Label = "关联平台", Description = "关联平台")]
        public string p_platform { get; set; }

        /// <summary>
        /// 成本费率
        /// </summary>
        public decimal CostRatio { get; set; }
    }
}

