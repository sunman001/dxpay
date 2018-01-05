using System;
using System.Collections.Generic;
using System.Web.Mvc;
using JMP.BLL;
using System.Configuration;
using JMP.TOOL;
using System.Data;

namespace WEBGW.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            string UserRegister = ConfigurationManager.AppSettings["UserRegister"];
            string Userpassword = ConfigurationManager.AppSettings["Userpassword"];
            ViewBag.UserRegister = UserRegister;
            ViewBag.Userpassword = Userpassword;
            List<JMP.MDL.newsrelease> list = new List<JMP.MDL.newsrelease>();
            JMP.BLL.newsrelease bll = new JMP.BLL.newsrelease();
            list = bll.SelectListxw();
            ViewBag.list = list;
            return View();
        }
        /// <summary>
        /// 新闻中心
        /// </summary>
        /// <returns></returns>
        public ActionResult News()
        {
            string UserRegister = ConfigurationManager.AppSettings["UserRegister"];
            string Userpassword = ConfigurationManager.AppSettings["Userpassword"];
            ViewBag.UserRegister = UserRegister;
            ViewBag.Userpassword = Userpassword;

            #region 行业新闻
            int hypageCount = 0;
            int hycount = string.IsNullOrEmpty(Request["count"]) ? 0 : Int32.Parse(Request["count"]);//标示
            int hypageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int hyPageSize = 5;//每页显示数量
            List<JMP.MDL.newsrelease> hylist = new List<JMP.MDL.newsrelease>();
            JMP.BLL.newsrelease hybll = new JMP.BLL.newsrelease();
            hylist = hybll.GetListsByType(2, hypageIndexs, hyPageSize, out hypageCount);
            ViewBag.hypageIndexs = hypageIndexs;
            ViewBag.hyPageSize = hyPageSize;
            ViewBag.hypageCount = hypageCount;
            ViewBag.hylist = hylist;
            ViewBag.hycount = hycount;
            #endregion
            ViewBag.UploadUrl = ConfigurationManager.AppSettings["imgurl"];
            return View();
        }

        public ActionResult Zixun()
        {
            string UserRegister = ConfigurationManager.AppSettings["UserRegister"];
            string Userpassword = ConfigurationManager.AppSettings["Userpassword"];
            ViewBag.UserRegister = UserRegister;
            ViewBag.Userpassword = Userpassword;
            #region 公司新闻
            int pageCount = 0;
            int count = string.IsNullOrEmpty(Request["count"]) ? 0 : Int32.Parse(Request["count"]);//标示
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = 5;//每页显示数量
            List<JMP.MDL.newsrelease> gslist = new List<JMP.MDL.newsrelease>();
            JMP.BLL.newsrelease bll = new JMP.BLL.newsrelease();
            gslist = bll.GetListsByType(1, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = gslist;
            ViewBag.count = count;
            #endregion

            ViewBag.UploadUrl = ConfigurationManager.AppSettings["imgurl"];
            return View();
        }


        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult GetValidateCode(int height)
        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.GetRandomCode(4);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateImageTwo(code, height);
            return File(bytes, @"image/jpeg");
        }


        public JsonResult UserLogin(string logName, string logPwd, string valCode)
        {

            //获取开发者平台地址

            object result = new { msg = "验证码错误！", success = "0" };

            if (valCode == Session["ValidateCode"].ToString())
            {
                string Pwd = DESEncrypt.Encrypt(logPwd);

                string Userurl = System.Configuration.ConfigurationManager.AppSettings["Userurl"] + "?qs=" + DESEncrypt.Encrypt(logName + ";" + Pwd + ";0;" + System.DateTime.Now.ToString());
                JMP.BLL.jmp_limit bll_limit = new JMP.BLL.jmp_limit();
                JMP.BLL.jmp_user bll = new JMP.BLL.jmp_user();
                JMP.MDL.jmp_user model = bll.GetModel(logName);

                if (model != null && model.u_state == 1)
                {

                    if ((model.u_email == logName || model.u_phone == logName) && model.u_password == Pwd)
                    {
                        UserInfo.UserId = model.u_id;
                        UserInfo.UserName = model.u_realname;
                        UserInfo.UserNo = model.u_email;
                        UserInfo.UserRoleId = model.u_role_id;
                        DataTable dtLimit = bll_limit.GetAppUserLimitSession(model.u_id, model.u_role_id);
                        if (dtLimit.Rows.Count > 0)
                        {
                            result = new { msg = "登录成功！", success = "1", url = Userurl };
                        }
                        else
                        {
                            result = new { msg = "权限不足！", success = "2" };
                        }
                    }
                    else
                    {
                        result = new { msg = "用户名或密码错误！", success = "2" };
                    }

                }
                else
                {
                    if (model == null)
                    {
                        result = new { msg = "用户名或密码错误！", success = "2" };
                    }
                    else if (model.u_state != 1)
                    {
                        result = new { msg = "该账号已冻结！", success = "2" };
                    }
                }


                //result = new { msg = "验证码成功！", status = 1, url = Userurl };

            }

            return Json(result);
        }

        /// <summary>
        /// 关于我们
        /// </summary>
        /// <returns></returns>

        public ActionResult About()
        {
            string UserRegister = ConfigurationManager.AppSettings["UserRegister"];
            string Userpassword = ConfigurationManager.AppSettings["Userpassword"];
            ViewBag.UserRegister = UserRegister;
            ViewBag.Userpassword = Userpassword;
            return View();
        }


        /// <summary>
        /// 手机端首页
        /// </summary>
        /// <returns></returns>
        public ActionResult New()
        {
            string UserRegister = ConfigurationManager.AppSettings["UserRegister"];
            string Userpassword = ConfigurationManager.AppSettings["Userpassword"];
            ViewBag.UserRegister = UserRegister;
            ViewBag.Userpassword = Userpassword;
            int pageCount = 0;
            int count = string.IsNullOrEmpty(Request["count"]) ? 0 : Int32.Parse(Request["count"]);//标示
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = 5;//每页显示数量
            List<JMP.MDL.newsrelease> list = new List<JMP.MDL.newsrelease>();
            JMP.BLL.newsrelease bll = new JMP.BLL.newsrelease();
            list = bll.GetLists(pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            ViewBag.count = count;
            ViewBag.UploadUrl = ConfigurationManager.AppSettings["imgurl"];
            return View();
        }

        /// <summary>
        /// 产品咨询
        /// </summary>
        /// <returns></returns>
        public ActionResult product()
        {
            string UserRegister = ConfigurationManager.AppSettings["UserRegister"];
            string Userpassword = ConfigurationManager.AppSettings["Userpassword"];
            ViewBag.UserRegister = UserRegister;
            ViewBag.Userpassword = Userpassword;
            return View();
        }

        public ActionResult loading()
        {
            return View();
        }


        /// <summary>
        /// 新闻详细
        /// </summary>
        /// <param name="n_id"></param>
        /// <returns></returns>
        public ActionResult NewDetil(int id = 0)
        {
            string UserRegister = ConfigurationManager.AppSettings["UserRegister"];
            string Userpassword = ConfigurationManager.AppSettings["Userpassword"];
            ViewBag.UserRegister = UserRegister;
            ViewBag.Userpassword = Userpassword;
            JMP.MDL.newsrelease model = new JMP.MDL.newsrelease();
            JMP.BLL.newsrelease bll = new JMP.BLL.newsrelease();
            List<JMP.MDL.newsrelease> list = new List<JMP.MDL.newsrelease>();
            if (id > 0)
            {
                model = bll.SelectId(id);
                if (model != null)
                {
                    bll.UpdateCount(id);
                    list = bll.SelectUpDw(id, model.n_category);
                }
            }
            ViewBag.id = id;
            ViewBag.list = list;
            ViewBag.UploadUrl = ConfigurationManager.AppSettings["imgurl"];
            ViewBag.model = model == null ? new JMP.MDL.newsrelease() : model;
            return View();
        }

        /// <summary>
        /// 添加合作信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult addCooperationApplication(JMP.MDL.CoCooperationApplication model)
        {
            object retJson = new { success = 0, msg = "操作失败" };
            JMP.BLL.CoCooperationApplication bll = new JMP.BLL.CoCooperationApplication();
            model.EmailAddress = string.IsNullOrEmpty(model.EmailAddress) ? "" : model.EmailAddress;
            model.CreatedOn = DateTime.Now;
            model.State = 0;
            try
            {
                int cg = bll.Add(model);
                if (cg > 0)
                {
                    retJson = new { success = 1, msg = "发送成功" };
                }
                else
                {
                    retJson = new { success = 0, msg = "发送失败" };
                }

            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString() + "报错位置：" + e.StackTrace.ToString();//报错信息
                JMP.TOOL.AddLocLog.AddLog(1, 4, "", "官网，商务咨询添加报错", "报错信息：" + bcxx);//写入报错日志
                retJson = new { success = 0, msg = "操作失败" };
            }


            return Json(retJson);
        }

        /// <summary>
        /// 手机端联系页面
        /// </summary>
        /// <returns></returns>
        public ActionResult contact()
        {
            string UserRegister = ConfigurationManager.AppSettings["UserRegister"];
            string Userpassword = ConfigurationManager.AppSettings["Userpassword"];
            ViewBag.UserRegister = UserRegister;
            ViewBag.Userpassword = Userpassword;
            return View();
        }

        public ActionResult consultation()
        {
            string UserRegister = ConfigurationManager.AppSettings["UserRegister"];
            string Userpassword = ConfigurationManager.AppSettings["Userpassword"];
            ViewBag.UserRegister = UserRegister;
            ViewBag.Userpassword = Userpassword;
            return View();
        }


    }
}