using Alipay;
using HbtPay;
using JMP.TOOL;
using Pay.DinPay;
using swiftpass.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;
using WxPayAPI;

namespace JMWBSR
{
    /// <summary>
    /// Pay 的摘要说明
    /// </summary>
    public class Pay : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request.Params.Count > 0 && (context.Request.Params["Pay"] != null && context.Request.Params["Pay"] != ""))
            {
                string encryption = context.Request.Params["Pay"].ToString();
                if (encryption.Length > 100 && encryption.Contains("o_appkey"))
                {
                    ModelZfJk zf = new ModelZfJk();
                    zf = JMP.TOOL.JsonHelper.Deserialize<ModelZfJk>(encryption);
                    if (zf != null)
                    {
                        string codeAppid = zf.o_bizcode + zf.o_goods_id;
                        string str = "";
                        if (JMP.TOOL.CacheHelper.IsCache(codeAppid))//判读是否存在缓存
                        {
                            //strcode = JMP.TOOL.CacheHelper.GetCaChe<string>(codename);//获取缓存
                            str = "{\"message\":\"订单校验失败\",\"result\":9985}";
                            context.Response.Write(str);
                        }
                        else
                        {
                            JMP.TOOL.CacheHelper.CacheObjectLocak<string>(codeAppid, codeAppid, 5);//存入缓存
                            str = PayInterface(zf, encryption);
                            if (str.StartsWith("http://") || str.StartsWith("https://"))
                            {
                                context.Response.Redirect(str, true);
                            }
                            else if (str.Contains("UnionPay") || str.Contains("cashdesk/h5/checkout.aspx"))
                            {
                                //string url = "/UnionPay.aspx?UnionPay=" + str;
                                context.Response.Redirect(str, true);
                            }
                            else
                            {
                                context.Response.Write(str);
                            }
                        }
                    }
                    else
                    {
                        context.Response.Write("{\"message\":\"json解析出错\",\"result\":9999}");
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
        /// 获取支付信息
        /// </summary>
        /// <param name="zdstr">参数字符串</param>
        /// <returns></returns>
        static string PayInterface(ModelZfJk zf, string zfstr)
        {
            string sHtmlText = "{\"message\":\"支付接口异常\",\"result\":102}";
            // ModelZfJk zf = new ModelZfJk();
            //  zf = JMP.TOOL.JsonHelper.Deserialize<ModelZfJk>(zfstr);

            JMP.MDL.jmp_order mod = new JMP.MDL.jmp_order();//订单表实体类
            JMP.BLL.jmp_order jmp_orderbll = new JMP.BLL.jmp_order();//订单表业务逻辑层
            JMP.BLL.jmp_app appbll = new JMP.BLL.jmp_app();//应用业务逻辑层

            // string zftd = "";//支付通道
            //DataTable zftddt = new DataTable();
            //JMP.BLL.jmp_paymenttype paybll = new JMP.BLL.jmp_paymenttype();
            DataTable dt = new DataTable();
            int tid = 0;//应用类型id
            DataRow[] ddt = null;
            string goodsname = "";//商品名称
            int paytype = 0;//关联平台（1:安卓，2:苹果，3:H5）
            string o_paymode_id = "";//支付配置（支付宝，微信）
            if (zf != null)
            {
                #region 判断参数
                try
                {
                    if (string.IsNullOrEmpty(zf.o_appkey) || zf.o_appkey.Length > 64)
                    {
                        return sHtmlText = "{\"message\":\"参数o_appkey有误\",\"result\":9998}";
                    }
                    else
                    {
                        if (JMP.TOOL.CacheHelper.IsCache(zf.o_appkey.ToString()))//判读是否存在缓存
                        {
                            dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(zf.o_appkey);//获取缓存
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                mod.o_app_id = Int32.Parse(dt.Rows[0]["a_id"].ToString());
                                tid = Int32.Parse(dt.Rows[0]["a_apptype_id"].ToString());
                                paytype = dt.Rows[0]["a_platform_id"] != null && dt.Rows[0]["a_platform_id"].ToString() != "" ? Int32.Parse(dt.Rows[0]["a_platform_id"].ToString()) : 0;//获取支付类型
                                o_paymode_id = dt.Rows[0]["a_paymode_id"] != null && dt.Rows[0]["a_paymode_id"].ToString() != "" ? dt.Rows[0]["a_paymode_id"].ToString() : "";
                            }
                            else
                            {
                                dt = appbll.GetListjK(zf.o_appkey).Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    mod.o_app_id = Int32.Parse(dt.Rows[0]["a_id"].ToString());
                                    tid = Int32.Parse(dt.Rows[0]["a_apptype_id"].ToString());
                                    JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, zf.o_appkey, 5);//存入缓存
                                    paytype = dt.Rows[0]["a_platform_id"] != null && dt.Rows[0]["a_platform_id"].ToString() != "" ? Int32.Parse(dt.Rows[0]["a_platform_id"].ToString()) : 0;//获取支付类型
                                    o_paymode_id = dt.Rows[0]["a_paymode_id"] != null && dt.Rows[0]["a_paymode_id"].ToString() != "" ? dt.Rows[0]["a_paymode_id"].ToString() : "";
                                }
                                else
                                {
                                    return sHtmlText = "{\"message\":\"应用无效或未审核\",\"result\":9997}";
                                }
                            }
                        }
                        else
                        {
                            dt = appbll.GetListjK(zf.o_appkey).Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                mod.o_app_id = Int32.Parse(dt.Rows[0]["a_id"].ToString());
                                tid = Int32.Parse(dt.Rows[0]["a_apptype_id"].ToString());
                                JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, zf.o_appkey, 5);//存入缓存
                                paytype = dt.Rows[0]["a_platform_id"] != null && dt.Rows[0]["a_platform_id"].ToString() != "" ? Int32.Parse(dt.Rows[0]["a_platform_id"].ToString()) : 0;//获取支付类型
                                o_paymode_id = dt.Rows[0]["a_paymode_id"] != null && dt.Rows[0]["a_paymode_id"].ToString() != "" ? dt.Rows[0]["a_paymode_id"].ToString() : "";
                            }
                            else
                            {
                                return sHtmlText = "{\"message\":\"应用无效或未审核\",\"result\":9997}";
                            }
                        }
                    }
                    if (zf.o_goods_id > 0)
                    {
                        ddt = dt.Select(" g_id='" + zf.o_goods_id + "' and a_id='" + mod.o_app_id + "' ");
                        if (ddt.Length == 1)
                        {
                            //mod.o_goodsname = zf.o_goods_id;
                        }
                        else
                        {
                            return sHtmlText = "{\"message\":\"无效商品\",\"result\":9996}";
                        }
                    }
                    else
                    {
                        return sHtmlText = "{\"message\":\"参数o_goods_id有误\",\"result\":9995}";
                    }
                    if (!string.IsNullOrEmpty(zf.o_bizcode) && zf.o_bizcode.Length <= 64)
                    {
                        mod.o_bizcode = zf.o_bizcode;
                    }
                    else
                    {
                        return sHtmlText = "{\"message\":\"参数o_bizcode有误\",\"result\":9994}";
                    }
                    if (!string.IsNullOrEmpty(zf.o_term_key) && zf.o_term_key.Length <= 64)
                    {
                        mod.o_term_key = zf.o_term_key;
                    }
                    else
                    {
                        return sHtmlText = "{\"message\":\"参数o_term_key有误\",\"result\":9993}";
                    }
                    if (string.IsNullOrEmpty(zf.o_address))
                    {
                        mod.o_address = dt.Rows[0]["a_notifyurl"].ToString();//获取应用回调地址
                    }
                    else
                    {
                        if (zf.o_address.Length > 64)
                        {
                            return sHtmlText = "{\"message\":\"参数o_address有误\",\"result\":9987}";
                        }
                        else
                        {
                            mod.o_address = zf.o_address;
                        }
                    }
                    if (paytype == 3)
                    {
                        if (string.IsNullOrEmpty(zf.o_showaddress))
                        {
                            if (dt.Rows[0]["a_showurl"].ToString() != "")
                            {
                                mod.o_showaddress = !string.IsNullOrEmpty(dt.Rows[0]["a_showurl"].ToString()) ? dt.Rows[0]["a_showurl"].ToString() : "";//获取应用回调地址
                            }
                            else
                            {
                                return sHtmlText = "{\"message\":\"参数o_showaddress有误\",\"result\":9986}";
                            }
                        }
                        else
                        {
                            if (zf.o_showaddress.Length > 64)
                            {
                                return sHtmlText = "{\"message\":\"参数o_showaddress有误\",\"result\":9986}";
                            }
                            else
                            {
                                mod.o_showaddress = zf.o_showaddress;
                            }
                        }
                    }
                    if (int.Parse(zf.o_paymode_id) == 9)
                    {
                        mod.o_paymode_id = zf.o_paymode_id;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(zf.o_paymode_id) || int.Parse(zf.o_paymode_id) > 7)
                        {
                            return sHtmlText = "{\"message\":\"参数o_paymode_id有误\",\"result\":9992}";
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(o_paymode_id) && o_paymode_id.Contains(zf.o_paymode_id))
                            {
                                mod.o_paymode_id = zf.o_paymode_id;
                            }
                            else
                            {
                                return sHtmlText = "{\"message\":\"支付通道已关闭\",\"result\":105}";
                            }
                        }
                    }
                    if (zf.o_price > 0)
                    {
                        //if (zf.o_price < 1)
                        //{
                        //    return sHtmlText = "{\"message\":\"支付金额不能小于1\",\"result\":9991}";
                        //}
                        //else
                        //{
                        mod.o_price = zf.o_price;
                        //}
                        // mod.o_price = zf.o_price;
                    }
                    else
                    {
                        mod.o_price = decimal.Parse(dt.Rows[0]["g_price"].ToString());
                        if (mod.o_price <= 0)
                        {
                            return sHtmlText = "{\"message\":\"支付金额不能小于0\",\"result\":9991}";
                        }
                        //else
                        //{
                        //    mod.o_price = decimal.Parse(dt.Rows[0]["g_price"].ToString());
                        //}
                    }
                    if (!string.IsNullOrEmpty(zf.o_privateinfo))
                    {
                        if (zf.o_privateinfo.Length > 64)
                        {
                            return sHtmlText = "{\"message\":\"商户私有信息超长\",\"result\":9990}";
                        }
                        else
                        {
                            mod.o_privateinfo = zf.o_privateinfo;
                        }
                    }
                    else
                    {
                        mod.o_privateinfo = "404";
                    }
                    if (!string.IsNullOrEmpty(zf.o_goods_name))
                    {
                        if (zf.o_goods_name.Length > 64)
                        {
                            return sHtmlText = "{\"message\":\"商品名称超长\",\"result\":9989}";
                        }
                        else
                        {
                            goodsname = zf.o_goods_name;
                        }
                    }
                    else
                    {
                        goodsname = ddt[0]["g_name"].ToString();
                    }
                    Random r = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
                    string dkip = HttpContext.Current.Request.UserHostAddress.Replace(".", "").Trim();
                    mod.o_code = DateTime.Now.ToString("yyyyMMddHHmmssfff") + r.Next(111111111, 999999999).ToString() + (dkip.Length > 4 ? dkip.Substring(0, 4) : r.Next(1111, 9999).ToString());
                }
                catch (Exception e)
                {
                    string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString() + "报错位置：" + e.StackTrace.ToString();//报错信息
                    AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "接口错误信息", "报错信息：" + bcxx + "，支付接口请求数据" + zfstr);//写入报错日志
                    sHtmlText = "{\"message\":\"参数有误\",\"result\":103}";
                    return sHtmlText;
                }
                #endregion
                #region 支付请求
                int cg = 0;
                mod.o_state = 0;
                mod.o_times = 0;
                mod.o_noticestate = 0;
                mod.o_ctime = DateTime.Now;
                mod.o_noticetimes = DateTime.Now;
                mod.o_ptime = DateTime.Now;
                //修改支付金额
                if ((tid == 61 || tid == 32 || tid == 33 || tid == 34))
                {
                    #region 获取系统配置
                    JMP.BLL.jmp_system bll = new JMP.BLL.jmp_system();
                    DataTable dad = new DataTable();
                    string[] paname = { "", "" };
                    string xtpzhc = "xtpzhcsz";//缓存名称
                    string name = "";//要显示的商品名称
                    string state = "0";//
                    if (JMP.TOOL.CacheHelper.IsCache(xtpzhc))//判读是否存在缓存
                    {
                        paname = JMP.TOOL.CacheHelper.GetCaChe<string[]>(xtpzhc);//获取缓存
                        if (paname.Length > 0)
                        {
                            name = paname[0];
                            state = paname[1];
                        }
                    }
                    else
                    {
                        dad = bll.GetList(" (s_name='goodsname' or  s_name='pbszstate') ").Tables[0];
                        if (dad.Rows.Count > 0)
                        {
                            for (int i = 0; i < dad.Rows.Count; i++)
                            {
                                switch (dad.Rows[i]["s_name"].ToString())
                                {
                                    case "goodsname":
                                        name = string.IsNullOrEmpty(dad.Rows[i]["s_value"].ToString()) ? "" : dad.Rows[i]["s_value"].ToString();
                                        break;
                                    case "pbszstate":
                                        state = string.IsNullOrEmpty(dad.Rows[i]["s_value"].ToString()) ? "" : dad.Rows[i]["s_value"].ToString();
                                        break;
                                }
                            }
                            paname[0] = name;
                            paname[1] = state;
                            JMP.TOOL.CacheHelper.CacheObjectLocak<string[]>(paname, xtpzhc, 30);//存入缓存
                        }
                    }
                    if (state == "1" && !string.IsNullOrEmpty(name))
                    {
                        //goodsname = "会员礼包 QQ:1653064375";
                        goodsname = name;
                    }
                    #endregion
                }
                cg = jmp_orderbll.AddOrder(mod);
                if (cg > 0)
                {
                    try
                    {

                        if (int.Parse(mod.o_paymode_id) == 9)
                        {
                            int SdkPriority = Int32.Parse(ConfigurationManager.AppSettings["SdkPriority"].ToString());//获取优先级设置
                            string sign = JMP.TOOL.Encrypt.IndexEncrypt(mod.o_code + "," + mod.o_price + "," + tid);
                            string spotpaytype = paytype == 3 ? o_paymode_id : SdkPriority == 2 ? o_paymode_id.Replace("5", "@").Replace(",@", "") : o_paymode_id.Replace("2", "@").Replace(",@", "");
                            string jsondate = "{\"code\":\"" + mod.o_code + "\",\"goodsname\":\"" + goodsname + "\",\"spotpaytype\":\"" + spotpaytype + "\",\"price\":\"" + mod.o_price + "\",\"tid\":" + tid + ",\"sign\":\"" + sign + "\",\"paymode\":0,\"paytype\":" + paytype + "}";
                            #region 收银台模式 
                            if (paytype == 3)
                            {
                                //跳转到h5收银台界面
                                sHtmlText = string.Format("/cashdesk/h5/checkout.aspx?p={0}", jsondate);
                            }
                            else
                            {
                                //sdk模式返回json格式数据
                                sHtmlText = "{\"message\":\"成功\",\"result\":100,\"data\":" + jsondate + "}";
                            }
                            #endregion
                        }
                        else
                        {
                            sHtmlText = PayType.PaySelect(mod.o_paymode_id, mod.o_app_id, tid, paytype, mod.o_code, goodsname, mod.o_price, cg, mod.o_privateinfo);//直接调取支付方式
                        }
                    }
                    catch (Exception e)
                    {
                        string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString() + "报错位置：" + e.StackTrace.ToString();//报错信息
                        sHtmlText = "{\"message\":\"支付接口异常\",\"result\":102}";
                        AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "接口错误信息", "报错信息：" + bcxx + "，支付接口请求数据" + zfstr);//写入报错日志
                        return sHtmlText;
                    }
                }
                else
                {
                    sHtmlText = "{\"message\":\"失败\",\"result\":101}";
                    AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "接口错误信息", "报错信息：支付信息生成订单失败");//写入报错日志
                }
                #endregion
            }
            else
            {
                sHtmlText = "{\"message\":\"json解析出错\",\"result\":9999}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "接口错误信息", "报错信息：支付信息未传入");//写入报错日志
            }
            return sHtmlText;

        }

        #region 支付宝支付方式
        /// <summary>
        /// 支付宝支付通道安卓调用方式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        /// <param name="TableName">订单表表名</param>
        /// <returns></returns>
        public static string PayZfbAz(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            Config cfg = new Config(tid);
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
            if (!bll.UpdatePay(oid, cfg.pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "支付宝支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            sParaTemp.Add("partner", cfg.partner);
            sParaTemp.Add("seller_id", cfg.seller_id);
            sParaTemp.Add("_input_charset", cfg.input_charset.ToLower());
            sParaTemp.Add("service", "mobile.securitypay.pay");
            sParaTemp.Add("payment_type", "1");
            sParaTemp.Add("notify_url", ConfigurationManager.AppSettings["TokenUrl"].ToString().Replace("{0}", cfg.pay_id.ToString()));//需要封装（接收回传地址）TokenUrl
            sParaTemp.Add("it_b_pay", "30m");
            sParaTemp.Add("out_trade_no", code);//我们的订单号
            sParaTemp.Add("subject", goodsname);//商品i名称（根据商品id查询商品名称）
            sParaTemp.Add("total_fee", price.ToString());//价格（已传入的为准，无就从数据库读取）
            sParaTemp.Add("body", goodsname);//商品名称（备注）
            str = new Alipay.Submit(tid).BuildRequest1(sParaTemp);
            str = "{\"message\":\"成功\",\"result\":100,\"data\":{\"pay\":\"" + str + "\"}}";
            return str;
        }
        /// <summary>
        /// 支付宝支付通道IOS调用方式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        /// <param name="TableName">订单表表名</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string PayZfbIos(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            Config cfg = new Config(tid);
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
            if (!bll.UpdatePay(oid, cfg.pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "支付宝支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            sParaTemp.Add("partner", cfg.partner);
            sParaTemp.Add("seller_id", cfg.seller_id);
            sParaTemp.Add("_input_charset", cfg.input_charset.ToLower());
            sParaTemp.Add("service", "mobile.securitypay.pay");
            sParaTemp.Add("payment_type", "1");
            sParaTemp.Add("notify_url", ConfigurationManager.AppSettings["TokenUrl"].ToString().Replace("{0}", cfg.pay_id.ToString()));//需要封装（接收回传地址）TokenUrl
            sParaTemp.Add("it_b_pay", "30m");
            sParaTemp.Add("out_trade_no", code);//我们的订单号
            sParaTemp.Add("subject", goodsname);//商品i名称（根据商品id查询商品名称）
            sParaTemp.Add("total_fee", price.ToString());//价格（已传入的为准，无就从数据库读取）
            sParaTemp.Add("body", goodsname);//商品名称（备注）
            str = new Alipay.Submit(tid).BuildRequest1(sParaTemp);
            str = "{\"message\":\"成功\",\"result\":100,\"data\":{\"pay\":\"" + str + "\"}}";
            return str;
        }
        /// <summary>
        /// 支付包支付通道H5调用方式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        /// <param name="TableName">订单表表名</param>
        /// <returns></returns>
        public static string PayZfbH5(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
            Config cfg = new Config(tid);
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            if (!bll.UpdatePay(oid, cfg.pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "支付宝支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            sParaTemp.Add("partner", cfg.partner);
            sParaTemp.Add("seller_id", cfg.seller_id);
            sParaTemp.Add("_input_charset", cfg.input_charset.ToLower());
            sParaTemp.Add("service", "alipay.wap.create.direct.pay.by.user");
            sParaTemp.Add("payment_type", "1");
            sParaTemp.Add("sign_type", "RSA");
            sParaTemp.Add("notify_url", ConfigurationManager.AppSettings["TokenUrl"].ToString().Replace("{0}", cfg.pay_id.ToString()));//需要封装TokenUrl(异步回调地址)
            sParaTemp.Add("return_url", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", oid.ToString()));//同步支付成功界面跳转地址
            sParaTemp.Add("it_b_pay", "30m");
            sParaTemp.Add("out_trade_no", code);//我们的订单号
            sParaTemp.Add("subject", goodsname);//商品名称（根据商品id查询商品名称）
            sParaTemp.Add("total_fee", price.ToString());//价格（已传入的为准，无就从数据库读取）
            sParaTemp.Add("body", goodsname);//商品名称（备注）
            sParaTemp.Add("app_pay", "Y");//吊起app
            string fromstr = new Alipay.Submit(tid).BuildRequest(sParaTemp, "get", "确认");//表单提交方式
            //str = new Alipay.Submit(tid).BuildRequestParameters(sParaTemp);//http提交方式
            // str = "{\"message\":\"成功\",\"result\":100,\"data\":{\"pay\":\"" + str + "\"}}";
            string h5key = "h5" + code;
            JMP.TOOL.CacheHelper.CacheObject(h5key, fromstr, 1);
            str = JMP.TOOL.Encrypt.IndexEncrypt(h5key);
            str = "/UnionPay.aspx?UnionPay=" + str;
            return str;
        }
        #endregion

        #region 威富通支付方式
        /// <summary>
        /// 威富通支付通道安卓调用方式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        /// <param name="privateinfo">商品户私有信息</param>
        ///<param name="TableName">订单表表名</param>
        /// <returns></returns>
        public static string PayWftAz(int tid, string code, string goodsname, decimal price, string privateinfo, int oid)
        {
            string str = "";
            ClientResponseHandler resHandler = new ClientResponseHandler();
            PayHttpClient pay = new PayHttpClient();
            RequestHandler reqHandler = new RequestHandler(null);
            Dictionary<string, string> cfg = Utils.loadCfg(tid);
            JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
            if (!bll.UpdatePay(oid, int.Parse(cfg["pay_id"].ToString())))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通微信wap支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            //初始化数据  
            reqHandler.setGateUrl(cfg["req_url"].ToString());
            reqHandler.setKey(cfg["key"].ToString());
            reqHandler.setParameter("out_trade_no", code);//我们的订单号
            reqHandler.setParameter("body", goodsname);//商品描述
            reqHandler.setParameter("attach", privateinfo);//附加信息
            reqHandler.setParameter("total_fee", (Convert.ToInt32(price * 100)).ToString());//价格（已传入的为准，无就从数据库读取）
            reqHandler.setParameter("mch_create_ip", HttpContext.Current.Request.UserHostAddress);//终端IP 
            reqHandler.setParameter("service", "unified.trade.pay");
            reqHandler.setParameter("mch_id", cfg["mch_id"].ToString());
            reqHandler.setParameter("version", cfg["version"].ToString());
            reqHandler.setParameter("notify_url", ConfigurationManager.AppSettings["WftTokenUrl"].ToString().Replace("{0}", cfg["pay_id"].ToString()));//回掉地址
            reqHandler.setParameter("nonce_str", Utils.random());
            reqHandler.setParameter("charset", "UTF-8");
            reqHandler.setParameter("sign_type", "MD5");
            reqHandler.setParameter("device_info", "AND_WAP");
            reqHandler.setParameter("mch_app_name", "测试");
            reqHandler.setParameter("mch_app_id", "http://www.baidu.com");
            reqHandler.setParameter("time_start", DateTime.Now.ToString("yyyyMMddHHmmss")); //订单生成时间
            reqHandler.setParameter("time_expire", DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss"));//订单超时时间
            reqHandler.createSign();
            string datawft = Utils.toXml(reqHandler.getAllParameters());
            Dictionary<string, string> reqContent = new Dictionary<string, string>();
            reqContent.Add("url", reqHandler.getGateUrl());
            reqContent.Add("data", datawft);
            pay.setReqContent(reqContent);
            if (pay.call())
            {
                resHandler.setContent(pay.getResContent());
                resHandler.setKey(cfg["key"].ToString());
                Hashtable param = resHandler.getAllParameters();
                if (resHandler.isTenpaySign())
                {
                    if (int.Parse(param["status"].ToString()) == 0)
                    {

                        string wxpay = "{\"paytype\":\"1\",\"token_id\":\"" + param["token_id"].ToString() + "\", \"services\":\"pay.weixin.wappay\", \"sign\":\"" + param["sign"] + "\",\"status\":\"0\", \"charset\":\"UTF-8\", \"version\":\"2.0\", \"sign_type\":\"MD5\"}";
                        str = "{\"message\":\"成功\",\"result\":100,\"data\":" + wxpay + "}";
                    }
                    else
                    {
                        string wftzfsbxin = "威富通支付失败信息，错误代码：" + param["err_code"] + ",错误信息：" + param["err_msg"] + ",商户号：" + cfg["mch_id"].ToString();
                        AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通微信wap接口错误信息", "报错信息：" + wftzfsbxin);//写入报错日志
                        str = "{\"message\":\"支付通道异常\",\"result\":104}";
                    }
                }
                else
                {
                    string mesage = "威富通支付失败信息，错误代码：" + resHandler.getContent() + ",错误信息：" + resHandler.getDebugInfo() + ",商户号：" + cfg["mch_id"].ToString();
                    AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通微信wap接口错误信息", "报错信息：" + mesage);//写入报错日志
                    str = "{\"message\":\"支付通道异常\",\"result\":104}";
                }
            }
            else
            {
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通微信wap接口错误信息", "报错信息：第一步验证错误");//写入报错日志
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }
        /// <summary>
        /// 威富通支付通道IOS调用方式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        /// <param name="privateinfo">商品户私有信息</param>
        ///<param name="TableName">订单表表名</param>
        ///<param name="oid">订单表ID</param>
        /// <returns></returns>
        public static string PayWftIos(int tid, string code, string goodsname, decimal price, string privateinfo, int oid)
        {
            string str = "";
            ClientResponseHandler resHandler = new ClientResponseHandler();
            PayHttpClient pay = new PayHttpClient();
            RequestHandler reqHandler = new RequestHandler(null);
            Dictionary<string, string> cfg = Utils.loadCfg(tid);
            JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
            if (!bll.UpdatePay(oid, int.Parse(cfg["pay_id"].ToString())))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通微信wap支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            //初始化数据  
            reqHandler.setGateUrl(cfg["req_url"].ToString());
            reqHandler.setKey(cfg["key"].ToString());
            reqHandler.setParameter("out_trade_no", code);//我们的订单号
            reqHandler.setParameter("body", goodsname);//商品描述
            reqHandler.setParameter("attach", privateinfo);//附加信息
            reqHandler.setParameter("total_fee", (Convert.ToInt32(price * 100)).ToString());//价格（已传入的为准，无就从数据库读取）
            reqHandler.setParameter("mch_create_ip", HttpContext.Current.Request.UserHostAddress);//终端IP 
            reqHandler.setParameter("service", "unified.trade.pay");
            reqHandler.setParameter("mch_id", cfg["mch_id"].ToString());
            reqHandler.setParameter("version", cfg["version"].ToString());
            reqHandler.setParameter("notify_url", ConfigurationManager.AppSettings["WftTokenUrl"].ToString().Replace("{0}", cfg["pay_id"].ToString()));//回掉地址
            reqHandler.setParameter("nonce_str", Utils.random());
            reqHandler.setParameter("charset", "UTF-8");
            reqHandler.setParameter("sign_type", "MD5");
            reqHandler.setParameter("device_info", "AND_WAP");
            reqHandler.setParameter("mch_app_name", "测试");
            reqHandler.setParameter("mch_app_id", "http://www.baidu.com");
            reqHandler.setParameter("time_start", DateTime.Now.ToString("yyyyMMddHHmmss")); //订单生成时间
            reqHandler.setParameter("time_expire", DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss"));//订单超时时间
            reqHandler.createSign();
            string datawft = Utils.toXml(reqHandler.getAllParameters());
            Dictionary<string, string> reqContent = new Dictionary<string, string>();
            reqContent.Add("url", reqHandler.getGateUrl());
            reqContent.Add("data", datawft);
            pay.setReqContent(reqContent);
            if (pay.call())
            {
                resHandler.setContent(pay.getResContent());
                resHandler.setKey(cfg["key"].ToString());
                Hashtable param = resHandler.getAllParameters();
                if (resHandler.isTenpaySign())
                {
                    if (int.Parse(param["status"].ToString()) == 0)
                    {

                        string wxpay = "{\"paytype\":\"1\",\"token_id\":\"" + param["token_id"].ToString() + "\", \"services\":\"pay.weixin.wappay\", \"sign\":\"" + param["sign"] + "\",\"status\":\"0\", \"charset\":\"UTF-8\", \"version\":\"2.0\", \"sign_type\":\"MD5\"}";
                        str = "{\"message\":\"成功\",\"result\":100,\"data\":" + wxpay + "}";
                    }
                    else
                    {
                        string wftzfsbxin = "威富通支付失败信息，错误代码：" + param["err_code"] + ",错误信息：" + param["err_msg"] + ",商户号：" + cfg["mch_id"].ToString();
                        AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通微信wap接口错误信息", "报错信息：" + wftzfsbxin);//写入报错日志
                        str = "{\"message\":\"支付通道异常\",\"result\":104}";
                    }
                }
                else
                {
                    string mesage = "威富通支付失败信息，错误代码：" + resHandler.getContent() + ",错误信息：" + resHandler.getDebugInfo() + ",商户号：" + cfg["mch_id"].ToString();
                    AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通微信wap接口错误信息", "报错信息：" + mesage);//写入报错日志
                    str = "{\"message\":\"支付通道异常\",\"result\":104}";
                }
            }
            else
            {
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通微信wap接口错误信息", "报错信息：第一步验证错误");//写入报错日志
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }
        /// <summary>
        /// 威富通支付通道H5调用方式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        /// <param name="privateinfo">商品户私有信息</param>
        ///<param name="TableName">订单表表名</param>
        /// <returns></returns>
        public static string PayWftAzH5(int tid, string code, string goodsname, decimal price, string privateinfo, int oid)
        {
            string str = "";
            ClientResponseHandler resHandler = new ClientResponseHandler();
            PayHttpClient pay = new PayHttpClient();
            RequestHandler reqHandler = new RequestHandler(null);
            Dictionary<string, string> cfg = Utils.loadCfg(tid);
            JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
            if (!bll.UpdatePay(oid, int.Parse(cfg["pay_id"].ToString())))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通微信wap支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            //初始化数据  
            reqHandler.setGateUrl(cfg["req_url"].ToString());
            reqHandler.setKey(cfg["key"].ToString());
            reqHandler.setParameter("out_trade_no", code);//我们的订单号
            reqHandler.setParameter("body", goodsname);//商品描述
            reqHandler.setParameter("attach", privateinfo);//附加信息
            reqHandler.setParameter("total_fee", (Convert.ToInt32(price * 100)).ToString());//价格（已传入的为准，无就从数据库读取）
            reqHandler.setParameter("mch_create_ip", HttpContext.Current.Request.UserHostAddress);//终端IP 
            reqHandler.setParameter("service", "pay.weixin.wappay");
            reqHandler.setParameter("mch_id", cfg["mch_id"].ToString());
            reqHandler.setParameter("version", cfg["version"].ToString());
            reqHandler.setParameter("notify_url", ConfigurationManager.AppSettings["WftTokenUrl"].ToString().Replace("{0}", cfg["pay_id"].ToString()));//回掉地址
            reqHandler.setParameter("callback_url", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", oid.ToString()));//同步回掉地址
            reqHandler.setParameter("nonce_str", Utils.random());
            reqHandler.setParameter("charset", "UTF-8");
            reqHandler.setParameter("sign_type", "MD5");
            reqHandler.setParameter("device_info", "AND_WAP");
            reqHandler.setParameter("mch_app_name", "测试");
            reqHandler.setParameter("mch_app_id", "http://www.baidu.com");
            reqHandler.setParameter("time_start", DateTime.Now.ToString("yyyyMMddHHmmss")); //订单生成时间
            reqHandler.setParameter("time_expire", DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss"));//订单超时时间
            reqHandler.createSign();
            string datawft = Utils.toXml(reqHandler.getAllParameters());
            Dictionary<string, string> reqContent = new Dictionary<string, string>();
            reqContent.Add("url", reqHandler.getGateUrl());
            reqContent.Add("data", datawft);
            pay.setReqContent(reqContent);
            if (pay.call())
            {
                resHandler.setContent(pay.getResContent());
                resHandler.setKey(cfg["key"].ToString());
                Hashtable param = resHandler.getAllParameters();
                if (resHandler.isTenpaySign())
                {
                    if (int.Parse(param["status"].ToString()) == 0)
                    {
                        string wxpay = "";
                        try
                        {
                            wxpay = param["pay_info"].ToString();
                            //str = "{\"message\":\"成功\",\"result\":100,\"data\":\"" + wxpay + "\"}";
                            str = wxpay;

                        }
                        catch
                        {
                            string wftzfsbxin = "威富通支付失败信息，错误代码：" + param["err_code"] + ",错误信息：" + param["err_msg"] + ",商户号：" + cfg["mch_id"].ToString();
                            AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通微信wap接口错误信息", "报错信息：" + wftzfsbxin);//写入报错日志
                            str = "{\"message\":\"支付通道异常\",\"result\":104}";
                        }
                    }
                    else
                    {
                        string wftzfsbxin = "威富通支付失败信息，错误代码：" + param["err_code"] + ",错误信息：" + param["err_msg"] + ",商户号：" + cfg["mch_id"].ToString();
                        AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通微信wap接口错误信息", "报错信息：" + wftzfsbxin);//写入报错日志
                        str = "{\"message\":\"支付通道异常\",\"result\":104}";
                    }
                }
                else
                {
                    string mesage = "威富通支付失败信息，错误代码：" + resHandler.getContent() + ",错误信息：" + resHandler.getDebugInfo();
                    AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通微信wap接口错误信息", "报错信息：" + mesage);//写入报错日志
                    str = "{\"message\":\"支付通道异常\",\"result\":104}";
                }
            }
            else
            {
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通微信wap接口错误信息", "报错信息：第一步验证错误");//写入报错日志
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }
        #endregion

        #region 威富通微信扫码支付
        /// <summary>
        /// 威富通微信扫码支付
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="privateinfo">商户私有信息</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string WftWxSm(int tid, string code, string goodsname, decimal price, string privateinfo, int oid)
        {

            string str = "";
            ClientResponseHandler resHandler = new ClientResponseHandler();
            PayHttpClient pay = new PayHttpClient();
            RequestHandler reqHandler = new RequestHandler(null);
            Dictionary<string, string> cfg = Utils.loadCfgWxSm(tid);
            JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
            if (!bll.UpdatePay(oid, int.Parse(cfg["pay_id"].ToString())))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通微信扫码支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            //初始化数据  
            reqHandler.setGateUrl(cfg["req_url"].ToString());
            reqHandler.setKey(cfg["key"].ToString());
            reqHandler.setParameter("out_trade_no", code);//我们的订单号
            reqHandler.setParameter("body", goodsname);//商品描述
            //reqHandler.setParameter("attach", privateinfo);//附加信息
            reqHandler.setParameter("total_fee", (Convert.ToInt32(price * 100)).ToString());//价格（已传入的为准，无就从数据库读取）
            reqHandler.setParameter("mch_create_ip", HttpContext.Current.Request.UserHostAddress);//终端IP 
            reqHandler.setParameter("service", "pay.weixin.native");//支付类型
            reqHandler.setParameter("mch_id", cfg["mch_id"].ToString());
            reqHandler.setParameter("version", cfg["version"].ToString());
            reqHandler.setParameter("notify_url", ConfigurationManager.AppSettings["WftTokenUrl"].ToString().Replace("{0}", cfg["pay_id"].ToString()));//回掉地址
            reqHandler.setParameter("nonce_str", Utils.random());
            reqHandler.setParameter("charset", "UTF-8");
            reqHandler.setParameter("sign_type", "MD5");
            reqHandler.setParameter("time_start", DateTime.Now.ToString("yyyyMMddHHmmss")); //订单生成时间
            reqHandler.setParameter("time_expire", DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss"));//订单超时时间
            reqHandler.createSign();
            string datawft = Utils.toXml(reqHandler.getAllParameters());
            Dictionary<string, string> reqContent = new Dictionary<string, string>();
            reqContent.Add("url", reqHandler.getGateUrl());
            reqContent.Add("data", datawft);

            pay.setReqContent(reqContent);
            if (pay.call())
            {
                resHandler.setContent(pay.getResContent());
                resHandler.setKey(cfg["key"].ToString());
                Hashtable param = resHandler.getAllParameters();
                if (resHandler.isTenpaySign())
                {
                    if (int.Parse(param["status"].ToString()) == 0)
                    {
                        string qurl = param["code_img_url"].ToString().Replace("https://pay.swiftpass.cn/pay/qrcode?uuid=", "") + "," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ",2";//组装二维码地址
                        string qrcodeurl = ConfigurationManager.AppSettings["QRcodeUrl"].ToString() + "?QRcode=" + JMP.TOOL.Encrypt.IndexEncrypt(qurl);//二维码展示地址
                        //str = "{\"message\":\"成功\",\"result\":100,\"data\":\"" + qrcodeurl + "\"}";
                        str = qrcodeurl;
                    }
                    else
                    {
                        string wftzfsbxin = "威富通支付失败信息，错误代码：" + param["err_code"] + ",错误信息：" + param["err_msg"] + ",商户号：" + cfg["mch_id"].ToString();
                        AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通微信扫码接口错误信息", "报错信息：" + wftzfsbxin);//写入报错日志
                        str = "{\"message\":\"支付通道异常\",\"result\":104}";
                    }
                }
                else
                {
                    string mesage = "威富通支付失败信息，错误代码：" + resHandler.getContent() + ",错误信息：" + resHandler.getDebugInfo() + ",商户号：" + cfg["mch_id"].ToString();
                    AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通微信扫码接口错误信息", "报错信息：" + mesage);//写入报错日志
                    str = "{\"message\":\"支付通道异常\",\"result\":104}";
                }
            }
            else
            {
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通微信扫码接口错误信息", "报错信息：" + "第一步验证错误");//写入报错日志
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }
        #endregion

        #region 威富通支付宝扫码支付
        /// <summary>
        /// 威富通支付宝扫码支付
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="privateinfo">商户私有信息</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string WftZfbSm(int tid, string code, string goodsname, decimal price, string privateinfo, int oid)
        {
            string str = "";
            ClientResponseHandler resHandler = new ClientResponseHandler();
            PayHttpClient pay = new PayHttpClient();
            RequestHandler reqHandler = new RequestHandler(null);
            Dictionary<string, string> cfg = Utils.loadCfgZfbSm(tid);
            JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
            if (!bll.UpdatePay(oid, int.Parse(cfg["pay_id"].ToString())))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通支付宝扫码支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            //初始化数据  
            reqHandler.setGateUrl(cfg["req_url"].ToString());
            reqHandler.setKey(cfg["key"].ToString());
            reqHandler.setParameter("out_trade_no", code);//我们的订单号
            reqHandler.setParameter("body", goodsname);//商品描述
                                                       // reqHandler.setParameter("attach", privateinfo);//附加信息
            reqHandler.setParameter("total_fee", (Convert.ToInt32(price * 100)).ToString());//价格（已传入的为准，无就从数据库读取）
            reqHandler.setParameter("mch_create_ip", HttpContext.Current.Request.UserHostAddress);//终端IP 
            reqHandler.setParameter("service", "pay.alipay.native");//支付类型
            reqHandler.setParameter("mch_id", cfg["mch_id"].ToString());
            reqHandler.setParameter("version", cfg["version"].ToString());
            reqHandler.setParameter("notify_url", ConfigurationManager.AppSettings["WftTokenUrl"].ToString().Replace("{0}", cfg["pay_id"].ToString()));//回掉地址
            reqHandler.setParameter("nonce_str", Utils.random());
            reqHandler.setParameter("charset", "UTF-8");
            reqHandler.setParameter("sign_type", "MD5");
            reqHandler.setParameter("time_start", DateTime.Now.ToString("yyyyMMddHHmmss")); //订单生成时间
            reqHandler.setParameter("time_expire", DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss"));//订单超时时间
            reqHandler.createSign();
            string datawft = Utils.toXml(reqHandler.getAllParameters());
            Dictionary<string, string> reqContent = new Dictionary<string, string>();
            reqContent.Add("url", reqHandler.getGateUrl());
            reqContent.Add("data", datawft);
            pay.setReqContent(reqContent);
            if (pay.call())
            {
                resHandler.setContent(pay.getResContent());
                resHandler.setKey(cfg["key"].ToString());
                Hashtable param = resHandler.getAllParameters();
                if (resHandler.isTenpaySign())
                {
                    if (int.Parse(param["status"].ToString()) == 0)
                    {
                        //string wxpay = param["code_img_url"].ToString();
                        string qurl = param["code_img_url"].ToString().Replace("https://pay.swiftpass.cn/pay/qrcode?uuid=", "") + "," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ",1";//组装二维码地址
                        string qrcodeurl = ConfigurationManager.AppSettings["QRcodeUrl"].ToString() + "?QRcode=" + JMP.TOOL.Encrypt.IndexEncrypt(qurl);//二维码展示地址
                        //str = "{\"message\":\"成功\",\"result\":100,\"data\":\"" + qrcodeurl + "\"}";
                        str = qrcodeurl;
                    }
                    else
                    {
                        string wftzfsbxin = "威富通支付失败信息，错误代码：" + param["err_code"] + ",错误信息：" + param["err_msg"] + ",商户号：" + cfg["mch_id"].ToString();
                        AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通支付宝扫码接口错误信息", "报错信息：" + wftzfsbxin);//写入报错日志
                        str = "{\"message\":\"支付通道异常\",\"result\":104}";
                    }
                }
                else
                {
                    string mesage = "威富通支付失败信息，错误代码：" + resHandler.getContent() + ",错误信息：" + resHandler.getDebugInfo() + ",商户号：" + cfg["mch_id"].ToString();
                    AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通支付宝扫码接口错误信息", "报错信息：" + mesage);//写入报错日志
                    str = "{\"message\":\"支付通道异常\",\"result\":104}";
                }
            }
            else
            {
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通支付宝接口错误信息", "报错信息：第一步验证错误");//写入报错日志
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }
        #endregion

        #region  威富通支付宝支付
        /// <summary>
        /// 威富通支付宝支付 h5调用
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="privateinfo">商户私有信息</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string WftZfbH5(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            ClientResponseHandler resHandler = new ClientResponseHandler();
            PayHttpClient pay = new PayHttpClient();
            RequestHandler reqHandler = new RequestHandler(null);
            Dictionary<string, string> cfg = Utils.loadCfgZfb(tid);
            JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
            if (!bll.UpdatePay(oid, int.Parse(cfg["pay_id"].ToString())))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通支付宝支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            //初始化数据  
            reqHandler.setGateUrl(cfg["req_url"].ToString());
            reqHandler.setKey(cfg["key"].ToString());
            reqHandler.setParameter("out_trade_no", code);//我们的订单号
            reqHandler.setParameter("body", goodsname);//商品描述
            reqHandler.setParameter("attach", "ces");//附加信息
            reqHandler.setParameter("total_fee", (Convert.ToInt32(price * 100)).ToString());//价格（已传入的为准，无就从数据库读取）
            reqHandler.setParameter("mch_create_ip", HttpContext.Current.Request.UserHostAddress);//终端IP 
            reqHandler.setParameter("service", "pay.alipay.native");//支付类型
            reqHandler.setParameter("mch_id", cfg["mch_id"].ToString());
            reqHandler.setParameter("version", cfg["version"].ToString());
            reqHandler.setParameter("notify_url", ConfigurationManager.AppSettings["WftTokenUrl"].ToString().Replace("{0}", cfg["pay_id"].ToString()));//回掉地址
            reqHandler.setParameter("callback_url", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", oid.ToString()));//同步回掉地址
            reqHandler.setParameter("nonce_str", Utils.random());
            reqHandler.setParameter("charset", "UTF-8");
            reqHandler.setParameter("sign_type", "MD5");
            reqHandler.setParameter("time_start", DateTime.Now.ToString("yyyyMMddHHmmss")); //订单生成时间
            reqHandler.setParameter("time_expire", DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss"));//订单超时时间
            reqHandler.createSign();
            string datawft = Utils.toXml(reqHandler.getAllParameters());
            Dictionary<string, string> reqContent = new Dictionary<string, string>();
            reqContent.Add("url", reqHandler.getGateUrl());
            reqContent.Add("data", datawft);
            pay.setReqContent(reqContent);
            if (pay.call())
            {
                resHandler.setContent(pay.getResContent());
                resHandler.setKey(cfg["key"].ToString());
                Hashtable param = resHandler.getAllParameters();
                if (resHandler.isTenpaySign())
                {
                    if (int.Parse(param["status"].ToString()) == 0)
                    {
                        str = param["code_url"].ToString();
                    }
                    else
                    {
                        string wftzfsbxin = "威富通支付失败信息，错误代码：" + param["err_code"] + ",错误信息：" + param["err_msg"] + ",商户号：" + cfg["mch_id"].ToString();
                        AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通支付宝接口错误信息", "报错信息：" + wftzfsbxin);//写入报错日志
                        str = "{\"message\":\"支付通道异常\",\"result\":104}";
                    }
                }
                else
                {
                    string mesage = "威富通支付失败信息，错误代码：" + resHandler.getContent() + ",错误信息：" + resHandler.getDebugInfo() + ",商户号：" + cfg["mch_id"].ToString();
                    AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通支付宝接口错误信息", "报错信息：" + mesage);//写入报错日志
                    str = "{\"message\":\"支付通道异常\",\"result\":104}";
                }
            }
            else
            {
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通支付宝接口错误信息", "报错信息：第一步验证错误");//写入报错日志
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }

        /// <summary>
        /// 威富通支付宝支付 安卓调用
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="privateinfo">商户私有信息</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string WftZfbAz(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            ClientResponseHandler resHandler = new ClientResponseHandler();
            PayHttpClient pay = new PayHttpClient();
            RequestHandler reqHandler = new RequestHandler(null);
            Dictionary<string, string> cfg = Utils.loadCfgZfb(tid);
            JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
            if (!bll.UpdatePay(oid, int.Parse(cfg["pay_id"].ToString())))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通支付宝支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            //初始化数据  
            reqHandler.setGateUrl(cfg["req_url"].ToString());
            reqHandler.setKey(cfg["key"].ToString());
            reqHandler.setParameter("out_trade_no", code);//我们的订单号
            reqHandler.setParameter("body", goodsname);//商品描述
            reqHandler.setParameter("attach", "ces");//附加信息
            reqHandler.setParameter("total_fee", (Convert.ToInt32(price * 100)).ToString());//价格（已传入的为准，无就从数据库读取）
            reqHandler.setParameter("mch_create_ip", HttpContext.Current.Request.UserHostAddress);//终端IP 
            reqHandler.setParameter("service", "pay.alipay.native");//支付类型
            reqHandler.setParameter("mch_id", cfg["mch_id"].ToString());
            reqHandler.setParameter("version", cfg["version"].ToString());
            reqHandler.setParameter("notify_url", ConfigurationManager.AppSettings["WftTokenUrl"].ToString().Replace("{0}", cfg["pay_id"].ToString()));//回掉地址
            reqHandler.setParameter("nonce_str", Utils.random());
            reqHandler.setParameter("charset", "UTF-8");
            reqHandler.setParameter("sign_type", "MD5");
            reqHandler.setParameter("time_start", DateTime.Now.ToString("yyyyMMddHHmmss")); //订单生成时间
            reqHandler.setParameter("time_expire", DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss"));//订单超时时间
            reqHandler.createSign();
            string datawft = Utils.toXml(reqHandler.getAllParameters());
            Dictionary<string, string> reqContent = new Dictionary<string, string>();
            reqContent.Add("url", reqHandler.getGateUrl());
            reqContent.Add("data", datawft);
            pay.setReqContent(reqContent);
            if (pay.call())
            {
                resHandler.setContent(pay.getResContent());
                resHandler.setKey(cfg["key"].ToString());
                Hashtable param = resHandler.getAllParameters();
                if (resHandler.isTenpaySign())
                {
                    if (int.Parse(param["status"].ToString()) == 0)
                    {
                        string codes = JMP.TOOL.Encrypt.IndexEncrypt(code);
                        str = "{\"message\":\"成功\",\"result\":100,\"data\":{\"code\":\"" + codes + "\",\"pay\":\"" + param["code_url"].ToString() + "\"}}";
                    }
                    else
                    {
                        string wftzfsbxin = "威富通支付失败信息，错误代码：" + param["err_code"] + ",错误信息：" + param["err_msg"] + ",商户号：" + cfg["mch_id"].ToString();
                        AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通支付宝接口错误信息", "报错信息：" + wftzfsbxin);//写入报错日志
                        str = "{\"message\":\"支付通道异常\",\"result\":104}";
                    }
                }
                else
                {
                    string mesage = "威富通支付失败信息，错误代码：" + resHandler.getContent() + ",错误信息：" + resHandler.getDebugInfo() + ",商户号：" + cfg["mch_id"].ToString();
                    AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通支付宝接口错误信息", "报错信息：" + mesage);//写入报错日志
                    str = "{\"message\":\"支付通道异常\",\"result\":104}";
                }
            }
            else
            {
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通支付宝接口错误信息", "报错信息：第一步验证错误");//写入报错日志
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }


        /// <summary>
        /// 威富通支付宝支付 Ios调用
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="privateinfo">商户私有信息</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string WftZfbIos(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            ClientResponseHandler resHandler = new ClientResponseHandler();
            PayHttpClient pay = new PayHttpClient();
            RequestHandler reqHandler = new RequestHandler(null);
            Dictionary<string, string> cfg = Utils.loadCfgZfb(tid);
            JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
            if (!bll.UpdatePay(oid, int.Parse(cfg["pay_id"].ToString())))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通支付宝支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            //初始化数据  
            reqHandler.setGateUrl(cfg["req_url"].ToString());
            reqHandler.setKey(cfg["key"].ToString());
            reqHandler.setParameter("out_trade_no", code);//我们的订单号
            reqHandler.setParameter("body", goodsname);//商品描述
            reqHandler.setParameter("attach", "ces");//附加信息
            reqHandler.setParameter("total_fee", (Convert.ToInt32(price * 100)).ToString());//价格（已传入的为准，无就从数据库读取）
            reqHandler.setParameter("mch_create_ip", HttpContext.Current.Request.UserHostAddress);//终端IP 
            reqHandler.setParameter("service", "pay.alipay.native");//支付类型
            reqHandler.setParameter("mch_id", cfg["mch_id"].ToString());
            reqHandler.setParameter("version", cfg["version"].ToString());
            reqHandler.setParameter("notify_url", ConfigurationManager.AppSettings["WftTokenUrl"].ToString().Replace("{0}", cfg["pay_id"].ToString()));//回掉地址
            reqHandler.setParameter("nonce_str", Utils.random());
            reqHandler.setParameter("charset", "UTF-8");
            reqHandler.setParameter("sign_type", "MD5");
            reqHandler.setParameter("time_start", DateTime.Now.ToString("yyyyMMddHHmmss")); //订单生成时间
            reqHandler.setParameter("time_expire", DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss"));//订单超时时间
            reqHandler.createSign();
            string datawft = Utils.toXml(reqHandler.getAllParameters());
            Dictionary<string, string> reqContent = new Dictionary<string, string>();
            reqContent.Add("url", reqHandler.getGateUrl());
            reqContent.Add("data", datawft);
            pay.setReqContent(reqContent);
            if (pay.call())
            {
                resHandler.setContent(pay.getResContent());
                resHandler.setKey(cfg["key"].ToString());
                Hashtable param = resHandler.getAllParameters();
                if (resHandler.isTenpaySign())
                {
                    if (int.Parse(param["status"].ToString()) == 0)
                    {
                        string codes = JMP.TOOL.Encrypt.IndexEncrypt(code);
                        str = "{\"message\":\"成功\",\"result\":100,\"data\":{\"code\":\"" + codes + "\",\"pay\":\"" + param["code_url"].ToString() + "\"}}";
                    }
                    else
                    {
                        string wftzfsbxin = "威富通支付失败信息，错误代码：" + param["err_code"] + ",错误信息：" + param["err_msg"] + ",商户号：" + cfg["mch_id"].ToString();
                        AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通支付宝接口错误信息", "报错信息：" + wftzfsbxin);//写入报错日志
                        str = "{\"message\":\"支付通道异常\",\"result\":104}";
                    }
                }
                else
                {
                    string mesage = "威富通支付失败信息，错误代码：" + resHandler.getContent() + ",错误信息：" + resHandler.getDebugInfo() + ",商户号：" + cfg["mch_id"].ToString();
                    AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通支付宝接口错误信息", "报错信息：" + mesage);//写入报错日志
                    str = "{\"message\":\"支付通道异常\",\"result\":104}";
                }
            }
            else
            {
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通支付宝接口错误信息", "报错信息：第一步验证错误");//写入报错日志
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }
        #endregion

        #region 智付支付方式
        /// <summary>
        /// 智付支付通道安卓调用方式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        /// <param name="TableName">订单表表名</param>
        /// <returns></returns>
        public static string PayZfAz(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            Dictionary<string, string> DPcfg = DPConfing.loadCfg(tid);
            JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
            if (!bll.UpdatePay(oid, Int32.Parse(DPcfg["pay_id"].ToString())))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "智付支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            //调用示例
            var formField = new FormField(
                             DPcfg["partner"].ToString(), //商家账号
                             code, //订单编号
                             price.ToString("f2"),//交易金额
                             ConfigurationManager.AppSettings["ZFTokenUrl"].ToString().Replace("{0}", DPcfg["pay_id"].ToString()), //通知地址
                             "RSA-S",//签名方式
                             DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), //订单时间
                            goodsname,//商品描述
                            new FormProperty("")//表单属性对象
                            );
            //商家私钥
            string merchantPrivateKey = DPcfg["dpkey"].ToString();
            //实例化HTML构造器
            var htmlCreator = new HtmlCreator(formField, new PrivateKeySignRsas(merchantPrivateKey));
            //生成表单字符串
            var htmlForm = htmlCreator.CreateHtmlAz();
            str = "{\"message\":\"成功\",\"result\":100,\"data\":{\"union\":" + htmlForm + "}}";
            return str;
        }
        /// <summary>
        /// 智付支付通道苹果调用方式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        /// <param name="TableName">订单表表名</param>
        /// <returns></returns>
        public static string PayZfIos(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            Dictionary<string, string> DPcfg = DPConfing.loadCfg(tid);
            JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
            if (!bll.UpdatePay(oid, Int32.Parse(DPcfg["pay_id"].ToString())))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "智付支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            //调用示例
            var formField = new FormField(
                             DPcfg["partner"].ToString(), //商家账号
                             code, //订单编号
                             price.ToString("f2"),//交易金额
                             ConfigurationManager.AppSettings["ZFTokenUrl"].ToString().Replace("{0}", DPcfg["pay_id"].ToString()), //通知地址
                             "RSA-S",//签名方式
                             DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), //订单时间
                            goodsname,//商品描述
                            new FormProperty("")//表单属性对象
                            );
            //商家私钥
            string merchantPrivateKey = DPcfg["dpkey"].ToString();
            //实例化HTML构造器
            var htmlCreator = new HtmlCreator(formField, new PrivateKeySignRsas(merchantPrivateKey));
            //生成表单字符串
            var htmlForm = htmlCreator.CreateHtmlAz();
            str = "{\"message\":\"成功\",\"result\":100,\"data\":{\"union\":" + htmlForm + "}}";
            return str;
        }
        /// <summary>
        /// 智付支付通道H5调用方式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        /// <param name="oid">订单表表名</param>
        /// <returns></returns>
        public static string PayZfH5(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "{\"message\":\"支付接口异常\",\"result\":102}";
            Dictionary<string, string> list = new Dictionary<string, string>();
            list.Add("UnionPay", "UnionPay");//特定表示
            list.Add("tid", tid.ToString());//应用类型id
            list.Add("code", code);//订单编号
            list.Add("goodsname", goodsname);//商品名称
            list.Add("price", price.ToString("f2"));//交易金额
            list.Add("oid", oid.ToString());//订单id
            Dictionary<string, string> DPcfg = DPConfing.loadCfg(tid);
            JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
            if (!bll.UpdatePay(oid, Int32.Parse(DPcfg["pay_id"].ToString())))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "智付支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            else
            {
                string tbtzurl = ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", oid.ToString());
                //调用示例
                var formField = new FormField(
                                 DPcfg["partner"].ToString(), //商家账号
                                 list["code"].ToString(), //订单编号
                                 list["price"].ToString(),//交易金额
                                  "direct_pay", //服务类型
                                  "UTF-8",//编码格式
                                 ConfigurationManager.AppSettings["ZFTokenUrl"].ToString().Replace("{0}", DPcfg["pay_id"].ToString()), //通知地址
                                 "RSA-S",//签名方式
                                 DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), //订单时间
                                list["goodsname"].ToString(),//商品描述
                                new FormProperty("")//表单属性对象
                                )
                {
                    ReturnUrl = "" + tbtzurl + ""
                };//同步通知地址
                  //商家私钥
                string merchantPrivateKey = DPcfg["dpkey"].ToString();
                //实例化HTML构造器
                var htmlCreator = new HtmlCreator(formField, new PrivateKeySignRsas(merchantPrivateKey));
                //生成表单字符串
                var htmlForm = htmlCreator.CreateHtmlForm();
                string fromstr = JMP.TOOL.Encrypt.IndexEncrypt(htmlForm);
                string h5key = "h5" + code;
                JMP.TOOL.CacheHelper.CacheObject(h5key, htmlForm, 1);
                str = JMP.TOOL.Encrypt.IndexEncrypt(h5key);
                str = "/UnionPay.aspx?UnionPay=" + HttpUtility.UrlEncode(str);
            }
            return str;
        }
        #endregion

        #region 微信支付方式
        /// <summary>
        /// 微信支付通道安卓调用方式
        /// </summary>
        /// <param name="tid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string PayWxAz(int appid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            int tid = 0;
            try
            {
                #region 查询微信支付通道id
                string wxappidzfjk = "wxappidzfjk" + appid;//组装缓存key值
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface blls = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(wxappidzfjk))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(wxappidzfjk);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        tid = int.Parse(dt.Rows[0]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "微信支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = blls.selectAppid("WX", appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        tid = int.Parse(dt.Rows[0]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, wxappidzfjk, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "微信支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                #endregion
                if (tid > 0)
                {
                    WxPayConfig wx = new WxPayConfig(tid);
                    JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
                    if (!bll.UpdatePay(oid, tid))
                    {
                        str = "{\"message\":\"失败\",\"result\":101}";
                        AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "支付宝支付接口错误", "修改支付渠道失败！");//写入报错日志
                        return str;
                    }
                    WxPayData data = new WxPayData();
                    data.SetValue("body", goodsname);//商品名称
                    data.SetValue("out_trade_no", code); //我们的订单号
                    data.SetValue("total_fee", (Convert.ToInt32(price * 100)).ToString());//价格
                    data.SetValue("notify_url", ConfigurationManager.AppSettings["WxTokenUrl"].ToString().Replace("{0}", tid.ToString()));//回调地址
                    data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
                    data.SetValue("time_expire", DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss"));
                    data.SetValue("trade_type", "APP");
                    WxPayData result = WxPayApi.UnifiedOrder(data, tid);
                    string noncestr = WxPayApi.GenerateNonceStr();
                    string timestamp = WxPayApi.GenerateTimeStamp();
                    WxPayData data1 = new WxPayData();
                    data1.SetValue("appid", wx.APPID);
                    data1.SetValue("noncestr", noncestr);
                    data1.SetValue("package", "Sign=WXPay");
                    data1.SetValue("partnerid", wx.MCHID);
                    data1.SetValue("prepayid", result.GetValue("prepay_id"));
                    data1.SetValue("timestamp", timestamp);
                    string sign = data1.MakeSign(tid);
                    string wxstr = "{\"paytype\":\"4\",\"appid\":\"" + result.GetValue("appid") + "\",\"partnerid\":\"" + result.GetValue("mch_id") + "\",\"prepayid\":\"" + result.GetValue("prepay_id") + "\",\"pkg\":\"Sign=WXPay\",\"noncestr\":\"" + noncestr + "\",\"timestamp\":\"" + timestamp + "\",\"sign\":\"" + sign + "\"}";
                    str = "{\"message\":\"成功\",\"result\":100,\"data\":" + wxstr + "}";
                }
                else
                {
                    str = "{\"message\":\"支付通道异常\",\"result\":104}";
                }
            }
            catch
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }

        /// <summary>
        /// 微信支付通道苹果调用方式
        /// </summary>
        /// <param name="tid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string PayWxIos(int appid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            int tid = 0;
            try
            {
                #region 查询微信支付通道id
                string wxappidzfjkios = "wxappidzfjkios" + appid;//组装缓存key值
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface blls = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(wxappidzfjkios))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(wxappidzfjkios);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        tid = int.Parse(dt.Rows[0]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "微信支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = blls.selectAppid("WX", appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        tid = int.Parse(dt.Rows[0]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, wxappidzfjkios, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "微信支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                #endregion
                if (tid > 0)
                {
                    WxPayConfig wx = new WxPayConfig(tid);
                    JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
                    if (!bll.UpdatePay(oid, tid))
                    {
                        str = "{\"message\":\"失败\",\"result\":101}";
                        AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "支付宝支付接口错误", "修改支付渠道失败！");//写入报错日志
                        return str;
                    }
                    WxPayData data = new WxPayData();
                    data.SetValue("body", goodsname);//商品名称
                    data.SetValue("out_trade_no", code); //我们的订单号
                    data.SetValue("total_fee", (Convert.ToInt32(price * 100)).ToString());//价格
                    data.SetValue("notify_url", ConfigurationManager.AppSettings["WxTokenUrl"].ToString().Replace("{0}", tid.ToString()));//回调地址
                    data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
                    data.SetValue("time_expire", DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss"));
                    data.SetValue("trade_type", "APP");
                    WxPayData result = WxPayApi.UnifiedOrder(data, tid);
                    string noncestr = WxPayApi.GenerateNonceStr();
                    string timestamp = WxPayApi.GenerateTimeStamp();
                    WxPayData data1 = new WxPayData();
                    data1.SetValue("appid", wx.APPID);
                    data1.SetValue("noncestr", noncestr);
                    data1.SetValue("package", "Sign=WXPay");
                    data1.SetValue("partnerid", wx.MCHID);
                    data1.SetValue("prepayid", result.GetValue("prepay_id"));
                    data1.SetValue("timestamp", timestamp);
                    string sign = data1.MakeSign(tid);
                    string wxstr = "{\"paytype\":\"4\",\"appid\":\"" + result.GetValue("appid") + "\",\"partnerid\":\"" + result.GetValue("mch_id") + "\",\"prepayid\":\"" + result.GetValue("prepay_id") + "\",\"pkg\":\"Sign=WXPay\",\"noncestr\":\"" + noncestr + "\",\"timestamp\":\"" + timestamp + "\",\"sign\":\"" + sign + "\"}";
                    str = "{\"message\":\"成功\",\"result\":100,\"data\":" + wxstr + "}";
                }
                else
                {
                    str = "{\"message\":\"支付通道异常\",\"result\":104}";
                }
            }
            catch
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }
        #endregion

        #region 威富通公众号支付方式
        /// <summary>
        /// 威富通公众号支付通道H5调用方式
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string PayWftGzhH5(int oid)
        {
            string wftwxgzh = ConfigurationManager.AppSettings["wftwxgzhget"].ToString() + "/wxggh" + oid + ".html";
            // return "http://api.dunxingpay.com/wxggh" + oid + ".html";
            return wftwxgzh;
        }
        #endregion

        #region 威富通app支付方式
        /// <summary>
        /// 威富通应用支付通道安卓调用方式
        /// </summary>
        /// <param name="tid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        /// <param name="privateinfo">商品户私有信息</param>
        ///<param name="TableName">订单表表名</param>
        /// <returns></returns>
        public static string PayWftAppAz(int tid, string code, string goodsname, decimal price, string privateinfo, int oid)
        {
            string str = "";
            ClientResponseHandler resHandler = new ClientResponseHandler();
            PayHttpClient pay = new PayHttpClient();
            RequestHandler reqHandler = new RequestHandler(null);
            Dictionary<string, string> cfg = Utils.loadCfgWxApp(tid);
            JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
            if (!bll.UpdatePay(oid, int.Parse(cfg["pay_id"].ToString())))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            //初始化数据  
            reqHandler.setGateUrl(cfg["req_url"].ToString());
            reqHandler.setKey(cfg["key"].ToString());
            reqHandler.setParameter("out_trade_no", code);//我们的订单号
            reqHandler.setParameter("body", goodsname);//商品描述
            reqHandler.setParameter("attach", privateinfo);//附加信息
            reqHandler.setParameter("total_fee", (Convert.ToInt32(price * 100)).ToString());//价格（已传入的为准，无就从数据库读取）
            reqHandler.setParameter("mch_create_ip", HttpContext.Current.Request.UserHostAddress);//终端IP 
            reqHandler.setParameter("service", "unified.trade.pay");//接口类型
            reqHandler.setParameter("mch_id", cfg["mch_id"].ToString());
            reqHandler.setParameter("version", cfg["version"].ToString());
            reqHandler.setParameter("notify_url", ConfigurationManager.AppSettings["WftTokenUrl"].ToString().Replace("{0}", cfg["pay_id"].ToString()));//回掉地址
            reqHandler.setParameter("nonce_str", Utils.random());
            reqHandler.setParameter("charset", "UTF-8");
            reqHandler.setParameter("sign_type", "MD5");
            reqHandler.setParameter("time_start", DateTime.Now.ToString("yyyyMMddHHmmss")); //订单生成时间
            reqHandler.setParameter("time_expire", DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss"));//订单超时时间
            reqHandler.createSign();
            string datawft = Utils.toXml(reqHandler.getAllParameters());
            Dictionary<string, string> reqContent = new Dictionary<string, string>();
            reqContent.Add("url", reqHandler.getGateUrl());
            reqContent.Add("data", datawft);
            pay.setReqContent(reqContent);
            if (pay.call())
            {
                resHandler.setContent(pay.getResContent());
                resHandler.setKey(cfg["key"].ToString());
                Hashtable param = resHandler.getAllParameters();
                if (resHandler.isTenpaySign())
                {
                    if (int.Parse(param["status"].ToString()) == 0)
                    {

                        string wxpay = "{\"paytype\":\"3\",\"token_id\":\"" + param["token_id"].ToString() + "\", \"services\":\"pay.weixin.app\", \"sign\":\"" + param["sign"] + "\",\"status\":\"0\", \"charset\":\"UTF-8\", \"version\":\"2.0\", \"sign_type\":\"MD5\",\"appid\":\"" + cfg["appid"].ToString() + "\"}";
                        str = "{\"message\":\"成功\",\"result\":100,\"data\":" + wxpay + "}";
                    }
                    else
                    {
                        string wftzfsbxin = "威富通应用安卓接口支付通道支付失败信息，错误代码：" + param["err_code"] + ",错误信息：" + param["err_msg"] + ",商户号：" + cfg["mch_id"].ToString();
                        AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通appid接口错误信息", "报错信息：" + wftzfsbxin);//写入报错日志
                        str = "{\"message\":\"支付通道异常\",\"result\":104}";
                    }
                }
                else
                {
                    string mesage = "威富通支付失败信息，错误代码：" + resHandler.getContent() + ",错误信息：" + resHandler.getDebugInfo() + ",商户号：" + cfg["mch_id"].ToString();
                    AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通appid接口错误信息", "报错信息：" + mesage);//写入报错日志
                    str = "{\"message\":\"支付通道异常\",\"result\":104}";
                }
            }
            else
            {
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通appid接口错误信息", "报错信息：第一步验证错误");//写入报错日志
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }
        /// <summary>
        /// 威富通应用支付通道苹果调用方式
        /// </summary>
        /// <param name="tid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        /// <param name="privateinfo">商品户私有信息</param>
        ///<param name="TableName">订单表表名</param>
        /// <returns></returns>
        public static string PayWftAppIos(int tid, string code, string goodsname, decimal price, string privateinfo, int oid)
        {
            string str = "";
            ClientResponseHandler resHandler = new ClientResponseHandler();
            PayHttpClient pay = new PayHttpClient();
            RequestHandler reqHandler = new RequestHandler(null);
            Dictionary<string, string> cfg = Utils.loadCfgWxApp(tid);
            JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
            if (!bll.UpdatePay(oid, int.Parse(cfg["pay_id"].ToString())))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            //初始化数据  
            reqHandler.setGateUrl(cfg["req_url"].ToString());
            reqHandler.setKey(cfg["key"].ToString());
            reqHandler.setParameter("out_trade_no", code);//我们的订单号
            reqHandler.setParameter("body", goodsname);//商品描述
            reqHandler.setParameter("attach", privateinfo);//附加信息
            reqHandler.setParameter("total_fee", (Convert.ToInt32(price * 100)).ToString());//价格（已传入的为准，无就从数据库读取）
            reqHandler.setParameter("mch_create_ip", HttpContext.Current.Request.UserHostAddress);//终端IP 
            reqHandler.setParameter("service", "unified.trade.pay");
            reqHandler.setParameter("mch_id", cfg["mch_id"].ToString());
            reqHandler.setParameter("version", cfg["version"].ToString());
            reqHandler.setParameter("notify_url", ConfigurationManager.AppSettings["WftTokenUrl"].ToString().Replace("{0}", cfg["pay_id"].ToString()));//回掉地址
            reqHandler.setParameter("nonce_str", Utils.random());
            reqHandler.setParameter("charset", "UTF-8");
            reqHandler.setParameter("sign_type", "MD5");
            reqHandler.setParameter("time_start", DateTime.Now.ToString("yyyyMMddHHmmss")); //订单生成时间
            reqHandler.setParameter("time_expire", DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss"));//订单超时时间
            reqHandler.createSign();
            string datawft = Utils.toXml(reqHandler.getAllParameters());
            Dictionary<string, string> reqContent = new Dictionary<string, string>();
            reqContent.Add("url", reqHandler.getGateUrl());
            reqContent.Add("data", datawft);
            pay.setReqContent(reqContent);
            if (pay.call())
            {
                resHandler.setContent(pay.getResContent());
                resHandler.setKey(cfg["key"].ToString());
                Hashtable param = resHandler.getAllParameters();
                if (resHandler.isTenpaySign())
                {
                    if (int.Parse(param["status"].ToString()) == 0)
                    {
                        string codes = JMP.TOOL.Encrypt.IndexEncrypt(code);
                        string wxpay = "{\"paytype\":\"3\",\"token_id\":\"" + param["token_id"].ToString() + "\", \"services\":\"pay.weixin.app\", \"sign\":\"" + param["sign"] + "\",\"status\":\"0\", \"charset\":\"UTF-8\", \"version\":\"2.0\", \"sign_type\":\"MD5\",\"appid\":\"" + cfg["appid"].ToString() + "\",\"code\":\"" + codes + "\"}";
                        str = "{\"message\":\"成功\",\"result\":100,\"data\":" + wxpay + "}";
                    }
                    else
                    {
                        string wftzfsbxin = "威富通应用IOS接口支付通道支付失败信息，错误代码：" + param["err_code"] + ",错误信息：" + param["err_msg"] + ",商户号：" + cfg["mch_id"].ToString();
                        AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通appid接口错误信息", "报错信息：" + wftzfsbxin);//写入报错日志
                        str = "{\"message\":\"支付通道异常\",\"result\":104}";
                    }
                }
                else
                {
                    string mesage = "威富通支付失败信息，错误代码：" + resHandler.getContent() + ",错误信息：" + resHandler.getDebugInfo() + ",商户号：" + cfg["mch_id"].ToString();
                    AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通appid接口错误信息", "报错信息：" + mesage);//写入报错日志
                    str = "{\"message\":\"支付通道异常\",\"result\":104}";
                }
            }
            else
            {
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "威富通appid接口错误信息", "报错信息：第一步验证错误");//写入报错日志
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }
        #endregion

        #region 南粤公众号支付
        /// <summary>
        /// 南粤公众号支付
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="price">商品价格</param>
        /// <param name="oid">订单id</param>
        /// <param name="goodsname">商品名称</param>
        /// <returns></returns>
        public static string NyGzhH5(int tid, string code, decimal price, int oid, string goodsname)
        {
            string str = "";

            string userid = "";//南粤公众号商户id
            string userkey = "";//南粤公众号key
            int pay_id = 0;//支付渠道id
            string Nywxgzh = "Nywxgzh" + tid;//组装缓存key值
            #region 南粤公众号支付账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(Nywxgzh))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(Nywxgzh);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的南粤公众号id
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的南粤公众号key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "南粤公众号支付支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("NYGZH", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取南粤公众号id
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取南粤公众号key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, Nywxgzh, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "南粤公众号支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "南粤公众号支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "南粤公众号支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            Dictionary<string, string> strlist = new Dictionary<string, string>();
            strlist.Add("tradeType", "cs.pay.submit");//交易类型
            strlist.Add("version", "1.3");//版本号
            strlist.Add("mchId", userid);//代理商号
            strlist.Add("channel", "wxPub");//支付渠道
            strlist.Add("body", goodsname);//商品描述
            strlist.Add("outTradeNo", code);//商户订单号
            strlist.Add("amount", price.ToString());//交易金额
            //strlist.Add("description", JMP.TOOL.DESEncrypt.Encrypt(code));//自定义信息
            strlist.Add("notifyUrl", ConfigurationManager.AppSettings["NyNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步通知
            strlist.Add("callbackUrl", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", oid.ToString()));//同步通知
            string md5str = JMP.TOOL.UrlStr.AzGetStr(strlist) + "&key=" + userkey;
            string md5 = JMP.TOOL.MD5.md5strGet(md5str, true);
            strlist.Add("sign", md5);//签名
            string extra = "";
            if (tid == 71)//判断应用类型是否需要禁用信用卡
            {
                extra = "{\"callbackUrl\":\"" + ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", oid.ToString()) + "\",\"notifyUrl\":\"" + ConfigurationManager.AppSettings["NyNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()) + "\",\"notifyUrl\":\"no_credit\"}";
            }
            else
            {
                extra = "{\"callbackUrl\":\"" + ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", oid.ToString()) + "\",\"notifyUrl\":\"" + ConfigurationManager.AppSettings["NyNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()) + "\"}";
            }
            strlist.Add("extra", extra);//扩展字段
            string postString = JMP.TOOL.JsonHelper.DictJsonstr(strlist, "extra");//这里即为传递的参数，可以用工具抓包分析，也可以自己分析，主要是form里面每一个name都要加进来 
            byte[] postData = Encoding.UTF8.GetBytes(postString);//编码，尤其是汉字，事先要看下抓取网页的编码方式  
            string url = ConfigurationManager.AppSettings["NYPOSTUrl"].ToString();//请求地址  
            WebClient webClient = new WebClient();
            byte[] responseData = webClient.UploadData(url, "POST", postData);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码  
            Dictionary<string, object> jsonstr = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
            if (jsonstr.ContainsKey("returnCode") && jsonstr["resultCode"].ToString() == "0")
            {
                // str = "{" + "\"paytype\":\" wxapp\"," + jsonstr["payCode"].ToString().Replace("{", "").Replace("}", "").Replace("package", "pkg") + "}";
                str = jsonstr["payCode"].ToString();
                //str = "{\"message\":\"成功\",\"result\":100,\"data\":\"" + str + "\"}";
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }

            return str;
        }
        #endregion

        #region 南粤微信扫码支付
        /// <summary>
        /// 南粤微信扫码支付
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="price">商品价格</param>
        /// <param name="oid">订单id</param>
        /// <param name="goodsname">商品名称</param>
        /// <returns></returns>
        public static string NywxsmH5(int tid, string code, decimal price, int oid, string goodsname)
        {
            string str = "";

            string userid = "";//南粤公众号商户id
            string userkey = "";//南粤公众号key
            int pay_id = 0;//支付渠道id
            string Nywxgzh = "Nywxgzh" + tid;//组装缓存key值
            #region 南粤微信扫码支付账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(Nywxgzh))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(Nywxgzh);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的南粤微信扫码id
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的南粤微信扫码key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "南粤微信扫码支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("nywxsm", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取南粤公众号id
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取南粤公众号key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, Nywxgzh, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "南粤微信扫码接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "南粤微信扫码支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "南粤微信扫码支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            Dictionary<string, string> strlist = new Dictionary<string, string>();
            strlist.Add("tradeType", "cs.pay.submit");//交易类型
            strlist.Add("version", "1.3");//版本号
            strlist.Add("mchId", userid);//代理商号
            strlist.Add("channel", "wxPubQR");//支付渠道
            strlist.Add("body", goodsname);//商品描述
            strlist.Add("outTradeNo", code);//商户订单号
            strlist.Add("amount", price.ToString());//交易金额
            //strlist.Add("description", JMP.TOOL.DESEncrypt.Encrypt(code));//自定义信息
            strlist.Add("notifyUrl", ConfigurationManager.AppSettings["NyNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步通知
            strlist.Add("callbackUrl", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", oid.ToString()));//同步通知
            string md5str = JMP.TOOL.UrlStr.AzGetStr(strlist) + "&key=" + userkey;
            string md5 = JMP.TOOL.MD5.md5strGet(md5str, true);
            strlist.Add("sign", md5);//签名
            string extra = "";
            //if (tid == 71)//判断应用类型是否需要禁用信用卡
            //{
            //    extra = "{\"callbackUrl\":\"" + ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", oid.ToString()) + "\",\"notifyUrl\":\"" + ConfigurationManager.AppSettings["NyNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()) + "\",\"notifyUrl\":\"no_credit\"}";
            //}
            //else
            //{
            extra = "{\"callbackUrl\":\"" + ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", oid.ToString()) + "\",\"notifyUrl\":\"" + ConfigurationManager.AppSettings["NyNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()) + "\"}";
            // }
            strlist.Add("extra", extra);//扩展字段
            string postString = JMP.TOOL.JsonHelper.DictJsonstr(strlist, "extra");//这里即为传递的参数，可以用工具抓包分析，也可以自己分析，主要是form里面每一个name都要加进来 
            byte[] postData = Encoding.UTF8.GetBytes(postString);//编码，尤其是汉字，事先要看下抓取网页的编码方式  
            string url = ConfigurationManager.AppSettings["NYPOSTUrl"].ToString();//请求地址  
            WebClient webClient = new WebClient();
            byte[] responseData = webClient.UploadData(url, "POST", postData);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码  
            Dictionary<string, object> jsonstr = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
            if (jsonstr.ContainsKey("returnCode") && jsonstr["resultCode"].ToString() == "0")
            {
                string qurl = jsonstr["codeUrl"].ToString() + "," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ",2";//组装二维码地址
                string qrcodeurl = ConfigurationManager.AppSettings["QRcodeUrl"].ToString() + "?QRcode=" + JMP.TOOL.Encrypt.IndexEncrypt(qurl);//二维码展示地址
                str = qrcodeurl;
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }

            return str;
        }
        #endregion

        #region 南粤app支付
        /// <summary>
        /// 南粤app支付安卓调用方式
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="price">商品价格</param>
        /// <param name="oid">订单id</param>
        /// <param name="goodsname">商品名称</param>
        /// <returns></returns>
        public static string NyAppidAz(int appid, string code, decimal price, int oid, string goodsname)
        {
            string str = "";
            string userid = "";//南粤app商户id
            string userkey = "";//南粤appkey
            string wxappid = "";//微信appid
            int pay_id = 0;//支付渠道id
            string Nyappid = "Nyappid" + appid;//组装缓存key值
            #region 南粤app支付账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(Nyappid))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(Nyappid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的南粤app账号id
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的南粤appkey
                        wxappid = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//获取缓存中的微信appid
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "南粤app支付支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.selectAppid("NYAPP", appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取南粤app
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取南粤appkey
                        wxappid = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//获取缓存中的微信appid
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, Nyappid, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "南粤app支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "南粤app支付接口错误应用ID：" + appid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "南粤app支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            Dictionary<string, string> strlist = new Dictionary<string, string>();
            strlist.Add("tradeType", "cs.pay.submit");//交易类型
            strlist.Add("version", "1.3");//版本号
            strlist.Add("mchId", userid);//代理商号
            strlist.Add("channel", "wxApp");//支付渠道wxApp wxPub
            strlist.Add("body", goodsname);//商品描述
            strlist.Add("outTradeNo", code);//商户订单号
            strlist.Add("amount", price.ToString());//交易金额
            //strlist.Add("description", JMP.TOOL.Encrypt.IndexEncrypt(code));//自定义信息
            strlist.Add("mobileAppId", wxappid);//appid时需要传入
            strlist.Add("notifyUrl", ConfigurationManager.AppSettings["NyNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步通知
            string md5str = JMP.TOOL.UrlStr.AzGetStr(strlist) + "&key=" + userkey;
            string md5 = JMP.TOOL.MD5.md5strGet(md5str, true);
            strlist.Add("sign", md5);//签名
            string extra = "{\"mobileAppId\":\"" + wxappid + "\",\"notifyUrl\":\"" + ConfigurationManager.AppSettings["NyNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()) + "\"}";
            strlist.Add("extra", extra);//扩展字段
            string postString = JMP.TOOL.JsonHelper.DictJsonstr(strlist, "extra");//这里即为传递的参数，可以用工具抓包分析，也可以自己分析，主要是form里面每一个name都要加进来  
            byte[] postData = Encoding.UTF8.GetBytes(postString);//编码，尤其是汉字，事先要看下抓取网页的编码方式  
            string url = ConfigurationManager.AppSettings["NYPOSTUrl"].ToString();//地址  
            WebClient webClient = new WebClient();
            byte[] responseData = webClient.UploadData(url, "POST", postData);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码  
            Dictionary<string, object> jsonstr = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
            if (jsonstr.ContainsKey("returnCode") && jsonstr["resultCode"].ToString() == "0")
            {
                str = "{\"paytype\":\"4\"," + jsonstr["payCode"].ToString().Replace("{", "").Replace("}", "").Replace("package", "pkg") + "}";
                str = "{\"message\":\"成功\",\"result\":100,\"data\":" + str + "}";
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }
        /// <summary>
        /// 南粤app支付苹果调用方式
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="price">商品价格</param>
        /// <param name="oid">订单id</param>
        /// <param name="goodsname">商品名称</param>
        /// <returns></returns>
        public static string NyAppidIos(int appid, string code, decimal price, int oid, string goodsname)
        {
            string str = "";
            string userid = "";//南粤app商户id
            string userkey = "";//南粤appkey
            string wxappid = "";//微信appid
            int pay_id = 0;//支付渠道id
            string Nyappid = "Nyappid" + appid;//组装缓存key值
            #region 南粤app支付账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(Nyappid))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(Nyappid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的南粤app账号id
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的南粤appkey
                        wxappid = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//获取缓存中的微信appid
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "南粤app支付支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.selectAppid("NYAPP", appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取南粤app
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取南粤appkey
                        wxappid = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//获取缓存中的微信appid
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, Nyappid, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "南粤app支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "南粤app支付接口错误应用ID：" + appid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "南粤app支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            Dictionary<string, string> strlist = new Dictionary<string, string>();
            strlist.Add("tradeType", "cs.pay.submit");//交易类型
            strlist.Add("version", "1.3");//版本号
            strlist.Add("mchId", userid);//代理商号
            strlist.Add("channel", "wxApp");//支付渠道wxApp wxPub
            strlist.Add("body", goodsname);//商品描述
            strlist.Add("outTradeNo", code);//商户订单号
            strlist.Add("amount", price.ToString());//交易金额
            //strlist.Add("description", JMP.TOOL.DESEncrypt.Encrypt(code));//自定义信息
            strlist.Add("mobileAppId", wxappid);//appid时需要传入
            strlist.Add("notifyUrl", ConfigurationManager.AppSettings["NyNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步通知
            string md5str = JMP.TOOL.UrlStr.AzGetStr(strlist) + "&key=" + userkey;
            string md5 = JMP.TOOL.MD5.md5strGet(md5str, true);
            strlist.Add("sign", md5);//签名
            string extra = "{\"mobileAppId\":\"" + wxappid + "\",\"notifyUrl\":\"" + ConfigurationManager.AppSettings["NyNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()) + "\"}";
            strlist.Add("extra", extra);//扩展字段
            string postString = JMP.TOOL.JsonHelper.DictJsonstr(strlist, "extra");//这里即为传递的参数，可以用工具抓包分析，也可以自己分析，主要是form里面每一个name都要加进来  
            byte[] postData = Encoding.UTF8.GetBytes(postString);//编码，尤其是汉字，事先要看下抓取网页的编码方式  
            string url = ConfigurationManager.AppSettings["NYPOSTUrl"].ToString();//地址  
            WebClient webClient = new WebClient();
            byte[] responseData = webClient.UploadData(url, "POST", postData);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码  
            Dictionary<string, object> jsonstr = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
            if (jsonstr.ContainsKey("returnCode") && jsonstr["resultCode"].ToString() == "0")
            {
                string codes = JMP.TOOL.Encrypt.IndexEncrypt(code);
                str = "{\"code\":\"" + codes + "\",\"paytype\":\"4\"," + jsonstr["payCode"].ToString().Replace("{", "").Replace("}", "").Replace("package", "pkg") + "}";
                str = "{\"message\":\"成功\",\"result\":100,\"data\":" + str + "}";
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }
        #endregion

        #region 首游支付宝
        /// <summary>
        /// 首游支付宝h5调用方式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string SyZfbH5(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            string uid = "";//商户编号
            string MD5KEY = "";//key
            int pay_id = 0;//支付通道id
            string syZFBjkhcH5 = "syZFBjkhcH5" + tid;//组装缓存key值
            #region 获取首游支付宝账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(syZFBjkhcH5))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(syZFBjkhcH5);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的首游支付宝编号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的首游支付宝key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "首游支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("SYZFB", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取首游支付宝编号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取首游支付宝key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, syZFBjkhcH5, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "首游支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "首游支付宝支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "首游支付宝支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();
            PostVars.Add("mchid", uid);//商户号
            PostVars.Add("paytype", "pay.alipay.wap");//支付类型
            PostVars.Add("time", DateTime.Now.ToString("yyyyMMddHHmmss"));//订单生成的时间
            PostVars.Add("amount", (Convert.ToInt32(price * 100)).ToString());//总金额，单位：分(price * 100).ToString()
            PostVars.Add("out_tradeid", code);//订单编号
            PostVars.Add("subject", goodsname);//商品描述
            //PostVars.Add("attach", JMP.TOOL.DESEncrypt.Encrypt(code));//附加信息
            PostVars.Add("clientip", HttpContext.Current.Request.UserHostAddress);//支付用户的终端 IP
            PostVars.Add("version", "1.0");//版本号
            PostVars.Add("returnurl", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", oid.ToString()));//同步跳转地址 
            PostVars.Add("notifyurl", ConfigurationManager.AppSettings["SYNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步跳转地址 
            string md5strs = PostVars["paytype"] + PostVars["mchid"].ToString() + PostVars["amount"].ToString() + PostVars["out_tradeid"].ToString() + PostVars["time"].ToString() + MD5KEY;
            string md5s = JMP.TOOL.MD5.md5strGet(md5strs, true).ToUpper(); //ToLower()
            PostVars.Add("sign", md5s);//签名
            string url = ConfigurationManager.AppSettings["SYPOSTUrl"].ToString();//地址  
            WebClient webClient = new WebClient();
            byte[] byRemoteInfo = webClient.UploadValues(url, "POST", PostVars);
            string srcString = Encoding.UTF8.GetString(byRemoteInfo);//解码  
            Dictionary<string, object> jsonstr = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
            if (jsonstr.ContainsKey("pay_info") && jsonstr["code"].ToString() == "1")
            {
                // str = "{\"message\":\"成功\",\"result\":100,\"data\":{\"pay\":\"" + jsonstr["pay_info"].ToString() + "\"}}";
                str = jsonstr["pay_info"].ToString();
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }

        /// <summary>
        /// 首游支付宝安卓调用方式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string SyZfbAz(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            string uid = "";//商户编号
            string MD5KEY = "";//key
            int pay_id = 0;//支付通道id
            string syZFBjkhcAz = "syZFBjkhcAz" + tid;//组装缓存key值
            #region 获取首游支付宝账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(syZFBjkhcAz))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(syZFBjkhcAz);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的首游支付宝编号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的首游支付宝key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "首游支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("SYZFB", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取首游支付宝编号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取首游支付宝key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, syZFBjkhcAz, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "首游支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "首游支付宝支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "首游支付宝支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();
            PostVars.Add("mchid", uid);//商户号
            PostVars.Add("paytype", "pay.alipay.wap");//支付类型
            PostVars.Add("time", DateTime.Now.ToString("yyyyMMddHHmmss"));//订单生成的时间
            PostVars.Add("amount", (Convert.ToInt32(price * 100)).ToString());//总金额，单位：分(price * 100).ToString()
            PostVars.Add("out_tradeid", code);//订单编号
            PostVars.Add("subject", goodsname);//商品描述
            //PostVars.Add("attach", JMP.TOOL.DESEncrypt.Encrypt(code));//附加信息
            PostVars.Add("clientip", HttpContext.Current.Request.UserHostAddress);//支付用户的终端 IP
            PostVars.Add("version", "1.0");//版本号
            PostVars.Add("returnurl", "http://www.baidu.com");//同步跳转地址 
            PostVars.Add("notifyurl", ConfigurationManager.AppSettings["SYNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步跳转地址 
            string md5strs = PostVars["paytype"] + PostVars["mchid"].ToString() + PostVars["amount"].ToString() + PostVars["out_tradeid"].ToString() + PostVars["time"].ToString() + MD5KEY;
            string md5s = JMP.TOOL.MD5.md5strGet(md5strs, true).ToUpper(); //ToLower()
            PostVars.Add("sign", md5s);//签名
            string url = ConfigurationManager.AppSettings["SYPOSTUrl"].ToString();//地址  
            //StringBuilder html = new StringBuilder();
            //html.AppendLine(" <form id='sypay' name='sypay'  method='POST' action='" + url + "'  > ");
            //foreach (string key in PostVars.Keys)
            //{
            //    html.AppendLine(" <input type='hidden' name ='" + key + "' value ='" + PostVars[key] + "'  />  ");
            //}
            //html.AppendLine("</form>");
            //html.AppendLine("<script> document.forms['sypay'].submit();</script>");
            WebClient webClient = new WebClient();
            byte[] byRemoteInfo = webClient.UploadValues(url, "POST", PostVars);
            string srcString = Encoding.UTF8.GetString(byRemoteInfo);//解码  
            Dictionary<string, object> jsonstr = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
            if (jsonstr.ContainsKey("pay_info") && jsonstr["code"].ToString() == "1")
            {
                str = "{\"message\":\"成功\",\"result\":100,\"data\":{\"pay\":\"" + jsonstr["pay_info"].ToString() + "\"}}";
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }

        /// <summary>
        /// 首游支付宝ios模式
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="code"></param>
        /// <param name="goodsname"></param>
        /// <param name="price"></param>
        /// <param name="oid"></param>
        /// <returns></returns>
        public static string SyZfbIOS(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            string uid = "";//商户编号
            string MD5KEY = "";//key
            int pay_id = 0;//支付通道id
            string sign = "";//签名
            string syZFBjkhcIos = "syZFBjkhcios" + tid;//组装缓存key值
            #region 获取首游支付宝账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(syZFBjkhcIos))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(syZFBjkhcIos);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的首游支付宝编号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的首游支付宝key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "首游支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("SYZFB", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取贝贝支付宝编号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取贝贝支付宝key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, syZFBjkhcIos, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "首游支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "首游支付宝支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "首游支付宝支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();
            PostVars.Add("mchid", uid);//商户号
            PostVars.Add("paytype", "pay.alipay.wap");//支付类型
            PostVars.Add("time", DateTime.Now.ToString("yyyyMMddHHmmss"));//订单生成的时间
            PostVars.Add("amount", (Convert.ToInt32(price * 100)).ToString());//总金额，单位：分(price * 100).ToString()
            PostVars.Add("out_tradeid", code);//订单编号
            PostVars.Add("subject", goodsname);//商品描述
            //PostVars.Add("attach", JMP.TOOL.DESEncrypt.Encrypt(code));//附加信息
            PostVars.Add("clientip", HttpContext.Current.Request.UserHostAddress);//支付用户的终端 IP
            PostVars.Add("version", "1.0");//版本号
            PostVars.Add("returnurl", "http://www.baidu.com");//同步跳转地址 
            PostVars.Add("notifyurl", ConfigurationManager.AppSettings["SYNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步跳转地址 
            string md5strs = PostVars["paytype"] + PostVars["mchid"].ToString() + PostVars["amount"].ToString() + PostVars["out_tradeid"].ToString() + PostVars["time"].ToString() + MD5KEY;
            string md5s = JMP.TOOL.MD5.md5strGet(md5strs, true).ToUpper(); //ToLower()
            PostVars.Add("sign", md5s);//签名
            string url = ConfigurationManager.AppSettings["SYPOSTUrl"].ToString();//地址  
            WebClient webClient = new WebClient();
            byte[] byRemoteInfo = webClient.UploadValues(url, "POST", PostVars);
            string srcString = Encoding.UTF8.GetString(byRemoteInfo);//解码  
            Dictionary<string, object> jsonstr = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
            if (jsonstr.ContainsKey("pay_info") && jsonstr["code"].ToString() == "1")
            {
                str = "{\"message\":\"成功\",\"result\":100,\"data\":{\"pay\":\"" + jsonstr["pay_info"].ToString() + "\"}}";
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }

        #endregion

        #region 首游wap支付
        /// <summary>
        /// 首游Wap支付h5调用方式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string SyWxWapH5(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            string uid = "";//商户编号
            string MD5KEY = "";//key
            int pay_id = 0;//支付通道id
            string syWxWapjkhcH5 = "syWxWapjkhcH5" + tid;//组装缓存key值
            #region 获取首游Wap账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(syWxWapjkhcH5))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(syWxWapjkhcH5);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的首游支付宝编号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的首游支付宝key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "首游wap支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("SYWAP", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取首游支付宝编号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取首游支付宝key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, syWxWapjkhcH5, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "首游支wap支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "首游wap支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "首游支付宝支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();
            PostVars.Add("mchid", uid);//商户号
            PostVars.Add("paytype", "pay.weixin.wap");//支付类型
            PostVars.Add("time", DateTime.Now.ToString("yyyyMMddHHmmss"));//订单生成的时间
            PostVars.Add("amount", (Convert.ToInt32(price * 100)).ToString());//总金额，单位：分(price * 100).ToString()
            PostVars.Add("out_tradeid", code);//订单编号
            PostVars.Add("subject", goodsname);//商品描述
                                               // PostVars.Add("attach", JMP.TOOL.DESEncrypt.Encrypt(code));//附加信息
            PostVars.Add("clientip", HttpContext.Current.Request.UserHostAddress);//支付用户的终端 IP
            PostVars.Add("version", "1.0");//版本号
            PostVars.Add("returnurl", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", oid.ToString()));//同步跳转地址 
            PostVars.Add("notifyurl", ConfigurationManager.AppSettings["SYNotifyUrl"].ToString());//异步跳转地址 
            string md5strs = PostVars["paytype"] + PostVars["mchid"].ToString() + PostVars["amount"].ToString() + PostVars["out_tradeid"].ToString() + PostVars["time"].ToString() + MD5KEY;
            string md5s = JMP.TOOL.MD5.md5strGet(md5strs, true).ToUpper(); //ToLower()
            PostVars.Add("sign", md5s);//签名
            string url = ConfigurationManager.AppSettings["SYPOSTUrl"].ToString();//地址  
            WebClient webClient = new WebClient();
            byte[] byRemoteInfo = webClient.UploadValues(url, "POST", PostVars);
            string srcString = Encoding.UTF8.GetString(byRemoteInfo);//解码  
            Dictionary<string, object> jsonstr = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
            if (jsonstr.ContainsKey("pay_info") && jsonstr["code"].ToString() == "1")
            {
                // str = "{\"message\":\"成功\",\"result\":100,\"data\":\"" + jsonstr["pay_info"].ToString() + "\"}";
                str = jsonstr["pay_info"].ToString();
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }


        /// <summary>
        /// 首游微信wap安卓调用方式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string SyWxWapAz(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            string uid = "";//商户编号
            string MD5KEY = "";//key
            int pay_id = 0;//支付通道id
            string syWxWapjkhcAz = "syWxWapjkhcAz" + tid;//组装缓存key值
            #region 获取首游微信wap账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(syWxWapjkhcAz))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(syWxWapjkhcAz);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的首游微信wap编号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的首游微信wapkey
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "首游微信wap支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("SYWAP", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取首游微信wap编号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取首游微信wapkey
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, syWxWapjkhcAz, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "首游微信wap支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "首游微信wap支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "首游支付宝支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();
            PostVars.Add("mchid", uid);//商户号
            PostVars.Add("paytype", "pay.weixin.wap");//支付类型
            PostVars.Add("time", DateTime.Now.ToString("yyyyMMddHHmmss"));//订单生成的时间
            PostVars.Add("amount", (Convert.ToInt32(price * 100)).ToString());//总金额，单位：分(price * 100).ToString()
            PostVars.Add("out_tradeid", code);//订单编号
            PostVars.Add("subject", goodsname);//商品描述
                                               // PostVars.Add("attach", JMP.TOOL.DESEncrypt.Encrypt(code));//附加信息
            PostVars.Add("clientip", HttpContext.Current.Request.UserHostAddress);//支付用户的终端 IP
            PostVars.Add("version", "1.0");//版本号
            PostVars.Add("returnurl", "http://www.baidu.com");//同步跳转地址 
            PostVars.Add("notifyurl", ConfigurationManager.AppSettings["SYNotifyUrl"].ToString());//异步跳转地址 
            string md5strs = PostVars["paytype"] + PostVars["mchid"].ToString() + PostVars["amount"].ToString() + PostVars["out_tradeid"].ToString() + PostVars["time"].ToString() + MD5KEY;
            string md5s = JMP.TOOL.MD5.md5strGet(md5strs, true).ToUpper(); //ToLower()
            PostVars.Add("sign", md5s);//签名
            string url = ConfigurationManager.AppSettings["SYPOSTUrl"].ToString();//地址  
            WebClient webClient = new WebClient();
            byte[] byRemoteInfo = webClient.UploadValues(url, "POST", PostVars);
            string srcString = Encoding.UTF8.GetString(byRemoteInfo);//解码  
            Dictionary<string, object> jsonstr = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
            if (jsonstr.ContainsKey("pay_info") && jsonstr["code"].ToString() == "1")
            {
                str = "{\"paytype\":\"2\",\"token_id\":\"" + jsonstr["pay_info"].ToString() + "\"}";
                str = "{\"message\":\"成功\",\"result\":100,\"data\":" + str + "}";
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }

        /// <summary>
        /// 首游微信wap ios模式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string SyWxWapIOS(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            string uid = "";//商户编号
            string MD5KEY = "";//key
            int pay_id = 0;//支付通道id
            string sywxwapjkhcIos = "sywxwapjkhcIos" + tid;//组装缓存key值
            #region 获取首游微信wap账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(sywxwapjkhcIos))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(sywxwapjkhcIos);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的首游微信wap编号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的首游微信wapkey
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "首游微信wap ios支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("SYWAP", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取首游账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取首游key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, sywxwapjkhcIos, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "首游微信wap ios支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "首游微信wap ios支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "首游微信wap ios支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();
            PostVars.Add("mchid", uid);//商户号
            PostVars.Add("paytype", "pay.weixin.wap");//支付类型
            PostVars.Add("time", DateTime.Now.ToString("yyyyMMddHHmmss"));//订单生成的时间
            PostVars.Add("amount", (Convert.ToInt32(price * 100)).ToString());//总金额，单位：分(price * 100).ToString()
            PostVars.Add("out_tradeid", code);//订单编号
            PostVars.Add("subject", goodsname);//商品描述
                                               // PostVars.Add("attach", JMP.TOOL.DESEncrypt.Encrypt(code));//附加信息
            PostVars.Add("clientip", HttpContext.Current.Request.UserHostAddress);//支付用户的终端 IP
            PostVars.Add("version", "1.0");//版本号
            PostVars.Add("returnurl", "http://www.baidu.com");//同步跳转地址 
            PostVars.Add("notifyurl", ConfigurationManager.AppSettings["SYNotifyUrl"].ToString());//异步跳转地址 
            string md5strs = PostVars["paytype"] + PostVars["mchid"].ToString() + PostVars["amount"].ToString() + PostVars["out_tradeid"].ToString() + PostVars["time"].ToString() + MD5KEY;
            string md5s = JMP.TOOL.MD5.md5strGet(md5strs, true).ToUpper(); //ToLower()
            PostVars.Add("sign", md5s);//签名
            string url = ConfigurationManager.AppSettings["SYPOSTUrl"].ToString();//地址  
            WebClient webClient = new WebClient();
            byte[] byRemoteInfo = webClient.UploadValues(url, "POST", PostVars);
            string srcString = Encoding.UTF8.GetString(byRemoteInfo);//解码  
            Dictionary<string, object> jsonstr = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
            if (jsonstr.ContainsKey("pay_info") && jsonstr["code"].ToString() == "1")
            {
                str = "{\"paytype\":\"2\",\"token_id\":\"" + jsonstr["pay_info"].ToString() + "\"}";
                str = "{\"message\":\"成功\",\"result\":100,\"data\":" + str + "}";
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }
        #endregion

        #region 首游appid支付
        /// <summary>
        /// 首游appid支付安卓调用方式
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string SyAppidAz(int appid, string code, int oid)
        {
            string str = "";
            string userid = "";//首游商户id
            string userkey = "";//首游key
            int pay_id = 0;//支付渠道id
            string syappidzfjkhc = "syappidzfjkhc" + appid;//组装缓存key值
            #region 首游app支付账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(syappidzfjkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(syappidzfjkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的首游账号id
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的首游key
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "首游appid支付支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.selectAppid("SYAPPID", appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取众首游账号
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取首游key
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, syappidzfjkhc, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "首游appid支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "首游appid支付接口错误应用ID：" + appid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "首游appid支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            string wxpay = "{\"paytype\":\"5\",\"orderid\":\"" + code + "\", \"syskname\":\"" + userid + "\"}";
            str = "{\"message\":\"成功\",\"result\":100,\"data\":" + wxpay + "}";
            return str;
        }
        /// <summary>
        /// 首游appid支付ios调用方式
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string SyAppidIos(int appid, string code, int oid)
        {
            string str = "";
            string userid = "";//首游商户id
            string userkey = "";//首游key
            int pay_id = 0;//支付渠道id
            string SyAppidIoszfjkhc = "SyAppidIoszfjkhc" + appid;//组装缓存key值
            #region 首游app支付账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(SyAppidIoszfjkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(SyAppidIoszfjkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的首游账号id
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的首游key
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "首游appid支付支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.selectAppid("SYAPPID", appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取众首游账号
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取首游key
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, SyAppidIoszfjkhc, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "首游appid支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "首游appid支付接口错误应用ID：" + appid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "首游appid支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            string codes = JMP.TOOL.Encrypt.IndexEncrypt(code);
            string wxpay = "{\"paytype\":\"5\",\"orderid\":\"" + code + "\", \"syskname\":\"" + userid + "\",\"code\":\"" + codes + "\"}";
            str = "{\"message\":\"成功\",\"result\":100,\"data\":" + wxpay + "}";
            return str;
        }
        #endregion

        #region 首游公众号支付
        /// <summary>
        /// 首游公众号支付
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="price">商品价格</param>
        /// <param name="oid">订单id</param>
        /// <param name="goodsname">商品名称</param>
        /// <returns></returns>
        public static string SyGzhH5(int tid, string code, decimal price, int oid, string goodsname)
        {
            string str = "";
            string userid = "";//首游公众号支付商户id
            string userkey = "";//首游公众号支付key
            int pay_id = 0;//支付渠道id
            string Nywxgzh = "Nywxgzh" + tid;//组装缓存key值
            #region 首游公众号支付账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(Nywxgzh))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(Nywxgzh);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的首游公众号支付id
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的首游公众号支付key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "首游公众号支付支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("sywxgzh", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取南粤公众号id
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取南粤公众号key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, Nywxgzh, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "首游公众号支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "首游公众号支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "首游公众号支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();
            PostVars.Add("channel", userid);//商户号
            PostVars.Add("order_id", code);//订单号
            PostVars.Add("money", (Convert.ToInt32(price * 100)).ToString());//金额单位：分
            PostVars.Add("pay_type", "wxwap");//支付方式
            PostVars.Add("time", JMP.TOOL.WeekDateTime.GetMilis);//unix 时间戳
            PostVars.Add("notify_url", ConfigurationManager.AppSettings["SygzhNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步通知地址
            PostVars.Add("return_url", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", oid.ToString()));//同步成功跳转地址
            string md5str = PostVars["channel"] + PostVars["order_id"] + PostVars["money"] + PostVars["pay_type"] + PostVars["time"] + JMP.TOOL.MD5.md5strGet(userkey, true).ToLower();
            string md5 = JMP.TOOL.MD5.md5strGet(md5str, true).ToLower();
            PostVars.Add("sign", md5);//签名
            WebClient webClient = new WebClient();
            string url = ConfigurationManager.AppSettings["SygzhPOSTUrl"].ToString();//请求地址  
            byte[] byRemoteInfo = webClient.UploadValues(url, "POST", PostVars);
            string srcString = Encoding.UTF8.GetString(byRemoteInfo);//解码  
            Dictionary<string, object> jsonstr = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
            if ((bool)jsonstr["net"] == true && (bool)jsonstr["ok"] == true && jsonstr.ContainsKey("data") && (jsonstr["data"].ToString().StartsWith("https://") || jsonstr["data"].ToString().StartsWith("http://")))
            {
                str = jsonstr["data"].ToString();
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }
        #endregion

        #region 鹏缘支付宝
        /// <summary>
        /// 鹏缘支付宝支付h5模式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string PyZfbH5(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            string uid = "";//商户编号
            string MD5KEY = "";//key
            int pay_id = 0;//支付通道id
            string PyZFBjkhc = "PyZFBjkhc" + tid;//组装缓存key值
            #region 获取鹏缘支付宝账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(PyZFBjkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(PyZFBjkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的鹏缘账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的鹏缘key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "鹏缘支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("PYZFB", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取贝贝鹏缘账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取鹏缘key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, PyZFBjkhc, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "鹏缘支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "鹏缘支付宝支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "鹏缘支付宝支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            NameValueCollection palis = new NameValueCollection();
            palis.Add("version", "2.0");//版本号
            palis.Add("organno", "123456");//机构号
            palis.Add("merchno", uid);//商户号
            palis.Add("paytype", "006");//支付类型
            palis.Add("remark", goodsname);//商品描述
            palis.Add("proname", goodsname);//商品名称
            palis.Add("ordno", code);//订单号
            palis.Add("price", (Convert.ToInt32(price * 100)).ToString());//金额（单位：分）
            palis.Add("callbackurl", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", oid.ToString()));//同步通知地址
            palis.Add("notifyurl", ConfigurationManager.AppSettings["pyzfbNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步通知地址
            palis.Add("cpchannel", "pf2");//渠道号
            Dictionary<string, string> list = palis.Cast<string>().ToDictionary(x => x, x => palis[x]);
            string md5 = JMP.TOOL.UrlStr.AzGetStr(list) + "&" + MD5KEY;
            string md5str = JMP.TOOL.MD5.md5strGet(md5, true).ToLower();
            palis.Add("sign", md5str);//签名
            string url = ConfigurationManager.AppSettings["pyzfbPOSTUrl"].ToString();//请求地址  
            WebClient webClient = new WebClient();
            byte[] responseData = webClient.UploadValues(url, "POST", palis);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码  
            Dictionary<string, object> jsonstr = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
            if (jsonstr.ContainsKey("url") && jsonstr["code"].ToString() == "10")
            {
                //str = "{\"message\":\"成功\",\"result\":100,\"data\":{\"pay\":\"" + jsonstr["url"].ToString() + "\"}}";
                str = jsonstr["url"].ToString();
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }
        /// <summary>
        /// 鹏缘支付宝支付安卓模式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string PyzfbAz(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            string uid = "";//商户编号
            string MD5KEY = "";//key
            int pay_id = 0;//支付通道id
            string PyZFBazjkhc = "PyZFBazjkhc" + tid;//组装缓存key值
            #region 获取鹏缘支付宝账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(PyZFBazjkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(PyZFBazjkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的鹏缘账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的鹏缘key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "鹏缘支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("PYZFB", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取贝贝鹏缘账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取鹏缘key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, PyZFBazjkhc, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "鹏缘支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "鹏缘支付宝支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "鹏缘支付宝支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            NameValueCollection palis = new NameValueCollection();
            palis.Add("version", "2.0");//版本号
            palis.Add("organno", "123456");//机构号
            palis.Add("merchno", uid);//商户号
            palis.Add("paytype", "006");//支付类型
            palis.Add("remark", goodsname);//商品描述
            palis.Add("proname", goodsname);//商品名称
            palis.Add("ordno", code);//订单号
            palis.Add("price", (Convert.ToInt32(price * 100)).ToString());//金额（单位：分）
            palis.Add("callbackurl", "http://www.baidu.com");//同步通知地址
            palis.Add("notifyurl", ConfigurationManager.AppSettings["pyzfbNotifyUrl"].ToString());//异步通知地址
            palis.Add("cpchannel", "pf2");//渠道号
            Dictionary<string, string> list = palis.Cast<string>().ToDictionary(x => x, x => palis[x]);
            string md5 = JMP.TOOL.UrlStr.AzGetStr(list) + "&" + MD5KEY;
            string md5str = JMP.TOOL.MD5.md5strGet(md5, true).ToLower();
            palis.Add("sign", md5str);//签名
            string url = ConfigurationManager.AppSettings["pyzfbPOSTUrl"].ToString();//请求地址  
            WebClient webClient = new WebClient();
            byte[] responseData = webClient.UploadValues(url, "POST", palis);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码  
            Dictionary<string, object> jsonstr = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
            if (jsonstr.ContainsKey("url") && jsonstr["code"].ToString() == "10")
            {
                str = "{\"message\":\"成功\",\"result\":100,\"data\":{\"pay\":\"" + jsonstr["url"].ToString() + "\"}}";
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }

        /// <summary>
        /// 鹏缘支付宝支付ios模式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string Pyzfbios(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            string uid = "";//商户编号
            string MD5KEY = "";//key
            int pay_id = 0;//支付通道id
            string PyZFBiosjkhc = "PyZFBiosjkhc" + tid;//组装缓存key值
            #region 获取鹏缘支付宝账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(PyZFBiosjkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(PyZFBiosjkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的鹏缘账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的鹏缘key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "鹏缘支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("PYZFB", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取贝贝鹏缘账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取鹏缘key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, PyZFBiosjkhc, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "鹏缘支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "鹏缘支付宝支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "鹏缘支付宝支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            NameValueCollection palis = new NameValueCollection();
            palis.Add("version", "2.0");//版本号
            palis.Add("organno", "123456");//机构号
            palis.Add("merchno", uid);//商户号
            palis.Add("paytype", "006");//支付类型
            palis.Add("remark", goodsname);//商品描述
            palis.Add("proname", goodsname);//商品名称
            palis.Add("ordno", code);//订单号
            palis.Add("price", (Convert.ToInt32(price * 100)).ToString());//金额（单位：分）
            palis.Add("callbackurl", "http://www.baidu.com");//同步通知地址
            palis.Add("notifyurl", ConfigurationManager.AppSettings["pyzfbNotifyUrl"].ToString());//异步通知地址
            palis.Add("cpchannel", "pf2");//渠道号
            Dictionary<string, string> list = palis.Cast<string>().ToDictionary(x => x, x => palis[x]);
            string md5 = JMP.TOOL.UrlStr.AzGetStr(list) + "&" + MD5KEY;
            string md5str = JMP.TOOL.MD5.md5strGet(md5, true).ToLower();
            palis.Add("sign", md5str);//签名
            string url = ConfigurationManager.AppSettings["pyzfbPOSTUrl"].ToString();//请求地址  
            WebClient webClient = new WebClient();
            byte[] responseData = webClient.UploadValues(url, "POST", palis);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码  
            Dictionary<string, object> jsonstr = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
            if (jsonstr.ContainsKey("url") && jsonstr["code"].ToString() == "10")
            {
                str = "{\"message\":\"成功\",\"result\":100,\"data\":{\"pay\":\"" + jsonstr["url"].ToString() + "\"}}";
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }
        #endregion

        #region 山东微保支付宝
        /// <summary>
        /// 山东微保支付宝h5调用模式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string SdWbZfbH5(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            string uid = "";//商户编号
            string MD5KEY = "";//key
            int pay_id = 0;//支付通道id
            string SDWBZFBjkhc = "SDWBZFBjkhc" + tid;//组装缓存key值
            #region 获取山东微保支付宝账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(SDWBZFBjkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(SDWBZFBjkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的山东微保账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的山东微保key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "山东微保支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("SDWBZFB", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取山东微保账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取山东微保key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, SDWBZFBjkhc, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "山东微保支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "山东微保支付宝支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "山东微保支付宝支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();
            PostVars.Add("merchantNo", uid);//商户编号
            PostVars.Add("orderAmount", (Convert.ToInt32(price * 100)).ToString());//金额单位分
            PostVars.Add("orderNo", code);//订单编号
            PostVars.Add("notifyUrl", ConfigurationManager.AppSettings["SDWBZFBNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步通知地址
            PostVars.Add("callbackUrl", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", oid.ToString()));//同步跳转地址
            PostVars.Add("payType", "3");//支付类型
            PostVars.Add("productName", goodsname);//商品名称
            PostVars.Add("productDesc", goodsname);//商品描述
                                                   // PostVars.Add("remark", JMP.TOOL.DESEncrypt.Encrypt(code));//备注
            Dictionary<string, string> list = new Dictionary<string, string>();
            list = PostVars.Cast<string>().ToDictionary(x => x, x => PostVars[x]);
            string sing = JMP.TOOL.UrlStr.AzGetStr(list) + MD5KEY;
            string md5 = JMP.TOOL.MD5.md5strGet(sing, true).ToUpper();
            PostVars.Add("sign", md5);//签名
            string url = ConfigurationManager.AppSettings["SDWBZFBPOSTUrl"].ToString();//请求地址  
            WebClient webClient = new WebClient();
            byte[] responseData = webClient.UploadValues(url, "POST", PostVars);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码  
            Dictionary<string, object> jsonstr = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
            if (jsonstr.ContainsKey("payUrl") && jsonstr["status"].ToString() == "T")
            {
                //str = "{\"message\":\"成功\",\"result\":100,\"data\":{\"pay\":\"" + jsonstr["payUrl"].ToString() + "\"}}";
                str = jsonstr["payUrl"].ToString();
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }

        /// <summary>
        /// 山东微保支付宝安卓调用模式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string SdWbZfbAz(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            string uid = "";//商户编号
            string MD5KEY = "";//key
            int pay_id = 0;//支付通道id
            string SDWBZFBAzjkhc = "SDWBZFBAzjkhc" + tid;//组装缓存key值
            #region 获取山东微保支付宝账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(SDWBZFBAzjkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(SDWBZFBAzjkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的山东微保账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的山东微保key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "山东微保支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("SDWBZFB", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取山东微保账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取山东微保key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, SDWBZFBAzjkhc, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "山东微保支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "山东微保支付宝支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "山东微保支付宝支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();
            PostVars.Add("merchantNo", uid);//商户编号
            PostVars.Add("orderAmount", (Convert.ToInt32(price * 100)).ToString());//金额单位分
            PostVars.Add("orderNo", code);//订单编号
            PostVars.Add("notifyUrl", ConfigurationManager.AppSettings["SDWBZFBNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步通知地址
            PostVars.Add("callbackUrl", "http://www.baidu.com");//同步跳转地址
            PostVars.Add("payType", "3");//支付类型
            PostVars.Add("productName", goodsname);//商品名称
            PostVars.Add("productDesc", goodsname);//商品描述
                                                   // PostVars.Add("remark", JMP.TOOL.DESEncrypt.Encrypt(code));//备注
            Dictionary<string, string> list = new Dictionary<string, string>();
            list = PostVars.Cast<string>().ToDictionary(x => x, x => PostVars[x]);
            string sing = JMP.TOOL.UrlStr.AzGetStr(list) + MD5KEY;
            string md5 = JMP.TOOL.MD5.md5strGet(sing, true).ToUpper();
            PostVars.Add("sign", md5);//签名
            string url = ConfigurationManager.AppSettings["SDWBZFBPOSTUrl"].ToString();//请求地址  
            WebClient webClient = new WebClient();
            byte[] responseData = webClient.UploadValues(url, "POST", PostVars);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码  
            Dictionary<string, object> jsonstr = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
            if (jsonstr.ContainsKey("payUrl") && jsonstr["status"].ToString() == "T")
            {
                str = "{\"message\":\"成功\",\"result\":100,\"data\":{\"pay\":\"" + jsonstr["payUrl"].ToString() + "\"}}";
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }

        /// <summary>
        /// 山东微保支付宝苹果调用模式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string SdWbZfbIos(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            string uid = "";//商户编号
            string MD5KEY = "";//key
            int pay_id = 0;//支付通道id
            string SDWBZFBIosjkhc = "SDWBZFBIosjkhc" + tid;//组装缓存key值
            #region 获取山东微保支付宝账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(SDWBZFBIosjkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(SDWBZFBIosjkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的山东微保账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的山东微保key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "山东微保支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("SDWBZFB", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取山东微保账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取山东微保key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, SDWBZFBIosjkhc, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "山东微保支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "山东微保支付宝支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "山东微保支付宝支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();
            PostVars.Add("merchantNo", uid);//商户编号
            PostVars.Add("orderAmount", (Convert.ToInt32(price * 100)).ToString());//金额单位分
            PostVars.Add("orderNo", code);//订单编号
            PostVars.Add("notifyUrl", ConfigurationManager.AppSettings["SDWBZFBNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步通知地址
            PostVars.Add("callbackUrl", "http://www.baidu.com");//同步跳转地址
            PostVars.Add("payType", "3");//支付类型
            PostVars.Add("productName", goodsname);//商品名称
            PostVars.Add("productDesc", goodsname);//商品描述
                                                   // PostVars.Add("remark", JMP.TOOL.DESEncrypt.Encrypt(code));//备注
            Dictionary<string, string> list = new Dictionary<string, string>();
            list = PostVars.Cast<string>().ToDictionary(x => x, x => PostVars[x]);
            string sing = JMP.TOOL.UrlStr.AzGetStr(list) + MD5KEY;
            string md5 = JMP.TOOL.MD5.md5strGet(sing, true).ToUpper();
            PostVars.Add("sign", md5);//签名
            string url = ConfigurationManager.AppSettings["SDWBZFBPOSTUrl"].ToString();//请求地址  
            WebClient webClient = new WebClient();
            byte[] responseData = webClient.UploadValues(url, "POST", PostVars);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码  
            Dictionary<string, object> jsonstr = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
            if (jsonstr.ContainsKey("payUrl") && jsonstr["status"].ToString() == "T")
            {
                str = "{\"message\":\"成功\",\"result\":100,\"data\":{\"pay\":\"" + jsonstr["payUrl"].ToString() + "\"}}";
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }
        #endregion

        #region 汇元微信wap支付
        /// <summary>
        /// 汇元微信wap支付h5调用模式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string HyWxWaPH5(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            string uid = "";//商户编号
            string MD5KEY = "";//key
            int pay_id = 0;//支付通道id
            string HyWxWaPH5jkhc = "HyWxWaPH5jkhc" + tid;//组装缓存key值
            #region 获取汇元微信wap支付账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(HyWxWaPH5jkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(HyWxWaPH5jkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的汇元微信wap支付账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的汇元微信wap支付key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "汇元微信wap支付支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("HYWX", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取汇元微信wap支付账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取汇元微信wap支付key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, HyWxWaPH5jkhc, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "汇元微信wap支付支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "汇元微信wap支付支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "汇元微信wap支付支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            System.Collections.Specialized.NameValueCollection Palist = new System.Collections.Specialized.NameValueCollection();
            Palist.Add("version", "1");//版本号
            Palist.Add("agent_id", uid);//商户编号
            Palist.Add("agent_bill_id", code);//订单号
            Palist.Add("agent_bill_time", DateTime.Now.ToString("yyyyMMddHHmmss"));//提交订单时间
            Palist.Add("pay_amt", price.ToString());//支付金额（单位：元）
            Palist.Add("notify_url", ConfigurationManager.AppSettings["HyNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步通知地址
            Palist.Add("return_url", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", oid.ToString()));//同步通知地址
            Palist.Add("user_ip", HttpContext.Current.Request.UserHostAddress.Replace('.', '_'));//ip地址
            Palist.Add("pay_type", "30");//支付类型
            Palist.Add("is_phone", "1");//微信wap支付传入1
            Palist.Add("is_frame", "0");//微信支付类型（0：wap，1：公众号）默认为1
            Palist.Add("goods_name", System.Web.HttpUtility.UrlEncode(goodsname));//商品名称
                                                                                  // Palist.Add("remark", System.Web.HttpUtility.UrlEncode("测试  231346 测试"));//自定义参数
            string meta_option = "{\"s\":\"WAP\",\"n\":\"测试\",\"id\":\"http://www.baidu.com\"}";
            Palist.Add("meta_option", Convert.ToBase64String(System.Text.Encoding.GetEncoding("GBK").GetBytes(meta_option.Trim())));
            string md5sing = "version=" + Palist["version"] + "&agent_id=" + Palist["agent_id"] + "&agent_bill_id=" + Palist["agent_bill_id"] + "&agent_bill_time=" + Palist["agent_bill_time"] + "&pay_type=30&pay_amt=" + Palist["pay_amt"] + "&notify_url=" + Palist["notify_url"] + "&return_url=" + Palist["return_url"] + "&user_ip=" + Palist["user_ip"] + "&key=" + MD5KEY;
            string md5str = JMP.TOOL.MD5.md5strGet(md5sing, true);
            Palist.Add("sign", md5str);//签名
            string url = ConfigurationManager.AppSettings["HyPOSTUrl"].ToString();//请求地址
            StringBuilder html = new StringBuilder();
            html.AppendLine(" <form id='hywxwappay' name='hywxwappay'  method='POST' action='" + url + "'  > ");
            foreach (string key in Palist.Keys)
            {
                html.AppendLine(" <input type='hidden' name ='" + key + "' value ='" + Palist[key] + "'  />  ");
            }
            html.AppendLine("</form>");
            html.AppendLine("<script> document.forms['hywxwappay'].submit();</script>");
            string h5key = "h5" + code;
            JMP.TOOL.CacheHelper.CacheObject(h5key, html, 1);
            str = JMP.TOOL.Encrypt.IndexEncrypt(h5key);
            str = "/UnionPay.aspx?UnionPay=" + HttpUtility.UrlEncode(str);
            return str;
        }
        /// <summary>
        /// 汇元微信wap支付安卓调用模式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string HyWxWaPAz(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            string uid = "";//商户编号
            string MD5KEY = "";//key
            int pay_id = 0;//支付通道id
            string HyWxWaPH5Azjkhc = "HyWxWaPH5Azjkhc" + tid;//组装缓存key值
            #region 获取汇元微信wap支付账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(HyWxWaPH5Azjkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(HyWxWaPH5Azjkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的汇元微信wap支付账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的汇元微信wap支付key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "汇元微信wap支付支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("HYWX", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取汇元微信wap支付账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取汇元微信wap支付key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, HyWxWaPH5Azjkhc, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "汇元微信wap支付支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "汇元微信wap支付支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "汇元微信wap支付支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            System.Collections.Specialized.NameValueCollection Palist = new System.Collections.Specialized.NameValueCollection();
            Palist.Add("version", "1");//版本号
            Palist.Add("agent_id", uid);//商户编号
            Palist.Add("agent_bill_id", code);//订单号
            Palist.Add("agent_bill_time", DateTime.Now.ToString("yyyyMMddHHmmss"));//提交订单时间
            Palist.Add("pay_amt", price.ToString());//支付金额（单位：元）
            Palist.Add("notify_url", ConfigurationManager.AppSettings["HyNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步通知地址
            Palist.Add("return_url", "http://www.baidu.com");//同步通知地址
            Palist.Add("user_ip", HttpContext.Current.Request.UserHostAddress.Replace('.', '_'));//ip地址
            Palist.Add("pay_type", "30");//支付类型
            Palist.Add("goods_note", HttpUtility.UrlEncode(goodsname, Encoding.GetEncoding("gb2312")));//支付说明
            Palist.Add("goods_num", "1");//产品数量
            Palist.Add("goods_name", HttpUtility.UrlEncode(goodsname, Encoding.GetEncoding("gb2312")));//商品名称
            //Palist.Add("remark", HttpUtility.UrlEncode(goodsname, Encoding.GetEncoding("gb2312")));//自定义参数
            string meta_option = "[{\"s\":\"Android\",\"n\":\"测试\",\"id\":\"测试\"},{\"s\":\"IOS\",\"n\":\"\",\"id\":\"\"}]";
            Palist.Add("meta_option", Convert.ToBase64String(System.Text.Encoding.GetEncoding("GBK").GetBytes(meta_option.Trim())));
            string md5sing = "version=" + Palist["version"] + "&agent_id=" + Palist["agent_id"] + "&agent_bill_id=" + Palist["agent_bill_id"] + "&agent_bill_time=" + Palist["agent_bill_time"] + "&pay_type=30&pay_amt=" + Palist["pay_amt"] + "&notify_url=" + Palist["notify_url"] + "&user_ip=" + Palist["user_ip"] + "&key=" + MD5KEY;
            string md5str = JMP.TOOL.MD5.md5strGet(md5sing, true);
            Palist.Add("sign", md5str);//签名
            Dictionary<string, string> list = Palist.Cast<string>().ToDictionary(x => x, x => Palist[x]);
            string aaa = JMP.TOOL.UrlStr.AzGetStr(list);
            string url = ConfigurationManager.AppSettings["HywxsdkPOSTUrl"].ToString();//请求地址
            WebClient webClient = new WebClient();
            byte[] responseData = webClient.UploadValues(url, "POST", Palist);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码 
            if (srcString.Contains("token_id"))
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(srcString);
                string token_id = xmldoc["token_id"].InnerText + "," + uid + "," + code + ",30";
                string wxpay = "{\"paytype\":\"6\",\"token_id\":\"" + token_id + "\"}";
                str = "{\"message\":\"成功\",\"result\":100,\"data\":" + wxpay + "}";
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }

        /// <summary>
        /// 汇元微信wap支付苹果调用模式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string HyWxWaPIOS(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            string uid = "";//商户编号
            string MD5KEY = "";//key
            int pay_id = 0;//支付通道id
            string HyWxWaPIOSjkhc = "HyWxWaPIOSjkhc" + tid;//组装缓存key值
            #region 获取汇元微信wap支付账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(HyWxWaPIOSjkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(HyWxWaPIOSjkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的汇元微信wap支付账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的汇元微信wap支付key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "汇元微信wap支付支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("HYWX", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取汇元微信wap支付账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取汇元微信wap支付key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, HyWxWaPIOSjkhc, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "汇元微信wap支付支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "汇元微信wap支付支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "汇元微信wap支付支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            System.Collections.Specialized.NameValueCollection Palist = new System.Collections.Specialized.NameValueCollection();
            Palist.Add("version", "1");//版本号
            Palist.Add("agent_id", uid);//商户编号
            Palist.Add("agent_bill_id", code);//订单号
            Palist.Add("agent_bill_time", DateTime.Now.ToString("yyyyMMddHHmmss"));//提交订单时间
            Palist.Add("pay_amt", price.ToString());//支付金额（单位：元）
            Palist.Add("notify_url", ConfigurationManager.AppSettings["HyNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步通知地址
            Palist.Add("return_url", "https://www.baidu.com");//同步通知地址
            Palist.Add("user_ip", HttpContext.Current.Request.UserHostAddress.Replace('.', '_'));//ip地址
            Palist.Add("pay_type", "30");//支付类型
            Palist.Add("goods_note", HttpUtility.UrlEncode(goodsname, Encoding.GetEncoding("gb2312")));//支付说明
            Palist.Add("goods_num", "1");//产品数量
            Palist.Add("goods_name", HttpUtility.UrlEncode(goodsname, Encoding.GetEncoding("gb2312")));//商品名称
                                                                                                       // Palist.Add("remark", HttpUtility.UrlEncode(goodsname, Encoding.GetEncoding("gb2312")));//自定义参数
            string meta_option = "[{\"s\":\"Android\",\"n\":\"\",\"id\":\"\"},{\"s\":\"IOS\",\"n\":\"测试\",\"id\":\"测试\"}]";
            Palist.Add("meta_option", Convert.ToBase64String(System.Text.Encoding.GetEncoding("GBK").GetBytes(meta_option.Trim())));
            string md5sing = "version=" + Palist["version"] + "&agent_id=" + Palist["agent_id"] + "&agent_bill_id=" + Palist["agent_bill_id"] + "&agent_bill_time=" + Palist["agent_bill_time"] + "&pay_type=30&pay_amt=" + Palist["pay_amt"] + "&notify_url=" + Palist["notify_url"] + "&user_ip=" + Palist["user_ip"] + "&key=" + MD5KEY;
            string md5str = JMP.TOOL.MD5.md5strGet(md5sing, true);
            Palist.Add("sign", md5str);//签名
            string url = ConfigurationManager.AppSettings["HywxsdkPOSTUrl"].ToString();//请求地址
            WebClient webClient = new WebClient();
            byte[] responseData = webClient.UploadValues(url, "POST", Palist);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码 
            if (srcString.Contains("token_id"))
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(srcString);
                string token_id = xmldoc["token_id"].InnerText + "," + uid + "," + code + ",30";
                string wxpay = "{\"paytype\":\"6\",\"token_id\":\"" + token_id + "\"}";
                str = "{\"message\":\"成功\",\"result\":100,\"data\":" + wxpay + "}";
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }

        #endregion

        #region 汇元银联支付
        /// <summary>
        /// 汇元银联支付h5调用模式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string HyYlPayH5(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            string uid = "";//商户编号
            string MD5KEY = "";//key
            int pay_id = 0;//支付通道id
            string HyYlPayH5jkhc = "HyYlPayH5jkhc" + tid;//组装缓存key值
            #region 获取汇元银联支付账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(HyYlPayH5jkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(HyYlPayH5jkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的汇元银联支付账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的汇元银联支付key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "汇元银联支付支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("HYYL", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取汇元银联支付账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取汇元银联支付key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, HyYlPayH5jkhc, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "汇元银联支付支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "汇元银联支付支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "汇元微信wap支付支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            System.Collections.Specialized.NameValueCollection Palist = new System.Collections.Specialized.NameValueCollection();
            Palist.Add("version", "1");//版本号
            Palist.Add("agent_id", uid);//商户编号
            Palist.Add("agent_bill_id", code);//订单号
            Palist.Add("agent_bill_time", DateTime.Now.ToString("yyyyMMddHHmmss"));//提交订单时间
            Palist.Add("pay_amt", price.ToString());//支付金额（单位：元）
            Palist.Add("notify_url", ConfigurationManager.AppSettings["HyNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步通知地址
            Palist.Add("return_url", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", oid.ToString()));//同步通知地址
            Palist.Add("user_ip", HttpContext.Current.Request.UserHostAddress.Replace('.', '_'));//ip地址
            Palist.Add("pay_type", "0");//支付类型
            Palist.Add("goods_name", System.Web.HttpUtility.UrlEncode(goodsname));//商品名称
            string md5sing = "version=" + Palist["version"] + "&agent_id=" + Palist["agent_id"] + "&agent_bill_id=" + Palist["agent_bill_id"] + "&agent_bill_time=" + Palist["agent_bill_time"] + "&pay_type=0&pay_amt=" + Palist["pay_amt"] + "&notify_url=" + Palist["notify_url"] + "&return_url=" + Palist["return_url"] + "&user_ip=" + Palist["user_ip"] + "&key=" + MD5KEY;
            string md5str = JMP.TOOL.MD5.md5strGet(md5sing, true);
            Palist.Add("sign", md5str);//签名
            string url = ConfigurationManager.AppSettings["HyPOSTUrl"].ToString();//请求地址
            StringBuilder html = new StringBuilder();
            html.AppendLine(" <form id='HyYlPayH5' name='HyYlPayH5'  method='POST' action='" + url + "'  > ");
            foreach (string key in Palist.Keys)
            {
                html.AppendLine(" <input type='hidden' name ='" + key + "' value ='" + Palist[key] + "'  />  ");
            }
            html.AppendLine("</form>");
            html.AppendLine("<script> document.forms['HyYlPayH5'].submit();</script>");
            string h5key = "h5" + code;
            JMP.TOOL.CacheHelper.CacheObject(h5key, html, 1);
            str = JMP.TOOL.Encrypt.IndexEncrypt(h5key);
            str = "/UnionPay.aspx?UnionPay=" + HttpUtility.UrlEncode(str);
            return str;
        }
        #endregion

        #region 众易鑫appid支付
        /// <summary>
        /// 众易鑫appid支付安卓调用方式
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="price">价格</param>
        /// <param name="oid">订单号</param>
        /// <param name="goodsname">商品名称</param>
        /// <returns></returns>
        public static string ZyxAppidAz(int appid, string code, decimal price, int oid, string goodsname)
        {
            string str = "";
            string userid = "";//众易鑫商户id
            string userkey = "";//众易鑫key
            int pay_id = 0;//支付渠道id
            string zyxappid = "zyxappid" + appid;//组装缓存key值
            #region 众易鑫app支付账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(zyxappid))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(zyxappid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的众易鑫账号id
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的众易鑫key
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "众易鑫appid支付支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.selectAppid("ZYXAPP", appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取众易鑫
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取南众易鑫key
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, zyxappid, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "众易鑫appid支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "众易鑫支付接口错误应用ID：" + appid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "众易鑫appid支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            Dictionary<string, string> list = new Dictionary<string, string>();
            list.Add("version", "1.0.0");//版本号
            list.Add("service", "0004");//支付类型
            list.Add("reqDate", DateTime.Now.ToString("yyyyMMdd"));//交易日期
            list.Add("transAmount", (Convert.ToInt32(price * 100)).ToString());//交易金额(单位分)
            list.Add("bgReturnUrl", ConfigurationManager.AppSettings["zyxNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步通知地址
            list.Add("customerNo", userid); //商户编号
            list.Add("externalId", code);//订单编号
            list.Add("selfData", code);//自定义参数
            list.Add("requestIp", "202.102.89.228");//ip地址
            list.Add("description", goodsname);//商品描述
            list.Add("reqTime", DateTime.Now.ToString("HHmmss"));//下单时间
            string md5 = JMP.TOOL.UrlStr.AzGetStr(list) + "&key=" + userkey;
            string md5str = JMP.TOOL.MD5.md5strGet(md5, true).ToUpper();
            list.Add("sign", md5str);//签名
            string url = ConfigurationManager.AppSettings["zyxPOSTUrl"].ToString();//请求地址
            string data = JMP.TOOL.JsonHelper.DictJsonstr(list);
            string srcString = JMP.TOOL.PostJsonRequest.GetHtmlByJson(url, data);
            if (!string.IsNullOrEmpty(srcString))
            {
                Dictionary<string, object> jsonstr = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
                if (jsonstr["code"].ToString() == "0")
                {
                    string wxpay = "{\"paytype\":\"3\",\"token_id\":\"" + jsonstr["tokenId"].ToString() + "\", \"services\":\"pay.weixin.app\", \"sign\":\"" + jsonstr["sign"].ToString() + "\",\"status\":\"0\", \"charset\":\"UTF-8\", \"version\":\"2.0\", \"sign_type\":\"MD5\",\"appid\":\"" + jsonstr["appId"].ToString() + "\"}";
                    str = "{\"message\":\"成功\",\"result\":100,\"data\":" + wxpay + "}";
                }
                else
                {
                    str = "{\"message\":\"支付通道异常\",\"result\":104}";
                }
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }
        /// <summary>
        /// 众易鑫appid支付ios调用方式
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="price">价格</param>
        /// <param name="oid">订单号</param>
        /// <param name="goodsname">商品名称</param>
        /// <returns></returns>
        public static string ZyxAppidIos(int appid, string code, decimal price, int oid, string goodsname)
        {
            string str = "";
            string userid = "";//众易鑫商户id
            string userkey = "";//众易鑫key
            int pay_id = 0;//支付渠道id
            string ZyxAppidIos = "ZyxAppidIos" + appid;//组装缓存key值
            #region 众易鑫app支付账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(ZyxAppidIos))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(ZyxAppidIos);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的众易鑫账号id
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的众易鑫key
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "众易鑫appid支付支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.selectAppid("ZYXAPP", appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取众易鑫
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取南众易鑫key
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, ZyxAppidIos, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "众易鑫appid支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "众易鑫支付接口错误应用ID：" + appid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "众易鑫appid支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            Dictionary<string, string> list = new Dictionary<string, string>();
            list.Add("version", "1.0.0");//版本号
            list.Add("service", "0004");//支付类型
            list.Add("reqDate", DateTime.Now.ToString("yyyyMMdd"));//交易日期
            list.Add("transAmount", (Convert.ToInt32(price * 100)).ToString());//交易金额(单位分)
            list.Add("bgReturnUrl", ConfigurationManager.AppSettings["zyxNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步通知地址
            list.Add("customerNo", userid); //商户编号
            list.Add("externalId", code);//订单编号
            list.Add("selfData", code);//自定义参数
            list.Add("requestIp", "202.102.89.228");//ip地址
            list.Add("description", goodsname);//商品描述
            list.Add("reqTime", DateTime.Now.ToString("HHmmss"));//下单时间
            string md5 = JMP.TOOL.UrlStr.AzGetStr(list) + "&key=" + userkey;
            string md5str = JMP.TOOL.MD5.md5strGet(md5, true).ToUpper();
            list.Add("sign", md5str);//签名
            string url = ConfigurationManager.AppSettings["zyxPOSTUrl"].ToString();//请求地址
            string data = JMP.TOOL.JsonHelper.DictJsonstr(list);
            string srcString = JMP.TOOL.PostJsonRequest.GetHtmlByJson(url, data);
            if (!string.IsNullOrEmpty(srcString))
            {
                Dictionary<string, object> jsonstr = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
                if (jsonstr["code"].ToString() == "0")
                {
                    string codes = JMP.TOOL.Encrypt.IndexEncrypt(code);
                    string wxpay = "{\"paytype\":\"3\",\"token_id\":\"" + jsonstr["tokenId"].ToString() + "\", \"services\":\"pay.weixin.app\", \"sign\":\"" + jsonstr["sign"].ToString() + "\",\"status\":\"0\", \"charset\":\"UTF-8\", \"version\":\"2.0\", \"sign_type\":\"MD5\",\"appid\":\"" + jsonstr["appId"].ToString() + "\",\"code\":\"" + codes + "\"}";
                    str = "{\"message\":\"成功\",\"result\":100,\"data\":" + wxpay + "}";
                }
                else
                {
                    str = "{\"message\":\"支付通道异常\",\"result\":104}";
                }
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }
        #endregion

        #region 发发啦appid

        /// <summary>
        /// 发发啦appid支付安卓调用方式
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string FflAppidAz(int appid, string code, int oid)
        {
            string str = "";
            string userid = "";//首游商户id
            string userkey = "";//首游key
            int pay_id = 0;//支付渠道id
            string FflAppidAzzfjkhc = "FflAppidAzzfjkhc" + appid;//组装缓存key值
            #region 发发啦支付账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(FflAppidAzzfjkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(FflAppidAzzfjkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的发发啦账号
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的发发啦key
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "发发啦支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.selectAppid("FFL", appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取发发啦账号
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取发发啦key
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, FflAppidAzzfjkhc, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "发发啦支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "发发啦支付接口错误应用ID：" + appid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "发发啦支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            string nofifyUrl = ConfigurationManager.AppSettings["fflNotifyUrl"].ToString().Replace("{0}", pay_id.ToString());//异步通知地址
            string wxpay = "{\"paytype\":\"7\",\"orderid\":\"" + code + "\", \"syskname\":\"" + userid + "\",\"nofifyUrl\":\"" + nofifyUrl + "\"}";
            str = "{\"message\":\"成功\",\"result\":100,\"data\":" + wxpay + "}";
            return str;
        }

        /// <summary>
        /// 发发啦appid支付苹果调用方式
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string FflAppidIos(int appid, string code, int oid)
        {
            string str = "";
            string userid = "";//首游商户id
            string userkey = "";//首游key
            int pay_id = 0;//支付渠道id
            string FflAppidIoszfjkhc = "FflAppidIoszfjkhc" + appid;//组装缓存key值
            #region 发发啦支付账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(FflAppidIoszfjkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(FflAppidIoszfjkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的发发啦账号
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的发发啦key
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "发发啦支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.selectAppid("FFL", appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取发发啦账号
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取发发啦key
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, FflAppidIoszfjkhc, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "发发啦支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "发发啦支付接口错误应用ID：" + appid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "发发啦支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            string nofifyUrl = ConfigurationManager.AppSettings["fflNotifyUrl"].ToString().Replace("{0}", pay_id.ToString());//异步通知地址
            string codes = JMP.TOOL.Encrypt.IndexEncrypt(code);
            string wxpay = "{\"paytype\":\"7\",\"orderid\":\"" + code + "\", \"syskname\":\"" + userid + "\",\"nofifyUrl\":\"" + nofifyUrl + "\",\"code\":\"" + codes + "\"}";
            str = "{\"message\":\"成功\",\"result\":100,\"data\":" + wxpay + "}";
            return str;
        }

        #endregion

        #region 微赢互动公众号
        /// <summary>
        /// 微互动公众号支付h5调用模式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string whdgzhH5(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            string uid = "";//商户编号
            string MD5KEY = "";//key
            int pay_id = 0;//支付通道id
            string whdgzhH5jkhc = "whdgzhH5jkhc" + tid;//组装缓存key值
            #region 微互动公众号支付账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(whdgzhH5jkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(whdgzhH5jkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的微互动公众号id
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的微互动公众号key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "微互动公众号支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("whdgzh", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取微互动公众号id
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取微互动公众号key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, whdgzhH5jkhc, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "微互动公众号支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "微互动公众号支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "微互动公众号支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            Dictionary<string, string> liststr = new Dictionary<string, string>();
            Random r = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
            string dkip = HttpContext.Current.Request.UserHostAddress.Replace(".", "").Trim();
            string order_no = uid + "_" + DateTime.Now.ToString("MMddHHmmssfff") + r.Next(111111111, 999999999).ToString() + (dkip.Length > 4 ? dkip.Substring(0, 4) : r.Next(1111, 9999).ToString());
            liststr.Add("order_no", order_no);//商户订单号
            liststr.Add("goods_name", code);//商品名称
            liststr.Add("remark", System.Web.HttpUtility.UrlEncode(goodsname));//备注
            liststr.Add("order_amt", price.ToString());//金额(单位：元)
            liststr.Add("return_url", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", oid.ToString()));//同步地址
            liststr.Add("notify_url", ConfigurationManager.AppSettings["whdgzhNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步地址
            liststr.Add("custom", System.Web.HttpUtility.UrlEncode(code));//自定义参数
            string md5 = "order_no=" + liststr["order_no"] + "&order_amt=" + liststr["order_amt"] + "&key=" + MD5KEY;
            byte[] s = System.Text.Encoding.Default.GetBytes(md5);
            md5 = Convert.ToBase64String(s);
            string md5str = JMP.TOOL.MD5.md5strGet(md5, true).ToLower();
            liststr.Add("sign", md5str);//签名
            string url = ConfigurationManager.AppSettings["whdgzhPOSTUrl"].ToString();//请求地址
            StringBuilder html = new StringBuilder();
            html.AppendLine(" <form id='whdgzh' name='whdgzh'  method='POST' action='" + url + "'  > ");
            foreach (string key in liststr.Keys)
            {
                html.AppendLine(" <input type='hidden' name ='" + key + "' value ='" + liststr[key] + "'  />  ");
            }
            html.AppendLine("</form>");
            html.AppendLine("<script> document.forms['whdgzh'].submit();</script>");
            string h5key = "h5" + code;
            JMP.TOOL.CacheHelper.CacheObject(h5key, html, 1);
            str = JMP.TOOL.Encrypt.IndexEncrypt(h5key);
            str = "/UnionPay.aspx?UnionPay=" + HttpUtility.UrlEncode(str);
            return str;
        }
        #endregion

        #region 微赢互动支付宝wap
        /// <summary>
        /// 微互动支付宝wap h5调用模式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string wyhdzfbh5(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            string uid = "";//商户编号
            string MD5KEY = "";//key
            int pay_id = 0;//支付通道id
            string wyhdzfbh5jkhc = "wyhdzfbh5jkhc" + tid;//组装缓存key值
            #region 微互动支付宝支付账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(wyhdzfbh5jkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(wyhdzfbh5jkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的微互动公众号id
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的微互动公众号key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "微互动支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("whdzfb", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取微互动公众号id
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取微互动公众号key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, wyhdzfbh5jkhc, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "微互动支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "微互动支付宝支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "微互动支付宝支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            NameValueCollection liststr = new NameValueCollection();
            // string order_no = uid + "_" + code;
            Random r = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
            string dkip = HttpContext.Current.Request.UserHostAddress.Replace(".", "").Trim();
            string order_no = uid + "_" + DateTime.Now.ToString("MMddHHmmssfff") + r.Next(111111111, 999999999).ToString() + (dkip.Length > 4 ? dkip.Substring(0, 4) : r.Next(1111, 9999).ToString());
            liststr.Add("order_no", order_no);//商户订单号
            liststr.Add("goods_name", code);//商品名称
            liststr.Add("remark", System.Web.HttpUtility.UrlEncode(goodsname));//备注
            liststr.Add("order_amt", price.ToString());//金额(单位：元)
            liststr.Add("return_url", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", oid.ToString()));//同步地址
            liststr.Add("notify_url", ConfigurationManager.AppSettings["whdzfbNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步地址
            liststr.Add("custom", System.Web.HttpUtility.UrlEncode(code));//自定义参数
            string md5 = "order_no=" + liststr["order_no"] + "&order_amt=" + liststr["order_amt"] + "&key=" + MD5KEY;
            byte[] s = System.Text.Encoding.Default.GetBytes(md5);
            md5 = Convert.ToBase64String(s);
            string md5str = JMP.TOOL.MD5.md5strGet(md5, true).ToLower();
            liststr.Add("sign", md5str);//签名
            string url = ConfigurationManager.AppSettings["whdzfbPOSTUrl"].ToString();//请求地址
            WebClient webClient = new WebClient();
            byte[] responseData = webClient.UploadValues(url, "POST", liststr);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码 
            if (srcString.StartsWith("https://mapi.alipay.com"))
            {
                str = srcString;
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }

        /// <summary>
        /// 微互动支付宝wap 安卓调用模式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string wyhdzfbAz(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            string uid = "";//商户编号
            string MD5KEY = "";//key
            int pay_id = 0;//支付通道id
            string wyhdzfbAzjkhc = "wyhdzfbAzjkhc" + tid;//组装缓存key值
            #region 微互动支付宝支付账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(wyhdzfbAzjkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(wyhdzfbAzjkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的微互动公众号id
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的微互动公众号key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "微互动支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("whdzfb", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取微互动公众号id
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取微互动公众号key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, wyhdzfbAzjkhc, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "微互动支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "微互动支付宝支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "微互动支付宝支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            NameValueCollection liststr = new NameValueCollection();
            // string order_no = uid + "_" + code;
            Random r = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
            string dkip = HttpContext.Current.Request.UserHostAddress.Replace(".", "").Trim();
            string order_no = uid + "_" + DateTime.Now.ToString("MMddHHmmssfff") + r.Next(111111111, 999999999).ToString() + (dkip.Length > 4 ? dkip.Substring(0, 4) : r.Next(1111, 9999).ToString());
            liststr.Add("order_no", order_no);//商户订单号
            liststr.Add("goods_name", code);//商品名称
            liststr.Add("remark", System.Web.HttpUtility.UrlEncode(goodsname));//备注
            liststr.Add("order_amt", price.ToString());//金额(单位：元)
            liststr.Add("return_url", "http://www.baidu.com");//同步地址
            liststr.Add("notify_url", ConfigurationManager.AppSettings["whdzfbNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步地址
            liststr.Add("custom", System.Web.HttpUtility.UrlEncode(code));//自定义参数
            string md5 = "order_no=" + liststr["order_no"] + "&order_amt=" + liststr["order_amt"] + "&key=" + MD5KEY;
            byte[] s = System.Text.Encoding.Default.GetBytes(md5);
            md5 = Convert.ToBase64String(s);
            string md5str = JMP.TOOL.MD5.md5strGet(md5, true).ToLower();
            liststr.Add("sign", md5str);//签名
            string url = ConfigurationManager.AppSettings["whdzfbPOSTUrl"].ToString();//请求地址
            WebClient webClient = new WebClient();
            byte[] responseData = webClient.UploadValues(url, "POST", liststr);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码 
            if (srcString.StartsWith("https://mapi.alipay.com"))
            {
                str = "{\"message\":\"成功\",\"result\":100,\"data\":{\"pay\":\"" + srcString + "\"}}";
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }

        /// <summary>
        /// 微互动支付宝wap ios调用模式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string wyhdzfbios(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            string uid = "";//商户编号
            string MD5KEY = "";//key
            int pay_id = 0;//支付通道id
            string wyhdzfbiosjkhc = "wyhdzfbiosjkhc" + tid;//组装缓存key值
            #region 微互动支付宝支付账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(wyhdzfbiosjkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(wyhdzfbiosjkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的微互动公众号id
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的微互动公众号key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "微互动支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("whdzfb", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取微互动公众号id
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取微互动公众号key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, wyhdzfbiosjkhc, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "微互动支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "微互动支付宝支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "微互动支付宝支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            NameValueCollection liststr = new NameValueCollection();
            //string order_no = uid + "_" + code;
            Random r = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
            string dkip = HttpContext.Current.Request.UserHostAddress.Replace(".", "").Trim();
            string order_no = uid + "_" + DateTime.Now.ToString("MMddHHmmssfff") + r.Next(111111111, 999999999).ToString() + (dkip.Length > 4 ? dkip.Substring(0, 4) : r.Next(1111, 9999).ToString());
            liststr.Add("order_no", order_no);//商户订单号
            liststr.Add("goods_name", code);//商品名称
            liststr.Add("remark", System.Web.HttpUtility.UrlEncode(goodsname));//备注
            liststr.Add("order_amt", price.ToString());//金额(单位：元)
            liststr.Add("return_url", "http://www.baidu.com");//同步地址
            liststr.Add("notify_url", ConfigurationManager.AppSettings["whdzfbNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步地址
            liststr.Add("custom", System.Web.HttpUtility.UrlEncode(code));//自定义参数
            string md5 = "order_no=" + liststr["order_no"] + "&order_amt=" + liststr["order_amt"] + "&key=" + MD5KEY;
            byte[] s = System.Text.Encoding.Default.GetBytes(md5);
            md5 = Convert.ToBase64String(s);
            string md5str = JMP.TOOL.MD5.md5strGet(md5, true).ToLower();
            liststr.Add("sign", md5str);//签名
            string url = ConfigurationManager.AppSettings["whdzfbPOSTUrl"].ToString();//请求地址
            WebClient webClient = new WebClient();
            byte[] responseData = webClient.UploadValues(url, "POST", liststr);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码 
            if (srcString.StartsWith("https://mapi.alipay.com"))
            {
                str = "{\"message\":\"成功\",\"result\":100,\"data\":{\"pay\":\"" + srcString + "\"}}";
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }

        #endregion

        #region 微赢互动微信扫码
        /// <summary>
        /// 微赢互动微信扫码 
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string wyhdwxsm(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            string uid = "";//商户编号
            string MD5KEY = "";//key
            int pay_id = 0;//支付通道id
            string wyhdwxsmjkhc = "wyhdwxsmjkhc" + tid;//组装缓存key值
            #region 微赢互动微信扫码账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(wyhdwxsmjkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(wyhdwxsmjkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的微赢互动微信扫码id
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的微赢互动微信扫码key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "微赢互动微信扫码支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("wxhdwxsm", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取微赢互动微信扫码id
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取微赢互动微信扫码key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, wyhdwxsmjkhc, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "微赢互动微信扫码支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "微赢互动微信扫码支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "微赢互动微信扫码支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            NameValueCollection liststr = new NameValueCollection();
            Random r = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
            string dkip = HttpContext.Current.Request.UserHostAddress.Replace(".", "").Trim();
            string order_no = uid + "_" + DateTime.Now.ToString("MMddHHmmssfff") + r.Next(111111111, 999999999).ToString() + (dkip.Length > 4 ? dkip.Substring(0, 4) : r.Next(1111, 9999).ToString());
            liststr.Add("order_no", order_no);//商户订单号
            liststr.Add("goods_name", code);//商品名称
            liststr.Add("remark", System.Web.HttpUtility.UrlEncode(goodsname));//备注
            liststr.Add("order_amt", price.ToString());//金额(单位：元)
            liststr.Add("return_url", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", oid.ToString()));//同步地址
            liststr.Add("notify_url", ConfigurationManager.AppSettings["wyhdwxsmNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步地址
            liststr.Add("custom", System.Web.HttpUtility.UrlEncode(code));//自定义参数
            string md5 = "order_no=" + liststr["order_no"] + "&order_amt=" + liststr["order_amt"] + "&key=" + MD5KEY;
            byte[] s = System.Text.Encoding.Default.GetBytes(md5);
            md5 = Convert.ToBase64String(s);
            string md5str = JMP.TOOL.MD5.md5strGet(md5, true).ToLower();
            liststr.Add("sign", md5str);//签名
            string url = ConfigurationManager.AppSettings["wyhdwxsmPOSTUrl"].ToString();//请求地址
            WebClient webClient = new WebClient();
            byte[] responseData = webClient.UploadValues(url, "POST", liststr);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码 
            Dictionary<string, object> jsonstr = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
            if (jsonstr.ContainsKey("code_url"))
            {
                string qurl = jsonstr["code_url"] + "," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ",2";//组装二维码地址
                string qrcodeurl = ConfigurationManager.AppSettings["QRcodeUrl"].ToString() + "?QRcode=" + JMP.TOOL.Encrypt.IndexEncrypt(qurl);//二维码展示地址
                str = qrcodeurl;
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }
        #endregion

        #region 微赢互动appid
        /// <summary>
        /// 微赢互动appid 安卓调用方式
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <param name="goodsname">商品名称</param>
        /// <returns></returns>
        public static string WyhdAppidAz(int appid, string code, decimal price, int oid, string goodsname)
        {
            string str = "";
            string userid = "";//众易鑫商户id
            string userkey = "";//众易鑫key
            int pay_id = 0;//支付渠道id
            string wyhdappidaz = "wyhdappidaz" + appid;//组装缓存key值
            #region 微赢互动appid支付账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(wyhdappidaz))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(wyhdappidaz);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的微赢互动账号id
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的微赢互动key
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "微赢互动appid支付支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.selectAppid("wyhdappid", appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取微赢互动账号
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取微赢互动
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, wyhdappidaz, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "微赢互动appid支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "微赢互动支付接口错误应用ID：" + appid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "微赢互动appid支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            Random r = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
            string dkip = HttpContext.Current.Request.UserHostAddress.Replace(".", "").Trim();
            string order_no = userid + "_" + DateTime.Now.ToString("MMddHHmmssfff") + r.Next(111111111, 999999999).ToString() + (dkip.Length > 4 ? dkip.Substring(0, 4) : r.Next(1111, 9999).ToString());
            System.Collections.Specialized.NameValueCollection Palist = new System.Collections.Specialized.NameValueCollection();
            Palist.Add("order_no", order_no);//订单号
            Palist.Add("goods_name", goodsname.Replace(" ", ""));//商品名称
            Palist.Add("order_amt", (Convert.ToInt32(price * 100)).ToString());//支付金额单位分
            Palist.Add("notify_url", ConfigurationManager.AppSettings["whdzfbNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步通知地址
            Palist.Add("custom", System.Web.HttpUtility.UrlEncode(code));//自定义参数
            WebClient webClient = new WebClient();
            string url = ConfigurationManager.AppSettings["whdappidPOSTUrl"].ToString();//请求地址
            byte[] responseData = webClient.UploadValues(url, "POST", Palist);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码 
            if (srcString.Contains("token_id") && srcString.Contains("appid"))
            {
                //string wxpay = "{\"paytype\":\"3\"," + srcString.Replace("{", "").Replace("}", "") + "}";
                Dictionary<string, object> list = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
                string wxpay = "{\"paytype\":\"3\",\"token_id\":\"" + list["token_id"].ToString() + "\", \"services\":\"pay.weixin.app\",\"appid\":\"" + list["appid"].ToString() + "\"}";
                str = "{\"message\":\"成功\",\"result\":100,\"data\":" + wxpay + "}";
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }
        /// <summary>
        /// 微赢互动appid ios调用方式
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <param name="goodsname">商品名称</param>
        public static string WyhdAppidIos(int appid, string code, decimal price, int oid, string goodsname)
        {
            string str = "";
            string userid = "";//众易鑫商户id
            string userkey = "";//众易鑫key
            int pay_id = 0;//支付渠道id
            string wyhdappidios = "wyhdappidios" + appid;//组装缓存key值
            #region 微赢互动appid支付账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(wyhdappidios))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(wyhdappidios);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的微赢互动账号id
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的微赢互动key
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "微赢互动appid支付支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.selectAppid("wyhdappid", appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取微赢互动账号
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取微赢互动
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, wyhdappidios, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "微赢互动appid支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "微赢互动支付接口错误应用ID：" + appid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "微赢互动appid支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            Random r = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
            string dkip = HttpContext.Current.Request.UserHostAddress.Replace(".", "").Trim();
            string order_no = userid + "_" + DateTime.Now.ToString("MMddHHmmssfff") + r.Next(111111111, 999999999).ToString() + (dkip.Length > 4 ? dkip.Substring(0, 4) : r.Next(1111, 9999).ToString());
            System.Collections.Specialized.NameValueCollection Palist = new System.Collections.Specialized.NameValueCollection();
            Palist.Add("order_no", order_no);//订单号
            Palist.Add("goods_name", goodsname.Replace(" ", ""));//商品名称
            Palist.Add("order_amt", (Convert.ToInt32(price * 100)).ToString());//支付金额单位分
            Palist.Add("notify_url", ConfigurationManager.AppSettings["whdzfbNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步通知地址
            Palist.Add("custom", System.Web.HttpUtility.UrlEncode(code));//自定义参数
            WebClient webClient = new WebClient();
            string url = ConfigurationManager.AppSettings["whdappidPOSTUrl"].ToString();//请求地址
            byte[] responseData = webClient.UploadValues(url, "POST", Palist);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码 
            if (srcString.Contains("token_id") && srcString.Contains("appid"))
            {
                //string wxpay = "{\"paytype\":\"3\"," + srcString.Replace("{", "").Replace("}", "") + "}";
                Dictionary<string, object> list = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
                string wxpay = "{\"paytype\":\"3\",\"token_id\":\"" + list["token_id"].ToString() + "\", \"services\":\"pay.weixin.app\",\"appid\":\"" + list["appid"].ToString() + "\"}";
                str = "{\"message\":\"成功\",\"result\":100,\"data\":" + wxpay + "}";
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }
        #endregion

        #region 思科无限支付宝

        /// <summary>
        ///思科无限支付宝wap h5调用模式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string skwxzfbh5(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            string uid = "";//商户编号
            string MD5KEY = "";//key
            int pay_id = 0;//支付通道id
            string skwxzfbh5jkhc = "skwxzfbh5jkhc" + tid;//组装缓存key值
            #region 思科无限支付宝支付账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(skwxzfbh5jkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(skwxzfbh5jkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的思科无限支付宝账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的思科无限支付宝key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "思科无限支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("skwxzfb", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取思科无限支付宝账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取思科无限支付宝key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, skwxzfbh5jkhc, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "思科无限支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "思科无限支付宝支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "思科无限支付宝支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            NameValueCollection Palist = new NameValueCollection();
            Palist.Add("order_no", code);//订单号
            Palist.Add("goods_name", System.Web.HttpUtility.UrlEncode(goodsname.Replace(" ", "")));//商品名称
            Palist.Add("order_amt", price.ToString());//支付金额单位元
            Palist.Add("return_url", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", oid.ToString()));//同步跳转
            Palist.Add("notify_url", ConfigurationManager.AppSettings["skwxzfbNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步跳转
            Palist.Add("mchid", uid);//商户号
            string md5 = "order_no=" + Palist["order_no"] + "&order_amt=" + Palist["order_amt"] + "&key=" + MD5KEY;
            string md5str = JMP.TOOL.MD5.md5strGet(md5, true).ToLower();
            Palist.Add("sign", md5str);//签名
            string url = ConfigurationManager.AppSettings["skwxzfbPOSTUrl"].ToString();
            //WebClient webClient = new WebClient();
            //byte[] responseData = webClient.UploadValues(url, "POST", Palist);//得到返回字符流  
            //string srcString = Encoding.UTF8.GetString(responseData);//解码 
            //if (srcString.StartsWith("https"))
            //{
            //    str = srcString;
            //}
            //else
            //{
            //    AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "思科无限支付宝支付接口错误", "获取到的参数！" + srcString);//写入报错日志
            //    str = "{\"message\":\"支付通道异常\",\"result\":104}";
            //}
            StringBuilder html = new StringBuilder();
            html.AppendLine(" <form id='skwxpay' name='skwxpay' method='POST' action='" + url + "'  > ");
            foreach (string key in Palist.Keys)
            {
                html.AppendLine(" <input type='hidden' name ='" + key + "' value ='" + Palist[key] + "'  />  ");
            }
            html.AppendLine("</form>");
            html.AppendLine("<script> document.forms['skwxpay'].submit();</script>");
            string h5key = "h5" + code;
            JMP.TOOL.CacheHelper.CacheObject(h5key, html, 1);
            str = JMP.TOOL.Encrypt.IndexEncrypt(h5key);
            str = "/UnionPay.aspx?UnionPay=" + str;
            return str;
        }

        /// <summary>
        ///思科无限支付宝wap 安卓调用模式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string skwxzfbAz(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            string uid = "";//商户编号
            string MD5KEY = "";//key
            int pay_id = 0;//支付通道id
            string skwxzfbAzjkhc = "skwxzfbAzjkhc" + tid;//组装缓存key值
            #region 思科无限支付宝支付账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(skwxzfbAzjkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(skwxzfbAzjkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的思科无限支付宝账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的思科无限支付宝key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "思科无限支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("skwxzfb", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取思科无限支付宝账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取思科无限支付宝key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, skwxzfbAzjkhc, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "思科无限支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "思科无限支付宝支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "思科无限支付宝支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            NameValueCollection Palist = new NameValueCollection();
            Palist.Add("order_no", code);//订单号
            Palist.Add("goods_name", System.Web.HttpUtility.UrlEncode(goodsname.Replace(" ", "")));//商品名称
            Palist.Add("order_amt", price.ToString());//支付金额单位元
            Palist.Add("return_url", "http://www.baidu.com");//同步跳转
            Palist.Add("notify_url", ConfigurationManager.AppSettings["skwxzfbNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步跳转
            Palist.Add("mchid", uid);//商户号
            string md5 = "order_no=" + Palist["order_no"] + "&order_amt=" + Palist["order_amt"] + "&key=" + MD5KEY;
            string md5str = JMP.TOOL.MD5.md5strGet(md5, true).ToLower();
            Palist.Add("sign", md5str);//签名
            //WebClient webClient = new WebClient();
            string url = ConfigurationManager.AppSettings["skwxzfbPOSTUrl"].ToString();
            string html = url + "?";
            foreach (string key in Palist.Keys)
            {
                html += key + "=" + Palist[key] + "&";
            }
            html = html.TrimEnd('&');
            string codes = JMP.TOOL.Encrypt.IndexEncrypt(code);
            str = "{\"message\":\"成功\",\"result\":100,\"data\":{\"pay\":\"" + html + "\"}}";
            //if (srcString.StartsWith("https"))
            //{
            //    str = "{\"message\":\"成功\",\"result\":100,\"data\":{\"pay\":\"" + srcString + "\"}}";
            //}
            //else
            //{
            //    str = "{\"message\":\"支付通道异常\",\"result\":104}";
            //}
            return str;
        }

        /// <summary>
        ///思科无限支付宝wap 苹果调用模式
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string skwxzfbIos(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            string uid = "";//商户编号
            string MD5KEY = "";//key
            int pay_id = 0;//支付通道id
            string skwxzfbIosjkhc = "skwxzfbIosjkhc" + tid;//组装缓存key值
            #region 思科无限支付宝支付账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(skwxzfbIosjkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(skwxzfbIosjkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的思科无限支付宝账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的思科无限支付宝key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "思科无限支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("skwxzfb", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取思科无限支付宝账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取思科无限支付宝key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, skwxzfbIosjkhc, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "思科无限支付宝支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "思科无限支付宝支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "思科无限支付宝支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            NameValueCollection Palist = new NameValueCollection();
            Palist.Add("order_no", code);//订单号
            Palist.Add("goods_name", System.Web.HttpUtility.UrlEncode(goodsname.Replace(" ", "")));//商品名称
            Palist.Add("order_amt", price.ToString());//支付金额单位元
            Palist.Add("return_url", "http://www.baidu.com");//同步跳转
            Palist.Add("notify_url", ConfigurationManager.AppSettings["skwxzfbNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步跳转
            Palist.Add("mchid", uid);//商户号
            string md5 = "order_no=" + Palist["order_no"] + "&order_amt=" + Palist["order_amt"] + "&key=" + MD5KEY;
            string md5str = JMP.TOOL.MD5.md5strGet(md5, true).ToLower();
            Palist.Add("sign", md5str);//签名
                                       // WebClient webClient = new WebClient();
            string url = ConfigurationManager.AppSettings["skwxzfbPOSTUrl"].ToString();
            //byte[] responseData = webClient.UploadValues(url, "POST", Palist);//得到返回字符流  
            //string srcString = Encoding.UTF8.GetString(responseData);//解码 
            string html = url + "?";
            foreach (string key in Palist.Keys)
            {
                html += key + "=" + Palist[key] + "&";
            }
            html = html.TrimEnd('&');
            str = "{\"message\":\"成功\",\"result\":100,\"data\":{\"pay\":\"" + html + "\"}}";
            //if (srcString.StartsWith("https"))
            //{
            //    str = "{\"message\":\"成功\",\"result\":100,\"data\":{\"pay\":\"" + srcString + "\"}}";
            //}
            //else
            //{
            //    str = "{\"message\":\"支付通道异常\",\"result\":104}";
            //}
            return str;
        }
        #endregion

        #region 思科无限appid
        /// <summary>
        /// 思科无限appid 安卓调用模式
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <param name="goodsname">商品名称</param>
        /// <returns></returns>
        public static string skwxappidAz(int appid, string code, decimal price, int oid, string goodsname)
        {
            string str = "";
            string userid = "";//思科无限appid账号
            string userkey = "";//思科无限appid key
            string userappid = "";//微信appid
            int pay_id = 0;//支付渠道id
            string skwxappidAz = "skwxappidAz" + appid;//组装缓存key值
            #region 思科无限appid账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(skwxappidAz))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(skwxappidAz);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的思科无限appid账号
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的思科无限appidkey
                        userappid = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//获取appid
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "思科无限appid支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.selectAppid("skwxappid", appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取思科无限appid账号
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取思科无限appidkey
                        userappid = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//获取appid
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, skwxappidAz, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "思科无限appid支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "思科无限appid支付接口错误应用ID：" + appid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "思科无限appid支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            NameValueCollection Palist = new NameValueCollection();
            Palist.Add("order_no", code);//订单号
            Palist.Add("goods_name", goodsname.Replace(" ", ""));//商品名称
            Palist.Add("order_amt", price.ToString());//支付金额单位元
            Palist.Add("notify_url", ConfigurationManager.AppSettings["skwxzfbNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步跳转
            Palist.Add("mchid", userid);//商户号
            Palist.Add("method", "pay");
            string md5 = "order_no=" + Palist["order_no"] + "&order_amt=" + Palist["order_amt"] + "&key=" + userkey;
            string md5str = JMP.TOOL.MD5.md5strGet(md5, true).ToLower();
            Palist.Add("sign", md5str);//签名
            WebClient webClient = new WebClient();
            string url = ConfigurationManager.AppSettings["skwxappidPOSTUrl"].ToString();
            byte[] responseData = webClient.UploadValues(url, "POST", Palist);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码 
            if (srcString.Contains("token_id"))
            {
                Dictionary<string, object> list = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
                string wxpay = "{\"paytype\":\"3\",\"token_id\":\"" + list["token_id"].ToString() + "\", \"services\":\"pay.weixin.app\",\"status\":\"0\", \"charset\":\"UTF-8\", \"version\":\"2.0\", \"sign_type\":\"MD5\",\"appid\":\"" + userappid + "\"}";
                str = "{\"message\":\"成功\",\"result\":100,\"data\":" + wxpay + "}";
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }

        /// <summary>
        /// 思科无限appid ios调用模式
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <param name="goodsname">商品名称</param>
        /// <returns></returns>
        public static string skwxappidIos(int appid, string code, decimal price, int oid, string goodsname)
        {
            string str = "";
            string userid = "";//思科无限appid账号
            string userkey = "";//思科无限appid key
            string userappid = "";//微信appid
            int pay_id = 0;//支付渠道id
            string skwxappidIos = "skwxappidIos" + appid;//组装缓存key值
            #region 思科无限appid账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(skwxappidIos))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(skwxappidIos);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的思科无限appid账号
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的思科无限appidkey
                        userappid = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//获取appid
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "思科无限appid支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.selectAppid("skwxappid", appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取思科无限appid账号
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取思科无限appidkey
                        userappid = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//获取appid
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, skwxappidIos, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "思科无限appid支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "思科无限appid支付接口错误应用ID：" + appid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "思科无限appid支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            NameValueCollection Palist = new NameValueCollection();
            Palist.Add("order_no", code);//订单号
            Palist.Add("goods_name", goodsname.Replace(" ", ""));//商品名称
            Palist.Add("order_amt", price.ToString());//支付金额单位元
            Palist.Add("notify_url", ConfigurationManager.AppSettings["skwxzfbNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步跳转
            Palist.Add("mchid", userid);//商户号
            Palist.Add("method", "pay");
            string md5 = "order_no=" + Palist["order_no"] + "&order_amt=" + Palist["order_amt"] + "&key=" + userkey;
            string md5str = JMP.TOOL.MD5.md5strGet(md5, true).ToLower();
            Palist.Add("sign", md5str);//签名
            WebClient webClient = new WebClient();
            string url = ConfigurationManager.AppSettings["skwxappidPOSTUrl"].ToString();
            byte[] responseData = webClient.UploadValues(url, "POST", Palist);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码 
            if (srcString.Contains("token_id"))
            {
                string codes = JMP.TOOL.Encrypt.IndexEncrypt(code);
                Dictionary<string, object> list = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
                string wxpay = "{\"paytype\":\"3\",\"token_id\":\"" + list["token_id"].ToString() + "\", \"services\":\"pay.weixin.app\",\"status\":\"0\", \"charset\":\"UTF-8\", \"version\":\"2.0\", \"sign_type\":\"MD5\",\"appid\":\"" + userappid + "\",\"code\":\"" + codes + "\"}";
                str = "{\"message\":\"成功\",\"result\":100,\"data\":" + wxpay + "}";
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
            }
            return str;
        }
        #endregion

        #region 兴业银行appid
        /// <summary>
        /// 兴业银行appid 安卓调用模式
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <param name="goodsname">商品名称</param>
        /// <returns></returns>
        public static string xyyhappidAz(int appid, string code, decimal price, int oid, string goodsname)
        {
            string str = "";
            string userid = "";//兴业银行appid商户号
            string userkey = "";//兴业银行appid key
            string userappid = "";//兴业银行应用id
            string wxappid = "";//微信appid
            string store_appid = "";//门店id
            string store_name = "";//门店名称
            int pay_id = 0;//支付渠道id
            string xyyhappidAz = "xyyhappidAz" + appid;//组装缓存key值
            #region 兴业银行appid账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(xyyhappidAz))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(xyyhappidAz);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();
                        wxappid = paypz[2].Replace("\r", "").Replace("\n", "").Trim();
                        userappid = paypz[3].Replace("\r", "").Replace("\n", "").Trim();
                        store_appid = paypz[4].Replace("\r", "").Replace("\n", "").Trim();
                        store_name = paypz[5].Replace("\r", "").Replace("\n", "").Trim();
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "兴业银行appid支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.selectAppid("xyyhappid", appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();
                        wxappid = paypz[2].Replace("\r", "").Replace("\n", "").Trim();
                        userappid = paypz[3].Replace("\r", "").Replace("\n", "").Trim();
                        store_appid = paypz[4].Replace("\r", "").Replace("\n", "").Trim();
                        store_name = paypz[5].Replace("\r", "").Replace("\n", "").Trim();
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, xyyhappidAz, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "兴业银行appid支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "兴业银行appid支付接口错误应用ID：" + appid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "兴业银行appid支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            Dictionary<string, string> list = new Dictionary<string, string>();
            list.Add("version", "1.0.4");//版本号
            list.Add("device_type", "ANDROID");//操作系统
            list.Add("appid", userappid);//应用id
            list.Add("mch_id", userid);//商户号
            list.Add("wx_appid", wxappid);//微信appid
            //string noncestr= Guid.NewGuid().ToString().Replace("-", "");
            list.Add("nonce_str", code);//随机字符串
            list.Add("body", goodsname);//商品描述
            list.Add("attach", "`store_appid=" + store_appid + "#store_name=" + store_name + "#op_user=");//附加数据
            list.Add("out_trade_no", code);//订单号
            list.Add("total_fee", (Convert.ToInt32(price * 100)).ToString());//支付金额（单位：分整数类型）
            list.Add("spbill_create_ip", HttpContext.Current.Request.UserHostAddress);//ip地址
            //list.Add("spbill_create_ip", "14.104.85.212");
            list.Add("notify_url", ConfigurationManager.AppSettings["xyyhappidNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步通知地址
            list.Add("trade_type", "APP");//交易类型
            list.Add("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//交易开始时间
            list.Add("time_expire", DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss"));//交易失效时间
            string md5 = JMP.TOOL.UrlStr.AzGetStr(list) + "&key=" + userkey;
            string md5str = JMP.TOOL.MD5.md5strGet(md5, true);
            list.Add("sign", md5str);//签名           
            string xml = JMP.TOOL.xmlhelper.ToXml(list);
            string url = ConfigurationManager.AppSettings["xyyhappidPOSTUrl"].ToString();
            string srcString = JMP.TOOL.postxmlhelper.postxml(url, xml);
            Dictionary<string, object> lisjg = JMP.TOOL.xmlhelper.FromXml(srcString);
            if (lisjg.Count > 0 && lisjg["return_code"].ToString() == "SUCCESS")
            {
                string wxstr = "{\"paytype\":\"4\",\"appid\":\"" + lisjg["wx_appid"] + "\",\"partnerid\":\"" + lisjg["req_partnerid"] + "\",\"prepayid\":\"" + lisjg["prepay_id"] + "\",\"pkg\":\"Sign=WXPay\",\"noncestr\":\"" + lisjg["nonce_str"] + "\",\"timestamp\":\"" + lisjg["req_timestamp"] + "\",\"sign\":\"" + lisjg["req_sign"] + "\"}";
                str = "{\"message\":\"成功\",\"result\":100,\"data\":" + wxstr + "}";
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "兴业银行appid支付接口错误", "支付接口异常,返回参数：" + srcString);//写入报错日志
            }
            return str;
        }

        /// <summary>
        /// 兴业银行appid ios调用模式
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <param name="goodsname">商品名称</param>
        /// <returns></returns>
        public static string xyyhappidIos(int appid, string code, decimal price, int oid, string goodsname)
        {
            string str = "";
            string userid = "";//兴业银行appid商户号
            string userkey = "";//兴业银行appid key
            string userappid = "";//兴业银行应用id
            string wxappid = "";//微信appid
            string store_appid = "";//门店id
            string store_name = "";//门店名称
            int pay_id = 0;//支付渠道id
            string xyyhappidIos = "xyyhappidIos" + appid;//组装缓存key值
            #region 兴业银行appid账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(xyyhappidIos))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(xyyhappidIos);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();
                        wxappid = paypz[2].Replace("\r", "").Replace("\n", "").Trim();
                        userappid = paypz[3].Replace("\r", "").Replace("\n", "").Trim();
                        store_appid = paypz[4].Replace("\r", "").Replace("\n", "").Trim();
                        store_name = paypz[5].Replace("\r", "").Replace("\n", "").Trim();
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "兴业银行appid支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.selectAppid("xyyhappid", appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string[] paypz = dt.Rows[0]["l_str"].ToString().Split(',');
                        userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();
                        userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();
                        wxappid = paypz[2].Replace("\r", "").Replace("\n", "").Trim();
                        userappid = paypz[3].Replace("\r", "").Replace("\n", "").Trim();
                        store_appid = paypz[4].Replace("\r", "").Replace("\n", "").Trim();
                        store_name = paypz[5].Replace("\r", "").Replace("\n", "").Trim();
                        pay_id = int.Parse(dt.Rows[0]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, xyyhappidIos, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "兴业银行appid支付接口错误", "应用id为：" + appid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "兴业银行appid支付接口错误应用ID：" + appid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "兴业银行appid支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            Dictionary<string, string> list = new Dictionary<string, string>();
            list.Add("version", "1.0.4");//版本号
            list.Add("device_type", "ANDROID");//操作系统
            list.Add("appid", userappid);//应用id
            list.Add("mch_id", userid);//商户号
            list.Add("wx_appid", wxappid);//微信appid
            list.Add("nonce_str", code);//随机字符串
            list.Add("body", goodsname);//商品描述
            list.Add("attach", "`store_appid=" + store_appid + "#store_name=" + store_name + "#op_user=");//附加数据
            list.Add("out_trade_no", code);//订单号
            list.Add("total_fee", (Convert.ToInt32(price * 100)).ToString());//支付金额（单位：分整数类型）
            list.Add("spbill_create_ip", HttpContext.Current.Request.UserHostAddress);//ip地址
                                                                                      // list.Add("spbill_create_ip", "14.104.85.212");
            list.Add("notify_url", ConfigurationManager.AppSettings["xyyhappidNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步通知地址
            list.Add("trade_type", "APP");//交易类型
            list.Add("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//交易开始时间
            list.Add("time_expire", DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss"));//交易失效时间
            string md5 = JMP.TOOL.UrlStr.AzGetStr(list) + "&key=" + userkey;
            string md5str = JMP.TOOL.MD5.md5strGet(md5, true);
            list.Add("sign", md5str);//签名           
            string xml = JMP.TOOL.xmlhelper.ToXml(list);
            string url = ConfigurationManager.AppSettings["xyyhappidPOSTUrl"].ToString();
            string srcString = JMP.TOOL.postxmlhelper.postxml(url, xml);
            Dictionary<string, object> lisjg = JMP.TOOL.xmlhelper.FromXml(srcString);
            if (lisjg.Count > 0 && lisjg["return_code"].ToString() == "SUCCESS")
            {
                string codes = JMP.TOOL.Encrypt.IndexEncrypt(code);
                string wxstr = "{\"paytype\":\"4\",\"appid\":\"" + lisjg["wx_appid"] + "\",\"partnerid\":\"" + lisjg["req_partnerid"] + "\",\"prepayid\":\"" + lisjg["prepay_id"] + "\",\"pkg\":\"Sign=WXPay\",\"noncestr\":\"" + lisjg["nonce_str"] + "\",\"timestamp\":\"" + lisjg["req_timestamp"] + "\",\"sign\":\"" + lisjg["req_sign"] + "\",\"code\":\"" + codes + "\"}";
                str = "{\"message\":\"成功\",\"result\":100,\"data\":" + wxstr + "}";
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "兴业银行appid支付接口错误", "支付接口异常,返回参数：" + srcString);//写入报错日志
            }
            return str;
        }
        #endregion

        #region 舒付微信扫码
        /// <summary>
        /// 舒付微信扫码支付
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static string sfwxsm(int tid, string code, string goodsname, decimal price, int oid)
        {
            string str = "";
            string uid = "";//商户编号
            string MD5KEY = "";//key
            int pay_id = 0;//支付通道id
            string sfwxsmjkhc = "sfwxsmjkhc" + tid;//组装缓存key值
            #region 舒付微信扫码支付账号信息
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(sfwxsmjkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(sfwxsmjkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的舒付微信扫码账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的舒付微信扫码key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "舒付微信扫码支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
                else
                {
                    dt = bll.SelectPay("sfwxsm", tid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        uid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取舒付微信扫码账号
                        MD5KEY = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取舒付微信扫码key
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, sfwxsmjkhc, 1);//存入缓存
                    }
                    else
                    {
                        AddLocLog.AddLog(1, 4, "", "舒付微信扫码支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "舒付微信扫码支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                throw;
            }
            #endregion
            JMP.BLL.jmp_order blls = new JMP.BLL.jmp_order();
            if (!blls.UpdatePay(oid, pay_id))
            {
                str = "{\"message\":\"失败\",\"result\":101}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "舒付微信扫码支付接口错误", "修改支付渠道失败！");//写入报错日志
                return str;
            }
            Dictionary<string, string> list = new Dictionary<string, string>();
            list.Add("ordercode", code);//订单号
            list.Add("amount", price.ToString());//支付金额单位元
            list.Add("goodsId", "142");//支付类型
            list.Add("statedate", DateTime.Now.ToString("yyyyMMdd"));//交易日期
            list.Add("merNo", uid);//商户号
            list.Add("goods_name", goodsname);//商品名称
            list.Add("callbackurl", ConfigurationManager.AppSettings["sfwxsmNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步通知地址
            list.Add("callbackMemo", goodsname);//自定义参数
            string json = JMP.TOOL.JsonHelper.DictJsonstr(list);
            string postString = JMP.TOOL.DEShelsp.Encryption(MD5KEY, json);
            string url = ConfigurationManager.AppSettings["sfwxsmPOSTUrl"].ToString() + "?merNo=" + uid;//支付请求地址
            WebClient webClient = new WebClient();
            byte[] postData = Encoding.UTF8.GetBytes(postString);//编码 
            byte[] responseData = webClient.UploadData(url, "POST", postData);//得到返回字符流  
            webClient.Dispose();
            string srcString = Encoding.UTF8.GetString(responseData);//解码 
            srcString = JMP.TOOL.DEShelsp.Decrypt(MD5KEY, srcString);
            Dictionary<string, object> jsonstr = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
            if (jsonstr.ContainsKey("codeUrl") && jsonstr["result"].ToString() == "200")
            {
                string qurl = jsonstr["codeUrl"] + "," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ",2";//组装二维码地址
                string qrcodeurl = ConfigurationManager.AppSettings["QRcodeUrl"].ToString() + "?QRcode=" + JMP.TOOL.Encrypt.IndexEncrypt(qurl);//二维码展示地址
                str = qrcodeurl;
            }
            else
            {
                str = "{\"message\":\"支付通道异常\",\"result\":104}";
                AddLocLog.AddLog(1, 4, HttpContext.Current.Request.UserHostAddress, "舒付微信扫码支付接口错误", "支付接口异常,返回参数：" + srcString);//写入报错日志
            }
            return str;
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 抓取微付通微信付款参数
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string GETWFTWAP(string URL)
        {
            try
            {
                Encoding encoding = Encoding.Default;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Timeout = 3000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8))
                {
                    string jmpay = reader.ReadToEnd();
                    string NewsTitle = "";
                    Regex regex1 = new Regex("" + "location.href=" + @"(?<name>[\s\S]+?)" + ";" + "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Match match1 = regex1.Match(jmpay);
                    {
                        NewsTitle = match1.Groups["name"].ToString();
                    }
                    return NewsTitle;
                }
            }
            catch { return "\"请求超时\""; }

        }
    }
}