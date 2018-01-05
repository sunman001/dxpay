namespace DxPay.Agent.Util.Logger
{
    public interface ILogWriter
    {
        /// <summary>
        /// 数据新增日志
        /// </summary>
        /// <param name="summary">日志摘要</param>
        /// <param name="entity">数据实体对象</param>
        void CreateLog<T>(string summary,T entity);

        /// <summary>
        /// 数据变更日志
        /// </summary>
        /// <param name="summary">日志摘要</param>
        /// <param name="original">原始的实体对象</param>
        /// <param name="modified">变更后的实体对象</param>
        void ModifyLog<T>(string summary, T original, T modified);
        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="summary">日志摘要</param>
        /// <param name="message">日志内容详情</param>
        void OperateLog(string summary, string message);
    }
}
