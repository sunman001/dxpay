using JMP.BLL;
using JMP.TOOL;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TOOL;
using TOOL.EnumUtil;
using TOOL.Extensions;
using WEB.Util.Logger;

namespace WEB.Controllers
{
    public class AppChannelReportController : Controller
    {
        //
        // GET: /AppChannelReport/
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;
        
        JMP.BLL.jmp_user_report bll_report = new JMP.BLL.jmp_user_report();
        JMP.BLL.jmp_limit bll_limit = new JMP.BLL.jmp_limit();
        /// <summary>
        /// 上游通道错误统计
        /// </summary>
        /// <param name="rtype"></param>
        /// <returns></returns>
        public ActionResult syChannelReport(string rtype)
        {
            rtype = !string.IsNullOrEmpty(rtype) ? rtype : "total";
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量            
            string types = string.IsNullOrEmpty(Request["s_type"]) ? "0" : Request["s_type"];
            string searchKey = string.IsNullOrEmpty(Request["s_key"]) ? "" : Request["s_key"];
            string stime = string.IsNullOrEmpty(Request["s_begin"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_begin"];
            string etime = string.IsNullOrEmpty(Request["s_end"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_end"];
            DataTable dt = new DataTable();
            string orderby = "order by RelatedId ";
            string BsaeDb = System.Configuration.ConfigurationManager.AppSettings["BaseDb"];
            string where = "";

            // 汇总查询
            #region 汇总查询

            if (rtype == "total")
            {
                if (!string.IsNullOrEmpty(types))
                {
                    if (!string.IsNullOrEmpty(searchKey))
                    {
                        switch (types)
                        {
                            case "1":
                                where += " and l_corporatename like '%" + searchKey + "%'";
                                break;
                            
                        }
                    }
                }
                where += " and CONVERT(nvarchar(10),a.CreatedOn,120)>='" + stime + "' and CONVERT(nvarchar(10),a.CreatedOn,120)<='" + etime + "' ";
                string sql = string.Format(@"select a.*, (b.Notpay+b.Success) as counts ,ISNULL((case when (b.Notpay+b.Success)=0 then 0 else( cast( a.sytzyccount /( b.Notpay+b.Success) as  numeric(5,2)))end),0) sytzycrato , ISNULL((case when (b.Notpay+b.Success)=0 then 0 else(  cast( a.syzfcwcount /( b.Notpay+b.Success) as  numeric(5,2)))end),0) syzfcwrato
               from (
               select sum( sytzyccount) as  sytzyccount, sum(syzfcwcount) as syzfcwcount ,l_corporatename,RelatedId ,CreatedOn
               from (
               select   '' as sytzyccount, COUNT(a.Id) as syzfcwcount ,b.l_corporatename,a.RelatedId ,CONVERT(nvarchar(10),a.CreatedOn,120)  as CreatedOn from {1}.dbo. LogForApi  a 
              left join  {1}.dbo.jmp_interface  b on A.RelatedId=B.l_id where a.PlatformId=1  {0}   
			   group by b.l_corporatename,a.RelatedId ,CONVERT(nvarchar(10),a.CreatedOn,120)
             union all
             select COUNT(Id) as sytzyccount,  ''as syzfcwcount ,b.l_corporatename,a.RelatedId ,CONVERT(nvarchar(10),a.CreatedOn,120)  as CreatedOn    from  {1}.dbo.LogForApi  a 
              left join  {1}.dbo.jmp_interface  b on A.RelatedId=B.l_id where a.PlatformId=3   {0}  
			  group by b.l_corporatename,a.RelatedId,CONVERT(nvarchar(10),a.CreatedOn,120)
			  union all 
			 select   '' as sytzyccount, COUNT(a.Id) as syzfcwcount ,'无通道' as l_corporatename,a.RelatedId ,CONVERT(nvarchar(10),a.CreatedOn,120)  as CreatedOn from {1}.dbo. LogForApi  a 
               where a.PlatformId=1 and    a.RelatedId =0 {0} 
			   group by a.RelatedId ,CONVERT(nvarchar(10),a.CreatedOn,120)
			  union all 
			    select COUNT(Id) as sytzyccount,  ''as syzfcwcount ,'无通道' as l_corporatename,a.RelatedId ,CONVERT(nvarchar(10),a.CreatedOn,120)  as CreatedOn    from  {1}.dbo.LogForApi  a 
              where a.PlatformId=3   and a.RelatedId=0  {0} 
			  group by a.RelatedId,CONVERT(nvarchar(10),a.CreatedOn,120)
			  ) as b  group by l_corporatename,RelatedId,CreatedOn) as a 
           inner join
		   ( select sum(Notpay) as Notpay ,sum(Success) as Success ,ChannelId ,CreatedDate from  dx_total.dbo.jmp_AppChannelReport  group by ChannelId,CreatedDate) as b on a.RelatedId=b.ChannelId 			
			 where b.CreatedDate=CONVERT(nvarchar(10),a.CreatedOn,120)", where, BsaeDb);
                dt = bll_report.GetLists(sql, orderby, pageIndexs, PageSize, out pageCount);

            }
            #endregion
            #region 今日查询 
            //今日查询
            else
            {
                if (!string.IsNullOrEmpty(types))
                {
                    if (!string.IsNullOrEmpty(searchKey))
                    {
                        switch (types)
                        {
                            case "1":
                                where += "  and l_corporatename like '%" + searchKey + "%'";
                                break;
                        }
                    }
                }
                string sql = string.Format(@"select a.*, (c.Notpay+c.Success) as counts ,ISNULL((case when (c.Notpay+c.Success)=0 then 0 else( cast( a.sytzyccount /( c.Notpay+c.Success) as  numeric(5,2)))end),0) sytzycrato ,
 ISNULL((case when (c.Notpay+c.Success)=0 then 0 else(  cast( a.syzfcwcount /( c.Notpay+c.Success) as  numeric(5,2)))end),0) syzfcwrato from (
select sum( sytzyccount) as  sytzyccount, sum(syzfcwcount) as syzfcwcount ,l_corporatename,RelatedId ,CreatedOn from (
select   '' as sytzyccount, COUNT(a.Id) as syzfcwcount ,b.l_corporatename,a.RelatedId ,CONVERT(nvarchar(10),a.CreatedOn,120)  as CreatedOn 
from   {1}.dbo.LogForApi  a 
 left join  {1}.dbo.jmp_interface  b on A.RelatedId=B.l_id 
 where a.PlatformId=1   and CONVERT(nvarchar(10),a.CreatedOn,120)=CONVERT(nvarchar(10),GETDATE(),120) {0}
group by b.l_corporatename,a.RelatedId ,CONVERT(nvarchar(10),a.CreatedOn,120)
union all 
select   COUNT(Id) as sytzyccount,  ''as syzfcwcount ,b.l_corporatename,a.RelatedId ,CONVERT(nvarchar(10),a.CreatedOn,120)  as CreatedOn    
from   {1}.dbo.LogForApi  a  
left join  {1}.dbo.jmp_interface  b on A.RelatedId=B.l_id 
where a.PlatformId=3  and CONVERT(nvarchar(10),a.CreatedOn,120)=CONVERT(nvarchar(10),GETDATE(),120) {0}
group by b.l_corporatename,a.RelatedId,CONVERT(nvarchar(10),a.CreatedOn,120)
union all 
select   '' as sytzyccount, COUNT(a.Id) as syzfcwcount ,'无通道' as l_corporatename,a.RelatedId ,CONVERT(nvarchar(10),a.CreatedOn,120)  as CreatedOn 
from  {1}.dbo.LogForApi  a 
 where a.PlatformId=1  and a.RelatedId=0  and CONVERT(nvarchar(10),a.CreatedOn,120)=CONVERT(nvarchar(10),GETDATE(),120) {0}
group by a.RelatedId ,CONVERT(nvarchar(10),a.CreatedOn,120)
union all 
select   COUNT(Id) as sytzyccount,  ''as syzfcwcount ,'无通道' as l_corporatename,a.RelatedId ,CONVERT(nvarchar(10),a.CreatedOn,120)  as CreatedOn    
from   {1}.dbo.LogForApi  a  
where a.PlatformId=3  and a.RelatedId=0 and CONVERT(nvarchar(10),a.CreatedOn,120)=CONVERT(nvarchar(10),GETDATE(),120) {0}
group by a.RelatedId,CONVERT(nvarchar(10),a.CreatedOn,120)
) as b  group by l_corporatename,RelatedId,CreatedOn
) as a 
left join 
(select sum(Notpay) as Notpay ,sum(Success) as Success ,ChannelId  ,CONVERT(nvarchar(10),CreatedDate,120)  as CreatedDate
 from  dx_total.dbo.jmp_AppChannelMinuteReport  group by ChannelId ,CONVERT(nvarchar(10),CreatedDate,120)) c on a.RelatedId=c.ChannelId 
 where CONVERT(nvarchar(10),c.CreatedDate,120) =CONVERT(nvarchar(10),a.CreatedOn,120)  
  ", where, BsaeDb);
  dt = bll_report.GetLists(sql, orderby, pageIndexs, PageSize, out pageCount);
    }
            #endregion

            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.rtype = rtype;
            return View(dt);
        }
        /// <summary>
        /// 下游
        /// </summary>
        /// <param name="rtype"></param>
        /// <returns></returns>
        public ActionResult xyChannelReport(string rtype)
        {
            rtype = !string.IsNullOrEmpty(rtype) ? rtype : "total";
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量            
            string types = string.IsNullOrEmpty(Request["s_type"]) ? "0" : Request["s_type"];
            string searchKey = string.IsNullOrEmpty(Request["s_key"]) ? "" : Request["s_key"];
            string stime = string.IsNullOrEmpty(Request["s_begin"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_begin"];
            string etime = string.IsNullOrEmpty(Request["s_end"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_end"];
            DataTable dt = new DataTable();
            DataTable ddt = new DataTable();
            string orderby = "order by RelatedId ";
            string BsaeDb = System.Configuration.ConfigurationManager.AppSettings["BaseDb"];
            string where = "";
            string Countwhere = "";

            // 汇总查询
            #region 汇总查询

            if (rtype == "total")
            {
                if (!string.IsNullOrEmpty(types))
                {
                    if (!string.IsNullOrEmpty(searchKey))
                    {
                        switch (types)
                        {
                            case "1":
                                where += " and b.a_name like '%" + searchKey + "%'";
                                break;

                        }
                    }
                }
                where += " and CONVERT(nvarchar(10),a.CreatedOn,120)>='" + stime + "' and CONVERT(nvarchar(10),a.CreatedOn,120)<='" + etime + "' ";

                Countwhere += "and CONVERT(nvarchar(10),a.CreatedOn,120)>='" + stime + "' and CONVERT(nvarchar(10),a.CreatedOn,120)<='" + etime + "'";



                string sql = string.Format(@"
select v.*,c.a_count , ISNULL((case when (c.a_count)=0 then 0 else( cast( v.ordercount /(a_count) as  numeric(5,2)))end),0) ordercountrato , ISNULL((case when (c.a_count)=0 then 0 else( cast( v.paymentcount /(a_count) as  numeric(5,2)))end),0)paymentcountrato,ISNULL((case when (c.a_count)=0 then 0 else( cast( v.othercount /(a_count) as  numeric(5,2)))end),0)othercountrato from (
select sum(ordercount)as ordercount  ,sum(paymentcount) as paymentcount , sum(othercount) as othercount,RelatedId ,a_name,CreatedOn from (
select count(a.Id) as ordercount, '' as paymentcount,''as othercount,a.RelatedId,b.a_name ,CONVERT(nvarchar(10),a.CreatedOn,120)  as CreatedOn from  {1}.dbo.LogForApi a 
left join  {1}.dbo.jmp_app b on a.RelatedId=b.a_id where a.PlatformId=2 and a.ErrorTypeId=1 {0}
group by a.RelatedId,b.a_name,CONVERT(nvarchar(10),a.CreatedOn,120)  
union all 
select '' as ordercount,count(a.Id) as paymentcount,''as othercount,a.RelatedId,b.a_name ,CONVERT(nvarchar(10),a.CreatedOn,120)  as CreatedOn from {1}.dbo.LogForApi a 
left join  {1}.dbo.jmp_app b on a.RelatedId=b.a_id where a.PlatformId=2 and a.ErrorTypeId=2 {0}
group by a.RelatedId,b.a_name,CONVERT(nvarchar(10),a.CreatedOn,120)  
union all 
select '' as ordercount,'' as paymentcount,count(a.Id)as othercount,a.RelatedId,b.a_name,CONVERT(nvarchar(10),a.CreatedOn,120)  as CreatedOn  from  {1}.dbo.LogForApi a 
left join  {1}.dbo.jmp_app b on a.RelatedId=b.a_id where a.PlatformId=2 and a.ErrorTypeId=3 {0}
group by a.RelatedId,b.a_name,CONVERT(nvarchar(10),a.CreatedOn,120)  
union all 
select count(Id) as ordercount,'' as paymentcount,''as othercount,RelatedId,'无应用' as a_name ,CONVERT(nvarchar(10),CreatedOn,120)  as CreatedOn from {1}.dbo.LogForApi a  where RelatedId=0 and PlatformId=2 and ErrorTypeId=1 {2} group by RelatedId,CONVERT(nvarchar(10),CreatedOn,120)  
union all 
select '' as ordercount,count(Id) as paymentcount,''as othercount,RelatedId,'无应用' as a_name ,CONVERT(nvarchar(10),CreatedOn,120)  as CreatedOn from  {1}.dbo.LogForApi a  where RelatedId=0 and PlatformId=2 and ErrorTypeId=2 {2} group by RelatedId,CONVERT(nvarchar(10),CreatedOn,120)  
union all 
select '' as ordercount,'' as paymentcount,count(Id)as othercount,RelatedId,'无应用' as a_name ,CONVERT(nvarchar(10),CreatedOn,120)  as CreatedOn from {1}.dbo.LogForApi a where RelatedId=0 and PlatformId=2 and ErrorTypeId=3 {2} group by RelatedId,CONVERT(nvarchar(10),CreatedOn,120)  
) as a group by RelatedId,a_name,CreatedOn) as v inner join  dx_total.dbo.jmp_appreport c on v.RelatedId=c.a_appid where c.a_time=CONVERT(nvarchar(10),CreatedOn,120) 
  ", where, BsaeDb, Countwhere);
  dt = bll_report.GetLists(sql, orderby, pageIndexs, PageSize, out pageCount);

            }
            #endregion
            #region 今日查询 
            //今日查询
            else
            {
                if (!string.IsNullOrEmpty(types))
                {
                    if (!string.IsNullOrEmpty(searchKey))
                    {
                        switch (types)
                        {
                            case "1":
                                where += "  and a_name like '%" + searchKey + "%'";
                                break;
                        }
                    }
                }
                string sql = string.Format(@"select v.*,(c.Notpay +c.Success) as a_count, ISNULL((case when (c.Notpay +c.Success)=0 then 0 else( cast( v.ordercount /(c.Notpay +c.Success) as  numeric(5,2)))end),0) ordercountrato , ISNULL((case when (c.Notpay +c.Success)=0 then 0 else( cast( v.paymentcount /(c.Notpay +c.Success) as  numeric(5,2)))end),0)paymentcountrato,ISNULL((case when (c.Notpay +c.Success)=0 then 0 else( cast( v.othercount /(c.Notpay +c.Success) as  numeric(5,2)))end),0)othercountrato from (
select sum(ordercount)as ordercount  ,sum(paymentcount) as paymentcount , sum(othercount) as othercount,RelatedId ,a_name,CreatedOn from (
select count(a.Id) as ordercount, '' as paymentcount,''as othercount,a.RelatedId,b.a_name ,CONVERT(nvarchar(10),a.CreatedOn,120)  as CreatedOn from  {1}.dbo.LogForApi a 
left join  {1}.dbo.jmp_app b on a.RelatedId=b.a_id where a.PlatformId=2 and a.ErrorTypeId=1 and CONVERT(nvarchar(10),a.CreatedOn,120)=CONVERT(nvarchar(10),GETDATE(),120) {0}
group by a.RelatedId,b.a_name,CONVERT(nvarchar(10),a.CreatedOn,120)  
union all 
select '' as ordercount,count(a.Id) as paymentcount,''as othercount,a.RelatedId,b.a_name ,CONVERT(nvarchar(10),a.CreatedOn,120)  as CreatedOn from  {1}.dbo.LogForApi a 
left join {1}.dbo.jmp_app b on a.RelatedId=b.a_id where a.PlatformId=2 and a.ErrorTypeId=2 and CONVERT(nvarchar(10),a.CreatedOn,120)=CONVERT(nvarchar(10),GETDATE(),120) {0}
group by a.RelatedId,b.a_name,CONVERT(nvarchar(10),a.CreatedOn,120)  
union all 
select '' as ordercount,'' as paymentcount,count(a.Id)as othercount,a.RelatedId,b.a_name,CONVERT(nvarchar(10),a.CreatedOn,120)  as CreatedOn  from {1}.dbo.LogForApi a 
left join   {1}.dbo.jmp_app b on a.RelatedId=b.a_id where a.PlatformId=2 and a.ErrorTypeId=3 and CONVERT(nvarchar(10),a.CreatedOn,120)=CONVERT(nvarchar(10),GETDATE(),120) {0}
group by a.RelatedId,b.a_name,CONVERT(nvarchar(10),a.CreatedOn,120)  
union all 
select count(Id) as ordercount,'' as paymentcount,''as othercount,RelatedId,'无应用' as a_name ,CONVERT(nvarchar(10),CreatedOn,120)  as CreatedOn from {1}.dbo.LogForApi  where RelatedId=0 and PlatformId=2 and ErrorTypeId=1 and CONVERT(nvarchar(10),CreatedOn,120)=CONVERT(nvarchar(10),GETDATE(),120)     group by RelatedId,CONVERT(nvarchar(10),CreatedOn,120) 
union all 
select '' as ordercount,count(Id) as paymentcount,''as othercount,RelatedId,'无应用' as a_name ,CONVERT(nvarchar(10),CreatedOn,120)  as CreatedOn from {1}.dbo.LogForApi  where RelatedId=0 and PlatformId=2 and ErrorTypeId=2 and CONVERT(nvarchar(10),CreatedOn,120)=CONVERT(nvarchar(10),GETDATE(),120)     group by RelatedId,CONVERT(nvarchar(10),CreatedOn,120)  
union all 
select '' as ordercount,'' as paymentcount,count(Id)as othercount,RelatedId,'无应用' as a_name ,CONVERT(nvarchar(10),CreatedOn,120)  as CreatedOn from  {1}.dbo.LogForApi  where RelatedId=0 and PlatformId=2 and ErrorTypeId=3 and CONVERT(nvarchar(10),CreatedOn,120)=CONVERT(nvarchar(10),GETDATE(),120)     group by RelatedId,CONVERT(nvarchar(10),CreatedOn,120)  
) as a group by RelatedId,a_name,CreatedOn) as v inner join 
(
select sum(Notpay) as Notpay ,sum(Success) as Success ,AppId ,CONVERT(nvarchar(10),CreatedDate,120) as CreatedDate  from  dx_total.dbo.jmp_AppChannelMinuteReport 
 group by AppId,CONVERT(nvarchar(10),CreatedDate,120)) c on v.RelatedId=c.AppId
  where CONVERT(nvarchar(10),c.CreatedDate,120)=CONVERT(nvarchar(10),v.CreatedOn,120)  ", where, BsaeDb);
                dt = bll_report.GetLists(sql, orderby, pageIndexs, PageSize, out pageCount);
            }
            #endregion

            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.rtype = rtype;
            return View(dt);
          
        }


    }
}
