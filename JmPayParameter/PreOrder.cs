using JmPayApiServer.Models;
using System;
using JMP.MDL;
using JMP.TOOL;
using JmPayParameter.Models;
using System.Configuration;
using DxPay.LogManager.LogFactory.ApiLog;
using TOOL.EnumUtil;
using System.Data;

namespace JmPayParameter
{
    /// <summary>
    /// 预下单接口 
    /// </summary>
    public class PreOrder
    {

        private PreOrdeModel pr = new PreOrdeModel();
        /// <summary>
        /// 预下单接口通道
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <param name="mode">参数实体类型</param>
        /// <param name="CacheTime">缓存时间</param>
        /// <param name="UserIp">ip地址</param>
        /// <returns></returns>
        public InnerResponse OrderInterface(string json, RequestParameter mode, int CacheTime, string UserIp)
        {
            InnerResponse Inn = new InnerResponse();
            if (!string.IsNullOrEmpty(json))
            {

                pr.ip = UserIp;
                //订单缓存时间
                int bizcodeTime = Int32.Parse(ConfigurationManager.AppSettings["bizcodeTime"]);
                Inn = ValidationParameter(mode, json, bizcodeTime);
                if (Inn.Success)
                {
                    SelectAPP selectAPP = new SelectAPP();
                    jmp_app app = selectAPP.SelectAppId(mode.appid, CacheTime);
                    if (app != null)
                    {
                        Apprate apprate = new Apprate();
                        if (mode.paytype > 0 && !apprate.SelectApprate(mode.appid, mode.paytype, CacheTime))
                        {
                            return Inn = Inn.ToResponse(ErrorCode.Code8987);
                        }
                        pr.appkey = app.a_key;
                        Inn = ValidationApp(mode, json, app);
                        if (Inn.Success == true)
                        {
                            Inn = DownOrder(mode, json);
                            if (Inn.Success == true)
                            {
                                //设置缓存
                                SetUpCache(mode.bizcode, mode.appid, bizcodeTime);
                                Inn = judge(mode.paytype, app.a_platform_id, app.a_rid, CacheTime, app.a_id);
                            }
                        }

                    }
                    else
                    {
                        Inn = Inn.ToResponse(ErrorCode.Code9998);
                    }
                }
            }
            return Inn;
        }

