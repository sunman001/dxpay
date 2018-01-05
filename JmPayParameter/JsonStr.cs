using JmPayParameter.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxPay.LogManager.LogFactory.ApiLog;

namespace JmPayParameter
{
    public class JsonStr
    {
        /// <summary>
        /// H5模式返回加密后的请求地址
        /// </summary>
        /// <param name="mode">参数实体</param>
        /// <param name="ip">ip地址</param>
        /// <returns></returns>
        public  InnerResponse H5JsonStr(PayBankModels mode, string ip)
        {
            InnerResponse Inn = new InnerResponse();
            try
            {
                string json = JMP.TOOL.JsonHelper.Serialize(mode);
                Inn = Inn.ToResponse(ErrorCode.Code100);
                Inn.ExtraData = ConfigurationManager.AppSettings["H5PayUrl"].ToString() + "?code=" + JMP.TOOL.Encrypt.IndexEncrypt(json);
            }
            catch (Exception e)
            {
                //string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString() + "报错位置：" + e.StackTrace.ToString();//报错信息
                //JMP.TOOL.AddLocLog.AddLog(1, 4, ip, "H5模式返回加密后的请求地址接口错误信息", "报错信息：" + bcxx);//写入报错日志             
                PayApiGlobalErrorLogger.Log("报错信息103：" + e.ToString(),summary: "H5模式返回加密后的请求地址接口错误信息");
                Inn = Inn.ToResponse(ErrorCode.Code103);
            }
            return Inn;
        }
        /// <summary>
        /// 收银台H5模式
        /// </summary>
        /// <param name="mode">参数实体</param>
        /// <param name="ip">ip地址</param>
        /// <returns></returns>
        public  InnerResponse BankJsonStr(PayBankModels mode, string ip)
        {
            InnerResponse Inn = new InnerResponse();
            try
            {
                string json = JMP.TOOL.JsonHelper.Serialize(mode);
                Inn = Inn.ToResponse(ErrorCode.Code100);
                Inn.ExtraData = ConfigurationManager.AppSettings["H5BankUrl"].ToString() + "?code=" + JMP.TOOL.Encrypt.IndexEncrypt(json);
            }
            catch (Exception e)
            {
                //string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString() + "报错位置：" + e.StackTrace.ToString();//报错信息
                //JMP.TOOL.AddLocLog.AddLog(1, 4, ip, "收银台H5模式返回加密地址接口错误信息", "报错信息：" + bcxx);//写入报错日志 
                PayApiGlobalErrorLogger.Log("报错信息103：" + e.ToString(),summary: "收银台H5模式返回加密地址接口错误信息");
                Inn = Inn.ToResponse(ErrorCode.Code103);
            }
            return Inn;
        }
        /// <summary>
        /// sdk收银台模式返回数据组装
        /// </summary>
        /// <param name="mode">参数实体</param>
        /// <param name="ip">ip地址</param>
        /// <returns></returns>
        public  InnerResponse BankSdk(PayBankModels mode, string ip)
        {
            InnerResponse Inn = new InnerResponse();
            try
            {
                string json = JMP.TOOL.JsonHelper.Serialize(mode);
                Inn = Inn.ToResponse(ErrorCode.Code100);
                Inn.ExtraData = JMP.TOOL.AesHelper.AesEncrypt(json, ConfigurationManager.AppSettings["encryption"].ToString());
            }
            catch (Exception e)
            {
                //string bcxx = "报错提示" + e.Message + "报错对象：" + e.Source + "报错方法：" + e.TargetSite + "报错信息：" + e.ToString() + "报错位置：" + e.StackTrace.ToString();//报错信息
                //JMP.TOOL.AddLocLog.AddLog(1, 4, ip, "收银台sdk模式返回加密地址接口错误信息", "报错信息：" + bcxx);//写入报错日志 
                PayApiGlobalErrorLogger.Log("报错信息103：" + e.ToString(), summary: "收银台sdk模式返回加密地址接口错误信息");
                Inn = Inn.ToResponse(ErrorCode.Code103);
            }
            return Inn;
        }

        /// <summary>
        /// 组装参数实体
        /// </summary>
        /// <param name="code">订单编号</param>
        /// <param name="goodsname">商品名称</param>
        /// <param name="price">金额</param>
        /// <param name="paytype">支付类型</param>
        /// <param name="apptype">应用类型子id</param>
        /// <param name="paymode">关联平台（1:安卓，2:苹果，3:H5）</param>
        /// <returns></returns>
        public  PayBankModels ParameterEntity(string code, string goodsname, decimal price, string paytype, int apptype, int paymode)
        {
            PayBankModels modes = new PayBankModels();
            modes.code = code;
            modes.goodsname = goodsname;
            modes.price = price;
            modes.paytype = paytype;
            modes.apptype = apptype;
            modes.paymode = paymode;
            string sin = (modes.code + modes.price);
            modes.sign = JMP.TOOL.MD5.md5strGet(sin, true).ToUpper();
            return modes;
        }


    }
}
