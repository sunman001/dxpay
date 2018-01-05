using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace JmPayApiServer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new DxExceptionAttribute());

            //config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //添加默认返回json格式数据
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }

    }
}
