/************聚米支付平台__开发者管理************/
//描述：开发者管理
//功能：开发者管理
//开发者：谭玉科
//开发时间: 2016.03.16
/************聚米支付平台__开发者管理************/
using System;
using System.Web;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using JMP.BLL;
using JMP.TOOL;
using System.Text;
using TOOL;
using WEB.Models;
using WEB.Models.AppUser;
using WEB.Util.DropdownDataSource;
using WEB.Util.Logger;
using TOOL.Extensions;
using WEB.ViewModel.Developer;

namespace WEB.Controllers
{
    /// <summary>
    /// 类名：AppUserController
    /// 功能：开发者管理
    /// 详细：开发者管理
    /// 修改日期：2016.03.16
    /// </summary>
    public class AppUserController : Controller
    {
        /// <summary>
        /// 日志收集器
        /// </summary>
		private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;

        //代理
        JMP.BLL.CoAgent bllAgent = new JMP.BLL.CoAgent();
        JMP.MDL.CoAgent modelAgent = new JMP.MDL.CoAgent();
        List<JMP.MDL.CoAgent> listAgent = new List<JMP.MDL.CoAgent>();
        //商务
        JMP.BLL.CoBusinessPersonnel bll_co = new JMP.BLL.CoBusinessPersonnel();
        List<JMP.MDL.CoBusinessPersonnel> bll_list = new List<JMP.MDL.CoBusinessPersonnel>();
        JMP.MDL.CoBusinessPersonnel co_model = new JMP.MDL.CoBusinessPersonnel();
        //开发者
        JMP.BLL.jmp_user bll_user = new JMP.BLL.jmp_user();
        JMP.MDL.jmp_user model_user = new JMP.MDL.jmp_user();

        #region 开发者列表页面
        /// <summary>
        /// 开发者list页面
        /// </summary>
        /// <returns>视图</returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult AppUserList()
        {
            #region 初始化
            //获取开发者平台地址
            string Userurl = System.Configuration.ConfigurationManager.AppSettings["Userurl"];
            ViewBag.Userurl = Userurl;
            //获取请求参数
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量
            string type = string.IsNullOrEmpty(Request["stype"]) ? "0" : Request["stype"];//查询条件类型
            string sea_name = string.IsNullOrEmpty(Request["skeys"]) ? "" : Request["skeys"].Trim();//查询条件值
            string stat = string.IsNullOrEmpty(Request["state"]) ? "1" : Request["state"];//用户状态
            string tag = string.IsNullOrEmpty(Request["scheck"]) ? "" : Request["scheck"];//审核状态
            string category = string.IsNullOrEmpty(Request["scategory"]) ? "" : Request["scategory"];//认证类型
            int px = string.IsNullOrEmpty(Request["s_sort"]) ? 1 : Int32.Parse(Request["s_sort"]);//排序
            string RiskM = string.IsNullOrEmpty(Request["RiskM"]) ? "" : Request["RiskM"];//风控资料

            int relation_type = string.IsNullOrEmpty(Request["relation_type"]) ? -1 : int.Parse(Request["relation_type"]);
            //string RoleID = System.Configuration.ConfigurationManager.AppSettings["RoleID"];
            //获取用户列表
            string where = "where 1=1";
            if (!string.IsNullOrEmpty(type.ToString()))
            {
                if (!string.IsNullOrEmpty(sea_name))
                {
                    switch (type)
                    {
                        case "0":
                            where += string.Format(" and  u.u_email like '%{0}%'", sea_name);
                            break;
                        case "1":
                            where += string.Format(" and  u.u_realname like '%{0}%'", sea_name);
                            break;
                        case "4":
                            where += string.Format(" and u.u_name like '%{0}%'", sea_name);
                            break;
                        case "7":
                            where += string.Format(" and (i.DisplayName like '%{0}%' or o.DisplayName like '%{0}%')", sea_name);
                            break;
                        case "8":
                            where += string.Format(" and u_id ='" + sea_name + "'");
                            break;
                    }
                }
            }
            if (!string.IsNullOrEmpty(stat))
                where += string.Format(" and u.u_state={0}", stat);
            if (!string.IsNullOrEmpty(tag))
                where += string.Format(" and u.u_auditstate={0}", tag);
            if (!string.IsNullOrEmpty(category))
                where += string.Format(" and u.u_category={0}", category);
            if (relation_type > -1)
            {
                where += string.Format(" and  u.relation_type={0}", relation_type);
            }
            if (!string.IsNullOrEmpty(RiskM))
            {
                switch (RiskM)
                {
                    case "0":
                        where += " and IsSignContract=1 and IsRecord=1";
                        break;
                    case "1":
                        where += " and IsSignContract=0 and IsRecord=0";
                        break;
                }

            }

            string Order = " order by u_id " + (px == 0 ? "" : " desc ") + " ";
            JMP.BLL.jmp_user bll = new JMP.BLL.jmp_user();
            string query = "select  u.IsSignContract,u.IsRecord, u.u_id,relation_type,u.u_category,u.u_idnumber,u.u_photo,u.u_blicense,u.u_blicensenumber,u.u_count,u.u_state,u.u_auditstate,u.u_topid,u.u_address,u.u_email,u.u_role_id,u.u_password,u.u_realname,u.u_phone,u.u_qq,u.u_bankname,u.u_name,u.u_account,u.u_time,u.u_auditor,u.IsSpecialApproval,u.SpecialApproval,i.DisplayName as sw,o.DisplayName as dls,g.ServiceFeeRatio  from jmp_user as u left join CoBusinessPersonnel as i on u.relation_person_id = i.Id and u.relation_type = 1 left join CoAgent as o on u.relation_person_id = o.Id and u.relation_type = 2 left join CoServiceFeeRatioGrade as g on g.Id=u.ServiceFeeRatioGradeId " + where;
            var list = bll.GetAppUserMerchantLists(query, Order, pageIndexs, PageSize, out pageCount);
            //返回
            ViewBag.CurrPage = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.PageCount = pageCount;
            ViewBag.stype = type;
            ViewBag.skeys = sea_name;
            ViewBag.state = stat;
            ViewBag.scategory = category;
            ViewBag.scheck = tag;
            ViewBag.s_sort = px;
            ViewBag.list = list;
            ViewBag.btnstr = GetVoidHtml();
            ViewBag.relation_type = relation_type;
            ViewBag.RiskM = RiskM;

            #endregion
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
            bool getUidT = bll_limit.GetLocUserLimitVoids("/AppUser/doAll(1)", u_id, r_id);
            if (getUidT)
                tempStr += "<li onclick=\"doAll(1)\"><i class='fa fa-check-square-o'></i>一键解冻</li>";
            //一键冻结
            bool getUidF = bll_limit.GetLocUserLimitVoids("/AppUser/doAll(0)", u_id, r_id);
            if (getUidF)
                tempStr += "<li onclick=\"doAll(0)\"><i class='fa fa-check-square-o'></i>一键冻结</li>";
            //添加用户
            var getlocuserAdd = bll_limit.GetLocUserLimitVoids("/AppUser/AddUser", u_id, r_id);
            if (getlocuserAdd)
                tempStr += "<li onclick=\"AddDlg()\"><i class='fa fa-plus'></i>添加用户</li>";
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
            JMP.BLL.jmp_user userBll = new JMP.BLL.jmp_user();
            bool flag = userBll.UpdateState(uids, tag);
            //写日志
            if (flag)
            {
                string info = "批量更新开发者（" + uids + "）的状态为" + (tag == 1 ? "正常。" : "冻结。");
                Logger.OperateLog("批量更新开发者状态", info);
            }
            return Json(new { success = flag ? 1 : 0, msg = flag ? "更新成功！" : "更新失败！" });
        }

