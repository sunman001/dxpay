using System;
using System.Collections.Generic;
using System.Linq;

namespace TOOL
{
    public static class DateTimeHelper
    {
        public static string TimeAgo(this DateTime dt, string suffix = "前")
        {
            DateTime dt1;
            try
            {
                dt1 = Convert.ToDateTime(dt);
            }
            catch (Exception)
            {
                return "--";
            }
            var ts = new TimeSpan(DateTime.Now.Ticks - dt1.Ticks);
            var delta = Math.Abs(ts.TotalSeconds);

            if (delta < 60)
            {
                return ts.Seconds == 0 ? "刚刚" : ts.Seconds + "秒" + suffix;
            }
            if (delta < 120)
            {
                return ts.Seconds == 0 ? "1分钟" + suffix : "1分" + ts.Seconds + "秒" + suffix;
            }
            if (delta < 2700) // 45 * 60
            {
                return ts.Seconds == 0 ? ts.Minutes + "分钟" + suffix : ts.Minutes + "分" + ts.Seconds + "秒" + suffix;
            }
            if (delta < 5400) // 90 * 60
            {
                return ts.Hours > 0 ? ts.Hours + "小时" + ts.Minutes + "分" + ts.Seconds + "秒" + suffix : "" + ts.Minutes + "分" + ts.Seconds + "秒" + suffix;
            }
            if (delta < 86400) // 24 * 60 * 60
            {
                return ts.Hours + "小时" + ts.Minutes + "分" + ts.Seconds + "秒" + suffix;
            }
            if (delta < 172800) // 48 * 60 * 60
            {
                return ts.Days > 0 ? string.Format("{0}天{1}小时{2}分{3}秒{4}", ts.Days, ts.Hours, ts.Minutes, ts.Seconds, suffix) : string.Format("{0}小时{1}分{2}秒{3}", ts.Hours, ts.Minutes, ts.Seconds, suffix);
            }
            if (delta < 2592000) // 30 * 24 * 60 * 60
            {
                return string.Format("{0}天{1}小时{2}分{3}秒{4}", ts.Days, ts.Hours, ts.Minutes, ts.Seconds, suffix);
            }
            if (delta < 31104000) // 12 * 30 * 24 * 60 * 60
            {
                var months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months + "月" + (ts.Days > 0 ? ts.Days + "天" : "") + suffix;
            }
            var years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years + "年" + suffix;
        }

        /// <summary>
        /// 获取传入日期的两位小时部分(不足两位的左补零),如:传入2016-05-26 1:20:00,返回:01
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string WhichHour(this DateTime date)
        {
            return date.ToString("HH");
        }
        /// <summary>
        /// 获取传入日期的两位分钟部分(不足两位的左补零),如:传入2016-05-26 1:5:00,返回:05
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string WhichMinute(this DateTime date)
        {
            return date.ToString("mm");
        }

        /// <summary>
        /// 获取传入日期的4位小时+分钟的组合字符串(小时和分钟不足两位的分别左补零),如:传入2016-05-26 1:5:00,返回:0105
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string HourAndMinute(this DateTime date)
        {
            return date.ToString("HHmm");
        }

        /// <summary>
        /// 格式化日期字符串
        /// </summary>
        /// <param name="input">传入:201605271136,格式化为:2016-05-27 11:36</param>
        /// <returns></returns>
        public static string FormatDate(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            return input.Insert(4, "-").Insert(7, "-").Insert(10, " ").Insert(13, ":");
        }

        /// <summary>
        /// 格式化日期字符串
        /// </summary>
        /// <param name="input">传入:1136,格式化为:11:36</param>
        /// <returns></returns>
        public static string FormatHourAndMinute(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            return input.Insert(2, ":");
        }

