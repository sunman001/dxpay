/************聚米支付平台__本地管理员模块************/
//描述：处理本地管理员的功能
//功能：处理本地管理员的功能
//开发者：陶涛
//开发时间: 2016.03.16
/************聚米支付平台__本地管理员模块************/

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
    public class LOCUSERController : Controller
    {
        //
        // GET: /LOCUSER/

        JMP.BLL.jmp_limit bll_limit = new JMP.BLL.jmp_limit();
        JMP.BLL.jmp_locuser bll_localuser = new JMP.BLL.jmp_locuser();
        JMP.BLL.jmp_role bll_role = new JMP.BLL.jmp_role();
        public JMP.MDL.jmp_locuser mol_locuser = new JMP.MDL.jmp_locuser();
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;
        [VisitRecord(IsRecord = true)]
        public ActionResult UsersList()
        {

            #region 分页列表数据查询
            int admin = Int32.Parse(ConfigurationManager.AppSettings["administrator"].ToString());
            ViewBag.admin = admin;
            int userid = UserInfo.UserId;
            ViewBag.userid = userid;
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            string where = " where 1=1 ";
            string Order = " order by u_id desc";
            string name = Request["name"];
            string type = Request["type"];
            string state = Request["state"];
            string sort = Request["sort"];
            if (!string.IsNullOrEmpty(type))
            {
                if (type == "1")
                {
                    where += " and u_id like '%" + name + "%' ";
                }
                else
                {
                    where += " and u_loginname like '%" + name + "%' ";
                }
            }
            if (!string.IsNullOrEmpty(state))
            {
                if (state == "1")
                {
                    where += " and u_state=1 ";
                }
                else if (state == "0")
                {
                    where += " and u_state=0 ";
                }
            }
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "0")
                {
                    Order = " order by u_id";
                }
            }
            if (ViewBag.admin != ViewBag.userid)
            {
                where += " and u_id !=" + ViewBag.admin;
            }

            string sql = "select * from JMP_LOCUSER" + where;
            List<JMP.MDL.jmp_locuser> list = new List<JMP.MDL.jmp_locuser>();
            list = bll_localuser.SelectList(sql, Order, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.name = name;
            ViewBag.type = type;
            ViewBag.list = list;
            ViewBag.state = state;
            ViewBag.sort = sort;
            #endregion
            GetVoidHtml();
            return View();
        }


        /// <summary>
        /// 显示页面方法是否有权限
        /// </summary>
        public void GetVoidHtml()
        {
            string yanzhenTop = "<div class=\"date-tool\"><div id=\"ToolBar\" class=\" help\">";
            string yanzhenBottom = "</div></div>";

            bool getUidT = bll_limit.GetLocUserLimitVoids("/LOCUSER/AjaxUpdateLcoUserState", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//本地管理员一键启用
            if (getUidT)
            {
                ViewBag.locUrl += "<li onclick=\"getUid(1)\"><i class='fa fa-check-square-o'></i>一键启用</li>";
            }
            bool getUidF = bll_limit.GetLocUserLimitVoids("/LOCUSER/AjaxUpdateLcoUserState", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//本地管理员一键禁用
            if (getUidF)
            {
                ViewBag.locUrl += "<li onclick=\"getUid(0)\"><i class='fa fa-check-square-o'></i>一键禁用</li>";
            }
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/LOCUSER/AjaxAddUser", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加管理员
            if (getlocuserAdd)
            {
                ViewBag.locUrl += "<li onclick=\"AddLocuserDialog()\"><i class='fa fa-plus'></i>添加用户</li>";
            }

            if (getUidT || getUidF || getlocuserAdd)
            {
                ViewBag.locUrl = yanzhenTop + ViewBag.locUrl + yanzhenBottom;
            }
            else
            {
                ViewBag.locUrl = "";
            }
        }

        public JsonResult AjaxUpdateLcoUserState()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            int state = int.Parse(Request["state"]);
            string str = Request["ids"];
            if (str.CompareTo("On") > 0)
            {
                str = str.Substring(3);
            }
            bool result = bll_localuser.UpdateLocUserState(str, state);

            if (result)
            {
                #region 添加日志
                string info = "";
                if (state == 1)
                {
                    info = "对用户ID为（" + str + "）的状态更新为启用。";
                }
                else
                {
                    info = "对用户ID为（" + str + "）的状态更新为停用。";
                }

                Logger.OperateLog("操作批量更新本地管理员状态页面，进行用户状态批量更新", info);
                #endregion
                retJson = new { success = 1, msg = "操作成功" };
            }

            return Json(retJson);
        }


        public ActionResult AddUsers()
        {
            GetListRote();
            return View();
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <returns></returns>
        public void GetListRote()
        {
            List<JMP.MDL.jmp_role> list = new List<JMP.MDL.jmp_role>();
            string where = " r_type = 0 ";
            int admin = Int32.Parse(ConfigurationManager.AppSettings["administrator"].ToString());
            int userid = UserInfo.UserId;
            if (admin != userid)
            {
                where += " and r_id !=1 ";
            }
            list = bll_role.GetModelList(where);

            string strHtml = "";
            for (int i = 0; i < list.Count; i++)
            {
                if (i == 0)
                {
                    strHtml += "<option value=" + list[i].r_id + " select>" + list[i].r_name + "</option>";
                }
                else
                {
                    strHtml += "<option value=" + list[i].r_id + ">" + list[i].r_name + "</option>";
                }


            }
            ViewBag.strHtml = strHtml;
        }

        /// <summary>
        /// 保存用户（添加页面）
        /// </summary>
        /// <returns></returns>
        public JsonResult AjaxAddUser()
        {
            object retJson = new { success = 0, msg = "操作失败" };

            JMP.MDL.jmp_locuser mol_localuser = new JMP.MDL.jmp_locuser();
            mol_localuser.u_loginname = Request["name"];
            mol_localuser.u_pwd = JMP.TOOL.DESEncrypt.Encrypt(Request["pwd"]);
            mol_localuser.u_realname = Request["realName"];
            mol_localuser.u_department = Request["department"];
            mol_localuser.u_position = Request["position"];
            mol_localuser.u_role_id = int.Parse(Request["roteId"]);
            mol_localuser.u_state = int.Parse(Request["state"]);
            mol_localuser.u_mobilenumber = Request["mobilenumber"];
            mol_localuser.u_emailaddress = Request["emailaddress"];
            mol_localuser.u_qq = Request["qq"];
            if (bll_localuser.ExistsName(mol_localuser.u_loginname))
            {
                retJson = new { success = 2, msg = "用户名重复，请重新填写" };
            }
            else
            {
                int result = bll_localuser.Add(mol_localuser);
                if (result >= 0)
                {
                    #region 添加日志

                    Logger.CreateLog("添加本地管理员用户", mol_localuser);
                    #endregion
                    retJson = new { success = 1, msg = "操作成功" };
                }
            }
            return Json(retJson);
        }

        /// <summary>
        /// 用户编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateUsers()
        {
            int uid = int.Parse(Request["u_id"]);
            int admin = Int32.Parse(ConfigurationManager.AppSettings["administrator"].ToString());
            ViewBag.admin = admin;
            mol_locuser = bll_localuser.GetModel(uid);
            ViewBag.model = mol_locuser;
            GetListRote(mol_locuser.u_role_id);
            GetState(mol_locuser.u_state);
            return View();
        }

        /// <summary>
        /// 获取权限下拉列表
        /// </summary>
        /// <param name="roteId"></param>
        public void GetListRote(int roteId)
        {
            List<JMP.MDL.jmp_role> list = new List<JMP.MDL.jmp_role>();
            string where = " r_type = 0 ";
            int admin = Int32.Parse(ConfigurationManager.AppSettings["administrator"].ToString());
            int userid = UserInfo.UserId;
            if (admin != userid)
            {
                where += " and r_id !=1 ";
            }

            list = bll_role.GetModelList(where);
            string strHtml = "";
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].r_id == roteId)
                {
                    strHtml += "<option value=" + list[i].r_id + " selected=\"selected\">" + list[i].r_name + "</option>";
                }
                else
                {
                    strHtml += "<option value=" + list[i].r_id + ">" + list[i].r_name + "</option>";
                }


            }
            ViewBag.rote = strHtml;
        }

        /// <summary>
        /// 获取用户状态
        /// </summary>
        /// <param name="state"></param>
        public void GetState(int state)
        {
            string htmls = "";
            htmls = "<label for=\"radOrvActnS_0\"><input id=\"radOrvActnS_0\" type=\"radio\" name=\"state\" value=\"1\" checked=\"checked\" />开启</label><label for=\"radOrvActnS_1\"><input id=\"radOrvActnS_1\" type=\"radio\" name=\"state\" value=\"0\"  />关闭</label>";

            if (state == 0)
            {
                htmls = "<label for=\"radOrvActnS_0\"><input id=\"radOrvActnS_0\" type=\"radio\" name=\"state\" value=\"1\"/>开启</label><label for=\"radOrvActnS_1\"><input id=\"radOrvActnS_1\" type=\"radio\" name=\"state\" value=\"0\"  checked=\"checked\"   />关闭</label>";
            }
            ViewBag.state = htmls;
        }

        /// <summary>
        /// 保存用户（编辑页面）
        /// </summary>
        /// <returns></returns>
        public JsonResult AjaxUpdateUser()
        {
            object retJson = new { success = 0, msg = "操作失败" };

            JMP.MDL.jmp_locuser mol_localuser = new JMP.MDL.jmp_locuser();
            JMP.MDL.jmp_locuser old_mol_localuser = new JMP.MDL.jmp_locuser();

            old_mol_localuser = bll_localuser.GetModel(int.Parse(Request["id"]));

            mol_localuser.u_pwd = JMP.TOOL.DESEncrypt.Encrypt(Request["pwd"]);
            mol_localuser.u_realname = Request["realName"];
            mol_localuser.u_department = Request["department"];
            mol_localuser.u_position = Request["position"];
            mol_localuser.u_role_id = int.Parse(Request["roteId"]);
            mol_localuser.u_state = int.Parse(Request["state"]);
            mol_localuser.u_id = int.Parse(Request["id"]);
            mol_localuser.u_loginname = Request["name"];
            mol_localuser.u_mobilenumber = Request["mobilenumber"];
            mol_localuser.u_emailaddress = Request["emailaddress"];
            mol_localuser.u_qq = Request["qq"];
            bool result = false;
            if (!bll_localuser.ExistsName(mol_localuser.u_loginname, mol_localuser.u_id.ToString()))
                result = bll_localuser.Update(mol_localuser);

            if (result)
            {


                Logger.ModifyLog("修改用户", old_mol_localuser, mol_localuser);

                retJson = new { success = 1, msg = "操作成功" };
            }
            return Json(retJson);
        }

        /// <summary>
        /// 是否存在登录名
        /// </summary>
        /// <param name="u_name">登录名</param>
        /// <param name="u_id">用户id</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult ExistsUserName(string u_name, string u_id)
        {
            bool flag = bll_localuser.ExistsName(u_name, u_id);
            return Json(new { success = flag, mess = flag ? "已存在该登录名！" : "不存在该登录名！" });
        }

    }
}
