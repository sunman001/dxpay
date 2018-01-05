using System;
using System.Collections.Generic;
using System.Linq;
using JMP.Model.Query;

namespace JmPay.PayChannelMonitor.AuditorDetector
{
    /// <summary>
    /// 应用请求率异常监控器
    /// </summary>
    public class AppRequestDetector
    {
        public event Action<string> OnDoingWork;
        /// <summary>
        /// 当天所有有订单的应用
        /// </summary>
        private List<AppPaySuccessAttenuation> _todayAppCountList;
        private JMP.BLL.jmp_appcount _appCountBll;

        /// <summary>
        /// 受监控的应用集合
        /// </summary>
        private List<JMP.MDL.appmonitor> _appMonitorList;

        public AppRequestDetector()
        {
            _appCountBll = new JMP.BLL.jmp_appcount();
        }

        public void Run()
        {
            DoingWork("正在执行应用请求率异常任务...");
            LoadData();
            if (_todayAppCountList.Count <= 0)
            {
                return;
            }
            var requests = _todayAppCountList.Where(x => x.SuccessAttenuation != null && x.SuccessAttenuation <= -15);
            foreach (var appReq in requests)
            {
                if (appReq.SuccessAttenuation == null)
                {
                    //没有前三天的数据
                    continue;
                }
                if (appReq.TodayTotalRequest < 15)
                {
                    //如果当日总请求量小于20
                    continue;
                }
                var monitor = _appMonitorList.Find(x => x.a_appid == appReq.AppId);
                if (monitor == null)
                {
                    continue;
                }
                if (!(appReq.SuccessAttenuation <= -monitor.a_request * 100)) continue;
                var auditor = new JMP.TOOL.Auditor.JmpAppRequestAuditor(appReq.AppId, appReq.AppName, string.Format("应用【{0}】请求异常:总请求数从前三天的:{1}变化为今日的:{2},成功数从前三天的:{3}变化为今日的:{4},支付成功率从前三天的:{5}%变化为:{6}%,衰减率:{7}%", appReq.AppName, appReq.FirstThreeDaysTotalRequest, appReq.TodayTotalRequest, appReq.FirstThreeDaysPaySuccess, appReq.TodayPaySuccess, appReq.FirstThreeDaysSuccessRatio, appReq.TodaySuccessRatio, Math.Abs((decimal)appReq.SuccessAttenuation)));
                auditor.Add();
            }
        }

        private void LoadData()
        {
            var today = DateTime.Now;
            //var today = new DateTime(2016, 12, 19);
            _todayAppCountList = _appCountBll.GetTodayAppPaySuccessAttenuation(today).ToList();
            _appMonitorList = new JMP.BLL.appmonitor().GetEnabledList();
        }

        protected virtual void DoingWork(string message)
        {
            if (OnDoingWork != null)
            {
                OnDoingWork.Invoke(message);
            }
        }
    }
}
