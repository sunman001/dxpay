using DxPay.LogManager.LogFactory.ApiLog;
using JMP.TOOL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace swiftpass.utils
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class Utils
    {
        public Utils() { }

        /// <summary>
        /// 对字符串进行URL编码
        /// </summary>
        /// <param name="instr">URL字符串</param>
        /// <param name="charset">编码</param>
        /// <returns></returns>
        public static string UrlEncode(string instr, string charset)
        {
            //return instr;
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                string res;

                try
                {
                    res = HttpUtility.UrlEncode(instr, Encoding.GetEncoding(charset));

                }
                catch (Exception ex)
                {
                    res = HttpUtility.UrlEncode(instr, Encoding.GetEncoding("GB2312"));
                    Console.WriteLine(ex);
                }


                return res;
            }
        }


        /// <summary>
        /// 对字符串进行URL解码
        /// </summary>
        /// <param name="instr">编码的URL字符串</param>
        /// <param name="charset">编码</param>
        /// <returns></returns>
        public static string UrlDecode(string instr, string charset)
        {
            if (instr == null || instr.Trim() == "")
                return "";
            else
            {
                string res;

                try
                {
                    res = HttpUtility.UrlDecode(instr, Encoding.GetEncoding(charset));

                }
                catch (Exception ex)
                {
                    res = HttpUtility.UrlDecode(instr, Encoding.GetEncoding("GB2312"));
                    Console.WriteLine(ex);
                }


                return res;

            }
        }


        /// <summary>
        /// 取时间戳生成随即数,替换交易单号中的后10位流水号
        /// </summary>
        /// <returns></returns>
        public static UInt32 UnixStamp()
        {
            TimeSpan ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return Convert.ToUInt32(ts.TotalSeconds);
        }


        /// <summary>
        /// 取随机数
        /// </summary>
        /// <param name="length">随机数的长度</param>
        /// <returns></returns>
        public static string BuildRandomStr(int length)
        {
            Random rand = new Random();

            int num = rand.Next();

            string str = num.ToString();

            if (str.Length > length)
            {
                str = str.Substring(0, length);
            }
            else if (str.Length < length)
            {
                int n = length - str.Length;
                while (n > 0)
                {
                    str.Insert(0, "0");
                    n--;
                }
            }

            return str;
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <returns></returns>
        public static Dictionary<String, String> loadCfg(int tid, int appid)
        {

            Dictionary<String, String> cfg = new Dictionary<string, string>();
            #region 获取威富通接口信息
            string private_key = "";//威富通密钥
            string partner = "";//威富通账号
            int pay_id = 0;//支付渠道id
            decimal minmun = 0;
            decimal maximum = 0;
            string wftjkhc = "wftjkhc" + appid;//组装缓存key值
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(wftjkhc))
                {
                    // string cache = JMP.TOOL.CacheHelper.GetCaChe(wftjkhc).ToString();
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(wftjkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        partner = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的威富通账号
                        private_key = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的威富通密钥
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        minmun = string.IsNullOrEmpty(dt.Rows[row]["l_minimum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        maximum = string.IsNullOrEmpty(dt.Rows[row]["l_maximum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                    }
                    else
                    {
                        dt = bll.SelectPay("WFT", tid, appid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                            partner = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取威富通账号
                            private_key = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取威富通密钥
                            pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                            //JMP.TOOL.CacheHelper.CacheObject(wftjkhc, str, 1);
                            minmun = string.IsNullOrEmpty(dt.Rows[row]["l_minimum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            maximum = string.IsNullOrEmpty(dt.Rows[row]["l_maximum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, wftjkhc, 1);//存入缓存
                        }
                        else
                        {
                            PayApiGlobalErrorLogger.Log("应用id为：" + appid + "的支付通道为空！风控id为：" + tid + ",获取缓存失败后，从数据库未查询到相关信息！", summary: "威富通支付接口错误");
                        }
                    }
                }
                else
                {
                    //JMP.TOOL.zfinterface zf = new JMP.TOOL.zfinterface();
                    //string str = zf.wftpzjk(tid);
                    dt = bll.SelectPay("WFT", tid, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        partner = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取威富通账号
                        private_key = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取威富通密钥
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        //JMP.TOOL.CacheHelper.CacheObject(wftjkhc, str, 1);
                        minmun = string.IsNullOrEmpty(dt.Rows[row]["l_minimum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        maximum = string.IsNullOrEmpty(dt.Rows[row]["l_maximum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, wftjkhc, 1);//存入缓存
                    }
                    else
                    {
                        //AddLocLog.AddLog(1, 4, "", "威富通支付接口错误", "应用id为：" + appid + "的支付通道为空！风控id为：" + tid);//写入报错日志
                        PayApiGlobalErrorLogger.Log("应用id为：" + appid + "的支付通道为空！风控id为：" + tid + ",直接从数据库未查询到相关信息！", summary: "威富通支付接口错误");
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                //AddLocLog.AddLog(1, 4, "", "威富通支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                PayApiGlobalErrorLogger.Log(bcxx, summary: "威富通支付接口错误应用类型ID：" + tid);
                throw;
            }
            #endregion
            //从数据库读取配置文件
            cfg.Add("mch_id", partner);
            cfg.Add("key", private_key);

            cfg.Add("req_url", "https://pay.swiftpass.cn/pay/gateway");
            cfg.Add("version", "2.0");
            cfg.Add("pay_id", pay_id.ToString());

            cfg.Add("minmun", minmun.ToString());
            cfg.Add("maximum", maximum.ToString());
            return cfg;
        }


        /// <summary>
        /// 微信公众号加载配置文件
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <returns></returns>
        public static Dictionary<String, String> loadCfgWxgzh(int tid, int appid)
        {

            Dictionary<String, String> cfg = new Dictionary<string, string>();
            #region 获取威富通接口信息
            string private_key = "";//威富通密钥
            string partner = "";//威富通账号
            int pay_id = 0;//支付渠道id
            decimal minmun = 0;
            decimal maximum = 0;
            string wftgzhjkhc = "wftgzhjkhc" + appid;//组装缓存key值
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(wftgzhjkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(wftgzhjkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        partner = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的威富通公众号账号
                        private_key = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的威富通公众号密钥
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        minmun = string.IsNullOrEmpty(dt.Rows[row]["l_minimum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        maximum = string.IsNullOrEmpty(dt.Rows[row]["l_maximum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                    }
                    else
                    {
                        dt = bll.SelectPay("WFTGZH", tid, appid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                            partner = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取威富通公众号账号
                            private_key = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取威富通公众号密钥
                            pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                            minmun = string.IsNullOrEmpty(dt.Rows[row]["l_minimum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            maximum = string.IsNullOrEmpty(dt.Rows[row]["l_maximum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, wftgzhjkhc, 1);//存入缓存
                        }
                        else
                        {
                            PayApiGlobalErrorLogger.Log("应用id为：" + appid + "的支付通道为空！风控id为：" + tid + ",获取缓存失败后，从数据库未查询到相关信息！", summary: "威富通公众号支付接口错误");
                        }
                    }
                }
                else
                {
                    dt = bll.SelectPay("WFTGZH", tid, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        partner = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取威富通公众号账号
                        private_key = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取威富通公众号密钥
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        minmun = string.IsNullOrEmpty(dt.Rows[row]["l_minimum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        maximum = string.IsNullOrEmpty(dt.Rows[row]["l_maximum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, wftgzhjkhc, 1);//存入缓存
                    }
                    else
                    {
                        PayApiGlobalErrorLogger.Log("应用id为：" + appid + "的支付通道为空！风控id为：" + tid + ",直接从数据库未查询到相关信息！", summary: "威富通公众号支付接口错误");
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                //AddLocLog.AddLog(1, 4, "", "威富通公众号支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                PayApiGlobalErrorLogger.Log(bcxx, summary: "威富通公众号支付接口错误应用类型ID：" + tid);
                throw;
            }
            #endregion
            //从数据库读取配置文件
            cfg.Add("mch_id", partner);
            cfg.Add("key", private_key);

            cfg.Add("req_url", "https://pay.swiftpass.cn/pay/gateway");
            cfg.Add("version", "2.0");
            cfg.Add("pay_id", pay_id.ToString());
            cfg.Add("minmun", minmun.ToString());
            cfg.Add("maximum", maximum.ToString());
            return cfg;
        }

        /// <summary>
        /// 威富通应用加载配置文件
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="apptype">风控配置表id</param>
        /// <returns></returns>
        public static Dictionary<String, String> loadCfgWxApp(int appid, int apptype)
        {

            Dictionary<String, String> cfg = new Dictionary<string, string>();
            #region 获取威富通接口信息
            string private_key = "";//威富通密钥
            string partner = "";//威富通账号
            int pay_id = 0;//支付渠道id
            string AppId = "";//微信app应用id
            decimal minmun = 0;
            decimal maximum = 0;
            string wftAppjkhc = "wftAppjkhc" + appid;//组装缓存key值
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(wftAppjkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(wftAppjkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        partner = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的威富通公众号账号
                        private_key = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的威富通公众号密钥
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        minmun = string.IsNullOrEmpty(dt.Rows[row]["l_minimum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        maximum = string.IsNullOrEmpty(dt.Rows[row]["l_maximum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        AppId = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//应用id
                    }
                    else
                    {
                        dt = bll.SelectPay("WFTAPP", apptype, appid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                            partner = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取威富通公众号账号
                            private_key = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取威富通公众号密钥
                            pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                            minmun = string.IsNullOrEmpty(dt.Rows[row]["l_minimum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            maximum = string.IsNullOrEmpty(dt.Rows[row]["l_maximum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                            AppId = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//应用id
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, wftAppjkhc, 1);//存入缓存
                        }
                        else
                        {
                            PayApiGlobalErrorLogger.Log("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",获取缓存后，从数据库未查询到相关信息！", summary: "威富通应用支付接口错误");
                        }
                    }
                }
                else
                {
                    dt = bll.SelectPay("WFTAPP", apptype, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        partner = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取威富通公众号账号
                        private_key = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取威富通公众号密钥
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        minmun = string.IsNullOrEmpty(dt.Rows[row]["l_minimum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        maximum = string.IsNullOrEmpty(dt.Rows[row]["l_maximum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        AppId = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//应用id
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, wftAppjkhc, 1);//存入缓存
                    }
                    else
                    {
                        PayApiGlobalErrorLogger.Log("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",直接从数据库未查询到相关信息！", summary: "威富通应用支付接口错误");
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：说" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                //AddLocLog.AddLog(1, 4, "", "威富通应用支付接口错误应用ID：" + appid, bcxx);//写入报错日志
                PayApiGlobalErrorLogger.Log(bcxx, summary: "威富通应用支付接口错误应用ID：" + appid);
                throw;
            }
            #endregion
            //从数据库读取配置文件
            cfg.Add("mch_id", partner);
            cfg.Add("key", private_key);

            cfg.Add("req_url", "https://pay.swiftpass.cn/pay/gateway");
            cfg.Add("version", "2.0");
            cfg.Add("pay_id", pay_id.ToString());
            cfg.Add("appid", AppId.ToString());
            cfg.Add("minmun", minmun.ToString());
            cfg.Add("maximum", maximum.ToString());
            return cfg;
        }
        /// <summary>
        /// 威富通微信扫码支付
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <returns></returns>
        public static Dictionary<string, string> loadCfgWxSm(int tid, int appid)
        {

            Dictionary<String, String> cfg = new Dictionary<string, string>();
            #region 获取威富通接口信息
            string private_key = "";//威富通密钥
            string partner = "";//威富通账号
            int pay_id = 0;//支付渠道id
            decimal minmun = 0;
            decimal maximum = 0;
            string wftwxsmjkhc = "wftwxsmjkhc" + appid;//组装缓存key值
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(wftwxsmjkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(wftwxsmjkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        partner = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的威富通账号
                        private_key = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的威富通密钥
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        minmun = string.IsNullOrEmpty(dt.Rows[row]["l_minimum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        maximum = string.IsNullOrEmpty(dt.Rows[row]["l_maximum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                    }
                    else
                    {
                        dt = bll.SelectPay("WFTSM", tid, appid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                            partner = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取威富通账号
                            private_key = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取威富通密钥
                            pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                            minmun = string.IsNullOrEmpty(dt.Rows[row]["l_minimum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            maximum = string.IsNullOrEmpty(dt.Rows[row]["l_maximum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, wftwxsmjkhc, 1);//存入缓存
                        }
                        else
                        {
                            PayApiGlobalErrorLogger.Log("应用id为：" + appid + "的支付通道为空！风控id为：" + tid + ",获取缓存失败后，从数据库未查询到相关信息！", summary: "威富通微信扫码支付接口错误");
                        }
                    }
                }
                else
                {
                    dt = bll.SelectPay("WFTSM", tid, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        partner = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取威富通账号
                        private_key = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取威富通密钥
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        minmun = string.IsNullOrEmpty(dt.Rows[row]["l_minimum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        maximum = string.IsNullOrEmpty(dt.Rows[row]["l_maximum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, wftwxsmjkhc, 1);//存入缓存
                    }
                    else
                    {
                        PayApiGlobalErrorLogger.Log("应用id为：" + appid + "的支付通道为空！风控id为：" + tid + ",直接从数据库未查询到相关信息！", summary: "威富通微信扫码支付接口错误");
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                //AddLocLog.AddLog(1, 4, "", "威富通微信扫码支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                PayApiGlobalErrorLogger.Log(bcxx, summary: "威富通微信扫码支付接口错误应用类型ID：" + tid);
                throw;
            }
            #endregion
            //从数据库读取配置文件
            cfg.Add("mch_id", partner);
            cfg.Add("key", private_key);
            cfg.Add("req_url", "https://pay.swiftpass.cn/pay/gateway");
            cfg.Add("version", "2.0");
            cfg.Add("pay_id", pay_id.ToString());
            cfg.Add("minmun", minmun.ToString());
            cfg.Add("maximum", maximum.ToString());
            return cfg;
        }
        /// <summary>
        /// 威富通支付宝扫码支付
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public static Dictionary<string, string> loadCfgZfbSm(int tid, int appid)
        {
            Dictionary<String, String> cfg = new Dictionary<string, string>();
            #region 获取威富通接口信息
            string private_key = "";//威富通密钥
            string partner = "";//威富通账号
            int pay_id = 0;//支付渠道id
            decimal minmun = 0;
            decimal maximum = 0;
            string wftZfbsmjkhc = "wftZfbsmjkhc" + appid;//组装缓存key值
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(wftZfbsmjkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(wftZfbsmjkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        partner = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的威富通账号
                        private_key = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的威富通密钥
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        minmun = string.IsNullOrEmpty(dt.Rows[row]["l_minimum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        maximum = string.IsNullOrEmpty(dt.Rows[row]["l_maximum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                    }
                    else
                    {
                        dt = bll.SelectPay("WFTZFBSM", tid, appid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                            partner = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取威富通账号
                            private_key = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取威富通密钥
                            pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                            minmun = string.IsNullOrEmpty(dt.Rows[row]["l_minimum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            maximum = string.IsNullOrEmpty(dt.Rows[row]["l_maximum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, wftZfbsmjkhc, 1);//存入缓存
                        }
                        else
                        {
                            PayApiGlobalErrorLogger.Log("应用id为：" + appid + "的支付通道为空！风控id为：" + tid + ",获取缓存后，从数据库未查询到相关信息！", summary: "威富通支付宝扫码支付接口错误");
                        }
                    }
                }
                else
                {
                    dt = bll.SelectPay("WFTZFBSM", tid, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        partner = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取威富通账号
                        private_key = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取威富通密钥
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        minmun = string.IsNullOrEmpty(dt.Rows[row]["l_minimum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        maximum = string.IsNullOrEmpty(dt.Rows[row]["l_maximum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, wftZfbsmjkhc, 1);//存入缓存
                    }
                    else
                    {
                        PayApiGlobalErrorLogger.Log("应用id为：" + appid + "的支付通道为空！风控id为：" + tid + ",直接从数据库未查询到相关信息！", summary: "威富通支付宝扫码支付接口错误");
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                //AddLocLog.AddLog(1, 4, "", "威富通支付宝扫码支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                PayApiGlobalErrorLogger.Log(bcxx, summary: "威富通支付宝扫码支付接口错误应用类型ID：" + tid);
                throw;
            }
            #endregion
            //从数据库读取配置文件
            cfg.Add("mch_id", partner);
            cfg.Add("key", private_key);
            cfg.Add("req_url", "https://pay.swiftpass.cn/pay/gateway");
            cfg.Add("version", "2.0");
            cfg.Add("pay_id", pay_id.ToString());
            cfg.Add("minmun", minmun.ToString());
            cfg.Add("maximum", maximum.ToString());
            return cfg;
        }
        /// <summary>
        /// 威富通支付宝支付
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public static Dictionary<string, string> loadCfgZfb(int tid, int appid)
        {
            Dictionary<String, String> cfg = new Dictionary<string, string>();
            #region 获取威富通接口信息
            string private_key = "";//威富通密钥
            string partner = "";//威富通账号
            int pay_id = 0;//支付渠道id
            decimal minmun = 0;
            decimal maximum = 0;
            string loadCfgZfb = "loadCfgZfb" + appid;//组装缓存key值
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(loadCfgZfb))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(loadCfgZfb);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        partner = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的威富通账号
                        private_key = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的威富通密钥
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        minmun = string.IsNullOrEmpty(dt.Rows[row]["l_minimum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        maximum = string.IsNullOrEmpty(dt.Rows[row]["l_maximum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                    }
                    else
                    {
                        dt = bll.SelectPay("WFTZFB", tid, appid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                            partner = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取威富通账号
                            private_key = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取威富通密钥
                            pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                            minmun = string.IsNullOrEmpty(dt.Rows[row]["l_minimum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            maximum = string.IsNullOrEmpty(dt.Rows[row]["l_maximum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, loadCfgZfb, 1);//存入缓存
                        }
                        else
                        {
                            PayApiGlobalErrorLogger.Log("应用id为：" + appid + "的支付通道为空！风控id为：" + tid + ",获取缓存失败后，从数据库未查询到相关信息！", summary: "威富通支付宝支付接口错误");
                        }
                    }
                }
                else
                {
                    dt = bll.SelectPay("WFTZFB", tid, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        partner = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取威富通账号
                        private_key = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取威富通密钥
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        minmun = string.IsNullOrEmpty(dt.Rows[row]["l_minimum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        maximum = string.IsNullOrEmpty(dt.Rows[row]["l_maximum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, loadCfgZfb, 1);//存入缓存
                    }
                    else
                    {
                        PayApiGlobalErrorLogger.Log("应用id为：" + appid + "的支付通道为空！风控id为：" + tid + ",直接从数据库未查询到相关信息！", summary: "威富通支付宝支付接口错误");
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                //AddLocLog.AddLog(1, 4, "", "威富通支付宝支付接口错误应用类型ID：" + tid, bcxx);//写入报错日志
                PayApiGlobalErrorLogger.Log(bcxx, summary: "威富通支付宝支付接口错误应用类型ID：" + tid);
                throw;
            }
            #endregion
            //从数据库读取配置文件
            cfg.Add("mch_id", partner);
            cfg.Add("key", private_key);
            cfg.Add("req_url", "https://pay.swiftpass.cn/pay/gateway");
            cfg.Add("version", "2.0");
            cfg.Add("pay_id", pay_id.ToString());
            cfg.Add("minmun", minmun.ToString());
            cfg.Add("maximum", maximum.ToString());
            return cfg;
        }

        /// <summary>
        /// 威富通QQ钱包支付
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public static Dictionary<string, string> loadCfgQQ(int tid, int appid)
        {
            Dictionary<String, String> cfg = new Dictionary<string, string>();
            #region 获取威富通接口信息
            string private_key = "";//威富通密钥
            string partner = "";//威富通账号
            int pay_id = 0;//支付渠道id
            decimal minmun = 0;
            decimal maximum = 0;
            string loadCfgQQWap = "loadCfgQQWap" + appid;//组装缓存key值
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(loadCfgQQWap))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(loadCfgQQWap);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        partner = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的威富通账号
                        private_key = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的威富通密钥
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        minmun = string.IsNullOrEmpty(dt.Rows[row]["l_minimum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        maximum = string.IsNullOrEmpty(dt.Rows[row]["l_maximum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                    }
                    else
                    {
                        dt = bll.SelectPay("WFTQQWAP", tid, appid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                            partner = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取威富通账号
                            private_key = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取威富通密钥
                            pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                            minmun = string.IsNullOrEmpty(dt.Rows[row]["l_minimum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            maximum = string.IsNullOrEmpty(dt.Rows[row]["l_maximum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, loadCfgQQWap, 1);//存入缓存
                        }
                        else
                        {
                            PayApiGlobalErrorLogger.Log("应用id为：" + appid + "的支付通道为空！风控id为：" + tid + ",获取缓存失败后，从数据库未查询到相关信息！", summary: "威富通QQ钱包wap支付接口错误");
                        }
                    }
                }
                else
                {
                    dt = bll.SelectPay("WFTQQWAP", tid, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        partner = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取威富通账号
                        private_key = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取威富通密钥
                        pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        minmun = string.IsNullOrEmpty(dt.Rows[row]["l_minimum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        maximum = string.IsNullOrEmpty(dt.Rows[row]["l_maximum"].ToString()) ? 0 : decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, loadCfgQQWap, 1);//存入缓存
                    }
                    else
                    {
                        PayApiGlobalErrorLogger.Log("应用id为：" + appid + "的支付通道为空！风控id为：" + tid + ",直接从数据库未查询到相关信息！", summary: "威富通QQ钱包支付接口错误");
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                PayApiGlobalErrorLogger.Log(bcxx, summary: "威富通QQ钱包支付接口错误应用类型ID：" + tid);
                throw;
            }
            #endregion
            //从数据库读取配置文件
            cfg.Add("mch_id", partner);
            cfg.Add("key", private_key);
            cfg.Add("req_url", "https://pay.swiftpass.cn/pay/gateway");
            cfg.Add("version", "2.0");
            cfg.Add("pay_id", pay_id.ToString());
            cfg.Add("minmun", minmun.ToString());
            cfg.Add("maximum", maximum.ToString());
            return cfg;
        }


        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="interfaceId">通道ID</param>
        /// <returns></returns>
        public static Dictionary<String, String> Load_CfgInterfaceId(int interfaceId)
        {

            Dictionary<String, String> cfg = new Dictionary<string, string>();
            #region 获取威富通接口信息
            string private_key = "";//威富通密钥
            string partner = "";//威富通账号
            string wftjkhc = "wftjkhc_monitor_" + interfaceId;//组装缓存key值
            try
            {
                if (JMP.TOOL.CacheHelper.IsCache(wftjkhc))
                {
                    string cache = JMP.TOOL.CacheHelper.GetCaChe(wftjkhc).ToString();
                    string[] paypz = cache.Split(',');
                    partner = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的威富通账号
                    private_key = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的威富通密钥
                }
                else
                {
                    var zf = new JMP.BLL.jmp_interface();
                    string str = zf.strzf_monitor(interfaceId);
                    string[] paypz = str.Split(',');
                    partner = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取威富通账号
                    private_key = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取威富通密钥
                    JMP.TOOL.CacheHelper.CacheObject(wftjkhc, str, 1);//存入缓存
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                //AddLocLog.AddLog(1, 4, "", "威富通支付接口错误通道ID：" + interfaceId, bcxx);//写入报错日志
                PayApiGlobalErrorLogger.Log(bcxx, summary: "威富通支付接口错误通道ID：" + interfaceId);
                throw;
            }
            #endregion
            //从数据库读取配置文件
            cfg.Add("mch_id", partner);
            cfg.Add("key", private_key);

            cfg.Add("req_url", "https://pay.swiftpass.cn/pay/gateway");
            cfg.Add("version", "2.0");
            return cfg;
        }

        /// <summary>
        /// 保存接口返回结果到文件中
        /// </summary>
        /// <param name="_param">接口结果</param>
        public static void writeFile(string title, Hashtable _param)
        {
            string resFilePath = Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ApplicationBase)
                                + Path.DirectorySeparatorChar + "result.txt";
            if (!File.Exists(resFilePath))
            {
                using (StreamWriter sw = new StreamWriter(resFilePath))
                {
                    sw.WriteLine("=====================" + title + "=====================");
                    foreach (DictionaryEntry de in _param)
                    {
                        sw.WriteLine("key:" + de.Key.ToString() + " value:" + de.Value.ToString());
                    }
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(resFilePath))
                {
                    sw.WriteLine("=====================" + title + "=====================");
                    foreach (DictionaryEntry de in _param)
                    {
                        sw.WriteLine("key:" + de.Key.ToString() + " value:" + de.Value.ToString());
                    }
                }
            }
        }

        public static void writeFile_message(string title, string message)
        {
            string resFilePath = Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ApplicationBase)
                                + Path.DirectorySeparatorChar + "result.txt";
            if (!File.Exists(resFilePath))
            {
                using (StreamWriter sw = new StreamWriter(resFilePath))
                {
                    sw.WriteLine("=====================" + title + "=====================");
                    sw.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(resFilePath))
                {
                    sw.WriteLine("=====================" + title + "=====================");
                    sw.WriteLine(message);
                }
            }
        }


        /// <summary>
        /// 生成32位随机数
        /// </summary>
        /// <returns></returns>
        public static string random()
        {
            char[] constant = {'0','1','2','3','4','5','6','7','8','9',
                               'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
                               'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
            StringBuilder sb = new StringBuilder(32);
            Random rd = new Random();
            for (int i = 0; i < 32; i++)
            {
                sb.Append(constant[rd.Next(62)]);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 生成16位订单号 by  hyf 2016年2月16日17:48:43
        /// </summary>
        /// <returns></returns>
        public static string Nmrandom()
        {
            string rm = "";
            Random ra = new Random();
            for (int i = 0; i < 16; i++)
            {
                rm += ra.Next(0, 9).ToString();
            }
            return rm;
        }
        /// <summary>
        /// 将Hashtable参数传为XML
        /// </summary>
        /// <param name="_params"></param>
        /// <returns></returns>
        public static string toXml(Hashtable _params)
        {
            StringBuilder sb = new StringBuilder("<xml>");
            foreach (DictionaryEntry de in _params)
            {
                string key = de.Key.ToString();
                sb.Append("<").Append(key).Append("><![CDATA[").Append(de.Value.ToString()).Append("]]></").Append(key).Append(">");
            }

            return sb.Append("</xml>").ToString();
        }

    }
}
