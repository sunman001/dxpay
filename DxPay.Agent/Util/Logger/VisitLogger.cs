using System;
using JMP.TOOL;

namespace DxPay.Agent.Util.Logger
{
    /// <summary>
    /// 访问日志类
    /// </summary>
    public class VisitLogger : ILog
    {
        /// <summary>
        /// 访问日志写入方法
        /// </summary>
        /// <param name="summary">日志摘要</param>
        /// <param name="message">日志详情</param>
        public void Log(string summary, string message)
        {
            var mLocuserlog = new JMP.MDL.jmp_locuserlog();
            var bllLocuserlog = new JMP.BLL.jmp_locuserlog();
            mLocuserlog.l_user_id = UserInfo.UserId;
            mLocuserlog.l_logtype_id = 7;
            mLocuserlog.l_ip = RequestHelper.GetClientIp();
            mLocuserlog.l_location = "";
            mLocuserlog.l_info = message;
            mLocuserlog.l_sms = summary;
            mLocuserlog.l_time = DateTime.Now;
            bllLocuserlog.Add(mLocuserlog);
        }
    }
}