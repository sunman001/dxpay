
using DxPay.Agent.Util.Logger;
using DxPay.Factory;
using DxPay.Services;
using JMP.TOOL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DxPay.Agent.Controllers
{
    public class UserController : Controller
    {
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;
        private readonly ICoAgentService _coAgentService;
        public UserController()
        {
            _coAgentService = ServiceFactory.CoAgentService;
        }

        // GET: User
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public ActionResult ModifyInfo()
        {
            
            var CoBusinessPersonnel = _coAgentService.FindById (UserInfo.UserId);
            ViewBag.jmpUser = CoBusinessPersonnel;
            return View();
        }
        /// <summary>
        /// 保存修改信息
        /// </summary>
        /// <param name="develop"></param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public JsonResult UpdateUserInfo(JMP.MDL.CoBusinessPersonnel develop)
        {
            object result = new { success = 0, msg = "修改失败！" };

            int u_id = string.IsNullOrEmpty(Request["u_id"]) ? 0 : int.Parse(Request["u_id"]);
            string o_pwd = !string.IsNullOrEmpty(Request["upass"]) ? Request["upass"] : "";
            string n_pwd = !string.IsNullOrEmpty(Request["xpass"]) ? Request["xpass"] : "";

            var j_user = _coAgentService.FindById(UserInfo.UserId);
            //判断是否修改了密码
            if (!string.IsNullOrEmpty(o_pwd))
            {
                string temp = DESEncrypt.Encrypt(o_pwd);
                if (temp == j_user.Password)
                {
                    string u_password = DESEncrypt.Encrypt(n_pwd);
                    bool flag = _coAgentService.UpdatePwd(j_user.Id, u_password);
                    result = new { success = 1, msg = "修改成功！" };
                }
                else
                {
                    result = new { success = 2, msg = "原密码输入错误！" };
                }
            }

            return Json(result);

        }

        /// <summary>
        /// 个人信息
        /// </summary>
        /// <returns></returns>
        public ActionResult PersonalInfo()
        {
            var CoBusinessPersonnel = _coAgentService.FindById(UserInfo.UserId);
            ViewBag.coagent = CoBusinessPersonnel;
            ViewBag.UploadUrl = ConfigurationManager.AppSettings["imgurl"];
            return View();
        }

    }
}