using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    //应用平台表
    public class jmp_platform
    {

        /// <summary>
        /// 平台ID
        /// </summary>		
        private int _p_id;
        [EntityTracker(Label = "平台ID", Description = "平台ID")]
        public int p_id
        {
            get { return _p_id; }
            set { _p_id = value; }
        }

        /// <summary>
        /// 平台名称
        /// </summary>		
        private string _p_name;
        [EntityTracker(Label = "平台名称", Description = "平台名称")]
        public string p_name
        {
            get { return _p_name; }
            set { _p_name = value; }
        }

        /// <summary>
        /// 平台值
        /// </summary>		
        private string _p_value;
        [EntityTracker(Label = "平台值", Description = "平台值")]
        public string p_value
        {
            get { return _p_value; }
            set { _p_value = value; }
        }

        /// <summary>
        /// 状态：1 启用 0 禁用
        /// </summary>		
        private int _p_state;
        [EntityTracker(Label = "状态", Description = "状态")]
        public int p_state
        {
            get { return _p_state; }
            set { _p_state = value; }
        }

    }
}