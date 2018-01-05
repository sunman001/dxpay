using System;
using System.Collections.Generic;
using Alipay;

namespace WEB.Extensions.PanChannelMonitor
{
    public class ZhiFuBaoPayChannelMonitor : IPayChannelMonitor
    {
        private int _tid;
        private string _aliPayForm;

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
        /// 支付宝支付HTML表单
        /// </summary>
        public string AliPayForm
        {
            get { return _aliPayForm; }
            private set { _aliPayForm = value; }
        }

        public bool Check()
        {
            var cfg = new ConfigMonitor(Tid);
            var sParaTemp = new SortedDictionary<string, string>
            {
                {"partner", cfg.partner},
                {"seller_id", cfg.seller_id},
                {"_input_charset", cfg.input_charset.ToLower()},
                {"service", "alipay.wap.create.direct.pay.by.user"},
                {"payment_type", "1"},
                {"sign_type", "RSA"},
                {"notify_url", "http://baidu.com"},
                {"return_url", "http://baidu.com"},
                {"it_b_pay", "30m"},
                {"out_trade_no", new Random().Next(111111, 999999).ToString()},
                {"subject", "会员商品"},
                {"total_fee", "0.01"},
                {"body", "会员商品"}
            };
            //需要封装TokenUrl(异步回调地址)
            //同步支付成功界面跳转地址
            //我们的订单号
            //商品名称（根据商品id查询商品名称）
            //价格（已传入的为准，无就从数据库读取）
            //商品名称（备注）
            var result = new Submit(Tid, cfg).BuildRequest(sParaTemp, "get", "OK");
            AliPayForm = result;
            return true;
            //var result = new Submit(Tid, cfg).BuildRequestParameters(sParaTemp);
            //throw new NotImplementedException("未实现支付宝通道状态判断");
            //return true;
        }

        public bool checkorder()
        {
            //获取最近10分钟支付宝订单
            var interBll = new JMP.BLL.jmp_interface();
            int sl = interBll.GetTodayOrderedInterfaces_byid(JMP.TOOL.WeekDateTime.GetCurrentOrderTableName);
            if (sl > 0)
                return true;
            else
                return false;

        }
    }
}