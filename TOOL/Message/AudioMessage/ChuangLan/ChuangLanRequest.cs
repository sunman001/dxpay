using System;
using System.Configuration;

namespace TOOL.Message.AudioMessage.ChuangLan
{
    /// <summary>
    /// 创蓝短信请求参数实体
    /// </summary>
    public class ChuangLanRequest
    {
        public ChuangLanRequest()
        {
            _account = ConfigurationManager.AppSettings["CHUANGLAN.ACCOUNT"];
            _password = ConfigurationManager.AppSettings["CHUANGLAN.PASSWORD"];
        }
        #region Fields
        private string _account;
        private string _password;
        private string _mobile;
        #endregion

        #region Properties
        /// <summary>
        /// 用户账号(必填参数)
        /// </summary>
        public string Account
        {
            get { return _account; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("用户账号为空");
                }
                _account = value;
            }
        }
        /// <summary>
        /// 用户密码(必填参数)
        /// </summary>
        public string Password
        {
            get { return _password; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("用户密码为空");
                }
                _password = value;
            }
        }
        /// <summary>
        /// 合法的手机号码,号码间用英文逗号分隔(必填参数)
        /// </summary>
        public string Mobile
        {
            get { return _mobile; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("手机号码为空");
                }
                _mobile = value;
            }
        }
        /// <summary>
        /// 短信内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 是否需要状态报告,取值true或false,true,表明需要状态报告；false不需要状态报告(必填参数)
        /// </summary>
        public bool NeedStatus { get; set; }
        /// <summary>
        /// 可选参数,扩展码,用户定义扩展码,3位
        /// </summary>
        public string ExtNo { get; set; }
        #endregion
    }
}
