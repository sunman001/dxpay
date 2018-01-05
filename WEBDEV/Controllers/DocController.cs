using System;
using System.Web.Mvc;
using WEBDEV.Models.Help;
using WEBDEV.Util.Caching;

namespace WEBDEV.Controllers
{
    /// <summary>
    /// 开发者中心
    /// </summary>
    public class DocController : Controller
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = false, IsRole = false)]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 帮助者中心概览页面
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = false, IsRole = false)]
        public ActionResult Overview()
        {
            return View();
        }

        /// <summary>
        /// 下载中心
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = false, IsRole = false)]
        public ActionResult Download()
        {
            return View();
        }

        /// <summary>
        /// 详情页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = false, IsRole = false)]
        public ActionResult Details(int? id)
        {
            var model = new ArticleDetailsViewModel();
            var helpArticleBll = new JMP.BLL.jmp_Help_Content();
            var article = helpArticleBll.GetModel((int)id);
            model.Id = article.ID;
            model.Content = article.Content;
            model.CreatedOn = article.CreateOn.ToString("yyyy年MM月dd日 HH:mm");
            model.Title = article.Title;
            model.UpdatedOn = article.UpdateOn == null
                ? ""
                : Convert.ToDateTime(article.UpdateOn).ToString("yyyy年MM月dd日 HH:mm");
            model.CreatedByUserName = article.CreateByName;
            model.UpdatedByUserName = article.UpdateByName;
            model.SubCatalogId = article.ClassId;

            var viewCount = article.Count + 1;
            helpArticleBll.ArticleViewed(article.ID, viewCount);
            return View(model);
        }

        /// <summary>
        /// 刷新开发者中心缓存
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = false, IsRole = false)]
        public ActionResult RefreshCache()
        {
            DocMenuCacheManager.Refresh();
            return new ContentResult { Content = "success" };
        }
    }
}
