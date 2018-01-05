using System;
namespace Pay.DinPay
{
    /// <summary>
    /// 表单值实体类
    /// </summary>
    public class FormField
    {
        private string _merchantCode;
        private string _orderNo;
        private string _orderAmount;
        private string _serviceType;
        private string _charset;
        private string _notifyUrl;
        private string _interfaceVersion;
        private string _signType;
        private string _orderTime;
        private string _productName;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="merchantCode">商家号</param>
        /// <param name="orderNo">订单编号</param>
        /// <param name="orderAmount">订单金额</param>
        /// <param name="serviceType">服务类型,可选值[direct_pay,b2b_pay],默认:direct_pay</param>
        /// <param name="charset">参数编码字符集,默认:UTF-8</param>
        /// <param name="notifyUrl">服务器异步通知地址</param>
        /// <param name="signType">签名方式,RSA-S或者RSA,默认:RSA-S</param>
        /// <param name="orderTime">订单时间</param>
        /// <param name="productName">商品名称</param>
        /// <param name="formProperty">表单属性对象</param>
        public FormField(string merchantCode, string orderNo, string orderAmount, string serviceType, string charset, string notifyUrl, string signType, string orderTime, string productName, FormProperty formProperty)
        {
            FormProperty = formProperty;
            _signType = "RSA-S";
            _interfaceVersion = "V3.0";
            if (string.IsNullOrEmpty(merchantCode))
            {
                throw new ArgumentNullException("商家号为空");
            }
            _merchantCode = merchantCode;
            if (string.IsNullOrEmpty(orderNo))
            {
                throw new ArgumentNullException("订单编号为空");
            }
            _orderNo = orderNo;
            if (string.IsNullOrEmpty(orderAmount))
            {
                throw new ArgumentNullException("订单金额为空");
            }
            _orderAmount = orderAmount;
            _serviceType = "direct_pay";
            if (serviceType.Length > 0)
            {
                _serviceType = serviceType;
            }
            _charset = "UTF-8";
            if (charset.Length > 0)
            {
                _charset = charset;
            }
            if (string.IsNullOrEmpty(notifyUrl))
            {
                throw new ArgumentNullException("服务器异步通知地址为空");
            }
            _notifyUrl = notifyUrl;
            if (signType.Length > 0)
            {
                _signType = signType;
            }
            if (string.IsNullOrEmpty(orderTime))
            {
                throw new ArgumentNullException("订单时间为空");
            }
            _orderTime = orderTime;
            if (string.IsNullOrEmpty(productName))
            {
                throw new ArgumentNullException("商品名称为空");
            }
            _productName = productName;
        }

        /// <summary>
        /// 构造函数安卓和ios调用
        /// </summary>
        /// <param name="merchantCode">商家号</param>
        /// <param name="orderNo">订单编号</param>
        /// <param name="orderAmount">订单金额</param>
        /// <param name="charset">参数编码字符集,默认:UTF-8</param>
        /// <param name="notifyUrl">服务器异步通知地址</param>
        /// <param name="signType">签名方式,RSA-S或者RSA,默认:RSA-S</param>
        /// <param name="orderTime">订单时间</param>
        /// <param name="productName">商品名称</param>
        /// <param name="formProperty">表单属性对象</param>
        public FormField(string merchantCode, string orderNo, string orderAmount, string notifyUrl, string signType, string orderTime, string productName, FormProperty formProperty)
        {
            FormProperty = formProperty;
            _signType = "RSA-S";
            _interfaceVersion = "V3.0";
            if (string.IsNullOrEmpty(merchantCode))
            {
                throw new ArgumentNullException("商家号为空");
            }
            _merchantCode = merchantCode;
            if (string.IsNullOrEmpty(orderNo))
            {
                throw new ArgumentNullException("订单编号为空");
            }
            _orderNo = orderNo;
            if (string.IsNullOrEmpty(orderAmount))
            {
                throw new ArgumentNullException("订单金额为空");
            }
            _orderAmount = orderAmount;
            //_serviceType = "direct_pay";
            //if (serviceType.Length > 0)
            //{
            //    _serviceType = serviceType;
            //}
            //_charset = "UTF-8";
            //if (charset.Length > 0)
            //{
            //    _charset = charset;
            //}
            if (string.IsNullOrEmpty(notifyUrl))
            {
                throw new ArgumentNullException("服务器异步通知地址为空");
            }
            _notifyUrl = notifyUrl;
            if (signType.Length > 0)
            {
                _signType = signType;
            }
            if (string.IsNullOrEmpty(orderTime))
            {
                throw new ArgumentNullException("订单时间为空");
            }
            _orderTime = orderTime;
            if (string.IsNullOrEmpty(productName))
            {
                throw new ArgumentNullException("商品名称为空");
            }
            _productName = productName;
        }

