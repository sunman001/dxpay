using JMP.MDL;
using JMP.TOOL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using TOOL;
using WEB.Util.Logger;

namespace WEB.Controllers
{
    public class HelpController : Controller
    {
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;
        JMP.BLL.jmp_limit bll_limit = new JMP.BLL.jmp_limit();
        jmp_Help_Classification model = new jmp_Help_Classification();
        JMP.BLL.jmp_Help_Classification bll = new JMP.BLL.jmp_Help_Classification();
        //
        // GET: /Help/
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ClassificationList()
        {
            List<JMP.MDL.jmp_Help_Classification> list = new List<JMP.MDL.jmp_Help_Classification>();
            JMP.BLL.jmp_Help_Classification bll = new JMP.BLL.jmp_Help_Classification();
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            string ClassName = string.IsNullOrEmpty(Request["ClassName"]) ? "" : Request["ClassName"];
            int Type = string.IsNullOrEmpty(Request["Type"]) ? -1 : int.Parse(Request["Type"]);
            int sType= string.IsNullOrEmpty(Request["sType"]) ? -1 : int.Parse(Request["sType"]);
            int ParentID = string.IsNullOrEmpty(Request["ParentID"]) ?0 : int.Parse(Request["ParentID"]);
            DataTable tablelist = bll.GetList(" 1=1 and ParentID=0 and State=0 ").Tables[0];
            List<JMP.MDL.jmp_Help_Classification> parentlist = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_Help_Classification>(tablelist);
            ViewBag.parentlist = parentlist;
            list = bll.SelectList(sType,ParentID, ClassName, Type, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            ViewBag.ClassName = ClassName;
            ViewBag.PrentID = ParentID;
            ViewBag.Type = Type;
            ViewBag.sType = sType;
            string locUrl = "";
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/Help/AddClassification", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加
            if (getlocuserAdd)
            {
                locUrl += "<li onclick=\"AddSdl()\"><i class='fa fa-plus'></i>添加类别</li>";
            }
            bool getUidT = bll_limit.GetLocUserLimitVoids("/Help/UpdateState", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//本地管理员一键启用
            if (getUidT)
            {
                locUrl += "<li onclick=\"javascript:Updatestate(0)\"><i class='fa fa-check-square-o'></i>一键启用</li>";
            }
            bool getUidF = bll_limit.GetLocUserLimitVoids("/Help/UpdateState", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//本地管理员一键禁用
            if (getUidF)
            {
                locUrl += "<li onclick=\"javascript:Updatestate(1);\"><i class='fa fa-check-square-o'></i>一键禁用</li>";
            }
            ViewBag.locUrl = locUrl;

            return View();
        }
        /// <summary>
        /// 添加修改页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddClassification()
        {
            int Id= string.IsNullOrEmpty(Request["Id"]) ? 0 : Int32.Parse(Request["Id"]);
            if(Id>0)
            {
                model = bll.GetModel(Id);
              
            }
            ViewBag.model = model;
            DataTable tablelist = bll.GetList(" 1=1 and ParentID=0 and State=0  ").Tables[0];
            List<JMP.MDL.jmp_Help_Classification> list = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_Help_Classification>(tablelist);
            ViewBag.list = list;
            return View();
        }
        /// <summary>
        /// 添加修改方法
        /// </summary>
        /// <returns></returns>
        public JsonResult AddorEidt(JMP.MDL.jmp_Help_Classification model)
        {
            object retJson = new { success = 0, msg = "操作失败" };
            if(model.ID<=0)
            {
                model.State = 0;
                model.CreateByID = UserInfo.Uid;
                model.CreateByName = UserInfo.UserName;
                model.CreateOn = DateTime.Now;
                int cg = bll.Add(model);
                if (cg > 0)
                {
                    Logger.CreateLog("添加分类", model);

                    retJson = new { success = 1, msg = "添加成功" };
                }
                else
                {
                    retJson = new { success = 0, msg = "添加失败" };
                }
            }
            else
            {
                JMP.MDL.jmp_Help_Classification mo = new jmp_Help_Classification();
                mo = bll.GetModel(model.ID);
                mo.ParentID = model.ParentID;
                mo.Sort = model.Sort;
                mo.ClassName = model.ClassName;
                mo.Description = model.Description;
                mo.Icon = model.Icon;
                mo.Type = model.Type;
                if (bll.Update(mo))
                {
                    Logger.ModifyLog("修改分类", mo, model);

                    retJson = new { success = 1, msg = "修改成功" };
                }
                else
                {
                    retJson = new { success = 0, msg = "修改失败" };
                }

            }
            return Json(retJson);
        }
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdateState()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            int state = string.IsNullOrEmpty(Request["state"]) ? 0 : Int32.Parse(Request["state"].ToString());
            string str = Request["ids"];
            string xgzfc = "";//组装说明
            string tsmsg = "";//提示
            JMP.BLL.jmp_Help_Classification bll = new JMP.BLL.jmp_Help_Classification();
            if (str.CompareTo("On") > 0)
            {
                str = str.Substring(3);
            }
            if (bll.UpdateState(str, state))
            {
                if (state == 1)
                {
                    xgzfc = "一键禁用ID为：" + str;
                    tsmsg = "禁用成功";
                }
                else
                {
                    tsmsg = "启用成功";
                    xgzfc = "一键启用ID为：" + str;
                }

                Logger.OperateLog("应用一键禁用或启用", xgzfc);

                retJson = new { success = 1, msg = tsmsg };
            }
            else
            {
                if (state == 1)
                {
                    tsmsg = "禁用失败";
                }
                else
                {
                    tsmsg = "启用失败";
                }
                retJson = new { success = 0, msg = tsmsg };
            }
            return Json(retJson);
        }

        public ActionResult ContentList()
        {
            List<JMP.MDL.jmp_Help_Content> list = new List<JMP.MDL.jmp_Help_Content>();
            JMP.BLL.jmp_Help_Content bll = new JMP.BLL.jmp_Help_Content();
            JMP.BLL.jmp_Help_Classification classbll = new JMP.BLL.jmp_Help_Classification();
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            string Type = string.IsNullOrEmpty(Request["Type"]) ? "" : Request["Type"];
            string sea_name= string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];
            int State = string.IsNullOrEmpty(Request["State"]) ? -1 : int.Parse(Request["State"]);
            int PrentID = string.IsNullOrEmpty(Request["PrentID"]) ? 0 : int.Parse(Request["PrentID"]);
            int ClassId = string.IsNullOrEmpty(Request["ClassId"]) ? 0 : int.Parse(Request["ClassId"]);
            int sType = string.IsNullOrEmpty(Request["sType"]) ? -1 : int.Parse(Request["sType"]);
            DataTable tablelist = classbll.GetList(" 1=1 and ParentID=0 and State=0  ").Tables[0];
            List<JMP.MDL.jmp_Help_Classification> parentlist = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_Help_Classification>(tablelist);
            ViewBag.parentlist = parentlist;
            list = bll.SelectList(sType,Type, sea_name, State, PrentID, ClassId, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            ViewBag.sea_name = sea_name;
            ViewBag.PrentID = PrentID;
            ViewBag.ClassId = ClassId;
            ViewBag.type = Type;
            ViewBag.State = State;
            ViewBag.sType = sType;
            string locUrl = "";
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/Help/AddContent", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加
            if (getlocuserAdd)
            {
                locUrl += "<li onclick=\"AddContent()\"><i class='fa fa-plus'></i>添加内容</li>";
            }
            bool getUidT = bll_limit.GetLocUserLimitVoids("/Help/UpdateStateContent", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//本地管理员一键启用
            if (getUidT)
            {
                locUrl += "<li onclick=\"javascript:UpdatestateContent(0)\"><i class='fa fa-check-square-o'></i>一键启用</li>";
            }
            bool getUidF = bll_limit.GetLocUserLimitVoids("/Help/UpdateStateContent", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//本地管理员一键禁用
            if (getUidF)
            {
                locUrl += "<li onclick=\"javascript:UpdatestateContent(1);\"><i class='fa fa-check-square-o'></i>一键禁用</li>";
            }
            ViewBag.locUrl = locUrl;

            return View();
         
        }
        public ActionResult AddContent()
        {
            JMP.MDL.jmp_Help_Content model = new jmp_Help_Content();
            JMP.BLL.jmp_Help_Content bllmode = new JMP.BLL.jmp_Help_Content();
            int Id = string.IsNullOrEmpty(Request["Id"]) ? 0 : Int32.Parse(Request["Id"]);
            if (Id > 0)
            {
                model = bllmode.GetModel(Id);
            }
            ViewBag.model = model;
            DataTable tablelist = bll.GetList(" 1=1 and ParentID=0 and State=0  ").Tables[0];
            List<JMP.MDL.jmp_Help_Classification> list = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_Help_Classification>(tablelist);
            ViewBag.list = list;
            return View();
        }
        /// <summary>
        /// 添加内容方法
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult AddorEditContent(jmp_Help_Content mod)
        {
           // mod.Content = mod.Content.RemoveStyles();
            JMP.BLL.jmp_Help_Content bll = new JMP.BLL.jmp_Help_Content();
            object retJson = new { success = 0, msg = "操作失败" };
            if(mod.ID>0)
            {
                JMP.MDL.jmp_Help_Content mo = new jmp_Help_Content();
                mo = bll.GetModel(mod.ID);
                mo.ISOverhead = mod.ISOverhead;
                mo.Title = mod.Title;
                mo.UpdateById = UserInfo.Uid;
                mo.UpdateByName = UserInfo.UserName;
                mo.UpdateOn = DateTime.Now;
                mo.Type = mod.Type;
                mo.Content = mod.Content;
                mo.PrentID = mod.PrentID;
                mo.ClassId = mod.ClassId;
                if (bll.Update(mo))
                {
                    Logger.ModifyLog("修改内容", mo, mod);
                    bll.UpdateClassCount(mod.PrentID, 0);
                    bll.UpdateClassCount(mod.ClassId, 1);
                    retJson = new { success = 1, msg = "修改成功" };
                }
                else
                {
                    retJson = new { success = 0, msg = "修改失败" };
                }
            }
            else
            {
                mod.State = 0;
                mod.CreateById = UserInfo.Uid;
                mod.CreateByName = UserInfo.UserName;
                mod.CreateOn = DateTime.Now;
                mod.UpdateOn = DateTime.Now;
                int cg = bll.Add(mod);
                if (cg > 0)
                {
                    Logger.CreateLog("添加内容", model);
                    bll.UpdateClassCount(mod.PrentID, 0);
                    bll.UpdateClassCount(mod.ClassId, 1);
                    retJson = new { success = 1, msg = "添加成功" };
                }
                else
                {
                    retJson = new { success = 0, msg = "添加失败" };
                }

            }
         
            return Json(retJson);
        }
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public string SelectPrentId()
        {
             int Type = string.IsNullOrEmpty(Request["Type"].ToString()) ? -1 : int.Parse(Request["Type"].ToString());
            int PrentID = string.IsNullOrEmpty(Request["PrentID"].ToString()) ? -1 : int.Parse(Request["PrentID"].ToString());
            JMP.BLL.jmp_Help_Classification bll = new JMP.BLL.jmp_Help_Classification();
            DataTable dt = bll.GetList(" 1=1 and State=0 and  ParentID=0 and  Type='" + Type + "' order by Sort desc  ").Tables[0];
            string yyzl = "<option value = '-1'> --请选择-- </option>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Int32.Parse(dt.Rows[i]["ID"].ToString()) == PrentID)
                {
                    yyzl += " <option value='" + dt.Rows[i]["ID"] + "' selected=selected >" + dt.Rows[i]["ClassName"] + "</option>";
                }
                else
                {
                    yyzl += " <option value='" + dt.Rows[i]["ID"] + "' >" + dt.Rows[i]["ClassName"] + "</option>";
                }
            }
            return yyzl;
        }
        /// <summary>
        /// 获取子类
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public string SelectClassId()
        {
            int PrentID = string.IsNullOrEmpty(Request["PrentID"].ToString()) ? -1 :int.Parse( Request["PrentID"].ToString());
            int ClassID= string.IsNullOrEmpty(Request["ClassID"].ToString()) ? -1 : int.Parse(Request["ClassID"].ToString());
            JMP.BLL.jmp_Help_Classification bll = new JMP.BLL.jmp_Help_Classification();
            DataTable dt = bll.GetList(" 1=1 and State=0 and  ParentID='"+PrentID+"' order by Sort desc  ").Tables[0];
            string yyzl = "<option value = '-1'> --请选择-- </option>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Int32.Parse(dt.Rows[i]["ID"].ToString()) == ClassID)
                {
                    yyzl += " <option value='" + dt.Rows[i]["ID"] + "' selected=selected >" + dt.Rows[i]["ClassName"] + "</option>";
                }
                else
                {
                    yyzl += " <option value='" + dt.Rows[i]["ID"] + "' >" + dt.Rows[i]["ClassName"] + "</option>";
                }
            }
            return yyzl;
        }

        public JsonResult UpdateStateContent()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            int state = string.IsNullOrEmpty(Request["state"]) ? 0 : Int32.Parse(Request["state"].ToString());
            string str = Request["ids"];
            string xgzfc = "";//组装说明
            string tsmsg = "";//提示
            JMP.BLL.jmp_Help_Content bll = new JMP.BLL.jmp_Help_Content();
            if (str.CompareTo("On") > 0)
            {
                str = str.Substring(3);
            }
            if (bll.UpdateState(str, state))
            {
                if (state == 1)
                {
                    xgzfc = "一键禁用ID为：" + str;
                    tsmsg = "禁用成功";
                }
                else
                {
                    tsmsg = "启用成功";
                    xgzfc = "一键启用ID为：" + str;
                }

                Logger.OperateLog("应用一键禁用或启用", xgzfc);

                retJson = new { success = 1, msg = tsmsg };
            }
            else
            {
                if (state == 1)
                {
                    tsmsg = "禁用失败";
                }
                else
                {
                    tsmsg = "启用失败";
                }
                retJson = new { success = 0, msg = tsmsg };
            }
            return Json(retJson);
        }


    }


   
}
