using System;

namespace JmPay.PayChannelMonitor.Models
{
    /// <summary>
    /// [应用监控:XX分钟支付成功率]的查询实体类
    /// </summary>
    public class AppPaySuccessRatioSinceLatest
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        public int AppId { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        public string AppName { get; set; }
        /// <summary>
        /// 设定的阀值
        /// </summary>
        public decimal ThresholdSuccessRatio { get; set; }
        /// <summary>
        /// 总订单数
        /// </summary>
        public int TotalOrder { get; set; }
        /// <summary>
        /// 总支付数
        /// </summary>
        public int TotalAmount { get; set; }
        /// <summary>
        /// 成功支付数
        /// </summary>
        public int SuccessAmount { get; set; }
        /// <summary>
        /// 成功率
        /// </summary>
        public decimal SuccessRatio { get; set; }
        /// <summary>
        /// 哪一个小时
        /// </summary>
        public int WhichHour { get; set; }
        /// <summary>
        /// 分钟数
        /// </summary>
        public int Minutes { get; set; }
        /// <summary>
        /// 监控类型
        /// </summary>
        public int MonitorType { get; set; }
    }
}
