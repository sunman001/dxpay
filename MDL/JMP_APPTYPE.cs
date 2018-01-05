using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    //应用类型表
    public class jmp_apptype
    {

        /// <summary>
        /// 应用类型
        /// </summary>		
        private int _t_id;

        [EntityTracker(Label = "id", Description = "id")]
        public int t_id
        {
            get { return _t_id; }
            set { _t_id = value; }
        }
        /// <summary>
        /// 类型名称
        /// </summary>		
        private string _t_name;
        [EntityTracker(Label = "类型名称", Description = "类型名称")]
        public string t_name
        {
            get { return _t_name; }
            set { _t_name = value; }
        }
        /// <summary>
        /// 排序(升序)
        /// </summary>		
        private string _t_sort;
        [EntityTracker(Label = "排序", Description = "排序")]
        public string t_sort
        {
            get { return _t_sort; }
            set { _t_sort = value; }
        }
        /// <summary>
        /// 上级ID
        /// </summary>		
        private int _t_topid;
        [EntityTracker(Label = "上级ID", Description = "上级ID")]
        public int t_topid
        {
            get { return _t_topid; }
            set { _t_topid = value; }
        }
        /// <summary>
        /// 状态：0 禁用 1 启用
        /// </summary>		
        private int _t_state;
        [EntityTracker(Label = "状态", Description = "状态")]
        public int t_state
        {
            get { return _t_state; }
            set { _t_state = value; }
        }
        /// <summary>
        /// 所属应用类型
        /// </summary>
        [EntityTracker(Label = "所属应用类型", Description = "所属应用类型")]
        public string t_namecj { get; set; }
    }
}