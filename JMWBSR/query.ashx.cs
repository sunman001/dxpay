using JMP.TOOL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace JMWBSR
{
    /// <summary>
    /// query 的摘要说明
    /// </summary>
    public class query : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            // context.Response.Write("Hello World");
            if (context.Request.Params.Count > 0 && (context.Request.Params["code"] != null && context.Request.Params["code"] != ""))
            {
                string code = context.Request.Params["code"];//获取查询参数
                string str = SelectQuery(code);//调用查询接口
                context.Response.Write(str);//返回查询结果
            }
            else
            {
                context.Response.Write("{\"message\":\"fail\"}");
            }
        }
        /// <summary>
        /// 查询订单信息接口
        /// </summary>
        /// <param name="code">查询参数</param>
        /// <returns></returns>
        public static string SelectQuery(string code)
        {
            string str = "{\"message\":\"fail\"}";
            if (!string.IsNullOrEmpty(code))
            {
                try
                {
                    string jmcode = JMP.TOOL.Encrypt.IndexDecrypt(code);
                    //mod.o_code = DateTime.Now.ToString("yyyyMMddHHmmssfff") + r.Next(111111111, 999999999).ToString() + (dkip.Length > 4 ? dkip.Substring(0, 4) : r.Next(1111, 9999).ToString());
                    bool pdsj = DateTime.ParseExact(jmcode.Substring(0, 14), "yyyyMMddHHmmss", CultureInfo.InvariantCulture) > DateTime.Parse(DateTime.Now.AddHours(-1).ToString("yyyy-MM-dd HH:mm:ss")) ? true : false;//判断是否在规定时间范围内
                    if (pdsj)
                    {
                        str = QuerySelect(jmcode);
                    }
                    else
                    {
                        str = "{\"message\":\"fail\"}";
                    }
                }
                catch 
                {
                    return str = "{\"message\":\"fail\"}";
                }
            }
            return str;
        }
        /// <summary>
        /// 根据订单查询判断是否符合查询条件
        /// </summary>
        /// <param name="jmcode">订单好</param>
        /// <returns></returns>
        public static string QuerySelect(string jmcode)
        {
            string str = "";
            try
            {
                JMP.BLL.jmp_query bll = new JMP.BLL.jmp_query();
                JMP.MDL.jmp_query mo = new JMP.MDL.jmp_query();
                JMP.MDL.jmp_query mode = new JMP.MDL.jmp_query();
                if (JMP.TOOL.CacheHelper.IsCache(jmcode))//判读是否存在缓存
                {
                    mo = JMP.TOOL.CacheHelper.GetCaChe<JMP.MDL.jmp_query>(jmcode);//获取缓存
                }
                else
                {
                    mo = bll.SelectCode(jmcode);
                }
                if (mo != null && mo.q_id > 0)
                {
                    mode.q_time = mo.q_time + 1;
                    mode.q_code = mo.q_code;
                    mode.q_id = mo.q_id;
                    if (mode.q_time > 5)
                    {
                        str = "{\"message\":\"fail\"}";
                    }
                    else
                    {
                        if (bll.Update(mode))
                        {
                            str = jxjson(SelectOrder(jmcode));
                            JMP.TOOL.CacheHelper.CacheObjectLocak<JMP.MDL.jmp_query>(mode, jmcode, 60);//存入缓存
                        }
                        else
                        {
                            str = "{\"message\":\"fail\"}";
                        }
                    }
                }
                else
                {
                    mode.q_code = jmcode;
                    mode.q_time = 1;
                    int cg = bll.Add(mode);
                    if (cg > 0)
                    {
                        str = jxjson(SelectOrder(jmcode));
                        JMP.TOOL.CacheHelper.CacheObjectLocak<JMP.MDL.jmp_query>(mode, jmcode, 60);//存入缓存
                    }
                    else
                    {
                        str = "{\"message\":\"fail\"}";
                    }
                }
            }
            catch (Exception ex)
            {
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "查询接口判断订单出错", "报错信息：" + ex.ToString() + ",查询参数：" + jmcode);//写入报错日志
                return str = "{\"message\":\"fail\"}";
            }
            return str;
        }
        /// <summary>
        /// 根据订单号查询订单信息
        /// </summary>
        /// <param name="code">订单编号</param>
        /// <returns></returns>
        public static string SelectOrder(string code)
        {
            string str = "";
            try
            {
                JMP.MDL.jmp_order mode = new JMP.MDL.jmp_order();
                JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
                mode = bll.GetModelbycode(code, "jmp_order");
                if (mode != null)
                {
                    #region 查询时时表信息
                    if (mode.o_state == 1)
                    {
                        str = "{\"message\":\"success\",\"price\":\"" + mode.o_price.ToString("f2") + "\"}";
                    }
                    else
                    {
                        str = "{\"message\":\"fail\"}";
                    }
                    #endregion
                }
                else
                {
                    #region 根据指定时间查询归档表
                    string orderTableName = JMP.TOOL.WeekDateTime.GetOrderTableName(DateTime.Now.ToString("yyyy-MM-dd"));//获取订单表名
                    mode = bll.GetModelbycode(code, orderTableName);//查询本周归档表
                    if (mode != null)
                    {
                        if (mode.o_state == 1)
                        {
                            str = "{\"message\":\"success\",\"price\":\"" + mode.o_price.ToString("f2") + "\"}";
                        }
                        else
                        {
                            str = "{\"message\":\"fail\"}";
                        }
                    }
                    else
                    {
                        string TableName = JMP.TOOL.WeekDateTime.GetOrderTableName(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"));//获取订单表名
                        mode = bll.GetModelbycode(code, TableName);//查询上周归档表
                        if (mode != null)
                        {
                            if (mode.o_state == 1)
                            {
                                str = "{\"message\":\"success\",\"price\":\"" + mode.o_price.ToString("f2") + "\"}";
                            }
                            else
                            {
                                str = "{\"message\":\"fail\"}";
                            }
                        }
                        else
                        {
                            str = "{\"message\":\"fail\"}";
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "查询接口查询订单出错", "报错信息：" + ex.ToString() + ",查询参数：" + code);//写入报错日志
                return str = "{\"message\":\"fail\"}";
            }
            return str;
        }
        /// <summary>
        /// 判断json数据
        /// </summary>
        /// <param name="json">json数据</param>
        /// <returns></returns>
        public static string jxjson(string json)
        {
            string str = "";
            if (json.Contains("success"))
            {
                str = json;
            }
            else
            {
                str = "{\"message\":\"fail\"}";
            }
            return str;
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