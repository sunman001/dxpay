using System;
using System.Collections.Generic;
using System.Web;
using JMP.TOOL;
using System.Data;
using DxPay.LogManager.LogFactory.ApiLog;

namespace WxPayAPI
{
    /**
    * 	配置账号信息
    */
    public class WxPayConfig
    {
        //=======【基本信息设置】=====================================
        /* 微信公众号信息配置
        * APPID：绑定支付的APPID（必须配置）
        * MCHID：商户号（必须配置）
        * KEY：商户支付密钥，参考开户邮件设置（必须配置）
        * APPSECRET：公众帐号secert（仅JSAPI支付的时候需要配置）
        */
        //public static string APPID = "wxea8d093ae88d269e";
        //public static string MCHID = "1315776801";
        //public static string KEY = "EpLYNdj4paNWoUqLCppeRSmUXzA3I9bw";
        //public const string APPSECRET = "6aadc88eeaf4b96b851200c767973e93";
        public string APPID;
        public string MCHID;
        public string KEY;
        public string APPSECRET;
        //=======【证书路径设置】===================================== 
        /* 证书路径,注意应该填写绝对路径（仅退款、撤销订单时需要）
        */
        public const string SSLCERT_PATH = "cert/apiclient_cert.p12";
        public const string SSLCERT_PASSWORD = "1233410002";
        //=======【支付结果通知url】===================================== 
        /* 支付结果通知回调url，用于商户接收支付结果
        */
        public const string NOTIFY_URL = "";

        //=======【商户系统后台机器IP】===================================== 
        /* 此参数可手动配置也可在程序中自动获取
        */
        public string IP = "8.8.8.8";


        //=======【代理服务器设置】===================================
        /* 默认IP和端口号分别为0.0.0.0和0，此时不开启代理（如有需要才设置）
        */
        public const string PROXY_URL = "http://0.0.0.0:0";

        //=======【上报信息配置】===================================
        /* 测速上报等级，0.关闭上报; 1.仅错误时上报; 2.全量上报
        */
        public const int REPORT_LEVENL = 1;

        //=======【日志级别】===================================
        /* 日志等级，0.不输出日志；1.只输出错误信息; 2.输出错误和正常信息; 3.输出错误信息、正常信息和调试信息
        */
        public const int LOG_LEVENL = 0;
        public WxPayConfig(int tid)
        {
            #region 获取微信口信息
            try
            {
                DataTable dt = new DataTable();
                var zf = new JMP.BLL.jmp_interface();
                string wxzfjk = "wxzfjk" + tid;//组装缓存key值
                if (JMP.TOOL.CacheHelper.IsCache(wxzfjk))
                {

                    string cache = JMP.TOOL.CacheHelper.GetCaChe(wxzfjk).ToString();
                    if (!String.IsNullOrEmpty(cache))
                    {
                        string[] str = cache.Split(',');
                        MCHID = str[0];//获取微信账号
                        KEY = str[1].Replace("\r", "").Replace("\n", "").Trim();//获取微信私钥
                        APPID = str[2].Replace("\r", "").Replace("\n", "").Trim();//获取微信appid
                        APPSECRET = cache.Split(',').Length > 3 ? str[3].Replace("\r", "").Replace("\n", "").Trim() : null;//获取微信appkey
                    }
                    else
                    {
                        //AddLocLog.AddLog(1, 4, "", "微信支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                        PayApiGlobalErrorLogger.Log("应用类型id为：" + tid + "的支付通道为空！", summary: "微信支付接口错误");
                    }

                }
                else
                {
                    string strs = zf.strzf_monitor(tid);
                    if (!String.IsNullOrEmpty(strs))
                    {
                        string[] str = strs.Split(',');
                        MCHID = str[0];//获取微信账号
                        KEY = str[1].Replace("\r", "").Replace("\n", "").Trim();//获取微信私钥
                        APPID = str[2].Replace("\r", "").Replace("\n", "").Trim();//获取微信appid
                        APPSECRET = strs.Split(',').Length > 3 ? str[3].Replace("\r", "").Replace("\n", "").Trim() : null;//获取微信appkey
                        JMP.TOOL.CacheHelper.CacheObject(wxzfjk, strs, 1);//存入缓存
                    }
                    else
                    {
                        //AddLocLog.AddLog(1, 4, "", "微信支付接口错误", "应用类型id为：" + tid + "的支付通道为空！");//写入报错日志
                        PayApiGlobalErrorLogger.Log("应用类型id为：" + tid + "的支付通道为空！", summary: "微信支付接口错误");
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                //AddLocLog.AddLog(1, 4, "", "微信支付接口错误", bcxx);//写入报错日志
                PayApiGlobalErrorLogger.Log(bcxx, summary: "微信支付接口错误");
            }

            #endregion
        }
    }
}