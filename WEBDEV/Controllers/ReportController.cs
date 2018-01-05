/************聚米支付平台__开发者前端报表************/
//描述：开发者前端报表
//功能：开发者前端报表
//开发者：谭玉科
//开发时间: 2016.05.06
/************聚米支付平台__开发者前端报表************/
using JMP.TOOL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using WEBDEV.Extensions;
using WEBDEV.Util.Logger;

namespace WEBDEV.Controllers
{
    /// <summary>
    /// 类名：ReportController
    /// 功能：开发者前端报表
    /// 详细：开发者前端报表
    /// 修改日期：2016.05.25
    /// </summary>
    public class ReportController : Controller
    {
        JMP.BLL.jmp_terminal bll_ter = new JMP.BLL.jmp_terminal();
        JMP.BLL.jmp_order bll_order = new JMP.BLL.jmp_order();
        JMP.BLL.jmp_user_report bll_report = new JMP.BLL.jmp_user_report();
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;

        /// <summary>
        /// 实时监控（页面）
        /// </summary>
        /// <returns></returns>
        public ActionResult RealTime()
        {
            return View();
        }

        /// <summary>
        /// 实时监控（报表）
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public string BuildChart(string days, string appid)
        {
            //默认查询当天
            days = !string.IsNullOrEmpty(days) ? days : "0";
            #region 查询数据
            //查询日期
            string sDate = string.Empty;
            switch (days)
            {
                case "0":
                    sDate = DateTime.Now.ToString("yyyy-MM-dd");
                    break;
                case "1":
                    sDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    break;
                case "2":
                    sDate = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");
                    break;
            }
            string tName = GetOrderTableName(sDate);
            JMP.BLL.jmp_appcount bll = new JMP.BLL.jmp_appcount();
            DataSet dsAverage = new DataSet();
            DataTable dt = bll.GetListDay(sDate,UserInfo.UserId);
            dsAverage = bll.GetListAverage(DateTime.Now.AddDays(-4).ToString("yyyy-MM-dd"), DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), "1", UserInfo.UserId, 0);
            #endregion
            string htmls = string.Empty;
            if (dt.Rows.Count > 0 || dsAverage.Tables[0].Rows.Count > 0)
            {
                #region 查询有数据时才构造json数据
                htmls += "{\"chart\":{\"caption\":\"\",\"numberprefix\":\"\",\"plotgradientcolor\":\"\",\"bgcolor\":\"ffffff\",\"showalternatehgridcolor\":\"0\",\"divlinecolor\":\"cccccc\",\"showvalues\":\"0\",\"showcanvasborder\":\"0\",\"canvasborderalpha\":\"0\",\"canvasbordercolor\":\"cccccc\",\"canvasborderthickness\":\"1\",\"yaxismaxvalue\":\"\",\"captionpadding\":\"50\",\"linethickness\":\"3\",\"yaxisvaluespadding\":\"30\",\"legendshadow\":\"0\",\"legendborderalpha\":\"0\",\"palettecolors\":\"#f8bd19,#008ee4,#33bdda,#e44a00,#6baa01,#583e78\",\"showborder\":\"0\",\"toolTipSepChar\":\"&nbsp;/&nbsp;\",\"formatNumberScale\":\"0\"},";

                htmls += "\"categories\": [";
                htmls += "{";
                htmls += "\"category\": [";
                //构建横坐标（每小时），今天截止当前时间的小时
                int len = 24;
                if (days == "0")
                {
                    len = int.Parse(DateTime.Now.ToString("HH")) + 1;
                }
                for (int i = 0; i < len; i++)
                {
                    htmls += "{\"label\": \"" + i + "\"},";
                }

                htmls = htmls.TrimEnd(',');
                htmls += "]";
                htmls += "}";
                htmls += "],";
                htmls += "\"dataset\": [";
                htmls += "{";
                //htmls += "\"seriesName\": \"交易额\",";
                htmls += "\"seriesName\": " + (days == "3" ? "\"今天\"," : "\"前三天平均交易量\",");
                htmls += "\"data\": [";
                string jyuser = "{\"seriesName\": \"交易用户数\",\"data\": [";
                string aprr = "{\"seriesName\":\"ARPPU\",\"data\": [";
                //根据每小时组装数据
                for (int i = 0; i <= len; i++)
                {
                    bool flag = true;
                    foreach (DataRow dr in dt.Rows)
                    {
                        int hours = int.Parse(DateTime.Parse(dr["a_datetime"].ToString()).ToString("HH"));
                        if (i == hours)
                        {
                            flag = false;
                            decimal jye = !string.IsNullOrEmpty(dr["a_curr"].ToString()) ? decimal.Parse(dr["a_curr"].ToString()) : 0;
                            string sJye = jye.ToString("f2");
                            if (days == "3")
                            {
                                htmls += "{\"value\": \"" + sJye + "\",\"toolText\": \"时段交易趋势" + i + "时：交易额" + sJye + "\"},";
                            }
                            decimal jyyhs = !string.IsNullOrEmpty(dr["a_success"].ToString()) ? decimal.Parse(dr["a_success"].ToString()) : 0;
                            string sJyyhs = jyyhs.ToString("f0");
                            jyuser += "{\"value\": \"" + sJyyhs + "\",\"toolText\": \"时段交易趋势" + i + "时：交易用户数" + sJyyhs + "\"},";
                            decimal arppu = jyyhs == 0 ? 0 : jye / jyyhs;
                            string sArppu = arppu.ToString("f2");
                            aprr += "{\"value\": \"" + sArppu + "\",\"toolText\": \"时段交易趋势" + i + "时：ARPPU " + sArppu + "\"},";
                            break;
                        }
                    }
                    if (flag)
                    {
                        if (days == "3")
                        {
                            htmls += "{\"value\": \"0\",\"toolText\": \"时段交易趋势" + i + "时：交易额0\"},";
                        }
                        jyuser += "{\"value\": \"0\",\"toolText\": \"时段交易趋势" + i + "时：交易用户数0\"},";
                        aprr += "{\"value\": \"0\",\"toolText\": \"时段交易趋势" + i + "时：ARPPU 0\"},";
                    }
                }
                #region 组装前三天平均交易量
                if (days != "3")
                {
                    //根据每小时组装数据
                    for (int i = 0; i < len; i++)
                    {
                        bool curr = true;
                        foreach (DataRow dr in dsAverage.Tables[0].Rows)
                        {
                            int hours = int.Parse(dr["a_datetime"].ToString());
                            if (i == hours)
                            {
                                curr = false;
                                htmls += "{\"value\": \"" + decimal.Parse(dr["a_success"].ToString()).ToString("f0") + "\",";
                                htmls += "\"toolText\": \"" + (days == "3" ? "支付量走势" : "交易走势") + i + "时：" + (days == "3" ? "今天" : "平均交易量") + decimal.Parse(dr["a_success"].ToString()).ToString("f0") + "\"},";
                                break;
                            }
                        }
                        if (curr)
                        {
                            htmls += "{\"value\": \"0\",";
                            htmls += "\"toolText\": \"" + (days == "3" ? "支付量走势" : "交易走势") + i + "时：" + (days == "3" ? "今天" : "平均交易量") + "0\"},";
                        }
                    }
                }
                #endregion
                htmls = htmls.TrimEnd(',');
                htmls += "]";
                htmls += "},";
                jyuser = jyuser.TrimEnd(',');
                jyuser += "]";
                jyuser += "},";
                aprr = aprr.TrimEnd(',');
                aprr += "]";
                aprr += "}";
                htmls = htmls + jyuser + aprr;
                htmls += "]";
                htmls += "}";
                #endregion
            }
            else
            {
                htmls = "0";
            }
            return htmls;
        }

