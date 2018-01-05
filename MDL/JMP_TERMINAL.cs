using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using JMP.Model;

namespace JMP.MDL
{
    //终端属性分析表
    public class jmp_terminal
    {
        /// <summary>
        /// ID自动编号主键
        /// </summary>		
        [EntityTracker(Label = "ID自动编号主键", Description = "ID自动编号主键")]
        public int t_id { get; set; }

        /// <summary>
        /// 用户唯一表示key
        /// </summary>		
        [EntityTracker(Label = "用户唯一表示key", Description = "用户唯一表示key")]
        public string t_key { get; set; }

      
        /// <summary>
        /// 移动端mark地址
        /// </summary>	
        [EntityTracker(Label = "移动端mark地址", Description = "移动端mark地址")]
        public string t_mark { get; set; }

        /// <summary>
        /// ip地址
        /// </summary>		
        [EntityTracker(Label = "ip地址", Description = "ip地址")]
        public string t_ip { get; set; }

        /// <summary>
        /// 省份地址
        /// </summary>		
        [EntityTracker(Label = "省份地址", Description = "省份地址")]
        public string t_province { get; set; }

        /// <summary>
        /// IMSIXI信息
        /// </summary>	
        [EntityTracker(Label = "IMSIXI信息", Description = "IMSIXI信息")]
        public string t_imsi { get; set; }

        /// <summary>
        /// 手机运营商
        /// </summary>	
        [EntityTracker(Label = "手机运营商", Description = "手机运营商")]
        public string t_nettype { get; set; }

        /// <summary>
        /// 手机品牌
        /// </summary>	
        [EntityTracker(Label = "手机品牌", Description = "手机品牌")]
        public string t_brand { get; set; }

        /// <summary>
        /// 手机系统
        /// </summary>	
        [EntityTracker(Label = "手机系统", Description = "手机系统")]
        public string t_system { get; set; }

        /// <summary>
        /// 硬件版本
        /// </summary>	
        [EntityTracker(Label = "硬件版本", Description = "硬件版本")]
        public string t_hardware { get; set; }

        /// <summary>
        /// sdk版本
        /// </summary>		
        [EntityTracker(Label = "sdk版本", Description = "sdk版本")]
        public string t_sdkver { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>	
        [EntityTracker(Label = "注册时间", Description = "注册时间")]
        public DateTime t_time { get; set; }

        /// <summary>
        /// 屏幕分辨率
        /// </summary>		
        [EntityTracker(Label = "屏幕分辨率", Description = "屏幕分辨率")]
        public string t_screen { get; set; }

        /// <summary>
        /// 手机网络
        /// </summary>	
        [EntityTracker(Label = "手机网络", Description = "手机网络")]
        public string t_network { get; set; }

        /// <summary>
        /// 应用id
        /// </summary>
        [EntityTracker(Label = "应用id", Description = "应用id")]
        public int t_appid { get; set; }

        /// <summary>
        /// 应用名称
        /// </summary>
        public string a_name { get; set; }
    }
}