using System;
using System.Web.Mvc;
using TOOL;
using TOOL.Sql;

namespace WEB.Controllers
{
    public class LogCoSettlementController : Controller
    {
        /// <summary>
        /// 接口服务日志列表
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = true, IsRole = false)]
        public ActionResult Index()
        {
            #region 分页列表数据查询
            int pageCount;

            //当前页
            var pageIndexs = Request.HttpGetParaByName("pageIndexs", 1);
            //每页显示数量
            var pageSize = Request.HttpGetParaByName("PageSize",x=>Convert.ToInt32(x), 20);
            //搜索关键词
            var searchKey = Request.HttpGetParaByName("searchKey", "");
            //排序
            var sort = Request.HttpGetParaByName("sort",x=>x.ToString(), "");
            //平台ID
            var typeId = Request.HttpGetParaByName("typeId", "100");

            var order = "order by Id DESC";

            var whereContainer = WhereBuilder.CreateContainer();

            if (!string.IsNullOrEmpty(searchKey))
            {
                whereContainer.Append("Message like '%" + searchKey + "%'");
            }

            if (!string.IsNullOrEmpty(typeId))
            {
                if (typeId != "100")
                {
                    whereContainer.Append("TypeId = " + typeId);
                }
            }
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "0")
                {
                    order = "order by Id";
                }
            }
            
            var list = new JMP.BLL.LogCoSettlement().SelectList(whereContainer.ToWhereString(), order, pageIndexs, pageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = pageSize;
            ViewBag.pageCount = pageCount;
            #endregion
            return View(list);
        }

    }
}
