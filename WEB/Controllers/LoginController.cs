/************聚米支付平台__业务逻辑************/
//描述：处理登录等业务逻辑
//功能：处理登录等业务逻辑
//开发者：谭玉科
//开发时间: 2016.03.14
/************聚米支付平台__业务逻辑************/
using JMP.BLL;
using JMP.TOOL;
using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Mvc;
using WEB.Util.Logger;

namespace WEB.Controllers
{
    /// <summary>
    /// 类名：LoginController
    /// 功能：
    /// 详细：
    /// 修改日期：2016.03.14
    /// </summary>
    public class LoginController : Controller
    {
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;
        #region 登录逻辑
        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false)]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="action">操作类型</param>
        /// <param name="userName">登录名</param>
        /// <param name="userPwd">用户密码</param>
        /// <param name="valCode">验证码</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false)]
        public JsonResult UserLogin(string action, string userName, string userPwd, string valCode)
        {
            object result = new { mag = "操作失败！", status = "0" };
            if (action != "login") return Json(result);
            var isDebug = ConfigurationManager.AppSettings["IsDebug"];
            if (!string.IsNullOrEmpty(isDebug) && isDebug == "true")
            {
                result = DoLogin(userName, userPwd);
                return Json(result);
            }

            result = string.Equals(valCode, Session["ValidateCode"].ToString(), StringComparison.CurrentCultureIgnoreCase) ? DoLogin(userName, userPwd) : new { mag = "验证码错误！", status = "3" };
            return Json(result);
        }

        private object DoLogin(string userName, string userPwd)
        {
            object result;
            var bllLimit = new jmp_limit();
            var bll = new jmp_locuser();
            var model = bll.GetModel(userName);
            //加密用户登录密码
            var jmPwd = DESEncrypt.Encrypt(userPwd).ToLower();
            if (model != null)
            {
                if (model.u_loginname == userName && model.u_pwd.ToLower() == jmPwd)
                {
                    UserInfo.UserId = model.u_id;
                    UserInfo.UserName = model.u_realname;
                    UserInfo.UserNo = model.u_loginname;
                    UserInfo.UserRoleId = model.u_role_id;
                    UserInfo.UserDept = model.u_department;
                    UserInfo.UserPostion = model.u_position;
                    var dtLimit = bllLimit.GetUserLimitSession(model.u_id, model.u_role_id);
                    if (dtLimit.Rows.Count > 0)
                    {
                        Session["dtSession"] = dtLimit;
                        model.u_count += 1;
                        bll.Update(model);
                        var log = string.Format("用户({0})于{1}登录后台系统。", UserInfo.UserName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        AddLocLog.AddLog(UserInfo.UserId, 2, RequestHelper.GetClientIp(), "用户" + UserInfo.UserName + "登录。", log);
                        result = new { mag = "登录成功！", status = "1" };
                    }
                    else
                    {
                        result = new { mag = "权限不足！", status = "4" };
                    }

                }
                else
                    result = new { mag = "用户名或密码错误！", status = "2" };
            }
            else
                result = new { mag = "用户名或密码错误！", status = "2" };
            return result;
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false)]
        public ActionResult GetValidateCode()
        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.GetRandomCode(4);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }
        #endregion
        #region 首页框架
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult Default()
        {
            string top_menu = string.Empty;
            string son_menu = string.Empty;

            int u_id = UserInfo.Uid;
            int r_id = UserInfo.Uid;
            top_menu = GetTopMenu(u_id, r_id, ref son_menu);
            string user_name = UserInfo.UserName;

            ViewBag.TopMenu = new HtmlString(top_menu);
            ViewBag.SonMenu = new HtmlString(son_menu);
            ViewBag.UserName = user_name;

            return View();
        }

        /// <summary>
        /// 查询最近五分钟订单数量
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public string SelectOrderList()
        {
            string str = "";
            JMP.BLL.jmp_order bll = new jmp_order();
            // string TableName = "jmp_order";
            string TableName = "jmp_order_" + (JMP.TOOL.WeekDateTime.GetWeekFirstDayMon(DateTime.Now)).ToString("yyyyMMdd");
            // 5分钟无新订单产生
            int ordernum = Convert.ToInt32(bll.GetOrderNum(DateTime.Now, TableName));
            //5分钟成功的订单
            int SuccessNum = Convert.ToInt32(bll.SelectCG(DateTime.Now, TableName));
            //if (ordernum < 1)
            //{
            //    TableName = "jmp_order_" + (JMP.TOOL.WeekDateTime.GetWeekFirstDayMon(DateTime.Now).AddDays(-7)).ToString("yyyyMMdd");
            //    ordernum = Convert.ToInt32(bll.GetOrderNum(DateTime.Now, TableName));
            //}
            //if (SuccessNum < 1)
            //{
            //    TableName = "jmp_order_" + (JMP.TOOL.WeekDateTime.GetWeekFirstDayMon(DateTime.Now).AddDays(-7)).ToString("yyyyMMdd");
            //    SuccessNum = Convert.ToInt32(bll.SelectCG(DateTime.Now, TableName));
            //}
            str = SuccessNum.ToString() + " /" + ordernum.ToString();
            return str;
        }
        /// <summary>
        /// 获取订单最后一次支付成功的时间
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public string SelectOrderTimes()
        {
            string str = "";
            JMP.BLL.jmp_order bll = new jmp_order();
            string TableName = "jmp_order";
            string times = "";
            //if (bll.SelectCgTimes(TableName) == null)
            //{
            //    TableName = "jmp_order_" + (JMP.TOOL.WeekDateTime.GetWeekFirstDayMon(DateTime.Now).AddDays(-7)).ToString("yyyyMMdd");
            //    times = bll.SelectCgTimes(TableName) == null ? "" : bll.SelectCgTimes(TableName).ToString();
            //}
            //else
            //{
            times = bll.SelectCgTimes(TableName) == null ? "" : bll.SelectCgTimes(TableName).ToString();
            // }
            str = string.IsNullOrEmpty(times) ? "无数据" : DateTime.Parse(times).ToString("yyyy-MM-dd HH:mm:ss");
            return str;
        }
        /// <summary>
        /// 获取顶级菜单
        /// </summary>
        /// <param name="uId">用户id</param>
        /// <param name="rId">权限id</param>
        /// <param name="son_menu">子级菜单</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        private string GetTopMenu(int uId, int rId, ref string son_menu)
        {
            //顶级菜单
            string topMenu = string.Empty;
            son_menu = "<ul><li id=\"menu_0\" class=\"\"><a href=\"javascript:\" onclick=\"TabObj(this, '交易走势图', '/REPORT/Index');\" ><span>交易走势图</span></a></li></ul>";
            jmp_limit bll = new jmp_limit();
            DataTable dt = bll.GetUserLimit(uId, rId, 0);
            string temp = "<li class=\"header-body-nav-li{0}\"><a href=\"javascript:\" {1}>{2}</a></li>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                string licss = i == 0 ? " selected" : "";
                string acss = !string.IsNullOrEmpty(dr["l_icon"].ToString()) ? "class=\"icon " + dr["l_icon"].ToString() + "\"" : "";
                string lname = !string.IsNullOrEmpty(dr["l_name"].ToString()) ? dr["l_name"].ToString() : "";
                topMenu += string.Format(temp, licss, acss, lname);
                int tid = int.Parse(dr["l_id"].ToString());
                son_menu += "<ul style=\"display:none\">" + GetSonMenu(uId, rId, tid) + "</ul>";
            }
            return topMenu;
        }

        /// <summary>
        /// 获取子级菜单
        /// </summary>
        /// <param name="uId">用户id</param>
        /// <param name="rId">权限id</param>
        /// <param name="topId">父级id</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        private string GetSonMenu(int uId, int rId, int topId)
        {
            string son_menu = string.Empty;
            jmp_limit bll = new jmp_limit();
            DataTable dt = bll.GetUserLimit(uId, rId, topId);
            string temp = "<li id=\"{0}\" {1}><a id=\"{2}\" href=\"javascript:\" onclick=\"TabObj(this,'{4}','{3}')\"><span>{4}</span></a></li>";
            string action = Request["action"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                string lid = !string.IsNullOrEmpty(dr["l_id"].ToString()) ? "menu_" + dr["l_id"].ToString() : "menu_" + i + 1;
                string aid = !string.IsNullOrEmpty(dr["l_id"].ToString()) ? "menu_" + dr["l_id"].ToString() + "_a" : "menu_" + i + 1 + "_a";
                string css = i == 0 ? "class=\"selected\"" : "";
                string url = !string.IsNullOrEmpty(dr["l_url"].ToString()) ? dr["l_url"].ToString() : "";
                string title = !string.IsNullOrEmpty(dr["l_name"].ToString()) ? dr["l_name"].ToString() : "";
                son_menu += string.Format(temp, lid, css, aid, url, title);
            }
            return son_menu;
        }



        /// <summary>
        /// 退出登录
        /// </summary>
        /// <param name="action">操作</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false)]
        public JsonResult LoginOut(string action)
        {
            Logger.OperateLog("退出登录", "退出登录");
            Session.Remove("u_id");
            Session.Remove("u_role_id");
            Session.Remove("u_login_name");

            return Json(new { success = true, gourl = "/Login/Index" });

        }
        #endregion
        #region 错误处理
        /// <summary>
        /// 错误提示页面
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false)]
        public ActionResult Error()
        {
            return View();
        }
        #endregion

        #region 修改密码

        [LoginCheckFilterAttribute(IsCheck = false)]
        public ActionResult UpdatePwd()
        {
            return View();
        }

        /// <summary>
        /// 修改密码方法
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false)]
        public JsonResult loginUpdatePwd()
        {
            JMP.BLL.jmp_locuser bll = new JMP.BLL.jmp_locuser();

            object result = new { success = 0, msg = "修改失败！" };


            string o_pwd = !string.IsNullOrEmpty(Request["upass"]) ? Request["upass"] : "";
            string n_pwd = !string.IsNullOrEmpty(Request["xpwd"]) ? Request["xpwd"] : "";

            JMP.MDL.jmp_locuser j_user = bll.GetModel(UserInfo.UserId);

            //判断是否修改了密码
            if (!string.IsNullOrEmpty(o_pwd))
            {
                string temp = DESEncrypt.Encrypt(o_pwd);
                if (temp == j_user.u_pwd)
                {
                    string u_password = DESEncrypt.Encrypt(n_pwd);

                    j_user.u_pwd = u_password;

                    bool flag = bll.Update(j_user);
                    result = new { success = 1, msg = "修改成功,请重新登录！" };
                }
                else
                {
                    result = new { success = 2, msg = "原密码输入错误！" };
                }
            }

            return Json(result);
        }

        #endregion
    }
}
