using System;
using JMP.Model;

namespace JMP.MDL
{
  public  class CoOperationLog
    {

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 详细信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// 创建者ID
        /// </summary>
        public int CreatedById { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatedByName { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }
    }
}
