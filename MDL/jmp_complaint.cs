using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    ///<summary>
    ///应用投诉表
    ///</summary>
    public class jmp_complaint
    {

        /// <summary>
        /// 主键
        /// </summary>	
        [EntityTracker(Label = "主键", Description = "主键")]
        public int c_id { get; set; }
        /// <summary>
        /// 所属应用id
        /// </summary>	
        [EntityTracker(Label = "所属应用id", Description = "所属应用id")]
        public int c_appid { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        [EntityTracker(Label = "应用名称", Description = "应用名称",Ignore =true)]
        public string a_name { get; set; }
        /// <summary>
        /// 所属用户id
        /// </summary>	
        [EntityTracker(Label = "所属用户id", Description = "所属用户id")]
        public int c_userid { get; set; }

        /// <summary>
        /// 所有用户
        /// </summary>
        [EntityTracker(Label = "所有用户", Description = "所有用户")]
        public string u_realname { get; set; }
        /// <summary>
        /// 支付渠道id
        /// </summary>
        [EntityTracker(Label = "支付渠道id", Description = "支付渠道id")]
        public int c_payid { get; set; }
        /// <summary>
        /// 支付渠道名称
        /// </summary>
        [EntityTracker(Label = "支付渠道名称", Description = "支付渠道名称",Ignore =true)]
        public string l_corporatename { get; set; }
        /// <summary>
        /// 交易流水号
        /// </summary>	
        [EntityTracker(Label = "交易流水号", Description = "交易流水号")]
        public string c_tradeno { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>	
        [EntityTracker(Label = "订单编号", Description = "订单编号")]
        public string c_code { get; set; }
        /// <summary>
        /// 付款金额
        /// </summary>	
        [EntityTracker(Label = "付款金额", Description = "付款金额")]
        public decimal c_money { get; set; }
        /// <summary>
        /// 付款时间
        /// </summary>	
        [EntityTracker(Label = "付款时间", Description = "付款时间")]
        public DateTime c_times { get; set; }
        /// <summary>
        /// 投诉时间
        /// </summary>	
        [EntityTracker(Label = "投诉时间", Description = "投诉时间")]
        public DateTime c_datimes { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>	
        [EntityTracker(Label = "提交时间", Description = "提交时间")]
        public DateTime c_tjtimes { get; set; }
        /// <summary>
        /// 提交人
        /// </summary>	
        [EntityTracker(Label = "提交人", Description = "提交人")]
        public string c_tjname { get; set; }
        /// <summary>
        /// 处理人
        /// </summary>	
        [EntityTracker(Label = "处理人", Description = "处理人")]
        public string c_clname { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>	
        [EntityTracker(Label = "处理时间", Description = "处理时间")]
        public DateTime c_cltimes { get; set; }
        /// <summary>
        /// 处理结果
        /// </summary>	
        [EntityTracker(Label = "处理结果", Description = "处理结果")]
        public string c_result { get; set; }
        /// <summary>
        /// 投诉原因
        /// </summary>
        [EntityTracker(Label = "投诉原因", Description = "投诉原因")]
        public string c_reason { get; set; }
        /// <summary>
        /// 状态（0：提交，1处理）
        /// </summary>	
        [EntityTracker(Label = "状态", Description = "状态")]
        public string c_state { get; set; }

       


    }
}