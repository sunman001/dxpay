using JMP.MDL;

namespace DxPay.LogManager.ApiLogManager
{
    /// <summary>
    /// SQL Server 日志写入器
    /// </summary>
    public sealed class ApiSqlServerLogWriter : IApiLogWriter
    {
        /// <summary>
        /// 向SQL SERVER中写入全局错误日志
        /// </summary>
        /// <param name="logEntity"></param>
        public void Write(LogForApi logEntity)
        {
            new JMP.BLL.LogForApi().Add(logEntity);
        }
    }
}
