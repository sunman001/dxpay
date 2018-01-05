using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    //活跃终端表
    public class jmp_liveteral
    {

        /// <summary>
        /// 活跃用户自增ID
        /// </summary>		
        private int _l_id;
        [EntityTracker(Label = "活跃用户自增ID", Description = "活跃用户自增ID")]
        public int l_id
        {
            get { return _l_id; }
            set { _l_id = value; }
        }
        /// <summary>
        /// 关联终端用户KEY
        /// </summary>
        [EntityTracker(Label = "关联终端用户KEY", Description = "关联终端用户KEY")]
        private string _l_teral_key;
        public string l_teral_key
        {
            get { return _l_teral_key; }
            set { _l_teral_key = value; }
        }
        /// <summary>
        /// 访问时间
        /// </summary>
      
        private DateTime _l_time;
        [EntityTracker(Label = "访问时间", Description = "访问时间")]
        public DateTime l_time
        {
            get { return _l_time; }
            set { _l_time = value; }
        }
        /// <summary>
        /// 应用id
        /// </summary>
        [EntityTracker(Label = "应用id", Description = "应用id")]
        public int l_appid { get; set; }
    }
}