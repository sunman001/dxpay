using JMP.BLL;

namespace JmPay.PayChannelMonitor.DataLoader
{
    /// <summary>
    /// 应用监控[XX分钟无订单]数据加载器
    /// </summary>
    public class AppMonitorNoOrderDataLoader :IDataLoader
    {
        public object Load(string procName)
        {
            return new BllCommonQuery().ExecuteProc(procName);
        }
    }
}