        #endregion

        #region 添加用户


        /// <summary>
        /// 添加用户页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AppUserAdd()
        {
            return View();
        }

        /// <summary>
        /// 保存用户（新增）
        /// </summary>
        /// <param name="jmpuser"></param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult AddUser(JMP.MDL.jmp_user jmpuser)
        {
            JMP.BLL.CoServiceFeeRatioGrade grade_bll = new JMP.BLL.CoServiceFeeRatioGrade();
            //查询默认费率
            JMP.MDL.CoServiceFeeRatioGrade grade_model = grade_bll.GetModelById();

            object obj = new { success = 0, msg = "添加失败！" };
            JMP.BLL.jmp_user userBll = new JMP.BLL.jmp_user();
            jmpuser.u_password = DESEncrypt.Encrypt(jmpuser.u_password);
            jmpuser.u_role_id = int.Parse(ConfigurationManager.AppSettings["JSRoleID"]);
            try
            {
                if (jmpuser.u_category == 0)
                {
                    jmpuser.u_photo = string.IsNullOrEmpty(jmpuser.u_photo) ? "" : jmpuser.u_photo;
                }
                else
                {
                    jmpuser.u_blicense = string.IsNullOrEmpty(jmpuser.u_blicense) ? "" : jmpuser.u_blicense;
                    jmpuser.u_photo = string.IsNullOrEmpty(jmpuser.u_photo) ? " " : jmpuser.u_photo;
                }
                jmpuser.u_qq = string.IsNullOrEmpty(jmpuser.u_qq) ? "" : jmpuser.u_qq;
                jmpuser.u_address = string.IsNullOrEmpty(jmpuser.u_address) ? " " : jmpuser.u_address;
                jmpuser.ServiceFeeRatioGradeId = string.IsNullOrEmpty(grade_model.Id.ToString()) ? 0 : grade_model.Id;
                jmpuser.u_time = DateTime.Now;

                bool flag = false;
                if (!userBll.ExistsEmail(jmpuser.u_email))
                    flag = userBll.Add(jmpuser) > 0;
                obj = new { success = flag ? 1 : 0, msg = flag ? "添加成功！" : "添加失败！" };
                //写日志
                if (flag)
                {

                    Logger.CreateLog("添加开发者", jmpuser);

                }
            }
            catch (Exception ex)
            {
                obj = new { success = 0, msg = "添加异常！" };

                Logger.OperateLog("添加开发者报错", ex.ToString());
            }
            return Json(obj);
        }

