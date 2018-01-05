using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    //公告表
    public class jmp_notice
    {

        /// <summary>
        /// 公告ID
        /// </summary>	
        [EntityTracker(Label = "公告ID", Description = "公告ID")]
        public int n_id { get; set; }
        /// <summary>
        /// 公告标题
        /// </summary>
        [EntityTracker(Label = "公告标题", Description = "公告标题")]
        public string n_title { get; set; }
        /// <summary>
        /// 公告内容
        /// </summary>	
        [EntityTracker(Label = "公告内容", Description = "公告内容")]
        public string n_content { get; set; }
        /// <summary>
        /// 公告时间
        /// </summary>		
        [EntityTracker(Label = "公告时间", Description = "公告时间")]
        public DateTime n_time { get; set; }
        /// <summary>
        /// 顶置优先级：默认 0
        /// </summary>	
        [EntityTracker(Label = "顶置优先级", Description = "顶置优先级")]
        public int n_top { get; set; }
        /// <summary>
        /// 公告状态
        /// </summary>	
        [EntityTracker(Label = "公告状态", Description = "公告状态")]
        public int n_state { get; set; }
        /// <summary>
        /// 发布人（管理管理员表id）
        /// </summary>	
        [EntityTracker(Label = "发布人", Description = "发布人")]
        public int n_locuserid { get; set; }
        /// <summary>
        /// 管理员用户名（发布人）
        /// </summary>
        [EntityTracker(Label = "管理员用户名", Description = "管理员用户名",Ignore =true)]
        public string  u_loginname { get; set; }

    }
}