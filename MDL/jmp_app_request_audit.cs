using JMP.Model;
using System;

namespace JMP.MDL
{
    ///<summary>
    ///应用核查表:应用异常请求监控数据记录表
    ///</summary>
    public class jmp_app_request_audit
    {

        /// <summary>
        /// 主键
        /// </summary>		
        [EntityTracker(Label = "主键", Description = "主键")]
        public int id { get; set; }

        /// <summary>
        /// 应用id
        /// </summary>	
        [EntityTracker(Label = "应用id", Description = "应用id")]
        public int app_id { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>	
        [EntityTracker(Label = "应用名称", Description = "应用名称")]
        public string app_name { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>		
        [EntityTracker(Label = "错误消息", Description = "错误消息")]
        public string message { get; set; }
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
        /// 核查结果类型[0:应用核查,1:通道核查],默认值:0
        /// </summary>
        [EntityTracker(Label = "核查结果类型", Description = "核查结果类型")]
        public int type { get; set; }
    }
}