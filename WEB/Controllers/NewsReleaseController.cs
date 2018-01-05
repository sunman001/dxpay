/************官网后台管理************/
//描述：新闻管理
//功能：新闻管理
//开发者：孙曼
//开发时间: 2017.01.09
/************官网后台管理************/

using JMP.BLL;
using JMP.TOOL;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TOOL.Extensions;
using WEB.Extensions;
using WEB.Util.Logger;

namespace WEB.Controllers
{
    public class NewsReleaseController : Controller
    {
        JMP.BLL.jmp_terminal bll_ter = new JMP.BLL.jmp_terminal();
        JMP.BLL.jmp_order bll_order = new JMP.BLL.jmp_order();
        JMP.BLL.jmp_user_report bll_report = new JMP.BLL.jmp_user_report();
        JMP.BLL.jmp_limit bll_limit = new JMP.BLL.jmp_limit();
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;


        #region  新闻管理
        /// <summary>
        ///新闻管理
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false), VisitRecord(IsRecord = true)]
        public ActionResult NewsReleaseList()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            string category = string.IsNullOrEmpty(Request["category"]) ? "" : Request["category"];//审核状态
            int type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);//查询条件选择
            //string stime = string.IsNullOrEmpty(Request["r_begin"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["r_begin"];
            //string etime = string.IsNullOrEmpty(Request["r_end"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["r_end"];
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            List<JMP.MDL.newsrelease> list = new List<JMP.MDL.newsrelease>();
            JMP.BLL.newsrelease bll = new JMP.BLL.newsrelease();
            list = bll.SelectList(category, sea_name, type, searchDesc,  pageIndexs, PageSize, out pageCount);
            ViewBag.searchDesc = searchDesc;
            ViewBag.type = type;
            ViewBag.sea_name = sea_name;
          
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            ViewBag.category = category;
            ViewBag.locUrl = GetVoidHtml();
            return View();
        }

        public string GetVoidHtml()
        {
            string locUrl = "";
            string u_id = UserInfo.UserId.ToString();
            int r_id = int.Parse(UserInfo.UserRoleId.ToString());
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/NewsRelease/InsertUpdateNewsRelease", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加管理员
            if (getlocuserAdd)
            {
                locUrl += "<li onclick=\"AddAPPlog()\"><i class='fa fa-plus'></i>新闻发布</li>";
            }
            return locUrl;
        }

        /// <summary>
        /// 添加或修改新闻管理
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult InsertUpdateNewsRelease(JMP.MDL.newsrelease model)
        {
            object retJson = new { success = 0, msg = "操作失败" };
            JMP.BLL.newsrelease bll = new JMP.BLL.newsrelease();
            //string xgzfc = "";
            if (model.n_id > 0)
            {
                // 修改新闻管理
                JMP.MDL.newsrelease modComplaint = new JMP.MDL.newsrelease();
                modComplaint = bll.GetModel(model.n_id);
                var modComplaintClone = modComplaint.Clone();
                modComplaint.n_title = model.n_title;
                modComplaint.n_info = model.n_info;
                modComplaint.n_picture = model.n_picture;
                modComplaint.n_category = model.n_category;
                modComplaint.keywords = model.keywords;
                modComplaint.description = model.description;
                //model.n_count = modComplaint.n_count;
                //model.n_time = modComplaint.n_time;
                //model.n_user = modComplaint.n_user;

                if (bll.Update(modComplaint))
                {
                   
                    Logger.ModifyLog("修改新闻信息", modComplaintClone, model);
                    retJson = new { success = 1, msg = "修改成功" };
                }
                else
                {
                    retJson = new { success = 0, msg = "修改失败" };
                }

            }
            else
            {
                model.n_time = DateTime.Now;
                model.n_user = UserInfo.UserName;
                model.n_count = 0;
                int cg = bll.Add(model);
                if (cg > 0)
                {
                  
                    Logger.CreateLog("添加新闻", model);
                    retJson = new { success = 1, msg = "添加成功" };
                }
                else
                {
                    retJson = new { success = 1, msg = "添加失败" };
                }

            }
            return Json(retJson);
        }

        /// <summary>
        /// 添加/修改新闻
        /// </summary>
        /// <returns></returns>
        public ActionResult NewsReleaseAdd()
        {
            int c_id = string.IsNullOrEmpty(Request["c_id"]) ? 0 : Int32.Parse(Request["c_id"]);
            JMP.BLL.newsrelease bll = new JMP.BLL.newsrelease();
            JMP.MDL.newsrelease model = new JMP.MDL.newsrelease();
            if (c_id > 0)
            {
                model = bll.SelectId(c_id);
            }
            ViewBag.model = model == null ? new JMP.MDL.newsrelease() : model;
            ViewBag.UploadUrl = ConfigurationManager.AppSettings["imgurl"];
            return View();

        }
        #endregion
        #region 官网投诉管理
        /// <summary>
        ///应用投诉管理
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false), VisitRecord(IsRecord = true)]
        public ActionResult GwComplaintList()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            string auditstate = string.IsNullOrEmpty(Request["auditstate"]) ? "" : Request["auditstate"];//审核状态
            int type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);//查询条件选择
            string stime = string.IsNullOrEmpty(Request["r_begin"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["r_begin"];
            string etime = string.IsNullOrEmpty(Request["r_end"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["r_end"];
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            List<JMP.MDL.jmp_gwcomplaint> list = new List<JMP.MDL.jmp_gwcomplaint>();
            JMP.BLL.jmp_gwcomplaint bll = new JMP.BLL.jmp_gwcomplaint();
            list = bll.SelectList(auditstate, sea_name, type, searchDesc, stime, etime, pageIndexs, PageSize, out pageCount);
            ViewBag.searchDesc = searchDesc;
            ViewBag.type = type;
            ViewBag.sea_name = sea_name;
            ViewBag.stime = stime;
            ViewBag.etime = etime;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            ViewBag.auditstate = auditstate;
            ViewBag.locUrl = GetVoidHtmlGw();
            return View();
        }

        public string GetVoidHtmlGw()
        {
            string locUrl = "";
            string u_id = UserInfo.UserId.ToString();
            int r_id = int.Parse(UserInfo.UserRoleId.ToString());
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/NewsRelease/InsertUpdateComplaint", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加管理员
          
            var bulkAssignToMerchant = bll_limit.GetLocUserLimitVoids("/NewsRelease/ComplaintCL", u_id, r_id);
            if (bulkAssignToMerchant)
            {
                locUrl += "<li onclick=\"bulkassign()\"><i class='fa fa-check-square-o'></i>处理</li>";
            }
            return locUrl;
        }

        /// <summary>
        /// 处理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ComplaintCL(string rid)
        {
            ViewBag.uids = rid;
            return View();
        }

        /// <summary>
        /// 处理投诉
        /// </summary>
        /// <returns></returns>

        public JsonResult ComplaintCLJG()
        {
            object retJson = new { success = 0, msg = "处理失败" };
            var rid = Request["rid"] ?? "";
            var r_remark = Request["remark"] ?? "";
            var r_auditor = UserInfo.UserName;
            if (rid.Length <= 0)
            {
                retJson = new { success = 0, msg = "参数错误" };
                return Json(retJson);
            }
            if (rid.CompareTo("On") > 0)
            {
                rid = rid.Substring(3);
            }
            var bll = new jmp_gwcomplaint();
            var success = bll.ComplaintLC(rid, r_remark, r_auditor);
            if (success)
            {
             
                Logger.OperateLog("处理投诉", "官网投诉id为:" + rid + "，处理结果:" + r_remark);
                retJson = new { success = 1, msg = "处理成功" };
            }
            else
            {
                retJson = new { success = 0, msg = "处理失败" };
            }

            return Json(retJson);

        }
        #endregion

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public JsonResult UploadImg()
        {
            //回传图片路径
            string returnurl = string.Empty;
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
                        purl = purl.StartsWith("B") ? purl.TrimStart('B') : purl.StartsWith("A") ? purl.TrimStart('A') : purl;
                        if (!string.IsNullOrEmpty(purl))
                        {
                            PubImageUp.DeleteImage(purl);
                        }
                    }
                    catch (Exception e)
                    {
                    }
                    //获取配置文件里的上传路径
                    string uploadurl = ConfigurationManager.AppSettings["newuploadurl"];
                    result = PubImageUp.UpnewImages("certificatefile", uploadurl);
                    if (result.Length == 2)
                    {
                        if (result[0] != "99")
                            returnurl = result[1];
                        else
                            msg = result[1];
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
                    mess = msg,
                    success = tag
                }, "text/html", JsonRequestBehavior.AllowGet);
        }

    }
}
