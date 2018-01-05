using DxPay.Bp.Models;
using DxPay.Bp.Util.Logger;
using DxPay.Factory;
using DxPay.Services;
using JMP.MDL;
using JMP.TOOL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using TOOL.EnumUtil;
using TOOL.Extensions;

namespace DxPay.Bp.Controllers
{
    public class AppController : Controller
    {
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;
        private readonly IAppService _AppService;
        private readonly IUserService _UserService;
        public AppController()
        {
            _AppService = ServiceFactory.AppService;
            _UserService = ServiceFactory.UserService;
        }
        /// <summary>
        /// 应用列表
        /// </summary>
        /// <returns></returns>
        public ActionResult AppList()
        {
           
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            int SelectState = string.IsNullOrEmpty(Request["SelectState"]) ? 1 : Int32.Parse(Request["SelectState"]);//状态
            int auditstate = string.IsNullOrEmpty(Request["auditstate"]) ? -1 : Int32.Parse(Request["auditstate"]);//审核状态
            int type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);//查询条件选择
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            int platformid = string.IsNullOrEmpty(Request["platformid"]) ? 0 : Int32.Parse(Request["platformid"]);//关联平台
            int userid = UserInfo.UserId;
            var list = _AppService.FindPagedListBySql(userid, (int)Relationtype.Bp, "", platformid, auditstate, sea_name, type, SelectState, searchDesc,null, pageIndexs, PageSize);
            var gridModel = new DataSource<jmp_app>(list)
            {
                Data = list.Select(x => x).ToList()
            };
            ViewBag.searchDesc = searchDesc;
            ViewBag.SelectState = SelectState;
            ViewBag.type = type;
            ViewBag.sea_name = sea_name;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = gridModel.Pagination.TotalCount; 
            ViewBag.list = list;
            ViewBag.auditstate = auditstate;
            ViewBag.platformid = platformid;
            return View();
        }

        /// <summary>
        /// 添加应用
        /// </summary>
        /// <returns></returns>
        public ActionResult AppAdd()
        {
            #region =========获取应用平台在用信息=========
            JMP.BLL.jmp_platform bll = new JMP.BLL.jmp_platform();
            DataTable dt = bll.GetList(" 1=1 and p_state='1' ").Tables[0];//获取应用平台在用信息 
            List<JMP.MDL.jmp_platform> yypt = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_platform>(dt);
            ViewBag.glptdt = yypt;
            #endregion
            #region========获取应用类型在用信息======
            JMP.BLL.jmp_apptype yybll = new JMP.BLL.jmp_apptype();
            string where = "  t_id in (select  DISTINCT(t_topid) from jmp_apptype where t_topid in( select t_id from jmp_apptype where t_topid='0'   )) and t_state='1' order by t_sort desc";
            DataTable yydt = yybll.GetList(where).Tables[0];//获取应用类型在用信息
            List<JMP.MDL.jmp_apptype> yylist = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_apptype>(yydt);
            ViewBag.yylist = yylist;
            #endregion
            int userid = UserInfo.UserId;
            var userlist =  _UserService.FindListBySql("relation_type=1 and relation_person_id='" + userid + "' and u_state=1", "");
            ViewBag.userlist = userlist;
            int appid = string.IsNullOrEmpty(Request["appid"]) ? 0 : Int32.Parse(Request["appid"]);

            #region 获取所有支付方式

            JMP.BLL.jmp_paymode zfbll = new JMP.BLL.jmp_paymode();
            DataTable zfdt = new DataTable();
            List<JMP.MDL.jmp_paymode> zflist = new List<JMP.MDL.jmp_paymode>();
            zfdt = zfbll.GetList(" 1=1 and p_state='1' ").Tables[0];//获取支付类型在用信息
            zflist = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_paymode>(zfdt);
            ViewBag.appid = appid;
            ViewBag.list = zflist;
            #endregion

            return View();
        }

