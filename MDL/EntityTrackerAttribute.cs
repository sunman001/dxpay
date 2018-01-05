using System;

namespace JMP.Model
{
    /// <summary>
    /// 实体类配置属性
    /// </summary>
    public class EntityTrackerAttribute : Attribute
    {
        /// <summary>
        /// 属性的标签[显示名称]
        /// </summary>
        [EntityTracker(Label = "属性的标签", Ignore = true)]
        public string Label { get; set; }
        /// <summary>
        /// 属性描述[可选]
        /// </summary>
        [EntityTracker(Label = "属性描述", Ignore = true)]
        public string Description { get; set; }
        /// <summary>
        /// 是否忽略,默认:否
        /// </summary>
        [EntityTracker(Label = "是否忽略", Ignore = true)]
        public bool Ignore { get; set; }
    }
}
