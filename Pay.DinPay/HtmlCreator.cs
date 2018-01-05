using System.Collections.Generic;
using System.Text;
using System;

namespace Pay.DinPay
{
    /// <summary>
    /// 创建订单HTML表单的类
    /// </summary>
    public class HtmlCreator
    {
        private readonly FormField _formField;
        private string _action;
        private string _signSrc = "";
        private readonly Dictionary<string, string> _fields = new Dictionary<string, string>();
        private readonly IPrivateKeySignInter _privateKeySigner;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="formField">表单类</param>
        /// <param name="privateKeySigner">私钥验证接口类</param>
        public HtmlCreator(FormField formField, IPrivateKeySignInter privateKeySigner)
        {
            if (formField == null)
            {
                throw new ArgumentNullException("支付表单未实例化");
            }
            _formField = formField;
            if (string.IsNullOrEmpty(_formField.FormProperty.Action))
            {
                _formField.FormProperty.Action = "https://pay.dinpay.com/gateway";
            }
            _privateKeySigner = privateKeySigner;
            SetField();
        }

       
        /// <summary>
        /// 组织订单信息并设置私钥签名
        /// </summary>
        private void SetSign()
        {
            //组织订单信息
            if (!string.IsNullOrEmpty(_formField.BankCode))
            {
                _signSrc = _signSrc + "bank_code=" + _formField.BankCode + "&";
            }
            if (!string.IsNullOrEmpty(_formField.ClientIp))
            {
                _signSrc = _signSrc + "client_ip=" + _formField.ClientIp + "&";
            }
            if (!string.IsNullOrEmpty(_formField.ExtendParam))
            {
                _signSrc = _signSrc + "extend_param=" + _formField.ExtendParam + "&";
            }
            if (!string.IsNullOrEmpty(_formField.ExtraReturnParam))
            {
                _signSrc = _signSrc + "extra_return_param=" + _formField.ExtraReturnParam + "&";
            }
            if (!string.IsNullOrEmpty(_formField.Charset))
            {
                _signSrc = _signSrc + "input_charset=" + _formField.Charset + "&";
            }
            if (!string.IsNullOrEmpty(_formField.InterfaceVersion))
            {
                _signSrc = _signSrc + "interface_version=" + _formField.InterfaceVersion + "&";
            }
            if (!string.IsNullOrEmpty(_formField.MerchantCode))
            {
                _signSrc = _signSrc + "merchant_code=" + _formField.MerchantCode + "&";
            }
            if (!string.IsNullOrEmpty(_formField.NotifyUrl))
            {
                _signSrc = _signSrc + "notify_url=" + _formField.NotifyUrl + "&";
            }
            if (!string.IsNullOrEmpty(_formField.OrderAmount))
            {
                _signSrc = _signSrc + "order_amount=" + _formField.OrderAmount + "&";
            }
            if (!string.IsNullOrEmpty(_formField.OrderNo))
            {
                _signSrc = _signSrc + "order_no=" + _formField.OrderNo + "&";
            }
            if (!string.IsNullOrEmpty(_formField.OrderTime))
            {
                _signSrc = _signSrc + "order_time=" + _formField.OrderTime + "&";
            }
            if (!string.IsNullOrEmpty(_formField.PayType))
            {
                _signSrc = _signSrc + "pay_type=" + _formField.PayType + "&";
            }
            if (!string.IsNullOrEmpty(_formField.ProductCode))
            {
                _signSrc = _signSrc + "product_code=" + _formField.ProductCode + "&";
            }
            if (!string.IsNullOrEmpty(_formField.ProductDesc))
            {
                _signSrc = _signSrc + "product_desc=" + _formField.ProductDesc + "&";
            }
            if (!string.IsNullOrEmpty(_formField.ProductName))
            {
                _signSrc = _signSrc + "product_name=" + _formField.ProductName + "&";
            }
            if (!string.IsNullOrEmpty(_formField.ProductNum))
            {
                _signSrc = _signSrc + "product_num=" + _formField.ProductNum + "&";
            }
            if (!string.IsNullOrEmpty(_formField.RedoFlag))
            {
                _signSrc = _signSrc + "redo_flag=" + _formField.RedoFlag + "&";
            }
            if (!string.IsNullOrEmpty(_formField.ReturnUrl))
            {
                _signSrc = _signSrc + "return_url=" + _formField.ReturnUrl + "&";
            }
            if (!string.IsNullOrEmpty(_formField.ServiceType))
            {
                _signSrc = _signSrc + "service_type=" + _formField.ServiceType + "&"; ;
            }
            if (!string.IsNullOrEmpty(_formField.ShowUrl))
            {
                _signSrc = _signSrc + "show_url=" + _formField.ShowUrl + "&"; ;
            }
            _signSrc = _signSrc.Substring(0, _signSrc.Length - 1);
            if (_formField.SignType.ToUpper() == "RSA-S")
            {
                _formField.Sign = _privateKeySigner.Sign(_signSrc);
            }
            else
            {
                //var rsa = new RSAWithHardware();
                //var merPubKeyDir = "D:/1111110166.pfx";   //证书路径
                //var password = "87654321";                //证书密码
                //var rsaWithH = new RSAWithHardware();
                //rsaWithH.Init(merPubKeyDir, password, "D:/dinpayRSAKeyVersion");//初始化
                //var signData = rsaWithH.Sign(_signSrc);    //签名
                //_formField.Sign = signData;
            }
        }

