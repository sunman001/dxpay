using JMP.TOOL;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using WEB.Util.Logger;
using System.Configuration;
using System.Collections;
using WEB.Util.RateLogger;
using System.Collections.Specialized;
using TOOL.Extensions;

namespace WEB.Controllers
{
    public class AgentController : Controller
    {
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;
        private static readonly IRateLogWriter RateLogger = RateLogWriterManager.GetOperateLogger;

        JMP.BLL.CoCooperationApplication bll = new JMP.BLL.CoCooperationApplication();

        JMP.BLL.jmp_limit bll_limit = new JMP.BLL.jmp_limit();
        JMP.MDL.jmp_workorder model = new JMP.MDL.jmp_workorder();

        #region 代理商信息

        JMP.BLL.CoAgent bllAgent = new JMP.BLL.CoAgent();
        JMP.MDL.CoAgent modelAgent = new JMP.MDL.CoAgent();
        List<JMP.MDL.CoAgent> listAgent = new List<JMP.MDL.CoAgent>();

        //商务
        JMP.BLL.CoBusinessPersonnel bll_co = new JMP.BLL.CoBusinessPersonnel();
        List<JMP.MDL.CoBusinessPersonnel> bll_list = new List<JMP.MDL.CoBusinessPersonnel>();
        JMP.MDL.CoBusinessPersonnel co_model = new JMP.MDL.CoBusinessPersonnel();

        /// <summary>
        ///代理商列表
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult AgentList()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            //查询类型
            int s_type = string.IsNullOrEmpty(Request["stype"]) ? 0 : int.Parse(Request["stype"]);

            //关键字
            string s_keys = string.IsNullOrEmpty(Request["skeys"]) ? "" : Request["skeys"];
            //账号状态
            string status = string.IsNullOrEmpty(Request["status"]) ? "" : Request["status"];
            //审核状态
            string AuditState = string.IsNullOrEmpty(Request["AuditState"]) ? "" : Request["AuditState"];
            //排序
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? -1 : int.Parse(Request["searchDesc"]);
            //查询
            listAgent = bllAgent.SelectList(s_type, s_keys, status, AuditState, searchDesc, pageIndexs, PageSize, out pageCount);
            ViewBag.Agenturl = System.Configuration.ConfigurationManager.AppSettings["Agenturl"];
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = listAgent;
            ViewBag.stype = s_type;
            ViewBag.skeys = s_keys;
            ViewBag.status = status;
            ViewBag.AuditState = AuditState;
            ViewBag.searchDesc = searchDesc;
            //
            ViewBag.locUrl = GetVoidHtml();

            return View();
        }

