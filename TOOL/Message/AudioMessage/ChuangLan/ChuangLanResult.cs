using System;

namespace TOOL.Message.AudioMessage.ChuangLan
{
    /// <summary>
    /// 创蓝的解析结果
    /// </summary>
    public class ChuangLanResult
    {
        /// <summary>
        /// 响应时间(字符串)
        /// </summary>
        public string ReponseTimeString { get; set; }
        /// <summary>
        /// 响应时间(时间格式)
        /// </summary>
        public DateTime ReponseTime
        {
            get { return Convert.ToDateTime(ReponseTimeString.Trim().Insert(12, ":").Insert(10, ":").Insert(8, " ").Insert(6, "-").Insert(4, "-")); }
        }
        /// <summary>
        /// 返回的状态码
        /// </summary>
        public string Code { get; set; }
        public string Message { get; set; }

        public bool Success
        {
            get { return Code.Trim() == "0"; }
        }
    }
}
