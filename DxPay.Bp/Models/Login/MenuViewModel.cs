using System.Collections.Generic;

namespace DxPay.Bp.Models.Login
{

    /// <summary>
    /// 菜单视图模式
    /// </summary>
    public class MenuViewModel
    {
        public MenuViewModel()
        {
            Children= new List<MenuViewModel>();
        }
        public string Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string Href { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 子菜单
        /// </summary>
        public List<MenuViewModel> Children { get; set; }
    }
}