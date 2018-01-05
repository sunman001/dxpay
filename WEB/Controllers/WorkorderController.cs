using JMP.Model.Query.WorkOrder;
using JMP.TOOL;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOOL;
using TOOL.Extensions;
using TOOL.Message.AudioMessage.ChuangLan;
using WEB.Extensions;
using WEB.Util;
using WEB.Util.Logger;
using WEB.ViewModel.Scheduler;

namespace WEB.Controllers
{
    public class WorkorderController : Controller
    {
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;
        JMP.BLL.jmp_limit bll_limit = new JMP.BLL.jmp_limit();
        JMP.BLL.jmp_workorder bll = new JMP.BLL.jmp_workorder();
        JMP.MDL.jmp_workorder model = new JMP.MDL.jmp_workorder();
        JMP.MDL.jmp_exchange exchangemodel = new JMP.MDL.jmp_exchange();
        JMP.MDL.jmp_scheduling schedumodle = new JMP.MDL.jmp_scheduling();
        JMP.BLL.jmp_exchange exchangebll = new JMP.BLL.jmp_exchange();

        #region 工单管理
        /// <summary>
        ///工单列表
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false), VisitRecord(IsRecord = true)]
        public ActionResult WorkorderList()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            string status = string.IsNullOrEmpty(Request["status"]) ? "" : Request["status"];//工单状态
            string progress = string.IsNullOrEmpty(Request["progress"]) ? "" : Request["progress"];//工单进度
            int type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);//查询条件选择
            string stime = string.IsNullOrEmpty(Request["r_begin"]) ? DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") : Request["r_begin"];
            string etime = string.IsNullOrEmpty(Request["r_end"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["r_end"];
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            List<JMP.MDL.jmp_workorder> list = new List<JMP.MDL.jmp_workorder>();

            list = bll.SelectList(status, progress, sea_name, type, searchDesc, stime, etime, pageIndexs, PageSize, out pageCount);
            int id = 0; string name = ""; string tel = ""; string email = ""; string qq = "";
            schedumodle = bll.SelectByDate(DateTime.Now);
            if (schedumodle != null)
            {
                id = schedumodle.watchid > 0 ? schedumodle.watchid : 0;
                name = string.IsNullOrEmpty(schedumodle.u_realname) ? "" : schedumodle.u_realname;
                tel = string.IsNullOrEmpty(schedumodle.u_mobilenumber) ? "" : schedumodle.u_mobilenumber;
                email = string.IsNullOrEmpty(schedumodle.u_emailaddress) ? "" : schedumodle.u_emailaddress;
                qq = string.IsNullOrEmpty(schedumodle.u_qq) ? "" : schedumodle.u_qq;
            }
            var isjs = false;
            var jsRoleId = ConfigurationManager.AppSettings["JSRoleID"];
            if (jsRoleId == UserInfo.UserRoleId.ToString())
            {
                isjs = true;
            }
            ViewBag.ISJS = isjs;
            ViewBag.id = id;
            ViewBag.name = name;
            ViewBag.tel = tel;
            ViewBag.email = email;
            ViewBag.qq = qq;
            ViewBag.searchDesc = searchDesc;
            ViewBag.type = type;
            ViewBag.sea_name = sea_name;
            ViewBag.stime = stime;
            ViewBag.etime = etime;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            ViewBag.status = status;
            ViewBag.progress = progress;
            ViewBag.locUrl = GetVoidHtml();
            return View();
        }

        public string GetVoidHtml()
        {
            string locUrl = "";
            string u_id = UserInfo.UserId.ToString();
            int r_id = int.Parse(UserInfo.UserRoleId.ToString());
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/Workorder/InsertWorkorder", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加管理员
            if (getlocuserAdd)
            {
                locUrl += "<li onclick=\"AddAPPlog()\"><i class='fa fa-plus'></i>添加工单</li>";
            }
            return locUrl;
        }

        /// <summary>
        /// 添加工单
        /// </summary>
        /// <returns></returns>
        public ActionResult WorkorderAdd()
        {

            schedumodle = bll.SelectByDate(DateTime.Now);
            int id = 0; string name = "";
            if (schedumodle != null)
            {
                id = schedumodle.watchid > 0 ? schedumodle.watchid : 0;
                name = string.IsNullOrEmpty(schedumodle.u_realname) ? "" : schedumodle.u_realname;
            }
            ViewBag.id = id;
            ViewBag.name = name;
            return View();
        }
        /// <summary>
        /// 添加或修改工单管理
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult InsertWorkorder(JMP.MDL.jmp_workorder model)
        {
            object retJson = new { success = 0, msg = "操作失败" };
            JMP.BLL.jmp_workorder bll = new JMP.BLL.jmp_workorder();

            model.createdon = DateTime.Now;
            //状态
            model.status = 0;
            //进度
            model.progress = 0;
            //查看次数
            model.viewcount = 0;
            model.createdbyid = UserInfo.UserId;
            model.pushedremind = false;
            int cg = bll.Add(model);
            if (cg > 0)
            {

                Logger.CreateLog("添加工单", model);
                retJson = new { success = 1, msg = "添加成功" };

            }
            else
            {
                retJson = new { success = 1, msg = "添加失败" };
            }
            return Json(retJson);
        }

        //关闭页面
        public ActionResult WorkorderGB()
        {
            int id = int.Parse(Request["id"]);
            ViewBag.id = id;
            return View();
        }

        //关闭状态
        public JsonResult UpdateState()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            int id = int.Parse(Request["ids"]);
            string state = Request["state"];
            string closeReason = Request["closeReason"];
            JMP.BLL.jmp_workorder bll = new JMP.BLL.jmp_workorder();

            if (bll.UpdateState(id, state, closeReason))
            {
                Logger.OperateLog("关闭工单", "关闭工单");
                retJson = new { success = 1, msg = "关闭成功" };
            }
            else
            {

                retJson = new { success = 0, msg = "关闭失败" };
            }
            return Json(retJson);
        }

        //处理
        public ActionResult WorkorderCL()
        {
            int id = int.Parse(Request["id"]);
            List<JMP.MDL.jmp_exchange> list = new List<JMP.MDL.jmp_exchange>();
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 10 : Int32.Parse(Request["PageSize"]);//每页显示数量

            //点击分页传递参数判断
            int jl = string.IsNullOrEmpty(Request["jl"]) ? 1 : Int32.Parse(Request["jl"]);

            if (id > 0)
            {
                model = bll.SelectId(id);
                list = exchangebll.SelectListByworkorderid(id, pageIndexs, PageSize, out pageCount);
                bll.UpdateView(id, DateTime.Now);
            }
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.id = id;
            ViewBag.model = model == null ? new JMP.MDL.jmp_workorder() : model;
            ViewBag.list = list;

            ViewBag.jl = jl;

            return View();
        }

        //处理结果以及交流内容
        public JsonResult WorkorderCLJG()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            int id = int.Parse(Request["id"]);
            int status = int.Parse(Request["status"]);
            int progress = int.Parse(Request["progress"]);
            string handleResultDescription = Request["handleResultDescription"];
            exchangemodel.handleresultdescription = handleResultDescription;
            exchangemodel.handlerid = UserInfo.UserId;
            exchangemodel.workorderid = id;
            exchangemodel.handledate = DateTime.Now;
            int state = 1;
            if (progress == 0 && status == 0)
            {
                if (bll.UpdateProgress(id, state))
                {
                    Logger.OperateLog("改变工单进度", "改变工单进度为处理中");
                    if (bll.updateResult(id, handleResultDescription))
                    {
                        Logger.OperateLog("修改工单响应内容", "修改工单响应内容为" + handleResultDescription);
                    }
                    else
                    {
                        retJson = new { success = 0, msg = "工单处理失败" };
                    }

                }
                else
                {
                    retJson = new { success = 0, msg = "工单处理失败" };
                }
            }
            int cg = exchangebll.Add(exchangemodel);
            if (cg > 0)
            {

                Logger.CreateLog("工单交流", model);
                retJson = new { success = 1, msg = "回复成功" };

            }
            else
            {
                retJson = new { success = 1, msg = "回复失败" };
            }


            return Json(retJson);
        }

        //完成工单页面
        public ActionResult WorkorderWC()
        {
            int id = int.Parse(Request["id"]);
            ViewBag.id = id;
            return View();
        }
        //工单处理完成
        public JsonResult WorkorderWCJG()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            int id = int.Parse(Request["id"]);
            string handleResultDescription = string.IsNullOrEmpty(Request["handleResultDescription"]) ? "" : Request["handleResultDescription"];//交流内容
            int state = 2;
            if (bll.UpdateProgress(id, state))
            {
                Logger.OperateLog("修改工单进度", "工单进度修改为处理中");
                if (handleResultDescription != "")
                {
                    exchangemodel.handleresultdescription = handleResultDescription;
                    exchangemodel.handlerid = UserInfo.UserId;
                    exchangemodel.workorderid = id;
                    exchangemodel.handledate = DateTime.Now;
                    int cg = exchangebll.Add(exchangemodel);
                    if (cg > 0)
                    {

                        Logger.CreateLog("工单交流", model);


                    }
                    else
                    {
                        retJson = new { success = 1, msg = "处理失败" };
                    }
                    retJson = new { success = 1, msg = "处理成功" };
                }
            }
            else
            {
                retJson = new { success = 0, msg = "处理失败" };
            }
            return Json(retJson);
        }

        //提交人申诉
        public ActionResult WorkorderTJSHS()
        {
            int id = int.Parse(Request["id"]);
            ViewBag.id = id;
            return View();
        }
        //提交人申诉结果
        public JsonResult WorkorderTJSHSJG()
        {
            object retJson = new { success = 0, msg = "操作失败" };

            JMP.BLL.jmp_workorder bll = new JMP.BLL.jmp_workorder();

            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);
            int nans = string.IsNullOrEmpty(Request["nans"]) ? 0 : int.Parse(Request["nans"]);
            string handleResultDescription = string.IsNullOrEmpty(Request["handleResultDescription"]) ? "" : Request["handleResultDescription"];

            switch (nans)
            {
                case 1:
                    //同意
                    if (bll.UpdateProgress(id, 4))
                    {
                        Logger.OperateLog("同意工单申述", "处理进度：已完成");

                        retJson = new { success = 1, msg = "操作成功" };
                    }
                    else
                    {
                        retJson = new { success = 0, msg = "操作失败" };
                    }

                    break;
                case 2:
                    //申述
                    if (bll.UpdateState(id, "-2", handleResultDescription))
                    {
                        Logger.OperateLog("不同意工单申述", "不同意原因：" + handleResultDescription);

                        retJson = new { success = 1, msg = "提交成功" };
                    }
                    else
                    {
                        retJson = new { success = 0, msg = "提交失败" };

                    }
                    break;
            }



            return Json(retJson);
        }

        //处理人申诉
        public ActionResult WorkorderCLSHS()
        {
            int id = int.Parse(Request["id"]);
            ViewBag.id = id;
            return View();

        }

        //处理人申诉结果
        public JsonResult WorkorderCLSHSJG()
        {
            object retJson = new { success = 0, msg = "操作失败" };

            int id = int.Parse(Request["id"]);
            int state = int.Parse(Request["state"]);
            int prec = 4;
            string handlerReason = Request["handlerReason"];
            //同意
            if (state == 1)
            {
                if (bll.UpdateProgress(id, prec))
                {
                    Logger.OperateLog("修改工单进度", "工单进度修改为已完成");
                    retJson = new { success = 1, msg = "处理成功" };
                }
                else
                {
                    retJson = new { success = 0, msg = "处理失败" };
                }
            }
            //不同意评价
            else if (state == 0)
            {
                string Status = "-2";
                if (bll.UpdateHandState(id, Status, handlerReason))
                {
                    Logger.OperateLog("申诉工单", "工单状态修改为申诉");
                    retJson = new { success = 1, msg = "申诉成功" };
                }
                else
                {

                    retJson = new { success = 0, msg = "申诉失败" };
                }
            }
            return Json(retJson);
        }
        //评分页面
        public ActionResult WorkorderPF()
        {
            int id = int.Parse(Request["id"]);
            ViewBag.id = id;
            return View();
        }

        //评分结果
        public JsonResult WorkorderPFJG(JMP.MDL.jmp_workorder mode)
        {
            object retJson = new { success = 0, msg = "操作失败" };

            if (mode.id > 0)
            {
                #region 修改信息

                JMP.MDL.jmp_workorder mo = new JMP.MDL.jmp_workorder();
                mo = bll.GetModel(mode.id);

                //评分
                if (bll.UpdateScore(mode))
                {
                    Logger.ModifyLog("修改工单信息，添加评分信息", mo, mode);

                    //修改进度状态为 已评分
                    if (bll.UpdateProgress(mode.id, 3))
                    {
                        Logger.OperateLog("评分完成修改进度状态", "处理进度：已评分");

                        retJson = new { success = 1, msg = "评分成功" };

                    }
                    else
                    {
                        retJson = new { success = 0, msg = "评分失败" };
                    }

                }
                else
                {
                    retJson = new { success = 0, msg = "评分失败" };
                }

                #endregion
            }

            return Json(retJson);
        }

        /// <summary>
        /// 查看详细
        /// </summary>
        /// <returns></returns>
        public ActionResult WorkorderInfo()
        {
            int id = int.Parse(Request["id"]);
            List<JMP.MDL.jmp_exchange> list = new List<JMP.MDL.jmp_exchange>();
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 10 : Int32.Parse(Request["PageSize"]);//每页显示数量

            int jl = string.IsNullOrEmpty(Request["jl"]) ? 1 : Int32.Parse(Request["jl"]);

            if (id > 0)
            {
                model = bll.SelectId(id);
                list = exchangebll.SelectListByworkorderid(id, pageIndexs, PageSize, out pageCount);
            }
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.id = id;
            ViewBag.model = model == null ? new JMP.MDL.jmp_workorder() : model;
            ViewBag.list = list;

            ViewBag.jl = jl;

            return View();
        }

        /// <summary>
        /// 工单统计
        /// </summary>
        /// <returns></returns>
        public ActionResult WorkorderTJ()
        {

            return View();
        }
        /// <summary>
        /// 工单统计方法
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false), VisitRecord(IsRecord = true)]
        public string TradeMain()
        {

            string begin = !string.IsNullOrEmpty(Request["begin"]) ? Request["begin"] : DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string end = !string.IsNullOrEmpty(Request["end"]) ? Request["end"] : DateTime.Now.ToString("yyyy-MM-dd");
            string showType = !string.IsNullOrEmpty(Request["showType"]) ? Request["showType"] : "0";

            #region 查询数据
            DataTable dt = new DataTable();
            dt = bll.GetListys(begin, end);
            #endregion
            string htmls = string.Empty;
            if (dt.Rows.Count > 0)
            {
                #region 查询有数据时才构造json数据
                htmls += "{\"chart\":{\"caption\":\"\",\"numberprefix\":\"\",\"plotgradientcolor\":\"\",\"bgcolor\":\"ffffff\",\"showalternatehgridcolor\":\"0\",\"divlinecolor\":\"cccccc\",\"showvalues\":\"0\",\"showcanvasborder\":\"0\",\"canvasborderalpha\":\"0\",\"canvasbordercolor\":\"cccccc\",\"canvasborderthickness\":\"1\",\"yaxismaxvalue\":\"\",\"captionpadding\":\"50\",\"linethickness\":\"3\",\"yaxisvaluespadding\":\"30\",\"legendshadow\":\"0\",\"legendborderalpha\":\"0\",\"palettecolors\":\"#f8bd19,#008ee4,#33bdda,#e44a00,#6baa01,#583e78\",\"showborder\":\"0\",\"toolTipSepChar\":\"&nbsp;/&nbsp;\",\"formatNumberScale\":\"0\"},";
                htmls += "\"categories\": [";
                htmls += "{";
                htmls += "\"category\": [";

                //计算X坐标长度
                ArrayList kssjfw = null;
                ArrayList jssjfw = null;
                TimeSpan tt = DateTime.Parse(end) - DateTime.Parse(begin);
                int len = 0;
                switch (showType)
                {
                    case "0"://天
                        len = tt.Days;
                        break;
                    case "1"://周
                        kssjfw = JMP.TOOL.WeekDateTime.WeekMonday(DateTime.Parse(begin), DateTime.Parse(end));//根据时间返回获取每周周一
                        jssjfw = JMP.TOOL.WeekDateTime.WeekDay(DateTime.Parse(begin), DateTime.Parse(end));//根据时间返回获取每周星期天
                        len = kssjfw.Count - 1;
                        break;
                    case "2"://月
                        len = DateTime.Parse(end).Month - DateTime.Parse(begin).Month;

                        break;
                }
                #region 构造X坐标
                DateTime temp = DateTime.Parse(begin);
                for (int i = 0; i <= len; i++)
                {
                    switch (showType)
                    {
                        case "0":
                            htmls += "{\"label\": \"" + temp.ToString("yy-MM-dd") + "\"},";
                            temp = temp.AddDays(1);
                            break;
                        case "1":
                            if (i == 0)
                            {
                                //htmls += DateTime.Parse(begin).ToString("yy-MM-dd") + "至" + DateTime.Parse(jssjfw[i].ToString()).ToString("yy-MM-dd") + "|";
                                htmls += "{\"label\": \"" + DateTime.Parse(begin).ToString("yy-MM-dd") + "至" + DateTime.Parse(jssjfw[i].ToString()).ToString("yy-MM-dd") + "\"},";
                            }
                            else
                            {
                                //htmls += DateTime.Parse(kssjfw[i].ToString()).ToString("yy-MM-dd") + "至" + DateTime.Parse(jssjfw[i].ToString()).ToString("yy-MM-dd") + "|";
                                htmls += "{\"label\": \"" + DateTime.Parse(kssjfw[i].ToString()).ToString("yy-MM-dd") + "至" + DateTime.Parse(jssjfw[i].ToString()).ToString("yy-MM-dd") + "\"},";
                            }
                            break;
                        case "2":
                            htmls += "{\"label\": \"" + temp.ToString("yy-MM") + "\"},";
                            temp = temp.AddMonths(1);
                            break;
                    }
                }
                htmls = htmls.TrimEnd(',');
                htmls += "]";
                htmls += "}";
                htmls += "],";
                #endregion

                htmls += "\"dataset\": [";
                #region 根据时间段取出数据
                Dictionary<string, object> dictcount = new Dictionary<string, object>();//工单总量
                Dictionary<string, object> dictcgcount = new Dictionary<string, object>();//成功处理量 
                Dictionary<string, object> dictavgxy = new Dictionary<string, object>();//平均响应时间
                Dictionary<string, object> dictavgpf = new Dictionary<string, object>();//平均评分
                if (showType == "0")
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dictcount.Add(DateTime.Parse(dr["createdon"].ToString()).ToString("yy-MM-dd"), dr["countworkorder"]);
                        dictcgcount.Add(DateTime.Parse(dr["createdon"].ToString()).ToString("yy-MM-dd"), dr["sucessworkorder"]);
                        dictavgxy.Add(DateTime.Parse(dr["createdon"].ToString()).ToString("yy-MM-dd"), dr["branch"]);
                        dictavgpf.Add(DateTime.Parse(dr["createdon"].ToString()).ToString("yy-MM-dd"), dr["socre"]);
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
                                n_dr = dt.Select("createdon >='" + kssjfw[k].ToString() + "' and  createdon <='" + jssjfw[k].ToString() + "'");
                                if (k == 0)
                                    tKey = begin + "至" + jssjfw[k].ToString();
                                else
                                    tKey = kssjfw[k].ToString() + "至" + jssjfw[k].ToString();
                                break;
                            case "2":
                                n_dr = dt.Select("createdon>='" + t_tep.ToString("yyyy-MM-01") + "' and createdon<'" + DateTime.Parse(t_tep.ToString("yyyy-MM-01")).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd") + "'");
                                tKey = t_tep.ToString("yy-MM");
                                t_tep = t_tep.AddMonths(1);
                                break;
                        }

                        DataTable n_dt = n_dr.Length > 0 ? n_dr.CopyToDataTable() : null;
                        decimal a_number = 0, a_curr = 0, a_success = 0, a_notpay = 0;
                        if (n_dt != null)
                        {
                            //工单总量
                            string str_user = n_dt.Compute("sum(countworkorder)", "countworkorder>0").ToString();
                            a_number = !string.IsNullOrEmpty(str_user) ? decimal.Parse(str_user) : 0;
                            //成功处理量
                            string str_mney = n_dt.Compute("sum(sucessworkorder)", "sucessworkorder>0").ToString();
                            a_curr = !string.IsNullOrEmpty(str_mney) ? decimal.Parse(str_mney) : 0;
                            //平均响应时间分钟
                            string str_order = n_dt.Compute("avg(branch)", "branch>0").ToString();
                            a_success = !string.IsNullOrEmpty(str_order) ? decimal.Parse(str_order) : 0;
                            //平均评分
                            string str_pay = n_dt.Compute("avg(socre)", "socre>0").ToString();
                            a_notpay = !string.IsNullOrEmpty(str_pay) ? decimal.Parse(str_pay) : 0;
                            dictcount.Add(tKey, a_number);
                            dictcgcount.Add(tKey, a_curr);
                            dictavgxy.Add(tKey, a_success);
                            dictavgpf.Add(tKey, a_notpay);
                        }
                        //  dictcount.Add(tKey, a_number);
                        //dictcgcount.Add(tKey, a_curr);
                        // dictavgxy.Add(tKey, a_success);
                        //  dictavgpf.Add(tKey, a_notpay);
                    }
                }
                #endregion
                #region 构造折线
                //用户类型（新增或活跃）


                htmls += BuildJsonStr(begin, end, showType, len, "工单总量", dictcount) + ",";
                htmls += BuildJsonStr(begin, end, showType, len, "成功处理量", dictcgcount) + ",";
                htmls += BuildJsonStr(begin, end, showType, len, "平均响应时间分钟", dictavgxy) + ",";
                htmls += BuildJsonStr(begin, end, showType, len, "平均评分", dictavgpf) + ",";
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
            {
                lens = kssjfw.Count - 1;
            }
            DateTime t_date = DateTime.Parse(start);
            string htmls = "{ \"seriesName\": \"" + showName + "\", \"data\": [";
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
                            curr = start + "至" + jssjfw[i].ToString();
                        else
                            curr = kssjfw[i].ToString() + "至" + jssjfw[i].ToString();
                        break;
                    case "2":
                        curr = t_date.ToString("yy-MM");
                        t_date = t_date.AddMonths(1);
                        break;
                    default:
                        break;
                }

                if (dict.ContainsKey(curr))
                {
                    htmls += "{\"value\":\"" + dict[curr] + "\", \"toolText\":\"" + showName + ":" + dict[curr] + "\"},";
                }
                else
                {
                    htmls += "{\"value\":\"" + 0 + "\", \"toolText\":\"" + showName + ":0\"},";
                }
            }
            htmls = htmls.TrimEnd(',');
            htmls += "]}";
            return htmls;
        }


        public ActionResult WorkorderCLlist()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            string stime = string.IsNullOrEmpty(Request["r_begin"]) ? DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") : Request["r_begin"];
            string etime = string.IsNullOrEmpty(Request["r_end"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["r_end"];
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            List<JMP.MDL.jmp_workorderReport> list = new List<JMP.MDL.jmp_workorderReport>();
            list = bll.Getlist(sea_name, searchDesc, stime, etime, pageIndexs, PageSize, out pageCount);

            ViewBag.searchDesc = searchDesc;
            ViewBag.sea_name = sea_name;
            ViewBag.stime = stime;
            ViewBag.etime = etime;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            return View();
        }
        #endregion

        #region 值班表

        /// <summary>
        /// 值班列表
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult SchedulingList()
        {
            List<JMP.MDL.jmp_scheduling> list = new List<JMP.MDL.jmp_scheduling>();
            JMP.BLL.jmp_scheduling bll = new JMP.BLL.jmp_scheduling();
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            string Sname = string.IsNullOrEmpty(Request["WatchId"]) ? "" : Request["WatchId"];
            string startdate = string.IsNullOrEmpty(Request["WatchstartDate"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["WatchstartDate"];
            string enddate = string.IsNullOrEmpty(Request["WatchEndDate"]) ? DateTime.Now.AddDays(30).ToString("yyyy-MM-dd") : Request["WatchEndDate"];
            string jsbm = ConfigurationManager.AppSettings["jsbm"].ToString();
            string yyb = ConfigurationManager.AppSettings["yyb"].ToString();
            string position = ConfigurationManager.AppSettings["position"].ToString();
            int type = 0;
            bool isSelect = false;
            bool isType = true;
            if (UserInfo.UserRoleId == 1)
            {
                isType = false;
            }
            if (UserInfo.UserDept.Trim() == jsbm.Trim())
            {
                type = 1;
            }
            else if (UserInfo.UserDept.Trim() == yyb.Trim())
            {
                type = 2;
            }
            if (UserInfo.UserPostion.Trim() == position.Trim())
            {
                isSelect = false;
            }
            else
            {
                isSelect = true;
            }
            list = bll.SelectList(isType, Sname, type, isSelect, UserInfo.UserId, DateTime.Parse(startdate), DateTime.Parse(enddate).AddDays(1).AddMinutes(-1), pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            ViewBag.Sname = Sname;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            //
            string locUrl = "";
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/Workorder/InsertOrUpdateScheduling", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加
            if (getlocuserAdd)
            {
                locUrl += "<li onclick=\"AddSdl()\"><i class='fa fa-plus'></i>添加值班表</li>";
                locUrl += "<li onclick=\"AddSdlP()\"><i class='fa fa-plus'></i>批量排班</li>";
            }

            ViewBag.locUrl = locUrl;

            return View();
        }


        /// <summary>
        /// 添加修改值班表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddScheduling()
        {
            JMP.BLL.jmp_locuser bll = new JMP.BLL.jmp_locuser();
            JMP.BLL.jmp_scheduling blls = new JMP.BLL.jmp_scheduling();
            List<JMP.MDL.jmp_locuser> list = new List<JMP.MDL.jmp_locuser>();

            int s_id = string.IsNullOrEmpty(Request["s_id"]) ? 0 : Int32.Parse(Request["s_id"]);
            JMP.MDL.jmp_scheduling model = new JMP.MDL.jmp_scheduling();
            if (s_id > 0)
            {
                model = blls.GetModel(s_id);

            }
            ViewBag.model = model;
            string dept = UserInfo.UserDept;
            DataTable dt = bll.GetList(" 1=1 and u_department='" + dept + "' ").Tables[0];
            list = MdlList.ToList<JMP.MDL.jmp_locuser>(dt);
            ViewBag.list = list;

            return View();
        }

        public ActionResult EidtScheduling()
        {
            JMP.BLL.jmp_locuser bll = new JMP.BLL.jmp_locuser();
            JMP.BLL.jmp_scheduling blls = new JMP.BLL.jmp_scheduling();
            List<JMP.MDL.jmp_locuser> list = new List<JMP.MDL.jmp_locuser>();

            int s_id = string.IsNullOrEmpty(Request["s_id"]) ? 0 : Int32.Parse(Request["s_id"]);
            JMP.MDL.jmp_scheduling model = new JMP.MDL.jmp_scheduling();
            if (s_id > 0)
            {
                model = blls.GetModel(s_id);

            }
            ViewBag.model = model;
            string dept = UserInfo.UserDept;
            DataTable dt = bll.GetList(" 1=1 and u_department='" + dept + "' ").Tables[0];
            list = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_locuser>(dt);
            ViewBag.list = list;

            return View();

        }

        /// <summary>
        /// 添加修改值班表信息方法
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult InsertOrUpdateScheduling(JMP.MDL.jmp_scheduling mode)
        {
            object retJson = new { success = 0, msg = "操作失败" };
            JMP.BLL.jmp_scheduling bll = new JMP.BLL.jmp_scheduling();
            JMP.MDL.jmp_locuser lmode = new JMP.MDL.jmp_locuser();
            JMP.BLL.jmp_locuser lbll = new JMP.BLL.jmp_locuser();
            lmode = lbll.GetModel(mode.watchid);
            mode.Type = UserInfo.UserDept.ConvertDeptToInt();
            if (mode.id > 0)
            {
                //判断值班人是否已填写手机号码
                if (!string.IsNullOrEmpty(lmode.u_mobilenumber))
                {
                    #region 修改值班表信息
                    JMP.MDL.jmp_scheduling mo = new JMP.MDL.jmp_scheduling();
                    mo = bll.GetModel(mode.id);
                    mo.watchid = mode.watchid;
                    if (bll.Update(mo))
                    {
                        Logger.ModifyLog("修改值班表信息", mo, mode);

                        retJson = new { success = 1, msg = "修改成功" };
                    }
                    else
                    {
                        retJson = new { success = 0, msg = "修改失败" };
                    }

                    //录入人和时间
                    //mode.createdbyid = UserInfo.UserId;
                    //mode.createdon = DateTime.Now;
                    ////拼装值班开始结束时间
                    //string watchstartdate = Request["WatchstartDate"] + " " + ConfigurationManager.AppSettings["startTime"];
                    //string watchenddate = Request["WatchstartDate"] + " " + ConfigurationManager.AppSettings["startTime"];
                    ////间隔天数
                    //double S_ady = double.Parse(ConfigurationManager.AppSettings["S_ady"]);

                    //拼装值班开始结束时间
                    //var watchstartdate = Request["WatchstartDate"] + " " + ConfigurationManager.AppSettings["startTime"];
                    //var watchenddate = Request["WatchstartDate"] + " " + ConfigurationManager.AppSettings["endTime"];

                    //mode.watchstartdate = DateTime.Parse(watchstartdate);
                    //mode.watchenddate = DateTime.Parse(watchenddate);
                    //JMP.MDL.jmp_scheduling mo2 = new JMP.MDL.jmp_scheduling();

                    //mo2 = bll.GetModel(watchstartdate);

                    //if (mo2 != null)
                    //{
                    //    //判断是否已存在相同数据
                    //    if (mo2.id == mo.id)
                    //    {


                    //    }
                    //    else
                    //    {
                    //        retJson = new { success = 0, msg = "当前日期的值班信息已存在！" };
                    //    }

                    //}
                    //else
                    //{
                    //    if (bll.Update(mode))
                    //    {
                    //        Logger.ModifyLog("修改值班表信息", mo, mode);

                    //        retJson = new { success = 1, msg = "修改成功" };
                    //    }
                    //    else
                    //    {
                    //        retJson = new { success = 0, msg = "修改失败" };
                    //    }
                    //}
                    #endregion
                }
                else
                {
                    retJson = new { success = 0, msg = "修改失败,请先到-系统管理-用户列表-中完善值班人的手机号码！" };
                }
            }
            else
            {
                //判断值班人是否已填写手机号码
                if (!string.IsNullOrEmpty(lmode.u_mobilenumber))
                {
                    #region 添加值班表信息

                    //录入人和时间
                    mode.createdbyid = UserInfo.UserId;
                    mode.createdon = DateTime.Now;
                    //拼装值班开始结束时间
                    var watchstartdate = Request["WatchstartDate"] + " " + ConfigurationManager.AppSettings["startTime"];
                    var watchenddate = Request["WatchstartDate"] + " " + ConfigurationManager.AppSettings["endTime"];

                    mode.watchstartdate = DateTime.Parse(watchstartdate);
                    mode.watchenddate = DateTime.Parse(watchenddate);

                    var exists = bll.ScheduleExists(watchstartdate, mode.Type);

                    //JMP.MDL.jmp_scheduling mo = new JMP.MDL.jmp_scheduling();
                    //mo = bll.GetModel(watchstartdate, mode.Type);
                    //判断是否已存在相同数据
                    if (exists)
                    {
                        retJson = new { success = 0, msg = "当前日期的值班信息已存在！" };
                    }
                    else
                    {
                        int cg = bll.Add(mode);
                        if (cg > 0)
                        {
                            Logger.CreateLog("添加值班表信息", mode);

                            retJson = new { success = 1, msg = "添加成功" };
                        }
                        else
                        {
                            retJson = new { success = 0, msg = "添加失败" };
                        }

                    }
                    #endregion
                }
                else
                {
                    retJson = new { success = 0, msg = "添加失败,请先到-系统管理-用户列表-中完善值班人的手机号码！" };
                }


            }
            return Json(retJson);

        }

        #endregion

        #region 响应记录

        /// <summary>
        /// 响应记录列表
        /// </summary>
        /// <returns></returns>
        public ActionResult RespondList()
        {
            List<JMP.MDL.CsCustomerServiceRecord> list = new List<JMP.MDL.CsCustomerServiceRecord>();
            JMP.BLL.CsCustomerServiceRecord bll = new JMP.BLL.CsCustomerServiceRecord();
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量

            int searchType = string.IsNullOrEmpty(Request["searchType"]) ? -1 : Int32.Parse(Request["searchType"]);//type
            string s_key = string.IsNullOrEmpty(Request["s_key"]) ? "" : Request["s_key"];//查询条件选择
            string startdate = string.IsNullOrEmpty(Request["startdate"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["startdate"];
            string enddate = string.IsNullOrEmpty(Request["enddate"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["enddate"];
            int Status = string.IsNullOrEmpty(Request["Status"]) ? -1 : Int32.Parse(Request["Status"]);//状态
            int Grade = string.IsNullOrEmpty(Request["Grade"]) ? -1 : Int32.Parse(Request["Grade"]);//评分
            int AuditStatus = string.IsNullOrEmpty(Request["AuditStatus"]) ? -1 : Int32.Parse(Request["AuditStatus"]);//主管审核
            var watcherId = 0;
            if (!UserInfo.IsSuperAdmin && (UserInfo.UserPostion != ConfigurationManager.AppSettings["position"]))
            {
                watcherId = UserInfo.UserId;
            }

            if (searchType == 4)
            {
                var dev = new JMP.BLL.jmp_user().FindByRealName(s_key);
                s_key = "0";
                if (dev != null)
                {
                    s_key = dev.u_id.ToString();
                }
            }

            //查询所有响应记录
            list = bll.CsCustomerRecordList(searchType, s_key, Status, Grade, AuditStatus, startdate, enddate, pageIndexs, PageSize, out pageCount, watcherId);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            ViewBag.searchType = searchType;
            ViewBag.s_key = s_key;
            ViewBag.Status = Status;
            ViewBag.Grade = Grade;
            ViewBag.AuditStatus = AuditStatus;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;

            //添加响应记录
            string locUrl = "";
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/Workorder/InsertRespond", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加
            if (getlocuserAdd)
            {
                locUrl += "<li onclick=\"AddRespond()\"><i class='fa fa-plus'></i>添加响应记录</li>";
            }

            ViewBag.locUrl = locUrl;

            return View();
        }

        /// <summary>
        /// 添加响应记录
        /// </summary>
        /// <returns></returns>
        public ActionResult RespondAdd()
        {
            JMP.BLL.jmp_scheduling bll = new JMP.BLL.jmp_scheduling();

            var list = bll.FindAllWatcherOfTheDay();
            if (!list.Exists(x => x.Id == UserInfo.UserId))
            {
                //当前登录的操作员当天没有值班
                list.Clear();
                list.Add(new WatcherQuerier
                {
                    Id = UserInfo.UserId,
                    LoginName = UserInfo.UserNo,
                    RealName = UserInfo.UserName,
                    MobileNumber = ""
                });
            }
            ViewBag.list = list;

            var recordBll = new JMP.BLL.CsCustomerServiceRecord();

            var id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);
            //查询一条客服响应记录详情
            var mode = recordBll.GetModel(id);

            //证据图片
            if (mode == null)
            {
                ViewBag.EvidenceScreenshot = new string[] { };
                mode = new JMP.MDL.CsCustomerServiceRecord
                {
                    AskDate = DateTime.Now
                };
            }
            else
            {
                var evidenceScreenshot = string.IsNullOrEmpty(mode.EvidenceScreenshot) ? new string[] { } : mode.EvidenceScreenshot.Split(',');

                ViewBag.EvidenceScreenshot = evidenceScreenshot;
            }

            ViewBag.UploadUrl = ConfigurationManager.AppSettings["imgurl"];

            return View(mode);
        }

        /// <summary>
        /// 添加响应记录方法
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult RespondInsertAdd(JMP.MDL.CsCustomerServiceRecord mode)
        {
            var bll = new JMP.BLL.CsCustomerServiceRecord();
            var isNew = mode.Id <= 0;
            object retJson = new { success = 0, msg = "操作失败" };

            var watcherId = mode.WatchId;
            if (isNew)
            {
                mode.CreatedOn = DateTime.Now;
                mode.CreatedByUserId = UserInfo.UserId;
                mode.HandlerId = UserInfo.UserId;
                mode.HandlerName = UserInfo.UserName;
                mode.No = RandomHelper.GetRandomizer(10, true, false, true, true);
            }

            mode.Status = 0;
            mode.AuditStatus = false;
            mode.AuditByUserId = 0;
            mode.AuditByUserName = "";
            mode.AuditDate = null;
            mode.WatchId = UserInfo.UserId;

            if (isNew)
            {
                mode.Id = bll.Add(mode);
                var success = mode.Id > 0;
                if (success)
                {
                    Logger.CreateLog("添加客服响应记录", mode);
                    if (watcherId != UserInfo.UserId && mode.IsCopy)
                    {
                        //值班人不是当前登录用户的ID,复制一条响应记录给指定的值班人
                        //var entity = bll.FindSingleByNo(mode.No);

                        var watcher = new JMP.BLL.jmp_locuser().GetModel(watcherId);

                        mode.ParentId = mode.Id;
                        mode.IsCopy = false;
                        mode.WatchId = watcherId;
                        mode.EvidenceScreenshot = "";
                        mode.HandleDetails = "";
                        mode.HandlerId = watcherId;
                        mode.ResponseDate = null;
                        mode.ResponseScreenshot = "";
                        mode.HandlerName = watcher.u_realname;
                        mode.NotifyDate = DateTime.Now;
                        mode.NotifyWatcher = true;

                        success = bll.Add(mode) > 0;
                        if (success)
                        {
                            Logger.CreateLog("转交客服响应记录", mode);
                            var teltemp = ConfigurationManager.AppSettings["CHUANGLAN.AUDIO.TELNO.NEW.RESPONSE"];
                            if (SystemConfigReader.CustomerResponseAllowSendAudioMessage)
                            {
                                if (!string.IsNullOrEmpty(teltemp))
                                {
                                    //语音电话
                                    var request = new RequestPayload
                                    {
                                        callingline = watcher.u_mobilenumber,
                                        company = ConfigurationManager.AppSettings["CHUANGLAN.AUDIO.RESOURCE.company"],
                                        contextparm = "",
                                        keytime = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                                        sex = 2,
                                        telno = ConfigurationManager.AppSettings["CHUANGLAN.AUDIO.RESOURCE.telno"],
                                        teltemp = long.Parse(teltemp)
                                    };
                                    request.key = TOOL.Message.AudioMessage.ChuangLan.Util.GetKeyString(
                                        ConfigurationManager.AppSettings["CHUANGLAN.AUDIO.RESOURCE.key"],
                                        ConfigurationManager.AppSettings["CHUANGLAN.AUDIO.RESOURCE.secret"],
                                        request.keytime);
                                    try
                                    {
                                        var messageSender = new ChuangLanAudioMessageSender(request);
                                        messageSender.Send();
                                        //messageSender.Response;
                                        Logger.OperateLog("转交客服响应记录拨打语音电话",
                                            JsonConvert.SerializeObject(messageSender.Response));
                                    }
                                    catch (Exception ex)
                                    {
                                        GlobalErrorLogger.Log("转交客服响应记录拨打语音电话出错:" + ex, "转交客服响应记录", "转交客服响应记录拨打语音电话出错");
                                    }
                                }
                                else
                                {
                                    Logger.OperateLog("转交客服响应记录拨打语音电话", "语音模板未配置");
                                }
                            }
                            else
                            {
                                Logger.OperateLog("转交客服响应记录拨打语音电话", "拨打响应转发的语音消息已关闭");
                            }
                        }
                    }
                    retJson = new { success = 1, msg = "操作成功！" };
                }

            }
            else
            {
                var entity = bll.GetModel(mode.Id);
                var copy = entity.Clone();
                entity.Status = mode.Status;
                entity.AuditStatus = false;
                entity.AuditByUserId = 0;
                entity.AuditByUserName = "";
                entity.AuditDate = null;
                entity.WatchId = mode.WatchId;
                entity.CompletedDate = mode.CompletedDate;
                entity.HandleDetails = mode.HandleDetails ?? "";
                entity.AskDate = mode.AskDate;
                entity.AskScreenshot = mode.AskScreenshot ?? "";
                entity.EvidenceScreenshot = mode.EvidenceScreenshot ?? "";
                entity.MainCategory = mode.MainCategory;
                entity.ResponseDate = mode.ResponseDate;
                entity.ResponseScreenshot = mode.ResponseScreenshot ?? "";
                entity.SubCategory = mode.SubCategory;
                entity.DeveloperId = mode.DeveloperId;
                entity.DeveloperEmail = mode.DeveloperEmail ?? "";
                bll.Update(entity);
                Logger.ModifyLog("修改响应记录", copy, entity);
                retJson = new { success = 1, msg = "操作成功！" };
            }

            return Json(retJson);
        }

        /// <summary>
        /// 追加详情
        /// </summary>
        /// <returns></returns>
        public ActionResult HandleDetails()
        {
            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);
            ViewBag.id = id;

            return View();
        }

        /// <summary>
        /// 追加详情方法
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdateHandleDetails()
        {
            JMP.BLL.CsCustomerServiceRecord bll = new JMP.BLL.CsCustomerServiceRecord();
            JMP.MDL.CsCustomerServiceRecord mode = new JMP.MDL.CsCustomerServiceRecord();

            object retJson = new { success = 0, msg = "操作失败" };

            //记录ID
            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);
            //追加详情
            string HandleDetails = string.IsNullOrEmpty(Request["HandleDetails"]) ? "" : Request["HandleDetails"];
            //查询一条记录详情
            mode = bll.GetModel(id);
            mode.HandleDetails = mode.HandleDetails + ",[追加]:" + HandleDetails;

            if (bll.Update(mode))
            {
                Logger.OperateLog("客服问题处理详情追加数据", UserInfo.UserName + "追加数据ID:" + id + "的处理详情：" + HandleDetails);

                retJson = new { success = 1, msg = "操作成功！" };
            }

            return Json(retJson);
        }


        /// <summary>
        /// 更新状态
        /// </summary>
        /// <returns></returns>
        public ActionResult RespondStatus()
        {
            JMP.BLL.CsCustomerServiceRecord bll = new JMP.BLL.CsCustomerServiceRecord();
            JMP.MDL.CsCustomerServiceRecord mode = new JMP.MDL.CsCustomerServiceRecord();

            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);
            //查询一条客服响应记录详情
            mode = bll.GetModel(id);
            ViewBag.mode = mode;

            return View();
        }

        /// <summary>
        /// 更新处理状态方法
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdateRespondStatus()
        {
            JMP.BLL.CsCustomerServiceRecord bll = new JMP.BLL.CsCustomerServiceRecord();
            JMP.MDL.CsCustomerServiceRecord mode = new JMP.MDL.CsCustomerServiceRecord();

            object retJson = new { success = 0, msg = "操作失败" };
            //记录ID
            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);
            //状态
            int start = string.IsNullOrEmpty(Request["Status"]) ? 0 : int.Parse(Request["Status"]);
            //查询一条记录详情
            mode = bll.GetModel(id);
            mode.Status = start;
            if (bll.Update(mode))
            {
                Logger.OperateLog("客服问题响应记录更新状态", UserInfo.UserName + "更新数据ID:" + id + "的状态为：" + start);

                retJson = new { success = 1, msg = "操作成功！" };
            }

            return Json(retJson);
        }

        /// <summary>
        /// 主管审核
        /// </summary>
        /// <returns></returns>
        public ActionResult AuditStatus()
        {
            JMP.BLL.CsCustomerServiceRecord bll = new JMP.BLL.CsCustomerServiceRecord();
            JMP.MDL.CsCustomerServiceRecord mode = new JMP.MDL.CsCustomerServiceRecord();

            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);
            //查询一条客服响应记录详情
            mode = bll.GetModel(id);
            ViewBag.mode = mode;

            return View();
        }

        /// <summary>
        /// 主管审核方法
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdateAuditStatus()
        {
            JMP.BLL.CsCustomerServiceRecord bll = new JMP.BLL.CsCustomerServiceRecord();
            JMP.MDL.CsCustomerServiceRecord mode = new JMP.MDL.CsCustomerServiceRecord();

            object retJson = new { success = 0, msg = "操作失败" };
            //记录ID
            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);
            //状态
            int AuditStatus = string.IsNullOrEmpty(Request["auditstate"]) ? 0 : int.Parse(Request["auditstate"]);

            //处理评级
            var handelGrade = string.IsNullOrEmpty(Request["HandelGrade"]) ? 0 : int.Parse(Request["HandelGrade"]);

            //查询一条记录详情
            mode = bll.GetModel(id);

            if (mode.ResponseDate == null)
            {
                return Json(new { success = 0, msg = "响应时间未填写" });
            }

            if (AuditStatus == -1)
            {
                mode.Status = -1;
            }
            else
            {
                mode.Status = 3;
                mode.AuditStatus = AuditStatus != 0;
                if (mode.AuditStatus)
                {
                    //获取相差分钟数
                    var ts = Convert.ToDateTime(mode.ResponseDate) - Convert.ToDateTime(mode.AskDate);
                    var datatime = ts.TotalMinutes;
                    //根据响应分钟差进行评分
                    if (datatime < 5)
                    {
                        mode.Grade = 1;
                    }
                    else if (datatime >= 5 && datatime < 10)
                    {
                        mode.Grade = 2;
                    }
                    else if (datatime >= 10 && datatime <= 30)
                    {
                        mode.Grade = 3;
                    }
                    else if (datatime > 30)
                    {
                        mode.Grade = 4;
                    }
                    else
                    {
                        mode.Grade = 0;
                    }
                }
                //审核人ID
                mode.AuditByUserId = UserInfo.UserId;
                //审核人姓名
                mode.AuditByUserName = UserInfo.UserName;
                //审核时间
                mode.AuditDate = DateTime.Now;

                mode.HandelGrade = handelGrade;
            }

            if (bll.Update(mode))
            {
                Logger.OperateLog("客服问题响应记录主管审核状态", UserInfo.UserName + "审核数据ID:" + id + "的状态为：" + AuditStatus);

                retJson = new { success = 1, msg = "操作成功！" };
            }

            return Json(retJson);
        }

        /// <summary>
        /// 查看详情
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult RespondDetails()
        {
            JMP.BLL.CsCustomerServiceRecord bll = new JMP.BLL.CsCustomerServiceRecord();
            JMP.MDL.CsCustomerServiceRecord mode = new JMP.MDL.CsCustomerServiceRecord();

            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);
            //查询一条客服响应记录详情
            mode = bll.GetModel(id);

            //证据图片
            string[] EvidenceScreenshot = mode.EvidenceScreenshot.Split(',');

            ViewBag.EvidenceScreenshot = EvidenceScreenshot;
            ViewBag.mode = mode;
            ViewBag.UploadUrl = ConfigurationManager.AppSettings["imgurl"];

            return View();
        }

        #endregion

        #region 上传证据

        /// <summary>
        /// 上传图片(提问截图)
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public JsonResult UploadImgAsk()
        {
            //回传图片路径
            string returnurl = string.Empty;
            //用于回传显示地址
            string returnurl2 = string.Empty;
            string msg = string.Empty;
            string tag = "0";
            NameValueCollection nvc = System.Web.HttpContext.Current.Request.Form;
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            string[] result = { null, null };
            if (hfc.Count == 1)
            {
                if (hfc[0].ContentLength > (1024 * 1024 * 2))
                {
                    return Json(new { mess = "图片大小不能超过2M", success = "0" }, "text/html", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string purl = nvc["purl"];
                    try
                    {
                        if (!string.IsNullOrEmpty(purl))
                        {
                            PubImageUp.DeleteImage(purl);
                        }
                    }
                    catch (Exception e)
                    {
                    }
                    //获取配置文件里的上传路径
                    string uploadurl = ConfigurationManager.AppSettings["uploadurlkfz"] + "/EvidenceScreenshot/";
                    //图片域名地址
                    string imgurlroot = ConfigurationManager.AppSettings["imgurl"] + "/EvidenceScreenshot/";
                    //上传图片
                    result = PubImageUp.UpImages("AskScreenshotfile", uploadurl);
                    //图片返回展示地址
                    returnurl2 = imgurlroot + result[0];
                    if (result.Length == 2)
                    {
                        if (result[0] != "99")
                            returnurl = "/EvidenceScreenshot/" + result[0];

                        else
                            msg = "/EvidenceScreenshot/" + result[0];

                        tag = "1";
                    }
                }
            }

            return Json(
                new
                {
                    Id = nvc["tid"],
                    name = nvc["tname"],
                    imgurl = returnurl,
                    imgurlroot = returnurl2,
                    mess = msg,
                    success = tag
                }, "text/html", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 上传图片(响应截图)
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public JsonResult UploadImgResponse()
        {
            //回传图片路径
            string returnurl = string.Empty;
            //用于回传显示地址
            string returnurl2 = string.Empty;
            string msg = string.Empty;
            string tag = "0";
            NameValueCollection nvc = System.Web.HttpContext.Current.Request.Form;
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            string[] result = { null, null };
            if (hfc.Count == 1)
            {
                if (hfc[0].ContentLength > (1024 * 1024 * 2))
                {
                    return Json(new { mess = "图片大小不能超过2M", success = "0" }, "text/html", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string purl = nvc["purl"];
                    try
                    {
                        if (!string.IsNullOrEmpty(purl))
                        {
                            PubImageUp.DeleteImage(purl);
                        }
                    }
                    catch (Exception e)
                    {
                    }
                    //获取配置文件里的上传路径
                    string uploadurl = ConfigurationManager.AppSettings["uploadurlkfz"] + "/EvidenceScreenshot/";
                    //图片域名地址
                    string imgurlroot = ConfigurationManager.AppSettings["imgurl"] + "/EvidenceScreenshot/";
                    //上传图片
                    result = PubImageUp.UpImages("ResponseScreenshotfile", uploadurl);
                    //图片返回展示地址
                    returnurl2 = imgurlroot + result[0];
                    if (result.Length == 2)
                    {
                        if (result[0] != "99")
                            returnurl = "/EvidenceScreenshot/" + result[0];

                        else
                            msg = "/EvidenceScreenshot/" + result[0];

                        tag = "1";
                    }
                }
            }

            return Json(
                new
                {
                    Id = nvc["tid"],
                    name = nvc["tname"],
                    imgurl = returnurl,
                    imgurlroot = returnurl2,
                    mess = msg,
                    success = tag
                }, "text/html", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 上传图片(证据1)
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public JsonResult UploadImg()
        {
            //回传图片路径
            string returnurl = string.Empty;
            //用于回传显示地址
            string returnurl2 = string.Empty;
            string msg = string.Empty;
            string tag = "0";
            NameValueCollection nvc = System.Web.HttpContext.Current.Request.Form;
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            string[] result = { null, null };
            if (hfc.Count == 1)
            {
                if (hfc[0].ContentLength > (1024 * 1024 * 2))
                {
                    return Json(new { mess = "图片大小不能超过2M", success = "0" }, "text/html", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string purl = nvc["purl"];
                    try
                    {
                        if (!string.IsNullOrEmpty(purl))
                        {
                            PubImageUp.DeleteImage(purl);
                        }
                    }
                    catch (Exception e)
                    {
                    }
                    //获取配置文件里的上传路径
                    string uploadurl = ConfigurationManager.AppSettings["uploadurlkfz"] + "/EvidenceScreenshot/";
                    //图片域名地址
                    string imgurlroot = ConfigurationManager.AppSettings["imgurl"] + "/EvidenceScreenshot/";
                    //上传图片
                    result = PubImageUp.UpImages("certificatefile1", uploadurl);
                    //图片返回展示地址
                    returnurl2 = imgurlroot + result[0];
                    if (result.Length == 2)
                    {
                        if (result[0] != "99")
                            returnurl = "/EvidenceScreenshot/" + result[0];

                        else
                            msg = "/EvidenceScreenshot/" + result[0];

                        tag = "1";
                    }
                }
            }

            return Json(
                new
                {
                    Id = nvc["tid"],
                    name = nvc["tname"],
                    imgurl = returnurl,
                    imgurlroot = returnurl2,
                    mess = msg,
                    success = tag
                }, "text/html", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 上传图片(证据2)
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public JsonResult UploadImg2()
        {
            //回传图片路径
            string returnurl = string.Empty;
            //用于回传显示地址
            string returnurl2 = string.Empty;
            string msg = string.Empty;
            string tag = "0";
            NameValueCollection nvc = System.Web.HttpContext.Current.Request.Form;
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            string[] result = { null, null };
            if (hfc.Count == 1)
            {
                if (hfc[0].ContentLength > (1024 * 1024 * 2))
                {
                    return Json(new { mess = "图片大小不能超过2M", success = "0" }, "text/html", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string purl = nvc["purl"];
                    try
                    {
                        if (!string.IsNullOrEmpty(purl))
                        {
                            PubImageUp.DeleteImage(purl);
                        }
                    }
                    catch (Exception e)
                    {
                    }
                    //获取配置文件里的上传路径
                    string uploadurl = ConfigurationManager.AppSettings["uploadurlkfz"] + "/EvidenceScreenshot/";
                    //图片域名地址
                    string imgurlroot = ConfigurationManager.AppSettings["imgurl"] + "/EvidenceScreenshot/";
                    //上传图片
                    result = PubImageUp.UpImages("certificatefile2", uploadurl);
                    //图片返回展示地址
                    returnurl2 = imgurlroot + result[0];
                    if (result.Length == 2)
                    {
                        if (result[0] != "99")
                            returnurl = "/EvidenceScreenshot/" + result[0];

                        else
                            msg = "/EvidenceScreenshot/" + result[0];

                        tag = "1";
                    }
                }
            }

            return Json(
                new
                {
                    Id = nvc["tid"],
                    name = nvc["tname"],
                    imgurl = returnurl,
                    imgurlroot = returnurl2,
                    mess = msg,
                    success = tag
                }, "text/html", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 上传图片(证据3)
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public JsonResult UploadImg3()
        {
            //回传图片路径
            string returnurl = string.Empty;
            //用于回传显示地址
            string returnurl2 = string.Empty;
            string msg = string.Empty;
            string tag = "0";
            NameValueCollection nvc = System.Web.HttpContext.Current.Request.Form;
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            string[] result = { null, null };
            if (hfc.Count == 1)
            {
                if (hfc[0].ContentLength > (1024 * 1024 * 2))
                {
                    return Json(new { mess = "图片大小不能超过2M", success = "0" }, "text/html", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string purl = nvc["purl"];
                    try
                    {
                        if (!string.IsNullOrEmpty(purl))
                        {
                            PubImageUp.DeleteImage(purl);
                        }
                    }
                    catch (Exception e)
                    {
                    }
                    //获取配置文件里的上传路径
                    string uploadurl = ConfigurationManager.AppSettings["uploadurlkfz"] + "/EvidenceScreenshot/";
                    //图片域名地址
                    string imgurlroot = ConfigurationManager.AppSettings["imgurl"] + "/EvidenceScreenshot/";
                    //上传图片
                    result = PubImageUp.UpImages("certificatefile3", uploadurl);
                    //图片返回展示地址
                    returnurl2 = imgurlroot + result[0];
                    if (result.Length == 2)
                    {
                        if (result[0] != "99")
                            returnurl = "/EvidenceScreenshot/" + result[0];

                        else
                            msg = "/EvidenceScreenshot/" + result[0];

                        tag = "1";
                    }
                }
            }

            return Json(
                new
                {
                    Id = nvc["tid"],
                    name = nvc["tname"],
                    imgurl = returnurl,
                    imgurlroot = returnurl2,
                    mess = msg,
                    success = tag
                }, "text/html", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 上传图片(证据4)
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public JsonResult UploadImg4()
        {
            //回传图片路径
            string returnurl = string.Empty;
            //用于回传显示地址
            string returnurl2 = string.Empty;
            string msg = string.Empty;
            string tag = "0";
            NameValueCollection nvc = System.Web.HttpContext.Current.Request.Form;
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            string[] result = { null, null };
            if (hfc.Count == 1)
            {
                if (hfc[0].ContentLength > (1024 * 1024 * 2))
                {
                    return Json(new { mess = "图片大小不能超过2M", success = "0" }, "text/html", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string purl = nvc["purl"];
                    try
                    {
                        if (!string.IsNullOrEmpty(purl))
                        {
                            PubImageUp.DeleteImage(purl);
                        }
                    }
                    catch (Exception e)
                    {
                    }
                    //获取配置文件里的上传路径
                    string uploadurl = ConfigurationManager.AppSettings["uploadurlkfz"] + "/EvidenceScreenshot/";
                    //图片域名地址
                    string imgurlroot = ConfigurationManager.AppSettings["imgurl"] + "/EvidenceScreenshot/";
                    //上传图片
                    result = PubImageUp.UpImages("certificatefile4", uploadurl);
                    //图片返回展示地址
                    returnurl2 = imgurlroot + result[0];
                    if (result.Length == 2)
                    {
                        if (result[0] != "99")
                            returnurl = "/EvidenceScreenshot/" + result[0];

                        else
                            msg = "/EvidenceScreenshot/" + result[0];

                        tag = "1";
                    }
                }
            }

            return Json(
                new
                {
                    Id = nvc["tid"],
                    name = nvc["tname"],
                    imgurl = returnurl,
                    imgurlroot = returnurl2,
                    mess = msg,
                    success = tag
                }, "text/html", JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region  客服对接响应统计
        /// <summary>
        /// 客服对接响应统计
        /// </summary>
        /// <returns></returns>
        public ActionResult ResponseReport()
        {
            List<JMP.MDL.CsCustomerServiceRecordReprot> list = new List<JMP.MDL.CsCustomerServiceRecordReprot>();
            JMP.BLL.CsCustomerServiceRecord bll = new JMP.BLL.CsCustomerServiceRecord();
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchType = string.IsNullOrEmpty(Request["searchType"]) ? 0 : int.Parse(Request["searchType"].ToString());
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"].ToString();
            int MainCategory = string.IsNullOrEmpty(Request["MainCategory"]) ? -1 : int.Parse(Request["MainCategory"]);
            int SubCategory = string.IsNullOrEmpty(Request["SubCategory"]) ? -1 : int.Parse(Request["SubCategory"]);
            string stime = string.IsNullOrEmpty(Request["stime"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["stime"];//开始时间
            string etime = string.IsNullOrEmpty(Request["etime"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["etime"];//结束时间
            int Type = string.IsNullOrEmpty(Request["Type"]) ? 0 : int.Parse(Request["Type"].ToString());
            int YearDate = string.IsNullOrEmpty(Request["YearDate"]) ? 0 : int.Parse(Request["YearDate"].ToString());
            int MonthDate = string.IsNullOrEmpty(Request["MonthDate"]) ? 0 : int.Parse(Request["MonthDate"].ToString());
            string sql = "";
            string Order = "";
            if (Type == 0)//日
            {
                sql += "select convert(nvarchar(10),CreatedOn,120) as CreatedOn ,MainCategory,SubCategory,HandlerId,HandlerName, avg (DATEDIFF(n, AskDate,ResponseDate)) as AvgRepsonse ,max(DATEDIFF(n,AskDate,ResponseDate)) as MaxRepsonse , min(DATEDIFF(n, AskDate, ResponseDate)) as MinRepsonse from [dbo].[CsCustomerServiceRecord] where 1=1 and AuditStatus=1";
                if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
                {
                    sql += " and convert(varchar(10),CreatedOn,120)>='" + stime + "' and convert(varchar(10),CreatedOn,120)<='" + etime + "' ";
                }
                if (searchType > 0 && !string.IsNullOrEmpty(sea_name))
                {
                    switch (searchType)
                    {
                        case 1:
                            sql += " and HandlerName='" + sea_name + "' ";
                            break;
                    }
                }
                if (MainCategory >= 0)
                {
                    sql += " and MainCategory='" + MainCategory + "' ";
                }
                if (SubCategory >= 0)
                {
                    sql += " and SubCategory='" + SubCategory + "' ";
                }

                sql += "group by convert(nvarchar(10),CreatedOn,120) ,MainCategory,SubCategory,HandlerId,HandlerName";
                Order = " order by convert(nvarchar(10),CreatedOn,120) desc ";
            }
            else//年
            {
                sql += " select MONTH(CreatedOn) as cmonth ,YEAR(CreatedOn)as yyear, MainCategory,SubCategory,HandlerId,HandlerName, avg (DATEDIFF(n, AskDate,ResponseDate)) as AvgRepsonse ,max(DATEDIFF(n,AskDate,ResponseDate)) as MaxRepsonse , min(DATEDIFF(n, AskDate, ResponseDate)) as MinRepsonse from[dbo].[CsCustomerServiceRecord] where 1=1 and AuditStatus=1";
                if (YearDate > 0)
                {
                    sql += " and  YEAR(CreatedOn)='" + YearDate + "'";
                }
                if (MonthDate > 0)
                {
                    sql += " and  Month(CreatedOn)='" + MonthDate + "'";
                }
                if (searchType > 0 && !string.IsNullOrEmpty(sea_name))
                {
                    switch (searchType)
                    {
                        case 1:
                            sql += " and HandlerName='" + sea_name + "' ";
                            break;
                    }
                }
                if (MainCategory >= 0)
                {
                    sql += " and MainCategory='" + MainCategory + "' ";
                }
                if (SubCategory >= 0)
                {
                    sql += " and SubCategory='" + SubCategory + "' ";
                }
                sql += " group by MONTH(CreatedOn)  , YEAR(CreatedOn),MainCategory,SubCategory,HandlerId,HandlerName";
                Order = " order by yyear desc ";
            }

            //查询所有响应记录
            list = bll.CsCustomerRecordReprotList(Order, sql, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.begin = stime;
            ViewBag.end = etime;
            ViewBag.MainCategory = MainCategory;
            ViewBag.SubCategory = SubCategory;
            ViewBag.searchType = searchType;
            ViewBag.sea_name = sea_name;
            ViewBag.YearDate = YearDate;
            ViewBag.MonthDate = MonthDate;
            ViewBag.Type = Type;
            ViewBag.list = list;


            return View();
        }

        /// <summary>
        /// 响应评分统计
        /// </summary>
        /// <returns></returns>
        public ActionResult SroceReport()
        {
            List<JMP.MDL.CsCustomerServiceSroceReprot> list = new List<JMP.MDL.CsCustomerServiceSroceReprot>();
            JMP.BLL.CsCustomerServiceRecord bll = new JMP.BLL.CsCustomerServiceRecord();
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchType = string.IsNullOrEmpty(Request["searchType"]) ? 0 : int.Parse(Request["searchType"].ToString());
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"].ToString();
            string stime = string.IsNullOrEmpty(Request["stime"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["stime"];//开始时间
            string etime = string.IsNullOrEmpty(Request["etime"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["etime"];//结束时间
            int Type = string.IsNullOrEmpty(Request["Type"]) ? 0 : int.Parse(Request["Type"].ToString());
            int YearDate = string.IsNullOrEmpty(Request["YearDate"]) ? 0 : int.Parse(Request["YearDate"].ToString());
            int MonthDate = string.IsNullOrEmpty(Request["MonthDate"]) ? 0 : int.Parse(Request["MonthDate"].ToString());
            string sql = "";
            string Order = "";
            if (Type == 0)//日
            {
                sql += "select convert(nvarchar(10),CreatedOn,120) as CreatedOn ,HandlerId,HandlerName ,Grade,COUNT(Grade)as Count from [dbo].[CsCustomerServiceRecord] where 1=1 and AuditStatus=1";
                if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
                {
                    sql += " and convert(varchar(10),CreatedOn,120)>='" + stime + "' and convert(varchar(10),CreatedOn,120)<='" + etime + "' ";
                }
                if (searchType > 0 && !string.IsNullOrEmpty(sea_name))
                {
                    switch (searchType)
                    {
                        case 1:
                            sql += " and HandlerName='" + sea_name + "' ";
                            break;
                    }
                }


                sql += "group by  convert(nvarchar(10),CreatedOn,120),HandlerId,HandlerName ,Grade";
                Order = " order by convert(nvarchar(10),CreatedOn,120) desc ";
            }
            else//年
            {
                sql += " select MONTH(CreatedOn) as cmonth ,YEAR(CreatedOn)as yyear, HandlerId,HandlerName ,Grade,COUNT(Grade)as Count from[dbo].[CsCustomerServiceRecord] where 1=1 and AuditStatus=1";
                if (YearDate > 0)
                {
                    sql += " and  YEAR(CreatedOn)='" + YearDate + "'";
                }
                if (MonthDate > 0)
                {
                    sql += " and  Month(CreatedOn)='" + MonthDate + "'";
                }
                if (searchType > 0 && !string.IsNullOrEmpty(sea_name))
                {
                    switch (searchType)
                    {
                        case 1:
                            sql += " and HandlerName='" + sea_name + "' ";
                            break;
                    }
                }
                sql += " group by MONTH(CreatedOn)  , YEAR(CreatedOn),HandlerId,HandlerName ,Grade";
                Order = " order by yyear desc ";
            }
            //查询所有响应记录
            list = bll.CsCustomerServiceReprotList(Order, sql, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.begin = stime;
            ViewBag.end = etime;
            ViewBag.searchType = searchType;
            ViewBag.sea_name = sea_name;
            ViewBag.YearDate = YearDate;
            ViewBag.MonthDate = MonthDate;
            ViewBag.Type = Type;
            ViewBag.list = list;


            return View();
        }
        /// <summary>
        /// 处理评分统计
        /// </summary>
        /// <returns></returns>
        public ActionResult HandelGradeReport()
        {
            List<JMP.MDL.CsCustomerServiceSroceReprot> list = new List<JMP.MDL.CsCustomerServiceSroceReprot>();
            JMP.BLL.CsCustomerServiceRecord bll = new JMP.BLL.CsCustomerServiceRecord();
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchType = string.IsNullOrEmpty(Request["searchType"]) ? 0 : int.Parse(Request["searchType"].ToString());
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"].ToString();
            string stime = string.IsNullOrEmpty(Request["stime"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["stime"];//开始时间
            string etime = string.IsNullOrEmpty(Request["etime"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["etime"];//结束时间
            int Type = string.IsNullOrEmpty(Request["Type"]) ? 0 : int.Parse(Request["Type"].ToString());
            int YearDate = string.IsNullOrEmpty(Request["YearDate"]) ? 0 : int.Parse(Request["YearDate"].ToString());
            int MonthDate = string.IsNullOrEmpty(Request["MonthDate"]) ? 0 : int.Parse(Request["MonthDate"].ToString());
            string sql = "";
            string Order = "";
            if (Type == 0)//日
            {
                sql += "select convert(nvarchar(10),CreatedOn,120) as CreatedOn ,HandlerId,HandlerName ,HandelGrade as Grade,COUNT(HandelGrade)as Count from [dbo].[CsCustomerServiceRecord] where 1=1 and AuditStatus=1";
                if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
                {
                    sql += " and convert(varchar(10),CreatedOn,120)>='" + stime + "' and convert(varchar(10),CreatedOn,120)<='" + etime + "' ";
                }
                if (searchType > 0 && !string.IsNullOrEmpty(sea_name))
                {
                    switch (searchType)
                    {
                        case 1:
                            sql += " and HandlerName='" + sea_name + "' ";
                            break;
                    }
                }


                sql += "group by  convert(nvarchar(10),CreatedOn,120),HandlerId,HandlerName ,HandelGrade";
                Order = " order by convert(nvarchar(10),CreatedOn,120) desc ";
            }
            else//年
            {
                sql += " select MONTH(CreatedOn) as cmonth ,YEAR(CreatedOn)as yyear, HandlerId,HandlerName ,HandelGrade as Grade,COUNT(HandelGrade)as Count from[dbo].[CsCustomerServiceRecord] where 1=1 and AuditStatus=1";
                if (YearDate > 0)
                {
                    sql += " and  YEAR(CreatedOn)='" + YearDate + "'";
                }
                if (MonthDate > 0)
                {
                    sql += " and  Month(CreatedOn)='" + MonthDate + "'";
                }
                if (searchType > 0 && !string.IsNullOrEmpty(sea_name))
                {
                    switch (searchType)
                    {
                        case 1:
                            sql += " and HandlerName='" + sea_name + "' ";
                            break;
                    }
                }
                sql += " group by MONTH(CreatedOn)  , YEAR(CreatedOn),HandlerId,HandlerName ,HandelGrade";
                Order = " order by yyear desc ";
            }
            //查询所有响应记录
            list = bll.CsCustomerServiceReprotList(Order, sql, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.begin = stime;
            ViewBag.end = etime;
            ViewBag.searchType = searchType;
            ViewBag.sea_name = sea_name;
            ViewBag.YearDate = YearDate;
            ViewBag.MonthDate = MonthDate;
            ViewBag.Type = Type;
            ViewBag.list = list;


            return View();
        }



        #endregion

        #region 批量排班
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        [HttpGet]
        public ActionResult CreateSchedule()
        {
            JMP.BLL.jmp_locuser bll = new JMP.BLL.jmp_locuser();
            var list = MdlList.ToList<JMP.MDL.jmp_locuser>(bll.GetList(" u_department='" + UserInfo.UserDept + "' ").Tables[0]);
            var model = new CreateScheduleViewModel
            {
                ScheduleMonth = DateTime.Now.ToString("yyyy-MM"),
                CreateScheduleUserViewModel = list.Select(x => new CreateScheduleUserViewModel
                {
                    Id = x.u_id,
                    RealName = x.u_realname
                }).ToList()
            };

            return View(model);
        }

        [LoginCheckFilter(IsCheck = true, IsRole = false)]
        [HttpPost]
        public JsonResult BatchCreateSchedule(CreateScheduleViewModel model)
        {
            if (string.IsNullOrEmpty(model.SelectedUserIds))
            {
                return Json(new { success = 0, msg = "请选择值班人员" });
            }
            var schedulingBll = new JMP.BLL.jmp_scheduling();
            var month = DateTime.Parse(model.ScheduleMonth);
            var firstDayOfMonth = new DateTime(month.Year, month.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var monthMaxDay = lastDayOfMonth.Day;

            var userIdList = model.SelectedUserIds.Split(',').Select(x => Convert.ToInt32(x)).OrderBy(x => Guid.NewGuid()).ToList();
            var currentIndex = 0;
            for (var i = 1; i <= monthMaxDay; i++)
            {
                var day = model.ScheduleMonth + "-" + ((i < 10) ? "0" + i : i.ToString());
                //拼装值班开始结束时间
                var watchstartdate = day + " " + ConfigurationManager.AppSettings["startTime"];
                var watchenddate = day + " " + ConfigurationManager.AppSettings["endTime"];

                var exists = schedulingBll.ScheduleExists(watchstartdate, UserInfo.UserDept.ConvertDeptToInt());
                if (exists)
                {
                    continue;
                }
                var sch = new JMP.MDL.jmp_scheduling
                {
                    createdby = UserInfo.UserName,
                    createdbyid = UserInfo.UserId,
                    createdon = DateTime.Now,
                    Type = UserInfo.UserDept.ConvertDeptToInt(),
                    watchstartdate = DateTime.Parse(watchstartdate),
                    watchenddate = DateTime.Parse(watchenddate),
                    watchid = userIdList[currentIndex]
                };
                schedulingBll.Add(sch);

                currentIndex++;
                if (currentIndex > userIdList.Count - 1)
                {
                    currentIndex = 0;
                }
            }
            Logger.OperateLog("批量排班", "批量排班成功");
            return Json(new { success = 1, msg = "批量排班成功" });
        }

        #endregion

    }
}

