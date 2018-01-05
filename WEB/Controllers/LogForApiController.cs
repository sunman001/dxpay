using System;
using System.Web.Mvc;
using TOOL;
using TOOL.Sql;

namespace WEB.Controllers
{
    public class LogForApiController : Controller
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
            //var pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            //var pageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            //var searchKey = Request["searchKey"];
            //var sort = Request["sort"];
            //var clientId = Request["clientId"];

            //当前页
            var pageIndexs = Request.HttpGetParaByName("pageIndexs", 1);
            //每页显示数量
            var pageSize = Request.HttpGetParaByName("PageSize",x=>Convert.ToInt32(x), 20);
            //搜索关键词
            var searchKey = Request.HttpGetParaByName("searchKey", "");
            //排序
            var s_type = Request.HttpGetParaByName("s_type", "");
            var sort = Request.HttpGetParaByName("sort",x=>x.ToString(), "");
            //平台ID
            var clientId = Request.HttpGetParaByName("clientId", "-1");

            var order = "order by Id DESC";

            var whereContainer = WhereBuilder.CreateContainer();

            if (!string.IsNullOrEmpty(searchKey) && !string.IsNullOrEmpty(s_type))
            {
                switch (s_type)
                {
                    case "0":
                        whereContainer.Append("id =" + searchKey +"");
                        break;
                    case "1":
                        whereContainer.Append("ClientName like '%" + searchKey + "%'");
                        break;
                    case "2":
                     whereContainer.Append("Summary like '%" + searchKey + "%'");
                     break;
                }
             
            }

            if (!string.IsNullOrEmpty(clientId))
            {
                if (clientId != "-1")
                {
                    whereContainer.Append("ClientId = " + clientId);
                }
            }
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "1")
                {
                    order = "order by Id DESC";
                }
                else
                {
                    order = "order by Id";
                }
            }
            var list = new JMP.BLL.LogForApi().SelectList(whereContainer.ToWhereString(), order, pageIndexs, pageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = pageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.stype = s_type;
            #endregion
            return View(list);
        }

    }
}
