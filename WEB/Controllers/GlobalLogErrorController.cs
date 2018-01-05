using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WEB.Controllers
{
    public class GlobalLogErrorController : Controller
    {
        [LoginCheckFilter(IsCheck = true,IsRole = false)]
        public ActionResult Index()
        {
            #region 分页列表数据查询
            var pageCount = 0;
            var pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            var pageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            var order = "order by Id desc";
            var searchKey = Request["searchKey"];
            var sort = Request["sort"];
            var clientId = Request["clientId"];

            var wheres = new List<string>();

            if (!string.IsNullOrEmpty(searchKey))
            {
                wheres.Add("Message like '%" + searchKey + "%'");
            }

            if (!string.IsNullOrEmpty(clientId))
            {
                if (clientId != "-1")
                {
                    wheres.Add("ClientId = " + clientId);
                }
            }
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "0")
                {
                    order = "order by Id ";
                }
                else
                {
                    order = "order by Id desc";
                }
            }
            var where = "";
            if (wheres.Count > 0)
            {
                where = " WHERE " + string.Join(" AND ", wheres);
            }
            var sql = "select a.* from DxGlobalLogError as a " + where + " ";
            var list = new JMP.BLL.DxGlobalLogError().SelectList(sql, order, pageIndexs, pageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = pageSize;
            ViewBag.pageCount = pageCount;
            #endregion
            return View(list);
        }

    }
}
