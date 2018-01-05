using System;
using JMP.Model;

namespace JMP.MDL
{
    ///<summary>
    ///订单异常核查表:异常订单监控数据记录表
    ///</summary>
    public class jmp_order_audit
    {

        /// <summary>
        /// 主键
        /// </summary>		
        [EntityTracker(Label = "主键", Description = "主键")]
        public int id { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>	
        [EntityTracker(Label = "订单号", Description = "订单号")]
        public string order_code { get; set; }

        /// <summary>
        /// 订单表名
        /// </summary>		
        [EntityTracker(Label = "订单表名", Description = "订单表名")]
        public string order_table_name { get; set; }

        /// <summary>
        /// 应用id
        /// </summary>	
        [EntityTracker(Label = "应用id", Description = "应用id")]
        public int app_id { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>		
        [EntityTracker(Label = "错误消息", Description = "错误消息")]
        public string message { get; set; }

        /// <summary>
        /// 交易流水号
        /// </summary>	
        [EntityTracker(Label = "交易流水号", Description = "交易流水号")]
        public string trade_no { get; set; }

        /// <summary>
        /// 付款时间
        /// </summary>	
        [EntityTracker(Label = "付款时间", Description = "付款时间")]
        public DateTime payment_time { get; set; }

        /// <summary>
        /// 实际支付金额
        /// </summary>		
        [EntityTracker(Label = "实际支付金额", Description = "实际支付金额")]
        public decimal payment_amount { get; set; }

        /// <summary>
        /// 状态(0:新建,1:已处理)
        /// </summary>	
        [EntityTracker(Label = "状态", Description = "状态")]
        public string payment_status { get; set; }

        /// <summary>
        /// 订单金额(在盾行生成订单时的金额)
        /// </summary>	
        [EntityTracker(Label = "订单金额", Description = "订单金额")]
        public decimal order_amount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [EntityTracker(Label = "创建时间", Description = "创建时间")]
        public DateTime created_on { get; set; }

        /// <summary>
        /// 是否已处理(0:否,1:是,默认值:0)
        /// </summary>
        [EntityTracker(Label = "是否已处理", Description = "是否已处理")]
        public int is_processed { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>	
        [EntityTracker(Label = "处理时间", Description = "处理时间")]
        public DateTime? processed_time { get; set; }

        /// <summary>
        /// 处理者
        /// </summary>	
        [EntityTracker(Label = "处理者", Description = "处理者")]
        public string processed_by { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>	
        [EntityTracker(Label = "处理结果", Description = "处理结果")]
        public string processed_result { get; set; }

        /// <summary>
        /// 是否已发送短信
        /// </summary>
        [EntityTracker(Label = "是否已发送短信", Description = "是否已发送短信")]
        public int is_send_message { get; set; }

        /// <summary>
        /// 短信发送时间
        /// </summary>
        [EntityTracker(Label = "短信发送时间", Description = "短信发送时间")]
        public DateTime? message_send_time { get; set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        [EntityTracker(Label = "应用名称", Ignore = true)]
        public string a_name { get; set; }
    }
}