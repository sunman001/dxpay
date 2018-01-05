using System;
using JMP.MDL;
using JMP.TOOL;

namespace WEBDEV.Util.Logger
{
    /// <summary>
    /// 操作日志类
    /// </summary>
    public class OperateLogger :ILog
    {
        /// <summary>
        /// 操作日志写入方法
        /// </summary>
        /// <param name="summary">日志摘要</param>
        /// <param name="message">日志详情</param>
        public void Log(string summary, string message)
        {
            var mUserlog = new jmp_userlog();
            var bllUserlog = new JMP.BLL.jmp_userlog();
            mUserlog.l_user_id = UserInfo.UserId;
            mUserlog.l_logtype_id = 3;
            mUserlog.l_ip = RequestHelper.GetClientIp();
            //m_userlog.l_location = IPAddress.GetAddressByIp(l_ip);
            mUserlog.l_location = "";
            mUserlog.l_sms = summary;
            mUserlog.l_info = message;
            mUserlog.l_time = DateTime.Now;
            bllUserlog.Add(mUserlog);
        }
    }
}