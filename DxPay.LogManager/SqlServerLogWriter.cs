using JMP.Model;

namespace DxPay.LogManager
{
    /// <summary>
    /// SQL Server 日志写入器
    /// </summary>
    public sealed class SqlServerLogWriter :ILogWriter
    {
        /// <summary>
        /// 向SQL SERVER中写入全局错误日志
        /// </summary>
        /// <param name="logEntity"></param>
        public void Write(DxGlobalLogError logEntity)
        {
            new JMP.BLL.DxGlobalLogError().Add(logEntity);
        }
    }
}
