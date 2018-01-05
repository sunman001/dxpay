using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    /// <summary>
    /// 管理员权限表
    /// </summary>
    public class jmp_limit
    {
        /// <summary>
        /// 权限ID
        /// </summary>	
        [EntityTracker(Label = "权限ID", Description = "权限ID")]
        public int l_id { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        [EntityTracker(Label = "权限名称", Description = "权限名称")]
        public string l_name { get; set; }
        /// <summary>
        /// 权限从属:父级为0
        /// </summary>		
        [EntityTracker(Label = "权限从属", Description = "权限从属")]
        public int l_topid { get; set; }
        /// <summary>
        /// 权限页面
        /// </summary>	
         [EntityTracker(Label = "权限页面", Description = "权限页面")]
        public string l_url { get; set; }
        /// <summary>
        /// 排序
        /// </summary>	
        [EntityTracker(Label = "排序", Description = "排序")]
        public int l_sort { get; set; }
        /// <summary>
        /// 权限状态
        /// </summary>	
        [EntityTracker(Label = "权限状态", Description = "权限状态")]
        public int l_state { get; set; }
        /// <summary>
        /// 图标
        /// </summary>	
        [EntityTracker(Label = "图标", Description = "图标")]
        public string l_icon { get; set; }
        /// <summary>
        /// 权限类型(0:后台权限，1：前台权限)
        /// </summary>	
        [EntityTracker(Label = "权限类型", Description = "权限类型")]
        public int l_type { get; set; }

    }
}