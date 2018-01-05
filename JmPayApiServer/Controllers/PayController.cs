using JmPayApiServer.Models;
using JmPayParameter;
using JmPayParameter.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using DxPay.LogManager.LogFactory;
using DxPay.LogManager.LogFactory.ApiLog;

namespace JmPayApiServer.Controllers
{
    /// <summary>
    /// 支付接口控制器
    /// </summary>
    public class PayController : ApiController
    {
        /// <summary>
        /// 支付接口第一步验证请求参数主通道(支持get和post)
        /// </summary>
        /// <returns>返回数据默认为json格式</returns>
        [HttpGet, HttpPost]
        public IHttpActionResult JmPay(JmPayApiServer.Models.RequestParameter model)
        {

            Response json = new Response();
            //string ip = ((System.Web.HttpContextWrapper)Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            string ip = ((System.Web.HttpContextWrapper)Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                #region 获取参数
                string pamentjson = "";
                //获取下单请求的参数
                if (Request.Method == HttpMethod.Get)
                {
                    dict = Request.GetQueryNameValuePairs().ToDictionary(x => x.Key, x => x.Value, StringComparer.CurrentCultureIgnoreCase);
                    if (dict.Count > 0)
                    {
                        pamentjson = JMP.TOOL.JsonHelper.DictJsonstr(dict);
                        model = JMP.TOOL.JsonHelper.Deserialize<RequestParameter>(pamentjson);
                    }
                }
                else if (Request.Method == HttpMethod.Post)
                {
                    pamentjson = JMP.TOOL.JsonHelper.Serialize(model);
                }
                #endregion

                if (model != null)
                {

                    //获取配置文件设置的缓存时间
                    int CacheTime = Int32.Parse(ConfigurationManager.AppSettings["CacheTime"].ToString());
                    if (string.IsNullOrEmpty(model.privateinfo))
                    {
                        model.privateinfo = "404";
                    }
                    //调用预下单接口
                    JmPayParameter.PreOrder pre = new PreOrder();
                    var message = pre.OrderInterface(pamentjson, model, CacheTime, ip);
                    //判断是否成功（成功后返回）
                    if (message.Success)
                    {
                        SuccessResponse sucee = new SuccessResponse();
                        sucee.ErrorCode = message.ErrorCode;
                        sucee.Message = message.Message;
                        sucee.ExtraData = message.ExtraData;
                        if (message.IsJump)
                        {
                            return Redirect(message.ExtraData.ToString());
                        }
                        else
                        {
                            return Ok(sucee);
                        }
                    }
                    else
                    {
                        json.ErrorCode = message.ErrorCode;
                        json.Message = message.Message;
                    }
                }
                else
                {
                    json = json.ToResponse(ErrorCode.Code9999);
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString() + "报错位置：" + e.StackTrace;//报错信息
                PayApiGlobalErrorLogger.Log("报错信息：" + bcxx, summary: "支付接口JmPay错误信息");
                json = json.ToResponse(ErrorCode.Code103);
            }
            return Ok(json);
        }

