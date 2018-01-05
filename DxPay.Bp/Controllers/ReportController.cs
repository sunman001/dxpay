
using DxPay.Bp;
using DxPay.Bp.Models;
using DxPay.Bp.Util.Logger;
using DxPay.Factory;
using DxPay.Infrastructure;
using DxPay.Services;
using JMP.MDL;
using JMP.TOOL;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using DxPay.Services.Inter;
using TOOL.Extensions;

namespace DxPay.Bp.Controllers
{
    public class ReportController : Controller
    {
        private readonly IOrderService _OrderService;
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;

        private readonly ICoSettlementDeveloperAppDetailsService _coSettlementDeveloperAppDetailsService;
        private readonly IUserReportService _UserReportService;


        public ReportController()
        {
            _OrderService = ServiceFactory.OrderService;
            _coSettlementDeveloperAppDetailsService = ServiceFactory.CoSettlementDeveloperAppDetailsService;
            _UserReportService = ServiceFactory.UserReportService;

        }

        #region  订单列表
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
            #region 获取信息

            JMP.BLL.jmp_paymode paymodebll = new JMP.BLL.jmp_paymode();
            List<JMP.MDL.jmp_paymode> paymodeList = paymodebll.GetModelList("1=1 and p_state='1' ");//支付类型
            ViewBag.paymodeList = paymodeList;
            #endregion
            #region 查询
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchType = string.IsNullOrEmpty(Request["searchType"]) ? 0 : Int32.Parse(Request["searchType"]); //查询条件选择
            string searchname = string.IsNullOrEmpty(Request["searchname"]) ? "" : Request["searchname"];//查询类容
            string stime = string.IsNullOrEmpty(Request["stime"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["stime"];//开始时间
            string etime = string.IsNullOrEmpty(Request["etime"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["etime"];//结束时间
            int paymode = string.IsNullOrEmpty(Request["paymode"]) ? 0 : Int32.Parse(Request["paymode"]);//支付类型
            string paymentstate = string.IsNullOrEmpty(Request["paymentstate"]) ? "1" : Request["paymentstate"];//支付状态
            string noticestate = string.IsNullOrEmpty(Request["noticestate"]) ? "" : Request["noticestate"];//通知状态
            int relationtype = string.IsNullOrEmpty(Request["relationtype"]) ? 0 : Int32.Parse(Request["relationtype"]);//商户类型
            int userid = UserInfo.UserId;
            var list = _OrderService.FindPagedListByBP(userid, relationtype, "", searchType, searchname, stime, etime, paymode, paymentstate, noticestate, null, pageIndexs, PageSize);
            var gridModel = new DataSource<jmp_order>(list)
            {
                Data = list.Select(x => x).ToList()
            };
            ViewBag.searchname = searchname;
            ViewBag.searchType = searchType;
            ViewBag.stime = stime;
            ViewBag.etime = etime;
            ViewBag.relationtype = relationtype;
            ViewBag.paymode = paymode;
            ViewBag.paymentstate = paymentstate;
            ViewBag.noticestate = noticestate;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.list = gridModel.Data;
            ViewBag.pageCount = gridModel.Pagination.TotalCount;
            #endregion
            return View();
        }

        /// <summary>
        /// 判断导出session是否有效
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public int pdDcSession()
        {
            int sess = (Session["daochu"] == null || Convert.ToDateTime(Session["daochu"]) < DateTime.Now.AddMinutes(-1)) ? 0 : 1;
            return sess;
        }
        /// <summary>
        /// 导出订单列表
        /// </summary>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public ActionResult DcDev()
        {
            #region 查询
            string sql = "select ord.o_id,ord.o_code,ord.o_bizcode,ord.o_tradeno,ord.o_paymode_id,ord.o_app_id,ord.o_goodsname,ord.o_term_key,ord.o_price,ord.o_payuser,ord.o_ctime,ord.o_ptime,ord.o_state,ord.o_times,ord.o_address,ord.o_noticestate,ord.o_noticetimes,ord.o_privateinfo,ord.o_interface_id,ord.o_showaddress,app.a_name, users.u_id,users.u_realname,users.DisplayName,users.relation_type,users.bpname,paymode.p_name,inn.l_corporatename ,app.a_platform_id from(  ";//组装查询条件
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
                sql += " SELECT o_id,o_code,o_bizcode,o_tradeno,o_paymode_id,o_app_id,o_goodsname,o_term_key,o_price,o_payuser,o_ctime,o_ptime,o_state,o_times,o_address,o_noticestate,o_noticetimes,o_privateinfo,o_interface_id, o_showaddress  FROM " + TableName + " where 1=1 ";
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
                sql += "   UNION ALL ";
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
            sql1 = "SELECT o_id,o_code,o_bizcode,o_tradeno,o_paymode_id,o_app_id,o_goodsname,o_term_key,o_price,o_payuser,o_ctime,o_ptime,o_state,o_times,o_address,o_noticestate,o_noticetimes,o_privateinfo,o_interface_id, o_showaddress  FROM jmp_order where 1=1";
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
            sql += " ) ord  ,   jmp_app  app ,(select aa.u_id, aa.u_realname, aa.DisplayName, aa.bpname, aa.relation_type from( select u_id, u_realname, null as DisplayName, c.DisplayName as bpname, relation_type from jmp_user a left join dx_base.dbo.CoBusinessPersonnel c on c.Id = a.relation_person_id  where relation_type = 1 and relation_person_id =" + UserInfo.UserId + " union all(select a.u_id, a.u_realname, c.DisplayName, null as bpname, a.relation_type from jmp_user a left join dx_base.dbo.CoAgent c on c.Id = a.relation_person_id where a.relation_type = 2 and c.OwnerId = " + UserInfo.UserId + "))  aa group by aa.u_id, aa.u_realname, aa.DisplayName, aa.relation_type, aa.bpname )  users, jmp_paymode as paymode, jmp_interface as inn  where app.a_id = ord.o_app_id and users.u_id = app.a_user_id and paymode.p_id = ord.o_paymode_id and inn.l_id = ord.o_interface_id   " + where + order;
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
                relation_type = x.relation_type == 1 ? "直客" : "代理商"
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
                ws.Cells["N1"].Value = "所属类型";
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
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
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
                                    Logger.OperateLog("商务平台手动重发通知失败", "订单号：" + morder.o_code + ",表名：" + tabalename);//写入报错日
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
        #endregion


        /// <summary>
        /// 营收报表
        /// </summary>
        /// <returns></returns>
        public ActionResult AppReport(string rtype)
        {
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
                        etime = DateTime.Now.ToString("yyyy-MM-dd");
                        break;
                    case 3:
                        stime = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01");
                        etime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).ToString("yyyy-MM-dd");
                        break;
                }
            }

            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量            
            string types = string.IsNullOrEmpty(Request["s_type"]) ? "0" : Request["s_type"];
            string searchKey = string.IsNullOrEmpty(Request["s_key"]) ? "" : Request["s_key"];
            int sort = string.IsNullOrEmpty(Request["s_sort"]) ? 1 : int.Parse(Request["s_sort"]);
            string searchTotal = string.IsNullOrEmpty(Request["s_field"]) ? "" : Request["s_field"];
            int relationtype = string.IsNullOrEmpty(Request["relationtype"]) ? 0 : Int32.Parse(Request["relationtype"]);//商户类型
            string helpUrl = System.Configuration.ConfigurationManager.AppSettings["helpUrl"].ToString();
            ViewBag.helpUrl = helpUrl;
            ViewBag.rtype = rtype;
            int userid = UserInfo.UserId;


            if (rtype == "total")
            {
                var list = _UserReportService.FindPagedListByBpTotal(userid, relationtype, "", rtype, stime, etime, time, num, types, searchKey, sort, searchTotal, null, pageIndexs, PageSize);
                var gridModel = new DataSource<jmp_user_report>(list)
                {
                    Data = list.Select(x => x).ToList()
                };
                ViewBag.stime = stime;
                ViewBag.etime = etime;
                ViewBag.list = gridModel.Data;
                ViewBag.total = gridModel.Pagination.TotalCount;
                ViewBag.pageCount = gridModel.Pagination.TotalCount;
            }
            else if (rtype == "today")
            {
                var list = _UserReportService.FindPagedListByBpToday(userid, relationtype, "", rtype, stime, etime, time, num, types, searchKey, sort, searchTotal, null, pageIndexs, PageSize);
                var gridModel = new DataSource<jmp_user_report>(list)
                {
                    Data = list.Select(x => x).ToList()
                };
                ViewBag.stime = DateTime.Now.ToString("yyyy-MM-dd");
                ViewBag.etime = DateTime.Now.ToString("yyyy-MM-dd");
                ViewBag.list = gridModel.Data;
                ViewBag.total = gridModel.Pagination.TotalCount;
                ViewBag.pageCount = gridModel.Pagination.TotalCount;
            }
            ViewBag.relationtype = relationtype;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.rtype = rtype;

            return View();
        }

        #region Demo
        [LoginCheckFilter(IsCheck = false, IsRole = false)]
        public ActionResult Demo()
        {
            var pagedList = _coSettlementDeveloperAppDetailsService.FindAll("Id DESC");
            var gridModel = new DataSourceResult<Domain.CoSettlementDeveloperAppDetails>(pagedList)
            {
                Data = pagedList.Select(x => x)
            };

            return View(gridModel);
        }
        #endregion

    }
}
