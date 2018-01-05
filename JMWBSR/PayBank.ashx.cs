using JMP.TOOL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace JMWBSR
{
    /// <summary>
    /// PayBank 的摘要说明
    /// </summary>
    public class PayBank : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request.Params.Count > 0 && (context.Request.Params["Pay"] != null && context.Request.Params["Pay"] != ""))
            {
                string encryption = context.Request.Params["Pay"].ToString();
                if (encryption.Contains("code") && encryption.Contains("sign"))
                {
                    string str = PayBankType(encryption);
                    if (str.StartsWith("http://") || str.StartsWith("https://"))
                    {
                        context.Response.Redirect(str, true);
                    }
                    else if (str.Contains("UnionPay"))
                    {
                        context.Response.Redirect(str, true);
                    }
                    else
                    {
                        context.Response.Write(str);
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
            //context.Response.Write("Hello World");
        }
        /// <summary>
        /// 收银台调起支付入口
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string PayBankType(string json)
        {
            string StrJson = "{\"message\":\"失败\",\"result\":101}";
            try
            {
                OrderCode od = new OrderCode();
                od = JMP.TOOL.JsonHelper.Deserialize<OrderCode>(json);
                if (od != null)
                {

                    StrJson = Parameter(od, json);
                    if (StrJson == "ok")
                    {
                        StrJson = SelectCode(od, json);
                    }

                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString() + "报错位置：" + e.StackTrace.ToString();//报错信息
                StrJson = "{\"message\":\"参数异常\",\"result\":8990}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "收银台接口主入口错误信息", "报错信息：" + bcxx + "，支付接口请求数据" + json);//写入报错日志
                throw;
            }

            return StrJson;
        }
        /// <summary>
        /// 判断参数是否合法
        /// </summary>
        /// <param name="od"></param>
        /// <returns></returns>
        public static string Parameter(OrderCode od, string json)
        {
            string str = "ok";
            try
            {
                if (string.IsNullOrEmpty(od.sign))
                {
                    str = "{\"message\":\"参数sign有误\",\"result\":8999}";
                }
                //规则=JMP.TOOL.Encrypt.IndexEncrypt(mod.o_code+","+mod.o_price+","+tid);
                string jmcode = JMP.TOOL.Encrypt.IndexDecrypt(od.sign);
                string[] jmstr = jmcode.Split(',');
                bool pdsj = DateTime.ParseExact(jmstr[0].Substring(0, 14), "yyyyMMddHHmmss", CultureInfo.InvariantCulture) > DateTime.Parse(DateTime.Now.AddMinutes(-5).ToString("yyyy-MM-dd HH:mm:ss")) ? true : false;//判断是否在规定时间范围内

                if (!pdsj || jmstr[0] != od.code)
                {
                    return str = "{\"message\":\"签名验证有误\",\"result\":8998}";
                }
                if (string.IsNullOrEmpty(od.code) || od.code.Length > 32)
                {
                    return str = "{\"message\":\"参数code有误\",\"result\":8997}";
                }
                if (od.price <= 0 || decimal.Parse(jmstr[1]) != od.price)
                {
                    return str = "{\"message\":\"参数price有误\",\"result\":8996}";
                }
                if (od.paymode > 7)
                {
                    return str = "{\"message\":\"参数paymode有误\",\"result\":8995}";
                }
                if (string.IsNullOrEmpty(od.goodsname) || od.goodsname.Length > 32)
                {
                    return str = "{\"message\":\"参数goodsname有误\",\"result\":8993}";
                }
                if (od.tid <= 0 || int.Parse(jmstr[2]) != od.tid)
                {
                    return str = "{\"message\":\"参数tid有误\",\"result\":8992}";
                }
                if (od.paytype < 1 || od.paytype > 3)
                {
                    return str = "{\"message\":\"参数paytype有误\",\"result\":8991}";
                }

            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString() + "报错位置：" + e.StackTrace.ToString();//报错信息
                str = "{\"message\":\"参数异常\",\"result\":8990}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "收银台接口判断参数错误信息", "报错信息：" + bcxx + "，支付接口请求数据" + json);//写入报错日志
                throw;
            }
            return str;


        }
        /// <summary>
        /// 根据订单编号查询订单信息并调取支付当时
        /// </summary>
        /// <param name="od">参数实体</param>
        /// <returns></returns>
        public static string SelectCode(OrderCode od, string json)
        {
            string StrJson = "{\"message\":\"参数code有误\",\"result\":8997}";
            try
            {
                JMP.MDL.jmp_order mode = new JMP.MDL.jmp_order();
                JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
                mode = bll.GetModelbycode(od.code, "jmp_order");
                if (mode != null && mode.o_state == 0)
                {
                    if (mode.o_price == od.price)
                    {
                        if (bll.UpdatePayMode(mode.o_id, od.paymode))
                        {
                            StrJson = PayType.PaySelect(od.paymode.ToString(), mode.o_app_id, od.tid, od.paytype, mode.o_code, od.goodsname, mode.o_price, mode.o_id, mode.o_privateinfo);//直接调取支付方式
                        }

                    }
                    else
                    {
                        StrJson = "{\"message\":\"请求金额不一致\",\"result\":8994}";
                    }
                }
                else
                {
                    StrJson = "{\"message\":\"参数code有误\",\"result\":8997}";
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString() + "报错位置：" + e.StackTrace.ToString();//报错信息
                StrJson = "{\"message\":\"参数异常\",\"result\":8990}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "收银台调取支付接口错误信息", "报错信息：" + bcxx + "，支付接口请求数据" + json);//写入报错日志
                throw;
            }

            return StrJson;
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