namespace WEB.Extensions.PanChannelMonitor
{
    /// <summary>
    /// 支付通道检测器的构造工厂
    /// </summary>
    public class PayChannelFactory
    {
        /// <summary>
        /// 支付通道检测器,传入通道类型,工厂自动生成对应的接口实现
        /// </summary>
        /// <param name="payType">通道类型,1:支付宝,2:微信</param>
        /// <returns></returns>
        public static IPayChannelMonitor Creator(int payType)
        {
            IPayChannelMonitor payChannelMonitor = null;
            switch (payType)
            {
                //支付宝通道
                case 1:
                    payChannelMonitor = new ZhiFuBaoPayChannelMonitor();
                    break;
                //微信通道
                case 2:
                    payChannelMonitor = new WeiXinPayChannelMonitor();
                    break;
            }
            return payChannelMonitor;
        }
    }
}