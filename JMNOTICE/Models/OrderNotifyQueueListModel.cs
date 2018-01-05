using System;

namespace JMNOTICE.Models
{
    /// <summary>
    /// 订单通知实体类
    /// </summary>
    public class OrderNotifyQueueListModel
    {
        public int  q_id { get; set; }
        public int q_times { get; set; }
        public string q_address { get; set; }
        public string q_sign { get; set; }
        public int q_noticestate { get; set; }
        public string q_tablename { get; set; }
        public int q_o_id { get; set; }
        public int trade_type { get; set; }
        public DateTime? trade_time { get; set; }
        public decimal trade_price { get; set; }
        public string trade_paycode { get; set; }
        public string trade_code { get; set; }
        public string trade_no { get; set; }
        public string q_privateinfo { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int q_uersid { get; set; }
    }
}
