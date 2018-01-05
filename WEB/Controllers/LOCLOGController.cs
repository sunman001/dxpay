/************聚米支付平台__后台管理日志模块************/
//描述：处理后台管理日志的功能
//功能：处理后台管理日志的功能
//开发者：陶涛
//开发时间: 2016.03.18
/************聚米支付平台__后台管理日志模块************/

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TOOL;
using TOOL.Sql;

namespace WEB.Controllers
{
    public class LoclogController : Controller
    {
        //
        // GET: /LOCLOG/
        readonly JMP.BLL.jmp_locuserlog _bllLocuserlog = new JMP.BLL.jmp_locuserlog();

        readonly JMP.BLL.CoOperationLog _bllRateLog = new JMP.BLL.CoOperationLog();
        readonly JMP.BLL.jmp_businessuserlog _bllBp = new JMP.BLL.jmp_businessuserlog();
        readonly JMP.BLL.jmp_agentuserlog _bllagent = new JMP.BLL.jmp_agentuserlog();

        [VisitRecord(IsRecord = true)]
        public ActionResult LogList()
        {
            #region 分页列表数据查询
            int pageCount;
            //var pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : int.Parse(Request["pageIndexs"]);//当前页
            //var pageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : int.Parse(Request["PageSize"]);//每页显示数量

            var pageIndexs = Request.HttpGetParaByName("pageIndexs", 1);
            var pageSize = Request.HttpGetParaByName("PageSize", x => Convert.ToInt32(x), 20);

            var whereBuidler = WhereBuilder.CreateContainer();
            var order = "order by l_id DESC";
            var types = Request["types"];
            var searchKey = Request["searchKey"];
            var sort = Request["sort"];
            var logtype = Request["logtype"];

            if (!string.IsNullOrEmpty(types))
            {
                if (!string.IsNullOrEmpty(searchKey))
                {
                    if (types == "1") //用户编号查询
                    {
                        whereBuidler.Append("l_user_id like '%" + searchKey + "%'");
                    }
                    else if (types == "2") //用户名称查询
                    {
                        whereBuidler.Append("u_loginname like '%" + searchKey + "%'");
                    }
                    else //IP地址查询
                    {
                        whereBuidler.Append("l_ip like '%" + searchKey + "%'");
                    }
                }
            }
            if (!string.IsNullOrEmpty(logtype))
            {
                if (logtype != "0")
                {
                    whereBuidler.Append("l_logtype_id = " + logtype);
                }
            }
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "0")
                {
                    order = "order by l_id";
                }
            }
            var @where = whereBuidler.ToWhereString();
            var sql = "SELECT L.*,U.u_loginname FROM jmp_locuserlog AS L LEFT JOIN jmp_locuser AS U ON l_user_id=u_id " + where;
            var list = _bllLocuserlog.SelectList(sql, order, pageIndexs, pageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = pageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            #endregion
            return View();
        }

