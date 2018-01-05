namespace JMP.Model.Query
{
    /// <summary>
    /// 当日应用支付成功衰减实体类
    /// </summary>
    public class AppPaySuccessAttenuation
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
        /// 当日请求总量
        /// </summary>
        public int TodayTotalRequest { get; set; }
        /// <summary>
        /// 当日成功支付量
        /// </summary>
        public int TodayPaySuccess { get; set; }
        /// <summary>
        /// 当日支付成功率(%)
        /// </summary>
        public decimal TodaySuccessRatio { get; set; }
        /// <summary>
        /// 前三天请求总量
        /// </summary>
        public int FirstThreeDaysTotalRequest { get; set; }
        /// <summary>
        /// 前三天成功支付量
        /// </summary>
        public int FirstThreeDaysPaySuccess { get; set; }
        /// <summary>
        /// 前三天支付成功率(%)
        /// </summary>
        public decimal FirstThreeDaysSuccessRatio { get; set; }
        /// <summary>
        /// 成功率衰减值(%)
        /// </summary>
        public decimal? SuccessAttenuation { get; set; }
    }
}
