using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JmPayParameter.Models
{
    /// <summary>
    /// 第二次请求接收实体对象
    /// </summary>
    public class BanlModels
    {
        /// <summary>
        /// 请求类型
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 加密字符串
        /// </summary>
        public string code { get; set; }
    }
}
