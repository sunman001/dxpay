using JMP.TOOL;
using JmPayParameter.PayChannel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using DxPay.LogManager.LogFactory.ApiLog;

namespace JmPayParameter.PlaceOrder.WxAppType
{
    /// <summary>
    /// 兴业银行appid支付接口
    /// </summary>
    public class XyyhpayApp
    {
        /// <summary>
        /// 兴业银行微信appid支付接口支付通道主入口
        /// </summary>
        /// <param name="paymode">平台类型（1：安卓，2：ios，3：H5）</param>
        /// <param name="appid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格（单位元）</param>
        /// <param name="orderid">订单id</param>
        /// <param name="ip">ip地址</param>
        /// <param name="apptype">风控配置表id</param>
        /// <returns></returns>
        public InnerResponse XyyhpayAppPayInfo(int paymode, int appid, string code, string goodsname, decimal price, int orderid, string ip, int infoTimes, int apptype)
        {
            InnerResponse inn = new InnerResponse();
            switch (paymode)
            {
                case 1://安卓方式
                    inn = xyyhappidAz(appid, code, price, orderid, goodsname, ip, apptype, infoTimes);
                    break;
                case 2://ios方式
                    inn = xyyhappidIos(appid, code, price, orderid, goodsname, ip, apptype, infoTimes);
                    break;
                default:
                    inn = inn.ToResponse(ErrorCode.Code9987);
                    break;
            }
            return inn;
        }
        /// <summary>
        /// 兴业银行appid 安卓调用模式
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="apptype">风控配置表id</param>
        /// <returns></returns>
        private InnerResponse xyyhappidAz(int appid, string code, decimal price, int orderid, string goodsname, string ip, int apptype, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            UserInf us = new UserInf();
            try
            {
                string xyyhappidAz = "xyyhappidAz" + appid;//组装缓存key值

                us = SelectInfo(xyyhappidAz, apptype, appid, infoTimes);
                if (us == null || us.pay_id <= 0 || string.IsNullOrEmpty(us.userid) || string.IsNullOrEmpty(us.userkey))
                {
                    inn = inn.ToResponse(ErrorCode.Code106);
                    return inn;
                }
                if (!UpdateOrde.OrdeUpdateInfo(orderid, us.pay_id))
                {
                    inn = inn.ToResponse(ErrorCode.Code101);
                    return inn;
                }
                if (!JudgeMoney.JudgeMinimum(price, us.minmun))
                {
                    inn = inn.ToResponse(ErrorCode.Code8990);
                    return inn;
                }
                if (!JudgeMoney.JudgeMaximum(price, us.maximum))
                {
                    inn = inn.ToResponse(ErrorCode.Code8989);
                    return inn;
                }
                Dictionary<string, string> list = new Dictionary<string, string>();
                list.Add("version", "1.0.4");//版本号
                list.Add("device_type", "ANDROID");//操作系统
                list.Add("appid", us.userappid);//应用id
                list.Add("mch_id", us.userid);//商户号
                list.Add("wx_appid", us.wxappid);//微信appid
                                                 //string noncestr= Guid.NewGuid().ToString().Replace("-", "");
                list.Add("nonce_str", code);//随机字符串
                list.Add("body", goodsname);//商品描述
                list.Add("attach", "`store_appid=" + us.store_appid + "#store_name=" + us.store_name + "#op_user=");//附加数据
                list.Add("out_trade_no", code);//订单号
                list.Add("total_fee", (Convert.ToInt32(price * 100)).ToString());//支付金额（单位：分整数类型）
                list.Add("spbill_create_ip", ip);//ip地址
                                                 //list.Add("spbill_create_ip", "14.104.85.212");
                list.Add("notify_url", ConfigurationManager.AppSettings["xyyhappidNotifyUrl"].ToString().Replace("{0}", us.pay_id.ToString()));//异步通知地址
                list.Add("trade_type", "APP");//交易类型
                list.Add("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//交易开始时间
                list.Add("time_expire", DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss"));//交易失效时间
                string md5 = JMP.TOOL.UrlStr.AzGetStr(list) + "&key=" + us.userkey;
                string md5str = JMP.TOOL.MD5.md5strGet(md5, true);
                list.Add("sign", md5str);//签名           
                string xml = JMP.TOOL.xmlhelper.ToXml(list);
                string url = ConfigurationManager.AppSettings["xyyhappidPOSTUrl"].ToString();
                string srcString = JMP.TOOL.postxmlhelper.postxml(url, xml);
                Dictionary<string, object> lisjg = JMP.TOOL.xmlhelper.FromXml(srcString);
                if (lisjg.Count > 0 && lisjg["return_code"].ToString() == "SUCCESS")
                {
                    string wxstr = "{\"PaymentType\":\"5\",\"SubType\":\"4\",\"appid\":\"" + lisjg["wx_appid"] + "\",\"partnerid\":\"" + lisjg["req_partnerid"] + "\",\"prepayid\":\"" + lisjg["prepay_id"] + "\",\"pkg\":\"Sign=WXPay\",\"noncestr\":\"" + lisjg["nonce_str"] + "\",\"timestamp\":\"" + lisjg["req_timestamp"] + "\",\"sign\":\"" + lisjg["req_sign"] + "\",\"IsH5\":\"0\"}";
                    // str = "{\"message\":\"成功\",\"result\":100,\"data\":" + wxstr + "}";
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(wxstr, ConfigurationManager.AppSettings["encryption"].ToString());
                }
                else
                {

                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("支付接口异常,返回参数：" + srcString, summary: "兴业银行appid支付接口错误", channelId: us.pay_id);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "兴业银行appid接口错误信息", channelId: us.pay_id);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }

        /// <summary>
        /// 兴业银行appid ios调用模式
        /// </summary>
        /// <param name="appid">应用id</param>
        /// <param name="code">订单编号</param>
        /// <param name="price">支付金额</param>
        /// <param name="oid">订单id</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="apptype">风控配置表id</param>
        /// <returns></returns>
        private InnerResponse xyyhappidIos(int appid, string code, decimal price, int orderid, string goodsname, string ip, int apptype, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            UserInf us = new UserInf();
            try
            {
                string xyyhappidIos = "xyyhappidIos" + appid;//组装缓存key值

                us = SelectInfo(xyyhappidIos, apptype,appid, infoTimes);
                if (us == null || us.pay_id <= 0 || string.IsNullOrEmpty(us.userid) || string.IsNullOrEmpty(us.userkey))
                {
                    inn = inn.ToResponse(ErrorCode.Code106);
                    return inn;
                }
                if (!UpdateOrde.OrdeUpdateInfo(orderid, us.pay_id))
                {
                    inn = inn.ToResponse(ErrorCode.Code101);
                    return inn;
                }

                if (!JudgeMoney.JudgeMinimum(price, us.minmun))
                {
                    inn = inn.ToResponse(ErrorCode.Code8990);
                    return inn;
                }
                if (!JudgeMoney.JudgeMaximum(price, us.maximum))
                {
                    inn = inn.ToResponse(ErrorCode.Code8989);
                    return inn;
                }
                Dictionary<string, string> list = new Dictionary<string, string>();
                list.Add("version", "1.0.4");//版本号
                list.Add("device_type", "ANDROID");//操作系统
                list.Add("appid", us.userappid);//应用id
                list.Add("mch_id", us.userid);//商户号
                list.Add("wx_appid", us.wxappid);//微信appid
                list.Add("nonce_str", code);//随机字符串
                list.Add("body", goodsname);//商品描述
                list.Add("attach", "`store_appid=" + us.store_appid + "#store_name=" + us.store_name + "#op_user=");//附加数据
                list.Add("out_trade_no", code);//订单号
                list.Add("total_fee", (Convert.ToInt32(price * 100)).ToString());//支付金额（单位：分整数类型）
                list.Add("spbill_create_ip", ip);//ip地址
                                                 // list.Add("spbill_create_ip", "14.104.85.212");
                list.Add("notify_url", ConfigurationManager.AppSettings["xyyhappidNotifyUrl"].ToString().Replace("{0}", us.pay_id.ToString()));//异步通知地址
                list.Add("trade_type", "APP");//交易类型
                list.Add("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//交易开始时间
                list.Add("time_expire", DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss"));//交易失效时间
                string md5 = JMP.TOOL.UrlStr.AzGetStr(list) + "&key=" + us.userkey;
                string md5str = JMP.TOOL.MD5.md5strGet(md5, true);
                list.Add("sign", md5str);//签名           
                string xml = JMP.TOOL.xmlhelper.ToXml(list);
                string url = ConfigurationManager.AppSettings["xyyhappidPOSTUrl"].ToString();
                string srcString = JMP.TOOL.postxmlhelper.postxml(url, xml);
                Dictionary<string, object> lisjg = JMP.TOOL.xmlhelper.FromXml(srcString);
                if (lisjg.Count > 0 && lisjg["return_code"].ToString() == "SUCCESS")
                {
                    string codes = JMP.TOOL.Encrypt.IndexEncrypt(code);
                    string wxstr = "{\"PaymentType\":\"5\",\"SubType\":\"4\",\"paytype\":\"4\",\"appid\":\"" + lisjg["wx_appid"] + "\",\"partnerid\":\"" + lisjg["req_partnerid"] + "\",\"prepayid\":\"" + lisjg["prepay_id"] + "\",\"pkg\":\"Sign=WXPay\",\"noncestr\":\"" + lisjg["nonce_str"] + "\",\"timestamp\":\"" + lisjg["req_timestamp"] + "\",\"sign\":\"" + lisjg["req_sign"] + "\",\"code\":\"" + codes + "\",\"IsH5\":\"0\"}";
                    //str = "{\"message\":\"成功\",\"result\":100,\"data\":" + wxstr + "}";
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(wxstr, ConfigurationManager.AppSettings["encryption"].ToString());
                }
                else
                {

                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("支付接口异常,返回参数：" + srcString, summary: "兴业银行appid支付接口错误", channelId: us.pay_id);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "兴业银行appid接口错误信息", channelId: us.pay_id);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }

        /// <summary>
        /// 查询支付通道信息
        /// </summary>
        /// <param name="cache">缓存值</param>
        /// <param name="appid">应用id</param>
        /// <param name="apptype">风控配置表id</param>
        /// <returns></returns>
        private UserInf SelectInfo(string cache, int apptype,int appid, int infoTimes)
        {
            UserInf us = new UserInf();
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
                        us.userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();
                        us.userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();
                        us.wxappid = paypz[2].Replace("\r", "").Replace("\n", "").Trim();
                        us.userappid = paypz[3].Replace("\r", "").Replace("\n", "").Trim();
                        us.store_appid = paypz[4].Replace("\r", "").Replace("\n", "").Trim();
                        us.store_name = paypz[5].Replace("\r", "").Replace("\n", "").Trim();
                        us.pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        us.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        us.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                    }
                    else
                    {
                        dt = bll.SelectPay("xyyhappid", apptype,appid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                            us.userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();
                            us.userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();
                            us.wxappid = paypz[2].Replace("\r", "").Replace("\n", "").Trim();
                            us.userappid = paypz[3].Replace("\r", "").Replace("\n", "").Trim();
                            us.store_appid = paypz[4].Replace("\r", "").Replace("\n", "").Trim();
                            us.store_name = paypz[5].Replace("\r", "").Replace("\n", "").Trim();
                            us.pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                            us.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            us.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                        }
                        else
                        {
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id：" + apptype + ",获取缓存失败后，从数据库未查询到相关信息！", summary: "兴业银行appid支付接口错误", channelId: us.pay_id);
                        }
                    }
                }
                else
                {
                    dt = bll.SelectPay("xyyhappid", apptype, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        us.userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();
                        us.userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();
                        us.wxappid = paypz[2].Replace("\r", "").Replace("\n", "").Trim();
                        us.userappid = paypz[3].Replace("\r", "").Replace("\n", "").Trim();
                        us.store_appid = paypz[4].Replace("\r", "").Replace("\n", "").Trim();
                        us.store_name = paypz[5].Replace("\r", "").Replace("\n", "").Trim();
                        us.pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                        us.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        us.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                    }
                    else
                    {
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id：" + apptype + ",从数据库未查询到相关信息！", summary: "兴业银行appid支付接口错误", channelId: us.pay_id);
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog(bcxx, summary: "兴业银行appid支付接口错误应用ID：" + appid, channelId: us.pay_id);

            }
            return us;
        }



    }
    /// <summary>
    /// 封装实体
    /// </summary>
    public class UserInf
    {
        /// <summary>
        /// 兴业银行appid商户号
        /// </summary>
        internal string userid { get; set; }
        /// <summary>
        /// 兴业银行appid key
        /// </summary>
        internal string userkey { get; set; }
        /// <summary>
        /// 兴业银行应用id
        /// </summary>
        internal string userappid { get; set; }
        /// <summary>
        /// 微信appid
        /// </summary>
        internal string wxappid { get; set; }
        /// <summary>
        ///门店id 
        /// </summary>
        internal string store_appid { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        internal string store_name { get; set; }
        /// <summary>
        /// 支付渠道id
        /// </summary>
        internal int pay_id { get; set; }
        /// <summary>
        /// 单笔最小支付金额
        /// </summary>
        internal decimal minmun { get; set; }
        /// <summary>
        /// 单笔最大支付金额
        /// </summary>
        internal decimal maximum { get; set; }
    }

}
