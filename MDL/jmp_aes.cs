using System;
using JMP.Model;
namespace JMP.MDL
{
    ///<summary>
    ///解密出错记录表
    ///</summary>
    public class jmp_aes
    {

        /// <summary>
        /// 主键
        /// </summary>		
        [EntityTracker(Label = "主键", Description = "主键")]
        public int id { get; set; }

        /// <summary>
        /// 原文
        /// </summary>		
        [EntityTracker(Label = "原文", Description = "原文")]
        public string yw { get; set; }
        /// <summary>
        /// 密文
        /// </summary>	
       [EntityTracker(Label = "密文", Description = "密文")]
        public string mw { get; set; }
        /// <summary>
        /// 密文大小
        /// </summary>	
        [EntityTracker(Label = "密文大小", Description = "密文大小")]
        public string mwdx { get; set; }
        /// <summary>
        /// 数据包大小
        /// </summary>	
        [EntityTracker(Label = "数据包大小", Description = "数据包大小")]
        public string bdx { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>		
        [EntityTracker(Label = "记录时间", Description = "记录时间")]
        public DateTime time { get; set; }

       
    }
}