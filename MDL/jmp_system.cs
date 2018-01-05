using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
	 	//系统配置表
		public class jmp_system
	{
   		     
      	/// <summary>
		/// 主键
        /// </summary>		
		private int _s_id;
        [EntityTracker(Label = "主键", Description = "主键")]
        public int s_id
        {
            get{ return _s_id; }
            set{ _s_id = value; }
        }   
             
		/// <summary>
		/// 名称
        /// </summary>		
		private string _s_name;
        [EntityTracker(Label = "名称", Description = "名称")]
        public string s_name
        {
            get{ return _s_name; }
            set{ _s_name = value; }
        }    
            
		/// <summary>
		/// 值
        /// </summary>		
		private string _s_value;
        [EntityTracker(Label = "值", Description = "值")]
        public string s_value
        {
            get{ return _s_value; }
            set{ _s_value = value; }
        }        

		/// <summary>
		/// 状态（0：冻结，1：正常）
        /// </summary>		
		private int _s_state;
        [EntityTracker(Label = "状态", Description = "状态")]
        public int s_state
        {
            get{ return _s_state; }
            set{ _s_state = value; }
        }     
           
		/// <summary>
		/// 备注
        /// </summary>		
		private string _s_remarks;
        [EntityTracker(Label = "备注", Description = "备注")]
        public string s_remarks
        {
            get{ return _s_remarks; }
            set{ _s_remarks = value; }
        }        
		   
	}
}