using DxPay.Dba.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMP.Model;

namespace JMP.MDL
{
    /// <summary>
    /// 风险等级类型表
    /// </summary>
    public class jmp_risklevel
    {
        /// <summary>
        /// 主键自增
        /// </summary>
        [EntityTracker(Label = "主键自增", Description = "主键自增")]
        public int r_id { get; set; }
        /// <summary>
        /// 风险等级名称
        /// </summary>
        [EntityTracker(Label = "风险等级名称", Description = "风险等级名称")]
        public string r_name { get; set; }

    }
}
