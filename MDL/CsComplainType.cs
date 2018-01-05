using System;
using JMP.Model;

namespace JMP.MDL
{
    /// <summary>
    /// 投诉类型表
    /// </summary>
    public class CsComplainType
    {
        /// <summary>
        /// Id
        /// </summary>
        [EntityTracker(Label = "主键", Description = "主键")]
        public int Id { get; set; }

        /// <summary>
        /// 投诉类型名称
        /// </summary>
        [EntityTracker(Label = "投诉类型名称", Description = "投诉类型名称")]
        public string Name { get; set; }

        /// <summary>
        /// 投诉类型描述
        /// </summary>
        [EntityTracker(Label = "投诉类型描述", Description = "投诉类型描述")]
        public string Description { get; set; }

        /// <summary>
        /// 状态（0：正常，1：冻结）
        /// </summary>
        [EntityTracker(Label = "状态（0：正常，1：冻结）", Description = "状态（0：正常，1：冻结）")]
        public int state { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [EntityTracker(Label = "创建时间", Description = "创建时间")]
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// 创建者ID
        /// </summary>
        [EntityTracker(Label = "创建者ID", Description = "创建者ID")]
        public int CreatedByUserId { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        [EntityTracker(Label = "创建人姓名", Description = "创建人姓名", Ignore = true)]
        public string u_realname { get; set; }

    }
}