        /// <summary>
        /// 运营平台数据库日志列表
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult DbLogList()
        {
            #region 分页列表数据查询
            int pageCount;
            //var pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : int.Parse(Request["pageIndexs"]);//当前页
            //var pageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : int.Parse(Request["PageSize"]);//每页显示数量
            //var types = Request["types"];
            //var searchKey = Request["searchKey"];
            //var sort = Request["sort"];

            var pageIndexs = Request.HttpGetParaByName("pageIndexs", 1);
            var pageSize = Request.HttpGetParaByName("PageSize", x=>Convert.ToInt32(x), 20);
            var types = Request.HttpGetParaByName("types", "");
            var searchKey = Request.HttpGetParaByName("searchKey", "");
            var sort = Request.HttpGetParaByName("sort", "");


            var whereBuidler = WhereBuilder.CreateContainer();
            var order = "order by l_id DESC";


            var logtype = 5;//数据库日志

            if (!string.IsNullOrEmpty(types))
            {
                if (!string.IsNullOrEmpty(searchKey))
                {
                    if (types == "1") //用户编号查询
                    {
                        whereBuidler.Append("u_id like '%" + searchKey + "%'");
                    }
                    else if (types == "2") //用户名称查询
                    {
                        whereBuidler.Append("u_loginname like '%" + searchKey + "%'");
                    }
                    else //IP地址查询
                    {
                        whereBuidler.Append("l_ip like '%" + searchKey + "%'");
                    }
                }
            }
            whereBuidler.Append("l_logtype_id = " + logtype);
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "0")
                {
                    order = "order by l_id";
                }
            }
            var @where = whereBuidler.ToWhereString();
            var sql = "SELECT L.*,U.u_loginname FROM jmp_locuserlog AS L LEFT JOIN jmp_locuser AS U ON l_user_id=u_id " + where;
            var list = _bllLocuserlog.SelectList(sql, order, pageIndexs, pageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = pageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            #endregion
            return View();
        }
        [VisitRecord(IsRecord = true)]
        public ActionResult LogOperList()
        {
            #region 分页列表数据查询
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            string where = " where  u_id=l_user_id ";
            string Order = "order by l_id desc";
            string types = Request["types"];
            string searchKey = Request["searchKey"];
            string sort = Request["sort"];
            string logtype = "3";

            if (!string.IsNullOrEmpty(types))
            {
                if (types == "1")//用户编号查询
                {
                    where += " and l_user_id like '%" + searchKey + "%'";
                }
                else if (types == "2")//用户名称查询
                {
                    where += " and u_loginname like '%" + searchKey + "%'";
                }
                else//IP地址查询
                {
                    where += " and l_ip like '%" + searchKey + "%'";
                }
            }
            if (!string.IsNullOrEmpty(logtype))
            {
                if (logtype != "0")
                {
                    where += " and l_logtype_id = " + logtype;
                }
            }
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "0")
                {
                    Order = "order by l_id";
                }
            }
            string sql = "select a.*,b.u_loginname from JMP_LOCUSERLOG as a,JMP_LOCUSER as b  " + where;
            List<JMP.MDL.jmp_locuserlog> list = new List<JMP.MDL.jmp_locuserlog>();
            list = _bllLocuserlog.SelectList(sql, Order, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            #endregion
            return View();
        }

        /// <summary>
        /// 日志类别：1 注册 2 登录 3 操作 4 错误日志
        /// </summary>
        /// <param name="l_logtype_id">1 注册 2 登录 3 操作 4  错误日志</param>
        /// <returns></returns>
        public string GetlLogtype(string l_logtype_id)
        {
            string str = "";
            if (l_logtype_id == "1")
            {
                str = "注册";
            }
            else if (l_logtype_id == "2")
            {
                str = "登录";
            }
            else if (l_logtype_id == "3")
            {
                str = "操作";
            }
            else if (l_logtype_id == "5")
            {
                str = "数据库错误日志";
            }
            else
            {
                str = "错误日志";
            }
            return str;
        }

        [VisitRecord(IsRecord = true)]
        public ActionResult RateLogList()
        {

            #region 分页列表数据查询
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            string where = " where 1=1";
            string Order = "order by id desc";
            string types = Request["types"];
            string searchKey = Request["searchKey"];
            string sort = Request["sort"];
            if (!string.IsNullOrEmpty(types))
            {
                if (types == "1")//用户编号查询
                {
                    where += " and CreatedById like '%" + searchKey + "%'";
                }
                else if (types == "2")//用户名称查询
                {
                    where += " and CreatedByName like '%" + searchKey + "%'";
                }
                else//IP地址查询
                {
                    where += " and IpAddress like '%" + searchKey + "%'";
                }
            }
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "0")
                {
                    Order = "order by id";
                }
            }
            string sql = "select * from  CoOperationLog " + where;
            List<JMP.MDL.CoOperationLog> list = new List<JMP.MDL.CoOperationLog>();
            list = _bllRateLog.SelectList(sql, Order, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            #endregion
            return View();
        }
        /// <summary>
        /// 商务平台日志记录
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult BpLogList()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            string where = " where 1=1";
            string Order = "order by l_id desc";
            string types = Request["types"];
            string searchKey = Request["searchKey"];
            string sort = Request["sort"];
            if (!string.IsNullOrEmpty(types))
            {
                if (types == "1")//用户编号查询
                {
                    where += " and l_user_id like '%" + searchKey + "%'";
                }
                else if (types == "2")//用户名称查询
                {
                    where += " and CreatedByName like '%" + searchKey + "%'";
                }
                else//IP地址查询
                {
                    where += " and l_ip like '%" + searchKey + "%'";
                }
            }
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "0")
                {
                    Order = "order by l_id";
                }
            }
            string sql = "select a.*,b.DisplayName as DisplayName  from [dbo].[jmp_businessuserlog] a left join  [dbo].[CoBusinessPersonnel] b on a.l_user_id=b.Id " + where;
            List<JMP.MDL.jmp_businessuserlog> list = new List<JMP.MDL.jmp_businessuserlog>();
            list = _bllBp.SelectList(sql, Order, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            return View();
        }

        /// <summary>
        /// 代理商平台日志记录
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult AgentLogList()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            string where = " where 1=1";
            string Order = "order by l_id desc";
            string types = Request["types"];
            string searchKey = Request["searchKey"];
            string sort = Request["sort"];
            if (!string.IsNullOrEmpty(types))
            {
                if (types == "1")//用户编号查询
                {
                    where += " and l_user_id like '%" + searchKey + "%'";
                }
                else if (types == "2")//用户名称查询
                {
                    where += " and CreatedByName like '%" + searchKey + "%'";
                }
                else//IP地址查询
                {
                    where += " and l_ip like '%" + searchKey + "%'";
                }
            }
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "0")
                {
                    Order = "order by l_id";
                }
            }
            string sql = "select a.* ,b.DisplayName from jmp_agentuserlog a left join CoAgent b on a.l_user_id = b.Id " + where;
            List<JMP.MDL.jmp_agentuserlog> list = new List<JMP.MDL.jmp_agentuserlog>();
            list = _bllagent.SelectList(sql, Order, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            return View();
        }

    }
}
