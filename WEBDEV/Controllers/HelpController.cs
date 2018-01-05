using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TOOL;
using WEBDEV.Models.Help;
using WEBDEV.Util.Caching;

namespace WEBDEV.Controllers
{
    /// <summary>
    /// 帮助中心控制器
    /// </summary>
    public class HelpController : Controller
    {
        /// <summary>
        /// 默认页面(首页)
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = false, IsRole = false)]
        public ActionResult Index()
        {
            var model = CatalogCacheManager.FindAllEnabledRoots().Select(x => new CatalogForIndexViewModel
            {
                ArticleCount = x.Count,
                Description = x.Description,
                Icon = x.Icon,
                Id = x.ID,
                Name = x.ClassName,
                ParentId = x.ParentID,
                Sort = x.Sort
            }).ToList();
            return View(model);
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
        /// 子分类对应的列表页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = false, IsRole = false)]
        public ActionResult Catalog(int? id)
        {
            //id:主分类ID
            var helpArticleBll = new JMP.BLL.jmp_Help_Content();
            var articles = helpArticleBll.FindTopArticleListGroupBySubCatalog((int)id);
            var model = articles.Select(x => new ArticleWithTotalCountViewModel
            {
                Id = x.Id,
                Title = x.Title,
                MainCatalogId = x.MainCatalogId,
                SubCatalogId = x.SubCatalogId,
                SubCatalogArticleCount = x.SubCatalogArticleCount,
                SubCatalogName = x.SubCatalogName
            }).ToList();
            return View(model);
        }

        /// <summary>
        /// 文章列表页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = false, IsRole = false)]
        public ActionResult List(int? id)
        {
            //id:子分类ID
            var helpArticleBll = new JMP.BLL.jmp_Help_Content();
            var articles = helpArticleBll.FindListByCatalogId((int)id);
            var model = articles.Select(x => new ArticleListViewModel
            {
                Id = x.ID,
                Summary = x.Content.CleanHtml().CutStrLength(200),
                Title = x.Title,
                CreatedOn = x.CreateOn.ToString("yyyy年MM月dd日 HH:mm"),
                UpdatedOn = x.UpdateOn == null
                    ? ""
                    : Convert.ToDateTime(x.UpdateOn).ToString("yyyy年MM月dd日 HH:mm")
            }).ToList();
            return View(model);
        }

        /// <summary>
        /// 搜索页面
        /// </summary>
        /// <returns></returns>

        [LoginCheckFilter(IsCheck = false, IsRole = false)]
        public ActionResult Search()
        {
            var q = Request.QueryString["q"] ?? "";
            if (string.IsNullOrEmpty(q))
            {
                return View(new List<ArticleListViewModel>());
            }
            var helpArticleBll = new JMP.BLL.jmp_Help_Content();
            var keywords = q.Split(' ');
            var articles = helpArticleBll.Search(keywords);
            var model = articles.Select(x => new ArticleListViewModel
            {
                Id = x.ID,
                Summary = x.Content.CleanHtml().CutStrLength(200),
                Title = x.Title,
                CreatedOn = x.CreateOn.ToString("yyyy年MM月dd日 HH:mm"),
                UpdatedOn = x.UpdateOn == null
                    ? ""
                    : Convert.ToDateTime(x.UpdateOn).ToString("yyyy年MM月dd日 HH:mm")
            }).ToList();
            return View(model);
        }

        /// <summary>
        /// 侧栏热门问题局部视图
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        [LoginCheckFilter(IsCheck = false, IsRole = false)]
        public PartialViewResult SideBarHotQuestion()
        {
            var bll = new JMP.BLL.jmp_Help_Content();
            var hotQuestions = bll.FindHotQuestionList();
            var model = hotQuestions.Select(x => new SimpleArticleViewModel
            {
                Id = x.ID,
                Title = x.Title
            }).ToList();
            return PartialView("~/views/shared/sidebar_hot_question.cshtml", model);
        }

        /// <summary>
        /// 刷新帮助中心缓存的方法
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = false, IsRole = false)]
        public ActionResult RefreshCache()
        {
            CatalogCacheManager.Refresh();
            return new ContentResult { Content = "success" };
        }
    }
}
