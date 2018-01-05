using System;
using JMP.MDL;

namespace JMP.TOOL.Auditor
{
    /// <summary>
    /// 应用异常请求监控器
    /// </summary>
    public class JmpAppRequestAuditor : IAuditor
    {
        private readonly jmp_app_request_audit _appRequestAudit;
        /// <summary>
        /// 应用异常请求监控器构造器
        /// </summary>
        /// <param name="appId">应用ID</param>
        /// <param name="appName">应用名称</param>
        /// <param name="message">错误消息</param>
        public JmpAppRequestAuditor(int appId,string appName,string message)
        {
            try
            {
                _appRequestAudit = new jmp_app_request_audit
                {
                    app_id = appId,
                    app_name = appName,
                    message = message,
                    created_on=DateTime.Now
                };
            }
            catch { }
        }
        public bool Add()
        {
            if (_appRequestAudit == null)
            {
                return false;
            }
            try
            {
                var bll = new JMP.BLL.jmp_app_request_audit();
                bll.Add(_appRequestAudit);
                return true;
            }
            catch(Exception ex)
            {
                AddLocLog.AddLog(0, 3, "", "添加应用异常请求监控器时出错", ex.ToString());
                return false;
            }
        }
    }
}
