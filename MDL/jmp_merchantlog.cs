using JMP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.MDL
{
    ///<summary>
    ///商户日志
    ///</summary>
    public class jmp_merchantlog
    {

        /// <summary>
        /// 日志ID
        /// </summary>	
        [EntityTracker(Label = "日志ID", Description = "日志ID")]
        public int l_id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>	
        [EntityTracker(Label = "用户ID", Description = "用户ID")]
        public int l_user_id { get; set; }
        /// <summary>
        /// 日志类别：1 注册 2 登录 3 操作
        /// </summary>	
        [EntityTracker(Label = "日志类别", Description = "日志类别")]
        public int l_logtype_id { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>	
        [EntityTracker(Label = "IP地址", Description = "IP地址")]
        public string l_ip { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        [EntityTracker(Label = "位置", Description = "位置")]
        public string l_location { get; set; }
        /// <summary>
        /// 附加信息
        /// </summary>	
        [EntityTracker(Label = "附加信息", Description = "附加信息")]
        public string l_info { get; set; }
        /// <summary>
        /// 简短说明
        /// </summary>	
        [EntityTracker(Label = "简短说明", Description = "简短说明")]
        public string l_sms { get; set; }
        /// <summary>
        /// 日志时间
        /// </summary>
        [EntityTracker(Label = "日志时间", Description = "日志时间")]
        public DateTime l_time { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [EntityTracker(Label = "用户名", Description = "用户名",Ignore =true)]
        public string m_realname { get; set; }

    }
}
