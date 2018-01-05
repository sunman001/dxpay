using JmPayApiServer.Extensions;
using JmPayApiServer.Models;
using JmPayParameter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Net;
using System.IO;
using System.Text;
using System.Drawing;
using DxPay.LogManager.LogFactory.ApiLog;
using System.Net.Http;

namespace JmPayApiServer.Controllers
{
    public class H5Controller : Controller
    {
        /// <summary>
        /// 是否为微信
        /// </summary>
        public bool IsWx
        {
            get
            {
                var userAgent = Request.UserAgent;
                return userAgent != null && userAgent.ToLower().Contains("micromessenger");
            }
        }
        /// <summary>
        /// 是否为移动设备
        /// </summary>
        private bool IsMobile
        {
            get { return Request.Browser.IsMobileDevice; }
        }
        /// <summary>
        /// H5收银台界面
        /// </summary>
        /// <returns></returns>
        public ActionResult checkout()
        {
            string code = string.IsNullOrEmpty(Request["code"]) ? "" : Request["code"];
            if (!string.IsNullOrEmpty(code))
            {
                string json = JMP.TOOL.Encrypt.IndexDecrypt(code);
                CheckoutJsonModel mdoe = JMP.TOOL.JsonHelper.Deserialize<CheckoutJsonModel>(json);

                if (mdoe != null)
                {
                    ViewBag.mode = mdoe;
                    var paymodes = mdoe.paytype.Split(',').Select(x => Convert.ToInt32(x)).ToList();
                    H5ViewModel H5ViewModel = new H5ViewModel
                    {
                        Price = mdoe.price,
                        code = mdoe.code,
                        goodsname = mdoe.goodsname,
                        Parameter = json
                    };
                    var lst = PaymentModeLoader.PaymentModeSource.Where(x => paymodes.Any(p => p == x.PayType)).ToList();
                    foreach (var t in lst)
                    {
                        t.Price = H5ViewModel.Price;
                        t.Enabled = t.PaymentEnableDetect(IsWx, IsMobile);
                        t.encryption = EncryptionStr(mdoe, t.PayType);
                    }
                    H5ViewModel.PaymentModes = lst.OrderByDescending(x => x.Enabled).ThenBy(x => x.Category).ToList();
                    ViewBag.H5ViewModel = H5ViewModel;
                }
                else
                {
                    Response.Redirect("/H5/Error");
                }
            }
            else
            {
                Response.Redirect("/H5/Error");
            }
            return View();
        }
        /// <summary>
        /// 封装加密字符串
        /// </summary>
        /// <returns></returns>
        private static string EncryptionStr(CheckoutJsonModel mode, int PayType)
        {
            string jsonStr = "";
            mode.paytype = PayType.ToString();
            string encrypt = JMP.TOOL.JsonHelper.Serialize(mode);
            jsonStr = JMP.TOOL.Encrypt.IndexEncrypt(encrypt);
            return jsonStr;
        }
        /// <summary>
        /// 错误提示界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Error()
        {
            return View();
        }

        /// <summary>
        /// from表单提交跳转界面
        /// </summary>
        /// <returns></returns>
        public ActionResult UnionPay()
        {

            string UnionPay = string.IsNullOrEmpty(Request["UnionPay"]) ? "" : Request["UnionPay"];
            if (!string.IsNullOrEmpty(UnionPay))
            {
                string str = "";
                string encryption = HttpUtility.UrlDecode(Request.Params["UnionPay"].ToString());
                string h5key = JMP.TOOL.Encrypt.IndexDecrypt(encryption);//解密缓存key
                if (JMP.TOOL.CacheHelper.IsCache(h5key))
                {
                    str = JMP.TOOL.CacheHelper.GetCaChe(h5key).ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        Response.Write(str);
                    }
                    else
                    {
                        PayApiGlobalErrorLogger.Log("from表单提交报错，根据缓存值未获取到相关信息！获取到的缓存值：" + UnionPay, summary: "from表单提交报错");
                        Response.Write("{\"Message\":\"订单超时\",\"ErrorCode\":9984}");
                    }
                }
                else
                {
                    PayApiGlobalErrorLogger.Log("from表单提交报错，根据获取到的值解密失败！获取到的缓存值：" + UnionPay, summary: "from表单提交报错");
                    Response.Write("{\"Message\":\"订单超时\",\"ErrorCode\":9984}");
                }
            }
            else
            {
                Response.Redirect("/H5/Error");
            }
            return View();
        }

