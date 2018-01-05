/************聚米支付平台__权限模块************/
//描述：处理权限的功能
//功能：处理权限的功能
//开发者：陶涛
//开发时间: 2016.03.16
/************聚米支付平台__权限模块************/

using JMP.TOOL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB.Models.Limit;
using WEB.Util.Logger;

namespace WEB.Controllers
{
    public class LIMITController : Controller
    {
        //
        // GET: /LIMIT/

        /// <summary>
        /// 日志收集器
        /// </summary>
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;

        JMP.BLL.jmp_limit bll_limit = new JMP.BLL.jmp_limit();
        JMP.MDL.jmp_limit m_limit = new JMP.MDL.jmp_limit();

        #region 后台权限管理
        /// <summary>
        /// 管理后台权限列表
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult LimitList()
        {
            GetVoidHtml();
            GetSelectNewHtml(Request["topid"], 0);
            #region 分页数据列表
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            string where = " where 1=1 and l_type='0' ";
            string Order = "order by l_id desc";
            string name = Request["name"];
            string type = Request["type"];
            if (!string.IsNullOrEmpty(type))
            {
                if (type == "1")
                {
                    where += " and l_id like '%" + name + "%' ";
                }
                else if (type == "2")
                {
                    where += " and l_name like '%" + name + "%' ";
                }
                else if (type == "3")
                {
                    where += " and l_url like '%" + name + "%' ";
                }
            }
            string state = Request["state"];
            if (!string.IsNullOrEmpty(state))
            {
                if (state == "1")
                {
                    where += " and l_state=1 ";
                }
                else if (state == "0")
                {
                    where += " and l_state=0 ";
                }
            }
            string sort = Request["sort"];
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "0")
                {
                    Order = "order by l_id  ";
                }
            }
            string topid = Request["topid"];
            if (!string.IsNullOrEmpty(topid))
            {
                where += " and l_topid=" + topid;
            }
            string sql = "select * from jmp_limit" + where;
            List<JMP.MDL.jmp_limit> list = new List<JMP.MDL.jmp_limit>();
            list = bll_limit.SelectList(sql, Order, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.name = name;
            ViewBag.type = type;
            ViewBag.list = list;
            ViewBag.state = state;
            ViewBag.sort = sort;
            ViewBag.topid = topid;
            #endregion
            return View();
        }

