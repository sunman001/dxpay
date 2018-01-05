using System;
using System.Web.Mvc;

namespace WEBDEV.Util
{
    public static class MenuHelper
    {
        public static bool IsCurrentPath(this HtmlHelper html, string path,string cls = "active")
        {
            var viewContext = html.ViewContext;
            var isChildAction = viewContext.Controller.ControllerContext.IsChildAction;

            if (isChildAction)
                viewContext = html.ViewContext.ParentActionViewContext;

            var routeValues = viewContext.RouteData.Values;
            var currentAction = routeValues["action"].ToString();
            var currentController = routeValues["controller"].ToString();
            var currentPath = currentController + "/" + currentAction;
            currentPath = currentPath.ToLower();

            if (string.IsNullOrEmpty(path))
                return false;
            path = path.Trim('/').ToLower();
            return string.Equals(path,currentPath,StringComparison.InvariantCultureIgnoreCase);
            //var acceptedActions = actions.Trim().Split(',').Distinct().ToArray();
            //var acceptedControllers = controllers.Trim().Split(',').Distinct().ToArray();

            //return acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController)? cls : string.Empty;

        }
    }
}