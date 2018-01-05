/************聚米支付平台__报表逻辑************/
//描述：报表业务逻辑
//功能：报表等业务逻辑
//开发者：陶涛
//开发时间: 2016.03.22
/************聚米支付平台__报表逻辑************/

using JMP.BLL;
using JMP.TOOL;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TOOL;
using TOOL.EnumUtil;
using TOOL.Extensions;
using WEB.Util.Logger;

namespace WEB.Controllers
{
    public class REPORTController : Controller
    {
        /// <summary>
        /// 日志收集器
        /// </summary>
		private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;

        JMP.BLL.jmp_terminal bll_ter = new JMP.BLL.jmp_terminal();
        JMP.BLL.jmp_order bll_order = new JMP.BLL.jmp_order();
        JMP.BLL.jmp_user_report bll_report = new JMP.BLL.jmp_user_report();
        JMP.BLL.jmp_limit bll_limit = new JMP.BLL.jmp_limit();

        /// <summary>
        /// 用户报表
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult UserReport()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量            
            string types = string.IsNullOrEmpty(Request["s_type"]) ? "0" : Request["s_type"];
            string searchKey = string.IsNullOrEmpty(Request["s_key"]) ? "" : Request["s_key"];
            string stime = string.IsNullOrEmpty(Request["s_begin"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_begin"];
            string etime = string.IsNullOrEmpty(Request["s_end"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_end"];
            int sort = string.IsNullOrEmpty(Request["s_sort"]) ? 1 : int.Parse(Request["s_sort"]);
            string BsaeDb = System.Configuration.ConfigurationManager.AppSettings["BaseDb"];
            int relationtype = string.IsNullOrEmpty(Request["relationtype"]) ? -1 : int.Parse(Request["relationtype"]);//商户类型
            DataTable dt = new DataTable();
            DataTable ddt = new DataTable();//用于统计
            string where = "where 1=1";
            string orderby = "order by a_curr " + (sort == 1 ? " desc " : " asc ");//排序
            #region 查询
            if (!string.IsNullOrEmpty(types))
            {
                if (!string.IsNullOrEmpty(searchKey) && searchKey != "undefined" && searchKey != "" && searchKey != "null")
                {
                    if (types == "0")
                        where += " and b.u_id=" + searchKey;
                    else if (types == "1")
                        where += " and b.u_realname like '%" + searchKey + "%'";
                }
            }
            if (relationtype > -1)
            {
                where += " and b.relation_type=" + relationtype;
            }


            where += " and a.a_time>='" + stime + " 00:00:00' and a.a_time<='" + etime + " 23:59:59' ";

            string sql = @"select b.u_id,b.u_realname,b.relation_type,a.a_time,SUM(a_equipment) a_equipment,SUM(a_count) a_count,SUM(a_success) a_success,
			SUM(a_notpay)a_notpay,SUM(a_request) a_request,SUM(a_successratio)a_successratio,SUM(a_alipay)a_alipay,SUM(a_wechat)a_wechat,isnull(SUM(a_qqwallet),0) a_qqwallet,sum(a_unionpay) a_unionpay,
			SUM(a_curr)a_curr,SUM(a_arpur) a_arpur,sum(complaintcount) complaintcount,sum(complaintl)complaintl,sum(refundcount) refundcount,sum(refundl) refundl,sum(a_money) a_money
			from jmp_appreport a
			left join " + BsaeDb + ".dbo.jmp_user b on a.a_uerid=b.u_id  " + where + " group by b.u_id,b.u_realname,a.a_time,b.relation_type ";

            string countsql = "	select SUM(a_equipment) a_equipment,SUM(a_count) a_count,SUM(a_success) a_success, SUM(a_notpay)a_notpay,SUM(a_request) a_request,sum(a_unionpay) a_unionpay,SUM(a_successratio)a_successratio,SUM(a_alipay)a_alipay,SUM(a_wechat)a_wechat,SUM(a_qqwallet) a_qqwallet,SUM(a_curr)a_curr,SUM(a_arpur) a_arpur,sum(complaintcount) complaintcount,sum(complaintl)complaintl,sum(refundcount) refundcount,sum(refundl) refundl,sum(a_money) a_money from jmp_appreport a left join " + BsaeDb + ".dbo.jmp_user b on a.a_uerid=b.u_id  " + where;


            dt = bll_report.GetLists(sql, orderby, pageIndexs, PageSize, out pageCount);
            if (dt.Rows.Count > 0)
            {
                ddt = bll_report.CountSect(countsql);
            }
            #endregion
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.ddt = ddt;
            ViewBag.relationtype = relationtype;
            return View(dt);
        }

        /// <summary>
        /// 用户报表导出
        /// </summary>
        [LoginCheckFilter(IsCheck = true, IsRole = false)]
        public FileContentResult UserReportDC()
        {


            string types = string.IsNullOrEmpty(Request["s_type"]) ? "0" : Request["s_type"];
            string searchKey = string.IsNullOrEmpty(Request["s_key"]) ? "" : Request["s_key"];
            string stime = string.IsNullOrEmpty(Request["s_begin"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_begin"];
            string etime = string.IsNullOrEmpty(Request["s_end"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_end"];
            int sort = string.IsNullOrEmpty(Request["s_sort"]) ? 1 : int.Parse(Request["s_sort"]);
            string BsaeDb = System.Configuration.ConfigurationManager.AppSettings["BaseDb"];
            //  string RoleID = System.Configuration.ConfigurationManager.AppSettings["RoleID"];
            string where = "where 1=1";

            #region 查询
            if (!string.IsNullOrEmpty(types))
            {
                if (!string.IsNullOrEmpty(searchKey))
                {
                    if (types == "0")
                        where += " and b.u_id=" + searchKey;
                    else if (types == "1")
                        where += " and b.u_realname like '%" + searchKey + "%'";
                }
            }
            //if (UserInfo.UserRoleId == RoleID)
            //{
            //    where += " and  b.u_merchant_id=" + UserInfo.UserId;
            //}
            where += " and a.a_time>='" + stime + " 00:00:00' and a.a_time<='" + etime + " 23:59:59' ";


            string sql = @"select b.u_id,b.u_realname as r_user_name ,a.a_time as r_date,SUM(a_equipment) r_equipment,SUM(a_count) a_count,SUM(a_success) a_succeed,
	SUM(a_notpay)a_notpay,SUM(a_request) a_request,SUM(a_successratio)a_successratio,SUM(a_alipay)a_alipay,SUM(a_wechat)a_wechat,SUM(a_qqwallet)a_qqwallet,sum(a_unionpay) a_unionpay,
	SUM(a_curr)a_curr,SUM(a_arpur) a_arpur,sum(complaintcount) complaint_count,sum(complaintl)complaintl,sum(refundcount) refundcount,sum(refundl) refundl
	 from jmp_appreport a
	left join " + BsaeDb + ".dbo.jmp_user b on a.a_uerid=b.u_id  " + where + " group by b.u_id,b.u_realname,a.a_time  order by a_curr " + (sort == 1 ? " desc " : " asc ");


            List<JMP.MDL.jmp_user_report> list = new List<JMP.MDL.jmp_user_report>();
            JMP.BLL.jmp_user_report userReportbll = new JMP.BLL.jmp_user_report();
            list = userReportbll.DcSelectList(sql);
            var lst = list.Select(x => new
            {

                r_date = x.r_date.ToString("yyyy-MM-dd "),
                x.r_user_name,
                x.r_equipment,
                x.a_count,
                x.a_succeed,
                x.a_notpay,
                a_qil = Convert.ToInt32(x.a_count) != 0 && Convert.ToInt32(x.r_equipment) != 0 ? (decimal.Parse(x.a_count.ToString()) / decimal.Parse(x.r_equipment.ToString()) * 100).ToString("f2") + "%" : "0.00%",
                a_fql = Convert.ToInt32(x.a_count) != 0 && Convert.ToInt32(x.a_succeed) != 0 ? (decimal.Parse(x.a_succeed.ToString()) / decimal.Parse(x.a_count.ToString()) * 100).ToString("f2") + "%" : "0.00%",

                x.a_alipay,
                x.a_wechat,
                x.a_qqwallet,
                x.a_unionpay,
                x.a_curr,
                arpu = Convert.ToInt32(x.r_equipment) != 0 && Convert.ToInt32(x.a_curr) != 0 ? (decimal.Parse(x.a_curr.ToString()) / decimal.Parse(x.r_equipment.ToString())).ToString("f2") : "0.00",

                x.complaint_count,
                tsl = Convert.ToInt32(x.complaint_count) != 0 && Convert.ToInt32(x.a_succeed) != 0 ? (decimal.Parse(x.complaint_count.ToString()) / decimal.Parse(x.a_succeed.ToString()) * 100).ToString("f2") + "%" : "0.00%",
                x.refund_count,
                BDL = Convert.ToInt32(x.refund_count) != 0 && Convert.ToInt32(x.a_succeed) != 0 ? (decimal.Parse(x.refund_count.ToString()) / decimal.Parse(x.a_succeed.ToString()) * 100).ToString("f2") + "%" : "0.00%"

            });
            var caption = "用户报表";
            byte[] fileBytes;
            //命名导出表格的StringBuilder变量
            using (var pck = new ExcelPackage())
            {

                var ws = pck.Workbook.Worksheets.Add(caption);
                ws.Cells["A1"].LoadFromCollection(lst, false);
                ws.InsertRow(1, 1);
                ws.Cells["A1"].Value = "日期";
                ws.Cells["B1"].Value = "用户名";
                ws.Cells["C1"].Value = "设备量";
                ws.Cells["D1"].Value = "请求量";
                ws.Cells["E1"].Value = "成功量";
                ws.Cells["F1"].Value = "未支付量";
                ws.Cells["G1"].Value = "请求率";
                ws.Cells["H1"].Value = "付费成功率";
                ws.Cells["I1"].Value = "支付宝收入";
                ws.Cells["J1"].Value = "微信收入";
                ws.Cells["K1"].Value = "QQ钱包收入";
                ws.Cells["L1"].Value = "银联收入";
                ws.Cells["M1"].Value = "合计收入";
                ws.Cells["N1"].Value = "arpu值";
                ws.Cells["O1"].Value = "投诉量";
                ws.Cells["P1"].Value = "投诉率";
                ws.Cells["Q1"].Value = "补单量";
                ws.Cells["R1"].Value = "补单率";
                fileBytes = pck.GetAsByteArray();

            }
            string fileName = "用户报表" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            #endregion
            return File(fileBytes, "application/vnd.ms-excel", fileName);
        }

        /// <summary>
        /// 应用报表
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]

        public ActionResult AppReport(string rtype)
        {
            rtype = !string.IsNullOrEmpty(rtype) ? rtype : "total";
            ViewBag.rtype = rtype;
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量            
            string types = string.IsNullOrEmpty(Request["s_type"]) ? "0" : Request["s_type"];
            string searchKey = string.IsNullOrEmpty(Request["s_key"]) ? "" : Request["s_key"];
            string stime = string.IsNullOrEmpty(Request["s_begin"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_begin"];
            string etime = string.IsNullOrEmpty(Request["s_end"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_end"];
            int sort = string.IsNullOrEmpty(Request["s_sort"]) ? 1 : int.Parse(Request["s_sort"]);
            string searchTotal = string.IsNullOrEmpty(Request["s_field"]) ? "" : Request["s_field"];
            string platformid = string.IsNullOrEmpty(Request["platformid"]) ? "" : Request["platformid"];
            //string RoleID = System.Configuration.ConfigurationManager.AppSettings["RoleID"];
            DataTable dt = new DataTable();
            DataTable ddt = new DataTable();
            string where = "where 1=1";
            string orderby = "order by ";
            string BsaeDb = System.Configuration.ConfigurationManager.AppSettings["BaseDb"];
            #region 组装排序字段
            //按日期倒序展示
            if (!string.IsNullOrEmpty(searchTotal))
            {
                if (searchTotal == "0")
                    orderby += "a_count ";
                else if (searchTotal == "1")
                    orderby += "a_success ";
                else if (searchTotal == "2")
                    orderby += "successratio ";
                else if (searchTotal == "3")
                    orderby += "a_curr ";
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

            if (!string.IsNullOrEmpty(platformid))
            {
                switch (platformid)
                {
                    case "1":
                        where += " and c.a_platform_id=1";
                        break;
                    case "2":
                        where += " and c.a_platform_id=2";
                        break;
                    case "3":
                        where += " and c.a_platform_id=3";
                        break;
                }
            }
            if (!string.IsNullOrEmpty(types))
            {
                if (!string.IsNullOrEmpty(searchKey))
                {
                    switch (types)
                    {
                        case "0":
                            // where += " and r_app_key like '%" + searchKey + "%'";
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
            where += " and a.a_time>='" + stime + "' and a.a_time<='" + etime + "' ";
            //if (UserInfo.UserRoleId == RoleID)
            //{
            //    where += " and  b.u_merchant_id=" + UserInfo.UserId;
            //}
            string sql = string.Format(@"select a.a_appname,a.a_appid,
isnull(SUM(a_equipment),0) as a_equipment,a.a_time,
isnull(SUM(a_success),0) a_success,
isnull(SUM(a_notpay),0) a_notpay,
isnull(SUM(a_alipay),0) a_alipay,
isnull(SUM(a_wechat),0) a_wechat,
isnull(SUM(a_qqwallet),0) a_qqwallet,
isnull(sum(a_unionpay),0) a_unionpay,
isnull(SUM(a_count),0) a_count,
isnull(SUM(a_curr),0) a_curr,
isnull(SUM(a_successratio),0) a_successratio,
isnull(SUM(a_arpur),0) a_arpur,
isnull(SUM(a_request),0) a_request,
ISNULL(SUM(complaintcount),0) complaintcount,
ISNULL(SUM(complaintl),0) complaintl,
ISNULL(SUM(refundcount),0) refundcount,
ISNULL(SUM(refundl),0) refundl,
ISNULL(SUM(a_money),0) a_money,
(case when SUM(a_count)=0 then 0 else SUM(a_success)/SUM(a_count) end)as successratio,
b.u_realname ,c.a_platform_id 
from jmp_appreport a left join {0}.dbo.jmp_user b on a.a_uerid=b.u_id  left join dx_base.dbo.jmp_app c on a.a_appid=c.a_id  {1} group by a_appname,b.u_realname,a_appid,a.a_time,c.a_platform_id ", BsaeDb, where);
            string counsql = string.Format(@"select sum(a_equipment) a_equipment,sum(a_success) a_success,SUM(a_notpay) a_notpay,sum(a_alipay) a_alipay,sum(a_wechat)a_wechat,sum(a_qqwallet)a_qqwallet,sum(a_unionpay) a_unionpay, sum(a_count)a_count,SUM(a_curr)a_curr,SUM(a_request) a_request,SUM(a_successratio) a_successratio,SUM(a_arpur) a_arpur, SUM(complaintcount) complaintcount,sum(a_money) a_money,SUM(complaintl) complaintl, SUM(refundcount) refundcount, SUM(refundl) refundl from jmp_appreport a left join {0}.dbo.jmp_user b on a.a_uerid=b.u_id left join dx_base.dbo.jmp_app c on a.a_appid=c.a_id  {1} ", BsaeDb, where);
            dt = bll_report.GetLists(sql, orderby, pageIndexs, PageSize, out pageCount);
            if (dt.Rows.Count > 0)
            {
                ddt = bll_report.CountSect(counsql);
            }
            #endregion
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.platformid = platformid;
            ViewBag.ddt = ddt;
            return View(dt);
        }
        /// <summary>
        /// 应用报表查询每日
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult AppReportDay()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量            
            string types = string.IsNullOrEmpty(Request["s_type"]) ? "0" : Request["s_type"];
            string searchKey = string.IsNullOrEmpty(Request["s_key"]) ? "" : Request["s_key"];
            string sdate = string.IsNullOrEmpty(Request["sdate"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["sdate"];
            string edate = string.IsNullOrEmpty(Request["edate"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["edate"];
            ViewBag.sdate = sdate;
            ViewBag.edate = edate;
            string stime = (string.IsNullOrEmpty(Request["s_begin"]) ? "00" : Request["s_begin"]);
            ViewBag.stime = stime;
            string etime = (string.IsNullOrEmpty(Request["s_end"]) ? "23" : Request["s_end"]);
            ViewBag.etime = etime;
            int sort = string.IsNullOrEmpty(Request["s_sort"]) ? 1 : int.Parse(Request["s_sort"]);
            string searchTotal = string.IsNullOrEmpty(Request["s_field"]) ? "" : Request["s_field"];
            int pdcsh = string.IsNullOrEmpty(Request["pdcsh"]) ? 0 : Int32.Parse(Request["pdcsh"]);
            string platformid = string.IsNullOrEmpty(Request["platformid"]) ? "" : Request["platformid"];
            // string RoleID = System.Configuration.ConfigurationManager.AppSettings["RoleID"];
            DataTable dt = new DataTable();
            DataTable ddt = new DataTable();
            string where = " where 1=1  and convert(nvarchar(2),a_datetime,108)>='" + stime + "' AND convert(nvarchar(2),a_datetime,108)<='" + etime + "' and convert(nvarchar(10), a_datetime, 120)>= '" + sdate + "' AND convert(nvarchar(10), a_datetime, 120)<= '" + edate + "' ";
            string orderby = "order by ";
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
                    orderby += "a_datetime ";
            }
            else
            {
                orderby += "a_appname ";
            }

            orderby += (sort == 1 ? "desc" : "asc");
            #endregion
            #region 查询今日
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
                            where += " and a_appname like '%" + searchKey + "%'";
                            break;
                        case "2":
                            where += " and b.u_realname like '%" + searchKey + "%'";
                            break;
                    }
                }
            }
            if (!string.IsNullOrEmpty(platformid))
            {
                switch (platformid)
                {
                    case "1":
                        where += " and c.a_platform_id=1";
                        break;
                    case "2":
                        where += " and c.a_platform_id=2";
                        break;
                    case "3":
                        where += " and c.a_platform_id=3";
                        break;
                }
            }
            //if (UserInfo.UserRoleId == RoleID)
            //{
            //    where += " and  b.u_merchant_id=" + UserInfo.UserId;
            //}
            string r_time = DateTime.Now.ToString("yyyy-MM-dd");
            string tname = JMP.TOOL.WeekDateTime.GetOrderTableName(r_time);
            string BsaeDb = System.Configuration.ConfigurationManager.AppSettings["BaseDb"];
            JMP.BLL.jmp_appcount bll = new JMP.BLL.jmp_appcount();
            string query = string.Format(@"select 
a_appname,
isnull(SUM(a_equipment),0) as a_equipment,CONVERT(nvarchar(10),a_datetime,120) a_datetime ,a_appid,c.a_platform_id,
isnull(SUM(a_success),0) a_success,
isnull(SUM(a_notpay),0) a_notpay,
isnull(SUM(a_alipay),0) a_alipay,
isnull(SUM(a_wechat),0) a_wechat,
isnull(SUM(a_qqwallet),0) a_qqwallet,
isnull(SUM(a_count),0) a_count,
isnull(SUM(a_curr),0) a_curr,
isnull(SUM(a_request),0) a_request,
isnull(SUM(a_successratio),0) a_successratio,
isnull(SUM(a_arpur),0) a_arpur,
isnull(sum(a_unionpay),0) a_unionpay,
isnull(sum(a_money),0) a_money,
b.u_realname    from jmp_appcount a left join {0}.dbo.jmp_user b on a.a_uerid=b.u_id  left join dx_base.dbo.jmp_app c  on c.a_id=a.a_appid  {1} group by a_appname,b.u_realname,a_appid, c.a_platform_id, CONVERT(nvarchar(10),a_datetime,120) ", BsaeDb, where);

            string counsql = string.Format(@"select sum(a_equipment) a_equipment,
sum(a_success) a_success,SUM(a_notpay) a_notpay,
sum(a_alipay) a_alipay,sum(a_wechat)a_wechat,sum(a_qqwallet)a_qqwallet,sum(a_count)a_count,
SUM(a_curr)a_curr,SUM(a_request) a_request,SUM(a_successratio) a_successratio,SUM(a_arpur) a_arpur,sum(a_unionpay) a_unionpay,sum(a_money) a_money
from jmp_appcount a
left join {0}.dbo.jmp_user b on a.a_uerid=b.u_id left join dx_base.dbo.jmp_app c  on c.a_id=a.a_appid  {1}  ", BsaeDb, where);

            dt = bll.GetTodayList(query, orderby, pageIndexs, PageSize, out pageCount);
            if (dt.Rows.Count > 0)
            {
                ddt = bll.CountSect(counsql);
            }

            #endregion
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.ddt = ddt;
            ViewBag.pdcsh = pdcsh;
            ViewBag.platformid = platformid;
            return View(dt);
        }
        [VisitRecord(IsRecord = true)]
        public ActionResult AppReportDayMinute()
        {

            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量            
            string types = string.IsNullOrEmpty(Request["s_type"]) ? "0" : Request["s_type"];
            string searchKey = string.IsNullOrEmpty(Request["s_key"]) ? "" : Request["s_key"];
            string sdate = string.IsNullOrEmpty(Request["sdate"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["sdate"];
            string edate = string.IsNullOrEmpty(Request["edate"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["edate"];
            ViewBag.sdate = sdate;
            ViewBag.edate = edate;
            string stime = (string.IsNullOrEmpty(Request["s_begin"]) ? "00:00" : Request["s_begin"]);
            ViewBag.stime = stime;
            string etime = (string.IsNullOrEmpty(Request["s_end"]) ? "23:00" : Request["s_end"]);
            ViewBag.etime = etime;
            int sort = string.IsNullOrEmpty(Request["s_sort"]) ? 1 : int.Parse(Request["s_sort"]);
            string searchTotal = string.IsNullOrEmpty(Request["s_field"]) ? "" : Request["s_field"];
            int pdcsh = string.IsNullOrEmpty(Request["pdcsh"]) ? 0 : Int32.Parse(Request["pdcsh"]);
            DataTable dt = new DataTable();
            DataTable ddt = new DataTable();
            string where = " where 1=1  and  convert(nvarchar(5), a_datetime,108)>='" + stime + "' AND  convert(nvarchar(5), a_datetime,108)<='" + etime + "' and convert(nvarchar(10), a_datetime, 120)>= '" + sdate + "' AND convert(nvarchar(10), a_datetime, 120)<= '" + edate + "' ";
            string orderby = "order by ";
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
                    orderby += "a_datetime ";
            }
            else
            {
                orderby += "a_appname ";
            }
            //orderby += " a_curr ";
            orderby += (sort == 1 ? "desc" : "asc");
            #endregion
            #region 查询今日
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
                            where += " and a_appname like '%" + searchKey + "%'";
                            break;
                        case "2":
                            where += " and b.u_realname like '%" + searchKey + "%'";
                            break;
                    }
                }
            }
            string r_time = DateTime.Now.ToString("yyyy-MM-dd");
            string tname = JMP.TOOL.WeekDateTime.GetOrderTableName(r_time);
            string BsaeDb = System.Configuration.ConfigurationManager.AppSettings["BaseDb"];
            JMP.BLL.jmp_appcountminute bll = new JMP.BLL.jmp_appcountminute();
            string query = string.Format(@"select 
a_appname,
isnull(SUM(a_equipment),0) as a_equipment,CONVERT(nvarchar(10),a_datetime,120) a_datetime ,a_appid,
isnull(SUM(a_success),0) a_success,
isnull(SUM(a_notpay),0) a_notpay,
isnull(SUM(a_alipay),0) a_alipay,
isnull(SUM(a_wechat),0) a_wechat,
isnull(SUM(a_qqwallet),0) a_qqwallet,
isnull(SUM(a_count),0) a_count,
isnull(SUM(a_curr),0) a_curr,
isnull(SUM(a_request),0) a_request,
isnull(SUM(a_successratio),0) a_successratio,
isnull(SUM(a_arpur),0) a_arpur,
isnull(sum(a_unionpay),0) a_unionpay,
isnull(SUM(a_money),0) a_money,
b.u_realname    from jmp_appcountminute a left join {0}.dbo.jmp_user b on a.a_uerid=b.u_id  {1} group by a_appname,b.u_realname,a_appid,CONVERT(nvarchar(10),a_datetime,120) ", BsaeDb, where);

            string counsql = string.Format(@"select sum(a_equipment) a_equipment,
sum(a_success) a_success,SUM(a_notpay) a_notpay,
sum(a_alipay) a_alipay,sum(a_wechat)a_wechat,sum(a_qqwallet)a_qqwallet,sum(a_count)a_count,
SUM(a_curr)a_curr,SUM(a_request) a_request,SUM(a_successratio) a_successratio,SUM(a_arpur) a_arpur,sum(a_unionpay) a_unionpay ,sum(a_money) a_money
from jmp_appcountminute a
left join {0}.dbo.jmp_user b on a.a_uerid=b.u_id {1}  ", BsaeDb, where);

            dt = bll.GetTodayList(query, orderby, pageIndexs, PageSize, out pageCount);
            if (dt.Rows.Count > 0)
            {
                ddt = bll.CountSect(counsql);
            }

            #endregion
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.ddt = ddt;
            ViewBag.pdcsh = pdcsh;
            return View(dt);

        }


        #region 应用通道报表
        public ActionResult AppChannelReport(string rtype)
        {
            rtype = !string.IsNullOrEmpty(rtype) ? rtype : "total";
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量            
            string types = string.IsNullOrEmpty(Request["s_type"]) ? "0" : Request["s_type"];
            string searchKey = string.IsNullOrEmpty(Request["s_key"]) ? "" : Request["s_key"];
            string stime = string.IsNullOrEmpty(Request["s_begin"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_begin"];
            string etime = string.IsNullOrEmpty(Request["s_end"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_end"];
            int sort = string.IsNullOrEmpty(Request["s_sort"]) ? 1 : int.Parse(Request["s_sort"]);
            string searchTotal = string.IsNullOrEmpty(Request["s_field"]) ? "" : Request["s_field"];
            string platformid = string.IsNullOrEmpty(Request["platformid"]) ? "" : Request["platformid"];
            string daytime = DateTime.Now.ToString("yyyy-MM-dd") + " " + (string.IsNullOrEmpty(Request["daytime"]) ? "00:00:00" : Request["daytime"]);
            ViewBag.daytime = Request["daytime"];
            string enddaytime = DateTime.Now.ToString("yyyy-MM-dd") + " " + (string.IsNullOrEmpty(Request["enddaytime"]) ? "23:59:59" : Request["enddaytime"]);
            ViewBag.enddaytime = Request["enddaytime"];
            DataTable dt = new DataTable();
            DataTable ddt = new DataTable();
            string orderby = "order by ";
            string BsaeDb = System.Configuration.ConfigurationManager.AppSettings["BaseDb"];
            string where = "where 1=1";
            if (!string.IsNullOrEmpty(platformid))
            {
                switch (platformid)
                {
                    case "1":
                        where += " and c.a_platform_id=1";
                        break;
                    case "2":
                        where += " and c.a_platform_id=2";
                        break;
                    case "3":
                        where += " and c.a_platform_id=3";
                        break;
                }
            }



            // 汇总查询
            #region 汇总查询

            if (rtype == "total")
            {
                if (!string.IsNullOrEmpty(types))
                {
                    if (!string.IsNullOrEmpty(searchKey))
                    {
                        switch (types)
                        {
                            case "0":
                                // where += " and r_app_key like '%" + searchKey + "%'";
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

                //按日期倒序展示
                if (!string.IsNullOrEmpty(searchTotal))
                {
                    switch (searchTotal)
                    {
                        case "0":
                            orderby += "a_success ";
                            break;
                        case "1":
                            orderby += "a_count ";
                            break;
                        case "2":
                            orderby += "successratio ";
                            break;
                        case "3":
                            orderby += "a_curr ";
                            break;
                        case "4":
                            orderby += "a_time ";
                            break;
                        default:
                            orderby += "a_time ";
                            break;
                    }
                }
                else
                {
                    orderby += "a_appid  ";
                }
                orderby += (sort == 1 ? "desc" : "asc");
                where += " and a.a_time>='" + stime + "' and a.a_time<='" + etime + "' ";
                string sql = string.Format(@"select a.a_appname,a.a_appid,
isnull(SUM(a_equipment),0) as a_equipment,a.a_time,
isnull(SUM(a_success),0) a_success,
isnull(SUM(a_notpay),0) a_notpay,
isnull(SUM(a_alipay),0) a_alipay,
isnull(SUM(a_wechat),0) a_wechat,
isnull(SUM(a_qqwallet),0) a_qqwallet,
isnull(sum(a_unionpay),0) a_unionpay,
isnull(SUM(a_count),0) a_count,
isnull(SUM(a_curr),0) a_curr,
isnull(SUM(a_successratio),0) a_successratio,
isnull(SUM(a_arpur),0) a_arpur,
isnull(SUM(a_request),0) a_request,
ISNULL(SUM(complaintcount),0) complaintcount,
ISNULL(SUM(complaintl),0) complaintl,
ISNULL(SUM(refundcount),0) refundcount,
ISNULL(SUM(refundl),0) refundl,
ISNULL(SUM(a_money),0) a_money,
(case when SUM(a_count)=0 then 0 else SUM(a_success)/SUM(a_count) end)as successratio,
b.u_realname ,c.a_platform_id 
from jmp_appreport a left join {0}.dbo.jmp_user b on a.a_uerid=b.u_id  left join {0}.dbo.jmp_app c on a.a_appid=c.a_id  {1} group by a_appname,b.u_realname,a_appid,a.a_time,c.a_platform_id  ", BsaeDb, where);
                string counsql = string.Format(@"select sum(a_equipment) a_equipment,sum(a_success) a_success,SUM(a_notpay) a_notpay,sum(a_alipay) a_alipay,sum(a_wechat)a_wechat,isnull(SUM(a_qqwallet),0) a_qqwallet, sum(a_unionpay) a_unionpay, sum(a_count)a_count,SUM(a_curr)a_curr,SUM(a_request) a_request,SUM(a_successratio) a_successratio,SUM(a_arpur) a_arpur, SUM(complaintcount) complaintcount,sum(a_money) a_money,SUM(complaintl) complaintl, SUM(refundcount) refundcount, SUM(refundl) refundl from jmp_appreport a left join {0}.dbo.jmp_user b on a.a_uerid=b.u_id left join dx_base.dbo.jmp_app c on a.a_appid=c.a_id   {1} ", BsaeDb, where);
                dt = bll_report.GetLists(sql, orderby, pageIndexs, PageSize, out pageCount);
                if (dt.Rows.Count > 0)
                {
                    ddt = bll_report.CountSect(counsql);
                }
                StringBuilder sqlinfo = new StringBuilder();
                sqlinfo.AppendFormat(@"select  ChannelId,ChannelName,PayTypeName,AppId,CreatedDate,
 isnull(SUM([Money]),0) [Money],
 isnull(SUM(Notpay),0) Notpay,
 isnull(SUM(Success),0) Success,
 isnull(SUM(Successratio),0) Successratio
  from [dx_total].[dbo].[jmp_AppChannelReport] WHERE CreatedDate>='{0}' AND CreatedDate<='{1}'
GROUP BY ChannelId,ChannelName,PayTypeName,AppId,CreatedDate", stime, etime);
                JMP.BLL.jmp_AppChannelReport bll = new JMP.BLL.jmp_AppChannelReport();
                List<JMP.MDL.jmp_AppChannelReport> list = new List<JMP.MDL.jmp_AppChannelReport>();
                DataTable dtinfo = bll.SelectList(sqlinfo.ToString());
                list = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_AppChannelReport>(dtinfo);
                ViewBag.colist = list;
                ViewBag.ddt = ddt;

            }
            #endregion
            #region 今日查询 [10分钟汇总表中统计]
            //今日查询
            else
            {
                if (!string.IsNullOrEmpty(types))
                {
                    if (!string.IsNullOrEmpty(searchKey))
                    {
                        switch (types)
                        {
                            case "0":
                                // where += " and r_app_key like '%" + searchKey + "%'";
                                break;
                            case "1":
                                where += " and  c.a_name like '%" + searchKey + "%'";
                                break;
                            case "2":
                                where += " and b.u_realname like '%" + searchKey + "%'";
                                break;
                        }
                    }
                }

                //按日期倒序展示
                if (!string.IsNullOrEmpty(searchTotal))
                {
                    switch (searchTotal)
                    {
                        case "1":
                            orderby += "a_success ";
                            break;
                        case "2":
                            orderby += "a_count ";
                            break;
                        case "3":
                            orderby += "a_curr ";
                            break;
                        case "6":
                            orderby += "a_time ";
                            break;
                        default:
                            orderby += "a_time ";
                            break;
                    }
                }
                else
                {
                    orderby += "a_appid  ";
                }
                orderby += (sort == 1 ? "desc" : "asc");

                where += " and CreatedDate>='" + daytime + "' and CreatedDate<'" + enddaytime + "' ";
                string sql = string.Format(@"select c.a_name as a_appname,a.AppId as a_appid,
GETDATE() as a_time,
isnull(SUM(Success),0) a_success,
isnull(SUM(Notpay),0) a_notpay,
isnull(SUM(Success+Notpay),0) a_count,
isnull(SUM([Money]),0) a_curr,
case when  SUM(Notpay)=0 then 0 else 
CONVERT(decimal(16,4),(
CONVERT(decimal(16,4),SUM(Success))/CONVERT (decimal(16,4),(SUM(Notpay)+SUM(Success)))))end as a_successratio,
b.u_realname ,c.a_platform_id 
from jmp_AppChannelMinuteReport a   left join {0}.dbo.jmp_app c on a.AppId=c.a_id left join {0}.dbo.jmp_user b 
on c.a_user_id=b.u_id
{1}group by  c.a_name,b.u_realname,AppId,c.a_platform_id 
 ", BsaeDb, where);

                string counsql = string.Format(@"    select sum(Success) a_success,SUM(Notpay) a_notpay,
     sum(Success+Notpay)a_count,SUM([Money])a_curr,CONVERT(DECIMAL(16,2),(case when sum(Success + Notpay)=0 then 0 else (sum(Success*1.0)/ sum(Success + Notpay))end)) a_successratio
    from jmp_AppChannelMinuteReport a 
   left join {0}.dbo.jmp_app c on a.AppId=c.a_id  left join {0}.dbo.jmp_user b on c.a_user_id=b.u_id 
     {1}   ", BsaeDb, where);
                dt = bll_report.GetLists(sql, orderby, pageIndexs, PageSize, out pageCount);
                if (dt.Rows.Count > 0)
                {
                    ddt = bll_report.CountSect(counsql);
                }
                ViewBag.ddt = ddt;

                StringBuilder sqlinfo = new StringBuilder();
                sqlinfo.AppendFormat(@" select  ChannelId,ChannelName,PayTypeName,AppId,
 isnull(SUM([Money]),0) [Money],
 isnull(SUM(Notpay),0) Notpay,
 isnull(SUM(Success),0) Success,
 isnull(SUM(Successratio),0) Successratio
  from [dx_total].[dbo].[jmp_AppChannelMinuteReport] WHERE CreatedDate>='{0}' AND CreatedDate<'{1}'
GROUP BY ChannelId,ChannelName,PayTypeName,AppId", daytime, enddaytime);
                JMP.BLL.jmp_AppChannelReport bll = new JMP.BLL.jmp_AppChannelReport();
                List<JMP.MDL.jmp_AppChannelReport> list = new List<JMP.MDL.jmp_AppChannelReport>();
                DataTable dtinfo = bll.SelectList(sqlinfo.ToString());
                list = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_AppChannelReport>(dtinfo);
                ViewBag.colist = list;


            }
            #endregion

            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.rtype = rtype;
            ViewBag.platformid = platformid;
            return View(dt);
        }
        #endregion

        /// <summary>
        /// 流量概述
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult FlowReport()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量            
            string types = string.IsNullOrEmpty(Request["s_type"]) ? "0" : Request["s_type"];
            string searchKey = string.IsNullOrEmpty(Request["s_key"]) ? "" : Request["s_key"];
            string stime = string.IsNullOrEmpty(Request["s_begin"]) ? DateTime.Now.AddDays(-8).ToString("yyyy-MM-dd") : Request["s_begin"];
            string etime = string.IsNullOrEmpty(Request["s_end"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_end"];
            int sort = string.IsNullOrEmpty(Request["s_sort"]) ? 1 : int.Parse(Request["s_sort"]);
            string searchTotal = string.IsNullOrEmpty(Request["s_field"]) ? "" : Request["s_field"];
            string BsaeDb = System.Configuration.ConfigurationManager.AppSettings["BaseDb"];
            string where = "where 1=1";
            string orderby = "order by ";
            string groupby = "";
            string fields = "";//
            #region 组装分组字段和查询条件
            if (!string.IsNullOrEmpty(types))
            {
                groupby = "group by";
                if (types == "0")
                {
                    if (!string.IsNullOrEmpty(searchKey))
                        where += " and a_time like '%" + searchKey + "%'";
                    groupby += " a_time ";
                    fields = "a_time t_fiels,a_time";
                }
                else if (types == "1")
                {
                    if (!string.IsNullOrEmpty(searchKey))
                        where += " and r_app_name like '%" + searchKey + "%'";
                    groupby += " a_appname,r_date ";
                    fields = "a_appname t_fiels,a_time";
                }
                else if (types == "2")
                {
                    if (!string.IsNullOrEmpty(searchKey))
                        where += " and r_user_name like '%" + searchKey + "%'";
                    groupby += " b.u_realname,a_time ";
                    fields = "u_realname t_fiels,a_time";
                }
            }
            #endregion
            #region 组装排序字段及查询语句
            //按日期倒序展示
            if (!string.IsNullOrEmpty(searchTotal))
            {
                if (searchTotal == "0")
                    orderby += "a_count ";
                else if (searchTotal == "1")
                    orderby += "a_success ";
                else if (searchTotal == "2")
                    orderby += "successratio ";
                else if (searchTotal == "3")
                    orderby += "a_curr ";
                else if (searchTotal == "4")
                    orderby += "a_time ";

            }
            else
            {
                orderby += "a_time ";
            }
            orderby += (sort == 1 ? "desc" : "asc");
            where += " and a_time>='" + stime + "' and a_time<='" + etime + "' ";

            string sql = string.Format(@"select {0},sum(a_equipment) a_equipment,sum(a_success) a_success,
sum(a_notpay) a_notpay,sum(a_alipay) a_alipay,sum(a_wechat) 
a_wechat,isnull(sum(a_qqwallet),0)a_qqwallet,isnull(sum(a_success)+sum(a_notpay),0) a_count,isnull(sum(a_unionpay),0) a_unionpay,
isnull(sum(a_alipay)+sum(a_wechat)+sum(a_unionpay) +sum( isnull (a_qqwallet,0)) ,0) a_curr,(case when SUM(a_count)=0 then 0 else SUM(a_success)/SUM(a_count) end)as successratio    from jmp_appreport  a
left join {3}.dbo.jmp_user b on a.a_uerid=b.u_id {1} {2}", fields, where, groupby, BsaeDb);

            DataTable dt = bll_report.GetLists(sql, orderby, pageIndexs, PageSize, out pageCount);
            DataTable ddt = new DataTable();
            string sqlcount = string.Format(@"select sum(a_equipment) a_equipment,sum(a_success) a_success,sum(a_notpay) a_notpay,sum(a_alipay) a_alipay,sum(a_wechat) a_wechat,isnull(sum(a_qqwallet),0)a_qqwallet, isnull(sum(a_unionpay),0) a_unionpay, isnull(sum(a_success)+sum(a_notpay),0) a_count,isnull(sum(a_alipay)+sum(a_wechat)+sum(a_unionpay)+sum( isnull (a_qqwallet,0)) ,0) a_curr from jmp_appreport  a
left join {2}.dbo.jmp_user b on a.a_uerid=b.u_id {1}", fields, where, BsaeDb);
            if (dt.Rows.Count > 0)
            {
                ddt = bll_report.CountSect(sqlcount);
            }
            #endregion
            ViewBag.show_fields = types;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.ddt = ddt;
            return View(dt);
        }

        public ActionResult Index()
        {
            string moneycount = "";
            JMP.BLL.jmp_appcount bll = new JMP.BLL.jmp_appcount();
            string UserRoleId = UserInfo.UserRoleId.ToString();
            int userid = UserInfo.UserId;
            moneycount = bll.SelectSumDay(UserRoleId, userid);
            ViewBag.moneycount = moneycount;
            return View();
        }

        /// <summary>
        /// 设备流量图
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]

        [LoginCheckFilter(IsCheck = true, IsRole = false)]
        public string DeviceTrend(string days)
        {
            //默认查询当天
            string dept = UserInfo.UserRoleId.ToString();
            // int RoleID = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RoleID"]);
            int RoleID = 9999;
            int userid = int.Parse(dept) == RoleID ? UserInfo.UserId : 0;
            days = !string.IsNullOrEmpty(days) ? days : "0";
            JMP.BLL.jmp_appcount bll = new JMP.BLL.jmp_appcount();
            //查询数据
            DataSet ds = new DataSet();
            DataSet dsOrderSuccess = new DataSet();
            DataSet dsOrder = new DataSet();
            DataSet dsverage = new DataSet();
            DataSet dsAverage = new DataSet();
            #region 查询一天（今天、昨天或前天）
            if (days != "3")
            {
                string s_time = "";

                if (days == "0")
                    s_time = DateTime.Now.ToString("yyyy-MM-dd");
                else if (days == "1")
                    s_time = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                else if (days == "2")
                    s_time = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");
                //取出表名
                string t_name = JMP.TOOL.WeekDateTime.GetOrderTableName(s_time);
                //查询数据

                ds = bll.GetListReportOrderSuccess(s_time, dept, userid, RoleID);
                dsAverage = bll.GetListAverage(DateTime.Now.AddDays(-4).ToString("yyyy-MM-dd"), DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), dept, userid, RoleID);
            }
            #endregion
            #region 查询三天的成功量
            else
            {
                ds = bll.GetListReportOrderSuccess(DateTime.Now.ToString("yyyy-MM-dd"), dept, userid, RoleID);
                dsOrderSuccess = bll.GetListReportOrderSuccess(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), dept, userid, RoleID);
                dsOrder = bll.GetListReportOrderSuccess(DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd"), dept, userid, RoleID);
            }
            #endregion
            bool hasData = days == "3" ? ds.Tables[0].Rows.Count > 0 || dsOrderSuccess.Tables[0].Rows.Count > 0 || dsOrder.Tables[0].Rows.Count > 0 : ds.Tables[0].Rows.Count > 0 || dsAverage.Tables[0].Rows.Count > 0;
            string html1 = "";
            string html2 = "";
            string htmls = string.Empty;
            if (hasData)
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
                htmls += "\"seriesName\": " + (days == "3" ? "\"今天\"," : "\"前三天平均交易量\",");
                htmls += "\"data\": [";
                html1 = "{\"seriesName\": " + (days == "3" ? "\"昨天\"," : "\"成功量\",") + "\"data\": [";//成功量
                html2 = "{\"seriesName\":" + (days == "3" ? "\"前天\"," : "\"交易笔数\",") + "\"data\": [";//交易笔数
                                                                                                     //根据每小时组装数据
                for (int i = 0; i < len; i++)
                {
                    bool flag = true;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        int hours = int.Parse(DateTime.Parse(dr["a_datetime"].ToString()).ToString("HH"));
                        if (i == hours)
                        {
                            flag = false;
                            if (days != "3")
                            {

                                html1 += "{\"value\": \"" + decimal.Parse(dr["a_success"].ToString()).ToString("f0") + "\",";
                                html1 += "\"toolText\": \"" + (days == "3" ? "支付量走势" : "交易走势") + i + "时：" + (days == "3" ? "昨天" : "成功量") + decimal.Parse(dr["a_success"].ToString()).ToString("f0") + "\"},";
                                html2 += "{\"value\": \"" + decimal.Parse(dr["a_count"].ToString()).ToString("f0") + "\",";
                                html2 += "\"toolText\": \"" + (days == "3" ? "支付量走势" : "交易走势") + i + "时：" + (days == "3" ? "前天" : "交易笔数") + decimal.Parse(dr["a_count"].ToString()).ToString("f0") + "\"},";
                            }
                            else
                            {
                                htmls += "{\"value\": \"" + decimal.Parse(dr["a_success"].ToString()).ToString("f0") + "\",";
                                htmls += "\"toolText\": \"" + (days == "3" ? "支付量走势" : "交易走势") + i + "时：" + (days == "3" ? "今天" : "设备量") + decimal.Parse(dr["a_success"].ToString()).ToString("f0") + "\"},";
                            }
                            break;
                        }
                    }
                    if (flag)
                    {
                        if (days != "3")
                        {
                            html1 += "{\"value\": \"0\",";
                            html1 += "\"toolText\": \"" + (days == "3" ? "支付量走势" : "交易走势") + i + "时：" + (days == "3" ? "昨天" : "成功量") + "0\"},";
                            html2 += "{\"value\": \"0\",";
                            html2 += "\"toolText\": \"" + (days == "3" ? "支付量走势" : "交易走势") + i + "时：" + (days == "3" ? "前天" : "交易笔数") + "0\"},";
                        }
                        else
                        {
                            htmls += "{\"value\": \"0\",";
                            htmls += "\"toolText\": \"" + (days == "3" ? "支付量走势" : "交易走势") + i + "时：" + (days == "3" ? "今天" : "设备量") + "0\"},";
                        }
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
                if (days == "3")
                {
                    #region 只有查询三天数据时才执行此方法
                    //根据每小时组装数据
                    for (int i = 0; i < len; i++)
                    {
                        bool curr = true;
                        foreach (DataRow dr in dsOrderSuccess.Tables[0].Rows)
                        {
                            int hours = int.Parse(DateTime.Parse(dr["a_datetime"].ToString()).ToString("HH"));
                            if (i == hours)
                            {
                                curr = false;
                                html1 += "{\"value\": \"" + decimal.Parse(dr["a_success"].ToString()).ToString("f0") + "\",";
                                html1 += "\"toolText\": \"" + (days == "3" ? "支付量走势" : "交易走势") + i + "时：" + (days == "3" ? "昨天" : "成功量") + decimal.Parse(dr["a_success"].ToString()).ToString("f0") + "\"},";
                                break;
                            }
                        }
                        if (curr)
                        {
                            html1 += "{\"value\": \"0\",";
                            html1 += "\"toolText\": \"" + (days == "3" ? "支付量走势" : "交易走势") + i + "时：" + (days == "3" ? "昨天" : "成功量") + "0\"},";
                        }
                    }
                    // 根据每小时组装数据
                    for (int i = 0; i < len; i++)
                    {
                        bool dqxs = true;
                        foreach (DataRow dr in dsOrder.Tables[0].Rows)
                        {
                            int hours = int.Parse(DateTime.Parse(dr["a_datetime"].ToString()).ToString("HH"));
                            if (i == hours)
                            {
                                dqxs = false;
                                html2 += "{\"value\": \"" + decimal.Parse(dr["a_success"].ToString()).ToString("f0") + "\",";
                                html2 += "\"toolText\": \"" + (days == "3" ? "支付量走势" : "交易走势") + i + "时：" + (days == "3" ? "前天" : "交易笔数") + decimal.Parse(dr["a_success"].ToString()).ToString("f0") + "\"},";
                                break;
                            }
                        }
                        if (dqxs)
                        {
                            html2 += "{\"value\": \"0\",";
                            html2 += "\"toolText\": \"" + (days == "3" ? "支付量走势" : "交易走势") + i + "时：" + (days == "3" ? "前天" : "交易笔数") + "0\"},";
                        }
                    }
                    #endregion
                }
                html1 = html1.TrimEnd(',');
                html1 += "]";
                html1 += "},";
                html2 = html2.TrimEnd(',');
                html2 += "]";
                html2 += "}";
                htmls = htmls + html1 + html2;
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
        /// 订单列表
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult OrderList()
        {
            #region 获取信息

            JMP.BLL.jmp_paymode paymodebll = new JMP.BLL.jmp_paymode();
            List<JMP.MDL.jmp_paymode> paymodeList = paymodebll.GetModelList("1=1 and p_state='1' ");//支付类型
            ViewBag.paymodeList = paymodeList;
            #endregion
            #region 查询
            string sql = "";
            string sql1 = "";
            //组装查询条件
            string TableName = "";//表名
            string order = "o_ptime ";//排序字段
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
            int platformid = string.IsNullOrEmpty(Request["platformid"]) ? 0 : Int32.Parse(Request["platformid"]);//关联平台
            int relationtype = string.IsNullOrEmpty(Request["relationtype"]) ? -1 : Int32.Parse(Request["relationtype"]);//商户类型   

            ViewBag.platformid = platformid;
            ArrayList sjfw = JMP.TOOL.WeekDateTime.WeekMonday(DateTime.Parse(stime), DateTime.Parse(etime));//根据时间返回获取每周周一组装查询语句
            for (int i = 0; i < sjfw.Count; i++)
            {
                TableName = "jmp_order_" + DateTime.Parse(sjfw[i].ToString()).ToString("yyyyMMdd");
                // TableName = "jmp_order_20161107";
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
            string where = "where 1=1";//组装查询条件
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
                        where += " and e.u_realname like '%" + searchname + "%' ";
                        break;
                    case 5:
                        where += " and a.o_tradeno= '" + searchname + "' ";
                        break;
                    case 6:
                        where += " and a.o_bizcode= '" + searchname + "' ";
                        break;
                    case 7:
                        where += " and inn.l_corporatename like '%" + searchname + "%' ";
                        break;
                }
            }
            if (platformid > 0)
            {
                where += " and b.a_platform_id=" + platformid;
            }
            if (relationtype > -1)
            {
                where += " and e.relation_type=" + relationtype;
            }

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
            ViewBag.relationtype = relationtype;
            #endregion


            return View();
        }


        [LoginCheckFilter(IsCheck = false, IsRole = false)]
        public JsonResult NotifyUrl(string orderNo)
        {
            var tableName = WeekDateTime.GetOrderTableName(GetOrderDay(orderNo));
            var order = new jmp_order().FindOrderByTableNameAndOrderNoIncludeRealtime(tableName, orderNo);
            var app = new jmp_app().GetModel(order.o_app_id);
            if (app == null)
            {
                return Json(new { success = false, message = "查询应用数据时出现了问题", url = "查询应用数据时出现了问题" }, JsonRequestBehavior.AllowGet);
            }
            var quli = new JMP.MDL.jmp_queuelist
            {
                q_address = order.o_address,
                q_sign = app.a_secretkey,
                q_noticestate = 0,
                q_times = 0,
                q_noticetimes = DateTime.Now,
                q_tablename = tableName,
                q_o_id = order.o_id,
                trade_type = Int32.Parse(order.o_paymode_id),
                trade_time = order.o_ptime,
                trade_price = order.o_price,
                trade_paycode = order.o_tradeno,
                trade_code = order.o_code,
                trade_no = order.o_bizcode,
                q_privateinfo = order.o_privateinfo,
                q_uersid = 0
            };
            var url = NotifyUrl(quli);
            return Json(new { success = true, message = "查询成功", url }, JsonRequestBehavior.AllowGet);
        }

        private string GetOrderDay(string orderNo)
        {
            //20171011
            var str = orderNo.Substring(0, 8);
            str = str.Insert(6, "-").Insert(4, "-");
            return str;
        }

        #region 生成异步通知地址

        private string NotifyUrl(JMP.MDL.jmp_queuelist order)
        {
            var mark = order.q_address.Contains("?") ? "&" : "?";
            var url = order.q_address + mark + "trade_md5=" +
                      JMP.TOOL.MD5.md5strGet(order.trade_no + order.trade_code + order.trade_price + order.q_sign,
                          true) + "&trade_type=" + order.trade_type +
                      "&trade_price=" + order.trade_price + "&trade_paycode=" + order.trade_paycode + "&trade_code=" +
                      order.trade_code + "&trade_no=" + order.trade_no + "&trade_privateinfo=" +
                      System.Web.HttpUtility.UrlEncode(order.q_privateinfo, Encoding.UTF8) + "&trade_sign=" +
                      JMP.TOOL.DESEncrypt.Encrypt(DateTime.Now.ToString(CultureInfo.InvariantCulture) + "," +
                                                  order.trade_no + "," + order.q_sign, "hyx") +
                      "&trade_status=TRADE_SUCCESS&trade_time=" + DateTime.Parse(order.trade_time.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            return url;
        }

        #endregion

        /// <summary>
        /// 导出订单列表
        /// </summary>
        [LoginCheckFilter(IsCheck = true, IsRole = false)]
        public FileContentResult DcDev()
        {
            #region 查询
            string sql = "select o_id,o_code,o_bizcode,o_tradeno,o_paymode_id,o_app_id,o_goodsname,o_term_key,o_price,o_payuser,o_ctime,o_ptime,o_state,o_times,o_address,o_noticestate,o_noticetimes,o_privateinfo,o_interface_id,o_showaddress,b.a_key,b.a_name,b.a_platform_id,c.p_name,e.u_realname ,inn.l_corporatename from (";//组装查询条件
            string sql1 = "";
            string TableName = "";//表名
            string order = "order by o_ctime desc";//排序字段
            int searchType = string.IsNullOrEmpty(Request["searchType"]) ? 0 : Int32.Parse(Request["searchType"]); //查询条件选择
            string searchname = string.IsNullOrEmpty(Request["searchname"]) ? "" : Request["searchname"];//查询类容
            string stime = string.IsNullOrEmpty(Request["stime"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["stime"];//开始时间
            string etime = string.IsNullOrEmpty(Request["etime"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["etime"];//结束时间
            int paymode = string.IsNullOrEmpty(Request["paymode"]) ? 0 : Int32.Parse(Request["paymode"]);//支付类型
            string paymentstate = string.IsNullOrEmpty(Request["paymentstate"]) ? "1" : Request["paymentstate"];//支付状态
            string noticestate = string.IsNullOrEmpty(Request["noticestate"]) ? "" : Request["noticestate"];//通知状态
            int platformid = string.IsNullOrEmpty(Request["platformid"]) ? 0 : Int32.Parse(Request["platformid"]);//关联平台
            ArrayList sjfw = JMP.TOOL.WeekDateTime.WeekMonday(DateTime.Parse(stime), DateTime.Parse(etime));//根据时间返回获取每周周一组装查询语句
            //string dept = System.Configuration.ConfigurationManager.AppSettings["RoleID"];
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
            string where = "";//组装查询条件
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
                        where += " and e.u_realname like '%" + searchname + "%' ";
                        break;
                    case 5:
                        where += " and a.o_tradeno= '" + searchname + "' ";
                        break;
                    case 6:
                        where += " and a.o_bizcode= '" + searchname + "' ";
                        break;
                    case 7:
                        where += " and inn.l_corporatename like '%" + searchname + "%' ";
                        break;
                }
            }
            if (platformid > 0)
            {
                where += " and b.a_platform_id=" + platformid;
            }
            //if (UserInfo.UserRoleId == dept)
            //{
            //    where += " and e.u_merchant_id=" + UserInfo.UserId;
            //}
            // sql = sql.Remove(sql.Length - 10);//去掉最后一个UNION ALL
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
            sql += ") a left join jmp_app  b on  a.o_app_id=b.a_id left join jmp_paymode c on c.p_id=a.o_paymode_id left join jmp_user e on e.u_id=b.a_user_id left join jmp_interface inn on inn.l_id=a.o_interface_id  where 1=1  " + where + order;

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
                x.l_corporatename,
                x.o_price,
                o_state = x.o_state.ConvertPayState(),
                o_ctime = x.o_ctime.ToString("yyyy-MM-dd HH:mm:ss"),
                o_ptime = x.o_ptime.ToString("yyyy-MM-dd HH:mm:ss"),
                o_times = x.o_noticestate == 0 ? "--" : x.o_times.ToString(),
                o_noticestate = x.o_noticestate.ConvertNoticeState(x.o_state),
                o_noticetimes = x.o_noticestate != 0 ? x.o_noticetimes.ToString("yyyy-MM-dd HH:mm:ss") : "--",
                a_platform_id = x.a_platform_id == 1 ? "安卓" : x.a_platform_id == 2 ? "苹果" : "H5"
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
                ws.Cells["G1"].Value = "支付壳子名称";
                ws.Cells["H1"].Value = "支付金额";
                ws.Cells["I1"].Value = "支付状态";
                ws.Cells["J1"].Value = "创建时间";
                ws.Cells["K1"].Value = "支付时间";
                ws.Cells["L1"].Value = "通知次数";
                ws.Cells["M1"].Value = "通知状态";
                ws.Cells["N1"].Value = "通知时间";
                ws.Cells["O1"].Value = "关联平台";
                fileBytes = pck.GetAsByteArray();

            }
            string fileName = "订单列表" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            #endregion
            return File(fileBytes, "application/vnd.ms-excel", fileName);
        }
        /// <summary>
        /// 订单重发通知
        /// </summary>
        [LoginCheckFilter(IsCheck = true, IsRole = false)]
        [HttpPost]
        public JsonResult Orderrewire()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            string ordercode = string.IsNullOrEmpty(Request["code"]) ? "" : Request["code"];
            string ptime = string.IsNullOrEmpty(Request["ptime"]) ? "" : Request["ptime"];
            bool sess = Convert.ToDateTime(Session["sendtime_" + ordercode]) > System.DateTime.Now.AddMinutes(-1) ? true : false;
            if (!string.IsNullOrEmpty(ordercode) && !string.IsNullOrEmpty(ptime))
            {
                if (sess)
                {
                    retJson = new { success = 0, msg = "请间隔一分钟，再次发送！" };
                    return Json(retJson);
                }
                else
                {
                    JMP.BLL.jmp_order bll = new jmp_order();
                    JMP.MDL.jmp_order morder = new JMP.MDL.jmp_order();
                    string tabalename = "dbo.jmp_order_" + JMP.TOOL.WeekDateTime.GetWeekFirstDayMon(DateTime.Parse(ptime)).ToString("yyyyMMdd");
                    morder = bll.SelectOrder(ordercode, tabalename);
                    if (morder != null)
                    {
                        JMP.MDL.jmp_app app = new JMP.MDL.jmp_app();
                        JMP.BLL.jmp_app appbll = new JMP.BLL.jmp_app();
                        app = appbll.SelectId(morder.o_app_id);
                        if (morder.o_times > 8 && morder.o_times < 12 && app != null)
                        {
                            JMP.MDL.jmp_queuelist quli = new JMP.MDL.jmp_queuelist();
                            JMP.BLL.jmp_queuelist bllq = new jmp_queuelist();
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
                            quli.q_uersid = app.u_id;
                            int cg = bllq.Add(quli);
                            if (cg > 0)
                            {
                                Session["sendtime_" + morder.o_code] = System.DateTime.Now;
                                retJson = new { success = 1, msg = "已重发通知！手动通知次数剩余：" + (11 - morder.o_times) + "次" };
                            }
                            else
                            {
                                AddLocLog.AddLog(1, 4, Request.UserHostAddress, "管理平台手动重发通知失败", "订单号：" + morder.o_code + ",表名：" + tabalename);//写入报错日志
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
                retJson = new { success = 0, msg = "数据异常！" };
            }
            return Json(retJson);
        }
        /// <summary>
        /// 设备信息列表
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult terminalList()
        {
            List<JMP.MDL.jmp_terminal> terminallist = new List<JMP.MDL.jmp_terminal>();//终端信息集合
            JMP.BLL.jmp_terminal terminalbll = new JMP.BLL.jmp_terminal();
            string sql = " select a.* ,b.a_name  from   jmp_terminal a  left join  dx_base.dbo.jmp_app  b on  a.t_appid=b.a_id where  convert(varchar(10),t_time,120)=convert(varchar(10),'" + System.DateTime.Now.ToString("yyyy-MM-dd") + "',120)      and 1=1 ";//组装查询条件
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchType = string.IsNullOrEmpty(Request["searchType"]) ? 0 : Int32.Parse(Request["searchType"]);
            //查询条件选择
            string searchname = string.IsNullOrEmpty(Request["searchname"]) ? "" : Request["searchname"];//查询类容
            string nettype = string.IsNullOrEmpty(Request["nettype"]) ? "" : Request["nettype"];//手机网络查询
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序
            if (searchType > 0 && !string.IsNullOrEmpty(searchname))
            {
                switch (searchType)
                {
                    case 1:
                        sql += " and t_id like'%" + searchname + "%' ";
                        break;
                    case 2:
                        sql += " and t_ip like'%" + searchname + "%' ";
                        break;
                    case 3:
                        sql += " and t_brand like'%" + searchname + "%' ";
                        break;
                    case 4:
                        sql += " and t_province like'%" + searchname + "%' ";
                        break;
                    case 5:
                        sql += " and b.a_name like'%" + searchname + "%' ";
                        break;
                }
            }
            if (!string.IsNullOrEmpty(nettype))
            {
                sql += " and t_nettype='" + nettype + "' ";
            }
            string order = "";
            if (searchDesc == 0)
            {
                order = " order by t_id desc ";
            }
            else
            {
                order = " order by t_id  ";
            }
            ViewBag.searchDesc = searchDesc;
            ViewBag.searchname = searchname;
            ViewBag.searchType = searchType;
            ViewBag.nettype = nettype;
            terminallist = terminalbll.SelectList(sql, order, pageIndexs, PageSize, out pageCount);
            ViewBag.terminallist = terminallist;
            ViewBag.pageCount = pageCount;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            return View();
        }
        /// <summary>
        /// 根据关联终端唯一KEY查询设备信息
        /// </summary>
        /// <param name="o_term_key">关联终端唯一KEY</param>
        /// <returns></returns>
        public ActionResult TermainaiLstEj(string o_term_key)
        {
            jmp_terminal bll = new jmp_terminal();

            JMP.MDL.jmp_terminal mo = new JMP.MDL.jmp_terminal();
            if (!string.IsNullOrEmpty(o_term_key))
            {

                mo = JMP.TOOL.MdlList.ToModel<JMP.MDL.jmp_terminal>(bll.GetList(" 1=1 and t_key='" + o_term_key + "' ").Tables[0]);
            }
            ViewBag.mo = mo;

            return PartialView();
        }
        #region 流量分析模块 秦际攀
        /// <summary>
        /// 终端属性报表统计界面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult Terminal()
        {
            string ksrq = DateTime.Now.ToString("yyyy-MM-01");//获取本月第一天
            ViewBag.ksrq = ksrq;
            string stime = "";//开始时间;
            if (DateTime.Now.ToString("yyyyMM") == DateTime.Now.AddDays(-7).ToString("yyyyMM"))
            {
                stime = string.IsNullOrEmpty(Request["stime"]) ? DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") : Request["stime"];//开始时间
            }
            else
            {
                stime = string.IsNullOrEmpty(Request["stime"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["stime"];//开始时间
            }
            ViewBag.stime = stime;
            string etime = string.IsNullOrEmpty(Request["etime"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["etime"];//结束时间
            ViewBag.etime = etime;
            return View();
        }
        /// <summary>
        /// 统计终端属性
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public string TerminalCount(string type)
        {
            string stime = string.IsNullOrEmpty(Request["stime"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["stime"];//开始时间
            string etime = string.IsNullOrEmpty(Request["etime"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["etime"];//结束时间
            int searchType = string.IsNullOrEmpty(Request["searchType"]) ? 0 : Int32.Parse(Request["searchType"]);//查询条件
            string searchname = string.IsNullOrEmpty(Request["searchname"]) ? "" : Request["searchname"];//查询内容
            string htmls = "{\"chart\": {\"caption\": \"\",\"subCaption\": \"\",\"numberSuffix\": \"%\",\"paletteColors\": \"#0075c2\",\"bgColor\": \"FFFFFF\",\"showBorder\": \"0\",\"showCanvasBorder\": \"0\",\"usePlotGradientColor\": \"0\",\"plotBorderAlpha\": \"10\",\"placeValuesInside\": \"1\",\"valueFontColor\": \"#ffffff\",\"showAxisLines\": \"1\",\"axisLineAlpha\": \"25\",\"divLineAlpha\": \"10\",\"alignCaptionWithCanvas\": \"0\",\"showAlternateVGridColor\": \"0\",\"captionFontSize\": \"14\",\"subcaptionFontSize\": \"14\",\"subcaptionFontBold\": \"0\",\"toolTipColor\": \"#ffffff\",\"toolTipBorderThickness\": \"0\",\"toolTipBgColor\": \"#000000\",\"toolTipBgAlpha\": \"80\",\"toolTipBorderRadius\": \"2\",\"toolTipPadding\": \"5\" }";
            htmls += ",\"data\": [";
            string datastr = "";
            switch (type)
            {
                case "statistics":
                    #region 手机品牌
                    JMP.BLL.jmp_statistics jmp_statisticsbll = new JMP.BLL.jmp_statistics();
                    List<JMP.MDL.jmp_statistics> jmp_statisticslist = jmp_statisticsbll.GetListTjCount(stime, etime, searchType, searchname);
                    JMP.MDL.jmp_statistics statimodel = jmp_statisticsbll.modelCoutn(stime, etime);
                    int sjpp = 1; decimal ppbl = 0;
                    if (statimodel != null)
                    {
                        sjpp = statimodel.s_count;
                    }
                    if (jmp_statisticslist.Count > 0)
                    {
                        foreach (var item in jmp_statisticslist)
                        {

                            if (sjpp == 1)
                            {
                                ppbl = 0;
                            }
                            else
                            {
                                ppbl = (decimal.Parse(item.s_count.ToString()) / decimal.Parse(sjpp.ToString())) * 100;
                            }
                            datastr += "{\"label\": \"" + item.s_brand + "\", \"value\": \"" + String.Format("{0:N2}", ppbl) + "\"},";
                        }
                    }
                    else
                    {
                        return "0";
                    }
                    #endregion
                    break;
                case "modelnumber":
                    #region 型号
                    JMP.BLL.jmp_modelnumber modelnumberbll = new JMP.BLL.jmp_modelnumber();
                    List<JMP.MDL.jmp_modelnumber> modelnumberlist = modelnumberbll.GetListTjCount(stime, etime, searchType, searchname);
                    JMP.MDL.jmp_modelnumber xhmodel = modelnumberbll.modelTjCount(stime, etime);
                    int xxcount = 1; decimal xxbl = 0;
                    if (xhmodel != null)
                    {
                        xxcount = xhmodel.m_count;
                    }
                    if (modelnumberlist.Count > 0)
                    {
                        foreach (var mo in modelnumberlist)
                        {
                            if (xxcount == 1)
                            {
                                xxbl = 0;
                            }
                            else
                            {
                                xxbl = (decimal.Parse(mo.m_count.ToString()) / decimal.Parse(xxcount.ToString())) * 100;
                            }
                            datastr += "{\"label\": \"" + mo.m_sdkver + "\", \"value\": \"" + String.Format("{0:N2}", xxbl) + "\"},";
                        }
                    }
                    else
                    {
                        return "0";
                    }
                    #endregion
                    break;
                case "operatingsystem":
                    #region 操作系统
                    JMP.BLL.jmp_operatingsystem operatingsystembll = new JMP.BLL.jmp_operatingsystem();
                    List<JMP.MDL.jmp_operatingsystem> operatingsystemlist = operatingsystembll.GetListTjCount(stime, etime, searchType, searchname);
                    JMP.MDL.jmp_operatingsystem opmdel = operatingsystembll.ModelTjCount(stime, etime);
                    int opcount = 1; decimal xtbl = 0;
                    if (opmdel != null)
                    {
                        opcount = opmdel.o_count;
                    }
                    if (operatingsystemlist.Count > 0)
                    {
                        foreach (var op in operatingsystemlist)
                        {
                            if (opcount == 1)
                            {
                                xtbl = 0;
                            }
                            else
                            {
                                xtbl = (decimal.Parse(op.o_count.ToString()) / decimal.Parse(opcount.ToString())) * 100;
                            }
                            datastr += "{\"label\": \"" + op.o_system + "\", \"value\": \"" + String.Format("{0:N2}", xtbl) + "\"},";
                        }
                    }
                    else
                    {
                        return "0";
                    }
                    #endregion
                    break;
                case "resolution":
                    #region 分辨率
                    JMP.BLL.jmp_resolution oresolutionbll = new JMP.BLL.jmp_resolution();
                    List<JMP.MDL.jmp_resolution> resolutionlist = oresolutionbll.GetListTjCount(stime, etime, searchType, searchname);
                    JMP.MDL.jmp_resolution remodel = oresolutionbll.modelTjCount(stime, etime);
                    int recount = 1; decimal fblbl = 0;
                    if (remodel != null)
                    {
                        recount = remodel.r_count;
                    }
                    if (resolutionlist.Count > 0)
                    {
                        foreach (var re in resolutionlist)
                        {
                            if (recount == 1)
                            {
                                fblbl = 0;
                            }
                            else
                            {
                                fblbl = (decimal.Parse(re.r_count.ToString()) / decimal.Parse(recount.ToString())) * 100;
                            }
                            datastr += "{\"label\": \"" + re.r_screen + "\", \"value\": \"" + String.Format("{0:N2}", fblbl) + "\"},";
                        }
                    }
                    else
                    {
                        return "0";
                    }
                    #endregion
                    break;
                case "network":
                    #region 网络
                    JMP.BLL.jmp_network networkbll = new JMP.BLL.jmp_network();
                    List<JMP.MDL.jmp_network> networklist = networkbll.GetListTjCount(stime, etime, searchType, searchname);
                    JMP.MDL.jmp_network nemodel = networkbll.modelTjCount(stime, etime);
                    int necoutn = 1; decimal wlbl = 0;
                    if (nemodel != null)
                    {
                        necoutn = nemodel.n_count;
                    }
                    if (networklist.Count > 0)
                    {
                        foreach (var ne in networklist)
                        {
                            if (necoutn == 1)
                            {
                                wlbl = 0;
                            }
                            else
                            {
                                wlbl = (decimal.Parse(ne.n_count.ToString()) / decimal.Parse(necoutn.ToString())) * 100;
                            }
                            datastr += "{\"label\": \"" + ne.n_network + "\", \"value\": \"" + String.Format("{0:N2}", wlbl) + "\"},";
                        }
                    }
                    else
                    {
                        return "0";
                    }
                    #endregion
                    break;
                case "operator":
                    #region 运营商
                    JMP.BLL.jmp_operator operatorbll = new JMP.BLL.jmp_operator();
                    List<JMP.MDL.jmp_operator> operatorlist = operatorbll.GetListTjCount(stime, etime, searchType, searchname);
                    JMP.MDL.jmp_operator opmodel = operatorbll.modelTjCount(stime, etime);
                    int opmocount = 1; decimal yys = 0;
                    if (opmodel != null)
                    {
                        opmocount = opmodel.o_count;
                    }
                    if (operatorlist.Count > 0)
                    {
                        foreach (var ope in operatorlist)
                        {
                            if (opmocount == 1)
                            {
                                yys = 0;
                            }
                            else
                            {
                                yys = (decimal.Parse(ope.o_count.ToString()) / decimal.Parse(opmocount.ToString())) * 100;
                            }
                            datastr += "{\"label\": \"" + ope.o_nettype + "\", \"value\": \"" + String.Format("{0:N2}", yys) + "\"},";
                        }
                    }
                    else
                    {
                        return "0";
                    }
                    #endregion
                    break;
                case "province":
                    #region 省份
                    JMP.BLL.jmp_province prbll = new JMP.BLL.jmp_province();
                    List<JMP.MDL.jmp_province> prlist = prbll.GetListTjCount(stime, etime, searchType, searchname);
                    JMP.MDL.jmp_province prmode = prbll.modelTjCount(stime, etime);
                    int prcount = 1; decimal prs = 0;
                    if (prmode != null)
                    {
                        prcount = prmode.p_count;
                    }
                    if (prlist.Count > 0)
                    {
                        foreach (var ope in prlist)
                        {
                            if (prcount == 1)
                            {
                                prs = 0;
                            }
                            else
                            {
                                prs = (decimal.Parse(ope.p_count.ToString()) / decimal.Parse(prcount.ToString())) * 100;
                            }
                            datastr += "{\"label\": \"" + ope.p_province + "\", \"value\": \"" + String.Format("{0:N2}", prs) + "\"},";
                        }
                    }
                    else
                    {
                        return "0";
                    }
                    #endregion
                    break;

            }
            if (datastr.Length > 0)
            {
                datastr = datastr.Remove(datastr.Length - 1);//去掉最后一个“，”号
            }
            htmls += datastr;
            htmls += "]}";
            return htmls;
        }
        /// <summary>
        /// 流量走势报表统计界面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult Trends()
        {
            string ksrq = DateTime.Now.ToString("yyyy-MM-01");//获取本月第一天
            ViewBag.ksrq = ksrq;
            string stime = "";//开始时间;
            if (DateTime.Now.ToString("yyyyMM") == DateTime.Now.AddDays(-7).ToString("yyyyMM"))
            {
                stime = string.IsNullOrEmpty(Request["stime"]) ? DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") : Request["stime"];//开始时间
            }
            else
            {
                stime = string.IsNullOrEmpty(Request["stime"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["stime"];//开始时间
            }
            ViewBag.stime = stime;
            string etime = string.IsNullOrEmpty(Request["etime"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["etime"];//结束时间
            ViewBag.etime = etime;
            return View();
        }
        /// <summary>
        /// 流量走势报表统计
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public string TrendsCount()
        {
            string stime = string.IsNullOrEmpty(Request["stime"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["stime"];//开始时间
            string etime = string.IsNullOrEmpty(Request["etime"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["etime"];//结束时间
            int searchType = string.IsNullOrEmpty(Request["searchType"]) ? 0 : Int32.Parse(Request["searchType"]);//查询条件
            string searchname = string.IsNullOrEmpty(Request["searchname"]) ? "" : Request["searchname"];//查询内容
            string htmls = "{";
            htmls += "\"chart\":{\"caption\":\"\",\"numberprefix\":\"\",\"plotgradientcolor\":\"\",\"bgcolor\":\"ffffff\",\"showalternatehgridcolor\":\"0\",\"divlinecolor\":\"cccccc\",\"showvalues\":\"0\",\"showcanvasborder\":\"0\",\"canvasborderalpha\":\"0\",\"canvasbordercolor\":\"cccccc\",\"canvasborderthickness\":\"1\",\"yaxismaxvalue\":\"\",\"captionpadding\":\"50\",\"linethickness\":\"3\",\"yaxisvaluespadding\":\"30\",\"legendshadow\":\"0\",\"legendborderalpha\":\"0\",\"palettecolors\":\"#f8bd19,#008ee4,#33bdda,#e44a00,#6baa01,#583e78\",\"showborder\":\"0\",\"toolTipSepChar\":\"\",\"formatNumberScale\":\"0\"},";
            string where = "1=1";
            JMP.BLL.jmp_trends blltrends = new JMP.BLL.jmp_trends();
            if (!string.IsNullOrEmpty(stime))
            {
                where += " and convert(varchar(10),t_time,120)>=convert(varchar(10),'" + stime + "',120) ";
            }
            if (!string.IsNullOrEmpty(etime))
            {
                where += " and convert(varchar(10),t_time,120)<=convert(varchar(10),'" + etime + "',120) ";
            }
            where += " order by t_time  ";
            DataTable dt = blltrends.GetListDataTable(stime, etime, searchType, searchname);
            if (dt.Rows.Count > 0)
            {
                #region 组装json格式
                htmls += "\"categories\": [";
                htmls += "{";
                htmls += "\"category\": [";
                //构建横坐标以天为单位
                DateTime ksstime = DateTime.Parse(stime);
                DateTime jsetime = DateTime.Parse(etime);
                int zbcs = Int32.Parse((DateTime.Parse(etime) - DateTime.Parse(stime)).TotalDays.ToString());
                for (int j = 0; j <= zbcs; j++)
                {
                    htmls += "{\"label\": \"" + ksstime.ToString("yyyy-MM-dd") + "\"},";
                    ksstime = ksstime.AddDays(1);
                }
                htmls = htmls.TrimEnd(',');
                htmls += "]";
                htmls += "}";
                htmls += "],";
                #region 构建数据
                htmls += "\"dataset\": [";
                htmls += "{";
                htmls += "\"seriesName\":\"每日新增用户\",";
                htmls += "\"data\": [";
                string hyhtml = "{\"seriesName\":\"每日活跃用户\",\"data\": [";//组装每日活跃用户字符串
                if ((zbcs + 1) == dt.Rows.Count)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        htmls += "{\"value\": \"" + dt.Rows[i]["t_newcount"] + "\"},";
                        hyhtml += "{\"value\": \"" + dt.Rows[i]["t_activecount"] + "\"},";
                    }
                }
                else
                {
                    DateTime ksstimes = DateTime.Parse(stime);
                    for (int m = 0; m <= zbcs; m++)
                    {
                        DataRow[] ddt = dt.Select("t_time>='" + ksstimes.ToString("yyyy-MM-dd") + " 00:00:00" + "' and t_time<='" + ksstimes.ToString("yyyy-MM-dd") + " 23:59:59" + "'");
                        if (ddt.Length == 1)
                        {
                            htmls += "{\"value\": \"" + ddt[0]["t_newcount"] + "\"},";
                            hyhtml += "{\"value\": \"" + ddt[0]["t_activecount"] + "\"},";
                        }
                        else
                        {
                            htmls += "{\"value\": \"0\"},";
                            hyhtml += "{\"value\": \"0\"},";
                        }
                        ksstimes = ksstimes.AddDays(1);
                    }
                }
                htmls = htmls.TrimEnd(',');
                hyhtml = hyhtml.TrimEnd(',');
                hyhtml += "]}";
                htmls += "]";
                htmls += "},";
                htmls = htmls + hyhtml;
                htmls += "]";
                htmls += "}";
                #endregion
                #endregion

            }
            else
            {
                htmls = "0";
            }
            return htmls;
        }
        /// <summary>
        /// 支付渠道汇总
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult paychannel(string rtype)
        {

            JMP.BLL.jmp_paymode paymodebll = new JMP.BLL.jmp_paymode();
            List<JMP.MDL.jmp_paymode> paymodeList = paymodebll.GetModelList("1=1 and p_state='1' ");//支付类型
            ViewBag.paymodeList = paymodeList;
            rtype = !string.IsNullOrEmpty(rtype) ? rtype : "total";
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量            
            int types = string.IsNullOrEmpty(Request["s_type"]) ? 0 : int.Parse(Request["s_type"]);
            string searchKey = string.IsNullOrEmpty(Request["s_key"]) ? "" : Request["s_key"];
            string stime = string.IsNullOrEmpty(Request["s_begin"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_begin"];//开始日期
            string etime = string.IsNullOrEmpty(Request["s_end"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_end"];//结束日期
            int sort = string.IsNullOrEmpty(Request["s_sort"]) ? 1 : int.Parse(Request["s_sort"]);//排序

            string daytime = DateTime.Now.ToString("yyyy-MM-dd") + " " + (string.IsNullOrEmpty(Request["daytime"]) ? "00:00:00" : Request["daytime"]);
            ViewBag.daytime = (string.IsNullOrEmpty(Request["daytime"]) ? "00:00:00" : Request["daytime"]);
            string enddaytime = DateTime.Now.ToString("yyyy-MM-dd") + " " + (string.IsNullOrEmpty(Request["enddaytime"]) ? "23:59:59" : Request["enddaytime"]);
            ViewBag.enddaytime = (string.IsNullOrEmpty(Request["enddaytime"]) ? "23:59:59" : Request["enddaytime"]);

            string paymode = string.IsNullOrEmpty(Request["paymode"]) ? "" : (Request["paymode"]);//支付类型
            string order = "";//排序
            string sqlcount = "";//查询总数sql语句
            string sql = "";//查询语句
            string where = "";
            List<JMP.MDL.jmp_paychannel> list = new List<JMP.MDL.jmp_paychannel>();
            JMP.BLL.jmp_paychannel bll = new JMP.BLL.jmp_paychannel();
            DataTable dt = new DataTable();
            int num = 0;


            if (rtype == "total")
            {
                if (types == 1 && !string.IsNullOrEmpty(searchKey))
                {
                    where += " and payname like '%" + searchKey + "%' ";

                }
                if (!string.IsNullOrEmpty(paymode))
                {
                    where += " and paytype = '" + paymode + "' ";
                }

                num = 0;

                #region 查询汇总
                //查询
                sql = string.Format(@"select a.id,a.payid,payname,[money],datetimes,paytype,success,successratio,notpay,ISNULL(ChannelCostRatio,0) as ChannelCostRatio,SUM(ISNULL(ChannelCostFee,0)) as ChannelCostFee,SUM(ISNULL(TotalAmount, 0) - ISNULL(ChannelCostFee, 0)) as SettlementAmount  from jmp_paychannel as a left join CoSettlementDeveloperAppDetails b on a.payid = b.ChannelId and CONVERT(varchar(10),a.datetimes,120)=b.SettlementDay where 1=1  and datetimes>='" + stime + " 00:00:00' and datetimes<='" + etime + " 23:59:59' {0} group by a.id,a.payid,payname,[money],datetimes,paytype,success,successratio,notpay,ChannelCostRatio", where);
                //合计
                sqlcount = string.Format(@"select SUM(money) as [money],SUM(ChannelCostFee) as ChannelCostFee,SUM(SettlementAmount) as SettlementAmount,SUM(success) as success,SUM(notpay) as notpay from(select a.id, payname,[money], datetimes, success, notpay,SUM(ISNULL(ChannelCostFee,0)) as ChannelCostFee,SUM(ISNULL(TotalAmount, 0) - ISNULL(ChannelCostFee, 0)) as SettlementAmount from jmp_paychannel as a left join CoSettlementDeveloperAppDetails b on a.payid = b.ChannelId and CONVERT(varchar(10), a.datetimes, 120) = b.SettlementDay where 1=1  and datetimes>='" + stime + " 00:00:00' and datetimes<='" + etime + " 23:59:59' {0} group by a.id, payname,[money], datetimes, paytype, success, successratio, notpay, ChannelCostRatio) as a", where);
                order = sort == 1 ? "order by id desc " : " order by id asc ";


                list = bll.GetLists(sql, order, pageIndexs, PageSize, out pageCount);
                if (list.Count > 0)
                {
                    dt = bll.CountSect(sqlcount);
                }
                #endregion
            }
            else
            {
                if (types == 1 && !string.IsNullOrEmpty(searchKey))
                {
                    where += " and ChannelName like '%" + searchKey + "%' ";

                }
                if (!string.IsNullOrEmpty(paymode))
                {
                    where += " and PayTypeName = '" + paymode + "' ";
                }

                num = 1;
                #region 查询今日

                sql = string.Format(@"select  ChannelId as payid,ChannelName as payname,PayTypeName as paytype,GETDATE() as datetimes,isnull(SUM([Money]),0) [money], cast ((isnull(SUM(Notpay),0)) as int) notpay, cast(isnull(SUM(Success), 0) as int) success, CONVERT(DECIMAL(16,2), ISNULL((case when (SUM(Success)+SUM(Notpay))=0 then 0 else ((SUM(Success))/(SUM(Success)+SUM(Notpay)))end),0)) successratio  from jmp_AppChannelMinuteReport WHERE CreatedDate>='" + daytime + "' AND CreatedDate<'" + enddaytime + "' {0} ", where);

                sqlcount = string.Format(@" select cast(sum(Success) as int )success, cast(SUM(Notpay) as int) notpay, sum(Success + Notpay)a_count,SUM([Money])[money],CONVERT(DECIMAL(16,2),(case when sum(Success + Notpay)=0 then 0 else (sum(Success*1.0)/ sum(Success + Notpay))end)) a_successratio from jmp_AppChannelMinuteReport where CreatedDate>='" + daytime + "' and CreatedDate<'" + enddaytime + "' {0} ", where);

                order = sort == 1 ? "order by payid desc " : " order by payid asc ";

                sql += "  GROUP BY ChannelId,ChannelName,PayTypeName  ";
                list = bll.GetLists(sql, order, pageIndexs, PageSize, out pageCount);
                if (list.Count > 0)
                {
                    dt = bll.CountSect(sqlcount);
                }
                #endregion
            }
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.paymode = paymode;
            ViewBag.list = list;
            ViewBag.dt = dt;
            ViewBag.num = num;
            return View();
        }
        #endregion

        /// <summary>
        ///应用投诉管理
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false), VisitRecord(IsRecord = true)]
        public ActionResult ComplaintList()
        {
            int UserDept = UserInfo.UserRoleId;
            string UserId = UserInfo.UserId.ToString();
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            string auditstate = string.IsNullOrEmpty(Request["auditstate"]) ? "" : Request["auditstate"];//审核状态
            int type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);//查询条件选择
            string stime = string.IsNullOrEmpty(Request["r_begin"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["r_begin"];
            string etime = string.IsNullOrEmpty(Request["r_end"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["r_end"];
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            List<JMP.MDL.jmp_complaint> list = new List<JMP.MDL.jmp_complaint>();
            JMP.BLL.jmp_complaint bll = new JMP.BLL.jmp_complaint();
            //  int dept = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RoleID"]);
            list = bll.SelectList(UserDept, UserId, auditstate, sea_name, type, searchDesc, stime, etime, pageIndexs, PageSize, out pageCount, 0);
            ViewBag.searchDesc = searchDesc;
            ViewBag.type = type;
            ViewBag.sea_name = sea_name;
            ViewBag.stime = stime;
            ViewBag.etime = etime;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            ViewBag.auditstate = auditstate;
            ViewBag.locUrl = GetVoidHtml();
            return View();
        }

        public string GetVoidHtml()
        {
            string locUrl = "";
            string u_id = UserInfo.UserId.ToString();
            int r_id = int.Parse(UserInfo.UserRoleId.ToString());
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/REPORT/InsertUpdateComplaint", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加管理员
            if (getlocuserAdd)
            {
                locUrl += "<li onclick=\"AddAPPlog()\"><i class='fa fa-plus'></i>添加投诉</li>";
            }
            var bulkAssignToMerchant = bll_limit.GetLocUserLimitVoids("/appuser/bulkassign", u_id, r_id);
            if (bulkAssignToMerchant)
            {
                locUrl += "<li onclick=\"bulkassign()\"><i class='fa fa-check-square-o'></i>处理</li>";
            }
            return locUrl;
        }



        /// <summary>
        /// 导出应用投诉信息
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public FileContentResult ComplaintDc()
        {
            #region 查询
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            string auditstate = string.IsNullOrEmpty(Request["auditstate"]) ? "" : Request["auditstate"];//审核状态
            int type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);//查询条件选择
            string stime = string.IsNullOrEmpty(Request["r_begin"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["r_begin"];
            string etime = string.IsNullOrEmpty(Request["r_end"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["r_end"];
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            string sql = string.Format("select a.c_id,a.c_appid,a.c_userid,a.c_payid,a.c_tradeno,a.c_code,a.c_money,a.c_times,a.c_datimes,a.c_tjtimes,a.c_tjname,a.c_clname,a.c_cltimes,a.c_reason,a.c_result,a.c_state, d.l_corporatename, c.a_name, f.u_realname from jmp_complaint a left join jmp_interface d on a.c_payid = d.l_id left join jmp_app c  on a.c_appid = c.a_id left join jmp_user as f on a.c_userid = f.u_id where 1=1");
            string Order = " Order by c_id desc";
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        sql += "  and c.a_name like '%" + sea_name + "%' ";
                        break;
                    case 2:
                        sql += " and f.u_realname like '%" + sea_name + "%' ";
                        break;
                    case 3:
                        sql += " and d.l_corporatename like  '%" + sea_name + "%' ";
                        break;
                    case 4:
                        sql += " and a.c_tradeno ='" + sea_name + "' ";
                        break;
                    case 5:
                        sql += " and a.c_tjname =  '" + sea_name + "' ";
                        break;
                    case 6:
                        sql += " and a.c_clname =  '" + sea_name + "' ";
                        break;

                }

            }
            if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
            {
                sql += " and a.c_tjtimes>='" + stime + " 00:00:00' and a.c_tjtimes<='" + etime + " 23:59:59' ";
            }
            if (!string.IsNullOrEmpty(auditstate))
            {
                sql += " and a.c_state='" + auditstate + "' ";
            }
            // string dept = System.Configuration.ConfigurationManager.AppSettings["RoleID"];
            //if (UserInfo.UserRoleId == dept)
            //{
            //    sql += " and  f.u_merchant_id=" + int.Parse(UserInfo.UserId);
            //}
            if (searchDesc == 1)
            {
                Order = " order by c_id  ";
            }
            else
            {
                Order = " order by c_id desc ";
            }
            sql += Order;
            List<JMP.MDL.jmp_complaint> list = new List<JMP.MDL.jmp_complaint>();
            JMP.BLL.jmp_complaint orderbll = new JMP.BLL.jmp_complaint();
            list = orderbll.DcSelectList(sql);
            var lst = list.Select(x => new
            {
                x.a_name,
                x.u_realname,
                x.l_corporatename,
                x.c_tradeno,
                x.c_code,
                x.c_money,
                c_times = x.c_times.ToString("yyyy-MM-dd HH:mm:ss"),
                c_datimes = x.c_datimes.ToString("yyyy-MM-dd HH:mm:ss"),
                c_reason = string.IsNullOrEmpty(x.c_reason) ? "--" : x.c_reason.ToString(),
                c_state = Convert.ToInt32(x.c_state) == 0 ? "未处理" : "已处理",
                x.c_tjname,
                x.c_clname,
                c_result = string.IsNullOrEmpty(x.c_result) ? "--" : x.c_result.ToString(),
                c_cltimes = !string.IsNullOrEmpty(x.c_clname) ? x.c_cltimes.ToString("yyyy-MM-dd HH:mm:ss") : ""
            });
            var caption = "应用投诉管理";
            byte[] fileBytes;
            //命名导出表格的StringBuilder变量
            using (var pck = new ExcelPackage())
            {
                var ws = pck.Workbook.Worksheets.Add(caption);
                ws.Cells["A1"].LoadFromCollection(lst, false);
                ws.InsertRow(1, 1);
                ws.Cells["A1"].Value = "所属应用";
                ws.Cells["B1"].Value = "所属用户";
                ws.Cells["C1"].Value = "支付渠道";
                ws.Cells["D1"].Value = "交易流水号";
                ws.Cells["E1"].Value = "订单编号";
                ws.Cells["F1"].Value = "付款金额";
                ws.Cells["G1"].Value = "付款时间";
                ws.Cells["H1"].Value = "投诉时间";
                ws.Cells["I1"].Value = "投诉原因";
                ws.Cells["J1"].Value = "状态";
                ws.Cells["K1"].Value = "提交人";
                ws.Cells["L1"].Value = "处理人";
                ws.Cells["M1"].Value = "处理结果";
                ws.Cells["N1"].Value = "处理时间";
                fileBytes = pck.GetAsByteArray();

            }
            string fileName = "应用投诉管理" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            #endregion
            return File(fileBytes, "application/vnd.ms-excel", fileName);
        }

        /// <summary>
        /// 添加应用投诉
        /// </summary>
        /// <returns></returns>
        public ActionResult ComplaintAdd()
        {
            int c_id = string.IsNullOrEmpty(Request["c_id"]) ? 0 : Int32.Parse(Request["c_id"]);
            JMP.BLL.jmp_complaint bll = new JMP.BLL.jmp_complaint();
            JMP.MDL.jmp_complaint model = new JMP.MDL.jmp_complaint();
            if (c_id > 0)
            {
                model = bll.SelectId(c_id);
            }
            ViewBag.model = model == null ? new JMP.MDL.jmp_complaint() : model;
            return View();
        }
        /// <summary>
        /// 添加或修改应用投诉管理
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult InsertUpdateComplaint(JMP.MDL.jmp_complaint model)
        {
            object retJson = new { success = 0, msg = "操作失败" };
            JMP.BLL.jmp_complaint bll = new JMP.BLL.jmp_complaint();

            model.c_cltimes = DateTime.Now;
            if (model.c_id > 0)
            {
                // 修改应用投诉管理
                JMP.MDL.jmp_complaint modComplaint = new JMP.MDL.jmp_complaint();
                modComplaint = bll.GetModel(model.c_id);
                model.c_result = modComplaint.c_result;
                model.c_tjname = modComplaint.c_tjname;
                model.c_clname = modComplaint.c_clname;
                model.c_cltimes = modComplaint.c_cltimes;



                if (bll.Update(model))
                {
                    #region 日志说明


                    #endregion

                    Logger.ModifyLog("修改应用投诉信息", modComplaint, model);
                    retJson = new { success = 1, msg = "修改成功" };
                }
                else
                {
                    retJson = new { success = 0, msg = "修改失败" };
                }

            }
            else
            {
                model.c_tjtimes = DateTime.Now;
                model.c_state = "0";
                model.c_tjname = UserInfo.UserName;

                int cg = bll.Add(model);
                if (cg > 0)
                {

                    Logger.CreateLog("添加应用投诉", model);
                    retJson = new { success = 1, msg = "添加成功" };

                }
                else
                {
                    retJson = new { success = 1, msg = "添加失败" };
                }

            }
            return Json(retJson);
        }


        /// <summary>
        /// 支付配置列表弹窗
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false), VisitRecord(IsRecord = true)]
        public ActionResult InterfaceListTC()
        {
            #region  查询
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 10 : Int32.Parse(Request["PageSize"]);//每页显示数量
            string sql = " select a .l_id, a .l_str, a .l_sort, a .l_isenable,a.l_apptypeid,a.l_corporatename, a .l_paymenttype_id,b.p_name,b.p_type,b.p_extend,c.p_name as zflxname   from  jmp_interface a  left join jmp_paymenttype b on b.p_id=a.l_paymenttype_id left join jmp_paymode c on c.p_id=b.p_type where 1=1 ";
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            int SelectState = string.IsNullOrEmpty(Request["SelectState"]) ? 1 : Int32.Parse(Request["SelectState"]);//状态
            int auditstate = string.IsNullOrEmpty(Request["auditstate"]) ? 0 : Int32.Parse(Request["auditstate"]);//支付类型
            int type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);//查询条件选择
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        sql += " and a.l_id='" + sea_name + "' ";
                        break;
                    case 2:
                        sql += " and b.p_name='" + sea_name + "' ";
                        break;
                    case 3:
                        sql += " and a.l_corporatename like'%" + sea_name + "%' ";
                        break;
                }
            }
            if (SelectState > -1)
            {
                sql += " and a.l_isenable='" + SelectState + "' ";
            }
            if (auditstate > 0)
            {
                sql += " and c.p_id='" + auditstate + "' ";
            }
            string Order = "order by l_id";
            if (searchDesc == 1)
            {
                Order = "  order by l_id  ";
            }
            else
            {
                Order = " order by l_id desc ";
            }
            JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
            List<JMP.MDL.jmp_interface> list = new List<JMP.MDL.jmp_interface>();
            list = bll.SelectList(sql, Order, pageIndexs, PageSize, out pageCount);
            ViewBag.list = list;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.searchDesc = searchDesc;
            ViewBag.SelectState = SelectState;
            ViewBag.type = type;
            ViewBag.auditstate = auditstate;
            ViewBag.sea_name = sea_name;
            #endregion
            return View();
        }

        /// <summary>
        /// 处理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ComplaintCL(string rid)
        {
            ViewBag.uids = rid;
            return View();
        }
        /// <summary>
        /// 处理方法
        /// </summary>
        /// <returns></returns>

        public JsonResult ComplaintCLJG()
        {
            object retJson = new { success = 0, msg = "审核失败" };
            var rid = Request["rid"] ?? "";
            var r_remark = Request["remark"] ?? "";
            var r_auditor = UserInfo.UserName;
            if (rid.Length <= 0)
            {
                retJson = new { success = 0, msg = "参数错误" };
                return Json(retJson);
            }
            if (rid.CompareTo("On") > 0)
            {
                rid = rid.Substring(3);
            }
            var bll = new jmp_complaint();
            var success = bll.ComplaintLC(rid, r_remark, r_auditor);
            if (success)
            {

                Logger.OperateLog("应用投诉处理", "应用投诉id为：" + rid + "处理内容为：" + r_remark);
                retJson = new { success = 1, msg = "处理成功" };
            }
            else
            {
                retJson = new { success = 0, msg = "处理失败" };
            }

            return Json(retJson);

        }



        /// <summary>
        /// 订单异常核查管理
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult OrderAuditList()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            string paymentstatus = string.IsNullOrEmpty(Request["paymentstatus"]) ? "" : Request["paymentstatus"];//支付状态
            string auditstate = string.IsNullOrEmpty(Request["auditstate"]) ? "" : Request["auditstate"];//处理状态
            int type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);//查询条件选择
            string stime = string.IsNullOrEmpty(Request["r_begin"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["r_begin"];
            string etime = string.IsNullOrEmpty(Request["r_end"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["r_end"];
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            List<JMP.MDL.jmp_order_audit> list = new List<JMP.MDL.jmp_order_audit>();
            JMP.BLL.jmp_order_audit bll = new JMP.BLL.jmp_order_audit();
            list = bll.SelectList(paymentstatus, auditstate, sea_name, type, searchDesc, stime, etime, pageIndexs, PageSize, out pageCount);
            ViewBag.searchDesc = searchDesc;
            ViewBag.type = type;
            ViewBag.sea_name = sea_name;
            ViewBag.stime = stime;
            ViewBag.etime = etime;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            ViewBag.auditstate = auditstate;
            ViewBag.locUrl = GetVoidOrderAuditHtml();
            return View();
        }
        public string GetVoidOrderAuditHtml()
        {
            string locUrl = "";
            int r_id = int.Parse(UserInfo.UserRoleId.ToString());
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/REPORT/InsertUpdateComplaint", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加管理员           
            var bulkAssignToMerchant = bll_limit.GetLocUserLimitVoids("/appuser/bulkassign", UserInfo.UserId.ToString(), r_id);
            if (bulkAssignToMerchant)
            {
                locUrl += "<li onclick=\"bulkassign()\"><i class='fa fa-check-square-o'></i>处理</li>";
            }
            return locUrl;
        }

        /// <summary>
        /// 处理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderAuditCL(string rid)
        {
            ViewBag.uids = rid;
            return View();
        }
        /// <summary>
        /// 订单异常处理页面
        /// </summary>
        /// <returns></returns>
        public JsonResult OrderAuditJG()
        {
            object retJson = new { success = 0, msg = "处理失败" };
            var rid = Request["rid"] ?? "";
            var r_remark = Request["remark"] ?? "";
            var r_auditor = UserInfo.UserName;
            if (rid.Length <= 0)
            {
                retJson = new { success = 0, msg = "参数错误" };
                return Json(retJson);
            }
            if (rid.CompareTo("On") > 0)
            {
                rid = rid.Substring(3);
            }
            var bll = new jmp_order_audit();
            var success = bll.OrderAuditLC(rid, r_remark, r_auditor);
            if (success)
            {

                Logger.OperateLog("订单异常处理", "订单异常处理id为：" + rid + "处理内容为：" + r_remark);
                retJson = new { success = 1, msg = "处理成功" };
            }
            else
            {
                retJson = new { success = 0, msg = "处理失败" };
            }

            return Json(retJson);

        }

        /// <summary>
        /// 导出订单异常核查信息
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public FileContentResult OrderAuditDc()
        {
            #region 查询
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            string paymentstatus = string.IsNullOrEmpty(Request["paymentstatus"]) ? "" : Request["paymentstatus"];//支付状态
            string auditstate = string.IsNullOrEmpty(Request["auditstate"]) ? "" : Request["auditstate"];//审核状态
            int type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);//查询条件选择
            string stime = string.IsNullOrEmpty(Request["r_begin"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["r_begin"];
            string etime = string.IsNullOrEmpty(Request["r_end"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["r_end"];
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            string sql = string.Format("select a.* ,b.a_name from  jmp_order_audit a  left join jmp_app b on a.app_id=b.a_id where 1=1");
            string Order = " Order by id desc";
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        sql += "  and b.a_name like '%" + sea_name + "%' ";
                        break;
                    case 2:
                        sql += "  and a.order_code like '%" + sea_name + "%' ";
                        break;
                    case 3:
                        sql += "  and a.order_table_name like '%" + sea_name + "%' ";
                        break;
                    case 4:
                        sql += "  and b.trade_no like '%" + sea_name + "%' ";
                        break;
                    case 5:
                        sql += "  and b.processed_by like '%" + sea_name + "%' ";
                        break;
                }

            }
            if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
            {
                sql += " and a.created_on>='" + stime + " 00:00:00' and a.created_on<='" + etime + " 23:59:59' ";
            }
            if (!string.IsNullOrEmpty(auditstate))
            {
                sql += " and a.is_processed='" + auditstate + "' ";
            }
            if (!string.IsNullOrEmpty(paymentstatus))
            {
                sql += " and a.payment_status='" + paymentstatus + "' ";
            }
            if (searchDesc == 1)
            {
                Order = " order by id  ";
            }
            else
            {
                Order = " order by id desc ";
            }
            sql += Order;
            List<JMP.MDL.jmp_order_audit> list = new List<JMP.MDL.jmp_order_audit>();
            JMP.BLL.jmp_order_audit orderbll = new JMP.BLL.jmp_order_audit();
            list = orderbll.DcSelectList(sql);
            var lst = list.Select(x => new
            {
                x.a_name,
                x.order_code,
                x.order_table_name,
                x.message,
                x.trade_no,
                payment_time = x.payment_time.ToString("yyyy-MM-dd HH:mm:ss"),
                x.payment_amount,
                payment_status = Convert.ToInt32(x.payment_status) == 0 ? "新建" : "已处理",
                x.order_amount,
                created_on = x.created_on.ToString("yyyy-MM-dd HH:mm:ss"),
                is_processed = Convert.ToInt32(x.is_processed) == 0 ? "未处理" : "已处理",
                processed_time = !string.IsNullOrEmpty(x.processed_by) ? Convert.ToDateTime(x.processed_time).ToString("yyyy-MM-dd HH:mm:ss") : "",
                x.processed_by,
                x.processed_result
            });
            var caption = "订单异常核查管理";
            byte[] fileBytes;
            //命名导出表格的StringBuilder变量
            using (var pck = new ExcelPackage())
            {
                var ws = pck.Workbook.Worksheets.Add(caption);
                ws.Cells["A1"].LoadFromCollection(lst, false);
                ws.InsertRow(1, 1);
                ws.Cells["A1"].Value = "所属应用";
                ws.Cells["B1"].Value = "订单编号";
                ws.Cells["C1"].Value = "订单表名";
                ws.Cells["D1"].Value = "错误消息";
                ws.Cells["E1"].Value = "交易流水号";
                ws.Cells["F1"].Value = "付款时间";
                ws.Cells["G1"].Value = "实际支付金额";
                ws.Cells["H1"].Value = "支付状态";
                ws.Cells["I1"].Value = "订单金额";
                ws.Cells["J1"].Value = "创建时间";
                ws.Cells["K1"].Value = "是否已处理";
                ws.Cells["L1"].Value = "处理时间";
                ws.Cells["M1"].Value = "处理者";
                ws.Cells["N1"].Value = "处理结果";
                fileBytes = pck.GetAsByteArray();

            }
            string fileName = "订单异常核查管理" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            #endregion
            return File(fileBytes, "application/vnd.ms-excel", fileName);
        }
        /// <summary>
        /// 应用请求核查管理
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult AppRequestAuditList()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式            
            string auditstate = string.IsNullOrEmpty(Request["auditstate"]) ? "" : Request["auditstate"];//处理状态
            string typeclass = string.IsNullOrEmpty(Request["typeclass"]) ? "" : Request["typeclass"];//类型
            int type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);//查询条件选择
            string stime = string.IsNullOrEmpty(Request["r_begin"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["r_begin"];
            string etime = string.IsNullOrEmpty(Request["r_end"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["r_end"];
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            List<JMP.MDL.jmp_app_request_audit> list = new List<JMP.MDL.jmp_app_request_audit>();
            JMP.BLL.jmp_app_request_audit bll = new JMP.BLL.jmp_app_request_audit();
            list = bll.SelectList(typeclass, auditstate, sea_name, type, searchDesc, stime, etime, pageIndexs, PageSize, out pageCount);
            ViewBag.searchDesc = searchDesc;
            ViewBag.type = type;
            ViewBag.sea_name = sea_name;
            ViewBag.stime = stime;
            ViewBag.etime = etime;
            ViewBag.typeclass = typeclass;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            ViewBag.auditstate = auditstate;
            ViewBag.locUrl = GetVoidOrderAuditHtml();
            return View();
        }

        /// <summary>
        /// 处理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AppRequestAuditCL(string rid)
        {
            ViewBag.uids = rid;
            return View();
        }
        /// <summary>
        /// 应用异常处理页面
        /// </summary>
        /// <returns></returns>
        public JsonResult AppRequestAuditJG()
        {
            object retJson = new { success = 0, msg = "处理失败" };
            var rid = Request["rid"] ?? "";
            var r_remark = Request["remark"] ?? "";
            var r_auditor = UserInfo.UserName;
            if (rid.Length <= 0)
            {
                retJson = new { success = 0, msg = "参数错误" };
                return Json(retJson);
            }
            if (rid.CompareTo("On") > 0)
            {
                rid = rid.Substring(3);
            }
            var bll = new jmp_app_request_audit();
            var success = bll.OrderAuditLC(rid, r_remark, r_auditor);
            if (success)
            {

                Logger.OperateLog("监控核查处理", "监控核查处理id为：" + rid + "处理内容为：" + r_remark);
                retJson = new { success = 1, msg = "处理成功" };
            }
            else
            {
                retJson = new { success = 0, msg = "处理失败" };
            }

            return Json(retJson);

        }
        /// <summary>
        /// 导出应用异常核查信息
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public FileContentResult AppRequestAuditDc()
        {
            #region 查询
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            string auditstate = string.IsNullOrEmpty(Request["auditstate"]) ? "" : Request["auditstate"];//审核状态
            int type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);//查询条件选择
            string stime = string.IsNullOrEmpty(Request["r_begin"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["r_begin"];
            string etime = string.IsNullOrEmpty(Request["r_end"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["r_end"];
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            string sql = string.Format("select * from  jmp_app_request_audit where 1=1");
            string Order = " Order by id desc";
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        sql += "  and app_name like '%" + sea_name + "%' ";
                        break;
                    case 2:
                        sql += "  and b.processed_by like '%" + sea_name + "%' ";
                        break;
                }

            }
            if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
            {
                sql += " and created_on>='" + stime + " 00:00:00' and created_on<='" + etime + " 23:59:59' ";
            }
            if (!string.IsNullOrEmpty(auditstate))
            {
                sql += " and is_processed='" + auditstate + "' ";
            }
            if (searchDesc == 1)
            {
                Order = " order by id  ";
            }
            else
            {
                Order = " order by id desc ";
            }
            sql += Order;
            List<JMP.MDL.jmp_app_request_audit> list = new List<JMP.MDL.jmp_app_request_audit>();
            JMP.BLL.jmp_app_request_audit orderbll = new JMP.BLL.jmp_app_request_audit();
            list = orderbll.DcSelectList(sql);
            var lst = list.Select(x => new
            {
                x.app_name,
                x.message,
                created_on = x.created_on.ToString("yyyy-MM-dd HH:mm:ss"),
                is_processed = Convert.ToInt32(x.is_processed) == 0 ? "未处理" : "已处理",
                processed_time = !string.IsNullOrEmpty(x.processed_by) ? Convert.ToDateTime(x.processed_time).ToString("yyyy-MM-dd HH:mm:ss") : "",
                x.processed_by,
                x.processed_result
            });
            var caption = "应用异常核查管理";
            byte[] fileBytes;
            //命名导出表格的StringBuilder变量
            using (var pck = new ExcelPackage())
            {
                var ws = pck.Workbook.Worksheets.Add(caption);
                ws.Cells["A1"].LoadFromCollection(lst, false);
                ws.InsertRow(1, 1);
                ws.Cells["A1"].Value = "所属应用";
                ws.Cells["B1"].Value = "错误此消息";
                ws.Cells["C1"].Value = "创建时间";
                ws.Cells["D1"].Value = "是否处理";
                ws.Cells["E1"].Value = "处理时间";
                ws.Cells["F1"].Value = "处理者";
                ws.Cells["G1"].Value = "处理结果";
                fileBytes = pck.GetAsByteArray();

            }
            string fileName = "应用异常核查管理" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            #endregion
            return File(fileBytes, "application/vnd.ms-excel", fileName);
        }

        #region  应用监控管理
        /// <summary>
        ///应用监控管理
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = true, IsRole = false), VisitRecord(IsRecord = true)]
        public ActionResult AppmonitorList()
        {
            int pageCount;
            var pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            var pageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            var searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            var type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);//查询条件选择
            var selectState = string.IsNullOrEmpty(Request["SelectState"]) ? "1" : Request["SelectState"];//状态
            var aType = string.IsNullOrEmpty(Request["a_type"]) ? "-1" : Request["a_type"];//状态
            var seaName = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            var bll = new appmonitor();
            var list = bll.SelectList(selectState, seaName, type, searchDesc, Convert.ToInt32(aType), pageIndexs, pageSize, out pageCount);
            ViewBag.searchDesc = searchDesc;
            ViewBag.type = type;
            ViewBag.sea_name = seaName;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = pageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            ViewBag.SelectState = selectState;
            ViewBag.a_type = aType;
            ViewBag.locUrl = GetVoidHtmlApp();
            return View();
        }

        public string GetVoidHtmlApp()
        {
            string locUrl = "";
            string u_id = UserInfo.UserId.ToString();
            int r_id = int.Parse(UserInfo.UserRoleId.ToString());
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/REPORT/InsertUpdateAppmonitor", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加管理员
            if (getlocuserAdd)
            {
                locUrl += "<li onclick=\"AddAPPlog()\"><i class='fa fa-plus'></i>添加应用监控</li>";
            }
            locUrl += "<li onclick=\"UpdateState(1)\"><i class='fa fa-check-square-o'></i>一键解冻</li>";
            locUrl += "<li onclick=\"UpdateState(0)\"><i class='fa fa-check-square-o'></i>一键冻结</li>";

            return locUrl;
        }

        /// <summary>
        /// 添加或修改应用监控管理
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult InsertUpdateAppmonitor(JMP.MDL.appmonitor model)
        {
            object retJson = new { success = 0, msg = "操作失败" };
            var bll = new appmonitor();
            var xgzfc = "";

            if (model.a_id > 0)
            {
                // 修改应用监控
                var modComplaint = bll.GetModel(model.a_id);
                var modComplaintClone = modComplaint.Clone();
                modComplaint.a_name = model.a_name;
                modComplaint.a_appid = model.a_appid;
                modComplaint.a_request = model.a_request > 0 ? model.a_request / 100 : 0;
                modComplaint.a_type = model.a_type;
                modComplaint.a_minute = model.a_minute;
                //model.a_datetime = modComplaint.a_datetime;
                //model.a_state = modComplaint.a_state;
                // model.a_appid = modComplaint.a_appid;
                // model.a_request = model.a_request / 100;
                modComplaint.a_time_range = "";
                if (model.StartDay != -1 && model.EndDay != -1 && model.DayMinute != 0)
                {
                    modComplaint.a_time_range += model.StartDay + "-" + model.EndDay + ":" + model.DayMinute + "_";
                }
                if (model.StartNight != -1 && model.EndNight != -1 && model.NightMinute != 0)
                {
                    modComplaint.a_time_range += "_" + model.StartNight + "-" + model.EndNight + ":" + model.NightMinute;
                }
                if (model.OtherMinte != 0)
                {
                    modComplaint.a_time_range += "_100:" + model.OtherMinte;
                }
                var monitorList = modComplaint.a_time_range.ParseAppMonitorTimeRangeTo24Hours();
                AddMonitorMinuteDetails(modComplaint.a_appid, modComplaint.a_type, monitorList);
                if (bll.Update(modComplaint))
                {

                    Logger.ModifyLog("修改应用监控信息", modComplaintClone, model);
                    retJson = new { success = 1, msg = "修改成功" };
                }
                else
                {
                    retJson = new { success = 0, msg = "修改失败" };
                }
            }
            else
            {
                model.a_datetime = DateTime.Now;
                model.a_state = 1;
                if (model.StartDay != -1 && model.EndDay != -1 && model.DayMinute != 0)
                {
                    model.a_time_range += model.StartDay + "-" + model.EndDay + ":" + model.DayMinute + "_";
                }
                if (model.StartNight != -1 && model.EndNight != -1 && model.NightMinute != 0)
                {
                    model.a_time_range += model.StartNight + "-" + model.EndNight + ":" + model.NightMinute;
                }
                if (model.OtherMinte == 0)
                {
                    model.a_time_range += "_100:" + 5;
                }
                else
                {
                    model.a_time_range += "_100:" + model.OtherMinte;
                }
                var monitorList = model.a_time_range.ParseAppMonitorTimeRangeTo24Hours();
                model.a_request = model.a_request > 0 ? model.a_request / 100 : 0;
                var appidList = model.a_appidList.Split(',');
                foreach (var i in appidList)
                {
                    var appId = int.Parse(i);
                    if (appId <= 0)
                    {
                        continue;
                    }
                    var exists = bll.Exists(appId, model.a_type);
                    if (exists)
                    {
                        retJson = new { success = 0, msg = "此应用监控已存在" };
                        continue;
                    }
                    model.a_appid = appId;
                    var cg = bll.Add(model);
                    if (cg > 0)
                    {
                        AddMonitorMinuteDetails(model.a_appid, model.a_type, monitorList);

                        Logger.CreateLog("添加应用监控", model);
                        retJson = new { success = 1, msg = "添加成功" };
                    }
                    else
                    {
                        retJson = new { success = 1, msg = "添加失败" };
                    }
                }

            }
            return Json(retJson);
        }

        /// <summary>
        /// 添加监控分钟详情数据
        /// </summary>
        /// <param name="appId">应用ID</param>
        /// <param name="monitorType">监控类型</param>
        /// <param name="monitorList">24小时的监控分钟数集合</param>
        private void AddMonitorMinuteDetails(int appId, int monitorType, IEnumerable<AppMonitorTimeRange> monitorList)
        {
            //从监控分钟设置详情表删除指定应用和指定监控类型的所有小时的监控分钟数
            var monitorMinuteBll = new JmpMonitorMinuteDetails();
            monitorMinuteBll.DeleteByMonitorType(appId, monitorType);
            foreach (var h in monitorList)
            {
                var monitorMinuteDetails = new JMP.Model.JmpMonitorMinuteDetails
                {
                    AppId = appId,
                    CreatedById = UserInfo.UserId,
                    CreatedByName = UserInfo.UserName,
                    Minutes = h.Minutes,
                    MonitorType = monitorType,
                    WhichHour = h.WhichHour
                };
                monitorMinuteBll.Add(monitorMinuteDetails);
            }
        }

        /// <summary>
        /// 添加应用监控
        /// </summary>
        /// <returns></returns>
        public ActionResult AppmonitorAdd()
        {
            return View();
        }

        public ActionResult AppmonitorEdit()
        {
            int c_id = string.IsNullOrEmpty(Request["c_id"]) ? 0 : Int32.Parse(Request["c_id"]);
            JMP.BLL.appmonitor bll = new JMP.BLL.appmonitor();
            JMP.MDL.appmonitor model = new JMP.MDL.appmonitor();
            if (c_id > 0)
            {
                model = bll.SelectId(c_id);
            }
            model.a_request = model.a_request * 100;
            var timeRanges = model.a_time_range.ParseAppMonitorTimeRangeModel();
            if (timeRanges.AppMonitorTimeDay != null)
            {
                model.DayMinute = timeRanges.AppMonitorTimeDay.Minutes;
                model.StartDay = timeRanges.AppMonitorTimeDay.Start;
                model.EndDay = timeRanges.AppMonitorTimeDay.End;
            }
            if (timeRanges.AppMonitorTimeNight != null)
            {
                model.StartNight = timeRanges.AppMonitorTimeNight.Start;
                model.EndNight = timeRanges.AppMonitorTimeNight.End;
                model.NightMinute = timeRanges.AppMonitorTimeNight.Minutes;
            }
            if (timeRanges.AppMonitorTimeCustom != null)
            {
                model.OtherMinte = timeRanges.AppMonitorTimeCustom.Minutes;
            }


            ViewBag.model = model == null ? new JMP.MDL.appmonitor() : model;
            return View();
        }

        /// <summary>
        /// 结算设置一键启用或禁用
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdateState()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            int state = string.IsNullOrEmpty(Request["state"]) ? 0 : Int32.Parse(Request["state"].ToString());
            string str = Request["ids"];
            string xgzfc = "";//组装说明
            string tsmsg = "";//提示
            JMP.BLL.appmonitor bll = new JMP.BLL.appmonitor();
            if (str.CompareTo("On") > 0)
            {
                str = str.Substring(3);
            }
            if (bll.UpdateState(str, state))
            {
                if (state == 1)
                {
                    xgzfc = "一键启用ID为：" + str;
                    tsmsg = "解冻成功";
                }
                else
                {
                    tsmsg = "冻结成功";
                    xgzfc = "一键禁用ID为：" + str;
                }

                Logger.OperateLog("应用监控一键启用或禁用", xgzfc);
                retJson = new { success = 1, msg = tsmsg };
            }
            else
            {
                if (state == 1)
                {
                    tsmsg = "启用失败";
                }
                else
                {
                    tsmsg = "禁用失败";
                }
                retJson = new { success = 0, msg = tsmsg };
            }
            return Json(retJson);
        }

        #endregion

        #region 通知短信分组信息管理

        /// <summary>
        ///通知短信分组信息管理
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false), VisitRecord(IsRecord = true)]
        public ActionResult NotificaitonList()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            int type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);//查询条件选择
            string SelectState = string.IsNullOrEmpty(Request["SelectState"]) ? "" : Request["SelectState"];//是否启用
            string IntervalUnit = string.IsNullOrEmpty(Request["IntervalUnit"]) ? "" : Request["IntervalUnit"];//状态

            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            List<JMP.MDL.jmp_notificaiton_group> list = new List<JMP.MDL.jmp_notificaiton_group>();
            JMP.BLL.jmp_notificaiton_group bll = new JMP.BLL.jmp_notificaiton_group();
            list = bll.SelectList(SelectState, IntervalUnit, sea_name, type, searchDesc, pageIndexs, PageSize, out pageCount);
            ViewBag.searchDesc = searchDesc;
            ViewBag.type = type;
            ViewBag.sea_name = sea_name;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            ViewBag.SelectState = SelectState;
            ViewBag.locUrl = GetVoidHtmlNotificaiton();
            return View();
        }

        public string GetVoidHtmlNotificaiton()
        {
            string locUrl = "";
            string u_id = UserInfo.UserId.ToString();
            int r_id = int.Parse(UserInfo.UserRoleId.ToString());
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/REPORT/InsertUpdateNotificaiton", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加管理员
            if (getlocuserAdd)
            {
                locUrl += "<li onclick=\"AddAPPlog()\"><i class='fa fa-plus'></i>添加短信分组信息</li>";
            }
            bool getlocuserEidt = bll_limit.GetLocUserLimitVoids("/REPORT/IsEnabled", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加管理员
            if (getlocuserEidt)
            {
                locUrl += "<li onclick=\"UpdateState(1)\"><i class='fa fa-check-square-o'></i>一键启用</li>";
                locUrl += "<li onclick=\"UpdateState(0)\"><i class='fa fa-check-square-o'></i>一键禁止</li>";
            }

            bool getlocuser = bll_limit.GetLocUserLimitVoids("/REPORT/IsDELETE", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加管理员
            if (getlocuser)
            {
                locUrl += "<li onclick=\"Delete()\"><i class='fa fa-check-square-o'></i>一键删除</li>";
            }


            return locUrl;
        }

        /// <summary>
        /// 一键启用或禁用
        /// </summary>
        /// <returns></returns>
        public JsonResult IsEnabled()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            int state = string.IsNullOrEmpty(Request["state"]) ? 0 : Int32.Parse(Request["state"].ToString());
            string str = Request["ids"];
            string xgzfc = "";//组装说明
            string tsmsg = "";//提示
            JMP.BLL.jmp_notificaiton_group bll = new JMP.BLL.jmp_notificaiton_group();
            if (str.CompareTo("On") > 0)
            {
                str = str.Substring(3);
            }
            if (bll.UpdateState(str, state))
            {
                if (state == 1)
                {
                    xgzfc = "一键启用ID为：" + str;
                    tsmsg = "启用成功";
                }
                else
                {
                    tsmsg = "禁止成功";
                    xgzfc = "一键禁用ID为：" + str;
                }

                Logger.OperateLog("通知短信分组一键启用或禁用", xgzfc);
                retJson = new { success = 1, msg = tsmsg };
            }
            else
            {
                if (state == 1)
                {
                    tsmsg = "启用失败";
                }
                else
                {
                    tsmsg = "禁用失败";
                }
                retJson = new { success = 0, msg = tsmsg };
            }
            return Json(retJson);
        }

        /// <summary>
        /// 添加或修改短信分组信息
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult InsertUpdateNotificaiton(JMP.MDL.jmp_notificaiton_group model)
        {
            object retJson = new { success = 0, msg = "操作失败" };
            JMP.BLL.jmp_notificaiton_group bll = new JMP.BLL.jmp_notificaiton_group();
            string xgzfc = "";
            var groups = Enum.GetValues(typeof(ScheduleGroupCode));
            foreach (ScheduleGroupCode item in groups)
            {
                var desc = item.GetDescription();
                var value = item.ToString();
                if (model.Code == item.ToString())
                {
                    model.Name = desc;
                }
            }
            if (string.IsNullOrEmpty(model.SendMode))
            {
                model.SendMode = "短信";
            }
            model.Code = model.Code.Trim();
            if (model.Id > 0)
            {
                // 修改
                JMP.MDL.jmp_notificaiton_group modComplaint = new JMP.MDL.jmp_notificaiton_group();
                modComplaint = bll.GetModel(model.Id);
                var modComplaintClone = modComplaint.Clone();
                modComplaint.Code = model.Code;
                modComplaint.Description = model.Description;
                modComplaint.NotifyMobileList = model.NotifyMobileList;
                modComplaint.MessageTemplate = model.MessageTemplate;
                modComplaint.IntervalUnit = model.IntervalUnit;
                modComplaint.IsAllowSendMessage = model.IsAllowSendMessage;
                modComplaint.SendMode = model.SendMode;
                modComplaint.AudioTelTempId = model.AudioTelTempId;
                modComplaint.AudioTelTempContent = model.AudioTelTempContent;
                //model.CreatedOn = modComplaint.CreatedOn;
                //model.IsDeleted = modComplaint.IsDeleted;
                //model.IsEnabled = modComplaint.IsEnabled;
                //model.CreatedOn = modComplaint.CreatedOn;
                //model.CreatedBy = Convert.ToInt32(UserInfo.UserId);
                //model.CreatedByUser = UserInfo.UserName;
                //model.ModifiedByUser = UserInfo.UserName;
                //model.ModifiedOn = DateTime.Now;
                if (bll.Update(modComplaint))
                {

                    Logger.ModifyLog("修改通知短信分组信息", modComplaintClone, model);
                    retJson = new { success = 1, msg = "修改成功" };
                }
                else
                {
                    retJson = new { success = 0, msg = "修改失败" };
                }

            }
            else
            {
                var exsits = bll.GetModelList("Code='" + model.Code.Trim() + "'");
                if (exsits.Count > 0)
                {
                    retJson = new { success = false, msg = "分组已存在" };
                    return Json(retJson);
                }
                model.CreatedOn = DateTime.Now;
                model.ModifiedOn = DateTime.Now;
                model.CreatedBy = Convert.ToInt32(UserInfo.UserId);
                model.CreatedByUser = UserInfo.UserName;
                model.IsEnabled = true;
                model.IsDeleted = false;
                int cg = bll.Add(model);
                if (cg > 0)
                {

                    Logger.CreateLog("添加通知短信分组信息", model);
                    retJson = new { success = 1, msg = "添加成功" };
                }
                else
                {
                    retJson = new { success = 1, msg = "添加失败" };
                }
            }

            return Json(retJson);
        }
        /// <summary>
        /// 添加/修改页面
        /// </summary>
        /// <returns></returns>
        public ActionResult NotificaitonEdit()
        {
            int c_id = string.IsNullOrEmpty(Request["c_id"]) ? 0 : Int32.Parse(Request["c_id"]);
            string zf = "";
            JMP.BLL.jmp_notificaiton_group bll = new JMP.BLL.jmp_notificaiton_group();
            JMP.MDL.jmp_notificaiton_group model = new JMP.MDL.jmp_notificaiton_group();
            if (c_id > 0)
            {
                model = bll.SelectId(c_id);

                if (model.SendMode == "短信,语音")
                {
                    zf += "&nbsp;&nbsp;<input type=\"checkbox\" name=\"zflx\" class=\"inputChck\" value=\"短信\" checked=\"checked\" />&nbsp;短信";
                    zf += "&nbsp;&nbsp;<input type=\"checkbox\" name=\"zflx\" class=\"inputChck\" value=\"语音\" checked=\"checked\" />&nbsp;语音";
                }
                else if (model.SendMode == "短信")
                {
                    zf += "&nbsp;&nbsp;<input type=\"checkbox\" name=\"zflx\" class=\"inputChck\" value=\"短信\" checked=\"checked\" />&nbsp;短信";
                    zf += "&nbsp;&nbsp;<input type=\"checkbox\" name=\"zflx\" class=\"inputChck\" value=\"语音\" />&nbsp;语音";
                }
                else if (model.SendMode == "语音")
                {
                    zf += "&nbsp;&nbsp;<input type=\"checkbox\" name=\"zflx\" class=\"inputChck\" value=\"短信\"  />&nbsp;短信";
                    zf += "&nbsp;&nbsp;<input type=\"checkbox\" name=\"zflx\" class=\"inputChck\" value=\"语音\" checked=\"checked\" />&nbsp;语音";
                }
                else if (string.IsNullOrEmpty(model.SendMode))
                {
                    zf += "&nbsp;&nbsp;<input type=\"checkbox\" name=\"zflx\" class=\"inputChck\" value=\"短信\" checked=\"\" />&nbsp;短信";
                    zf += "&nbsp;&nbsp;<input type=\"checkbox\" name=\"zflx\" class=\"inputChck\" value=\"语音\" checked=\"\" />&nbsp;语音";
                }

            }
            ViewBag.zf = zf;
            ViewBag.model = model == null ? new JMP.MDL.jmp_notificaiton_group() : model;
            return View();
        }

        /// <summary>
        /// 添加/修改页面(只允许修改手机号码)
        /// </summary>
        /// <returns></returns>
        public ActionResult NotificaitonEditMobile()
        {
            int c_id = string.IsNullOrEmpty(Request["c_id"]) ? 0 : Int32.Parse(Request["c_id"]);
            string zf = "";
            JMP.BLL.jmp_notificaiton_group bll = new JMP.BLL.jmp_notificaiton_group();
            JMP.MDL.jmp_notificaiton_group model = new JMP.MDL.jmp_notificaiton_group();
            if (c_id > 0)
            {
                model = bll.SelectId(c_id);
                if (c_id > 0)
                {
                    model = bll.SelectId(c_id);

                    if (model.SendMode == "短信,语音")
                    {
                        zf += "&nbsp;&nbsp;<input type=\"checkbox\" name=\"zflx\" class=\"inputChck\" value=\"短信\" checked=\"checked\" />&nbsp;短信";
                        zf += "&nbsp;&nbsp;<input type=\"checkbox\" name=\"zflx\" class=\"inputChck\" value=\"语音\" checked=\"checked\" />&nbsp;语音";
                    }
                    else if (model.SendMode == "短信")
                    {
                        zf += "&nbsp;&nbsp;<input type=\"checkbox\" name=\"zflx\" class=\"inputChck\" value=\"短信\" checked=\"checked\" />&nbsp;短信";
                        zf += "&nbsp;&nbsp;<input type=\"checkbox\" name=\"zflx\" class=\"inputChck\" value=\"语音\" />&nbsp;语音";
                    }
                    else if (model.SendMode == "语音")
                    {
                        zf += "&nbsp;&nbsp;<input type=\"checkbox\" name=\"zflx\" class=\"inputChck\" value=\"短信\"  />&nbsp;短信";
                        zf += "&nbsp;&nbsp;<input type=\"checkbox\" name=\"zflx\" class=\"inputChck\" value=\"语音\" checked=\"checked\" />&nbsp;语音";
                    }
                    else if (string.IsNullOrEmpty(model.SendMode))
                    {
                        zf += "&nbsp;&nbsp;<input type=\"checkbox\" name=\"zflx\" class=\"inputChck\" value=\"短信\" checked=\"\" />&nbsp;短信";
                        zf += "&nbsp;&nbsp;<input type=\"checkbox\" name=\"zflx\" class=\"inputChck\" value=\"语音\" checked=\"\" />&nbsp;语音";
                    }

                }
            }
            ViewBag.zf = zf;
            ViewBag.model = model == null ? new JMP.MDL.jmp_notificaiton_group() : model;
            return View();
        }

        public JsonResult IsDELETE()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            string str = Request["ids"];
            string xgzfc = "";//组装说明
            string tsmsg = "";//提示
            JMP.BLL.jmp_notificaiton_group bll = new JMP.BLL.jmp_notificaiton_group();
            if (str.CompareTo("On") > 0)
            {
                str = str.Substring(3);
            }
            if (bll.Delete(str))
            {
                xgzfc = "一键删除ID为：" + str;
                tsmsg = "删除成功";

                Logger.OperateLog("通知分组短信信息一键删除", xgzfc);
                retJson = new { success = 1, msg = tsmsg };
            }
            return Json(retJson);
        }

        #endregion

    }
}
