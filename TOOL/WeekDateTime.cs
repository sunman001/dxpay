using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMP.TOOL
{
    /// <summary>
    /// 获取每周公共方法
    /// </summary>
    public class WeekDateTime
    {
        /// <summary>  
        /// 得到本周第一天(以星期一为第一天)  
        /// </summary>  
        /// <param name="datetime">传入系统当前时间</param>  
        /// <returns>返回本周第一台(以星期一为第一天)  </returns>  
        public static DateTime GetWeekFirstDayMon(DateTime datetime)
        {
            //星期一为第一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);

            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。  
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            int daydiff = (-1) * weeknow;
            //本周第一天  
            string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        }


        /// <summary>  
        /// 得到本周第一天(以星期天为第一天)  
        /// </summary>  
        /// <param name="datetime">传入系统当前时间</param>  
        /// <returns>返回本周第一台(以星期天为第一天)</returns>  
        public static DateTime GetWeekFirstDaySun(DateTime datetime)
        {
            //星期天为第一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            int daydiff = (-1) * weeknow;

            //本周第一天  
            string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        }
        /// <summary>  
        /// 得到本周最后一天(以星期六为最后一天)  
        /// </summary>  
        /// <param name="datetime">传入系统当前时间</param>  
        /// <returns>返回本周最后一天(以星期六为最后一天)</returns>  
        public static DateTime GetWeekLastDaySat(DateTime datetime)
        {
            //星期六为最后一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            int daydiff = (7 - weeknow) - 1;

            //本周最后一天  
            string LastDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(LastDay);
        }

        /// <summary>  
        /// 得到本周最后一天(以星期天为最后一天)  
        /// </summary>  
        /// <param name="datetime">传入系统当前时间</param>  
        /// <returns>返回本周最后一天(以星期天为最后一天)</returns>  
        public static DateTime GetWeekLastDaySun(DateTime datetime)
        {
            //星期天为最后一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            weeknow = (weeknow == 0 ? 7 : weeknow);
            int daydiff = (7 - weeknow);

            //本周最后一天  
            string LastDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(LastDay);
        }
        /// <summary>
        /// 根据时间返回获取到时间返回内的每周星期一
        /// </summary>
        /// <param name="KsDateTime">开始时间</param>
        /// <param name="JsDateTime">结束时间</param>
        /// <returns>返回一个数组</returns>
        public static ArrayList WeekMonday(DateTime KsDateTime, DateTime JsDateTime)
        {
            ArrayList fhsj = new ArrayList();//返回时间数组
            DateTime ks = GetWeekFirstDayMon(KsDateTime);//开始时间的本周星期一
            DateTime js = GetWeekFirstDayMon(JsDateTime);//结束时间的本周星期一
            fhsj.Add(ks.ToString("yyyy-MM-dd"));
            while (true)
            {
                if (ks == js)
                {
                    break;
                }
                else
                {
                    ks = ks.AddDays(7);
                    ks = GetWeekFirstDayMon(ks);
                    fhsj.Add(ks.ToString("yyyy-MM-dd"));
                }
            }
            return fhsj;
        }
        /// <summary>
        /// 根据时间返回获取到时间返回内的每周星期天
        /// </summary>
        /// <param name="KsDateTime">开始时间</param>
        /// <param name="JsDateTime">结束时间</param>
        /// <returns>返回一个数组</returns>
        public static ArrayList WeekDay(DateTime KsDateTime, DateTime JsDateTime)
        {
            ArrayList fhsj = new ArrayList();//返回时间数组
            DateTime ks = GetWeekLastDaySun(KsDateTime);//开始时间的本周星期天
            DateTime js = GetWeekLastDaySun(JsDateTime);//结束时间的本周星期
            fhsj.Add(ks.ToString("yyyy-MM-dd"));
            while (true)
            {
                if (ks == js)
                {
                    break;
                }
                else
                {
                    ks = ks.AddDays(7);
                    ks = GetWeekLastDaySun(ks);
                    fhsj.Add(ks.ToString("yyyy-MM-dd"));
                }
            }
            return fhsj;
        }
        /// <summary>
        /// 获取当前时间对应的订单表名
        /// </summary>
        public static string GetCurrentOrderTableName
        {
            get
            {
                return "jmp_order_" + GetWeekFirstDayMon(DateTime.Now).ToString("yyyyMMdd");
            }
        }

        /// <summary>
        /// 获取指定时间对应的订单表名
        /// </summary>
        /// <param name="tTime">日期字符串</param>
        /// <returns></returns>
        public static string GetOrderTableName(string tTime)
        {
            string tname;
            var list = WeekMonday(DateTime.Parse(tTime), DateTime.Parse(tTime));
            if (list.Count == 1)
                tname = "jmp_order_" + DateTime.Parse(list[0].ToString()).ToString("yyyyMMdd");
            else
            {
                var tDate = DateTime.Parse(tTime).AddDays(-7);
                list = WeekMonday(tDate, tDate);
                tname = "jmp_order_" + DateTime.Parse(list[0].ToString()).ToString("yyyyMMdd");
            }
            return tname;
        }

        /// <summary>
        /// 获取当前时间的时间戳(10位)
        /// </summary>
        public static string GetMilis
        {
            get
            {
                //DateTime dt1970 = new DateTime(1970, 1, 1);
                //DateTime current = DateTime.Now;
                //TimeSpan span = current - dt1970;
                //return span.TotalMilliseconds.ToString("f0");
                //Console.WriteLine(span.TotalMilliseconds.ToString());

                return ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString();
            }
        }

        /// <summary>
        /// 获取当前时间的时间戳（13位）
        /// </summary>
        public static string GetMiliss
        {
            get
            {
                DateTime time = DateTime.Now;
                System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
                long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
                return t.ToString();

            }
        }

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name=”timeStamp”></param>
        /// <returns></returns>
        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime); return dtStart.Add(toNow);
        }
    }
}
