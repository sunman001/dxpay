/************聚米支付平台__角色模块************/
//描述：处理角色的功能
//功能：处理角色的功能
//开发者：陶涛
//开发时间: 2016.03.18
/************聚米支付平台__角色模块************/

using JMP.TOOL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB.Util.Logger;

namespace WEB.Controllers
{
    public class ROTEController : Controller
    {
        //
        // GET: /ROTE/
        JMP.BLL.jmp_role bll_role = new JMP.BLL.jmp_role();
        JMP.BLL.jmp_limit bll_limit = new JMP.BLL.jmp_limit();
        JMP.MDL.jmp_role m_role = new JMP.MDL.jmp_role();
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;
        #region 后台角色管理
        [VisitRecord(IsRecord = true)]
        public ActionResult RoteList()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            string sql = "select * from JMP_ROLE where  r_type='0' ";
            if (UserInfo.UserId.ToString() != ConfigurationManager.AppSettings["administrator"].ToString())
            {
                sql += " and r_id !=1 ";
            }
            string Order = " order by r_id desc ";
            List<JMP.MDL.jmp_role> list = new List<JMP.MDL.jmp_role>();
            list = bll_role.SelectList(sql, Order, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            GetVoidHtml(0);
            return View();
        }
        /// <summary>
        /// 验证添加角色按钮
        /// </summary>
        public void GetVoidHtml(int type)
        {
            switch (type)
            {
                case 0:
                    bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/ROTE/AddRole", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加角色
                    if (getlocuserAdd)
                    {
                        ViewBag.locUrl += "<li id=\"ToolBar\" onclick=\"AddRoleDialog()\"><i class='fa fa-plus'></i>添加角色</li>";
                    }
                    break;
                case 1:
                    bool getlocuserAdd1 = bll_limit.GetLocUserLimitVoids("/ROTE/AddUserRote", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加角色
                    if (getlocuserAdd1)
                    {
                        ViewBag.locUrl += "<li id=\"ToolBar\" onclick=\"AddRoleDialog()\"><i class='fa fa-plus'></i>添加角色</li>";
                    }
                    break;
                case 2:
                    bool getlocuserAdd2 = bll_limit.GetLocUserLimitVoids("/ROTE/AddBusinessRote", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加角色
                    if (getlocuserAdd2)
                    {
                        ViewBag.locUrl += "<li id=\"ToolBar\" onclick=\"AddRoleDialog()\"><i class='fa fa-plus'></i>添加角色</li>";
                    }
                    break;
                case 3:
                    bool getlocuserAdd3 = bll_limit.GetLocUserLimitVoids("/ROTE/AddAgentRote", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加角色
                    if (getlocuserAdd3)
                    {
                        ViewBag.locUrl += "<li id=\"ToolBar\" onclick=\"AddRoleDialog()\"><i class='fa fa-plus'></i>添加角色</li>";
                    }
                    break;

            }

        }

        /// <summary>
        /// 添加后台角色界面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddRole()
        {
            return View();
        }
        /// <summary>
        /// 添加后台角色方法
        /// </summary>
        /// <returns></returns>
        public JsonResult AddRoleAjax()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            m_role.r_name = Request["name"];
            m_role.r_value = "";
            m_role.r_state = int.Parse(Request["state"]);
            m_role.r_type = 0;
            int result = bll_role.Add(m_role);
            if (result > 0)
            {
                #region 添加日志

                Logger.CreateLog("添加角色数据", m_role);
                #endregion
                retJson = new { success = 1, msg = "操作成功" };
            }
            return Json(retJson);
        }
        /// <summary>
        /// 修改后台角色界面
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleUpdate()
        {
            m_role = bll_role.GetModel(int.Parse(Request["rid"]));
            ViewBag.data = m_role;
            GetState(m_role.r_state);
            return View();
        }
        /// <summary>
        /// 获取开启或者关闭状态
        /// </summary>
        /// <param name="state"></param>
        public void GetState(int state)
        {
            string htmls = "";
            htmls = " <label for=\"radOrvActnS_0\"><input id=\"radOrvActnS_0\" type=\"radio\" name=\"state\" value=\"1\" checked=\"checked\" />开启</label><label for=\"radOrvActnS_1\"><input id=\"radOrvActnS_1\" type=\"radio\" name=\"state\" value=\"0\"  />关闭</label>";

            if (state == 0)
            {
                htmls = "<label for=\"radOrvActnS_0\"><input id=\"radOrvActnS_0\" type=\"radio\" name=\"state\" value=\"1\"/>开启</label><label for=\"radOrvActnS_1\"><input id=\"radOrvActnS_1\" type=\"radio\" name=\"state\" value=\"0\"  checked=\"checked\"   />关闭</label>";
            }
            ViewBag.state = htmls;
        }
        /// <summary>
        /// 修改后台角色方法
        /// </summary>
        /// <returns></returns>
        public JsonResult RoleUpdateAjax()
        {
            object retJson = new { success = 0, msg = "操作失败" };

            m_role.r_id = int.Parse(Request["rid"]);
            m_role.r_name = Request["name"];
            m_role.r_state = int.Parse(Request["state"]);
            m_role.r_type = 0;
            JMP.MDL.jmp_role old_m_role = bll_role.GetModel(int.Parse(Request["rid"]));
            m_role.r_value = old_m_role.r_value;
            bool result = bll_role.Update(m_role);
            if (result)
            {
                #region 添加日志

                Logger.ModifyLog("操作修改角色页面", old_m_role, m_role); ;
                #endregion
                retJson = new { success = 1, msg = "操作成功" };
            }

            return Json(retJson);
        }

        public ActionResult UpdateRoleLimit()
        {
            int rid = string.IsNullOrEmpty(Request["rid"]) ? 0 :int.Parse(Request["rid"]);
            int type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);
            if (rid > 0)
            {
                m_role = bll_role.GetModel(rid);
            }
            GetFirstHtml(m_role.r_value, type);
            ViewBag.rid = m_role.r_id;
            return View();
        }

        public JsonResult UpdateRoleLimitAjax()
        {
            object retJson = new { success = 0, msg = "操作失败" };

            int rid = int.Parse(Request["rid"]);
            string r_value = Request["value"];
            m_role.r_id = rid;
            m_role.r_value = r_value.Replace(",on", "");
            JMP.MDL.jmp_role old_m_role = bll_role.GetModel(rid);//查询本次更新前的数据
            bool result = bll_role.UpdateValue(m_role);

            if (result)
            {
                Session.Remove("dtSession");
                Session["dtSession"] = bll_limit.GetUserLimitSession(UserInfo.UserId, UserInfo.UserRoleId);
                Logger.OperateLog("角色的权限值更新为", m_role.r_value);


                retJson = new { success = 1, msg = "操作成功" };
            }


            return Json(retJson);
        }
        #endregion
        /// <summary>
        /// 查询一级是否选中
        /// </summary>
        /// <param name="r_value">角色值</param>
        public void GetFirstHtml(string r_value, int type)
        {

            string userlimit = "," + new JMP.BLL.jmp_limit().getrolelimit(Convert.ToInt32(UserInfo.UserRoleId)) + ",";
            if (UserInfo.UserRoleId.ToString() == "1")
            {
                userlimit = "1";
            }
            string where = " l_state=1 "; //
            switch (type)
            {
                case 0:
                    where += " and l_type ='0' ";
                    break;
                case 1:
                    where += " and l_type ='1' ";
                    break;
                case 2:
                    where += " and l_type ='2' ";
                    break;
                case 3:
                    where += " and l_type ='3' ";
                    break;
            }

            List<JMP.MDL.jmp_limit> listF = bll_limit.GetModelList(where);
            ViewBag.htmls += "<div id=\"RolePanel\" class=\"tab-dcnt tab-dcnt-rote\">";
            ViewBag.htmls += "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"table-cnt table-cnt-rote lay-table-cnt\">";
            ViewBag.htmls += "<thead>                        <tr>                            <th class='first'>导航名称</th>                            <th>权限分配</th>                            <th width=\"40\">全选</th>                        </tr>                    </thead>";
            ViewBag.htmls += "<tbody>";
            List<JMP.MDL.jmp_limit> lilili = listF.Where(a => a.l_topid == 0).ToList();
            for (int i = 0; i < lilili.Count; i++)
            {
                if (userlimit == "1" || userlimit.Contains("," + lilili[i].l_id.ToString() + ","))
                {
                    ViewBag.htmls += "<tr>";
                    ViewBag.htmls += "<td>";
                    ViewBag.htmls += "<span class=\"folder-open\">" + lilili[i].l_name + "</span>";
                    ViewBag.htmls += "</td>";
                    ViewBag.htmls += "<td>";
                    ViewBag.htmls += "<span class=\"cbllist\" id=\"child_" + lilili[i].l_id + "\">";
                    ViewBag.htmls += "<input type=\"checkbox\" onclick=\"childChecked(this)\" name=\"chk_menu\" id=\"chk_menu_" + lilili[i].l_id + "\" class=\"checkall\" " + GetCheck(r_value, lilili[i].l_id) + " value=\"" + lilili[i].l_id + "\" style=\"vertical-align: middle;\" /><label for=\"chk_menu_" + lilili[i].l_id + "\">显示</label>";
                    ViewBag.htmls += "</span>";
                    ViewBag.htmls += "<td align=\"center\"><input name=\"checkAll\" child=\"child_" + listF[i].l_id + "\" type=\"checkbox\"  /></td>";
                    ViewBag.htmls += "</tr>";
                    GetSecondHtml(userlimit, r_value, lilili[i].l_id, listF);
                }
            }
            ViewBag.htmls += "</tbody>";
            ViewBag.htmls += "</table>";
            ViewBag.htmls += "</div>";
        }

        /// <summary>
        /// 查询二级是否选中
        /// </summary>
        /// <param name="r_value">角色值</param>
        ///  <param name="l_id">上级id</param>
        /// <returns></returns>
        public void GetSecondHtml(string rolelimit, string r_value, int l_id, List<JMP.MDL.jmp_limit> listF)
        {
            List<JMP.MDL.jmp_limit> listS = listF.Where(a => a.l_topid == l_id).ToList();// ll_limit.GetModelList(" l_topid=" + l_id);
            foreach (var item in listS)
            {
                if (rolelimit == "1" || rolelimit.Contains("," + item.l_id.ToString() + ","))
                {
                    ViewBag.htmls += "<tr>";
                    ViewBag.htmls += "<td>";
                    ViewBag.htmls += "<span class=\"folder-line\">" + item.l_name + "</span>";
                    ViewBag.htmls += "</td>";
                    ViewBag.htmls += "<td>";
                    ViewBag.htmls += "<span class=\"cbllist\" id=\"child_" + item.l_id + "\">";
                    ViewBag.htmls += "<input type=\"checkbox\" onclick=\"childChecked(this)\" name=\"chk_menu2\" id=\"chk_menu_" + item.l_id + "\" class=\"checkall\" value=\"" + item.l_id + "\" style=\"vertical-align: middle;\" " + GetCheck(r_value, item.l_id) + " /><label for=\"chk_menu_" + item.l_id + "\">显示</label>";
                    GetThirdHtml(rolelimit, r_value, item.l_id, listF);
                    ViewBag.htmls += "</td>";

                    ViewBag.htmls += "<td align=\"center\">";
                    ViewBag.htmls += "<input name=\"checkAll\" child=\"child_" + item.l_id + "\" type=\"checkbox\" /></td>";

                    ViewBag.htmls += "</tr>";
                }
            }
        }

        /// <summary>
        /// 查询三级是否选中
        /// </summary>
        /// <param name="r_value">角色值</param>
        ///  <param name="l_id">上级id</param>
        /// <returns></returns>
        public void GetThirdHtml(string rolelimit, string r_value, int l_id, List<JMP.MDL.jmp_limit> listF)
        {
            List<JMP.MDL.jmp_limit> listT = listF.Where(a => a.l_topid == l_id).ToList();// bll_limit.GetModelList(" l_topid=" + l_id);
            foreach (var item in listT)
            {
                if (rolelimit == "1" || rolelimit.Contains("," + item.l_id.ToString() + ","))
                {
                    ViewBag.htmls += "<input type=\"checkbox\" onclick=\"childChecked(this)\" name=\"chk_menu\" id=\"chk_menu_" + item.l_id + "\" class=\"checkall\" value=\"" + item.l_id + "\" style=\"vertical-align: middle;\" " + GetCheck(r_value, item.l_id) + " /><label for=\"chk_menu_" + item.l_id + "\">" + item.l_name + "</label>";
                }
            }
        }

        /// <summary>
        /// 返回check是否选中
        /// </summary>
        /// <param name="r_value">权限值</param>
        /// <param name="l_id">对比值</param>
        /// <returns></returns>
        public string GetCheck(string r_value, int l_id)
        {
            string check = "";
            string[] strSplit = r_value.Split(',');
            foreach (var item in strSplit)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    if (l_id == int.Parse(item))
                    {
                        check = " checked=\"checked\"";
                    }
                }
            }
            return check;
        }

