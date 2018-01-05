/************聚米支付平台__专题分析报表控制器************/
//描述：专题分析报表控制器
//功能：专题分析报表控制器
//开发者：秦际攀
//开发时间: 2016.05.25
/************聚米支付平台__专题分析报表控制器************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JMP.TOOL;
using System.Data;
using Newtonsoft.Json;

namespace WEBDEV.Controllers
{
    /// <summary>
    /// 专题分析报表控制器
    /// </summary>
    public class specialController : Controller
    {
        /// <summary>
        /// 终端属性报表统计界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Terminal()
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
            string htmls = "{\"chart\": {\"caption\": \"\",\"subCaption\": \"\",\"numberSuffix\": \"%\",\"paletteColors\": \"#0075c2\",\"bgColor\": \"FFFFFF\",\"showBorder\": \"0\",\"showCanvasBorder\": \"0\",\"usePlotGradientColor\": \"0\",\"plotBorderAlpha\": \"10\",\"placeValuesInside\": \"1\",\"valueFontColor\": \"#ffffff\",\"showAxisLines\": \"1\",\"axisLineAlpha\": \"25\",\"divLineAlpha\": \"10\",\"alignCaptionWithCanvas\": \"0\",\"showAlternateVGridColor\": \"0\",\"captionFontSize\": \"14\",\"subcaptionFontSize\": \"14\",\"subcaptionFontBold\": \"0\",\"toolTipColor\": \"#ffffff\",\"toolTipBorderThickness\": \"0\",\"toolTipBgColor\": \"#000000\",\"toolTipBgAlpha\": \"80\",\"toolTipBorderRadius\": \"2\",\"toolTipPadding\": \"5\" }";
            htmls += ",\"data\": [";
            string datastr = "";
            int searchType = 2; string searchname = UserInfo.UserNo;
            string a_name = string.IsNullOrEmpty(Request["a_name"]) ? "" : Request["a_name"];
            if (!string.IsNullOrEmpty(a_name) && a_name != "所有应用")
            {
                searchType = 1;
                searchname = a_name;
            }
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
        public ActionResult Trends()
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
        public ContentResult TrendsCount()
        {
            DataSet ds1 = new DataSet();

            string stime = string.IsNullOrEmpty(Request["stime"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["stime"];//开始时间
            string etime = string.IsNullOrEmpty(Request["etime"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["etime"];//结束时间

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

            int searchType = 2; string searchname = UserInfo.UserNo;
            string a_name = string.IsNullOrEmpty(Request["a_name"]) ? "" : Request["a_name"];
            if (!string.IsNullOrEmpty(a_name) && a_name != "所有应用")
            {
                searchType = 1;
                searchname = a_name;
            }

            ds1 = blltrends.GetListSet(stime, etime, searchType, searchname);

            return Content(JsonConvert.SerializeObject(new { Data = ds1 }), "application/json");
        }
    }
}
