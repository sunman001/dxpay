/************聚米支付平台__营收分析************/
//描述：开发者的营收分析
//功能：开发者的营收分析
//开发者：谭玉科
//开发时间: 2016.04.14
/************聚米支付平台__营收分析************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB.Controllers
{
    /// <summary>
    /// 类名：RevenueAnalysisController
    /// 功能：营收分析
    /// 详细：营收分析
    /// 修改日期：2016.03.24
    /// </summary>
    public class RevenueAnalysisController : Controller
    {
        JMP.BLL.jmp_revenue_add bll_add = new JMP.BLL.jmp_revenue_add();
        JMP.BLL.jmp_revenue_active bll_active = new JMP.BLL.jmp_revenue_active();

        JMP.BLL.jmp_appreport bll = new JMP.BLL.jmp_appreport();

        /// <summary>
        /// 营收概况页面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord =true)]
        public ActionResult RevenueProfile()
        {
            return View();
        }

        /// <summary>
        /// 营收概况--交易用户、金额及交易笔数
        /// </summary>        
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false), VisitRecord(IsRecord = true)]
        public string TradeMain()
        {
            string begin = !string.IsNullOrEmpty(Request["begin"]) ? Request["begin"] : DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string end = !string.IsNullOrEmpty(Request["end"]) ? Request["end"] : DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            string showType = !string.IsNullOrEmpty(Request["showType"]) ? Request["showType"] : "0";
            string sumType = !string.IsNullOrEmpty(Request["sumType"]) ? Request["sumType"] : "0";
            string sType = !string.IsNullOrEmpty(Request["sType"]) ? Request["sType"] : "0";
            string sKey = !string.IsNullOrEmpty(Request["sKey"]) ? Request["sKey"] : "";
            #region 查询数据
            DataTable dt = new DataTable();
            dt = bll.GetListys(begin, end, sType, sKey);
            #endregion
            string htmls = string.Empty;
            if (dt.Rows.Count > 0)
            {
                #region 查询有数据时才构造json数据
                htmls = "{\"chart\":{\"caption\":\"\",\"paletteColors\":\"#0075c2,#1aaf5d\",\"xAxisNameFontSize\":\"12\",\"subcaptionFontSize\":\"14\",\"subcaptionFontBold\":\"0\",\"showBorder\":\"0\",\"bgColor\":\"#ffffff\",\"baseFont\":\"Helvetica Neue,Arial\",\"showCanvasBorder\":\"0\",\"showShadow\":\"0\",\"showAlternateHGridColor\":\"0\",\"canvasBgColor\":\"#ffffff\",\"forceAxisLimits\":\"1\",\"pixelsPerPoint\":\"0\",\"pixelsPerLabel\":\"30\",\"lineThickness\":\"1\",\"compactdatamode\":\"1\",\"dataseparator\":\"|\",\"labelHeight\":\"70\",\"scrollheight\":\"10\",\"flatScrollBars\":\"1\",\"scrollShowButtons\":\"0\",\"scrollColor\":\"#cccccc\",\"legendBgAlpha\":\"0\",\"legendBorderAlpha\":\"0\",\"legendShadow\":\"0\",\"legendItemFontSize\":\"10\",\"legendItemFontColor\":\"#666666\",\"showToolTip\":\"true\"},";
                htmls += "\"categories\": [";
                htmls += "{";
                htmls += "\"category\":\"";

                //计算X坐标长度
                ArrayList kssjfw = null;
                ArrayList jssjfw = null;
                TimeSpan tt = DateTime.Parse(end) - DateTime.Parse(begin);
                int len = 0;
                if (showType == "0")//天
                    len = tt.Days;
                else if (showType == "1")//周
                {
                    kssjfw = JMP.TOOL.WeekDateTime.WeekMonday(DateTime.Parse(begin), DateTime.Parse(end));//根据时间返回获取每周周一
                    jssjfw = JMP.TOOL.WeekDateTime.WeekDay(DateTime.Parse(begin), DateTime.Parse(end));//根据时间返回获取每周星期天
                    len = kssjfw.Count - 1;
                }
                else if (showType == "2")//月
                    len = DateTime.Parse(end).Month - DateTime.Parse(begin).Month;
                #region 构造X坐标
                DateTime temp = DateTime.Parse(begin);
                for (int i = 0; i <= len; i++)
                {
                    switch (showType)
                    {
                        case "0":
                            htmls += temp.ToString("yy-MM-dd") + "|";
                            temp = temp.AddDays(1);
                            break;
                        case "1":
                            if (i == 0)
                                htmls += DateTime.Parse(begin).ToString("yy-MM-dd") + "至" + DateTime.Parse(jssjfw[i].ToString()).ToString("yy-MM-dd") + "|";
                            else
                                htmls += DateTime.Parse(kssjfw[i].ToString()).ToString("yy-MM-dd") + "至" + DateTime.Parse(jssjfw[i].ToString()).ToString("yy-MM-dd") + "|";
                            break;
                        case "2":
                            htmls += temp.ToString("yy-MM") + "|";
                            temp = temp.AddMonths(1);
                            break;
                    }
                }
                htmls = htmls.TrimEnd('|');
                htmls += "\"}";
                htmls += "],";
                #endregion

                htmls += "\"dataset\": [";

                #region 根据时间段取出数据
                Dictionary<string, object> dictUser = new Dictionary<string, object>();//交易用户数
                Dictionary<string, object> dictMoney = new Dictionary<string, object>();//交易金额
                Dictionary<string, object> dictOrder = new Dictionary<string, object>();//交易笔数
                Dictionary<string, object> dictnotpay = new Dictionary<string, object>();//未成交笔数
                if (showType == "0")
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dictUser.Add(DateTime.Parse(dr["a_time"].ToString()).ToString("yy-MM-dd"), dr["a_number"]);
                        dictMoney.Add(DateTime.Parse(dr["a_time"].ToString()).ToString("yy-MM-dd"), dr["a_curr"]);
                        dictOrder.Add(DateTime.Parse(dr["a_time"].ToString()).ToString("yy-MM-dd"), dr["a_success"]);
                        dictnotpay.Add(DateTime.Parse(dr["a_time"].ToString()).ToString("yy-MM-dd"),dr["a_notpay"]);
                    }
                }
                else
                {
                    //按周和月展示
                    DateTime t_tep = DateTime.Parse(begin);
                    for (int k = 0; k <= len; k++)
                    {
                        DataRow[] n_dr = null;
                        string tKey = string.Empty;
                        switch (showType)
                        {
                            case "1":
                                n_dr = dt.Select("a_time>='" + DateTime.Parse(kssjfw[k].ToString()).ToString("yy-MM-dd") + "' and a_time<'" + DateTime.Parse(jssjfw[k].ToString()).ToString("yy-MM-dd") + "'");
                                if (k == 0)
                                    tKey = DateTime.Parse(begin).ToString("yy-MM-dd") + "至" + DateTime.Parse(jssjfw[k].ToString()).ToString("yy-MM-dd");
                                else
                                    tKey = DateTime.Parse(kssjfw[k].ToString()).ToString("yy-MM-dd") + "至" + DateTime.Parse(jssjfw[k].ToString()).ToString("yy-MM-dd");
                                break;
                            case "2":
                                n_dr = dt.Select("a_time>='" + t_tep.ToString("yyyy-MM-01") + "' and a_time<'" + DateTime.Parse(t_tep.ToString("yyyy-MM-01")).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd") + "'");
                                tKey = t_tep.ToString("yy-MM");
                                t_tep = t_tep.AddMonths(1);
                                break;
                        }

                        DataTable n_dt = n_dr.Length > 0 ? n_dr.CopyToDataTable() : null;
                        decimal a_number = 0, a_curr = 0, a_success = 0, a_notpay = 0;
                        if (n_dt != null)
                        {
                            //交易用户数
                            string str_user = n_dt.Compute("sum(a_number)", "a_number>0").ToString();
                            a_number = !string.IsNullOrEmpty(str_user) ? decimal.Parse(str_user) : 0;
                            //交易金额
                            string str_mney = n_dt.Compute("sum(a_curr)", "a_curr>0").ToString();
                            a_curr = !string.IsNullOrEmpty(str_mney) ? decimal.Parse(str_mney) : 0;
                            //交易笔数
                            string str_order = n_dt.Compute("sum(a_success)", "a_success>0").ToString();
                            a_success = !string.IsNullOrEmpty(str_order) ? decimal.Parse(str_order) : 0;

                            string str_pay= n_dt.Compute("sum(a_notpay)", "a_notpay>0").ToString();
                            a_notpay = !string.IsNullOrEmpty(str_pay) ? decimal.Parse(str_pay) : 0;
                        }
                        dictUser.Add(tKey, a_number);
                        dictMoney.Add(tKey, a_curr);
                        dictOrder.Add(tKey, a_success);
                        dictnotpay.Add(tKey,a_notpay);
                    }
                }
                #endregion
                #region 构造折线
                //用户类型（新增或活跃）
                switch (sumType)
                {
                    case "0":
                        htmls += BuildJsonStr(begin, end, showType, len, "交易用户", dictUser);
                        htmls += BuildJsonStr(begin, end, showType, len, "交易金额", dictMoney);
                        break;
                    case "1":
                        htmls += BuildJsonStr(begin, end, showType, len, "交易笔数", dictOrder);
                        htmls += BuildJsonStr(begin, end, showType, len, "未成交笔数", dictnotpay);
                        break;
                }
                #endregion
                htmls = htmls.TrimEnd(',');
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
        /// 构造折线
        /// </summary>
        /// <param name="start">开始日期</param>
        /// <param name="etime">结束日期</param>
        /// <param name="sType">展示类型（按日、周、月展示）</param>
        /// <param name="len">横坐标长度</param>
        /// <param name="showName">折线名称</param>
        /// <param name="dict">键值对</param>
        /// <returns></returns>
        private string BuildJsonStr(string start, string etime, string sType, int lens, string showName, Dictionary<string, object> dict)
        {
            ArrayList kssjfw = JMP.TOOL.WeekDateTime.WeekMonday(DateTime.Parse(start), DateTime.Parse(etime));//根据时间返回获取每周周一
            ArrayList jssjfw = JMP.TOOL.WeekDateTime.WeekDay(DateTime.Parse(start), DateTime.Parse(etime));//根据时间返回获取每周星期天

            if (sType == "1")
                lens = kssjfw.Count - 1;

            DateTime t_date = DateTime.Parse(start);
            string htmls = "{";
            htmls += "\"seriesName\":\"" + showName + "\",";
            htmls += "\"data\": \"";
            for (int i = 0; i <= lens; i++)
            {
                string curr = string.Empty;
                switch (sType)
                {
                    case "0":
                        curr = t_date.ToString("yy-MM-dd");
                        t_date = t_date.AddDays(1);
                        break;
                    case "1":
                        if (i == 0)
                            curr = DateTime.Parse(start).ToString("yy-MM-dd") + "至" + DateTime.Parse(jssjfw[i].ToString()).ToString("yy-MM-dd");
                        else
                            curr = DateTime.Parse(kssjfw[i].ToString()).ToString("yy-MM-dd") + "至" + DateTime.Parse(jssjfw[i].ToString()).ToString("yy-MM-dd");
                        break;
                    case "2":
                        curr = t_date.ToString("yy-MM");
                        t_date = t_date.AddMonths(1);
                        break;
                    default:
                        break;
                }

                if (dict.ContainsKey(curr))
                    htmls += dict[curr] + "|";
                else
                    htmls += "0|";

            }
            htmls = htmls.TrimEnd('|');
            htmls += "\"";
            htmls += "},";

            return htmls;
        }

        /// <summary>
        /// 销售排行页面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord =true)]
        public ActionResult SalesRank()
        {
            return View();
        }

        /// <summary>
        /// 销售排行--交易额排行top10
        /// </summary>        
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false),VisitRecord(IsRecord = true)]
        public string SalesTopTen()
        {
            string begin = !string.IsNullOrEmpty(Request["begin"]) ? Request["begin"] : DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string end = !string.IsNullOrEmpty(Request["end"]) ? Request["end"] : DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            string sumType = !string.IsNullOrEmpty(Request["sumType"]) ? Request["sumType"] : "0";
            string sType = !string.IsNullOrEmpty(Request["sType"]) ? Request["sType"] : "0";
            string sKeys = !string.IsNullOrEmpty(Request["sKeys"]) ? Request["sKeys"] : "";
            #region 查询数据
            DataTable tab = null;
            DataTable dt = null;
            switch (sumType)
            {
                case "0"://应用
                    JMP.BLL.jmp_sales_app bll_sales_app = new JMP.BLL.jmp_sales_app();
                    tab = bll_sales_app.GetLists(begin, end, sType, sKeys, 10);
                    dt = bll_sales_app.GetLists(begin, end, sType, sKeys);
                    break;
                case "1"://商户
                    JMP.BLL.jmp_sales_user bll_sales_user = new JMP.BLL.jmp_sales_user();
                    tab = bll_sales_user.GetLists(begin, end, sType, sKeys, 10);
                    dt = bll_sales_user.GetLists(begin, end, sType, sKeys);
                    break;
                case "2"://渠道                    
                    JMP.BLL.jmp_sales_pay bll_sales_pay = new JMP.BLL.jmp_sales_pay();
                    tab = bll_sales_pay.GetLists(begin, end, sType, sKeys, 10);
                    dt = bll_sales_pay.GetLists(begin, end, sType, sKeys);
                    break;
            }
            #endregion
            string htmls = string.Empty;
            if (tab.Rows.Count > 0 || dt.Rows.Count > 0)
            {
                #region 查询有数据时才构造json数据
                htmls = "{\"chart\": {\"caption\": \"\",\"subCaption\": \"\",\"numberSuffix\": \"%\",\"paletteColors\": \"#0075c2\",\"bgColor\": \"FFFFFF\",\"showBorder\": \"0\",\"showCanvasBorder\": \"0\",\"usePlotGradientColor\": \"0\",\"plotBorderAlpha\": \"10\",\"placeValuesInside\": \"1\",\"valueFontColor\": \"#ffffff\",\"showAxisLines\": \"1\",\"axisLineAlpha\": \"25\",\"divLineAlpha\": \"10\",\"alignCaptionWithCanvas\": \"0\",\"showAlternateVGridColor\": \"0\",\"captionFontSize\": \"14\",\"subcaptionFontSize\": \"14\",\"subcaptionFontBold\": \"0\",\"toolTipColor\": \"#ffffff\",\"toolTipBorderThickness\": \"0\",\"toolTipBgColor\": \"#000000\",\"toolTipBgAlpha\": \"80\",\"toolTipBorderRadius\": \"2\",\"toolTipPadding\": \"5\"}";
                htmls += ",\"data\": [";

                string datastr = "";
                //计算交易总额和
                string t_curr = dt.Compute("sum(r_moneys)", "r_moneys>0").ToString();
                decimal totalMoney = decimal.Parse(string.IsNullOrEmpty(t_curr) ? "0" : t_curr);
                foreach (DataRow dr in tab.Rows)
                {
                    decimal curr = decimal.Parse(dr["r_moneys"].ToString());
                    decimal rate = totalMoney == 0 ? 0 : curr / totalMoney;
                    datastr += "{\"label\": \"" + dr["tname"].ToString() + "\", \"value\": \"" + (rate * 100).ToString("f2") + "\"},";
                }

                htmls += datastr.TrimEnd(',');
                htmls += "]}";
                #endregion
            }
            else
            {
                htmls = "0";
            }
            return htmls;
        }

    }
}
