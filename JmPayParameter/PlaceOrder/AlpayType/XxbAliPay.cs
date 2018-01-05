using DxPay.LogManager.LogFactory.ApiLog;
using JmPayParameter.Models;
using JmPayParameter.PayChannel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JmPayParameter.PlaceOrder.AlpayType
{

    public class XxbAliPay
    {
        /// <summary>
        /// 小小贝支付宝wap支付通道主入口
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
        public InnerResponse XxbZFBPayInfo(int paymode, int apptype, string code, string goodsname, decimal price, int orderid, string ip, int infoTimes, int appid)
        {
            InnerResponse inn = new InnerResponse();
            switch (paymode)
            {
                case 1://安卓方式
                    inn = XxbZFBWaPAz(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                case 2://ios方式
                    inn = XxbZFBWaPIOS(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                case 3://H5支付方式
                    inn = XxbZFBWaPH5(apptype, code, goodsname, price, orderid, ip, appid, infoTimes);
                    break;
                default:
                    inn = inn.ToResponse(ErrorCode.Code9987);
                    break;
            }
            return inn;
        }


        #region 小小贝支付宝wap支付

        /// <summary>
        ///小小贝支付宝wap支付h5调用模式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse XxbZFBWaPH5(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {

                string XxbZfbWaPH5jkhc = "XxbZfbWaPH5jkhc" + appid;//组装缓存key值
                SeIn = SelectUserInfo(XxbZfbWaPH5jkhc, apptype, appid, infoTimes);
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
                Dictionary<string, string> Palist = new Dictionary<string, string>();

                Palist.Add("merchantID", SeIn.UserId);//小小贝分配的appId
                Palist.Add("waresname", goodsname);//商品名称
                Palist.Add("cporderid", code);//商户单号
                Palist.Add("price", price.ToString("f2"));//商品价格(单位:元)，必填项
                Palist.Add("returnurl", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", orderid.ToString()));//同步
                Palist.Add("notifyurl", ConfigurationManager.AppSettings["XxbPayNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//异步通知地址
                Palist.Add("paytype", "10006");//支付方式
                Palist.Add("ip", ip);//ip

                //拼装签名
                var sign_prep = JMP.TOOL.UrlStr.AzGetStr(Palist) + "&key=" + SeIn.UserKey;
                //生成签名
                string sign = JMP.TOOL.MD5.md5strGet(sign_prep, true).ToLower();

                string json = "transdata=" + HttpUtility.UrlEncode(JMP.TOOL.JsonHelper.DictJsonstr(Palist)) + "&sign=" + HttpUtility.UrlEncode(sign) + "&signtype=MD5";
                //拼装地址
                string urlstr = ConfigurationManager.AppSettings["XxbZfbPayUrl"].ToString() + json;//请求地址

                //请求
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(urlstr);    //创建一个请求示例
                request.Method = "POST";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();  //获取响应，即发送请求
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                string srcString = streamReader.ReadToEnd();

                //json 转 键值集合
                RootObject obj = new RootObject();
                obj = JMP.TOOL.JsonHelper.Deserializes<RootObject>(srcString);


                if (obj != null && obj.resultCode == "20000")
                {

                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = obj.info.payurl;//http提交方式;
                    inn.IsJump = true;
                }
                else
                {
                    string xxbwxsbxinxi = "小小贝支付宝wap支付错误代码：" + srcString + ",商户号：" + SeIn.UserId;
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + xxbwxsbxinxi, summary: "小小贝支付宝wap支付接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }

            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "小小贝支付宝wap接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }

            return inn;
        }

        /// <summary>
        /// 小小贝支付宝wap支付安卓调用模式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse XxbZFBWaPAz(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {
                string XxbZfbWaPAzjkhc = "XxbZfbWaPAzjkhc" + appid;//组装缓存key值

                SeIn = SelectUserInfo(XxbZfbWaPAzjkhc, apptype, appid, infoTimes);
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
                Dictionary<string, string> Palist = new Dictionary<string, string>();

                Palist.Add("merchantID", SeIn.UserId);//小小贝分配的appId
                Palist.Add("waresname", goodsname);//商品名称
                Palist.Add("cporderid", code);//商户单号
                Palist.Add("price", price.ToString("f2"));//商品价格(单位:元)，必填项
                Palist.Add("returnurl", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", orderid.ToString()));//同步
                Palist.Add("notifyurl", ConfigurationManager.AppSettings["XxbPayNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//异步通知地址
                Palist.Add("paytype", "10006");//支付方式
                Palist.Add("ip", ip);//ip

                //拼装签名
                var sign_prep = JMP.TOOL.UrlStr.AzGetStr(Palist) + "&key=" + SeIn.UserKey;
                //生成签名
                string sign = JMP.TOOL.MD5.md5strGet(sign_prep, true).ToLower();

                string json = "transdata=" + HttpUtility.UrlEncode(JMP.TOOL.JsonHelper.DictJsonstr(Palist)) + "&sign=" + HttpUtility.UrlEncode(sign) + "&signtype=MD5";
                //拼装地址
                string urlstr = ConfigurationManager.AppSettings["XxbZfbPayUrl"].ToString() + json;//请求地址

                //请求
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(urlstr);    //创建一个请求示例
                request.Method = "POST";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();  //获取响应，即发送请求
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                string srcString = streamReader.ReadToEnd();

                //json 转 键值集合
                RootObject obj = new RootObject();
                obj = JMP.TOOL.JsonHelper.Deserializes<RootObject>(srcString);

                if (obj != null && obj.resultCode == "20000")
                {
                    string Alpay = "{\"PaymentType\":\"1\",\"SubType\":\"7\",\"data\":\"" + obj.info.payurl + "\",\"IsH5\":\"1\"}";
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(Alpay, ConfigurationManager.AppSettings["encryption"].ToString());
                }
                else
                {
                    string xxbwxsbxinxi = "小小贝支付宝wap支付错误代码：" + srcString + ",商户号：" + SeIn.UserId;
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + xxbwxsbxinxi, summary: "小小贝支付宝wap支付接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "小小贝支付宝wap接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }

        /// <summary>
        /// 小小贝支付宝wap支付苹果调用模式
        /// </summary>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">支付金额</param>
        /// <param name="orderid">订单id</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse XxbZFBWaPIOS(int apptype, string code, string goodsname, decimal price, int orderid, string ip, int appid, int infoTimes)
        {
            InnerResponse inn = new InnerResponse();
            SelectInterface SeIn = new SelectInterface();
            try
            {

                string XxbZfbWaPIOSjkhc = "XxbZfbWaPIOSjkhc" + appid;//组装缓存key值
                SeIn = SelectUserInfo(XxbZfbWaPIOSjkhc, apptype, appid, infoTimes);
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
                Dictionary<string, string> Palist = new Dictionary<string, string>();

                Palist.Add("merchantID", SeIn.UserId);//小小贝分配的appId
                Palist.Add("waresname", goodsname);//商品名称
                Palist.Add("cporderid", code);//商户单号
                Palist.Add("price", price.ToString("f2"));//商品价格(单位:元)，必填项
                Palist.Add("returnurl", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", orderid.ToString()));//同步
                Palist.Add("notifyurl", ConfigurationManager.AppSettings["XxbPayNotifyUrl"].ToString().Replace("{0}", SeIn.PayId.ToString()));//异步通知地址
                Palist.Add("paytype", "10006");//支付方式
                Palist.Add("ip", ip);//ip

                //拼装签名
                var sign_prep = JMP.TOOL.UrlStr.AzGetStr(Palist) + "&key=" + SeIn.UserKey;
                //生成签名
                string sign = JMP.TOOL.MD5.md5strGet(sign_prep, true).ToLower();

                string json = "transdata=" + HttpUtility.UrlEncode(JMP.TOOL.JsonHelper.DictJsonstr(Palist)) + "&sign=" + HttpUtility.UrlEncode(sign) + "&signtype=MD5";
                //拼装地址
                string urlstr = ConfigurationManager.AppSettings["XxbZfbPayUrl"].ToString() + json;//请求地址

                //请求
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(urlstr);    //创建一个请求示例
                request.Method = "POST";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();  //获取响应，即发送请求
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                string srcString = streamReader.ReadToEnd();

                //json 转 键值集合
                RootObject obj = new RootObject();
                obj = JMP.TOOL.JsonHelper.Deserializes<RootObject>(srcString);

                if (obj != null && obj.resultCode == "20000")
                {
                    string Alpay = "{\"PaymentType\":\"1\",\"SubType\":\"7\",\"data\":\"" + obj.info.payurl + "\",\"IsH5\":\"1\"}";
                    inn = inn.ToResponse(ErrorCode.Code100);
                    inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(Alpay, ConfigurationManager.AppSettings["encryption"].ToString());
                }
                else
                {
                    string xxbwxsbxinxi = "小小贝支付宝wap支付错误代码：" + srcString + ",商户号：" + SeIn.UserId;
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + xxbwxsbxinxi, summary: "小小贝支付宝wap支付接口错误信息", channelId: SeIn.PayId);
                    inn = inn.ToResponse(ErrorCode.Code104);
                }
            }
            catch (Exception E)
            {

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "小小贝支付宝wap接口错误信息", channelId: SeIn.PayId);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }

        /// <summary>
        /// 获取小小贝账号信息
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
                        SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的小小贝支付宝wap支付账号
                        SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的小小贝支付宝wap支付key
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                    }
                    else
                    {

                        dt = bll.SelectPay("XXBZFB", apptype, appid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                            SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取小小贝支付宝wap支付账号
                            SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取小小贝支付宝wap支付key
                            SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                            SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                        }
                        else
                        {
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",获取缓存未成功后在次查询数据！", summary: "小小贝支付宝wap支付支付接口错误", channelId: SeIn.PayId);
                        }
                    }
                }
                else
                {
                    dt = bll.SelectPay("XXBZFB", apptype, appid);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int row = new Random().Next(0, dt.Rows.Count);
                        string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                        SeIn.UserId = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取小小贝支付宝wap支付账号
                        SeIn.UserKey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取小小贝支付宝wap支付key
                        SeIn.PayId = int.Parse(dt.Rows[row]["l_id"].ToString());
                        SeIn.minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                        SeIn.maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                        JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, cache, infoTimes);//存入缓存
                    }
                    else
                    {
                        PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",在数据库为查询到数据", summary: "小小贝支付宝wap支付支付接口错误", channelId: SeIn.PayId);
                    }
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                PayApiDetailErrorLogger.UpstreamPaymentErrorLog(bcxx, summary: "小小贝支付宝wap支付支付接口错误应用类型ID：" + apptype, channelId: SeIn.PayId);
            }
            return SeIn;
        }


        #endregion


        public class Info
        {
            public string payurl { get; set; }
            public string nonceStr { get; set; }
        }

        public class RootObject
        {
            public Info info { get; set; }
            public string resultCode { get; set; }
            public string sign { get; set; }
            public string signtype { get; set; }
        }

    }
}
