using JMP.BLL;

namespace JmPay.PayChannelMonitor.DataLoader
{
    /// <summary>
    /// 应用监控[XX分钟支付成功率]数据加载器
    /// </summary>
    public class AppMonitorPaySuccessRatioDataLoader : IDataLoader
    {
        public object Load(string procName)
        {
            return new BllCommonQuery().ExecuteProc(procName);
        }
    }
}
