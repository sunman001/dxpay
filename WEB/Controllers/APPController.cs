/************聚米支付平台__应用管理控制器************/
//描述：应用管理控制器
//功能：应用管理控制器
//开发者：秦际攀
//开发时间: 2016.03.14
/************聚米支付平台__应用管理控制器************/
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data;
using JMP.TOOL;
using WEB.Util.Logger;
using TOOL;
using TOOL.Extensions;
using JMP.BLL;

namespace WEB.Controllers
{
    /// <summary>
    /// 应用管理控制器
    /// </summary>
    public class APPController : Controller
    {
        /// <summary>
        /// 日志收集器
        /// </summary>
		private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;

        JMP.BLL.jmp_limit bll_limit = new JMP.BLL.jmp_limit();

        #region 应用管理
        /// <summary>
        /// 应用管理界面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult AppList()
        {
            #region========获取应用类型在用信息======
            JMP.BLL.jmp_apptype yybll = new JMP.BLL.jmp_apptype();
            string where = "  t_id in (select  DISTINCT(t_topid) from jmp_apptype where t_topid in( select t_id from jmp_apptype where t_topid='0'   )) and t_state='1' order by t_sort desc";
            DataTable yydt = yybll.GetList(where).Tables[0];//获取应用类型在用信息
            List<JMP.MDL.jmp_apptype> yylist = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_apptype>(yydt);
            ViewBag.yylist = yylist;
            #endregion

            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            int SelectState = string.IsNullOrEmpty(Request["SelectState"]) ? 1 : Int32.Parse(Request["SelectState"]);//状态
            int auditstate = string.IsNullOrEmpty(Request["auditstate"]) ? -1 : Int32.Parse(Request["auditstate"]);//审核状态
            int type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);//查询条件选择
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            int platformid = string.IsNullOrEmpty(Request["platformid"]) ? 0 : Int32.Parse(Request["platformid"]);//关联平台
            int appType = string.IsNullOrEmpty(Request["appType"]) ? 0 : int.Parse(Request["appType"]); //所属应用类型	
            int r_id = string.IsNullOrEmpty(Request["r_id"]) ? 0 : int.Parse(Request["r_id"]); //风险等级	
            int paytype= string.IsNullOrEmpty(Request["paytype"]) ? 0 : int.Parse(Request["paytype"]); //支付类型	
            List<JMP.MDL.jmp_app> list = new List<JMP.MDL.jmp_app>();
            JMP.BLL.jmp_app bll = new JMP.BLL.jmp_app();
            list = bll.SelectList(paytype, r_id,platformid, auditstate, sea_name, type, SelectState, appType, searchDesc, pageIndexs, PageSize, out pageCount);
            string wherepay = " p_state=1";
            JMP.BLL.jmp_paymode yybllt = new JMP.BLL.jmp_paymode();
            DataTable yydtt = yybllt.GetList(wherepay).Tables[0];//获取支付方式
            List<JMP.MDL.jmp_paymode> yylistt = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_paymode>(yydtt);
            ViewBag.yylistt = yylistt;
            ViewBag.searchDesc = searchDesc;
            ViewBag.paytype = paytype;
            ViewBag.SelectState = SelectState;
            ViewBag.r_id = r_id;
            ViewBag.type = type;
            ViewBag.sea_name = sea_name;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            ViewBag.auditstate = auditstate;
            ViewBag.platformid = platformid;
            ViewBag.locUrl = GetVoidHtml();
            ViewBag.appType = appType;
            return View();
        }
        /// <summary>
        /// 选择列表
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult AppListTC()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            //int SelectState = string.IsNullOrEmpty(Request["SelectState"]) ? 1 : Int32.Parse(Request["SelectState"]);//状态
            //int auditstate = string.IsNullOrEmpty(Request["auditstate"]) ? -1 : Int32.Parse(Request["auditstate"]);//审核状态
            int type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);//查询条件选择
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            int platformid = string.IsNullOrEmpty(Request["platformid"]) ? 0 : Int32.Parse(Request["platformid"]);//关联平台
            List<JMP.MDL.jmp_app> list = new List<JMP.MDL.jmp_app>();
            JMP.BLL.jmp_app bll = new JMP.BLL.jmp_app();
            list = bll.SelectListTc(platformid, searchDesc, type, sea_name, pageIndexs, PageSize, out pageCount);
            ViewBag.searchDesc = searchDesc;
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
        /// 显示页面方法是否有权限
        /// </summary>
        public string GetVoidHtml()
        {
            string locUrl = "";

            bool getUidT = bll_limit.GetLocUserLimitVoids("/APP/UpdateState", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//本地管理员一键启用
            if (getUidT)
            {
                locUrl += "<li onclick=\"javascript:Updatestate(1)\"><i class='fa fa-check-square-o'></i>一键启用</li>";
            }
            bool getUidF = bll_limit.GetLocUserLimitVoids("/APP/UpdateState", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//本地管理员一键禁用
            if (getUidF)
            {
                locUrl += "<li onclick=\"javascript:Updatestate(0);\"><i class='fa fa-check-square-o'></i>一键禁用</li>";
            }
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/APP/InsertUpdateApp", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加管理员
            if (getlocuserAdd)
            {
                locUrl += "<li onclick=\"AddAPPlog()\"><i class='fa fa-plus'></i>添加应用</li>";
            }
            return locUrl;
        }
        /// <summary>
        /// 添加修改应用界面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult APPAdd()
        {
            #region =========获取应用平台在用信息=========
            JMP.BLL.jmp_platform bll = new JMP.BLL.jmp_platform();
            DataTable dt = bll.GetList(" 1=1 and p_state='1' ").Tables[0];//获取应用平台在用信息 
            List<JMP.MDL.jmp_platform> yypt = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_platform>(dt);
            ViewBag.glptdt = yypt;
            #endregion
            #region=====获取支付类型在用信息======
            JMP.BLL.jmp_paymode zfbll = new JMP.BLL.jmp_paymode();
            DataTable zfdt = zfbll.GetList(" 1=1 and p_state='1' ").Tables[0];//获取支付类型在用信息
            List<JMP.MDL.jmp_paymode> zflist = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_paymode>(zfdt);
            ViewBag.zflist = zflist;
            #endregion
            #region========获取应用类型在用信息======
            JMP.BLL.jmp_apptype yybll = new JMP.BLL.jmp_apptype();
            string where = "  t_id in (select  DISTINCT(t_topid) from jmp_apptype where t_topid in( select t_id from jmp_apptype where t_topid='0'   )) and t_state='1' order by t_sort desc";
            DataTable yydt = yybll.GetList(where).Tables[0];//获取应用类型在用信息
            List<JMP.MDL.jmp_apptype> yylist = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_apptype>(yydt);
            ViewBag.yylist = yylist;
            #endregion
            return View();
        }
        /// <summary>
        /// 
        /// 根据主应用id查询子类应用
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public string SelectApp()
        {
            string yyid = string.IsNullOrEmpty(Request["id"].ToString()) ? "0" : Request["id"].ToString();
            int a_apptype_id = string.IsNullOrEmpty(Request["a_apptype_id"]) ? 0 : Int32.Parse(Request["a_apptype_id"]);
            JMP.BLL.jmp_apptype bll = new JMP.BLL.jmp_apptype();
            DataTable dt = bll.GetList(" 1=1 and t_topid='" + yyid.Replace("yy", "").Trim() + "' and t_state='1' order by t_sort desc  ").Tables[0];
            string yyzl = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (a_apptype_id > 0)
                {
                    if (dt.Rows[i]["t_id"].ToString() == a_apptype_id.ToString())
                    {
                        yyzl += "<label><input type=\"radio\" name=\"zlxz\" id='" + dt.Rows[i]["t_id"] + "' value='" + dt.Rows[i]["t_id"] + "' checked=\"checked\" style=\"margin-left:10px;\" />&nbsp;" + dt.Rows[i]["t_name"] + "</label>";
                    }
                    else
                    {
                        yyzl += "<label><input type=\"radio\" name=\"zlxz\" id='" + dt.Rows[i]["t_id"] + "' value='" + dt.Rows[i]["t_id"] + "'  style=\"margin-left:10px;\" />&nbsp;" + dt.Rows[i]["t_name"] + "</label>";
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        yyzl += "<label><input type=\"radio\" name=\"zlxz\" id='" + dt.Rows[i]["t_id"] + "' value='" + dt.Rows[i]["t_id"] + "' checked=\"checked\" style=\"margin-left:10px;\" />&nbsp;" + dt.Rows[i]["t_name"] + "</label>";
                    }
                    else
                    {
                        yyzl += "<label><input type=\"radio\" name=\"zlxz\" id='" + dt.Rows[i]["t_id"] + "' value='" + dt.Rows[i]["t_id"] + "'  style=\"margin-left:10px;\" />&nbsp;" + dt.Rows[i]["t_name"] + "</label>";
                    }
                }


            }
            return yyzl;
        }
        /// <summary>
        /// 添加或修改应用
        /// </summary>
        /// <returns></returns>
        public JsonResult InsertUpdateApp(JMP.MDL.jmp_app mod)
        {
            object retJson = new { success = 0, msg = "操作失败" };
            JMP.BLL.jmp_app bll = new JMP.BLL.jmp_app();



            //string xgzfc = "";
            if (string.IsNullOrEmpty(mod.a_paymode_id))
            {
                retJson = new { success = 0, msg = "请选择支付类型" };
            }
            else
            {
                if (mod.a_apptype_id == 0)
                {
                    retJson = new { success = 0, msg = "请选择应用类型" };
                }
                else
                {
                    if (mod.a_id > 0)
                    {

                        #region 修改应用
                        JMP.MDL.jmp_app modapp = new JMP.MDL.jmp_app();
                        modapp = bll.GetModel(mod.a_id);
                        //克隆对象
                        // object modclone = CloneObject.Clone(modapp);
                        var modclone = modapp.Clone();
                        if (mod.a_apptype_id != modapp.a_apptype_id)
                        {
                           modapp.a_auditstate = 0;//应用审核状态(0未审核)
                           modapp.a_rid = 0;//风险等级配置表id
                        }
                        modapp.a_name = mod.a_name;//应用名称
                        modapp.a_platform_id = mod.a_platform_id;//关联平台ID
                        modapp.a_paymode_id = mod.a_paymode_id;//关联支付类型ID
                        modapp.a_apptype_id = mod.a_apptype_id;//关联应用类型ID
                        modapp.a_notifyurl = mod.a_notifyurl;//回掉地址
                        modapp.a_user_id = mod.a_user_id;//开发者ID
                        modapp.a_showurl = mod.a_showurl;//同步地址
                        modapp.a_appurl = mod.a_appurl;//应用审核地址
                        modapp.a_appsynopsis = mod.a_appsynopsis;//应用简介
                        if (mod.a_auditstate != 0)
                        {
                            if (string.IsNullOrEmpty(mod.a_auditor))
                            {
                                mod.a_auditor = UserInfo.UserName;
                            }
                        }
                        else
                        {
                            mod.a_auditor = "";
                        }
                        if (bll.Update(modapp))
                        {
                            Logger.ModifyLog("修改应用", modclone, modapp);

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
                        #region 添加应用
                        mod.a_state = 1;
                        mod.a_auditstate = 0;
                        mod.a_key = "";
                        mod.a_secretkey = "";
                        mod.a_time = DateTime.Now;
                        int cg = bll.Add(mod);
                        if (cg > 0)
                        {
                            Logger.CreateLog("添加应用", mod);

                            //while (true)
                            //{
                            mod.a_key = DESEncrypt.Encrypt(mod.a_user_id + ";" + cg + ";" + DateTime.Now.ToString("yyyyMMddssmmfff"));
                            //if (!bll.Existss(mod.a_key))
                            //{
                            //    break;
                            //}
                            //}
                            mod.a_secretkey = DESEncrypt.Encrypt(cg + ";" + mod.a_key + ";" + DateTime.Now.ToString("yyyyMMddssmmfff"));
                            mod.a_id = cg;
                            if (bll.Update(mod))
                            {
                                Logger.OperateLog("修改应用key", mod.a_key);

                                retJson = new { success = 1, msg = "添加成功" };
                            }
                            else
                            {
                                retJson = new { success = 0, msg = "添加失败" };
                            }

                        }
                        else
                        {
                            retJson = new { success = 0, msg = "添加失败" };
                        }
                        #endregion
                    }
                }
            }
            return Json(retJson);
        }
        /// <summary>
        /// 修改应用
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateAPP(int a_id)
        {
            JMP.BLL.jmp_app bll = new JMP.BLL.jmp_app();
            JMP.MDL.jmp_app model = new JMP.MDL.jmp_app();
            string glpt = "";
            string zf = "";
            string szmrdj = "";
            string yy = "";

            if (a_id > 0)
            {
                model = bll.SelectId(a_id);
                #region =========获取应用平台在用信息=========
                JMP.BLL.jmp_platform bllpl = new JMP.BLL.jmp_platform();
                DataTable dt = bllpl.GetList(" 1=1 and p_state='1' ").Tables[0];//获取应用平台在用信息 
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Int32.Parse(dt.Rows[i]["p_id"].ToString()) == model.a_platform_id)
                    {
                        glpt += "<option value=\"" + dt.Rows[i]["p_id"] + "\" selected=\"selected\" >" + dt.Rows[i]["p_name"] + "</option>";
                    }
                    else
                    {
                        glpt += "<option value=\"" + dt.Rows[i]["p_id"] + "\">" + dt.Rows[i]["p_name"] + "</option>";
                    }

                }
                #endregion
                #region=====获取支付类型在用信息======
                JMP.BLL.jmp_paymode zfbll = new JMP.BLL.jmp_paymode();
                DataTable zfdt = zfbll.GetList(" 1=1 and p_state='1' ").Tables[0];//获取支付类型在用信息
                string[] a_paymode = model.a_paymode_id.Split(',');
                for (int k = 0; k < zfdt.Rows.Count; k++)
                {
                    bool check = true;
                    for (int i = 0; i < a_paymode.Length; i++)
                    {
                        if (zfdt.Rows[k]["p_id"].ToString() == a_paymode[i])
                        {
                            zf += "&nbsp;&nbsp;<input type=\"checkbox\" name=\"zflx\" class=\"inputChck\" id=paytype_"+ zfdt.Rows[k]["p_id"] + "  data-stat="+ zfdt.Rows[k]["p_islocked"] + "   value=" + zfdt.Rows[k]["p_id"] + " checked=\"checked\" />&nbsp;" + zfdt.Rows[k]["p_name"];
                            check = false;
                            break;
                        }
                    }
                    if (check)
                    {
                        zf += "&nbsp;&nbsp;<input type=\"checkbox\" name=\"zflx\" class=\"inputChck\" id=paytype_" + zfdt.Rows[k]["p_id"] + "  data-stat=" + zfdt.Rows[k]["p_islocked"] + "    value=" + zfdt.Rows[k]["p_id"] + " />&nbsp;" + zfdt.Rows[k]["p_name"];
                    }
                }
                #endregion
                #region========获取应用类型在用信息======
                JMP.BLL.jmp_apptype yybll = new JMP.BLL.jmp_apptype();
                string where = "  t_id in (select  DISTINCT(t_topid) from jmp_apptype where t_topid in( select t_id from jmp_apptype where t_topid='0'   )) and t_state='1' order by t_sort desc";
                DataTable yydt = yybll.GetList(where).Tables[0];//获取应用类型在用信息
                JMP.MDL.jmp_apptype models = new JMP.MDL.jmp_apptype();
                int t_topid = 0;
                if (model.a_apptype_id > 0)
                {
                    models = yybll.GetModel(model.a_apptype_id);//查询单条信息
                    t_topid = models.t_topid;
                }
                string yyid = "";
                for (int j = 0; j < yydt.Rows.Count; j++)
                {
                    yyid = "yy" + yydt.Rows[j]["t_id"].ToString();
                    if (t_topid > 0)
                    {
                        if (Int32.Parse(yydt.Rows[j]["t_id"].ToString()) == models.t_topid)
                        {
                            szmrdj = yyid;
                            yy += "<input type=\"button\" id='" + yyid + "' name=\"yyname\" onclick=\"xzyylx(this.id,0)\" class=\"xzinput\" value=" + yydt.Rows[j]["t_name"] + "  />";
                        }
                        else
                        {
                            yy += "<input type=\"button\" id='" + yyid + "' name=\"yyname\" onclick=\"xzyylx(this.id,0)\" class=\"inpuwxz\" value=" + yydt.Rows[j]["t_name"] + "  />";
                        }
                    }
                    else
                    {
                        yy += "<input type=\"button\" id='" + yyid + "' name=\"yyname\" onclick=\"xzyylx(this.id,0)\" class=\"inpuwxz\" value=" + yydt.Rows[j]["t_name"] + "  />";
                    }
                }

                #endregion

                #region 根据应用子类型获取风险等级
                JMP.BLL.jmp_risklevelallocation ribll = new JMP.BLL.jmp_risklevelallocation();
                List<JMP.MDL.jmp_risklevelallocation> rilist = new List<JMP.MDL.jmp_risklevelallocation>();
                rilist = ribll.SelectAppType(model.a_apptype_id);
                ViewBag.rilist = rilist;
                #endregion
            }
            ViewBag.model = model;
            ViewBag.glpt = glpt;
            ViewBag.zf = zf;
            ViewBag.yy = yy;
            ViewBag.szmrdj = szmrdj;
            return View();
        }
        /// <summary>
        /// 批量修改应用状态
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdateState()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            int state = string.IsNullOrEmpty(Request["state"]) ? 0 : Int32.Parse(Request["state"].ToString());
            string str = Request["ids"];
            string xgzfc = "";//组装说明
            string tsmsg = "";//提示
            JMP.BLL.jmp_app bll = new JMP.BLL.jmp_app();
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

                Logger.OperateLog("应用一键启用或禁用", xgzfc);

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
        /// <summary>
        /// 冻结或解冻应用
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdateStateDt()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            JMP.MDL.jmp_app model = new JMP.MDL.jmp_app();//应用实体类
            JMP.BLL.jmp_app bll = new JMP.BLL.jmp_app();
            int a_id = string.IsNullOrEmpty(Request["a_id"]) ? 0 : Int32.Parse(Request["a_id"].ToString());
            model = bll.GetModel(a_id);
            model.a_state = string.IsNullOrEmpty(Request["state"]) ? 0 : Int32.Parse(Request["state"].ToString());
            string xgzfc = "";//组装说明
            string tsmsg = "";//提示
            if (bll.Update(model))
            {
                if (model.a_state == 1)
                {
                    xgzfc = "解冻ID为" + a_id;
                    tsmsg = "解冻成功";
                }
                else
                {
                    tsmsg = "冻结成功";
                    xgzfc = "冻结ID为：" + a_id;
                }

                Logger.OperateLog("应用冻结或解冻", xgzfc);

                retJson = new { success = 1, msg = tsmsg };
            }
            else
            {
                retJson = new { success = 0, msg = "操作失败" };
            }
            return Json(retJson);
        }
        /// <summary>
        /// 查询开发者用户用于选择
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult UserList()
        {
            List<JMP.MDL.jmp_user> list = new List<JMP.MDL.jmp_user>();
            JMP.BLL.jmp_user bll = new JMP.BLL.jmp_user();
            #region 初始化
            //获取请求参数
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量
            string type = string.IsNullOrEmpty(Request["stype"]) ? "5" : Request["stype"];//查询条件类型
            string sea_name = string.IsNullOrEmpty(Request["skeys"]) ? "" : Request["skeys"];//查询条件值
            string category = string.IsNullOrEmpty(Request["scategory"]) ? "" : Request["scategory"];//认证类型
            int px = string.IsNullOrEmpty(Request["s_sort"]) ? 0 : Int32.Parse(Request["s_sort"]);//排序
            //获取用户列表
            string where = " where 1=1 and u_auditstate='1' and u_state='1' ";
            if (!string.IsNullOrEmpty(type.ToString()))
            {
                if (!string.IsNullOrEmpty(sea_name))
                {
                    switch (type)
                    {
                        case "0":
                            where += string.Format(" and u_email like '%{0}%'", sea_name);
                            break;
                        case "1":
                            where += string.Format(" and u_phone like '%{0}%'", sea_name);
                            break;
                        case "3":
                            where += string.Format(" and u_idnumber like '%{0}%'", sea_name);
                            break;
                        case "6":
                            where += string.Format(" and u_blicensenumber like '%{0}%'", sea_name);
                            break;
                        case "5":
                            where += string.Format(" and u_realname like '%{0}%'", sea_name);
                            break;
                    }
                }
            }
            if (!string.IsNullOrEmpty(category))
            {
                where += string.Format(" and u_category={0}", category);
            }
            string Order = " order by u_id " + (px == 0 ? "" : " desc ") + " ";
            string query = "select * from jmp_user" + where;
            list = bll.GetLists(query, Order, pageIndexs, PageSize, out pageCount);
            //返回
            ViewBag.CurrPage = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.PageCount = pageCount;
            ViewBag.stype = type;
            ViewBag.skeys = sea_name;
            ViewBag.scategory = category;
            ViewBag.s_sort = px;
            ViewBag.list = list;
            #endregion
            return View();
        }
        #endregion

        #region 审核
        /// <summary>
        /// 审核应用
        /// </summary>
        /// <param name="a_id"></param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult AppAuditing(int a_id)
        {

            JMP.BLL.jmp_app bll = new JMP.BLL.jmp_app();
            JMP.MDL.jmp_app model = new JMP.MDL.jmp_app();
            string glpt = "";
            string zf = "";
            string szmrdj = "";
            string yy = "";

            if (a_id > 0)
            {
                model = bll.SelectId(a_id);

                #region 根据应用子类型获取风险等级
                JMP.BLL.jmp_risklevelallocation ribll = new JMP.BLL.jmp_risklevelallocation();
                List<JMP.MDL.jmp_risklevelallocation> rilist = new List<JMP.MDL.jmp_risklevelallocation>();
                rilist = ribll.SelectAppType(model.a_apptype_id);
                ViewBag.rilist = rilist;
                #endregion
            }
            ViewBag.model = model;
            ViewBag.glpt = glpt;
            ViewBag.zf = zf;
            ViewBag.yy = yy;
            ViewBag.szmrdj = szmrdj;
            return View();

        }

        /// <summary>
        /// 修改应用
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdateAppAuditing()
        {
            int id = string.IsNullOrEmpty(Request["aid"]) ? 0 : int.Parse(Request["aid"]);
            int u_auditstate = string.IsNullOrEmpty(Request["a_auditstate"]) ? 0 : int.Parse(Request["a_auditstate"]);
            int a_rid = string.IsNullOrEmpty(Request["a_rid"]) ? 0 : int.Parse(Request["a_rid"]);
            string name = UserInfo.UserName;


            JMP.BLL.jmp_app bll = new JMP.BLL.jmp_app();
            bool flag = bll.Update_auditstate(id, u_auditstate, a_rid, name);

            if (flag)
            {
                string info = "审核应用状态（" + id + "）的状态为" + u_auditstate + "";
                Logger.OperateLog("审核应用状态", info);
            }

            return Json(new { success = flag ? 1 : 0, msg = flag ? "审核成功！" : "审核失败！" });
        }

        #endregion

        #region 应用类型管理
        /// <summary>
        /// 应用类型列表界面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult AppTypeList()
        {
            List<JMP.MDL.jmp_apptype> list = new List<JMP.MDL.jmp_apptype>();
            JMP.BLL.jmp_apptype bll = new JMP.BLL.jmp_apptype();
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            int SelectState = string.IsNullOrEmpty(Request["SelectState"]) ? -1 : Int32.Parse(Request["SelectState"]);//状态
            string type = string.IsNullOrEmpty(Request["type"]) ? "" : Request["type"];//查询条件选择
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            int yylx = string.IsNullOrEmpty(Request["yylx"]) ? -1 : Int32.Parse(Request["yylx"].ToString());//所属应用类型
            list = bll.SelectList(yylx, sea_name, type, SelectState, searchDesc, pageIndexs, PageSize, out pageCount);
            ViewBag.searchDesc = searchDesc;
            ViewBag.SelectState = SelectState;
            ViewBag.type = type;
            ViewBag.sea_name = sea_name;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            ViewBag.yylx = yylx;
            string locUrl = "";
            bool getUidT = bll_limit.GetLocUserLimitVoids("/APP/PlUpdateState", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//本地管理员一键启用
            if (getUidT)
            {
                locUrl += "<li onclick=\"javascript:Updatestate(1)\"><i class='fa fa-check-square-o'></i>一键启用</li>";
                locUrl += "<li onclick=\"javascript:Updatestate(0);\"><i class='fa fa-check-square-o'></i>一键禁用</li>";
            }
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/APP/InsertOrUpdateAddType", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加管理员
            if (getlocuserAdd)
            {
                locUrl += "<li onclick=\"AddAppType()\"><i class='fa fa-plus'></i>添加应用类型</li>";
            }
            ViewBag.locUrl = locUrl;
            List<JMP.MDL.jmp_apptype> listtype = new List<JMP.MDL.jmp_apptype>();
            DataTable dt = bll.GetList(" 1=1 and t_topid='0' ").Tables[0];
            listtype = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_apptype>(dt);
            ViewBag.listtype = listtype;
            return View();
        }
        /// <summary>
        /// 添加或修改应用类型界面
        /// </summary>
        /// <returns></returns>
        public ActionResult AppTypeAdd()
        {
            JMP.BLL.jmp_apptype bll = new JMP.BLL.jmp_apptype();
            List<JMP.MDL.jmp_apptype> list = new List<JMP.MDL.jmp_apptype>();
            int t_id = string.IsNullOrEmpty(Request["t_id"]) ? 0 : Int32.Parse(Request["t_id"]);
            JMP.MDL.jmp_apptype model = new JMP.MDL.jmp_apptype();
            if (t_id > 0)
            {
                model = bll.GetModel(t_id);
            }
            ViewBag.model = model;
            DataTable dt = bll.GetList(" 1=1 and t_topid='0' ").Tables[0];
            list = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_apptype>(dt);
            ViewBag.list = list;
            return View();
        }
        /// <summary>
        /// 批量启用或禁用
        /// </summary>
        /// <returns></returns>
        public JsonResult PlUpdateState()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            JMP.BLL.jmp_apptype bll = new JMP.BLL.jmp_apptype();
            int state = string.IsNullOrEmpty(Request["state"]) ? 0 : Int32.Parse(Request["state"].ToString());
            string str = Request["ids"];
            string xgzfc = "";
            string tssm = "";//提示说明
            if (str.CompareTo("On") > 0)
            {
                str = str.Substring(3);
            }
            if (bll.UpdateLocUserState(str, state))
            {
                if (state == 1)
                {
                    xgzfc = "一键启用ID为：" + str;
                    tssm = "启用成功";
                }
                else
                {
                    xgzfc = "一键禁用ID为：" + str;
                    tssm = "禁用成功";
                }

                Logger.OperateLog("应用类型一键启用或禁用", xgzfc);
                retJson = new { success = 1, msg = tssm };
            }
            else
            {
                if (state == 1)
                {
                    tssm = "启用失败";
                }
                else
                {
                    tssm = "禁用失败";
                }
                retJson = new { success = 0, msg = tssm };
            }
            return Json(retJson);
        }
        /// <summary>
        /// 添加或修改应用类型
        /// </summary>
        /// <returns></returns>
        public JsonResult InsertOrUpdateAddType(JMP.MDL.jmp_apptype mode)
        {
            object retJson = new { success = 0, msg = "操作失败" };
            JMP.BLL.jmp_apptype bll = new JMP.BLL.jmp_apptype();
            if (mode.t_id > 0)
            {
                #region 修改应用类型
                JMP.MDL.jmp_apptype mo = new JMP.MDL.jmp_apptype();
                mo = bll.GetModel(mode.t_id);
                var mocole = mo.Clone();
                mo.t_name = mode.t_name;
                mo.t_sort = mode.t_sort;
                mo.t_topid = mode.t_topid;
                mo.t_namecj = mode.t_namecj;
               //mode.t_state = mo.t_state;
                //string xgzfc = "";
                if (bll.Update(mo))
                {
                    Logger.ModifyLog("修改应用类型", mocole, mode);

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
                #region 添加应用类型
                mode.t_state = 1;
                int cg = bll.Add(mode);
                if (cg > 0)
                {
                    Logger.CreateLog("添加应用类型", mode);

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
        #endregion


        #region 应用费率设置
        public ActionResult AppKl()
        {
            int appid = string.IsNullOrEmpty(Request["appid"]) ? 0 : Int32.Parse(Request["appid"]);
            List<JMP.MDL.jmp_apprate> list = new List<JMP.MDL.jmp_apprate>();
            JMP.BLL.jmp_apprate bll = new jmp_apprate();
            if (appid > 0)
            {
                list = bll.SelectListAppid(appid);
            }
            ViewBag.list = list;
            ViewBag.appid = appid;
            return View();
        }


        /// <summary>
        /// 应用手续费设置
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult Inserkl()
        {
            object retJson = new { success = 0, msg = "操作失败！" };
            string str = string.IsNullOrEmpty(Request["str"]) ? "" : Request["str"];
            int appid = string.IsNullOrEmpty(Request["appid"]) ? 0 : Int32.Parse(Request["appid"]);
            JMP.BLL.jmp_apprate bll = new jmp_apprate();
            if (appid > 0 && !string.IsNullOrEmpty(str))
            {
                string sql = " delete from jmp_apprate where  r_appid=" + appid;
                string[] sxf = str.Split('|');
                string[] lis = new string[sxf.Length + 1];
                lis[0] = sql;
                int a = 1;
                for (int i = 0; i < sxf.Length; i++)
                {
                    string[] bl = sxf[i].Split(',');
                    lis[a] = " insert into jmp_apprate(r_appid,r_paymodeid,r_proportion,r_state,r_time,r_name) values(" + appid + "," + bl[0] + "," + bl[1] + ",0,GETDATE(),'" + UserInfo.UserName + "') ";
                    sql += " insert into jmp_apprate(r_appid,r_paymodeid,r_proportion,r_state,r_time,r_name) values(" + appid + "," + bl[0] + "," + bl[1] + ",0,GETDATE(),'" + UserInfo.UserName + "'); ";
                    a = a + 1;
                }
                int cg = bll.InserSxF(lis);
                if (cg > 0)
                {
                    retJson = new { success = 1, msg = "设置成功！" };

                    Logger.OperateLog("通道费率设置扣量比例", "操作数据：" + sql);
                }
                else
                {
                    retJson = new { success = 0, msg = "设置失败！" };
                }

            }
            return Json(retJson);
        }
        #endregion
    }
}
