/***********聚米支付平台__添加本地管理员日志************/
//描述：
//功能：
//开发者：陶涛
//开发时间: 2016.03.09
/************聚米支付平台__添加本地管理员日志************/

using System;
using System.Collections.Generic;
using JMP.MDL;

namespace JMP.TOOL
{
    public static class AddLocLog
    {
        /// <summary>
        /// 添加本地管理员日志公用方法
        /// </summary>
        /// <param name="l_user_id">用户ID</param>
        /// <param name="l_logtype_id">日志类别：1 注册 2 登录 3 操作</param>
        /// <param name="l_ip">IP地址</param>
        /// <param name="l_sms">简短说明</param>
        /// <param name="l_info">附加信息</param>
        public static void AddLog(int l_user_id, int l_logtype_id, string l_ip, string l_sms, string l_info)
        {
            JMP.MDL.jmp_locuserlog m_locuserlog = new JMP.MDL.jmp_locuserlog();
            JMP.BLL.jmp_locuserlog bll_locuserlog = new JMP.BLL.jmp_locuserlog();
            m_locuserlog.l_user_id = l_user_id;
            m_locuserlog.l_logtype_id = l_logtype_id;
            m_locuserlog.l_ip = l_ip;
            //m_locuserlog.l_location = IPAddress.GetAddressByIp(l_ip);
            m_locuserlog.l_location = "";
            m_locuserlog.l_info = l_info;
            m_locuserlog.l_sms = l_sms;
            m_locuserlog.l_time = DateTime.Now;
            bll_locuserlog.Add(m_locuserlog);
        }

        /// <summary>
        /// 添加开发者日志公用方法
        /// </summary>
        /// <param name="l_user_id">用户ID</param>
        /// <param name="l_logtype_id">日志类别：1 注册 2 登录 3 操作</param>
        /// <param name="l_ip">IP地址</param>
        /// <param name="l_sms">简短说明</param>
        /// <param name="l_info">附加信息</param>
        public static void AddUserLog(int l_user_id, int l_logtype_id, string l_ip, string l_sms, string l_info)
        {
            var m_userlog = new jmp_userlog();
            var bll_userlog = new BLL.jmp_userlog();
            m_userlog.l_user_id = l_user_id;
            m_userlog.l_logtype_id = l_logtype_id;
            m_userlog.l_ip = l_ip;
            //m_userlog.l_location = IPAddress.GetAddressByIp(l_ip);
            m_userlog.l_location ="";
            m_userlog.l_sms = l_sms;
            m_userlog.l_info = l_info;
            m_userlog.l_time = DateTime.Now;
            bll_userlog.Add(m_userlog);
        }

        /// <summary>
        /// 批量写入日志数据(管理员日志)
        /// </summary>
        /// <param name="userLogs">批量日志集合</param>
        public static void AddUserLogBulk(List<jmp_userlog> userLogs)
        {
            var _bll = new BLL.BllCommonQuery();
            _bll.BulkInsert("jmp_locuserlog", userLogs);
        }
    }
}
