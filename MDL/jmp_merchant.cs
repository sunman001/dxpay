using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    ///<summary>
    ///商户表
    ///</summary>
    public class jmp_merchant
    {

        /// <summary>
        /// 主键
        /// </summary>	
        [EntityTracker(Label = "主键", Description = "主键")]
        public int m_id { get; set; }
        /// <summary>
        /// 登陆名
        /// </summary>	
        [EntityTracker(Label = "登陆名", Description = "登陆名")]
        public string m_loginname { get; set; }
        /// <summary>
        /// 密码
        /// </summary>	
        [EntityTracker(Label = "密码", Description = "密码")]
        public string m_pwd { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>	
        [EntityTracker(Label = "真实姓名", Description = "真实姓名")]
        public string m_realname { get; set; }
        /// <summary>
        /// 登陆次数
        /// </summary>	
        [EntityTracker(Label = "登陆次数", Description = "登陆次数")]
        public int m_count { get; set; }
        /// <summary>
        /// 状态(0:正常，1：冻结)
        /// </summary>		
        [EntityTracker(Label = "状态", Description = "状态")]
        public int m_state { get; set; }

    }
}