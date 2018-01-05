using System;
using TOOL;

namespace JMNOTICE.Util
{
    /// <summary>
    /// 全局参数
    /// </summary>
    public class GlobalPara
    {
        /// <summary>
        /// 程序启动时间
        /// </summary>
        public static DateTime ApplicationStartTime { get; set; }
        /// <summary>
        /// 成功数
        /// </summary>
        public static int SuccessCount { get; private set; }
        /// <summary>
        /// 请求数
        /// </summary>
        public static int RequestCount { get; private set; }
        /// <summary>
        /// 成功率
        /// </summary>
        public static double SuccessRatio
        {
            get
            {
                if (RequestCount == 0 || SuccessCount == 0)
                {
                    return 0d;
                }
                return SuccessCount * 1.0 / RequestCount * 100;
            }
        }
        /// <summary>
        /// 第一次通知时间
        /// </summary>
        private static DateTime? FirstNotifyTime { get; set; }
        /// <summary>
        /// 最近一次通知时间
        /// </summary>
        private static DateTime? LatestNotifyTime { get; set; }

        public static string Info
        {
            get
            {
                var result = "";
                try
                {
                    result = string.Format("已发送通知请求{0}个,成功通知{1}个,成功率{2:f2}%\r\n最近发起通知时间:\r\n{3} [{4}]", RequestCount,
                        SuccessCount, SuccessRatio, LatestNotifyTime == null ? "无" : LatestNotifyTime.ToString(),
                        LatestNotifyTime == null ? "无" : Convert.ToDateTime(LatestNotifyTime).TimeAgo());
                }
                catch
                {
                    result = string.Format("已发送通知请求{0}个,成功通知{1}个,成功率{2:f2}%\r\n最近发起通知时间:{3}", RequestCount,
                        SuccessCount, SuccessRatio, LatestNotifyTime == null ? "无" : LatestNotifyTime.ToString());
                }
                result += "\r\n任务执行次数累加总数:" + GlobalConfig.TaskExecuteCount;
                return result;
            }
        }
        /// <summary>
        /// 发起通知请求调用的方法
        /// </summary>
        public static void Request()
        {
            if (FirstNotifyTime == null)
            {
                FirstNotifyTime = DateTime.Now;
            }
            RequestCount = RequestCount + 1;
            LatestNotifyTime = DateTime.Now;
        }

        /// <summary>
        /// 通知成功调用的方法
        /// </summary>
        public static void RequestSuccess()
        {
            LatestNotifyTime = DateTime.Now;
            SuccessCount = SuccessCount + 1;
        }
    }
}
