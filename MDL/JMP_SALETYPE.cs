using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
	 	//销售类型表
		public class jmp_saletype
	{
   		     
      	/// <summary>
		/// 销售方式ID
        /// </summary>		
		private int _s_id;
        [EntityTracker(Label = "销售方式ID", Description = "销售方式ID")]
        public int s_id
        {
            get{ return _s_id; }
            set{ _s_id = value; }
        }  
              
		/// <summary>
		/// 销售方式
        /// </summary>		
		private string _s_name;
        [EntityTracker(Label = "销售方式", Description = "销售方式")]
        public string s_name
        {
            get{ return _s_name; }
            set{ _s_name = value; }
        } 
               
		/// <summary>
		/// 值:0 自定义价格 1 平台价格
        /// </summary>		
		private int _s_value;
        [EntityTracker(Label = "值", Description = "值")]
        public int s_value
        {
            get{ return _s_value; }
            set{ _s_value = value; }
        }   
             
		/// <summary>
		/// 是否启用
        /// </summary>		
		private int _s_state;
        [EntityTracker(Label = "是否启用", Description = "是否启用")]
        public int s_state
        {
            get{ return _s_state; }
            set{ _s_state = value; }
        }        
		   
	}
}