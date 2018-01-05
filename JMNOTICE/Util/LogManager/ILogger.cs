using System.Collections.Generic;

namespace JMNOTICE.Util.LogManager
{
    /// <summary>
    /// 日志接口
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// 写日志的方法
        /// </summary>
        /// <param name="content">日志内容</param>
        void Write(string content);

        /// <summary>
        /// 日志内容行
        /// </summary>
        /// <param name="lines"></param>
        void Write(List<string> lines);
    }
}
