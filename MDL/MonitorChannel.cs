using System;

namespace JMP.Model
{
    /// <summary>
    /// 通道监控表
    /// </summary>
    public class MonitorChannel
    {
        /// <summary>
        /// 通道监控表
        /// </summary>
        public MonitorChannel()
        {
            StartDay = -1;
            EndDay = -1;
            DayMinute = 5;
            StartNight = -1;
            EndNight = -1;
            NightMinute = 5;
            OtherMinte = 5;
        }
        /// <summary>
        /// 主键
        /// </summary>		
        [EntityTracker(Label = "主键", Description = "主键")]
        public int a_id { get; set; }


        [EntityTracker(Ignore = true)]
        public string a_name { get; set; }

        /// <summary>
        /// 通道id
        /// </summary>		
        [EntityTracker(Label = "通道id", Description = "通道id")]
        public  int   ChannelId { get; set; }

        [EntityTracker(Ignore = true)]
        public string a_appidList { get; set; }

        /// <summary>
        /// 请求率
        /// </summary>		
        [EntityTracker(Label = "请求率", Description = "请求率")]
        public decimal Threshold { get; set; }

        /// <summary>
        /// 白天开始时间段
        /// </summary>
        [EntityTracker(Label = "白天开始时间段", Ignore = true)]
        public int StartDay { get; set; }

        /// <summary>
        /// 白天结束时间段
        /// </summary>
        [EntityTracker(Label = "白天结束时间段", Ignore = true)]
        public int EndDay { get; set; }

        /// <summary>
        /// 白天分钟数
        /// </summary>
        [EntityTracker(Label = "白天分钟数", Ignore = true)]
        public int DayMinute { get; set; }

        /// <summary>
        /// 晚上开始时间段
        /// </summary>
        [EntityTracker(Label = "晚上开始时间段", Ignore = true)]
        public int StartNight { get; set; }

        /// <summary>
        /// 晚上结束时间段
        /// </summary>
        [EntityTracker(Label = "晚上结束时间段", Ignore = true)]
        public int EndNight { get; set; }

        /// <summary>
        /// 晚上分钟数
        /// </summary>
        [EntityTracker(Label = "晚上分钟数", Ignore = true)]
        public int NightMinute { get; set; }

        /// <summary>
        /// 其他分钟数
        /// </summary>
        [EntityTracker(Label = "其他分钟数", Ignore = true)]
        public int OtherMinte { get; set; }

        /// <summary>
        /// 分钟数
        /// </summary>	
        [EntityTracker(Label = "分钟数", Description = "分钟数")]
        public int a_minute { get; set; }

        /// <summary>
        /// 状态（0:正常，1:冻结）
        /// </summary>		
        [EntityTracker(Label = "状态", Description = "状态")]
        public int a_state { get; set; }

        /// <summary>
        /// 录入时间
        /// </summary>		
        [EntityTracker(Label = "录入时间", Description = "录入时间")]
        public DateTime a_datetime { get; set; }

        /// <summary>
		/// 时间区域和对应的分钟数
        /// </summary>		
		private string _a_time_range;

        /// <summary>
        /// 通道监控类型[20:XX分钟无成功订单]
        /// </summary>
        [EntityTracker(Label = "通道监控类型", Description = "通道监控类型")]
        public int a_type { get; set; }

        /// <summary>
        /// 时间区域和对应的分钟数
        /// </summary>
        [EntityTracker(Label = "时间区域和对应的分钟数", Description = "时间区域和对应的分钟数")]
        public string a_time_range
        {
            get { return _a_time_range; }
            set { _a_time_range = value; }
        }
    }
}
