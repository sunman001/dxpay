using DxPay.LogManager.LogManager.Platform.Administrator;
using JMP.TOOL;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace WEB.Controllers
{
    public class DemoController : Controller
    {
        //
        // GET: /Demo/
        [LoginCheckFilter(IsCheck = false, IsRole = false)]
        public ActionResult Index()
        {
            var logger = new AdministratorGlobalFactory().CreateLogger();
            var message = "报错信息：测试";
            logger.Log(UserInfo.Uid, message, RequestHelper.GetClientIp(), "location", "错误信息");
            return new ContentResult { Content = "success" };
        }

        [LoginCheckFilter(IsCheck = false, IsRole = false)]
        public JsonResult FindAllAction()
        {
            var asm = Assembly.GetAssembly(typeof(MvcApplication));

            var controlleractionlist = asm.GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                .Select(x => new { Controller = x.DeclaringType.Name, Action = x.Name, ReturnType = x.ReturnType.Name, Attributes = string.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))) })
                //.Where(x => x.ReturnType == "ActionResult" || x.ReturnType == "JsonResult")
                //.Select(x=>new{ x.ReturnType })
                //.Distinct()
                .OrderBy(x => x.Controller).ThenBy(x => x.Action)
                .ToList();
            return Json(controlleractionlist, JsonRequestBehavior.AllowGet);
        }
        [LoginCheckFilter(IsCheck = true, IsRole = false)]
        public ActionResult BuildIncreasedPermission(int lid)
        {
            var result = new ContentResult();
            var limitBll = new JMP.BLL.jmp_limit();
            var increasedPermissions = limitBll.GetModelList("l_id>=" + lid);
            var list = new List<string>();
            increasedPermissions.ForEach(x =>
            {
                list.Add(string.Format("INSERT INTO jmp_limit ([l_name], [l_topid], [l_url], [l_sort], [l_state], [l_icon], [l_type]) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", x.l_name, x.l_topid, x.l_url, x.l_sort, x.l_state, x.l_icon, x.l_type));
            });
            result.Content = string.Join(";\n", list);
            return result;
        }

        [LoginCheckFilter(IsCheck = true, IsRole = false)]
        public ActionResult Permission()
        {
            return View();
        }

    }
}
