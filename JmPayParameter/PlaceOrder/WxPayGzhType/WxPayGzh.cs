using DxPay.LogManager.LogFactory.ApiLog;
using JmPayParameter.Models;
using JmPayParameter.PayChannel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WxPayAPI;

namespace JmPayParameter.PlaceOrder.WxPayGzhType
{
    /// <summary>
    /// 微信官方微信公众号
    /// </summary>
    public class WxPayGzh
    {
        /// <summary>
        /// 微信公众号主通道
        /// </summary>
        /// <param name="paymode">平台类型（1：安卓，2：ios，3：H5）</param>
        /// <param name="apptype">应用类型子id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">价格</param>
        /// <param name="orderid">订单id</param>
        /// <param name="infoTime">缓存时间</param>
        /// <param name="appid">应用id</param>
        /// <param name="ip">ip地址</param>
        /// <returns></returns>
        public InnerResponse WxGzhPayInfo(int paymode, int apptype, string code, string goodsname, decimal price, int orderid, int infoTime, int appid, string ip)
        {
            InnerResponse inn = new InnerResponse();
            if (paymode == 3)
            {
                inn = WxGzhH5(apptype, code, price, orderid, goodsname, infoTime, appid, ip);
            }
            else
            {
                inn = inn.ToResponse(ErrorCode.Code9987);
            }
            return inn;
        }
        /// <summary>
        /// 微信公众号直接付接口
        /// </summary>
        /// <param name="apptype">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="price">商品价格</param>
        /// <param name="orderid">订单id</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="infoTime">缓存时间</param>
        /// <param name="appid">应用id</param>
        /// <param name="ip">ip地址</param>
        /// <returns></returns>
        private InnerResponse WxGzhH5(int apptype, string code, decimal price, int orderid, string goodsname, int infoTime, int appid, string ip)
        {

            InnerResponse inn = new InnerResponse();
            inn = inn.ToResponse(ErrorCode.Code100);
            //inn.ExtraData = ConfigurationManager.AppSettings["wftwxgzhget"].ToString() + "/WeChatNumber" + orderid + ".html";
            inn.ExtraData = ConfigurationManager.AppSettings["wftwxgzhget"].ToString() + "/WeChatNumber.aspx?pid=" + orderid;
            inn.IsJump = true;
            return inn;
        }
        #region 获取微信官方扫码支付账户信息
        /// <summary>
        /// 查询微信官方扫码支付账户信息
        /// </summary>
        /// <param name="cache">缓存值</param>
        /// <param name="appid">应用id</param>
        /// <param name="apptype">风控配置表id</param>
        /// <returns></returns>
        private SelectInterface SelectInfo(string cache, int appid, int apptype, int infoTimes)
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
                        SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取微信商户号
                        SeIn.wxappid = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取微信appid
                        SeIn.UserKey = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//获取微信秘钥
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());//获取支付通道id
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                    }
                    else
                    {
                        dt = bll.SelectPay("WXGFGZH", appid, apptype);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                            SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取微信商户号
                            SeIn.wxappid = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取微信appid
                            SeIn.UserKey = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//获取微信秘钥
                            SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());//获取支付通道id
                            SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                        }
                        else
                        {
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",获取缓存失败后，从数据库未查询到相关信息！", summary: "微信官方扫码支付接口错误", channelId: SeIn.PayId);
                        }
                    }
                }
                else
                {
                    dt = bll.SelectPay("WXGFGZH", appid, apptype);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取微信商户号
                        SeIn.wxappid = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取微信appid
                        SeIn.UserKey = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//获取微信秘钥
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());//获取支付通道id
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                    }
                    else
                    {
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",直接从数据库未查询到相关信息！", summary: "微信官方扫码支付接口错误", channelId: SeIn.PayId);
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog(bcxx, summary: "微信官方扫码支付接口错误应用ID：" + appid, channelId: SeIn.PayId);//写入报错日志
            }
            return SeIn;
        }
        #endregion
    }
}
