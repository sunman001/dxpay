using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB.Util.Logger;
using JMP.TOOL;
using System.Data;
using TOOL.Extensions;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections;
using System.Globalization;
using System.Text;
using TOOL;

namespace WEB.Controllers
{
    public class CustomServiceController : Controller
    {
        /// <summary>
        /// 日志收集器
        /// </summary>
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;

        #region 投诉类型

        JMP.BLL.jmp_limit bll_limit = new JMP.BLL.jmp_limit();
        List<JMP.MDL.CsComplainType> list = new List<JMP.MDL.CsComplainType>();
        JMP.BLL.CsComplainType bll = new JMP.BLL.CsComplainType();
        JMP.MDL.CsComplainType mo = new JMP.MDL.CsComplainType();

        /// <summary>
        /// 投诉类型管理列表
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult ComplainType()
        {

            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            int SelectState = string.IsNullOrEmpty(Request["SelectState"]) ? -1 : Int32.Parse(Request["SelectState"]);//状态
            string type = string.IsNullOrEmpty(Request["type"]) ? "" : Request["type"];//查询条件选择
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容

            list = bll.SelectList(sea_name, type, SelectState, searchDesc, pageIndexs, PageSize, out pageCount);
            ViewBag.searchDesc = searchDesc;
            ViewBag.SelectState = SelectState;
            ViewBag.type = type;
            ViewBag.sea_name = sea_name;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;

            string locUrl = "";
            bool getUidT = bll_limit.GetLocUserLimitVoids("/CustomService/Updatestate", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//本地管理员一键启用
            if (getUidT)
            {
                locUrl += "<li onclick=\"javascript:Updatestate(0)\"><i class='fa fa-check-square-o'></i>一键启用</li>";
                locUrl += "<li onclick=\"javascript:Updatestate(1);\"><i class='fa fa-check-square-o'></i>一键禁用</li>";
            }
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/CustomService/InsertOrUpdateAddType", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加管理员
            if (getlocuserAdd)
            {
                locUrl += "<li onclick=\"AddType()\"><i class='fa fa-plus'></i>添加投诉类型</li>";
            }
            ViewBag.locUrl = locUrl;

            return View();
        }

        /// <summary>
        /// 添加投诉类型
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult ComplainTypeAdd()
        {
            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);
            if (id > 0)
            {
                mo = bll.GetModel(id);
            }

            ViewBag.mo = mo;
            return View();
        }


        /// <summary>
        /// 添加修改投诉类型
        /// </summary>
        /// <returns></returns>
        public JsonResult InsertOrUpdateAddType(JMP.MDL.CsComplainType mode)
        {
            object retJson = new { success = 0, msg = "操作失败" };

            if (mode.Id > 0)
            {
                #region 修改
                //得到一个实体对象
                mo = bll.GetModel(mode.Id);
                //拷贝
                var mocolne = mo.Clone();
                mo.Name = mode.Name;
                mo.Description = mode.Description;

                if (bll.Update(mo))
                {
                    Logger.ModifyLog("修改投诉类型", mocolne, mo);

                    retJson = new { success = 1, msg = "修改成功" };
                }
                else
                {
                    retJson = new { success = 0, msg = "修改失败" };
                }
                #endregion
            }
            else
            {
                #region 添加

                mode.state = 0;
                mode.CreatedOn = DateTime.Now;
                mode.CreatedByUserId = UserInfo.UserId;
                int num = bll.Add(mode);
                if (num > 0)
                {
                    Logger.CreateLog("添加投诉类型", mode);

                    retJson = new { success = 1, msg = "添加成功" };
                }
                else
                {
                    retJson = new { success = 0, msg = "添加失败" };
                }
                #endregion
            }
            return Json(retJson);
        }