        /// <summary>
        /// 获取订单表名
        /// </summary>
        /// <param name="t_time">日期</param>
        /// <returns></returns>
        private string GetOrderTableName(string t_time)
        {
            string tname = string.Empty;
            ArrayList list = JMP.TOOL.WeekDateTime.WeekMonday(DateTime.Parse(t_time), DateTime.Parse(t_time));
            if (list.Count == 1)
                tname = "jmp_order_" + DateTime.Parse(list[0].ToString()).ToString("yyyyMMdd");
            else
            {
                DateTime t_date = DateTime.Parse(t_time).AddDays(-7);
                list = JMP.TOOL.WeekDateTime.WeekMonday(t_date, t_date);
                tname = "jmp_order_" + DateTime.Parse(list[0].ToString()).ToString("yyyyMMdd");
            }
            return tname;
        }

        /// <summary>
        /// 订单列表
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderList()
        {
            #region 获取用户实名认证状态信息

            JMP.BLL.jmp_user sm_bll = new JMP.BLL.jmp_user();
            JMP.MDL.jmp_user sm_model = new JMP.MDL.jmp_user();

            int u_ids = UserInfo.Uid;

            //查询登录信息
            sm_model = sm_bll.GetModel(u_ids);

            ViewBag.auditstate = sm_model.u_auditstate;
            ViewBag.linkEmail = sm_model.u_email;

            #endregion

            #region 获取信息

            JMP.BLL.jmp_paymode paymodebll = new JMP.BLL.jmp_paymode();
            List<JMP.MDL.jmp_paymode> paymodeList = paymodebll.GetModelList("1=1 and p_state='1' ");//支付类型
            ViewBag.paymodeList = paymodeList;
            #endregion
            #region 查询
            //string sql = " select o_id,o_code,o_bizcode,o_tradeno,o_paymode_id,o_app_id,o_goods_id,o_term_key,o_price,o_payuser,o_ctime,o_ptime,o_state,o_times,o_address,o_noticestate,o_noticetimes,o_privateinfo,o_interface_id,o_showaddress,b.a_key,b.a_name,c.p_name,d.g_name from (  ";//组装查询条件
            string sql = "";
            string sql1 = "";
            string TableName = "";//表名
            string order = "o_ptime";//排序字段
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchType = string.IsNullOrEmpty(Request["searchType"]) ? 0 : Int32.Parse(Request["searchType"]); //查询条件选择
            string searchname = string.IsNullOrEmpty(Request["searchname"]) ? "" : Request["searchname"];//查询类容
            string stime = string.IsNullOrEmpty(Request["stime"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["stime"];//开始时间
            string etime = string.IsNullOrEmpty(Request["etime"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["etime"];//结束时间
            int paymode = string.IsNullOrEmpty(Request["paymode"]) ? 0 : Int32.Parse(Request["paymode"]);//支付类型
            string paymentstate = string.IsNullOrEmpty(Request["paymentstate"]) ? "1" : Request["paymentstate"];//支付状态
            string noticestate = string.IsNullOrEmpty(Request["noticestate"]) ? "" : Request["noticestate"];//通知状态
            ArrayList sjfw = JMP.TOOL.WeekDateTime.WeekMonday(DateTime.Parse(stime), DateTime.Parse(etime));//根据时间返回获取每周周一组装查询语句
            string where = "where 1=1";
            for (int i = 0; i < sjfw.Count; i++)
            {
                TableName = "jmp_order_" + DateTime.Parse(sjfw[i].ToString()).ToString("yyyyMMdd");
                // TableName = "jmp_order_20161107";
                sql += " SELECT o_id,o_code,o_bizcode,o_tradeno,o_paymode_id,o_app_id,o_goodsname,o_term_key,o_price,o_payuser,o_ctime,o_ptime,o_state,o_times,o_address,o_noticestate,o_noticetimes,o_privateinfo,o_interface_id,o_showaddress FROM " + TableName + " where 1=1 ";
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
                sql += "    UNION ALL ";
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
                        where += " and  a.o_goodsname='" + searchname + "' ";
                        break;
                    case 4:
                        where += " and a.o_tradeno= '" + searchname + "' ";
                        break;
                    case 5:
                        where += " and a.o_bizcode like '%" + searchname + "%' ";
                        break;
                }
            }
            where += "and b.a_user_id='" + UserInfo.UserId + "'";
            //sql = sql.Remove(sql.Length - 10);//去掉最后一个UNION ALL
            //sql += " ) a   left join jmp_app  b on  a.o_app_id=b.a_id left join jmp_paymode c on c.p_id=a.o_paymode_id  left join jmp_goods d on d.g_id=a.o_goods_id and d.g_app_id=b.a_id and d.g_app_id=a.o_app_id where 1=1  and  " + where;
            //组装时时表数据
            sql1 = "SELECT o_id,o_code,o_bizcode,o_tradeno,o_paymode_id,o_app_id,o_goodsname,o_term_key,o_price,o_payuser,o_ctime,o_ptime,o_state,o_times,o_address,o_noticestate,o_noticetimes,o_privateinfo,o_interface_id, o_showaddress FROM jmp_order where 1=1";
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
            ViewBag.searchname = searchname;
            ViewBag.searchType = searchType;
            ViewBag.stime = stime;
            ViewBag.etime = etime;
            ViewBag.paymode = paymode;
            ViewBag.paymentstate = paymentstate;
            ViewBag.noticestate = noticestate;
            List<JMP.MDL.jmp_order> list = new List<JMP.MDL.jmp_order>();
            JMP.BLL.jmp_order orderbll = new JMP.BLL.jmp_order();
            list = orderbll.SelectPager(where, sql, order, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            #endregion
            return View();
        }
        /// <summary>
        /// 导出订单列表
        /// </summary>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult DcDev()
        {
            #region 查询
            string sql = " select a.*,b.a_key,b.a_name,c.p_name from (  ";//组装查询条件
            string sql1 = "";
            string TableName = "";//表名
            string order = " order by o_ctime desc";//排序字段
            int searchType = string.IsNullOrEmpty(Request["searchType"]) ? 0 : Int32.Parse(Request["searchType"]); //查询条件选择
            string searchname = string.IsNullOrEmpty(Request["searchname"]) ? "" : Request["searchname"];//查询类容
            string stime = string.IsNullOrEmpty(Request["stime"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["stime"];//开始时间
            string etime = string.IsNullOrEmpty(Request["etime"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["etime"];//结束时间
            int paymode = string.IsNullOrEmpty(Request["paymode"]) ? 0 : Int32.Parse(Request["paymode"]);//支付类型
            string paymentstate = string.IsNullOrEmpty(Request["paymentstate"]) ? "1" : Request["paymentstate"];//支付状态
            string noticestate = string.IsNullOrEmpty(Request["noticestate"]) ? "" : Request["noticestate"];//通知状态
            ArrayList sjfw = JMP.TOOL.WeekDateTime.WeekMonday(DateTime.Parse(stime), DateTime.Parse(etime));//根据时间返回获取每周周一组装查询语句
            string where = "";
            for (int i = 0; i < sjfw.Count; i++)
            {
                TableName = "jmp_order_" + DateTime.Parse(sjfw[i].ToString()).ToString("yyyyMMdd");
                sql += " SELECT o_id,o_code,o_bizcode,o_tradeno,o_paymode_id,o_app_id,o_goodsname,o_term_key,o_price,o_payuser,o_ctime,o_ptime,o_state,o_times,o_address,o_noticestate,o_noticetimes,o_privateinfo,o_interface_id, o_showaddress FROM " + TableName + " where 1=1 ";
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
                sql += "    UNION ALL ";
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
                        where += " and  d.g_name='" + searchname + "' ";
                        break;
                    case 4:
                        where += " and a.o_tradeno= '" + searchname + "' ";
                        break;
                    case 5:
                        where += " and a.o_bizcode like '%" + searchname + "%' ";
                        break;
                }
            }
            // sql = sql.Remove(sql.Length - 10);//去掉最后一个UNION ALL
            sql1 = "SELECT o_id,o_code,o_bizcode,o_tradeno,o_paymode_id,o_app_id,o_goodsname,o_term_key,o_price,o_payuser,o_ctime,o_ptime,o_state,o_times,o_address,o_noticestate,o_noticetimes,o_privateinfo,o_interface_id, o_showaddress FROM jmp_order where 1=1";
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
            sql += " ) a   left join jmp_app  b on  a.o_app_id=b.a_id left join jmp_paymode c on c.p_id=a.o_paymode_id  where 1=1  and b.a_user_id='" + UserInfo.UserId + "'  " + where + order;
            List<JMP.MDL.jmp_order> list = new List<JMP.MDL.jmp_order>();
            JMP.BLL.jmp_order orderbll = new JMP.BLL.jmp_order();
            list = orderbll.DcSelectList(sql);
            var lst = list.Select(x => new
            {
                x.o_code,
                x.a_name,
                x.o_goodsname,
                x.o_bizcode,
                x.o_tradeno,
                x.p_name,
                x.o_price,
                o_state = x.o_state.ConvertPayState(),
                o_ctime = x.o_ctime.ToString("yyyy-MM-dd HH:mm:ss"),
                o_ptime = x.o_ptime.ToString("yyyy-MM-dd HH:mm:ss"),
                o_times = x.o_noticestate == 0 ? "--" : x.o_times.ToString(),
                o_noticestate = x.o_noticestate.ConvertNoticeState(x.o_state),
                o_noticetimes = x.o_noticestate != 0 ? x.o_noticetimes.ToString("yyyy-MM-dd HH:mm:ss") : "--",
                o_privateinfo = x.o_privateinfo
            });
            var caption = "订单列表";
            byte[] fileBytes;
            //命名导出表格的StringBuilder变量
            using (var pck = new ExcelPackage())
            {
                var ws = pck.Workbook.Worksheets.Add(caption);
                ws.Cells["A1"].LoadFromCollection(lst, false);
                ws.InsertRow(1, 1);
                ws.Cells["A1"].Value = "订单编号";
                ws.Cells["B1"].Value = "应用名称";
                ws.Cells["C1"].Value = "商品名称";
                ws.Cells["D1"].Value = "商家订单号";
                ws.Cells["E1"].Value = "支付流水号";
                ws.Cells["F1"].Value = "支付类型";
                ws.Cells["G1"].Value = "支付金额";
                ws.Cells["H1"].Value = "支付状态";
                ws.Cells["I1"].Value = "创建时间";
                ws.Cells["J1"].Value = "支付时间";
                ws.Cells["K1"].Value = "通知次数";
                ws.Cells["L1"].Value = "通知状态";
                ws.Cells["M1"].Value = "通知时间";
                ws.Cells["N1"].Value = "私有信息";
                fileBytes = pck.GetAsByteArray();

            }
            Session["daochu"] = DateTime.Now;
            string fileName = "订单列表" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            #endregion
            return File(fileBytes, "application/vnd.ms-excel", fileName);
        }
        /// <summary>
        /// 订单重发通知
        /// </summary>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        [HttpPost]
        public JsonResult Orderrewire()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            string ordercode = string.IsNullOrEmpty(Request["code"]) ? "" : Request["code"];
            string ptime = string.IsNullOrEmpty(Request["ptime"]) ? "" : Request["ptime"];
            bool sess = Convert.ToDateTime(Session["sendtime_" + ordercode]) > System.DateTime.Now.AddMinutes(-1) ? true : false;
            if (!string.IsNullOrEmpty(ordercode) && !string.IsNullOrEmpty(ptime))
            {
                if (DateTime.Parse(DateTime.Parse(ptime).ToString("yyyy-MM-dd")) > DateTime.Parse(DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd")))
                {
                    if (sess)
                    {
                        retJson = new { success = 0, msg = "请间隔一分钟，再次发送！" };
                        return Json(retJson);
                    }
                    else
                    {
                        JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
                        JMP.MDL.jmp_order morder = new JMP.MDL.jmp_order();
                        string tabalename = "dbo.jmp_order_" + JMP.TOOL.WeekDateTime.GetWeekFirstDayMon(DateTime.Parse(ptime)).ToString("yyyyMMdd");
                        morder = bll.SelectOrder(ordercode, tabalename);
                        if (morder != null)
                        {
                            if (morder.o_times > 8 && morder.o_times < 12)
                            {
                                JMP.MDL.jmp_queuelist quli = new JMP.MDL.jmp_queuelist();
                                JMP.BLL.jmp_queuelist bllq = new JMP.BLL.jmp_queuelist();
                                quli.q_address = morder.o_address;
                                quli.q_sign = new JMP.BLL.jmp_app().GetModel(morder.o_app_id).a_secretkey;
                                quli.q_noticestate = 0;
                                quli.q_times = morder.o_times;
                                quli.q_noticetimes = DateTime.Now;
                                quli.q_tablename = tabalename;
                                quli.q_o_id = morder.o_id;
                                quli.trade_type = Int32.Parse(morder.o_paymode_id);
                                quli.trade_time = morder.o_ptime;
                                quli.trade_price = morder.o_price;
                                quli.trade_paycode = morder.o_tradeno;
                                quli.trade_code = morder.o_code;
                                quli.trade_no = morder.o_bizcode;
                                quli.q_privateinfo = morder.o_privateinfo;
                                quli.q_uersid = UserInfo.UserId;
                                int cg = bllq.Add(quli);
                                if (cg > 0)
                                {
                                    Session["sendtime_" + morder.o_code] = System.DateTime.Now;
                                    retJson = new { success = 1, msg = "已重发通知！手动通知次数剩余：" + (11 - morder.o_times) + "次" };
                                }
                                else
                                {
                                    AddLocLog.AddLog(1, 4, Request.UserHostAddress, "管理平台手动重发通知失败", "订单号：" + morder.o_code + ",表名：" + tabalename);//写入报错日
                                    retJson = new { success = 0, msg = "操作失败！" };
                                }
                            }
                            else
                            {
                                retJson = new { success = 0, msg = "手动通知无效！" };
                            }

                        }

                    }
                }
                else
                {
                    retJson = new { success = 0, msg = "只能重发三天以内的数据！" };
                }

            }
            else
            {
                retJson = new { success = 0, msg = "数据异常！" };
            }
            return Json(retJson);
        }
        /// <summary>
        /// 判断导出session是否有效
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public int pdDcSession()
        {
            int sess = (Session["daochu"] == null || Convert.ToDateTime(Session["daochu"]) < DateTime.Now.AddMinutes(-1)) ? 0 : 1;
            return sess;
        }
        /// <summary>
        /// 应用报表
        /// </summary>
        /// <returns></returns>
        public ActionResult AppReport(string rtype)
        {

            #region 获取用户实名认证状态信息

            JMP.BLL.jmp_user sm_bll = new JMP.BLL.jmp_user();
            JMP.MDL.jmp_user sm_model = new JMP.MDL.jmp_user();

            int u_ids = UserInfo.Uid;

            //查询登录信息
            sm_model = sm_bll.GetModel(u_ids);

            ViewBag.auditstate = sm_model.u_auditstate;
            ViewBag.linkEmail = sm_model.u_email;

            #endregion

            rtype = !string.IsNullOrEmpty(rtype) ? rtype : "total";

            //日期
            string stime = string.IsNullOrEmpty(Request["s_begin"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_begin"];
            string etime = string.IsNullOrEmpty(Request["s_end"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_end"];

            //首页跳转标识
            int num = string.IsNullOrEmpty(Request["start"]) ? -1 : int.Parse(Request["start"]);
            //账单管理跳转标识
            string time = string.IsNullOrEmpty(Request["time"]) ? "" : Request["time"];
            if (time != "")
            {
                stime = JMP.TOOL.DESEncrypt.Decrypt(time);
                etime = JMP.TOOL.DESEncrypt.Decrypt(time);
            }
            else
            {
                if (rtype == "today")
                {
                    num = 0;
                }

                switch (num)
                {
                    case 0:
                        stime = DateTime.Now.ToString("yyyy-MM-dd");
                        etime = DateTime.Now.ToString("yyyy-MM-dd");
                        break;
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

            ViewBag.rtype = rtype;
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量            
            string types = string.IsNullOrEmpty(Request["s_type"]) ? "0" : Request["s_type"];
            string searchKey = string.IsNullOrEmpty(Request["s_key"]) ? "" : Request["s_key"];

            int sort = string.IsNullOrEmpty(Request["s_sort"]) ? 1 : int.Parse(Request["s_sort"]);
            string searchTotal = string.IsNullOrEmpty(Request["s_field"]) ? "" : Request["s_field"];

            DataTable dt = new DataTable();
            DataTable ddt = new DataTable();//用于查询总和CountSect
            string where = "where 1=1";
            string orderby = "order by ";
            string BsaeDb = System.Configuration.ConfigurationManager.AppSettings["BaseDb"];
            string helpUrl = System.Configuration.ConfigurationManager.AppSettings["helpUrl"].ToString();
            ViewBag.helpUrl = helpUrl;

            #region 组装排序字段
            //按日期倒序展示
            if (!string.IsNullOrEmpty(searchTotal))
            {
                if (searchTotal == "0")
                    orderby += "a_equipment ";
                else if (searchTotal == "1")
                    orderby += "a_success ";
                else if (searchTotal == "2")
                    orderby += "a_notpay ";
                else if (searchTotal == "3")
                    orderby += "a_alipay ";
                else if (searchTotal == "4")
                    orderby += "a_wechat ";
                else
                    orderby += "a_time ";
            }
            else
            {
                orderby += rtype == "total" ? "a_time  " : "a_appid  ";
            }
            orderby += (sort == 1 ? "desc" : "asc");
            #endregion
            #region 查询
            if (rtype == "total")
            {
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
                                where += " and a.a_appname like '%" + searchKey + "%'";
                                break;
                            case "2":
                                where += " and b.u_realname like '%" + searchKey + "%'";
                                break;
                        }
                    }
                }
                where += " and b.u_id='" + UserInfo.UserId + "'";
                where += " and a_time>='" + stime + "' and a_time<='" + etime + "' ";
                string sql = string.Format(@"select a_appname,a_appid,
isnull(SUM(a_equipment),0) as a_equipment,a_time,
isnull(SUM(a_success),0) a_success,
isnull(SUM(a_notpay),0) a_notpay,
isnull(SUM(a_alipay),0) a_alipay,
isnull(SUM(a_wechat),0) a_wechat,
isnull(SUM(a_qqwallet),0) a_qqwallet,
isnull(SUM(a_count),0) a_count,
isnull(SUM(a_curr),0) a_curr,
isnull(SUM(a_successratio),0) a_successratio,
isnull(SUM(a_arpur),0) a_arpur,
isnull(SUM(a_request),0) a_request,
isnull(sum(a_unionpay),0) a_unionpay,
b.u_realname   
from jmp_appreport a left join {0}.dbo.jmp_user b on a.a_uerid=b.u_id  {1} group by a_appname,b.u_realname,a_appid,a_time", BsaeDb, where);
                string countsql = string.Format(@"select sum(a_equipment) a_equipment,sum(a_success) a_success,SUM(a_notpay) a_notpay,sum(a_alipay) a_alipay,sum(a_wechat)a_wechat,isnull(SUM(a_qqwallet),0) a_qqwallet,sum(a_unionpay) a_unionpay, sum(a_count)a_count,SUM(a_curr)a_curr,SUM(a_request) a_request,SUM(a_successratio) a_successratio,SUM(a_arpur) a_arpur from jmp_appreport a left join {0}.dbo.jmp_user b on a.a_uerid=b.u_id {1} ", BsaeDb, where);
                dt = bll_report.GetLists(sql, orderby, pageIndexs, PageSize, out pageCount);
                ddt = bll_report.CountSect(countsql);
            }
            #endregion
            #region 查询今日
            else if (rtype == "today")
            {
                where += " and b.u_id='" + UserInfo.UserId + "' ";
                where += " and a_datetime>='" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00" + "' and a_datetime<='" + DateTime.Now.ToString("yyyy-MM-dd") + " 23:59" + "' ";
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
                                where += " and a.a_appname like '%" + searchKey + "%'";
                                break;
                            case "2":
                                where += " and b.u_realname like '%" + searchKey + "%'";
                                break;
                        }
                    }
                }
                string r_time = DateTime.Now.ToString("yyyy-MM-dd");
                string tname = GetOrderTableName(r_time);
                string query = string.Format(@"select a_appname,a_appid,
isnull(SUM(a_equipment),0) as a_equipment,GETDATE() a_time,
isnull(SUM(a_success),0) a_success,
isnull(SUM(a_notpay),0) a_notpay,
isnull(SUM(a_alipay),0) a_alipay,
isnull(SUM(a_wechat),0) a_wechat,
isnull(SUM(a_qqwallet),0) a_qqwallet,
isnull(SUM(a_count),0) a_count,
isnull(SUM(a_curr),0) a_curr,
isnull(SUM(a_successratio),0) a_successratio,
isnull(SUM(a_arpur),0) a_arpur,
isnull(SUM(a_request),0) a_request,
isnull(sum(a_unionpay),0) a_unionpay,
b.u_realname   
from jmp_appcount a left join {0}.dbo.jmp_user b on a.a_uerid=b.u_id  {1} group by a_appname,b.u_realname,a_appid  ", BsaeDb, where);

                string countsql = string.Format(@"select sum(a_equipment) a_equipment,sum(a_success) a_success,SUM(a_notpay) a_notpay,sum(a_alipay) a_alipay,sum(a_wechat)a_wechat,isnull(SUM(a_qqwallet),0) a_qqwallet,sum(a_unionpay) a_unionpay, sum(a_count)a_count,SUM(a_curr)a_curr,SUM(a_request) a_request,SUM(a_successratio) a_successratio,SUM(a_arpur) a_arpur from jmp_appcount a left join {0}.dbo.jmp_user b on a.a_uerid=b.u_id {1}", BsaeDb, where);
                JMP.BLL.jmp_appcount bll = new JMP.BLL.jmp_appcount();
                dt = bll.GetTodayList(query, orderby, pageIndexs, PageSize, out pageCount);
                if (dt.Rows.Count > 0)
                {
                    ddt = bll.CountSect(countsql);
                }
            }
            #endregion
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.ddt = ddt;
            ViewBag.rtype = rtype;

            ViewBag.stime = stime;
            ViewBag.etime = etime;
            return View(dt);
        }

    }
}
