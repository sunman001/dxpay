/************聚米支付平台__系统管理************/
//描述：支付类型管理、系统设置、手续费管理
//功能：支付类型管理、系统设置、手续费管理
//开发者：秦际攀
//开发时间: 2016.04.06
/************聚米支付平台__系统管理************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JMP.TOOL;
using System.Data;
using WEB.Util.Logger;
using WEB.Util.RateLogger;
using TOOL.Extensions;

namespace WEB.Controllers
{
    public class SystemController : Controller
    {
        JMP.BLL.jmp_limit bll_limit = new JMP.BLL.jmp_limit();
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;
        private static readonly IRateLogWriter RateLogger = RateLogWriterManager.GetOperateLogger;

        #region 支付类型管理---秦际攀2016年4月5日
        /// <summary>
        /// 支付类型管理界面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult PaymodeList()
        {
            #region 权限查询
            string locUrl = "";

            bool getUidT = bll_limit.GetLocUserLimitVoids("/System/UpdatePaymodeState", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//一键启用支付类型
            if (getUidT)
            {
                locUrl += "<li onclick=\"UpdatePaymodeState(1)\"><i class='fa fa-check-square-o'></i>一键解锁</li>";
            }
            bool getUidF = bll_limit.GetLocUserLimitVoids("/System/UpdatePaymodeState", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//一键禁用支付类型
            if (getUidF)
            {
                locUrl += "<li onclick=\"UpdatePaymodeState(0)\"><i class='fa fa-check-square-o'></i>一键锁定</li>";
            }
            ViewBag.locUrl = locUrl;
            #endregion
            #region 查询信息
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            string sql = "select * from jmp_paymode where 1=1 ";
            JMP.BLL.jmp_paymode bll = new JMP.BLL.jmp_paymode();
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            ViewBag.searchDesc = searchDesc;
            int SelectState = string.IsNullOrEmpty(Request["SelectState"]) ? -1 : Int32.Parse(Request["SelectState"]);//状态
            ViewBag.SelectState = SelectState;
            int searchType = string.IsNullOrEmpty(Request["searchType"]) ? 0 : Int32.Parse(Request["searchType"]);//查询条件选择
            ViewBag.searchType = searchType;
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            ViewBag.sea_name = sea_name;
            if (searchType > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (searchType)
                {
                    case 1:
                        sql += " and p_id='" + sea_name + "' ";
                        break;
                    case 2:
                        sql += " and p_name='" + sea_name + "' ";
                        break;
                }
            }
            if (SelectState > -1)
            {
                sql += " and p_state='" + SelectState + "' ";
            }
            string Order = " order by p_id desc ";
            if (searchDesc == 1)
            {
                Order = " order by p_id  ";
            }
            else
            {
                Order = " order by p_id desc ";
            }
            List<JMP.MDL.jmp_paymode> list = bll.SelectList(sql, Order, pageIndexs, PageSize, out pageCount);
            ViewBag.pageCount = pageCount;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.list = list;
            #endregion
            return View();
        }

        /// <summary>
        /// 设置接口费率
        /// </summary>
        /// <returns></returns>
        public ActionResult PaymodeRartAdd()
        {
            int id = string.IsNullOrEmpty(Request["pid"]) ? 0 : int.Parse(Request["pid"]);

            JMP.MDL.jmp_paymode model = new JMP.MDL.jmp_paymode();
            JMP.BLL.jmp_paymode bll = new JMP.BLL.jmp_paymode();

            //查询
            model = bll.GetModel(id);

            ViewBag.model = model;

            return View();
        }

        /// <summary>
        /// 方法
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult UpdatePayRart()
        {
            JMP.BLL.jmp_paymode bll = new JMP.BLL.jmp_paymode();
            JMP.MDL.jmp_paymode model = new JMP.MDL.jmp_paymode();

            object retJson = new { success = 0, msg = "操作失败" };

            int pid = string.IsNullOrEmpty(Request["pid"]) ? 0 : int.Parse(Request["pid"]);
            string p_rate = string.IsNullOrEmpty(Request["p_rate"]) ? "0" : Request["p_rate"];

            //获取一个实体对象
            model = bll.GetModel(pid);

            if (bll.Update_rate(pid, p_rate))
            {
                //记录日志（会定期清理）
                Logger.OperateLog("修改接口费率", "操作数据ID：" + pid + ",接口费率由：" + model.p_rate + ",改为：" + p_rate + "。");
                //记录日志（不会清理）
                RateLogger.OperateLog("修改接口费率", "操作数据ID：" + pid + ",接口费率由：" + model.p_rate + ",改为：" + p_rate + "。");

                retJson = new { success = 1, msg = "设置接口费率成功" };
            }

            else
            {
                retJson = new { success = 0, msg = "设置接口费率失败" };
            }

            return Json(retJson);
        }


        /// <summary>
        /// 添加或修改支付类型管理界面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult PaymodeAddOrUpdate()
        {
            int p_id = string.IsNullOrEmpty(Request["p_id"]) ? 0 : Int32.Parse(Request["p_id"]);
            JMP.BLL.jmp_paymode bll = new JMP.BLL.jmp_paymode();
            JMP.MDL.jmp_paymode mode = new JMP.MDL.jmp_paymode();
            if (p_id > 0)
            {
                mode = bll.GetModel(p_id);
            }
            ViewBag.mode = mode;
            return View();
        }
        /// <summary>
        /// 一键启用或禁用支付类型状态
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdatePaymodeState()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            int state = string.IsNullOrEmpty(Request["state"]) ? 0 : Int32.Parse(Request["state"].ToString());
            string str = Request["ids"];
            string xgzfc = "";//组装说明
            string tsmsg = "";//提示
            JMP.BLL.jmp_paymode bll = new JMP.BLL.jmp_paymode();
            if (str.CompareTo("On") > 0)
            {
                str = str.Substring(3);
            }
            if (bll.UpdateLocUserState(str, state))
            {
                if (state == 1)
                {
                    xgzfc = "一键启用ID为：" + str;
                    tsmsg = "启用成功";
                }
                else
                {
                    tsmsg = "禁用成功";
                    xgzfc = "一键禁用ID为：" + str;
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
            return Json(retJson);
        }

        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false), VisitRecord(IsRecord = true)]

        public ActionResult SelectInterface()
        {
            #region  查询
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            string sql = " select a .l_id, a .l_str, a .l_sort, a .l_isenable, a .l_paymenttype_id,b.p_name,b.p_type from  jmp_interface a  left join jmp_paymenttype b on b.p_id=a.l_paymenttype_id  where 1=1  and a.l_isenable='1' ";
            string Order = " order by l_id desc ";
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            int auditstate = string.IsNullOrEmpty(Request["auditstate"]) ? 0 : Int32.Parse(Request["auditstate"]);//支付类型
            int type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);//查询条件选择
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        sql += " and a.l_id='" + sea_name + "' ";
                        break;
                    case 2:
                        sql += " and b.p_name='" + sea_name + "' ";
                        break;
                }
            }
            if (auditstate > -1)
            {
                sql += " and b.p_type='" + auditstate + "' ";
            }
            if (searchDesc == 1)
            {
                Order = "  order by l_id  ";
            }
            else
            {
                Order = " order by l_id desc ";
            }
            JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
            List<JMP.MDL.jmp_interface> list = new List<JMP.MDL.jmp_interface>();
            list = bll.SelectList(sql, Order, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            ViewBag.searchDesc = searchDesc;
            ViewBag.type = type;
            ViewBag.auditstate = auditstate;
            ViewBag.sea_name = sea_name;
            #endregion
            return View();
        }
        #endregion

        #region 字典管理-----秦际攀2016年4月6日
        /// <summary>
        /// 字典管理
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult dictionary()
        {
            var bll = new JMP.BLL.jmp_system();
            var model = bll.GetModelList("");
            return View(model);
        }
        /// <summary>
        /// 添加或修改系统配置
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public JsonResult InsertOrUpdateSystem(JMP.MDL.jmp_system mode)
        {
            object retJson = new { success = 0, msg = "操作失败" };
            JMP.BLL.jmp_system bll = new JMP.BLL.jmp_system();
            JMP.MDL.jmp_system mo = new JMP.MDL.jmp_system();
            if (mode.s_id > 0)
            {
                mo = bll.GetModel(mode.s_id);
                mode.s_state = mo.s_state;
                if (mo.s_name == "password")
                {
                    //md5加密
                    mode.s_value = DESEncrypt.Encrypt(mode.s_value);
                }
                if (bll.Update(mode))
                {
                    #region 日志说明

                    Logger.ModifyLog("用户" + UserInfo.UserName + "修改系统配置", mo, mode);
                    #endregion
                    retJson = new { success = 1, msg = "修改成功" };
                }
                else
                {
                    retJson = new { success = 0, msg = "修改失败" };
                };
            }
            else
            {
                #region
                mode.s_state = 1;
                int cg = bll.Add(mode);
                if (cg > 0)
                {

                    Logger.CreateLog("添加系统配置", mode);
                    retJson = new { success = 1, msg = "操作成功" };
                }
                else
                {
                    retJson = new { success = 0, msg = "操作失败" };
                }
                #endregion
            }
            return Json(retJson);
        }

        [VisitRecord(IsRecord = true)]
        [LoginCheckFilter(IsCheck = true, IsRole = false)]
        public JsonResult FindByName(string name)
        {
            var bll = new JMP.BLL.jmp_system();
            var mo = bll.GetModelList("s_name='" + name + "'").FirstOrDefault();
            return Json(mo, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 结算管理-----秦际攀2016年4月6日
        /// <summary>
        /// 结算设置界面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult SettlementList()
        {
            #region 获取权限
            string locUrl = "";

            bool getUidT = bll_limit.GetLocUserLimitVoids("/System/UpdatepoundageState", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//一键启用和禁用结算
            if (getUidT)
            {
                locUrl += " <li onclick=\"javascript:UpdateSettlementState(1)\"><i class='fa fa-check-square-o'></i>一键启用</li>";

                locUrl += "<li onclick=\"javascript:UpdateSettlementState(0);\"><i class='fa fa-check-square-o'></i>一键禁用</li>";
            }
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/System/SettlementAddOrUpdate", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加支付类型
            if (getlocuserAdd)
            {
                locUrl += "<li onclick=\"AddSettlement()\"><i class='fa fa-plus'></i>添加结算</li>";
            }
            ViewBag.locUrl = locUrl;
            #endregion
            #region 查询
            JMP.BLL.jmp_poundage bll = new JMP.BLL.jmp_poundage();
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            string sql = "select * from jmp_poundage where 1=1 ";
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            ViewBag.searchDesc = searchDesc;
            int SelectState = string.IsNullOrEmpty(Request["SelectState"]) ? -1 : Int32.Parse(Request["SelectState"]);//状态
            ViewBag.SelectState = SelectState;
            int searchType = string.IsNullOrEmpty(Request["searchType"]) ? 0 : Int32.Parse(Request["searchType"]);//查询条件选择
            ViewBag.searchType = searchType;
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            ViewBag.sea_name = sea_name;
            string Order = "order by p_id ";
            if (searchType > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (searchType)
                {
                    case 1:
                        sql += " and p_id='" + sea_name + "' ";
                        break;
                }
            }
            if (SelectState > -1)
            {
                sql += " and p_state='" + SelectState + "' ";
            }
            if (searchDesc == 1)
            {
                Order = " order by p_id  ";
            }
            else
            {
                Order = " order by p_id desc ";
            }
            List<JMP.MDL.jmp_poundage> list = bll.SelectList(sql, Order, pageIndexs, PageSize, out pageCount);
            ViewBag.pageCount = pageCount;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.list = list;
            #endregion
            return View();
        }
        /// <summary>
        /// 添加或修改结算界面
        /// </summary>
        /// <returns></returns>
        public ActionResult SettlementAddOrUpdate()
        {
            int p_id = string.IsNullOrEmpty(Request["p_id"]) ? 0 : Int32.Parse(Request["p_id"]);
            JMP.BLL.jmp_poundage bll = new JMP.BLL.jmp_poundage();
            JMP.MDL.jmp_poundage mode = new JMP.MDL.jmp_poundage();
            if (p_id > 0)
            {
                mode = bll.GetModel(p_id);
            }
            ViewBag.mode = mode;
            return View();
        }
        /// <summary>
        /// 添加或修改结算设置
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public JsonResult AddOrUpdateSettlement(JMP.MDL.jmp_poundage mode)
        {
            object retJson = new { success = 0, msg = "操作失败" };
            JMP.BLL.jmp_poundage bll = new JMP.BLL.jmp_poundage();
            if (mode.p_id > 0)
            {
                #region 修改
                JMP.MDL.jmp_poundage mo = bll.GetModel(mode.p_id);
                mode.p_state = mo.p_state;
                if (bll.Update(mode))
                {
                    #region 日志说明
                    Logger.ModifyLog("修改结算设置", mo, mode);
                    #endregion
                    retJson = new { success = 1, msg = "修改成功" };
                }
                else
                {
                    retJson = new { success = 0, msg = "修改失败" };
                }
                #endregion
            }
            else
            {
                #region 添加
                mode.p_state = 0;
                int cg = bll.Add(mode);
                if (cg > 0)
                {

                    Logger.CreateLog("添加结算设置", mode);
                    retJson = new { success = 1, msg = "添加成功" };
                }
                else
                {
                    retJson = new { success = 0, msg = "添加失败" };
                }
                #endregion
            }
            return Json(retJson);
        }
        /// <summary>
        /// 结算设置一键启用或禁用
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdatepoundageState()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            int state = string.IsNullOrEmpty(Request["state"]) ? 0 : Int32.Parse(Request["state"].ToString());
            string str = Request["ids"];
            string xgzfc = "";//组装说明
            string tsmsg = "";//提示
            JMP.BLL.jmp_poundage bll = new JMP.BLL.jmp_poundage();
            if (str.CompareTo("On") > 0)
            {
                str = str.Substring(3);
            }
            if (bll.UpdateLocUserState(str, state))
            {
                if (state == 1)
                {
                    xgzfc = "一键启用ID为：" + str;
                    tsmsg = "启用成功";
                }
                else
                {
                    tsmsg = "禁用成功";
                    xgzfc = "一键禁用ID为：" + str;
                }

                Logger.OperateLog("结算设置一键启用或禁用", xgzfc);
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
        #endregion

        #region 调单管理----秦际攀2016年9月20日
        /// <summary>
        /// 调单管理界面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult dispatchorderLsit()
        {
            #region 权限查询
            string locUrl = "";
            bool getUidT = bll_limit.GetLocUserLimitVoids("/System/UpdateDisoOderState", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//一键启用支付类型
            if (getUidT)
            {
                locUrl += "<li onclick=\"javascript:UpdateddState(0)\"><i class='fa fa-check-square-o'></i>一键启用</li>";
                locUrl += "<li onclick=\"javascript:UpdateddState(1);\"><i class='fa fa-check-square-o'></i>一键禁用</li>";
            }
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/System/AddOrUpdatedispatchorder", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加支付类型
            if (getlocuserAdd)
            {
                locUrl += "<li id=\"ToolBar\" onclick=\"adddispatchorderLsit()\"><i class='fa fa-plus'></i>掉单设置</li>";
            }
            ViewBag.locUrl = locUrl;
            #endregion
            #region 查询
            List<JMP.MDL.jmp_dispatchorder> list = new List<JMP.MDL.jmp_dispatchorder>();
            JMP.BLL.jmp_dispatchorder bll = new JMP.BLL.jmp_dispatchorder();
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            ViewBag.searchDesc = searchDesc;
            int SelectState = string.IsNullOrEmpty(Request["SelectState"]) ? -1 : Int32.Parse(Request["SelectState"]);//状态
            ViewBag.SelectState = SelectState;
            int searchType = string.IsNullOrEmpty(Request["searchType"]) ? 0 : Int32.Parse(Request["searchType"]);//查询条件选择
            ViewBag.searchType = searchType;
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            ViewBag.sea_name = sea_name;
            string sql = " select a.d_apptyeid,a.d_id,a.d_ratio,a.d_state,a.d_datatime,b.t_name from  jmp_dispatchorder a left join  jmp_apptype b on a.d_apptyeid=b.t_id ";
            string where = " where 1=1 ";
            if (searchType > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (searchType)
                {
                    case 1:
                        where += " and a.d_id=" + sea_name;
                        break;
                    case 2:
                        where += " and b.t_name like'%" + sea_name + "%' ";
                        break;
                }
            }
            if (SelectState > -1)
            {
                where += SelectState == 1 ? " and a.d_state=1 " : " and a.d_state=0 ";
            }
            string Order = searchDesc == 0 ? "order by d_id desc " : "order by d_id ";
            sql = sql + where;
            list = bll.SelectPager(sql, Order, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            #endregion
            return View();
        }
        /// <summary>
        /// 调单添加界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Adddispatchorder()
        {
            int did = string.IsNullOrEmpty(Request["did"]) ? 0 : Int32.Parse(Request["did"]);
            string where = "";
            where = did > 0 ? " t_state='1' and t_id  =(select d_apptyeid from jmp_dispatchorder where d_id=" + did + "  ) and t_topid=0" : " t_state='1' and t_id not in( select d_apptyeid from jmp_dispatchorder   ) and t_topid=0";
            JMP.BLL.jmp_apptype yybll = new JMP.BLL.jmp_apptype();
            DataTable yydt = yybll.GetList(where).Tables[0];//获取应用类型在用信息
            List<JMP.MDL.jmp_apptype> yylist = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_apptype>(yydt);
            ViewBag.yylist = yylist;
            JMP.BLL.jmp_dispatchorder bll = new JMP.BLL.jmp_dispatchorder();
            JMP.MDL.jmp_dispatchorder mode = new JMP.MDL.jmp_dispatchorder();
            if (did > 0)
            {
                mode = bll.GetModel(did) == null ? new JMP.MDL.jmp_dispatchorder() : bll.GetModel(did);
            }
            ViewBag.mode = mode;
            return View();
        }
        /// <summary>
        /// 添加或修改调单设置
        /// </summary>
        /// <param name="mo"></param>
        /// <returns></returns>
        public JsonResult AddOrUpdatedispatchorder(JMP.MDL.jmp_dispatchorder mo)
        {
            object retJson = new { success = 0, msg = "操作失败" };
            JMP.BLL.jmp_dispatchorder bll = new JMP.BLL.jmp_dispatchorder();
            if (mo.d_id > 0)
            {
                JMP.MDL.jmp_dispatchorder mod = new JMP.MDL.jmp_dispatchorder();
                mod = bll.GetModel(mo.d_id);

                mo.d_state = mod.d_state;
                mo.d_datatime = DateTime.Now;
                if (bll.Update(mo))
                {
                    #region 日志说明
                    Logger.ModifyLog("修改调单设置", mod, mo);
                    #endregion
                    retJson = new { success = 1, msg = "编辑成功！" };
                }
                else
                {
                    retJson = new { success = 0, msg = "编辑失败！" };
                }
            }
            else
            {
                mo.d_datatime = DateTime.Now;
                mo.d_state = 0;
                int cg = bll.Add(mo);
                if (cg > 0)
                {

                    Logger.CreateLog("添加调单设置", mo);
                    retJson = new { success = 1, msg = "添加成功！" };
                }
                else
                {
                    retJson = new { success = 0, msg = "添加失败！" };
                }
            }
            return Json(retJson);
        }
        /// <summary>
        /// 冻结或解冻掉单设置
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdateDisoOderState()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            int state = string.IsNullOrEmpty(Request["state"]) ? 0 : Int32.Parse(Request["state"].ToString());
            string str = Request["ids"];
            JMP.BLL.jmp_dispatchorder bll = new JMP.BLL.jmp_dispatchorder();
            string tsmsg = ""; //提示说明
            string xgzfc = "";//日志说明
            if (str.CompareTo("On") > 0)
            {
                str = str.Substring(3);
            }
            if (bll.UpdateState(str, state))
            {
                tsmsg = state == 0 ? "启用成功" : "禁用成功";
                xgzfc = state == 0 ? "一键启用ID为：" + str : "一键禁用ID为：" + str;
                Logger.OperateLog("掉单设置一键启用或禁用", xgzfc);
                retJson = new { success = 1, msg = tsmsg };
            }
            else
            {
                tsmsg = state == 0 ? "启用失败" : "禁用失败";
                retJson = new { success = 0, msg = tsmsg };
            }
            return Json(retJson);
        }
        #endregion

        #region  服务费/提成等级信息管理
        /// <summary>
        /// 服务费/提成等级信息
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false), VisitRecord(IsRecord = true)]
        public ActionResult ServiceFeeRatioGradeList()
        {
            JMP.BLL.CoServiceFeeRatioGrade bll = new JMP.BLL.CoServiceFeeRatioGrade();
            JMP.MDL.CoServiceFeeRatioGrade model = new JMP.MDL.CoServiceFeeRatioGrade();
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
            List<JMP.MDL.CoServiceFeeRatioGrade> list = new List<JMP.MDL.CoServiceFeeRatioGrade>();
            list = bll.SelectList(sea_name, type, searchDesc, pageIndexs, PageSize, out pageCount);
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
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/System/InsertUpdateSerViceFeeRationGrade", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加管理员
            if (getlocuserAdd)
            {
                locUrl += "<li onclick=\"AddAPPlog()\"><i class='fa fa-plus'></i>添加服务费等级信息</li>";
            }
            return locUrl;
        }
        public ActionResult SerViceFeeRationGradeAdd()
        {
            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : Int32.Parse(Request["id"]);
            JMP.BLL.CoServiceFeeRatioGrade bll = new JMP.BLL.CoServiceFeeRatioGrade();
            JMP.MDL.CoServiceFeeRatioGrade model = new JMP.MDL.CoServiceFeeRatioGrade();
            if (id > 0)
            {
                model = bll.SelectId(id);
                model.AgentPushMoneyRatio = decimal.Parse((model.AgentPushMoneyRatio * 100).ToString("f2"));
                model.BusinessPersonnelAgentRatio = decimal.Parse((model.BusinessPersonnelAgentRatio * 100).ToString("f2"));
                model.CustomerWithoutAgentRatio = decimal.Parse((model.CustomerWithoutAgentRatio * 100).ToString("f2"));
                model.ServiceFeeRatio = decimal.Parse((model.ServiceFeeRatio * 100).ToString("f2"));
            }
            ViewBag.model = model == null ? new JMP.MDL.CoServiceFeeRatioGrade() : model;
            return View();
        }

        /// <summary>
        /// 添加或修改服务费等级信息
        /// </summary>
        /// <returns></returns>
        public JsonResult InsertUpdateSerViceFeeRationGrade(JMP.MDL.CoServiceFeeRatioGrade model)
        {
            object retJson = new { success = 0, msg = "操作失败" };
            JMP.BLL.CoServiceFeeRatioGrade bll = new JMP.BLL.CoServiceFeeRatioGrade();
            model.AgentPushMoneyRatio = model.AgentPushMoneyRatio / 100;
            model.BusinessPersonnelAgentRatio = model.BusinessPersonnelAgentRatio / 100;
            model.CustomerWithoutAgentRatio = model.CustomerWithoutAgentRatio / 100;
            model.ServiceFeeRatio = model.ServiceFeeRatio / 100;
            var exists = bll.GetModelByName(model.Name);
            if (model.Id > 0)
            {
                // 修改
                JMP.MDL.CoServiceFeeRatioGrade oldmodel = new JMP.MDL.CoServiceFeeRatioGrade();
                oldmodel = bll.GetModel(model.Id);
                var oldmodelConle = oldmodel.Clone();
                oldmodel.Name = model.Name;
                oldmodel.ServiceFeeRatio = model.ServiceFeeRatio;
                oldmodel.BusinessPersonnelAgentRatio = model.BusinessPersonnelAgentRatio;
                oldmodel.CustomerWithoutAgentRatio = model.CustomerWithoutAgentRatio;
                oldmodel.AgentPushMoneyRatio = model.AgentPushMoneyRatio;
                oldmodel.Description = model.Description;
                if (exists != null && model.Id != exists.Id)
                {
                    retJson = new { success = 0, msg = "此服务费级别名称已存在" };
                    return Json(retJson);
                }
                if (bll.Update(oldmodel))
                {

                    Logger.ModifyLog("修改服务费等级信息", oldmodelConle, model);
                    RateLogger.ModifyLog("修改服务费等级信息", oldmodelConle, model);
                    retJson = new { success = 1, msg = "修改成功" };
                }
                else
                {
                    retJson = new { success = 0, msg = "修改失败" };
                }

            }
            else
            {
                model.CreatedOn = DateTime.Now;
                model.CreatedById = UserInfo.UserId;
                model.CreatedByName = UserInfo.UserName;

                if (exists != null)
                {
                    retJson = new { success = 0, msg = "此服务费级别名称已存在" };
                    return Json(retJson);
                }
                int cg = bll.Add(model);
                if (cg > 0)
                {

                    Logger.CreateLog("添加服务费等级信息", model);
                    RateLogger.CreateLog("添加服务费等级信息", model);
                    retJson = new { success = 1, msg = "添加成功" };

                }
                else
                {
                    retJson = new { success = 1, msg = "添加失败" };
                }

            }
            return Json(retJson);
        }

        #endregion


    }
}
