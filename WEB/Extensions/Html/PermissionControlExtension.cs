using System.Web;
using System.Web.Mvc;
using WEB.Util.CacheManager;

namespace WEB.Extensions.Html
{
    public static class PermissionControlExtension
    {
        /// <summary>
        /// 按钮权限控制(如果有权限,则生成一个按钮)
        /// </summary>
        /// <param name="helper">HtmlHelper对象</param>
        /// <param name="buttonActionName">操作方法名称</param>
        /// <param name="buttonName">按钮名称</param>
        /// <param name="jsClickMethodName">javascript函数名称</param>
        /// <param name="iconCls">fa 图标样式类</param>
        /// <returns></returns>
        public static IHtmlString ButtonPermission(this HtmlHelper helper, string buttonActionName, string buttonName,
            string jsClickMethodName, string iconCls = "")
        {
            if (!RoleCacheManager.HasPermission(buttonActionName))
            {
                return null;
            }
            //如果有权限
            var html = string.Format("<li onclick=\"javascript:{0}\"><i class='fa{1}'></i>{2}</li>", jsClickMethodName, " " + iconCls, buttonName);
            return MvcHtmlString.Create(html);
        }
        /// <summary>
        /// 检查是否有权限
        /// </summary>
        /// <param name="helper">HtmlHelper对象</param>
        /// <param name="buttonActionName">操作方法名称</param>
        /// <returns></returns>
        public static bool ButtonHasPermission(this HtmlHelper helper, string buttonActionName)
        {
            return RoleCacheManager.HasPermission(buttonActionName);
        }

    }
}