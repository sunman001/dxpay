/************聚米支付平台__分页控件************/
//描述：分页的帮助类
//功能：分页处理
//开发者：谭玉科
//开发时间: 2016.03.14
/************聚米支付平台__分页控件************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace JMP.TOOL
{
    /// <summary>
    /// 类名：HtmlPage
    /// 功能：处理分页
    /// 详细：处理分页
    /// 修改日期：2016.03.14
    /// </summary>
    public class HtmlPage
    {
        /// <summary>
        /// 计算当页数
        /// </summary>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="pageSize">分页数量</param>
        /// <returns></returns>
        public static int pageIndex(int pageIndex = 1, int pageSize = 20)
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            pageSize = pageSize <= 0 ? 20 : pageSize;
            int pageIndexs = (pageIndex - 1) * pageSize + 1;
            return pageIndexs;
        }
        /// <summary>
        /// 计算要查询的数据页数
        /// </summary>
        /// <returns></returns>
        public static int pageSize(int pageIndex = 1, int pageSize = 20)
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            pageSize = pageSize <= 0 ? 20 : pageSize;
            int pageSizes = pageIndex * pageSize;
            return pageSizes;
        }

        /// <summary>
        /// 获取页号数据
        /// </summary>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="num">显示最大数目的一半</param>
        /// <returns></returns>
        private static IList<string> GetPageNumList(int pageIndex, int pageCount, int num = 5)
        {
            IList<string> pageNums = new List<string>();

            if (pageCount <= num * 2)
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    pageNums.Add(i.ToString());
                }
            }
            else
            {
                if (pageIndex - num < 2)
                {
                    int temp = num * 2;

                    for (int i = 1; i <= temp; i++)
                    {
                        pageNums.Add(i.ToString());
                    }

                    pageNums.Add("...");
                    pageNums.Add(pageCount.ToString());
                }
                else
                {
                    pageNums.Add("1");
                    pageNums.Add("...");
                    int start = pageIndex - num + 1;
                    int end = pageIndex + num - 1;

                    if (pageCount - end > 1)
                    {
                        for (int i = start; i <= end; i++)
                        {
                            pageNums.Add(i.ToString());
                        }

                        pageNums.Add("...");
                        pageNums.Add(pageCount.ToString());
                    }
                    else
                    {
                        start = pageCount - num * 2 + 1;
                        end = pageCount;
                        for (int i = start; i <= end; i++)
                        {
                            pageNums.Add(i.ToString());
                        }
                    }
                }
            }
            return pageNums;
        }

        /// <summary>
        /// 构造分页htlml代码
        /// </summary>
        /// <param name="jsFunc">js方法</param>
        /// <param name="pageNo">当前页号(从0开始)</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="num">显示页号数量</param>
        /// <returns></returns>
        public static HtmlString Pager(string jsFunc, int pageNo, int pageSize, int pageCount, int num = 5)
        {
            StringBuilder pageInfo = new StringBuilder();
            int count = pageCount;
            int pages = pageNo + 1;
            pageCount = Math.Max((pageCount + pageSize - 1) / pageSize, 1);//总分页数
            if (pageCount >= 1)
            {
                pageInfo.AppendLine(" <div id='pager' class='pagination' > ");
                pageInfo.AppendLine("<div class='paginnum'>");
                if (pageNo < 1)
                {
                    pageInfo.AppendLine("<a>首页</a>");
                    pageInfo.AppendLine("<a>上一页</a>");
                }
                if (pageNo > 1)
                {
                    pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"首页\" onclick=\"" + jsFunc + "(" + 1 + "," + pageSize + ")\">首页</a>");
                    pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"上一页\" onclick=\"" + jsFunc + "(" + (pageNo - 1) + "," + pageSize + ")\">上一页</a>");
                }
                foreach (var pageNum in GetPageNumList(pageNo, pageCount, num))
                {
                    if (pageNum == "...")
                    {
                        pageInfo.AppendLine("<i>...</i>");
                    }
                    else if (int.Parse(pageNum) - 1 == pageNo)
                    {

                        pageInfo.AppendLine("<span class=\"current\">" + pageNum + "</span>");
                    }
                    else
                    {
                        pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"第" + pageNum + "页\" onclick=\"" + jsFunc + "(" + (int.Parse(pageNum) - 1) + "," + pageSize + ")\">" + pageNum + "</a>");
                    }
                }
                if (pageNo < pageCount)
                {
                    pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"下一页\" onclick=\"" + jsFunc + "(" + (pageNo + 1) + "," + pageSize + ")\">下一页</a>");
                    pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"尾页\" onclick=\"" + jsFunc + "(" + (pageCount) + "," + pageSize + ")\">尾页</a>");
                }
                if (pageNo >= pageCount)
                {
                    pageInfo.AppendLine("<a>下一页</a>");
                    pageInfo.AppendLine("<a>尾页</a>");
                }
                pageInfo.AppendLine("</div>");

                pageInfo.AppendLine("<div class='information'> ");
                pageInfo.AppendLine(" <span>显示</span> ");
                pageInfo.AppendLine(" <select id='pagexz' name='pagexz' onchange='pagexz()' ><option value='10' " + (pageSize == 10 ? "selected='selected'" : "") + "   >10</option><option value='20' " + (pageSize == 20 ? "selected='selected'" : "") + ">20</option><option value='50' " + (pageSize == 50 ? "selected='selected'" : "") + ">50</option><option value='100' " + (pageSize == 100 ? "selected='selected'" : "") + ">100</option><option value='200' " + (pageSize == 200 ? "selected='selected'" : "") + ">200</option><option value='300' " + (pageSize == 300 ? "selected='selected'" : "") + ">300</option><option value='400' " + (pageSize == 400 ? "selected='selected'" : "") + ">400</option><option value='500' " + (pageSize == 500 ? "selected='selected'" : "") + ">500</option></select>");
                pageInfo.AppendLine("<span>条/页</span> ");
                pageInfo.AppendLine("<span>共" + count + "条记录</span> <span>第 <i class='colorfont'>" + pages + "</i> 页 </span>");
                pageInfo.AppendLine("</div> ");
                pageInfo.AppendLine("</div>");
            }
            return new HtmlString(pageInfo.ToString());
        }


        /// <summary>
        /// 分页控件
        /// </summary>
        /// <param name="jsFunc">js方法</param>
        /// <param name="pageNo">当前页号(从1开始)</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="num">显示页号数量</param>
        /// <returns></returns>
        public static HtmlString Pagers(string jsFunc, int pageNo, int pageSize, int pageCount, int num = 5)
        {
            int count = pageCount;
            int pages = pageNo;
            StringBuilder pageInfo = new StringBuilder();
            pageCount = Math.Max((pageCount + pageSize - 1) / pageSize, 1);//总分页数
            if (pageCount >= 1)
            {
                pageInfo.AppendLine(" <div id='pager' class='pagination' > ");
                pageInfo.AppendLine("<div class='paginnum'>");
                if (pageNo <= 1)
                {
                    pageInfo.AppendLine("<a>首页</a>");
                    pageInfo.AppendLine("<a>上一页</a>");
                }
                if (pageNo > 1)
                {
                    pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"首页\" onclick=\"" + jsFunc + "(" + 1 + "," + pageSize + ")\">首页</a>");
                    pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"上一页\" onclick=\"" + jsFunc + "(" + (pageNo - 1) + "," + pageSize + ")\">上一页</a>");
                }
                foreach (var pageNum in GetPageNumList(pageNo, pageCount, num))
                {
                    if (pageNum == "...")
                    {
                        pageInfo.AppendLine("<i>...</i>");
                    }
                    else if (int.Parse(pageNum) == pageNo)
                    {

                        pageInfo.AppendLine("<span class=\"current\">" + pageNum + "</span>");
                    }
                    else
                    {
                        pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"第" + pageNum + "页\" onclick=\"" + jsFunc + "(" + (int.Parse(pageNum)) + "," + pageSize + ")\">" + pageNum + "</a>");
                    }
                }
                if (pageNo < pageCount)
                {
                    pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"下一页\" onclick=\"" + jsFunc + "(" + (pageNo + 1) + "," + pageSize + ")\">下一页</a>");
                    pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"尾页\" onclick=\"" + jsFunc + "(" + (pageCount) + "," + pageSize + ")\">尾页</a>");
                }
                if (pageNo >= pageCount)
                {
                    pageInfo.AppendLine("<a>下一页</a>");
                    pageInfo.AppendLine("<a>尾页</a>");
                }
                pageInfo.AppendLine("</div>");

                pageInfo.AppendLine("<div class='information'> ");
                pageInfo.AppendLine(" <span>显示</span> ");
                pageInfo.AppendLine(" <select id='pagexz' name='pagexz' onchange='pagexz()' ><option value='10' " + (pageSize == 10 ? "selected='selected'" : "") + "   >10</option><option value='20' " + (pageSize == 20 ? "selected='selected'" : "") + ">20</option><option value='50' " + (pageSize == 50 ? "selected='selected'" : "") + ">50</option><option value='100' " + (pageSize == 100 ? "selected='selected'" : "") + ">100</option><option value='200' " + (pageSize == 200 ? "selected='selected'" : "") + ">200</option><option value='300' " + (pageSize == 300 ? "selected='selected'" : "") + ">300</option><option value='400' " + (pageSize == 400 ? "selected='selected'" : "") + ">400</option><option value='500' " + (pageSize == 500 ? "selected='selected'" : "") + ">500</option></select>");
                pageInfo.AppendLine("<span>条/页</span> ");
                pageInfo.AppendLine("<span>共" + count + "条记录</span> <span>第 <i class='colorfont'>" + pages + "</i> 页 </span>");
                pageInfo.AppendLine("</div> ");
                pageInfo.AppendLine("</div>");
            }
            return new HtmlString(pageInfo.ToString());
        }

        /// <summary>
        /// 分页控件
        /// </summary>
        /// <param name="jsFunc">js方法</param>
        /// <param name="pageNo">当前页号(从1开始)</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="num">显示页号数量</param>
        /// <returns></returns>
        public static HtmlString MobilePagers(string jsFunc, int pageNo, int pageSize, int pageCount, int num = 5)
        {
            int count = pageCount;
            int pages = pageNo;
            StringBuilder pageInfo = new StringBuilder();
            pageCount = Math.Max((pageCount + pageSize - 1) / pageSize, 1);//总分页数
            if (pageCount >= 1)
            {
                pageInfo.AppendLine(" <div id='pager' class='pagination' > ");

                pageInfo.AppendLine("<div class='pagination-items'>");
                if (pageNo <= 1)
                {
                    //pageInfo.AppendLine("<a>首页</a>");
                    pageInfo.AppendLine("<a title='上页'><i class='fa fa-chevron-left'></i></a>");
                }
                if (pageNo > 1)
                {
                    //pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"首页\" onclick=\"" + jsFunc + "(" + 1 + "," + pageSize + ")\">首页</a>");
                    pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"上页\" onclick=\"" + jsFunc + "(" + (pageNo - 1) + "," + pageSize + ")\"><i class='fa fa-chevron-left'></i></a>");
                }
                foreach (var pageNum in GetPageNumList(pageNo, pageCount, num))
                {
                    if (pageNum == "...")
                    {
                        pageInfo.AppendLine("<i>...</i>");
                    }
                    else if (int.Parse(pageNum) == pageNo)
                    {
                        //pageInfo.AppendLine("<a class=\"currentpage\">" + pageNum + "</a>");
                        pageInfo.AppendLine("<span class=\"current\">" + pageNum + "</span>");
                    }
                    else
                    {
                        pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"第" + pageNum + "页\" onclick=\"" + jsFunc + "(" + (int.Parse(pageNum)) + "," + pageSize + ")\">" + pageNum + "</a>");
                    }
                }
                if (pageNo < pageCount)
                {
                    pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"下页\" onclick=\"" + jsFunc + "(" + (pageNo + 1) + "," + pageSize + ")\"><i class='fa fa-chevron-right'></i></a>");
                    //pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"尾页\" onclick=\"" + jsFunc + "(" + (pageCount) + "," + pageSize + ")\">尾页</a>");
                }
                if (pageNo >= pageCount)
                {
                    pageInfo.AppendLine("<a title='下页'><i class='fa fa-chevron-right'></i></a>");
                    //pageInfo.AppendLine("<a>尾页</a>");
                }
                pageInfo.AppendLine("</div>");
                pageInfo.AppendLine("<div class='information' style='display:none;'> ");
                //pageInfo.AppendLine(" <span>显示</span><span>20</span><span>条/页</span><span>共" + count + "条记录</span> 第" + pageNo + "页 ");
                pageInfo.AppendLine(" <span>显示</span> ");
                pageInfo.AppendLine(" <select id='pagexz' name='pagexz' onchange='pagexz()'><option value='10' " + (pageSize == 10 ? "selected='selected'" : "") + "   >10</option><option value='20' " + (pageSize == 20 ? "selected='selected'" : "") + ">20</option><option value='50' " + (pageSize == 50 ? "selected='selected'" : "") + ">50</option><option value='100' " + (pageSize == 100 ? "selected='selected'" : "") + ">100</option><option value='200' " + (pageSize == 200 ? "selected='selected'" : "") + ">200</option><option value='300' " + (pageSize == 300 ? "selected='selected'" : "") + ">300</option><option value='400' " + (pageSize == 400 ? "selected='selected'" : "") + ">400</option><option value='500' " + (pageSize == 500 ? "selected='selected'" : "") + ">500</option></select>");
                pageInfo.AppendLine("<span>条/页</span> ");
                pageInfo.AppendLine("<span>共" + count + "条记录</span> 第" + pages + "页  ");
                pageInfo.AppendLine("</div> ");
                pageInfo.AppendLine("</div>");
            }
            return new HtmlString(pageInfo.ToString());
        }


        /// <summary>
        /// 分页控件
        /// </summary>
        /// <param name="jsFunc">js方法</param>
        /// <param name="pageNo">当前页号(从1开始)</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="num">显示页号数量</param>
        /// <returns></returns>
        public static HtmlString PagersCL(string jsFunc, int pageNo, int pageSize, int pageCount, int num = 2)
        {
            int count = pageCount;
            int pages = pageNo;
            StringBuilder pageInfo = new StringBuilder();
            pageCount = Math.Max((pageCount + pageSize - 1) / pageSize, 1);//总分页数
            if (pageCount >= 1)
            {
                pageInfo.AppendLine(" <div id='pager' class='paginationcl' > ");

                pageInfo.AppendLine("<div class='pagination-itemscl'>");
                if (pageNo <= 1)
                {
                    pageInfo.AppendLine("<a title='上页'><i class='fa fa-chevron-left'></i></a>");
                }
                if (pageNo > 1)
                {

                    pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"上页\" onclick=\"" + jsFunc + "(" + (pageNo - 1) + "," + pageSize + ")\"><i class='fa fa-chevron-left'></i></a>");
                }
                foreach (var pageNum in GetPageNumList(pageNo, pageCount, num))
                {
                    if (pageNum == "...")
                    {
                        pageInfo.AppendLine("<i>...</i>");
                    }
                    else if (int.Parse(pageNum) == pageNo)
                    {

                        pageInfo.AppendLine("<span class=\"current\">" + pageNum + "</span>");
                    }
                    else
                    {
                        pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"第" + pageNum + "页\" onclick=\"" + jsFunc + "(" + (int.Parse(pageNum)) + "," + pageSize + ")\">" + pageNum + "</a>");
                    }
                }
                if (pageNo < pageCount)
                {
                    pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"下页\" onclick=\"" + jsFunc + "(" + (pageNo + 1) + "," + pageSize + ")\"><i class='fa fa-chevron-right'></i></a>");

                }
                if (pageNo >= pageCount)
                {
                    pageInfo.AppendLine("<a title='下页'><i class='fa fa-chevron-right'></i></a>");

                }
                pageInfo.AppendLine("</div>");
                pageInfo.AppendLine("<div class='informationcl' style='display:none;'> ");
                pageInfo.AppendLine(" <span>显示</span> ");
                pageInfo.AppendLine(" <select id='pagexz' name='pagexzCL' onchange='pagexzCL()'><option value='10' " + (pageSize == 10 ? "selected='selected'" : "") + "   >10</option><option value='20' " + (pageSize == 20 ? "selected='selected'" : "") + ">20</option><option value='50' " + (pageSize == 50 ? "selected='selected'" : "") + ">50</option><option value='100' " + (pageSize == 100 ? "selected='selected'" : "") + ">100</option><option value='200' " + (pageSize == 200 ? "selected='selected'" : "") + ">200</option><option value='300' " + (pageSize == 300 ? "selected='selected'" : "") + ">300</option><option value='400' " + (pageSize == 400 ? "selected='selected'" : "") + ">400</option><option value='500' " + (pageSize == 500 ? "selected='selected'" : "") + ">500</option></select>");
                pageInfo.AppendLine("<span>条/页</span> ");
                pageInfo.AppendLine("<span>共" + count + "条记录</span> 第" + pages + "页  ");
                pageInfo.AppendLine("</div> ");
                pageInfo.AppendLine("</div>");
            }
            return new HtmlString(pageInfo.ToString());
        }

        /// <summary>
        /// 前台分页控件
        /// </summary>
        /// <param name="jsFunc">js方法</param>
        /// <param name="pageNo">当前页号(从1开始)</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="num">显示页号数量</param>
        /// <returns></returns>
        public static HtmlString UserPagers(string jsFunc, int pageNo, int pageSize, int pageCount, int num = 5)
        {
            int count = pageCount;
            //int pages = pageNo + 1;
            StringBuilder pageInfo = new StringBuilder();
            pageCount = Math.Max((pageCount + pageSize - 1) / pageSize, 1);//总分页数
            if (pageCount >= 1)
            {
                pageInfo.AppendLine(" <div id='pager' class='pagination' > ");
                pageInfo.AppendLine("<div class='paginnum'>");
                if (pageNo <= 1)
                {
                    pageInfo.AppendLine("<a>首页</a>");
                    pageInfo.AppendLine("<a>上一页</a>");
                }
                if (pageNo > 1)
                {
                    pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"首页\" onclick=\"" + jsFunc + "(" + 1 + "," + pageSize + ")\">首页</a>");
                    pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"上一页\" onclick=\"" + jsFunc + "(" + (pageNo - 1) + "," + pageSize + ")\">上一页</a>");
                }
                foreach (var pageNum in GetPageNumList(pageNo, pageCount, num))
                {
                    if (pageNum == "...")
                    {
                        pageInfo.AppendLine("<i>...</i>");
                    }
                    else if (int.Parse(pageNum) == pageNo)
                    {

                        pageInfo.AppendLine("<span class=\"current\">" + pageNum + "</span>");
                    }
                    else
                    {
                        pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"第" + pageNum + "页\" onclick=\"" + jsFunc + "(" + (int.Parse(pageNum)) + "," + pageSize + ")\">" + pageNum + "</a>");
                    }
                }
                if (pageNo < pageCount)
                {
                    pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"下一页\" onclick=\"" + jsFunc + "(" + (pageNo + 1) + "," + pageSize + ")\">下一页</a>");
                    pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"尾页\" onclick=\"" + jsFunc + "(" + (pageCount) + "," + pageSize + ")\">尾页</a>");
                }
                if (pageNo >= pageCount)
                {
                    pageInfo.AppendLine("<a>下一页</a>");
                    pageInfo.AppendLine("<a>尾页</a>");
                }
                pageInfo.AppendLine("</div>");

                pageInfo.AppendLine("<div class='information'> ");
                pageInfo.AppendLine(" <span>显示</span> ");
                pageInfo.AppendLine(" <select id='pagexz' name='pagexz' onchange='pagexz()' ><option value='10' " + (pageSize == 10 ? "selected='selected'" : "") + "   >10</option><option value='20' " + (pageSize == 20 ? "selected='selected'" : "") + ">20</option><option value='50' " + (pageSize == 50 ? "selected='selected'" : "") + ">50</option><option value='100' " + (pageSize == 100 ? "selected='selected'" : "") + ">100</option><option value='200' " + (pageSize == 200 ? "selected='selected'" : "") + ">200</option><option value='300' " + (pageSize == 300 ? "selected='selected'" : "") + ">300</option><option value='400' " + (pageSize == 400 ? "selected='selected'" : "") + ">400</option><option value='500' " + (pageSize == 500 ? "selected='selected'" : "") + ">500</option></select>");
                pageInfo.AppendLine("<span>条/页</span> ");
                pageInfo.AppendLine("<span>共" + count + "条记录</span> <span>第 <i class='colorfont'>" + pageNo + "</i> 页 </span>");
                pageInfo.AppendLine("</div> ");
                pageInfo.AppendLine("</div>");


                /*( pageInfo.AppendLine(" <div id='pager' class='pagination'> ");
                 pageInfo.AppendLine("  <font class='page_count'>每页<span class=''>" + pageSize + "</span>条</font><font class='page_total'>共<span class=''>" + count + "</span>条</font> ");
                 pageInfo.AppendLine("<div class='page_links' >");
                 if (pageNo <= 1)
                 {
                     pageInfo.AppendLine("<a >首页</a>");
                     pageInfo.AppendLine("<a>上一页</a>");
                 }
                 if (pageNo > 1)
                 {
                     pageInfo.AppendLine("<a href=\"javascript:void(0);\" class='pg_next' title=\"首页\" onclick=\"" + jsFunc + "(" + 1 + "," + pageSize + ")\">首页</a>");
                     pageInfo.AppendLine("<a href=\"javascript:void(0);\" class='pg_next' title=\"上一页\" onclick=\"" + jsFunc + "(" + (pageNo - 1) + "," + pageSize + ")\">上一页</a>");
                 }
                 foreach (var pageNum in GetPageNumList(pageNo, pageCount, num))
                 {
                     if (pageNum == "...")
                     {
                         pageInfo.AppendLine("<i>...</i>");
                     }
                     else if (int.Parse(pageNum) == pageNo)
                     {
                         //pageInfo.AppendLine("<a class=\"currentpage\">" + pageNum + "</a>");
                         pageInfo.AppendLine("<span class=\"page_cur\">" + pageNum + "</span>");
                     }
                     else
                     {
                         pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"第" + pageNum + "页\" onclick=\"" + jsFunc + "(" + (int.Parse(pageNum) ) + "," + pageSize + ")\">" + pageNum + "</a>");
                     }
                 }
                 if (pageNo < pageCount)
                 {
                     pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"下一页\" class='pg_next' onclick=\"" + jsFunc + "(" + (pageNo + 1) + "," + pageSize + ")\">下一页</a>");
                     pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"尾页\" class='pg_next' onclick=\"" + jsFunc + "(" + (pageCount) + "," + pageSize + ")\">尾页</a>");
                 }
                 if (pageNo >= pageCount)
                 {
                     pageInfo.AppendLine("<a>下一页</a>");
                     pageInfo.AppendLine("<a>尾页</a>");
                 }
                 pageInfo.AppendLine("</div>");
                 pageInfo.AppendLine("</div>");*/
            }
            return new HtmlString(pageInfo.ToString());
        }

        public static HtmlString HtmlPagers(string jsFunc, int pageNo, int pageSize, int pageCount, int num = 5)
        {
            int count = pageCount;
            //int pages = pageNo + 1;
            StringBuilder pageInfo = new StringBuilder();
            pageCount = Math.Max((pageCount + pageSize - 1) / pageSize, 1);//总分页数
            if (pageCount > 1)
            {

                foreach (var pageNum in GetPageNumList(pageNo, pageCount, num))
                {
                    if (pageNum == "...")
                    {
                        pageInfo.AppendLine("<i>...</i>");
                    }
                    else if (int.Parse(pageNum) == pageNo)
                    {
                        //pageInfo.AppendLine("<a class=\"currentpage\">" + pageNum + "</a>");
                        pageInfo.AppendLine("<a class=\"active \">" + pageNum + "</a>");
                    }
                    else
                    {
                        pageInfo.AppendLine("<a href=\"javascript:void(0);\" title=\"第" + pageNum + "页\" onclick=\"" + jsFunc + "(" + (int.Parse(pageNum)) + "," + pageSize + ")\">" + pageNum + "</a>");
                    }
                }
            }

            return new HtmlString(pageInfo.ToString());
        }

        public static HtmlString HtmlPagesGw(string url, int pageNo, int pageSize, int pageCount, int num = 5)
        {
            int count = pageCount;
            StringBuilder pageInfo = new StringBuilder();
            pageCount = Math.Max((pageCount + pageSize - 1) / pageSize, 1);//总分页数
            if (pageCount > 1)
            {

                foreach (var pageNum in GetPageNumList(pageNo, pageCount, num))
                {
                    if (pageNum == "...")
                    {
                        pageInfo.AppendLine("<i>...</i>");
                    }
                    else if (int.Parse(pageNum) == pageNo)
                    {
                        //pageInfo.AppendLine("<a class=\"currentpage\">" + pageNum + "</a>");
                        pageInfo.AppendLine("<a class=\"active \">" + pageNum + "</a>");
                    }
                    else
                    {
                        pageInfo.AppendLine("<a href=\"" + url + "?pageIndexs=" + (int.Parse(pageNum)) + "&PageSize=" + pageSize + "\" title=\"第" + pageNum + "页\">" + pageNum + "</a>");
                    }
                }
            }

            return new HtmlString(pageInfo.ToString());
        }

        /// <summary>
        /// 长内容分页 哈希表需要引用命名空间System.Collections 
        /// </summary>
        /// <param name="n_content">新闻内容</param>
        /// <param name="page">是新闻当前页数</param>
        /// <param name="size">每页显示字符长度</param>
        /// <param name="linkurl">页码链接地址</param>
        /// <returns></returns>
        public static Hashtable SeparatePages(string n_content, string page, int size, string linkurl)
        {
            //在此处放置初始化页的用户代码
            System.Collections.Hashtable returnHash = new System.Collections.Hashtable();
            int start, stops, t, stat, pp, pagecount, pagesize;
            string pa, articletxt, articletext, html;
            int pig = 0;
            //变量初始值
            stat = 0;
            start = 0; //开始查询的字符串位置，初始为0
            stops = 0;
            pagesize = size;//定义每页至少显示字符串数
            pagecount = 0;
            html = "";
            //获得当前的页数
            pa = page;
            if (pa == "" || pa == null)
                pa = "1";
            pp = Convert.ToInt32(pa);
            //获得内容
            articletxt = n_content;
            //判断页面的内容长度是否大于定义的每页至少显示字符串数
            if (articletxt.Length >= pagesize) // 如果大于字符串数，则我们可以分页显示
            {
                t = articletxt.Length / pagesize; //获得大致的总页数
                for (int j = 0; j <= t; j++)
                {    //如果查询开始位置到查询的范围超出整个内容的长度，那么就不用寻找断点（分页点）；反之，查找
                    if (start + pagesize < articletxt.Length)
                    {
                        stat = articletxt.ToLower().IndexOf("</p>", start + pagesize); //查找</P>分页点的位置
                        if (stat == -1)
                            stat = articletxt.ToLower().IndexOf("<br>", start + pagesize); //查找</P>分页点的位置
                        if (stat == -1)
                            stat = articletxt.ToLower().IndexOf("<br/>", start + pagesize); //查找</P>分页点的位置
                    }
                    if (stat <= 0)//如果找不到
                    {
                    }
                    else
                    {
                        stops = stat; //分页点的位置也就作为这一页的终点位置
                        if (start < articletxt.Length)
                        {
                            if ((articletxt.Length - start) < pagesize)
                            {
                                if (pig == 0)
                                {
                                    pagecount = pagecount + 1;
                                }
                                pig = 1;
                            }
                            else
                            {
                                pagecount = pagecount + 1;
                            }
                        }
                        if (start + pagesize >= articletxt.Length) //如果起始位置到查询的范围超出整个内容的长度，那么这一页的终点位置为内容的终点
                        {
                            stops = articletxt.Length;
                        }
                        if (pp == j + 1) //如果是当前，那么输出当前页的内容
                        {
                            articletext = articletxt.Substring(start, stops - start); //取内容的起始位置到终点位置这段字符串输出
                            returnHash["content"] = articletext;
                        }
                        start = stat; //将终点位置作为下一页的起始位置
                    }
                }// pagecount = pagecount - 1;
            }
            else
            {
                returnHash["content"] = n_content;
            }
            //分页部分(这里就简单多了)
            //定义分页代码变量
            if (pagecount > 1) //当页数大于1的时候我们显示页数
            {
                if (pp - 1 > 0) //显示上一页，方便浏览
                { html = html + "<a href=\"" + linkurl + "&page=" + (pp - 1) + "\">[上一页]</a> "; }
                else
                {
                    if (pp == 1)
                    { html = html + "[<font color=#cccccc>上一页</font>] "; }
                    else
                    { html = html + "<a href=\"" + linkurl + "&page=\"" + (1) + "\">[上一页]</a> "; }
                }
                for (int i = 1; i <= pagecount; i++)
                {
                    if (i == pp)  //如果是当前页，加粗显示
                    { html = html + "<b>[" + i + "]</b> "; }
                    else
                    { html = html + "<a href=\"" + linkurl + "&page=" + i + "\">[" + i + "]</a> "; }
                }
                if (pp + 1 > pagecount)  //显示下一页，方便浏览
                {
                    if (pp == pagecount)
                    { html = html + "[<font color=#cccccc>下一页</font>] "; }
                    else
                    { html = html + "<a href=\"" + linkurl + "&page=" + (pagecount) + "\">[下一页]</a></p>"; }
                }
                else
                {
                    html = html + "<a href=\"" + linkurl + "&page=" + (pp + 1) + "\">[下一页]</a></p>";
                }
            }
            returnHash["pagetxt"] = html;
            return returnHash;
        }

    }
}
