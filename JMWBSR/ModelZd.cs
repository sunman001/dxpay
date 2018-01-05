/************聚米支付平台__接收终端信息实体类************/
//描述：接收终端信息实体类
//功能：接收终端信息实体类
//开发者：秦际攀
//开发时间: 2016.03.21
/************聚米支付平台__接收终端信息实体类************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMWBSR
{
    /// <summary>
    /// 终端信息类
    /// </summary>
    public class ModelZd
    {
        /// <summary>
        /// 终端唯一表示
        /// </summary>
        //public string t_key { get; set; }
        private string T_key;
        /// <summary>
        /// 终端唯一表示
        /// </summary>
        public string t_key
        {
            get { return T_key; }
            set
            {
                if (!string.IsNullOrEmpty(value.Trim()))
                {
                    if (value.Trim().Length > 64)
                    {
                        throw new JMP.TOOL.Ex("{\"message\":\"t_key超长\",\"result\":9986}");
                    }
                    else
                    {
                        T_key = value;
                    }
                }
                else
                {
                    throw new JMP.TOOL.Ex("{\"message\":\"参数t_key有误\",\"result\":9997}");
                }
            }
        }
        /// <summary>
        /// 应用Key
        /// </summary>
        //public string t_appkey { get; set; }
        private string T_appkey;
        /// <summary>
        /// 应用Key
        /// </summary>
        public string t_appkey
        {
            get { return T_appkey; }
            set
            {
                if (!string.IsNullOrEmpty(value.Trim()) && value.Trim().Length < 100)
                {
                    T_appkey = value;
                }
                else
                {
                    throw new JMP.TOOL.Ex("{\"message\":\"参数t_appkey有误\",\"result\":9996}");
                }
            }
        }
        /// <summary>
        /// 移动端mark地址
        /// </summary>
        //public string t_mark { get; set; }
        private string T_mark;
        /// <summary>
        /// 移动端mark地址
        /// </summary>
        public string t_mark
        {
            get { return T_mark; }
            set
            {
                if (!string.IsNullOrEmpty(value.Trim()) && value.Trim().Length <= 32)
                {
                    T_mark = value;
                }
                else
                {
                    throw new JMP.TOOL.Ex("{\"message\":\"参数t_mark有误\",\"result\":9994}");
                }
            }
        }
        /// <summary>
        /// IMSIXI信息
        /// </summary>
        //public string t_imsi { get; set; }
        private string T_imsi;
        /// <summary>
        /// IMSIXI信息
        /// </summary>
        public string t_imsi
        {
            get { return T_imsi; }
            set
            {
                if (!string.IsNullOrEmpty(value.Trim()) && value.Trim().Length <= 32)
                {
                    T_imsi = value;
                }
                else
                {
                    throw new JMP.TOOL.Ex("{\"message\":\"参数t_imsi有误\",\"result\":9992}");
                }
            }
        }
        /// <summary>
        /// 手机品牌
        /// </summary>
        //public string t_brand { get; set; }
        private string T_brand;
        /// <summary>
        /// 手机品牌
        /// </summary>
        public string t_brand
        {
            get { return T_brand; }
            set
            {
                if (!string.IsNullOrEmpty(value.Trim()) && value.Trim().Length <= 32)
                {
                    T_brand = value;
                }
                else
                {
                    throw new JMP.TOOL.Ex("{\"message\":\"参数t_brand有误\",\"result\":9991}");
                }
            }
        }
        /// <summary>
        /// 手机系统
        /// </summary>
        //public string t_system { get; set; }
        private string T_system;
        /// <summary>
        /// 手机系统
        /// </summary>
        public string t_system
        {
            get { return T_system; }
            set
            {
                if (!string.IsNullOrEmpty(value.Trim()) && value.Trim().Length <= 32)
                {
                    T_system = value;
                }
                else
                {
                    throw new JMP.TOOL.Ex("{\"message\":\"参数t_system有误\",\"result\":9990}");
                }
            }
        }
        /// <summary>
        /// 硬件版本（手机型号）
        /// </summary>
        //public string t_hardware { get; set; }
        private string T_hardware;
        /// <summary>
        /// 硬件版本（手机型号）
        /// </summary>
        public string t_hardware
        {
            get { return T_hardware; }
            set
            {
                if (!string.IsNullOrEmpty(value.Trim()) && value.Trim().Length <= 32)
                {
                    T_hardware = value;
                }
                else
                {
                    throw new JMP.TOOL.Ex("{\"message\":\"参数t_hardware有误\",\"result\":9989}");
                }
            }
        }
        /// <summary>
        /// sdk版本
        /// </summary>
        //public string t_sdkver { get; set; }
        private string T_sdkver;
        /// <summary>
        /// sdk版本
        /// </summary>
        public string t_sdkver
        {
            get { return T_sdkver; }
            set
            {
                if (!string.IsNullOrEmpty(value.Trim()) && value.Trim().Length <= 16)
                {
                    T_sdkver = value;
                }
                else
                {
                    throw new JMP.TOOL.Ex("{\"message\":\"参数t_sdkver有误\",\"result\":9987}");
                }
            }
        }
        /// <summary>
        /// 屏幕分辨率
        /// </summary>
        //public string t_screen { get; set; }
        private string T_screen;
        /// <summary>
        /// 屏幕分辨率
        /// </summary>
        public string t_screen
        {
            get { return T_screen; }
            set
            {
                if (!string.IsNullOrEmpty(value.Trim()) && value.Trim().Length <= 16)
                {
                    T_screen = value;
                }
                else
                {
                    throw new JMP.TOOL.Ex("{\"message\":\"参数t_screen有误\",\"result\":9988}");
                }
            }
        }
        /// <summary>
        /// 是否新增（0：新增，1非新增）
        /// </summary>
        public int t_isnew { get; set; }
        /// <summary>
        /// 手机网络
        /// </summary>
        //public string t_network { get; set; }
        private string T_network;
        /// <summary>
        /// 手机网络
        /// </summary>
        public string t_network
        {
            get { return T_network; }
            set
            {
                if (!string.IsNullOrEmpty(value.Trim()) && value.Trim().Length <= 16)
                {
                    T_network = value;
                }
                else
                {
                    throw new JMP.TOOL.Ex("{\"message\":\"参数t_network有误\",\"result\":9993}");
                }
            }
        }


    }
}
