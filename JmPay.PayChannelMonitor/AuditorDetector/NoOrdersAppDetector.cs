using System.Collections.Generic;
using System.Linq;
using JmPay.PayChannelMonitor.Models;
using JMP.DBA;
using TOOL;

namespace JmPay.PayChannelMonitor.AuditorDetector
{
    public class NoOrdersAppDetector
    {
        /// <summary>
        /// 指定时间内无订单的应用集合
        /// </summary>
        private readonly List<NoOrdersAppSinceLatest> _noOrderApps;
        private List<JMP.MDL.appmonitor> _appMonitorList;

        public NoOrdersAppDetector()
        {
            _noOrderApps = new List<NoOrdersAppSinceLatest>();
        }
        private void LoadData()
        {
            _appMonitorList = new JMP.BLL.appmonitor().GetEnabledList();
            if (_appMonitorList.Count <= 0)
            {
                return;
            }
            var bll = new JMP.BLL.appmonitor();
            var dt = bll.GetNoOrderApp().Tables[0];

            //最近一次下单时间到当前时间的所有应用的集合
            var noOrderApps = DbHelperSQL.ConvertToList<NoOrdersAppSinceLatest>(dt).ToList();
            foreach (var app in noOrderApps)
            {
                var appMonitor = _appMonitorList.Find(x => x.a_appid == app.AppId);
                if (appMonitor == null)
                {
                    continue;
                }
                var timeRanges = appMonitor.a_time_range.ParseAppMonitorTimeRange();
                var t = timeRanges.Find(x => x.WhichHour == app.WhichHour);
                if (t == null)
                {
                    t = timeRanges.Find(x => x.WhichHour == 100);
                    //if (t == null)
                    //{
                    //    t = new AppMonitorTimeRange();
                    //}
                }
                if (t == null)
                {
                    continue;
                }
                if (app.LatestOrderTimespan > 60 * t.Minutes)
                {
                    _noOrderApps.Add(app);
                }
            }

        }

        /// <summary>
        /// 指定时间内无订单的应用集合
        /// </summary>
        public List<NoOrdersAppSinceLatest> NoOrderApps
        {
            get { return _noOrderApps; }
        }
        public void Run()
        {
            LoadData();
        }
    }
}
