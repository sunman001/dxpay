using System;
using System.Configuration;
using System.Web.Mvc;
using DxPay.Bp.Util.Logger;
using JMP.BLL;
using JMP.TOOL;
using System.Data;

namespace DxPay.Bp.Controllers
{
    public class LoginController : Controller
    {
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;

        #region 登录逻辑
        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = false)]
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
        [LoginCheckFilter(IsCheck = false)]
        public JsonResult UserLogin(string userName, string userPwd, string valCode)
        {
            object result = new { mag = "操作失败！", status = "0" };
            var isDebug = ConfigurationManager.AppSettings["IsDebug"];
            if (!string.IsNullOrEmpty(isDebug) && isDebug == "true")
            {
                result = DoLogin(userName, userPwd);
                return Json(result);
            }

            result = string.Equals(valCode, Session["ValidateCode"].ToString(), StringComparison.CurrentCultureIgnoreCase) ? DoLogin(userName, userPwd) : new { msg = "验证码错误！", success = "2" };
            return Json(result);
        }
        private object DoLogin(string userName, string userPwd)
        {
            object result;
            var bllLimit = new jmp_limit();
            var bll = new CoBusinessPersonnel();
            var model = bll.GetModel(userName);
            //加密用户登录密码
            var jmPwd = DESEncrypt.Encrypt(userPwd).ToLower();
            if (model != null)
            {
                if(model.State==1)
                {
                    result = new { msg = "您的账号已经冻结无法登录！", success = "2" };
                    return result;
                }
                if (model.LoginName == userName && model.Password.ToLower() == jmPwd)
                {
                    UserInfo.UserId = model.Id;
                    UserInfo.UserName = model.DisplayName;
                    UserInfo.UserNo = model.LoginName;
                    UserInfo.UserRoleId = model.RoleId;
                    var dtLimit = bllLimit.GetBusinessLimitSession(model.Id, model.RoleId);
                    if (dtLimit.Rows.Count > 0)
                    {
                        Session["dtSession"] = dtLimit;
                        model.LoginCount += 1;
                        model.LogintTime = DateTime.Now;
                        bll.Update(model);
                        var log = string.Format("用户({0})于{1}登录系统。", UserInfo.UserName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        Logger.OperateLog("登录日志", log);
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
                result = new { msg = "用户名或密码错误！", success = "2" };
            
            }
               
            return result;
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
                var bllLimit = new jmp_limit();
                var bll = new CoBusinessPersonnel();
                var model = bll.GetModel(u_name);
                //加密用户登录密码
                
                if (model != null)
                {
                    if (model.State == 1)
                    {
                        result = new { mag = "您的账号已经冻结无法登录！", status = "2" };
                    }
                    else if (model.LoginName == u_name && model.Password.ToLower() == u_pwd.ToLower())
                    {
                        UserInfo.UserId = model.Id;
                        UserInfo.UserName = model.DisplayName;
                        UserInfo.UserNo = model.LoginName;
                        UserInfo.UserRoleId = model.RoleId;
                        var dtLimit = bllLimit.GetBusinessLimitSession(model.Id, model.RoleId);
                        if (dtLimit.Rows.Count > 0)
                        {
                            Session["dtSession"] = dtLimit;
                            model.LoginCount += 1;
                            model.LogintTime = DateTime.Now;
                            bll.Update(model);
                            var log = string.Format("管理员({0})用{1}在{2}登录系统。", admname, UserInfo.UserName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            Logger.OperateLog("登录日志", log);
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
                    result = new { msg = "用户名或密码错误！", success = "2" };

                }
            }
            ViewBag.message = result;
            return View();
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = false)]
        public ActionResult GetValidateCode(int height)
        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.GetRandomCode(4);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateImageTwo(code, height);
            return File(bytes, @"image/jpeg");
        }
        #endregion
        [LoginCheckFilterAttribute(IsCheck = false)]
        public ActionResult Error()
        {
            return View();
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
    }
}