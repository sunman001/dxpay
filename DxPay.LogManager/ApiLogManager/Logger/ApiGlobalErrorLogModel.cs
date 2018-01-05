using JMP.MDL;
using System;

namespace DxPay.LogManager.ApiLogManager.Logger
{
    public abstract class ApiGlobalErrorLogModel : ILogger
    {
        protected readonly IApiLogWriter LogWriter;
        protected ApiGlobalErrorLogModel()
        {
            LogForApi = new LogForApi { TypeValue = 1};
            LogWriter=new ApiSqlServerLogWriter();
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
        protected LogForApi LogForApi { get; set; }
        public void Log(int userId, string message, string ipAddress, string location = "", string summary = "")
        {
            LogForApi.Message = message;
            LogForApi.IpAddress = ipAddress;
            LogForApi.Location = location;
            LogForApi.Summary = summary;
            LogForApi.CreatedOn = DateTime.Now;
            LogWriter.Write(LogForApi);
        }

        #region Implementation of IApiLogWriter

        #endregion
    }
}
