using System.Collections.Generic;
using WEB.Models;

namespace WEB.ViewModel.Developer
{
    /// <summary>
    /// 特批服务费率视图
    /// </summary>
    public class SpecialApprovalViewModel
    {
        /// <summary>
        /// 当前开发者ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 是否允许特批
        /// </summary>
        public int IsSpecialApproval { get; set; }
        /// <summary>
        /// 已设定的特批服务费率
        /// </summary>
        public string SpecialApproval { get; set; }
        /// <summary>
        /// 可选的特批服务费率数据源
        /// </summary>
        public List<ValueTextModel> SpecialApprovalDataSource { get; set; }
    }
}