        /// <summary>
        /// 设置智付需要的字段和对应的值
        /// </summary>
        private void SetField()
        {
            SetSign();
            _fields.Add("sign", _formField.Sign);
            _fields.Add("merchant_code", _formField.MerchantCode);
            _fields.Add("bank_code", _formField.BankCode);
            _fields.Add("order_no", _formField.OrderNo);
            _fields.Add("order_amount", _formField.OrderAmount);
            _fields.Add("service_type", _formField.ServiceType);
            _fields.Add("input_charset", _formField.Charset);
            _fields.Add("notify_url", _formField.NotifyUrl);
            _fields.Add("interface_version", _formField.InterfaceVersion);
            _fields.Add("sign_type", _formField.SignType);
            _fields.Add("order_time", _formField.OrderTime);
            _fields.Add("product_name", _formField.ProductName);
            _fields.Add("client_ip", _formField.ClientIp);
            _fields.Add("extend_param", _formField.ExtendParam);
            _fields.Add("extra_return_param", _formField.ExtraReturnParam);
            _fields.Add("product_code", _formField.ProductCode);
            _fields.Add("product_desc", _formField.ProductDesc);
            _fields.Add("product_num", _formField.ProductNum);
            _fields.Add("return_url", _formField.ReturnUrl);
            _fields.Add("show_url", _formField.ShowUrl);
            _fields.Add("redo_flag", _formField.RedoFlag);
            _fields.Add("pay_type", _formField.PayType);
        }

        /// <summary>
        /// 建立请求，以表单HTML形式构造（默认）
        /// </summary>
        /// <returns>提交表单HTML文本</returns>
        public string CreateHtmlForm()
        {
            var sbHtml = new StringBuilder();
            sbHtml.Append("<form id='frmDinPay' name='frmDinPay' action='" + _formField.FormProperty.Action + "?input_charset=" + _formField.Charset.ToUpper() + "' method='" + _formField.FormProperty.Method.ToUpper().Trim() + "'>");
            foreach (var temp in _fields)
            {
                sbHtml.Append("<input type='hidden' name='" + temp.Key + "' value='" + temp.Value + "'/>");
            }
            //submit按钮控件请不要含有name属性
            sbHtml.Append("<input type='submit' value='SUBMIT' style='display:none;'></form>");
            sbHtml.Append("<script>document.forms['frmDinPay'].submit();</script>");
            return sbHtml.ToString();
        }
        /// <summary>
        /// 建立请求，以安卓形式构造
        /// </summary>
        /// <returns>提交json格式</returns>
        public string CreateHtmlAz()
        {
            //待请求参数数组
            Dictionary<string, string> dicPara = new Dictionary<string, string>();
            dicPara = _fields;

            StringBuilder sbHtml = new StringBuilder();

            foreach (KeyValuePair<string, string> temp in dicPara)
            {
                if (!string.IsNullOrEmpty(temp.Value))
                {
                    if (temp.Key == "sign")
                    {
                        sbHtml.Append(",\"" + temp.Key + "\":\"" + temp.Value.Replace("+", "%2B") + "\"");
                    }
                    else
                    {

                        sbHtml.Append(",\"" + temp.Key + "\":\"" + temp.Value + "\"");
                    }
                }
            }
            return "{" + sbHtml.ToString().Substring(1) + "}";
        }
    }
}