        /// <summary>
        /// 弹出(代理商)
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult AppUserAddTc()
        {

            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            //查询类型
            int s_type = string.IsNullOrEmpty(Request["stype"]) ? 0 : int.Parse(Request["stype"]);

            //关键字
            string s_keys = string.IsNullOrEmpty(Request["skeys"]) ? "" : Request["skeys"];
            //账号状态
            string status = string.IsNullOrEmpty(Request["status"]) ? "0" : Request["status"];
            //审核状态
            string AuditState = string.IsNullOrEmpty(Request["AuditState"]) ? "1" : Request["AuditState"];
            //排序
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? -1 : int.Parse(Request["searchDesc"]);
            //查询
            listAgent = bllAgent.SelectList(s_type, s_keys, status, AuditState, searchDesc, pageIndexs, PageSize, out pageCount);

            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = listAgent;
            ViewBag.stype = s_type;
            ViewBag.skeys = s_keys;
            ViewBag.status = status;
            ViewBag.AuditState = AuditState;
            ViewBag.searchDesc = searchDesc;

            return View();
        }

        /// <summary>
        /// 弹出(商务)
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult AppUserAddBpTc()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量

            string s_type = string.IsNullOrEmpty(Request["s_type"]) ? "1" : Request["s_type"];
            string s_keys = string.IsNullOrEmpty(Request["s_keys"]) ? "" : Request["s_keys"];
            string s_state = string.IsNullOrEmpty(Request["s_state"]) ? "0" : Request["s_state"];

            //
            bll_list = bll_co.SelectList(s_type, s_keys, s_state, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = bll_list;

            ViewBag.stype = s_type;
            ViewBag.skeys = s_keys;
            ViewBag.state = s_state;


            return View();

        }

        /// <summary>
        /// 开发者审核
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult AppUserAuditing()
        {
            int userid = string.IsNullOrEmpty(Request["userid"]) ? 0 : int.Parse(Request["userid"]);
            JMP.BLL.jmp_user bll = new JMP.BLL.jmp_user();
            JMP.MDL.jmp_user model = bll.GetModel(userid);
            ViewBag.usermodel = model;
            //审核状态
            ViewBag.start = model.u_auditstate;
            ViewBag.userid = userid;
            return View();
        }
        /// <summary>
        /// 风控资料审核页面
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult AppUserRiskSh()
        {
            int userid = string.IsNullOrEmpty(Request["userid"]) ? 0 : int.Parse(Request["userid"]);
            JMP.BLL.jmp_user bll = new JMP.BLL.jmp_user();
            JMP.MDL.jmp_user model = bll.GetModel(userid);
            ViewBag.usermodel = model;
            return View();
        }
        public JsonResult CheckRiskSH()
        {
            int id = string.IsNullOrEmpty(Request["userid"]) ? 0 : int.Parse(Request["userid"]);
            int IsSignContract = string.IsNullOrEmpty(Request["IsSignContract"]) ? 0 : int.Parse(Request["IsSignContract"]);
            int IsRecord = string.IsNullOrEmpty(Request["IsRecord"]) ? 0 : int.Parse(Request["IsRecord"]);
            JMP.BLL.jmp_user bll = new JMP.BLL.jmp_user();
            bool flag = bll.UpdateRiskSH(id, IsSignContract, IsRecord);
            if (flag)
            {
                string info = "审核风控资料信息（" + id + "）的签订合同状态为" + IsSignContract + ",产品备案状态为" + IsRecord + "";
                Logger.OperateLog("审核风控资料信息", info);
            }

            return Json(new { success = flag ? 1 : 0, msg = flag ? "风控资料审核成功！" : "风控资料审核失败！" });
        }

        /// <summary>
        /// 审核开发者
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult CheckAuditing()
        {

            int id = string.IsNullOrEmpty(Request["userid"]) ? 0 : int.Parse(Request["userid"]);
            int u_auditstate = string.IsNullOrEmpty(Request["u_auditstate"]) ? 0 : int.Parse(Request["u_auditstate"]);
            string name = UserInfo.UserName;

            JMP.BLL.jmp_user bll = new JMP.BLL.jmp_user();
            bool flag = bll.UpdateAuditState(id, u_auditstate, name);

            if (flag)
            {
                string info = "审核开发者状态（" + id + "）的状态为" + u_auditstate + "";
                Logger.OperateLog("审核开发者状态", info);
            }

            return Json(new { success = flag ? 1 : 0, msg = flag ? "审核成功！" : "审核失败！" });
        }

        #endregion

        #region 编辑用户

        public ActionResult AppUserEdit()
        {
            int userId = !string.IsNullOrEmpty(Request["uid"]) ? int.Parse(Request["uid"]) : 0;
            JMP.BLL.jmp_user bll = new JMP.BLL.jmp_user();
            JMP.MDL.jmp_user model = bll.GetModel(userId);
            ViewBag.UserData = model;
            ViewBag.UploadUrl = ConfigurationManager.AppSettings["imgurl"];
            string DisplayName = "";
            //
            switch (model.relation_type)
            {
                case 1:

                    co_model = bll_co.GetModel(model.relation_person_id);
                    if (co_model != null)
                    {
                        DisplayName = co_model.DisplayName;

                    }

                    break;
                case 2:

                    modelAgent = bllAgent.GetModel(model.relation_person_id);

                    if (modelAgent != null)
                    {
                        DisplayName = modelAgent.DisplayName;

                    }
                    break;
            }

            ViewBag.DisplayName = DisplayName;

            return View();
        }

