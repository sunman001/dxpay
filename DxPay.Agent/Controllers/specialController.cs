
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using JMP.TOOL;
using System.Data;
using Newtonsoft.Json;
using DxPay.Services;
using DxPay.Factory;
using DxPay.Agent;
using DxPay.Agent.Util.Logger;
using System.Linq;
using TOOL.EnumUtil;

namespace WEBDEV.Controllers
{
    /// <summary>
    /// 专题分析报表控制器
    /// </summary>
    public class specialController : Controller
    {
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;
        private readonly ITrendsService _TrendsService;
        private readonly IStatisticsService _StatisticsService;
        private readonly IModelnumbersService _ModelnumbersService;
        private readonly IOperatingsystemService _OperatingsystemService;
        public readonly IResolutionService _ResolutionService;
        public readonly INetworkService _NetworkService;
        public readonly IOperatorService _OperatorService;
        public readonly IProvinceService _ProvinceService;
        public readonly IAppService _AppService;

        public specialController()
        {
            _TrendsService = ServiceFactory.TrendsService;
            _StatisticsService = ServiceFactory.StatisticsService;
            _ModelnumbersService = ServiceFactory.ModelnumbersService;
            _OperatingsystemService = ServiceFactory.OperatingsystemService;
            _ResolutionService = ServiceFactory.ResolutionService;
            _NetworkService = ServiceFactory.NetworkService;
            _OperatorService = ServiceFactory.OperatorService;
            _ProvinceService = ServiceFactory.ProvinceService;
            _AppService = ServiceFactory.AppService;

        }
        /// <summary>
        /// 终端属性报表统计界面
        /// </summary>
        /// <returns></returns>
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
            int userId =  UserInfo.UserId;
            int aid = string.IsNullOrEmpty(Request["aid"]) ? 0 : Int32.Parse(Request["aid"].ToString());
            var list = _AppService.FindListByUserId( (int)Relationtype.Agent, userId, " order by a.a_id");

            if (list.Count() > 0)
            {
                ViewBag.list = list;
            }
            ViewBag.aid = aid;
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
            string htmls = "{\"chart\": {\"caption\": \"\",\"subCaption\": \"\",\"numberSuffix\": \"%\",\"paletteColors\": \"#0075c2\",\"bgColor\": \"FFFFFF\",\"showBorder\": \"0\",\"showCanvasBorder\": \"0\",\"usePlotGradientColor\": \"0\",\"plotBorderAlpha\": \"10\",\"placeValuesInside\": \"1\",\"valueFontColor\": \"#ffffff\",\"showAxisLines\": \"1\",\"axisLineAlpha\": \"25\",\"divLineAlpha\": \"10\",\"alignCaptionWithCanvas\": \"0\",\"showAlternateVGridColor\": \"0\",\"captionFontSize\": \"14\",\"subcaptionFontSize\": \"14\",\"subcaptionFontBold\": \"0\",\"toolTipColor\": \"#ffffff\",\"toolTipBorderThickness\": \"0\",\"toolTipBgColor\": \"#000000\",\"toolTipBgAlpha\": \"80\",\"toolTipBorderRadius\": \"2\",\"toolTipPadding\": \"5\" }";
            htmls += ",\"data\": [";
            string datastr = "";
            int userid = UserInfo.UserId;
            string a_name = string.IsNullOrEmpty(Request["a_name"]) ? "" : Request["a_name"];
            switch (type)
            {
                case "statistics":
                    #region 手机品牌
                    var statimodel = _StatisticsService.FindBytime(stime, etime);

                    var jmp_statisticslist = _StatisticsService.FindPagedListByAgent(userid, "", stime, etime, a_name);
                    int sjpp = 1; decimal ppbl = 0;
                    if (statimodel != null)
                    {
                        sjpp = statimodel.s_count;
                    }
                    if (jmp_statisticslist.Count() > 0)
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
                    var modelnumberlist = _ModelnumbersService.FindPagedList(userid,  "", stime, etime, a_name);
                    var xhmodel = _ModelnumbersService.FindBytime(stime, etime);

                    int xxcount = 1; decimal xxbl = 0;
                    if (xhmodel != null)
                    {
                        xxcount = xhmodel.m_count;
                    }
                    if (modelnumberlist.Count() > 0)
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
                    var operatingsystemlist = _OperatingsystemService.FindPagedList(userid, "", stime, etime, a_name);
                    var opmdel = _OperatingsystemService.FindBytime(stime, etime);
                    int opcount = 1; decimal xtbl = 0;
                    if (opmdel != null)
                    {
                        opcount = opmdel.o_count;
                    }
                    if (operatingsystemlist.Count() > 0)
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
                    var resolutionlist = _ResolutionService.FindPagedList(userid, "", stime, etime, a_name);
                    var remodel = _ResolutionService.FindBytime(stime, etime);
                    JMP.BLL.jmp_resolution oresolutionbll = new JMP.BLL.jmp_resolution();
                    int recount = 1; decimal fblbl = 0;
                    if (remodel != null)
                    {
                        recount = remodel.r_count;
                    }
                    if (resolutionlist.Count() > 0)
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
                    var networklist = _NetworkService.FindPagedList(userid, "", stime, etime, a_name);
                    var nemodel = _NetworkService.FindBytime(stime, etime);
                    int necoutn = 1; decimal wlbl = 0;
                    if (nemodel != null)
                    {
                        necoutn = nemodel.n_count;
                    }
                    if (networklist.Count() > 0)
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
                    var operatorlist = _OperatorService.FindPagedList(userid,  "", stime, etime, a_name);
                    var opmodel = _OperatorService.FindBytime(stime, etime);
                    int opmocount = 1; decimal yys = 0;
                    if (opmodel != null)
                    {
                        opmocount = opmodel.o_count;
                    }
                    if (operatorlist.Count() > 0)
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
                    var prlist = _ProvinceService.FindPagedList(userid,"", stime, etime, a_name);
                    var prmode = _ProvinceService.FindBytime(stime, etime);

                    int prcount = 1; decimal prs = 0;

                    if (prmode != null)
                    {
                        prcount = prmode.p_count;
                    }
                    if (prlist.Count() > 0)
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
            int userId = UserInfo.UserId;
            int aid = string.IsNullOrEmpty(Request["aid"]) ? 0 : Int32.Parse(Request["aid"].ToString());
            int  userid = UserInfo.UserId;
            var list = _AppService.FindListByUserId(1,userId, " order by a.a_id");
           
            if (list.Count() > 0)
            {
                ViewBag.list = list;
            }
            ViewBag.aid = aid;
            ViewBag.list = list;
            ViewBag.etime = etime;
            return View();
        }
        /// <summary>
        /// 流量走势报表统计
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ContentResult TrendsCount()
        {

            string stime = string.IsNullOrEmpty(Request["stime"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["stime"];//开始时间
            string etime = string.IsNullOrEmpty(Request["etime"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["etime"];//结束时间
            string a_name = string.IsNullOrEmpty(Request["a_name"]) ? "" : Request["a_name"];

            int userid = UserInfo.UserId;
            var list = _TrendsService.FindPagedListByAgent(userid, "", stime, etime, a_name);

            DataSet ds = JMP.TOOL.MdlList.ToDataSet(list.ToList());

            return Content(JsonConvert.SerializeObject(new { Data = ds }), "application/json");
        }
    }
}
