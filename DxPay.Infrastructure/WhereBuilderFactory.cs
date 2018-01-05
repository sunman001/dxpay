namespace DxPay.Infrastructure
{
    /// <summary>
    /// WHERE生成器工厂
    /// </summary>
    public class WhereBuilderFactory
    {
        /// <summary>
        /// 创建WHERE生成器的静态方法
        /// </summary>
        /// <returns></returns>
        public static IWhereBuilder Create()
        {
            return new WhereBuilder();
        }
    }
}
