using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    ///<summary>
    ///支付接口配置表
    ///</summary>
    public class jmp_interface
    {

        /// <summary>
        /// 支付接口id
        /// </summary>
        [EntityTracker(Label = "支付接口id", Description = "支付接口id")]
        public int l_id { get; set; }
        /// <summary>
        /// 支付接口配置信息
        /// </summary>	
        [EntityTracker(Label = "支付接口配置信息", Description = "支付接口配置信息")]
        public string l_str { get; set; }
        /// <summary>
        /// 排序
        /// </summary>	
        [EntityTracker(Label = "排序", Description = "排序")]
        public int l_sort { get; set; }
        /// <summary>
        /// 是否启用(1:启用,0:冻结 2：可用 3：超出，4：备用)
        /// </summary>		
        [EntityTracker(Label = "是否启用", Description = "是否启用")]
        public int l_isenable { get; set; }
        /// <summary>
        /// 支付类型id
        /// </summary>	
        [EntityTracker(Label = "支付类型id", Description = "支付类型id")]
        public int l_paymenttype_id { get; set; }
        /// <summary>
        /// 应用id或者风险配置id
        /// </summary>
        [EntityTracker(Label = "支付应用类型id", Description = "支付应用类型id")]
        public string l_apptypeid { get; set; }
        /// <summary>
        /// 申请公司名称
        /// </summary>
        [EntityTracker(Label = "申请公司名称", Description = "申请公司名称")]
        public string l_corporatename { get; set; }
        /// <summary>
        /// json字符串
        /// </summary>
        [EntityTracker(Label = "json字符串", Description = "json字符串")]
        public string l_jsonstr { get; set; }
        /// <summary>
        /// 支付类型
        /// </summary>
        [EntityTracker(Label = "支付类型", Description = "支付类型")]
        public int p_type { get; set; }
        /// <summary>
        /// 支付名称
        /// </summary>
        [EntityTracker(Label = "支付名称", Description = "支付名称")]
        public string p_name { get; set; }
        /// <summary>
        /// 支付通道类型
        /// </summary>
        [EntityTracker(Label = "支付通道类型", Description = "支付通道类型")]
        public string p_extend { get; set; }
        /// <summary>
        /// 支付类型名称
        /// </summary>
        [EntityTracker(Label = "支付类型名称", Description = "支付类型名称")]
        public string zflxname { get; set; }
        /// <summary>
        /// 优先类型（0：应用，1：；类型）
        /// </summary>
        [EntityTracker(Label = "优先类型", Description = "优先类型")]
        public int l_priority { get; set; }
        /// <summary>
        /// 风控类型(0：l_apptypeid对应风险配置表id,1：应用id,2通道池)
        /// </summary>
        [EntityTracker(Label = "风控类型", Description = "风控类型")]
        public int l_risk { get; set; }

        [EntityTracker(Label = "日收入最大金额", Description = "日收入最大金额")]
        public decimal l_daymoney { get; set; }
        /// <summary>
        /// 关联平台
        /// </summary>
        [EntityTracker(Label = "关联平台", Description = "关联平台")]
        public string p_platform { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [EntityTracker(Label = "修改时间", Description = "修改时间")]
        public DateTime? l_time { get; set; }

        /// <summary>
        /// 单笔最小金额
        /// </summary>
        [EntityTracker(Label = "单笔最小金额", Description = "单笔最小金额")]
        public decimal l_minimum { get; set; }
        /// <summary>
        /// 单笔最大金额
        /// </summary>
        [EntityTracker(Label = "单笔最大金额", Description = "单笔最大金额")]
        public decimal l_maximum { get; set; }

        /// <summary>
        /// 成本费率
        /// </summary>
        [EntityTracker(Label = "成本费率", Description = "成本费率")]
        public decimal l_CostRatio { get; set; }

    }
}

