using DxPay.LogManager.LogFactory.ApiLog;
using JmPayParameter.PayChannel;
using Pay.DinPay;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace JmPayParameter.PlaceOrder.UnionPayType
{
    /// <summary>
    /// 智付银联支付
    /// </summary>
    public class ZfPay
    {
        /// <summary>
        /// 智付银联支付通道主入口
        /// </summary>
        /// <param name="paymode">平台类型（1：安卓，2：ios，3：H5）</param>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格（单位元）</param>
        /// <param name="orderid">订单id</param>
        /// <returns></returns>
        public  InnerResponse ZfPayInfo(int paymode, int apptype, string code, string goodsname, decimal price, int orderid, int appid)
        {
            InnerResponse inn = new InnerResponse();
            switch (paymode)
            {
                case 1://安卓方式
                    inn = PayZfAz(apptype, code, goodsname, price, orderid, appid);
                    break;
                case 2://ios方式
                    inn = PayZfIos(apptype, code, goodsname, price, orderid, appid);
                    break;
                case 3://H5支付方式
                    inn = PayZfH5(apptype, code, goodsname, price, orderid, appid);
                    break;
                default:
                    inn = inn.ToResponse(ErrorCode.Code9987);
                    break;
            }
            return inn;
        }
        #region 智付支付方式
        /// <summary>
        /// 智付支付通道安卓调用方式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        /// <param name="orderid">订单表id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private  InnerResponse PayZfAz(int apptype, string code, string goodsname, decimal price, int orderid, int appid)
        {
            InnerResponse inn = new InnerResponse();
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            Dictionary<string, string> DPcfg = DPConfing.loadCfg(apptype, appid);
            if (!UpdateOrde.OrdeUpdateInfo(orderid, Int32.Parse(DPcfg["pay_id"].ToString())))
            {
                inn = inn.ToResponse(ErrorCode.Code101);
                return inn;
            }
            //调用示例
            var formField = new FormField(
                             DPcfg["partner"].ToString(), //商家账号
                             code, //订单编号
                             price.ToString("f2"),//交易金额
                             ConfigurationManager.AppSettings["ZFTokenUrl"].ToString().Replace("{0}", DPcfg["pay_id"].ToString()), //通知地址
                             "RSA-S",//签名方式
                             DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), //订单时间
                            goodsname,//商品描述
                            new FormProperty("")//表单属性对象
                            );
            //商家私钥
            string merchantPrivateKey = DPcfg["dpkey"].ToString();
            //实例化HTML构造器
            var htmlCreator = new HtmlCreator(formField, new PrivateKeySignRsas(merchantPrivateKey));
            //生成表单字符串
            var htmlForm = htmlCreator.CreateHtmlAz();
            //str = "{\"message\":\"成功\",\"result\":100,\"data\":{\"union\":" + htmlForm + "}}";
            inn = inn.ToResponse(ErrorCode.Code100);
            string data = "{" + htmlForm.ToString().Replace("{", "").Replace("}", "") + ",\"PaymentType\":\"3\",\"SubType\":\"1\",\"IsH5\":\"0\"}";
            inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(data, ConfigurationManager.AppSettings["encryption"].ToString());
            return inn;
        }
        /// <summary>
        /// 智付支付通道苹果调用方式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        /// <param name="TableName">订单表表名</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private  InnerResponse PayZfIos(int apptype, string code, string goodsname, decimal price, int orderid, int appid)
        {
            InnerResponse inn = new InnerResponse();
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            Dictionary<string, string> DPcfg = DPConfing.loadCfg(apptype, appid);
            if (!UpdateOrde.OrdeUpdateInfo(orderid, Int32.Parse(DPcfg["pay_id"].ToString())))
            {
                inn = inn.ToResponse(ErrorCode.Code101);
                return inn;
            }
            //调用示例
            var formField = new FormField(
                             DPcfg["partner"].ToString(), //商家账号
                             code, //订单编号
                             price.ToString("f2"),//交易金额
                             ConfigurationManager.AppSettings["ZFTokenUrl"].ToString().Replace("{0}", DPcfg["pay_id"].ToString()), //通知地址
                             "RSA-S",//签名方式
                             DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), //订单时间
                            goodsname,//商品描述
                            new FormProperty("")//表单属性对象
                            );
            //商家私钥
            string merchantPrivateKey = DPcfg["dpkey"].ToString();
            //实例化HTML构造器
            var htmlCreator = new HtmlCreator(formField, new PrivateKeySignRsas(merchantPrivateKey));
            //生成表单字符串
            var htmlForm = htmlCreator.CreateHtmlAz();
            inn = inn.ToResponse(ErrorCode.Code100);
            string data = "{" + htmlForm.ToString().Replace("{", "").Replace("}", "") + ",\"PaymentType\":\"3\",\"SubType\":\"1\",\"IsH5\":\"0\"}";
            inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(data, ConfigurationManager.AppSettings["encryption"].ToString());
            return inn;
        }
        /// <summary>
        /// 智付支付通道H5调用方式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格</param>
        /// <param name="orderid">订单表表名</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private  InnerResponse PayZfH5(int apptype, string code, string goodsname, decimal price, int orderid, int appid)
        {
            InnerResponse inn = new InnerResponse();
            Dictionary<string, string> DPcfg = new Dictionary<string, string>();
            try
            {
                Dictionary<string, string> list = new Dictionary<string, string>();
                list.Add("UnionPay", "UnionPay");//特定表示
                list.Add("tid", apptype.ToString());//风控配置表id
                list.Add("code", code);//订单编号
                list.Add("goodsname", goodsname);//商品名称
                list.Add("price", price.ToString("f2"));//交易金额
                list.Add("oid", orderid.ToString());//订单id
                DPcfg = DPConfing.loadCfg(apptype, appid);
                if (!UpdateOrde.OrdeUpdateInfo(orderid, Int32.Parse(DPcfg["pay_id"].ToString())))
                {
                    inn = inn.ToResponse(ErrorCode.Code101);
                    return inn;
                }
                if (!JudgeMoney.JudgeMinimum(price, decimal.Parse(DPcfg["minmun"].ToString())))
                {
                    inn = inn.ToResponse(ErrorCode.Code8990);
                    return inn;
                }
                if (!JudgeMoney.JudgeMaximum(price, decimal.Parse(DPcfg["maximum"].ToString())))
                {
                    inn = inn.ToResponse(ErrorCode.Code8989);
                    return inn;
                }
                string tbtzurl = ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", orderid.ToString());
                //调用示例
                var formField = new FormField(
                                 DPcfg["partner"].ToString(), //商家账号
                                 list["code"].ToString(), //订单编号
                                 list["price"].ToString(),//交易金额
                                  "direct_pay", //服务类型
                                  "UTF-8",//编码格式
                                 ConfigurationManager.AppSettings["ZFTokenUrl"].ToString().Replace("{0}", DPcfg["pay_id"].ToString()), //通知地址
                                 "RSA-S",//签名方式
                                 DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), //订单时间
                                list["goodsname"].ToString(),//商品描述
                                new FormProperty("")//表单属性对象
                                )
                {
                    ReturnUrl = "" + tbtzurl + ""
                };//同步通知地址
                  //商家私钥
                string merchantPrivateKey = DPcfg["dpkey"].ToString();
                //实例化HTML构造器
                var htmlCreator = new HtmlCreator(formField, new PrivateKeySignRsas(merchantPrivateKey));
                //生成表单字符串
                var htmlForm = htmlCreator.CreateHtmlForm();
                string fromstr = JMP.TOOL.Encrypt.IndexEncrypt(htmlForm);
                string h5key = "h5zf" + code;
                string str = "";
                JMP.TOOL.CacheHelper.CacheObject(h5key, htmlForm, 1);
                str = JMP.TOOL.Encrypt.IndexEncrypt(h5key);
                str = ConfigurationManager.AppSettings["PostUrl"].ToString() + "?UnionPay=" + str;
                inn = inn.ToResponse(ErrorCode.Code100);
                inn.ExtraData = str;//http提交方式;
                inn.IsJump = true;
            }
            catch (Exception ex)
            {
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + ex.ToString(), summary: "智付银联接口错误信息", channelId: Int32.Parse(DPcfg["pay_id"].ToString()));
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }
        #endregion
    }
}