        /// <summary>
        /// 批量启用或禁用
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult PlUpdateState()
        {
            object retJson = new { success = 0, msg = "操作失败" };

            int state = string.IsNullOrEmpty(Request["state"]) ? 0 : Int32.Parse(Request["state"].ToString());
            string str = Request["ids"];
            string xgzfc = "";
            string tssm = "";//提示说明
            if (str.CompareTo("On") > 0)
            {
                str = str.Substring(3);
            }
            if (bll.UpdateCustomState(str, state))
            {
                if (state == 0)
                {
                    xgzfc = "一键启用ID为：" + str;
                    tssm = "启用成功";
                }
                else
                {
                    xgzfc = "一键禁用ID为：" + str;
                    tssm = "禁用成功";
                }

                Logger.OperateLog("投诉类型一键启用或禁用", xgzfc);
                retJson = new { success = 1, msg = tssm };
            }
            else
            {
                if (state == 1)
                {
                    tssm = "启用失败";
                }
                else
                {
                    tssm = "禁用失败";
                }
                retJson = new { success = 0, msg = tssm };
            }
            return Json(retJson);
        }



        #endregion

        #region 投诉管理

        JMP.BLL.CsComplainOrder cscobll = new JMP.BLL.CsComplainOrder();
        List<JMP.MDL.CsComplainOrder> cscolist = new List<JMP.MDL.CsComplainOrder>();
        JMP.MDL.CsComplainOrder cscomod = new JMP.MDL.CsComplainOrder();