        /// <summary>
        /// 收银台和H5模式第二次请求发起支付
        /// </summary>
        /// <returns></returns>
        [HttpGet, HttpPost]
        public IHttpActionResult PayBank(JmPayParameter.Models.BanlModels mod)
        {

            Response json = new Response();
            string ip = ((System.Web.HttpContextWrapper)Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                string pamentjson = "";
                #region 获取请求参数
                //获取下单请求的参数
                if (Request.Method == HttpMethod.Get)
                {
                    dict = Request.GetQueryNameValuePairs()
                        .ToDictionary(x => x.Key, x => x.Value, StringComparer.CurrentCultureIgnoreCase);
                    if (dict.Count > 0)
                    {
                        pamentjson = JMP.TOOL.JsonHelper.DictJsonstr(dict);
                        mod = JMP.TOOL.JsonHelper.Deserialize<BanlModels>(pamentjson);
                    }
                }
                else if (Request.Method == HttpMethod.Post)
                {
                    pamentjson = JMP.TOOL.JsonHelper.Serialize(mod);
                }
                #endregion
                var strjson = "";
                if (mod != null && !string.IsNullOrEmpty(mod.code))
                {
                    #region 判断请求方式调用不同的方式解密
                    //判断请求方式（0为H5,1:sdk）根据不同的方式使用不同的方式解密 
                    switch (mod.type)
                    {
                        case 0: //H5模式 
                            strjson = JMP.TOOL.Encrypt.IndexDecrypt(mod.code);
                            break;
                        case 1: //SDK模式
                            strjson = JMP.TOOL.AesHelper.AesDecrypt(mod.code, ConfigurationManager.AppSettings["encryption"].ToString());
                            break;
                        default:
                            json = json.ToResponse(ErrorCode.Code9999);
                            return Ok(json);
                    }
                    #endregion
                    var mode = JMP.TOOL.JsonHelper.Deserialize<PayBankModels>(strjson);
                    if (mode != null)
                    {
                        #region 根据参数信息调取对应的通道
                        //获取配置文件设置的缓存时间
                        int CacheTime = Int32.Parse(ConfigurationManager.AppSettings["CacheTime"].ToString());
                        JmPayParameter.BankOrder bankOrder = new BankOrder();
                        //调用预下单接口
                        var message = bankOrder.H5OrBankEntrance(mode, CacheTime, ip);
                        //判断是否成功（成功后返回）
                        if (message != null && message.Success == true && !string.IsNullOrEmpty(message.ExtraData.ToString()))
                        {
                            #region 下单成功后根据关联平台和支付方式组装对应的返回数据格式
                            if (mode.paymode == 3 && message.IsJump)
                            {
                                //成功后跳转方式
                                return Redirect(message.ExtraData.ToString());
                            }
                            else if (mode.paymode == 3 && (mode.paytype == "7" || mode.paytype == "6"))
                            {
                                string jsonurl = JMP.TOOL.JsonHelper.Serialize(message.ExtraData);
                                Dictionary<string, object> exdata = JMP.TOOL.JsonHelper.DataRowFromJSON(jsonurl);
                                return Redirect(exdata["ImgQRcode"].ToString());
                            }
                            else
                            {
                                SuccessResponse sucee = new SuccessResponse();
                                sucee.ErrorCode = message.ErrorCode;
                                sucee.Message = message.Message;
                                sucee.ExtraData = message.ExtraData;
                                return Ok(sucee);
                            }
                            #endregion
                        }
                        else
                        {
                            json.ErrorCode = message.ErrorCode;
                            json.Message = message.Message;
                        }
                        #endregion
                    }
                    else
                    {
                        json = json.ToResponse(ErrorCode.Code9999);
                    }
                }
                else
                {
                    json = json.ToResponse(ErrorCode.Code9999);
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.ToString() + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString() + "报错位置：" + e.StackTrace + ",Model:" + JMP.TOOL.JsonHelper.Serialize(mod);//报错信息
                PayApiGlobalErrorLogger.Log("报错信息：" + bcxx, summary: "收银台和H5模式第二次请求发起支付接口PayBank错误信息");
                json = json.ToResponse(ErrorCode.Code103);
            }
            return Ok(json);
        }

        /// <summary>
        /// 初始化接口通道入口
        /// </summary>
        /// <returns></returns>
        [HttpGet, HttpPost]
        public Response Info(JmPayParameter.Models.Initialization mode)
        {

            Response json = new Response();
            string ip = ((System.Web.HttpContextWrapper)Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            try
            {
                #region 获取传入的参数
                Dictionary<string, string> dict = new Dictionary<string, string>();
                string pamentjson = "";
                if (Request.Method == HttpMethod.Get)
                {
                    dict = Request.GetQueryNameValuePairs().ToDictionary(x => x.Key, x => x.Value, StringComparer.CurrentCultureIgnoreCase);
                    if (dict.Count > 0)
                    {
                        pamentjson = JMP.TOOL.JsonHelper.DictJsonstr(dict);
                        mode = JMP.TOOL.JsonHelper.Deserialize<Initialization>(pamentjson);
                    }
                }
                else if (Request.Method == HttpMethod.Post)
                {
                    pamentjson = JMP.TOOL.JsonHelper.Serialize(mode);
                }
                #endregion
                if (mode != null)
                {
                    //获取配置文件设置的缓存时间
                    int CacheTime = Int32.Parse(ConfigurationManager.AppSettings["CacheTime"].ToString());
                    InfoInterface infoInterface = new InfoInterface();
                    var message = infoInterface.InfoPass(mode, pamentjson, CacheTime, ip);
                    json.ErrorCode = message.ErrorCode;
                    json.Message = message.Message;
                }
                else
                {
                    json = json.InfoToResponse(InfoErrorCode.Code9999);
                }
            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString() + "报错位置：" + e.StackTrace;//报错信息
                PayApiGlobalErrorLogger.Log("报错信息：" + bcxx, summary: "初始化接口主通道Info报错");
                json = json.InfoToResponse(InfoErrorCode.Code9998);
            }
            return json;
        }
        /// <summary>
        /// 查询接口通道入口
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        [HttpGet, HttpPost]
        public IHttpActionResult Qusery(JmPayParameter.Models.QueryModels mode)
        {
            Response json = new Response();
            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                string pamentjson = "";
                //获取下单请求的参数
                if (Request.Method == HttpMethod.Get)
                {
                    dict = Request.GetQueryNameValuePairs().ToDictionary(x => x.Key, x => x.Value, StringComparer.CurrentCultureIgnoreCase);
                    if (dict.Count > 0)
                    {
                        pamentjson = JMP.TOOL.JsonHelper.DictJsonstr(dict);
                        mode = JMP.TOOL.JsonHelper.Deserialize<QueryModels>(pamentjson);
                    }
                }
                else if (Request.Method == HttpMethod.Post)
                {
                    pamentjson = JMP.TOOL.JsonHelper.Serialize(mode);
                }
                if (mode != null)
                {
                    //获取配置文件设置的缓存时间
                    int CacheTime = Int32.Parse(ConfigurationManager.AppSettings["CacheTime"].ToString());
                    JmPayParameter.Query query = new Query();
                    var message = query.SelectQuery(mode, pamentjson, CacheTime);
                    if (message.Success)
                    {
                        QuerySuccessResponse qu = new QuerySuccessResponse();
                        qu.ErrorCode = message.ErrorCode;
                        qu.Message = message.Message;
                        qu.trade_code = message.trade_code;
                        qu.trade_no = message.trade_no;
                        qu.trade_price = message.trade_price;
                        qu.o_state = message.o_state;
                        qu.trade_time = message.trade_time;
                        return Ok(qu);
                    }
                    else
                    {
                        json = json.QueryToResponse(QueryErrorCode.Code101);
                    }

                }
                else
                {
                    json = json.QueryToResponse(QueryErrorCode.Code9999);
                }


            }
            catch (Exception e)
            {
                string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString() + "报错位置：" + e.StackTrace;//报错信息
                PayApiGlobalErrorLogger.Log("报错信息：" + bcxx, summary: "查询接口通道入口报错");
                json = json.QueryToResponse(QueryErrorCode.Code101);
            }

            return Ok(json);
        }
    }
}
