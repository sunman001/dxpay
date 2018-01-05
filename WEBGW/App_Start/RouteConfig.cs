using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WEBGW
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.apk");// 加入此行z
            routes.MapRoute(
          name: "News",
          url: "Index/News/{id}.html",
          defaults: new { controller = "Index", action = "NewDetil", id = UrlParameter.Optional }
      );
            routes.MapRoute(
   name: "zixun",
   url: "Index/zixun/{id}.html",
   defaults: new { controller = "Index", action = "NewDetil", id = UrlParameter.Optional }
);


            routes.MapRoute(
                       name: "Action1Html", // action伪静态    
                       url: "{controller}/{action}.html",// 带有参数的 URL    
                       defaults: new { controller = "Index", action = "Index" }// 参数默认值    
                          );
            routes.MapRoute(
                            name: "IDHtml",// id伪静态    
                            url: "{controller}/{action}/{id}.html",// 带有参数的 URL    
                            defaults: new { controller = "Index", action = "Index", id = UrlParameter.Optional }// 参数默认值    
                            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Index", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
