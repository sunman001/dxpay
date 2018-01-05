using System;
using System.Collections.Generic;
using System.Linq;
using JMP.TOOL;

namespace JmPay.PayChannelMonitor.Util.LogManager
{
    /// <summary>
    /// 数据日志类
    /// </summary>
    public class DbLogger : ILogger
    {
        public void Write(string content)
        {
            AddLocLog.AddLog(1, 5, "", "核查监控程序", content);
        }

        public void Write(string summary,string content)
        {
            AddLocLog.AddLog(1, 5, "", summary , content);
        }

        public void Write(List<string> lines)
        {
            var logs = lines.Where(x=>x.Length>0).Select(x => new JMP.MDL.jmp_userlog
            {
                l_info = x,
                l_ip = "核查监控程序",
                l_location = "",
                l_logtype_id = 5,
                l_sms = "核查监控程序",
                l_user_id = 1,
                l_time=DateTime.Now
            }).ToList();
            AddLocLog.AddUserLogBulk(logs);
        }
    }
}
