using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JmPayParameter.Models
{
    /// <summary>
    /// 查询接口参数实体
    /// </summary>
    public class QueryModels
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string bizcode { get; set; }
        /// <summary>
        /// 应用id
        /// </summary>
        public int appid { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string timestamp { get; set; }
        /// <summary>
        /// 签名（md5（aiiz 非空字符串+appkey）大写）
        /// </summary>
        public string sign { get; set; }
    }
}
