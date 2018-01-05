using System.Collections.Generic;
using System.Reflection;

namespace DxPay.Dba.Extensions
{
    public class PropertyMapping
    {
        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 属性信息
        /// </summary>
        public PropertyInfo[] PropertyInfos { get; set; }
        /// <summary>
        /// 属性字段
        /// </summary>
        public string PropertiesString { get; set; }
        /// <summary>
        /// 属性集合
        /// </summary>
        public List<string> PropertiesList { get; set; }
        /// <summary>
        /// 字段
        /// </summary>
        public string PrimaryKey { get; set; }
    }
}