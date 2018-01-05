using JMP.Model;

namespace DxPay.LogManager
{
    /// <summary>
    /// 日志持久化接口
    /// </summary>
    public interface ILogWriter
    {
        void Write(DxGlobalLogError logEntity);
    }
}
