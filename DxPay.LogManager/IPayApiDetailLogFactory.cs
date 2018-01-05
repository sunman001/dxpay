namespace DxPay.LogManager
{
    /// <summary>
    /// 平台日志器工厂
    /// </summary>
    public interface IPayApiDetailLogFactory
    {
        IApiLogger CreateLogger();
    }
}
