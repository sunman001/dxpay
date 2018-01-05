using JMP.TOOL;
using JmPayParameter.PayChannel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Net;
using System.Text;
using DxPay.LogManager.LogFactory.ApiLog;

namespace JmPayParameter.PlaceOrder.WxPaySmType
{
    /// <summary>
    /// 南粤微信扫码接口
    /// </summary>
    public class NyWxSmPay
    {

        /// <summary>
        /// 南粤微信扫码通道主入口
        /// </summary>
        /// <param name="paymode">平台类型（1：安卓，2：ios，3：H5）</param>
        /// <param name="apptype">应用类型子id</param>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">商品价格（单位元）</param>
        /// <param name="orderid">订单id</param>
        /// <param name="infoTime">缓存时间</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        public  InnerResponse NyWxSmPayInfo(int paymode, int apptype, string code, string goodsname, decimal price, int orderid, int infoTime, int appid)
        {
            InnerResponse inn = new InnerResponse();
            if (paymode == 3)
            {
                inn = NywxsmH5(apptype, code, price, orderid, goodsname, infoTime, appid);
            }
            else
            {
                inn = inn.ToResponse(ErrorCode.Code9987);
            }
            return inn;
        }

        /// <summary>
        /// 南粤微信扫码支付
        /// </summary>
        /// <param name="apptype">应用类型id</param>
        /// <param name="code">订单编号</param>
        /// <param name="price">商品价格</param>
        /// <param name="orderid">订单id</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private  InnerResponse NywxsmH5(int apptype, string code, decimal price, int orderid, string goodsname, int infoTime, int appid)
        {
            InnerResponse inn = new InnerResponse();
            int pay_id = 0;//支付渠道id
            try
            {
                string userid = "";//南粤公众号商户id
                string userkey = "";//南粤公众号key

                decimal minmun = 0;
                decimal maximum = 0;
                string Nywxgzh = "Nywxgzh" + appid;//组装缓存key值
                #region 南粤微信扫码支付账号信息
                try
                {
                    DataTable dt = new DataTable();
                    JMP.BLL.jmp_interface bll = new JMP.BLL.jmp_interface();
                    if (JMP.TOOL.CacheHelper.IsCache(Nywxgzh))
                    {
                        dt = JMP.TOOL.CacheHelper.GetCaChe<DataTable>(Nywxgzh);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                            userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的南粤微信扫码id
                            userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取缓存数据中的南粤微信扫码key
                            pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                            minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额

                        }
                        else
                        {
                            dt = bll.SelectPay("nywxsm", apptype, appid);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                int row = new Random().Next(0, dt.Rows.Count);
                                string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                                userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取南粤公众号id
                                userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取南粤公众号key
                                pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                                minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                                maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                                JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, Nywxgzh, infoTime);//存入缓存
                            }
                            else
                            {
                                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",获取缓存失败后，从数据库未查询到相关信息！", summary: "南粤微信扫码接口错误", channelId: pay_id);
                                inn = inn.ToResponse(ErrorCode.Code106);
                                return inn;
                            }
                        }
                    }
                    else
                    {
                        dt = bll.SelectPay("nywxsm", apptype, appid);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int row = new Random().Next(0, dt.Rows.Count);
                            string[] paypz = dt.Rows[row]["l_str"].ToString().Split(',');
                            userid = paypz[0].Replace("\r", "").Replace("\n", "").Trim();//获取南粤公众号id
                            userkey = paypz[1].Replace("\r", "").Replace("\n", "").Trim();//获取南粤公众号key
                            pay_id = int.Parse(dt.Rows[row]["l_id"].ToString());
                            minmun = decimal.Parse(dt.Rows[row]["l_minimum"].ToString());//单笔最小支付金额
                            maximum = decimal.Parse(dt.Rows[row]["l_maximum"].ToString());//单笔最大支付金额
                            JMP.TOOL.CacheHelper.CacheObjectLocak<DataTable>(dt, Nywxgzh, infoTime);//存入缓存
                        }
                        else
                        {
                            PayApiDetailErrorLogger.UpstreamPaymentErrorLog("应用id为：" + appid + "的支付通道为空！风控id为：" + apptype + ",从数据库未查询到相关信息！", summary: "南粤微信扫码接口错误", channelId: pay_id);
                            inn = inn.ToResponse(ErrorCode.Code106);
                            return inn;
                        }
                    }
                }
                catch (Exception e)
                {
                    string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString();//报错信息
                    PayApiDetailErrorLogger.UpstreamPaymentErrorLog(bcxx, summary: "南粤微信扫码支付接口错误应用类型ID：" + apptype, channelId: pay_id);
                    inn = inn.ToResponse(ErrorCode.Code106);
                    return inn;
                }
                #endregion
                if (!UpdateOrde.OrdeUpdateInfo(orderid, pay_id))
                {
                    inn = inn.ToResponse(ErrorCode.Code101);
                    return inn;
                }
                if (!JudgeMoney.JudgeMinimum(price, minmun))
                {
                    inn = inn.ToResponse(ErrorCode.Code8990);
                    return inn;
                }
                if (!JudgeMoney.JudgeMaximum(price, maximum))
                {
                    inn = inn.ToResponse(ErrorCode.Code8989);
                    return inn;
                }
                Dictionary<string, string> strlist = new Dictionary<string, string>();
                strlist.Add("tradeType", "cs.pay.submit");//交易类型
                strlist.Add("version", "1.3");//版本号
                strlist.Add("mchId", userid);//代理商号
                strlist.Add("channel", "wxPubQR");//支付渠道
                strlist.Add("body", goodsname);//商品描述
                strlist.Add("outTradeNo", code);//商户订单号
                strlist.Add("amount", price.ToString());//交易金额
                                                        //strlist.Add("description", JMP.TOOL.DESEncrypt.Encrypt(code));//自定义信息
                strlist.Add("notifyUrl", ConfigurationManager.AppSettings["NyNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()));//异步通知
                strlist.Add("callbackUrl", ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", orderid.ToString()));//同步通知
                string md5str = JMP.TOOL.UrlStr.AzGetStr(strlist) + "&key=" + userkey;
                string md5 = JMP.TOOL.MD5.md5strGet(md5str, true);
                strlist.Add("sign", md5);//签名
                string extra = "";
                //if (tid == 71)//判断应用类型是否需要禁用信用卡
                //{
                //    extra = "{\"callbackUrl\":\"" + ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", oid.ToString()) + "\",\"notifyUrl\":\"" + ConfigurationManager.AppSettings["NyNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()) + "\",\"notifyUrl\":\"no_credit\"}";
                //}
                //else
                //{
                extra = "{\"callbackUrl\":\"" + ConfigurationManager.AppSettings["GOTOUrl"].ToString().Replace("{0}", orderid.ToString()) + "\",\"notifyUrl\":\"" + ConfigurationManager.AppSettings["NyNotifyUrl"].ToString().Replace("{0}", pay_id.ToString()) + "\"}";
                // }
                strlist.Add("extra", extra);//扩展字段
                string postString = JMP.TOOL.JsonHelper.DictJsonstr(strlist, "extra");//这里即为传递的参数，可以用工具抓包分析，也可以自己分析，主要是form里面每一个name都要加进来 
                byte[] postData = Encoding.UTF8.GetBytes(postString);//编码，尤其是汉字，事先要看下抓取网页的编码方式  
                string url = ConfigurationManager.AppSettings["NYPOSTUrl"].ToString();//请求地址  
                WebClient webClient = new WebClient();
                byte[] responseData = webClient.UploadData(url, "POST", postData);//得到返回字符流  
                string srcString = Encoding.UTF8.GetString(responseData);//解码  
                Dictionary<string, object> jsonstr = JMP.TOOL.JsonHelper.DataRowFromJSON(srcString);
                if (jsonstr.ContainsKey("returnCode") && jsonstr["resultCode"].ToString() == "0")
                {
                    string qurl = jsonstr["codeUrl"].ToString() + "," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ",2";//组装二维码地址
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

                PayApiDetailErrorLogger.UpstreamPaymentErrorLog("报错信息：" + E.ToString(), summary: "南粤微信扫码接口错误信息", channelId: pay_id);
                inn = inn.ToResponse(ErrorCode.Code104);
            }
            return inn;
        }
    }
}
