using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMP.Model;

namespace JMP.MDL
{
    /// <summary>
    /// 银行打款对接表
    /// </summary>
    public class jmp_BankPlaymoney
    {
        /// <summary>
        /// 主键
        /// </summary>
        [EntityTracker(Label = "主键", Description = "主键")]
        public int b_id { get; set; }

        /// <summary>
        /// 提款批次号
        /// </summary>
        [EntityTracker(Label = "提款批次号", Description = "提款批次号")]
        public string b_batchnumber { get; set; }

        /// <summary>
        /// 交易编号(提交给代付的)
        /// </summary>
        [EntityTracker(Label = "交易编号", Description = "交易编号")]
        public string b_number { get; set; }

        /// <summary>
        /// 交易流水号(代付返回的)
        /// </summary>
        [EntityTracker(Label = "交易流水号", Description = "交易流水号")]
        public string b_tradeno { get; set; }

        /// <summary>
        /// 交易状态（0：等待打款，1：交易成功，-1：交易失败，2：处理中,3:订单异常,4:已退款,5:审核未通过，6：冻结）
        /// </summary>
        [EntityTracker(Label = "交易状态", Description = "交易状态")]
        public int b_tradestate { get; set; }

        /// <summary>
        /// 申请日期
        /// </summary>
        [EntityTracker(Label = "申请日期", Description = "申请日期")]
        public DateTime b_date { get; set; }

        /// <summary>
        /// 关联卡号id
        /// </summary>
        [EntityTracker(Label = "关联卡号id", Description = "关联卡号id")]
        public int b_bankid { get; set; }

        /// <summary>
        /// 打款金额
        /// </summary>
        [EntityTracker(Label = "打款金额", Description = "打款金额")]
        public decimal b_money { get; set; }

        /// <summary>
        /// 代付申请提交时间
        /// </summary>
        [EntityTracker(Label = "代付申请提交时间", Description = "代付申请提交时间")]
        public DateTime? b_paydate { get; set; }

        /// <summary>
        /// 上游代付通道ID
        /// </summary>
        [EntityTracker(Label = "上游代付通道ID", Description = "上游代付通道ID", Ignore = true)]
        public int b_payforanotherId { get; set; }

        /// <summary>
        /// 银行名称
        /// </summary>
        [EntityTracker(Label = "银行名称", Description = "银行名称", Ignore = true)]
        public string u_bankname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [EntityTracker(Label = "", Description = "", Ignore = true)]
        public string u_banknumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [EntityTracker(Label = "", Description = "", Ignore = true)]
        public string u_name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [EntityTracker(Label = "", Description = "", Ignore = true)]
        public string u_realname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [EntityTracker(Label = "", Description = "", Ignore = true)]
        public string u_phone { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        [EntityTracker(Label = "审核状态", Description = "审核状态", Ignore = true)]
        public int p_state { get; set; }

        /// <summary>
        /// 打款方式(2:代付打款，1：财务手动打款，0：未处理)
        /// </summary>
        [EntityTracker(Label = "打款方式", Description = "打款方式")]
        public int b_payfashion { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [EntityTracker(Label = "备注", Description = "备注")]
        public string b_remark { get; set; }

        /// <summary>
        /// 代付通道名称
        /// </summary>
        [EntityTracker(Label = "代付通道名称", Description = "代付通道名称", Ignore = true)]
        public string p_InterfaceName { get; set; }

        /// <summary>
        /// 代付通道商户号
        /// </summary>
        [EntityTracker(Label = "代付通道商户号", Description = "代付通道商户号", Ignore = true)]
        public string p_MerchantNumber { get; set; }

        /// <summary>
        /// 付款标识
        /// </summary>
        [EntityTracker(Label = "付款标识", Description = "付款标识", Ignore = true)]
        public string u_flag { get; set; }

        /// <summary>
        /// 提现手续费
        /// </summary>
        [EntityTracker(Label = "提现手续费", Description = "提现手续费")]
        public int b_ServiceCharge { get; set; }
    }
}
