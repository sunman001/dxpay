using JMP.TOOL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB.ViewModel.Monitoringconfig;
using WEB.Util.Logger;
using TOOL.Extensions;
using System.Data;
using JMP.MDL;
using System.Text;

namespace WEB.Controllers
{
    public class RiskController : Controller
    {
        JMP.BLL.jmp_limit bll_limit = new JMP.BLL.jmp_limit();
        JMP.BLL.jmp_channel_filter_config bll = new JMP.BLL.jmp_channel_filter_config();
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;
        //
        // GET: /Risk/
        /// <summary>
        /// 监控配置列表
        /// </summary>
        /// <returns></returns>
        public ActionResult MonitoringconfigList()
        {
            List<JMP.MDL.jmp_channel_filter_config> list = new List<JMP.MDL.jmp_channel_filter_config>();
            List<JMP.MDL.jmp_channel_filter_config> listall = new List<JMP.MDL.jmp_channel_filter_config>();
            JMP.BLL.jmp_channel_filter_config bll = new JMP.BLL.jmp_channel_filter_config();
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int TypeId = string.IsNullOrEmpty(Request["TypeId"]) ? -1 : Int32.Parse(Request["TypeId"]);
            int TargetId = string.IsNullOrEmpty(Request["TargetId"]) ? -1 : Int32.Parse(Request["TargetId"]);
            int type = string.IsNullOrEmpty(Request["searchType"]) ? 0 : Int32.Parse(Request["searchType"]);//查询条件选择
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            list = bll.SelectList(type, sea_name, TypeId, TargetId, pageIndexs, PageSize, out pageCount);
            JMP.BLL.jmp_channel_filter_config yybll = new JMP.BLL.jmp_channel_filter_config();
            DataTable yydt = yybll.GetList("").Tables[0];//获取应用类型在用信息
            List<JMP.MDL.jmp_channel_filter_config> yylist = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_channel_filter_config>(yydt);
            listall = yylist;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            ViewBag.listall = listall;
            ViewBag.TypeId = TypeId;
            ViewBag.TargetId = TargetId;
            ViewBag.sea_name = sea_name;
            ViewBag.type = type;
            string locUrl = "";
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/Risk/AddMonitoringconfig", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加
            if (getlocuserAdd)
            {
                locUrl += "<li onclick=\"AddSdl()\"><i class='fa fa-plus'></i>添加监控配置</li>";

            }

            ViewBag.locUrl = locUrl;

            return View();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult AddMonitoringconfig()
        {
            int TypeId = string.IsNullOrEmpty(Request["TypeId"]) ? -1 : int.Parse(Request["TypeId"]);
            int TargetId = string.IsNullOrEmpty(Request["TargetId"]) ? -1 : int.Parse(Request["TargetId"]);
            int RelatedId = string.IsNullOrEmpty(Request["RelatedId"]) ? -1 : int.Parse(Request["RelatedId"]);
            var list = new List<JMP.MDL.jmp_channel_filter_config>();
            var model = new MonitoringconfigViewModel();
            if (TypeId >= 0 && TargetId >= 0 && RelatedId >= 0)
            {
                JMP.BLL.jmp_channel_filter_config yybll = new JMP.BLL.jmp_channel_filter_config();
                string where = " TypeId=" + TypeId + " and  TargetId=" + TargetId + " and RelatedId=" + RelatedId + "";
                DataTable yydt = yybll.GetList(where).Tables[0];//获取应用类型在用信息
                List<JMP.MDL.jmp_channel_filter_config> yylist = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_channel_filter_config>(yydt);
                list = yylist;

                model.RelatedId = list[0].RelatedId;
                model.IntervalOfRecover = list[0].IntervalOfRecover;
                model.TypeId = list[0].TypeId;
                model.TargetId = list[0].TargetId;
                model.Id = list[0].Id;
            }
            for (int i = 0; i < 24; i++)
            {
                var hourConfig = new WhichHourList
                {
                    Threshold = 0,
                    WhichHour = i,

                };
                var filter = list.FirstOrDefault(x => x.WhichHour == i);
                if (filter != null)
                {
                    hourConfig.Threshold = filter.Threshold;

                }
                model.WhichHourLists.Add(hourConfig);
            }
            ViewBag.mo = model;
            return View();
        }

        /// <summary>
        /// 添加/修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddorEidt(MonitoringconfigViewModel model)
        {

            object retJson = new { success = 0, msg = "操作失败" };
            if (model.Id > 0)//如果是修改 删除已存在的数据
            {
                var boo = bll.DeleteByType(model.TypeId, model.TargetId, model.RelatedId);
            }
            else//如果是修改判断是否已存在
            {
                var isextist = bll.Exists(model.TypeId, model.TargetId, model.RelatedId);
                if (isextist)
                {
                    retJson = new { success = 1, msg = "此数据已存在" };
                    return Json(retJson);
                }
            }
            foreach (var op in model.WhichHourLists)
            {
                JMP.MDL.jmp_channel_filter_config mo = new JMP.MDL.jmp_channel_filter_config();
                if (op.Threshold == 0)
                {
                    continue;
                }
                else
                {
                    mo.TypeId = model.TypeId;
                    mo.RelatedId = model.RelatedId;
                    mo.TargetId = model.TargetId;
                    mo.WhichHour = op.WhichHour;
                    mo.IntervalOfRecover = model.IntervalOfRecover;
                    mo.Threshold = op.Threshold;
                    mo.CreatedByUserId = UserInfo.UserId;
                    mo.CreatedByUserName = UserInfo.UserName;
                    mo.CreatedOn = DateTime.Now;
                    int cg = bll.Add(mo);
                    if (cg > 0)
                    {
                        Logger.CreateLog("操作监控配置", mo);

                        retJson = new { success = 1, msg = "操作成功" };
                    }
                    else
                    {
                        retJson = new { success = 0, msg = "操作失败" };
                    }



                }
            }
            return Json(retJson);
        }


        #region 通道池管理

        //通道池
        JMP.MDL.jmp_channel_pool poolModel = new JMP.MDL.jmp_channel_pool();
        JMP.BLL.jmp_channel_pool poolBll = new JMP.BLL.jmp_channel_pool();
        List<JMP.MDL.jmp_channel_pool> poolList = new List<JMP.MDL.jmp_channel_pool>();
        //通道池配置应用
        JMP.BLL.jmp_channel_app_mapping appmapingBll = new JMP.BLL.jmp_channel_app_mapping();
        List<JMP.MDL.jmp_channel_app_mapping> appmapingList = new List<JMP.MDL.jmp_channel_app_mapping>();
        JMP.MDL.jmp_channel_app_mapping appmapingModel = new JMP.MDL.jmp_channel_app_mapping();
        //通道池配置通道数量
        JMP.BLL.jmp_channel_amount_config amountBll = new JMP.BLL.jmp_channel_amount_config();
        JMP.MDL.jmp_channel_amount_config amountModel = new JMP.MDL.jmp_channel_amount_config();
        List<JMP.MDL.jmp_channel_amount_config> amountList = new List<JMP.MDL.jmp_channel_amount_config>();

        /// <summary>
        /// 通道池管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ChannelPoolList()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量

            int PoolName = string.IsNullOrEmpty(Request["PoolName"]) ? 0 : Int32.Parse(Request["PoolName"]);//查询类型
            string searchKey = string.IsNullOrEmpty(Request["searchKey"]) ? "" : Request["searchKey"];//关键字
            int IsEnabled = string.IsNullOrEmpty(Request["IsEnabled"]) ? 1 : Int32.Parse(Request["IsEnabled"]);//状态

            //查询所有
            poolList = poolBll.poolList(PoolName, searchKey, IsEnabled, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = poolList;
            ViewBag.PoolName = PoolName;
            ViewBag.searchKey = searchKey;
            ViewBag.IsEnabled = IsEnabled;

            //查询应用名称
            StringBuilder sqlinfo = new StringBuilder();
            sqlinfo.AppendFormat(@"select ChannelId,a_name from jmp_channel_app_mapping a left join jmp_app b on a.AppId=b.a_id");

            DataTable dt = new DataTable();
            dt = appmapingBll.SelectList(sqlinfo.ToString());
            appmapingList = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_channel_app_mapping>(dt);
            ViewBag.appmapinglist = appmapingList;

            //查询通道池下所有通道
            StringBuilder sqlinfo2 = new StringBuilder();
            sqlinfo2.AppendFormat(@"select id,l_corporatename from jmp_channel_pool a left join jmp_interface b on a.Id=b.l_apptypeid and b.l_risk=2 and b.l_isenable=1");
            DataTable dt2 = new DataTable();
            dt2 = appmapingBll.SelectList(sqlinfo2.ToString());
            poolList = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_channel_pool>(dt2);
            ViewBag.PoolInterface = poolList;

            //权限
            string locUrl = "";
            bool getpoolAdd = bll_limit.GetLocUserLimitVoids("/Risk/ChannelPoolAdd", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加
            if (getpoolAdd)
            {
                locUrl += "<li onclick=\"CPoolAdd()\"><i class='fa fa-plus'></i>添加通道池</li>";
            }


            ViewBag.locUrl = locUrl;

            return View();
        }

        /// <summary>
        /// 添加通道池管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ChannelPoolAdd()
        {
            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);

            if (id > 0)
            {
                poolModel = poolBll.GetModel(id);
            }

            ViewBag.poolModel = poolModel;

            return View();
        }

        /// <summary>
        /// 添加通道池方法
        /// </summary>
        /// <returns></returns>
        public JsonResult PooLAdd(JMP.MDL.jmp_channel_pool mode)
        {
            object retJson = new { success = 0, msg = "操作失败" };

            if (mode.Id > 0)
            {
                poolModel = poolBll.GetModel(mode.Id);
                //拷贝
                var moPool = poolModel.Clone();
                poolModel.PoolName = mode.PoolName;
                poolModel.Description = mode.Description;

                if (poolBll.Update(poolModel))
                {
                    Logger.ModifyLog("修改通道池", moPool, poolModel);
                    retJson = new { success = 1, msg = "操作成功" };
                }

            }
            else
            {
                mode.CreatedByUserId = UserInfo.UserId;
                mode.CreatedOn = DateTime.Now;
                mode.IsEnabled = true;

                int num = poolBll.Add(mode);
                if (num > 0)
                {
                    Logger.CreateLog("添加通道池", mode);

                    retJson = new { success = 1, msg = "操作成功" };
                }

            }
            return Json(retJson);
        }

        /// <summary>
        /// 一键启用与禁用
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult PoolStart()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            int state = string.IsNullOrEmpty(Request["state"]) ? 0 : Int32.Parse(Request["state"].ToString());
            int id = string.IsNullOrEmpty(Request["ids"]) ? 0 : Int32.Parse(Request["ids"].ToString());

            string xgzfc = "";//组装说明
            string tsmsg = "";//提示

            if (state == 1)
            {
                //查询应用名称
                StringBuilder sqlinfo = new StringBuilder();
                sqlinfo.AppendFormat(@"
select a.ChannelId ,a.AppId,a.PoolName from (select a.AppId,a.ChannelId,b.IsEnabled,b.PoolName from jmp_channel_app_mapping a left join jmp_channel_pool b on a.ChannelId=b.Id) as a 
left join jmp_channel_app_mapping b
on a.AppId=b.AppId where b.ChannelId='" + id + "' and a.ChannelId!='" + id + "' and a.IsEnabled=1");

                DataTable dt = appmapingBll.SelectList(sqlinfo.ToString());
                appmapingList = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_channel_app_mapping>(dt);


                if (appmapingList.Count > 0)
                {
                    tsmsg = "启用失败，";

                    foreach (var item in appmapingList)
                    {
                        tsmsg += item.PoolName + "已包含了，应用ID为：" + item.AppId + "的产品。";
                    }

                    retJson = new { success = 0, msg = tsmsg };
                }
                else
                {
                    #region 启用禁用

                    if (poolBll.UpdatePoolState(id, state))
                    {
                        if (state == 1)
                        {
                            xgzfc = "一键启用ID为：" + id;
                            tsmsg = "启用成功";
                        }
                        else
                        {
                            tsmsg = "禁用成功";
                            xgzfc = "一键禁用ID为：" + id;
                        }

                        Logger.OperateLog("一键启用或禁用支付类型状态", xgzfc);
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

                    #endregion
                }

            }
            else
            {
                #region 启用禁用

                if (poolBll.UpdatePoolState(id, state))
                {
                    if (state == 1)
                    {
                        xgzfc = "一键启用ID为：" + id;
                        tsmsg = "启用成功";
                    }
                    else
                    {
                        tsmsg = "禁用成功";
                        xgzfc = "一键禁用ID为：" + id;
                    }

                    Logger.OperateLog("一键启用或禁用支付类型状态", xgzfc);
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

                #endregion
            }



            return Json(retJson);
        }

        /// <summary>
        /// 通道池应用管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ChannelAppMappingAdd()
        {
            //应用池ID
            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);

            string appidstring = "";
            if (id > 0)
            {
                //获取数据列
                DataTable dt = new DataTable();
                dt = appmapingBll.GetModelChannelId(id).Tables[0];
                appmapingList = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_channel_app_mapping>(dt);

                for (int i = 0; i < appmapingList.Count; i++)
                {
                    if (i == (appmapingList.Count - 1))
                    {
                        appidstring += appmapingList[i].AppId;
                    }
                    else
                    {
                        appidstring += appmapingList[i].AppId + ",";
                    }

                }

            }

            ViewBag.appidstring = appidstring;
            ViewBag.id = id;

            return View();
        }

        /// <summary>
        /// 通道池应用配置管理
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult AppMappingAdd()
        {
            object retJson = new { success = 0, msg = "操作失败" };

            int cid = string.IsNullOrEmpty(Request["cid"]) ? 0 : int.Parse(Request["cid"]);
            string appId = string.IsNullOrEmpty(Request["appId"]) ? "" : Request["appId"];
            //appid
            string[] appidstr = appId.Split(',');

            if (cid > 0)
            {
                string sql = " delete from jmp_channel_app_mapping where  ChannelId=" + cid;
                string[] amount = appId.Split(',');
                string[] array = new string[amount.Length + 1];
                array[0] = sql;
                int a = 1;
                for (int i = 0; i < amount.Length; i++)
                {

                    if (appidstr[i] != "")
                    {
                        array[a] = "insert into jmp_channel_app_mapping(ChannelId,AppId,CreatedOn,CreatedByUerId,CreatedByUserName) values ('" + cid + "','" + appidstr[i] + "',GETDATE(),'" + UserInfo.UserId + "','" + UserInfo.UserName + "')";
                        sql += "insert into jmp_channel_app_mapping(ChannelId,AppId,CreatedOn,CreatedByUerId,CreatedByUserName) values ('" + cid + "','" + appidstr[i] + "',GETDATE(),'" + UserInfo.UserId + "','" + UserInfo.UserName + "')";
                    }

                    a = a + 1;
                }
                int num = appmapingBll.AddAppMapping(array);
                if (num > 0)
                {
                    retJson = new { success = 1, msg = "设置成功！" };

                    Logger.OperateLog("通道池应用配置", "操作数据：" + sql);
                }
                else
                {
                    retJson = new { success = 0, msg = "设置失败！" };
                }
            }
            else
            {
                retJson = new { success = 0, msg = "应用池ID错误，请联系管理员！" };
            }


            return Json(retJson);
        }

        /// <summary>
        /// 应用弹窗
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false), VisitRecord(IsRecord = true)]
        public ActionResult AppListTc()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);//查询条件选择
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            int platformid = string.IsNullOrEmpty(Request["platformid"]) ? 0 : Int32.Parse(Request["platformid"]);//关联平台
            string appstr = string.IsNullOrEmpty(Request["appstr"]) ? "" : Request["appstr"];//已选择的应用id
            int cid = string.IsNullOrEmpty(Request["cid"]) ? 0 : Int32.Parse(Request["cid"]);//通道池ID
            ViewBag.appstr = appstr;
            ViewBag.cid = cid;
            List<JMP.MDL.jmp_app> list = new List<JMP.MDL.jmp_app>();
            JMP.BLL.jmp_app bll = new JMP.BLL.jmp_app();
            List<JMP.MDL.jmp_app> listapp = new List<JMP.MDL.jmp_app>();
            string xzsql = "   select a.a_id,a.a_name,b.u_realname,b.u_id,a.a_user_id,a.a_auditstate,a.a_state,a.a_platform_id   from jmp_app a  left join jmp_user b on a.a_user_id=b.u_id   where a.a_state=1 and a.a_auditstate=1 and b.u_auditstate=1 and a_id  in(" + appstr + ")  ";
            DataTable dt = !string.IsNullOrEmpty(appstr) ? bll.selectsql(xzsql) : new DataTable();
            listapp = dt.Rows.Count > 0 ? JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_app>(dt) : new List<JMP.MDL.jmp_app>();
            ViewBag.listapp = listapp;
            //string sql = "  select a.a_id,a.a_name,b.u_realname,b.u_id,a.a_user_id,a.a_auditstate,a.a_state,a.a_platform_id  from jmp_app a  left join jmp_user b on a.a_user_id=b.u_id  where a.a_state=1 and a.a_auditstate=1 and b.u_auditstate=1    ";
            string sql = "select * from (select a.a_id,a.a_name,b.u_realname,b.u_id,a.a_user_id,a.a_auditstate,a.a_state,a.a_platform_id from jmp_app a left join jmp_user b on a.a_user_id = b.u_id where a.a_state = 1 and a.a_auditstate = 1 and b.u_auditstate = 1 ) as a left join (select b.AppId from jmp_channel_pool a  join jmp_channel_app_mapping b on a.Id = b.ChannelId and a.IsEnabled = 1 and a.Id!=" + cid + ") as b on a.a_id = b.AppId where b.AppId is null";

            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        sql += " and   a.a_id=" + sea_name;//应用编号
                        break;
                    case 2:
                        sql += " and a.a_name like '%" + sea_name + "%' ";//应用名称
                        break;
                    case 3:
                        sql += " and b.u_realname like '%" + sea_name + "%' ";//用户名称
                        break;
                }
            }
            if (platformid > 0)
            {
                sql += " and a.a_platform_id=" + platformid;
            }
            if (!string.IsNullOrEmpty(appstr))
            {
                sql += "  and a_id not in(" + appstr + ") ";
            }
            string order = " order by a_state ";
            list = bll.SelectTClist(sql, order, pageIndexs, PageSize, out pageCount);
            ViewBag.type = type;
            ViewBag.sea_name = sea_name;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            ViewBag.platformid = platformid;

            return View();
        }

        /// <summary>
        /// 通道池通道数量配置
        /// </summary>
        /// <returns></returns>
        public ActionResult ChannelAmountAdd()
        {
            //应用池ID
            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);

            if (id > 0)
            {
                DataTable dt = new DataTable();
                dt = amountBll.GetModelChannelPoolId(id).Tables[0];
                amountList = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_channel_amount_config>(dt);

            }

            ViewBag.id = id;
            ViewBag.amountList = amountList;

            return View();
        }

        /// <summary>
        /// 通道池通道数据配置方法
        /// </summary>
        /// <returns></returns>
        public JsonResult AmountAdd()
        {
            object retJson = new { success = 0, msg = "操作失败" };

            int cid = string.IsNullOrEmpty(Request["cid"]) ? 0 : int.Parse(Request["cid"]);
            string amountArr = string.IsNullOrEmpty(Request["amountArr"]) ? "" : Request["amountArr"];

            if (cid > 0)
            {
                string sql = " delete from jmp_channel_amount_config where  ChannelPoolId=" + cid;
                string[] amount = amountArr.Split('|');
                string[] array = new string[amount.Length + 1];
                array[0] = sql;
                int a = 1;
                for (int i = 0; i < amount.Length; i++)
                {
                    string[] b = amount[i].Split(',');
                    array[a] = "insert into jmp_channel_amount_config(ChannelPoolId,WhichHour,Amount,CreatedOn,CreatedByUserId,CreatedByUserName) values ('" + cid + "','" + b[0] + "','" + b[1] + "',GETDATE(),'" + UserInfo.UserId + "','" + UserInfo.UserName + "')";
                    sql += "insert into jmp_channel_amount_config(ChannelPoolId,WhichHour,Amount,CreatedOn,CreatedByUserId,CreatedByUserName) values ('" + cid + "','" + b[0] + "','" + b[1] + "',GETDATE(),'" + UserInfo.UserId + "','" + UserInfo.UserName + "')";
                    a = a + 1;
                }
                int num = amountBll.AddAmount(array);
                if (num > 0)
                {
                    retJson = new { success = 1, msg = "设置成功！" };

                    Logger.OperateLog("通道池通道数据配置", "操作数据：" + sql);
                }
                else
                {
                    retJson = new { success = 0, msg = "设置失败！" };
                }
            }
            else
            {
                retJson = new { success = 0, msg = "应用池ID错误，请联系管理员！" };
            }

            return Json(retJson);
        }

        #endregion

        #region 通道池通道管理

        public ActionResult PoolInterfaceList()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量

            int PoolName = string.IsNullOrEmpty(Request["PoolName"]) ? 0 : Int32.Parse(Request["PoolName"]);//查询类型
            string searchKey = string.IsNullOrEmpty(Request["searchKey"]) ? "" : Request["searchKey"];//关键字
            int IsEnabled = string.IsNullOrEmpty(Request["IsEnabled"]) ? -1 : Int32.Parse(Request["IsEnabled"]);//状态

            //查询所有
            poolList = poolBll.poolList(PoolName, searchKey, IsEnabled, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = poolList;
            ViewBag.PoolName = PoolName;
            ViewBag.searchKey = searchKey;
            ViewBag.IsEnabled = IsEnabled;

            //查询应用名称
            StringBuilder sqlinfo = new StringBuilder();
            sqlinfo.AppendFormat(@"select ChannelId,a_name from jmp_channel_app_mapping a left join jmp_app b on a.AppId=b.a_id");

            DataTable dt = new DataTable();
            dt = appmapingBll.SelectList(sqlinfo.ToString());
            appmapingList = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_channel_app_mapping>(dt);
            ViewBag.appmapinglist = appmapingList;

            //查询通道池下所有通道
            StringBuilder sqlinfo2 = new StringBuilder();
            sqlinfo2.AppendFormat(@"select id,l_isenable,l_corporatename from jmp_channel_pool a left join jmp_interface b on a.Id=b.l_apptypeid and b.l_risk=2");
            DataTable dt2 = new DataTable();
            dt2 = appmapingBll.SelectList(sqlinfo2.ToString());
            poolList = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_channel_pool>(dt2);
            ViewBag.PoolInterface = poolList;

            return View();
        }

        #endregion

    }
}
