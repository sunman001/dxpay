using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DxPay.Services;
using DxPay.Agent.Util.Logger;
using DxPay.Factory;
using JMP.MDL;
using JMP.TOOL;
using DxPay.Agent.Models;
using System.Data;
using DxPay.Services.Inter;

namespace DxPay.Agent.Controllers
{
    public class FinancialController : Controller
    {
        private readonly ICoSettlementDeveloperOverviewService _CoSettlementDeveloperOverviewService;
        private readonly IUserService _UserService;
        private readonly ICoSettlementDeveloperAppDetailsService _CoSettlementDeveloperAppDetailsServic;

        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;

        public FinancialController()
        {
            _CoSettlementDeveloperOverviewService = ServiceFactory.CoSettlementDeveloperOverviewService;
            _UserService = ServiceFactory.UserService;
            _CoSettlementDeveloperAppDetailsServic = ServiceFactory.CoSettlementDeveloperAppDetailsService;
        }

        /// <summary>
        /// 账单管理
        /// </summary>
        /// <returns></returns>
        public ActionResult BillList()
        {
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 :  Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量    
            string stime = string.IsNullOrEmpty(Request["stime"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["stime"];//开始时间
            string etime = string.IsNullOrEmpty(Request["etime"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["etime"];//结束时间  

            //首页跳转标识
            int num = string.IsNullOrEmpty(Request["start"]) ? -1 : int.Parse(Request["start"]);
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

            int u_id = UserInfo.UserId;

            //查询开发者账单
            var list = _CoSettlementDeveloperOverviewService.CoSettlementDeveloperOverview_Agent(u_id, "SettlementDay  desc", stime, etime, pageIndexs, PageSize);

            var gridModel = new DataSource<CoSettlementDeveloperOverview>(list)
            {
                Data = list.Select(x => x).ToList()
            };

            ViewBag.stime = stime;
            ViewBag.etime = etime;
            ViewBag.pageIndexs = pageIndexs ;
            ViewBag.PageSize = PageSize;
            ViewBag.list = gridModel.Data;
            ViewBag.pageCount = gridModel.Pagination.TotalCount;
            ViewBag.total = gridModel.Pagination.TotalCount;


            return View();
        }

        /// <summary>
        /// 账单详情
        /// </summary>
        /// <param name="appid">开发者ID</param>
        /// <param name="SettlementDay">账单日期</param>
        /// <returns></returns>
        public ActionResult BillList_Details(string SettlementDay)
        {

            List<JMP.MDL.CoSettlementDeveloperOverview> colist = new List<JMP.MDL.CoSettlementDeveloperOverview>();

            int u_id = UserInfo.UserId;

            //查询开发者账单
            DataSet dt = _CoSettlementDeveloperOverviewService.FindPagedListByDeveloperAgentUId(u_id, SettlementDay);

            colist = JMP.TOOL.MdlList.ToList<JMP.MDL.CoSettlementDeveloperOverview>(dt.Tables[0]);

            ViewBag.colist = colist;

            return PartialView();
        }
    }
}