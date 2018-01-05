namespace WEB.Extensions.PanChannelMonitor
{
    /// <summary>
    /// 支付通道检测接口
    /// </summary>
    public interface IPayChannelMonitor
    {
        /// <summary>
        /// 通道ID
        /// </summary>
        int Tid { get; set; }
        /// <summary>
        /// 是否启用检测
        /// </summary>
        bool AllowCheck { get; set; }
        /// <summary>
        /// 启用自动检测
        /// </summary>
        bool AllowAutoCheck { get; set; }
        /// <summary>
        /// 检测支付通道状态的接口方法
        /// </summary>
        /// <returns></returns>
        bool Check();
        /// <summary>
        /// 检测支付通道状态的接口方法
        /// </summary>
        /// <returns></returns>
        bool checkorder();
    }
}
