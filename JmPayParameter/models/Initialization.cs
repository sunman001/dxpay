using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JmPayParameter.Models
{
    /// <summary>
    /// 初始化接口实体
    /// </summary>
    public class Initialization
    {
        /// <summary>
        /// 终端唯一标识
        /// </summary>
        public string t_key { get; set; }
        /// <summary>
        /// 应用id
        /// </summary>
        public int t_appid { get; set; }
        /// <summary>
        /// 移动端mark地址
        /// </summary>
        public string t_mark { get; set; }
        /// <summary>
        /// IMSIXI信息
        /// </summary>
        public string t_imsi { get; set; }
        /// <summary>
        /// 手机品牌
        /// </summary>
        public string t_brand { get; set; }
        /// <summary>
        /// 手机系统
        /// </summary>
        public string t_system { get; set; }
        /// <summary>
        /// 手机型号
        /// </summary>
        public string t_hardware { get; set; }
        /// <summary>
        /// sdk版本
        /// </summary>
        public string t_sdkver { get; set; }
        /// <summary>
        /// 屏幕分辨率
        /// </summary>
        public string t_screen { get; set; }
        /// <summary>
        /// 是否新增（0：新增，1非新增）
        /// </summary>
        public int t_isnew { get; set; }
        /// <summary>
        /// 手机网络
        /// </summary>
        public string t_network { get; set; }
    }
}
