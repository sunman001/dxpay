using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    /// <summary>
    /// 消息表
    /// </summary>
    public class jmp_message
    {
        /// <summary>
        /// 消息ID
        /// </summary>	
        [EntityTracker(Label = "消息ID", Description = "消息ID")]
        public int m_id { get; set; }
        /// <summary>
        /// 发送者
        /// </summary>	
        [EntityTracker(Label = "发送者", Description = "发送者")]
        public int m_sender { get; set; }
        /// <summary>
        /// 接收人
        /// </summary>	
        [EntityTracker(Label = "接收人", Description = "接收人")]
        public string m_receiver { get; set; }
        /// <summary>
        /// 消息类型: 1 系统消息 , 2 客户 对 管理 , 3 管理 对客户
        /// </summary>	
        [EntityTracker(Label = "消息类型", Description = "消息类型")]
        public int m_type { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>		
        [EntityTracker(Label = "发送时间", Description = "发送时间")]
        public DateTime m_time { get; set; }
        /// <summary>
        /// 状态: -1 删除 0 未读 1 已读
        /// </summary>	
        [EntityTracker(Label = "状态", Description = "状态")]
        public int m_state { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>	
        [EntityTracker(Label = "消息内容", Description = "消息内容")]
        public string m_content { get; set; }
        /// <summary>
        /// 上级id
        /// </summary>		
        [EntityTracker(Label = "上级id", Description = "上级id")]
        public int m_topid { get; set; }
        /// <summary>
        /// 管理员后台登陆账户（后台发送人）
        /// </summary>
        [EntityTracker(Label = "管理员后台登陆账户", Description = "管理员后台登陆账户",Ignore =true)]
        public string u_loginname { get; set; }

    }
}