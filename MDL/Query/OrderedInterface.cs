namespace JMP.Model.Query
{
    /// <summary>
    /// 正在使用中支付通道实体类
    /// </summary>
    public class OrderedInterface
    {
        /// <summary>
        /// 通道ID
        /// </summary>
        public int InterfaceId { get; set; }
        /// <summary>
        /// 通道名称
        /// </summary>
        public string InterfaceName { get; set; }
        /// <summary>
        /// 订单量
        /// </summary>
        public int OrderTotal { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}
