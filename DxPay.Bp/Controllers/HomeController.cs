using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using DxPay.Bp.Models;
using DxPay.Factory;
using DxPay.Services;
using JMP.MDL;
using System.Data;
using System;
using JMP.TOOL;
using Newtonsoft.Json;
using DxPay.Services.Inter;

namespace DxPay.Bp.Controllers
{
    public class HomeController : Controller
    {


        private readonly ICoSettlementDeveloperAppDetailsService _CoSettlementDeveloperAppDetailsServic;
        private readonly IAppCountService _AppCountService;


        JMP.BLL.jmp_user user_bll = new JMP.BLL.jmp_user();

        public HomeController()
        {
            _CoSettlementDeveloperAppDetailsServic = ServiceFactory.CoSettlementDeveloperAppDetailsService;
            _AppCountService = ServiceFactory.AppCountService;
        }

        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult Index()
        {
            //开发者每日应用汇总
            JMP.MDL.jmp_appcount model = new JMP.MDL.jmp_appcount();

            //开发者每日结算详情表
            JMP.MDL.CoSettlementDeveloperAppDetails comodel_yesterday = new JMP.MDL.CoSettlementDeveloperAppDetails();
            JMP.MDL.CoSettlementDeveloperAppDetails comodel_month = new JMP.MDL.CoSettlementDeveloperAppDetails();
            JMP.MDL.CoSettlementDeveloperAppDetails comodel_preceding_month = new JMP.MDL.CoSettlementDeveloperAppDetails();
            //登录者ID
            int u_id = UserInfo.UserId;

            //今天
            string u_time = DateTime.Now.ToString("yyyy-MM-dd");
            //昨天
            string u_time_yesterday = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            //本月
            string u_time_month = DateTime.Now.ToString("yyyy-MM");
            //上月
            string u_time_preceding_month = DateTime.Now.AddMonths(-1).ToString("yyyy-MM");

            //根据日期查询交易金额和笔数（今天）
            model = _AppCountService.DataAppcountAdyBp(u_time, u_id);

            //根据不同日期统计查询(昨天)
            comodel_yesterday = _CoSettlementDeveloperAppDetailsServic.FindPagedListByDeveloperKFZ(u_id, u_time_yesterday, 0);
            //根据不同日期统计查询(本月)
            comodel_month = _CoSettlementDeveloperAppDetailsServic.FindPagedListByDeveloperKFZ(u_id, u_time_month, 1);
            //根据不同日期统计查询(上月)
            comodel_preceding_month = _CoSettlementDeveloperAppDetailsServic.FindPagedListByDeveloperKFZ(u_id, u_time_preceding_month, 1);

            //流水及收入金额和笔数
            ViewBag.comodel_yesterday = comodel_yesterday;
            ViewBag.comodel_month = comodel_month;
            ViewBag.comodel_preceding_month = comodel_preceding_month;

            ViewBag.AppCount = model;

            return View();
        }

        [LoginCheckFilter(IsCheck = false, IsRole = false)]
        public ActionResult Default()
        {
            //开发者每日应用汇总
            JMP.MDL.jmp_appcount model = new JMP.MDL.jmp_appcount();

            //开发者每日结算详情表
            JMP.MDL.CoSettlementDeveloperAppDetails comodel_yesterday = new JMP.MDL.CoSettlementDeveloperAppDetails();
            JMP.MDL.CoSettlementDeveloperAppDetails comodel_month = new JMP.MDL.CoSettlementDeveloperAppDetails();
            JMP.MDL.CoSettlementDeveloperAppDetails comodel_preceding_month = new JMP.MDL.CoSettlementDeveloperAppDetails();
            //登录者ID
            int u_id = UserInfo.UserId;

            //今天
            string u_time = DateTime.Now.ToString("yyyy-MM-dd");
            //昨天
            string u_time_yesterday = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            //本月
            string u_time_month = DateTime.Now.ToString("yyyy-MM");
            //上月
            string u_time_preceding_month = DateTime.Now.AddMonths(-1).ToString("yyyy-MM");

            //根据日期查询交易金额和笔数（今天）
            model = _AppCountService.DataAppcountAdyBp(u_time, u_id);

            //根据不同日期统计查询(昨天)
            comodel_yesterday = _CoSettlementDeveloperAppDetailsServic.FindPagedListByDeveloperKFZ(u_id, u_time_yesterday, 0);
            //根据不同日期统计查询(本月)
            comodel_month = _CoSettlementDeveloperAppDetailsServic.FindPagedListByDeveloperKFZ(u_id, u_time_month, 1);
            //根据不同日期统计查询(上月)
            comodel_preceding_month = _CoSettlementDeveloperAppDetailsServic.FindPagedListByDeveloperKFZ(u_id, u_time_preceding_month, 1);

            //流水及收入金额和笔数
            ViewBag.comodel_yesterday = comodel_yesterday;
            ViewBag.comodel_month = comodel_month;
            ViewBag.comodel_preceding_month = comodel_preceding_month;

            ViewBag.AppCount = model;
            return View();
        }

        #region 首页曲线图
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ContentResult HomeCharts(string days)
        {
            JMP.BLL.jmp_appcount bll = new JMP.BLL.jmp_appcount();
            DataSet ds = new DataSet();

            //登录用户ID
            int userid = UserInfo.UserId;

            //时间
            string s_time = "";
            //状态（前天昨天今天）
            if (days == "0")
            {
                s_time = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else if (days == "1")
            {

                s_time = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            }
            else if (days == "2")
            {

                s_time = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");

            }

            //时间
            string startTime = s_time + " 00:00:00";
            string endTime = s_time + " 23:59:59";

            //前三日平均
            string startTimeAdy = DateTime.Now.AddDays(-4).ToString("yyyy-MM-dd") + " 00:00:00";
            string endTimeAdy = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59";

            //查询数据
            ds = _AppCountService.FindPagedListBp(userid, startTime, endTime, startTimeAdy, endTimeAdy);

            return Content(JsonConvert.SerializeObject(new { Data = ds }), "application/json");
        }

        #endregion


    }
}
