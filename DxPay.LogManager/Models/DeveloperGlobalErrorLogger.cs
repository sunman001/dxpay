using TOOL.EnumUtil;

namespace DxPay.LogManager.Models
{
    /// <summary>
    /// 开发者平台错误日志类
    /// </summary>
    public class DeveloperGlobalErrorLogger : AbstractGlobalErrorLogModel
    {
        /// <summary>
        /// 开发者平台错误日志类
        /// </summary>
        public DeveloperGlobalErrorLogger()
        {
            
        }
       
        protected override void SetClient()
        {
            base.DxGlobalLogError.ClientId = (int)DxClient.Developer;
            base.DxGlobalLogError.ClientName = DxClient.Developer.GetDescription();
        }
    }
}
