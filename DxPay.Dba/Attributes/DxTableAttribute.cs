using System;

namespace DxPay.Dba.Attributes
{
    /// <summary>
    /// 表-实体映射属性类
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DxTableAttribute : Attribute
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 表描述
        /// </summary>
        public string Description { get; set; }
    }
}
