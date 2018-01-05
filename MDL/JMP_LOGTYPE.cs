using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
	 	//日志类型表
		public class jmp_logtype
	{
   		     
      	/// <summary>
		/// 类型id
        /// </summary>		
		private int _l_id;
        [EntityTracker(Label = "类型id", Description = "类型id")]
        public int l_id
        {
            get{ return _l_id; }
            set{ _l_id = value; }
        }        
		/// <summary>
		/// 类型名称
        /// </summary>
        /// 
		private string _l_name;
        [EntityTracker(Label = "类型名称", Description = "类型名称")]
        public string l_name
        {
            get{ return _l_name; }
            set{ _l_name = value; }
        }        
		/// <summary>
		/// 类型值
        /// </summary>		
		private string _l_value;
        [EntityTracker(Label = "类型值", Description = "类型值")]
        public string l_value
        {
            get{ return _l_value; }
            set{ _l_value = value; }
        }        
		   
	}
}