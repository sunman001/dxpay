using System;
using System.Collections.Generic;
using System.Web.Mvc;
using JMP.BLL;
using JMP.TOOL;
using TOOL;
using WEB.Util.Logger;
using TOOL.Extensions;

namespace WEB.Controllers
{
    public class MonitorChannelController : Controller
    {
        readonly jmp_limit _bllLimit = new jmp_limit();
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;
        [HttpGet]
        public ActionResult Create()
        {
            return View(new JMP.Model.MonitorChannel());
        }

        public ActionResult Edit()
        {
            var cId = string.IsNullOrEmpty(Request["c_id"]) ? 0 : int.Parse(Request["c_id"]);
            var bll = new MonitorChannel();
            var model = new JMP.Model.MonitorChannel();
            if (cId > 0)
            {
                model = bll.SelectId(cId);
            }
            model.Threshold = model.Threshold * 100;
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
            model.a_name=new jmp_interface().GetModel(model.ChannelId).l_corporatename;
            return View("Create",model);
        }

        /// <summary>
        ///通道监控管理
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = true, IsRole = false), VisitRecord(IsRecord = true)]
        public ActionResult List()
        {
            int pageCount;
            var pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : int.Parse(Request["pageIndexs"]);//当前页
            var pageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : int.Parse(Request["PageSize"]);//每页显示数量
            var searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : int.Parse(Request["searchDesc"]);//排序方式
            var type = string.IsNullOrEmpty(Request["type"]) ? 0 : int.Parse(Request["type"]);//查询条件选择
            var selectState = string.IsNullOrEmpty(Request["SelectState"]) ? "1" : Request["SelectState"];//状态
            var aType = string.IsNullOrEmpty(Request["a_type"]) ? "-1" : Request["a_type"];//状态
            var seaName = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            var bll = new MonitorChannel();
            var list = bll.SelectList(selectState, seaName, type, searchDesc, Convert.ToInt32(aType), pageIndexs, pageSize, out pageCount);
            ViewBag.searchDesc = searchDesc;
            ViewBag.type = type;
            ViewBag.sea_name = seaName;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = pageSize;
            ViewBag.pageCount = pageCount;
           
            ViewBag.SelectState = selectState;
            ViewBag.a_type = aType;
            ViewBag.locUrl = GetVoidHtmlApp();
            return View(list);
        }

        public string GetVoidHtmlApp()
        {
            var locUrl = "";
            var uId = UserInfo.UserId.ToString();
            var rId = UserInfo.UserRoleId;
            var getlocuserAdd = _bllLimit.GetLocUserLimitVoids("/monitorchannel/create", uId, rId);//添加管理员
            if (getlocuserAdd)
            {
                locUrl += "<li onclick=\"CreateMonitorChannel()\"><i class='fa fa-plus'></i>添加通道监控</li>";
            }
            locUrl += "<li onclick=\"UpdateState(1)\"><i class='fa fa-check-square-o'></i>一键解冻</li>";
            locUrl += "<li onclick=\"UpdateState(0)\"><i class='fa fa-check-square-o'></i>一键冻结</li>";

            return locUrl;
        }

        /// <summary>
        /// 添加或修改通道监控管理
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = true, IsRole = false)]
        public JsonResult Create(JMP.Model.MonitorChannel model)
        {
            object retJson = new { success = 0, msg = "操作失败" };
            var bll = new MonitorChannel();
            var xgzfc = "";


            if (model.a_id > 0)
            {
                // 修改通道监控
                var modComplaint = bll.GetModel(model.a_id);
                var modComplaintClone = modComplaint.Clone();
                modComplaint.ChannelId = model.ChannelId;
                modComplaint.a_type = model.a_type;
           
                // model.a_datetime = modComplaint.a_datetime;
                // model.a_state = modComplaint.a_state;
                // model.ChannelId = modComplaint.ChannelId;
                modComplaint.Threshold = model.Threshold / 100;
                if (model.StartDay != -1 && model.EndDay != -1 && model.DayMinute != 0)
                {
                    modComplaint.a_time_range += model.StartDay + "-" + model.EndDay + ":" + model.DayMinute + "_";
                }
                if (model.StartNight != -1 && model.EndNight != -1 && model.NightMinute != 0)
                {
                    modComplaint.a_time_range += model.StartNight + "-" + model.EndNight + ":" + model.NightMinute;
                }
                if (model.OtherMinte != 0)
                {
                    modComplaint.a_time_range += "_100:" + model.OtherMinte;
                }
                var exitmod = bll.GetModelByTD(model.ChannelId, model.a_type);
                if (exitmod != null && model.a_id!=exitmod.a_id)
                {
                    retJson = new { success = 0, msg = "此通道监控已存在" };
                    return Json(retJson);
                }
                var monitorList = modComplaint.a_time_range.ParseAppMonitorTimeRangeTo24Hours();
                AddMonitorMinuteDetails(modComplaint.ChannelId, modComplaint.a_type, monitorList);
                if (bll.Update(modComplaint))
                {
                    Logger.ModifyLog("修改通道监控信息", modComplaintClone, model);
                   
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
                model.Threshold = model.Threshold / 100;
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
                        retJson = new { success = 0, msg = "此通道监控已存在" };
                        continue;
                    }
                    model.ChannelId = appId;
                    var cg = bll.Add(model);
                    if (cg > 0)
                    {
                        AddMonitorMinuteDetails(model.ChannelId, model.a_type, monitorList);
                        Logger.CreateLog("添加通道监控信息", model);
                       
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
        /// <param name="channelId">通道ID</param>
        /// <param name="monitorType">监控类型</param>
        /// <param name="monitorList">24小时的监控分钟数集合</param>
        private void AddMonitorMinuteDetails(int channelId, int monitorType, IEnumerable<AppMonitorTimeRange> monitorList)
        {
            //从监控分钟设置详情表删除指定通道和指定监控类型的所有小时的监控分钟数
            var monitorMinuteBll = new JmpMonitorMinuteDetails();
            monitorMinuteBll.DeleteByMonitorType(channelId, monitorType);
            foreach (var h in monitorList)
            {
                var monitorMinuteDetails = new JMP.Model.JmpMonitorMinuteDetails
                {
                    AppId = channelId,
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
        /// 结算设置一键启用或禁用
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdateState()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            var state = string.IsNullOrEmpty(Request["state"]) ? 0 : int.Parse(Request["state"]);
            var str = Request["ids"];
            string xgzfc;//组装说明
            string tsmsg;//提示
            var bll = new MonitorChannel();
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
                Logger.OperateLog("通道监控一键启用或禁用", xgzfc);
            
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

    }
}
