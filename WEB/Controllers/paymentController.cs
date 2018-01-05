using JMP.TOOL;
/************聚米支付平台__支付配置控制器************/
//描述：支付配置控制器
//功能：支付配置控制器
//开发者：秦际攀
//开发时间: 2016.05.18
/************聚米支付平台__支付配置控制器************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Text;
using JMP.DBA;
using JMP.MDL;
using WEB.Models;
using WEB.Util.Logger;
using WEB.Util.RateLogger;
using TOOL.Extensions;

namespace WEB.Controllers
{
    /// <summary>
    /// 支付配置控制器
    /// </summary>

    public class paymentController : Controller
    {
        private static readonly ILogWriter Logger = LogWriterManager.GetOperateLogger;
        private static readonly IRateLogWriter RateLogger = RateLogWriterManager.GetOperateLogger;
        JMP.BLL.jmp_limit bll_limit = new JMP.BLL.jmp_limit();
        /// <summary>
        /// 支付配置列表界面
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult InterfaceList()
        {
            #region 获取操作权限
            string locUrl = "";
            bool getUidT = bll_limit.GetLocUserLimitVoids("/payment/UpdateState", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//本地管理员一键启用
            if (getUidT)
            {
                locUrl += "<li onclick=\"javascript:Updatestate(1)\"><i class='fa fa-check-square-o'></i>一键启用</li>";
            }
            bool getUidF = bll_limit.GetLocUserLimitVoids("/payment/UpdateState", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//本地管理员一键禁用
            if (getUidF)
            {
                locUrl += "<li onclick=\"javascript:Updatestate(0);\"><i class='fa fa-check-square-o'></i>一键禁用</li>";
            }
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/payment/InterfaceAddOrUpdate", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加管理员
            if (getlocuserAdd)
            {
                locUrl += "<li onclick=\"AddInterface()\"><i class='fa fa-plus'></i>添加支付配置</li>";
            }
            ViewBag.locUrl = locUrl;
            #endregion
            #region  查询
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            string sql = " select a .l_id, a .l_str, a .l_sort, a .l_isenable,a.l_apptypeid,a.l_corporatename, a .l_paymenttype_id,a.l_jsonstr,a.l_risk, a.l_daymoney,a.l_CostRatio, b.p_name,b.p_type,b.p_extend,c.p_name as zflxname,p_platform,a.l_minimum,a.l_maximum   from  jmp_interface a  left join jmp_paymenttype b on b.p_id=a.l_paymenttype_id left join jmp_paymode c on c.p_id=b.p_type where 1=1 ";
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            int SelectState = string.IsNullOrEmpty(Request["SelectState"]) ? 1 : Int32.Parse(Request["SelectState"]);//状态
            int auditstate = string.IsNullOrEmpty(Request["auditstate"]) ? 0 : Int32.Parse(Request["auditstate"]);//支付类型
            int type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);//查询条件选择
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            int risk = string.IsNullOrEmpty(Request["risk"]) ? -1 : Int32.Parse(Request["risk"]);//风控类型 
            int risl = string.IsNullOrEmpty(Request["risl"]) ? 0 : Int32.Parse(Request["risl"]);//风控类型
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        sql += " and a.l_id='" + sea_name + "' ";
                        break;
                    case 2:
                        sql += " and b.p_name='" + sea_name + "' ";
                        break;
                    case 3:
                        sql += " and a.l_corporatename like '%" + sea_name + "%' ";
                        break;
                    case 4:
                        sql += " and a.l_sort='" + sea_name + "'";
                        break;
                    case 5:
                        risk = 1;
                        JMP.BLL.jmp_app appbll = new JMP.BLL.jmp_app();
                        DataTable appdt = appbll.selectsql(" select a_id from jmp_app where a_name='" + sea_name + "'");
                        if (appdt.Rows.Count > 0)
                        {
                            for (int i = 0; i < appdt.Rows.Count; i++)
                            {
                                sql += "  and  ',' + l_apptypeid + ',' like '%,' + cast('" + appdt.Rows[i]["a_id"] + "'    as varchar(20)) + ',%'  ";
                            }
                        }
                        else
                        {
                            sql += "  and  ',' + l_apptypeid + ',' like '%,' + cast(''    as varchar(20)) + ',%'  ";
                        }
                        break;
                    case 6:
                        risk = 2;
                        JMP.BLL.jmp_channel_pool chbll = new JMP.BLL.jmp_channel_pool();
                        string where = "  PoolName='" + sea_name + "' ";
                        DataTable chdt = chbll.GetList(where).Tables[0];
                        if (chdt.Rows.Count > 0)
                        {
                            for (int i = 0; i < chdt.Rows.Count; i++)
                            {
                                sql += "  and  ',' + l_apptypeid + ',' like '%,' + cast('" + chdt.Rows[i]["Id"] + "'    as varchar(20)) + ',%'  ";
                            }
                        }
                        else
                        {
                            sql += "  and  ',' + l_apptypeid + ',' like '%,' + cast(''    as varchar(20)) + ',%'  ";
                        }
                        break;
                }
            }
            if (risk == 0 && risl > 0 && risl <= 3)
            {
                JMP.BLL.jmp_risklevelallocation fxdjbll = new JMP.BLL.jmp_risklevelallocation();
                DataTable fxdjdt = fxdjbll.GetList(" r_risklevel=" + risl + "  ").Tables[0];
                if (fxdjdt.Rows.Count > 0)
                {
                    for (int i = 0; i < fxdjdt.Rows.Count; i++)
                    {
                        sql += "  and  ',' + l_apptypeid + ',' like '%,' + cast('" + fxdjdt.Rows[i]["r_id"] + "'    as varchar(20)) + ',%'  ";
                    }
                }
                else
                {
                    sql += "  and  ',' + l_apptypeid + ',' like '%,' + cast(''    as varchar(20)) + ',%'  ";
                }
            }
            if (SelectState > -1)
            {
                sql += " and a.l_isenable='" + SelectState + "' ";
            }
            if (auditstate > 0)
            {
                sql += " and c.p_id='" + auditstate + "' ";
            }
            if (risk > -1)
            {
                sql += " and a.l_risk='" + risk + "'";
            }



            string Order = "order by l_id";
            if (searchDesc == 1)
            {
                Order = "  order by l_id  ";
            }
            else
            {
                Order = " order by l_id desc ";
            }
            JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
            List<JMP.MDL.jmp_interface> list = new List<JMP.MDL.jmp_interface>();
            list = bll.SelectList(sql, Order, pageIndexs, PageSize, out pageCount);
            ViewBag.list = list;
            JMP.BLL.jmp_paymode yybll = new JMP.BLL.jmp_paymode();
            string wherepay = " p_state=1";
            DataTable yydt = yybll.GetList(wherepay).Tables[0];//获取支付方式
            List<JMP.MDL.jmp_paymode> yylist = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_paymode>(yydt);
            ViewBag.yylist = yylist;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.searchDesc = searchDesc;
            ViewBag.SelectState = SelectState;
            ViewBag.type = type;
            ViewBag.auditstate = auditstate;
            ViewBag.sea_name = sea_name;
            ViewBag.risk = risk;
            ViewBag.risl = risl;
            #endregion
            return View();
        }


        #region

        /// <summary>
        /// 获取风控配置信息
        /// </summary>
        /// <param name="apptypeid"></param>
        /// <returns></returns>
        public static string SelectAppTyep(string apptypeid)
        {
            JMP.BLL.jmp_risklevelallocation yybll = new JMP.BLL.jmp_risklevelallocation();
            DataTable yydt = yybll.SelectRid(apptypeid);//获取应用类型在用信息
            string apptye = "";
            if (yydt.Rows.Count > 0)
            {
                for (int i = 0; i < yydt.Rows.Count; i++)
                {
                    apptye += yydt.Rows[i]["t_name"] + ",";
                }
            }
            if (apptye.Length > 0)
            {
                apptye = apptye.Substring(0, apptye.Length - 1);
            }
            return apptye;
        }
        /// <summary>
        /// 添加支付配置界面
        /// </summary>
        /// <returns></returns>
        public ActionResult InterfaceAdd()
        {

            JMP.BLL.jmp_paymode bl = new JMP.BLL.jmp_paymode();
            List<JMP.MDL.jmp_paymode> paylist = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_paymode>(bl.GetList(" 1=1 and p_state=1 ").Tables[0]);
            JMP.MDL.jmp_interface mo = new JMP.MDL.jmp_interface();
            JMP.BLL.jmp_interface blls = new JMP.BLL.jmp_interface();
            int lid = string.IsNullOrEmpty(Request["lid"]) ? 0 : Int32.Parse(Request["lid"]);
            if (lid > 0)
            {
                mo = blls.GetModels(lid);
            }
            else
            {
                mo.l_risk = -1;
            }
            ViewBag.mo = mo;
            ViewBag.paylist = paylist;
            JMP.BLL.jmp_apptype yybll = new JMP.BLL.jmp_apptype();
            string where = "  t_id in (select  DISTINCT(t_topid) from jmp_apptype where t_topid in( select t_id from jmp_apptype where t_topid='0'   )) and t_state='1' order by t_sort desc";
            DataTable yydt = yybll.GetList(where).Tables[0];//获取应用类型在用信息
            List<JMP.MDL.jmp_apptype> yylist = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_apptype>(yydt);
            ViewBag.yylist = yylist;
            return View();
        }
        /// <summary>
        /// 根据支付类型id查询支付通道信息
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public string SelectPaymenttype(int? p_type, int? zftdid)
        {
            JMP.BLL.jmp_paymenttype bll = new JMP.BLL.jmp_paymenttype();
            List<JMP.MDL.jmp_paymenttype> list = new List<JMP.MDL.jmp_paymenttype>();
            string liststr = "<option value=\"0\">--请选择类型--</option>";
            if (p_type > 0)
            {
                list = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_paymenttype>(bll.GetList(" 1=1 and p_type='" + p_type + "' and  p_forbidden=0 ").Tables[0]);
                list = list.Count > 0 ? list : new List<JMP.MDL.jmp_paymenttype>();
                string zftype = "";

                foreach (var item in list)
                {
                    zftype = item.p_id + "," + item.p_extend;
                    if (zftdid == item.p_id)
                    {
                        liststr += "<option value='" + zftype + "' selected='selected'>" + item.p_name + "</option>";
                    }
                    else
                    {
                        liststr += "<option value='" + zftype + "'>" + item.p_name + "</option>";
                    }
                }

            }
            return liststr;
        }
        /// <summary>
        /// 添加修改支付配置
        /// </summary>
        /// <returns></returns>
        public JsonResult InterfaceAddOrUpdate(JMP.MDL.jmp_interface mo)
        {
            object retJson = new { success = 0, msg = "操作失败" };
            JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();

            if (mo.l_id > 0)
            {
                JMP.MDL.jmp_interface mod = bll.GetModel(mo.l_id);
                var modClone = mod.Clone();
                mod.l_sort = mo.l_sort;
                mod.l_paymenttype_id = mo.l_paymenttype_id;
                mod.l_str = mo.l_str;
                mod.l_apptypeid = mo.l_apptypeid;
                mod.l_corporatename = mo.l_corporatename;
                mod.l_jsonstr = mo.l_jsonstr;
                mod.l_risk = mo.l_risk;
                mod.l_daymoney = mo.l_daymoney;
                mod.l_maximum = mo.l_maximum;
                mod.l_minimum = mo.l_minimum;
                // mo.l_isenable = mod.l_isenable;
                string xgsm = "";
                if (bll.Update(mod))
                {

                    Logger.ModifyLog("修改支付配置", modClone, mo);
                    retJson = new { success = 1, msg = "修改成功！" };
                }
                else
                {
                    retJson = new { success = 0, msg = "修改失败！" };
                }
            }
            else
            {
                // mo.l_isenable = 1;
                int cg = bll.Add(mo);
                if (cg > 0)
                {

                    Logger.CreateLog("添加支付配置", mo);
                    retJson = new { success = 1, msg = "添加成功！" };

                }
                else
                {
                    retJson = new { success = 0, msg = "添加失败！" };
                }
            }
            return Json(retJson);
        }
        /// <summary>
        /// 转换配置信息
        /// </summary>
        /// <param name="item"></param>
        /// <param name="configExt"></param>
        /// <returns></returns>
        private List<jmp_payment_type_config> ParsePaymentTypeConfigList(jmp_interface item, List<PaymentTypeConfigExt> configExt)
        {
            var models = new List<jmp_payment_type_config>();
            var splits = item.l_str.Split(',');
            for (var i = 0; i < splits.Length; i++)
            {
                var p = configExt[i];
                var model = new jmp_payment_type_config
                {
                    CreatedBy = UserInfo.UserName,
                    Description = p.label,
                    FieldName = p.fieldName,
                    InputType = p.InputType,
                    Label = p.label,
                    PaymentTypeId = item.l_paymenttype_id,
                    Status = 0
                };
                models.Add(model);
            }
            return models;
        }

        /// <summary>
        /// 修改原支付通道数据
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = false, IsRole = false)]
        public int UpdataInter()
        {
            var success = 0;
            var bll = new JMP.BLL.jmp_interface();
            List<jmp_interface> list;
            list = MdlList.ToList<jmp_interface>(DbHelperSQL.Query(@"select a.l_id, a .l_str, a.l_sort, a.l_isenable,a.l_apptypeid,a.l_corporatename, a.l_paymenttype_id,a.l_jsonstr,b.p_name,b.p_type,b.p_extend,c.p_name as zflxname   
from  jmp_interface a left join jmp_paymenttype b on b.p_id=a.l_paymenttype_id left join jmp_paymode c on c.p_id=b.p_type where (a.l_jsonstr is  null or a.l_jsonstr ='')").Tables[0]);
            var addedIds = new List<int>();
            var configExt = new List<PaymentTypeConfigExt>();
            for (var k = 0; k < list.Count; k++)
            {
                var addNewPaymentTypeConfig = false;
                var item = list[k];
                if (!addedIds.Contains(item.l_paymenttype_id))
                {
                    addedIds.Add(item.l_paymenttype_id);
                    addNewPaymentTypeConfig = true;
                }

                var str = "";
                var my = item.p_extend + "MY";
                var sql = "";
                switch (item.p_extend)
                {
                    case "ZFB":
                        #region

                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"支付宝账号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"支付宝私钥\"}]";

                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="支付宝账号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="支付宝私钥",
                                InputType="textarea"
                            }
                        };

                        #endregion
                        break;
                    case "WX":
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"微信账号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"微信私钥\"},{\"fieldName\":\"wxappid\",\"value\":\"" + item.l_str.Split(',')[2] + "\",\"label\":\"微信应用id\"},{\"fieldName\":\"wxappkey\",\"value\":\"" + item.l_str.Split(',')[3] + "\",\"label\":\"微信应用key\"}]";

                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="微信账号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="微信私钥",
                                InputType="textarea"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName="wxappid",
                                label="微信应用id"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName="wxappkey",
                                label="微信应用key",
                                InputType="textarea"
                            }
                        };

                        break;
                    case "WFT":
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"威富通账号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"威富通秘钥\"}]";

                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="威富通账号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="威富通秘钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "CFWX":
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"畅付账号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"畅付秘钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="畅付账号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="畅付秘钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "ZF":
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"智付账号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"智付私钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="智付账号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="智付私钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "WFTGZH":
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"威富通公众号账号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"威富通公众号秘钥\"},{\"fieldName\":\"wxyyid\",\"value\":\"" + item.l_str.Split(',')[2] + "\",\"label\":\"微信应用id\"},{\"fieldName\":\"wxyymy\",\"value\":\"" + item.l_str.Split(',')[3] + "\",\"label\":\"微信应用密钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="威富通公众号账号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="威富通公众号秘钥",
                                InputType="textarea"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName="wxyyid",
                                label="微信应用id"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName="wxyymy",
                                label="微信应用密钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "WFTAPP":
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"威富通应用账号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"威富通秘钥\"},{\"fieldName\":\"WFTAPPid\",\"value\":\"" + item.l_str.Split(',')[2] + "\",\"label\":\"威富通AppId\"},{\"fieldName\":\"WFTAPPbm\",\"value\":\"" + item.l_str.Split(',')[3] + "\",\"label\":\"威富通应用包名\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="威富通应用账号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="威富通秘钥",
                                InputType="textarea"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName="WFTAPPid",
                                label="威富通AppId"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName="WFTAPPbm",
                                label="威富通应用包名"
                            }
                        };
                        break;
                    case "MPAPI":
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str + "\",\"label\":\"明鹏渠道号\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="明鹏渠道号"
                            }
                        };
                        break;
                    case "BJXH":
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"北京星合\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"北京星合秘钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="北京星合"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="北京星合秘钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "BBZFB":
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"贝贝支付宝账号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"贝贝支付宝秘钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="贝贝支付宝账号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="贝贝支付宝秘钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "CFZFB":
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"畅付账号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"畅付秘钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="畅付账号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="畅付秘钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "JHFGZH":
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"聚合福公众号账号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"聚合福公众号私钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="聚合福公众号账号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="聚合福公众号私钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "NYGZH":
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"南粤公众号账号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"南粤公众号私钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="南粤公众号账号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="南粤公众号私钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "NYAPP":
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"南粤APP账号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"南粤APP私钥\"},{\"fieldName\":\"nywxapp\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"微信APP\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="南粤APP账号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="南粤APP私钥",
                                InputType="textarea"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName = "nywxapp",
                                label = "微信APP"
                            }
                        };
                        break;
                    case "SYZFB":
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"首游支付宝账号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"首游支付宝私钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="首游支付宝账号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="首游支付宝私钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "HBTWXSM":
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"汇宝通账号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"汇宝通私钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="汇宝通账号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="汇宝通私钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "ZYXAPP":
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"众易鑫账号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"众易鑫私钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="众易鑫账号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="众易鑫私钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "SYWAP":
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"首游账号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"首游私钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="首游账号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="首游私钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "SYAPPID":
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"首游账号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"首游私钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="首游账号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="首游私钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "WFTSM"://威富通微信扫码
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"威富通账号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"威富通私钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="威富通账号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="威富通私钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "WFTZFBSM"://威富通支付宝扫码
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"威富通账号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"威富通私钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="威富通账号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="威富通私钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "PYZFB"://鹏缘支付宝
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"鹏缘账号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"鹏缘私钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="鹏缘账号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="鹏缘私钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "SDWBZFB"://鹏缘支付宝
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"山东微保账号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"山东微保私钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="山东微保账号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="山东微保私钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "HYWX"://汇元微信
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"汇元微信账号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"汇元微信私钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="汇元微信账号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="汇元微信私钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "HYYL"://汇元银联
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"汇元银联账号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"汇元银联私钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="汇元银联账号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="汇元银联私钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "FFL"://发发啦appid
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"发发啦账号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"发发啦私钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="发发啦账号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="发发啦私钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "whdgzh"://微赢互动公众号
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"微赢互动商户号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"微赢互动私钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="微赢互动商户号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="微赢互动私钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "whdzfb"://微赢互动支付宝
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"微赢互动商户号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"微赢互动私钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="微赢互动商户号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="微赢互动私钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "skwxzfb"://思科无限支付宝
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"思科无限支付宝商户号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"思科无限支付宝私钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="思科无限支付宝商户号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="思科无限支付宝私钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "skwxappid"://思科无限appid
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"思科无限账号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"思科无限秘钥\"},{\"fieldName\":\"skwxappidid\",\"value\":\"" + item.l_str.Split(',')[2] + "\",\"label\":\"思科无限AppId\"},{\"fieldName\":\"skwxappidbm\",\"value\":\"" + item.l_str.Split(',')[3] + "\",\"label\":\"思科无限应用包名\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="思科无限账号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="思科无限秘钥",
                                InputType="textarea"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName="skwxappidid",
                                label="思科无限AppId"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName="skwxappidbm",
                                label="思科无限应用包名"
                            }
                        };
                        break;
                    case "xyyhappid"://兴业银行微信appid
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"兴业银行商户号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"兴业银行秘钥\"},{\"fieldName\":\"xyyhappidwxid\",\"value\":\"" + item.l_str.Split(',')[2] + "\",\"label\":\"微信appid\"},{\"fieldName\":\"wxyyappid\",\"value\":\"" + item.l_str.Split(',')[3] + "\",\"label\":\"应用id\"},{\"fieldName\":\"mdid\",\"value\":\"" + item.l_str.Split(',')[4] + "\",\"label\":\"门店id\"},{\"fieldName\":\"mdname\",\"value\":\"" + item.l_str.Split(',')[5] + "\",\"label\":\"门店名称\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="兴业银行商户号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="兴业银行秘钥",
                                InputType="textarea"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName="xyyhappidwxid",
                                label="微信appid"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName="wxyyappid",
                                label="应用id"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName="mdid",
                                label="门店id"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName="mdname",
                                label="门店名称"
                            }
                        };
                        break;
                    case "sfwxsm"://舒付微信扫码
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"舒付商户号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"舒付私钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="舒付商户号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="舒付私钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "wxhdwxsm"://舒付微信扫码
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"微赢互动商户号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"微赢互动私钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="微赢互动商户号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="微赢互动私钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "sywxgzh"://首游公众号
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"首游公众号商户号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"首游公众号私钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="首游公众号商户号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="首游公众号私钥",
                                InputType="textarea"
                            }
                        };
                        break;
                    case "nywxsm"://微信扫码
                        str = "[{\"fieldName\":\"" + item.p_extend + "\",\"value\":\"" + item.l_str.Split(',')[0] + "\",\"label\":\"南粤微信扫码账号\"},{\"fieldName\":\"" + my + "\",\"value\":\"" + item.l_str.Split(',')[1] + "\",\"label\":\"南粤微信扫码私钥\"}]";
                        configExt = new List<PaymentTypeConfigExt>
                        {
                            new PaymentTypeConfigExt
                            {
                                fieldName=item.p_extend,
                                label="南粤微信扫码账号"
                            },
                            new PaymentTypeConfigExt
                            {
                                fieldName=my,
                                label="南粤微信扫码私钥",
                                InputType="textarea"
                            }
                        };
                        break;
                }

                item.l_jsonstr = str;
                bll.Update(item);

                if (addNewPaymentTypeConfig)
                {
                    var models = ParsePaymentTypeConfigList(item, configExt);

                    foreach (var model in models)
                    {
                        var configBll = new JMP.BLL.jmp_payment_type_config();
                        configBll.Add(model);
                    }
                }
                success++;
                //TODO:SQL
            }
            return success;

        }
        /// <summary>
        /// 批量修改支付配置状态
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdateState()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            int state = string.IsNullOrEmpty(Request["state"]) ? 0 : Int32.Parse(Request["state"].ToString());
            string str = Request["ids"];
            string xgzfc = "";//组装说明
            string tsmsg = "";//提示
            JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
            if (str.CompareTo("On") > 0)
            {
                str = str.Substring(3);
            }
            if (state == 0)
            {
                //DataTable zfbdt = bll.SelectDataTable(str, 1);//查询支付宝信息
                DataTable wxdt = bll.SelectDataTable(str, 2);//查询微信信息

                if (wxdt.Rows.Count == 0)
                {
                    retJson = new { success = 0, msg = "支付配置每一个类型必须留一个不能冻结！" };
                    return Json(retJson);
                }
            }
            if (bll.UpdateLocUserState(str, state))
            {
                if (state == 1)
                {
                    xgzfc = "一键禁用ID为：" + str;
                    tsmsg = "启用成功";
                }
                else
                {
                    tsmsg = "禁用成功";
                    xgzfc = "一键禁用ID为：" + str;
                }
                Logger.OperateLog("支付接口配置一键启用或禁用", xgzfc);

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
            return Json(retJson);
        }
        /// <summary>
        /// 修改支付通道为可用
        /// </summary>
        /// <returns></returns>
        public JsonResult payUpdateSeate()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
            int lid = string.IsNullOrEmpty(Request["lid"]) ? 0 : Int32.Parse(Request["lid"]);
            if (lid > 0)
            {
                if (bll.UpdateLocUserState(lid.ToString(), 2))
                {

                    Logger.OperateLog("支付通道配置修改为可用", "支付通道配置修改为可用id为：" + lid);
                    retJson = new { success = 1, msg = "操作成功" };
                }
                else
                {
                    retJson = new { success = 0, msg = "操作失败" };
                }
            }
            return Json(retJson);
        }
        /// <summary>
        ///  支付通道管理
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult PaymenttypeList()
        {
            List<JMP.MDL.jmp_paymenttype> list = new List<JMP.MDL.jmp_paymenttype>();
            JMP.BLL.jmp_paymenttype bll = new JMP.BLL.jmp_paymenttype();
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            string sql = " select a.p_id,a.p_name,a.p_type,a.p_extend,a.CostRatio,b.p_name as zflxname,p_priority,p_forbidden,p_platform from jmp_paymenttype as a left join jmp_paymode b on a.p_type=b.p_id where 1=1 ";
            string Order = "order by p_id";

            list = bll.SelectListPage(sql, Order, pageIndexs, PageSize, out pageCount);
            ViewBag.list = list;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            return View();
        }

        /// <summary>
        /// 批量修改支付通道状态
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false), VisitRecord(IsRecord = true)]
        public JsonResult PaymentTypeUpdateState()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            int state = string.IsNullOrEmpty(Request["state"]) ? 0 : Int32.Parse(Request["state"].ToString());
            string str = Request["ids"];
            string xgzfc = "";//组装说明
            string tsmsg = "";//提示
            JMP.BLL.jmp_paymenttype bll = new JMP.BLL.jmp_paymenttype();

            if (bll.UpdatState(str, state))
            {
                if (state == 1)
                {
                    xgzfc = "结冻ID为：" + str;
                    tsmsg = "结冻成功";
                }
                else
                {
                    tsmsg = "启用成功";
                    xgzfc = "启用ID为：" + str;
                }

                Logger.OperateLog("支付通道配置一键启用或禁用", xgzfc);
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
                    tsmsg = "结冻失败";
                }
                retJson = new { success = 0, msg = tsmsg };
            }
            return Json(retJson);

        }
        /// <summary>
        /// 通道池弹窗
        /// </summary>
        /// <returns></returns>

        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false), VisitRecord(IsRecord = true)]
        public ActionResult ChannelPoolTC()
        {
            JMP.BLL.jmp_channel_pool bll = new JMP.BLL.jmp_channel_pool();
            List<jmp_channel_pool> list = new List<jmp_channel_pool>();
            string SelectName = string.IsNullOrEmpty(Request["SelectName"]) ? "" : Request["SelectName"];//选择后需要显示名称文本框
            ViewBag.SelectName = SelectName;
            string SelectId = string.IsNullOrEmpty(Request["SelectId"]) ? "" : Request["SelectId"];//选择后要显示id的文本框
            ViewBag.SelectId = SelectId;
            string judge = string.IsNullOrEmpty(Request["judge"]) ? "" : Request["judge"];//选择后验证css样式
            ViewBag.judge = judge;

            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);//查询条件选择
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            list = bll.SelectListTc(type, sea_name, pageIndexs, PageSize, out pageCount);
            ViewBag.type = type;
            ViewBag.sea_name = sea_name;
            ViewBag.list = list;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            return View();
        }

        /// <summary>
        /// 应用弹窗
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false), VisitRecord(IsRecord = true)]
        public ActionResult AppListTC()
        {
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            //  int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            int type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);//查询条件选择
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            int platformid = string.IsNullOrEmpty(Request["platformid"]) ? 0 : Int32.Parse(Request["platformid"]);//关联平台
            string appstr = string.IsNullOrEmpty(Request["appstr"]) ? "" : Request["appstr"];//已选择的应用id
            ViewBag.appstr = appstr;
            List<JMP.MDL.jmp_app> list = new List<JMP.MDL.jmp_app>();
            JMP.BLL.jmp_app bll = new JMP.BLL.jmp_app();
            List<JMP.MDL.jmp_app> listapp = new List<JMP.MDL.jmp_app>();
            string xzsql = "   select a.a_id,a.a_name,b.u_realname,b.u_id,a.a_user_id,a.a_auditstate,a.a_state,a.a_platform_id   from jmp_app a  left join jmp_user b on a.a_user_id=b.u_id   where a.a_state=1 and a.a_auditstate=1 and b.u_auditstate=1    and a_id  in(" + appstr + ")  ";
            DataTable dt = !string.IsNullOrEmpty(appstr) ? bll.selectsql(xzsql) : new DataTable();
            listapp = dt.Rows.Count > 0 ? JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_app>(dt) : new List<JMP.MDL.jmp_app>();
            ViewBag.listapp = listapp;
            string sql = "  select a.a_id,a.a_name,b.u_realname,b.u_id,a.a_user_id,a.a_auditstate,a.a_state,a.a_platform_id  from jmp_app a  left join jmp_user b on a.a_user_id=b.u_id  where a.a_state=1 and a.a_auditstate=1 and b.u_auditstate=1    ";
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        sql += " and   a.a_id=" + sea_name;//应用编号
                        break;
                    case 2:
                        sql += " and a.a_name like '%" + sea_name + "%' ";//应用名称
                        break;
                    case 3:
                        sql += " and b.u_realname like '%" + sea_name + "%' ";//用户名称
                        break;
                }
            }
            if (platformid > 0)
            {
                sql += " and a.a_platform_id=" + platformid;
            }
            if (!string.IsNullOrEmpty(appstr))
            {
                sql += "  and a_id not in(" + appstr + ") ";
            }
            string order = " order by a_state ";
            list = bll.SelectTClist(sql, order, pageIndexs, PageSize, out pageCount);
            ViewBag.type = type;
            ViewBag.sea_name = sea_name;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.list = list;
            ViewBag.platformid = platformid;
            return View();
        }
        /// <summary>
        /// 查询应用名称
        /// </summary>
        /// <param name="apptypeid">根据应用id</param>
        /// <returns></returns>
        public static string SelectAppName(string apptypeid)
        {
            JMP.BLL.jmp_app yybll = new JMP.BLL.jmp_app();
            string where = "  a_id in (" + apptypeid + ") and a_state='1' order by a_id desc";
            DataTable yydt = yybll.GetList(where).Tables[0];//获取应用类型在用信息
            string apptye = "";
            if (yydt.Rows.Count > 0)
            {
                for (int i = 0; i < yydt.Rows.Count; i++)
                {
                    apptye += yydt.Rows[i]["a_name"] + ",";
                }
            }
            if (apptye.Length > 0)
            {
                apptye = apptye.Substring(0, apptye.Length - 1);
            }
            return apptye;
        }
        /// <summary>
        /// 查询通道池名称
        /// </summary>
        /// <param name="apptypeid">根据应用id</param>
        /// <returns></returns>
        public static string SelectTdcName(string apptypeid)
        {
            JMP.BLL.jmp_channel_pool bll = new JMP.BLL.jmp_channel_pool();
            string where = "  id in (" + apptypeid + ")  order by id desc";
            string apptye = "";
            DataTable yydt = bll.GetList(where).Tables[0];
            if (yydt.Rows.Count > 0)
            {
                for (int i = 0; i < yydt.Rows.Count; i++)
                {
                    apptye += yydt.Rows[i]["PoolName"] + ",";
                }
            }
            if (apptye.Length > 0)
            {
                apptye = apptye.Substring(0, apptye.Length - 1);
            }
            return apptye;
        }

        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public ActionResult PaymentUpdate()
        {
            var pid = Convert.ToInt32(Request.QueryString["pid"] ?? "0");
            var model = new JMP.BLL.jmp_paymenttype().GetModel(pid);
            model.p_id = pid;
            return View(model);
        }

        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        [HttpPost]
        public ActionResult PaymentUpdate(FormCollection form)
        {
            var pid = Convert.ToInt32(form["pid"] ?? "0");
            var bll = new JMP.BLL.jmp_paymenttype();
            var model = bll.GetModel(pid);
            model.p_id = pid;
            model.p_priority = Convert.ToInt32(form["p_priority"]);
            model.p_platform = Request["p_platform"];
            bll.Update(model);
            return Json(new { success = true });
        }

        /// <summary>
        /// 支付参数管理列表
        /// </summary>
        /// <returns></returns>
        [VisitRecord(IsRecord = true)]
        public ActionResult typeconfiglist()
        {
            #region 获取操作权限
            string locUrl = "";
            bool getUidT = bll_limit.GetLocUserLimitVoids("/payment/Updatetypeconfig", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//本地管理员一键启用
            if (getUidT)
            {
                locUrl += " <li onclick=\"javascript:Updatestate(0)\"> <i class='fa fa-check-square-o'> </i> 一键启用</li>";
            }
            bool getUidF = bll_limit.GetLocUserLimitVoids("/payment/Updatetypeconfig", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//本地管理员一键禁用
            if (getUidF)
            {
                locUrl += "<li   onclick=\"javascript:Updatestate(1);\"> <i class='fa fa-check-square-o'> </i> 一键禁用</li>";
            }
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/payment/typeconfigAdd", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));//添加管理员
            if (getlocuserAdd)
            {
                locUrl += "<li    onclick=\"Addtypeconfig()\"> <i class='fa fa-plus'></i> 添加</li>";
            }
            ViewBag.locUrl = locUrl;
            #endregion

            #region  查询数据
            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            string sql = " select a.*,b.p_name as  paymenttypeName,c.p_id,c.p_name as paymodeName   from  [dbo].jmp_payment_type_config a left join [dbo].[jmp_paymenttype] b  on a.PaymentTypeId=b.p_id left join [dbo].[jmp_paymode] c on b.p_type=c.p_id where 1=1 ";
            int searchDesc = string.IsNullOrEmpty(Request["searchDesc"]) ? 0 : Int32.Parse(Request["searchDesc"]);//排序方式
            int SelectState = string.IsNullOrEmpty(Request["SelectState"]) ? -1 : Int32.Parse(Request["SelectState"]);//状态
            int auditstate = string.IsNullOrEmpty(Request["auditstate"]) ? 0 : Int32.Parse(Request["auditstate"]);//支付类型
            int type = string.IsNullOrEmpty(Request["type"]) ? 0 : Int32.Parse(Request["type"]);//查询条件选择
            string sea_name = string.IsNullOrEmpty(Request["sea_name"]) ? "" : Request["sea_name"];//查询条件内容
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        sql += " and  b.p_name='" + sea_name + "' ";
                        break;
                    case 2:
                        sql += " and a.Label like '%" + sea_name + "%' ";
                        break;
                    case 3:
                        sql += " and a.FieldName like '%" + sea_name + "%' ";
                        break;
                    case 4:
                        sql += " and a.InputType='" + sea_name + "'";
                        break;
                }
            }
            if (SelectState > -1)
            {
                sql += " and a.Status='" + SelectState + "' ";
            }
            if (auditstate > 0)
            {
                sql += " and c.p_id='" + auditstate + "' ";
            }
            string Order = "order by Id";
            if (searchDesc == 1)
            {
                Order = "  order by Id  ";
            }
            else
            {
                Order = " order by Id desc ";
            }
            JMP.BLL.jmp_payment_type_config bll = new JMP.BLL.jmp_payment_type_config();
            List<JMP.MDL.jmp_payment_type_config> list = new List<JMP.MDL.jmp_payment_type_config>();
            list = bll.SelectList(sql, Order, pageIndexs, PageSize, out pageCount);

            JMP.BLL.jmp_paymode yybll = new JMP.BLL.jmp_paymode();
            string wherepay = " p_state=1";
            DataTable yydt = yybll.GetList(wherepay).Tables[0];//获取应用类型在用信息
            List<JMP.MDL.jmp_paymode> yylist = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_paymode>(yydt);
            ViewBag.yylist = yylist;
            ViewBag.list = list;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            ViewBag.searchDesc = searchDesc;
            ViewBag.SelectState = SelectState;
            ViewBag.type = type;
            ViewBag.auditstate = auditstate;
            ViewBag.sea_name = sea_name;
            #endregion

            return View();
        }
        /// <summary>
        /// 根据支付通道查询参数信息
        /// </summary>
        /// <param name="typeid"></param>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public string SelectTypeConfig(int? typeid, string jsonstr, int? zftdid)
        {
            string data = "";
            if (typeid > 0)
            {
                JMP.BLL.jmp_payment_type_config bll = new JMP.BLL.jmp_payment_type_config();
                List<JMP.MDL.jmp_payment_type_config> list = new List<JMP.MDL.jmp_payment_type_config>();
                list = bll.SelectPaymentTypeId((int)typeid);
                StringBuilder html = new StringBuilder();
                if (list.Count > 0)
                {
                    if (!string.IsNullOrEmpty(jsonstr) && zftdid == typeid)
                    {

                        List<WEB.Models.PaymentTypeConfig> jsonlist = JMP.TOOL.JsonHelper.Deserialize<List<WEB.Models.PaymentTypeConfig>>(jsonstr);

                        foreach (var item in list)
                        {
                            string yzid = item.FieldName + "yy";
                            foreach (var m in jsonlist)
                            {
                                if (m.fieldName == item.FieldName)
                                {
                                    if (item.InputType == "text")
                                    {
                                        data += "<dl><dt>" + item.Label + "：</dt><dd><div class='single-input normal'><input type ='text'  id='" + item.FieldName + "' data-label='" + item.Label + "'  name='payconfig'   value='" + m.value + "' /></div> <div class='Validform_checktip' id='" + yzid + "'>*" + item.Label + "</div></dd> </dl>";
                                    }
                                    else
                                    {
                                        data += "<dl><dt>" + item.Label + "：</dt><dd><div class='single-input normal'> <textarea id = '" + item.FieldName + "' name='payconfig' data-label='" + item.Label + "'  style = 'width: 400px; height: 100px;'>" + m.value + "</textarea></div> <div class='Validform_checktip' id='" + yzid + "'>*" + item.Label + "</div></dd> </dl>";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var item in list)
                        {
                            string yzid = item.FieldName + "yy";
                            if (item.InputType == "text")
                            {
                                data += "<dl><dt>" + item.Label + "：</dt><dd><div class='single-input normal'><input type ='text'  id='" + item.FieldName + "' data-label='" + item.Label + "'  name='payconfig'   value='' /></div> <div class='Validform_checktip' id='" + yzid + "'>*" + item.Label + "</div></dd> </dl>";
                            }
                            else
                            {
                                data += "<dl><dt>" + item.Label + "：</dt><dd><div class='single-input normal'> <textarea id = '" + item.FieldName + "' name='payconfig' data-label='" + item.Label + "'  style = 'width: 400px; height: 100px;'></textarea></div> <div class='Validform_checktip' id='" + yzid + "'>*" + item.Label + "</div></dd> </dl>";
                            }
                        }
                    }

                }
            }
            return data;
        }

        /// <summary>
        /// 支付参数管理添加
        /// </summary>
        /// <returns></returns>
        public ActionResult typeconfigAdd()
        {
            JMP.BLL.jmp_paymode bl = new JMP.BLL.jmp_paymode();
            JMP.MDL.jmp_payment_type_config mo = new JMP.MDL.jmp_payment_type_config();
            JMP.BLL.jmp_payment_type_config blls = new JMP.BLL.jmp_payment_type_config();
            List<JMP.MDL.jmp_paymode> paylist = JMP.TOOL.MdlList.ToList<JMP.MDL.jmp_paymode>(bl.GetList(" 1=1 and p_state=1 ").Tables[0]);
            int id = string.IsNullOrEmpty(Request["id"]) ? 0 : Int32.Parse(Request["id"]);

            if (id > 0)
            {
                mo = blls.GetModels(id);
            }
            ViewBag.mo = mo;
            ViewBag.paylist = paylist;
            return View();
        }
        /// <summary>
        /// 添加或修改支付参数配置
        /// </summary>
        /// <param name="mo"></param>
        /// <returns></returns>
        public JsonResult typeconfigAddOrUpdate(JMP.MDL.jmp_payment_type_config mo)
        {
            object retJson = new { success = 0, msg = "操作失败" };
            JMP.BLL.jmp_payment_type_config bll = new JMP.BLL.jmp_payment_type_config();
            if (mo.Id > 0)
            {
                JMP.MDL.jmp_payment_type_config mod = bll.GetModel(mo.Id);
                mod.PaymentTypeId = mo.PaymentTypeId;
                mod.Label = mo.Label;
                mod.InputType = mo.InputType;
                mod.FieldName = mo.FieldName;
                mod.InputType = mo.InputType;
                mod.Description = mo.Description;

                // mo.CreatedBy = mod.CreatedBy;
                // mo.CreatedOn = mod.CreatedOn;
                // mo.Status = mod.Status;
                //string xgsm = "";
                if (bll.Update(mo))
                {

                    Logger.ModifyLog("修改支付配置参数", mod, mo);
                    retJson = new { success = 1, msg = "修改成功！" };
                }
                else
                {
                    retJson = new { success = 0, msg = "修改失败！" };
                }
            }
            else
            {
                mo.Status = 0;
                mo.CreatedBy = UserInfo.UserName;
                int cg = bll.Add(mo);
                if (cg > 0)
                {
                    //日志
                    Logger.CreateLog("添加支付配置参数", mo);

                    retJson = new { success = 1, msg = "添加成功！" };
                }
                else
                {
                    retJson = new { success = 0, msg = "添加失败！" };
                }
            }
            return Json(retJson);
        }

        /// <summary>
        /// 批量修改支付配置状态
        /// </summary>
        /// <returns></returns>
        public JsonResult Updatetypeconfig()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            int state = string.IsNullOrEmpty(Request["state"]) ? 0 : Int32.Parse(Request["state"].ToString());
            string str = Request["ids"];
            string xgzfc = "";//组装说明
            string tsmsg = "";//提示
            JMP.BLL.jmp_payment_type_config bll = new JMP.BLL.jmp_payment_type_config();
            if (str.CompareTo("On") > 0)
            {
                str = str.Substring(3);
            }
            if (bll.UpdateLocUserState(str, state))
            {
                if (state == 1)
                {
                    xgzfc = "一键结冻ID为：" + str;
                    tsmsg = "结冻成功";
                }
                else
                {
                    tsmsg = "启用成功";
                    xgzfc = "一键启用ID为：" + str;
                }

                Logger.OperateLog("支付接口配置一键启用或结冻", xgzfc);
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
                    tsmsg = "结冻失败";
                }
                retJson = new { success = 0, msg = tsmsg };
            }
            return Json(retJson);
        }

        #endregion

        #region 设置通道成本费率

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult PayMenttypeCostRatio()
        {
            int id = string.IsNullOrEmpty(Request["lid"]) ? 0 : int.Parse(Request["lid"]);

            JMP.MDL.jmp_interface model = new jmp_interface();
            JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();

            //获取一个实体对象
            model = bll.GetModel(id);

            ViewBag.JmpInterface = model;

            return View();
        }

        /// <summary>
        /// 修改通道成本费率
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false)]
        public JsonResult UpdatePayCostRatio()
        {
            JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
            JMP.MDL.jmp_interface model = new JMP.MDL.jmp_interface();

            object retJson = new { success = 0, msg = "操作失败" };

            int pid = string.IsNullOrEmpty(Request["pid"]) ? 0 : int.Parse(Request["pid"]);
            string CostRatio = string.IsNullOrEmpty(Request["CostRatio"]) ? "0" : Request["CostRatio"];

            //获取一个实体对象
            model = bll.GetModel(pid);

            if (bll.UpdateInterfaceCostRatio(pid, CostRatio))
            {
                //记录日志（会定期清理）
                Logger.OperateLog("修改通道成本费率", "操作数据ID：" + pid + ",成本费率由：" + model.l_CostRatio + ",改为：" + CostRatio + "。");
                //记录日志（不会清理）
                RateLogger.OperateLog("修改通道成本费率", "操作数据ID：" + pid + ",成本费率由：" + model.l_CostRatio + ",改为：" + CostRatio + "。");

                retJson = new { success = 1, msg = "设置通道成本费率成功" };
            }

            else
            {
                retJson = new { success = 0, msg = "设置通道成本费率失败" };
            }

            return Json(retJson);
        }

        #endregion

        #region 风险等级配置
        public ActionResult RisklevelList()
        {
            string locUrl = "";
            #region 获取按钮操作权限
            bool getlocuserAdd = bll_limit.GetLocUserLimitVoids("/payment/RisklevelAddOrUpdate", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));
            if (getlocuserAdd)
            {
                locUrl += "<li onclick=\"AddRisklevel()\"><i class='fa fa-plus'></i>添加风险配置</li>";
            }
            bool getlocuserState = bll_limit.GetLocUserLimitVoids("/payment/RisklevelUpdateState", UserInfo.UserId.ToString(), int.Parse(UserInfo.UserRoleId.ToString()));
            if (getlocuserAdd)
            {
                locUrl += "<li onclick=\"Updatestate(0)\"><i class='fa fa-plus'></i>一键启用</li>";
                locUrl += "<li onclick=\"Updatestate(1)\"><i class='fa fa-plus'></i>一键禁用</li>";
            }
            ViewBag.locUrl = locUrl;
            #endregion
            //查询风险等级
            List<jmp_risklevel> rilist = new List<jmp_risklevel>();
            JMP.BLL.jmp_risklevel ribll = new JMP.BLL.jmp_risklevel();
            rilist = ribll.GetModelList("");
            ViewBag.rilist = rilist;
            //查询应用类型
            List<jmp_apptype> applist = new List<jmp_apptype>();
            JMP.BLL.jmp_apptype appbll = new JMP.BLL.jmp_apptype();
            applist = appbll.GetModelList(" t_topid='0' and t_state='1' order by t_id desc ");
            ViewBag.applist = applist;

            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int apptypeid = string.IsNullOrEmpty(Request["apptypeid"]) ? 0 : Int32.Parse(Request["apptypeid"]); //应用类型id
            ViewBag.apptypeid = apptypeid;
            int risklevelid = string.IsNullOrEmpty(Request["risklevelid"]) ? 0 : Int32.Parse(Request["risklevelid"]);//风险等级id
            ViewBag.risklevelid = risklevelid;
            int state = string.IsNullOrEmpty(Request["state"]) ? -1 : Int32.Parse(Request["state"]);//状态
            ViewBag.state = state;
            List<jmp_risklevelallocation> list = new List<jmp_risklevelallocation>();
            JMP.BLL.jmp_risklevelallocation bll = new JMP.BLL.jmp_risklevelallocation();
            string sql = " select a.r_id,a.r_apptypeid,a.r_risklevel,a.r_state,b.t_name,c.r_name from jmp_risklevelallocation a left join  jmp_apptype b on a.r_apptypeid = b.t_id left join  jmp_risklevel c on a.r_risklevel = c.r_id ";
            string order = "  order by  r_id desc ";
            string where = " where 1=1 ";
            if (apptypeid > 0)
            {
                where += " and  a.r_apptypeid=" + apptypeid;
            }
            if (risklevelid > 0)
            {
                where += " and a.r_risklevel=" + risklevelid;
            }
            if (state > -1)
            {
                where += " and a.r_state=" + state;
            }
            sql = sql + where;
            list = bll.SelectPage(sql, order, pageIndexs, PageSize, out pageCount);
            ViewBag.list = list;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            return View();
        }
        /// <summary>
        /// 添加或修改风险配置界面
        /// </summary>
        /// <returns></returns>
        public ActionResult RisklevelAddOrUpdate()
        {
            int rid = string.IsNullOrEmpty(Request["rid"]) ? 0 : Int32.Parse(Request["rid"]);
            List<jmp_risklevel> list = new List<jmp_risklevel>();
            JMP.BLL.jmp_risklevel bll = new JMP.BLL.jmp_risklevel();
            list = bll.GetModelList("");
            ViewBag.list = list;
            List<jmp_apptype> applist = new List<jmp_apptype>();
            JMP.BLL.jmp_apptype appbll = new JMP.BLL.jmp_apptype();
            applist = appbll.GetModelList(" t_topid='0' and t_state='1' order by t_id desc ");
            ViewBag.applist = applist;
            jmp_risklevelallocation mo = new jmp_risklevelallocation();
            JMP.BLL.jmp_risklevelallocation blls = new JMP.BLL.jmp_risklevelallocation();
            if (rid > 0)
            {
                mo = blls.GetModel(rid);
            }
            ViewBag.mo = mo;
            return View();
        }
        /// <summary>
        /// 添加或修改风险配置
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public JsonResult AddRisklevel(jmp_risklevelallocation mode)
        {
            object retJson = new { success = 0, msg = "操作失败" };
            JMP.BLL.jmp_risklevelallocation bll = new JMP.BLL.jmp_risklevelallocation();
            DataTable dt = new DataTable();
            string where = " r_apptypeid = " + mode.r_apptypeid + " and r_risklevel = " + mode.r_risklevel;
            where = mode.r_id > 0 ? where + " and r_id <> " + mode.r_id + "  " : where;
            dt = bll.GetList(where).Tables[0];
            if (dt.Rows.Count <= 0)
            {
                if (mode.r_id > 0)
                {
                    jmp_risklevelallocation mo = new jmp_risklevelallocation();
                    mo = bll.GetModel(mode.r_id);
                    mo.r_apptypeid = mode.r_apptypeid;
                    mo.r_risklevel = mode.r_risklevel;
                    if (bll.Update(mode))
                    {
                        Logger.ModifyLog("修改支付配置", mo, mode);
                        retJson = new { success = 1, msg = "修改成功！" };
                    }
                    else
                    {
                        retJson = new { success = 0, msg = "修改失败！" };
                    }
                }
                else
                {
                    int cg = bll.Add(mode);
                    if (cg > 0)
                    {
                        //记录日志
                        Logger.CreateLog("添加风险配置", mode);
                        retJson = new { success = 1, msg = "添加成功！" };
                    }
                    else
                    {
                        retJson = new { success = 0, msg = "添加失败！" };
                    }
                }
            }
            else
            {
                retJson = new { success = 0, msg = "数据库中已存在改数据！" };
            }
            return Json(retJson);
        }
        /// <summary>
        /// 一键禁用或启用
        /// </summary>
        /// <returns></returns>
        public JsonResult RisklevelUpdateState()
        {
            object retJson = new { success = 0, msg = "操作失败" };
            int state = string.IsNullOrEmpty(Request["state"]) ? 0 : Int32.Parse(Request["state"].ToString());
            string str = Request["ids"];
            string xgzfc = "";//组装说明
            string tsmsg = "";//提示
            JMP.BLL.jmp_risklevelallocation bll = new JMP.BLL.jmp_risklevelallocation();
            if (str.CompareTo("On") > 0)
            {
                str = str.Substring(3);
            }
            if (bll.UpdateState(str, state))
            {
                if (state == 1)
                {
                    xgzfc = "一键结冻ID为：" + str;
                    tsmsg = "结冻成功";
                }
                else
                {
                    tsmsg = "启用成功";
                    xgzfc = "一键启用ID为：" + str;
                }

                Logger.OperateLog("风险等级", xgzfc);
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
                    tsmsg = "结冻失败";
                }
                retJson = new { success = 0, msg = tsmsg };
            }
            return Json(retJson);
        }
        /// <summary>
        /// 风险等级配置弹窗
        /// </summary>
        /// <returns></returns>
        [LoginCheckFilterAttribute(IsCheck = true, IsRole = false), VisitRecord(IsRecord = true)]
        public ActionResult RisklevelListTc()
        {
            //查询风险等级
            List<jmp_risklevel> rilist = new List<jmp_risklevel>();
            JMP.BLL.jmp_risklevel ribll = new JMP.BLL.jmp_risklevel();
            rilist = ribll.GetModelList("");
            ViewBag.rilist = rilist;
            //查询应用类型
            List<jmp_apptype> applist = new List<jmp_apptype>();
            JMP.BLL.jmp_apptype appbll = new JMP.BLL.jmp_apptype();
            applist = appbll.GetModelList(" t_topid='0' and t_state='1' order by t_id desc ");
            ViewBag.applist = applist;

            int pageCount = 0;
            int pageIndexs = string.IsNullOrEmpty(Request["pageIndexs"]) ? 1 : Int32.Parse(Request["pageIndexs"]);//当前页
            int PageSize = string.IsNullOrEmpty(Request["PageSize"]) ? 20 : Int32.Parse(Request["PageSize"]);//每页显示数量
            int apptypeid = string.IsNullOrEmpty(Request["apptypeid"]) ? 0 : Int32.Parse(Request["apptypeid"]); //应用类型id
            ViewBag.apptypeid = apptypeid;
            int risklevelid = string.IsNullOrEmpty(Request["risklevelid"]) ? 0 : Int32.Parse(Request["risklevelid"]);//风险等级id
            ViewBag.risklevelid = risklevelid;
            string appstr = string.IsNullOrEmpty(Request["appstr"]) ? "" : Request["appstr"];//已选择的应用id
            ViewBag.appstr = appstr;
            //int state = string.IsNullOrEmpty(Request["state"]) ? -1 : Int32.Parse(Request["state"]);//状态
            //ViewBag.state = state;
            List<jmp_risklevelallocation> yxlist = new List<jmp_risklevelallocation>();//已选择的查询结果集合
            List<jmp_risklevelallocation> list = new List<jmp_risklevelallocation>();
            JMP.BLL.jmp_risklevelallocation bll = new JMP.BLL.jmp_risklevelallocation();


            DataTable dt = !string.IsNullOrEmpty(appstr) ? bll.SelectRid(appstr) : new DataTable();
            yxlist = dt.Rows.Count > 0 ? JMP.TOOL.MdlList.ToList<jmp_risklevelallocation>(dt) : new List<jmp_risklevelallocation>();
            ViewBag.yxlist = yxlist;
            string sql = " select a.r_id,a.r_apptypeid,a.r_risklevel,a.r_state,b.t_name,c.r_name from jmp_risklevelallocation a left join  jmp_apptype b on a.r_apptypeid = b.t_id left join  jmp_risklevel c on a.r_risklevel = c.r_id ";
            string order = "  order by  r_id desc ";
            string where = " where 1=1 ";
            if (apptypeid > 0)
            {
                where += " and  a.r_apptypeid=" + apptypeid;
            }
            if (risklevelid > 0)
            {
                where += " and a.r_risklevel=" + risklevelid;
            }
            where += " and a.r_state=0 ";
            if (!string.IsNullOrEmpty(appstr))
            {
                where += "  and a.r_id not in(" + appstr + ") ";
            }
            sql = sql + where;
            list = bll.SelectPage(sql, order, pageIndexs, PageSize, out pageCount);
            ViewBag.list = list;
            ViewBag.pageIndexs = pageIndexs;
            ViewBag.PageSize = PageSize;
            ViewBag.pageCount = pageCount;
            return View();
        }
        #endregion
    }
}