        /// <summary>
        /// 添加修改应用
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public JsonResult InsertUpdateApp(JMP.MDL.jmp_app mod)
        {
            object retJson = new { success = 0, msg = "操作失败" };
            if (mod.a_apptype_id == 0)
            {
                retJson = new { success = 0, msg = "请选择应用类型" };
            }
            else
            {
                if (mod.a_id > 0)
                {
                    #region 修改应用
                    string rzsm = "";
                  //  JMP.MDL.jmp_app modapp = new JMP.MDL.jmp_app();
                  
                    var modapp = _AppService.FindById(mod.a_id);
                    var modclone = modapp.Clone();
                    modapp.a_name = mod.a_name;//应用名称
                    modapp.a_platform_id = mod.a_platform_id;//关联平台ID
                    modapp.a_paymode_id = mod.a_paymode_id;//关联支付类型ID
                    modapp.a_apptype_id = mod.a_apptype_id;//关联应用类型ID
                    modapp.a_notifyurl = mod.a_notifyurl;//回掉地址
                    modapp.a_user_id = mod.a_user_id;//开发者ID
                    modapp.a_showurl = mod.a_showurl;//同步地址
                    modapp.a_appurl = mod.a_appurl;//应用审核地址
                    modapp.a_appsynopsis = mod.a_appsynopsis;//应用简介
                    if (_AppService.Update(modapp))
                    {
                        Logger.ModifyLog("修改应用", modclone, mod);
                        retJson = new { success = mod.a_id, msg = "修改成功" };
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
                    mod.a_rid = 0;
                    mod.a_auditor = "";
                    mod.a_state = 1;
                    mod.a_auditstate = 0;
                    mod.a_key = "";
                    mod.a_secretkey = "";
                    mod.a_time = DateTime.Now;
                    int cg = _AppService.Insert(mod);
                   // int cg = bll.Add(mod);
                    if (cg > 0)
                    {
                        mod.a_key = DESEncrypt.Encrypt(mod.a_user_id + ";" + cg + ";" + DateTime.Now.ToString("yyyyMMddssmmfff"));

                        mod.a_secretkey = DESEncrypt.Encrypt(cg + ";" + mod.a_key + ";" + DateTime.Now.ToString("yyyyMMddssmmfff"));
                        mod.a_id = cg;
                        if (_AppService.Update(mod))
                        {

                            Logger.CreateLog("新增应用", mod);
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
            return Json(retJson);

        }


        /// <summary>
        /// 修改应用界面
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult UpdateApp()
        {
            int appid = string.IsNullOrEmpty(Request["appid"]) ? 0 : Int32.Parse(Request["appid"]);
           // JMP.BLL.jmp_app appbll = new JMP.BLL.jmp_app();
           // JMP.MDL.jmp_app model = new JMP.MDL.jmp_app();

            string yy = "";

            if (appid > 0)
            {
               var model =_AppService.FindById(appid);
                ViewBag.model = model;
                //支付方式
                string[] zffs = model.a_paymode_id.Split(',');
                ViewBag.zffs = zffs;
                #region =========获取应用平台在用信息=========
                JMP.BLL.jmp_platform bll = new JMP.BLL.jmp_platform();
                DataTable dt = bll.GetList(" 1=1 and p_state='1' ").Tables[0];//获取应用平台在用信息 
                List<JMP.MDL.jmp_platform> yypt = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_platform>(dt);
                ViewBag.glptdt = yypt;
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

                if (model.a_auditstate == 1)
                {
                    yy += "<select id='xzyylx' disabled ='disabled' > ";
                }
                else
                {
                    yy += "<select id='xzyylx' onclick='xzyylx()'> ";
                }

                for (int j = 0; j < yydt.Rows.Count; j++)
                {
                    yyid = yydt.Rows[j]["t_id"].ToString();
                    if (t_topid > 0)
                    {
                        if (Int32.Parse(yydt.Rows[j]["t_id"].ToString()) == models.t_topid)
                        {
                            yy += "<option value='" + yyid + "' selected=selected >" + yydt.Rows[j]["t_name"] + "</option>";
                        }
                        else
                        {
                            yy += "<option value='" + yyid + "' >" + yydt.Rows[j]["t_name"] + "</option>";
                        }
                    }
                    else
                    {
                        yy += "<option value='" + yyid + "' >" + yydt.Rows[j]["t_name"] + "</option>";
                    }
                }

                yy += "</select>";
                #endregion
            }
            int userid = UserInfo.UserId;
            var userlist = _UserService.FindListBySql("relation_type='"+(int)Relationtype.Bp+"' and relation_person_id='" + userid + "' and u_state=1", "");
            ViewBag.userlist = userlist;

            #region 获取所有支付方式

            JMP.BLL.jmp_paymode zfbll = new JMP.BLL.jmp_paymode();

            DataTable zfdt = new DataTable();
            List<JMP.MDL.jmp_paymode> zflist = new List<JMP.MDL.jmp_paymode>();
            zfdt = zfbll.GetList(" 1=1 and p_state='1' ").Tables[0];//获取支付类型在用信息
            zflist = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_paymode>(zfdt);
            ViewBag.appid = appid;
            ViewBag.list = zflist;
            #endregion

            ViewBag.yy = yy;
          
            return View();
        }

        /// <summary>
        /// 删除应用
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public JsonResult DeleteApp()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            JMP.MDL.jmp_app model = new JMP.MDL.jmp_app();//应用实体类
            JMP.BLL.jmp_app bll = new JMP.BLL.jmp_app();
            int a_id = string.IsNullOrEmpty(Request["a_id"]) ? 0 : Int32.Parse(Request["a_id"].ToString());
            if (a_id > 0)
            {
                model = bll.GetModel(a_id);
                model.a_state = -1;
                if (bll.Update(model))
                {
                    Logger.OperateLog("删除应用", "删除应用ID为'" + a_id + "'");
                    retJson = new { success = 1, msg = "操作成功" };
                }
                else
                {
                    retJson = new { success = 0, msg = "操作失败" };
                }
            }
            return Json(retJson);
        }

        /// <summary>
        /// 根据主应用id查询子类应用
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
       public string SelectApp()
        {
            string yyid = string.IsNullOrEmpty(Request["id"].ToString()) ? "0" : Request["id"].ToString();
            int a_apptype_id = string.IsNullOrEmpty(Request["a_apptype_id"]) ? 0 : Int32.Parse(Request["a_apptype_id"]);
            int paymodeid = string.IsNullOrEmpty(Request["paymodeid"]) ? 0 : Int32.Parse(Request["paymodeid"]);

            JMP.BLL.jmp_apptype bll = new JMP.BLL.jmp_apptype();
            DataTable dt = bll.GetList(" 1=1 and t_topid='" + yyid.Replace("yy", "").Trim() + "' and t_state='1' order by t_sort desc  ").Tables[0];
            // string yyzl = "<select id='zlyy'><option value = '0'> --请选择-- </option>";
            string yyzl = "<option value = '0'> --请选择-- </option>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Int32.Parse(dt.Rows[i]["t_id"].ToString()) == a_apptype_id)
                {
                    yyzl += " <option value='" + dt.Rows[i]["t_id"] + "' selected=selected >" + dt.Rows[i]["t_name"] + "</option>";
                }
                else
                {
                    yyzl += " <option value='" + dt.Rows[i]["t_id"] + "' >" + dt.Rows[i]["t_name"] + "</option>";
                }
            }
            return yyzl;
        }

        /// <summary>
        /// 下载资料
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult Appinfo()
        {
            int appid = string.IsNullOrEmpty(Request["appid"]) ? 0 : Int32.Parse(Request["appid"]);
            JMP.BLL.jmp_app bll = new JMP.BLL.jmp_app();
            JMP.MDL.jmp_app modapp = new JMP.MDL.jmp_app();
            if (appid > 0)
            {
                modapp = bll.SelectAppId(appid);
            }
            string payfs = SelectPay(modapp.a_paymode_id);

            string[] pay = payfs.Split(',');
            ViewBag.pay = pay;
            ViewBag.modapp = modapp;
            string helpUrl = System.Configuration.ConfigurationManager.AppSettings["helpUrl"].ToString();
            ViewBag.helpUrl = helpUrl;
            return View();
        }

        /// <summary>
        /// 根据应用配置选择支付id查询支付类型
        /// </summary>
        /// <param name="pid">支付类型id</param>
        /// <returns></returns>
        public static string SelectPay(string pid)
        {
            string paystr = "";
            if (!string.IsNullOrEmpty(pid.ToString()))
            {
                JMP.BLL.jmp_paymode zfbll = new JMP.BLL.jmp_paymode();
                DataTable zfdt = new DataTable();
                zfdt = zfbll.GetList(" 1=1 and p_state='1' and  p_id in(" + pid + ") ").Tables[0];//获取支付类型在用信息
                if (zfdt.Rows.Count > 0)
                {
                    for (int i = 0; i < zfdt.Rows.Count; i++)
                    {
                        paystr += zfdt.Rows[i]["p_name"] + ",";
                    }
                }
            }
            return paystr;
        }
    }

}