        #region 开发者角色管理
        /// <summary>
        /// 开发者角色管理
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult UserRoteList()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            string sql = "select r_id, r_name, r_value, r_state, r_type  from JMP_ROLE where  r_type='1'  ";
            List<JMP.MDL.jmp_role> list = new List<JMP.MDL.jmp_role>();
            string Order = "order by r_id desc";
            list = bll_role.SelectList(sql, Order, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            GetVoidHtml(1);
            return View();
        }

        /// <summary>
        /// 添加前台角色界面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddUserRote()
        {
            return View();
        }
        /// <summary>
        /// 添加后台角色方法
        /// </summary>
        /// <returns></returns>
        public JsonResult AddUserRoleAjax()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            m_role.r_name = Request["name"];
            m_role.r_value = "";
            m_role.r_state = int.Parse(Request["state"]);
            m_role.r_type = 1;
            int result = bll_role.Add(m_role);
            if (result > 0)
            {
                #region 添加日志
                Logger.CreateLog("操作添加开发者色页面，进行开发者角色添加", m_role);
                #endregion
                retJson = new { success = 1, msg = "操作成功" };
            }
            return Json(retJson);
        }
        /// <summary>
        /// 修改后台角色界面
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleUserUpdate()
        {
            m_role = bll_role.GetModel(int.Parse(Request["rid"]));
            ViewBag.data = m_role;
            GetState(m_role.r_state);
            return View();
        }
        /// <summary>
        /// 修改前台角色方法
        /// </summary>
        /// <returns></returns>
        public JsonResult RoleUserUpdateAjax()
        {
            object retJson = new { success = 0, msg = "操作失败" };

            m_role.r_id = int.Parse(Request["rid"]);
            m_role.r_name = Request["name"];
            m_role.r_state = int.Parse(Request["state"]);
            m_role.r_type = 1;
            JMP.MDL.jmp_role old_m_role = bll_role.GetModel(int.Parse(Request["rid"]));

            bool result = bll_role.Update(m_role);

            if (result)
            {
                #region 添加日志
                Logger.ModifyLog("操作修改角色页面", old_m_role, m_role);

                #endregion
                retJson = new { success = 1, msg = "操作成功" };
            }

            return Json(retJson);
        }
        #endregion

        #region 商务角色管理
        /// <summary>
        /// 商务角色管理
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult BusinessRoteList()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            string sql = "select r_id, r_name, r_value, r_state, r_type  from JMP_ROLE where  r_type='2'  ";
            List<JMP.MDL.jmp_role> list = new List<JMP.MDL.jmp_role>();
            string Order = "order by r_id desc";
            list = bll_role.SelectList(sql, Order, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            GetVoidHtml(2);
            return View();
        }

        /// <summary>
        /// 添加前台角色界面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddBusinessRote()
        {
            return View();
        }
        /// <summary>
        /// 添加后台角色方法
        /// </summary>
        /// <returns></returns>
        public JsonResult AddBusinessRoleAjax()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            m_role.r_name = Request["name"];
            m_role.r_value = "";
            m_role.r_state = int.Parse(Request["state"]);
            m_role.r_type = 2;
            int result = bll_role.Add(m_role);
            if (result > 0)
            {
                #region 添加日志
                Logger.CreateLog("操作添加商务角色页面，进行商务角色添加", m_role);
                #endregion
                retJson = new { success = 1, msg = "操作成功" };
            }
            return Json(retJson);
        }
        /// <summary>
        /// 修改商务角色界面
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleBusinessUpdate()
        {
            m_role = bll_role.GetModel(int.Parse(Request["rid"]));
            ViewBag.data = m_role;
            GetState(m_role.r_state);
            return View();
        }
        /// <summary>
        /// 修改商务角色方法
        /// </summary>
        /// <returns></returns>
        public JsonResult RoleBusinessUpdateAjax()
        {
            object retJson = new { success = 0, msg = "操作失败" };

            m_role.r_id = int.Parse(Request["rid"]);
            m_role.r_name = Request["name"];
            m_role.r_state = int.Parse(Request["state"]);
            m_role.r_type = 2;
            JMP.MDL.jmp_role old_m_role = bll_role.GetModel(int.Parse(Request["rid"]));
            m_role.r_value = old_m_role.r_value;
            bool result = bll_role.Update(m_role);
            if (result)
            {
                #region 添加日志
                Logger.ModifyLog("操作修改商务角色页面", old_m_role, m_role);

                #endregion
                retJson = new { success = 1, msg = "操作成功" };
            }

            return Json(retJson);
        }
        #endregion

        #region 代理商角色管理
        /// <summary>
        /// 商务角色管理
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult AgentRoteList()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            string sql = "select r_id, r_name, r_value, r_state, r_type  from JMP_ROLE where  r_type='3'  ";
            List<JMP.MDL.jmp_role> list = new List<JMP.MDL.jmp_role>();
            string Order = "order by r_id desc";
            list = bll_role.SelectList(sql, Order, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            GetVoidHtml(3);
            return View();
        }

        /// <summary>
        /// 添加前台角色界面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddAgentRote()
        {
            return View();
        }
        /// <summary>
        /// 添加后台角色方法
        /// </summary>
        /// <returns></returns>
        public JsonResult AddAgentRoleAjax()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            m_role.r_name = Request["name"];
            m_role.r_value = "";
            m_role.r_state = int.Parse(Request["state"]);
            m_role.r_type = 3;
            int result = bll_role.Add(m_role);
            if (result > 0)
            {
                #region 添加日志
                Logger.CreateLog("操作添加代理商角色页面，进行代理商角色添加", m_role);
                #endregion
                retJson = new { success = 1, msg = "操作成功" };
            }
            return Json(retJson);
        }
        /// <summary>
        /// 修改商务角色界面
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleAgentUpdate()
        {
            m_role = bll_role.GetModel(int.Parse(Request["rid"]));
            ViewBag.data = m_role;
            GetState(m_role.r_state);
            return View();
        }
        /// <summary>
        /// 修改商务角色方法
        /// </summary>
        /// <returns></returns>
        public JsonResult RoleAgentUpdateAjax()
        {
            object retJson = new { success = 0, msg = "操作失败" };

            m_role.r_id = int.Parse(Request["rid"]);
            m_role.r_name = Request["name"];
            m_role.r_state = int.Parse(Request["state"]);
            m_role.r_type = 3;
            JMP.MDL.jmp_role old_m_role = bll_role.GetModel(int.Parse(Request["rid"]));
            m_role.r_value = old_m_role.r_value;
            bool result = bll_role.Update(m_role);
            if (result)
            {
                #region 添加日志
                Logger.ModifyLog("操作修改代理商角色页面", old_m_role, m_role);

                #endregion
                retJson = new { success = 1, msg = "操作成功" };
            }

            return Json(retJson);
        }
        #endregion
    }
}
