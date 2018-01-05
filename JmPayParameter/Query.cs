using DxPay.LogManager.LogFactory.ApiLog;
using JMP.MDL;
using JmPayParameter.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JmPayParameter
{
    /// <summary>
    /// 支付查询接口主通道
    /// </summary>
    public class Query
    {
        /// <summary>
        /// 查询接口主通道
        /// </summary>
        /// <param name="mode">请求参数实体</param>
        /// <param name="json">请求参数json字符串</param>
        /// <param name="CacheTime">查询有效时间</param>
        /// <returns></returns>
        public  QueryRespon SelectQuery(QueryModels mode, string json, int CacheTime)
        {

            QueryRespon qu = new QueryRespon();
            qu = parameter(mode, json, CacheTime);
            if (qu.Success)
            {
                if (SelectNum(mode))
                {
                    qu = Selectparameter(mode, json, CacheTime);
                    if (qu.Success)
                    {
                        qu = OrderSelect(mode, json);
                    }
                }
                else
                {
                    qu = qu.QueryResp(QueryErrorCode.Code9992);
                }
            }
            return qu;
        }
        /// <summary>
        /// 查询接口参数验证方法
        /// </summary>
        /// <param name="mode">参数实体</param>
        /// <param name="json">参数json字符串</param>
        /// <returns></returns>
        private  QueryRespon parameter(QueryModels mode, string json, int CacheTime)
        {
            QueryRespon qu = new QueryRespon();
            try
            {
                //订单缓存时间
                int bizcodeTime = Int32.Parse(ConfigurationManager.AppSettings["bizcodeTime"]);
                if (!string.IsNullOrEmpty(mode.bizcode) && mode.bizcode.Length > 64)
                {
                    return qu = qu.QueryResp(QueryErrorCode.Code9998);
                }
                if (!string.IsNullOrEmpty(mode.code) && mode.code.Length > 32)
                {
                    return qu = qu.QueryResp(QueryErrorCode.Code9997);
                }
                if (string.IsNullOrEmpty(mode.timestamp) || mode.timestamp.Length != 10)
                {
                    return qu = qu.QueryResp(QueryErrorCode.Code9996);
                }
                PreOrder preOrder = new PreOrder();
                if (!preOrder.VerificationTimestamp(mode.timestamp, bizcodeTime))
                {
                    return qu = qu.QueryResp(QueryErrorCode.Code9995);
                }
                if (string.IsNullOrEmpty(mode.code) && string.IsNullOrEmpty(mode.bizcode))
                {
                    return qu = qu.QueryResp(QueryErrorCode.Code9991);
                }
                qu = qu.QueryResp(QueryErrorCode.Code100);
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString() + "报错位置：" + e.StackTrace.ToString();//报错信息
                PayApiGlobalErrorLogger.Log("报错信息：查询接口验证参数错误,获取到的参数：" + json + ",报错信息：" + bcxx, summary: "接口错误信息");
                qu = qu.QueryResp(QueryErrorCode.Code101);
            }
            return qu;
        }
        /// <summary>
        /// 验证应用和签名是否合法
        /// </summary>
        /// <param name="mode">请求参数实体</param>
        /// <param name="json">请求参数json</param>
        /// <param name="CacheTime">有效查询时间</param>
        /// <returns></returns>
        private  QueryRespon Selectparameter(QueryModels mode, string json, int CacheTime)
        {
            QueryRespon qu = new QueryRespon();
            SelectAPP selectAPP = new SelectAPP();
            jmp_app app = selectAPP.SelectAppId(mode.appid, CacheTime);
            if (app != null)
            {
                //验证签名的方式 参数AIIZ首字母顺序排列md532大写加密
                Dictionary<string, string> dic = JMP.TOOL.JsonHelper.Deserialize<Dictionary<string, string>>(json);
                dic = dic.Where(x => x.Key != "sign").ToDictionary(x => x.Key, x => x.Value);
                string signstr = JMP.TOOL.UrlStr.AzGetStr(dic) + "&key=" + app.a_key;
                string md5str = JMP.TOOL.MD5.md5strGet(signstr, true).ToUpper();
                if (mode.sign != md5str)
                {
                    return qu = qu.QueryResp(QueryErrorCode.Code9993);
                }
                else
                {
                    qu = qu.QueryResp(QueryErrorCode.Code100);
                }
            }
            else
            {
                qu = qu.QueryResp(QueryErrorCode.Code9994);
            }
            return qu;
        }
        /// <summary>
        /// 查询相关订单信息
        /// </summary>
        /// <param name="mod">查询参数实体</param>
        /// <param name="json">json字符串</param>
        /// <returns></returns>
        private  QueryRespon OrderSelect(QueryModels mod, string json)
        {
            //订单缓存时间
            int infoTime = Int32.Parse(ConfigurationManager.AppSettings["infoTime"]);
            QueryRespon qu = new QueryRespon();
            JMP.MDL.jmp_order mode = new JMP.MDL.jmp_order();
            string hcname = "Query" + mod.appid.ToString();
            int QueryNum = 0;
            if (JMP.TOOL.CacheHelper.IsCache(hcname))//判读是否存在缓存
            {
                QueryNum = JMP.TOOL.CacheHelper.GetCaChe<int>(hcname);//获取缓存
            }
            mode = SelectOrder(mod, json);
            if (mode != null)
            {
                qu = qu.QueryResp(QueryErrorCode.Code100);
                qu.trade_code = mode.o_code;
                qu.trade_no = mode.o_bizcode;
                qu.trade_price = mode.o_price.ToString("f2");
                qu.o_state = mode.o_state == 1 ? mode.o_state : 0;
                qu.trade_time = mode.o_state == 1 ? mode.o_ptime.ToString("yyyyMMddHHmmss") : "";
                qu.trade_type = string.IsNullOrEmpty(mode.o_paymode_id) ? 0 : Int32.Parse(mode.o_paymode_id);
                QueryNum = QueryNum + 1;
                JMP.TOOL.CacheHelper.UpdateCacheObjectLocak<int>(QueryNum, hcname, infoTime);//存入缓存
            }
            else
            {
                qu = qu.QueryResp(QueryErrorCode.Code101);
            }
            return qu;
        }
        /// <summary>
        /// 验证查询次数是否合法
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        private  bool SelectNum(QueryModels mod)
        {
            bool succeed = false;
            string hcname = "Query" + mod.appid.ToString();
            int QueryNum = 0;
            if (JMP.TOOL.CacheHelper.IsCache(hcname))//判读是否存在缓存
            {
                QueryNum = JMP.TOOL.CacheHelper.GetCaChe<int>(hcname);//获取缓存
            }
            //规定时间内的查询次数
            int SelectQueryNum = Int32.Parse(ConfigurationManager.AppSettings["SelectQueryNum"]);
            if (SelectQueryNum > QueryNum)
            {
                succeed = true;
            }
            return succeed;
        }

        /// <summary>
        /// 根据商户订单号和appid查询订单信息
        /// </summary>
        /// <param name="mod">参数实体</param>
        /// <param name="json">参数json字符串</param>
        /// <returns></returns>
        private  jmp_order SelectOrder(QueryModels mod, string json)
        {
            JMP.MDL.jmp_order mode = new JMP.MDL.jmp_order();
            JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
            try
            {
                mode = bll.SelectOrderbizcode(mod.appid, mod.code, mod.bizcode, "jmp_order");
                if (mode == null)
                {
                    string orderTableName = JMP.TOOL.WeekDateTime.GetOrderTableName(DateTime.Now.ToString("yyyy-MM-dd"));//获取订单表名
                    mode = bll.SelectOrderbizcode(mod.appid, mod.code, mod.bizcode, orderTableName);//查询本周归档表
                    string weekstr = DateTime.Now.DayOfWeek.ToString();
                    if (mode == null && weekstr == "Monday" && Int32.Parse(DateTime.Now.ToString("HH")) <= 2)
                    {
                        string TableName = JMP.TOOL.WeekDateTime.GetOrderTableName(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"));//获取订单表名
                        mode = bll.SelectOrderbizcode(mod.appid, mod.code, mod.bizcode, orderTableName);//查询上周归档表
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString() + "报错位置：" + e.StackTrace.ToString();//报错信息
                PayApiGlobalErrorLogger.Log("报错信息：查询接口查询订单报错,获取到的参数：" + json + ",报错信息：" + bcxx, summary: "接口错误信息");
                return null;
            }
            return mode;
        }

    }
}
