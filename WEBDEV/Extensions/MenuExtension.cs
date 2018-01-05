using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WEBDEV.Util.Caching;

namespace WEBDEV.Extensions
{
    public static class MenuExtension
    {
        /// <summary>
        /// 生成帮助中心左侧菜单
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="viewContext">当前视图上下文</param>
        /// <returns></returns>
        public static IHtmlString CreateHelpMenu(this HtmlHelper helper, ViewContext viewContext)
        {
            var data = viewContext.RouteData;
            var id = data.Values["id"] ?? "0";
            var mainCatalogId = Convert.ToInt32(id);
            var cat = CatalogCacheManager.FindById(mainCatalogId);
            var isRoot = cat.ParentID == 0;
            if (!isRoot)
            {
                mainCatalogId = CatalogCacheManager.FindById(cat.ParentID).ID;
            }
            var parents = CatalogCacheManager.FindAllEnabledRoots();
            var sb = new StringBuilder();
            sb.AppendFormat("<ul class='list-unstyled'>");
            parents.ForEach(x =>
            {
                sb.AppendFormat("<li>");
                sb.AppendFormat("<a{0} href='/help/catalog/{1}'>{2}</a>", x.ID == mainCatalogId ? " class='active'" : "", x.ID, x.ClassName);
                sb.AppendFormat("</li>");
            });
            sb.AppendFormat("</ul>");
            return MvcHtmlString.Create(sb.ToString());
        }

        /// <summary>
        /// 生成帮助中心面包屑
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="viewContext">当前视图上下文</param>
        /// <returns></returns>
        public static IHtmlString CreateBreadCrumb(this HtmlHelper helper, ViewContext viewContext)
        {
            var data = viewContext.RouteData;
            var id = data.Values["id"] ?? "0";
            var mainCatalogId = Convert.ToInt32(id);
            var catalog = CatalogCacheManager.FindById(mainCatalogId);
            /*
             * <ul class="list-unstyled breadcrumb">
                   <li><a href="~/help/index">帮助中心</a></li>
                   <li><a href="~/help/index">开始使用</a></li>
                   <li>通道申请</li>
               </ul>
             */
            var sb = new StringBuilder();
            sb.AppendFormat("<ul class='list-unstyled breadcrumb'>");
            sb.AppendFormat("<li><a href='/help/index'>帮助中心</a></li>");
            var hasChild = catalog.ParentID > 0;
            if (!hasChild)
            {
                sb.AppendFormat("<li>{0}</li>", catalog.ClassName);
            }
            else
            {
                var parent = CatalogCacheManager.FindById(catalog.ParentID);
                sb.AppendFormat("<li><a href='/help/catalog/{0}'>{1}</a></li>", parent.ID, parent.ClassName);
                sb.AppendFormat("<li>{0}</li>", catalog.ClassName);
            }
            sb.AppendFormat("</ul>");
            return MvcHtmlString.Create(sb.ToString());
        }

        /// <summary>
        /// 生成帮助中心面包屑
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="catalogId">当前分类ID</param>
        /// <returns></returns>
        public static IHtmlString CreateDetailBreadCrumb(this HtmlHelper helper, int catalogId)
        {
            var catalog = CatalogCacheManager.FindById(catalogId);
            /*
             * <ul class="list-unstyled breadcrumb">
                   <li><a href="~/help/index">帮助中心</a></li>
                   <li><a href="~/help/index">开始使用</a></li>
                   <li>通道申请</li>
               </ul>
             */
            if (catalog == null)
            {
                return MvcHtmlString.Create("");
            }
            var sb = new StringBuilder();
            sb.AppendFormat("<ul class='list-unstyled breadcrumb'>");
            sb.AppendFormat("<li><a href='/help/index'>帮助中心</a></li>");
            var hasChild = catalog.ParentID > 0;
            if (!hasChild)
            {
                sb.AppendFormat("<li>{0}</li>", catalog.ClassName);
            }
            else
            {
                var parent = CatalogCacheManager.FindById(catalog.ParentID);
                sb.AppendFormat("<li><a href='/help/catalog/{0}'>{1}</a></li>", parent.ID, parent.ClassName);
                sb.AppendFormat("<li>{0}</li>", catalog.ClassName);
            }
            sb.AppendFormat("</ul>");
            return MvcHtmlString.Create(sb.ToString());
        }

        /// <summary>
        /// 开发者中心菜单
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="catalogId">当前分类ID</param>
        /// <param name="articleId">当前文章ID</param>
        /// <returns></returns>
        public static IHtmlString CreateDocMenu(this HtmlHelper helper, int catalogId,int articleId)
        {
            if (catalogId > 0)
            {
                var cat = DocMenuCacheManager.FindById(catalogId);
                var isRoot = cat.ParentID == 0;
                if (!isRoot)
                {
                    catalogId = DocMenuCacheManager.FindById(cat.ParentID).ID;
                }
            }
            var parents = DocMenuCacheManager.FindAllEnabledSecondChildList();
            var sb = new StringBuilder();
            sb.AppendFormat("<ul class='list-unstyled'>");
            sb.AppendFormat("<li class='title'><a{0} href='/doc/overview'>文档用途</a></li>",catalogId==0? " class='active'" : "");
            parents.ForEach(x =>
            {
                sb.AppendFormat("<li  class='title'>");
                sb.AppendFormat("<a{0}>{1}</a>", x.ID == catalogId ? " class='active'" : "",x.Title);
                //children
                var children = DocMenuCacheManager.FindListByClassId(x.ID);
                if (children.Count > 0)
                {
                    sb.AppendFormat("<ul class='list-unstyled'>");
                    foreach (var child in children)
                    {
                        sb.AppendFormat("<li class='ztitle'>");
                        sb.AppendFormat("<a{0} href='/doc/details/{1}'>{2}</a>", child.ArticleId == articleId ? " class='active'" : "", child.ArticleId, child.Title);
                        sb.AppendFormat("</li>");
                    }
                    sb.AppendFormat("</ul>");
                }
                sb.AppendFormat("</li>");
            });
            sb.AppendFormat("</ul>");
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}