/************聚米支付平台__消息管理************/
//描述：消息管理
//功能：消息管理
//开发者：秦际攀
//开发时间: 2016.04.25
/************聚米支付平台__消息管理************/
using JMP.TOOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TOOL.Extensions;
using WEB.Util.Logger;

namespace WEB.Controllers
{
    /// <summary>
    /// 消息管理
    /// </summary>
    public class MessageManagementController : Controller
    {
        JMP.BLL.jmp_limit bll_limit = new JMP.BLL.jmp_limit();
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;
        /// <summary>
        /// 公告管理界面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult noticeList()
        {
            #region 获取操作权限
            string locUrl = "";
            bool getUidF = bll_limit.GetLocUserLimitVoids("/MessageManagement/UpdateDelete", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//一键删除
            if (getUidF)
            {
                locUrl += "<li onclick=\"javascript:Updatestate(1);\"><i class='fa fa-check-square-o'></i>一键删除</li>";
            }
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/MessageManagement/insertOrUpdatenotice", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//发布公告
            if (getlocuserAdd)
            {
                locUrl += "<li onclick=\"addnotice()\"><i class='fa fa-plus'></i>发布公告</li>";
            }
            ViewBag.locUrl = locUrl;
            #endregion
            JMP.BLL.jmp_notice noticebll = new JMP.BLL.jmp_notice();
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchType = string.IsNullOrEmpty(Request["searchType"]) ? 0 : Int32.Parse(Request["searchType"]);//查询条件
            ViewBag.searchType = searchType;
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询内容
            ViewBag.sea_name = sea_name;
            string strsql = "select a.n_id, a.n_title, a.n_content, a.n_time, a.n_top, a.n_state, a.n_locuserid,b.u_loginname from jmp_notice a left join jmp_locuser b on a.n_locuserid=b.u_id where 1=1 and n_state='0'";
            if (searchType > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (searchType)
                {
                    case 1:
                        strsql += " and a.n_id like '%" + sea_name + "%' ";
                        break;
                    case 2:
                        strsql += " and  a.n_title like '%" + sea_name + "%' ";
                        break;
                }
            }
          
            string order = " order by n_top desc ,n_time desc ";//排序
            List<JMP.MDL.jmp_notice> list = noticebll.SelectList(strsql, order, pageIndexs, PageSize, out pageCount);
            ViewBag.pageCount = pageCount;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.list = list;
            return View();
        }
        /// <summary>
        /// 公共发布界面
        /// </summary>
        /// <returns></returns>
        public ActionResult notice()
        {
            int n_id = string.IsNullOrEmpty(Request["n_id"]) ? 0 : Int32.Parse(Request["n_id"]);
            JMP.BLL.jmp_notice noticebll = new JMP.BLL.jmp_notice();
            JMP.MDL.jmp_notice mo = new JMP.MDL.jmp_notice();
            if (n_id > 0)
            {
                mo = noticebll.GetModel(n_id);
            }
            ViewBag.mo = mo;
            return View();
        }
        /// <summary>
        /// 添加或修改公告
        /// </summary>
        /// <returns></returns>
        public JsonResult insertOrUpdatenotice(JMP.MDL.jmp_notice mo)
        {
            object retJson = new { success = 0, msg = "操作失败" };
            JMP.BLL.jmp_notice noticebll = new JMP.BLL.jmp_notice();
            mo.n_time = DateTime.Now;
            mo.n_state = 0;
            mo.n_locuserid = UserInfo.UserId;
            if (mo.n_id > 0)
            {

                JMP.MDL.jmp_notice mod = noticebll.GetModel(mo.n_id);
                var modClone = mod.Clone();
                mod.n_title = mo.n_title;
                mod.n_content = mo.n_content;
                mod.n_top = mo.n_top;
                if (noticebll.Update(mod))
                {
                   
                    Logger.ModifyLog("修改公告", modClone, mo);
                    retJson = new { success = 1, msg = "修改成功!" };
                }
                else
                {
                    retJson = new { success = 1, msg = "修改失败！" };
                }

            }
            else
            {
                int cg = noticebll.Add(mo);
                if (cg > 0)
                {
                 
                    Logger.CreateLog("新增公告",mo);
                    retJson = new { success = 1, msg = "发布成功！" };
                }
                else
                {
                    retJson = new { success = 0, msg = "发布失败！" };
                }
            }
            return Json(retJson);
        }
        /// <summary>
        /// 一键删除公告
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdateDelete()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            int state = string.IsNullOrEmpty(Request["state"]) ? 0 : Int32.Parse(Request["state"].ToString());
            string str = Request["ids"];
            string xgzfc = "";//组装说明 
            string tsmsg = "";//提示
            JMP.BLL.jmp_notice noticebll = new JMP.BLL.jmp_notice();
            if (str.CompareTo("On") > 0)
            {
                str = str.Substring(3);
            }
            if (noticebll.UpdateLocUserState(str, state))
            {
                
                xgzfc = "删除公告ID为：" + str;
                tsmsg = "删除成功";
              
                Logger.OperateLog("删除公告", xgzfc);
                retJson = new { success = 1, msg = tsmsg };
            }
            else
            {
                
                tsmsg = "删除失败";
               
                retJson = new { success = 0, msg = tsmsg };
            }
            return Json(retJson);
        }
        /// <summary>
        /// 消息管理界面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult messagelist()
        {
            #region 获取权限
            string locUrl = "";
            bool getUidF = bll_limit.GetLocUserLimitVoids("/MessageManagement/UpdateDeleteMessage", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//一键删除
            if (getUidF)
            {
                locUrl += "<li onclick=\"javascript:UpdatestateMessage(-1);\"><i class='fa fa-check-square-o'></i>一键删除</li>";
            }
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/MessageManagement/InserOrUpdatemessage", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//发布公告
            if (getlocuserAdd)
            {
                locUrl += "<li onclick=\"addmessage()\"><i class='fa fa-plus'></i>发布消息</li>";
            }
            ViewBag.locUrl = locUrl;
            #endregion
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchType = string.IsNullOrEmpty(Request["searchType"]) ? 0 : Int32.Parse(Request["searchType"]);//查询条件
            ViewBag.searchType = searchType;
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询内容
            ViewBag.sea_name = sea_name;
            List<JMP.MDL.jmp_message> list = new List<JMP.MDL.jmp_message>();
            JMP.BLL.jmp_message mebll = new JMP.BLL.jmp_message();
            string strsql = "select m_id, m_sender, m_receiver, m_type, m_time, m_state, m_content, m_topid,b.u_loginname  from jmp_message a left join jmp_locuser b on a.m_sender=b.u_id  where 1=1  and a.m_state>-1  ";
            if (searchType > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (searchType)
                {
                    case 1:
                        strsql += " and a.m_id like '%" + sea_name + "%' ";
                        break;
                    case 2:
                        strsql += " and  b.u_loginname like '%" + sea_name + "%' ";
                        break;
                }
            }
          
            string order = " order by m_time desc";//排序字段
            list = mebll.SelectList(strsql, order, pageIndexs, PageSize, out pageCount);
            ViewBag.pageCount = pageCount;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.list = list;
            return View();
        }
        /// <summary>
        /// 发布或修改消息界面
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false), VisitRecord(IsRecord = true)]
        public ActionResult message()
        {
            int m_id = string.IsNullOrEmpty(Request["m_id"]) ? 0 : Int32.Parse(Request["m_id"]);
            JMP.MDL.jmp_message mo = new JMP.MDL.jmp_message();
            JMP.BLL.jmp_message mebll = new JMP.BLL.jmp_message();
            string type = string.IsNullOrEmpty(Request["type"]) ? "dd" : Request["type"];
            if (m_id > 0)
            {
                mo = mebll.GetModel(m_id);
            }
            ViewBag.type = type;
            ViewBag.mo = mo;
            return View();
        }
        /// <summary>
        /// 添加或修改消息
        /// </summary>
        /// <param name="mo"></param>
        /// <returns></returns>
        public JsonResult InserOrUpdatemessage(JMP.MDL.jmp_message mo)
        {
            object retJson = new { success = 0, msg = "操作失败" };
            mo.m_sender = UserInfo.UserId;
            JMP.BLL.jmp_message mebll = new JMP.BLL.jmp_message();
            string[] sta = Request["m_receiver"].Split(',');
            if (mo.m_id > 0)
            {
                JMP.MDL.jmp_message mod = mebll.GetModel(mo.m_id);
                var modClone = mod.Clone();
                mod.m_receiver = mo.m_receiver;
                mod.m_content = mo.m_content;
                //mo.m_state = mod.m_state;
                //mo.m_type = mod.m_type;
                //mo.m_time = mod.m_time;
                string sm = "";//日志说明
                if (mebll.Update(mod))
                {
                   
                    Logger.ModifyLog("修改消息", modClone, mo);
                    retJson = new { success = 1, msg = "修改成功" };
                }
                else
                {
                    retJson = new { success = 0, msg = "修改失败" };
                }
            }
            else
            {
                mo.m_time = DateTime.Now;
                mo.m_state = 0;
                mo.m_type = 1;
                StringBuilder strSql = new StringBuilder();
                for (int i = 0; i < sta.Length; i++)
                {
                    strSql.Append("insert into jmp_message(m_sender,m_receiver,m_type,m_time,m_state,m_content,m_topid) values ('" + mo.m_sender + "','" + sta[i] + "','1','" + DateTime.Now + "','0','" + mo.m_content + "','0') ");

                }
                int cg = mebll.AdminAdd(strSql);
                if (cg > 0)
                {
                  
                    Logger.CreateLog("发送消息", mo);
                    retJson = new { success = 1, msg = "发送成功" };
                }
                else
                {
                    retJson = new { success = 0, msg = "发送失败" };
                }
            }
            return Json(retJson);
        }
        /// <summary>
        /// 查询开发者用户用于选择
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false),VisitRecord(IsRecord = true)]
        public ActionResult UserList()
        {
            List<JMP.MDL.jmp_user> list = new List<JMP.MDL.jmp_user>();
            JMP.BLL.jmp_user bll = new JMP.BLL.jmp_user();
            #region 初始化
            //获取请求参数
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量
            string type = string.IsNullOrEmpty(Request["stype"]) ? "0" : Request["stype"];//查询条件类型
            string sea_name = string.IsNullOrEmpty(Request["skeys"]) ? "" : Request["skeys"];//查询条件值
            string category = string.IsNullOrEmpty(Request["scategory"]) ? "" : Request["scategory"];//认证类型
            int px = string.IsNullOrEmpty(Request["s_sort"]) ? 0 : Int32.Parse(Request["s_sort"]);//排序
            //获取用户列表
            string where = " where 1=1 and u_auditstate='1' and u_state='1' ";
            if (!string.IsNullOrEmpty(type.ToString()))
            {
                if (!string.IsNullOrEmpty(sea_name))
                {
                    if (type == "0")
                    {
                        where += string.Format(" and u_email like '%{0}%'", sea_name);
                    }
                    else if (type == "1")
                    {
                        where += string.Format(" and u_phone like '%{0}%'", sea_name);
                    }
                    else if (type == "3")
                    {
                        where += string.Format(" and u_idnumber like '%{0}%'", sea_name);
                    }
                    else if (type == "6")
                    {
                        where += string.Format(" and u_blicensenumber like '%{0}%'", sea_name);
                    }
                }
            }
            if (!string.IsNullOrEmpty(category))
            {
                where += string.Format(" and u_category={0}", category);
            }
            string Order = " order by u_id " + (px == 0 ? "" : " desc ") + " ";
            string query = "select * from jmp_user" + where;
            list = bll.GetLists(query, Order, pageIndexs, PageSize, out pageCount);
            //返回
            ViewBag.CurrPage = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.PageCount = pageCount;
            ViewBag.stype = type;
            ViewBag.skeys = sea_name;
            ViewBag.scategory = category;
            ViewBag.s_sort = px;
            ViewBag.list = list;
            #endregion
            return View();
        }
        /// <summary>
        /// 一键删除消息
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdateDeleteMessage()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            int state = string.IsNullOrEmpty(Request["state"]) ? 0 : Int32.Parse(Request["state"].ToString());
            string str = Request["ids"];
            string xgzfc = "";//组装说明 
            string tsmsg = "";//提示
            JMP.BLL.jmp_message mebll = new JMP.BLL.jmp_message();
            if (str.CompareTo("On") > 0)
            {
                str = str.Substring(3);
            }
            if (mebll.UpdateLocUserState(str, state))
            {
                xgzfc = "删除消息ID为：" + str;
                tsmsg = "删除成功";
              
                Logger.OperateLog("删除消息", xgzfc);
                retJson = new { success = 1, msg = tsmsg };
            }
            else
            {
                tsmsg = "删除失败";
                retJson = new { success = 0, msg = tsmsg };
            }
            return Json(retJson);
        }
        /// <summary>
        /// 消息回复界面
        /// </summary>
        /// <returns></returns>
        public ActionResult ReplyMessage()
        {
            int topid = string.IsNullOrEmpty(Request["topid"]) ? 0 : Int32.Parse(Request["topid"]);
            List<JMP.MDL.jmp_message> list = new List<JMP.MDL.jmp_message>();
            JMP.BLL.jmp_message mebll = new JMP.BLL.jmp_message();
            if (topid > 0)
            {
                list = mebll.ReplySelect(topid);
            }
            ViewBag.topid = topid;
            ViewBag.list = list;
            return View();
        }
        /// <summary>
        /// 消息回复
        /// </summary>
        /// <returns></returns>
        public JsonResult ReplyMessageUser(JMP.MDL.jmp_message mo)
        {
            object retJson = new { success = 0, msg = "操作失败" };
            mo.m_time = DateTime.Now;
            mo.m_state = 0;
            mo.m_type = 3;
            mo.m_sender = UserInfo.UserId;
            JMP.BLL.jmp_message mebll = new JMP.BLL.jmp_message();
            int cg = mebll.Add(mo);
            if (cg > 0)
            {
             
                Logger.OperateLog("回复消息", mo.m_content);
                retJson = new { success = 1, msg = "发送成功" };
            }
            else
            {
                retJson = new { success = 0, msg = "发送失败" };
            }
            return Json(retJson);
        }
    }
}
