using DxPay.LogManager.LogFactory.ApiLog;
using swiftpass.utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JMALI
{
    public partial class PfWxGzh : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int oid = !string.IsNullOrEmpty(Request["pid"]) ? Convert.ToInt32(Request["pid"].ToString()) : 0; //订单表ID
                if (oid > 0)
                {
                    //获取缓存
                    try
                    {
                        string code = !string.IsNullOrEmpty(Request["code"]) ? Request["code"] : "";
                        if (!string.IsNullOrEmpty(code))
                        {
                            string url = "";
                            System.Threading.Thread.Sleep(new Random().Next(100, 500));
                            if (JMP.TOOL.CacheHelper.IsCache(oid.ToString()) == false)
                            {
                                url = "/pfwxgzhorder" + oid + ".html?code=" + code;
                                JMP.TOOL.CacheHelper.CacheObject(url, oid.ToString(), 1);//存入缓存
                            }
                            else
                            {
                                url = JMP.TOOL.CacheHelper.GetCaChe<string>(oid.ToString());
                            }
                            Response.Redirect(url, false);
                        }
                        else
                        {
                            string appid = "";
                            JMP.MDL.jmp_app mo = new JMP.MDL.jmp_app();
                            JMP.BLL.jmp_app blls = new JMP.BLL.jmp_app();
                            mo = JMP.TOOL.MdlList.ToModel<JMP.MDL.jmp_app>(blls.GetList(" a_id=(SELECT o_app_id FROM jmp_order WHERE o_id=" + oid + ")  ").Tables[0]);
                            int pay_id = SelectUserInfo(mo.a_rid, mo.a_id);
                            JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
                            if (pay_id > 0)
                            {
                                if (bll.UpdatePay(oid, pay_id))
                                {
                                    string ddjj = Get_paystr(pay_id.ToString());
                                    appid = ddjj.ToString().Split(',')[2];
                                    string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + appid + "&redirect_uri=" + ConfigurationManager.AppSettings["redirecturipf"].ToString() + oid + ".html&response_type=code&scope=snsapi_base&state=1#wechat_redirect";
                                    Response.Redirect(url, false);
                                }
                            }
                            else
                            {
                                Response.Write("非法访问！");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + ex.Message, summary: "浦发银行公众号支付接口错误信息", channelId: oid);
                        Response.Write("非法访问！");
                    }
                }
                else
                {
                    Response.Write("非法访问！");
                }
            }
        }
        /// <summary>
        /// 获取支付字符串
        /// </summary>
        /// <param name="pid"> 支付通道ID</param>
        /// <returns></returns>
        string Get_paystr(string pid)
        {
            string pfwxgghzfhc = "pfwxgghzfhc" + pid.ToString();
            if (JMP.TOOL.CacheHelper.IsCache(pfwxgghzfhc))
            {
                object ddjj = JMP.TOOL.CacheHelper.GetCaChe(pfwxgghzfhc);
                return ddjj.ToString();
            }
            else
            {

                object ddjj = JMP.DBA.DbHelperSQL.GetSingle("SELECT l_str FROM jmp_interface WHERE l_id=" + pid + "");
                if (ddjj != null)
                {
                    JMP.TOOL.CacheHelper.CacheObject(pfwxgghzfhc, ddjj, 5);//存入缓存
                    return ddjj.ToString();
                }
                else
                {
                    return "";
                }
            }

        }

        /// <summary>
        /// 获取浦发账号信息 截取到通道id
        /// </summary>
        /// <param name="cache">缓存key</param>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private int SelectUserInfo(int apptype, int appid)
        {
            int PayId = 0;
            string cache = "pfyhwxgzhpay" + appid;
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(cache))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(cache);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        PayId = Int32.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype, channelId: PayId);
                    }
                }
                else
                {
                    dt = bll.SelectPay("PFWXGZH", apptype, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        PayId = Int32.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, int.Parse(ConfigurationManager.AppSettings["CacheTime"].ToString()));//存入缓存
                    }
                    else
                    {
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype, channelId: PayId);
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog(bcxx, summary:"浦发银行微信公众号支付接口错误", channelId: PayId);
            }
            return PayId;
        }


    }
}