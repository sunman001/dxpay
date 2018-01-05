using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    ///<summary>
    ///退款申请表
    ///</summary>
    public class jmp_refund
    {

        /// <summary>
        /// 主键
        /// </summary>		
        [EntityTracker(Label = "主键", Description = "主键")]
        public int r_id { get; set; }

        /// <summary>
        /// 申请退款人姓名
        /// </summary>		
        [EntityTracker(Label = "申请退款人姓名", Description = "申请退款人姓名")]
        public string r_name { get; set; }

        /// <summary>
        /// 退款人电话
        /// </summary>	
        [EntityTracker(Label = "退款人电话", Description = "退款人电话")]
        public string r_tel { get; set; }

        /// <summary>
        /// 所属商户id
        /// </summary>	
        [EntityTracker(Label = "所属商户id", Description = "所属商户id")]
        public int r_userid { get; set; }

        /// <summary>
        /// 所属应用id
        /// </summary>		
        [EntityTracker(Label = "所属应用id", Description = "所属应用id")]
        public int r_appid { get; set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        [EntityTracker(Label = "应用名称", Ignore = true)]
        public string a_name { get; set; }

        /// <summary>
        /// 支付流水号
        /// </summary>		
        [EntityTracker(Label = "支付流水号", Description = "支付流水号")]
        public string r_tradeno { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>	
        [EntityTracker(Label = "订单编号", Description = "订单编号")]
        public string r_code { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        [EntityTracker(Label = "退款金额", Description = "退款金额")]
        public decimal r_price { get; set; }

        /// <summary>
        /// 实际支付金额
        /// </summary>		
        [EntityTracker(Label = "实际支付金额", Description = "实际支付金额")]
        public decimal r_money { get; set; }

        /// <summary>
        /// 用户付费时间
        /// </summary>	
        [EntityTracker(Label = "用户付费时间", Description = "用户付费时间")]
        public DateTime r_date { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>		
        [EntityTracker(Label = "提交时间", Description = "提交时间")]
        public DateTime r_time { get; set; }

        /// <summary>
        /// 状态（0：提交申请，1：审核通过，-1：审核未通过）
        /// </summary>	
        [EntityTracker(Label = "状态", Description = "状态")]
        public int r_static { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>	
        [EntityTracker(Label = "审核人", Description = "审核人")]
        public string r_auditor { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>	
        [EntityTracker(Label = "审核时间", Description = "审核时间")]
        public DateTime r_auditortime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>	
        [EntityTracker(Label = "备注", Description = "备注")]
        public string r_remark { get; set; }

        /// <summary>
        /// 商户名称
        /// </summary>
        [EntityTracker(Label = "商户名称", Ignore = true)]
        public string u_realname { get; set; }

        /// <summary>
        /// 渠道id
        /// </summary>
        [EntityTracker(Label = "渠道id", Description = "渠道id")]
        public int r_payid { get; set; }

        /// <summary>
        /// 渠道名称
        /// </summary>
        [EntityTracker(Label = "渠道名称", Ignore = true)]
        public string l_corporatename { get; set; }

        [EntityTracker(Label = "u_id", Ignore = true)]
        public int u_id { get; set; }

    }
}