namespace JMNOTICE.Util
{
    public class GlobalConfig
    {
        /// <summary>
        /// 日志保存目录
        /// </summary>
        public static string LOG_DIRECTORY = "";
        /// <summary>
        /// 订单通知任务的所有需开启的任务的配置字符串
        /// </summary>
        public static string TASK_CRONS_CONFIG_STRING = "";
        /// <summary>
        /// 最大并行线程数(默认值:5)
        /// </summary>
        public static int MAX_THREAD_COUNT = 5;
        /// <summary>
        /// 每次从数据库加载的数据行数(默认值:6)
        /// </summary>
        public static int EACH_TIME_SELECT_TOP_COUNT = 6;
        /// <summary>
        /// 循环加载数据的最大时间间隔的阀值(默认值:5秒),当定时任务的间隔周期大于此值,此任务每次执行将会启动循环加载数据,直到没有数据取出为止
        /// </summary>
        public static int LOOP_LOAD_DATA_THRESHOLD = 5;
        /// <summary>
        /// 日志最大显示行数(默认值:2000)
        /// </summary>
        public static int CLEAR_LOG_THRESHOLD = 2000;

        /// <summary>
        /// 加密服务器IP地址
        /// </summary>
        public static string ENCRYPT_IP_ADDRESS = "";

        /// <summary>
        /// 客户端ID
        /// </summary>
        public static string CLIENT_ID = "";

        /// <summary>
        /// 任务执行总次数累加器
        /// </summary>
        public static long TaskExecuteCount = 0;
        /// <summary>
        /// 是否允许从数据库加载数据
        /// </summary>
        public static bool AllowReadDataFromDatabase = true;

    }
}
