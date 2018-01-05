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
    public class OrderService : GenericService<jmp_order>, IOrderService
    {
        private readonly IOrderRepository _repository;
        public OrderService(IOrderRepository repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 订单明细（商务）
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="orderBy"></param>
        /// <param name="searchType"></param>
        /// <param name="searchname"></param>
        /// <param name="stime"></param>
        /// <param name="etime"></param>
        /// <param name="paymode"></param>
        /// <param name="paymentstate"></param>
        /// <param name="noticestate"></param>
        /// <param name="parameters"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>

        public IPagedList<jmp_order> FindPagedListByBP(int userid,  int relationtype,string orderBy, int searchType, string searchname, string stime, string etime, int paymode, string paymentstate, string noticestate, object parameters = null, int pageIndex = 0, int pageSize = 20)
        {
            string sql = "";
            string sql1 = "";
            string TableName = "";//表名
            orderBy = "o_ptime";//排序字段
            ArrayList sjfw = WeekDateTime.WeekMonday(DateTime.Parse(stime), DateTime.Parse(etime));//根据时间返回获取每周周一组装查询语句
           // var where = new List<string>();
            string where = "  ";
            string BpWhere = "";
            string AgentWhere = "";
            for (int i = 0; i < sjfw.Count; i++)
            {
                TableName = "jmp_order_" + DateTime.Parse(sjfw[i].ToString()).ToString("yyyyMMdd");
                sql += " SELECT o_id,o_code,o_bizcode,o_tradeno,o_paymode_id,o_app_id,o_goodsname,o_term_key,o_price,o_payuser,o_ctime,o_ptime,o_state,o_times,o_address,o_noticestate,o_noticetimes,o_privateinfo,o_interface_id,o_showaddress FROM " + TableName + " where 1=1  ";
                if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
                {
                    sql += " and convert(varchar(10),o_ptime,120)>='" + stime + "' and convert(varchar(10),o_ptime,120)<='" + etime + "' ";
                }
                if (paymode > 0)
                {
                    sql += " and o_paymode_id='" + paymode + "' ";
                }
                if (!string.IsNullOrEmpty(paymentstate))
                {
                    sql += " and o_state='" + paymentstate + "' ";
                }
                if (!string.IsNullOrEmpty(noticestate))
                {
                    sql += " and o_noticestate='" + noticestate + "' ";
                }
                sql += "AND EXISTS ( SELECT a_id FROM APP WHERE APP.a_id=o_app_id)";
                sql += "UNION ALL ";
            }
            if (searchType > 0 && !string.IsNullOrEmpty(searchname))
            {
                switch (searchType)
                {
                    case 1:
                        where+=" and ord.o_code='" + searchname + "' ";
                        break;
                    case 2:
                        where+= "and app.a_name='" + searchname + "'";
                        break;
                    case 3:
                        where+= " and ord.o_goodsname='" + searchname + "'";
                        break;
                    case 4:
                        where+= " and ord.o_tradeno= '" + searchname + "' ";
                        break;
                    case 5:
                        where+= " and ord.o_bizcode like '%" + searchname + "%'";
                        break;
                }
            }
            if (relationtype > 0)
            {
                where+=" and users.relation_type = '" + relationtype + "'";
            }
            //查询直客条件
            BpWhere = "  relation_type =" + (int)Relationtype.Bp + " and relation_person_id="+userid+"";
            AgentWhere = "  a.relation_type =" + (int)Relationtype.Agent + " and c.OwnerId=" + userid + "";
            //组装时时表数据
            sql1 = "SELECT o_id,o_code,o_bizcode,o_tradeno,o_paymode_id,o_app_id,o_goodsname,o_term_key,o_price,o_payuser,o_ctime,o_ptime,o_state,o_times,o_address,o_noticestate,o_noticetimes,o_privateinfo,o_interface_id, o_showaddress FROM jmp_order where 1=1  ";
            if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
            {
                sql1 += " and convert(varchar(10),o_ptime,120)>='" + stime + "' and convert(varchar(10),o_ptime,120)<='" + etime + "' ";

            }
            if (paymode > 0)
            {
                sql1 += " and o_paymode_id='" + paymode + "' ";
            }
            if (!string.IsNullOrEmpty(paymentstate))
            {
                sql1 += " and o_state='" + paymentstate + "' ";
            }
            if (!string.IsNullOrEmpty(noticestate))
            {
                sql1 += " and o_noticestate='" + noticestate + "' ";
            }
            sql1 += "AND EXISTS ( SELECT a_id FROM APP WHERE APP.a_id=o_app_id)";
            sql = sql + sql1;
            return _repository.FindPagedListByBP(orderBy,sql,where, BpWhere,AgentWhere, null,pageIndex,pageSize);

        }

        /// <summary>
        /// 订单明细（代理商）
        /// </summary>
        /// <param name="userid">当前登录人ID</param>
        /// <param name="orderBy">排序字段</param>
        /// <param name="searchType">查询类型</param>
        /// <param name="searchname">查询内容</param>
        /// <param name="stime">开始时间</param>
        /// <param name="etime">结束时间</param>
        /// <param name="paymode">支付类型</param>
        /// <param name="paymentstate">状态</param>
        /// <param name="noticestate">状态</param>
        /// <param name="parameters">参数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每月显示数量</param>
        /// <returns></returns>
        public IPagedList<jmp_order> FindPagedListByAgent(int userid, string orderBy, int searchType, string searchname, string stime, string etime, int paymode, string paymentstate, string noticestate, object parameters = null, int pageIndex = 0, int pageSize = 20)
        {
            string sql = "";
            string sql1 = "";
            string TableName = "";//表名
            string agentwhere = "";
            orderBy = "o_ptime";//排序字段
            ArrayList sjfw = WeekDateTime.WeekMonday(DateTime.Parse(stime), DateTime.Parse(etime));//根据时间返回获取每周周一组装查询语句
            string where = "";
            for (int i = 0; i < sjfw.Count; i++)
            {
                TableName = "jmp_order_" + DateTime.Parse(sjfw[i].ToString()).ToString("yyyyMMdd");
                sql += " SELECT o_id,o_code,o_bizcode,o_tradeno,o_paymode_id,o_app_id,o_goodsname,o_term_key,o_price,o_payuser,o_ctime,o_ptime,o_state,o_times,o_address,o_noticestate,o_noticetimes,o_privateinfo,o_interface_id,o_showaddress FROM " + TableName + " where 1=1  and exists(select a_id from app where app.a_id=o_app_id) ";
                if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
                {
                    sql += " and convert(varchar(10),o_ptime,120)>='" + stime + "' and convert(varchar(10),o_ptime,120)<='" + etime + "' ";
                }
                if (paymode > 0)
                {
                    sql += " and o_paymode_id='" + paymode + "' ";
                }
                if (!string.IsNullOrEmpty(paymentstate))
                {
                    sql += " and o_state='" + paymentstate + "' ";
                }
                if (!string.IsNullOrEmpty(noticestate))
                {
                    sql += " and o_noticestate='" + noticestate + "' ";
                }
                
                sql += "UNION ALL ";
            }
            if (searchType > 0 && !string.IsNullOrEmpty(searchname))
            {
                switch (searchType)
                {
                    case 1:
                        where += " and a.o_code='" + searchname + "' ";
                        break;
                    case 2:
                        where += " and  b.a_name='" + searchname + "'   ";
                        break;
                    case 3:
                        where += " and  o_goodsname='" + searchname + "' ";
                        break;
                    case 4:
                        where += " and a.o_tradeno= '" + searchname + "' ";
                        break;
                    case 5:
                        where += " and a.o_bizcode like '%" + searchname + "%' ";
                        break;
                }
            }
            //代理商
            agentwhere = "relation_type =" + (int)Relationtype.Agent + " and relation_person_id='" + UserInfo.UserId + "'";            
            //组装时时表数据
            sql1 = "SELECT o_id,o_code,o_bizcode,o_tradeno,o_paymode_id,o_app_id,o_goodsname,o_term_key,o_price,o_payuser,o_ctime,o_ptime,o_state,o_times,o_address,o_noticestate,o_noticetimes,o_privateinfo,o_interface_id, o_showaddress FROM jmp_order where 1=1  and exists(select a_id from app where app.a_id=o_app_id)";
            if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
            {
                sql1 += " and convert(varchar(10),o_ptime,120)>='" + stime + "' and convert(varchar(10),o_ptime,120)<='" + etime + "' ";

            }
            if (paymode > 0)
            {
                sql1 += " and o_paymode_id='" + paymode + "' ";
            }
            if (!string.IsNullOrEmpty(paymentstate))
            {
                sql1 += " and o_state='" + paymentstate + "' ";
            }
            if (!string.IsNullOrEmpty(noticestate))
            {
                sql1 += " and o_noticestate='" + noticestate + "' ";
            }
            sql = sql + sql1;

            return _repository.FindPagedListBySql(orderBy, sql, where, agentwhere, null, pageIndex, pageSize);
            
        }

    }
}