        /// <summary>
        /// 二维码展示界面
        /// </summary>
        /// <returns></returns>
        public ActionResult QRcode()
        {
            string qrcode = string.IsNullOrEmpty(Request["QRcode"]) ? "" : Request["QRcode"];
            try
            {
                if (!string.IsNullOrEmpty(qrcode))
                {
                    qrcode = JMP.TOOL.Encrypt.IndexDecrypt(qrcode);
                    DateTime tims = DateTime.Parse(qrcode.Split(',')[1]).AddMinutes(30);//默认添加30分钟
                    string qcode = qrcode.Split(',')[0];
                    if (tims >= DateTime.Now)
                    {
                        Bitmap bmp = JMP.TOOL.QRcode.GetQRCodeBmp(qcode);//生成二维码
                        MemoryStream ms = new MemoryStream();
                        bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);//转换流输出
                        byte[] byteImage = new Byte[ms.Length];
                        byteImage = ms.ToArray();
                        Response.ClearContent();//直接输出流文件
                        Response.ContentType = "image/Gif";
                        Response.BinaryWrite(ms.ToArray());//这里的Write改成BinaryWrite即可
                    }
                    else
                    {
                        Response.Write("二维码已失效！");
                    }
                }
            }
            catch
            {

                Response.Write("参数有误！");
            }
            return View();
        }
        /// <summary>
        /// 二维码图片带样式展示界面
        /// </summary>
        /// <returns></returns>
        public ActionResult ImgQRcode()
        {
            string qrcode = string.IsNullOrEmpty(Request["QRcode"]) ? "" : Request["QRcode"];
            string message = "非法访问";
            string Name = "";
            try
            {
                if (!string.IsNullOrEmpty(qrcode))
                {
                    qrcode = JMP.TOOL.Encrypt.IndexDecrypt(qrcode);
                    DateTime tims = DateTime.Parse(qrcode.Split(',')[1]).AddMinutes(30);//默认添加30分钟
                    string qcode = qrcode.Split(',')[0];
                    switch (qrcode.Split(',')[2])
                    {
                        case "1":
                            Name = "支付宝";
                            break;
                        case "2":
                            Name = "微信";
                            break;
                        case "3":
                            Name = "QQ钱包扫码";
                            break;
                        default:
                            Name = "支付宝";
                            break;
                    }
                    //Name = !string.IsNullOrEmpty(qrcode.Split(',')[2]) && qrcode.Split(',')[2] == "2" ? "微信" : "支付宝";
                    if (tims >= DateTime.Now)
                    {
                        Bitmap bmp = JMP.TOOL.QRcode.GetQRCodeBmp(qcode);//生成二维码
                        MemoryStream ms = new MemoryStream();
                        bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);//转换流输出
                        byte[] byteImage = new Byte[ms.Length];
                        byteImage = ms.ToArray();
                        message = "<img src =\"data:image/jpeg;base64," + Convert.ToBase64String(byteImage) + "\" />";
                    }
                    else
                    {
                        message = "二维码已失效！";
                    }
                }
            }
            catch
            {

                message = "参数有误！";
            }

            ViewBag.Name = Name;
            ViewBag.message = message;
            return View();
        }

        /// <summary>
        /// 封装跳转界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Jump()
        {
            string URL = string.IsNullOrEmpty(Request["url"]) ? null : Request["url"];
            string jmtype = string.IsNullOrEmpty(Request["jmtype"]) ? null : Request["jmtype"];
            Dictionary<string, string> jsonlist = JMP.TOOL.UrlStr.GetRequestGet(System.Web.HttpContext.Current, "域名跳转接口");
            if (jsonlist.Count <= 0)
            {
                jsonlist = JMP.TOOL.UrlStr.GetRequestfrom(System.Web.HttpContext.Current, "域名跳转接口");
            }
            if (!string.IsNullOrEmpty(URL) && jsonlist.Count > 0)
            {
                jsonlist.Remove("url");
                jsonlist.Remove("jmtype");
                if (jmtype == "HY")
                {
                    if (jsonlist.ContainsKey("meta_option"))
                    {
                        jsonlist["meta_option"] = Convert.ToBase64String(System.Text.Encoding.GetEncoding("GBK").GetBytes(jsonlist["meta_option"].Trim()));
                    }
                    if (jsonlist.ContainsKey("goods_name"))
                    {
                        jsonlist["goods_name"] = HttpUtility.UrlEncode(jsonlist["goods_name"], Encoding.GetEncoding("gb2312"));
                    }
                    if (jsonlist.ContainsKey("goods_note"))
                    {
                        jsonlist["goods_note"] = HttpUtility.UrlEncode(jsonlist["goods_note"], Encoding.GetEncoding("gb2312"));
                    }
                }
                string RequestUrl = "";
                RequestUrl = URL + "?" + JMP.TOOL.UrlStr.getstr(jsonlist);
                if (Request.HttpMethod == HttpMethod.Get.ToString())
                {
                    Response.Redirect(RequestUrl);

                }
                else
                {
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(RequestUrl);    //创建一个请求示例
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();  //获取响应，即发送请求
                    Stream responseStream = response.GetResponseStream();
                    string srcString = "";
                    if (jmtype == "HY")
                    {
                        StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("GB2312"));
                        srcString = streamReader.ReadToEnd();
                    }
                    else
                    {
                        StreamReader streamReader = new StreamReader(responseStream);
                        srcString = streamReader.ReadToEnd();
                    }
                    Response.Write(srcString);
                }
            }
            else
            {
                Response.Write("非法请求");
            }

            return View();
        }
    }
}
