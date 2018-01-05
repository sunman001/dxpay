using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    /// <summary>
    /// 新增通道池配置表,一对多的关系
    /// </summary>
    public class jmp_channel_pool
    {
        /// <summary>
        /// Id
        /// </summary>
        [EntityTracker(Label = "Id", Description = "Id")]
        public int Id { get; set; }
        /// <summary>
        /// 通道名称
        /// </summary>
        [EntityTracker(Label = "通道名称", Description = "通道名称")]
        public string PoolName { get; set; }
        /// <summary>
        /// 是否启用[0:否,1:是]
        /// </summary>
        [EntityTracker(Label = "是否启用", Description = "是否启用")]
        public bool IsEnabled { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [EntityTracker(Label = "创建时间", Description = "创建时间")]
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// 创建者ID
        /// </summary>
        [EntityTracker(Label = "创建者ID", Description = "创建者ID")]
        public int CreatedByUserId { get; set; }
        /// <summary>
        /// 描述信息
        /// </summary>
        [EntityTracker(Label = "描述信息", Description = "描述信息")]
        public string Description { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        [EntityTracker(Label = "操作人", Description = "操作人", Ignore = true)]
        public string u_realname { get; set; }

        /// <summary>
        /// 通道申请公司
        /// </summary>
        [EntityTracker(Label = "通道申请公司", Description = "通道申请公司", Ignore = true)]
        public string l_corporatename { get; set; }

        /// <summary>
        /// 是否启用(1:启用,0:冻结 2：可用 3：超出，4：备用)
        /// </summary>	
        [EntityTracker(Label = "是否启用", Description = "是否启用", Ignore = true)]
        public int l_isenable { get; set; }
    }
}