        /// <summary>
        /// 将应用监控时间区间和对应分钟字符串转换为实体对象集合,示例字符串格式【1-8:5_18-23:2_100:5】
        /// </summary>
        /// <param name="timeRangeString">应用监控时间区间和我对应分钟字符串,示例字符串格式【1-8:5_18-23:2_100:5】</param>
        /// <returns></returns>
        public static List<AppMonitorTimeRange> ParseAppMonitorTimeRange(this string timeRangeString)
        {
            var list = new List<AppMonitorTimeRange>();
            if (string.IsNullOrEmpty(timeRangeString))
            {
                list.Add(new AppMonitorTimeRange());
                return list;
            }
            var eachTimeRange = timeRangeString.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var rangeAndMinute in eachTimeRange)
            {
                var split = rangeAndMinute.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (split.Length != 2)
                {
                    continue;
                }
                if (split[0] == "100")
                {
                    list.Add(new AppMonitorTimeRange
                    {
                        Minutes = Convert.ToInt32(split[1]),
                        WhichHour = 100
                    });
                }
                else
                {
                    var time = split[0].Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    if (time.Length != 2)
                    {
                        continue;
                    }
                    var minutes = Convert.ToInt32(split[1]);
                    var start = Convert.ToInt32(time[0]);
                    var end = Convert.ToInt32(time[1]);
                    for (var i = 0; i <= end - start; i++)
                    {
                        var hour = start + i;
                        if (hour == 24)
                        {
                            hour = 0;
                        }
                        var item = new AppMonitorTimeRange
                        {
                            Minutes = minutes,
                            WhichHour = hour
                        };
                        if (list.Exists(x => x.WhichHour == hour))
                        {
                            continue;
                        }
                        list.Add(item);
                    }
                }
            }
            list = list.OrderBy(x => x.WhichHour).ToList();
            return list;
        }

        /// <summary>
        /// 将应用监控时间区间和对应分钟字符串转换为24个小时的实体对象集合
        /// </summary>
        /// <param name="timeRangeString">应用监控时间区间和我对应分钟字符串,示例字符串格式【1-8:5_18-23:2_100:5】</param>
        /// <returns>返回每小时对应监控的分钟数集合</returns>
        public static List<AppMonitorTimeRange> ParseAppMonitorTimeRangeTo24Hours(this string timeRangeString)
        {
            var list = new List<AppMonitorTimeRange>();
            if (string.IsNullOrEmpty(timeRangeString))
            {
                list.Add(new AppMonitorTimeRange());
                return list;
            }

            var defaultMinutes = 5;

            var eachTimeRange = timeRangeString.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var rangeAndMinute in eachTimeRange)
            {
                var split = rangeAndMinute.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (split.Length != 2)
                {
                    continue;
                }
                if (split[0] == "100")
                {
                    defaultMinutes = Convert.ToInt32(split[1]);
                    for (var i = 0; i < 24; i++)
                    {
                        if (list.Exists(x => x.WhichHour == i))
                        {
                            continue;
                        }
                        list.Add(new AppMonitorTimeRange
                        {
                            Minutes = defaultMinutes,
                            WhichHour = i
                        });
                    }
                }
                else
                {
                    var time = split[0].Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    if (time.Length != 2)
                    {
                        continue;
                    }
                    var minutes = Convert.ToInt32(split[1]);
                    var start = Convert.ToInt32(time[0]);
                    var end = Convert.ToInt32(time[1]);
                    for (var i = 0; i <= end - start; i++)
                    {
                        var hour = start + i;
                        if (hour == 24)
                        {
                            hour = 0;
                        }
                        var item = new AppMonitorTimeRange
                        {
                            Minutes = minutes,
                            WhichHour = hour
                        };
                        if (list.Exists(x => x.WhichHour == hour))
                        {
                            continue;
                        }
                        list.Add(item);
                    }
                }
            }
            
