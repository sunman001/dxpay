using TOOL.EnumUtil;

namespace DxPay.LogManager.Models
{
    /// <summary>
    /// 开发者平台错误日志类
    /// </summary>
    public class AgentGlobalErrorLogger : AbstractGlobalErrorLogModel
    {
        /// <summary>
        /// 开发者平台错误日志类
        /// </summary>
        public AgentGlobalErrorLogger()
        {
            
        }
       
        protected override void SetClient()
        {
            base.DxGlobalLogError.ClientId = (int)DxClient.Agent;
            base.DxGlobalLogError.ClientName = DxClient.Agent.GetDescription();
        }
    }
}
