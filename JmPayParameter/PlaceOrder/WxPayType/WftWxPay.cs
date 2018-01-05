using JMP.TOOL;
using JmPayParameter.PayChannel;
using swiftpass.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using DxPay.LogManager.LogFactory.ApiLog;

namespace JmPayParameter.PlaceOrder.WxPayType
{
    /// <summary>
    /// 威富通微信wap支付通道
    /// </summary>
    public class WftWxPay
    {
        /// <summary>
        /// 威富通微信wap支付通道主入口
        /// </summary>
        /// <param name="paymode">平台类型（1：安卓，2：ios，3：H5）</param>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格（单位元）</param>
        /// <param name="oderid">订单id</param>
        /// <param name="ip">ip地址</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        public InnerResponse WftWxPayInfo(int paymode, int apptype, string code, string goodsname, decimal price, int oderid, string ip, int appid)
        {
            InnerResponse inn = new InnerResponse();
            switch (paymode)
            {
                case 1://安卓方式
                    inn = PayWftAz(apptype, code, goodsname, price, oderid, ip, appid);
                    break;
                case 2://ios方式
                    inn = PayWftIos(apptype, code, goodsname, price, oderid, ip, appid);
                    break;
                case 3://H5支付方式
                    inn = PayWftAzH5(apptype, code, goodsname, price, oderid, ip, appid);
                    break;
                default:
                    inn = inn.ToResponse(ErrorCode.Code9987);
                    break;
            }
            return inn;
        }


