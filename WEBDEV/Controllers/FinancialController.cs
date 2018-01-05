/************聚米支付平台__财务管理************/
//描述：开发者前端的账单及提款管理
//功能：开发者前端的账单及提款管理
//开发者：谭玉科
//开发时间: 2016.05.05
/************聚米支付平台__财务管理************/
using WEBDEV.Util.Logger;
using JMP.TOOL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace WEBDEV.Controllers
{
    /// <summary>
    /// 类名：FinancialController
    /// 功能：财务管理
    /// 详细：财务管理
    /// 修改日期：2016.05.25
    /// </summary>
    public class FinancialController : Controller
    {
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;
        JMP.BLL.CoSettlementDeveloperOverview bll_CoSDO = new JMP.BLL.CoSettlementDeveloperOverview();
        JMP.BLL.jmp_paymode bll_paymode = new JMP.BLL.jmp_paymode();
        List<JMP.MDL.jmp_paymode> List_paymode = new List<JMP.MDL.jmp_paymode>();

        /// <summary>
        /// 账单管理
        /// </summary>
        /// <returns></returns>
        public ActionResult BillList()
        {

            #region 获取用户实名认证状态信息

            JMP.BLL.jmp_user sm_bll = new JMP.BLL.jmp_user();
            JMP.MDL.jmp_user sm_model = new JMP.MDL.jmp_user();

            int u_ids = UserInfo.Uid;

            //查询登录信息
            sm_model = sm_bll.GetModel(u_ids);

            ViewBag.auditstate = sm_model.u_auditstate;
            ViewBag.linkEmail = sm_model.u_email;

            #endregion

            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量    
            string stime = string.IsNullOrEmpty(Request["stime"]) ? DateTime.Now.AddDays(-8).ToString("yyyy-MM-dd") : Request["stime"];//开始时间
            string etime = string.IsNullOrEmpty(Request["etime"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["etime"];//结束时间  

            //首页跳转标识
            int num = string.IsNullOrEmpty(Request["start"]) ? -1 : int.Parse(Request["start"]);

            switch (num)
            {
                case 2:
                    stime = DateTime.Now.ToString("yyyy-MM-01");
                    etime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    break;
                case 3:
                    stime = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01");
                    etime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddDays(-1).ToString("yyyy-MM-dd");
                    break;
            }


            #region 组装查询语句
            string where = "where DeveloperId='" + UserInfo.UserId + "'";
            if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
            {
                where += " and SettlementDay >='" + stime + "' and SettlementDay<='" + etime + "' ";
            }
            string orderby = "order by SettlementDay desc";

            string sql = string.Format(@"select a.Id,DeveloperId,CreatedOn,SettlementDay,TotalAmount,ServiceFee,PortFee,(TotalAmount-ServiceFee-PortFee) as KFZIncome,isnull(SUM(b.p_money),0) as p_money,ISNULL((OriginalTotalAmount-TotalAmount),0.0000) AS RefundAmount
 from  dx_total.dbo.[CoSettlementDeveloperOverview] as a
 left join (select * from dx_base.dbo.jmp_pays a,dx_base.dbo.jmp_BankPlaymoney b where a.p_batchnumber=b.b_batchnumber and a.p_state!=-1 and b.b_tradestate!=4
) as b on a.Id=b.p_bill_id {0}  group by a.Id,DeveloperId,CreatedOn,SettlementDay,TotalAmount,ServiceFee,PortFee,OriginalTotalAmount", where);

            List<JMP.MDL.CoSettlementDeveloperOverview> list = new List<JMP.MDL.CoSettlementDeveloperOverview>();
            JMP.MDL.CoSettlementDeveloperOverview model = new JMP.MDL.CoSettlementDeveloperOverview();
            list = bll_CoSDO.GetLists(sql, orderby, pageIndexs, PageSize, out pageCount);
            #endregion

            #region 合计组装查询语句

            string countsql = string.Format(@"select ISNULL(SUM(TotalAmount),0) as TotalAmount,isnull(SUM(ServiceFee),0) as ServiceFee,isnull(SUM(PortFee),0) as PortFee,
isnull(SUM(TotalAmount)-SUM(ServiceFee)-SUM(PortFee),0) as KFZIncome,ISNULL(SUM(p_money),0) as p_money,
ISNULL(SUM(OriginalTotalAmount)-SUM(TotalAmount),0) as RefundAmount 
from
 (
 select a.Id,a.SettlementDay,TotalAmount,ServiceFee,PortFee,OriginalTotalAmount,ISNULL(SUM(b.p_money),0) as p_money
from dx_total.dbo.CoSettlementDeveloperOverview as a
left join (select * from dx_base.dbo.jmp_pays a,dx_base.dbo.jmp_BankPlaymoney b where a.p_batchnumber=b.b_batchnumber and a.p_state!=-1 and b.b_tradestate!=4
) as b on a.Id=b.p_bill_id
{0}
group by a.Id,a.SettlementDay,TotalAmount,ServiceFee,PortFee,OriginalTotalAmount
) a", where);
            if (list.Count > 0)
            {
                DataTable dt = bll_CoSDO.SelectSum(countsql);
                model = dt.Rows.Count > 0 ? JMP.TOOL.MdlList.ToModel<JMP.MDL.CoSettlementDeveloperOverview>(dt) : new JMP.MDL.CoSettlementDeveloperOverview();

            }

            #endregion

            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.stime = stime;
            ViewBag.etime = etime;
            ViewBag.list = list;
            ViewBag.model = model;
            return View();
        }

        /// <summary>
        /// 账单详情
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="SettlementDay"></param>
        /// <returns></returns>
        public ActionResult BillList_Details(int appid, string SettlementDay)
        {
            JMP.BLL.CoSettlementDeveloperAppDetails cobll = new JMP.BLL.CoSettlementDeveloperAppDetails();
            List<JMP.MDL.CoSettlementDeveloperAppDetails> colist = new List<JMP.MDL.CoSettlementDeveloperAppDetails>();

            DataTable dt = new DataTable();

            //查询账单详情
            dt = cobll.GetModel_Statistics(appid, SettlementDay).Tables[0];
            colist = JMP.TOOL.MdlList.ToList<JMP.MDL.CoSettlementDeveloperAppDetails>(dt);

            ViewBag.colist = colist;

            return PartialView();
        }

        /// <summary>
        /// 多卡提款
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult pays()
        {
            JMP.MDL.CoSettlementDeveloperOverview comodeT1 = new JMP.MDL.CoSettlementDeveloperOverview();
            JMP.MDL.CoSettlementDeveloperOverview comodeT2 = new JMP.MDL.CoSettlementDeveloperOverview();
            //开发者选中的账单ID
            string payid = string.IsNullOrEmpty(Request["payid"]) ? "" : Request["payid"];
            ViewBag.payid = payid;

            string sqlstr = string.Format(@";WITH O AS(
  select * from CoSettlementDeveloperOverview where id in({0}) and DeveloperId={1}
),bll as(
  select  p_bill_id,isnull(SUM(p_money),0) as p_money  from  dx_base.dbo.jmp_pays a, dx_base.dbo.jmp_BankPlaymoney b 
  where a.p_batchnumber=b.b_batchnumber and a.p_state!=-1 and b.b_tradestate!=4 and p_bill_id in({0})  and p_userid={1}
  group by  p_bill_id
)
select 
(isnull(SUM(TotalAmount),0)-isnull(SUM(ServiceFee),0)-isnull(SUM(PortFee),0)-isnull(SUM(bll.p_money),0)) as ketiMoney

from  O
left join  bll on bll.p_bill_id=O.Id", payid, UserInfo.UserId);
            //查询选中数据
            DataTable dt = bll_CoSDO.SelectSum(sqlstr);

            comodeT1 = dt.Rows.Count > 0 ? JMP.TOOL.MdlList.ToModel<JMP.MDL.CoSettlementDeveloperOverview>(dt) : new JMP.MDL.CoSettlementDeveloperOverview();
            ViewBag.comodeT1 = comodeT1;

            //开发者账单总金额与冻结金额
            string sqlstrSum = string.Format(@";
WITH O AS(
  select * from dx_total.dbo.CoSettlementDeveloperOverview where  DeveloperId={0}
),bll as(
  select  p_bill_id,isnull(SUM(p_money),0) as p_money  from  dx_base.dbo.jmp_pays a, dx_base.dbo.jmp_BankPlaymoney b 
  where a.p_batchnumber=b.b_batchnumber and a.p_state!=-1 and b.b_tradestate!=4 and p_userid={0}
  group by  p_bill_id
),users as
(
select u_id,ISNULL(FrozenMoney,0) as FrozenMoney from dx_base.dbo.jmp_user where u_id={0}
),kt as
(
select DeveloperId,
(isnull(SUM(TotalAmount),0)-isnull(SUM(ServiceFee),0)-isnull(SUM(PortFee),0)-isnull(SUM(bll.p_money),0)) as ketiMoney
from  O
left join  bll on bll.p_bill_id=O.Id group by DeveloperId
)
select FrozenMoney,ketiMoney from users inner join kt on users.u_id=kt.DeveloperId", UserInfo.UserId);

            DataTable dt2 = bll_CoSDO.SelectSum(sqlstrSum);

            comodeT2 = dt2.Rows.Count > 0 ? JMP.TOOL.MdlList.ToModel<JMP.MDL.CoSettlementDeveloperOverview>(dt2) : new JMP.MDL.CoSettlementDeveloperOverview();
            ViewBag.comodeT2 = comodeT2;




            JMP.BLL.jmp_userbank ubkBll = new JMP.BLL.jmp_userbank();
            List<JMP.MDL.jmp_userbank> ubklist = new List<JMP.MDL.jmp_userbank>();

            //查询开发者绑定的银行卡信息(审核通过并未被冻结的)
            ubklist = ubkBll.GetModelList("u_userid=" + UserInfo.UserId + " and u_state=1 and u_freeze=0");
            ViewBag.ubklist = ubklist;

            return View();
        }

        /// <summary>
        /// 单卡提现
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult pays_single()
        {
            JMP.MDL.CoSettlementDeveloperOverview comodeT1 = new JMP.MDL.CoSettlementDeveloperOverview();
            JMP.MDL.CoSettlementDeveloperOverview comodeT2 = new JMP.MDL.CoSettlementDeveloperOverview();
            //开发者选中的账单ID
            string payid = string.IsNullOrEmpty(Request["payid"]) ? "" : Request["payid"];
            ViewBag.payid = payid;

            string sqlstr = string.Format(@";WITH O AS(
  select * from CoSettlementDeveloperOverview where id in({0}) and DeveloperId={1}
),bll as(
  select  p_bill_id,isnull(SUM(p_money),0) as p_money  from  dx_base.dbo.jmp_pays a, dx_base.dbo.jmp_BankPlaymoney b 
  where a.p_batchnumber=b.b_batchnumber and a.p_state!=-1 and b.b_tradestate!=4 and p_bill_id in({0})  and p_userid={1}
  group by  p_bill_id
)
select 
(isnull(SUM(TotalAmount),0)-isnull(SUM(ServiceFee),0)-isnull(SUM(PortFee),0)-isnull(SUM(bll.p_money),0)) as ketiMoney

from  O
left join  bll on bll.p_bill_id=O.Id", payid, UserInfo.UserId);
            //查询选中数据
            DataTable dt = bll_CoSDO.SelectSum(sqlstr);

            comodeT1 = dt.Rows.Count > 0 ? JMP.TOOL.MdlList.ToModel<JMP.MDL.CoSettlementDeveloperOverview>(dt) : new JMP.MDL.CoSettlementDeveloperOverview();
            ViewBag.comodeT1 = comodeT1;

            //开发者账单总金额与冻结金额
            string sqlstrSum = string.Format(@";
WITH O AS(
  select * from dx_total.dbo.CoSettlementDeveloperOverview where  DeveloperId={0}
),bll as(
  select  p_bill_id,isnull(SUM(p_money),0) as p_money  from  dx_base.dbo.jmp_pays a, dx_base.dbo.jmp_BankPlaymoney b 
  where a.p_batchnumber=b.b_batchnumber and a.p_state!=-1 and b.b_tradestate!=4 and p_userid={0}
  group by  p_bill_id
),users as
(
select u_id,ISNULL(FrozenMoney,0) as FrozenMoney from dx_base.dbo.jmp_user where u_id={0}
),kt as
(
select DeveloperId,
(isnull(SUM(TotalAmount),0)-isnull(SUM(ServiceFee),0)-isnull(SUM(PortFee),0)-isnull(SUM(bll.p_money),0)) as ketiMoney
from  O
left join  bll on bll.p_bill_id=O.Id group by DeveloperId
)
select FrozenMoney,ketiMoney from users inner join kt on users.u_id=kt.DeveloperId", UserInfo.UserId);

            DataTable dt2 = bll_CoSDO.SelectSum(sqlstrSum);

            comodeT2 = dt2.Rows.Count > 0 ? JMP.TOOL.MdlList.ToModel<JMP.MDL.CoSettlementDeveloperOverview>(dt2) : new JMP.MDL.CoSettlementDeveloperOverview();
            ViewBag.comodeT2 = comodeT2;




            JMP.BLL.jmp_userbank ubkBll = new JMP.BLL.jmp_userbank();
            List<JMP.MDL.jmp_userbank> ubklist = new List<JMP.MDL.jmp_userbank>();

            //查询开发者绑定的银行卡信息(审核通过并未被冻结的)
            ubklist = ubkBll.GetModelList("u_userid=" + UserInfo.UserId + " and u_state=1 and u_freeze=0");
            ViewBag.ubklist = ubklist;

            return View();
        }

        /// <summary>
        /// 提现信息列表展示
        /// </summary>
        /// <returns></returns>
        public ActionResult PaymentList()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量            
            string types = string.IsNullOrEmpty(Request["s_type"]) ? "1" : Request["s_type"];
            string searchKey = string.IsNullOrEmpty(Request["s_key"]) ? "" : Request["s_key"];
            string stime = string.IsNullOrEmpty(Request["s_begin"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["s_begin"];
            string etime = string.IsNullOrEmpty(Request["s_end"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["s_end"];
            string b_tradestate = string.IsNullOrEmpty(Request["check"]) ? "" : Request["check"];//交易状态
            string payfashion = string.IsNullOrEmpty(Request["payfashion"]) ? "" : Request["payfashion"];//付款方式

            #region 组装查询语句
            string where = "where c.u_id=" + UserInfo.UserId + " ";
            string orderby = " order by b_id desc";
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
                }
            }

            if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
            {
                where += " and b_date >='" + stime + " 00:00:00'  and b_date<='" + etime + " 23:59:59' ";
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
(select  a.*,b.u_bankname,b.u_banknumber,b.u_name ,c.u_realname,c.u_phone from jmp_BankPlaymoney  a left join jmp_userbank  b on a.b_bankid=b.u_id left join  jmp_user c on b.u_userid=c.u_id {0} ) a 
left join (select p_state,p_batchnumber from jmp_pays group by p_batchnumber,p_state) b on a.b_batchnumber=b.p_batchnumber
 ", where);
            JMP.BLL.jmp_BankPlaymoney bankpbll = new JMP.BLL.jmp_BankPlaymoney();
            List<JMP.MDL.jmp_BankPlaymoney> bankplist = new List<JMP.MDL.jmp_BankPlaymoney>();

            bankplist = bankpbll.GetLists(sql.ToString(), orderby, pageIndexs, PageSize, out pageCount);

            #endregion

            #region 合计组装查询语句

            JMP.MDL.jmp_BankPlaymoney model = new JMP.MDL.jmp_BankPlaymoney();

            string countsql = string.Format(@"select SUM(b_money) as b_money from 
(select  a.*,b.u_bankname,b.u_banknumber,b.u_name ,c.u_realname,c.u_phone from jmp_BankPlaymoney  a left join jmp_userbank  b on a.b_bankid=b.u_id left join  jmp_user c on b.u_userid=c.u_id {0} ) a 
left join (select p_state,p_batchnumber from jmp_pays group by p_batchnumber,p_state) b on a.b_batchnumber=b.p_batchnumber
 ", where);
            if (bankplist.Count > 0)
            {
                DataTable dt = bankpbll.SelectSQL(countsql);
                model = dt.Rows.Count > 0 ? JMP.TOOL.MdlList.ToModel<JMP.MDL.jmp_BankPlaymoney>(dt) : new JMP.MDL.jmp_BankPlaymoney();

            }

            #endregion


            StringBuilder sqlinfo = new StringBuilder();
            sqlinfo.AppendFormat(@";WITH a AS( 
 select a.*,b.SettlementDay
from [dbo].[jmp_pays]  a 
left join [dx_total].[dbo].[CoSettlementDeveloperOverview]  b on a.p_bill_id=b.Id and a.p_userid={0} 
), b as(
  select a.Id,(TotalAmount-ServiceFee-PortFee) as KFZIncome,
isnull(SUM(b.p_money),0) as p_moneys
from  dx_total.dbo.[CoSettlementDeveloperOverview] as a
left join (select * from dx_base.dbo.jmp_pays a,dx_base.dbo.jmp_BankPlaymoney b 
 where a.p_batchnumber=b.b_batchnumber and a.p_state!=-1 and b.b_tradestate!=4
) as b on a.Id=b.p_bill_id and b.p_userid={0} group by a.Id,TotalAmount,ServiceFee,PortFee
)
select  *  from  a 
left join b on a.p_bill_id=b.Id", UserInfo.UserId);
            JMP.BLL.jmp_pays bllinfo = new JMP.BLL.jmp_pays();
            List<JMP.MDL.jmp_pays> listinfo = new List<JMP.MDL.jmp_pays>();
            DataTable dts = bllinfo.SelectList(sqlinfo.ToString());
            listinfo = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_pays>(dts);
            ViewBag.colist = listinfo;


            ViewBag.show_fields = types;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = bankplist;
            ViewBag.model = model;
            ViewBag.scheck = b_tradestate;
            ViewBag.stime = stime;
            ViewBag.etime = etime;
            ViewBag.payfashion = payfashion;
            ViewBag.searchname = searchKey;

            return View();
        }

        /// <summary>
        /// 多卡提现银行卡弹窗管理
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult PayTc()
        {
            JMP.BLL.jmp_userbank ubkBll = new JMP.BLL.jmp_userbank();
            List<JMP.MDL.jmp_userbank> ubklist = new List<JMP.MDL.jmp_userbank>();


            int id = UserInfo.UserId;
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 10 : Int32.Parse(Request["PageSize"]);//每页显示数量
            //搜索条件
            string searchType = string.IsNullOrEmpty(Request["searchType"]) ? "0" : Request["searchType"];
            //搜索信息
            string banknumber = string.IsNullOrEmpty(Request["banknumber"]) ? "" : Request["banknumber"];
            //付款标识
            string flag = string.IsNullOrEmpty(Request["flag"]) ? "" : Request["flag"];

            //查询开发者绑定的银行卡信息(审核通过并未被冻结的)
            ubklist = ubkBll.SelectUserBankListStart(id, searchType, banknumber, flag, pageIndexs, PageSize, out pageCount);

            ViewBag.ubklist = ubklist;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.searchType = searchType;
            ViewBag.banknumber = banknumber;
            ViewBag.flag = flag;

            return View();
        }

        #region 拼装提款表与银行打款对接表sql 方法

        /// <summary>
        /// 多卡提现拼装提款表与银行打款对接表sql
        /// </summary>
        /// <param name="payMoney">提现金额</param>
        /// <param name="b_bankid">提现银行卡</param>
        /// <returns></returns>
        private List<string> BankPay(decimal payMoney, string b_bankid, string payid)
        {
            Random r = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
            List<JMP.MDL.CoSettlementDeveloperOverview> comode = new List<JMP.MDL.CoSettlementDeveloperOverview>();

            //提现最小金额
            string WithdrawalsMinimum = System.Configuration.ConfigurationManager.AppSettings["WithdrawalsMinimum"].ToString();

            //查询选中的账单信息
            comode = coSDO(payid);
            //sql集合
            List<string> sqllist = new List<string>();
            //扣掉提现手续费的提现金额
            decimal Money = 0;
            //银行卡最大打款金额
            decimal remainder = 0;
            //银行卡信息
            string[] amount = b_bankid.Split('|');
            //用户判断最后的余额时跳出循环
            int determine = 0;

            if (amount.Length > 0)
            {
                for (int i = 0; i < amount.Length; i++)
                {
                    int num = 0;
                    //卡号与最大提现金额
                    string[] info = amount[i].Split(',');
                    //提款批次号
                    string code = DateTime.Now.ToString("yyyyMMddHHmmssfff") + r.Next(111111111, 999999999).ToString() + r.Next(1111, 9999).ToString();

                    if (info[0] != "")
                    {
                        //赋值
                        remainder = decimal.Parse(info[1]);
                        if (remainder >= decimal.Parse(WithdrawalsMinimum))
                        {
                            //提款金额
                            if (payMoney > remainder)
                            {
                                payMoney = payMoney - remainder;
                            }
                            else
                            {
                                //最后的余额
                                remainder = payMoney;
                                determine = 1;
                            }
                            //手续费
                            string ServiceChargestr = ConfigurationManager.AppSettings["ServiceCharge"].ToString();
                            int ServiceCharge = string.IsNullOrEmpty(ServiceChargestr) ? 0 : int.Parse(ServiceChargestr);
                            //扣手续费
                            Money = remainder - ServiceCharge;
                            if (payMoney > 0)
                            {
                                #region 拼装sql语句

                                for (int j = 0; j <= comode.Count; j++)
                                {
                                    j = 0;
                                    if (comode[j].ketiMoney > 0)
                                    {
                                        if ((remainder - comode[j].ketiMoney) > 0)
                                        {
                                            sqllist.Add("insert into jmp_pays(p_remarks,p_applytime,p_money,p_bill_id,p_userid,p_state,p_batchnumber) values('',GETDATE(),'" + comode[j].ketiMoney + "','" + comode[j].Id + "','" + UserInfo.UserId + "','0','" + code + "')");

                                            remainder = remainder - comode[j].ketiMoney;
                                            comode.Remove(comode[j]);

                                        }
                                        else
                                        {
                                            sqllist.Add("insert into jmp_pays(p_remarks,p_applytime,p_money,p_bill_id,p_userid,p_state,p_batchnumber) values('',GETDATE(),'" + remainder + "','" + comode[j].Id + "','" + UserInfo.UserId + "','0','" + code + "')");
                                            decimal difference = comode[j].ketiMoney - remainder;
                                            comode[j].ketiMoney = difference;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        num = 1;
                                        break;
                                    }
                                }

                                if (num == 0)
                                {
                                    //银行打款对接数据
                                    sqllist.Add("insert into jmp_BankPlaymoney(b_batchnumber,b_number,b_tradestate,b_bankid,b_money,b_date,b_ServiceCharge) values('" + code + "','','0','" + info[0] + "','" + Money + "',GETDATE(),'" + ServiceCharge + "')");
                                }

                                #endregion

                                //执行完最后的余额后跳出循环
                                if (determine > 0)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return sqllist;
        }

        /// <summary>
        /// 查询选中的账单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<JMP.MDL.CoSettlementDeveloperOverview> coSDO(string id)
        {
            List<JMP.MDL.CoSettlementDeveloperOverview> comode = new List<JMP.MDL.CoSettlementDeveloperOverview>();
            //查询账单结算金额，已提现金额，可提现金额
            string sqlstr = string.Format(@"select a.Id,(TotalAmount-ServiceFee-PortFee) as KFZIncome,isnull(SUM(b.p_money),0) as p_money,(TotalAmount-ServiceFee-PortFee-isnull(SUM(b.p_money),0)) as ketiMoney from  dx_total.dbo.CoSettlementDeveloperOverview as a left join (select * from dx_base.dbo.jmp_pays a,dx_base.dbo.jmp_BankPlaymoney b 
 where a.p_batchnumber=b.b_batchnumber and a.p_state!=-1 and b.b_tradestate!=4
) as b on a.Id=b.p_bill_id where Id in ({0}) group by a.Id,TotalAmount,ServiceFee,PortFee order by a.Id desc", id);
            //查询选中数据
            DataTable dt = bll_CoSDO.SelectSum(sqlstr);
            return comode = JMP.TOOL.MdlList.ToList<JMP.MDL.CoSettlementDeveloperOverview>(dt);
        }

        /// <summary>
        /// 单卡提现拼装提款表与银行打款对接表sql
        /// </summary>
        /// <param name="payMoney">提现金额</param>
        /// <param name="b_bankid">提现银行卡</param>
        /// <returns></returns>
        private List<string> BankPaySongle(decimal payMoney, string b_bankid, string payid)
        {
            Random r = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
            string code = DateTime.Now.ToString("yyyyMMddHHmmssfff") + r.Next(111111111, 999999999).ToString() + r.Next(1111, 9999).ToString();

            List<JMP.MDL.CoSettlementDeveloperOverview> comode = new List<JMP.MDL.CoSettlementDeveloperOverview>();
            //查询选中的账单信息
            comode = coSDO(payid);
            //sql集合
            List<string> sqllist = new List<string>();

            //手续费
            string ServiceChargestr = ConfigurationManager.AppSettings["ServiceCharge"].ToString();
            int ServiceCharge = string.IsNullOrEmpty(ServiceChargestr) ? 0 : int.Parse(ServiceChargestr);

            decimal Money = payMoney - ServiceCharge;
            int num = 0;

            if (comode.Count > 0)
            {

                foreach (var item in comode)
                {
                    if (item.ketiMoney > 0)
                    {
                        //可提金额 
                        if ((payMoney - item.ketiMoney) > 0)
                        {
                            sqllist.Add("insert into jmp_pays(p_remarks,p_applytime,p_money,p_bill_id,p_userid,p_state,p_batchnumber) values('',GETDATE(),'" + item.ketiMoney + "','" + item.Id + "','" + UserInfo.UserId + "','0','" + code + "')");

                            //剩余现象金额
                            payMoney = payMoney - item.ketiMoney;
                        }
                        else
                        {
                            sqllist.Add("insert into jmp_pays(p_remarks,p_applytime,p_money,p_bill_id,p_userid,p_state,p_batchnumber) values('',GETDATE(),'" + payMoney + "','" + item.Id + "','" + UserInfo.UserId + "','0','" + code + "')");

                            break;
                        }
                    }
                    else
                    {
                        num = 1;
                        break;
                    }

                }

                if (num == 0)
                {
                    //银行打款对接数据
                    sqllist.Add("insert into jmp_BankPlaymoney(b_batchnumber,b_number,b_tradestate,b_bankid,b_money,b_date,b_ServiceCharge) values('" + code + "','','0','" + b_bankid + "','" + Money + "',GETDATE(),'" + ServiceCharge + "')");
                }
            }

            return sqllist;

        }

        #endregion

        #region 提现申请方法

        /// <summary>
        /// 提现申请方法
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult paysAdd()
        {
            object result = new { success = 0, msg = "操作失败！" };

            JMP.BLL.jmp_user userbll = new JMP.BLL.jmp_user();
            JMP.BLL.jmp_system systembll = new JMP.BLL.jmp_system();

            //可提现金额
            decimal ketiMoney = Convert.ToDecimal(Request["ketiMoney"] ?? "0");
            //提现金额
            decimal payMoney = Convert.ToDecimal(Request["payMoney"] ?? "0");
            //提现银行卡信息
            string b_bankid = string.IsNullOrEmpty(Request["b_bankid"]) ? "" : Request["b_bankid"];
            //提现选中账单ID
            string payid = string.IsNullOrEmpty(Request["payid"]) ? "0" : Request["payid"];
            //支付密码
            string PayPwd = string.IsNullOrEmpty(Request["PayPwd"]) ? "" : Request["PayPwd"];
            //提现类型（1：单卡提现，2：多卡提现）
            int WithdrawalsType = string.IsNullOrEmpty(Request["WithdrawalsType"]) ? 0 : int.Parse(Request["WithdrawalsType"]);

            //查询开发者信息
            JMP.MDL.jmp_user j_user = userbll.GetModel(UserInfo.UserId);
            //查询超级密码
            JMP.MDL.jmp_system j_sys = systembll.GetModel_name("password");
            //提现最小金额
            string WithdrawalsMinimum = System.Configuration.ConfigurationManager.AppSettings["WithdrawalsMinimum"].ToString();

            //验证提现金额
            if (payMoney >= decimal.Parse(WithdrawalsMinimum))
            {
                if (payMoney > ketiMoney)
                {
                    result = new { success = 0, msg = "提现金额不能超过可提金额！" };
                }
                else
                {
                    #region 提现金额验证成功，后续处理

                    //判断是否验证原支付密码
                    if (!string.IsNullOrEmpty(PayPwd))
                    {
                        string temp = DESEncrypt.Encrypt(PayPwd);
                        if (temp == j_user.u_paypwd || temp == j_sys.s_value)
                        {

                            #region 拼装提款表与银行打款对接表sql



                            if (WithdrawalsType == 1 || WithdrawalsType == 2)
                            {
                                List<string> sqllist = new List<string>();

                                if (WithdrawalsType == 1)
                                {
                                    sqllist = BankPaySongle(payMoney, b_bankid, payid);
                                }
                                else
                                {
                                    sqllist = BankPay(payMoney, b_bankid, payid);
                                }

                                if (sqllist.Count > 0)
                                {

                                    JMP.BLL.jmp_BankPlaymoney bankBll = new JMP.BLL.jmp_BankPlaymoney();

                                    //执行多条SQL语句，实现数据库事务。
                                    int num = bankBll.SelectBankPayMoney(sqllist);

                                    if (num > 0)
                                    {
                                        string log = "用户：" + UserInfo.UserId + ",申请提现：" + payMoney + ",申请时间：" + DateTime.Now;

                                        Logger.OperateLog("提现申请", log);

                                        result = new { success = 1, msg = "提现申请成功，请等待审核通过！" };
                                    }
                                    else
                                    {
                                        result = new { success = 0, msg = "提现申请失败！" };
                                    }
                                }
                                else
                                {
                                    result = new { success = 0, msg = "提现申请失败！" };
                                }
                            }
                            else
                            {
                                Logger.OperateLog("提现申请失败", "提现类型（1：单卡提现，2：多卡提现）:" + WithdrawalsType);
                                result = new { success = 0, msg = "提现申请失败！" };

                            }

                            #endregion
                        }
                        else
                        {
                            result = new { success = 0, msg = "支付密码输入错误！" };
                        }


                    }
                    else
                    {
                        result = new { success = 0, msg = "请输入支付密码！" };
                    }

                    #endregion
                }
            }
            else
            {
                result = new { success = 0, msg = "提现金额不能为空且必须大于等于100！" };
            }

            return Json(result);
        }

        #endregion
    }
}