        /// <summary>
        /// 显示页面方法是否有权限
        /// </summary>
        public void GetVoidHtml()
        {
            string yanzhenTop = "<div class=\"date-tool\"><div id=\"ToolBar\" class=\" help\">";
            string yanzhenBottom = "</div></div>";
            bool getUidT = bll_limit.GetLocUserLimitVoids("/LIMIT/AjaxLimitState", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//权限一键启用
            if (getUidT)
            {
                ViewBag.locUrlHtml += "<li onclick=\"getLid(1)\"><i class='fa fa-check-square-o'></i>一键启用</li>";
            }
            bool getUidF = bll_limit.GetLocUserLimitVoids("/LIMIT/AjaxLimitState", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//权限一键禁用
            if (getUidF)
            {
                ViewBag.locUrlHtml += "<li onclick=\"getLid(0)\"><i class='fa fa-check-square-o'></i>一键禁用</li>";
            }
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/LIMIT/AjaxAddLimit", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加权限
            if (getlocuserAdd)
            {
                ViewBag.locUrlHtml += "<li onclick=\"AddLimitDialog()\"><i class='fa fa-plus'></i>添加权限</li>";
            }

            if (getUidT || getUidF || getlocuserAdd)
            {
                ViewBag.locUrlHtml = yanzhenTop + ViewBag.locUrlHtml + yanzhenBottom;
            }
            else
            {
                ViewBag.locUrlHtml = "";
            }
        }

        /// <summary>
        /// 查询页面下拉框显示
        /// </summary>
        public void GetSelectNewHtml(string topid, int type)
        {
            if (string.IsNullOrEmpty(topid))
            {
                ViewBag.selectTopid += "<option selected=\"selected\" value=\"\">所有</option>";
            }
            else
            {
                ViewBag.selectTopid += "<option value>所有</option>";
            }
            if (topid == "0")
            {

                ViewBag.selectTopid += "<option value=\"0\"  selected=\"selected\">父级</option>";
            }
            else
            {
                ViewBag.selectTopid += "<option value=\"0\">父级</option>";
            }
            string where = "l_state=1 ";
            switch (type)
            {
                case 0:
                    where += " and l_type='0' ";
                    break;
                case 1:
                    where += " and l_type='1' ";
                    break;
                case 2:
                    where += " and l_type='2' ";
                    break;
                case 3:
                    where += " and l_type='3' ";
                    break;
            }
            List<JMP.MDL.jmp_limit> listF = bll_limit.GetModelList(where);//获取父类
            foreach (var item in listF.Where(a => a.l_topid == 0))
            {
                if (item.l_id.ToString() == topid)
                {
                    ViewBag.selectTopid += "<option value=\"" + item.l_id + "\"  selected=\"selected\"> ├ " + item.l_name + "</option>";
                }
                else
                {
                    ViewBag.selectTopid += "<option value=\"" + item.l_id + "\"> ├ " + item.l_name + "</option>";
                }
                List<JMP.MDL.jmp_limit> listS = listF.Where(a => a.l_topid == item.l_id).ToList();
                foreach (var itemS in listS)
                {
                    if (itemS.l_id.ToString() == topid)
                    {
                        ViewBag.selectTopid += "<option value=\"" + itemS.l_id + "\"  selected=\"selected\">&nbsp;&nbsp;&nbsp;├ " + itemS.l_name + "</option>";
                    }
                    else
                    {
                        ViewBag.selectTopid += "<option value=\"" + itemS.l_id + "\">&nbsp;&nbsp;&nbsp;├ " + itemS.l_name + "</option>";
                    }
                    //List<JMP.MDL.jmp_limit> listT = listF.Where(a => a.l_topid == itemS.l_id).ToList();
                    //foreach (var itemT in listT)
                    //{
                    //    if (itemS.l_id.ToString() == topid)
                    //    {
                    //        ViewBag.selectTopid += "<option value=\"" + itemT.l_id + "\"  selected=\"selected\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;├ " + itemT.l_name + "</option>";
                    //    }
                    //    else
                    //    {
                    //        ViewBag.selectTopid += "<option value=\"" + itemT.l_id + "\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;├ " + itemT.l_name + "</option>";
                    //    }
                    //}
                }
            }
        }

        public JsonResult AjaxLimitState()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            int state = int.Parse(Request["state"]);
            string str = Request["ids"];
            if (str.CompareTo("On") > 0)
            {
                str = str.Substring(3);
            }

            bool result = bll_limit.UpdateLimitState(str, state);
            if (result)
            {
                #region 添加日志
                string info = "";
                if (state == 1)
                {
                    info = "对权限ID为" + str + "的状态更新为启用。";
                }
                else
                {
                    info = "对权限ID为" + str + "的状态更新为停用。";
                }
                //AddLocLog.AddLog(int.Parse(UserInfo.UserId), 3, RequestHelper.GetClientIp(), "操作权限批量更新页面，进行权限状态批量更新", info);
                Logger.OperateLog("操作权限批量更新页面，进行权限状态批量更新", info);
                #endregion
                retJson = new { success = 1, msg = "操作成功" };
            }
            return Json(retJson);
        }

        /// <summary>
        /// 添加权限页面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult AddLimit()
        {
            GetZTreeJSON(0, 0, 0);
            string where = "l_topid=0 and  l_type='0' and  l_state='1' ";
            List<JMP.MDL.jmp_limit> listF = bll_limit.GetModelList(where);//获取父类
            ViewBag.list = listF;
            return View();
        }

        /// <summary>
        /// 获取权限第二级菜单子类
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public string SelectLimitChirdID()
        {
            int sellimit_id = string.IsNullOrEmpty(Request["sellimit_id"].ToString()) ? -1 : int.Parse(Request["sellimit_id"].ToString());

            JMP.BLL.jmp_limit bll = new JMP.BLL.jmp_limit();
            DataTable dt = bll.GetList(" 1=1 and l_state=1 and  l_topid='" + sellimit_id + "' order by l_sort desc  ").Tables[0];
            string yyzl = "<option value = '-1'> --请选择-- </option>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Int32.Parse(dt.Rows[i]["l_id"].ToString()) == sellimit_id)
                {
                    yyzl += " <option value='" + dt.Rows[i]["l_id"] + "' selected=selected >" + dt.Rows[i]["l_name"] + "</option>";
                }
                else
                {
                    yyzl += " <option value='" + dt.Rows[i]["l_id"] + "' >" + dt.Rows[i]["l_name"] + "</option>";
                }
            }
            return yyzl;
        }

