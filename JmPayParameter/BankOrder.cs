using DxPay.LogManager.LogFactory.ApiLog;
using JMP.MDL;
using JMP.TOOL;
using JmPayParameter.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOOL.EnumUtil;

namespace JmPayParameter
{
    /// <summary>
    /// 收银台和H5模式下单通道
    /// </summary>
    public class BankOrder
    {

        /// <summary>
        /// H5和收银台模式下单主通道
        /// </summary>
        /// <param name="mode">接受参数modes实体</param>
        /// <param name="CacheTime"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public InnerResponse H5OrBankEntrance(PayBankModels mode, int CacheTime, string ip)
        {
            InnerResponse Inn = new InnerResponse();
            if (mode != null)
            {
                Inn = InspectParameter(mode);
                if (Inn.Success == true)
                {
                    JMP.MDL.jmp_order mod = new JMP.MDL.jmp_order();
                    mod = SelectCode(mode.code, CacheTime, mode.paytype);
                    if (mod != null && mod.o_state == 0)
                    {
                        Inn = checkSign(mode, mod);
                        if (Inn.Success == true)
                        {
                            Apprate apprate = new Apprate();
                            if (Int32.Parse(mode.paytype) > 0 && !apprate.SelectApprate(mod.o_app_id, Int32.Parse(mode.paytype), CacheTime))
                            {
                                return Inn = Inn.ToResponse(ErrorCode.Code8987);
                            }
                            if (UpdateCode(mod.o_id, int.Parse(mode.paytype)))
                            {
                                try
                                {
                                    JmPayParameter.PayTypeFactory.PayTypeFactory payTypeFactory = new PayTypeFactory.PayTypeFactory();
                                    var payType = payTypeFactory.Create(int.Parse(mode.paytype));
                                    if (mode.paytype == "4" && (mode.paymode == 2 || mode.paymode == 1))
                                    {
                                        mode.paymode = 3;
                                    }
                                    var channel = payType.LoadChannel(mode.paymode, mode.apptype, CacheTime, mod.o_app_id);
                                    JmPayParameter.PlaceOrder.PlaceOrderFactory placeOrderFactory = new PlaceOrder.PlaceOrderFactory();
                                    //返回支付信息
                                    Inn = placeOrderFactory.Create(channel.PassName, mode.paymode, mode.apptype, mode.code, mode.goodsname, mode.price, mod.o_id, ip, mod.o_app_id);
                                }
                                catch (Exc e)
                                {
                                    Inn = e.Response;
                                    return Inn;
                                }
                            }
                            else
                            {
                                PayApiDetailErrorLogger.DownstreamErrorLog("报错信息：支付接口收银台模式报错,修改订单失败：订单id：" + mod.o_id + ",支付方式：" + mode.paymode + ",订单编号：" + mode.code, summary: "接口错误信息", appId: mod.o_app_id, errorType: EnumForLogForApi.ErrorType.RequestRepeat);
                                Inn = Inn.ToResponse(ErrorCode.Code8999);
                            }
                        }
                    }
                    else
                    {
                        Inn = Inn.ToResponse(ErrorCode.Code8991);
                    }
                }
            }
            else
            {
                PayApiGlobalErrorLogger.Log("报错信息103：支付接口收银台模式报错,未获取到下单参数，接收参数的实体为空", summary: "接口错误信息");
                Inn = Inn.ToResponse(ErrorCode.Code103);
            }
            return Inn;
        }
        /// <summary>
        /// 验证接受参数是否合法以及有效性
        /// </summary>
        /// <param name="mode">接受的参数实体</param>
        /// <returns></returns>
        private InnerResponse InspectParameter(PayBankModels mode)
        {
            InnerResponse Inn = new InnerResponse();
            if (string.IsNullOrEmpty(mode.code))
            {
                return Inn = Inn.ToResponse(ErrorCode.Code8999);
            }
            if (string.IsNullOrEmpty(mode.sign))
            {
                return Inn = Inn.ToResponse(ErrorCode.Code8998);
            }
            if (mode.price <= 0)
            {
                return Inn = Inn.ToResponse(ErrorCode.Code8997);
            }
            if (string.IsNullOrEmpty(mode.goodsname))
            {
                return Inn = Inn.ToResponse(ErrorCode.Code8996);
            }
            if (mode.apptype <= 0)
            {
                return Inn = Inn.ToResponse(ErrorCode.Code8995);
            }
            if (mode.paymode > 3 || mode.paymode < 1)
            {
                return Inn = Inn.ToResponse(ErrorCode.Code8994);
            }
            if (string.IsNullOrEmpty(mode.paytype))
            {
                return Inn = Inn.ToResponse(ErrorCode.Code8993);
            }
            else
            {
                try
                {
                    int type = int.Parse(mode.paytype);
                    if (type < 1 || type > 8)
                    {
                        return Inn = Inn.ToResponse(ErrorCode.Code8993);
                    }
                }
                catch
                {

                    return Inn = Inn.ToResponse(ErrorCode.Code8993);
                }

            }
            Inn = Inn.ToResponse(ErrorCode.Code100);
            return Inn;
        }
        /// <summary>
        /// 验证签名和订单有效性
        /// </summary>
        /// <param name="mod">查询到的订单实体</param>
        /// <param name="mode">接收到的参数实体</param>
        /// <returns></returns>
        private InnerResponse checkSign(PayBankModels mode, JMP.MDL.jmp_order mod)
        {
            InnerResponse Inn = new InnerResponse();
            int bizcodeTime = Int32.Parse(ConfigurationManager.AppSettings["bizcodeTime"]);
            //规则=JMP.TOOL.MD5.md5strGet((mod.o_code + mod.o_price), true).ToUpper();
            if (!VerificationCode(mode.code, bizcodeTime, mod.o_app_id))
            {
                return Inn = Inn.ToResponse(ErrorCode.Code8988);
            }
            string sign = JMP.TOOL.MD5.md5strGet((mod.o_code + mode.price), true).ToUpper();
            if (mode.price == mod.o_price && sign == mode.sign)
            {
                int szsj = int.Parse(ConfigurationManager.AppSettings["EffectiveTime"].ToString());
                bool pdsj = DateTime.ParseExact(mode.code.Substring(0, 14), "yyyyMMddHHmmss", CultureInfo.InvariantCulture) > DateTime.Parse(DateTime.Now.AddMinutes(-(szsj)).ToString("yyyy-MM-dd HH:mm:ss")) ? true : false;//判断是否在规定时间范围内
                if (pdsj == false)
                {
                    Inn = Inn.ToResponse(ErrorCode.Code8992);
                }
                else
                {
                    Inn = Inn.ToResponse(ErrorCode.Code100);
                }
            }
            else
            {
                Inn = Inn.ToResponse(ErrorCode.Code9989);
            }
            return Inn;
        }

