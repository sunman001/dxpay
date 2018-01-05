using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;
using WEB.Util;

namespace WEB
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //移动设备显示模式监测器
            DisplayModeProvider.Instance.Modes.Insert(0, new CustomMobileDisplayMode());

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}