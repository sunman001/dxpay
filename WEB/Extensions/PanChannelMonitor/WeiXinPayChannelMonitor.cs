using System;
using System.Collections.Generic;
using swiftpass.utils;

namespace WEB.Extensions.PanChannelMonitor
{
    public class WeiXinPayChannelMonitor : IPayChannelMonitor
    {
        private int _tid;
        private string _ipAddress = "";

        /// <summary>
        /// 通道ID
        /// </summary>
        public int Tid
        {
            get { return _tid; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentNullException("通道ID不正确");
                }
                _tid = value;
            }
        }


        public bool AllowCheck { get; set; }
        public bool AllowAutoCheck { get; set; }
        /// <summary>
        /// 客户端IP地址
        /// </summary>
        public string IpAddress
        {
            get { return _ipAddress; }
            set { _ipAddress = value; }
        }

        public bool Check()
        {
            var str = "";
            var bll = new JMP.BLL.jmp_order();
            var resHandler = new ClientResponseHandler();
            var pay = new PayHttpClient();
            var reqHandler = new RequestHandler(null);
            var cfg = Utils.Load_CfgInterfaceId(Tid);
            //初始化数据  
            reqHandler.setGateUrl(cfg["req_url"].ToString());
            reqHandler.setKey(cfg["key"].ToString());

            reqHandler.setParameter("out_trade_no", new Random().Next(111111, 999999).ToString());//我们的订单号
            reqHandler.setParameter("body", "会员商品");//商品描述
            reqHandler.setParameter("attach", "会员");//附加信息
            reqHandler.setParameter("total_fee", "100");//价格（已传入的为准，无就从数据库读取）

            reqHandler.setParameter("mch_create_ip", "127.0.0.1");//终端IP 
            reqHandler.setParameter("service", "pay.weixin.wappay");


            reqHandler.setParameter("mch_id", cfg["mch_id"].ToString());

            reqHandler.setParameter("version", cfg["version"].ToString());
            reqHandler.setParameter("notify_url", "http://baidu.com");//回掉地址
            reqHandler.setParameter("callback_url", "http://baidu.com");//同步回掉地址
            reqHandler.setParameter("nonce_str", Utils.random());
            reqHandler.setParameter("charset", "UTF-8");
            reqHandler.setParameter("sign_type", "MD5");
            reqHandler.createSign();
            var datawft = Utils.toXml(reqHandler.getAllParameters());
            var reqContent = new Dictionary<string, string>
            {
                {"url", reqHandler.getGateUrl()},
                { "data", datawft}
            };
            pay.setReqContent(reqContent);
            var success = false;
            if (pay.call())
            {
                resHandler.setContent(pay.getResContent());
                resHandler.setKey(cfg["key"].ToString());
                var param = resHandler.getAllParameters();
                if (resHandler.isTenpaySign())
                {
                    if (int.Parse(param["status"].ToString()) == 0)
                    {
                        try
                        {
                            var wxpay = param["pay_info"].ToString();
                            if (wxpay.Contains("http"))
                            {
                                success = true;
                            }
                        }
                        catch
                        {

                        }
                    }
                }
            }
            return success;

        }

        public bool checkorder()
        { return true; }
    }
}