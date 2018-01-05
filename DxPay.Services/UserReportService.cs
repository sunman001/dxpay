using System;
using System.Collections.Generic;
using DxPay.Infrastructure;
using DxPay.Repositories;
using JMP.MDL;
using System.Collections;
using JMP.TOOL;
using TOOL.EnumUtil;

namespace DxPay.Services
{
    public class UserReportService :  IUserReportService
    {
        private readonly IUserReportRepository _repository;
        public UserReportService(IUserReportRepository repository) 
        {
            _repository = repository;
        }
        /// <summary>
        /// 查询今日报表(代理商)
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="orderBy"></param>
        /// <param name="rtype"></param>
        /// <param name="stime"></param>
        /// <param name="etime"></param>
        /// <param name="time"></param>
        /// <param name="num"></param>
        /// <param name="types"></param>
        /// <param name="searchKey"></param>
        /// <param name="sort"></param>
        /// <param name="searchTotal"></param>
        /// <param name="parameters"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IPagedList<jmp_user_report> FindPagedListByAgentToday(int userid,  string orderBy, string rtype, string stime, string etime, string time, int num, string types, string searchKey, int sort, string searchTotal, object parameters = null, int pageIndex = 0, int pageSize = 20)
        {
            var where = new List<string>();
            orderBy = "order by ";
            if (time != "")
            {
                stime = JMP.TOOL.DESEncrypt.Decrypt(time);
                etime = JMP.TOOL.DESEncrypt.Decrypt(time);
            }
            else
            {
                switch (num)
                {
                    case 2:
                        stime = DateTime.Now.ToString("yyyy-MM-01");
                        etime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                        break;
                    case 3:
                        stime = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01");
                        etime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddDays(-1).ToString("yyyy-MM-dd");
                        break;
                }
            }
        
            //按日期倒序展示
            if (!string.IsNullOrEmpty(searchTotal))
            {
                if (searchTotal == "0")
                    orderBy += "a_equipment ";
                else if (searchTotal == "1")
                    orderBy += "a_success ";
                else if (searchTotal == "2")
                    orderBy += "a_notpay ";
                else if (searchTotal == "3")
                    orderBy += "a_alipay ";
                else if (searchTotal == "4")
                    orderBy += "a_wechat ";
                else
                    orderBy += "a_time ";
            }
            else
            {
                orderBy += rtype == "total" ? "a_time  " : "a_appid  ";
            }
            orderBy += (sort == 1 ? "desc" : "asc");
           
                where.Add("b.relation_type="+(int)Relationtype.Agent+ "");
                where.Add(" b.relation_person_id='" + userid + "'");
            
          
            where.Add("a_datetime>='" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00" + "'");
            where.Add("a_datetime <= '" + DateTime.Now.ToString("yyyy-MM-dd") + " 23:59" + "'");
            if (!string.IsNullOrEmpty(types))
            {
                if (!string.IsNullOrEmpty(searchKey))
                {
                    switch (types)
                    {
                        case "0":
                            //where += " and temp.t_appkey like '%" + searchKey + "%' ";
                            break;
                        case "1":
                            where.Add(" a.a_appname like '%" + searchKey + "%'");
                            break;
                        case "2":
                            where.Add("d b.u_realname like '%" + searchKey + "%'");
                            break;
                    }
                }
            }
            return _repository.FindPagedListByToday(string.Join(" AND ", where),orderBy,null ,pageIndex,pageSize);

        }

       

        /// <summary>
        /// 查询总的报表（代理商）
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="calsstype"></param>
        /// <param name="orderBy"></param>
        /// <param name="rtype"></param>
        /// <param name="stime"></param>
        /// <param name="etime"></param>
        /// <param name="time"></param>
        /// <param name="num"></param>
        /// <param name="types"></param>
        /// <param name="searchKey"></param>
        /// <param name="sort"></param>
        /// <param name="searchTotal"></param>
        /// <param name="parameters"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>

        public IPagedList<jmp_user_report> FindPagedListByAgentTotal(int userid,  string orderBy, string rtype, string stime, string etime, string time, int num, string types, string searchKey, int sort, string searchTotal, object parameters = null, int pageIndex = 0, int pageSize = 20)
        {
            var where = new List<string>();
            orderBy = "order by ";
            if (time != "")
            {
                stime = JMP.TOOL.DESEncrypt.Decrypt(time);
                etime = JMP.TOOL.DESEncrypt.Decrypt(time);
            }
            else
            {
                switch (num)
                {
                    case 2:
                        stime = DateTime.Now.ToString("yyyy-MM-01");
                        etime = DateTime.Now.ToString("yyyy-MM-dd");
                        break;
                    case 3:
                        stime = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01");
                        etime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).ToString("yyyy-MM-dd");
                        break;
                }
            }
           
            //按日期倒序展示
            if (!string.IsNullOrEmpty(searchTotal))
            {
                if (searchTotal == "0")
                    orderBy += "a_equipment ";
                else if (searchTotal == "1")
                    orderBy += "a_success ";
                else if (searchTotal == "2")
                    orderBy += "a_notpay ";
                else if (searchTotal == "3")
                    orderBy += "a_alipay ";
                else if (searchTotal == "4")
                    orderBy += "a_wechat ";
                else
                    orderBy += "a_time ";
            }
            else
            {
                orderBy += rtype == "total" ? "a_time  " : "a_appid  ";
            }
            orderBy += (sort == 1 ? "desc" : "asc");
            if (!string.IsNullOrEmpty(types))
            {
                if (!string.IsNullOrEmpty(searchKey))
                {
                    switch (types)
                    {
                        case "0":
                            //where += " and r_app_key like '%" + searchKey + "%'";
                            break;
                        case "1":
                            where.Add("a.a_appname like '%" + searchKey + "%'");
                            break;
                        case "2":
                            where.Add("b.u_realname like '%" + searchKey + "%'");
                            break;
                    }
                }
            }
            where.Add("a_time>='" + stime + "'");
            where.Add("a_time<='" + etime + "'");
            where.Add("b.relation_type=" + (int)Relationtype.Agent + "");
            where.Add(" b.relation_person_id='" + userid + "'");

            return _repository.FindPagedListByTotal(string.Join(" AND ", where), orderBy, null, pageIndex, pageSize);
        }