        /// <summary>
        /// 保存用户（编辑）
        /// </summary>
        /// <param name="jmpuser"></param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult UpdateUser(JMP.MDL.jmp_user jmpuser)
        {
            object obj = new { success = 0, msg = "更新失败！" };
            JMP.BLL.jmp_user userBll = new JMP.BLL.jmp_user();
            JMP.MDL.jmp_user oldUser = userBll.GetModel(jmpuser.u_id);
            var oldUserColne = oldUser.Clone();
            oldUser.u_category = jmpuser.u_category;//类别
            oldUser.u_email = jmpuser.u_email;
            oldUser.u_realname = jmpuser.u_realname;
            oldUser.u_password = DESEncrypt.Encrypt(jmpuser.u_password);
            oldUser.u_phone = jmpuser.u_phone;
            oldUser.u_qq = jmpuser.u_qq;
            oldUser.u_address = jmpuser.u_address;
            oldUser.u_account = jmpuser.u_account;
            oldUser.u_name = jmpuser.u_name;
            oldUser.u_bankname = jmpuser.u_bankname;
            oldUser.u_idnumber = jmpuser.u_idnumber;
            oldUser.FrozenMoney = jmpuser.FrozenMoney;
            oldUser.BusinessEntity = jmpuser.BusinessEntity;
            oldUser.RegisteredAddress = jmpuser.RegisteredAddress;
            oldUser.relation_type = jmpuser.relation_type;
            oldUser.relation_person_id = jmpuser.relation_person_id;
            oldUser.u_photof = string.IsNullOrEmpty(jmpuser.u_photof) ? "" : jmpuser.u_photof;
            oldUser.u_licence = string.IsNullOrEmpty(jmpuser.u_licence) ? "" : jmpuser.u_licence;
            try
            {
                if (oldUser.u_category == 0)
                {
                    oldUser.u_photo = string.IsNullOrEmpty(jmpuser.u_photo) ? "" : jmpuser.u_photo;

                }
                else
                {
                    oldUser.u_blicense = string.IsNullOrEmpty(jmpuser.u_blicense) ? "" : jmpuser.u_blicense;
                    oldUser.u_photo = string.IsNullOrEmpty(jmpuser.u_photo) ? " " : jmpuser.u_photo;
                    oldUser.u_blicensenumber = jmpuser.u_blicensenumber;
                }
                if (string.IsNullOrEmpty(oldUser.u_qq))
                {
                    oldUser.u_qq = " ";
                }
                if (string.IsNullOrEmpty(oldUser.u_address))
                {
                    oldUser.u_address = " ";
                }
                bool flag = userBll.Update(oldUser);
                obj = new { success = flag ? 1 : 0, msg = flag ? "更新成功！" : "更新失败！" };
                //写日志
                if (flag)
                {
                    Logger.ModifyLog("修改开发者", oldUserColne, jmpuser);
                    //审核状态改变是发送私信和邮件给用户
                    if (jmpuser.u_auditstate != oldUserColne.u_auditstate)
                    {
                        JMP.BLL.jmp_message messbll = new JMP.BLL.jmp_message();
                        JMP.MDL.jmp_message j_mess = new JMP.MDL.jmp_message();
                        string tipmsg = string.Empty;
                        #region 组装私信实体
                        j_mess.m_sender = UserInfo.UserId;
                        j_mess.m_receiver = oldUser.u_id.ToString();
                        j_mess.m_type = 1;
                        j_mess.m_time = DateTime.Now;
                        j_mess.m_state = 0;
                        switch (jmpuser.u_auditstate)
                        {
                            case -1:
                                tipmsg = "你的账号审核未通过！";
                                break;
                            case 0:
                                tipmsg = "你的账号正在审核中，如有疑问请联系我们！";
                                break;
                            case 1:
                                tipmsg = "你的账号审核通过！";
                                break;
                        }
                        j_mess.m_content = tipmsg;
                        j_mess.m_topid = oldUser.u_topid;
                        #endregion
                        //发送私信
                        if (jmpuser.u_auditstate == 1)
                        {
                            //更改审核状态为通过时，才发送私信
                            int record = messbll.Add(j_mess);
                            if (record > 0)
                            {

                                Logger.CreateLog("发送私信给用户", j_mess);
                            }
                        }
                        #region 发送邮件
                        //StringBuilder MailContent = new StringBuilder();
                        //MailContent.Append("亲爱的开发者：<br/>");
                        //MailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;您好！" + tipmsg);
                        //MailContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;如果你没有用该邮件账号注册过聚米支付平台的账号，请忽视本邮件！");
                        //MailContent.Append("亲爱的开发者：<br/>");
                        //bool isSend = MailHelper.SendText("jm@adjumi.com", "聚米网络科技", jmpuser.u_email, "重置密码", MailContent.ToString(), "smtp.adjumi.com", "jm@adjumi.com", "");
                        //if (isSend)
                        //{
                        //    string tmsg = string.Format("用户{0}({1})发送邮件至用户邮箱{2}，邮件内容为：{3}", UserInfo.UserName, UserInfo.UserId, jmpuser.u_email, tipmsg);
                        //    AddLocLog.AddLog(int.Parse(UserInfo.UserId), 3, RequestHelper.GetClientIp(), "发送邮件给用户" + jmpuser.u_id, tmsg);
                        //}
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.OperateLog("修改开发者报错" + jmpuser.u_id, ex.ToString());
                obj = new { success = 0, msg = "更新出错了！" };
            }
            return Json(obj);
        }
        #endregion

        #region 用户手续费设置
        /// <summary>
        /// 用户手续费设置以及扣量设置
        /// </summary>
        /// <returns></returns>
        public ActionResult UserSxF()
        {
            int userid = string.IsNullOrEmpty(Request["userid"]) ? 0 : Int32.Parse(Request["userid"]);

            //根据ID查询用户信息
            model_user = bll_user.GetModel(userid);

            JMP.BLL.CoServiceFeeRatioGrade bll = new JMP.BLL.CoServiceFeeRatioGrade();
            DataTable dt = bll.GetList(" ").Tables[0];//获取应用平台在用信息 
            List<JMP.MDL.CoServiceFeeRatioGrade> yypt = JMP.TOOL.MdlList.ToList<JMP.MDL.CoServiceFeeRatioGrade>(dt);
            ViewBag.glptdt = yypt;
            ViewBag.user = model_user;
            ViewBag.id = userid;

            return View();
        }
        /// <summary>
        /// 用户扣量设置
        /// </summary>
        /// <returns></returns>
        public ActionResult UserKl()
        {
            int userid = string.IsNullOrEmpty(Request["userid"]) ? 0 : Int32.Parse(Request["userid"]);
            List<JMP.MDL.jmp_rate> list = new List<JMP.MDL.jmp_rate>();
            JMP.BLL.jmp_rate bll = new jmp_rate();
            if (userid > 0)
            {
                list = bll.SelectListUserid(userid);
            }
            ViewBag.list = list;
            ViewBag.userid = userid;
            return View();
        }
        /// <summary>
        /// 手续费设置
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult InserSxf()
        {
            object retJson = new { success = 0, msg = "操作失败！" };
            int str = string.IsNullOrEmpty(Request["s_type"]) ? 0 : int.Parse(Request["s_type"]);
            int userid = string.IsNullOrEmpty(Request["userid"]) ? 0 : Int32.Parse(Request["userid"]);
            JMP.BLL.jmp_rate bll = new jmp_rate();
            if (userid > 0 && str > 0)
            {

                if (bll_user.UpdateServiceFeeRatioGradeId(userid, str))
                {
                    retJson = new { success = 1, msg = "设置成功！" };

                    Logger.OperateLog("设置手续费比例", "设置开发者(" + userid + ")手续费为:" + str);
                }
                else
                {
                    retJson = new { success = 0, msg = "设置失败！" };
                }

            }
            return Json(retJson);
        }



        /// <summary>
        /// 扣量设置
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult Inserkl()
        {
            object retJson = new { success = 0, msg = "操作失败！" };
            string str = string.IsNullOrEmpty(Request["str"]) ? "" : Request["str"];
            int userid = string.IsNullOrEmpty(Request["userid"]) ? 0 : Int32.Parse(Request["userid"]);
            JMP.BLL.jmp_rate bll = new jmp_rate();
            if (userid > 0 && !string.IsNullOrEmpty(str))
            {
                string sql = " delete from jmp_rate where  r_userid=" + userid;
                string[] sxf = str.Split('|');
                string[] lis = new string[sxf.Length + 1];
                lis[0] = sql;
                int a = 1;
                for (int i = 0; i < sxf.Length; i++)
                {
                    string[] bl = sxf[i].Split(',');
                    lis[a] = " insert into jmp_rate(r_userid,r_paymodeid,r_proportion,r_klproportion,r_state,r_time,r_name) values(" + userid + "," + bl[0] + "," + bl[1] + "," + bl[2] + ",0,GETDATE(),'" + UserInfo.UserName + "') ";
                    sql += " insert into jmp_rate(r_userid,r_paymodeid,r_proportion,r_klproportion,r_state,r_time,r_name) values(" + userid + "," + bl[0] + "," + bl[1] + "," + bl[2] + ",0,GETDATE(),'" + UserInfo.UserName + "'); ";
                    a = a + 1;
                }
                int cg = bll.InserSxF(lis);
                if (cg > 0)
                {
                    retJson = new { success = 1, msg = "设置成功！" };

                    Logger.OperateLog("设置扣量比例", "操作数据：" + sql);
                }
                else
                {
                    retJson = new { success = 0, msg = "设置失败！" };
                }

            }
            return Json(retJson);
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 验证邮箱地址是否存在
        /// </summary>
        /// <param name="cval">邮箱地址</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult CheckEmail(string cval, string uid)
        {
            jmp_user userBll = new jmp_user();
            bool flag = userBll.ExistsEmail(cval, uid);
            return Json(new { success = flag, mess = flag ? "已存在该邮件地址！" : "不存在邮件地址！" });
        }

        /// <summary>
        /// 验证身份证号是否存在
        /// </summary>
        /// <param name="cval">身份证号</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult CheckIdno(string cval, string uid)
        {
            jmp_user userBll = new jmp_user();
            bool flag = userBll.ExistsIdno(cval, uid);
            return Json(new { success = flag, mess = flag ? "已存在该身份证号！" : "不存在身份证号！" });
        }

        /// <summary>
        /// 验证营业执照是否存在
        /// </summary>
        /// <param name="cval">营业执照</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult CheckYyzz(string cval, string uid)
        {
            jmp_user userBll = new jmp_user();
            bool flag = userBll.ExistsYyzz(cval, uid);
            return Json(new { success = flag, mess = flag ? "已存在该营业执照！" : "不存在营业执照！" });
        }

        /// <summary>
        /// 验证开户账号是否存在
        /// </summary>
        /// <param name="cval">开户账号</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult CheckBankNo(string cval, string uid)
        {
            jmp_user userBll = new jmp_user();
            bool flag = userBll.ExistsBankNo(cval, uid);
            return Json(new { success = flag, mess = flag ? "已存在该开户账号！" : "不存在开户账号！" });
        }

        /// <summary>
        /// 上传图片(身份证正面)
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public JsonResult UploadImg()
        {
            //回传图片路径
            string returnurl = string.Empty;
            //用于回传显示地址
            string returnurl2 = string.Empty;
            string msg = string.Empty;
            string tag = "0";
            NameValueCollection nvc = System.Web.HttpContext.Current.Request.Form;
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            string[] result = { null, null };
            if (hfc.Count == 1)
            {
                if (hfc[0].ContentLength > (1024 * 1024 * 2))
                {
                    return Json(new { mess = "图片大小不能超过2M", success = "0" }, "text/html", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string purl = nvc["purl"];
                    try
                    {
                        if (!string.IsNullOrEmpty(purl))
                        {
                            PubImageUp.DeleteImage(purl);
                        }
                    }
                    catch (Exception e)
                    {
                    }
                    //获取配置文件里的上传路径
                    string uploadurl = ConfigurationManager.AppSettings["uploadurlkfz"] + "/kfz_img/";
                    //图片域名地址
                    string imgurlroot = ConfigurationManager.AppSettings["imgurl"] + "/kfz_img/";
                    //上传图片
                    result = PubImageUp.UpImages("certificatefile", uploadurl);
                    //图片返回展示地址
                    returnurl2 = imgurlroot + result[0];
                    if (result.Length == 2)
                    {
                        if (result[0] != "99")
                            returnurl = "/kfz_img/" + result[0];

                        else
                            msg = "/kfz_img/" + result[0];

                        tag = "1";
                    }
                }
            }

            return Json(
                new
                {
                    Id = nvc["tid"],
                    name = nvc["tname"],
                    imgurl = returnurl,
                    imgurlroot = returnurl2,
                    mess = msg,
                    success = tag
                }, "text/html", JsonRequestBehavior.AllowGet);
        }
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public JsonResult UploadImgF()
        {
            //回传图片路径
            string returnurl = string.Empty;
            //用于回传显示地址
            string returnurl2 = string.Empty;
            string msg = string.Empty;
            string tag = "0";
            NameValueCollection nvc = System.Web.HttpContext.Current.Request.Form;
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            string[] result = { null, null };
            if (hfc.Count == 1)
            {
                if (hfc[0].ContentLength > (1024 * 1024 * 2))
                {
                    return Json(new { mess = "图片大小不能超过2M", success = "0" }, "text/html", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string purl = nvc["purl"];
                    try
                    {
                        if (!string.IsNullOrEmpty(purl))
                        {
                            PubImageUp.DeleteImage(purl);
                        }
                    }
                    catch (Exception e)
                    {
                    }
                    //获取配置文件里的上传路径
                    string uploadurl = ConfigurationManager.AppSettings["uploadurlkfz"] + "/kfz_img/";
                    //图片域名地址
                    string imgurlroot = ConfigurationManager.AppSettings["imgurl"] + "/kfz_img/";
                    //上传图片
                    result = PubImageUp.UpImages("certificatefilef", uploadurl);
                    //图片返回展示地址
                    returnurl2 = imgurlroot + result[0];
                    if (result.Length == 2)
                    {
                        if (result[0] != "99")
                            returnurl = "/kfz_img/" + result[0];

                        else
                            msg = "/kfz_img/" + result[0];

                        tag = "1";
                    }
                }
            }

            return Json(
                new
                {
                    Id = nvc["tid"],
                    name = nvc["tname"],
                    imgurl = returnurl,
                    imgurlroot = returnurl2,
                    mess = msg,
                    success = tag
                }, "text/html", JsonRequestBehavior.AllowGet);
        }
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public JsonResult UploadImgxkz()
        {
            //回传图片路径
            string returnurl = string.Empty;
            //用于回传显示地址
            string returnurl2 = string.Empty;
            string msg = string.Empty;
            string tag = "0";
            NameValueCollection nvc = System.Web.HttpContext.Current.Request.Form;
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            string[] result = { null, null };
            if (hfc.Count == 1)
            {
                if (hfc[0].ContentLength > (1024 * 1024 * 2))
                {
                    return Json(new { mess = "图片大小不能超过2M", success = "0" }, "text/html", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string purl = nvc["purl"];
                    try
                    {
                        if (!string.IsNullOrEmpty(purl))
                        {
                            PubImageUp.DeleteImage(purl);
                        }
                    }
                    catch (Exception e)
                    {
                    }
                    //获取配置文件里的上传路径
                    string uploadurl = ConfigurationManager.AppSettings["uploadurlkfz"] + "/kfz_img/";
                    //图片域名地址
                    string imgurlroot = ConfigurationManager.AppSettings["imgurl"] + "/kfz_img/";
                    //上传图片
                    result = PubImageUp.UpImages("licencefilef", uploadurl);
                    //图片返回展示地址
                    returnurl2 = imgurlroot + result[0];
                    if (result.Length == 2)
                    {
                        if (result[0] != "99")
                            returnurl = "/kfz_img/" + result[0];

                        else
                            msg = "/kfz_img/" + result[0];

                        tag = "1";
                    }
                }
            }

            return Json(
                new
                {
                    Id = nvc["tid"],
                    name = nvc["tname"],
                    imgurl = returnurl,
                    imgurlroot = returnurl2,
                    mess = msg,
                    success = tag
                }, "text/html", JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 上传图片（营业执照照片）
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public JsonResult UploadImgsfz()
        {
            //回传图片路径
            string returnurl = string.Empty;
            //用于回传显示地址
            string returnurl2 = string.Empty;
            string msg = string.Empty;
            string tag = "0";
            NameValueCollection nvc = System.Web.HttpContext.Current.Request.Form;
            HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            string[] result = { null, null };
            if (hfc.Count == 1)
            {
                if (hfc[0].ContentLength > (1024 * 1024 * 2))
                {
                    return Json(new { mess = "图片大小不能超过2M", success = "0" }, "text/html", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string purl = nvc["purl"];
                    try
                    {
                        // purl = purl.StartsWith("B") ? purl.TrimStart('B') : purl.StartsWith("A") ? purl.TrimStart('A') : purl;
                        if (!string.IsNullOrEmpty(purl))
                        {
                            PubImageUp.DeleteImage(purl);
                        }
                    }
                    catch (Exception e)
                    {
                    }
                    //获取配置文件里的上传路径
                    string uploadurl = ConfigurationManager.AppSettings["uploadurlkfz"] + "/kfz_img/";
                    //图片域名地址
                    string imgurlroot = ConfigurationManager.AppSettings["imgurl"] + "/kfz_img/";
                    //上传图片
                    result = PubImageUp.UpImages("sfzcertificatefile", uploadurl);
                    //图片返回展示地址
                    returnurl2 = imgurlroot + result[0];
                    if (result.Length == 2)
                    {
                        if (result[0] != "99")
                            returnurl = "/kfz_img/" + result[0];

                        else
                            msg = "/kfz_img/" + result[0];

                        tag = "1";
                    }
                }
            }

            return Json(
                new
                {
                    Id = nvc["tid"],
                    name = nvc["tname"],
                    imgurl = returnurl,
                    imgurlroot = returnurl2,
                    mess = msg,
                    success = tag
                }, "text/html", JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 用户日志
        [VisitRecord(IsRecord = true)]
        public ActionResult AppUserLog()
        {
            #region 初始化
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量
            string stype = !string.IsNullOrEmpty(Request["types"]) ? Request["types"] : "";
            string sname = !string.IsNullOrEmpty(Request["searchKey"]) ? Request["searchKey"] : "";
            string ltype = !string.IsNullOrEmpty(Request["logtype"]) ? Request["logtype"] : "";
            int sort = !string.IsNullOrEmpty(Request["sort"]) ? int.Parse(Request["sort"]) : 1;
            JMP.BLL.jmp_userlog bll = new JMP.BLL.jmp_userlog();
            string where = " where 1=1";
            if (!string.IsNullOrEmpty(stype))
            {
                if (!string.IsNullOrEmpty(sname))
                {
                    if (stype == "1")
                        where += string.Format(" and a.l_ip like '%{0}%'", sname);
                    else if (stype == "2")
                        where += string.Format(" and b.u_realname like '%{0}%'", sname);
                    else if (stype == "3")
                        where += string.Format(" and a.l_sms like '%{0}%'", sname);
                    else if (stype == "4")
                        where += string.Format(" and a.l_info like '%{0}%'", sname);
                }
            }
            if (!string.IsNullOrEmpty(ltype))
                where += string.Format(" and a.l_logtype_id={0}", ltype);

            string orderby = "order by l_time " + (sort == 1 ? "desc" : "asc");
            string query = "select a.*,b.u_realname from jmp_userlog a left join jmp_user b on a.l_user_id=b.u_id";
            DataTable dt = bll.GetLists(query + where, orderby, pageIndexs, PageSize, out pageCount);
            ViewBag.stype = stype;
            ViewBag.sname = sname;
            ViewBag.ltype = ltype;
            ViewBag.sort = sort;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            #endregion
            return View(dt);
        }
        #endregion

        #region 用户日志通知类型
        [VisitRecord(IsRecord = true)]
        public ActionResult AppUserLogNotice()
        {
            #region 初始化
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量
            string stype = !string.IsNullOrEmpty(Request["types"]) ? Request["types"] : "";
            string sname = !string.IsNullOrEmpty(Request["searchKey"]) ? Request["searchKey"] : "";
            //string ltype = !string.IsNullOrEmpty(Request["logtype"]) ? Request["logtype"] : "";
            int sort = !string.IsNullOrEmpty(Request["sort"]) ? int.Parse(Request["sort"]) : 1;
            //string RoleID = System.Configuration.ConfigurationManager.AppSettings["RoleID"];
            JMP.BLL.jmp_userlog bll = new JMP.BLL.jmp_userlog();
            string where = " where 1=1";
            if (!string.IsNullOrEmpty(stype))
            {
                if (!string.IsNullOrEmpty(sname))
                {
                    if (stype == "1")
                        where += string.Format(" and a.l_ip like '%{0}%'", sname);
                    else if (stype == "2")
                        where += string.Format(" and b.u_realname like '%{0}%'", sname);
                    else if (stype == "3")
                        where += string.Format(" and a.l_sms like '%{0}%'", sname);
                    else if (stype == "4")
                        where += string.Format(" and a.l_info like '%{0}%'", sname);
                }
            }
            // if (!string.IsNullOrEmpty(ltype))
            where += string.Format(" and a.l_logtype_id={0}", 5);
            //if (UserInfo.UserRoleId == RoleID)
            //{
            //    where += string.Format(" and b.u_merchant_id={0}", UserInfo.UserId);
            //}
            string orderby = "order by l_time " + (sort == 1 ? "desc" : "asc");
            string query = "select a.*,b.u_realname from jmp_userlog a left join jmp_user b on a.l_user_id=b.u_id";
            DataTable dt = bll.GetLists(query + where, orderby, pageIndexs, PageSize, out pageCount);
            ViewBag.stype = stype;
            ViewBag.sname = sname;
            ViewBag.ltype = "5";
            ViewBag.sort = sort;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            #endregion
            return View(dt);
        }
        #endregion

        #region 特批

        /// <summary>
        /// 特批页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AppUserTePi()
        {
            var userid = Request.HttpGetParaByName("userid", 0);

            var bll = new jmp_user();
            var model = bll.GetModel(userid);
            var m = new SpecialApprovalViewModel
            {
                UserId = userid,
                IsSpecialApproval = model.IsSpecialApproval ? 1 : 0,
                SpecialApproval = model.SpecialApproval.ToString(CultureInfo.InvariantCulture),
                SpecialApprovalDataSource = SpecialApprovalDataSource.DataSource
            };
            return View(m);

        }

        /// <summary>
        /// 特批方法
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = true, IsRole = false)]
        public JsonResult AppUserUpdateTePi(SpecialApprovalViewModel model)
        {
            var bll = new jmp_user();

            var flag = bll.UpdateIsSpecialApproval(model.UserId, model.IsSpecialApproval, model.IsSpecialApproval == 1 ? model.SpecialApproval : "0");

            if (!flag) return Json(new { success = 0, msg = "特批失败！" });
            var info = "修改开发者特批状态（" + model.UserId + "）的状态为" + model.IsSpecialApproval + "";
            Logger.OperateLog("修改开发者特批状态", info);

            return Json(new { success = flag ? 1 : 0, msg = flag ? "特批成功！" : "特批失败！" });
        }


        #endregion

        /// <summary>
        /// 结冻金额
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateFrozenMoney()
        {
            int userId = !string.IsNullOrEmpty(Request["uid"]) ? int.Parse(Request["uid"]) : 0;
            var bll = new jmp_user();
            var model = bll.GetModel(userId);
            ViewBag.model = model;
            return View();
        }
        /// <summary>
        /// 特批方法
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = true, IsRole = false)]
        public JsonResult AppUserUpdateFrozenMoney()
        {
            decimal FrozenMoney = string.IsNullOrEmpty(Request["FrozenMoney"]) ? 0 : decimal.Parse(Request["FrozenMoney"]);
            int userid = string.IsNullOrEmpty(Request["userid"]) ? 0 : Int32.Parse(Request["userid"]);
            var bll = new jmp_user();
            var flag = bll.UpdateFrozenMoney(userid, FrozenMoney);

            if (!flag) return Json(new { success = 0, msg = "特批失败！" });
            var info = "修改开发者特批状态（" + userid + "）的冻结金额为" + FrozenMoney + "";
            Logger.OperateLog("修改开发者特批状态", info);
            return Json(new { success = flag ? 1 : 0, msg = flag ? "设置结冻金额成功！" : "设置结冻金额失败！" });
        }


        #region 重置开发者提现密码

        /// <summary>
        /// 重置开发者提现密码
        /// </summary>
        /// <returns></returns>
        public JsonResult AppRePassword()
        {
            object result = new { success = 0, msg = "重置失败！" };

            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);
            //默认密码
            string RePwd = ConfigurationManager.AppSettings["RePwd"].ToString();
            //得到一个实体对象
            model_user = bll_user.GetModel(id);
            //加密
            string u_paypwd = DESEncrypt.Encrypt(RePwd);

            if (bll_user.UpdateUserPayPwd(id, u_paypwd))
            {
                string log = UserInfo.UserName + "重置了开发者ID为:{" + id + "}的提现密码!";
                Logger.OperateLog("重置开发者提现密码", log);
                result = new { success = 1, msg = "重置成功！" };
            }
            else
            {
                result = new { success = 0, msg = "重置失败！" };
            }

            return Json(result);
        }

        #endregion
    }
}
