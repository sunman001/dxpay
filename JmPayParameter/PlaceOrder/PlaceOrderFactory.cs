using JmPayParameter.PlaceOrder.AlPaySmType;
using JmPayParameter.PlaceOrder.AlpayType;
using JmPayParameter.PlaceOrder.UnionPayType;
using JmPayParameter.PlaceOrder.WxAppType;
using JmPayParameter.PlaceOrder.WxPayGzhType;
using JmPayParameter.PlaceOrder.WxPaySmType;
using JmPayParameter.PlaceOrder.WxPayType;
using JmPayParameter.PlaceOrder.QQPayType;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JmPayParameter.PlaceOrder
{
    /// <summary>
    /// 根据支付通道获取支付信息
    /// </summary>
    public class PlaceOrderFactory
    {
        /// <summary>
        /// 根据支付通道标示加载对应的支付通道 
        /// </summary>
        /// <param name="channel">支付通道标识</param>
        /// <param name="paymode">平台类型（1：安卓，2：ios，3：H5）</param>
        /// <param name="apptype">风控等级配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">价格（单位：元）</param> 
        /// <param name="orderid">订单id</param>
        /// <param name="ip">ip地址</param>
        /// <param name="appid">应用id</param> 
        /// <returns></returns>
        public InnerResponse Create(string channel, int paymode, int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid)
        {

            InnerResponse inn = new InnerResponse();
            //获取查询接口信息缓存时间
            int infoTime = int.Parse(ConfigurationManager.AppSettings["infoTime"].ToString());
            switch (channel)
            {
                case "ZFB"://支付宝官网支付接口
                    IAliPay aliPay = new IAliPay();
                    inn = aliPay.AlpayInfo(paymode, apptype, code, goodsname, price, orderid, ip, appid);
                    break;
                case "WFT"://威富通微信wap支付接口
                    WftWxPay wft = new WftWxPay();
                    inn = wft.WftWxPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, appid);
                    break;
                case "HYWX"://汇元微信wap支付接口
                    HyWxPay hywx = new HyWxPay();
                    inn = hywx.HyWxPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, infoTime, appid);
                    break;
                case "ZF"://智付银联支付接口
                    ZfPay zf = new ZfPay();
                    inn = zf.ZfPayInfo(paymode, apptype, code, goodsname, price, orderid, appid);
                    break;
                case "HYYL"://汇元银联
                    HyPay hyuin = new HyPay();
                    inn = hyuin.HyYlPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, infoTime, appid);
                    break;
                case "WFTGZH"://威富通微信公众号支付接口
                    WftWxGzhPay wftWxGzhPay = new WftWxGzhPay();
                    inn = wftWxGzhPay.WftWxGzhPayInfo(paymode, orderid);
                    break;
                case "NYGZH"://南粤银行微信公众号
                    NYGZH ny = new NYGZH();
                    inn = ny.NyWxGzhPayInfo(paymode, apptype, code, goodsname, price, orderid, infoTime, appid);
                    break;
                case "WX"://微信appid支付接口
                    WxPay wx = new WxPay();
                    inn = wx.MyWxPayInfo(paymode, appid, code, goodsname, price, orderid, ip, infoTime, apptype);
                    break;
                case "WFTAPP"://威富通微信appid支付接口
                    WftWxAppPay wftWxApp = new WftWxAppPay();
                    inn = wftWxApp.WftWxAppPayInfo(paymode, appid, code, goodsname, price, orderid, ip, apptype);
                    break;
                case "NYAPP"://南粤微信appid支付接口
                    NyPayApp nyapp = new NyPayApp();
                    inn = nyapp.NyWxAppPayInfo(paymode, appid, code, goodsname, price, orderid, ip, infoTime, apptype);
                    break;
                case "xyyhappid"://兴业银行微信appid支付接口
                    XyyhpayApp xyapp = new XyyhpayApp();
                    inn = xyapp.XyyhpayAppPayInfo(paymode, appid, code, goodsname, price, orderid, ip, infoTime, apptype);
                    break;
                case "WFTSM"://威富通微信扫码
                    WftWxSm wftWxSm = new WftWxSm();
                    inn = wftWxSm.WftWxsmPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, appid);
                    break;
                case "nywxsm"://南粤微信扫码
                    NyWxSmPay nyWxSmPay = new NyWxSmPay();
                    inn = nyWxSmPay.NyWxSmPayInfo(paymode, apptype, code, goodsname, price, orderid, infoTime, appid);
                    break;
                case "WFTZFBSM"://威富通支付宝扫码
                    WftAlPaySm wftAlPaySm = new WftAlPaySm();
                    inn = wftAlPaySm.WftPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, appid);
                    break;
                case "YLWXWAP"://优乐微信wap通道
                    YlWxPay yl = new YlWxPay();
                    inn = yl.ylWxPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, infoTime, appid);
                    break;
                case "HYZFBWAP"://汇元支付宝wap通道
                    HyZfbPay hyzfb = new HyZfbPay();
                    inn = hyzfb.HyZfbPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, infoTime, appid);
                    break;
                case "RMWXSM"://融梦微信扫码支付
                    RmWXSmPay rmWXSmPay = new RmWXSmPay();
                    inn = rmWXSmPay.RmWxsmPayInfo(paymode, apptype, code, goodsname, price, orderid, infoTime, appid);
                    break;
                case "RMZFBSM"://融梦支付宝扫码支付
                    RmAlPaySm rmAlPaySm = new RmAlPaySm();
                    inn = rmAlPaySm.RmZfbsmPayInfo(paymode, apptype, code, goodsname, price, orderid, infoTime, appid);
                    break;
                case "PFZFB"://浦发银行支付宝
                    PfAliPay pfal = new PfAliPay();
                    inn = pfal.PfZfbPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, infoTime, appid);
                    break;
                case "PFZFBSM"://浦发银行支付宝扫码
                    PfAlPaySm pfsm = new PfAlPaySm();
                    inn = pfsm.PfZfbSmPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, infoTime, appid);
                    break;
                case "PFWXSM"://浦发银行微信扫码
                    PfWxSmPay pfwxsm = new PfWxSmPay();
                    inn = pfwxsm.PfWXSmPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, infoTime, appid);
                    break;
                case "PFWXGZH"://浦发微信公众号
                    PfWxGzhPay pfWxGzhPay = new PfWxGzhPay();
                    inn = pfWxGzhPay.PfWxGzhPayInfo(paymode, orderid);
                    break;
                case "YLWXGZH"://优乐微信公众号
                    YlWxGzhPay ylWXGzh = new YlWxGzhPay();
                    inn = ylWXGzh.ylWxGzhPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, infoTime, appid);
                    break;
                case "ZXYHWXWAP"://中信银行微信wap
                    ZxYhWxPay zxyhwx = new ZxYhWxPay();
                    inn = zxyhwx.ZxYhWxPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, infoTime, appid);
                    break;
                case "TBWXWAP"://途贝微信H5
                    TbWxPay tbWxPay = new TbWxPay();
                    inn = tbWxPay.TbWxPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, infoTime, appid);
                    break;
                case "TBWXSM"://途贝微信扫码
                    TbWxSmPay tbWxSmPay = new TbWxSmPay();
                    inn = tbWxSmPay.TbWxSmPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, infoTime, appid);
                    break;
                case "TBZFBSM"://途贝支付宝扫码
                    TbAlPaySm tbAlSmPay = new TbAlPaySm();
                    inn = tbAlSmPay.TbZfbSmPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, infoTime, appid);
                    break;
                case "TBWXGZH"://途贝微信公众号
                    TbWxGzhPay tbWxGzhPay = new TbWxGzhPay();
                    inn = tbWxGzhPay.TbWxGzhPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, infoTime, appid);
                    break;
                case "WWWXWAP"://微唯网络微信支付
                    WwWxPay wwWxPay = new WwWxPay();
                    inn = wwWxPay.WwWxPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, infoTime, appid);
                    break;
                case "GZHZWAP"://公众号转微信wap支付
                    LmsjWxPay lmsjwxwap = new LmsjWxPay();
                    inn = lmsjwxwap.LmsjWxPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, infoTime, appid);
                    break;
                case "WFTZFB"://威富通支付宝wap
                    WftAlPay wftzfbPay = new WftAlPay();
                    inn = wftzfbPay.WftZFBPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, appid);
                    break;
                case "WPWXWAP"://微派微信wap
                    WpWxPay wpwxwap = new WpWxPay();
                    inn = wpwxwap.WpWxPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, infoTime, appid);
                    break;
                case "WPZFB"://微派支付宝wap
                    WpAliPay wpzfbwap = new WpAliPay();
                    inn = wpzfbwap.WpZFBPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, infoTime, appid);
                    break;
                case "XDZFB"://现在支付宝wap
                    XdAliPay xd = new XdAliPay();
                    inn = xd.XdZfbPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, infoTime, appid);
                    break;
                case "XXBZFB"://小小贝支付宝wap
                    XxbAliPay xxbzfbwap = new XxbAliPay();
                    inn = xxbzfbwap.XxbZFBPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, infoTime, appid);
                    break;
                case "ZQWXWAP"://掌趣微信wap
                    ZqWxPay zqwxwap = new ZqWxPay();
                    inn = zqwxwap.ZqWxPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, infoTime, appid);
                    break;
                case "HYQQWAP"://汇元QQ钱包wap
                    HyQQPay hyqqwap = new HyQQPay();
                    inn = hyqqwap.HyQQPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, infoTime, appid);
                    break;
                case "WFTQQWAP"://威富通QQ钱包Wap
                    WftQQPay wftqqwap = new WftQQPay();
                    inn = wftqqwap.WftQQPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, appid);
                    break;
                case "TBQQWAP"://途贝QQ钱包Wap
                    TbQQPay tbqqwap = new TbQQPay();
                    inn = tbqqwap.TbQQPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, infoTime, appid);
                    break;
                case "YLQQWAP":
                    YlQQPay ylqqwap = new YlQQPay();//优络QQ钱包wap
                    inn = ylqqwap.ylQQPayInfo(paymode, apptype, code, goodsname, price, orderid, ip, infoTime, appid);
                    break;
                case "WXGFSM"://微信官方扫码
                    WxSmType wxSmType = new WxSmType();
                    inn = wxSmType.WxSmPayInfo(paymode, apptype, code, goodsname, price, orderid, infoTime, appid, ip);
                    break;
                case "ZFBSM"://支付宝官方扫码
                    IAliPaySm plipaySm = new IAliPaySm();
                    inn = plipaySm.IAliPaySMInfo(paymode, apptype, code, goodsname, price, orderid, ip, infoTime, appid);
                    break;
                case "WXGFGZH"://微信官方公众号
                    WxPayGzh wxPayGzh = new WxPayGzh();
                    inn = wxPayGzh.WxGzhPayInfo(paymode, apptype, code, goodsname, price, orderid, infoTime, appid, ip);
                    break;
                case "WXService"://微信官方服务商扫码
                    WxServiceSmPay WxServiceSm = new WxServiceSmPay();
                    inn = WxServiceSm.WxServiceSmPayInfo(paymode, apptype, code, goodsname, price, orderid, infoTime, appid, ip);
                    break;
                case "ZFBSMPACK"://支付宝官方扫码封装wap
                    IAlPaySmPacking alPaySmPacking = new IAlPaySmPacking();
                    inn = alPaySmPacking.IAlPaySmPackingInfo(paymode, apptype, code, goodsname, price, orderid, ip, infoTime, appid);
                    break;
                default:
                    inn = inn.ToResponse(ErrorCode.Code106);
                    break;
            }
            return inn;
        }

    }
}
