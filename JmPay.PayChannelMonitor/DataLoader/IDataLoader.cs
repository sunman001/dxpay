namespace JmPay.PayChannelMonitor.DataLoader
{
    /// <summary>
    /// 数据加载器
    /// </summary>
    public interface IDataLoader
    {
        /// <summary>
        /// 根据存储过程名称加载对就的数据集
        /// </summary>
        /// <param name="procName"></param>
        /// <returns></returns>
        object Load(string procName="");
    }
}
