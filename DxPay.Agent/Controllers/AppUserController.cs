using DxPay.Agent;
using DxPay.Agent.Models;
using DxPay.Agent.Util.Logger;
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
using System.Web;
using System.Web.Mvc;
using TOOL.EnumUtil;
using TOOL.Extensions;

namespace DxPay.Agent.Controllers
{
  
    public class AppUserController : Controller
    {
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;
        JMP.BLL.jmp_user userBll = new JMP.BLL.jmp_user();

        private readonly IUserService _UserService;
        private readonly ICoAgentService _CoAgentService;


        public AppUserController()
        {

            _UserService = ServiceFactory.UserService;
            _CoAgentService = ServiceFactory.CoAgentService;
        }

        /// <summary>
        /// 开发者管理
        /// </summary>
        /// <returns></returns>
        
        public ActionResult AppUserList()
        {
            #region 初始化
            //获取开发者平台地址
            string Userurl = System.Configuration.ConfigurationManager.AppSettings["Userurl"];
            ViewBag.Userurl = Userurl;
            //获取请求参数
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量
            string type = string.IsNullOrEmpty(Request["stype"]) ? "0" : Request["stype"];//查询条件类型
            string sea_name = string.IsNullOrEmpty(Request["skeys"]) ? "" : Request["skeys"].Trim();//查询条件值
            string stat = string.IsNullOrEmpty(Request["state"]) ? "" : Request["state"];//用户状态
            string tag = string.IsNullOrEmpty(Request["scheck"]) ? "" : Request["scheck"];//审核状态
            string category = string.IsNullOrEmpty(Request["scategory"]) ? "" : Request["scategory"];//认证类型
            int px = string.IsNullOrEmpty(Request["s_sort"]) ? 1 : Int32.Parse(Request["s_sort"]);//排序
            //获取用户列表
            string where = "";
            if (!string.IsNullOrEmpty(type.ToString()))
            {
                if (!string.IsNullOrEmpty(sea_name))
                {
                    if (type == "0")
                        where += string.Format(" and  u_email like '%{0}%'", sea_name);
                    else if (type == "1")
                        where += string.Format(" and  u_realname like '%{0}%'", sea_name);

                    else if (type == "4")
                        where += string.Format(" and u_name like '%{0}%'", sea_name);
                 
                }
            }
            if (!string.IsNullOrEmpty(stat))
                where += string.Format(" and u_state={0}", stat);
            if (!string.IsNullOrEmpty(tag))
                where += string.Format(" and u_auditstate={0}", tag);
            if (!string.IsNullOrEmpty(category))
                where += string.Format(" and u_category={0}", category);
            string Order = " order by u_id " + (px == 0 ? "" : " desc ") + " ";
            var userid = UserInfo.UserId;
            string query = "select * from jmp_user a  left join  CoServiceFeeRatioGrade  b  on a.ServiceFeeRatioGradeId=b.Id  where relation_type=" + (int)Relationtype.Agent+" and relation_person_id='" + userid+"' " + where;
            var list = _UserService.FindPagedListBySql(Order,query,null, pageIndexs, PageSize);
            var gridModel = new DataSource<jmp_user>(list)
            {
                Data = list.Select(x => x).ToList()
            };
            //返回
            ViewBag.CurrPage = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.PageCount = gridModel.Pagination.TotalCount; 
            ViewBag.stype = type;
            ViewBag.skeys = sea_name;
            ViewBag.state = stat;
            ViewBag.scategory = category;
            ViewBag.scheck = tag;
            ViewBag.s_sort = px;
            ViewBag.list = gridModel.Data;
           // ViewBag.btnstr = GetVoidHtml();
            #endregion
            return View();
        }
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
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public JsonResult AddUser(JMP.MDL.jmp_user jmpuser)
        {
            object obj = new { success = 0, msg = "添加失败！" };
            JMP.BLL.jmp_user userBll = new JMP.BLL.jmp_user();
            jmpuser.u_password = DESEncrypt.Encrypt(jmpuser.u_password);
            jmpuser.relation_person_id =  UserInfo.UserId;
            jmpuser.relation_type = (int)Relationtype.Agent;
            jmpuser.u_state = 1;
            jmpuser.ServiceFeeRatioGradeId = _CoAgentService.FindMax(" select top 1 Id  from  CoServiceFeeRatioGrade where  ServiceFeeRatio in(select  max(ServiceFeeRatio) from CoServiceFeeRatioGrade ) ");
            int RoleID = int.Parse(ConfigurationManager.AppSettings["UserRoleId"]);
            jmpuser.u_role_id = RoleID;
            jmpuser.u_photof = string.IsNullOrEmpty(jmpuser.u_photof) ? "" : jmpuser.u_photof;
            jmpuser.u_licence = string.IsNullOrEmpty(jmpuser.u_licence) ? "" : jmpuser.u_licence;
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
                jmpuser.u_time = DateTime.Now;

                bool flag = false;
                if (!userBll.ExistsEmail(jmpuser.u_email))
                    flag = _UserService.Insert(jmpuser) > 0;
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
        /// 批量更新用户状态
        /// </summary>
        /// <param name="uIds">用户id列表</param>
        /// <param name="tag">状态</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
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

        /// <summary>
        /// 修改开发者
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public ActionResult AppUserEdit()
        {
            int userId = !string.IsNullOrEmpty(Request["uid"]) ? int.Parse(Request["uid"]) : 0;
            JMP.BLL.jmp_user bll = new JMP.BLL.jmp_user();
            var model = _UserService.FindById(userId);
            ViewBag.UserData = model;
            ViewBag.UploadUrl = ConfigurationManager.AppSettings["imgurl"];
            return View();
        }
        /// <summary>
        /// 保存用户（编辑）
        /// </summary>
        /// <param name="jmpuser"></param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public JsonResult UpdateUser(JMP.MDL.jmp_user jmpuser)
        {
            object obj = new { success = 0, msg = "更新失败！" };
            JMP.BLL.jmp_user userBll = new JMP.BLL.jmp_user();
            var oldUser = _UserService.FindById(jmpuser.u_id);
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
            oldUser.u_blicensenumber = jmpuser.u_blicensenumber;
            oldUser.BusinessEntity = jmpuser.BusinessEntity;
            oldUser.RegisteredAddress = jmpuser.RegisteredAddress;
            oldUser.u_auditstate = jmpuser.u_auditstate;
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
                }
                if (string.IsNullOrEmpty(oldUser.u_qq))
                {
                    oldUser.u_qq = " ";
                }
                if (string.IsNullOrEmpty(oldUser.u_address))
                {
                    oldUser.u_address = " ";
                }
                bool flag = _UserService.Update(oldUser);
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

        /// <summary>
        /// 设置费率
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public ActionResult ServiceChargeAdd()
        {
            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);
            var user = _UserService.FindById(id);
            JMP.BLL.CoServiceFeeRatioGrade bll = new JMP.BLL.CoServiceFeeRatioGrade();
            DataTable dt = bll.GetList(" ").Tables[0];//获取应用平台在用信息 
            List<JMP.MDL.CoServiceFeeRatioGrade> yypt = JMP.TOOL.MdlList.ToList<JMP.MDL.CoServiceFeeRatioGrade>(dt);
            ViewBag.glptdt = yypt;
            ViewBag.user = user;
            ViewBag.glptdt = yypt;
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

            JMP.BLL.jmp_user bllUser = new JMP.BLL.jmp_user();
            JMP.MDL.jmp_user modelUser = new JMP.MDL.jmp_user();
            int ServiceFeeRatioGradeId = string.IsNullOrEmpty(Request["ServiceFeeRatioGradeId"]) ? 0 : int.Parse(Request["ServiceFeeRatioGradeId"]);
            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);
            //修改前的数据
            modelUser = bllUser.GetModel(id);
            if (bllUser.UpdateServiceFeeRatioGradeId(id, ServiceFeeRatioGradeId))
            {
                Logger.OperateLog("商务设置直客开发者费率", "商务ID：" + modelUser.relation_person_id + ",操作数据(jmp_user)ID：" + id + ",费率等级由：" + modelUser.ServiceFeeRatioGradeId + ",改为：" + ServiceFeeRatioGradeId + "。");
                retJson = new { success = 1, msg = "设置成功！" };
            }
            else
            {
                retJson = new { success = 0, msg = "设置失败！" };
            }

            return Json(retJson);
        }
      

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

        /// <summary>
        /// 上传图片(身份证正面)
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// 上传图片(许可证照片)
        /// </summary>
        /// <returns></returns>
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


    }
    #endregion
}