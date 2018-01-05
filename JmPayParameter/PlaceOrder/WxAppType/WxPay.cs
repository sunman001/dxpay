using JMP.TOOL;
using JmPayParameter.PayChannel;
using System;
using System.Configuration;
using System.Data;
using DxPay.LogManager.LogFactory.ApiLog;
using WxPayAPI;
using JmPayParameter.Models;

namespace JmPayParameter.PlaceOrder.WxAppType
{
    public class WxPay
    {
        /// <summary>
        /// 微信appid支付通道主入口
        /// </summary>
        /// <param name="paymode">平台类型（1：安卓，2：ios，3：H5）</param>
        /// <param name="appid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格（单位元）</param>
        /// <param name="orderid">订单id</param>
        /// <param name="ip">ip地址</param>
        /// <param name="infoTime">查询接口信息缓存时间</param>
        /// <param name="apptype">风控配置表id</param>
        /// <returns></returns>
        public InnerResponse MyWxPayInfo(int paymode, int appid, string code, string goodsname, decimal price, int orderid, string ip, int infoTimes, int apptype)
        {
            InnerResponse inn = new InnerResponse();
            switch (paymode)
            {
                case 1://安卓方式
                    inn = PayWxAz(appid, code, goodsname, price, orderid, ip, apptype, infoTimes);
                    break;
                case 2://ios方式
                    inn = PayWxIos(appid, code, goodsname, price, orderid, ip, apptype, infoTimes);
                    break;
                default:
                    inn = inn.ToResponse(ErrorCode.Code9987);
                    break;
            }
            return inn;
        }
        #region 微信支付方式
        /// <summary>
        /// 微信支付通道安卓调用方式
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        /// <param name="orderid">订单id</param>
        /// <param name="apptype">风控配置表id</param>
        /// <returns></returns>
        private InnerResponse PayWxAz(int appid, string code, string goodsname, decimal price, int orderid, string ip, int apptype, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {
                string wxappidzfjk = "wxappidzfjk" + appid;//组装缓存key值

                SeIn = SelectUserInfo(wxappidzfjk, appid, apptype, infoTimes);
                if (SeIn == null || SeIn.PayId <= 0)
                {
                    inn = inn.ToResponse(ErrorCode.Code106);
                    return inn;
                }
                if (SeIn.PayId > 0)
                {
                    WxPayConfig wx = new WxPayConfig(SeIn.PayId);
                    JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
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
                    WxPayData data = new WxPayData();
                    data.SetValue("body", goodsname);//商品名称
                    data.SetValue("out_trade_no", code); //我们的订单号
                    data.SetValue("total_fee", (Convert.ToInt32(price * 100)).ToString());//价格
                    data.SetValue("notify_url", ConfigurationManager.AppSettings["WxTokenUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//回调地址
                    data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
                    data.SetValue("time_expire", DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss"));
                    data.SetValue("trade_type", "APP");
                    WxPayData result = WxPayApi.UnifiedOrder(data, SeIn.PayId);
                    string noncestr = WxPayApi.GenerateNonceStr();
                    string timestamp = WxPayApi.GenerateTimeStamp();
                    WxPayData data1 = new WxPayData();
                    data1.SetValue("appid", wx.APPID);
                    data1.SetValue("noncestr", noncestr);
                    data1.SetValue("package", "Sign=WXPay");
                    data1.SetValue("partnerid", wx.MCHID);
                    data1.SetValue("prepayid", result.GetValue("prepay_id"));
                    data1.SetValue("timestamp", timestamp);
                    string sign = data1.MakeSign(SeIn.PayId);
                    string wxstr = "{\"appid\":\"" + result.GetValue("appid") + "\",\"partnerid\":\"" + result.GetValue("mch_id") + "\",\"prepayid\":\"" + result.GetValue("prepay_id") + "\",\"pkg\":\"Sign=WXPay\",\"noncestr\":\"" + noncestr + "\",\"timestamp\":\"" + timestamp + "\",\"sign\":\"" + sign + "\",\"PaymentType\":\"5\",\"SubType\":\"1\",\"IsH5\":\"0\"}";
                    // str = "{\"message\":\"成功\",\"result\":100,\"data\":" + wxstr + "}";
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(wxstr, ConfigurationManager.AppSettings["encryption"].ToString());
                }
                else
                {

                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：支付通道异常", summary: "微信appid支付接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "微信appid支付接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }

        /// <summary>
        /// 微信支付通道苹果调用方式
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        /// <param name="orderid">订单id</param>
        /// <param name="apptype">风控配置表id</param>
        /// <returns></returns>
        private InnerResponse PayWxIos(int appid, string code, string goodsname, decimal price, int orderid, string ip, int apptype, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {
                string wxappidzfjkios = "wxappidzfjkios" + appid;//组装缓存key值

                SeIn = SelectUserInfo(wxappidzfjkios, appid, apptype, infoTimes);
                if (SeIn == null || SeIn.PayId <= 0)
                {
                    inn = inn.ToResponse(ErrorCode.Code106);
                    return inn;
                }
                if (SeIn.PayId > 0)
                {
                    WxPayConfig wx = new WxPayConfig(SeIn.PayId);
                    JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
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
                    WxPayData data = new WxPayData();
                    data.SetValue("body", goodsname);//商品名称
                    data.SetValue("out_trade_no", code); //我们的订单号
                    data.SetValue("total_fee", (Convert.ToInt32(price * 100)).ToString());//价格
                    data.SetValue("notify_url", ConfigurationManager.AppSettings["WxTokenUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//回调地址
                    data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
                    data.SetValue("time_expire", DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss"));
                    data.SetValue("trade_type", "APP");
                    WxPayData result = WxPayApi.UnifiedOrder(data, SeIn.PayId);
                    string noncestr = WxPayApi.GenerateNonceStr();
                    string timestamp = WxPayApi.GenerateTimeStamp();
                    WxPayData data1 = new WxPayData();
                    data1.SetValue("appid", wx.APPID);
                    data1.SetValue("noncestr", noncestr);
                    data1.SetValue("package", "Sign=WXPay");
                    data1.SetValue("partnerid", wx.MCHID);
                    data1.SetValue("prepayid", result.GetValue("prepay_id"));
                    data1.SetValue("timestamp", timestamp);
                    string sign = data1.MakeSign(SeIn.PayId);
                    string wxstr = "{\"appid\":\"" + result.GetValue("appid") + "\",\"partnerid\":\"" + result.GetValue("mch_id") + "\",\"prepayid\":\"" + result.GetValue("prepay_id") + "\",\"pkg\":\"Sign=WXPay\",\"noncestr\":\"" + noncestr + "\",\"timestamp\":\"" + timestamp + "\",\"sign\":\"" + sign + "\",\"PaymentType\":\"5\",\"SubType\":\"1\",\"IsH5\":\"0\"}";
                    //str = "{\"message\":\"成功\",\"result\":100,\"data\":" + wxstr + "}";
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(wxstr, ConfigurationManager.AppSettings["encryption"].ToString());
                }
                else
                {
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：支付通道异常", summary: "微信appid支付接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "微信appid支付接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }
        /// <summary>
        /// 根据应用id查询对应的支付通道
        /// </summary>
        /// <param name="cache">缓存名称</param>
        /// <param name="appid">应用id</param>
        /// <param name="apptype">风控配置表id</param>
        /// <returns></returns>
        private SelectInterface SelectUserInfo(string cache, int appid, int apptype, int infoTimes)
        {
            SelectInterface SeIn = new SelectInterface();
            DataTable dt = new DataTable();
            JMP.BLL.jmp_interface blls = new JMP.BLL.jmp_interface();
            if (JMP.TOOL.CacheHelper.IsCache(cache))
            {
                dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(cache);
                if (dt != null && dt.Rows.Count > 0)
                {
                    int row = new Random().Next(0, dt.Rows.Count);
                    SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                    SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                    SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                }
                else
                {
                    dt = blls.SelectPay("WX", apptype, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                    }
                    else
                    {
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",获取缓存失败后，从数据库未查询到相关信息！", summary: "微信支付接口错误", channelId: SeIn.PayId);
                    }
                }
            }
            else
            {
                dt = blls.SelectPay("WX", apptype, appid);
                if (dt != null && dt.Rows.Count > 0)
                {
                    int row = new Random().Next(0, dt.Rows.Count);
                    SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                    SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                    SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                    JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                }
                else
                {
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",从数据未查询到相关信息！", summary: "微信支付接口错误", channelId: SeIn.PayId);
                }
            }
            return SeIn;
        }
        #endregion
    }
}
