namespace Pay.DinPay
{
    /// <summary>
    /// 表单属性类,主要设置表单的method,action等属性
    /// </summary>
    public class FormProperty
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="action">表单的提交地址</param>
        public FormProperty(string action)
        {
            Method = "POST";
            Action = action;
        }
        /// <summary>
        /// 表单提交方式,POST或者GET,默认POST
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 表单的提交地址
        /// </summary>
        public string Action { get; set; }
    }
}