        /// <summary>
        /// 验证数据是否合法
        /// </summary>
        /// <param name="dict">请求参数键值集合</param>
        /// <param name="json">参数json字符串</param>
        /// <param name="bizcodeTime">缓存时间从配置文件中读取的</param>
        /// <param name="app">应用实体</param>
        /// <returns></returns>
        private InnerResponse ValidationParameter(RequestParameter mode, string json, int bizcodeTime)
        {
            InnerResponse Inn = new InnerResponse();
            try
            {
                if (string.IsNullOrEmpty(mode.timestamp) || mode.timestamp.Length != 10)
                {
                    return Inn = Inn.ToResponse(ErrorCode.Code9985);
                }
                if (!VerificationTimestamp(mode.timestamp, bizcodeTime))
                {
                    return Inn = Inn.ToResponse(ErrorCode.Code9984);
                }
                if (string.IsNullOrEmpty(mode.bizcode) && mode.bizcode.Length > 64)
                {
                    return Inn = Inn.ToResponse(ErrorCode.Code9997);
                }
                if (mode.appid <= 0)
                {
                    return Inn = Inn.ToResponse(ErrorCode.Code9998);
                }
                if (VerificationCode(mode.bizcode, mode.appid))
                {
                    return Inn = Inn.ToResponse(ErrorCode.Code9988);
                }
                if (string.IsNullOrEmpty(mode.goodsname) || mode.goodsname.Length > 16)
                {

                    return Inn = Inn.ToResponse(ErrorCode.Code9993);
                }
                if (mode.price <= 0)
                {
                    return Inn = Inn.ToResponse(ErrorCode.Code9992);
                }
                if (!string.IsNullOrEmpty(mode.privateinfo) && mode.privateinfo.Length > 64)
                {
                    return Inn = Inn.ToResponse(ErrorCode.Code9991);
                }
                if (!JMP.TOOL.Regular.IsDem(mode.price.ToString()))
                {
                    return Inn = Inn.ToResponse(ErrorCode.Code9986);
                }
                Inn = Inn.ToResponse(ErrorCode.Code100);
            }
            catch (Exception e)
            {
                PayApiGlobalErrorLogger.Log("报错信息103：支付接口验证参数错误,获取到的参数：" + json + ",报错信息：" + e.ToString(), summary: "接口错误信息");
                Inn = Inn.ToResponse(ErrorCode.Code103);
            }
            return Inn;
        }
        /// <summary>
        /// 验证数据是否合法（需要使用查询数据）
        /// </summary>
        /// <param name="mode">请求参数实体</param>
        /// <param name="json">参数json字符串</param>
        /// <param name="app">应用实体</param>
        /// <returns></returns>
        private InnerResponse ValidationApp(RequestParameter mode, string json, jmp_app app)
        {
            InnerResponse Inn = new InnerResponse();
            try
            {
                if (app == null)
                {
                    return Inn = Inn.ToResponse(ErrorCode.Code9998);
                }
                //验证签名的方式  price + bizcode+timestamp+appkey
                string Verificationsign = mode.price + mode.bizcode + mode.timestamp + app.a_key;
                string sign = JMP.TOOL.MD5.md5strGet(Verificationsign, true).ToUpper();
                if (mode.sign != sign)
                {
                    PayApiDetailErrorLogger.DownstreamErrorLog("报错信息9989：支付接口签名验证失败,获取到的参数：" + json + ",组装的签名字符串：" + Verificationsign + ",我们生产的签名：" + sign, summary: "接口错误信息", appId: app.a_id, errorType: EnumForLogForApi.ErrorType.Other);

                    return Inn = Inn.ToResponse(ErrorCode.Code9989);
                }
                //判断终端唯一标示码。ios和安卓模式为必传
                if (app.a_platform_id < 3 && app.a_platform_id > 0)
                {
                    if (string.IsNullOrEmpty(mode.termkey) || mode.termkey.Length > 64)
                    {
                        return Inn = Inn.ToResponse(ErrorCode.Code9996);
                    }
                }
                else
                {
                    mode.termkey = "";
                }
                if (string.IsNullOrEmpty(mode.address))
                {
                    mode.address = app.a_notifyurl;
                }
                else
                {
                    if (mode.address.Length > 200)
                    {
                        return Inn = Inn.ToResponse(ErrorCode.Code9995);
                    }
                }

                if (app.a_platform_id == 3)
                {
                    if (string.IsNullOrEmpty(mode.showaddress))
                    {
                        mode.showaddress = app.a_showurl;
                    }
                    else
                    {
                        if (mode.showaddress.Length > 200)
                        {
                            return Inn = Inn.ToResponse(ErrorCode.Code9994);
                        }
                    }
                }

                if (mode.paytype < 0 || mode.paytype > 8)
                {
                    return Inn = Inn.ToResponse(ErrorCode.Code9990);
                }
                else
                {

                    if (mode.paytype > 0 && !app.a_paymode_id.Contains(mode.paytype.ToString()))
                    {
                        return Inn = Inn.ToResponse(ErrorCode.Code105);
                    }
                    else
                    {
                        pr.ThispayType = app.a_paymode_id;
                    }
                }
                Inn = Inn.ToResponse(ErrorCode.Code100);
            }
            catch (Exception e)
            {
                PayApiGlobalErrorLogger.Log("报错信息103：支付接口验证参数错误,获取到的参数：" + json + ",报错信息：" + e.ToString(), summary: "接口错误信息");
                Inn = Inn.ToResponse(ErrorCode.Code103);
            }
            return Inn;
        }
        /// <summary>
        /// 添加订单入库
        /// </summary>
        /// <param name="mode">传入参数实体</param>
        /// <param name="json">传入参数json字符串</param>
        /// <returns></returns>
        private InnerResponse DownOrder(RequestParameter mode, string json)
        {
            InnerResponse Inn = new InnerResponse();
            JMP.MDL.jmp_order mod = new JMP.MDL.jmp_order();//订单表实体类
            JMP.BLL.jmp_order jmp_orderbll = new JMP.BLL.jmp_order();//订单表业务逻辑层 
            mod.o_address = mode.address;
            mod.o_showaddress = mode.showaddress;
            mod.o_app_id = mode.appid;
            mod.o_bizcode = mode.bizcode;
            mod.o_term_key = mode.termkey;
            mod.o_paymode_id = mode.paytype.ToString();
            mod.o_goodsname = mode.goodsname;
            mod.o_price = mode.price;
            mod.o_privateinfo = mode.privateinfo;
            Random r = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
            mod.o_code = DateTime.Now.ToString("yyyyMMddHHmmssfff") + r.Next(111111111, 999999999).ToString() + r.Next(1111, 9999).ToString();
            mod.o_state = 0;
            mod.o_times = 0;
            mod.o_noticestate = 0;
            mod.o_ctime = DateTime.Now;
            mod.o_noticetimes = DateTime.Now;
            mod.o_ptime = DateTime.Now;
            int cg = 0;
            cg = jmp_orderbll.AddOrder(mod);
            if (cg > 0)
            {
                pr.orderid = cg;
                pr.code = mod.o_code;
                pr.goodsname = mode.goodsname;
                pr.price = mode.price;
                Inn = Inn.ToResponse(ErrorCode.Code100);
            }
            else
            {
                PayApiGlobalErrorLogger.Log("报错信息：支付信息生成订单失败,获取到的参数：" + json, summary: "接口错误信息");
                Inn = Inn.ToResponse(ErrorCode.Code101);
            }
            return Inn;
        }
        /// <summary>
        /// 判断支付平台，根据支付平台返回对应信息
        /// </summary>
        /// <param name="paytype">支付类型</param>
        /// <param name="paymode">关联平台（1：安卓，2：ios，3:H5）</param>
        /// <param name="apptype">风控配置表id</param>
        /// <param name="CacheTime">缓存时间</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private InnerResponse judge(int paytype, int paymode, int apptype, int CacheTime, int appid)
        {
            InnerResponse Inn = new InnerResponse();
            JmPayParameter.JsonStr Jsonstr = new JsonStr();
            JmPayParameter.PayTypeFactory.PayTypeFactory payTypeFactory = new PayTypeFactory.PayTypeFactory();
            if (paytype == 0)
            {
                #region 收银台模式
                //收银台模式
                if (paymode == 3)
                {

                    PayBankModels modes = Jsonstr.ParameterEntity(pr.code, pr.goodsname, pr.price, pr.ThispayType, apptype, paymode);
                    //h5模式
                    Inn = Jsonstr.BankJsonStr(modes, pr.ip);
                    //设置为跳转模式
                    Inn.IsJump = true;
                }
                else
                {
                    //获取优先级设置(微信wap和微信appid根据优先级返回其中之一)
                    int SdkPriority = Int32.Parse(ConfigurationManager.AppSettings["SdkPriority"].ToString());
                    //判断收银台模式优先返回微信调用方式
                    if (SdkPriority == 2 && pr.ThispayType.Contains("2") && pr.ThispayType.Contains("5"))
                    {
                        pr.ThispayType = pr.ThispayType.Replace("5", "@").Replace(",@", "");
                    }
                    else if (SdkPriority == 5 && pr.ThispayType.Contains("2") && pr.ThispayType.Contains("5"))
                    {
                        pr.ThispayType = pr.ThispayType.Replace("2", "@").Replace(",@", "");
                    }

                    PayBankModels modes = Jsonstr.ParameterEntity(pr.code, pr.goodsname, pr.price, pr.ThispayType, apptype, paymode);
                    //sdk模式
                    Inn = Jsonstr.BankSdk(modes, pr.ip);
                }
                #endregion
            }
            else
            {
                if (paymode == 3)
                {
                    #region H5模式
                    if (paytype == 6 || paytype == 7)
                    {
                        try
                        {
                            var payType = payTypeFactory.Create(paytype);
                            var channel = payType.LoadChannel(paymode, apptype, CacheTime, appid);
                            JmPayParameter.PlaceOrder.PlaceOrderFactory placeOrderFactory = new PlaceOrder.PlaceOrderFactory();
                            //返回支付信息
                            Inn = placeOrderFactory.Create(channel.PassName, paymode, apptype, pr.code, pr.goodsname, pr.price, pr.orderid, pr.ip, appid);
                        }
                        catch (Exc e)
                        {
                            Inn = e.Response;
                            return Inn;
                        }

                    }
                    else
                    {
                        PayBankModels modes = Jsonstr.ParameterEntity(pr.code, pr.goodsname, pr.price, paytype.ToString(), apptype, paymode);
                        //h5模式
                        Inn = Jsonstr.H5JsonStr(modes, pr.ip);
                        //设置为跳转模式
                        //Inn.IsJump = true; 
                    }
                    #endregion
                }
                else
                {
                    #region sdk模式
                    //sdk模式
                    try
                    {
                        var payType = payTypeFactory.Create(paytype);
                        var channel = payType.LoadChannel(paymode, apptype, CacheTime, appid);
                        JmPayParameter.PlaceOrder.PlaceOrderFactory placeOrderFactory = new PlaceOrder.PlaceOrderFactory();
                        //返回支付信息
                        Inn = placeOrderFactory.Create(channel.PassName, paymode, apptype, pr.code, pr.goodsname, pr.price, pr.orderid, pr.ip, appid);
                    }
                    catch (Exc e)
                    {
                        Inn = e.Response;
                        return Inn;
                    }
                    #endregion
                }
            }
            return Inn;
        }
        /// <summary>
        /// 验证商户订单号是否唯一
        /// </summary>
        /// <param name="bizcode">商户订单号</param>
        /// <param name="appid">应用id</param>
        /// <returns></returns>
        private bool VerificationCode(string bizcode, int appid)
        {
            bool IsRepeat = false;
            JMP.MDL.jmp_order mode = new JMP.MDL.jmp_order();
            JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
            string Cachekey = "VerificationCode" + bizcode + appid;
            if (JMP.TOOL.CacheHelper.IsCache(Cachekey))//判读是否存在缓存
            {
                PayApiDetailErrorLogger.DownstreamErrorLog("报错信息：支付接口验证参数错误,商户订单重复，缓存值：" + Cachekey + ",商户订单号：" + bizcode, summary: "接口错误信息", appId: appid, errorType: EnumForLogForApi.ErrorType.OrderNoRepeat);
                IsRepeat = true;
            }
            else
            {

                IsRepeat = false;
            }
            return IsRepeat;
        }
        /// <summary>
        /// 设置订单缓存
        /// </summary>
        /// <param name="bizcode">商户订单编号</param>
        /// <param name="appid">应用id</param>
        /// <param name="bizcodeTime">缓存时间</param>
        public void SetUpCache(string bizcode, int appid, int bizcodeTime)
        {
            string Cachekey = "VerificationCode" + bizcode + appid;
            string CacheValue = bizcode + "," + appid;
            JMP.TOOL.CacheHelper.CacheObjectLocak<string>(CacheValue, Cachekey, bizcodeTime);//存入缓存
        }
        /// <summary>
        /// 验证时间戳是否合法以及是否在规定时间内
        /// </summary>
        /// <param name="timestamp">缓存的时间</param>
        /// <returns></returns>
        public bool VerificationTimestamp(string timestamp, int bizcodeTime)
        {
            try
            {
                bool istamp = false;
                DateTime tamp = JMP.TOOL.WeekDateTime.GetTime(timestamp);
                if (DateTime.Now < tamp.AddMinutes(bizcodeTime))
                {
                    istamp = true;
                }
                return istamp;
            }
            catch
            {
                return false;
            }


        }


    }
}
