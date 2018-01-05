using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB.Models.Limit
{
    public class ZtreeModel
    {
        /// <summary>
        /// 权限ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 从属权限ID
        /// </summary>
        public int pId { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 是否打开
        /// </summary>
        public bool open { get; set; }

        /// <summary>
        /// 是否要 Radio 
        /// </summary>
        public bool nocheck { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        [JsonProperty("checked")]
        public bool @checked { get; set; }
    }
}