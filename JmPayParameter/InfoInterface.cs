using JMP.MDL;
using JMP.TOOL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxPay.LogManager.LogFactory.ApiLog;

namespace JmPayParameter
{
    /// <summary>
    /// 初始化接口
    /// </summary>
    public class InfoInterface
    {
        /// <summary>
        /// 初始化接口主通道
        /// </summary>
        /// <param name="mode">参数实体</param>
        /// <param name="json">参数json字符串</param>
        /// <param name="CacheTime">缓存时间</param>
        /// <param name="ip">ip地址</param>
        /// <returns></returns>
        public  InnerResponse InfoPass(JmPayParameter.Models.Initialization mode, string json, int CacheTime, string ip)
        {
            InnerResponse Inn = new InnerResponse();
            Inn = VerificationParameter(mode, json);
            if (Inn.Success == true)
            {
                if (mode.t_appid > 0)
                {
                    SelectAPP selectAPP = new SelectAPP();
                    JMP.MDL.jmp_app app = selectAPP.SelectAppId(mode.t_appid, CacheTime);
                    if (app != null)
                    {
                        jmp_terminal model = EntityEvaluation(mode, ip);
                        if (mode != null)
                        {
                            if (mode.t_isnew == 1)
                            {
                                Inn = Insertliveteral(model);
                            }
                            else
                            {
                                Inn = InsertNew(model);
                            }
                        }
                        else
                        {
                            Inn = Inn.InfoToResponse(InfoErrorCode.Code9998);
                        }
                    }
                    else
                    {
                        Inn = Inn.InfoToResponse(InfoErrorCode.Code9996);
                    }
                }
                else
                {
                    Inn = Inn.InfoToResponse(InfoErrorCode.Code9996);
                }

            }
            return Inn;
        }
        /// <summary>
        /// 验证参数是否合法
        /// </summary>
        /// <param name="mode">参数实体</param>
        /// <param name="json">获取到的json字符串</param>
        /// <returns></returns>
        private  InnerResponse VerificationParameter(JmPayParameter.Models.Initialization mode, string json)
        {
            InnerResponse Inn = new InnerResponse();
            try
            {
                if (string.IsNullOrEmpty(mode.t_key) || mode.t_key.Length > 64)
                {
                    return Inn = Inn.InfoToResponse(InfoErrorCode.Code9997);
                }
                if (string.IsNullOrEmpty(mode.t_mark) || mode.t_mark.Length > 32)
                {
                    return Inn = Inn.InfoToResponse(InfoErrorCode.Code9995);
                }
                if (string.IsNullOrEmpty(mode.t_imsi) || mode.t_imsi.Length > 32)
                {
                    return Inn = Inn.InfoToResponse(InfoErrorCode.Code9994);
                }
                if (string.IsNullOrEmpty(mode.t_brand) || mode.t_brand.Length > 32)
                {
                    return Inn = Inn.InfoToResponse(InfoErrorCode.Code9993);
                }
                if (string.IsNullOrEmpty(mode.t_system) || mode.t_system.Length > 32)
                {
                    return Inn = Inn.InfoToResponse(InfoErrorCode.Code9992);
                }
                if (string.IsNullOrEmpty(mode.t_hardware) || mode.t_hardware.Length > 32)
                {
                    return Inn = Inn.InfoToResponse(InfoErrorCode.Code9991);
                }
                if (string.IsNullOrEmpty(mode.t_sdkver) || mode.t_sdkver.Length > 16)
                {
                    return Inn = Inn.InfoToResponse(InfoErrorCode.Code9990);
                }
                if (string.IsNullOrEmpty(mode.t_screen) || mode.t_screen.Length > 16)
                {
                    return Inn = Inn.InfoToResponse(InfoErrorCode.Code9989);
                }
                if (string.IsNullOrEmpty(mode.t_network) || mode.t_network.Length > 16)
                {
                    return Inn = Inn.InfoToResponse(InfoErrorCode.Code9988);
                }
                Inn = Inn.InfoToResponse(InfoErrorCode.Code100);
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString() + "报错位置：" + e.StackTrace.ToString();//报错信息
                //AddLocLog.AddLog(1, 4, "", "接口错误信息", "报错信息：支付接口验证参数错误,获取到的参数：" + json + ",报错信息：" + bcxx);//写入报错日志
                PayApiGlobalErrorLogger.Log("报错信息：支付接口验证参数错误,获取到的参数：" + json + ",报错信息：" + bcxx,summary: "接口错误信息");
                Inn = Inn.InfoToResponse(InfoErrorCode.Code9998);
            }
            return Inn;
        }
        /// <summary>
        /// 赋值到对应的实体
        /// </summary>
        /// <param name="mod"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        private  jmp_terminal EntityEvaluation(JmPayParameter.Models.Initialization mod, string ip)
        {
            jmp_terminal model = new jmp_terminal();
            model.t_key = mod.t_key;
            model.t_appid = mod.t_appid;
            model.t_mark = mod.t_mark;
            model.t_network = mod.t_network;
            model.t_ip = string.IsNullOrEmpty(ip) ? "" : ip;
            string ipstr = ConfigurationManager.AppSettings["ipkstr"].ToString();//获取ip库文件地址
            model.t_province = JMP.TOOL.IpProvince.IpAddress(model.t_ip, ipstr);
            model.t_imsi = mod.t_imsi;
            if (model.t_imsi == "404")
            {
                model.t_nettype = "其他";
            }
            else
            {
                if (model.t_imsi.Length >= 5)
                {
                    string wlxx = model.t_imsi.Substring(0, 5);
                    if (wlxx == "46000" || wlxx == "46002" || wlxx == "46007" || wlxx == "46020")
                    {
                        model.t_nettype = "移动";
                    }
                    else if (wlxx == "46003" || wlxx == "46005" || wlxx == "46011")
                    {
                        model.t_nettype = "电信";
                    }
                    else if (wlxx == "46001" || wlxx == "46006" || wlxx == "46010")
                    {
                        model.t_nettype = "联通";
                    }
                    else
                    {
                        model.t_nettype = "其他";
                    }
                }
                else
                {
                    model.t_nettype = "其他";
                }
            }
            model.t_brand = mod.t_brand;
            model.t_system = mod.t_system;
            model.t_hardware = mod.t_hardware;
            model.t_screen = mod.t_screen;
            model.t_sdkver = mod.t_sdkver;
            return model;
        }
        /// <summary>
        ///添加到终端属性表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private  InnerResponse InsertNew(jmp_terminal model)
        {
            InnerResponse Inn = new InnerResponse();
            try
            {
                model.t_time = DateTime.Now;
                int cg = 0;
                JMP.BLL.jmp_terminal zdbll = new JMP.BLL.jmp_terminal();//终端属性业务逻辑层
                cg = zdbll.Add(model);
                if (cg > 0)
                {
                    Inn = Inn.InfoToResponse(InfoErrorCode.Code100);
                }
                else
                {
                    Inn = Inn.InfoToResponse(InfoErrorCode.Code101);
                }
            }
            catch (Exception e)
            {

                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString() + "报错位置：" + e.StackTrace.ToString();//报错信息
                //AddLocLog.AddLog(1, 4, "", "接口错误信息", "添加到终端属性表,报错信息：" + bcxx);//写入报错日志
                PayApiGlobalErrorLogger.Log("添加到终端属性表,报错信息：" + bcxx,summary: "接口错误信息");
                Inn = Inn.InfoToResponse(InfoErrorCode.Code101);
            }

            return Inn;
        }
        /// <summary>
        /// 添加到活跃表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private  InnerResponse Insertliveteral(jmp_terminal model)
        {
            InnerResponse Inn = new InnerResponse();
            try
            {
                JMP.MDL.jmp_liveteral mo = new JMP.MDL.jmp_liveteral();//活跃实体类型
                JMP.BLL.jmp_liveteral hybll = new JMP.BLL.jmp_liveteral();//活跃业务逻辑层
                mo.l_teral_key = model.t_key;
                mo.l_time = DateTime.Now;
                mo.l_appid = model.t_appid;
                int cg = 0;
                cg = hybll.Add(mo);
                if (cg > 0)
                {
                    Inn = Inn.InfoToResponse(InfoErrorCode.Code100);
                }
                else
                {
                    Inn = Inn.InfoToResponse(InfoErrorCode.Code101);
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString() + "报错位置：" + e.StackTrace.ToString();//报错信息
                //AddLocLog.AddLog(1, 4, "", "接口错误信息", "添加到活跃表,报错信息：" + bcxx);//写入报错日志
                PayApiGlobalErrorLogger.Log("添加到活跃表,报错信息：" + bcxx, summary: "接口错误信息");
                Inn = Inn.InfoToResponse(InfoErrorCode.Code101);
            }

            return Inn;
        }
    }
}
