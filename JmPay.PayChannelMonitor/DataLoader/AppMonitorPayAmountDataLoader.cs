using JMP.BLL;

namespace JmPay.PayChannelMonitor.DataLoader
{
    /// <summary>
    /// 应用监控[XX分钟金额成功率]数据加载器
    /// </summary>
    public class AppMonitorPayAmountDataLoader : IDataLoader
    {
        public object Load(string procName)
        {
            return new BllCommonQuery().ExecuteProc(procName);
        }
    }
}
