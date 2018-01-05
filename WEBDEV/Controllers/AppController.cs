/************聚米支付平台__应用管理控制器************/
//描述：应用管理控制器
//功能：应用管理控制器
//开发者：秦际攀
//开发时间: 2016.05.03
/************聚米支付平台__应用管理控制器************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JMP.TOOL;
using System.Data;
using System.IO;
using System.Configuration;
using WEBDEV.Util.Logger;
using TOOL.Extensions;

namespace WEBDEV.Controllers
{
    /// <summary>
    /// 应用管理控制器
    /// </summary>
    public class AppController : Controller
    {
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;
        /// <summary>
        /// 应用列表
        /// </summary>
        /// <returns></returns>
        public ActionResult AppList()
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

            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量

            string searchname = string.IsNullOrEmpty(Request["searchname"]) ? "" : Request["searchname"];//应用名称
            ViewBag.searchname = searchname;
            int terrace = string.IsNullOrEmpty(Request["terrace"]) ? 0 : int.Parse(Request["terrace"]); //运行平台
            ViewBag.terrace = terrace;

            List<JMP.MDL.jmp_app> list = new List<JMP.MDL.jmp_app>();
            JMP.BLL.jmp_app bll = new JMP.BLL.jmp_app();
            list = bll.SelectUserList(UserInfo.UserId.ToString(), searchname, terrace, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;

            #region =========获取应用平台在用信息=========
            JMP.BLL.jmp_platform paybll = new JMP.BLL.jmp_platform();
            DataTable dt = paybll.GetList(" 1=1 and p_state='1' ").Tables[0];//获取应用平台在用信息 
            List<JMP.MDL.jmp_platform> yypt = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_platform>(dt);
            ViewBag.glptdt = yypt;
            #endregion

            return View();
        }
        /// <summary>
        /// 添加应用基本
        /// </summary>
        /// <returns></returns>
        public ActionResult AppAdd()
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
        /// 修改应用界面
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult UpdateApp()
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

            int appid = string.IsNullOrEmpty(Request["appid"]) ? 0 : Int32.Parse(Request["appid"]);
            JMP.BLL.jmp_app appbll = new JMP.BLL.jmp_app();
            JMP.MDL.jmp_app model = new JMP.MDL.jmp_app();

            string yy = "";

            if (appid > 0)
            {
                model = appbll.SelectId(appid);
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
            ViewBag.model = model;
            //支付方式
            string[] zffs = model.a_paymode_id.Split(',');
            ViewBag.zffs = zffs;
            return View();
        }

        /// <summary>
        /// 根据主应用id查询子类应用
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
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
            // yyzl += "</select>";
            return yyzl;
        }

        /// <summary>
        /// 添加或修改应用
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult InsertUpdateApp(JMP.MDL.jmp_app mod)
        {
            object retJson = new { success = 0, msg = "操作失败" };

            JMP.BLL.jmp_app bll = new JMP.BLL.jmp_app();

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
                    var modclone = modapp.Clone();

                    modapp.a_name = mod.a_name;//应用名称
                    modapp.a_platform_id = mod.a_platform_id;//关联平台ID
                    modapp.a_paymode_id = mod.a_paymode_id;//关联支付类型ID
                    modapp.a_apptype_id = mod.a_apptype_id;//关联应用类型ID
                    modapp.a_notifyurl = mod.a_notifyurl;//回掉地址
                    modapp.a_showurl = mod.a_showurl;//同步地址
                    modapp.a_appurl = mod.a_appurl;//应用审核地址
                    modapp.a_appsynopsis = mod.a_appsynopsis;//应用简介
                    //mod.a_auditstate = modapp.a_auditstate;//应用审核状态
                    //mod.a_key = modapp.a_key;
                    //mod.a_state = modapp.a_state;
                    //mod.a_secretkey = modapp.a_secretkey;
                    //mod.a_time = modapp.a_time;
                    //mod.a_user_id = modapp.a_user_id;
                    //mod.a_rid = modapp.a_rid;
                    if (bll.Update(modapp))
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
                    mod.a_user_id = UserInfo.UserId;
                    int cg = bll.Add(mod);
                    if (cg > 0)
                    {
                        AddLocLog.AddUserLog(UserInfo.UserId, 3, RequestHelper.GetClientIp(), "添加应用", "添加应用");

                        mod.a_key = DESEncrypt.Encrypt(mod.a_user_id + ";" + cg + ";" + DateTime.Now.ToString("yyyyMMddssmmfff"));

                        mod.a_secretkey = DESEncrypt.Encrypt(cg + ";" + mod.a_key + ";" + DateTime.Now.ToString("yyyyMMddssmmfff"));
                        mod.a_id = cg;
                        if (bll.Update(mod))
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
        /// 删除应用
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
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
                    // AddLocLog.AddUserLog(Int32.Parse(UserInfo.UserId), 3, RequestHelper.GetClientIp(), "删除应用", "删除应用，应用id为：" + model.a_id);
                    Logger.OperateLog("删除应用", "删除应用");
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


        /// <summary>
        /// 应用详情
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult down()
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
            return View();
        }
        /// <summary>
        /// 上传APP应用sdk
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult fileupload()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            string fileName = "file_upload";
            HttpPostedFile uploadFile = System.Web.HttpContext.Current.Request.Files[fileName];
            var context = HttpContext;
            string Name = context.Request.Files[0].FileName;
            FileInfo hzm = new FileInfo(Name);//获取后缀名
            //获取配置文件里的上传路径
            string uploadurl = ConfigurationManager.AppSettings["APPuploadurl"];
            if (context.Request.Files.Count > 0)
            {
                //HttpContext.Current.Request.FilePath;
                //System.Web.HttpContext.Current.Server.MapPath("~/upload/AppSdk")
                string strPath = System.Web.HttpContext.Current.Server.MapPath("~") + "\\" + uploadurl + "\\";
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);//在根目录下建立文件夹
                }
                string strName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + hzm.Extension;
                context.Request.Files[0].SaveAs(System.IO.Path.Combine(strPath, strName));
                string url = uploadurl + strName;
                retJson = new { success = 1, msg = url };
            }
            return Json(retJson);
        }
        /// <summary>
        /// 把上传应用sdk存入到数据库
        /// </summary>
        /// <param name="mo"></param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult InsertOrUpdate(JMP.MDL.jmp_appsdk mo)
        {
            object retJson = new { success = 0, msg = "操作失败" };
            if (mo.appid > 0)
            {
                JMP.BLL.jmp_appsdk bll = new JMP.BLL.jmp_appsdk();

                DataTable dt = bll.GetList(" appid='" + mo.appid + "' ").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    mo.id = Int32.Parse(dt.Rows[0]["id"].ToString());
                    mo.uptimes = DateTime.Now;
                    if (bll.Update(mo))
                    {
                        retJson = new { success = 1, msg = "上传成功！" };
                        AddLocLog.AddUserLog(UserInfo.UserId, 3, Request.UserHostAddress, "修改上传应用asdk", "文件名：" + mo.appurl + ",应用id编号：" + mo.appid);
                    }
                    else
                    {
                        retJson = new { success = 0, msg = "上传失败！" };
                    }
                }
                else
                {
                    mo.uptimes = DateTime.Now;
                    int cg = bll.Add(mo);
                    if (cg > 0)
                    {
                        retJson = new { success = 1, msg = "上传成功！" };
                        AddLocLog.AddUserLog(UserInfo.UserId, 3, Request.UserHostAddress, "上传应用asdk", "文件名：" + mo.appurl + ",应用id编号：" + mo.appid);
                    }
                    else
                    {
                        retJson = new { success = 0, msg = "上传失败！" };
                    }
                }
            }
            return Json(retJson);

        }
        /// <summary>
        /// 上传应用adk界面
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult Upload()
        {
            int appid = string.IsNullOrEmpty(Request["appid"]) ? 0 : Int32.Parse(Request["appid"]);
            ViewBag.appid = appid;
            JMP.BLL.jmp_appsdk bll = new JMP.BLL.jmp_appsdk();
            JMP.MDL.jmp_appsdk model = new JMP.MDL.jmp_appsdk();
            JMP.BLL.jmp_app bllapp = new JMP.BLL.jmp_app();
            JMP.MDL.jmp_app mo = new JMP.MDL.jmp_app();
            if (appid > 0)
            {
                model = bll.SelectModel(appid);
                mo = bllapp.GetModel(appid);
            }
            ViewBag.model = model == null ? new JMP.MDL.jmp_appsdk() : model;
            ViewBag.mo = mo == null ? new JMP.MDL.jmp_app() : mo;
            return View();
        }

        /// <summary>
        /// 收银台预览
        /// </summary>
        /// <returns></returns>
        public ActionResult PayBank()
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

            return View();
        }

    }
}