        #region 威富通支付方式
        /// <summary>
        /// 威富通支付通道安卓调用方式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        ///<param name="ooderid">订单表id</param>
        ///<param name="ip">ip地址</param>
        ///<param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse PayWftAz(int apptype, string code, string goodsname, decimal price, int oderid, string ip, int appid)
        {
            InnerResponse inn = new InnerResponse();
            Dictionary<string, string> cfg = new Dictionary<string, string>();
            try
            {
                ClientResponseHandler resHandler = new ClientResponseHandler();
                PayHttpClient pay = new PayHttpClient();
                RequestHandler reqHandler = new RequestHandler(null);
                cfg = Utils.loadCfg(apptype, appid);

                if (cfg == null || string.IsNullOrEmpty(cfg["mch_id"]) || string.IsNullOrEmpty(cfg["pay_id"]) || string.IsNullOrEmpty(cfg["key"]))
                {
                    inn = inn.ToResponse(ErrorCode.Code106);
                    return inn;
                }
                if (!UpdateOrde.OrdeUpdateInfo(oderid, int.Parse(cfg["pay_id"].ToString())))
                {
                    inn = inn.ToResponse(ErrorCode.Code101);
                    return inn;
                }
                if (!JudgeMoney.JudgeMinimum(price, decimal.Parse(cfg["minmun"].ToString())))
                {
                    inn = inn.ToResponse(ErrorCode.Code8990);
                    return inn;
                }
                if (!JudgeMoney.JudgeMaximum(price, decimal.Parse(cfg["maximum"].ToString())))
                {
                    inn = inn.ToResponse(ErrorCode.Code8989);
                    return inn;
                }
                //初始化数据  
                reqHandler.setGateUrl(cfg["req_url"].ToString());
                reqHandler.setKey(cfg["key"].ToString());
                reqHandler.setParameter("out_trade_no", code);//我们的订单号
                reqHandler.setParameter("body", goodsname);//商品描述
                                                           //reqHandler.setParameter("attach", privateinfo);//附加信息
                reqHandler.setParameter("total_fee", (Convert.ToInt32(price * 100)).ToString());//价格（已传入的为准，无就从数据库读取）
                reqHandler.setParameter("mch_create_ip", ip);//终端IP 
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

                            string wxpay = "{\"token_id\":\"" + param["token_id"].ToString() + "\", \"services\":\"pay.weixin.wappay\", \"sign\":\"" + param["sign"] + "\",\"status\":\"0\", \"charset\":\"UTF-8\", \"version\":\"2.0\", \"sign_type\":\"MD5\",\"PaymentType\":\"2\",\"SubType\":\"1\",\"IsH5\":\"0\"}";
                            // str = "{\"message\":\"成功\",\"result\":100,\"data\":" + wxpay + "}";
                            inn = inn.ToResponse(ErrorCode.Code100);
                            inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(wxpay, ConfigurationManager.AppSettings["encryption"].ToString());
                        }
                        else
                        {
                            string wftzfsbxin = "威富通支付失败信息，错误代码：" + param["err_code"] + ",错误信息：" + param["err_msg"] + ",商户号：" + cfg["mch_id"].ToString();
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + wftzfsbxin, summary: "威富通微信wap接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                            inn = inn.ToResponse(ErrorCode.Code104);
                        }
                    }
                    else
                    {
                        string mesage = "威富通支付失败信息，错误代码：" + resHandler.getContent() + ",错误信息：" + resHandler.getDebugInfo() + ",商户号：" + cfg["mch_id"].ToString();
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + mesage, summary: "威富通微信wap接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                        inn = inn.ToResponse(ErrorCode.Code104);
                    }
                }
                else
                {
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：第一步验证错误", summary: "威富通微信wap接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "威富通微信wap接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }
        /// <summary>
        /// 威富通支付通道IOS调用方式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        ///<param name="oderid">订单表ID</param>
        ///<param name="ip">ip地址</param>
        ///<param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse PayWftIos(int apptype, string code, string goodsname, decimal price, int oderid, string ip, int appid)
        {
            InnerResponse inn = new InnerResponse();
            Dictionary<string, string> cfg = new Dictionary<string, string>();
            try
            {
                ClientResponseHandler resHandler = new ClientResponseHandler();
                PayHttpClient pay = new PayHttpClient();
                RequestHandler reqHandler = new RequestHandler(null);
                cfg = Utils.loadCfg(apptype, appid);

                if (cfg == null || string.IsNullOrEmpty(cfg["mch_id"]) || string.IsNullOrEmpty(cfg["pay_id"]) || string.IsNullOrEmpty(cfg["key"]))
                {
                    inn = inn.ToResponse(ErrorCode.Code106);
                    return inn;
                }
                if (!UpdateOrde.OrdeUpdateInfo(oderid, int.Parse(cfg["pay_id"].ToString())))
                {
                    inn = inn.ToResponse(ErrorCode.Code101);
                    return inn;
                }
                if (!JudgeMoney.JudgeMinimum(price, decimal.Parse(cfg["minmun"].ToString())))
                {
                    inn = inn.ToResponse(ErrorCode.Code8990);
                    return inn;
                }
                if (!JudgeMoney.JudgeMaximum(price, decimal.Parse(cfg["maximum"].ToString())))
                {
                    inn = inn.ToResponse(ErrorCode.Code8989);
                    return inn;
                }
                //初始化数据  
                reqHandler.setGateUrl(cfg["req_url"].ToString());
                reqHandler.setKey(cfg["key"].ToString());
                reqHandler.setParameter("out_trade_no", code);//我们的订单号
                reqHandler.setParameter("body", goodsname);//商品描述
                                                           //reqHandler.setParameter("attach", privateinfo);//附加信息
                reqHandler.setParameter("total_fee", (Convert.ToInt32(price * 100)).ToString());//价格（已传入的为准，无就从数据库读取）
                reqHandler.setParameter("mch_create_ip", ip);//终端IP 
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

                            string wxpay = "{\"token_id\":\"" + param["token_id"].ToString() + "\", \"services\":\"pay.weixin.wappay\", \"sign\":\"" + param["sign"] + "\",\"status\":\"0\", \"charset\":\"UTF-8\", \"version\":\"2.0\", \"sign_type\":\"MD5\",\"PaymentType\":\"2\",\"SubType\":\"1\",\"IsH5\":\"0\"}";
                            //str = "{\"message\":\"成功\",\"result\":100,\"data\":" + wxpay + "}";
                            inn = inn.ToResponse(ErrorCode.Code100);
                            inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(wxpay, ConfigurationManager.AppSettings["encryption"].ToString());
                        }
                        else
                        {
                            string wftzfsbxin = "威富通支付失败信息，错误代码：" + param["err_code"] + ",错误信息：" + param["err_msg"] + ",商户号：" + cfg["mch_id"].ToString();
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + wftzfsbxin, summary: "威富通微信wap接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                            inn = inn.ToResponse(ErrorCode.Code104);
                        }
                    }
                    else
                    {
                        string mesage = "威富通支付失败信息，错误代码：" + resHandler.getContent() + ",错误信息：" + resHandler.getDebugInfo() + ",商户号：" + cfg["mch_id"].ToString();
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + mesage, summary: "威富通微信wap接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                        inn = inn.ToResponse(ErrorCode.Code104);
                    }
                }
                else
                {
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：第一步验证错误", summary: "威富通微信wap接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "威富通微信wap接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }
        /// <summary>
        /// 威富通支付通道H5调用方式
        /// </summary>
        /// <param name="tid">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        /// <param name="privateinfo">商品户私有信息</param>
        ///<param name="TableName">订单表表名</param>
        ///<param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse PayWftAzH5(int apptype, string code, string goodsname, decimal price, int oderid, string ip, int appid)
        {
            InnerResponse inn = new InnerResponse();
            Dictionary<string, string> cfg = new Dictionary<string, string>();
            try
            {
                ClientResponseHandler resHandler = new ClientResponseHandler();
                PayHttpClient pay = new PayHttpClient();
                RequestHandler reqHandler = new RequestHandler(null);
                cfg = Utils.loadCfg(apptype, appid);

                if (cfg == null || string.IsNullOrEmpty(cfg["mch_id"]) || string.IsNullOrEmpty(cfg["pay_id"]) || string.IsNullOrEmpty(cfg["key"]))
                {
                    inn = inn.ToResponse(ErrorCode.Code106);
                    return inn;
                }
                if (!UpdateOrde.OrdeUpdateInfo(oderid, int.Parse(cfg["pay_id"].ToString())))
                {
                    inn = inn.ToResponse(ErrorCode.Code101);
                    return inn;
                }
                if (!JudgeMoney.JudgeMinimum(price, decimal.Parse(cfg["minmun"].ToString())))
                {
                    inn = inn.ToResponse(ErrorCode.Code8990);
                    return inn;
                }
                if (!JudgeMoney.JudgeMaximum(price, decimal.Parse(cfg["maximum"].ToString())))
                {
                    inn = inn.ToResponse(ErrorCode.Code8989);
                    return inn;
                }
                //初始化数据  
                reqHandler.setGateUrl(cfg["req_url"].ToString());
                reqHandler.setKey(cfg["key"].ToString());
                reqHandler.setParameter("out_trade_no", code);//我们的订单号
                reqHandler.setParameter("body", goodsname);//商品描述
                                                           //reqHandler.setParameter("attach", privateinfo);//附加信息
                reqHandler.setParameter("total_fee", (Convert.ToInt32(price * 100)).ToString());//价格（已传入的为准，无就从数据库读取）
                reqHandler.setParameter("mch_create_ip", ip);//终端IP 
                reqHandler.setParameter("service", "pay.weixin.wappay");
                reqHandler.setParameter("mch_id", cfg["mch_id"].ToString());
                reqHandler.setParameter("version", cfg["version"].ToString());
                reqHandler.setParameter("notify_url", ConfigurationManager.AppSettings["WftTokenUrl"].ToString().Replace("{0}", cfg["pay_id"].ToString()));//回掉地址
                reqHandler.setParameter("callback_url", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", oderid.ToString()));//同步回掉地址
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
                        if (int.Parse(param["status"].ToString()) == 0 && int.Parse(param["result_code"].ToString()) == 0)
                        {
                            string wxpay = "";
                            try
                            {
                                wxpay = param["pay_info"].ToString();
                                inn = inn.ToResponse(ErrorCode.Code100);
                                inn.ExtraData = wxpay;//http提交方式;
                                inn.IsJump = true;
                                return inn;
                            }
                            catch
                            {
                                string wftzfsbxin = "威富通支付失败信息，错误代码：" + param["err_code"] + ",错误信息：" + param["err_msg"] + ",商户号：" + cfg["mch_id"].ToString();
                                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + wftzfsbxin, summary: "威富通微信wap接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                                inn = inn.ToResponse(ErrorCode.Code104);
                            }
                        }
                        else
                        {
                            string wftzfsbxin = "威富通支付失败信息，错误代码：" + param["err_code"] + ",错误信息：" + param["err_msg"] + ",商户号：" + cfg["mch_id"].ToString();
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + wftzfsbxin, summary: "威富通微信wap接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                            inn = inn.ToResponse(ErrorCode.Code104);
                        }
                    }
                    else
                    {
                        string mesage = "威富通支付失败信息，错误代码：" + resHandler.getContent() + ",错误信息：" + resHandler.getDebugInfo();
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + mesage, summary: "威富通微信wap接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                        inn = inn.ToResponse(ErrorCode.Code104);
                    }
                }
                else
                {
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：第一步验证错误", summary: "威富通微信wap接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "威富通微信wap接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }
        #endregion
    }
}
