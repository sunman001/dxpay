using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using WEB.Util.Logger;
using System.Configuration;
using System.Collections;
using JMP.TOOL;
using TOOL;
using TOOL.Extensions;

namespace WEB.Controllers
{
    public class BusinessPersonnelController : Controller
    {
        //
        // GET: /BusinessPersonnel/

        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;
        JMP.BLL.jmp_limit bll_limit = new JMP.BLL.jmp_limit();
        JMP.BLL.jmp_role bll_role = new JMP.BLL.jmp_role();
        JMP.BLL.CoBusinessPersonnel bll_co = new JMP.BLL.CoBusinessPersonnel();
        List<JMP.MDL.CoBusinessPersonnel> bll_list = new List<JMP.MDL.CoBusinessPersonnel>();
        JMP.MDL.CoBusinessPersonnel co_model = new JMP.MDL.CoBusinessPersonnel();

        #region 平台商务

        /// <summary>
        /// 商务信息列表
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult BusinessPersonnelList()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量

            string s_type = string.IsNullOrEmpty(Request["s_type"]) ? "0" : Request["s_type"];
            string s_keys = string.IsNullOrEmpty(Request["s_keys"]) ? "" : Request["s_keys"];
            string s_state = string.IsNullOrEmpty(Request["s_state"]) ? "" : Request["s_state"];
            ViewBag.Bpurl = System.Configuration.ConfigurationManager.AppSettings["Bpurl"];
            //
            bll_list = bll_co.SelectList(s_type, s_keys, s_state, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = bll_list;

            ViewBag.stype = s_type;
            ViewBag.skeys = s_keys;
            ViewBag.state = s_state;
            //
            ViewBag.locUrl = GetVoidHtml();

            return View();
        }


        /// <summary>
        /// 显示页面方法是否有权限
        /// </summary>
        public string GetVoidHtml()
        {
            string locUrl = "";

            bool getUidT = bll_limit.GetLocUserLimitVoids("/BusinessPersonnel/CoAll(0)", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//本地管理员一键启用
            if (getUidT)
            {
                locUrl += "<li onclick=\"javascript:Updatestate(0)\"><i class='fa fa-check-square-o'></i>一键启用</li>";
            }
            bool getUidF = bll_limit.GetLocUserLimitVoids("/BusinessPersonnel/CoAll(1)", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//本地管理员一键禁用
            if (getUidF)
            {
                locUrl += "<li onclick=\"javascript:Updatestate(1);\"><i class='fa fa-check-square-o'></i>一键禁用</li>";
            }
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/BusinessPersonnel/InsertOrUpdateBusinessPersonnel", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加商务
            if (getlocuserAdd)
            {
                locUrl += "<li onclick=\"Addsw()\"><i class='fa fa-plus'></i>添加商务</li>";
            }
            return locUrl;
        }

        /// <summary>
        /// 添加修改商务信息
        /// </summary>
        /// <returns></returns>
        public ActionResult BusinessPersonnelAdd()
        {
            GetListRote();
            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);

            if (id > 0)
            {
                co_model = bll_co.GetModel(id);
            }

            ViewBag.co_model = co_model;

            return View();
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <returns></returns>
        public void GetListRote()
        {
            List<JMP.MDL.jmp_role> list = new List<JMP.MDL.jmp_role>();
            list = bll_role.GetModelList("r_type=2");
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
        /// 添加修改商务信息方法
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult InsertOrUpdateBusinessPersonnel(JMP.MDL.CoBusinessPersonnel mode)
        {
            object retJson = new { success = 0, msg = "操作失败" };

            string loname = Request["LoginName"];
            string userName = Request["DisplayName"];

            if (mode.Id > 0)
            {
                #region 修改商务

                //修改前数据
                co_model = bll_co.GetModel(mode.Id);
                var modclone = co_model.Clone();
                JMP.MDL.CoBusinessPersonnel co_mod = new JMP.MDL.CoBusinessPersonnel();
                co_model.Password = DESEncrypt.Encrypt(mode.Password);
                co_model.CreatedOn = DateTime.Now;
                co_model.LoginName = mode.LoginName;
                co_model.DisplayName = mode.DisplayName;
                co_model.EmailAddress = mode.EmailAddress;
                co_model.MobilePhone = mode.MobilePhone;
                co_model.QQ = mode.QQ;
                co_model.Website = mode.Website;
                // mode.CreatedById = UserInfo.UserId;
                // mode.CreatedByName = UserInfo.UserName;
                // mode.LoginCount = co_model.LoginCount;
                // mode.State = co_model.State;
                // mode.LogintTime = co_model.LogintTime == null ? null : co_model.LogintTime;

                if (bll_co.Update(co_model))
                {
                    Logger.ModifyLog("修改商务信息", modclone, co_model);

                    retJson = new { success = 1, msg = "修改成功" };
                }
                else
                {
                    retJson = new { success = 0, msg = "修改失败" };
                }


                #endregion
            }
            else
            {
                #region 添加商务

                JMP.MDL.CoBusinessPersonnel co_mod = new JMP.MDL.CoBusinessPersonnel();
                mode.Password = DESEncrypt.Encrypt(mode.Password);
                mode.CreatedOn = DateTime.Now;
                mode.CreatedById = UserInfo.UserId;
                mode.CreatedByName = UserInfo.UserName;
                mode.LoginCount = 0;
                int roleId;
                int.TryParse(ConfigReader.GetSettingValueByKey("RoleID"), out roleId);
                mode.RoleId = roleId;
                mode.State = 0;
                int cg = bll_co.Add(mode);
                if (cg > 0)
                {
                    Logger.CreateLog("添加商务信息", mode);

                    retJson = new { success = 1, msg = "添加成功" };
                }
                else
                {
                    retJson = new { success = 0, msg = "添加失败" };
                }

                #endregion

            }

            return Json(retJson);
        }

        /// <summary>
        /// 批量更新商务状态
        /// </summary>
        /// <param name="coid">商务id列表</param>
        /// <param name="tag">状态</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult CoAll(string coid, int tag)
        {

            bool flag = bll_co.UpdateState(coid, tag);

            //写日志
            if (flag)
            {
                string info = "批量更新商务（" + coid + "）的状态为" + (tag == 0 ? "正常。" : "冻结。");
                Logger.OperateLog("批量更新商务状态", info);
            }
            return Json(new { success = flag ? 1 : 0, msg = flag ? "更新成功！" : "更新失败！" });
        }


        #region 验证方法

        /// <summary>
        /// 验证登录名称是否存在
        /// </summary>
        /// <param name="lname">邮箱地址</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult CheckLoName(string lname, string uid)
        {
            JMP.BLL.CoBusinessPersonnel userBll = new JMP.BLL.CoBusinessPersonnel();
            bool flag = userBll.ExistsLogName(lname, uid);
            return Json(new { success = flag, mess = flag ? "已存在该登录名称！" : "不存在登录名称！" });
        }

        #endregion


        #endregion




    }
}
