using JMP.TOOL;
using JmPayParameter.PayChannel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxPay.LogManager.LogFactory.ApiLog;
using JmPayParameter.Models;
using System.Web;

namespace JmPayParameter.PlaceOrder.UnionPayType
{
    /// <summary>
    /// 汇元银联接口通道
    /// </summary>
    public class HyPay
    {

        /// <summary>
        /// 汇元银联支付通道主入口
        /// </summary>
        /// <param name="paymode">平台类型（1：安卓，2：ios，3：H5）</param>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格（单位元）</param>
        /// <param name="orderid">订单id</param>
        /// <param name="ip">ip地址</param>
        /// <param name="infoTime">查询接口信息缓存时间</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        public InnerResponse HyYlPayInfo(int paymode, int apptype, string code, string goodsname, decimal price, int orderid, string ip, int infoTimes, int appid)
        {
            InnerResponse inn = new InnerResponse();
            if (paymode == 3)
            {
                inn = HyYlPayH5(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
            }
            else
            {
                inn = inn.ToResponse(ErrorCode.Code9987);
            }
            return inn;
        }

        #region 汇元银联支付
        /// <summary>
        /// 汇元银联 支付h5调用模式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse HyYlPayH5(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            string HyYlPayH5jkhc = "HyYlPayH5jkhc" + appid;//组装缓存key值
            SelectInterface SeIn = new SelectInterface();
            SeIn = SelectUserInfo(HyYlPayH5jkhc, apptype, appid, infoTimes);
            if (SeIn == null || SeIn.PayId <= 0 || string.IsNullOrEmpty(SeIn.UserId) || string.IsNullOrEmpty(SeIn.UserKey))
            {
                inn = inn.ToResponse(ErrorCode.Code106);
                return inn;
            }
            if (!UpdateOrde.OrdeUpdateInfo(orderid, SeIn.PayId))
            {
                inn = inn.ToResponse(ErrorCode.Code101);
                return inn;
            }
            if (!JudgeMoney.JudgeMinimum(price, SeIn.minmun))
            {
                inn = inn.ToResponse(ErrorCode.Code8990);
                return inn;
            }
            if (!JudgeMoney.JudgeMaximum(price, SeIn.maximum))
            {
                inn = inn.ToResponse(ErrorCode.Code8989);
                return inn;
            }
            System.Collections.Specialized.NameValueCollection Palist = new System.Collections.Specialized.NameValueCollection();
            Palist.Add("version", "1");//版本号
            Palist.Add("agent_id", SeIn.UserId);//商户编号
            Palist.Add("agent_bill_id", code);//订单号
            Palist.Add("agent_bill_time", DateTime.Now.ToString("yyyyMMddHHmmss"));//提交订单时间
            Palist.Add("pay_amt", price.ToString());//支付金额（单位：元）
            Palist.Add("notify_url", ConfigurationManager.AppSettings["HyNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//异步通知地址
            Palist.Add("return_url", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", orderid.ToString()));//同步通知地址
            Palist.Add("user_ip", ip.Replace('.', '_'));//ip地址
            Palist.Add("pay_type", "0");//支付类型
            Palist.Add("goods_name", HttpUtility.UrlEncode(goodsname, Encoding.GetEncoding("gb2312")));//商品名称
            string md5sing = "version=" + Palist["version"] + "&agent_id=" + Palist["agent_id"] + "&agent_bill_id=" + Palist["agent_bill_id"] + "&agent_bill_time=" + Palist["agent_bill_time"] + "&pay_type=0&pay_amt=" + Palist["pay_amt"] + "&notify_url=" + Palist["notify_url"] + "&return_url=" + Palist["return_url"] + "&user_ip=" + Palist["user_ip"] + "&key=" + SeIn.UserKey;
            string md5str = JMP.TOOL.MD5.md5strGet(md5sing, true);
            Palist.Add("sign", md5str);//签名
            string url = ConfigurationManager.AppSettings["HyPOSTUrl"].ToString();//请求地址
            string strurl = url + "?" + JMP.TOOL.UrlStr.GetStrNV(Palist);
            inn = inn.ToResponse(ErrorCode.Code100);
            inn.ExtraData = strurl;//http提交方式;
            inn.IsJump = true;
            return inn;
        }
        /// <summary>
        /// 获取汇元网账号信息
        /// </summary>
        /// <param name="cache">缓存key</param>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private SelectInterface SelectUserInfo(string cache, int apptype, int appid, int infoTimes)
        {
            SelectInterface SeIn = new SelectInterface();
            try
            {
                DataTable dt = new DataTable();
                JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                if (JMP.TOOL.CacheHelper.IsCache(cache))
                {
                    dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(cache);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的汇元银联 支付账号
                        SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的汇元银联 支付key
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                    }
                    else
                    {
                        dt = bll.SelectPay("HYYL", apptype, appid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                            SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取汇元银联 支付账号
                            SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取汇元银联 支付key
                            SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                            SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                        }
                        else
                        {
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",获取缓存失败后，从数据库未查询到相关信息！", summary: "汇元银联 支付支付接口错误", channelId: SeIn.PayId);
                        }
                    }
                }
                else
                {
                    dt = bll.SelectPay("HYYL", apptype, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取汇元银联 支付账号
                        SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取汇元银联 支付key
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                    }
                    else
                    {
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",从数据库未直接查询到相关信息！", summary: "汇元银联 支付支付接口错误", channelId: SeIn.PayId);
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog(bcxx, summary: "汇元银联支付支付接口错误应用类型ID：" + apptype, channelId: SeIn.PayId);
            }
            return SeIn;
        }
        #endregion
    }
}
