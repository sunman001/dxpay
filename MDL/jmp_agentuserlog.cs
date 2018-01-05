using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    //用户日志表
    public class jmp_agentuserlog
    {
        /// <summary>
        /// 日志ID
        /// </summary>		
        private int _l_id;

        [EntityTracker(Label = "日志ID", Description = "日志ID")]
        public int l_id
        {
            get { return _l_id; }
            set { _l_id = value; }
        }

        /// <summary>
        /// 用户ID
        /// </summary>		
        private int _l_user_id;

        [EntityTracker(Label = "用户ID", Description = "用户ID")]
        public int l_user_id
        {
            get { return _l_user_id; }
            set { _l_user_id = value; }
        }

        /// <summary>
        /// 日志类别：1 注册 2 登录 3 操作
        /// </summary>		
        private int _l_logtype_id;

        [EntityTracker(Label = "日志类别", Description = "日志类别")]
        public int l_logtype_id
        {
            get { return _l_logtype_id; }
            set { _l_logtype_id = value; }
        }

        /// <summary>
        /// IP地址
        /// </summary>		
        private string _l_ip;

        [EntityTracker(Label = "IP地址", Description = "IP地址")]
        public string l_ip
        {
            get { return _l_ip; }
            set { _l_ip = value; }
        }

        /// <summary>
        /// 位置
        /// </summary>		
        private string _l_location;

        [EntityTracker(Label = "位置", Description = "位置")]
        public string l_location
        {
            get { return _l_location; }
            set { _l_location = value; }
        }

        /// <summary>
        /// 附加信息
        /// </summary>		
        private string _l_info;

        [EntityTracker(Label = "附加信息", Description = "附加信息")]
        public string l_info
        {
            get { return _l_info; }
            set { _l_info = value; }
        }

        /// <summary>
        /// 简短说明
        /// </summary>	
        private string _l_sms;

        [EntityTracker(Label = "简短说明", Description = "简短说明")]
        public string l_sms
        {
            get { return _l_sms; }
            set { _l_sms = value; }
        }

        /// <summary>
        /// 日志时间
        /// </summary>		
        private DateTime _l_time;

        [EntityTracker(Label = "日志时间", Description = "日志时间")]
        public DateTime l_time
        {
            get { return _l_time; }
            set { _l_time = value; }
        }

        [EntityTracker(Label = "姓名", Description = "姓名",Ignore =true)]
        public string DisplayName { get; set; }
        
    }
}