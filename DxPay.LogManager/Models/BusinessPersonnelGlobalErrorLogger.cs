using TOOL.EnumUtil;

namespace DxPay.LogManager.Models
{
    /// <summary>
    /// 开发者平台错误日志类
    /// </summary>
    public class BusinessPersonnelGlobalErrorLogger : AbstractGlobalErrorLogModel
    {
        /// <summary>
        /// 开发者平台错误日志类
        /// </summary>
        public BusinessPersonnelGlobalErrorLogger()
        {
            
        }
       
        protected override void SetClient()
        {
            base.DxGlobalLogError.ClientId = (int)DxClient.BusinessPersonnel;
            base.DxGlobalLogError.ClientName = DxClient.BusinessPersonnel.GetDescription();
        }
    }
}