        /// <summary>
        /// 投诉列表
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult ComplainOrderList()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            int SelectState = string.IsNullOrEmpty(Request["SelectState"]) ? -1 : Int32.Parse(Request["SelectState"]);//状态
            string type = string.IsNullOrEmpty(Request["type"]) ? "" : Request["type"];//查询条件选择
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            string SeachDate = string.IsNullOrEmpty(Request["SeachDate"]) ? "0" : Request["SeachDate"];
            string stime = string.IsNullOrEmpty(Request["stime"]) ? DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") : Request["stime"];
            string etime = string.IsNullOrEmpty(Request["etime"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["etime"];
            cscolist = cscobll.SelectList(SeachDate, stime, etime, sea_name, type, SelectState, searchDesc, pageIndexs, PageSize, out pageCount);
            ViewBag.searchDesc = searchDesc;
            ViewBag.SelectState = SelectState;
            ViewBag.type = type;
            ViewBag.SeachDate = SeachDate;
            ViewBag.stime = stime;
            ViewBag.etime = etime;
            ViewBag.sea_name = sea_name;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.cscolist = cscolist;

            string locUrl = "";
            bool getUidT = bll_limit.GetLocUserLimitVoids("/CustomService/UpdatestateOrder", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//本地管理员一键启用
            if (getUidT)
            {
                locUrl += "<li onclick=\"javascript:Updatestate(0)\"><i class='fa fa-check-square-o'></i>一键启用</li>";
                locUrl += "<li onclick=\"javascript:Updatestate(1);\"><i class='fa fa-check-square-o'></i>一键禁用</li>";
            }

            ViewBag.locUrl = locUrl;
            return View();
        }

        /// <summary>
        /// 添加投诉
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult ComplainOrderAdd()
        {
            string o_code = string.IsNullOrEmpty(Request["o_code"]) ? "" : Request["o_code"];
            string u_id = string.IsNullOrEmpty(Request["u_id"]) ? "" : Request["u_id"];
            //appId
            string o_app_id = string.IsNullOrEmpty(Request["o_app_id"]) ? "" : Request["o_app_id"];
            //通道配置表ID
            int o_interface_id = string.IsNullOrEmpty(Request["o_interface_id"]) ? 0 : int.Parse(Request["o_interface_id"]);

          
            //查询投诉类型名称
            cscomod = cscobll.SelectListOrder(o_code);
            ViewBag.CustomModel = cscomod;

            ViewBag.o_code = o_code;
            ViewBag.u_id = u_id;
            ViewBag.o_app_id = o_app_id;
            ViewBag.o_interface_id = o_interface_id;

            return View();
        }

        /// <summary>
        /// 修改投诉
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult ComplainOrderUpdate()
        {
            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);

            if (id > 0)
            {
                cscomod = cscobll.GetModel(id);
                string[] Envidence = cscomod.Envidence.Split(',');
                ViewBag.Envidence = Envidence;

            }

            ViewBag.UploadUrl = ConfigurationManager.AppSettings["imgurl"];
            ViewBag.cscomod = cscomod;

            return View();
        }

        /// <summary>
        /// 订单投诉列表
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult OrderList()
        {

            #region 获取信息

            JMP.BLL.jmp_paymode paymodebll = new JMP.BLL.jmp_paymode();
            List<JMP.MDL.jmp_paymode> paymodeList = paymodebll.GetModelList("1=1 and p_state='1' ");//支付类型
            ViewBag.paymodeList = paymodeList;
            #endregion
            #region 查询
            string sql = "";
            string sql1 = "";
            //组装查询条件
            string TableName = "";//表名
            string order = "o_ptime ";//排序字段
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchType = string.IsNullOrEmpty(Request["searchType"]) ? 0 : Int32.Parse(Request["searchType"]); //查询条件选择
            string searchname = string.IsNullOrEmpty(Request["searchname"]) ? "" : Request["searchname"];//查询类容
            string stime = string.IsNullOrEmpty(Request["stime"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["stime"];//开始时间
            string etime = string.IsNullOrEmpty(Request["etime"]) ? DateTime.Now.ToString("yyyy-MM-dd") : Request["etime"];//结束时间
            int paymode = string.IsNullOrEmpty(Request["paymode"]) ? 0 : Int32.Parse(Request["paymode"]);//支付类型
            string paymentstate = string.IsNullOrEmpty(Request["paymentstate"]) ? "1" : Request["paymentstate"];//支付状态
            string noticestate = string.IsNullOrEmpty(Request["noticestate"]) ? "" : Request["noticestate"];//通知状态
            int platformid = string.IsNullOrEmpty(Request["platformid"]) ? 0 : Int32.Parse(Request["platformid"]);//关联平台
            int relationtype = string.IsNullOrEmpty(Request["relationtype"]) ? -1 : Int32.Parse(Request["relationtype"]);//商户类型   

            ViewBag.platformid = platformid;
            ArrayList sjfw = JMP.TOOL.WeekDateTime.WeekMonday(DateTime.Parse(stime), DateTime.Parse(etime));//根据时间返回获取每周周一组装查询语句
            for (int i = 0; i < sjfw.Count; i++)
            {
                TableName = "jmp_order_" + DateTime.Parse(sjfw[i].ToString()).ToString("yyyyMMdd");
                // TableName = "jmp_order_20161107";
                sql += " SELECT o_id,o_code,o_bizcode,o_tradeno,o_paymode_id,o_app_id,o_goodsname,o_term_key,o_price,o_payuser,o_ctime,o_ptime,o_state,o_times,o_address,o_noticestate,o_noticetimes,o_privateinfo,o_interface_id, o_showaddress  FROM " + TableName + " where 1=1 ";
                if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
                {
                    sql += " and convert(varchar(10),o_ptime,120)>='" + stime + "' and convert(varchar(10),o_ptime,120)<='" + etime + "' ";

                }
                if (paymode > 0)
                {
                    sql += " and o_paymode_id='" + paymode + "' ";
                }
                if (!string.IsNullOrEmpty(paymentstate))
                {
                    sql += " and o_state='" + paymentstate + "' ";
                }
                if (!string.IsNullOrEmpty(noticestate))
                {
                    sql += " and o_noticestate='" + noticestate + "' ";
                }
                sql += "    UNION ALL ";
            }
            string where = "where 1=1";//组装查询条件
            if (searchType > 0 && !string.IsNullOrEmpty(searchname))
            {
                switch (searchType)
                {
                    case 1:
                        where += " and a.o_code='" + searchname + "' ";
                        break;
                    case 2:
                        where += " and  b.a_name='" + searchname + "'   ";
                        break;
                    case 3:
                        where += " and  a.o_goodsname='" + searchname + "' ";
                        break;
                    case 4:
                        where += " and e.u_realname like '%" + searchname + "%' ";
                        break;
                    case 5:
                        where += " and a.o_tradeno= '" + searchname + "' ";
                        break;
                    case 6:
                        where += " and a.o_bizcode= '" + searchname + "' ";
                        break;
                    case 7:
                        where += " and inn.l_corporatename like '%" + searchname + "%' ";
                        break;
                }
            }
            if (platformid > 0)
            {
                where += " and b.a_platform_id=" + platformid;
            }
            if (relationtype > -1)
            {
                where += " and e.relation_type=" + relationtype;
            }


            //组装时时表数据
            sql1 = "SELECT o_id,o_code,o_bizcode,o_tradeno,o_paymode_id,o_app_id,o_goodsname,o_term_key,o_price,o_payuser,o_ctime,o_ptime,o_state,o_times,o_address,o_noticestate,o_noticetimes,o_privateinfo,o_interface_id, o_showaddress FROM jmp_order where 1=1";
            if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
            {
                sql1 += " and convert(varchar(10),o_ptime,120)>='" + stime + "' and convert(varchar(10),o_ptime,120)<='" + etime + "' ";

            }
            if (paymode > 0)
            {
                sql1 += " and o_paymode_id='" + paymode + "' ";
            }
            if (!string.IsNullOrEmpty(paymentstate))
            {
                sql1 += " and o_state='" + paymentstate + "' ";
            }
            if (!string.IsNullOrEmpty(noticestate))
            {
                sql1 += " and o_noticestate='" + noticestate + "' ";
            }
            sql = sql + sql1;
            ViewBag.searchname = searchname;
            ViewBag.searchType = searchType;
            ViewBag.stime = stime;
            ViewBag.etime = etime;
            ViewBag.paymode = paymode;
            ViewBag.paymentstate = paymentstate;
            ViewBag.noticestate = noticestate;
            List<JMP.MDL.jmp_order> list = new List<JMP.MDL.jmp_order>();
            JMP.BLL.jmp_order orderbll = new JMP.BLL.jmp_order();
            list = orderbll.SelectPager(where, sql, order, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            ViewBag.relationtype = relationtype;
            #endregion

            return View();
        }

        /// <summary>
        /// 投诉类型弹窗
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult ComplainTypeTc()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            int SelectState = string.IsNullOrEmpty(Request["SelectState"]) ? -1 : Int32.Parse(Request["SelectState"]);//状态
            string type = string.IsNullOrEmpty(Request["type"]) ? "" : Request["type"];//查询条件选择
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容

            list = bll.SelectList(sea_name, type, SelectState, searchDesc, pageIndexs, PageSize, out pageCount);
            ViewBag.searchDesc = searchDesc;
            ViewBag.SelectState = SelectState;
            ViewBag.type = type;
            ViewBag.sea_name = sea_name;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;

            return View();
        }

        /// <summary>
        /// 添加修改投诉
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult CustomAdd(JMP.MDL.CsComplainOrder mode)
        {
            object retJson = new { success = 0, msg = "操作失败" };

            if (mode.Id > 0)
            {
                #region 修改
                //得到一个实体对象
                cscomod = cscobll.GetModel(mode.Id);
                //拷贝
                var mocolne = cscomod.Clone();
                cscomod.ComplainTypeId = mode.ComplainTypeId;
                cscomod.ComplainTypeName = mode.ComplainTypeName;
                cscomod.ComplainDate = mode.ComplainDate;
                cscomod.Envidence = mode.Envidence;
                cscomod.DownstreamStartTime = mode.DownstreamStartTime;
                cscomod.DownstreamEndTime = mode.DownstreamEndTime;

                if (cscobll.Update(cscomod))
                {
                    Logger.ModifyLog("修改投诉", mocolne, cscomod);

                    retJson = new { success = 1, msg = "修改成功" };
                }
                else
                {
                    retJson = new { success = 0, msg = "修改失败" };
                }
                #endregion
            }
            else
            {

                //判断该订单是否已经添加到投诉表中
                var exists = cscobll.GetModelList(string.Format("OrderNumber='{0}'", mode.OrderNumber.Replace("'", "''")));
                if (exists != null && exists.Count > 0)
                {
                    return Json(new { success = 0, msg = "此订单已提交过了" });
                }
                var time = DateTime.ParseExact(mode.OrderNumber.Substring(0, 8), "yyyyMMdd", CultureInfo.CurrentCulture);

                var tableName = WeekDateTime.GetOrderTableName(time.ToString(CultureInfo.InvariantCulture));

                mode.state = 0;
                mode.CreatedOn = DateTime.Now;
                mode.FounderId = UserInfo.UserId;
                mode.FounderName = UserInfo.UserName;
                mode.OrderTable = tableName;

                try
                {
                    //从订单归档表读取订单金额
                    var orderBll = new JMP.BLL.jmp_order();
                    var archiveOrder = orderBll.FindOrderByTableNameAndOrderNo(tableName, mode.OrderNumber);
                    if (archiveOrder == null || archiveOrder.o_price <= 0)
                    {
                        retJson = new { success = 0, msg = "添加失败[读取订单金额失败]" };
                        GlobalErrorLogger.Log("添加失败[读取订单金额失败]", summary: "添加投诉失败");
                        return Json(retJson);
                    }
                    mode.Price = archiveOrder.o_price;
                }
                catch (Exception ex)
                {
                    retJson = new { success = 0, msg = "添加失败[读取订单金额失败]" };
                    GlobalErrorLogger.Log("添加失败[读取订单金额失败]", summary: "添加投诉失败");
                    return Json(retJson);
                }
                int num = cscobll.Add(mode);
                if (num > 0)
                {
                    Logger.CreateLog("添加投诉", mode);

                    retJson = new { success = 1, msg = "添加成功" };
                }
                else
                {
                    retJson = new { success = 0, msg = "添加失败" };
                }

            }
            return Json(retJson);
        }


        /// <summary>
        /// 批量启用或禁用
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult PlUpdateStateOrder()
        {
            object retJson = new { success = 0, msg = "操作失败" };

            int state = string.IsNullOrEmpty(Request["state"]) ? 0 : Int32.Parse(Request["state"].ToString());
            string str = Request["ids"];
            string xgzfc = "";
            string tssm = "";//提示说明
            if (str.CompareTo("On") > 0)
            {
                str = str.Substring(3);
            }
            if (cscobll.UpdateCustomState(str, state))
            {
                if (state == 0)
                {
                    xgzfc = "一键启用ID为：" + str;
                    tssm = "启用成功";
                }
                else
                {
                    xgzfc = "一键禁用ID为：" + str;
                    tssm = "禁用成功";
                }

                Logger.OperateLog("投诉信息一键启用或禁用", xgzfc);
                retJson = new { success = 1, msg = tssm };
            }
            else
            {
                if (state == 1)
                {
                    tssm = "启用失败";
                }
                else
                {
                    tssm = "禁用失败";
                }
                retJson = new { success = 0, msg = tssm };
            }
            return Json(retJson);
        }

        /// <summary>
        /// 处理投诉
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult ComplainHandler()
        {
            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);
            //得到一个实体
            cscomod = cscobll.GetModel(id);
            ViewBag.cscomod = cscomod;

            return View();
        }


        /// <summary>
        /// 处理方法
        /// </summary>
        /// <returns></returns>
        public JsonResult ComplainHandlerAdd()
        {
            object retJson = new { success = 0, msg = "操作失败" };

            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : Int32.Parse(Request["id"].ToString());
            string HandleResult = Request["HandleResult"];
            var isRefund = Request["IsRefund"] ?? "0";
            string xgzfc = "";

            if (cscobll.UpdateCustomHandleResult(id, UserInfo.UserId, UserInfo.UserName, HandleResult, isRefund == "1"))
            {

                xgzfc = "投诉信息ID：" + id + ",处理结果：" + HandleResult + ",处理人：" + UserInfo.UserName;
                Logger.OperateLog("处理投诉", xgzfc);
                retJson = new { success = 1, msg = "处理成功！" };
            }
            else
            {

                retJson = new { success = 0, msg = "处理失败！" };
            }
            return Json(retJson);
        }

        #endregion

        #region 上传证据

        /// <summary>
        /// 上传图片(证据1)
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
                    string uploadurl = ConfigurationManager.AppSettings["uploadurlkfz"] + "/Envidence/";
                    //图片域名地址
                    string imgurlroot = ConfigurationManager.AppSettings["imgurl"] + "/Envidence/";
                    //上传图片
                    result = PubImageUp.UpImages("certificatefile1", uploadurl);
                    //图片返回展示地址
                    returnurl2 = imgurlroot + result[0];
                    if (result.Length == 2)
                    {
                        if (result[0] != "99")
                            returnurl = "/Envidence/" + result[0];

                        else
                            msg = "/Envidence/" + result[0];

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
        /// 上传图片(证据2)
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public JsonResult UploadImg2()
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
                    string uploadurl = ConfigurationManager.AppSettings["uploadurlkfz"] + "/Envidence/";
                    //图片域名地址
                    string imgurlroot = ConfigurationManager.AppSettings["imgurl"] + "/Envidence/";
                    //上传图片
                    result = PubImageUp.UpImages("certificatefile2", uploadurl);
                    //图片返回展示地址
                    returnurl2 = imgurlroot + result[0];
                    if (result.Length == 2)
                    {
                        if (result[0] != "99")
                            returnurl = "/Envidence/" + result[0];

                        else
                            msg = "/Envidence/" + result[0];

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
        /// 上传图片(证据3)
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public JsonResult UploadImg3()
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
                    string uploadurl = ConfigurationManager.AppSettings["uploadurlkfz"] + "/Envidence/";
                    //图片域名地址
                    string imgurlroot = ConfigurationManager.AppSettings["imgurl"] + "/Envidence/";
                    //上传图片
                    result = PubImageUp.UpImages("certificatefile3", uploadurl);
                    //图片返回展示地址
                    returnurl2 = imgurlroot + result[0];
                    if (result.Length == 2)
                    {
                        if (result[0] != "99")
                            returnurl = "/Envidence/" + result[0];

                        else
                            msg = "/Envidence/" + result[0];

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
        /// 上传图片(证据4)
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public JsonResult UploadImg4()
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
                    string uploadurl = ConfigurationManager.AppSettings["uploadurlkfz"] + "/Envidence/";
                    //图片域名地址
                    string imgurlroot = ConfigurationManager.AppSettings["imgurl"] + "/Envidence/";
                    //上传图片
                    result = PubImageUp.UpImages("certificatefile4", uploadurl);
                    //图片返回展示地址
                    returnurl2 = imgurlroot + result[0];
                    if (result.Length == 2)
                    {
                        if (result[0] != "99")
                            returnurl = "/Envidence/" + result[0];

                        else
                            msg = "/Envidence/" + result[0];

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

        #region 投诉统计

        List<JMP.MDL.CsComplainArchive> archivelist = new List<JMP.MDL.CsComplainArchive>();
        JMP.MDL.CsComplainArchive archivemodel = new JMP.MDL.CsComplainArchive();
        JMP.BLL.CsComplainArchive archivemobll = new JMP.BLL.CsComplainArchive();

        /// <summary>
        /// 投诉统计
        /// </summary>
        /// <returns></returns>
        public ActionResult ComplainArchiveList()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["curr"]) ? 1 : Int32.Parse(Request["curr"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["psize"]) ? 20 : Int32.Parse(Request["psize"]);//每页显示数量            
            string types = string.IsNullOrEmpty(Request["s_type"]) ? "1" : Request["s_type"];
            string searchKey = string.IsNullOrEmpty(Request["s_key"]) ? "" : Request["s_key"];
            string stime = string.IsNullOrEmpty(Request["s_begin"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_begin"];
            string etime = string.IsNullOrEmpty(Request["s_end"]) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : Request["s_end"];
            int sort = string.IsNullOrEmpty(Request["s_sort"]) ? 1 : int.Parse(Request["s_sort"]);

            #region 组装查询语句
            string where = "where 1=1";
            string orderby = "";
            if (types == "1" && !string.IsNullOrEmpty(searchKey))
            {
                where += " and b.u_realname like '%" + searchKey + "%'";
            }
            if (!string.IsNullOrEmpty(stime) && !string.IsNullOrEmpty(etime))
            {
                where += " and ArchiveDay >='" + stime + "' and ArchiveDay<='" + etime + "'";
            }
            if (sort == 1)
            {
                orderby = "order by ArchiveDay desc";

            }
            else
            {
                orderby = "order by ArchiveDay asc";
            }
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@"select ISNULL(avg(a.AvgHandleTime),0) as AvgHandleTime ,ArchiveDay,UserId,b.u_realname,b.relation_type,ISNULL(SUM(a.Amount),0) as Amount,(
select ISNULL(SUM(a_success),0)as a_success from jmp_appreport 
where a.UserId=a_uerid and a_time=a.ArchiveDay ) as a_success
from 
dx_total.dbo.CsComplainArchive a
left join 
dx_base.dbo.jmp_user b on a.UserId=b.u_id  
{0} 
group by ArchiveDay,UserId,b.u_realname,b.relation_type", where);

            archivelist = archivemobll.GetLists(sql.ToString(), orderby, pageIndexs, PageSize, out pageCount);
            #endregion
            ViewBag.show_fields = types;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = archivelist;

            return View();
        }

        /// <summary>
        /// 投诉统计详情
        /// </summary>
        /// <returns></returns>
        public ActionResult ComplainArchiveList_details(int userid, string ArchiveDay)
        {
            string type = "";
            //查询所有未冻结类型
            DataSet ds = bll.GetList("[state]=0");

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                type += row["Name"].ToString() + ",";
            }

            DataTable dt = new DataTable();

            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@"select m.*,n.总计,CASE when a_success=0 then 0 else Convert(decimal(18,2),(n.总计/a_success)*100) end as 投诉率 from (select * from (select b.a_name as 应用名称,c.[Name],a.Amount from dx_total.dbo.CsComplainArchive a
left join dx_base.dbo.jmp_app b on a.AppId=b.a_id
left join dx_base.dbo.CsComplainType c on a.ComplainTypeId=c.Id
where a.ArchiveDay='" + ArchiveDay + "' and a.UserId='" + userid + "') as a PIVOT(sum(a.Amount) FOR a.[Name] in(" + type.Substring(0, type.LastIndexOf(",")) + ")) b) m,(SELECT 应用名称,SUM(Amount)总计,a_success FROM(select b.a_name as 应用名称, c.[Name], a.Amount,(select ISNULL(SUM(a_success),0)as a_success from jmp_appreport where a.UserId = a_uerid and a.AppId = a_appid and a_time = a.ArchiveDay) as a_success from dx_total.dbo.CsComplainArchive a left join dx_base.dbo.jmp_app b on a.AppId = b.a_id left join dx_base.dbo.CsComplainType c on a.ComplainTypeId = c.Id where a.ArchiveDay = '" + ArchiveDay + "' and a.UserId = '" + userid + "') as a GROUP BY 应用名称,a_success) n WHERE m.应用名称 = n.应用名称");


            //查询详情
            dt = archivemobll.SelectArchive(sql.ToString());
            //archivelist = JMP.TOOL.MdlList.ToList<JMP.MDL.CsComplainArchive>(dt);

            ViewBag.archivelist = dt;
            ViewBag.type = type;

            return PartialView();
        }


        #endregion
    }
}
