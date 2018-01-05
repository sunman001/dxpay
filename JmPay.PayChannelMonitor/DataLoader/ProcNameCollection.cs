namespace JmPay.PayChannelMonitor.DataLoader
{
    public struct ProcNameCollection
    {
        /// <summary>
        /// 通道监控[XX分钟无订单]
        /// </summary>
        public static readonly string MonitorForChannelOnOrderProcName = "PROC_MONITOR_CHANNEL_NO_ORDER_WITH_MINUTES";

        /// <summary>
        /// 应用监控[XX分钟支付成功率]
        /// </summary>
        public static readonly string MonitorForAppPaySuccessRatioProcName = "PROC_MONITOR_APP_PAY_SUCCESS_RATIO";
        /// <summary>
        /// 应用监控[XX分钟无订单]
        /// </summary>
        public static readonly string MonitorForAppNoOrderProcName = "PROC_GET_NO_ORDERS_APP_FROM_TIMESPAN";
        /// <summary>
        /// 应用监控[XX分钟金额成功率]
        /// </summary>
        public static readonly string MonitorForAppAmountSuccessRatioProcName = "PROC_MONITOR_APP_AMOUNT_SUCCESS_RATIO";
    }
}
