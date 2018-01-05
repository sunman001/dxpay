using System.ComponentModel;

namespace TOOL.EnumUtil
{
    /// <summary>
    /// 任务执行周期单位,可选值[second,minute,hour,day,month,year]
    /// </summary>
    public enum IntervalUnit
    {
        Second,
        Minute,
        Hour,
        Day,
        Month,
        Year
    }
    /// <summary>
    /// 任务分组枚举
    /// </summary>
    public enum ScheduleGroupCode
    {
        /// <summary>
        /// 应用监控[XX分钟内支付成功率]
        /// </summary>
        [Description("应用监控_XX分钟内支付成功率")]
        PROC_MONITOR_APP_PAY_SUCCESS_RATIO = 0,
        /// <summary>
        /// 应用监控[XX分钟内无订单]
        /// </summary>
        [Description("应用监控_XX分钟内无订单")]
        PROC_GET_NO_ORDERS_APP_FROM_TIMESPAN = 1,
        /// <summary>
        /// 应用监控[XX分钟内金额成功率]
        /// </summary>
        [Description("应用监控_XX分钟内金额成功率")]
        PROC_MONITOR_APP_AMOUNT_SUCCESS_RATIO = 2,
        /// <summary>
        /// 工单系统[新工单实时提醒任务]
        /// </summary>
        [Description("新工单实时提醒任务")]
        SCHE_MONITOR_WORK_ORDER_REMIND = 3,
        /// <summary>
        /// 通道监控[XX分钟无订单]
        /// </summary>
        [Description("通道监控_XX分钟无订单")]
        PROC_MONITOR_CHANNEL_NO_ORDER_WITH_MINUTES = 20,

        /// <summary>
        /// 发送订单异常的消息
        /// </summary>
        [Description("发送订单异常的消息")]
        SCHE_AUDITOR_MESSAGE_SCHEDULER_MONITOR = 100
    }

    /// <summary>
    /// 盾行所有平台枚举
    /// </summary>
    public enum DxClient
    {
        /// <summary>
        /// 运营平台
        /// </summary>
        [Description("运营平台")]
        Administrator = 0,
        /// <summary>
        /// 开发者平台
        /// </summary>
        [Description("开发者平台")]
        Developer = 1,
        /// <summary>
        /// 商务平台
        /// </summary>
        [Description("商务平台")]
        BusinessPersonnel = 2,
        /// <summary>
        /// 代理商平台
        /// </summary>
        [Description("代理商平台")]
        Agent = 3
    }

    /// <summary>
    /// 盾行所有接口平台枚举
    /// </summary>
    public enum DxApiClient
    {
        /// <summary>
        /// 支付接口
        /// </summary>
        [Description("支付接口")]
        PayServer = 0,
        /// <summary>
        /// 通知接口
        /// </summary>
        [Description("通知接口")]
        PayNotify = 1
    }

    public enum Relationtype
    {
        /// <summary>
        /// 未指定
        /// </summary>
        [Description("未指定")]
        Unknown = 0,

        /// <summary>
        /// 商务
        /// </summary>
        [Description("商务")]
        Bp = 1,

        /// <summary>
        /// 代理商
        /// </summary>
        [Description("代理商")]
        Agent = 2

    }

    /// <summary>
    /// 运营平台日志分类[AdminLogType]
    /// </summary>
    public enum AdminLogType
    {
        /// <summary>
        /// 所有
        /// </summary>
        [Description("所有")]
        All = 0,
        /// <summary>
        /// 注册
        /// </summary>
        [Description("注册")]
        Register = 1,
        /// <summary>
        /// 登录
        /// </summary>
        [Description("登录")]
        Login = 2,
        /// <summary>
        /// 操作
        /// </summary>
        [Description("操作")]
        Operation = 3,
        /// <summary>
        /// 错误
        /// </summary>
        [Description("错误")]
        Error = 4,
        /// <summary>
        /// 数据库
        /// </summary>
        [Description("数据库")]
        Database = 5,
        /// <summary>
        /// 归档
        /// </summary>
        [Description("归档")]
        Archive = 6,
        /// <summary>
        /// 访问
        /// </summary>
        [Description("访问")]
        Visit = 7,
        /// <summary>
        /// 代付
        /// </summary>
        [Description("代付")]
        PayForAnother = 8
    }

    /// <summary>
    /// 对接响应相关
    /// </summary>
    public class CustomerResponse
    {
        /// <summary>
        /// 状态[-1:被打回,0:新建,1:处理中,2:处理完成,3:已关闭(评分后自动设成已关闭状态,已关闭的不能再修改和追加)]
        /// </summary>
        public enum Status
        {
            /// <summary>
            /// -1:被打回
            /// </summary>
            [Description("被打回")]
            OutOfCourt = -1,
            /// <summary>
            /// 0:新建
            /// </summary>
            [Description("新建")]
            Fresh = 0,
            /// <summary>
            /// 1:处理中
            /// </summary>
            [Description("处理中")]
            Handling = 1,
            /// <summary>
            /// 2:处理完成
            /// </summary>
            [Description("处理完成")]
            Completed = 2,
            /// <summary>
            /// 3:已关闭
            /// </summary>
            [Description("已关闭")]
            Closed = 3
        }
        /// <summary>
        /// 小类[0:商户注册,1:结算相关,2:技术对接,3:通道配置,4:故障排查],默认值:0
        /// </summary>
        public enum SubCategory
        {
            /// <summary>
            /// 0:商户注册
            /// </summary>
            [Description("商户注册")]
            Register = 0,
            /// <summary>
            /// 1:结算相关
            /// </summary>
            [Description("结算相关")]
            Settlement = 1,
            /// <summary>
            /// 2:技术对接
            /// </summary>
            [Description("技术对接")]
            TechJoint = 2,
            /// <summary>
            /// 3:通道配置
            /// </summary>
            [Description("通道配置")]
            ChannelSetting = 3,
            /// <summary>
            /// 4:故障排查
            /// </summary>
            [Description("故障排查")]
            Troubleshooting = 4
        }
    }

    /// <summary>
    /// 接口日志枚举类
    /// </summary>
    public class EnumForLogForApi
    {
        /// <summary>
        /// 平台类型ID[1:上游支付错误,2:下游用户错误,3:上游通知异常],默认值:0
        /// </summary>
        public enum Platform
        {
            /// <summary>
            /// 未知
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// 1:上游支付错误
            /// </summary>
            UpstreamPaymentError = 1,
            /// <summary>
            /// 2:下游用户错误
            /// </summary>
            DownstreamError = 2,
            /// <summary>
            /// 3:上游通知异常
            /// </summary>
            UpsteamNotifyError = 3
        }

        /// <summary>
        /// 下游错误类型[1:订单号重复,2:重复发起支付,3:其他]
        /// </summary>
        public enum ErrorType
        {
            /// <summary>
            /// 未知
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// 1:订单号重复
            /// </summary>
            OrderNoRepeat = 1,
            /// <summary>
            /// 2:重复发起支付
            /// </summary>
            RequestRepeat = 2,
            /// <summary>
            /// 3:其他
            /// </summary>
            Other = 3
        }
    }
}
