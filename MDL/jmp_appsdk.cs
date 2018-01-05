using JMP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.MDL
{
    ///<summary>
    ///应用上传
    ///</summary>
    public class jmp_appsdk
    {

        /// <summary>
        /// 主键
        /// </summary>	
        [EntityTracker(Label = "主键", Description = "主键")]
        public int id { get; set; }
        /// <summary>
        /// 应用id
        /// </summary>		
        [EntityTracker(Label = "应用id", Description = "应用id")]
        public int appid { get; set; }
        /// <summary>
        /// 连接地址
        /// </summary>	
        [EntityTracker(Label = "连接地址", Description = "连接地址")]
        public string appurl { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>		
        [EntityTracker(Label = "上传时间", Description = "上传时间")]
        public DateTime uptimes { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        [EntityTracker(Label = "应用名称", Description = "应用名称")]
        public string a_name { get; set; }
        /// <summary>
        /// 应用审核状态
        /// </summary>
        [EntityTracker(Label = "应用审核状态", Description = "应用审核状态")]
        public int a_auditstate { get; set; }
        /// <summary>
        /// 应用类型
        /// </summary>
        [EntityTracker(Label = "应用类型", Description = "应用类型")]
        public int a_platform_id { get; set; }
    }
}
