using JMP.TOOL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace JMWBSR
{
    /// <summary>
    /// Info 的摘要说明
    /// </summary>
    public class Info : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request.Params.Count > 0 && (context.Request.Params["Info"] != null && context.Request.Params["Info"] != ""))
            {
                string encryption = context.Request.Params["Info"].ToString();
                string ip = context.Request.Params["HTTP_X_FORWARDED_FOR"].ToString();
                if (encryption.Length > 100 && encryption.Contains("t_appkey"))
                {
                    try
                    {
                        ModelZd zd = new ModelZd();
                        zd = JMP.TOOL.JsonHelper.Deserializes<ModelZd>(encryption);
                        string codeAppid = zd.t_appkey + HttpContext.Current.Request.UserHostAddress;
                        string str = "";
                        if (JMP.TOOL.CacheHelper.IsCache(codeAppid))//判读是否存在缓存
                        {
                            //strcode = JMP.TOOL.CacheHelper.GetCaChe<string>(codename);//获取缓存
                            context.Response.Write("非法请求");
                        }
                        else
                        {
                            JMP.TOOL.CacheHelper.CacheObjectLocak<string>(codeAppid, codeAppid, 5);//存入缓存
                            str = InfoInterface(zd, encryption, ip);
                            context.Response.Write(str);
                        }
                    }
                    catch (Ex e)
                    {
                        string json = "";
                        //if (e is Ex)
                        //{
                        //    json = e.Message;
                        //}
                        //else
                        //{
                        //    string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString() + "报错位置：" + e.StackTrace.ToString();//报错信息
                        //    string bccsxxts = "终端属性请求数据：" + encryption;//报错信息
                        //  //  AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "接口错误信息", "报错信息：" + bcxx + "，" + bccsxxts);//写入报错日志
                        //    json = "{\"Name\":\"参数出错\",\"result\":9998}";
                        //}
                        json = "{\"Name\":\"参数出错\",\"result\":9998}";
                        context.Response.Write(json);
                    }
                }
                else
                {
                    context.Response.Write("非法请求");
                }
            }
            else
            {
                context.Response.Write("非法请求");
            }
        }
        /// <summary>
        /// 初始化接口
        /// </summary>
        /// <param name="encryption"></param>
        /// <returns></returns>
        public string InfoInterface(ModelZd zd, string encryption, string ip)
        {
            object json = "";
            // ModelZd zd = new ModelZd();
            JMP.MDL.jmp_terminal model = new JMP.MDL.jmp_terminal();//终端属性实体类型
            JMP.MDL.jmp_liveteral mo = new JMP.MDL.jmp_liveteral();//活跃实体类型
            JMP.BLL.jmp_terminal zdbll = new JMP.BLL.jmp_terminal();//终端属性业务逻辑层
            JMP.BLL.jmp_liveteral hybll = new JMP.BLL.jmp_liveteral();//活跃业务逻辑层
                                                                      //   zd = JMP.TOOL.JsonHelper.Deserializes<ModelZd>(encryption);
            #region 处理初始化数据
            if (zd != null)
            {
                #region 判断参数
                model.t_key = zd.t_key;
                JMP.BLL.jmp_app appbll = new JMP.BLL.jmp_app();//应用业务逻辑层
                DataTable dt = new DataTable();
                if (JMP.TOOL.CacheHelper.IsCache(zd.t_appkey))//判读是否存在缓存
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(zd.t_appkey);//获取缓存
                    model.t_appid = string.IsNullOrEmpty(dt.Rows[0]["a_id"].ToString()) ? 0 : Int32.Parse(dt.Rows[0]["a_id"].ToString());
                    model.t_appkey = zd.t_appkey;
                }
                else
                {
                    dt = appbll.GetListjK(zd.t_appkey).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        model.t_appkey = zd.t_appkey;
                        model.t_appid = string.IsNullOrEmpty(dt.Rows[0]["a_id"].ToString()) ? 0 : Int32.Parse(dt.Rows[0]["a_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, zd.t_appkey, 5);//存入缓存
                    }
                    else
                    {
                        return (json = "{\"message\":\"应用无效\",\"result\":9995}").ToString();
                    }
                }
                model.t_mark = zd.t_mark;
                model.t_network = zd.t_network;
                //model.t_ip = HttpContext.Current.Request.UserHostAddress;
                model.t_ip = string.IsNullOrEmpty(ip) ? "" : ip;
                //string IP = IPAddress.GetAddressByIp(model.t_ip.Split(':')[0].ToString());
                string ipstr = ConfigurationManager.AppSettings["ipkstr"].ToString();//获取ip库文件地址
                model.t_province = JMP.TOOL.IpProvince.IpAddress(model.t_ip, ipstr);
                //model.t_province = "";
                model.t_imsi = zd.t_imsi;
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
                model.t_brand = zd.t_brand;
                model.t_system = zd.t_system;
                model.t_hardware = zd.t_hardware;
                model.t_screen = zd.t_screen;
                model.t_sdkver = zd.t_sdkver;
                #endregion
                int cg = 0;
                if (zd.t_isnew == 1)
                {
                    #region 活跃用户
                    mo.l_teral_key = model.t_key;
                    mo.l_time = DateTime.Now;
                    mo.l_appkey = model.t_appkey;
                    mo.l_appid = model.t_appid;
                    cg = hybll.Add(mo);
                    #endregion
                }
                else
                {
                    #region 新增用户
                    model.t_time = DateTime.Now;
                    cg = zdbll.Add(model);
                    #endregion
                }
                if (cg > 0)
                {
                    json = "{\"message\":\"成功\",\"result\":100}";
                }
                else
                {
                    json = "{\"message\":\"失败\",\"error\":101}";
                    AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "接口错误信息", "报错信息：终端属性添加失败");//写入报错日志
                }
            }
            else
            {
                json = "{\"message\":\"json解析出错\",\"error\":9999}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "接口错误信息", "报错信息：终端属性参数为传入");//写入报错日志
            }
            return json.ToString();
            #endregion

        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}