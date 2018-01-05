using JMP.BLL;

namespace JmPay.PayChannelMonitor.DataLoader
{
    /// <summary>
    /// 通道监控[XX分钟无订单]数据加载器
    /// </summary>
    public class ChannelMonitorNoOrderDataLoader : IDataLoader
    {
        public object Load(string procName)
        {
            return new BllCommonQuery().ExecuteProc(procName);
        }
    }
}
