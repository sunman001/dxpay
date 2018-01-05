using System.Collections.Generic;
using System.Linq;
using JMP.BLL;
using DxPay.Bp.Models.Login;

namespace DxPay.Bp.Util
{
    /// <summary>
    /// 菜单加载器
    /// </summary>
    public class MenuLoader
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="rid">权限ID</param>
        /// <returns></returns>
        public IEnumerable<MenuViewModel> Load(int uid, int rid)
        {
            var menus = new List<MenuViewModel>();

            var bll = new jmp_limit();
            var dt = bll.GetAppBusinessLimit(uid, rid);
            var lst = bll.DataTableToList(dt);
            //得到顶级菜单
            var level1 = lst.Where(x => x.l_topid == 0).ToList();

            foreach (var l in level1)
            {
                //获取子菜单集合
                var level2 = lst.Where(x => x.l_topid == l.l_id).ToList();
                var children = level2.Select(x => new MenuViewModel
                {
                    Id=x.l_id.ToString(),
                    Title = x.l_name,
                    Href = x.l_url,
                    Icon = x.l_icon
                }).ToList();
                var menu = new MenuViewModel
                {
                    Id = l.l_id.ToString(),
                    Title = l.l_name,
                    Href = l.l_url,
                    Icon = l.l_icon,
                    Children = children
                };

                menus.Add(menu);
            }
            return menus;
        }
    }
}