        /// <summary>
        /// 签名字符串(必填)
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 商家号(必填)
        /// </summary>
        public string MerchantCode
        {
            get { return _merchantCode; }
            set
            {
                _merchantCode = value;
            }
        }
        /// <summary>
        /// 银行标识
        /// </summary>
        public string BankCode { get; set; }
        /// <summary>
        /// 订单编号(必填)
        /// </summary>
        public string OrderNo
        {
            get { return _orderNo; }
            set
            {
                _orderNo = value;
            }
        }
        /// <summary>
        /// 订单金额(必填)
        /// </summary>
        public string OrderAmount
        {
            get { return _orderAmount; }
            set
            {
                _orderAmount = value;
            }
        }
        /// <summary>
        /// 服务类型(必填),可选值[direct_pay,b2b_pay],默认:direct_pay
        /// </summary>
        public string ServiceType
        {
            get { return _serviceType; }
            set
            {
                _serviceType = value;
            }
        }
        /// <summary>
        /// 参数编码字符集(必填),默认:UTF-8
        /// </summary>
        public string Charset
        {
            get { return _charset; }
            set
            {
                _charset = value;
            }
        }
        /// <summary>
        /// 服务器异步通知地址(必填)
        /// </summary>
        public string NotifyUrl
        {
            get { return _notifyUrl; }
            set
            {
                _notifyUrl = value;
            }
        }
        /// <summary>
        /// 接口版本(必填),默认:V3.0
        /// </summary>
        public string InterfaceVersion
        {
            get { return _interfaceVersion; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("接口版本为空");
                }
                _interfaceVersion = value;
            }
        }
        /// <summary>
        /// 签名方式(必填),RSA-S或者RSA
        /// </summary>
        public string SignType
        {
            get { return _signType; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("签名方式为空");
                }
                _signType = value;
            }
        }
        /// <summary>
        /// 订单时间(必填)
        /// </summary>
        public string OrderTime
        {
            get { return _orderTime; }
            set
            {

                _orderTime = value;
            }
        }
        /// <summary>
        /// 商品名称(必填)
        /// </summary>
        public string ProductName
        {
            get { return _productName; }
            set
            {
                _productName = value;
            }
        }
        /// <summary>
        /// 客户端IP
        /// </summary>
        public string ClientIp { get; set; }
        /// <summary>
        /// 公用业务扩展参数
        /// </summary>
        public string ExtendParam { get; set; }
        /// <summary>
        /// 公用回传参数
        /// </summary>
        public string ExtraReturnParam { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string ProductDesc { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public string ProductNum { get; set; }
        /// <summary>
        /// 页面跳转同步通知地址
        /// </summary>
        public string ReturnUrl { get; set; }
        /// <summary>
        /// 商品展示URL
        /// </summary>
        public string ShowUrl { get; set; }
        /// <summary>
        /// 是否允许重复订单
        /// </summary>
        public string RedoFlag { get; set; }
        /// <summary>
        /// 支付类型
        /// </summary>
        public string PayType { get; set; }

        public FormProperty FormProperty { get; set; }
    }
}