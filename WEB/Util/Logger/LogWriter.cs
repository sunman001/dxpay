using TOOL.Extensions;

namespace WEB.Util.Logger
{
    public class LogWriter : ILogWriter
    {
        private void Write(string summary,string message)
        {
            LogManager.GetOperateLogger.Log(summary, message);
        }
        /// <summary>
        /// 数据新增日志
        /// </summary>
        /// <param name="summary">日志摘要</param>
        /// <param name="entity">数据实体</param>
        public void CreateLog<T>(string summary, T entity)
        {
            Write(summary, entity.GetCreateEntityPropTracker().Message);
        }
        /// <summary>
        /// 数据变更日志
        /// </summary>
        /// <param name="summary">日志摘要</param>
        /// <param name="original">原始实体</param>
        /// <param name="modified">变更实体</param>
        public void ModifyLog<T>(string summary, T original, T modified)
        {
            var message = original.GetModifiedTracker(modified).Message;
            Write(summary, message);
        }
        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="summary">日志摘要</param>
        /// <param name="message">日志内容</param>
        public void OperateLog(string summary, string message)
        {
            Write(summary, message);
        }
    }
}