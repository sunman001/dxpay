/************聚米支付平台__财务管理************/
//描述：开发者前端框架逻辑处理
//功能：开发者前端框架逻辑处理
//开发者：谭玉科
//开发时间: 2016.04.22
/************聚米支付平台__财务管理************/
using JMP.TOOL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace WEBDEV.Controllers
{
    /// <summary>
    /// 类名：HomeController
    /// 功能：开发者前端框架逻辑
    /// 详细：开发者前端框架逻辑处理
    /// 修改日期：2016.05.25
    /// </summary>
    public class HomeController : Controller
    {
        #region 登录逻辑
        /// <summary>
        /// 登陆页面
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false)]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 后台用户登录
        /// </summary>
        /// <param name="u_name">用户名</param>
        /// <param name="u_pwd">密码</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false)]
        public JsonResult UserLogin(string u_name, string u_pwd, string code)
        {
            object result = new { msg = "操作失败！", status = "0" };

            JMP.BLL.jmp_limit bll_limit = new JMP.BLL.jmp_limit();
            JMP.BLL.jmp_user bll = new JMP.BLL.jmp_user();
            JMP.MDL.jmp_user model = new JMP.MDL.jmp_user();

            var isDebug = ConfigurationManager.AppSettings["IsDebug"];
            if (!string.IsNullOrEmpty(isDebug) && isDebug == "true")
            {
                string yzcode = Session["ValidateCode"].ToString();
                if (yzcode != code)
                {
                    result = new { msg = "验证码错误！", success = "2" };
                    return Json(result);
                }
            }

            model = bll.GetModel(u_name);

            //加密用户登录密码
            string jm_pwd = DESEncrypt.Encrypt(u_pwd);
            if (model != null && model.u_state == 1)
            {

                if ((model.u_email == u_name || model.u_phone == u_name) && model.u_password == jm_pwd)
                {
                    UserInfo.UserId = model.u_id;
                    UserInfo.UserName = model.u_realname;
                    UserInfo.UserNo = model.u_email;
                    UserInfo.UserRoleId = model.u_role_id;
                    UserInfo.auditstate = model.u_auditstate.ToString();
                    DataTable dtLimit = bll_limit.GetAppUserLimitSession(model.u_id, model.u_role_id);
                    if (dtLimit.Rows.Count > 0)
                    {
                        Session["dtSession"] = dtLimit;
                        model.u_count += 1;

                        string log = string.Format("开发者{0}于{1}登录聚米支付平台。", UserInfo.UserNo, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        AddLocLog.AddUserLog(UserInfo.UserId, 2, RequestHelper.GetClientIp(), "用户" + UserInfo.UserName + "登录。", log);
                        result = new { msg = "登录成功！", success = "1" };
                        return Json(result);
                    }
                    else
                    {
                        result = new { msg = "权限不足！", success = "2" };
                    }
                }
                else
                {
                    result = new { msg = "用户名或密码错误！", success = "2" };
                }
            }
            else
            {
                if (model == null)
                {
                    result = new { msg = "用户名或密码错误！", success = "2" };
                }
                else if (model.u_state != 1)
                {
                    result = new { msg = "该账号已冻结！", success = "2" };
                }
            }
            return Json(result);
        }
        [LoginCheckFilterAttribute(IsCheck = false)]
        public ActionResult UserLoginbyadm(string qs)
        {
            object result = "";
            string ms = JMP.TOOL.DESEncrypt.Decrypt(qs);
            string u_name = ms.Split(';')[0].ToString();
            string u_pwd = ms.Split(';')[1].ToString();
            string admname = ms.Split(';')[2].ToString();
            string admtime = ms.Split(';')[3].ToString();
            if (Convert.ToDateTime(admtime).AddMinutes(5) < System.DateTime.Now)
            {
                result = new { msg = "登录超时，后台密码失效，请从新刷新后台用户列表！", success = "2" };
            }
            else
            {
                JMP.BLL.jmp_limit bll_limit = new JMP.BLL.jmp_limit();
                JMP.BLL.jmp_user bll = new JMP.BLL.jmp_user();
                JMP.MDL.jmp_user model = bll.GetModel(u_name);
                //加密用户登录密码
                string jm_pwd = u_pwd;
                if (model != null && model.u_state == 1)
                {
                    //审核通过才能进入系统
                    if (model.u_auditstate == 1)
                    {
                        if ((model.u_email == u_name || model.u_phone == u_name) && model.u_password == jm_pwd)
                        {
                            UserInfo.UserId = model.u_id;
                            UserInfo.UserName = model.u_realname;
                            UserInfo.UserNo = model.u_email;
                            UserInfo.UserRoleId = model.u_role_id;
                            DataTable dtLimit = bll_limit.GetAppUserLimitSession(model.u_id, model.u_role_id);
                            if (dtLimit.Rows.Count > 0)
                            {
                                Session["dtSession"] = dtLimit;
                                if (admname != "0")
                                {
                                    string log = string.Format(admname + "从后台登录。", UserInfo.UserNo, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                    AddLocLog.AddUserLog(UserInfo.UserId, 2, RequestHelper.GetClientIp(), admname + "从后台登录。", log);

                                }
                                result = new { msg = "登录成功！", success = "1" };

                            }
                            else
                            {
                                result = new { msg = "权限不足！", success = "2" };
                            }
                        }
                        else
                        {
                            result = new { msg = "用户名或密码错误！", success = "2" };
                        }
                    }
                    else
                    {
                        result = new { msg = "该账号还未审核通过！", success = "3", uname = JMP.TOOL.DESEncrypt.Encrypt(u_name, "email") };
                    }
                }
                else
                {
                    if (model == null)
                    {
                        result = new { msg = "用户名或密码错误！", success = "2" };
                    }
                    else if (model.u_state != 1)
                    {
                        result = new { msg = "该账号已冻结！", success = "2" };
                    }
                }
            }
            ViewBag.message = result;
            return View();
        }

        [LoginCheckFilterAttribute(IsCheck = false)]
        public ActionResult UserLoginbygw(string qs)
        {
            object result = "";
            string ms = JMP.TOOL.DESEncrypt.Decrypt(qs);
            string u_name = ms.Split(';')[0].ToString();
            string u_pwd = ms.Split(';')[1].ToString();
            string admname = ms.Split(';')[2].ToString();
            string admtime = ms.Split(';')[3].ToString();
            if (Convert.ToDateTime(admtime).AddMinutes(5) < System.DateTime.Now)
            {
                result = new { msg = "登录超时，后台密码失效，请从新刷新后台用户列表！", success = "2" };
            }
            else
            {
                JMP.BLL.jmp_limit bll_limit = new JMP.BLL.jmp_limit();
                JMP.BLL.jmp_user bll = new JMP.BLL.jmp_user();
                JMP.MDL.jmp_user model = bll.GetModel(u_name);
                //加密用户登录密码
                string jm_pwd = u_pwd;
                if (model != null && model.u_state == 1)
                {
                      if ((model.u_email == u_name || model.u_phone == u_name) && model.u_password == jm_pwd)
                        {
                            UserInfo.UserId = model.u_id;
                            UserInfo.UserName = model.u_realname;
                            UserInfo.UserNo = model.u_email;
                            UserInfo.UserRoleId = model.u_role_id;
                            DataTable dtLimit = bll_limit.GetAppUserLimitSession(model.u_id, model.u_role_id);
                            if (dtLimit.Rows.Count > 0)
                            {
                                Session["dtSession"] = dtLimit;
                                if (admname != "0")
                                {
                                    string log = string.Format(admname + "从官网登录。", UserInfo.UserNo, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                    AddLocLog.AddUserLog(UserInfo.UserId, 2, RequestHelper.GetClientIp(), admname + "从官网登录。", log);

                                }
                                result = new { msg = "登录成功！", success = "1" };

                            }
                            else
                            {
                                result = new { msg = "权限不足！", success = "2" };
                            }
                        }
                        else
                        {
                            result = new { msg = "用户名或密码错误！", success = "2" };
                        }
                    
                   
                }
                else
                {
                    if (model == null)
                    {
                        result = new { msg = "用户名或密码错误！", success = "2" };
                    }
                    else if (model.u_state != 1)
                    {
                        result = new { msg = "该账号已冻结！", success = "2" };
                    }
                }
            }
            ViewBag.message = result;
            return View();
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false)]
        public ActionResult GetValidateCode(int height)
        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.GetRandomCode(4);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateImageTwo(code, height);
            return File(bytes, @"image/jpeg");
        }
        #endregion

        #region 首页逻辑
        /// <summary>
        /// 首页主框架
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult Default()
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

            int r_id = UserInfo.UserRid;

            string menuStr = GetMenStr(u_ids, r_id);
            ViewBag.UserName = UserInfo.UserNo;
            ViewBag.MsgCount = sm_bll.GetUserMsgCount(u_ids);
            ViewBag.MenuTopStr = menuStr;
            ViewBag.QQ = ConfigurationManager.AppSettings["linkqq"];
            ViewBag.Tel = ConfigurationManager.AppSettings["linkphone"];

            //每日应用汇总
            JMP.BLL.jmp_appcount bllapp = new JMP.BLL.jmp_appcount();
            JMP.MDL.jmp_appcount modelapp = new JMP.MDL.jmp_appcount();
            //开发者每日结算详情
            JMP.BLL.CoSettlementDeveloperAppDetails cobll = new JMP.BLL.CoSettlementDeveloperAppDetails();
            JMP.MDL.CoSettlementDeveloperAppDetails comodel_yesterday = new JMP.MDL.CoSettlementDeveloperAppDetails();
            JMP.MDL.CoSettlementDeveloperAppDetails comodel_month = new JMP.MDL.CoSettlementDeveloperAppDetails();
            JMP.MDL.CoSettlementDeveloperAppDetails comodel_preceding_month = new JMP.MDL.CoSettlementDeveloperAppDetails();

            int u_id = UserInfo.UserId;
            //今天
            string u_time = DateTime.Now.ToString("yyyy-MM-dd");
            //昨天
            string u_time_yesterday = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            //本月
            string u_time_month = DateTime.Now.ToString("yyyy-MM");
            //上月
            string u_time_preceding_month = DateTime.Now.AddMonths(-1).ToString("yyyy-MM");

            //根据日期查询交易金额和笔数（今天）
            modelapp = bllapp.DataAppcountAdy(u_time, u_id, 0);


            //根据不同日期统计查询(昨天)
            comodel_yesterday = cobll.GetModelKFZ_total(u_id, u_time_yesterday, 0);
            //根据不同日期统计查询(本月)
            comodel_month = cobll.GetModelKFZ_total(u_id, u_time_month, 1);
            //根据不同日期统计查询(上月)
            comodel_preceding_month = cobll.GetModelKFZ_total(u_id, u_time_preceding_month, 1);

            //流水及收入金额
            ViewBag.comodel_yesterday = comodel_yesterday;
            ViewBag.comodel_month = comodel_month;
            ViewBag.comodel_preceding_month = comodel_preceding_month;

            ViewBag.AppCount = modelapp;

            return View();
        }

        public ActionResult Index()
        {
            //每日应用汇总
            JMP.BLL.jmp_appcount bll = new JMP.BLL.jmp_appcount();
            JMP.MDL.jmp_appcount model = new JMP.MDL.jmp_appcount();
            JMP.MDL.jmp_user sm_model = new JMP.MDL.jmp_user();
            //开发者每日结算详情
            JMP.BLL.CoSettlementDeveloperAppDetails cobll = new JMP.BLL.CoSettlementDeveloperAppDetails();
            JMP.MDL.CoSettlementDeveloperAppDetails comodel_yesterday = new JMP.MDL.CoSettlementDeveloperAppDetails();
            JMP.MDL.CoSettlementDeveloperAppDetails comodel_month = new JMP.MDL.CoSettlementDeveloperAppDetails();
            JMP.MDL.CoSettlementDeveloperAppDetails comodel_preceding_month = new JMP.MDL.CoSettlementDeveloperAppDetails();

            int u_id = UserInfo.UserId;
            JMP.BLL.jmp_user sm_bll = new JMP.BLL.jmp_user();
            sm_model = sm_bll.GetModel(u_id);
            ViewBag.FrozenMoney = sm_model.FrozenMoney.ToString("f0"); 
            //今天
            string u_time = DateTime.Now.ToString("yyyy-MM-dd");
            //昨天
            string u_time_yesterday = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            //本月
            string u_time_month = DateTime.Now.ToString("yyyy-MM");
            //上月
            string u_time_preceding_month = DateTime.Now.AddMonths(-1).ToString("yyyy-MM");

            //根据日期查询交易金额和笔数（今天）
            model = bll.DataAppcountAdy(u_time, u_id, 0);


            //根据不同日期统计查询(昨天)
            comodel_yesterday = cobll.GetModelKFZ_total(u_id, u_time_yesterday, 0);
            //根据不同日期统计查询(本月)
            comodel_month = cobll.GetModelKFZ_total(u_id, u_time_month, 1);
            //根据不同日期统计查询(上月)
            comodel_preceding_month = cobll.GetModelKFZ_total(u_id, u_time_preceding_month, 1);

            //流水及收入金额
            ViewBag.comodel_yesterday = comodel_yesterday;
            ViewBag.comodel_month = comodel_month;
            ViewBag.comodel_preceding_month = comodel_preceding_month;

            ViewBag.AppCount = model;
            return View();
        }
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="roleid">角色id</param>
        /// <returns></returns>
        private string GetMenStr(int userid, int roleid)
        {
            JMP.BLL.jmp_limit bll = new JMP.BLL.jmp_limit();
            DataTable dt = bll.GetAppUserLimit(userid, roleid);
            string menustr = "";
            Session["LimitDt"] = dt;
            DataRow[] t_dr = dt.Select("l_topid=0", "l_sort desc");
            for (int i = 0; i < t_dr.Length; i++)
            {
                DataRow dr = t_dr[i];
                menustr += string.Format("<li id=\"topmenu_{0}\"><a>{1}</a><span class=\"arrow arrow-up\"></span></li>"
                    , dr["l_id"].ToString(), dr["l_name"].ToString());
            }
            return menustr;
        }

        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <param name="topid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public JsonResult GetSonTitle(string topid)
        {
            DataTable dt = (DataTable)Session["LimitDt"];
            string temp = BuildMenuStr(topid, ref dt, "");
            return Json(new { success = 1, msg = temp });
        }

        /// <summary>
        /// 获取左边导航菜单
        /// </summary>
        /// <param name="topid">父级id</param>
        /// <param name="tab">导航菜单DataTable</param>
        /// <param name="type">类型（是否递归调用）</param>
        /// <returns></returns>
        public string BuildMenuStr(string topid, ref DataTable tab, string type = "")
        {
            string build = "";
            DataRow[] t_dr = tab.Select("l_topid=" + topid + "", "l_sort desc");

            if (string.IsNullOrEmpty(type))
            {
                for (int i = 0; i < t_dr.Length; i++)
                {
                    DataRow dr = t_dr[i];

                    DataRow[] o_dr = tab.Select("l_topid=" + dr["l_id"].ToString(), "l_sort desc");
                    if (o_dr.Length > 0)
                    {
                        build += string.Format("<div class=\"box_header box_list_h\" onclick=\"OpenOrFoldMenu(this)\">{0}<span class=\"arrow arrow-up\"></span></div>"
                            , dr["l_name"].ToString());
                        build += BuildMenuStr(dr["l_id"].ToString(), ref tab, "1");
                    }
                    else
                    {
                        build += string.Format("<div class=\"box_header\"><a onclick=\"SecObjMenu(this,'{0}')\">{1}</a></div>", dr["l_url"].ToString(), dr["l_name"].ToString());
                    }
                }
            }
            else
            {
                build += "<ul class=\"list_menu menu_expand\">";
                for (int i = 0; i < t_dr.Length; i++)
                {
                    string temp = !string.IsNullOrEmpty(t_dr[i]["l_url"].ToString()) ? t_dr[i]["l_url"].ToString() : "";
                    string t_url = temp.IndexOf(",") > -1 ? temp.Remove(temp.IndexOf(",")) : temp;
                    build += string.Format("<li><a onclick=\"ObjMenu(this,'{0}')\">{1}</a></li>", t_url, t_dr[i]["l_name"].ToString());
                }
                build += "</ul>";
            }
            return build;
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false)]
        public JsonResult LoginOut()
        {
            Session.Remove("u_id");
            Session.Remove("u_role_id");
            Session.Remove("u_login_name");
            Session.Remove("u_real_name");
            Session.Remove("u_role_name");
            return Json(new { success = 1, gourl = "/Home/Login" });
        }

        /// <summary>
        /// 获取未读消息数
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false)]
        public JsonResult MsgCount()
        {
            JMP.BLL.jmp_user bll = new JMP.BLL.jmp_user();
            int userid = UserInfo.UserId;
            int msgCount = bll.GetUserMsgCount(userid);
            return Json(new { success = msgCount > 0 ? 1 : 0, mess = msgCount });
        }
        #endregion

        #region 错误处理
        /// <summary>
        /// 出错页面
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult Error()
        {
            return View();
        }
        #endregion

        #region 获取应用列表
        /// <summary>
        /// 获取用户应用列表
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult UserAppList()
        {
            int userId = UserInfo.UserId;
            int aid = string.IsNullOrEmpty(Request["aid"]) ? 0 : Int32.Parse(Request["aid"].ToString());
            JMP.BLL.jmp_app bll = new JMP.BLL.jmp_app();
            DataTable dt = bll.GetList("a_user_id=" + userId).Tables[0];
            List<JMP.MDL.jmp_app> list = new List<JMP.MDL.jmp_app>();

            if (dt.Rows.Count > 0)
            {
                list = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_app>(dt);
            }
            ViewBag.aid = aid;
            ViewBag.list = list;
            return PartialView();
        }
        #endregion

        #region 首页曲线图
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ContentResult HomeCharts(string days)
        {
            JMP.BLL.jmp_appcount bll = new JMP.BLL.jmp_appcount();

            
            //时间
            string s_time = "";

            if (days == "0")
            {
                s_time = DateTime.Now.ToString("yyyy-MM-dd");

            }
            else if (days == "1")
            {
                s_time = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            }
            else if (days == "2")
            {

                s_time = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");

            }

            //时间
            string startTime = s_time + " 00:00:00";
            string endTime = s_time + " 23:59:59";
            //前三日平均
            string startTimeAdy = DateTime.Now.AddDays(-4).ToString("yyyy-MM-dd") + " 00:00:00";
            string endTimeAdy = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59";

            //查询数据

            DataSet ds = bll.DataStatisticsAdy(UserInfo.UserId, startTime, endTime, startTimeAdy, endTimeAdy);

            return Content(JsonConvert.SerializeObject(new { Data = ds }), "application/json");
        }

        #endregion
    }
}
