using System;
using JMP.Model;

namespace DxPay.LogManager.Models
{
    public abstract class AbstractGlobalErrorLogModel:ILogger
    {
        protected readonly ILogWriter LogWriter;
        protected AbstractGlobalErrorLogModel()
        {
            DxGlobalLogError = new DxGlobalLogError {TypeValue = 1};
            LogWriter=new SqlServerLogWriter();
            Init();
        }

        protected void Init()
        {
            SetClient();
        }
        /// <summary>
        /// 继承类需要设置不同的平台标识
        /// </summary>
        protected abstract void SetClient();
        protected DxGlobalLogError DxGlobalLogError { get; set; }
        public void Log(int userId, string message, string ipAddress, string location = "", string summary = "")
        {
            DxGlobalLogError.UserId = userId;
            DxGlobalLogError.Message = message;
            DxGlobalLogError.IpAddress = ipAddress;
            DxGlobalLogError.Location = location;
            DxGlobalLogError.Summary = summary;
            DxGlobalLogError.CreatedOn = DateTime.Now;
            LogWriter.Write(DxGlobalLogError);
        }
    }
}
