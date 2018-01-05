using System;
using System.Web;
using System.Web.Mvc;
using JMP.TOOL;
using System.Text;
using System.Data;
using WEB.Util.CacheManager;
using WEB.Util.Logger;

namespace WEB
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LoginCheckFilterAttribute { IsCheck = true, IsRole = true });
            filters.Add(new MyHandleErrorAttribute());
            filters.Add(new VisitRecordAttribute());
        }
    }

    /// <summary>
    /// 处理异常
    /// </summary>
    public class MyHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {

            int u_id = UserInfo.Uid;
            string bcxx = filterContext.Exception.Message;//报错信息
            string bcController = filterContext.Controller.ToString();//报错控制器
            string bcff = filterContext.Exception.TargetSite.ToString();//报错方法
            string bcdx = filterContext.Exception.Source;//报错对象
            string bcwz = filterContext.Exception.StackTrace;//引发异常位置
            //AddLocLog.AddLog(u_id, 4, RequestHelper.GetClientIp(), "错误日志", "报错信息：" + bcxx + "，报错控制器：" + bcController + ",报错方法：" + bcff + ",报错对象：" + bcdx + ",报错位置：" + bcwz);//写入报错日志

            var message = "报错信息：" + bcxx + "，报错控制器：" + bcController + ",报错方法：" + bcff + ",报错对象：" + bcdx + ",报错位置：" + bcwz;
            GlobalErrorLogger.Log(message, filterContext.Exception.Source);

            filterContext.ExceptionHandled = true;//设置异常已处理
            // 跳转到登陆页
            //filterContext.Result = new RedirectResult("/Login/Error");
            //filterContext.RequestContext.HttpContext.Response.End();
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        success = 9997,
                        msg = "出错了！",
                        Redirect = "/Login/Error"
                    }
                };
            }
            else
            {
                filterContext.Result = new RedirectResult("/Login/Error");
            }
            base.OnException(filterContext);
        }
    }

    /// <summary>
    /// 验证用户是否登录
    /// </summary>
    public class LoginCheckFilterAttribute : ActionFilterAttribute
    {
        //表示是否检查登录
        public bool IsCheck { get; set; }
        public bool IsRole { get; set; }

        /// <summary>
        /// Action方法执行之前执行此方法(控制器)
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (IsCheck)
            {
                //校验用户是否已经登录
                if (!UserInfo.IsLogin)
                {
                    #region 验证session是否过期
                    if (filterContext.HttpContext.Request.IsAjaxRequest())//判断是否为ajax请求
                    {
                        filterContext.Result = new JsonResult
                        {
                            Data = new
                            {
                                success = 9999,
                                msg = "凭证已失效，请重新登陆！",
                                Redirect = "/Login/Index"
                            }
                        };
                    }
                    else
                    {
                        filterContext.HttpContext.Response.Write("<script language='javascript'>window.parent.location.href='/Login/Index';</script>");
                        filterContext.HttpContext.Response.End();
                        //var url = new UrlHelper(filterContext.RequestContext);
                        //var loginUrl = url.Content("/Login/Index");
                        //filterContext.HttpContext.Response.Redirect(loginUrl, true);
                        //filterContext.Result = new RedirectResult("/Login/Index");
                    }
                    #endregion
                    //IsCheck = false;
                }
                else//验证权限
                {
                    if (IsRole)
                    {
                        #region 用保存的Sessioin来判断权限
                        bool f = false;
                        string requestPath = RequestHelper.GetScriptName;
                        if (filterContext.HttpContext.Session["dtSession"] != null)
                        {
                            DataTable dtSess = (DataTable)filterContext.HttpContext.Session["dtSession"];
                            if (dtSess.Rows.Count > 0)
                            {
                                if (requestPath == "/Login/Default")
                                {
                                    f = true;
                                }
                                else
                                {

                                    int countNew = dtSess.Select("l_url like '%" + requestPath + "%'").Length;
                                    if (countNew > 0)
                                    {
                                        f = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            filterContext.HttpContext.Response.Write("<script language='javascript'>window.parent.location.href='/Login/Index';</script>");
                            filterContext.HttpContext.Response.End();
                            f = true;
                        }
                        if (!f)
                        {
                            if (filterContext.HttpContext.Request.IsAjaxRequest())//判断是否为ajax请求
                            {
                                filterContext.Result = new JsonResult
                                {
                                    Data = new
                                    {
                                        success = 9998,
                                        msg = "权限不足"
                                    }
                                };
                            }
                            else
                            {
                                StringBuilder strHTML = new StringBuilder();
                                strHTML.Append("<div style='text-align: center; line-height: 300px;'>");
                                strHTML.Append("<font style=\"font-size: 13;font-weight: bold; color: red;\">权限不足</font></div>");
                                HttpContext.Current.Response.Write(strHTML.ToString());
                                HttpContext.Current.Response.End();
                            }

                        }
                        #endregion
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }

    }

    /// <summary>
    /// 页面访问记录器
    /// </summary>
    public class VisitRecordAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 是否记录此页面的访问记录
        /// </summary>
        public bool IsRecord { get; set; }
        private static readonly ILog Logger = LogManager.VisitLogger;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (IsRecord)
            {
                var descriptor = filterContext.ActionDescriptor;
                var actionName = descriptor.ActionName;
                var controllerName = descriptor.ControllerDescriptor.ControllerName;
                var location = string.Format("/{0}/{1}", controllerName, actionName);
                //var permission=new JMP.BLL.jmp_limit().GetByUrlPath(location);
                var permission = PermissionCacheManager.Get(location);
                if (permission != null)
                {
                    location = string.Format("{0}[{1}]", permission.l_name, location);
                }
                var message = string.Format("用户({0})于[{1}]访问页面:{2}", UserInfo.UserName, DateTime.Now, location);
                Logger.Log("用户访问页面", message);
            }
            base.OnActionExecuting(filterContext);
        }
    }
}