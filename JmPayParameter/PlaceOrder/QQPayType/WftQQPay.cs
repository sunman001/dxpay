using DxPay.LogManager.LogFactory.ApiLog;
using JmPayParameter.PayChannel;
using swiftpass.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace JmPayParameter.PlaceOrder.QQPayType
{
    /// <summary>
    /// 威富通QQ钱包Wap
    /// </summary>
    public class WftQQPay
    {
        /// <summary>
        /// 威富通QQ钱包wap支付接口主通道
        /// </summary>
        /// <param name="paymode">平台类型（1：安卓，2：ios，3：H5）</param>
        /// <param name="apptype">应用类型子id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格（单位元）</param>
        /// <param name="orderid">订单id</param>
        /// <param name="ip">ip地址</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        public InnerResponse WftQQPayInfo(int paymode, int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid)
        {

            InnerResponse inn = new InnerResponse();
            switch (paymode)
            {
                case 1://安卓方式
                    inn = WftQQAz(apptype, code, goodsname, price, orderid, ip, appid);
                    break;
                case 2://ios方式
                    inn = WftQQIOS(apptype, code, goodsname, price, orderid, ip, appid);
                    break;
                case 3://H5方式
                    inn = WftQQH5(apptype, code, goodsname, price, orderid, ip, appid);
                    break;
                default:
                    inn = inn.ToResponse(ErrorCode.Code9987);
                    break;
            }
            return inn;

        }
        /// <summary>
        /// 威富通QQ钱包wap支付
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额(单位：元)</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private static InnerResponse WftQQH5(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid)
        {
            // string str = "";
            InnerResponse inn = new InnerResponse();
            Dictionary<string, string> cfg = new Dictionary<string, string>();
            try
            {
                ClientResponseHandler resHandler = new ClientResponseHandler();
                PayHttpClient pay = new PayHttpClient();
                RequestHandler reqHandler = new RequestHandler(null);
                cfg = Utils.loadCfgQQ(apptype, appid);
                UpdateOrdes order = new UpdateOrdes();
                if (cfg == null || string.IsNullOrEmpty(cfg["mch_id"]) || string.IsNullOrEmpty(cfg["pay_id"]) || string.IsNullOrEmpty(cfg["key"]))
                {
                    inn = inn.ToResponse(ErrorCode.Code106);
                    return inn;
                }
                if (!order.OrdeUpdateInfo(orderid, int.Parse(cfg["pay_id"].ToString()), code))
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
                reqHandler.setParameter("total_fee", (Convert.ToInt32(price * 100)).ToString());//价格（已传入的为准，无就从数据库读取）
                reqHandler.setParameter("mch_create_ip", ip);//终端IP 
                reqHandler.setParameter("service", "pay.tenpay.native");//支付类型
                reqHandler.setParameter("mch_id", cfg["mch_id"].ToString());
                reqHandler.setParameter("version", cfg["version"].ToString());
                string notify_url = ConfigurationManager.AppSettings["WftTokenUrl"].ToString().Replace("{0}", cfg["pay_id"].ToString());
                reqHandler.setParameter("notify_url", notify_url);//回掉地址
                reqHandler.setParameter("nonce_str", Utils.random());
                reqHandler.setParameter("charset", "UTF-8");
                reqHandler.setParameter("sign_type", "MD5");
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
                            inn = inn.ToResponse(ErrorCode.Code100);
                            inn.ExtraData = param["code_url"];//http提交方式;
                            inn.IsJump = true;

                        }
                        else
                        {
                            string wftzfsbxin = "威富通支付失败信息，错误代码：" + param["err_code"] + ",错误信息：" + param["err_msg"] + ",商户号：" + cfg["mch_id"].ToString();
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + wftzfsbxin, summary: "威富通QQ钱包Wap接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                            inn = inn.ToResponse(ErrorCode.Code104);
                        }
                    }
                    else
                    {
                        string mesage = "威富通支付失败信息，错误代码：" + resHandler.getContent() + ",错误信息：" + resHandler.getDebugInfo() + ",商户号：" + cfg["mch_id"].ToString();
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + mesage, summary: "威富通QQ钱包Wap接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                        inn = inn.ToResponse(ErrorCode.Code104);
                    }
                }
                else
                {
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：第一步验证错误", summary: "威富通QQ钱包Wap接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "威富通QQ钱包Wap接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }

        /// <summary>
        /// 威富通QQ钱包Wap支付ios
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额(单位：元)</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private static InnerResponse WftQQIOS(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid)
        {
            // string str = "";
            InnerResponse inn = new InnerResponse();
            Dictionary<string, string> cfg = new Dictionary<string, string>();
            try
            {
                ClientResponseHandler resHandler = new ClientResponseHandler();
                PayHttpClient pay = new PayHttpClient();
                RequestHandler reqHandler = new RequestHandler(null);
                cfg = Utils.loadCfgQQ(apptype, appid);

                UpdateOrdes order = new UpdateOrdes();

                if (cfg == null || string.IsNullOrEmpty(cfg["mch_id"]) || string.IsNullOrEmpty(cfg["pay_id"])|| string.IsNullOrEmpty(cfg["key"]))
                {
                    inn = inn.ToResponse(ErrorCode.Code106);
                    return inn;
                }
                if (!order.OrdeUpdateInfo(orderid, int.Parse(cfg["pay_id"].ToString()), code))
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
                reqHandler.setParameter("total_fee", (Convert.ToInt32(price * 100)).ToString());//价格（已传入的为准，无就从数据库读取）
                reqHandler.setParameter("mch_create_ip", ip);//终端IP 
                reqHandler.setParameter("service", "pay.tenpay.native");//支付类型
                reqHandler.setParameter("mch_id", cfg["mch_id"].ToString());
                reqHandler.setParameter("version", cfg["version"].ToString());
                reqHandler.setParameter("notify_url", ConfigurationManager.AppSettings["WftTokenUrl"].ToString().Replace("{0}", cfg["pay_id"].ToString()));//回掉地址
                reqHandler.setParameter("nonce_str", Utils.random());
                reqHandler.setParameter("charset", "UTF-8");
                reqHandler.setParameter("sign_type", "MD5");
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
                            string Alpay = "{\"PaymentType\":\"8\",\"SubType\":\"2\",\"IsH5\":\"1\",\"data\":\"" + param["code_url"] + "\"}";
                            inn = inn.ToResponse(ErrorCode.Code100);
                            inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(Alpay, ConfigurationManager.AppSettings["encryption"].ToString());

                        }
                        else
                        {
                            string wftzfsbxin = "威富通支付失败信息，错误代码：" + param["err_code"] + ",错误信息：" + param["err_msg"] + ",商户号：" + cfg["mch_id"].ToString();
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + wftzfsbxin, summary: "威富通QQ钱包Wap接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                            inn = inn.ToResponse(ErrorCode.Code104);
                        }
                    }
                    else
                    {
                        string mesage = "威富通支付失败信息，错误代码：" + resHandler.getContent() + ",错误信息：" + resHandler.getDebugInfo() + ",商户号：" + cfg["mch_id"].ToString();
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + mesage, summary: "威富通QQ钱包Wap接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                        inn = inn.ToResponse(ErrorCode.Code104);
                    }
                }
                else
                {

                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：第一步验证错误", summary: "威富通QQ钱包Wap接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "威富通QQ钱包Wap接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }

        /// <summary>
        /// 威富通QQ钱包Wap支付安卓
        /// </summary>
        /// <param name="tid">应用类型id</param>
        /// <param name="code">订单号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额(单位：元)</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private static InnerResponse WftQQAz(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid)
        {
            // string str = "";
            InnerResponse inn = new InnerResponse();
            Dictionary<string, string> cfg = new Dictionary<string, string>();
            try
            {
                ClientResponseHandler resHandler = new ClientResponseHandler();
                PayHttpClient pay = new PayHttpClient();
                RequestHandler reqHandler = new RequestHandler(null);
                cfg = Utils.loadCfgQQ(apptype, appid);

                UpdateOrdes order = new UpdateOrdes();

                if (cfg == null || string.IsNullOrEmpty(cfg["mch_id"]) || string.IsNullOrEmpty(cfg["pay_id"]) || string.IsNullOrEmpty(cfg["key"]))
                {
                    inn = inn.ToResponse(ErrorCode.Code106);
                    return inn;
                }
                if (!order.OrdeUpdateInfo(orderid, int.Parse(cfg["pay_id"].ToString()), code))
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
                reqHandler.setParameter("total_fee", (Convert.ToInt32(price * 100)).ToString());//价格（已传入的为准，无就从数据库读取）
                reqHandler.setParameter("mch_create_ip", ip);//终端IP 
                reqHandler.setParameter("service", "pay.tenpay.native");//支付类型
                reqHandler.setParameter("mch_id", cfg["mch_id"].ToString());
                reqHandler.setParameter("version", cfg["version"].ToString());
                reqHandler.setParameter("notify_url", ConfigurationManager.AppSettings["WftTokenUrl"].ToString().Replace("{0}", cfg["pay_id"].ToString()));//回掉地址
                reqHandler.setParameter("nonce_str", Utils.random());
                reqHandler.setParameter("charset", "UTF-8");
                reqHandler.setParameter("sign_type", "MD5");
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
                            string Alpay = "{\"PaymentType\":\"8\",\"SubType\":\"2\",\"IsH5\":\"1\",\"data\":\"" + param["code_url"] + "\"}";
                            inn = inn.ToResponse(ErrorCode.Code100);
                            inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(Alpay, ConfigurationManager.AppSettings["encryption"].ToString());
                        }
                        else
                        {
                            string wftzfsbxin = "威富通支付失败信息，错误代码：" + param["err_code"] + ",错误信息：" + param["err_msg"] + ",商户号：" + cfg["mch_id"].ToString();
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + wftzfsbxin, summary: "威富通QQ钱包Wap接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                            inn = inn.ToResponse(ErrorCode.Code104);
                        }
                    }
                    else
                    {
                        string mesage = "威富通支付失败信息，错误代码：" + resHandler.getContent() + ",错误信息：" + resHandler.getDebugInfo() + ",商户号：" + cfg["mch_id"].ToString();
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + mesage, summary: "威富通QQ钱包Wap接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                        inn = inn.ToResponse(ErrorCode.Code104);
                    }
                }
                else
                {
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：第一步验证错误", summary: "威富通QQ钱包Wap接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "威富通QQ钱包Wap接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }
    }
}
