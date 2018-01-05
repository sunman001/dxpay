using System;

namespace WEBDEV.Models
{
    /// <summary>
    /// 手机验证码类
    /// </summary>
    public class PhoneVerifyModel
    {
        public PhoneVerifyModel()
        {
            ExpiredTime = 3;
        }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 过期时间(单位:分钟),默认3分钟
        /// </summary>
        public int ExpiredTime { get; set; }
        /// <summary>
        /// 最近一次发送短信的时间
        /// </summary>
        public DateTime LatestSendTime { get; set; }
        /// <summary>
        /// 是否已被使用
        /// </summary>
        public bool Used { get; set; }
    }
}