        /// <summary>
        /// 获取权限第三级菜单子类
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public string SelectLimitthirdID()
        {
            int sellimit_chirdid = string.IsNullOrEmpty(Request["sellimit_chirdid"].ToString()) ? -1 : int.Parse(Request["sellimit_chirdid"].ToString());

            JMP.BLL.jmp_limit bll = new JMP.BLL.jmp_limit();
            DataTable dt = bll.GetList(" 1=1 and l_state=1 and  l_topid='" + sellimit_chirdid + "' order by l_sort desc  ").Tables[0];
            string yyzl = "<option value = '-1'> --请选择-- </option>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Int32.Parse(dt.Rows[i]["l_id"].ToString()) == sellimit_chirdid)
                {
                    yyzl += " <option value='" + dt.Rows[i]["l_id"] + "' selected=selected >" + dt.Rows[i]["l_name"] + "</option>";
                }
                else
                {
                    yyzl += " <option value='" + dt.Rows[i]["l_id"] + "' >" + dt.Rows[i]["l_name"] + "</option>";
                }
            }
            return yyzl;
        }

        [LoginCheckFilter(IsCheck = false, IsRole = false)]
        public void GetZTreeJSON(int id, int type, int topid)
        {
            List<ZtreeModel> treelist = new List<ZtreeModel>();
            string where = "l_state=1 and l_type=" + type;
            List<JMP.MDL.jmp_limit> listF = bll_limit.GetModelList(where);//获取父类
            int parentId = 0;
            JMP.MDL.jmp_limit parent;
            //先添加父级
            treelist.Add(new ZtreeModel
            {
                id = 0,
                pId = 0,
                name = "父级",
                open = true,
                nocheck = false,
                @checked = true
            });
            foreach (var item in listF)
            {
                var hasParent = listF.Count(x => x.l_id == item.l_topid) > 0;
                if (!hasParent && item.l_topid != 0)
                {
                    continue;
                }
                // var hasChild = listF.Count(x => x.l_topid == item.l_id) > 0;
                if (item.l_id == topid)
                {
                    parent = listF.FirstOrDefault(x => x.l_id == item.l_topid);
                    if (parent != null)
                    {
                        parentId = parent.l_id;
                    }
                    var treefj = treelist.Find(x => x.id == 0);
                    if (treefj != null)
                    {
                        treefj.@checked = false;
                    }
                    treelist.Add(new ZtreeModel
                    {
                        id = item.l_id,
                        pId = item.l_topid,
                        name = item.l_name,
                        open = true,
                        nocheck = false,
                        @checked = true
                    });
                }
                else
                {
                    treelist.Add(new ZtreeModel
                    {
                        id = item.l_id,
                        pId = item.l_topid,
                        name = item.l_name,
                        open = false,
                        nocheck = false,
                        @checked = false
                    });
                }

            }

            //如果此权限是父类选中
            if (id > 0 && topid == 0)
            {
                var treefj = treelist.Find(x => x.id == 0);
                if (treefj != null)
                {
                    treefj.@checked = true;
                }
            }
            var treeparent = treelist.Find(x => x.id == parentId);
            if (treeparent != null)
            {
                treeparent.open = true;
            }
            ViewBag.curNode = JsonHelper.Serialize(treelist.FirstOrDefault(x => x.id == id));
            ViewBag.selectTopid = JsonHelper.Serialize(treelist);
        }


        /// <summary>
        /// 添加页面下拉框显示
        /// </summary>
        public void GetSelectNewHtmlAdd(string topid, int type)
        {
            if (topid == "0")
            {

                ViewBag.selectTopid += "<option value=\"0\" data-type=0 selected=\"selected\">父级</option>";
            }
            else
            {
                ViewBag.selectTopid += "<option value=\"0\" data-type=0>父级</option>";
            }
            string where = "l_topid=0";
            switch (type)
            {
                case 0:
                    where += " and l_type='0' ";
                    break;
                case 1:
                    where += " and l_type='1' ";
                    break;
                case 2:
                    where += " and l_type='2' ";
                    break;
                case 3:
                    where += " and l_type='3' ";
                    break;
            }

            List<JMP.MDL.jmp_limit> listF = bll_limit.GetModelList(where);//获取父类
            foreach (var item in listF)
            {
                if (item.l_id.ToString() == topid)
                {
                    ViewBag.selectTopid += "<option value=\"" + item.l_id + "\" data-type=1 selected=\"selected\"> ├ " + item.l_name + "</option>";
                }
                else
                {
                    ViewBag.selectTopid += "<option value=\"" + item.l_id + "\" data-type=1> ├ " + item.l_name + "</option>";
                }
                List<JMP.MDL.jmp_limit> listS = bll_limit.GetModelList(" l_topid=" + item.l_id);
                foreach (var itemS in listS)
                {
                    if (itemS.l_id.ToString() == topid)
                    {
                        ViewBag.selectTopid += "<option value=\"" + itemS.l_id + "\"  selected=\"selected\" data-type=2>&nbsp;&nbsp;&nbsp;├ " + itemS.l_name + "</option>";
                    }
                    else
                    {
                        ViewBag.selectTopid += "<option value=\"" + itemS.l_id + "\" data-type=2>&nbsp;&nbsp;&nbsp;├ " + itemS.l_name + "</option>";
                    }
                    List<JMP.MDL.jmp_limit> listT = bll_limit.GetModelList(" l_topid=" + itemS.l_id);
                    foreach (var itemT in listT)
                    {
                        if (itemT.l_id.ToString() == topid)
                        {
                            ViewBag.selectTopid += "<option value=\"" + itemT.l_id + "\"  selected=\"selected\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;├ " + itemT.l_name + "</option>";
                        }
                        else
                        {
                            ViewBag.selectTopid += "<option value=\"" + itemT.l_id + "\" data-type=3>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;├ " + itemT.l_name + "</option>";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 添加权限方法
        /// </summary>
        /// <returns></returns>
        public JsonResult AjaxAddLimit()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            m_limit.l_name = Request["name"];
            m_limit.l_url = Request["url"];
            m_limit.l_sort = int.Parse(Request["values"]);
            m_limit.l_topid = int.Parse(Request["topid"]);
            m_limit.l_state = int.Parse(Request["state"]);
            m_limit.l_icon = Request["icon"];
            m_limit.l_type = 0;
            int result = bll_limit.Add(m_limit);
            if (result > 0)
            {
                #region 添加日志
                //AddLocLog.AddLog(int.Parse(UserInfo.UserId), 3, RequestHelper.GetClientIp(), "操作添加权限页面，进行权限值添加", "添加权限");
                Logger.CreateLog("操操作添加权限页面，进行权限值添加", m_limit);
                #endregion
                retJson = new { success = 1, msg = "操作成功" };
            }

            return Json(retJson);
        }

        /// <summary>
        /// 更新权限页面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult UpdateLimit()
        {
            int lid = int.Parse(Request["lid"]);
            var dataLimit = bll_limit.GetModel(lid);
            ViewBag.m_limit = dataLimit;
            GetZTreeJSON(dataLimit.l_id, 0, dataLimit.l_topid);
            // GetSelectNewHtmlAdd(dataLimit.l_topid.ToString(), 0);
            string where = "l_topid=0 and  l_type='0' and  l_state='1' ";
            List<JMP.MDL.jmp_limit> listF = bll_limit.GetModelList(where);//获取父类
            ViewBag.list = listF;
            GetVoidHtml(dataLimit.l_url);
            return View();
        }

        /// <summary>
        /// 获取第三层的方法数据
        /// </summary>
        /// <param name="urls">链接地址里面得</param>
        public void GetVoidHtml(string urls)
        {
            string[] str = urls.Split(',');
            string display = "style=\"display:none;\"";
            string voidValue = "";
            if (str.Length > 1)
            {
                display = "";
                voidValue = str[1];
            }
            ViewBag.topid2HtmlVoid += "<dl  id=\"Voids\" " + display + ">";
            ViewBag.topid2HtmlVoid += "<dt>权限方法：</dt>";
            ViewBag.topid2HtmlVoid += "<dd>";
            ViewBag.topid2HtmlVoid += "<div class=\"single-input normal\">";
            ViewBag.topid2HtmlVoid += "<span class=\"ie7-input-bug-start\"></span>";
            ViewBag.topid2HtmlVoid += "<input name=\"inputVoid\" type=\"text\" id=\"inputVoid\" value=\"" + voidValue + "\"  onblur=\"changes()\" /><span class=\"ie7-input-bug-end\"></span>";
            ViewBag.topid2HtmlVoid += "</div>";
            ViewBag.topid2HtmlVoid += "<div class=\"Validform_checktip\" id=\"checkCode\">* 权限方法不能为空</div>";
            ViewBag.topid2HtmlVoid += "</dd>";
            ViewBag.topid2HtmlVoid += "</dl>";
        }

        /// <summary>
        /// 修改权限方法
        /// </summary>
        /// <returns></returns>
        public JsonResult AjaxUpdateLimit()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            m_limit.l_id = int.Parse(Request["lid"]);
            m_limit.l_name = Request["name"];
            m_limit.l_url = Request["url"];
            m_limit.l_sort = int.Parse(Request["values"]);
            m_limit.l_topid = int.Parse(Request["topid"]);
            m_limit.l_state = int.Parse(Request["state"]);
            m_limit.l_icon = Request["icon"];
            m_limit.l_type = 0;
            JMP.MDL.jmp_limit old_m_limit = new JMP.MDL.jmp_limit();
            old_m_limit = bll_limit.GetModel(int.Parse(Request["lid"]));//查询本次更新前的数据

            bool result = bll_limit.Update(m_limit);
            if (result)
            {
                #region 添加日志

                #endregion
                Logger.ModifyLog("操作更新权限页面，进行权限修改", old_m_limit, m_limit);


                retJson = new { success = 1, msg = "操作成功" };
            }
            return Json(retJson);
        }
        #endregion

        #region 开发者权限管理
        /// <summary>
        /// 开发者权限管理界面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult UserLimit()
        {
            UserGetVoidHtml();
            GetSelectNewHtml(Request["topid"], 1);
            #region 分页数据列表
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            string where = " where 1=1 and l_type='1' ";
            string Order = "order by l_sort desc";
            string name = Request["name"];
            string type = Request["type"];
            if (!string.IsNullOrEmpty(type))
            {
                if (type == "1")
                {
                    where += " and l_id like '%" + name + "%' ";
                }
                else if (type == "2")
                {
                    where += " and l_name like '%" + name + "%' ";
                }
                else if (type == "3")
                {
                    where += " and l_url like '%" + name + "%' ";
                }
            }
            string state = Request["state"];
            if (!string.IsNullOrEmpty(state))
            {
                if (state == "1")
                {
                    where += " and l_state=1 ";
                }
                else if (state == "0")
                {
                    where += " and l_state=0 ";
                }
            }
            string sort = Request["sort"];
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "0")
                {
                    Order = " order by l_sort";
                }
            }
            string topid = Request["topid"];
            if (!string.IsNullOrEmpty(topid))
            {
                where += " and l_topid=" + topid;
            }
            string sql = "select * from jmp_limit" + where;
            List<JMP.MDL.jmp_limit> list = new List<JMP.MDL.jmp_limit>();
            list = bll_limit.SelectList(sql, Order, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.name = name;
            ViewBag.type = type;
            ViewBag.list = list;
            ViewBag.state = state;
            ViewBag.sort = sort;
            ViewBag.topid = topid;
            #endregion
            return View();
        }
        /// <summary>
        /// 显示页面方法是否有权限
        /// </summary>
        public void UserGetVoidHtml()
        {
            string yanzhenTop = "<div class=\"date-tool\"><div id=\"ToolBar\" class=\" help\">";
            string yanzhenBottom = "</div></div>";
            bool getUidT = bll_limit.GetLocUserLimitVoids("/LIMIT/AjaxLimitState", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//权限一键启用
            if (getUidT)
            {
                ViewBag.locUrlHtml += "<li onclick=\"getLid(1)\"><i class='fa fa-check-square-o'></i>一键启用</li>";
            }
            bool getUidF = bll_limit.GetLocUserLimitVoids("/LIMIT/AjaxLimitState", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//权限一键禁用
            if (getUidF)
            {
                ViewBag.locUrlHtml += "<li onclick=\"getLid(0)\"><i class='fa fa-check-square-o'></i>一键禁用</li>";
            }
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/LIMIT/addUserLimit", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加权限
            if (getlocuserAdd)
            {
                ViewBag.locUrlHtml += "<li onclick=\"AddLimitDialog()\"><i class='fa fa-plus'></i>添加权限</li>";
            }

            if (getUidT || getUidF || getlocuserAdd)
            {
                ViewBag.locUrlHtml = yanzhenTop + ViewBag.locUrlHtml + yanzhenBottom;
            }
            else
            {
                ViewBag.locUrlHtml = "";
            }
        }

        /// <summary>
        /// 添加开发者权限界面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult InsertUserLimit()
        {
            GetSelectNewHtmlAdd("0", 1);
            return View();
        }

        /// <summary>
        /// 添加开发者权限方法
        /// </summary>
        /// <returns></returns>
        public JsonResult addUserLimit()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            m_limit.l_name = Request["name"];
            m_limit.l_url = Request["url"];
            m_limit.l_sort = int.Parse(Request["values"]);
            m_limit.l_topid = int.Parse(Request["topid"]);
            m_limit.l_state = int.Parse(Request["state"]);
            m_limit.l_icon = Request["icon"];
            m_limit.l_type = 1;

            int result = bll_limit.Add(m_limit);
            if (result > 0)
            {
                #region 添加日志

                Logger.CreateLog("操作前台添加权限页面，进行权限值添加", m_limit);
                #endregion
                retJson = new { success = 1, msg = "操作成功" };
            }

            return Json(retJson);
        }

        /// <summary>
        /// 更新开发者权限页面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult UserUpdateLimit()
        {
            int lid = int.Parse(Request["lid"]);
            var dataLimit = bll_limit.GetModel(lid);
            ViewBag.m_limit = dataLimit;
            GetSelectNewHtmlAdd(dataLimit.l_topid.ToString(), 1);
            GetVoidHtml(dataLimit.l_url);
            return View();
        }

        /// <summary>
        /// 修改开发者权限方法
        /// </summary>
        /// <returns></returns>
        public JsonResult UserAjaxUpdateLimit()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            m_limit.l_id = int.Parse(Request["lid"]);
            m_limit.l_name = Request["name"];
            m_limit.l_url = Request["url"];
            m_limit.l_sort = int.Parse(Request["values"]);
            m_limit.l_topid = int.Parse(Request["topid"]);
            m_limit.l_state = int.Parse(Request["state"]);
            m_limit.l_icon = Request["icon"];
            m_limit.l_type = 1;
            JMP.MDL.jmp_limit old_m_limit = new JMP.MDL.jmp_limit();
            old_m_limit = bll_limit.GetModel(int.Parse(Request["lid"]));//查询本次更新前的数据

            bool result = bll_limit.Update(m_limit);
            if (result)
            {


                Logger.ModifyLog("操作更新权限页面，进行权限修改", old_m_limit, m_limit);

                retJson = new { success = 1, msg = "操作成功" };
            }
            return Json(retJson);
        }
        #endregion


        #region 商务权限管理
        /// <summary>
        ///  商务权限管理界面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult BusinessLimit()
        {
            BusinessGetVoidHtml();
            GetSelectNewHtml(Request["topid"], 2);
            #region 分页数据列表
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            string where = " where 1=1 and l_type='2' ";
            string Order = "order by l_sort desc";
            string name = Request["name"];
            string type = Request["type"];
            if (!string.IsNullOrEmpty(type))
            {
                if (type == "1")
                {
                    where += " and l_id like '%" + name + "%' ";
                }
                else if (type == "2")
                {
                    where += " and l_name like '%" + name + "%' ";
                }
                else if (type == "3")
                {
                    where += " and l_url like '%" + name + "%' ";
                }
            }
            string state = Request["state"];
            if (!string.IsNullOrEmpty(state))
            {
                if (state == "1")
                {
                    where += " and l_state=1 ";
                }
                else if (state == "0")
                {
                    where += " and l_state=0 ";
                }
            }
            string sort = Request["sort"];
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "0")
                {
                    Order = " order by l_sort";
                }
            }
            string topid = Request["topid"];
            if (!string.IsNullOrEmpty(topid))
            {
                where += " and l_topid=" + topid;
            }
            string sql = "select * from jmp_limit" + where;
            List<JMP.MDL.jmp_limit> list = new List<JMP.MDL.jmp_limit>();
            list = bll_limit.SelectList(sql, Order, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.name = name;
            ViewBag.type = type;
            ViewBag.list = list;
            ViewBag.state = state;
            ViewBag.sort = sort;
            ViewBag.topid = topid;
            #endregion
            return View();
        }
        /// <summary>
        /// 显示页面方法是否有权限
        /// </summary>
        public void BusinessGetVoidHtml()
        {
            string yanzhenTop = "<div class=\"date-tool\"><div id=\"ToolBar\" class=\" help\">";
            string yanzhenBottom = "</div></div>";
            bool getUidT = bll_limit.GetLocUserLimitVoids("/LIMIT/AjaxLimitState", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//权限一键启用
            if (getUidT)
            {
                ViewBag.locUrlHtml += "<li onclick=\"getLid(1)\"><i class='fa fa-check-square-o'></i>一键启用</li>";
            }
            bool getUidF = bll_limit.GetLocUserLimitVoids("/LIMIT/AjaxLimitState", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//权限一键禁用
            if (getUidF)
            {
                ViewBag.locUrlHtml += "<li onclick=\"getLid(0)\"><i class='fa fa-check-square-o'></i>一键禁用</li>";
            }
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/LIMIT/addBusinessLimit", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加权限
            if (getlocuserAdd)
            {
                ViewBag.locUrlHtml += "<li onclick=\"AddLimitDialog()\"><i class='fa fa-plus'></i>添加权限</li>";
            }

            if (getUidT || getUidF || getlocuserAdd)
            {
                ViewBag.locUrlHtml = yanzhenTop + ViewBag.locUrlHtml + yanzhenBottom;
            }
            else
            {
                ViewBag.locUrlHtml = "";
            }
        }

        /// <summary>
        /// 添加商务权限界面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult InsertBusinessLimit()
        {
            GetSelectNewHtmlAdd("0", 2);
            return View();
        }

        /// <summary>
        /// 添加开发者权限方法
        /// </summary>
        /// <returns></returns>
        public JsonResult addBusinessLimit()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            m_limit.l_name = Request["name"];
            m_limit.l_url = Request["url"];
            m_limit.l_sort = int.Parse(Request["values"]);
            m_limit.l_topid = int.Parse(Request["topid"]);
            m_limit.l_state = int.Parse(Request["state"]);
            m_limit.l_icon = Request["icon"];
            m_limit.l_type = 2;

            int result = bll_limit.Add(m_limit);
            if (result > 0)
            {
                #region 添加日志

                Logger.CreateLog("操作商务添加权限页面，进行权限值添加", m_limit);
                #endregion
                retJson = new { success = 1, msg = "操作成功" };
            }

            return Json(retJson);
        }

        /// <summary>
        /// 更新商务权限页面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult BusinessUpdateLimit()
        {
            int lid = int.Parse(Request["lid"]);
            var dataLimit = bll_limit.GetModel(lid);
            ViewBag.m_limit = dataLimit;
            GetSelectNewHtmlAdd(dataLimit.l_topid.ToString(), 2);
            GetVoidHtml(dataLimit.l_url);
            return View();
        }

        /// <summary>
        /// 修改商务权限方法
        /// </summary>
        /// <returns></returns>
        public JsonResult BusinessAjaxUpdateLimit()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            m_limit.l_id = int.Parse(Request["lid"]);
            m_limit.l_name = Request["name"];
            m_limit.l_url = Request["url"];
            m_limit.l_sort = int.Parse(Request["values"]);
            m_limit.l_topid = int.Parse(Request["topid"]);
            m_limit.l_state = int.Parse(Request["state"]);
            m_limit.l_icon = Request["icon"];
            m_limit.l_type = 2;
            JMP.MDL.jmp_limit old_m_limit = new JMP.MDL.jmp_limit();
            old_m_limit = bll_limit.GetModel(int.Parse(Request["lid"]));//查询本次更新前的数据

            bool result = bll_limit.Update(m_limit);
            if (result)
            {


                Logger.ModifyLog("操作更新权限页面，进行权限修改", old_m_limit, m_limit);

                retJson = new { success = 1, msg = "操作成功" };
            }
            return Json(retJson);
        }
        #endregion

        #region 代理商权限管理
        /// <summary>
        ///  商务权限管理界面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult AgentLimit()
        {
            AgentGetVoidHtml();
            GetSelectNewHtml(Request["topid"], 3);
            #region 分页数据列表
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            string where = " where 1=1 and l_type='3' ";
            string Order = "order by l_sort desc";
            string name = Request["name"];
            string type = Request["type"];
            if (!string.IsNullOrEmpty(type))
            {
                if (type == "1")
                {
                    where += " and l_id like '%" + name + "%' ";
                }
                else if (type == "2")
                {
                    where += " and l_name like '%" + name + "%' ";
                }
                else if (type == "3")
                {
                    where += " and l_url like '%" + name + "%' ";
                }
            }
            string state = Request["state"];
            if (!string.IsNullOrEmpty(state))
            {
                if (state == "1")
                {
                    where += " and l_state=1 ";
                }
                else if (state == "0")
                {
                    where += " and l_state=0 ";
                }
            }
            string sort = Request["sort"];
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "0")
                {
                    Order = " order by l_sort";
                }
            }
            string topid = Request["topid"];
            if (!string.IsNullOrEmpty(topid))
            {
                where += " and l_topid=" + topid;
            }
            string sql = "select * from jmp_limit" + where;
            List<JMP.MDL.jmp_limit> list = new List<JMP.MDL.jmp_limit>();
            list = bll_limit.SelectList(sql, Order, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.name = name;
            ViewBag.type = type;
            ViewBag.list = list;
            ViewBag.state = state;
            ViewBag.sort = sort;
            ViewBag.topid = topid;
            #endregion
            return View();
        }
        /// <summary>
        /// 显示页面方法是否有权限
        /// </summary>
        public void AgentGetVoidHtml()
        {
            string yanzhenTop = "<div class=\"date-tool\"><div id=\"ToolBar\" class=\" help\">";
            string yanzhenBottom = "</div></div>";
            bool getUidT = bll_limit.GetLocUserLimitVoids("/LIMIT/AjaxLimitState", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//权限一键启用
            if (getUidT)
            {
                ViewBag.locUrlHtml += "<li onclick=\"getLid(1)\"><i class='fa fa-check-square-o'></i>一键启用</li>";
            }
            bool getUidF = bll_limit.GetLocUserLimitVoids("/LIMIT/AjaxLimitState", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//权限一键禁用
            if (getUidF)
            {
                ViewBag.locUrlHtml += "<li onclick=\"getLid(0)\"><i class='fa fa-check-square-o'></i>一键禁用</li>";
            }
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/LIMIT/addAgentLimit", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加权限
            if (getlocuserAdd)
            {
                ViewBag.locUrlHtml += "<li onclick=\"AddLimitDialog()\"><i class='fa fa-plus'></i>添加权限</li>";
            }

            if (getUidT || getUidF || getlocuserAdd)
            {
                ViewBag.locUrlHtml = yanzhenTop + ViewBag.locUrlHtml + yanzhenBottom;
            }
            else
            {
                ViewBag.locUrlHtml = "";
            }
        }

        /// <summary>
        /// 添加商务权限界面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult InsertAgentLimit()
        {
            GetSelectNewHtmlAdd("0", 3);
            return View();
        }

        /// <summary>
        /// 添加开发者权限方法
        /// </summary>
        /// <returns></returns>
        public JsonResult addAgentLimit()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            m_limit.l_name = Request["name"];
            m_limit.l_url = Request["url"];
            m_limit.l_sort = int.Parse(Request["values"]);
            m_limit.l_topid = int.Parse(Request["topid"]);
            m_limit.l_state = int.Parse(Request["state"]);
            m_limit.l_icon = Request["icon"];
            m_limit.l_type = 3;
            int result = bll_limit.Add(m_limit);
            if (result > 0)
            {
                #region 添加日志

                Logger.CreateLog("操作商务添加权限页面，进行权限值添加", m_limit);
                #endregion
                retJson = new { success = 1, msg = "操作成功" };
            }

            return Json(retJson);
        }

        /// <summary>
        /// 更新商务权限页面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult AgentUpdateLimit()
        {
            int lid = int.Parse(Request["lid"]);
            var dataLimit = bll_limit.GetModel(lid);
            ViewBag.m_limit = dataLimit;
            GetSelectNewHtmlAdd(dataLimit.l_topid.ToString(), 3);
            GetVoidHtml(dataLimit.l_url);
            return View();
        }

        /// <summary>
        /// 修改商务权限方法
        /// </summary>
        /// <returns></returns>
        public JsonResult AgentAjaxUpdateLimit()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            m_limit.l_id = int.Parse(Request["lid"]);
            m_limit.l_name = Request["name"];
            m_limit.l_url = Request["url"];
            m_limit.l_sort = int.Parse(Request["values"]);
            m_limit.l_topid = int.Parse(Request["topid"]);
            m_limit.l_state = int.Parse(Request["state"]);
            m_limit.l_icon = Request["icon"];
            m_limit.l_type = 3;
            JMP.MDL.jmp_limit old_m_limit = new JMP.MDL.jmp_limit();
            old_m_limit = bll_limit.GetModel(int.Parse(Request["lid"]));//查询本次更新前的数据

            bool result = bll_limit.Update(m_limit);
            if (result)
            {


                Logger.ModifyLog("操作更新权限页面，进行权限修改", old_m_limit, m_limit);

                retJson = new { success = 1, msg = "操作成功" };
            }
            return Json(retJson);
        }
        #endregion
    }
}
