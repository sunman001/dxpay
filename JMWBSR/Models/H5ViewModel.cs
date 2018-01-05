using System.Collections.Generic;
using System.Configuration;

namespace JMWBSR.Models
{
    /// <summary>
    /// H5收银台视图实体类
    /// </summary>
    public class H5ViewModel
    {
        public H5ViewModel()
        {
            PaymentModes = new List<PaymentMode>();
        }

        private readonly string _payApiBaseUrl = ConfigurationManager.AppSettings["PAY_API_BASE_URL"];
        /// <summary>
        /// 应支付的总价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 支付参数
        /// </summary>
        public string Parameter { get; set; }
        /// <summary>
        /// 支付提交基地址
        /// </summary>
        public string PayApiBaseUrl { get { return _payApiBaseUrl; } }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string goodsname { get; set; }
        /// <summary>
        /// 支付类型集合
        /// </summary>
        public List<PaymentMode> PaymentModes { get; set; }
    }
    /// <summary>
    /// 对付类型实体对象
    /// </summary>
    public class PaymentMode
    {
        public PaymentMode()
        {
            Enabled = true;
            PayType = 1;
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// CSS类名
        /// </summary>
        public string ClsName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 跳转地址
        /// </summary>
        public string RedirectUrl { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 支付类型
        /// </summary>
        public int PayType { get; set; }
        /// <summary>
        /// 是否检测微信
        /// </summary>
        public bool CheckWeiChat { get; set; }
        /// <summary>
        /// 是否检测移动设备
        /// </summary>
        public bool CheckMobileDevice { get; set; }
        /// <summary>
        /// 分类[0:支付宝,1:微信,2:银联]
        /// </summary>
        public int Category { get; set; }
    }

    /// <summary>
    /// 支付类型数据源加载器
    /// </summary>
    public class PaymentModeLoader
    {
        private static readonly List<PaymentMode> PaymentModes = new List<PaymentMode>(new[]
        {
            new PaymentMode
            {
                ClsName = "ali",
                Description = "安全快捷，官方推荐",
                Enabled = true,
                Name = "支付宝",
                RedirectUrl = "",
                Category=0
            },
            new PaymentMode
            {
                ClsName = "wx",
                Description = "",
                Enabled = true,
                Name = "微信支付",
                PayType=2,
                RedirectUrl = "",
                CheckWeiChat = false,
                CheckMobileDevice=true,
                Category=2
            },
            new PaymentMode
            {
                ClsName = "up",
                Description = "",
                Enabled = true,
                Name = "银联卡",
                PayType=3,
                RedirectUrl = "",
                Category=2
            },
            new PaymentMode
            {
                ClsName = "wx",
                Description = "",
                Enabled = true,
                Name = "微信公众号",
                PayType=4,
                RedirectUrl = "",
                CheckWeiChat=true,
                Category=1
            },
            new PaymentMode
            {
                ClsName = "wx",
                Description = "",
                Enabled = true,
                Name = "微信APP",
                PayType=5,
                RedirectUrl = "",
                CheckWeiChat=true,
                Category=1
            },
            new PaymentMode
            {
                ClsName = "wx",
                Description = "",
                Enabled = true,
                Name = "微信扫码",
                PayType=6,
                RedirectUrl = "",
                CheckWeiChat=false,
                Category=1
            },
            new PaymentMode
            {
                ClsName = "ali",
                Description = "",
                Enabled = true,
                Name = "支付宝扫码",
                PayType=7,
                RedirectUrl = "",
                Category=0
            }

        });

        /// <summary>
        /// 读取所有的支付类型集合
        /// </summary>
        public static List<PaymentMode> PaymentModeSource
        {
            get { return PaymentModes; }
        }
    }
}