            for (var i = 0; i < 24; i++)
            {
                if (list.Exists(x => x.WhichHour == i))
                {
                    continue;
                }
                list.Add(new AppMonitorTimeRange
                {
                    Minutes = defaultMinutes,
                    WhichHour = i
                });
            }
            list = list.OrderBy(x => x.WhichHour).ToList();
            return list;
        }

        /// <summary>
        /// 将应用监控时间区间和对应分钟字符串转换为实体对象集合,示例字符串格式【1-8:5_18-23:2_100:5】
        /// </summary>
        /// <param name="timeRangeString">应用监控时间区间和我对应分钟字符串,示例字符串格式【1-8:5_18-23:2_100:5】</param>
        /// <returns></returns>
        public static AppMonitorTimeRangeModel ParseAppMonitorTimeRangeModel(this string timeRangeString)
        {
            var item = new AppMonitorTimeRangeModel();
            if (string.IsNullOrEmpty(timeRangeString))
            {
                return item;
            }
            var eachTimeRange = timeRangeString.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var rangeAndMinute in eachTimeRange)
            {
                var split = rangeAndMinute.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (split.Length != 2)
                {
                    continue;
                }
                if (split[0] == "100")
                {
                    item.AppMonitorTimeCustom = new AppMonitorTimeCustom
                    {
                        Minutes = Convert.ToInt32(split[1])
                    };
                }
                else
                {
                    var time = split[0].Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    if (time.Length != 2)
                    {
                        continue;
                    }

                    var minutes = Convert.ToInt32(split[1]);
                    var start = Convert.ToInt32(time[0]);
                    var end = Convert.ToInt32(time[1]);
                    if (item.AppMonitorTimeDay == null)
                    {
                        item.AppMonitorTimeDay = new AppMonitorTimeDay
                        {
                            Start = start,
                            End = end,
                            Minutes = minutes
                        };
                        continue;
                    }
                    else if (item.AppMonitorTimeNight == null)
                    {
                        item.AppMonitorTimeNight = new AppMonitorTimeNight
                        {
                            Start = start,
                            End = end,
                            Minutes = minutes
                        };
                    }
                }
            }
            return item;
        }

        /// <summary>
        /// 是否允许发送监控警报
        /// </summary>
        /// <param name="appMonitorTimeRangeList">配置的应用监控时间区间和对应的分钟集合</param>
        /// <param name="timespanSinceLatestOrder">自上次下单以来的秒数</param>
        /// <returns></returns>
        public static bool Allow(this List<AppMonitorTimeRange> appMonitorTimeRangeList, int timespanSinceLatestOrder)
        {
            if (appMonitorTimeRangeList == null || appMonitorTimeRangeList.Count <= 0)
            {
                return false;
            }

            return true;
        }
    }

    /// <summary>
    /// 应用监控时间区间和对应分钟实体类
    /// </summary>
    public class AppMonitorTimeRange
    {
        public AppMonitorTimeRange()
        {
            WhichHour = 100;
            Minutes = 5;
        }
        /// <summary>
        /// 小时,100表示其他时间段
        /// </summary>
        public int WhichHour { get; set; }
        /// <summary>
        /// 分钟数(默认:5分钟)
        /// </summary>
        public int Minutes { get; set; }
    }


    public abstract class AppMonitorTimeBase
    {
        public int Minutes { get; set; }

    }

    public class AppMonitorTimeDay : AppMonitorTimeBase
    {
        public int Start { get; set; }
        public int End { get; set; }
    }
    public class AppMonitorTimeNight : AppMonitorTimeBase
    {
        public int Start { get; set; }
        public int End { get; set; }
    }
    public class AppMonitorTimeCustom : AppMonitorTimeBase
    {

    }

    public class AppMonitorTimeRangeModel
    {
        public AppMonitorTimeDay AppMonitorTimeDay { get; set; }
        public AppMonitorTimeNight AppMonitorTimeNight { get; set; }
        public AppMonitorTimeCustom AppMonitorTimeCustom { get; set; }
    }
}