        public string GetVoidHtml()
        {
            string locUrl = "";

            bool getUidT = bll_limit.GetLocUserLimitVoids("/Agent/AgentList", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//本地管理员一键启用
            if (getUidT)
            {
                locUrl += "<li onclick=\"javascript:Updatestate(0)\"><i class='fa fa-check-square-o'></i>一键启用</li>";
            }
            bool getUidF = bll_limit.GetLocUserLimitVoids("/Agent/AgentList", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//本地管理员一键禁用
            if (getUidF)
            {
                locUrl += "<li onclick=\"javascript:Updatestate(1);\"><i class='fa fa-check-square-o'></i>一键禁用</li>";
            }
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/Agent/InsertAgent", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加管理员
            if (getlocuserAdd)
            {
                locUrl += "<li onclick=\"AddAgents()\"><i class='fa fa-plus'></i>添加代理商</li>";
            }
            return locUrl;
        }

        /// <summary>
        /// 批量更新代理商状态
        /// </summary>
        /// <param name="coid">商务id列表</param>
        /// <param name="tag">状态</param>
        /// <returns></returns>
        
        public JsonResult CoAgentUpdate(string coid, int tag)
        {

            bool flag = bllAgent.UpdateAgentState(coid, tag);

            //写日志
            if (flag)
            {
                string info = "批量更新商务（" + coid + "）的状态为" + (tag == 0 ? "正常。" : "冻结。");
                Logger.OperateLog("批量更新商务状态", info);
            }
            return Json(new { success = flag ? 1 : 0, msg = flag ? "更新成功！" : "更新失败！" });
        }

        /// <summary>
        /// 添加代理商
        /// </summary>
        /// <returns></returns>
        public ActionResult AgentAdd()
        {
            return View();
        }

        /// <summary>
        /// 添加代理商方法
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult InsertAgent(JMP.MDL.CoAgent jmpagent)
        {
            object obj = new { success = 0, msg = "添加失败！" };
            JMP.BLL.CoAgent userBll = new JMP.BLL.CoAgent();
            jmpagent.Password = DESEncrypt.Encrypt(jmpagent.Password);
            jmpagent.RoleId = int.Parse(ConfigurationManager.AppSettings["AgentRoleID"]);
            try
            {
                if (jmpagent.Classify == 0)
                {
                    jmpagent.PersonalPhotoPath = string.IsNullOrEmpty(jmpagent.PersonalPhotoPath) ? "" :  jmpagent.PersonalPhotoPath;
                }
                else
                {
                    jmpagent.BusinessLicensePhotoPath = string.IsNullOrEmpty(jmpagent.BusinessLicensePhotoPath) ? "" :  jmpagent.BusinessLicensePhotoPath;
                    jmpagent.PersonalPhotoPath = string.IsNullOrEmpty(jmpagent.PersonalPhotoPath) ? " " :  jmpagent.PersonalPhotoPath;
                }

                jmpagent.QQ = string.IsNullOrEmpty(jmpagent.QQ) ? "" : jmpagent.QQ;
                jmpagent.ContactAddress = string.IsNullOrEmpty(jmpagent.ContactAddress) ? " " : jmpagent.ContactAddress;
                jmpagent.CreatedOn = DateTime.Now;
                jmpagent.CreatedById = UserInfo.UserId;
                jmpagent.CreatedByName = UserInfo.UserName;
                
                bool flag = false;
                flag = userBll.Add(jmpagent) > 0;
                obj = new { success = flag ? 1 : 0, msg = flag ? "添加成功！" : "添加失败！" };
                //写日志
                if (flag)
                {

                    Logger.CreateLog("添加代理商", jmpagent);

                }
            }
            catch (Exception ex)
            {
                obj = new { success = 0, msg = "添加异常！" };

                Logger.OperateLog("添加代理商报错", ex.ToString());
            }
            return Json(obj);
        }

        /// <summary>
        /// 修改代理商
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult AgentUpdate()
        {
            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);
            //查询一条数据
            modelAgent = bllAgent.GetModel(id);

            ViewBag.UploadUrl = ConfigurationManager.AppSettings["imgurl"];

            ViewBag.modelAgent = modelAgent;

            return View();
        }

        /// <summary>
        /// 修改代理商
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdateAgents(JMP.MDL.CoAgent jmpagent)
        {
            object obj = new { success = 0, msg = "更新失败！" };
            JMP.BLL.CoAgent userBll = new JMP.BLL.CoAgent();
            JMP.MDL.CoAgent oldUser = userBll.GetModel(jmpagent.Id);
            var oldUserClone=oldUser.Clone();
            oldUser.Classify = jmpagent.Classify;
            oldUser.OwnerId = jmpagent.OwnerId;
            oldUser.OwnerName = jmpagent.OwnerName;
            oldUser.LoginName = jmpagent.LoginName;
            oldUser.Password= DESEncrypt.Encrypt(jmpagent.Password);
            oldUser.DisplayName = jmpagent.DisplayName;
            oldUser.EmailAddress = jmpagent.EmailAddress;
            oldUser.MobilePhone = jmpagent.MobilePhone;
            oldUser.QQ = jmpagent.QQ;
            oldUser.Website = jmpagent.Website;
            oldUser.ContactAddress = jmpagent.ContactAddress;
            oldUser.IDCardNumber = jmpagent.IDCardNumber;
            oldUser.BusinessLicenseNumber = jmpagent.BusinessLicenseNumber;
            oldUser.BankAccount = jmpagent.BankAccount;
            oldUser.BankAccountName = jmpagent.BankAccountName;
            oldUser.BankFullName = jmpagent.BankFullName;

            try
            {
                if (oldUser.Classify == 0)
                {
                    oldUser.PersonalPhotoPath = string.IsNullOrEmpty(jmpagent.PersonalPhotoPath) ? "" : jmpagent.PersonalPhotoPath;
                }
                else
                {
                    oldUser.BusinessLicensePhotoPath = string.IsNullOrEmpty(jmpagent.BusinessLicensePhotoPath) ? "" : jmpagent.BusinessLicensePhotoPath;
                    oldUser.PersonalPhotoPath = string.IsNullOrEmpty(jmpagent.PersonalPhotoPath) ? " " : jmpagent.PersonalPhotoPath;
                }


                if (string.IsNullOrEmpty(jmpagent.QQ))
                {
                    oldUser.QQ = " ";
                }
                if (string.IsNullOrEmpty(jmpagent.ContactAddress))
                {
                    oldUser.ContactAddress = " ";
                }
                bool flag = userBll.Update(oldUser);
                obj = new { success = flag ? 1 : 0, msg = flag ? "更新成功！" : "更新失败！" };
                //写日志
                if (flag)
                {

                    Logger.ModifyLog("修改代理商", oldUserClone, jmpagent);

                    //审核状态改变是发送私信和邮件给用户
                    if (jmpagent.AuditState != oldUserClone.AuditState)
                    {
                        JMP.BLL.jmp_message messbll = new JMP.BLL.jmp_message();
                        JMP.MDL.jmp_message j_mess = new JMP.MDL.jmp_message();
                        string tipmsg = string.Empty;
                        #region 组装私信实体
                        j_mess.m_sender = UserInfo.UserId;
                        j_mess.m_receiver = oldUser.Id.ToString();
                        j_mess.m_type = 1;
                        j_mess.m_time = DateTime.Now;
                        j_mess.m_state = 0;
                        switch (jmpagent.AuditState)
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
                        #endregion
                        //发送私信
                        if (jmpagent.AuditState == 1)
                        {
                            //更改审核状态为通过时，才发送私信
                            int record = messbll.Add(j_mess);
                            if (record > 0)
                            {

                                Logger.CreateLog("发送私信给用户", j_mess);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {

                Logger.OperateLog("修改代理商报错" + jmpagent.Id, ex.ToString());
                obj = new { success = 0, msg = "更新出错了！" };
            }
            return Json(obj);
        }

        /// <summary>
        /// 商务信息弹窗页面
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult AgentAddBpTc()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量

            string s_type = string.IsNullOrEmpty(Request["s_type"]) ? "1" : Request["s_type"];
            string s_keys = string.IsNullOrEmpty(Request["s_keys"]) ? "" : Request["s_keys"];
            string s_state = string.IsNullOrEmpty(Request["s_state"]) ? "0" : Request["s_state"];

            //查询商务信息
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
            JMP.BLL.CoAgent userBll = new JMP.BLL.CoAgent();
            bool flag = userBll.ExistsLogName(lname, uid);
            return Json(new { success = flag, mess = flag ? "已存在该登录名称！" : "不存在登录名称！" });
        }


        /// <summary>
        /// 验证邮箱地址是否存在
        /// </summary>
        /// <param name="cval">邮箱地址</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult CheckEmail(string cval, string uid)
        {
            JMP.BLL.CoAgent userBll = new JMP.BLL.CoAgent();
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
            JMP.BLL.CoAgent userBll = new JMP.BLL.CoAgent();
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
            JMP.BLL.CoAgent userBll = new JMP.BLL.CoAgent();
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
            JMP.BLL.CoAgent userBll = new JMP.BLL.CoAgent();
            bool flag = userBll.ExistsBankNo(cval, uid);
            return Json(new { success = flag, mess = flag ? "已存在该开户账号！" : "不存在开户账号！" });
        }

        /// <summary>
        /// 上传图片(身份证)
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
                    string uploadurl = ConfigurationManager.AppSettings["uploadurldls"] + "/dls_img/";
                    //图片域名地址
                    string imgurlroot = ConfigurationManager.AppSettings["imgurl"] + "/dls_img/";
                    //上传图片
                    result = PubImageUp.UpImages("certificatefile", uploadurl);
                    //图片返回展示地址
                    returnurl2 = imgurlroot + result[0];
                    if (result.Length == 2)
                    {
                        if (result[0] != "99")
                            returnurl = "/dls_img/" + result[0];

                        else
                            msg = "/dls_img/" + result[0];

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
                    string uploadurl = ConfigurationManager.AppSettings["uploadurldls"] + "/dls_img/";
                    //图片域名地址
                    string imgurlroot = ConfigurationManager.AppSettings["imgurl"] + "/dls_img/";
                    //上传图片
                    result = PubImageUp.UpImages("sfzcertificatefile", uploadurl);
                    //图片返回展示地址
                    returnurl2 = imgurlroot + result[0];
                    if (result.Length == 2)
                    {
                        if (result[0] != "99")
                            returnurl = "/dls_img/" + result[0];

                        else
                            msg = "/dls_img/" + result[0];

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

        #endregion

        #region 审核

        /// <summary>
        /// 审核代理商
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult AgentAuditing()
        {
            int userid = string.IsNullOrEmpty(Request["userid"]) ? 0 : int.Parse(Request["userid"]);

            JMP.BLL.CoAgent bll = new JMP.BLL.CoAgent();
            JMP.MDL.CoAgent model = bll.GetModel(userid);
            //审核状态
            ViewBag.start = model.AuditState;
            ViewBag.userid = userid;
            return View();

        }

        /// <summary>
        /// 审核方法
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult UpdateAgentAuditState()
        {
            int id = string.IsNullOrEmpty(Request["userid"]) ? 0 : int.Parse(Request["userid"]);
            int u_auditstate = string.IsNullOrEmpty(Request["u_auditstate"]) ? 0 : int.Parse(Request["u_auditstate"]);

            JMP.BLL.CoAgent bll = new JMP.BLL.CoAgent();
            bool flag = bll.UpdateState(id, u_auditstate);

            if (flag)
            {
                string info = "审核代理商状态（" + id + "）的状态为" + u_auditstate + "";
                Logger.OperateLog("审核代理商状态", info);
            }

            return Json(new { success = flag ? 1 : 0, msg = flag ? "审核成功！" : "审核失败！" });
        }

        #endregion

        #region 开发者直客管理


        JMP.BLL.jmp_user bllUser = new JMP.BLL.jmp_user();
        JMP.MDL.jmp_user modelUser = new JMP.MDL.jmp_user();
        List<JMP.MDL.jmp_user> listUser = new List<JMP.MDL.jmp_user>();

        /// <summary>
        /// 开发者直客管理
        /// </summary>
        /// <returns></returns>
        public ActionResult userList()
        {
            #region 初始化
            //获取请求参数
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量

            string type = string.IsNullOrEmpty(Request["stype"]) ? "5" : Request["stype"];//查询条件类型
            string sea_name = string.IsNullOrEmpty(Request["skeys"]) ? "" : Request["skeys"];//查询条件值
            string category = string.IsNullOrEmpty(Request["scategory"]) ? "" : Request["scategory"];//认证类型
            int px = string.IsNullOrEmpty(Request["s_sort"]) ? 0 : Int32.Parse(Request["s_sort"]);//排序
            //获取用户列表

            //查询已通过审核，账户状态正常，所属关系为商务的直客开发者
            string where = " where 1=1 and u_auditstate='1' and u_state='1' and relation_type='1' ";

            //关联人的ID，比如商务ID，代理商ID
            //where += " and relation_person_id='"+id+"'";

            if (!string.IsNullOrEmpty(type.ToString()))
            {
                if (!string.IsNullOrEmpty(sea_name))
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
                        case "5":
                            where += string.Format(" and u_realname like '%{0}%'", sea_name);
                            break;
                    }
                }
            }
            if (!string.IsNullOrEmpty(category))
            {
                where += string.Format(" and u_category={0}", category);
            }
            string Order = " order by u_id " + (px == 0 ? "" : " desc ") + " ";
            string query = "select a.*,b.DisplayName,c.[Name] from jmp_user a left join CoBusinessPersonnel b on a.relation_person_id = b.Id left join CoServiceFeeRatioGrade c on a.ServiceFeeRatioGradeId = c.Id" + where;
            listUser = bllUser.GetLists(query, Order, pageIndexs, PageSize, out pageCount);
            //返回
            ViewBag.CurrPage = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.PageCount = pageCount;
            ViewBag.stype = type;
            ViewBag.skeys = sea_name;
            ViewBag.scategory = category;
            ViewBag.s_sort = px;
            ViewBag.list = listUser;
            #endregion

            return View();
        }

        /// <summary>
        /// 设置费率
        /// </summary>
        /// <returns></returns>
        public ActionResult ServiceChargeAdd()
        {
            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);

            ViewBag.id = id;

            return View();
        }

        /// <summary>
        /// 设置费率方法
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult ScAdd()
        {
            object retJson = new { success = 0, msg = "操作失败" };

            int ServiceFeeRatioGradeId = string.IsNullOrEmpty(Request["ServiceFeeRatioGradeId"]) ? 0 : int.Parse(Request["ServiceFeeRatioGradeId"]);
            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);
            //修改前的数据
            modelUser = bllUser.GetModel(id);

            if (bllUser.UpdateServiceFeeRatioGradeId(id, ServiceFeeRatioGradeId))
            {
                //记录日志（会定期清理）
                Logger.OperateLog("商务设置直客开发者费率", "商务ID：" + modelUser.relation_person_id + ",操作数据(jmp_user)ID：" + id + ",费率等级由：" + modelUser.ServiceFeeRatioGradeId + ",改为：" + ServiceFeeRatioGradeId + "。");
                //记录日志（不会清理）
                RateLogger.OperateLog("商务设置直客开发者费率", "商务ID：" + modelUser.relation_person_id + ",操作数据(jmp_user)ID：" + id + ",费率等级由：" + modelUser.ServiceFeeRatioGradeId + ",改为：" + ServiceFeeRatioGradeId + "。");

                retJson = new { success = 1, msg = "设置成功！" };

            }
            else
            {
                retJson = new { success = 0, msg = "设置失败！" };
            }

            return Json(retJson);
        }


        /// <summary>
        /// 费率等级弹窗页面
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult CoServiceList()
        {
            JMP.BLL.CoServiceFeeRatioGrade bll = new JMP.BLL.CoServiceFeeRatioGrade();
            JMP.MDL.CoServiceFeeRatioGrade model = new JMP.MDL.CoServiceFeeRatioGrade();
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            int type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);//查询条件选择
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容

            List<JMP.MDL.CoServiceFeeRatioGrade> list = new List<JMP.MDL.CoServiceFeeRatioGrade>();
            list = bll.SelectList(sea_name, type, searchDesc, pageIndexs, PageSize, out pageCount);

            ViewBag.searchDesc = searchDesc;
            ViewBag.type = type;
            ViewBag.sea_name = sea_name;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;

            return View();
        }


        #endregion 
    }
}



