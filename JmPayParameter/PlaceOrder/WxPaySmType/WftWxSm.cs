using JmPayParameter.PayChannel;
using swiftpass.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using DxPay.LogManager.LogFactory.ApiLog;

namespace JmPayParameter.PlaceOrder.WxPaySmType
{
    /// <summary>
    /// 威富通微信扫码支付接口
    /// </summary>
    public class WftWxSm
    {
        #region 威富通微信扫码支付
        /// <summary>
        /// 威富通微信扫码支付通道主入口
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
        public  InnerResponse WftWxsmPayInfo(int paymode, int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid)
        {
            InnerResponse inn = new InnerResponse();
            if (paymode == 3)
            {
                inn = WftWxSmH5(apptype, code, goodsname, price, orderid, ip, appid);
            }
            else
            {
                inn = inn.ToResponse(ErrorCode.Code9987);
            }
            return inn;
        }

        /// <summary>
        /// 威富通微信扫码支付
        /// </summary>
        /// <param name="apptype">应用类型id</param>
        /// <param name="code">订单号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private  InnerResponse WftWxSmH5(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid)
        {

            InnerResponse inn = new InnerResponse();
            Dictionary<string, string> cfg = new Dictionary<string, string>();
            try
            {
                ClientResponseHandler resHandler = new ClientResponseHandler();
                PayHttpClient pay = new PayHttpClient();
                RequestHandler reqHandler = new RequestHandler(null);
                cfg = Utils.loadCfgWxSm(apptype, appid);

                if (cfg == null || string.IsNullOrEmpty(cfg["mch_id"]) || string.IsNullOrEmpty(cfg["pay_id"]) || string.IsNullOrEmpty(cfg["key"]))
                {
                    inn = inn.ToResponse(ErrorCode.Code106);
                    return inn;
                }
                if (!UpdateOrde.OrdeUpdateInfo(orderid, int.Parse(cfg["pay_id"].ToString())))
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
                reqHandler.setParameter("service", "pay.weixin.native");//支付类型
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
                            string qurl = param["code_url"].ToString() + "," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ",2";//组装二维码地址
                            string ImgQRcode = ConfigurationManager.AppSettings["ImgQRcode"].ToString() + "?QRcode=" + JMP.TOOL.Encrypt.IndexEncrypt(qurl);//二维码图片展示地址
                            string codeurl = ConfigurationManager.AppSettings["QRcode"].ToString() + "?QRcode=" + JMP.TOOL.Encrypt.IndexEncrypt(qurl);//二维码展示地址
                            inn = inn.ToResponse(ErrorCode.Code100);
                            inn.ExtraData = new { ImgQRcode = ImgQRcode, codeurl = codeurl };//http提交方式;
                                                                                             //inn.IsJump = true;
                        }
                        else
                        {
                            string wftzfsbxin = "威富通支付失败信息，错误代码：" + param["err_code"] + ",错误信息：" + param["err_msg"] + ",商户号：" + cfg["mch_id"].ToString();
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + wftzfsbxin, summary: "威富通微信扫码接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                            inn = inn.ToResponse(ErrorCode.Code104);
                        }
                    }
                    else
                    {
                        string mesage = "威富通支付失败信息，错误代码：" + resHandler.getContent() + ",错误信息：" + resHandler.getDebugInfo() + ",商户号：" + cfg["mch_id"].ToString();
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + mesage, summary: "威富通微信扫码接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                        inn = inn.ToResponse(ErrorCode.Code104);
                    }
                }
                else
                {
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + "第一步验证错误", summary: "威富通微信扫码接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "威富通微信扫码接口错误信息", channelId: int.Parse(cfg["pay_id"].ToString()));
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }
        #endregion
    }
}
