using DxPay.LogManager.LogFactory.ApiLog;
using JmPayParameter.Models;
using JmPayParameter.PayChannel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JmPayParameter.PlaceOrder.WxPayType
{
    /// <summary>
    /// 中信银行wap接口
    /// </summary>
    public class ZxYhWxPay
    {
        /// <summary>
        /// 中信银行微信wap支付通道主入口
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
        public InnerResponse ZxYhWxPayInfo(int paymode, int apptype, string code, string goodsname, decimal price, int orderid, string ip, int infoTimes, int appid)
        {
            InnerResponse inn = new InnerResponse();
            switch (paymode)
            {
                case 1://安卓方式
                    inn = ZxYhWxWaPAz(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                case 2://ios方式
                    inn = ZxYhWxWaPIOS(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                case 3://H5支付方式
                    inn = ZxYhWxWaPH5(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                default:
                    inn = inn.ToResponse(ErrorCode.Code9987);
                    break;
            }
            return inn;
        }
        #region 中信银行微信wap支付
        /// <summary>
        /// 中信银行微信wap支付h5调用模式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse ZxYhWxWaPH5(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {
                string ZxYhWxWaPH5jkhc = "ZxYhWxWaPH5jkhc" + appid;//组装缓存key值

                SeIn = SelectUserInfo(ZxYhWxWaPH5jkhc, apptype, appid, infoTimes);
                if (SeIn == null || SeIn.PayId <= 0 || string.IsNullOrEmpty(SeIn.UserId) || string.IsNullOrEmpty(SeIn.UserKey))
                {
                    inn = inn.ToResponse(ErrorCode.Code106);
                    return inn;
                }
                if (!UpdateOrde.OrdeUpdateInfo(orderid, SeIn.PayId))
                {
                    inn = inn.ToResponse(ErrorCode.Code101);
                    return inn;
                }
                if (!JudgeMoney.JudgeMinimum(price, SeIn.minmun))
                {
                    inn = inn.ToResponse(ErrorCode.Code8990);
                    return inn;
                }
                if (!JudgeMoney.JudgeMaximum(price, SeIn.maximum))
                {
                    inn = inn.ToResponse(ErrorCode.Code8989);
                    return inn;
                }
                System.Collections.Specialized.NameValueCollection Palist = new System.Collections.Specialized.NameValueCollection();
                Palist.Add("version", "1");//版本号
                Palist.Add("agent_id", SeIn.UserId);//商户编号
                Palist.Add("agent_bill_id", code);//订单号
                Palist.Add("agent_bill_time", DateTime.Now.ToString("yyyyMMddHHmmss"));//提交订单时间
                Palist.Add("pay_amt", price.ToString());//支付金额（单位：元）
                Palist.Add("notify_url", ConfigurationManager.AppSettings["HyNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//异步通知地址
                Palist.Add("return_url", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", orderid.ToString()));//同步通知地址
                Palist.Add("user_ip", HttpContext.Current.Request.UserHostAddress.Replace('.', '_'));//ip地址
                Palist.Add("pay_type", "30");//支付类型
                Palist.Add("goods_name", HttpUtility.UrlEncode(goodsname, Encoding.GetEncoding("gb2312")));//商品名称
                Palist.Add("bank_card_type", "1");//银行类型
                Palist.Add("scene", "h5");
                Palist.Add("remark", SeIn.UserIdZ);//自定义参数
                string meta_option = "{\"s\":\"WAP\",\"n\":\"测试\",\"id\":\"http://www.baidu.com\"}";
                Palist.Add("meta_option", Convert.ToBase64String(System.Text.Encoding.GetEncoding("GBK").GetBytes(meta_option.Trim())));
                string md5sing = "version=" + Palist["version"] + "&agent_id=" + Palist["agent_id"] + "&agent_bill_id=" + Palist["agent_bill_id"] + "&agent_bill_time=" + Palist["agent_bill_time"] + "&pay_type=30&pay_amt=" + Palist["pay_amt"] + "&notify_url=" + Palist["notify_url"] + "&return_url=" + Palist["return_url"] + "&user_ip=" + Palist["user_ip"] + "&bank_card_type=" + Palist["bank_card_type"] + "&remark=" + Palist["remark"] + "&key=" + SeIn.UserKey;
                string md5str = JMP.TOOL.MD5.md5strGet(md5sing, true).ToLower();
                Palist.Add("sign", md5str);//签名
                string url = ConfigurationManager.AppSettings["ZxYhPostUrl"].ToString();//请求地址


                string strurl = url + "?" + JMP.TOOL.UrlStr.GetStrNV(Palist);

                inn = inn.ToResponse(ErrorCode.Code100);
                inn.ExtraData = strurl;//http提交方式;
                inn.IsJump = true;
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "中信银行微信wap接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }
        /// <summary>
        /// 中信银行微信wap支付安卓调用模式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse ZxYhWxWaPAz(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {
                string ZxYhWxWaPAzjkhc = "ZxYhWxWaPAzjkhc" + appid;//组装缓存key值

                SeIn = SelectUserInfo(ZxYhWxWaPAzjkhc, apptype, appid, infoTimes);
                if (SeIn == null || SeIn.PayId <= 0 || string.IsNullOrEmpty(SeIn.UserId) || string.IsNullOrEmpty(SeIn.UserKey))
                {
                    inn = inn.ToResponse(ErrorCode.Code106);
                    return inn;
                }
                if (!UpdateOrde.OrdeUpdateInfo(orderid, SeIn.PayId))
                {
                    inn = inn.ToResponse(ErrorCode.Code101);
                    return inn;
                }
                if (!JudgeMoney.JudgeMinimum(price, SeIn.minmun))
                {
                    inn = inn.ToResponse(ErrorCode.Code8990);
                    return inn;
                }
                if (!JudgeMoney.JudgeMaximum(price, SeIn.maximum))
                {
                    inn = inn.ToResponse(ErrorCode.Code8989);
                    return inn;
                }
                System.Collections.Specialized.NameValueCollection Palist = new System.Collections.Specialized.NameValueCollection();
                Palist.Add("version", "1");//版本号
                Palist.Add("agent_id", SeIn.UserId);//商户编号
                Palist.Add("agent_bill_id", code);//订单号
                Palist.Add("agent_bill_time", DateTime.Now.ToString("yyyyMMddHHmmss"));//提交订单时间
                Palist.Add("pay_amt", price.ToString());//支付金额（单位：元）
                Palist.Add("notify_url", ConfigurationManager.AppSettings["HyNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//异步通知地址
                Palist.Add("return_url", "http://www.baidu.com");//同步通知地址
                Palist.Add("user_ip", HttpContext.Current.Request.UserHostAddress.Replace('.', '_'));//ip地址
                Palist.Add("pay_type", "30");//支付类型
                Palist.Add("goods_name", HttpUtility.UrlEncode(goodsname, Encoding.GetEncoding("gb2312")));//商品名称
                Palist.Add("bank_card_type", "1");//银行类型
                Palist.Add("scene", "h5");
                Palist.Add("remark", SeIn.UserIdZ);//自定义参数
                string meta_option = "{\"s\":\"Android\",\"n\":\"测试\",\"id\":\"测试\"}";
                Palist.Add("meta_option", Convert.ToBase64String(System.Text.Encoding.GetEncoding("GBK").GetBytes(meta_option.Trim())));
                string md5sing = "version=" + Palist["version"] + "&agent_id=" + Palist["agent_id"] + "&agent_bill_id=" + Palist["agent_bill_id"] + "&agent_bill_time=" + Palist["agent_bill_time"] + "&pay_type=30&pay_amt=" + Palist["pay_amt"] + "&notify_url=" + Palist["notify_url"] + "&return_url=" + Palist["return_url"] + "&user_ip=" + Palist["user_ip"] + "&bank_card_type=" + Palist["bank_card_type"] + "&remark=" + Palist["remark"] + "&key=" + SeIn.UserKey;
                string md5str = JMP.TOOL.MD5.md5strGet(md5sing, true).ToLower();
                Palist.Add("sign", md5str);//签名
                string url = ConfigurationManager.AppSettings["ZxYhPostUrl"].ToString();//请求地址
                string strurl = url + "?" + JMP.TOOL.UrlStr.GetStrNV(Palist);
                string wxpay = "{\"data\":\"" + strurl + "\",\"PaymentType\":\"2\",\"SubType\":\"4\",\"agent_id\":\"" + Palist["agent_id"] + "\",\"agent_bill_id\":\"" + Palist["agent_bill_id"] + "\",\"agent_bill_time\":\"" + Palist["agent_bill_time"] + "\",\"remark\":\"" + Palist["remark"] + "\",\"key\":\"" + SeIn.UserKey + "\",\"IsH5\":\"0\"}";
                inn = inn.ToResponse(ErrorCode.Code100);
                inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(wxpay, ConfigurationManager.AppSettings["encryption"].ToString());
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "中信银行微信wap接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }

        /// <summary>
        /// 中信银行微信wap支付苹果调用模式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse ZxYhWxWaPIOS(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {
                string ZxYhWxWaPIOSjkhc = "ZxYhWxWaPIOSjkhc" + appid;//组装缓存key值

                SeIn = SelectUserInfo(ZxYhWxWaPIOSjkhc, apptype, appid, infoTimes);
                if (SeIn == null || SeIn.PayId <= 0 || string.IsNullOrEmpty(SeIn.UserId) || string.IsNullOrEmpty(SeIn.UserKey))
                {
                    inn = inn.ToResponse(ErrorCode.Code106);
                    return inn;
                }
                if (!UpdateOrde.OrdeUpdateInfo(orderid, SeIn.PayId))
                {
                    inn = inn.ToResponse(ErrorCode.Code101);
                    return inn;
                }
                if (!JudgeMoney.JudgeMinimum(price, SeIn.minmun))
                {
                    inn = inn.ToResponse(ErrorCode.Code8990);
                    return inn;
                }
                if (!JudgeMoney.JudgeMaximum(price, SeIn.maximum))
                {
                    inn = inn.ToResponse(ErrorCode.Code8989);
                    return inn;
                }
                System.Collections.Specialized.NameValueCollection Palist = new System.Collections.Specialized.NameValueCollection();
                Palist.Add("version", "1");//版本号
                Palist.Add("agent_id", SeIn.UserId);//商户编号
                Palist.Add("agent_bill_id", code);//订单号
                Palist.Add("agent_bill_time", DateTime.Now.ToString("yyyyMMddHHmmss"));//提交订单时间
                Palist.Add("pay_amt", price.ToString());//支付金额（单位：元）
                Palist.Add("notify_url", ConfigurationManager.AppSettings["HyNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//异步通知地址
                Palist.Add("return_url", "http://www.baidu.com");//同步通知地址
                Palist.Add("user_ip", HttpContext.Current.Request.UserHostAddress.Replace('.', '_'));//ip地址
                Palist.Add("pay_type", "30");//支付类型
                Palist.Add("goods_name", HttpUtility.UrlEncode(goodsname, Encoding.GetEncoding("gb2312")));//商品名称
                Palist.Add("bank_card_type", "1");//银行类型
                Palist.Add("scene", "h5");
                Palist.Add("remark", SeIn.UserIdZ);//自定义参数
                string meta_option = "{\"s\":\"IOS\",\"n\":\"测试\",\"id\":\"com.jurtevfdb.rykueryeqrg\"}";
                Palist.Add("meta_option", Convert.ToBase64String(System.Text.Encoding.GetEncoding("GBK").GetBytes(meta_option.Trim())));
                string md5sing = "version=" + Palist["version"] + "&agent_id=" + Palist["agent_id"] + "&agent_bill_id=" + Palist["agent_bill_id"] + "&agent_bill_time=" + Palist["agent_bill_time"] + "&pay_type=30&pay_amt=" + Palist["pay_amt"] + "&notify_url=" + Palist["notify_url"] + "&return_url=" + Palist["return_url"] + "&user_ip=" + Palist["user_ip"] + "&bank_card_type=" + Palist["bank_card_type"] + "&remark=" + Palist["remark"] + "&key=" + SeIn.UserKey;
                string md5str = JMP.TOOL.MD5.md5strGet(md5sing, true).ToLower();
                Palist.Add("sign", md5str);//签名
                string url = ConfigurationManager.AppSettings["ZxYhPostUrl"].ToString();//请求地址
                string strurl = url + "?" + JMP.TOOL.UrlStr.GetStrNV(Palist);
                string wxpay = "{\"data\":\"" + strurl + "\",\"PaymentType\":\"2\",\"SubType\":\"4\",\"agent_id\":\"" + Palist["agent_id"] + "\",\"agent_bill_id\":\"" + Palist["agent_bill_id"] + "\",\"agent_bill_time\":\"" + Palist["agent_bill_time"] + "\",\"remark\":\"" + Palist["remark"] + "\",\"key\":\"" + SeIn.UserKey + "\",\"IsH5\":\"0\"}";
                inn = inn.ToResponse(ErrorCode.Code100);
                inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(wxpay, ConfigurationManager.AppSettings["encryption"].ToString());
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "中信银行微信wap接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }
        /// <summary>
        /// 获取中信银行网账号信息
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
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的中信银行微信wap支付账号
                        SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的中信银行微信wap支付key
                        SeIn.UserIdZ = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//获取中信银行子商户账号
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                    }
                    else
                    {

                        dt = bll.SelectPay("ZXYHWXWAP", apptype, appid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                            SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取中信银行微信wap支付账号
                            SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取中信银行微信wap支付key
                            SeIn.UserIdZ = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//获取中信银行子商户账号
                            SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                            SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                        }
                        else
                        {
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",获取缓存失败后，从数据库未查询到相关信息！", summary: "中信银行微信wap支付支付接口错误", channelId: SeIn.PayId);
                        }
                    }
                }
                else
                {
                    dt = bll.SelectPay("ZXYHWXWAP", apptype, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取中信银行微信wap支付账号
                        SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取中信银行微信wap支付key
                        SeIn.UserIdZ = paypz[2].Replace("\r", "").Replace("\n", "").Trim();//获取中信银行子商户账号
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                    }
                    else
                    {

                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",从数据库未查询到相关信息！", summary: "中信银行微信wap支付支付接口错误", channelId: SeIn.PayId);
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog(bcxx, summary: "中信银行微信wap支付支付接口错误应用类型ID：" + apptype, channelId: SeIn.PayId);
            }
            return SeIn;
        }


        #endregion
    }
}
