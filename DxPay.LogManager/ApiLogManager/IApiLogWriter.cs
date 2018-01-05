using JMP.MDL;

namespace DxPay.LogManager.ApiLogManager
{
    /// <summary>
    /// 日志持久化接口
    /// </summary>
    public interface IApiLogWriter
    {
        void Write(LogForApi logEntity);
    }
}
