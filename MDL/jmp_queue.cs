using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMP.Model;

namespace JMP.MDL
{
    ///<summary>
    ///订单通知队列表
    ///</summary>
    public class jmp_queue
    {
        /// <summary>
        /// id主键
        /// </summary>	
        [EntityTracker(Label = "id主键", Description = "id主键")]
        public int q_id { get; set; }

        /// <summary>
        /// 订单表编号
        /// </summary>		
        [EntityTracker(Label = "订单表编号", Description = "订单表编号")]
        public string q_order_code { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        [EntityTracker(Label = "商户订单号", Description = "商户订单号")]
        public string q_bizcode { get; set; }

        /// <summary>
        /// 通知地址
        /// </summary>	
        [EntityTracker(Label = "通知地址", Description = "通知地址")]
        public string q_address { get; set; }

        /// <summary>
        /// 应用密匙
        /// </summary>	
        [EntityTracker(Label = "应用密匙", Description = "应用密匙")]
        public string q_sign { get; set; }

        /// <summary>
        /// 通知状态(0:创建，1：通知成功，-1通知失败)
        /// </summary>		
        [EntityTracker(Label = "通知状态", Description = "通知状态")]
        public int q_noticestate { get; set; }

        /// <summary>
        /// 通知次数
        /// </summary>
        [EntityTracker(Label = "通知次数", Description = "通知次数")]
        public int q_times { get; set; }

        /// <summary>
        /// 通知时间
        /// </summary>	
        [EntityTracker(Label = "通知时间", Description = "通知时间")]
        public DateTime q_noticetimes { get; set; }

        /// <summary>
        /// 更新表名
        /// </summary>		
        [EntityTracker(Label = "更新表名", Description = "更新表名")]
        public string q_tablename { get; set; }

        /// <summary>
        /// 商户私有信息
        /// </summary>
        [EntityTracker(Label = "商户私有信息", Description = "商户私有信息")]
        public string q_privateinfo { get; set; }
    }
}
