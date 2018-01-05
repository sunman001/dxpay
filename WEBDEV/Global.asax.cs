using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.WebPages;
using WEBDEV.Models.Message;
using WEBDEV.Util;

namespace WEBDEV
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static TextMessageSendHistory TextMessageSendHistoryInstance;
        protected void Application_Start()
        {
            DisplayModeProvider.Instance.Modes.Insert(0, new CustomMobileDisplayMode());
            AreaRegistration.RegisterAllAreas();
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InitTextMessageSendHistory();
        }

        private void InitTextMessageSendHistory()
        {
            TextMessageSendHistoryInstance = new TextMessageSendHistory();
        }
    }
}