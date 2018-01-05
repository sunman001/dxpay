using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMP.Model;

namespace JMP.MDL
{
    public class jmp_queuelist
    {
        /// <summary>
        /// 主键
        /// </summary>	
        [EntityTracker(Label = "主键", Description = "主键")]
        public int q_id { get; set; }

        /// <summary>
        /// 通知地址
        /// </summary>	
        [EntityTracker(Label = "通知地址", Description = "通知地址")]
        public string q_address { get; set; }

        /// <summary>
        /// 签 名
        /// </summary>		
        [EntityTracker(Label = "签名", Description = "签名")]
        public string q_sign { get; set; }

        /// <summary>
        /// 通知状态（通知中、通知结束）
        /// </summary>	、
        [EntityTracker(Label = "通知状态", Description = "通知状态")]
        public int q_noticestate { get; set; }

        /// <summary>
        /// 通知次数
        /// </summary>	
        [EntityTracker(Label = "通知次数", Description = "通知次数")]
        public int q_times { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>	
        [EntityTracker(Label = "添加时间", Description = "添加时间")]
        public DateTime q_noticetimes { get; set; }

        /// <summary>
        /// 表名
        /// </summary>	
        [EntityTracker(Label = "表名", Description = "表名")]
        public string q_tablename { get; set; }

        /// <summary>
        /// 订单id
        /// </summary>	
        [EntityTracker(Label = "订单id", Description = "订单id")]
        public int q_o_id { get; set; }

        /// <summary>
        /// 支付类型
        /// </summary>	
        [EntityTracker(Label = "支付类型", Description = "支付类型")]
        public int trade_type { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        [EntityTracker(Label = "支付时间", Description = "支付时间")]
        public DateTime trade_time { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>	
        [EntityTracker(Label = "支付金额", Description = "支付金额")]
        public decimal trade_price { get; set; }

        /// <summary>
        /// 第三方流水号
        /// </summary>		
        [EntityTracker(Label = "第三方流水号", Description = "第三方流水号")]
        public string trade_paycode { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>		
        [EntityTracker(Label = "订单编号", Description = "订单编号")]
        public string trade_code { get; set; }

        /// <summary>
        /// 商户订单号  
        /// </summary>	
        [EntityTracker(Label = "商户订单号", Description = "商户订单号")]
        public string trade_no { get; set; }

        /// <summary>
        /// 私有信息
        /// </summary>	
        [EntityTracker(Label = "私有信息", Description = "私有信息")]
        public string q_privateinfo { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [EntityTracker(Label = "用户id", Description = "用户id")]
        public int q_uersid { get; set; }
    }
}
