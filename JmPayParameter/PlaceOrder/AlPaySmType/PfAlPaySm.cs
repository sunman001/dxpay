using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxPay.LogManager.LogFactory.ApiLog;
using JmPayParameter.PayChannel;
using System.Configuration;
using JmPayParameter.Models;

namespace JmPayParameter.PlaceOrder.AlPaySmType
{
    /// <summary>
    /// 浦发支付宝扫码通道入口
    /// </summary>
    public class PfAlPaySm
    {
        /// <summary>
        /// 浦发银行支付宝扫码支付通道主入口
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
        public InnerResponse PfZfbSmPayInfo(int paymode, int apptype, string code, string goodsname, decimal price, int orderid, string ip, int infoTimes, int appid)
        {
            InnerResponse inn = new InnerResponse();
            if (paymode == 3)
            {
                inn = PfZfbSmH5(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
            }
            else
            {
                inn = inn.ToResponse(ErrorCode.Code9987);
            }
            return inn;
        }
        /// <summary>
        /// 浦发银行支付宝扫码支付H5调用模式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse PfZfbSmH5(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {
                string PfZfbSmH5 = "PfZfbSmH5" + appid;//组装缓存key值

                SeIn = SelectUserInfo(PfZfbSmH5, apptype, appid, infoTimes);

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
                string xml = "<?xml version='1.0' encoding='utf-8' ?><ORDER_REQ><BUSI_ID>" + SeIn.UserId + "</BUSI_ID><OPER_ID>oper01</OPER_ID><DEV_ID>dev01</DEV_ID><AMT>" + price + "</AMT><CHANNEL_TYPE>1</CHANNEL_TYPE><PAY_SUBJECT>" + goodsname + "</PAY_SUBJECT ><CHARGE_CODE>" + code + "</CHARGE_CODE><NODIFY_URL>" + ConfigurationManager.AppSettings["pfalpayNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()) + "</NODIFY_URL><TIME_EXPIRE>" + ConfigurationManager.AppSettings["overtime"].ToString() + "</TIME_EXPIRE></ORDER_REQ>";
                string timestamp = JMP.TOOL.WeekDateTime.GetMilis;//时间戳
                string signstr = timestamp + SeIn.UserKey + xml.Replace(" ", "");
                string sign = JMP.TOOL.MD5.md5strGet(signstr, true).ToLower() + ":" + timestamp; ;
                string url = ConfigurationManager.AppSettings["pfalpayPostUrl"].ToString() + "?sign=" + sign + "&_type=json&busiCode=" + SeIn.UserId;
                string json = JMP.TOOL.postxmlhelper.postxml(url, xml);
                RootObject obj = new RootObject();
                obj = JMP.TOOL.JsonHelper.Deserializes<RootObject>(json);
                if (obj != null && obj.ORDER_RESP.RESULT.CODE == "SUCCESS" && json.Contains("BAR_CODE"))
                {
                    string qurl = obj.ORDER_RESP.BAR_CODE + "," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ",1";//组装二维码地址
                    string ImgQRcode = ConfigurationManager.AppSettings["ImgQRcode"].ToString() + "?QRcode=" + JMP.TOOL.Encrypt.IndexEncrypt(qurl);//二维码图片展示地址
                    string codeurl = ConfigurationManager.AppSettings["QRcode"].ToString() + "?QRcode=" + JMP.TOOL.Encrypt.IndexEncrypt(qurl);//二维码展示地址
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = new { ImgQRcode = ImgQRcode, codeurl = codeurl };//http提交方式;
                }
                else
                {
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), SeIn.PayId, summary: "浦发支付宝扫码接口错误信息");
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }

        /// <summary>
        /// 获取浦发账号信息
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
                        SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的浦发银行支付宝wap支付账号
                        SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的浦发银行支付宝wap支付key
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());//支付通道id
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                    }
                    else
                    {
                        dt = bll.SelectPay("PFZFBSM", apptype, appid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                            SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取浦发银行支付宝wap支付账号
                            SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取浦发银行支付宝wap支付key
                            SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                            SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                        }
                        else
                        {
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",获取缓存失败后从数据库未查询到相关信息！", summary: "浦发银行支付宝扫码支付接口错误", channelId: SeIn.PayId);
                        }
                    }
                }
                else
                {
                    dt = bll.SelectPay("PFZFBSM", apptype, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取浦发银行支付宝wap支付账号
                        SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取浦发银行支付宝wap支付key
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                    }
                    else
                    {
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + "直接从数据库未查询到相关信息！", summary: "浦发银行支付宝扫码支付接口错误", channelId: SeIn.PayId);
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog(bcxx, summary: "浦发银行支付宝wap支付支付接口错误应用类型ID：" + apptype, channelId: SeIn.PayId);
            }
            return SeIn;
        }

        #region 接受回传参数实体
        private class RESULT
        {
            public string CODE { get; set; }
            public string INFO { get; set; }
        }
        private class ORDERRESP
        {
            public string BEGIN_TIME { get; set; }
            public string END_TIME { get; set; }
            public string CHARGE_CODE { get; set; }
            public string CHARGE_DOWN_CODE { get; set; }
            public RESULT RESULT { get; set; }
            public string BAR_CODE { get; set; }
        }
        private class RootObject
        {
            public ORDERRESP ORDER_RESP { get; set; }
        }
        #endregion
    }
}
