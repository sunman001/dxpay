using System;
using JMP.TOOL;

namespace WEB.Util.RateLogger
{
    /// <summary>
    /// 操作日志类
    /// </summary>
    public class RateOperateLogger : IRateLog
    {
        /// <summary>
        /// 操作日志写入方法
        /// </summary>
        /// <param name="summary">日志摘要</param>
        /// <param name="message">日志详情</param>
        public void Log(string summary, string message)
        {
            var mOperationLog= new JMP.MDL.CoOperationLog();
            var bllOperationLog = new JMP.BLL.CoOperationLog();
            mOperationLog.CreatedById = UserInfo.UserId;
            mOperationLog.IpAddress = RequestHelper.GetClientIp();
            mOperationLog.CreatedByName = UserInfo.UserName;
            mOperationLog.Message = message;
            mOperationLog.Summary = summary;
            mOperationLog.CreatedOn = DateTime.Now;
            bllOperationLog.Add(mOperationLog);
        }
    }
}