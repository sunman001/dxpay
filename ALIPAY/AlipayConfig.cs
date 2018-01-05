using System;
using JMP.TOOL;
using System.Data;
using DxPay.LogManager.LogFactory.ApiLog;

namespace Alipay
{
    public class Config
    {
        #region 字段
        public string partner = "";
        public string seller_id = "";
        public string private_key = "";
        public string public_key = "";
        public string input_charset = "";
        public string sign_type = "";
        public int pay_id = 0;//支付渠道id
        public decimal minmun = 0;
        public decimal maximum = 0;
        #endregion

        public Config(int tid, int appid)
        {
            #region 获取支付宝接口信息
            string zfbjkhc = "ZFBjkhc" + appid;//组装缓存key值
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(zfbjkhc))
                {
                    //string zfbpz = JMP.TOOL.CacheHelper.GetCaChe(zfbjkhc).ToString();
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(zfbjkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] payzf = dt.Rows[row]["l_str"].ToString().Split(',');
                        private_key = payzf[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的支付宝私钥 
                        partner = payzf[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的支付宝账号
                        pay_id = Int32.Parse(dt.Rows[row]["l_id"].ToString());//支付渠道id
                        minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                    }
                    else
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] payzf = dt.Rows[row]["l_str"].ToString().Split(',');
                            partner = payzf[0].Replace("\r", "").Replace("\n", "").Trim();//获取支付宝账号
                            private_key = payzf[1].Replace("\r", "").Replace("\n", "").Trim();//获取支付宝私钥
                            pay_id = Int32.Parse(dt.Rows[row]["l_id"].ToString());//支付渠道id
                            minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                                                                                          //JMP.TOOL.CacheHelper.CacheObject(zfbjkhc, str, 1);//存入缓存
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, zfbjkhc, 1);
                        }
                        else
                        {
                            PayApiGlobalErrorLogger.Log("报错信息：支付宝支付接口错误", "应用id为：" + appid + "的支付通道为空，风控配置表id：" + tid + ",获取缓存失败后，从数据库未查询到相关信息！", summary: "支付宝支付接口错误");
                        }
                    }
                }
                else
                {
                    dt = bll.SelectPay("ZFB", tid, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] payzf = dt.Rows[row]["l_str"].ToString().Split(',');
                        partner = payzf[0].Replace("\r", "").Replace("\n", "").Trim();//获取支付宝账号
                        private_key = payzf[1].Replace("\r", "").Replace("\n", "").Trim();//获取支付宝私钥
                        pay_id = Int32.Parse(dt.Rows[row]["l_id"].ToString());//支付渠道id
                        minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        //JMP.TOOL.CacheHelper.CacheObject(zfbjkhc, str, 1);//存入缓存
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, zfbjkhc, 1);
                    }
                    else
                    {
                        PayApiGlobalErrorLogger.Log("报错信息：支付宝支付接口错误", "应用id为：" + appid + "的支付通道为空，风控配置表id：" + tid + ",直接从数据库未查询到相关信息", summary: "支付宝支付接口错误");
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                                                                                                                        // AddLocLog.AddLog(1, 4, "", "支付宝支付接口错误", bcxx);//写入报错日志
                PayApiGlobalErrorLogger.Log("报错信息：" + bcxx, summary: "支付宝支付接口错误");
                throw;
            }

            #endregion

            seller_id = partner;
            //public_key = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCnxj/9qwVfgoUh/y2W89L6BkRAFljhNhgPdyPuBV64bfQNN1PjbCzkIM6qRdKBoLPXmKKMiFYnkd6rAoprih3/PrQEB/VsW8OoM8fxn67UDYuyBTqA23MML9q1+ilIZwBC2AQ2UBVOrFXfFl75p6/B5KsiNG9zpgmLCUYuLkxpLQIDAQAB";
            public_key = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDDI6d306Q8fIfCOaTXyiUeJHkrIvYISRcc73s3vF1ZT7XN8RNPwJxo8pWaJMmvyTn9N4HQ632qJBVHf8sxHi/fEsraprwCtzvzQETrNRwVxLO5jVmRGi60j8Ue1efIlzPXV9je9mkjzOmdssymZkh2QhUrCmZYI/FCEa3/cNMW0QIDAQAB";

            input_charset = "utf-8";
            sign_type = "RSA";
        }

    }

    /// <summary>
    /// 支付通道检测配置类
    /// </summary>
    public class ConfigMonitor
    {
        #region 字段
        public string partner = "";
        public string seller_id = "";
        public string private_key = "";
        public string public_key = "";
        public string input_charset = "";
        public string sign_type = "";
        public int pay_id = 0;//支付渠道id
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="interfaceId">通道ID</param>
        public ConfigMonitor(int interfaceId)
        {
            #region 获取支付宝接口信息
            var zfbjkhc = "ZFBjkhc_monitor_" + interfaceId;//组装缓存key值
            try
            {

                if (CacheHelper.IsCache(zfbjkhc))
                {
                    var zfbpz = CacheHelper.GetCaChe(zfbjkhc).ToString();
                    if (!string.IsNullOrEmpty(zfbpz))
                    {
                        var payzf = zfbpz.Split(',');
                        private_key = payzf[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的支付宝私钥 
                        partner = payzf[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的支付宝账号
                    }
                }
                else
                {
                    var zf = new JMP.BLL.jmp_interface();
                    var str = zf.strzf_monitor(interfaceId);
                    if (!string.IsNullOrEmpty(str))
                    {
                        var payzf = str.Split(',');
                        partner = payzf[0].Replace("\r", "").Replace("\n", "").Trim();//获取支付宝账号
                        private_key = payzf[1].Replace("\r", "").Replace("\n", "").Trim();//获取支付宝私钥
                        CacheHelper.CacheObject(zfbjkhc, str, 1);//存入缓存
                    }
                }
            }
            catch (Exception e)
            {
                var bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                //AddLocLog.AddLog(1, 4, "", "支付宝支付接口错误通道ID:" + interfaceId, bcxx);//写入报错日志
                PayApiGlobalErrorLogger.Log(bcxx, summary: "支付宝支付接口错误通道ID:" + interfaceId);
                throw;
            }

            #endregion

            seller_id = partner;
            //public_key = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCnxj/9qwVfgoUh/y2W89L6BkRAFljhNhgPdyPuBV64bfQNN1PjbCzkIM6qRdKBoLPXmKKMiFYnkd6rAoprih3/PrQEB/VsW8OoM8fxn67UDYuyBTqA23MML9q1+ilIZwBC2AQ2UBVOrFXfFl75p6/B5KsiNG9zpgmLCUYuLkxpLQIDAQAB";

            public_key = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDDI6d306Q8fIfCOaTXyiUeJHkrIvYISRcc73s3vF1ZT7XN8RNPwJxo8pWaJMmvyTn9N4HQ632qJBVHf8sxHi/fEsraprwCtzvzQETrNRwVxLO5jVmRGi60j8Ue1efIlzPXV9je9mkjzOmdssymZkh2QhUrCmZYI/FCEa3/cNMW0QIDAQAB";

            input_charset = "utf-8";
            sign_type = "RSA";
        }

    }
}