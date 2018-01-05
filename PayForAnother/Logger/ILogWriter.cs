namespace PayForAnother.Logger
{
    public interface ILogWriter
    {

        /// <summary>
        /// 代付日志
        /// </summary>
        /// <param name="summary">日志摘要</param>
        /// <param name="message">日志内容详情</param>
        void PayForAnotherLog(string summary, string message);
    }
}
