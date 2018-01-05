using DxPay.LogManager.LogFactory.ApiLog;
using JmPayParameter.Models;
using JmPayParameter.PayChannel;
using JmPayParameter.PayType;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JmPayParameter.PlaceOrder.WxPayType
{
    public class LmsjWxPay
    {
        /// <summary>
        /// 公众号转微信wap支付通道主入口
        /// </summary>
        /// <param name="paymode">平台类型（1：安卓，2：ios，3：H5）</param>
        /// <param name="apptype">应用类型子id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格（单位元）</param>
        /// <param name="orderid">订单id</param>
        /// <param name="ip">ip地址</param>
        /// <param name="infoTime">查询接口信息缓存时间</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        public InnerResponse LmsjWxPayInfo(int paymode, int apptype, string code, string goodsname, decimal price, int orderid, string ip, int infoTimes, int appid)
        {
            InnerResponse inn = new InnerResponse();

            if (paymode >= 1 && paymode <= 3)
            {
                inn = gzhwaph5(apptype, code, goodsname, price, orderid, ip, appid, infoTimes, paymode);
            }
            else
            {
                inn = inn.ToResponse(ErrorCode.Code9987);

            }
            return inn;
        }

        /// <summary>
        /// 公众号转微信wap支付接口H5模式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="ip">ip地址</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse gzhwaph5(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes, int paymode)
        {
            InnerResponse respon = new InnerResponse();
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {
                //查询应用是否开通微信公众号支付
                var payType = new PayWxGzh();
                var payc = payType.LoadChannel(paymode, apptype, infoTimes, appid);
                if (string.IsNullOrEmpty(payc.PassName))
                {
                    inn = inn.ToResponse(ErrorCode.Code106);
                    return inn;
                }
                System.Collections.Specialized.NameValueCollection Palist = new System.Collections.Specialized.NameValueCollection();

                string hckey = "wxgzhzwap" + appid;
                SeIn = SelectUserInfo(hckey, apptype, appid, infoTimes);
                JmPayParameter.JsonStr jsonStr = new JsonStr();
                PayBankModels modes = jsonStr.ParameterEntity(code, goodsname, price, "4", apptype, paymode);
                //h5模式
                respon = jsonStr.H5JsonStr(modes, ip);
                if (respon.ErrorCode == 100)
                {
                    Palist.Add("key", SeIn.UserKey);//key
                    Palist.Add("f", "json");//API返回的格式支持json和js
                    Palist.Add("url", respon.ExtraData.ToString());//要跳转的链接，先要经过urlencode编码
                    Palist.Add("b", "other");//浏览器 默认other 表示其他浏览器,  baidu则表示手机百度，androd_chrome表示android chrome浏览器
                    string urlstr = ConfigurationManager.AppSettings["gzhzwapUrl"].ToString();//公众号转wap请求地址
                    WebClient webClient = new WebClient();
                    byte[] responseData = webClient.UploadValues(urlstr, "POST", Palist);//得到返回字符流  
                    string srcString = Encoding.UTF8.GetString(responseData);//解码 
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
                    if (dic["status"].ToString() == "ok")
                    {
                        inn = inn.ToResponse(ErrorCode.Code100);
                        string ticket_url = dic["ticket_url"].ToString();
                        if (paymode == 3)
                        {
                            inn.ExtraData = ticket_url;
                            inn.IsJump = true;
                        }
                        else
                        {
                            string json = "{\"data\":\"" + ticket_url + "\",\"PaymentType\":\"2\",\"SubType\":\"6\",\"IsH5\":\"1\"}";
                            inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(json, ConfigurationManager.AppSettings["encryption"].ToString());
                        }
                    }
                    else
                    {
                        inn = inn.ToResponse(ErrorCode.Code104);
                        //转换微信链接失败
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + srcString, summary: "公众号转wap支付接口错误信息", channelId: SeIn.PayId);
                    }
                    return inn;
                }
                else
                {
                    return respon;
                }
            }
            catch (Exception ex)
            {
                inn = inn.ToResponse(ErrorCode.Code104);
                //转换微信链接失败
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + ex, summary: "公众号转wap支付接口错误信息", channelId: SeIn.PayId);
                return inn;
            }
        }



        /// <summary>
        /// 获取公众号转微信wap账号信息
        /// </summary>
        /// <param name="cache">缓存key</param>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private SelectInterface SelectUserInfo(string cache, int apptype, int appid, int infoTimes)
        {
            SelectInterface SeIn = new SelectInterface();
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
                        string l_str = dt.Rows[row]["l_str"].ToString();
                        SeIn.UserKey = l_str;//获取key
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                    }
                    else
                    {

                        dt = bll.SelectPay("GZHZWAP", apptype, appid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string l_str = dt.Rows[row]["l_str"].ToString();
                            SeIn.UserKey = l_str;//获取公众号转微信wap支付key
                            SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                        }
                        else
                        {
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",获取缓存失败后，从数据库未查询到相关信息！", summary: "公众号转微信wap支付支付接口错误", channelId: SeIn.PayId);
                        }
                    }
                }
                else
                {
                    dt = bll.SelectPay("GZHZWAP", apptype, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string l_str = dt.Rows[row]["l_str"].ToString();
                        SeIn.UserKey = l_str;//获取公众号转微信wap支付key
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                    }
                    else
                    {
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",从数据库未查询到相关信息！", summary: "公众号转微信wap支付支付接口错误", channelId: SeIn.PayId);
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog(bcxx, summary: "公众号转微信wap支付支付接口错误应用类型ID：" + apptype, channelId: SeIn.PayId);
            }
            return SeIn;
        }
    }
}
