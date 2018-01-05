using JMP.TOOL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxPay.LogManager.LogFactory.ApiLog;
namespace Pay.DinPay
{
    public class DPConfing
    {
        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <returns></returns>
        public static Dictionary<String, String> loadCfg(int tid,int appid)
        {

            Dictionary<String, String> cfg = new Dictionary<string, string>();
            string dpkey = "";//智付私钥
            string dppartner = "";//智付账号
            int dppay_id = 0;//支付渠道id
            decimal minmun = 0;
            decimal maximum = 0;
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface blli = new JMP.BLL.jmp_interface();
                string DPzfjkhc = "DPzfjkhc" + appid;//组装缓存key值
                if (JMP.TOOL.CacheHelper.IsCache(DPzfjkhc))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(DPzfjkhc);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] payzf = dt.Rows[row]["l_str"].ToString().Split(',');
                        dpkey = payzf[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的智付私钥 
                        dppartner = payzf[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的智付账号
                        dppay_id = Int32.Parse(dt.Rows[row]["l_id"].ToString());//支付渠道id
                        minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                    }
                    else
                    {
                        dt = blli.SelectPay("ZF", tid, appid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] payzf = dt.Rows[row]["l_str"].ToString().Split(',');
                            dppartner = payzf[0].Replace("\r", "").Replace("\n", "").Trim();//获取智付账号
                            dpkey = payzf[1].Replace("\r", "").Replace("\n", "").Trim();//获取智付私钥
                            dppay_id = Int32.Parse(dt.Rows[row]["l_id"].ToString());//支付渠道id
                            minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, DPzfjkhc, 1);//存入缓存
                        }
                        else
                        {
                            PayApiGlobalErrorLogger.Log("应用id为：" + appid + "的支付通道为空！风控id为：" + tid + ",获取缓存失败后从数据库未查询到相关信息！", summary: "智付支付接口错误");
                        }
                    }
                }
                else
                {
                    dt = blli.SelectPay("ZF", tid,appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] payzf = dt.Rows[row]["l_str"].ToString().Split(',');
                        dppartner = payzf[0].Replace("\r", "").Replace("\n", "").Trim();//获取智付账号
                        dpkey = payzf[1].Replace("\r", "").Replace("\n", "").Trim();//获取智付私钥
                        dppay_id = Int32.Parse(dt.Rows[row]["l_id"].ToString());//支付渠道id
                        minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, DPzfjkhc, 1);//存入缓存
                    }
                    else
                    {
                        PayApiGlobalErrorLogger.Log("应用id为：" + appid + "的支付通道为空！风控id为：" + tid + ",从数据库未查询到相关信息！", summary: "智付支付接口错误");
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                AddLocLog.AddLog(1, 4, "", "智付支付接口错误", bcxx);//写入报错日志
                throw;
            }
            //从数据库读取配置文件
            cfg.Add("partner", dppartner);
            cfg.Add("dpkey", dpkey);
            cfg.Add("pay_id", dppay_id.ToString());
            cfg.Add("minmun", minmun.ToString());
            cfg.Add("maximum", maximum.ToString());
            return cfg;
        }
    }
}
