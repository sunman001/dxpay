/************聚米支付平台__财务管理************/
//描述：开发者的账单及提款管理
//功能：开发者的账单及提款管理
//开发者：谭玉科
//开发时间: 2016.03.24
/************聚米支付平台__财务管理************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JMP.TOOL;
using JMP.BLL;
using WEB.Models;
using OfficeOpenXml;
using WEB.Util.Logger;
using System.Text;
using TOOL;
using JMP.DBA;
using PayForAnother;
using PayForAnother.ChinPay;
using PayForAnother.Model;

namespace WEB.Controllers
{
    /// <summary>
    /// 类名：FinancialController
    /// 功能：财务管理
    /// 详细：财务管理
    /// 修改日期：2016.03.24
    /// </summary>
    public class FinancialController : Controller
    {

        /// <summary>
        /// 日志收集器
        /// </summary>
		private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;

        JMP.BLL.jmp_bill bll_bill = new JMP.BLL.jmp_bill();
        JMP.BLL.jmp_pay bll_pay = new JMP.BLL.jmp_pay();
        JMP.BLL.jmp_limit bll_limit = new JMP.BLL.jmp_limit();

        JMP.BLL.CoSettlementDeveloperOverview bll_CoSDO = new JMP.BLL.CoSettlementDeveloperOverview();
        JMP.BLL.jmp_paymode bll_paymode = new JMP.BLL.jmp_paymode();
        List<JMP.MDL.jmp_paymode> List_paymode = new List<JMP.MDL.jmp_paymode>();

        JMP.BLL.PayForAnotherInfo PayForAnotherBll = new JMP.BLL.PayForAnotherInfo();
        JMP.MDL.PayForAnotherInfo PayForAnotherMode = new JMP.MDL.PayForAnotherInfo();
        List<JMP.MDL.PayForAnotherInfo> PayForAnotherInfoList = new List<JMP.MDL.PayForAnotherInfo>();

        #region 开发者账单(每天)

        /// <summary>
        /// 开发者账单管理
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult BillList()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量            
            string types = string.IsNullOrEmpty(Request["s_type"]) ? "1" : Request["s_type"];
            string searchKey = string.IsNullOrEmpty(Request["s_key"]) ? "" : Request["s_key"];
            string stime = string.IsNullOrEmpty(Request["s_begin"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_begin"];
            string etime = string.IsNullOrEmpty(Request["s_end"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_end"];
            int sort = string.IsNullOrEmpty(Request["s_sort"]) ? 1 : int.Parse(Request["s_sort"]);
            int relation_type = string.IsNullOrEmpty(Request["relation_type"]) ? -1 : int.Parse(Request["relation_type"]);
            // string dept = System.Configuration.ConfigurationManager.AppSettings["RoleID"];

            #region 组装查询语句
            string where = "where 1=1";
            string where1 = "";
            string orderby = "";
            if (types == "1" && !string.IsNullOrEmpty(searchKey))
            {
                where += " and DeveloperName like '%" + searchKey + "%'";
            }
            if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
            {
                where += " and SettlementDay >='" + stime + "' and SettlementDay<='" + etime + "' ";
            }
            if (relation_type > -1)
            {
                where1 += " and user_bp.relation_type='" + relation_type + "'";
            }
            if (sort == 1)
            {
                orderby = "order by SettlementDay desc";

            }
            else
            {
                orderby = "order by SettlementDay asc";
            }

            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@"select Id,DeveloperId,DeveloperName,CreatedOn,SettlementDay,TotalAmount, OriginalTotalAmount,ServiceFee,PortFee,
DisplayName,bpname,relation_type,ISNULL((OriginalTotalAmount-TotalAmount),0) AS RefundAmount,ISNULL((TotalAmount-ServiceFee-PortFee),0) as KFZIncome,
(isnull(TotalAmount-ServiceFee-PortFee,0)-a.p_money) as ketiMoney,
a.p_money
,isnull(SUM(b.p_money),0) as yf_money 
from (
select Id,DeveloperId,DeveloperName,CreatedOn,SettlementDay,TotalAmount, OriginalTotalAmount,ServiceFee,PortFee,
user_bp.DisplayName,user_bp.bpname,user_bp.relation_type,isnull(SUM(b.p_money),0) as p_money
 from  dx_total.dbo.CoSettlementDeveloperOverview total
inner join(
select u_id,b.DisplayName as bpname,'' as DisplayName,relation_type 
from dx_base.dbo.jmp_user a
left join dx_base.dbo.CoBusinessPersonnel b on b.Id=a.relation_person_id
where relation_type=1 
union all 
select a.u_id,'' as bpname,c.DisplayName as DisplayName,a.relation_type from  
dx_base.dbo.jmp_user a 
left join dx_base.dbo.CoAgent c on c.Id=a.relation_person_id
where a.relation_type=2 
) as user_bp on total.DeveloperId=user_bp.u_id {0}
left join (select p_bill_id,p_money from dx_base.dbo.jmp_pays a,dx_base.dbo.jmp_BankPlaymoney b where a.p_batchnumber=b.b_batchnumber and a.p_state!=-1 and b.b_tradestate!=4
) as b on total.Id=b.p_bill_id {1}
group by Id,DeveloperId,DeveloperName,CreatedOn,SettlementDay,TotalAmount,ServiceFee,PortFee,DisplayName,bpname,relation_type,OriginalTotalAmount
) a
left join 
(select p_bill_id,p_money from dx_base.dbo.jmp_pays a,dx_base.dbo.jmp_BankPlaymoney b where a.p_batchnumber=b.b_batchnumber and a.p_state=1 and b.b_tradestate=1
) as b 
on a.Id=b.p_bill_id {1}
group by Id,DeveloperId,DeveloperName,CreatedOn,SettlementDay,TotalAmount,ServiceFee,PortFee,DisplayName,bpname,relation_type,OriginalTotalAmount,a.p_money", where1, where);

            List<JMP.MDL.CoSettlementDeveloperOverview> list = new List<JMP.MDL.CoSettlementDeveloperOverview>();
            JMP.MDL.CoSettlementDeveloperOverview model = new JMP.MDL.CoSettlementDeveloperOverview();

            list = bll_CoSDO.GetLists(sql.ToString(), orderby, pageIndexs, PageSize, out pageCount);
            #endregion

            #region 合计组装查询语句

            string countsql = string.Format(@"select  
ISNULL(SUM(TotalAmount),0) as TotalAmount,ISNULL(SUM(PortFee),0) as PortFee,ISNULL(SUM(KFZIncome),0) as KFZIncome,ISNULL(SUM(RefundAmount),0) as RefundAmount,
ISNULL(SUM(p_money),0) as p_money,ISNULL(SUM(yf_money),0) as yf_money,ISNULL(SUM(ketiMoney),0) as ketiMoney
from (
select Id,DeveloperId,DeveloperName,CreatedOn,SettlementDay,TotalAmount, OriginalTotalAmount,ServiceFee,PortFee,
DisplayName,bpname,relation_type,ISNULL((OriginalTotalAmount-TotalAmount),0) AS RefundAmount,ISNULL((TotalAmount-ServiceFee-PortFee),0) as KFZIncome,
(isnull(TotalAmount-ServiceFee-PortFee,0)-a.p_money) as ketiMoney,
a.p_money
,isnull(SUM(b.p_money),0) as yf_money 
from (
select Id,DeveloperId,DeveloperName,CreatedOn,SettlementDay,TotalAmount, OriginalTotalAmount,ServiceFee,PortFee,
user_bp.DisplayName,user_bp.bpname,user_bp.relation_type,isnull(SUM(b.p_money),0) as p_money
 from  dx_total.dbo.CoSettlementDeveloperOverview total
inner join(
select u_id,b.DisplayName as bpname,'' as DisplayName,relation_type 
from dx_base.dbo.jmp_user a
left join dx_base.dbo.CoBusinessPersonnel b on b.Id=a.relation_person_id
where relation_type=1 
union all 
select a.u_id,'' as bpname,c.DisplayName as DisplayName,a.relation_type from  
dx_base.dbo.jmp_user a 
left join dx_base.dbo.CoAgent c on c.Id=a.relation_person_id
where a.relation_type=2 
) as user_bp on total.DeveloperId=user_bp.u_id {0}
left join (select p_bill_id,p_money from dx_base.dbo.jmp_pays a,dx_base.dbo.jmp_BankPlaymoney b where a.p_batchnumber=b.b_batchnumber and a.p_state!=-1 and b.b_tradestate!=4
) as b on total.Id=b.p_bill_id {1}
group by Id,DeveloperId,DeveloperName,CreatedOn,SettlementDay,TotalAmount,ServiceFee,PortFee,DisplayName,bpname,relation_type,OriginalTotalAmount
) a
left join 
(select p_bill_id,p_money from dx_base.dbo.jmp_pays a,dx_base.dbo.jmp_BankPlaymoney b where a.p_batchnumber=b.b_batchnumber and a.p_state=1 and b.b_tradestate=1
) as b 
on a.Id=b.p_bill_id {1}
group by Id,DeveloperId,DeveloperName,CreatedOn,SettlementDay,TotalAmount,ServiceFee,PortFee,DisplayName,bpname,relation_type,OriginalTotalAmount,a.p_money) a", where1, where);
            if (list.Count > 0)
            {
                DataTable dt = bll_CoSDO.SelectSum(countsql);
                model = dt.Rows.Count > 0 ? JMP.TOOL.MdlList.ToModel<JMP.MDL.CoSettlementDeveloperOverview>(dt) : new JMP.MDL.CoSettlementDeveloperOverview();

            }

            #endregion

            ViewBag.show_fields = types;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            ViewBag.model = model;
            ViewBag.relation_type = relation_type;
            return View();
        }

        /// <summary>
        /// 应用名称账单详情
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [LoginCheckFilter(IsCheck = false)]
        public ActionResult BillList_AppName()
        {
            int id = int.Parse(Request["usrid"]);
            string time = string.IsNullOrEmpty(Request["time"]) ? "" : Request["time"];

            JMP.BLL.CoSettlementDeveloperAppDetails cobll = new JMP.BLL.CoSettlementDeveloperAppDetails();
            List<JMP.MDL.CoSettlementDeveloperAppDetails> colist = new List<JMP.MDL.CoSettlementDeveloperAppDetails>();

            DataTable dt = new DataTable();

            //查询账单详情
            dt = cobll.GetModelAppName(id, time).Tables[0];
            colist = JMP.TOOL.MdlList.ToList<JMP.MDL.CoSettlementDeveloperAppDetails>(dt);

            ViewBag.colist = colist;

            return PartialView();
        }

        /// <summary>
        /// 支付方式账单详情
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="SettlementDay"></param>
        /// <returns></returns>
        [HttpPost]
        [LoginCheckFilter(IsCheck = false)]
        public ActionResult BillList_Details()
        {
            JMP.BLL.CoSettlementDeveloperAppDetails cobll = new JMP.BLL.CoSettlementDeveloperAppDetails();
            List<JMP.MDL.CoSettlementDeveloperAppDetails> colist = new List<JMP.MDL.CoSettlementDeveloperAppDetails>();

            int appid = string.IsNullOrEmpty(Request["appid"]) ? 0 : int.Parse(Request["appid"]);
            string SettlementDay = string.IsNullOrEmpty(Request["time"]) ? "" : Request["time"];

            DataTable dt = new DataTable();

            //查询账单详情
            dt = cobll.GetModelPayType(appid, SettlementDay).Tables[0];
            colist = JMP.TOOL.MdlList.ToList<JMP.MDL.CoSettlementDeveloperAppDetails>(dt);

            ViewBag.colist = colist;

            return PartialView();
        }

        #endregion

        #region 开发者账单（汇总）

        /// <summary>
        /// 开发者账单管理
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult BillSummaryList()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量            
            string types = string.IsNullOrEmpty(Request["s_type"]) ? "1" : Request["s_type"];
            string searchKey = string.IsNullOrEmpty(Request["s_key"]) ? "" : Request["s_key"];
            string stime = string.IsNullOrEmpty(Request["s_begin"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_begin"];
            string etime = string.IsNullOrEmpty(Request["s_end"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_end"];
            int sort = string.IsNullOrEmpty(Request["s_sort"]) ? 1 : int.Parse(Request["s_sort"]);
            int relation_type = string.IsNullOrEmpty(Request["relation_type"]) ? -1 : int.Parse(Request["relation_type"]);

            #region 组装查询语句
            string where = "where 1=1";
            string where1 = "";
            string orderby = "";
            if (types == "1" && !string.IsNullOrEmpty(searchKey))
            {
                where += " and DeveloperName like '%" + searchKey + "%'";
            }
            if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
            {
                where += " and SettlementDay >='" + stime + "' and SettlementDay<='" + etime + "' ";
            }
            if (relation_type > -1)
            {
                where1 += " and user_bp.relation_type='" + relation_type + "'";
            }
            if (sort == 1)
            {
                orderby = "order by DeveloperId desc";

            }
            else
            {
                orderby = "order by DeveloperId asc";
            }

            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@"select DeveloperId,DeveloperName,DisplayName,bpname,relation_type ,ISNULL(SUM(TotalAmount),0) as TotalAmount,
isnull(SUM(ServiceFee),0) as ServiceFee,isnull(SUM(PortFee),0) as PortFee,
isnull(SUM(TotalAmount)-SUM(ServiceFee)-SUM(PortFee),0) as KFZIncome,ISNULL(SUM(p_money),0) as p_money,
ISNULL(SUM(yf_money),0) as yf_money,
ISNULL(SUM(OriginalTotalAmount)-SUM(TotalAmount),0) as RefundAmount,(isnull(SUM(TotalAmount)-SUM(ServiceFee)-SUM(PortFee),0)-ISNULL(SUM(p_money),0)) as ketiMoney
from (
select a.Id,DeveloperId,DeveloperName,DisplayName,bpname,relation_type,SettlementDay,TotalAmount,ServiceFee,PortFee,OriginalTotalAmount,a.p_money,
ISNULL(SUM(b.p_money),0) as yf_money
from
 (
 select a.Id,DeveloperId,DeveloperName,DisplayName,bpname,relation_type,SettlementDay,TotalAmount,ServiceFee,PortFee,OriginalTotalAmount,
 ISNULL(SUM(b.p_money),0) as p_money
from dx_total.dbo.CoSettlementDeveloperOverview as a
inner join(select u_id,b.DisplayName as bpname,'' as DisplayName,relation_type 
from dx_base.dbo.jmp_user a
left join dx_base.dbo.CoBusinessPersonnel b on b.Id=a.relation_person_id
where relation_type=1 
union all 
select a.u_id,'' as bpname,c.DisplayName as DisplayName,a.relation_type from  
dx_base.dbo.jmp_user a 
left join dx_base.dbo.CoAgent c on c.Id=a.relation_person_id
where a.relation_type=2 
) as user_bp on a.DeveloperId=user_bp.u_id {0}
left join  (select p_bill_id,p_money from dx_base.dbo.jmp_pays a,dx_base.dbo.jmp_BankPlaymoney b where a.p_batchnumber=b.b_batchnumber and a.p_state!=-1 and b.b_tradestate!=4
) as b on a.Id=b.p_bill_id
{1}
group by a.Id,DeveloperId,DeveloperName,DisplayName,bpname,relation_type,SettlementDay,TotalAmount,ServiceFee,PortFee,OriginalTotalAmount
) a
left join 
(select p_bill_id,p_money from dx_base.dbo.jmp_pays a,dx_base.dbo.jmp_BankPlaymoney b where a.p_batchnumber=b.b_batchnumber and a.p_state=1 and b.b_tradestate=1
) as b on a.Id=b.p_bill_id {1}
group by a.Id,DeveloperId,DeveloperName,DisplayName,bpname,relation_type,SettlementDay,TotalAmount,ServiceFee,PortFee,OriginalTotalAmount,a.p_money
) a
group by DeveloperId,DeveloperName,DisplayName,bpname,relation_type", where1, where);

            List<JMP.MDL.CoSettlementDeveloperOverview> list = new List<JMP.MDL.CoSettlementDeveloperOverview>();
            JMP.MDL.CoSettlementDeveloperOverview model = new JMP.MDL.CoSettlementDeveloperOverview();

            list = bll_CoSDO.GetLists(sql.ToString(), orderby, pageIndexs, PageSize, out pageCount);
            #endregion

            #region 合计组装查询语句

            string countsql = string.Format(@"select ISNULL(SUM(TotalAmount),0) as TotalAmount,isnull(SUM(ServiceFee),0) as ServiceFee,
isnull(SUM(PortFee),0) as PortFee,isnull(SUM(KFZIncome),0) as KFZIncome,ISNULL(SUM(p_money),0) as p_money,ISNULL(SUM(yf_money),0) as yf_money,
ISNULL(SUM(RefundAmount),0) as RefundAmount,(isnull(SUM(ketiMoney),0)) as ketiMoney 
from (select DeveloperId,DeveloperName,DisplayName,bpname,relation_type ,ISNULL(SUM(TotalAmount),0) as TotalAmount,
isnull(SUM(ServiceFee),0) as ServiceFee,isnull(SUM(PortFee),0) as PortFee,
isnull(SUM(TotalAmount)-SUM(ServiceFee)-SUM(PortFee),0) as KFZIncome,ISNULL(SUM(p_money),0) as p_money,
ISNULL(SUM(yf_money),0) as yf_money,
ISNULL(SUM(OriginalTotalAmount)-SUM(TotalAmount),0) as RefundAmount,(isnull(SUM(TotalAmount)-SUM(ServiceFee)-SUM(PortFee),0)-ISNULL(SUM(p_money),0)) as ketiMoney
from (
select a.Id,DeveloperId,DeveloperName,DisplayName,bpname,relation_type,SettlementDay,TotalAmount,ServiceFee,PortFee,OriginalTotalAmount,a.p_money,
ISNULL(SUM(b.p_money),0) as yf_money
from
 (select a.Id,DeveloperId,DeveloperName,DisplayName,bpname,relation_type,SettlementDay,TotalAmount,ServiceFee,PortFee,OriginalTotalAmount,
 ISNULL(SUM(b.p_money),0) as p_money
from dx_total.dbo.CoSettlementDeveloperOverview as a
inner join(select u_id,b.DisplayName as bpname,'' as DisplayName,relation_type 
from dx_base.dbo.jmp_user a	
left join dx_base.dbo.CoBusinessPersonnel b on b.Id=a.relation_person_id
where relation_type=1 
union all 
select a.u_id,'' as bpname,c.DisplayName as DisplayName,a.relation_type from  
dx_base.dbo.jmp_user a 
left join dx_base.dbo.CoAgent c on c.Id=a.relation_person_id
where a.relation_type=2 
) as user_bp on a.DeveloperId=user_bp.u_id {0}
left join  (select p_bill_id,p_money from dx_base.dbo.jmp_pays a,dx_base.dbo.jmp_BankPlaymoney b where a.p_batchnumber=b.b_batchnumber and a.p_state!=-1 and b.b_tradestate!=4
) as b on a.Id=b.p_bill_id
{1}
group by a.Id,DeveloperId,DeveloperName,DisplayName,bpname,relation_type,SettlementDay,TotalAmount,ServiceFee,PortFee,OriginalTotalAmount
) a
left join 
(select p_bill_id,p_money from dx_base.dbo.jmp_pays a,dx_base.dbo.jmp_BankPlaymoney b where a.p_batchnumber=b.b_batchnumber and a.p_state=1 and b.b_tradestate=1
) as b on a.Id=b.p_bill_id {1}
group by a.Id,DeveloperId,DeveloperName,DisplayName,bpname,relation_type,SettlementDay,TotalAmount,ServiceFee,PortFee,OriginalTotalAmount,a.p_money
) a
group by DeveloperId,DeveloperName,DisplayName,bpname,relation_type
) a ", where1, where);
            if (list.Count > 0)
            {
                DataTable dt = bll_CoSDO.SelectSum(countsql);
                model = dt.Rows.Count > 0 ? JMP.TOOL.MdlList.ToModel<JMP.MDL.CoSettlementDeveloperOverview>(dt) : new JMP.MDL.CoSettlementDeveloperOverview();
            }

            #endregion

            ViewBag.show_fields = types;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            ViewBag.model = model;
            ViewBag.relation_type = relation_type;

            return View();
        }

        /// <summary>
        /// 账单详情
        /// </summary>
        /// <param name="UserId">开发者ID</param>
        /// <param name="stime">开始日期</param>
        /// <param name="etime">结束日期</param>
        /// <returns></returns>
        [HttpPost]
        [LoginCheckFilter(IsCheck = false)]
        public ActionResult BillSummaryList_Details()
        {
            int UserId = string.IsNullOrEmpty(Request["usrid"]) ? 0 : int.Parse(Request["usrid"]);


            string stime = string.IsNullOrEmpty(Request["stime"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["stime"];
            string etime = string.IsNullOrEmpty(Request["etime"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["etime"];


            StringBuilder sql = new StringBuilder();

            string where = " and DeveloperId ='" + UserId + "'";
            string where1 = "where  SettlementDay >= '" + stime + "' and SettlementDay<= '" + etime + "' ";

            sql.AppendFormat(@"select a.Id,DeveloperId,SettlementDay,TotalAmount, OriginalTotalAmount ,ServiceFee,PortFee,a.p_money,  
isnull(SUM(b.p_money),0) as yf_money,(isnull(TotalAmount,0)-isnull(ServiceFee,0)-isnull(PortFee,0)) as KFZIncome, 
ISNULL((OriginalTotalAmount - TotalAmount), 0) AS RefundAmount,
((isnull(TotalAmount, 0) - isnull(ServiceFee, 0) - isnull(PortFee, 0))-a.p_money) as ketiMoney
from (
select Id,DeveloperId,SettlementDay,TotalAmount, OriginalTotalAmount ,ServiceFee,PortFee, 
isnull(SUM(b.p_money), 0) as p_money
 from dx_total.dbo.CoSettlementDeveloperOverview total
inner
 join(
select u_id, b.DisplayName as bpname, '' as DisplayName, relation_type
from dx_base.dbo.jmp_user a
left
join dx_base.dbo.CoBusinessPersonnel b on b.Id = a.relation_person_id
where relation_type = 1
union all
select a.u_id, '' as bpname, c.DisplayName as DisplayName, a.relation_type from
   dx_base.dbo.jmp_user a
   left join dx_base.dbo.CoAgent c on c.Id = a.relation_person_id
where a.relation_type = 2
) as user_bp on total.DeveloperId = user_bp.u_id {0}
left join 
(select p_bill_id,p_money from dx_base.dbo.jmp_pays a, dx_base.dbo.jmp_BankPlaymoney b where a.p_batchnumber = b.b_batchnumber and a.p_state != -1 and b.b_tradestate != 4
) as b on total.Id = b.p_bill_id {1}
group by Id,DeveloperId,SettlementDay,TotalAmount,ServiceFee,PortFee,OriginalTotalAmount
) a
left join 
(select p_bill_id,p_money from dx_base.dbo.jmp_pays a, dx_base.dbo.jmp_BankPlaymoney b where a.p_batchnumber = b.b_batchnumber and a.p_state =1 and b.b_tradestate=1
) as b on a.Id = b.p_bill_id {1}
group by Id,DeveloperId,SettlementDay,TotalAmount,ServiceFee,PortFee,OriginalTotalAmount,a.p_money
order by SettlementDay desc
", where, where1);

            List<JMP.MDL.CoSettlementDeveloperOverview> list = new List<JMP.MDL.CoSettlementDeveloperOverview>();

            DataTable dt = bll_CoSDO.SelectSum(sql.ToString());

            list = JMP.TOOL.MdlList.ToList<JMP.MDL.CoSettlementDeveloperOverview>(dt);

            ViewBag.list = list;

            return PartialView();
        }

        #endregion


        #region 代理商账单

        /// <summary>
        /// 代理商账单
        /// </summary>
        /// <returns></returns>
        public ActionResult BillAgentList()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量            
            string types = string.IsNullOrEmpty(Request["s_type"]) ? "1" : Request["s_type"];
            string searchKey = string.IsNullOrEmpty(Request["s_key"]) ? "" : Request["s_key"];
            string stime = string.IsNullOrEmpty(Request["s_begin"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_begin"];
            string etime = string.IsNullOrEmpty(Request["s_end"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_end"];
            int sort = string.IsNullOrEmpty(Request["s_sort"]) ? 1 : int.Parse(Request["s_sort"]);
            // string dept = System.Configuration.ConfigurationManager.AppSettings["RoleID"];

            #region 组装查询语句
            string where = "1=1";
            string orderby = "";
            if (types == "1" && !string.IsNullOrEmpty(searchKey))
            {
                where += " and DisplayName like '%" + searchKey + "%'";
            }
            if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
            {
                where += " and SettlementDay >='" + stime + "' and SettlementDay<='" + etime + "' ";
            }
            if (sort == 1)
            {
                orderby = " order by SettlementDay desc";

            }
            else
            {
                orderby = " order by SettlementDay asc";
            }

            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@"select SettlementDay,DisplayName,user_bp.agentid,user_bp.bpname,ISNULL(SUM(TotalAmount),0) as TotalAmount,ISNULL(SUM(ServiceFee),0) as ServiceFee,
ISNULL(SUM(AgentPushMoney),0) as AgentPushMoney
from  dx_total.dbo.CoSettlementDeveloperOverview total
inner join(
select a.u_id,c.DisplayName,c.Id as agentid,b.DisplayName as bpname
from  
dx_base.dbo.jmp_user a 
left join  dx_base.dbo.CoAgent c on c.Id=a.relation_person_id
left join  dx_base.dbo.CoBusinessPersonnel b on b.Id=a.relation_person_id and b.id=c.OwnerId
where  a.relation_type=2
) as user_bp on total.DeveloperId=user_bp.u_id 
where {0}  
group by SettlementDay,DisplayName,user_bp.agentid,user_bp.bpname ", where);

            List<JMP.MDL.CoSettlementDeveloperOverview> list = new List<JMP.MDL.CoSettlementDeveloperOverview>();
            JMP.MDL.CoSettlementDeveloperOverview model = new JMP.MDL.CoSettlementDeveloperOverview();

            list = bll_CoSDO.GetLists(sql.ToString(), orderby, pageIndexs, PageSize, out pageCount);

            #endregion

            #region 合计组装查询语句

            StringBuilder countsql = new StringBuilder();

            countsql.AppendFormat(@"select ISNULL(SUM(TotalAmount),0) as TotalAmount,ISNULL(SUM(ServiceFee),0) as ServiceFee,
ISNULL(SUM(AgentPushMoney),0) as AgentPushMoney
from  dx_total.dbo.CoSettlementDeveloperOverview total
inner join(
select a.u_id,DisplayName,Id as agentid
from  
dx_base.dbo.jmp_user a 
left join  dx_base.dbo.CoAgent c on c.Id=a.relation_person_id
where  a.relation_type=2
) as user_bp on total.DeveloperId=user_bp.u_id 
where {0} ", where);


            if (list.Count > 0)
            {
                DataTable dt = bll_CoSDO.SelectSum(countsql.ToString());
                model = dt.Rows.Count > 0 ? JMP.TOOL.MdlList.ToModel<JMP.MDL.CoSettlementDeveloperOverview>(dt) : new JMP.MDL.CoSettlementDeveloperOverview();

            }

            #endregion

            ViewBag.show_fields = types;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            ViewBag.model = model;
            return View();
        }

        /// <summary>
        /// 代理商账单详情
        /// </summary>
        /// <param name="agentid">代理商ID</param>
        /// <param name="SettlementDay">账单日期</param>
        /// <returns></returns>
        public ActionResult BillAgentList_Details(int agentid, string SettlementDay)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@"select * from dx_total.dbo.CoSettlementDeveloperOverview total
inner join(
select a.u_id from  
dx_base.dbo.jmp_user a 
left join  dx_base.dbo.CoAgent c on c.Id=a.relation_person_id
where  a.relation_type=2 and c.Id={0}
) as user_bp on total.DeveloperId=user_bp.u_id 
where SettlementDay>='{1}' and SettlementDay<='{1}'", agentid, SettlementDay);

            List<JMP.MDL.CoSettlementDeveloperOverview> list = new List<JMP.MDL.CoSettlementDeveloperOverview>();

            DataTable dt = bll_CoSDO.SelectSum(sql.ToString());

            list = JMP.TOOL.MdlList.ToList<JMP.MDL.CoSettlementDeveloperOverview>(dt);

            ViewBag.colist = list;

            return PartialView();
        }

        #endregion

        #region 商务账单

        /// <summary>
        /// 商务管理
        /// </summary>
        /// <returns></returns>
        public ActionResult BillBpList()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量            
            string types = string.IsNullOrEmpty(Request["s_type"]) ? "1" : Request["s_type"];
            string searchKey = string.IsNullOrEmpty(Request["s_key"]) ? "" : Request["s_key"];
            string stime = string.IsNullOrEmpty(Request["s_begin"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_begin"];
            string etime = string.IsNullOrEmpty(Request["s_end"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_end"];
            int sort = string.IsNullOrEmpty(Request["s_sort"]) ? 1 : int.Parse(Request["s_sort"]);

            #region 组装查询语句
            string where = "";
            string where1 = "";
            string orderby = "";
            if (types == "1" && !string.IsNullOrEmpty(searchKey))
            {
                where1 += " and user_bp.bpname like '%" + searchKey + "%'";
            }
            if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
            {
                where += " where SettlementDay >='" + stime + "' and SettlementDay<='" + etime + "' ";
            }
            if (sort == 1)
            {
                orderby = " order by SettlementDay desc";

            }
            else
            {
                orderby = " order by SettlementDay asc";
            }

            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@"
select SettlementDay,user_bp.bpname,user_bp.bpid,ISNULL(SUM(TotalAmount),0) as TotalAmount,ISNULL(SUM(ServiceFee),0) as ServiceFee,
ISNULL(SUM(BpPushMoney),0) as BpPushMoney
from  dx_total.dbo.CoSettlementDeveloperOverview total
inner join(
select u_id,b.DisplayName as bpname,b.Id as bpid
from dx_base.dbo.jmp_user a
left join dx_base.dbo.CoBusinessPersonnel b on  b.Id=a.relation_person_id
where relation_type=1 
union all 
select a.u_id,b.DisplayName as bpname,b.Id as bpid 
from  
dx_base.dbo.jmp_user a 
left join  dx_base.dbo.CoAgent c on c.Id=a.relation_person_id
left join dx_base.dbo.CoBusinessPersonnel b on  b.Id=c.OwnerId
where  a.relation_type=2 
) as user_bp on total.DeveloperId=user_bp.u_id {0}{1}
group by SettlementDay,user_bp.bpname,user_bp.bpid", where1, where);

            List<JMP.MDL.CoSettlementDeveloperOverview> list = new List<JMP.MDL.CoSettlementDeveloperOverview>();
            JMP.MDL.CoSettlementDeveloperOverview model = new JMP.MDL.CoSettlementDeveloperOverview();

            list = bll_CoSDO.GetLists(sql.ToString(), orderby, pageIndexs, PageSize, out pageCount);
            #endregion

            #region 合计组装查询语句

            StringBuilder countsql = new StringBuilder();

            countsql.AppendFormat(@"select ISNULL(SUM(TotalAmount),0) as TotalAmount,ISNULL(SUM(ServiceFee),0) as ServiceFee,
ISNULL(SUM(BpPushMoney),0) as BpPushMoney
from  dx_total.dbo.CoSettlementDeveloperOverview total
inner join(
select u_id,b.DisplayName as bpname,b.Id as bpid
from dx_base.dbo.jmp_user a
left join dx_base.dbo.CoBusinessPersonnel b on  b.Id=a.relation_person_id
where relation_type=1 
union all 
select a.u_id,b.DisplayName as bpname,b.Id as bpid 
from  
dx_base.dbo.jmp_user a 
left join  dx_base.dbo.CoAgent c on c.Id=a.relation_person_id
left join dx_base.dbo.CoBusinessPersonnel b on  b.Id=c.OwnerId
where  a.relation_type=2 
) as user_bp on total.DeveloperId=user_bp.u_id {0}{1}", where1, where);

            if (list.Count > 0)
            {
                DataTable dt = bll_CoSDO.SelectSum(countsql.ToString());
                model = dt.Rows.Count > 0 ? JMP.TOOL.MdlList.ToModel<JMP.MDL.CoSettlementDeveloperOverview>(dt) : new JMP.MDL.CoSettlementDeveloperOverview();

            }

            #endregion

            ViewBag.show_fields = types;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            ViewBag.model = model;
            return View();
        }

        /// <summary>
        /// 商务账单详情
        /// </summary>
        /// <param name="bpid">商务ID</param>
        /// <param name="SettlementDay">日期</param>
        /// <returns></returns>
        public ActionResult BillBpList_Details(int bpid, string SettlementDay)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@"
select  total.*,user_bp.DisplayName,user_bp.relation_type
from  dx_total.dbo.CoSettlementDeveloperOverview total
inner join(
select u_id,u_realname,'' as DisplayName,relation_type 
from dx_base.dbo.jmp_user  where relation_type=1 and relation_person_id={0}
union all 
select a.u_id,a.u_realname,c.DisplayName,a.relation_type from  
dx_base.dbo.jmp_user a 
left join dx_base.dbo.CoAgent c on c.Id=a.relation_person_id
where a.relation_type=2 and c.OwnerId={0}
) as user_bp on total.DeveloperId=user_bp.u_id
where SettlementDay>='{1}' and SettlementDay<='{1}'", bpid, SettlementDay);

            List<JMP.MDL.CoSettlementDeveloperOverview> list = new List<JMP.MDL.CoSettlementDeveloperOverview>();

            DataTable dt = bll_CoSDO.SelectSum(sql.ToString());

            list = JMP.TOOL.MdlList.ToList<JMP.MDL.CoSettlementDeveloperOverview>(dt);

            ViewBag.colist = list;

            return PartialView();
        }

        #endregion

        #region 银卡打款管理

        public ActionResult BankPlaymoneyList()
        {
            JMP.BLL.jmp_user_report bll_report = new JMP.BLL.jmp_user_report();
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量            
            string types = string.IsNullOrEmpty(Request["s_type"]) ? "1" : Request["s_type"];
            string searchKey = string.IsNullOrEmpty(Request["s_key"]) ? "" : Request["s_key"];
            string stime = string.IsNullOrEmpty(Request["s_begin"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["s_begin"];
            string etime = string.IsNullOrEmpty(Request["s_end"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["s_end"];
            string s_paytime = string.IsNullOrEmpty(Request["s_paytime"]) ? "1" : Request["s_paytime"];//日期查询方式
            int sort = string.IsNullOrEmpty(Request["s_sort"]) ? 1 : int.Parse(Request["s_sort"]);
            string b_tradestate = string.IsNullOrEmpty(Request["check"]) ? "" : Request["check"];//交易状态
            string payfashion = string.IsNullOrEmpty(Request["payfashion"]) ? "" : Request["payfashion"];//付款方式
            #region 组装查询语句
            string where = "where 1=1";
            string orderby = "";
            if (!string.IsNullOrEmpty(types.ToString()))
            {
                if (!string.IsNullOrEmpty(searchKey))
                {
                    if (types == "0")
                        where += string.Format(" and  b_batchnumber like '%{0}%'", searchKey);
                    else if (types == "1")
                        where += string.Format(" and b_number like '%{0}%'", searchKey);
                    else if (types == "2")
                        where += string.Format(" and b_tradeno like '%{0}%'", searchKey);
                    else if (types == "3")
                        where += string.Format(" and b.u_name like '%{0}%'", searchKey);
                    else if (types == "4")
                        where += string.Format(" and c.u_realname like '%{0}%'", searchKey);
                }
            }

            if (!string.IsNullOrEmpty(s_paytime))
            {
                if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
                {
                    if (s_paytime == "1")
                    {
                        where += " and b_date >='" + stime + " 00:00:00'  and b_date<='" + etime + " 23:59:59' ";
                    }
                    else if (s_paytime == "2")
                    {
                        where += " and a.b_paydate >='" + stime + " 00:00:00'  and a.b_paydate<='" + etime + " 23:59:59' ";
                    }
                }

            }
            if (sort == 1)
            {
                orderby = " order by b_id desc";
            }
            else
            {
                orderby = " order by b_id asc";
            }
            if (!string.IsNullOrEmpty(b_tradestate))
            {
                where += string.Format(" and a.b_tradestate={0}", b_tradestate);
            }
            if (!string.IsNullOrEmpty(payfashion))
            {
                where += string.Format(" and a.b_payfashion={0}", payfashion);
            }

            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@"select * from 
(select  a.*,b.u_bankname,b.u_banknumber,b.u_name ,c.u_realname,c.u_phone,d.p_InterfaceName,d.p_MerchantNumber,b.u_flag from jmp_BankPlaymoney  a left join jmp_userbank  b on a.b_bankid=b.u_id left join 
 jmp_user c on b.u_userid=c.u_id left join PayForAnotherInfo d on a.b_payforanotherId=d.p_Id {0} ) a 
left join (select p_state,p_batchnumber from jmp_pays group by p_batchnumber,p_state) b on a.b_batchnumber=b.p_batchnumber
 ", where);
            JMP.BLL.jmp_BankPlaymoney bll = new JMP.BLL.jmp_BankPlaymoney();
            List<JMP.MDL.jmp_BankPlaymoney> list = new List<JMP.MDL.jmp_BankPlaymoney>();
            JMP.MDL.jmp_BankPlaymoney model = new JMP.MDL.jmp_BankPlaymoney();
            list = bll.GetLists(sql.ToString(), orderby, pageIndexs, PageSize, out pageCount);

            StringBuilder sqlCount = new StringBuilder();

            sqlCount.AppendFormat(@"select sum(b_money) as countmoney from 
(select  a.*,b.u_bankname,b.u_banknumber,b.u_name ,c.u_realname,c.u_phone,d.p_InterfaceName,d.p_MerchantNumber from jmp_BankPlaymoney  a left join jmp_userbank  b on a.b_bankid=b.u_id left join 
 jmp_user c on b.u_userid=c.u_id left join PayForAnotherInfo d on a.b_payforanotherId=d.p_Id {0} ) a 
left join (select p_state,p_batchnumber from jmp_pays group by p_batchnumber,p_state) b on a.b_batchnumber=b.p_batchnumber", where);
            DataTable ddt = new DataTable();//用于统计
            if (list.Count > 0)
            {
                ddt = bll.CountSect(sqlCount.ToString());
            }

            #endregion

            StringBuilder sqlinfo = new StringBuilder();
            sqlinfo.AppendFormat(@";WITH a AS( 
 select a.*,b.SettlementDay 
from [dbo].[jmp_pays]  a 
left join [dx_total].[dbo].[CoSettlementDeveloperOverview]  b on a.p_bill_id=b.Id
), b as(
  select a.Id,(TotalAmount-ServiceFee-PortFee) as KFZIncome,
isnull(SUM(b.p_money),0) as p_moneys
from  dx_total.dbo.[CoSettlementDeveloperOverview] as a
left join (select * from dx_base.dbo.jmp_pays a,dx_base.dbo.jmp_BankPlaymoney b 
 where a.p_batchnumber=b.b_batchnumber and a.p_state!=-1 and b.b_tradestate!=4
) as b on a.Id=b.p_bill_id 
group by a.Id,TotalAmount,ServiceFee,PortFee
)
select  *  from  a 
left join b on a.p_bill_id=b.Id");
            JMP.BLL.jmp_pays bllinfo = new JMP.BLL.jmp_pays();
            List<JMP.MDL.jmp_pays> listinfo = new List<JMP.MDL.jmp_pays>();
            DataTable dt = bllinfo.SelectList(sqlinfo.ToString());
            listinfo = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_pays>(dt);
            ViewBag.colist = listinfo;

            ViewBag.s_paytime = s_paytime;
            ViewBag.show_fields = types;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            ViewBag.ddt = ddt;
            ViewBag.model = model;
            ViewBag.scheck = b_tradestate;
            ViewBag.payfashion = payfashion;
            ViewBag.btnstr = GetVoidHtml1();
            return View();
        }

        /// <summary>
        /// 判断权限
        /// </summary>
        private string GetVoidHtml1()
        {
            string tempStr = string.Empty;
            JMP.BLL.jmp_limit bll_limit = new JMP.BLL.jmp_limit();
            string u_id = UserInfo.UserId.ToString();
            int r_id = UserInfo.UserRid;
            //一键解冻
            bool getUidT = bll_limit.GetLocUserLimitVoids("/Financial/BankPlayAuditing", u_id, r_id);
            if (getUidT)
                tempStr += "<li onclick=\"doAll()\"><i class='fa fa-check-square-o'>批量审核</i></li>";

            return tempStr;
        }

        /// <summary>
        /// 冻结方法
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult BankPlayDongJie()
        {
            JMP.BLL.jmp_BankPlaymoney bankBll = new JMP.BLL.jmp_BankPlaymoney();

            object retJson = new { success = 0, msg = "操作失败，请联系管理员！" };

            string b_batchnumber = string.IsNullOrEmpty(Request["b_batchnumber"]) ? "" : Request["b_batchnumber"];

            if (bankBll.UpdateBankPayTradestate(b_batchnumber, 6, ""))
            {
                retJson = new { success = 1, msg = "冻结成功！" };
            }
            else
            {
                retJson = new { success = 0, msg = "冻结失败！" };
            }
            return Json(retJson);


        }


        /// <summary>
        /// 自动打款审核页面
        /// </summary>
        /// <returns></returns>
        public ActionResult BankPlayAuditing()
        {
            string batchnumber = string.IsNullOrEmpty(Request["ids"]) ? "" : Request["ids"].ToString();

            ViewBag.batchnumber = batchnumber;
            return View();
        }

        /// <summary>
        /// 审核提款信息
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult CheckAuditing()
        {
            object retJson = new { success = 0, msg = "操作失败，请联系管理员！" };

            JMP.MDL.PayForAnotherInfo pay = new JMP.MDL.PayForAnotherInfo();
            JMP.BLL.PayForAnotherInfo bll_pay = new PayForAnotherInfo();

            //批次号
            string batchnumber = string.IsNullOrEmpty(Request["ids"]) ? "" : Request["ids"].ToString();
            if (batchnumber.CompareTo("On") > 0)
            {
                batchnumber = batchnumber.Substring(3);
            }
            //审核状态
            int p_state = string.IsNullOrEmpty(Request["p_state"]) ? 0 : int.Parse(Request["p_state"]);
            //用户姓名
            string name = UserInfo.UserName;
            //操作备注
            string remarks = string.IsNullOrEmpty(Request["b_remark"]) ? "" : Request["b_remark"];
            //通道ID
            int payId = int.Parse(string.IsNullOrEmpty(Request["payId"]) ? "0" : Request["payId"]);
            if (payId <= 0 && p_state == 1)
            {
                retJson = new { success = 0, msg = "通道ID小于0！" };
            }

            //审核提现申请
            JMP.BLL.jmp_BankPlaymoney bll = new JMP.BLL.jmp_BankPlaymoney();
            bool flag = bll.UpdateState(batchnumber, p_state, name, payId);

            if (flag)
            {
                //同意的
                if (p_state == 1)
                {

                    //查询通道信息
                    pay = bll_pay.GetPayInfoId(payId);
                    HandelPay handelPay = new HandelPay
                    {
                        batchnumber = batchnumber,
                        MerchantNumber = pay.p_MerchantNumber,
                        PrivateKey = pay.p_PrivateKey,
                        PublicKey = pay.p_PublicKey
                    };

                    //记录日志
                    string info = "审核提款信息批号为（" + batchnumber + "）的状态为" + p_state + "";
                    Logger.OperateLog("审核提款信息状态", info);
                    bool flag1 = false;
                    #region 根据代付类型调用对应的代付接口
                    switch (pay.ChannelIdentifier)
                    {
                        //代清分银联代付
                        case "DQFYL":
                            HandelChinPay handelChinPay = new HandelChinPay();
                            flag1 = handelChinPay.HandleChinaPay(handelPay);
                            break;
                    }
                    if (flag1)
                    {
                        retJson = new { success = 1, msg = "审核成功！" };

                    }
                    else
                    {
                        retJson = new { success = 0, msg = "审核失败！" };
                    }

                    #endregion
                }
                else
                {

                    if (batchnumber.Contains(","))
                    {
                        #region 批量循环提交

                        string[] strarr = batchnumber.Split(',');
                        batchnumber = "";
                        foreach (var item in strarr)
                        {
                            #region 审核不同意处理逻辑

                            //备注
                            string remark = remarks + "|" + "操作人：" + UserInfo.UserName + ",于时间：" + DateTime.Now + ",对批次号：" + item + ",进行代付自动打款,操作状态:审核未通过，";

                            //操作手动打款状态方法
                            flag = bll.UpdateBankPayHandMovement(item, 5, "", "", DateTime.Now, 0, remark);

                            if (!flag)
                            {
                                Logger.OperateLog("修改银行打款对接表状态失败", "批次号：" + item + "交易状态:" + 5 + ",代付自动打款");

                            }

                            //记录日志
                            string info = "审核提款信息批号为（" + batchnumber + "）的状态为" + p_state + "";
                            Logger.OperateLog("审核提款信息状态", info);
                            retJson = new { success = 1, msg = "审核成功！" };

                            #endregion

                        }

                        #endregion
                    }
                    else
                    {

                        #region 审核不同意处理逻辑

                        //备注
                        string remark = remarks + "|" + "操作人：" + UserInfo.UserName + ",于时间：" + DateTime.Now + ",对批次号：" + batchnumber + ",进行代付自动打款,操作状态:审核未通过，";

                        //操作手动打款状态方法
                        flag = bll.UpdateBankPayHandMovement(batchnumber, 5, "", "", DateTime.Now, 0, remark);

                        if (!flag)
                        {
                            Logger.OperateLog("修改银行打款对接表状态失败", "批次号：" + batchnumber + "交易状态:" + 5 + ",代付自动打款");

                        }

                        //记录日志
                        string info = "审核提款信息批号为（" + batchnumber + "）的状态为" + p_state + "";
                        Logger.OperateLog("审核提款信息状态", info);
                        retJson = new { success = 1, msg = "审核成功！" };

                        #endregion
                    }



                }


            }
            else
            {
                retJson = new { success = 0, msg = "审核失败！" };
            }

            return Json(retJson);
        }

        /// <summary>
        /// 查询代付信息
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult SelectPay()
        {
            object retJson = new { success = 0, msg = "操作失败，请联系管理员！" };

            JMP.BLL.jmp_BankPlaymoney bll = new JMP.BLL.jmp_BankPlaymoney();

            string number = string.IsNullOrEmpty(Request["number"]) ? "" : Request["number"];
            string paydate = string.IsNullOrEmpty(Request["paydate"]) ? "" : Request["paydate"];
            int pid = int.Parse(string.IsNullOrEmpty(Request["pid"]) ? "" : Request["pid"]);

            if (number == "" || paydate == "" || pid < 0)
            {
                retJson = new { success = 0, msg = "信息不完整无法查询，请联系管理员！" };
            }
            else
            {
                //调用查询接口

                JMP.MDL.PayForAnotherInfo pay = new JMP.MDL.PayForAnotherInfo();
                JMP.BLL.PayForAnotherInfo bll_pay = new PayForAnotherInfo();
                pay = bll_pay.GetPayInfoId(pid);
                SelectChinaPayParameter PayParameter = new SelectChinaPayParameter
                {
                    merSeqId = number,
                    merDate = paydate,
                    pid = pay.p_Id,
                    p_PublicKey = pay.p_PublicKey,
                    merId = pay.p_MerchantNumber,
                    PrivateKey = pay.p_PrivateKey
                };
                SelectChinaPay ChinaPay = new SelectChinaPay();

                bool flag = ChinaPay.SubmitSelectChinaPay(PayParameter);

                if (flag)
                {
                    retJson = new { success = 1, msg = "查询成功！" };

                }
                else
                {
                    retJson = new { success = 0, msg = "查询失败！" };
                }

            }

            return Json(retJson);
        }

        /// <summary>
        /// 手动打款审核页面
        /// </summary>
        /// <returns></returns>
        public ActionResult HandMovementAuditing()
        {
            string batchnumber = string.IsNullOrEmpty(Request["ids"]) ? "" : Request["ids"].ToString();
            ViewBag.batchnumber = batchnumber;
            return View();
        }

        /// <summary>
        /// 手动打款审核方法
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult CheckHandMovementAuditing()
        {
            object retJson = new { success = 0, msg = "操作失败，请联系管理员！" };

            //批次号
            string batchnumber = string.IsNullOrEmpty(Request["ids"]) ? "" : Request["ids"];
            //审核状态
            int p_state = string.IsNullOrEmpty(Request["p_state"]) ? 0 : int.Parse(Request["p_state"]);
            //手动打款交易流水号
            string b_tradeno = string.IsNullOrEmpty(Request["b_tradeno"]) ? "" : Request["b_tradeno"];
            //用户姓名
            string name = UserInfo.UserName;
            //操作备注
            string remarks = string.IsNullOrEmpty(Request["b_remark"]) ? "" : Request["b_remark"];

            JMP.BLL.jmp_pays paysbll = new jmp_pays();
            JMP.BLL.jmp_BankPlaymoney bankbll = new jmp_BankPlaymoney();
            //根据批次号查询提款表
            JMP.MDL.jmp_pays pays = paysbll.GetPaysModel(batchnumber);
            //根据批次号查询打款对接表状态
            JMP.MDL.jmp_BankPlaymoney bankPlay = bankbll.GetBankPlaymoneyModel(batchnumber);

            if (pays.p_state == 0 || bankPlay.b_tradestate == -1)
            {
                //审核提现申请
                JMP.BLL.jmp_BankPlaymoney bll = new JMP.BLL.jmp_BankPlaymoney();
                bool flag = bll.UpdateState(batchnumber, p_state, name, 0);

                if (flag)
                {
                    //同意的
                    if (p_state == 1)
                    {
                        //记录日志
                        string info = "审核提款信息批号为（" + batchnumber + "）的状态为" + p_state + "";
                        Logger.OperateLog("审核提款信息状态", info);

                        #region 手动打款处理逻辑

                        Random r = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
                        //流水号
                        string merSeqId = DateTime.Now.ToString("yyMMddHHmmss") + r.Next(1111, 9999).ToString();
                        //备注（默认）
                        string remark = "操作人：" + UserInfo.UserName + ",于时间：" + DateTime.Now + ",对批次号：" + batchnumber + ",进行财务手动打款";


                        //操作手动打款状态方法
                        flag = bll.UpdateBankPayHandMovement(batchnumber, 1, merSeqId, b_tradeno, DateTime.Now, 1, remark);

                        if (flag)
                        {
                            retJson = new { success = 1, msg = "审核成功！" };

                        }
                        else
                        {
                            retJson = new { success = 0, msg = "审核失败！" };
                        }


                        #endregion
                    }
                    else
                    {
                        #region 不同意处理逻辑

                        //备注
                        string remark = remarks + "|" + "操作人：" + UserInfo.UserName + ",于时间：" + DateTime.Now + ",对批次号：" + batchnumber + ",进行财务手动打款,操作状态:审核未通过，";

                        //操作手动打款状态方法
                        flag = bll.UpdateBankPayHandMovement(batchnumber, 5, "", "", DateTime.Now, 0, remark);

                        if (!flag)
                        {
                            Logger.OperateLog("修改银行打款对接表状态失败", "批次号：" + batchnumber + "交易状态:" + 5 + ",财务手动打款");

                        }

                        //记录日志
                        string info = "审核提款信息批号为（" + batchnumber + "）的状态为" + p_state + "";
                        Logger.OperateLog("审核提款信息状态", info);
                        retJson = new { success = 1, msg = "审核成功！" };

                        #endregion
                    }


                }
                else
                {
                    retJson = new { success = 0, msg = "审核失败！" };
                }
            }
            else
            {
                retJson = new { success = 0, msg = "此条数据已被处理，请刷新界面！" };
            }

            return Json(retJson);
        }

        /// <summary>
        /// 退款页面
        /// </summary>
        /// <returns></returns>
        public ActionResult RefundAuditing()
        {
            string batchnumber = string.IsNullOrEmpty(Request["ids"]) ? "" : Request["ids"].ToString();
            ViewBag.batchnumber = batchnumber;
            return View();
        }

        /// <summary>
        /// 退款
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult CheckRefundAuditing()
        {
            object retJson = new { success = 0, msg = "操作失败，请联系管理员！" };
            //批次号
            string batchnumber = string.IsNullOrEmpty(Request["ids"]) ? "" : Request["ids"];
            //操作备注
            string remarks = string.IsNullOrEmpty(Request["b_remark"]) ? "" : Request["b_remark"];
            //备注
            string remark = remarks + "|" + "操作人：" + UserInfo.UserName + ",于时间：" + DateTime.Now + ",对批次号：" + batchnumber + ",进行退款,操作状态:已退款，";

            JMP.BLL.jmp_BankPlaymoney bll = new JMP.BLL.jmp_BankPlaymoney();

            bool flag = bll.UpdateBankPayTradestate(batchnumber, 4, remark);

            if (flag)
            {
                retJson = new { success = 1, msg = "退款成功！" };

            }
            else
            {
                retJson = new { success = 0, msg = "退款失败！" };
            }

            return Json(retJson);


        }

        /// <summary>
        /// 通道信息
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult SelectionChannel()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量

            int PayType = string.IsNullOrEmpty(Request["PayType"]) ? 0 : Int32.Parse(Request["PayType"]);//查询类型
            string searchKey = string.IsNullOrEmpty(Request["searchKey"]) ? "" : Request["searchKey"];//关键字

            //查询所有
            PayForAnotherInfoList = PayForAnotherBll.PayForAnotherIsEnabledList(PayType, searchKey, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = PayForAnotherInfoList;
            ViewBag.PayType = PayType;
            ViewBag.searchKey = searchKey;

            return View();
        }


        #endregion

        #region


        /// <summary>
        /// 提款管理
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult PaymentList()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量            
            string types = string.IsNullOrEmpty(Request["s_type"]) ? "1" : Request["s_type"];
            string searchKey = string.IsNullOrEmpty(Request["s_key"]) ? "" : Request["s_key"];
            string stime = string.IsNullOrEmpty(Request["s_begin"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["s_begin"];
            string etime = string.IsNullOrEmpty(Request["s_end"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["s_end"];
            string state = string.IsNullOrEmpty(Request["s_state"]) ? "" : Request["s_state"];
            int sort = string.IsNullOrEmpty(Request["s_sort"]) ? 1 : int.Parse(Request["s_sort"]);
            string searchTotal = string.IsNullOrEmpty(Request["s_field"]) ? "" : Request["s_field"];

            string where = "where 1=1";
            string orderby = "order by ";
            #region 组装查询语句
            if (!string.IsNullOrEmpty(types))
            {
                if (!string.IsNullOrEmpty(searchKey))
                {
                    //if (types == "0")
                    //    where += " and t_bill.b_ctime like '%" + searchKey + "%'";
                    //else if (types == "1")
                    //    where += " and t_user.u_realname like '%" + searchKey + "%'";
                    //else if (types == "2")
                    //    where += " and t_bill.b_stime like '%" + searchKey + "%'";
                }
            }
            if (!string.IsNullOrEmpty(searchTotal))
            {
                if (searchTotal == "0")
                    orderby += "p_money ";
                else if (searchTotal == "2")
                    orderby = "p_paytime ";
                else
                    orderby += "p_applytime ";
            }
            else
            {
                orderby += "p_applytime ";
            }
            orderby += (sort == 1 ? "desc" : "asc");

            if (!string.IsNullOrEmpty(state))
                where += " and t_pay.p_state=" + state;
            else
                where += " and t_pay.p_state=0";

            where += " and t_pay.p_applytime>='" + stime + " 00:00:00' and t_pay.p_applytime<='" + etime + " 23:59:59'";
            string sql = string.Format(@"select t_pay.*,t_user.u_account,t_user.u_name,t_user.u_bankname from jmp_pay t_pay left join jmp_bill t_bill on substring(t_pay.p_bill_id,1,1)=t_bill.b_id left join jmp_user t_user on t_bill.b_user_id=t_user.u_id {0}", where);

            DataTable dt = bll_pay.GetLists(sql, orderby, pageIndexs, PageSize, out pageCount);
            #endregion

            ViewBag.show_fields = types;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            return View(dt);
        }

        /// <summary>
        /// 更新交易号
        /// </summary>
        /// <param name="pid">提款id</param>
        /// <param name="dealno">交易号</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult AjaxUpdateDealno(string pid, string dealno)
        {
            bool flag = bll_pay.Update(int.Parse(pid), dealno);
            return Json(new { success = flag ? 1 : 0, msg = flag ? "更新成功！" : "更新失败！" });
        }

        /// <summary>
        /// 手动提款管理
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult ManualList()
        {
            string locUrl = "";
            bool getUidT = bll_limit.GetLocUserLimitVoids("/Financial/ManuaTk", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//本地管理员一键启用
            if (getUidT)
            {
                locUrl += " <a href=\"javascript:\" class=\"icon icon-add\" onclick=\"javascript:Manual()\">申请提款</a>";
            }
            ViewBag.locUrl = locUrl;
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量            
            string userid = string.IsNullOrEmpty(Request["userid"]) ? "0" : Request["userid"];//用户名
            string username = string.IsNullOrEmpty(Request["username"]) ? "" : Request["username"];//用户id
            string state = string.IsNullOrEmpty(Request["s_state"]) ? "" : Request["s_state"];//提款状态
            int sort = string.IsNullOrEmpty(Request["s_sort"]) ? 1 : int.Parse(Request["s_sort"]);//排序
            List<JMP.MDL.jmp_bill> list = new List<JMP.MDL.jmp_bill>();
            string sql = "select  a.*,b.u_account,b.u_name,b.u_realname from jmp_bill a left join jmp_user b on a.b_user_id=b.u_id where a.b_user_id='" + userid + "' and b.u_drawing='1'  ";
            if (state == "1")
            {
                sql += " and a.b_state='1' ";
            }
            else
            {
                sql += " and a.b_state='0' ";
            }

            string orderby = sort == 1 ? " order by b_ctime desc" : " order by b_ctime ";
            DataTable dt = bll_pay.GetLists(sql, orderby, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            return View(dt);
        }

        /// <summary>
        /// 查询开发者用户用于选择
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult UserList()
        {
            List<JMP.MDL.jmp_user> list = new List<JMP.MDL.jmp_user>();
            JMP.BLL.jmp_user bll = new JMP.BLL.jmp_user();
            #region 初始化
            //获取请求参数
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量
            string type = string.IsNullOrEmpty(Request["stype"]) ? "0" : Request["stype"];//查询条件类型
            string sea_name = string.IsNullOrEmpty(Request["skeys"]) ? "" : Request["skeys"];//查询条件值
            string category = string.IsNullOrEmpty(Request["scategory"]) ? "" : Request["scategory"];//认证类型
            int px = string.IsNullOrEmpty(Request["s_sort"]) ? 0 : Int32.Parse(Request["s_sort"]);//排序
            //获取用户列表
            string where = " where 1=1 and u_auditstate='1' and u_state='1'  and u_drawing='1' ";
            if (!string.IsNullOrEmpty(type.ToString()) && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case "0":
                        where += string.Format(" and u_email like '%{0}%'", sea_name);
                        break;
                    case "1":
                        where += string.Format(" and u_phone like '%{0}%'", sea_name);
                        break;
                    case "3":
                        where += string.Format(" and u_idnumber like '%{0}%'", sea_name);
                        break;
                    case "6":
                        where += string.Format(" and u_blicensenumber like '%{0}%'", sea_name);
                        break;
                }
            }
            if (!string.IsNullOrEmpty(category))
            {
                where += string.Format(" and u_category={0}", category);
            }
            string Order = " order by u_id " + (px == 0 ? "" : " desc ") + " ";
            string query = "select * from jmp_user" + where;
            list = bll.GetLists(query, Order, pageIndexs, PageSize, out pageCount);
            //返回
            ViewBag.CurrPage = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.PageCount = pageCount;
            ViewBag.stype = type;
            ViewBag.skeys = sea_name;
            ViewBag.scategory = category;
            ViewBag.s_sort = px;
            ViewBag.list = list;
            #endregion
            return View();
        }

        /// <summary>
        /// 提款申请操作
        /// </summary>
        /// <returns></returns>
        public JsonResult ManuaTk()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            string str = Request["ids"];
            if (str.CompareTo("On") > 0)
            {
                str = str.Substring(3);
            }
            JMP.MDL.jmp_bill mo = new JMP.MDL.jmp_bill();
            JMP.BLL.jmp_bill bll = new JMP.BLL.jmp_bill();
            mo = bll.GetselectSum(str);
            if (mo != null)
            {
                JMP.MDL.jmp_pay paymo = new JMP.MDL.jmp_pay();
                JMP.BLL.jmp_pay paybll = new JMP.BLL.jmp_pay();
                paymo.p_money = 0;
                paymo.p_bill_id = str;
                paymo.p_state = 0;
                paymo.p_applytime = DateTime.Now;
                paymo.p_tradeno = paybll.SelectBh();
                paymo.p_paytime = DateTime.Now;
                int cg = paybll.Add(paymo);
                if (cg > 0)
                {
                    if (bll.UpdateLocUserState(str))
                    {

                        Logger.OperateLog("手动申请提款", "申请提款金额：" + 0 + "申请提款编号：" + str);

                        retJson = new { success = 1, msg = "申请成功！" };
                    }
                    else
                    {
                        retJson = new { success = 0, msg = "申请失败！" };
                    }
                }
                else
                {
                    retJson = new { success = 0, msg = "申请失败！" };
                }
            }
            else
            {
                retJson = new { success = 0, msg = "提款金额不能小于零！" };
            }
            return Json(retJson);
        }

        /// <summary>
        /// 退款管理
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult RefundList()
        {
            int UserRoleId = UserInfo.UserRoleId;
            string UserId = UserInfo.UserId.ToString();
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            string auditstate = string.IsNullOrEmpty(Request["auditstate"]) ? "" : Request["auditstate"];//审核状态
            int type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);//查询条件选择
            string stime = string.IsNullOrEmpty(Request["r_begin"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["r_begin"];
            string etime = string.IsNullOrEmpty(Request["r_end"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["r_end"];
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            List<JMP.MDL.jmp_refund> list = new List<JMP.MDL.jmp_refund>();
            JMP.BLL.jmp_refund bll = new JMP.BLL.jmp_refund();
            //int RoleID = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RoleID"]);
            list = bll.SelectList(UserRoleId, UserId, auditstate, sea_name, type, searchDesc, stime, etime, pageIndexs, PageSize, out pageCount, 0);
            ViewBag.searchDesc = searchDesc;
            ViewBag.type = type;
            ViewBag.sea_name = sea_name;
            ViewBag.stime = stime;
            ViewBag.etime = etime;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            ViewBag.auditstate = auditstate;
            ViewBag.locUrl = GetVoidHtml();
            return View();
        }

        public string GetVoidHtml()
        {
            string locUrl = "";
            string u_id = UserInfo.UserId.ToString();
            int r_id = int.Parse(UserInfo.UserRoleId.ToString());
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/Financial/InsertUpdateRefund", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加管理员
            if (getlocuserAdd)
            {
                locUrl += "<li onclick=\"AddAPPlog()\"><i class='fa fa-plus'></i>退款申请</li>";
            }
            var bulkAssignToMerchant = bll_limit.GetLocUserLimitVoids("/appuser/bulkassign", u_id, r_id);
            if (bulkAssignToMerchant)
            {
                locUrl += "<li onclick=\"bulkassign()\"><i class='fa fa-check-square-o'></i>审核退款</li>";
            }
            return locUrl;
        }


        /// <summary>
        /// 退款数据导出
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public FileContentResult RefundDc()
        {
            #region 查询
            string sql = "select a.r_id, a.r_name, a.r_tel,a.r_appid,a.r_payid , a.r_userid, a.r_tradeno, a.r_code, a.r_price, a.r_money, a.r_date, a.r_time, a.r_auditor, a.r_auditortime, a.r_remark, a.r_static, b.u_realname ,c.a_name,d.l_corporatename from jmp_refund as a left join jmp_app as c on a.r_appid = c.a_id left join jmp_user as b on a.r_userid = b.u_id left join jmp_interface as d on a.r_payid = d.l_id  where 1 = 1 ";//组装查询条件

            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            string auditstate = string.IsNullOrEmpty(Request["auditstate"]) ? "" : Request["auditstate"];//审核状态
            int type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);//查询条件选择
            string stime = string.IsNullOrEmpty(Request["r_begin"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["r_begin"];
            string etime = string.IsNullOrEmpty(Request["r_end"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["r_end"];
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            //string RoleID = System.Configuration.ConfigurationManager.AppSettings["RoleID"];
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        sql += "  and a.r_name like '%" + sea_name + "%' ";
                        break;
                    case 2:
                        sql += " and c.a_name like '%" + sea_name + "%' ";
                        break;
                    case 3:
                        sql += " and b.u_realname like  '%" + sea_name + "%' ";
                        break;
                    case 4:
                        sql += " and a.r_tradeno ='" + sea_name + "' ";
                        break;
                    case 5:
                        sql += " and a.r_code =  '" + sea_name + "' ";
                        break;
                    case 6:
                        sql += " and d.l_corporatename =  '" + sea_name + "' ";
                        break;

                }

            }
            if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
            {
                sql += " and r_time>='" + stime + " 00:00:00' and r_time<='" + etime + " 23:59:59' ";
            }
            if (!string.IsNullOrEmpty(auditstate))
            {
                sql += " and a.r_static='" + auditstate + "' ";
            }
            //if (UserInfo.UserRoleId == RoleID)
            //{
            //    sql += " and b.u_merchant_id= " + int.Parse(UserInfo.UserId);
            //}
            string order = searchDesc == 0 ? " Order by a.r_id " : " Order by a.r_id  ";//排序字段
            List<JMP.MDL.jmp_refund> list = new List<JMP.MDL.jmp_refund>();
            JMP.BLL.jmp_refund bll = new JMP.BLL.jmp_refund();
            list = bll.SelectDc(sql);
            var lst = list.Select(x => new
            {
                x.r_name,
                x.r_tel,
                x.u_realname,
                x.a_name,
                x.l_corporatename,
                x.r_tradeno,
                x.r_code,
                x.r_price,
                x.r_money,
                r_date = x.r_date.ToString("yyyy-MM-dd"),
                r_time = x.r_time.ToString("yyyy-MM-dd HH:mm:ss"),
                r_static = x.r_static == 0 ? "提交申请" : x.r_static == 1 ? "审核通过" : "审核未通过",
                x.r_auditor,
                r_auditortime = x.r_auditortime.ToString("yyyy-MM-dd HH:mm:ss"),
                x.r_remark
            });
            var caption = "订单列表";
            byte[] fileBytes;
            //命名导出表格的StringBuilder变量
            using (var pck = new ExcelPackage())
            {
                var ws = pck.Workbook.Worksheets.Add(caption);
                ws.Cells["A1"].LoadFromCollection(lst, false);
                ws.InsertRow(1, 1);
                ws.Cells["A1"].Value = "申请退款人姓名";
                ws.Cells["B1"].Value = "申请退款人电话";
                ws.Cells["C1"].Value = "所属商户";
                ws.Cells["D1"].Value = "所属应用";
                ws.Cells["E1"].Value = "渠道名称";
                ws.Cells["F1"].Value = "支付流水号";
                ws.Cells["G1"].Value = "订单编号";
                ws.Cells["H1"].Value = "退款金额";
                ws.Cells["I1"].Value = "实际支付金额";
                ws.Cells["J1"].Value = "用户付费日期";
                ws.Cells["K1"].Value = "提交时间";
                ws.Cells["L1"].Value = "审核状态";
                ws.Cells["M1"].Value = "审核人";
                ws.Cells["N1"].Value = "审核时间";
                ws.Cells["O1"].Value = "备注";
                fileBytes = pck.GetAsByteArray();

            }
            string fileName = "退款申请" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            #endregion
            return File(fileBytes, "application/vnd.ms-excel", fileName);
        }
        /// <summary>
        /// 添加退款申请
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult RefundAdd()
        {

            return View();
        }

        /// <summary>
        /// 添加或修改退款申请
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult InsertUpdateRefund(JMP.MDL.jmp_refund model)
        {
            object retJson = new { success = 0, msg = "操作失败" };
            JMP.BLL.jmp_refund bll = new JMP.BLL.jmp_refund();
            //string xgzfc = "";
            model.r_auditortime = DateTime.Now;
            if (model.r_id > 0)
            {
                //修改退款管理
                JMP.MDL.jmp_refund modRefund = new JMP.MDL.jmp_refund();
                modRefund = bll.GetModel(model.r_id);
                model.r_static = modRefund.r_static;
                model.r_remark = modRefund.r_remark;
                model.r_auditor = modRefund.r_auditor;
                model.r_auditortime = modRefund.r_auditortime;
                //if (model.r_static == 1 || model.r_static == -1)
                //{
                //    retJson = new { success = 0, msg = "已审核，不能修改" };
                //    return Json(retJson);
                //}

                if (bll.Update(model))
                {

                    Logger.ModifyLog("修改退款信息", modRefund, model);
                    retJson = new { success = 1, msg = "修改成功" };
                }
                else
                {
                    retJson = new { success = 0, msg = "修改失败" };
                }

            }
            else
            {
                model.r_static = 0;
                model.r_time = DateTime.Now;
                //model.r_remark = "";
                int cg = bll.Add(model);
                if (cg > 0)
                {

                    Logger.CreateLog("退款申请", model);
                    retJson = new { success = 1, msg = "添加成功" };

                }
                else
                {
                    retJson = new { success = 1, msg = "添加失败" };
                }

            }
            return Json(retJson);
        }

        /// <summary>
        /// 根据ID查询退款信息
        /// </summary>
        /// <param name="r_id"></param>
        /// <returns></returns>
        public ActionResult UpdateRefund(int r_id)
        {
            JMP.BLL.jmp_refund bll = new JMP.BLL.jmp_refund();
            JMP.MDL.jmp_refund model = new JMP.MDL.jmp_refund();
            if (r_id > 0)
            {
                model = bll.SelectId(r_id);
            }
            ViewBag.model = model;
            return View();
        }

        /// <summary>
        /// 审核退款页面
        /// </summary>
        /// <param name="uids"></param>
        /// <returns></returns>
        public ActionResult AuditorRefund(string rid)
        {
            ViewBag.uids = rid;
            return View();
        }

        /// <summary>
        /// 审核退款信息（可多审核）
        /// </summary>
        /// <returns></returns>
        public JsonResult AuditorTORefund()
        {
            object retJson = new { success = 0, msg = "审核失败" };
            var rid = Request["rid"] ?? "";
            var r_static = Request["state"] ?? "";
            var r_remark = Request["remark"] ?? "";
            var r_payid = Request["r_payid"] ?? "";
            var r_auditor = UserInfo.UserName;
            if (rid.Length <= 0)
            {
                retJson = new { success = 0, msg = "参数错误" };
                return Json(retJson);
            }
            if (rid.CompareTo("On") > 0)
            {
                rid = rid.Substring(3);
            }
            var bll = new jmp_refund();
            var success = bll.AuditorToRefund(rid, r_static, r_auditor, r_remark, r_payid);
            if (success)
            {
                Logger.OperateLog("审核补单：", "审核的补单ID为" + rid + "审核的内容为 " + r_remark);

                retJson = new { success = 1, msg = "审核成功" };
            }
            else
            {
                retJson = new { success = 0, msg = "审核失败" };
            }

            return Json(retJson);

        }

        #endregion

    }
}