        /// <summary>
        /// 根据订单编号查询订单信息并调取支付当时
        /// </summary>
        /// <param name="Code">订单编号</param>
        /// <param name="json">接受参数json字符串</param>
        /// <param name="ip">请求ip地址</param>
        /// <returns></returns>
        private JMP.MDL.jmp_order SelectCode(string Code, int CacheTime, string paytype)
        {
            JMP.MDL.jmp_order mode = new JMP.MDL.jmp_order();
            JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
            string Cachekey = "SelectCode" + Code + paytype;
            if (JMP.TOOL.CacheHelper.IsCache(Cachekey))//判读是否存在缓存
            {
                if (paytype == "4")
                {
                    mode = JMP.TOOL.CacheHelper.GetCaChe<JMP.MDL.jmp_order>(Cachekey);//获取缓存
                }
                else
                {
                    JMP.MDL.jmp_order moded = new JMP.MDL.jmp_order();
                    moded = JMP.TOOL.CacheHelper.GetCaChe<JMP.MDL.jmp_order>(Cachekey);//获取缓存
                    PayApiDetailErrorLogger.DownstreamErrorLog("报错信息：商户发起重复下单请求，订单编号" + Code, summary: "接口错误信息,商户多次发起支付请求！", appId: moded.o_app_id, errorType: EnumForLogForApi.ErrorType.RequestRepeat);
                    mode = null;
                }
                //mode = JMP.TOOL.CacheHelper.GetCaChe<JMP.MDL.jmp_order>(Cachekey);//获取缓存
                //if (mode == null)
                //{
                //    mode = bll.GetModelbycode(Code, "jmp_order");
                //    if (mode != null)
                //    {
                //        JMP.TOOL.CacheHelper.CacheObjectLocak<JMP.MDL.jmp_order>(mode, Cachekey, CacheTime);//存入缓存
                //    }
                //}
            }
            else
            {
                mode = bll.SelectCode(Code, "jmp_order");
                if (mode != null)
                {
                    JMP.TOOL.CacheHelper.CacheObjectLocak<JMP.MDL.jmp_order>(mode, Cachekey, CacheTime);//存入缓存

                }
            }
            return mode;
        }

        /// <summary>
        /// 验证平台订单号是否存在重复
        /// </summary>
        /// <param name="Code">订单号</param>
        /// <param name="CacheTime">缓存时间</param>
        /// <returns></returns>
        private bool VerificationCode(string Code, int CacheTime, int appid)
        {
            bool msg = false;
            string Cachekey = "VerificationCode" + Code;
            if (JMP.TOOL.CacheHelper.IsCache(Cachekey))//判读是否存在缓存
            {
                PayApiDetailErrorLogger.DownstreamErrorLog("报错信息：商户发起重复下单请求，订单编号" + Code, summary: "接口错误信息,商户多次发起支付请求！", appId: appid, errorType: EnumForLogForApi.ErrorType.RequestRepeat);
            }
            else
            {
                JMP.TOOL.CacheHelper.CacheObjectLocak<string>(Code, Cachekey, CacheTime);//存入缓存
                msg = true;
            }
            return msg;
        }
        /// <summary>
        /// 修改原订单支付方式
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="paymode"></param>
        /// <returns></returns>
        private bool UpdateCode(int oid, int paymode)
        {
            bool Success = false;
            JMP.BLL.jmp_order bll = new JMP.BLL.jmp_order();
            if (bll.UpdatePayMode(oid, paymode))
            {
                Success = true;
            }
            return Success;
        }
    }
}
