using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JMP.TOOL
{
    public class Ex : Exception
    {
        /// <summary>
        /// 错误提示码
        /// </summary>
        public virtual int ErrorCode { get; set; }
        /// <summary>
        /// 错误提示信息
        /// </summary>
        public virtual string Message { get; set; }
        public Ex() : base() { }
        /// <summary>
        /// 接收异常自定义代码
        /// </summary>
        /// <param name="code">自定义编码</param>
        /// <param name="message">错误提示</param>
        public Ex(int code, string message)
            : base(message)
        {
            ErrorCode = code;
            Message = message;
        }
        /// <summary>
        /// 接收异常自定义代码
        /// </summary>
        /// <param name="message">错误提示代码</param>
        public Ex(string message)
            : base(message)
        {
            this.Message = message;
        }
    }
}
