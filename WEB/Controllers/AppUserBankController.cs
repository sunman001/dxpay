using System;
using System.Web.Mvc;
using JMP.TOOL;
using WEB.Util.Logger;


namespace WEB.Controllers
{
    public class AppUserBankController : Controller
    {
        //
        // GET: /AppUserBank/
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;
        public ActionResult Index()
        {
            //获取请求参数
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量
            string type = string.IsNullOrEmpty(Request["stype"]) ? "0" : Request["stype"];//查询条件类型
            string sea_name = string.IsNullOrEmpty(Request["skeys"]) ? "" : Request["skeys"].Trim();//查询条件值
            string u_state = string.IsNullOrEmpty(Request["scheck"]) ? "" : Request["scheck"];//银行卡审核状态
            string u_freeze = string.IsNullOrEmpty(Request["state"]) ? "" : Request["state"];//银行卡状态
            int px = string.IsNullOrEmpty(Request["s_sort"]) ? 1 : Int32.Parse(Request["s_sort"]);//排序
            //获取用户列表
            string where = "where 1=1";
            if (!string.IsNullOrEmpty(type.ToString()))
            {
                if (!string.IsNullOrEmpty(sea_name))
                {
                    if (type == "0")
                        where += string.Format(" and  b.u_email ='{0}'", sea_name);
                    else if (type == "1")
                        where += string.Format(" and  a.u_name ='{0}'", sea_name);
                    else if (type == "3")
                        where += string.Format(" and a.u_banknumber ='{0}'", sea_name);
                    else if (type == "4")
                        where += string.Format(" and (a.u_bankname like '%{0}%')", sea_name);
                    else if (type == "5")
                        where += string.Format(" and (a.u_openbankname like '%{0}%')", sea_name);
                }
            }
            if (!string.IsNullOrEmpty(u_state))
                where += string.Format(" and a.u_state={0}", u_state);
            if (!string.IsNullOrEmpty(u_freeze))
                where += string.Format(" and a.u_freeze={0}", u_freeze);

            string Order = " order by u_id " + (px == 0 ? "" : " desc ") + " ";
            JMP.BLL.jmp_userbank bll = new JMP.BLL.jmp_userbank();
            string query = "select a.*,b.u_email,b.u_realname from jmp_userbank a  left join jmp_user b on a.u_userid=b.u_id " + where;
            var list = bll.GetAppuserbankLists(query, Order, pageIndexs, PageSize, out pageCount);
            //返回
            ViewBag.CurrPage = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.PageCount = pageCount;
            ViewBag.stype = type;
            ViewBag.skeys = sea_name;
            ViewBag.state = u_freeze;
            ViewBag.scheck = u_state;
            ViewBag.s_sort = px;
            ViewBag.list = list;
            ViewBag.btnstr = GetVoidHtml();
            return View();
        }
        /// <summary>
        /// 判断权限
        /// </summary>
        private string GetVoidHtml()
        {
            string tempStr = string.Empty;
            JMP.BLL.jmp_limit bll_limit = new JMP.BLL.jmp_limit();
            string u_id = UserInfo.UserId.ToString();
            int r_id = UserInfo.UserRid;
            //一键解冻
            bool getUidT = bll_limit.GetLocUserLimitVoids("/AppUserBank/doAll(1)", u_id, r_id);
            if (getUidT)
                tempStr += "<li onclick=\"doAll(1)\"><i class='fa fa-check-square-o'></i>一键冻结</li>";
            //一键冻结
            bool getUidF = bll_limit.GetLocUserLimitVoids("/AppUserBank/doAll(0)", u_id, r_id);
            if (getUidF)
                tempStr += "<li onclick=\"doAll(0)\"><i class='fa fa-check-square-o'></i>一键解冻</li>";
            return tempStr;
        }

        /// <summary>
        /// 批量更新用户状态
        /// </summary>
        /// <param name="uIds">用户id列表</param>
        /// <param name="tag">状态</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult DoAll(string uids, int tag)
        {
            JMP.BLL.jmp_userbank userBll = new JMP.BLL.jmp_userbank();
            bool flag = userBll.UpdateState(uids, tag);
            //写日志
            if (flag)
            {
                string info = "批量更新开发者的银行账号ID（" + uids + "）的状态为" + (tag == 1 ? "正常。" : "冻结。");
                Logger.OperateLog("批量更新开发者状态", info);
            }
            return Json(new { success = flag ? 1 : 0, msg = flag ? "更新成功！" : "更新失败！" });
        }

        public ActionResult AppUserBankAuditing(int id)
        {
            int userid = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);
            JMP.BLL.jmp_userbank bll = new JMP.BLL.jmp_userbank();
            JMP.MDL.jmp_userbank model = bll.GetModel(userid);
            //审核状态
            ViewBag.start = model.u_state;
            ViewBag.reaks = model.u_remarks;
            ViewBag.userid = userid;
            return View();
           
        }


        /// <summary>
        /// 审核开发者绑定的银行卡信息
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult CheckAuditing()
        {

            int id = string.IsNullOrEmpty(Request["userid"]) ? 0 : int.Parse(Request["userid"]);
            int u_auditstate = string.IsNullOrEmpty(Request["u_auditstate"]) ? 0 : int.Parse(Request["u_auditstate"]);
            string u_remarks= string.IsNullOrEmpty(Request["u_remarks"]) ? "" : Request["u_remarks"];
            string name = UserInfo.UserName;

            JMP.BLL.jmp_userbank bll = new JMP.BLL.jmp_userbank();
            bool flag = bll.UpdateAuditState(id, u_auditstate, name, u_remarks);

            if (flag)
            {
                string info = "审核开发者（" + id + "）绑定的银行卡信息状态为" + u_auditstate + "";
                Logger.OperateLog("审核开发者绑定的银行卡信息状态", info);
            }

            return Json(new { success = flag ? 1 : 0, msg = flag ? "审核成功！" : "审核失败！" });
        }

    }
}
