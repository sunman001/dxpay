using DxPay.Bp.Models;
using DxPay.Bp.Util.Logger;
using DxPay.Factory;
using DxPay.Services;
using JMP.MDL;
using JMP.TOOL;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TOOL.Extensions;

namespace DxPay.Bp.Controllers
{
    public class AgentController : Controller
    {
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;
        private readonly ICoCooperationApplicationService _coCooperationApplicationService;
        private readonly ICoAgentService _coAgentService;
        public AgentController()
        {
            _coCooperationApplicationService = ServiceFactory.CoCooperationApplicationService;
            _coAgentService = ServiceFactory.CoAgentService;
        }
        #region 合作信息管理
        /// <summary>
        /// 未枪单代理商信息
        /// </summary>
        /// <returns></returns>
        public ActionResult CooperationApplicationList()
        {
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            string status = "";
            int type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);//查询条件选择
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            var list = _coCooperationApplicationService.FindPagedList("", status, sea_name, type, searchDesc, null, pageIndex: pageIndexs - 1, pageSize: PageSize);
            var gridModel = new DataSource<CoCooperationApplication>(list)
            {
                Data = list.Select(x => x).ToList()
            };
            ViewBag.searchDesc = searchDesc;
            ViewBag.type = type;
            ViewBag.sea_name = sea_name;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = gridModel.Pagination.TotalCount;
            ViewBag.list = gridModel.Data;
            // ViewBag.status = status;
            return View();
        }
        /// <summary>
        /// 修改状态 抢单 或者 关闭
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdateState()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            int id = int.Parse(Request["id"]);
            int state = int.Parse(Request["state"]);
            DateTime GrabbedDate = DateTime.Now;
            int GrabbedById = UserInfo.UserId;
            string GrabbedByName = UserInfo.UserName;
            if (_coCooperationApplicationService.UpdateState(id, state, GrabbedDate, GrabbedByName, GrabbedById))
            {
                if (state == 1)
                {
                    Logger.OperateLog("抢单", "抢单成功");
                    retJson = new { success = 1, msg = "抢单成功" };
                }
                else if (state == -1)
                {
                    Logger.OperateLog("关闭代理商合作信息", "关闭成功");
                    retJson = new { success = 1, msg = "关闭成功" };
                }
                else if (state == 2)
                {
                    Logger.OperateLog("分配代理商合作信息", "分配成功");
                    retJson = new { success = 1, msg = "分配成功" };
                }
                else if (state ==0)
                {
                    Logger.OperateLog("释放", "释放成功");
                    retJson = new { success = 1, msg = "释放成功" };
                }
            }
            else
            {
                retJson = new { success = 0, msg = "操作失败" };

            }
            return Json(retJson);
        }
        /// <summary>
        /// 我的代理商页面
        /// </summary>
        /// <returns></returns>
        public ActionResult MyCooperationApplicationList()
        {

            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            int type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);//查询条件选择
            string status = string.IsNullOrEmpty(Request["status"]) ? "" : Request["status"];//代理商合作信息状态
            // string stime = string.IsNullOrEmpty(Request["r_begin"]) ? DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") : Request["r_begin"];
            //string etime = string.IsNullOrEmpty(Request["r_end"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["r_end"];
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            int userid = UserInfo.UserId;
            var list = _coCooperationApplicationService.FindPagedList("", status, sea_name, type, searchDesc, userid, null, pageIndex: pageIndexs - 1, pageSize: PageSize);
            var gridModel = new DataSource<CoCooperationApplication>(list)
            {
                Data = list.Select(x => x).ToList()
            };
            ViewBag.searchDesc = searchDesc;
            ViewBag.type = type;
            ViewBag.sea_name = sea_name;
            ViewBag.status = status;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = gridModel.Pagination.TotalCount;
            ViewBag.list = gridModel.Data;

            return View();
        }
        #endregion

        /// <summary>
        /// 代理商管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AgentList()
        {

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
            int userid = UserInfo.UserId;
            //查询
            var list = _coAgentService.FindJionList("", s_type, s_keys, status, AuditState, userid, searchDesc, null, pageIndexs - 1, PageSize);
            ViewBag.Agenturl = System.Configuration.ConfigurationManager.AppSettings["Agenturl"];
            var gridModel = new DataSource<CoAgent>(list)
            {
                Data = list.Select(x => x).ToList()
            };
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = gridModel.Pagination.TotalCount;
            ViewBag.list = gridModel.Data;
            ViewBag.stype = s_type;
            ViewBag.skeys = s_keys;
            ViewBag.status = status;
            ViewBag.AuditState = AuditState;
            ViewBag.searchDesc = searchDesc;
            return View();

        }

        /// <summary>
        /// 一键启用或者禁用
        /// </summary>
        /// <param name="coid">商务id列表</param>
        /// <param name="tag">状态</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public JsonResult CoAgentUpdate(string  coid, int tag)
        {

            bool flag = _coAgentService.UpdateState(coid, tag);

            //写日志
            if (flag)
            {
                string info = "批量更新代理商（" + coid + "）的状态为" + (tag == 0 ? "正常" : "冻结");

                Logger.OperateLog("批量更新商务状态", info);
            }
            string message = "";
            if (tag == 0)
            {
                message = "一键启用成功！";
            }
            else
            {
                message = "一键禁用成功！";
            }
            return Json(new { success = flag ? 1 : 0, msg = flag ? message : "操作失败！" });
        }

        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public ActionResult AgentAdd()
        {
            ViewBag.UploadUrl = ConfigurationManager.AppSettings["imgurl"];
            return View();
        }

        /// <summary>
        /// 添加代理商方法
        /// </summary>
        /// <param name="jmpagent"></param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false)]
        public JsonResult InsertAgent(JMP.MDL.CoAgent jmpagent)
        {

            object obj = new { success = 0, msg = "添加失败！" };
            jmpagent.Password = DESEncrypt.Encrypt(jmpagent.Password);
            jmpagent.ServiceFeeRatioGradeId = _coAgentService.FindMax(" select top 1 Id  from   CoServiceFeeRatioGrade where  AgentPushMoneyRatio in(select  max(AgentPushMoneyRatio)   from CoServiceFeeRatioGrade )");
            jmpagent.RoleId = int.Parse(ConfigurationManager.AppSettings["AgentRoleId"]);
            try
            {
                if (jmpagent.Classify == 0)
                {
                    jmpagent.PersonalPhotoPath = string.IsNullOrEmpty(jmpagent.PersonalPhotoPath) ? "" : jmpagent.PersonalPhotoPath;
                }
                else
                {
                    jmpagent.BusinessLicensePhotoPath = string.IsNullOrEmpty(jmpagent.BusinessLicensePhotoPath) ? "" : jmpagent.BusinessLicensePhotoPath;
                    jmpagent.PersonalPhotoPath = string.IsNullOrEmpty(jmpagent.PersonalPhotoPath) ? " " : jmpagent.PersonalPhotoPath;
                }

                jmpagent.QQ = string.IsNullOrEmpty(jmpagent.QQ) ? "" : jmpagent.QQ;
                jmpagent.ContactAddress = string.IsNullOrEmpty(jmpagent.ContactAddress) ? " " : jmpagent.ContactAddress;
                jmpagent.CreatedOn = DateTime.Now;
                jmpagent.CreatedById = UserInfo.UserId;
                jmpagent.CreatedByName = UserInfo.UserName;
                jmpagent.OwnerId = UserInfo.UserId;
                jmpagent.OwnerName = UserInfo.UserName;
                bool flag = false;
                flag = _coAgentService.Insert(jmpagent) > 0;
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
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public ActionResult AgentUpdate()
        {
            ViewBag.UploadUrl = ConfigurationManager.AppSettings["imgurl"];

            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);

            // JMP.BLL.CoAgent bllAgent = new JMP.BLL.CoAgent();
            // JMP.MDL.CoAgent modelAgent = new JMP.MDL.CoAgent();
            //查询一条数据
            //  modelAgent = bllAgent.GetModel(id);
            var modelAgent = _coAgentService.FindById(id);
            ViewBag.modelAgent = modelAgent;
            return View();
        }
        /// <summary>
        /// 修改代理商
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public JsonResult UpdateAgents(JMP.MDL.CoAgent jmpagent)
        {
            object obj = new { success = 0, msg = "更新失败！" };
            JMP.BLL.CoAgent userBll = new JMP.BLL.CoAgent();
            JMP.MDL.CoAgent oldUser = userBll.GetModel(jmpagent.Id);
            var oldUserClone = oldUser.Clone();
            oldUser.Classify = jmpagent.Classify;
            oldUser.LoginName = jmpagent.LoginName;
            oldUser.Password = DESEncrypt.Encrypt(jmpagent.Password);
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

                if (jmpagent.Classify == 0)
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

                //jmpagent.CreatedOn = oldUser.CreatedOn;
                //jmpagent.CreatedById = oldUser.CreatedById;
                //jmpagent.CreatedByName = oldUser.CreatedByName;
                //jmpagent.OwnerId = oldUser.OwnerId;
                //jmpagent.OwnerName = oldUser.OwnerName;
                bool flag = _coAgentService.Update(oldUser);
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
        /// 设置费率
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public ActionResult ServiceChargeAdd()
        {
            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);
            var user = _coAgentService.FindById(id);
            JMP.BLL.CoServiceFeeRatioGrade bll = new JMP.BLL.CoServiceFeeRatioGrade();
            DataTable dt = bll.GetList(" ").Tables[0];//获取应用平台在用信息 
            List<JMP.MDL.CoServiceFeeRatioGrade> yypt = JMP.TOOL.MdlList.ToList<JMP.MDL.CoServiceFeeRatioGrade>(dt);
            ViewBag.glptdt = yypt;
            ViewBag.user = user;
            ViewBag.id = id;
            return View();
        }

        /// <summary>
        /// 设置费率方法
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public JsonResult ScAdd()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);
            int ServiceFeeRatioGradeId = string.IsNullOrEmpty(Request["ServiceFeeRatioGradeId"]) ? 0 : int.Parse(Request["ServiceFeeRatioGradeId"]);
            var mode = _coAgentService.FindById(id);
            if (_coAgentService.UpdateById(id, new { ServiceFeeRatioGradeId = ServiceFeeRatioGradeId  }))
            {
                Logger.OperateLog("代理商设置服务费率", "代理商id：" + mode.Id + ",费率等级由：" + mode.ServiceFeeRatioGradeId + ",改为：" + ServiceFeeRatioGradeId + "。");
                retJson = new { success = 1, msg = "设置成功！" };
            }
            else
            {
                retJson = new { success = 0, msg = "设置失败！" };
            }

            return Json(retJson);
        }


        #region 验证方法

        /// <summary>
        /// 验证登录名称是否存在
        /// </summary>
        /// <param name="lname">邮箱地址</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
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
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
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
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
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
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
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
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
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

    }
}
