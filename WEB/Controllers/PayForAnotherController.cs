using JMP.TOOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB.ViewModel.Monitoringconfig;
using WEB.Util.Logger;
using TOOL.Extensions;
using System.Data;

namespace WEB.Controllers
{
    public class PayForAnotherController : Controller
    {
        JMP.BLL.jmp_limit bll_limit = new JMP.BLL.jmp_limit();
        JMP.BLL.jmp_channel_filter_config bll = new JMP.BLL.jmp_channel_filter_config();
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;

        JMP.BLL.PayForAnotherInfo PayForAnotherBll = new JMP.BLL.PayForAnotherInfo();
        JMP.MDL.PayForAnotherInfo PayForAnotherMode = new JMP.MDL.PayForAnotherInfo();
        List<JMP.MDL.PayForAnotherInfo> PayForAnotherInfoList = new List<JMP.MDL.PayForAnotherInfo>();

        JMP.BLL.PayChannel PayChannelBll = new JMP.BLL.PayChannel();
        JMP.MDL.PayChannel PayChannelMode = new JMP.MDL.PayChannel();
        List<JMP.MDL.PayChannel> PayCList = new List<JMP.MDL.PayChannel>();

        #region 代付通道

        /// <summary>
        /// 通道列表
        /// </summary>
        /// <returns></returns>
        public ActionResult PayChannelList()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量

            //查询所有
            PayCList = PayChannelBll.PayChannelList(pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = PayCList;

            //权限
            string locUrl = "";
            bool getPayAdd = bll_limit.GetLocUserLimitVoids("/PayForAnother/PayChannelAdd", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加
            if (getPayAdd)
            {
                locUrl += "<li onclick=\"PayChannelsAdd(0)\"><i class='fa fa-plus'></i>添加代付通道</li>";
            }

            ViewBag.locUrl = locUrl;

            return View();

        }

        /// <summary>
        /// 添加通道
        /// </summary>
        /// <returns></returns>
        public ActionResult PayChannelAdd()
        {
            return View();
        }

        /// <summary>
        /// 添加代付通道信息
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult InsertPayChannel(JMP.MDL.PayChannel mode)
        {
            object retJson = new { success = 0, msg = "操作失败" };

            mode.Append = UserInfo.UserName;
            mode.Appendtime = DateTime.Now;

            int num = PayChannelBll.Add(mode);
            if (num > 0)
            {
                Logger.CreateLog("添加代付通道", mode);

                retJson = new { success = 1, msg = "操作成功" };

            }

            return Json(retJson);
        }


        #endregion


        #region 代付通道信息

        /// <summary>
        /// 代付通道列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult PayForAnotherList()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量

            int PayType = string.IsNullOrEmpty(Request["PayType"]) ? 0 : Int32.Parse(Request["PayType"]);//查询类型
            string searchKey = string.IsNullOrEmpty(Request["searchKey"]) ? "" : Request["searchKey"];//关键字
            int IsEnabled = string.IsNullOrEmpty(Request["IsEnabled"]) ? -1 : Int32.Parse(Request["IsEnabled"]);//状态

            //查询所有
            PayForAnotherInfoList = PayForAnotherBll.PayForAnotherInfoList(PayType, searchKey, IsEnabled, pageIndexs, PageSize, out pageCount);
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = PayForAnotherInfoList;
            ViewBag.PayType = PayType;
            ViewBag.searchKey = searchKey;
            ViewBag.IsEnabled = IsEnabled;


            //权限
            string locUrl = "";
            bool getPayAdd = bll_limit.GetLocUserLimitVoids("/PayForAnother/PayForAnotherAdd", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加
            if (getPayAdd)
            {
                locUrl += "<li onclick=\"PayForAnohterAdd(0)\"><i class='fa fa-plus'></i>添加通道信息</li>";
            }

            ViewBag.locUrl = locUrl;

            return View();
        }

        /// <summary>
        /// 添加代付通道
        /// </summary>
        /// <returns></returns>
        public ActionResult PayForAnotherAdd()
        {
            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : int.Parse(Request["id"]);
            if (id > 0)
            {
                //查询一条数据
                PayForAnotherMode = PayForAnotherBll.GetModel(id);
            }

            DataSet ds = PayChannelBll.GetList("");

            PayCList = JMP.TOOL.MdlList.ToList<JMP.MDL.PayChannel>(ds.Tables[0]);

            ViewBag.mode = PayForAnotherMode;
            ViewBag.list = PayCList;

            return View();
        }

        /// <summary>
        /// 添加代付方法
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult PayForAnother_PayChannelAdd(JMP.MDL.PayForAnotherInfo mode)
        {
            object retJson = new { success = 0, msg = "操作失败" };

            if (mode.p_Id > 0)
            {
                PayForAnotherMode = PayForAnotherBll.GetModel(mode.p_Id);
                //拷贝
                var moPayForAnother = PayForAnotherMode.Clone();
                PayForAnotherMode.p_InterfaceName = mode.p_InterfaceName;
                PayForAnotherMode.p_InterfaceType = mode.p_InterfaceType;
                PayForAnotherMode.p_MerchantNumber = mode.p_MerchantNumber;
                PayForAnotherMode.p_KeyType = mode.p_KeyType;
                PayForAnotherMode.p_PrivateKey = mode.p_PrivateKey;
                PayForAnotherMode.p_PublicKey = mode.p_PublicKey;
                PayForAnotherMode.p_auditor = UserInfo.UserName;
                PayForAnotherMode.p_auditortime = DateTime.Now;

                if (PayForAnotherBll.Update(PayForAnotherMode))
                {
                    Logger.ModifyLog("修改代付通道信息", moPayForAnother, PayForAnotherMode);
                    retJson = new { success = 1, msg = "操作成功" };
                }

            }
            else
            {

                mode.p_append = UserInfo.UserName;
                mode.p_appendtime = DateTime.Now;
                mode.p_auditor = null;
                mode.p_auditortime = null;
                mode.IsEnabled = true;

                int num = PayForAnotherBll.Add(mode);
                if (num > 0)
                {
                    Logger.CreateLog("添加代付通道信息", mode);

                    retJson = new { success = 1, msg = "操作成功" };
                }
            }
            return Json(retJson);
        }

        /// <summary>
        /// 一键启用禁用
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult PayForAnotherStart()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            int state = string.IsNullOrEmpty(Request["state"]) ? 0 : Int32.Parse(Request["state"].ToString());
            int id = string.IsNullOrEmpty(Request["ids"]) ? 0 : Int32.Parse(Request["ids"].ToString());


            #region 启用禁用
            string xgzfc = "";//组装说明
            string tsmsg = "";//提示

            if (PayForAnotherBll.UpdatePayForAnotherState(id, state))
            {
                if (state == 1)
                {
                    xgzfc = "一键启用ID为：" + id;
                    tsmsg = "启用成功";
                }
                else
                {
                    tsmsg = "禁用成功";
                    xgzfc = "一键禁用ID为：" + id;
                }

                Logger.OperateLog("一键启用或禁用代付通道状态", xgzfc);
                retJson = new { success = 1, msg = tsmsg };
            }
            else
            {
                if (state == 1)
                {
                    tsmsg = "启用失败";
                }
                else
                {
                    tsmsg = "禁用失败";
                }
                retJson = new { success = 0, msg = tsmsg };
            }

            #endregion

            return Json(retJson);
        }

        #endregion
    }
}
