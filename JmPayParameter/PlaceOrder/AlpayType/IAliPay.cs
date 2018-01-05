using Alipay;
using JMP.TOOL;
using JmPayParameter.PayChannel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JmPayParameter.PlaceOrder
{
    /// <summary>
    /// 支付宝官网支付方式
    /// </summary>
    public class IAliPay
    {
        /// <summary>
        /// 支付宝接口主通道
        /// </summary>
        /// <param name="paymode">平台类型（1：安卓，2：ios，3：H5）</param>
        /// <param name="apptype">应用类型子id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格（单位元）</param>
        /// <param name="oderid">订单id</param>
        /// <param name="ip">ip地址</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        public InnerResponse AlpayInfo(int paymode, int apptype, string code, string goodsname, decimal price, int oderid, string ip, int appid)
        {
            InnerResponse inn = new InnerResponse();
            switch (paymode)
            {
                case 1://安卓方式
                    inn = PayZfbAz(apptype, code, goodsname, price, oderid, ip, appid);
                    break;
                case 2://ios方式
                    inn = PayZfbIos(apptype, code, goodsname, price, oderid, ip, appid);
                    break;
                case 3://H5支付方式
                    inn = PayZfbH5(apptype, code, goodsname, price, oderid, ip, appid);
                    break;
                default:
                    inn = inn.ToResponse(ErrorCode.Code9987);
                    break;
            }
            return inn;
        }

        #region 支付宝支付方式
        /// <summary>
        /// 支付宝支付通道安卓调用方式
        /// </summary>
        /// <param name="apptype">应用类型子id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">价格（单位元）</param>
        /// <param name="oderid">订单id</param>
        /// <param name="ip">ip地址</param>
        /// <returns></returns>
        private InnerResponse PayZfbAz(int apptype, string code, string goodsname, decimal price, int oderid, string ip, int appid)
        {
            InnerResponse inn = new InnerResponse();
            Config cfg = new Config(apptype, appid);
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
            if (!UpdateOrde.OrdeUpdateInfo(oderid, cfg.pay_id))
            {
                inn = inn.ToResponse(ErrorCode.Code101);
                return inn;
            }
            if (!JudgeMoney.JudgeMinimum(price, cfg.minmun))
            {
                inn = inn.ToResponse(ErrorCode.Code8990);
                return inn;
            }
            if (!JudgeMoney.JudgeMaximum(price, cfg.maximum))
            {
                inn = inn.ToResponse(ErrorCode.Code8989);
                return inn;
            }
            sParaTemp.Add("partner", cfg.partner);
            sParaTemp.Add("seller_id", cfg.seller_id);
            sParaTemp.Add("_input_charset", cfg.input_charset.ToLower());
            sParaTemp.Add("service", "mobile.securitypay.pay");
            sParaTemp.Add("payment_type", "1");
            sParaTemp.Add("notify_url", ConfigurationManager.AppSettings["TokenUrl"].ToString().Replace("{0}", cfg.pay_id.ToString()));//需要封装（接收回传地址）TokenUrl
            string overtime = (int.Parse(ConfigurationManager.AppSettings["overtime"].ToString()) / 60) + "m";
            sParaTemp.Add("it_b_pay", overtime);//订单超时时间
            sParaTemp.Add("out_trade_no", code);//我们的订单号
            sParaTemp.Add("subject", goodsname);//商品i名称（根据商品id查询商品名称）
            sParaTemp.Add("total_fee", price.ToString());//价格（已传入的为准，无就从数据库读取）
            sParaTemp.Add("body", goodsname);//商品名称（备注）
            string str = "{\"data\":\"" + new Alipay.Submit(apptype, appid).BuildRequest1(sParaTemp) + "\",\"PaymentType\":\"1\",\"SubType\":\"1\",\"IsH5\":\"0\"} ";
            inn = inn.ToResponse(ErrorCode.Code100);
            inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(str, ConfigurationManager.AppSettings["encryption"].ToString());
            return inn;
        }

        /// <summary>
        /// 支付宝支付通道ios调用方式
        /// </summary>
        /// <param name="apptype">应用类型子id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">价格（单位元）</param>
        /// <param name="oderid">订单id</param>
        /// <param name="ip">ip地址</param>
        /// <returns></returns>
        private InnerResponse PayZfbIos(int apptype, string code, string goodsname, decimal price, int oderid, string ip, int appid)
        {
            InnerResponse inn = new InnerResponse();
            Config cfg = new Config(apptype, appid);
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
            if (!UpdateOrde.OrdeUpdateInfo(oderid, cfg.pay_id))
            {
                inn = inn.ToResponse(ErrorCode.Code101);
                return inn;
            }
            if (!JudgeMoney.JudgeMinimum(price, cfg.minmun))
            {
                inn = inn.ToResponse(ErrorCode.Code8990);
                return inn;
            }
            if (!JudgeMoney.JudgeMaximum(price, cfg.maximum))
            {
                inn = inn.ToResponse(ErrorCode.Code8989);
                return inn;
            }
            sParaTemp.Add("partner", cfg.partner);
            sParaTemp.Add("seller_id", cfg.seller_id);
            sParaTemp.Add("_input_charset", cfg.input_charset.ToLower());
            sParaTemp.Add("service", "mobile.securitypay.pay");
            sParaTemp.Add("payment_type", "1");
            sParaTemp.Add("notify_url", ConfigurationManager.AppSettings["TokenUrl"].ToString().Replace("{0}", cfg.pay_id.ToString()));//需要封装（接收回传地址）TokenUrl
            string overtime = (int.Parse(ConfigurationManager.AppSettings["overtime"].ToString()) / 60) + "m";
            sParaTemp.Add("it_b_pay", overtime);//订单超时时间
            sParaTemp.Add("out_trade_no", code);//我们的订单号
            sParaTemp.Add("subject", goodsname);//商品i名称（根据商品id查询商品名称）
            sParaTemp.Add("total_fee", price.ToString());//价格（已传入的为准，无就从数据库读取）
            sParaTemp.Add("body", goodsname);//商品名称（备注）
            string str = "{\"data\":\"" + new Alipay.Submit(apptype, appid).BuildRequest1(sParaTemp) + "\",\"PaymentType\":\"1\",\"SubType\":\"1\",\"IsH5\":\"0\"} ";
            inn = inn.ToResponse(ErrorCode.Code100);
            inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(str, ConfigurationManager.AppSettings["encryption"].ToString());
            return inn;
        }
        /// <summary>
        /// 支付包支付通道H5调用方式
        /// </summary>
        /// <param name="apptype">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        /// <param name="TableName">订单表表名</param>
        /// <returns></returns>
        private InnerResponse PayZfbH5(int apptype, string code, string goodsname, decimal price, int oderid, string IP, int appid)
        {
            InnerResponse inn = new InnerResponse();
            JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
            Config cfg = new Config(apptype, appid);
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            if (!UpdateOrde.OrdeUpdateInfo(oderid, cfg.pay_id))
            {
                inn = inn.ToResponse(ErrorCode.Code101);
                return inn;
            }
            if (!JudgeMoney.JudgeMinimum(price, cfg.minmun))
            {
                inn = inn.ToResponse(ErrorCode.Code8990);
                return inn;
            }
            if (!JudgeMoney.JudgeMaximum(price, cfg.maximum))
            {
                inn = inn.ToResponse(ErrorCode.Code8989);
                return inn;
            }
            sParaTemp.Add("partner", cfg.partner);
            sParaTemp.Add("seller_id", cfg.seller_id);
            sParaTemp.Add("_input_charset", cfg.input_charset.ToLower());
            sParaTemp.Add("service", "alipay.wap.create.direct.pay.by.user");
            sParaTemp.Add("payment_type", "1");
            sParaTemp.Add("sign_type", "RSA");
            sParaTemp.Add("notify_url", ConfigurationManager.AppSettings["TokenUrl"].ToString().Replace("{0}", cfg.pay_id.ToString()));//需要封装TokenUrl(异步回调地址)
            sParaTemp.Add("return_url", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", oderid.ToString()));//同步支付成功界面跳转地址
            string overtime = (int.Parse(ConfigurationManager.AppSettings["overtime"].ToString()) / 60) + "m";
            sParaTemp.Add("it_b_pay", overtime);//订单超时时间
            sParaTemp.Add("out_trade_no", code);//我们的订单号
            sParaTemp.Add("subject", goodsname);//商品名称（根据商品id查询商品名称）
            sParaTemp.Add("total_fee", price.ToString());//价格（已传入的为准，无就从数据库读取）
            sParaTemp.Add("body", goodsname);//商品名称（备注）
            sParaTemp.Add("app_pay", "Y");//吊起app
            string httpurl = new Alipay.Submit(apptype, appid).BuildRequestHttp(sParaTemp);
            inn = inn.ToResponse(ErrorCode.Code100);
            inn.ExtraData = httpurl;//http提交方式;
            inn.IsJump = true;
            return inn;
        }
        #endregion
    }
}
