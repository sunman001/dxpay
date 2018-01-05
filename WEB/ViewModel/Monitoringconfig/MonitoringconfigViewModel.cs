using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB.ViewModel.Monitoringconfig
{
    public class MonitoringconfigViewModel
    {
        public MonitoringconfigViewModel()
        {
            WhichHourLists = new List<WhichHourList>();
        }
        /// <summary>
        /// 时间段集合
        /// </summary>
        public List<WhichHourList> WhichHourLists { get; set; }
        /// <summary>
        /// 类型0 成功率 1：请求率
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// 过滤对象[0:全局通道过滤,1:指定通道过滤,2:指定通道池过滤]
        /// </summary>
        public int TargetId { get; set; }
        /// <summary>
        /// 关联ID
        /// </summary>
        public int   RelatedId { get; set; }
        public string RelatedName { get; set; }
        public int Id { get; set; }
        public int IntervalOfRecover { get; set; }


    }
    public class WhichHourList
    {
        /// <summary>
        /// 小时
        /// </summary>
        public int WhichHour { get; set; }

        /// <summary>
        ///阀值
        /// </summary>
        public decimal Threshold { get; set; }

       
       
    }
}