/************聚米支付平台__用户分析报表控制器************/
//描述：用户分析报表控制器
//功能：用户分析报表控制器
//开发者：秦际攀
//开发时间: 2016年4月19日
/************聚米支付平台__用户分析报表控制器************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB.Util.Logger;

namespace WEB.Controllers
{
    /// <summary>
    /// 用户分析报表控制器
    /// </summary>
    public class UserReportController : Controller
    {
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;
        /// <summary>
        /// 新增用户报表界面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult NewUser()
        {
            string ksrq = DateTime.Now.ToString("yyyy-01-01");//获取本月第一天
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
        /// 新增用户报错统计方法
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false), VisitRecord(IsRecord = true)]
        public string NewUserCount()
        {
            string stime = string.IsNullOrEmpty(Request["stime"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["stime"];//开始时间
            string etime = string.IsNullOrEmpty(Request["etime"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["etime"];//结束时间
            int searchType = string.IsNullOrEmpty(Request["searchType"]) ? 0 : Int32.Parse(Request["searchType"]);//查询条件
            string searchname = string.IsNullOrEmpty(Request["searchname"]) ? "" : Request["searchname"];//查询内容
            string htmls = "{\"chart\":{\"caption\":\"\",\"paletteColors\":\"#0075c2,#1aaf5d\",\"xAxisNameFontSize\":\"12\",\"subcaptionFontSize\":\"14\",\"subcaptionFontBold\":\"0\",\"showBorder\":\"0\",\"bgColor\":\"#ffffff\",\"baseFont\":\"Helvetica Neue,Arial\",\"showCanvasBorder\":\"0\",\"showShadow\":\"0\",\"showAlternateHGridColor\":\"0\",\"canvasBgColor\":\"#ffffff\",\"forceAxisLimits\":\"1\",\"pixelsPerPoint\":\"0\",\"pixelsPerLabel\":\"30\",\"lineThickness\":\"1\",\"compactdatamode\":\"1\",\"dataseparator\":\"|\",\"labelHeight\":\"70\",\"scrollheight\":\"10\",\"flatScrollBars\":\"1\",\"scrollShowButtons\":\"0\",\"scrollColor\":\"#cccccc\",\"legendBgAlpha\":\"0\",\"legendBorderAlpha\":\"0\",\"legendShadow\":\"0\",\"legendItemFontSize\":\"10\",\"legendItemFontColor\":\"#666666\",\"showToolTip\":\"true\"},";
            //string htmls = "{\"chart\":{\"caption\":\"\",\"numberprefix\":\"\",\"plotgradientcolor\":\"\",\"bgcolor\":\"ffffff\",\"showalternatehgridcolor\":\"0\",\"divlinecolor\":\"cccccc\",\"showvalues\":\"0\",\"showcanvasborder\":\"0\",\"canvasborderalpha\":\"0\",\"canvasbordercolor\":\"cccccc\",\"canvasborderthickness\":\"1\",\"yaxismaxvalue\":\"\",\"captionpadding\":\"50\",\"linethickness\":\"3\",\"yaxisvaluespadding\":\"30\",\"legendshadow\":\"0\",\"legendborderalpha\":\"0\",\"palettecolors\":\"#008ee4\",\"showborder\":\"0\",\"toolTipSepChar\":\"\",\"formatNumberScale\":\"0\"},";
            JMP.BLL.jmp_trends blltrends = new JMP.BLL.jmp_trends();
            DataTable dt = blltrends.GetListDataTable(stime, etime, searchType, searchname);
            #region 组装json格式
            htmls += "\"categories\": [";
            htmls += "{";
            //htmls += "\"category\": [";
            htmls += "\"category\":\" ";
            string type = string.IsNullOrEmpty(Request["type"]) ? "" : Request["type"];//获取显示类型
            #region 构建坐标
            #region 按天需要组装的条件
            DateTime ksstime = DateTime.Parse(stime);
            DateTime jsetime = DateTime.Parse(etime);
            int zbcs = Int32.Parse((DateTime.Parse(etime) - DateTime.Parse(stime)).TotalDays.ToString());
            #endregion
            #region 按周需要组装的条件
            ArrayList kssjfw = JMP.TOOL.WeekDateTime.WeekMonday(DateTime.Parse(stime), DateTime.Parse(etime));//根据时间返回获取每周周一
            ArrayList jssjfw = JMP.TOOL.WeekDateTime.WeekDay(DateTime.Parse(stime), DateTime.Parse(etime));//根据时间返回获取每周星期天
            #endregion
            #region 按月需要组装的条件
            DateTime ksmonth = DateTime.Parse(stime);
            int xsmonth = Int32.Parse(DateTime.Parse(etime).ToString("yyyyMM")) - Int32.Parse(DateTime.Parse(stime).ToString("yyyyMM"));
            #endregion
            string seriesName = "每日新增用户";
            string labelvalue = "";//显示值
            switch (type)
            {
                case "day"://天

                    for (int j = 0; j <= zbcs; j++)
                    {
                        //htmls += "{\"label\": \"" + ksstime.ToString("yyyy-MM-dd") + "\"},";
                        htmls += ksstime.ToString("yyyy-MM-dd") + "|";
                        ksstime = ksstime.AddDays(1);
                    }
                    seriesName = "每日新增用户";
                    break;
                case "week"://周
                    for (int k = 0; k < kssjfw.Count; k++)
                    {
                        labelvalue = DateTime.Parse(kssjfw[k].ToString()).ToString("yy-MM-dd") + "至" + DateTime.Parse(jssjfw[k].ToString()).ToString("yy-MM-dd");
                        //htmls += "{\"label\": \"" + labelvalue + "\"},";
                        htmls += labelvalue + "|";
                    }
                    seriesName = "每周新增用户";
                    break;
                case "month"://月
                    for (int i = 0; i <= xsmonth; i++)
                    {
                        //htmls += "{\"label\": \"" + ksmonth.ToString("yyyy-MM") + "\"},";
                        htmls += ksmonth.ToString("yyyy-MM") + "|";
                        ksmonth = ksmonth.AddMonths(1);
                    }
                    seriesName = "每月新增用户";
                    break;
            }
            htmls = htmls.TrimEnd('|');
            //htmls += "]";
            htmls += "\"";
            htmls += "}";
            htmls += "],";
            #endregion
            #region 构建数据
            htmls += "\"dataset\": [";
            htmls += "{";
            htmls += "\"seriesName\":\"" + seriesName + "\",";
            // htmls += "\"data\": [";
            htmls += "\"data\": \"";
            switch (type)
            {
                case "day"://天
                    #region 按天显示
                    if (dt.Rows.Count > 0)
                    {
                        if ((zbcs + 1) == dt.Rows.Count)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                //htmls += "{\"value\": \"" + dt.Rows[i]["t_newcount"] + "\"},";
                                htmls += dt.Rows[i]["t_newcount"] + "|";
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
                                    //htmls += "{\"value\": \"" + ddt[0]["t_newcount"] + "\"},";
                                    htmls += ddt[0]["t_newcount"] + "|";
                                }
                                else
                                {
                                    //htmls += "{\"value\": \"0\"},";
                                    htmls += "0|";
                                }
                                ksstimes = ksstimes.AddDays(1);
                            }
                        }
                    }
                    else
                    {
                        return "0";
                    }
                    #endregion
                    break;
                case "week"://周
                    #region 按周
                    if (dt.Rows.Count > 0)
                    {
                        for (int kw = 0; kw < kssjfw.Count; kw++)
                        {
                            DataTable kwdt = JMP.TOOL.MdlList.TableSelect(dt, "t_time>='" + kssjfw[kw] + " 00:00:00" + "' and t_time<='" + jssjfw[kw] + " 23:59:59" + "'");
                            if (kwdt != null)
                            {
                                if (kwdt.Rows.Count > 0)
                                {
                                    object sumObject = kwdt.Compute("sum(t_newcount)", "TRUE");//根据DataTable列求和
                                    // htmls += "{\"value\": \"" + sumObject + "\"},";
                                    htmls += sumObject + "|";
                                }
                                else
                                {
                                    //htmls += "{\"value\": \"0\"},";
                                    htmls += "0|";
                                }
                            }
                            else
                            {
                                //htmls += "{\"value\": \"0\"},";
                                htmls += "0|";
                            }
                        }
                    }
                    else
                    {
                        return "0";
                    }
                    #endregion
                    break;
                case "month"://月
                    #region 按月
                    if (dt.Rows.Count > 0)
                    {
                        DateTime monthtime = DateTime.Parse(stime);
                        for (int mm = 0; mm <= xsmonth; mm++)
                        {
                            DataTable mdt = JMP.TOOL.MdlList.TableSelect(dt, "t_time>='" + monthtime.ToString("yyyy-MM-01") + " 00:00:00" + "' and t_time<='" + monthtime.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59" + "'");
                            if (mdt != null)
                            {
                                if (mdt.Rows.Count > 0)
                                {
                                    object sumObject = mdt.Compute("sum(t_newcount)", "TRUE");//根据DataTable列求和
                                    // htmls += "{\"value\": \"" + sumObject + "\"},";
                                    htmls += sumObject + "|";
                                }
                                else
                                {
                                    //htmls += "{\"value\": \"0\"},";
                                    htmls += "0|";
                                }
                            }
                            else
                            {
                                // htmls += "{\"value\": \"0\"},";
                                htmls += "0|";
                            }
                            monthtime = monthtime.AddMonths(1);
                        }
                    }
                    else
                    {
                        return "0";
                    }
                    #endregion
                    break;
            }
            //htmls = htmls.TrimEnd(',');
            htmls = htmls.TrimEnd('|');
            // htmls += "]";
            htmls += "\"";
            htmls += "}";
            htmls += "]";
            htmls += "}";
            #endregion
            #endregion
            return htmls;
        }
        /// <summary>
        /// 活跃用户报表界面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult active()
        {
            string ksrq = DateTime.Now.ToString("yyyy-01-01");//获取本年第一天
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
        /// 活跃用户报表统计方法
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public string activecount()
        {
            string stime = string.IsNullOrEmpty(Request["stime"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["stime"];//开始时间
            string etime = string.IsNullOrEmpty(Request["etime"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["etime"];//结束时间
            int searchType = string.IsNullOrEmpty(Request["searchType"]) ? 0 : Int32.Parse(Request["searchType"]);//查询条件
            string searchname = string.IsNullOrEmpty(Request["searchname"]) ? "" : Request["searchname"];//查询内容
            string htmls = "{\"chart\":{\"caption\":\"\",\"paletteColors\":\"#0075c2,#1aaf5d\",\"captionFontSize\":\"14\",\"subcaptionFontSize\":\"14\",\"subcaptionFontBold\":\"0\",\"showBorder\":\"0\",\"bgColor\":\"#ffffff\",\"baseFont\":\"Helvetica Neue,Arial\",\"showCanvasBorder\":\"0\",\"showShadow\":\"0\",\"showAlternateHGridColor\":\"0\",\"canvasBgColor\":\"#ffffff\",\"forceAxisLimits\":\"1\",\"pixelsPerPoint\":\"0\",\"pixelsPerLabel\":\"30\",\"lineThickness\":\"1\",\"compactdatamode\":\"1\",\"dataseparator\":\"|\",\"labelHeight\":\"30\",\"scrollheight\":\"10\",\"flatScrollBars\":\"1\",\"scrollShowButtons\":\"0\",\"scrollColor\":\"#cccccc\",\"legendBgAlpha\":\"0\",\"legendBorderAlpha\":\"0\",\"legendShadow\":\"0\",\"legendItemFontSize\":\"10\",\"legendItemFontColor\":\"#666666\",\"showToolTip\":\"true\"},";
            JMP.BLL.jmp_trends blltrends = new JMP.BLL.jmp_trends();
            DataTable dt = blltrends.GetListDataTable(stime, etime, searchType, searchname);
            #region 组装json格式
            htmls += "\"categories\": [";
            htmls += "{";
            htmls += "\"category\":\" ";
            string type = string.IsNullOrEmpty(Request["type"]) ? "" : Request["type"];//获取显示类型
            #region 构建坐标
            #region 按天需要组装的条件
            DateTime ksstime = DateTime.Parse(stime);
            DateTime jsetime = DateTime.Parse(etime);
            int zbcs = Int32.Parse((DateTime.Parse(etime) - DateTime.Parse(stime)).TotalDays.ToString());
            #endregion
            #region 按周需要组装的条件
            ArrayList kssjfw = JMP.TOOL.WeekDateTime.WeekMonday(DateTime.Parse(stime), DateTime.Parse(etime));//根据时间返回获取每周周一
            ArrayList jssjfw = JMP.TOOL.WeekDateTime.WeekDay(DateTime.Parse(stime), DateTime.Parse(etime));//根据时间返回获取每周星期天
            #endregion
            #region 按月需要组装的条件
            DateTime ksmonth = DateTime.Parse(stime);
            int xsmonth = Int32.Parse(DateTime.Parse(etime).ToString("yyyyMM")) - Int32.Parse(DateTime.Parse(stime).ToString("yyyyMM"));
            #endregion
            string seriesName = "每日活跃用户";
            string labelvalue = "";//显示值
            switch (type)
            {
                case "day"://天

                    for (int j = 0; j <= zbcs; j++)
                    {
                        htmls += ksstime.ToString("yyyy-MM-dd") + "|";
                        ksstime = ksstime.AddDays(1);
                    }
                    seriesName = "每日活跃用户";
                    break;
                case "week"://周
                    for (int k = 0; k < kssjfw.Count; k++)
                    {
                        labelvalue = DateTime.Parse(kssjfw[k].ToString()).ToString("yy-MM-dd") + "至" + DateTime.Parse(jssjfw[k].ToString()).ToString("yy-MM-dd");
                        htmls += labelvalue + "|";
                    }
                    seriesName = "每周活跃用户";
                    break;
                case "month"://月
                    for (int i = 0; i <= xsmonth; i++)
                    {
                        htmls += ksmonth.ToString("yyyy-MM") + "|";
                        ksmonth = ksmonth.AddMonths(1);
                    }
                    seriesName = "每月活跃用户";
                    break;
            }
            htmls = htmls.TrimEnd('|');
            htmls += "\"";
            htmls += "}";
            htmls += "],";
            #endregion
            #region 构建数据
            htmls += "\"dataset\": [";
            htmls += "{";
            htmls += "\"seriesName\":\"" + seriesName + "\",";
            htmls += "\"data\": \"";
            switch (type)
            {
                case "day"://天
                    #region 按天显示
                    if (dt.Rows.Count > 0)
                    {
                        if ((zbcs + 1) == dt.Rows.Count)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                htmls += dt.Rows[i]["t_activecount"] + "|";
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
                                    htmls += ddt[0]["t_activecount"] + "|";
                                }
                                else
                                {
                                    htmls += "0|";
                                }
                                ksstimes = ksstimes.AddDays(1);
                            }
                        }
                    }
                    else
                    {
                        return "0";
                    }
                    #endregion
                    break;
                case "week"://周
                    #region 按周
                    if (dt.Rows.Count > 0)
                    {
                        for (int kw = 0; kw < kssjfw.Count; kw++)
                        {
                            DataTable kwdt = JMP.TOOL.MdlList.TableSelect(dt, "t_time>='" + kssjfw[kw] + " 00:00:00" + "' and t_time<='" + jssjfw[kw] + " 23:59:59" + "'");
                            if (kwdt != null)
                            {
                                if (kwdt.Rows.Count > 0)
                                {
                                    object sumObject = kwdt.Compute("sum(t_activecount)", "TRUE");//根据DataTable列求和
                                    htmls += sumObject + "|";
                                }
                                else
                                {
                                    htmls += "0|";
                                }
                            }
                            else
                            {
                                htmls += "0|";
                            }
                        }
                    }
                    else
                    {
                        return "0";
                    }
                    #endregion
                    break;
                case "month"://月
                    #region 按月
                    if (dt.Rows.Count > 0)
                    {
                        DateTime monthtime = DateTime.Parse(stime);
                        for (int mm = 0; mm <= xsmonth; mm++)
                        {
                            DataTable mdt = JMP.TOOL.MdlList.TableSelect(dt, "t_time>='" + monthtime.ToString("yyyy-MM-01") + " 00:00:00" + "' and t_time<='" + monthtime.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59" + "'");
                            if (mdt != null)
                            {
                                if (mdt.Rows.Count > 0)
                                {
                                    object sumObject = mdt.Compute("sum(t_activecount)", "TRUE");//根据DataTable列求和
                                    htmls += sumObject + "|";
                                }
                                else
                                {
                                    htmls += "0|";
                                }
                            }
                            else
                            {
                                htmls += "0|";
                            }
                            monthtime = monthtime.AddMonths(1);
                        }
                    }
                    else
                    {
                        return "0";
                    }
                    #endregion
                    break;
            }
            htmls = htmls.TrimEnd('|');
            htmls += "\"";
            htmls += "}";
            htmls += "]";
            htmls += "}";
            #endregion
            #endregion
            return htmls;
        }
        /// <summary>
        /// 查询留存用户信息
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult keep()
        {
            string ksrq = DateTime.Now.ToString("yyyy-01-01");//获取本月第一天
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
            int searchType = string.IsNullOrEmpty(Request["searchType"]) ? 0 : Int32.Parse(Request["searchType"]);//查询条件
            ViewBag.searchType = searchType;
            string searchname = string.IsNullOrEmpty(Request["searchname"]) ? "" : Request["searchname"];//查询内容
            ViewBag.searchname = searchname;
            int setype = string.IsNullOrEmpty(Request["setype"]) ? 0 : Int32.Parse(Request["setype"]);//新增或者活跃
            ViewBag.setype = setype;
            int selecttype = string.IsNullOrEmpty(Request["selecttype"]) ? 0 : Int32.Parse(Request["selecttype"]);//日或周选择
            ViewBag.selecttype = selecttype;
            JMP.BLL.jmp_keep keepbll = new JMP.BLL.jmp_keep();
            DataTable dt = keepbll.SelectList(stime, etime, searchType, searchname, setype);
            ViewBag.dt = dt;
            return View();
        }
        /// <summary>
        /// 流失用户界面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult defect()
        {
            string ksrq = DateTime.Now.ToString("yyyy-01-01");//获取本月第一天
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
        /// 统计流失用户方法
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public string defectCount()
        {
            string stime = string.IsNullOrEmpty(Request["stime"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["stime"];//开始时间
            string etime = string.IsNullOrEmpty(Request["etime"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["etime"];//结束时间
            int searchType = string.IsNullOrEmpty(Request["searchType"]) ? 0 : Int32.Parse(Request["searchType"]);//查询条件
            string searchname = string.IsNullOrEmpty(Request["searchname"]) ? "" : Request["searchname"];//查询内容
            int stype = string.IsNullOrEmpty(Request["stype"]) ? 0 : Int32.Parse(Request["stype"]);//新增或活跃
            int sedatatype = string.IsNullOrEmpty(Request["sedatatype"]) ? 0 : Int32.Parse(Request["sedatatype"]);//连续不登陆天数
            string htmls = "{\"chart\": {\"caption\": \"\",\"subCaption\": \"\",\"xAxisname\": \"\", \"pYAxisName\": \"\",\"\": \"\",\"numberPrefix\": \"\",\"sNumberSuffix\": \"%\",\"sYAxisMaxValue\": \"50\",\"paletteColors\": \"#0075c2,#1aaf5d,#f2c500\", \"baseFontColor\": \"#333333\",\"baseFont\": \"Helvetica Neue,Arial\", \"captionFontSize\": \"14\",\"subcaptionFontSize\": \"14\",\"subcaptionFontBold\": \"0\",\"showBorder\": \"0\",\"bgColor\": \"#ffffff\",\"showShadow\": \"0\", \"canvasBgColor\": \"#ffffff\",\"canvasBorderAlpha\": \"0\",\"divlineAlpha\": \"100\",\"divlineColor\": \"#999999\",\"divlineThickness\": \"1\",\"divLineDashed\": \"1\",\"divLineDashLen\": \"1\",\"divLineGapLen\": \"1\",\"usePlotGradientColor\": \"0\",\"showplotborder\": \"0\",\"showXAxisLine\": \"1\",\"xAxisLineThickness\": \"1\",\"xAxisLineColor\": \"#999999\",\"showAlternateHGridColor\": \"0\", \"showAlternateVGridColor\":\"0\",\"legendBgAlpha\": \"0\",\"legendBorderAlpha\": \"0\",\"legendShadow\": \"0\",\"legendItemFontSize\": \"10\",\"legendItemFontColor\": \"#666666\",\"formatNumberScale\":\"false\"},";
            string valuestr = "{\"seriesName\": \"流失用户数\",\"data\": [";//柱状图字符串
            string valuest = "{\"seriesName\": \"每日流失率\",\"parentYAxis\": \"S\",\"renderAs\": \"line\",\"showValues\": \"0\",\"data\": [";//曲线图字符传
            htmls += "\"categories\": [{\"category\": [";
            JMP.BLL.jmp_defect debll = new JMP.BLL.jmp_defect();
            DataTable dt = debll.selectDataTable(stime, etime, searchType, searchname, stype, sedatatype);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    htmls += "{ \"label\": \"" + DateTime.Parse(dt.Rows[i]["d_time"].ToString()).ToString("MM/dd") + "\"},";//Y轴坐标
                    valuestr += "{\"value\": \"" + dt.Rows[i]["d_losscount"] + "\"},";//柱状图
                    valuest += "{ \"value\": \"" + (decimal.Parse(dt.Rows[i]["d_lossproportion"].ToString()) * 100) + "\"},";//曲线图
                }
            }
            else
            {
                return "0";
            }
            htmls = htmls.TrimEnd(',');
            valuestr = valuestr.TrimEnd(',');
            valuest = valuest.TrimEnd(',');
            valuest += "]}";
            valuestr += "]},";
            htmls += "]}],";
            htmls += " \"dataset\": [";
            htmls = htmls + valuestr + valuest;
            htmls += "]}";
            return htmls;
        }
    }
}
