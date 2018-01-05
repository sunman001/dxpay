namespace JMP.Model.Query.WorkOrder
{
    /// <summary>
    /// 工单系统当前时间的值班人
    /// </summary>
    public class WatcherQuerier
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 真实名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string MobileNumber { get; set; }
    }
}
