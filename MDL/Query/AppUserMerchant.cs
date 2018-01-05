namespace JMP.Model.Query
{
    public class AppUserMerchant :MDL.jmp_user
    {
        /// <summary>
        /// 商务名
        /// </summary>
        public string sw { get; set; }
        /// <summary>
        /// 代理商名
        /// </summary>
        public string dls { get; set; }

        /// <summary>
        /// 服务费
        /// </summary>
        public decimal ServiceFeeRatio { get; set; }
    }
}
