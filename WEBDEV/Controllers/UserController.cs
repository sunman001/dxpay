/************聚米支付平台__用户管理************/
//描述：开发者前端的管理（注册、激活、找回密码等）
//功能：开发者前端的管理（注册、激活、找回密码等）
//开发者：谭玉科
//开发时间: 2016.04.27
/************聚米支付平台__用户管理************/
using JMP.TOOL;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TOOL;
using TOOL.EnumUtil;
using TOOL.Extensions;
using TOOL.Message;
using TOOL.Message.TextMessage.ChuangLan;
using WEBDEV.Models;
using WEBDEV.Util.Logger;

namespace WEBDEV.Controllers
{
    /// <summary>
    /// 类名：UserController
    /// 功能：开发者前端的管理
    /// 详细：开发者前端的管理
    /// 修改日期：2016.05.25
    /// </summary>
    public class UserController : Controller
    {
        /// <summary>
        /// 验证码SESSION KEY
        /// </summary>
        private static string VALIDATE_CODE_KEY = "vc_key";
        JMP.BLL.jmp_user bll = new JMP.BLL.jmp_user();
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;
        #region 注册
        /// <summary>
        /// 注册页面
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false)]
        public ActionResult Register()
        {
            List<JMP.MDL.CoBusinessPersonnel> list = new List<JMP.MDL.CoBusinessPersonnel>();
            JMP.MDL.CoBusinessPersonnel model = new JMP.MDL.CoBusinessPersonnel();
            JMP.BLL.CoBusinessPersonnel bll = new JMP.BLL.CoBusinessPersonnel();

            //查询所有商务
            list = bll.GetModelList(" State=0  ");
            ViewBag.list = list;
            var rand = new JMP.MDL.CoBusinessPersonnel();

            var today = list.Where(x => x.LogintTime != null && x.LogintTime.GetValueOrDefault().ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd")).OrderBy(x => Guid.NewGuid()).FirstOrDefault();

            ////随机获取一个当天登录过的商务
            //model = bll.getModelLoginTime();
            ViewBag.model = today;
            return View();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = false)]
        public JsonResult UserReg(JMP.MDL.jmp_user jUser)
        {
            //pv_code:phone_verify_code
            var message = MobileVerifyCodeIsEnable(jUser.u_phone, Request.QueryString["pv_code"] ?? "");
            if (!message.Success)
            {
                return Json(new { success = false, msg = message.Message });
            }
            JMP.BLL.jmp_limit bll_limit = new JMP.BLL.jmp_limit();
            var sName = DESEncrypt.Encrypt(jUser.u_email, "email");
            jUser.u_password = DESEncrypt.Encrypt(jUser.u_password);
            jUser.u_realname = "";
            jUser.u_phone = jUser.u_phone.Trim();
            jUser.u_qq = "";
            jUser.u_address = "";
            jUser.u_drawing = 0;
            //正式上线后，邮箱激活了且审核通过了才能登录
            jUser.u_state = 1;
            jUser.u_auditstate = 0;
            jUser.u_role_id = int.Parse(ConfigurationManager.AppSettings["UserRoleId"]);
            //关联关系商务
            jUser.relation_type = (int)Relationtype.Bp;
            jUser.relation_person_id = jUser.relation_person_id;
            jUser.IsSpecialApproval = false;

            JMP.BLL.CoServiceFeeRatioGrade grade_bll = new JMP.BLL.CoServiceFeeRatioGrade();
            //查询默认费率
            JMP.MDL.CoServiceFeeRatioGrade grade_model = grade_bll.GetModelById();
            //设置默认费率
            jUser.ServiceFeeRatioGradeId = string.IsNullOrEmpty(grade_model.Id.ToString()) ? 0 : grade_model.Id;
            jUser.u_time = DateTime.Now;

            if (jUser.u_phone.Length <= 0)
            {
                return Json(new { success = false, msg = "注册失败,请填写并验证您的手机号码!" });
            }
            var exist = bll.ExistsPhone(jUser.u_phone);
            if (exist)
            {
                return Json(new { success = false, msg = "注册失败,该手机号码已存在!" });
            }
            var uid = bll.Add(jUser);
            JMP.MDL.jmp_user Model = bll.GetModelByTel(jUser.u_phone);
            //写日志
            if (uid > 0)
            {
                UserInfo.UserId = Model.u_id;
                UserInfo.UserName = Model.u_realname;
                UserInfo.UserNo = Model.u_email;
                UserInfo.UserRoleId = Model.u_role_id;
                UserInfo.auditstate = Model.u_auditstate.ToString();
                DataTable dtLimit = bll_limit.GetAppUserLimitSession(Model.u_id, Model.u_role_id);
                if (dtLimit.Rows.Count > 0)
                {
                    Session["dtSession"] = dtLimit;
                    jUser.u_count += 1;
                    bll.Update(jUser);
                    string log = string.Format("开发者{0}于{1}登录聚米支付平台。", UserInfo.UserNo, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    AddLocLog.AddUserLog(UserInfo.UserId, 2, RequestHelper.GetClientIp(), "用户" + UserInfo.UserName + "登录。", log);

                }
                AddLocLog.AddUserLog(uid, 1, RequestHelper.GetClientIp(), "开发者注册", jUser.u_email + "于" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "注册。");
            }
            return Json(new { success = uid > 0 ? 1 : 0, msg = uid > 0 ? "注册成功！" : "注册失败！", uname = sName });
        }

        /// <summary>
        /// 判断是否存在邮箱
        /// </summary>
        /// <param name="u_email">邮箱账号</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false)]
        public JsonResult CheckEmail(string u_email)
        {
            bool flag = bll.ExistsEmail(u_email);
            return Json(new { success = flag ? 1 : 0, msg = flag ? "已存在该账号！" : "不存在该账号！" });
        }
        #endregion


        # region 判断手机是否存在
        /// <summary>
        /// 判断手机是否存在
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = false)]
        public JsonResult CheckPhone(string phone)
        {
            var flag = bll.ExistsPhone(phone);
            return Json(new { success = flag ? 1 : 0, msg = flag ? "已存在该手机号码！" : "不存在该手机号码！" });
        }
        #endregion

        #region 注册激活
        /// <summary>
        /// 注册成功页面
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false)]
        public ActionResult RegSuccess(string u_email)
        {
            ViewBag.UserEmail = u_email;
            return View();
        }


        #endregion

        #region 提交认证资料


        /// <summary>
        /// 开发者认证页面
        /// </summary>
        /// <param name="uname">要认证的邮箱账号</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public ActionResult DevVerify()
        {
            int name = UserInfo.Uid;

            JMP.MDL.jmp_user j_user = bll.GetModel(name);

            ViewBag.j_user = j_user;
            ViewBag.auditstate = j_user.u_auditstate;
            ViewBag.linkEmail = j_user.u_email;

            ViewBag.UploadUrl = ConfigurationManager.AppSettings["imgurl"];
            return View();
        }

        #endregion

        /// <summary>
        /// 提交开发者认证资料 
        /// </summary>
        /// <param name="develop">开发者实例</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false)]
        public JsonResult VerifyInfo(JMP.MDL.jmp_user develop)
        {
            object obj = new { success = 0, msg = "开发者资料提交失败！" };
            develop.u_address = !string.IsNullOrEmpty(develop.u_address) ? develop.u_address : "";
            develop.u_photof = !string.IsNullOrEmpty(develop.u_photof) ? develop.u_photof : "";
            develop.u_licence = !string.IsNullOrEmpty(develop.u_licence) ? develop.u_licence : "";
            JMP.MDL.jmp_user j_user = bll.GetModel(develop.u_email);
            if (j_user != null)
            {
                if (develop.u_category == 0)
                {
                    develop.u_photo = string.IsNullOrEmpty(develop.u_photo) ? "" : develop.u_photo;
                }
                else
                {
                    develop.u_blicense = string.IsNullOrEmpty(develop.u_blicense) ? "" : develop.u_blicense;
                    develop.u_photo = string.IsNullOrEmpty(develop.u_photo) ? " " : develop.u_photo;
                }

                bool flag = bll.UpdateByEmail(develop);
                obj = new { success = flag ? 1 : 0, msg = flag ? "开发者资料提交成功！" : "开发者资料提交失败！" };

            }
            return Json(obj);
        }

        /// <summary>
        /// 提交开发者认证资料成功页面
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false)]
        public ActionResult DevVerifySucc()
        {

            return View();
        }
        /// <summary>
        /// 导入页面
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false)]
        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        [LoginCheckFilter(IsCheck = false)]
        public JsonResult ImportApi(FormCollection form)
        {
            var errorCards = new List<string>();
            var freezeCards = new List<string>();
            var freeCount = new List<string>();
           if (!UserInfo.IsLogin)
            {
                return Json(new { success = false, msg = "登录超时，请重新登录！" });
            }
            if (Request.Files["files"] != null && Request.Files["files"].ContentLength > 0)

            {
                var file = Request.Files["files"];
                string fileEx = System.IO.Path.GetExtension(file.FileName);
                if(fileEx!=".xls" && fileEx != ".xlsx")
                {
                    return Json(new { success = false, msg = "请上传正确的文件" });
                }
                var rootDir = Server.MapPath("~/files/uploads/");
                if(!Directory.Exists(rootDir))
                {
                    Directory.CreateDirectory(rootDir);
                }
                var uploadFileName = Path.GetFileName(file.FileName);
                var fileFullPath = rootDir + uploadFileName;
                file.SaveAs(fileFullPath);

                var excel = new FileInfo(fileFullPath);
                using (var package = new ExcelPackage(excel))
                {
                    var workbook = package.Workbook;

                    //*** Sheet 1
                    var worksheet = workbook.Worksheets.First();

                    //*** Retrieve to List
                    var ls = new List<ImportUserCardModel>();
                    var totalRows = worksheet.Dimension.End.Row;
                    for (var i = 2; i <= totalRows; i++)
                    {
                        //是否合法
                        var valid = false;
                        var msg = new List<string>();

                        var d = worksheet.Cells[i, 1];
                        var card = new ImportUserCardModel
                        {
                            u_state = 0,
                            u_freeze = 0
                            //u_date = DateTime.Now
                        };
                        //card.u_userid = worksheet.Cells[i, 1].Value == null ? 0 : Convert.ToInt32(worksheet.Cells[i, 1].Value.ToString());
                        card.u_banknumber = worksheet.Cells[i, 1].Value == null ? "" : worksheet.Cells[i, 1].Value.ToString();
                        card.u_bankname = worksheet.Cells[i, 2].Value == null ? "" : worksheet.Cells[i,2].Value.ToString();
                        card.u_openbankname = worksheet.Cells[i, 3].Value == null ? "" : worksheet.Cells[i, 3].Value.ToString();
                        card.u_name = worksheet.Cells[i, 4].Value == null ? "" : worksheet.Cells[i, 4].Value.ToString();
                        card.u_province = worksheet.Cells[i, 5].Value == null ? "" : worksheet.Cells[i, 5].Value.ToString();
                        card.u_area = worksheet.Cells[i, 6].Value == null ? "" : worksheet.Cells[i, 6].Value.ToString();
                        card.u_flag = worksheet.Cells[i, 7].Value == null ? "" : worksheet.Cells[i, 7].Value.ToString();
                        card.u_userid = UserInfo.Uid;
                        if (string.IsNullOrEmpty(card.u_banknumber))
                        {
                            msg.Add(string.Format("银行卡为空,第{0}行第{1}列", i, 2));
                        }
                        if (string.IsNullOrEmpty(card.u_name))
                        {
                            msg.Add(string.Format("持卡人姓名为空,第{0}行第{1}列", i, 5));
                        }
                        if (string.IsNullOrEmpty(card.u_province))
                        {
                            msg.Add(string.Format("省份为空,第{0}行第{1}列", i, 6));
                        }
                        if (string.IsNullOrEmpty(card.u_area))
                        {
                            msg.Add(string.Format("城市为空,第{0}行第{1}列", i, 7));
                        }
                        if(string.IsNullOrEmpty(card.u_flag))
                        {
                            msg.Add(string.Format("付款标识为空,第{0}行第{1}列", i, 7));
                        }
                        else if (card.u_flag!="00"&& card.u_flag!="01")
                        {
                            msg.Add(string.Format("付款标识值错误,第{0}行第{1}列", i, 7));
                        }
                        //判断此银行卡是否存在
                         valid = userBankBll.ExistsBankNo(card.u_banknumber.Trim(),"");
                        if (valid)
                        {
                            var userbank = userBankBll.GetUserBankByBankNo(card.u_banknumber.Trim(), card.u_userid);
                            if (userbank != null)
                            {
                                if (userbank.u_freeze == 1)
                                {
                                  userBankBll.UpdateState(userbank.u_id.ToString(),0);
                                  freezeCards.Add(string.Format("已存在的银行卡被冻结{0}",userbank.u_banknumber));
                                  freeCount.Add(string.Format("已存在的银行卡被冻结{0}", userbank.u_banknumber));
                                }
                            }
                            else
                            {
                            msg.Add(string.Format("银行卡已存在,第{0}行第{1}列", i, 2)); 
                            }
                        }
                        if (msg.Count > 0)
                        {
                            errorCards.Add(string.Format("银行卡验证失败:{0}--{1}", card.u_banknumber, string.Join(",", msg)));
                            continue;
                        }
                        if (freezeCards.Count!=1)
                        {
                             ls.Add(card);
                        }
                        freezeCards.Clear();
                    }
                    if(ls.Count>0)
                    {
                    var _bll = new JMP.BLL.BllCommonQuery();
                    _bll.BulkInsert("jmp_userbank", ls);
                    }                  
                    if (errorCards.Count > 0)
                    {

                        if (freeCount.Count > 0)
                        {
                            return Json(new { success = errorCards.Count <= 0, msg = string.Join("<br />", errorCards.Select((x, i) => string.Format("{0}: {1}", i + 1, x))), successcount = ls.Count, errcount = errorCards.Count,freeCount=freeCount.Count });
                        }
                        else
                        {
                             return Json(new { success = errorCards.Count <= 0, msg = string.Join("<br />", errorCards.Select((x,i)=>string.Format("{0}: {1}",i+1,x))),successcount=ls.Count,errcount=errorCards.Count }); 
                        }
                      
                       
                    }
                 
                    #region 删除上传的临时文件
                    //TODO:删除上传的临时文件
                    Thread.Sleep(1500);
                    try
                    {
                        System.IO.File.Delete(fileFullPath);
                    }
                    catch { }
                    #endregion

                }
            }
            else
            {
              return Json(new { success = false, msg = "请上传附件" });
            }
            return Json(new { success = true, msg = "操作成功" });
        }
        /// <summary>
        /// 验证身份证号是否存在
        /// </summary>
        /// <param name="idno">身份证号</param>
        /// <param name="uname">邮箱</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false)]
        public JsonResult CheckIdno(string idno, string uname)
        {
            bool flag = bll.ExistsIdnos(idno, uname);
            return Json(new { success = flag ? 1 : 0, msg = flag ? "已存在该身份证号！" : "不存在身份证号！" });
        }

        /// <summary>
        /// 验证营业执照是否存在
        /// </summary>
        /// <param name="yyzz">营业执照</param>
        /// <param name="uname">邮箱</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false)]
        public JsonResult CheckYyzz(string yyzz, string uname)
        {
            bool flag = bll.ExistsYyzzs(yyzz, uname);
            return Json(new { success = flag ? 1 : 0, msg = flag ? "已存在该营业执照！" : "不存在营业执照！" });
        }

        /// <summary>
        /// 验证开户账号是否存在
        /// </summary>
        /// <param name="account">开户账号</param>
        /// <param name="uname">邮箱</param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false)]
        public JsonResult CheckBankNo(string account, string uname)
        {
            bool flag = bll.ExistsBankNos(account, uname);
            return Json(new { success = flag ? 1 : 0, msg = flag ? "已存在该开户账号！" : "不存在开户账号！" });
        }

        /// <summary>
        /// 上传图片(正身份证)
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
        /// 上传图片(正身份证)
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
        /// 上传开户许可证
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


        #region 个人信息
        /// <summary>
        /// 个人信息
        /// </summary>
        /// <returns></returns>
        public ActionResult PersonalInfo()
        {
            JMP.MDL.jmp_user j_user = bll.GetModel(UserInfo.Uid);
            ViewBag.jmpUser = j_user;
            ViewBag.auditstate = j_user.u_auditstate;
            ViewBag.linkEmail = j_user.u_email;
            ViewBag.UploadUrl = ConfigurationManager.AppSettings["imgurl"];

            return View();
        }
        #endregion

        #region 修改密码
        /// <summary>
        /// 修改信息页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ModifyInfo()
        {
            JMP.MDL.jmp_user j_user = bll.GetModel(UserInfo.UserNo);
            ViewBag.jmpUser = j_user;
            ViewBag.auditstate = j_user.u_auditstate;
            ViewBag.linkEmail = j_user.u_email;

            return View();
        }

        /// <summary>
        /// 保存修改信息
        /// </summary>
        /// <param name="develop"></param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult UpdateUserInfo(JMP.MDL.jmp_user develop)
        {
            object result = new { success = 0, msg = "修改失败！" };

            int u_id = string.IsNullOrEmpty(Request["u_id"]) ? 0 : int.Parse(Request["u_id"]);
            string o_pwd = !string.IsNullOrEmpty(Request["upass"]) ? Request["upass"] : "";
            string n_pwd = !string.IsNullOrEmpty(Request["xpass"]) ? Request["xpass"] : "";

            JMP.MDL.jmp_user j_user = bll.GetModel(u_id);

            //判断是否修改了密码
            if (!string.IsNullOrEmpty(o_pwd))
            {
                string temp = DESEncrypt.Encrypt(o_pwd);
                if (temp == j_user.u_password)
                {
                    string u_password = DESEncrypt.Encrypt(n_pwd);
                    bool flag = bll.UpdatePwd(j_user.u_email, u_password);
                    result = new { success = 1, msg = "修改成功！" };
                }
                else
                {
                    result = new { success = 2, msg = "原密码输入错误！" };
                }
            }

            return Json(result);

        }
        #endregion

        #region 手机短信验证
        #region 获取手机验证码(注册)

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false)]
        public ActionResult GetValidateCode()
        {
            var _vh = Request.QueryString["vh"] ?? "36";
            var vh = 36;
            try
            {
                vh = Convert.ToInt32(_vh);
            }
            catch
            {
            }
            var vCode = new ValidateCode();
            var code = vCode.GetRandomCode(4);
            Session[VALIDATE_CODE_KEY] = code;

            var bytes = vCode.CreateImageTwo(code, vh);
            return File(bytes, @"image/jpeg");
        }

        /// <summary>
        /// 获取手机验证码
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = false)]
        public JsonResult GetVerifyCode()
        {
            if (ConfigReader.GetSettingValueByKey("MESSAGE.ENABLED") == "false")
            {
                return Json(new { success = false, message = "短信验证不可用" });
            }
            var phone = Request["u_phone"].Trim();
            var vc = Request["vc"] == null ? "" : Request["vc"].Trim().ToLower();
            if (string.IsNullOrEmpty(vc))
            {
                return Json(new { success = false, message = "验证码错误" });
            }
            var validateCode = Session[VALIDATE_CODE_KEY];
            if (validateCode == null)
            {
                return Json(new { success = false, message = "验证码已失效,请重新获取" });
            }
            if (validateCode.ToString() != vc)
            {
                return Json(new { success = false, message = "验证码错误" });
            }
            if (string.IsNullOrEmpty(phone))
            {
                return Json(new { success = false, message = "请输入手机号码!" });
            }
            var validate = MvcApplication.TextMessageSendHistoryInstance.RecordSentMessage(phone);
            if (!validate.AllowSend)
            {
                return Json(new { success = false, message = validate.Message });
            }

            var phoneExist = bll.ExistsPhone(phone);
            if (phoneExist)
            {
                return Json(new { success = false, message = "该手机号码已被注册!" });
            }
            var model = Session["verify_code"] as PhoneVerifyModel;
            if (model != null)
            {
                var interval = 60;
                try
                {
                    interval = Convert.ToInt32(ConfigReader.GetSettingValueByKey("MESSAGE.INTERVAL"));
                }
                catch { }
                if ((DateTime.Now - model.LatestSendTime).TotalMilliseconds <= 1000 * interval)
                {
                    return Json(new { success = false, message = "请勿重复提交" });
                }
            }
            model = new PhoneVerifyModel
            {
                CreatedTime = DateTime.Now,
                Phone = phone,
                Code = RandomHelper.GetRandomizer(6, true, false, false, false)
            };
            var chuangLanRequest = new ChuangLanRequest
            {
                //必填
                Account = ConfigReader.GetSettingValueByKey("CHUANGLAN.ACCOUNT"),
                //必填
                Password = ConfigReader.GetSettingValueByKey("CHUANGLAN.PASSWORD"),
                //必填
                Content = string.Format(ConfigReader.GetSettingValueByKey("CHUANGLAN.CONTENT.REGISTER"), model.Code),
                //必填(多个手机号以英文逗号隔开)
                Mobile = model.Phone
            };
            IMessageSender messageSender = new ChuangLanMessageSender(chuangLanRequest);
            var success = false;
            model.LatestSendTime = DateTime.Now;
            try
            {
                success = messageSender.Send();
                if (success)
                {
                    Session["verify_code"] = model;
                }
            }
            catch (Exception ex)
            {
                AddLocLog.AddLog(1, 4, Request.UserHostAddress, "短信接口错误", ex.ToString());
            }
            return Json(success ? new { success = true, message = "验证码已发送至你的手机,请查收" } : new { success = false, message = "服务器忙,请稍候再试" });
        }
        #endregion
        #region 验证手机验证码(注册)
        /// <summary>
        /// 验证手机验证码
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = false)]
        public JsonResult VerifyCode()
        {
            var phone = Request["u_phone"] ?? "";
            var code = Request["code"] ?? "";
            var message = MobileVerifyCodeIsEnable(phone, code);
            return Json(new { success = message.Success, message = message.Message });
        }
        #endregion

        /// <summary>
        /// 判断手机短信验证码是否可用的方法
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private MobileVerifyMessage MobileVerifyCodeIsEnable(string phone, string code)
        {
            var message = new MobileVerifyMessage();
            if (string.IsNullOrEmpty(phone) || code.Trim().Length <= 0)
            {
                message.Message = "信息不完整";
                return message;
                //return Json(new { success = false, message = "信息不完整" });
            }
            var model = Session["verify_code"] as PhoneVerifyModel;
            if (model == null)
            {
                message.Message = "请先获取验证码";
                return message;
                //return Json(new { success = false, message = "验证码为空,请重新获取" });
            }
            //过期时间:3分钟
            if ((DateTime.Now - model.LatestSendTime).TotalMilliseconds > 1000 * 60 * 3)
            {
                Session["verify_code"] = null;
                message.Message = "验证码已过期,请重新获取";
                return message;
                //return Json(new { success = false, message = "验证码已过期,请重新获取" });
            }
            if (!model.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase) || !model.Phone.Equals(phone, StringComparison.InvariantCultureIgnoreCase))
            {
                message.Message = "验证码错误,请重新输入";
                return message;
                //return Json(new {success = false, message = "验证码错误,请重新输入"});
            }
            //如果执行到此,则说明手机验证码验证成功,清除Session中的短信验证码
            Session["verify_code"] = null;
            message.Success = true;
            message.Message = "手机验证成功";
            return message;
        }

        #region 获取手机验证码(修改密码)
        /// <summary>
        /// 获取手机验证码
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = false)]
        public JsonResult GetVerifyCodePwd()
        {
            var phone = Request["u_phone"];
            if (string.IsNullOrEmpty(phone))
            {
                return Json(new { success = false, message = "请输入手机号码!" });
            }
            var phoneExist = bll.ExistsPhone(phone);
            if (!phoneExist)
            {
                return Json(new { success = false, message = "该手机号码不存在!" });
            }
            var model = Session["pwd_verify_code"] as PhoneVerifyModel;
            if (model != null)
            {
                if ((DateTime.Now - model.LatestSendTime).TotalMilliseconds <= 1000 * 60 * 3)
                {
                    if ((DateTime.Now - model.LatestSendTime).TotalMilliseconds <= 1000 * 60)
                    {
                        return Json(new { success = false, message = "请勿重复提交" });
                    }
                    if (model.Used)
                    {
                        Session["pwd_verify_code"] = null;
                        return Json(new { success = false, message = "验证码已过期" });
                    }
                }
            }
            model = new PhoneVerifyModel
            {
                CreatedTime = DateTime.Now,
                Phone = phone,
                Code = RandomHelper.GetRandomizer(6, true, false, false, false),
                LatestSendTime = DateTime.Now
            };
            var chuangLanRequest = new ChuangLanRequest
            {
                //必填
                Account = ConfigReader.GetSettingValueByKey("CHUANGLAN.ACCOUNT"),
                //必填
                Password = ConfigReader.GetSettingValueByKey("CHUANGLAN.PASSWORD"),
                //必填
                Content = string.Format(ConfigReader.GetSettingValueByKey("CHUANGLAN.CONTENT.FORGETPASSWORD"), model.Code),
                //必填(多个手机号以英文逗号隔开)
                Mobile = model.Phone
            };
            IMessageSender messageSender = new ChuangLanMessageSender(chuangLanRequest);
            var success = false;
            try
            {
                success = messageSender.Send();
                if (success)
                {

                    Session["pwd_verify_code"] = model;
                }
            }
            catch (Exception ex)
            {
                AddLocLog.AddLog(1, 4, Request.UserHostAddress, "短信接口错误", ex.ToString());
            }
            return Json(success ? new { success = true, message = "验证码已发送至你的手机,请查收" } : new { success = false, message = "服务器忙,请稍候再试" });
        }
        #endregion

        #region 验证重置密码的验证码
        /// <summary>
        /// 验证重置密码的验证码
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = false)]
        public JsonResult CheckVerifyCodePwd()
        {
            var phone = Request["u_phone"] ?? "";
            var code = Request["code"] ?? "";
            if (phone.Trim().Length <= 0 || code.Trim().Length <= 0)
            {
                return Json(new { success = false, message = "信息不完整" });
            }
            var model = Session["pwd_verify_code"] as PhoneVerifyModel;
            if (model == null)
            {
                return Json(new { success = false, message = "请先获取验证码" });
            }
            if (model.Used)
            {
                return Json(new { success = false, message = "验证码已过期" });
            }
            //过期时间:3分钟
            if ((DateTime.Now - model.LatestSendTime).TotalMilliseconds > 1000 * 60 * 3)
            {
                Session["pwd_verify_code"] = null;
                return Json(new { success = false, message = "验证码已过期,请重新获取" });
            }
            if (!model.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase) ||
                !model.Phone.Equals(phone, StringComparison.InvariantCultureIgnoreCase))
                return Json(new { success = false, message = "验证码错误,请重新输入" });
            model.Used = true;
            Session["pwd_verify_code"] = null;

            return Json(new { success = true, message = "手机验证成功", });
        }
        #endregion

        #endregion

        #region 找回密码(手机)

        /// <summary>
        /// 修改密码(手机)
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = false)]
        [HttpGet]
        public ActionResult ChangePwd()
        {
            var phone = Request.QueryString["phone"] ?? "";
            var code = Request.QueryString["code"] ?? "";
            ViewBag.Phone = phone;
            ViewBag.Code = code;
            return View();
        }

        [LoginCheckFilter(IsCheck = false)]
        public JsonResult clickChangePwd()
        {
            var phone = Request["phone"] ?? "";
            var code = Request["code"] ?? "";
            if (phone.Trim().Length <= 0 || code.Trim().Length <= 0)
            {
                return Json(new { success = false, message = "信息不完整" });
            }
            var model = Session["pwd_verify_code"] as PhoneVerifyModel;
            if (model == null)
            {
                return Json(new { success = false, message = "请先获取验证码" });
            }
            if (model.Used)
            {
                return Json(new { success = false, message = "验证码已过期" });
            }
            //过期时间:3分钟
            if ((DateTime.Now - model.LatestSendTime).TotalMilliseconds > 1000 * 60 * 3)
            {
                Session["pwd_verify_code"] = null;
                return Json(new { success = false, message = "验证码已过期,请重新获取" });
            }
            if (!model.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase) ||
                !model.Phone.Equals(phone, StringComparison.InvariantCultureIgnoreCase))
                return Json(new { success = false, message = "验证码错误,请重新输入" });
            model.Used = true;
            Session["pwd_verify_code"] = null;
            var a = DESEncrypt.Encrypt(phone);
            return Json(new { success = true, message = "手机验证成功", url = a });
        }

        [LoginCheckFilter(IsCheck = false)]
        public ActionResult ResetPwd()
        {
            var phone = Request.QueryString["id"] ?? "";
            if (phone != "")
            {
                ViewBag.Phone = DESEncrypt.Decrypt(phone);
            }
            return View();
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = false)]
        public JsonResult ResetPwdAdd()
        {
            object result = new { success = 0, msg = "重置密码失败,请联系管理员" };

            var phone = Request["u_phone"] ?? "";
            var pwd = Request["xpass"] ?? "";
            pwd = DESEncrypt.Encrypt(pwd);
            var flag = bll.UpdatePwdByPhone(phone, pwd);
            if (flag)
            {
                result = new { success = 1, msg = "重置密码成功,请重新登录" };

                AddLocLog.AddLog(1, 3, RequestHelper.GetClientIp(), "开发者找回密码",
                    phone + "于" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "找回密码。");
            }

            return Json(result);
        }


        #region 找回密码
        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [LoginCheckFilter(IsCheck = false)]
        public JsonResult ForgetPassword(FormCollection form)
        {
            var phone = form["u_phone"] ?? "";
            if (phone.Trim().Length <= 0)
            {
                return Json(new { success = false, message = "请填写您的注册手机号" });
            }
            var lst = bll.GetModelList(string.Format("u_phone='{0}'", phone.Trim()));
            if (lst.Count <= 0)
            {
                return Json(new { success = false, message = "请填写您的手机号还没注册" });
            }
            var model = lst.FirstOrDefault(x => x.u_phone == phone);
            if (model == null)
            {
                return Json(new { success = false, message = "请填写您的手机号还没注册" });
            }

            var newPassword = RandomHelper.GetRandomizer(8, true, false, true, false);
            model.u_password = DESEncrypt.Encrypt(newPassword);
            var updateSuccess = bll.UpdatePwd(model.u_email, model.u_password);
            if (updateSuccess)
            {
                AddLocLog.AddLog(1, 3, RequestHelper.GetClientIp(), "重置密码", "用户于" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "申请找回密码,发送短信至" + phone + ".");
            }
            else
            {
                return Json(new { success = false, message = "服务器忙,请稍候再试" });
            }
            var chuangLanRequest = new ChuangLanRequest
            {
                //必填
                Account = ConfigReader.GetSettingValueByKey("CHUANGLAN.ACCOUNT"),
                //必填
                Password = ConfigReader.GetSettingValueByKey("CHUANGLAN.PASSWORD"),
                //必填
                Content = string.Format(ConfigReader.GetSettingValueByKey("CHUANGLAN.CONTENT.FORGETPASSWORD"), newPassword),
                //必填(多个手机号以英文逗号隔开)
                Mobile = phone
            };
            IMessageSender messageSender = new ChuangLanMessageSender(chuangLanRequest);
            var success = false;
            try
            {
                success = messageSender.Send();
            }
            catch (Exception ex)
            {
                AddLocLog.AddLog(1, 4, Request.UserHostAddress, "短信接口错误", ex.ToString());
            }
            if (success)
            {
                AddLocLog.AddLog(1, 4, RequestHelper.GetClientIp(), "重置密码", "用户于" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "申请找回密码,发送短信至" + phone + ".");
            }
            return Json(new { success = true, message = "系统已为您重新分配密码并以短信形式发送,请注意查收." });
        }
        #endregion
        #endregion

        #region 支付密码与银行卡管理

        /// <summary>
        /// 支付密码管理
        /// </summary>
        /// <returns></returns>
        public ActionResult PayPwd()
        {
            #region 获取用户实名认证状态信息

            JMP.BLL.jmp_user sm_bll = new JMP.BLL.jmp_user();
            JMP.MDL.jmp_user sm_model = new JMP.MDL.jmp_user();

            int u_ids = UserInfo.Uid;

            //查询登录信息
            sm_model = sm_bll.GetModel(u_ids);

            ViewBag.auditstate = sm_model.u_auditstate;
            ViewBag.linkEmail = sm_model.u_email;

            #endregion

            JMP.MDL.jmp_user j_user = bll.GetModel(UserInfo.UserNo);
            ViewBag.jmpUser = j_user;
            ViewBag.auditstate = j_user.u_auditstate;
            ViewBag.linkEmail = j_user.u_email;

            return View();
        }

        /// <summary>
        /// 设置支付密码
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult UpdateUserPayPwd()
        {
            object result = new { success = 0, msg = "设置失败！" };

            int uid = string.IsNullOrEmpty(Request["uid"]) ? 0 : int.Parse(Request["uid"]);
            string pwd = !string.IsNullOrEmpty(Request["pwd"]) ? Request["pwd"] : "";
            string oldpwd = !string.IsNullOrEmpty(Request["oldpwd"]) ? Request["oldpwd"] : "";

            JMP.MDL.jmp_user j_user = bll.GetModel(uid);

            if (!string.IsNullOrEmpty(pwd))
            {
                //判断是否验证原支付密码
                if (!string.IsNullOrEmpty(oldpwd))
                {
                    string temp = DESEncrypt.Encrypt(oldpwd);
                    if (temp == j_user.u_paypwd)
                    {

                        string u_paypwd = DESEncrypt.Encrypt(pwd);
                        bool flag = bll.UpdateUserPayPwd(uid, u_paypwd);
                        if (flag)
                        {
                            string log = "原支付密码：" + temp + "改为新支付密码：" + u_paypwd;
                            Logger.OperateLog("设置支付密码", log);

                            result = new { success = 1, msg = "设置成功！" };
                        }
                    }
                    else
                    {
                        result = new { success = 2, msg = "原支付密码输入错误！" };
                    }

                }
                else
                {
                    string u_paypwd = DESEncrypt.Encrypt(pwd);
                    bool flag = bll.UpdateUserPayPwd(uid, u_paypwd);
                    if (flag)
                    {
                        string log = "初次设置支付密码：" + u_paypwd;
                        Logger.OperateLog("设置支付密码", log);

                        result = new { success = 1, msg = "设置成功！" };
                    }
                }
            }

            return Json(result);
        }

        //提款银行卡
        JMP.MDL.jmp_userbank userBankMode = new JMP.MDL.jmp_userbank();
        JMP.BLL.jmp_userbank userBankBll = new JMP.BLL.jmp_userbank();
        List<JMP.MDL.jmp_userbank> userBankList = new List<JMP.MDL.jmp_userbank>();

        /// <summary>
        /// 银行卡管理
        /// </summary>
        /// <returns></returns>
        public ActionResult BankCardList()
        {
            #region 获取用户实名认证状态信息

            JMP.BLL.jmp_user sm_bll = new JMP.BLL.jmp_user();
            JMP.MDL.jmp_user sm_model = new JMP.MDL.jmp_user();

            int u_ids = UserInfo.Uid;

            //查询登录信息
            sm_model = sm_bll.GetModel(u_ids);

            ViewBag.auditstate = sm_model.u_auditstate;
            ViewBag.linkEmail = sm_model.u_email;

            #endregion

            int id = UserInfo.UserId;
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            //搜索条件
            string searchType = string.IsNullOrEmpty(Request["searchType"]) ? "0" : Request["searchType"];
            //搜索信息
            string banknumber = string.IsNullOrEmpty(Request["banknumber"]) ? "" : Request["banknumber"];
            //付款标识
            string flag = string.IsNullOrEmpty(Request["flag"]) ? "" : Request["flag"];
            //审核状态
            string state = string.IsNullOrEmpty(Request["state"]) ? "" : Request["state"];
            //冻结状态
            string freeze = string.IsNullOrEmpty(Request["freeze"]) ? "" : Request["freeze"];
            //查询开发者绑定的所有银行卡信息
            userBankList = userBankBll.SelectUserBankList(id, searchType, banknumber, flag, state, freeze, pageIndexs, PageSize, out pageCount);

            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = userBankList;
            ViewBag.searchType = searchType;
            ViewBag.banknumber = banknumber;
            ViewBag.flag = flag;
            ViewBag.state = state;
            ViewBag.freeze = freeze;

            return View();
        }

        /// <summary>
        /// 录入银行卡信息
        /// </summary>
        /// <returns></returns>
        public ActionResult BankCardAdd()
        {
            #region 获取用户实名认证状态信息
            int Bankcount = int.Parse(ConfigurationManager.AppSettings["Bankcount"].ToString());
            ViewBag.Bankcount = Bankcount;
            JMP.BLL.jmp_user sm_bll = new JMP.BLL.jmp_user();
            JMP.MDL.jmp_user sm_model = new JMP.MDL.jmp_user();

            int u_ids = UserInfo.Uid;

            //查询登录信息
            sm_model = sm_bll.GetModel(u_ids);

            userBankList = userBankBll.GetModelList("u_userid =" + UserInfo.UserId + "and u_state!=-1 and u_freeze=0");
            ViewBag.userBankCount = userBankList.Count;
            ViewBag.auditstate = sm_model.u_auditstate;
            ViewBag.linkEmail = sm_model.u_email;

            #endregion

            return View();
        }

        /// <summary>
        /// 录入or编辑银行卡信息方法
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult AddBankCard(JMP.MDL.jmp_userbank userBank)
        {
            object result = new { success = 0, msg = "操作失败！" };

            if (userBank.u_id > 0)
            {
                #region 编辑

                JMP.MDL.jmp_userbank ubk = new JMP.MDL.jmp_userbank();
                //查询
                ubk = userBankBll.GetModel(userBank.u_id);

                if (ubk.u_state == 0)
                {
                    var modclone = ubk.Clone();

                    ubk.u_banknumber = userBank.u_banknumber;
                    ubk.u_bankname = userBank.u_bankname;
                    ubk.u_openbankname = userBank.u_openbankname;
                    ubk.u_name = userBank.u_name;
                    ubk.u_area = userBank.u_area;
                    ubk.u_province = userBank.u_province;
                    ubk.u_flag = userBank.u_flag;

                    if (userBankBll.Update(ubk))
                    {
                        //日志
                        Logger.ModifyLog("修改银行卡信息", modclone, userBank);
                        result = new { success = 1, msg = "操作成功" };
                    }
                    else
                    {
                        result = new { success = 0, msg = "操作失败" };
                    }
                }
                else
                {
                    result = new { success = 0, msg = "已审核通过，不能再修改！" };
                }

                #endregion

            }
            else
            {
                #region 添加

                userBank.u_userid = UserInfo.UserId;
                userBank.u_state = 0;
                userBank.u_remarks = "";
                userBank.u_auditor = "";
                userBank.u_date = null;
                userBank.u_freeze = 0;
                userBankList = userBankBll.GetModelList("u_userid =" + UserInfo.UserId + " and u_state!=-1 and u_freeze=0");
                int Bankcount = int.Parse(ConfigurationManager.AppSettings["Bankcount"].ToString());
                if (userBankList.Count < Bankcount)
                {
                    int num = userBankBll.Add(userBank);

                    if (num > 0)
                    {
                        Logger.CreateLog("新增银行卡", userBank);
                        result = new { success = 1, msg = "操作成功！" };
                    }
                }
                else
                {
                    result = new { success = 0, msg = "最多添加" + Bankcount + "张银行卡信息！" };
                }
                #endregion
            }

            return Json(result);
        }


        /// <summary>
        /// 修改银行卡信息页面
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult BankCardUpdate()
        {
            #region 获取用户实名认证状态信息

            JMP.BLL.jmp_user sm_bll = new JMP.BLL.jmp_user();
            JMP.MDL.jmp_user sm_model = new JMP.MDL.jmp_user();

            int u_ids = UserInfo.Uid;

            //查询登录信息
            sm_model = sm_bll.GetModel(u_ids);

            ViewBag.auditstate = sm_model.u_auditstate;
            ViewBag.linkEmail = sm_model.u_email;

            #endregion

            //id
            int bkid = string.IsNullOrEmpty(Request["bkid"]) ? 0 : int.Parse(Request["bkid"]);
            //查询信息
            userBankMode = userBankBll.GetModel(bkid);
            ViewBag.userBankMode = userBankMode;

            return View();
        }

        /// <summary>
        /// 验证银行卡账号是否存在
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult CheckBankCardNo(string u_banknumber, string uid)
        {
            bool flag = userBankBll.ExistsBankNo(u_banknumber, uid);
            return Json(new { success = flag ? 1 : 0, msg = flag ? "已存在该银行卡账号！" : "不存在银行卡账号！" });
        }


        #endregion
    }

    /// <summary>
    /// 手机验证码消息对象
    /// </summary>
    public class MobileVerifyMessage
    {
        public MobileVerifyMessage()
        {
            Message = "";
        }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
