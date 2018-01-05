using System;
using System.Collections.Generic;
using System.Web.Mvc;
using JMP.TOOL;
using WEB.Models;
using WEB.Models.Merchant;
using WEB.Util.Logger;

namespace WEB.Controllers
{
    public class MerchantController : Controller
    {
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;
        //
        // GET: /Merchant/
        [VisitRecord(IsRecord = true)]
        public ActionResult List()
        {
            var model = new ListViewModel();
            var key = Request.QueryString["skeys"] ?? "";
            var state = Request.QueryString["state"] ?? "";
            var _sort = Request.QueryString["s_sort"] ?? "0";

            model.CurrentPage = Convert.ToInt32(Request.QueryString["curr"] ?? "1");
            if (model.CurrentPage==0)
            {
                model.CurrentPage = 1;
            }
            string stype = !string.IsNullOrEmpty(Request["stype"]) ? Request["stype"] : "";
            model.PageSize = Convert.ToInt32(Request.QueryString["psize"] ?? "10");
            int pageCount;
            var sort = Convert.ToInt32(_sort);

            var lstWhere = new List<string>();
            if (!string.IsNullOrEmpty(state))
            {
                lstWhere.Add(string.Format("m_state={0}", state));
            }
            if (!string.IsNullOrEmpty(stype))
            {
                if (!string.IsNullOrEmpty(key))
                {
                    if (stype == "1")
                        lstWhere.Add(string.Format("m_loginname like '%{0}%'", key));
                    if (stype == "2")
                        lstWhere.Add(string.Format("m_realname like '%{0}%'", key));
                }
            }
            var where = lstWhere.Count > 0 ? string.Format(" WHERE {0}", string.Join(" AND ", lstWhere)) : "";

            var strSql = string.Format("SELECT * FROM jmp_merchant {0}", where);
            var strOrderBy = string.Format(" ORDER BY m_id {0}", sort == 0 ? "" : "DESC");
            var lst = new JMP.BLL.BllCommonQuery().GetLists<JMP.MDL.jmp_merchant>(strSql, strOrderBy, model.CurrentPage, model.PageSize, out pageCount);
            model.PageCount = pageCount;
            if (lst != null && lst.Count > 0)
            {
                model.Merchants = lst;
            }
            model.ButtonsTags = GetVoidHtml();
            model.MerchantSearchModel.Sort = sort;
            model.MerchantSearchModel.State = state;
            model.MerchantSearchModel.SearchKey = key;
            ViewBag.stype = stype;
            return View(model);
        }
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false), VisitRecord(IsRecord = true)]
        public ActionResult ChoiceMerchantList()
        {

            var model = new ListUserViewModel();
            var key = Request.QueryString["skeys"] ?? "";
            var _sort = Request.QueryString["s_sort"] ?? "0";
            model.CurrentPage = Convert.ToInt32(Request.QueryString["curr"] ?? "1");
            model.PageSize = Convert.ToInt32(Request.QueryString["psize"] ?? "10");
            int pageCount;
            var sort = Convert.ToInt32(_sort);
            string stype = !string.IsNullOrEmpty(Request["stype"]) ? Request["stype"] : "";
            //string dept = System.Configuration.ConfigurationManager.AppSettings["RoleID"];
            var lstWhere = new List<string>();
           
            lstWhere.Add(string.Format("u_state={0}", 1));
            ///lstWhere.Add(string.Format(" u_role_id ={0}", ""+dept+"" ));
            if (!string.IsNullOrEmpty(stype))
            {
                if (!string.IsNullOrEmpty(key))
                {
                    if (stype == "1")
                        lstWhere.Add(string.Format("u_loginname like '%{0}%'", key));
                    if(stype=="2")
                        lstWhere.Add(string.Format("u_realname like '%{0}%'", key));
                }
            }
            var where = lstWhere.Count > 0 ? string.Format(" WHERE {0}", string.Join(" AND ", lstWhere)) : "";

            var strSql = string.Format("SELECT * FROM jmp_locuser {0}", where);
            var strOrderBy = string.Format(" ORDER BY u_id {0}", sort == 0 ? "" : "DESC");
            var lst = new JMP.BLL.BllCommonQuery().GetLists<JMP.MDL.jmp_locuser>(strSql, strOrderBy, model.CurrentPage, model.PageSize, out pageCount);
            model.PageCount = pageCount;
            if (lst != null && lst.Count > 0)
            {
                model.Users = lst;
            }
            model.ButtonsTags = GetVoidHtml();
            ViewBag.stype = stype;
            model.MerchantSearchModel.Sort = sort;
            model.MerchantSearchModel.SearchKey = key;
            return View(model);

    }



    /// <summary>
    /// 判断权限
    /// </summary>
    private string GetVoidHtml()
        {
            string tempStr = string.Empty;
            JMP.BLL.jmp_limit bll_limit = new JMP.BLL.jmp_limit();
            string u_id = UserInfo.UserId.ToString();
            int r_id = UserInfo.UserRoleId;
            //一键解冻
            bool getUidT = bll_limit.GetLocUserLimitVoids("/merchant/thaw", u_id, r_id);
            if (getUidT)
                tempStr += "<li onclick=\"doAll(0)\"><i class='fa fa-check-square-o'></i>一键解冻</li>";
            //一键冻结
            bool getUidF = bll_limit.GetLocUserLimitVoids("/merchant/frozen", u_id, r_id);
            if (getUidF)
                tempStr += "<li onclick=\"doAll(1)\"><i class='fa fa-check-square-o'></i>一键冻结</li>";
            //添加用户
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/merchant/create", u_id, r_id);
            if (getlocuserAdd)
            tempStr += "<li onclick=\"AddDlg()\"><i class='fa fa-plus'></i>添加用户</li>";
            return tempStr;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(JMP.MDL.jmp_merchant model)
        {
            var json = new JsonResponseModel { success = 1, msg = "" };
            var bll = new JMP.BLL.jmp_merchant();
            model.m_loginname = model.m_loginname.Trim();
            var exsits = bll.GetModelList(string.Format("m_loginname='{0}'", model.m_loginname));
            if (exsits.Count > 0)
            {
                json.success = 0;
                json.msg = "登录名已存在";
                return Json(json);
            }
            model.m_realname = model.m_realname.Trim();
            model.m_pwd = DESEncrypt.Encrypt(model.m_pwd);
            bll.Add(model);
            Logger.CreateLog("信息商户信息", model);
            json.success = 1;
            json.msg = "操作成功";
            return Json(json);
        }

        public ActionResult Edit(int id)
        {
            var model = new JMP.BLL.jmp_merchant().GetModel(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            model.m_pwd = DESEncrypt.Decrypt(model.m_pwd);
            return View(model);
        }

        [HttpPost]
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult Edit(JMP.MDL.jmp_merchant model)
        {
            var json = new JsonResponseModel { success = 1, msg = "" };
            var bll = new JMP.BLL.jmp_merchant();
         
            var exsits = bll.GetModelList(string.Format("m_loginname='{0}'", model.m_loginname));
            if (exsits.Count <= 0)
            {
                json.success = 0;
                json.msg = "数据不存在";
                return Json(json);
            }
            model.m_realname = model.m_realname.Trim();
          
            var entity = exsits[0];

            Logger.ModifyLog("修改商户信息", entity, model);

            entity.m_realname = model.m_realname;
            entity.m_pwd = DESEncrypt.Encrypt(model.m_pwd);
           
            bll.Update(entity);
            json.success = 1;
            json.msg = "操作成功";
            return Json(json);
        }

        /// <summary>
        /// 更改商务账号的状态
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateState()
        {
            var ids = Request["ids"] ?? "";
            var state = Request["state"] ?? "";
            if (state.Length <= 0) return Json(new JsonResponseModel { success = 0, msg = "参数错误" });
            var _state = Convert.ToInt32(state);
            var bll = new JMP.BLL.jmp_merchant();
            bll.UpdateState(_state, ids.Trim(','));
            Logger.OperateLog("更改商务账号的状态", "修改商务ID为"+ids +", 状态为"+state);
            return Json(new JsonResponseModel { success = 1, msg = "操作成功" });
        }
        /// <summary>
        /// 商务日志管理
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult MerchLog()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            List<JMP.MDL.jmp_merchantlog> list = new List<JMP.MDL.jmp_merchantlog>();
            JMP.BLL.jmp_merchantlog bll = new JMP.BLL.jmp_merchantlog();
            string where = " where  1=1 ";
            string types = Request["types"];
            string searchKey = Request["searchKey"];
            string sort = Request["sort"];
            string logtype = Request["logtype"];

            if (!string.IsNullOrEmpty(types))
            {
                if (types == "1")//用户编号查询
                {
                    where += " and l_id like '%" + searchKey + "%'";
                }
                else if (types == "2")//用户名称查询
                {
                    where += " and m_realname like '%" + searchKey + "%'";
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
            string sql = string.Format(" select a.*,b.m_realname from dbo.jmp_merchantlog a left join  dbo.jmp_merchant b on a.l_user_id=b.m_id  " + where);
            string order = sort == "0" ? " order by l_id" : " order by l_id desc ";
            list = bll.SelectList(sql, order, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            return View();
        }
    }
}
