using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMP.Model;

namespace JMP.MDL
{
    ///<summary>
    ///查询记录表
    ///</summary>
    public class jmp_query
    {

        /// <summary>
        /// 主键
        /// </summary>	
        [EntityTracker(Label = "主键", Description = "主键")]
        public int q_id { get; set; }

        /// <summary>
        /// 查询订单号
        /// </summary>	
        [EntityTracker(Label = "查询订单号", Description = "查询订单号")]
        public string q_code { get; set; }

        /// <summary>
        /// 查询次数
        /// </summary>		
        [EntityTracker(Label = "查询次数", Description = "查询次数")]
        public int q_time { get; set; }

    }
}
