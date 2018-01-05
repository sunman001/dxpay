using TOOL.EnumUtil;

namespace DxPay.LogManager.Models
{
    /// <summary>
    /// 运营平台错误日志类
    /// </summary>
    public class AdministratorGlobalErrorLogger : AbstractGlobalErrorLogModel, ILogger
    {
        /// <summary>
        /// 运营平台错误日志类
        /// </summary>
        public AdministratorGlobalErrorLogger()
        {
            
        }

        protected override void SetClient()
        {
            base.DxGlobalLogError.ClientId = (int)DxClient.Administrator;
            base.DxGlobalLogError.ClientName = DxClient.Administrator.GetDescription();
        }
    }
}