        public IPagedList<jmp_user_report> FindPagedListByBpToday(int userid,  int relationtype,string orderBy, string rtype, string stime, string etime, string time, int num, string types, string searchKey, int sort, string searchTotal, object parameters = null, int pageIndex = 0, int pageSize = 20)
        {
            var where = new List<string>();
            string bpWhere = "";
            string agentWhere = "";
            orderBy = "order by ";
            if (time != "")
            {
                stime = JMP.TOOL.DESEncrypt.Decrypt(time);
                etime = JMP.TOOL.DESEncrypt.Decrypt(time);
            }
            else
            {
                switch (num)
                {
                    case 2:
                        stime = DateTime.Now.ToString("yyyy-MM-01");
                        etime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                        break;
                    case 3:
                        stime = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01");
                        etime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddDays(-1).ToString("yyyy-MM-dd");
                        break;
                }
            }

            //按日期倒序展示
            if (!string.IsNullOrEmpty(searchTotal))
            {
                switch (searchTotal)
                {
                    case "0":
                        orderBy += "a_equipment ";
                        break;
                    case "1":
                        orderBy += "a_success ";
                        break;
                    case "2":
                        orderBy += "a_notpay ";
                        break;
                    case "3":
                        orderBy += "a_curr ";
                        break;
                    case "5":
                        orderBy += "a_time ";
                        break;
                    case "6":
                        orderBy += "u_realname ";
                        break;
                    default:
                        orderBy += "a_time ";
                        break;
                }
            }
            else
            {
                orderBy += rtype == "total" ? "a_time  " : "a_appid  ";
            }
            orderBy += (sort == 1 ? "desc" : "asc");
            where.Add(" a_datetime>='" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00" + "' and a_datetime<='" + DateTime.Now.ToString("yyyy-MM-dd") + " 23:59" + "' ");
            if(relationtype>0)
            {
                where.Add("users.relation_type ="+relationtype+" ");
            }
            if (!string.IsNullOrEmpty(types))
            {
                if (!string.IsNullOrEmpty(searchKey))
                {
                    switch (types)
                    {
                        case "1":
                            where.Add(" a.a_appname like '%" + searchKey + "%'");
                            break;
                        case "2":
                            where.Add("d b.u_realname like '%" + searchKey + "%'");
                            break;
                    }
                }
            }
            bpWhere += "relation_type=" + (int)Relationtype.Bp + " and relation_person_id=" + userid + " ";
            agentWhere = "a.relation_type=" + (int)Relationtype.Agent + " and c.OwnerId=" + userid + "";
            return _repository.FindPagedListByBpToday(string.Join(" AND ", where), bpWhere,agentWhere,  orderBy, null, pageIndex, pageSize);
        }

        public IPagedList<jmp_user_report> FindPagedListByBpTotal(int userid, int relationtype, string orderBy, string rtype, string stime, string etime, string time, int num, string types, string searchKey, int sort, string searchTotal, object parameters = null, int pageIndex = 0, int pageSize = 20)
        {
            var where = new List<string>();
            string bpWhere = "";
            string agentWhere = "";
            orderBy = "order by ";
            if (time != "")
            {
                stime = JMP.TOOL.DESEncrypt.Decrypt(time);
                etime = JMP.TOOL.DESEncrypt.Decrypt(time);
            }
            else
            {
                switch (num)
                {
                    case 2:
                        stime = DateTime.Now.ToString("yyyy-MM-01");
                        etime = DateTime.Now.ToString("yyyy-MM-dd");
                        break;
                    case 3:
                        stime = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01");
                        etime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).ToString("yyyy-MM-dd");
                        break;
                }
            }

            //按日期倒序展示
            if (!string.IsNullOrEmpty(searchTotal))
            {

                switch (searchTotal)
                {
                    case "0":
                      orderBy += "a_equipment ";
                        break;
                    case "1":
                        orderBy += "a_success ";
                        break;
                    case "2":
                        orderBy += "a_notpay ";
                        break;
                    case "3":
                        orderBy += "a_curr ";
                        break;
                    case "5":
                        orderBy += "a_time ";
                        break;
                    case "6":
                        orderBy += "u_realname ";
                        break;
                    default:
                        orderBy += "a_time ";
                        break;
                }
            }
            else
            {
                orderBy += rtype == "total" ? "a_time  " : "a_appid  ";
            }
            orderBy += (sort == 1 ? "desc" : "asc");
            if (!string.IsNullOrEmpty(types))
            {
                if (!string.IsNullOrEmpty(searchKey))
                {
                    switch (types)
                    {
                        case "1":
                            where.Add("a_appname like '%" + searchKey + "%'");
                            break;
                        case "2":
                            where.Add("u_realname like '%" + searchKey + "%'");
                            break;
                    }
                }
            }
            if (relationtype > 0)
            {
                where.Add("users.relation_type =" + relationtype + " ");
            }
            where.Add("a_time>='" + stime + "'");
            where.Add("a_time<='" + etime + "'");
       
            //where.Add("a_datetime>='" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00" + "'");
            //where.Add("a_datetime <= '" + DateTime.Now.ToString("yyyy-MM-dd") + " 23:59" + "'");
            bpWhere += "relation_type=" + (int)Relationtype.Bp + " and relation_person_id=" + userid + " ";
            agentWhere = "a.relation_type=" + (int)Relationtype.Agent + " and c.OwnerId=" + userid + "";
            return _repository.FindPagedListByBpTotal(string.Join(" AND ", where),bpWhere, agentWhere,  orderBy, null, pageIndex, pageSize);
        }
    }
}

