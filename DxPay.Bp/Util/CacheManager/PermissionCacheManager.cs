using System;
using System.Collections.Generic;
using System.Linq;
using JMP.TOOL;

namespace DxPay.Bp.Util.CacheManager
{
    /// <summary>
    /// 权限缓存管理类
    /// </summary>
    public class PermissionCacheManager
    {
        /// <summary>
        /// 权限缓存键
        /// </summary>
        private static readonly string Key = "c_k_permission_list";
        /// <summary>
        /// 根据页面访问路径获取对应的权限配置对象
        /// </summary>
        /// <param name="url">页面控制器访问路径</param>
        /// <returns></returns>
        public static JMP.MDL.jmp_limit Get(string url)
        {
            List<JMP.MDL.jmp_limit> permissions;
            if (CacheHelper.IsCache(Key))
            {
                permissions = CacheHelper.GetCaChe<List<JMP.MDL.jmp_limit>>(Key);
            }
            else
            {
                var list = new JMP.BLL.jmp_limit().GetModelList("");
                CacheHelper.CacheObjectLocak(list, Key, 60 * 12);
                permissions = list;
            }
            var m = permissions.FirstOrDefault(x => string.Equals(x.l_url.Trim('/').Trim(), url.Trim('/').Trim(), StringComparison.CurrentCultureIgnoreCase));
            return m;
        }
    }
}