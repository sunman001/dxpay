using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;
using DxPay.Bp;
using DxPay.Agent.Util;

namespace DxPay.Agent
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //移动设备显示模式监测器
            DisplayModeProvider.Instance.Modes.Insert(0, new CustomMobileDisplayMode());

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
