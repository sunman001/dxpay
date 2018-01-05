using JMP.Model.Query;
using JMP.TOOL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WEB.Util.CacheManager
{
    /// <summary>
    /// 权限缓存管理类
    /// </summary>
    public class RoleCacheManager
    {
        private static readonly object Lock = new object();
        /// <summary>
        /// 角色-权限映射缓存键
        /// </summary>
        private static readonly string RolePermissionMappingCacheKey = "c_k_role_permission_mapping";

        /// <summary>
        /// 检查指定的按钮名称是否有权限
        /// </summary>
        /// <param name="actionName">页面控制器访问路径</param>
        /// <returns></returns>
        public static bool HasPermission(string actionName)
        {
            if (UserInfo.IsSuperAdmin)
            {
                return true;
            }
            if (actionName.IsEmpty())
            {
                return false;
            }
            HashSet<RolePermissionMappingQueryModel> mappings;
            if (CacheHelper.IsCache(RolePermissionMappingCacheKey))
            {
                mappings = CacheHelper.GetCaChe<HashSet<RolePermissionMappingQueryModel>>(RolePermissionMappingCacheKey);
            }
            else
            {
                mappings = LoadData();
                SetCache(mappings);
            }
            return mappings.Any(x => x.RoleId == UserInfo.UserRoleId && x.PermissionUrl.IndexOf(actionName, StringComparison.InvariantCultureIgnoreCase) >= 0);
        }

        private static HashSet<RolePermissionMappingQueryModel> LoadData()
        {
            var list = new JMP.BLL.jmp_role().FindAdminRolePermissionMappingList();
            for (var i = 0; i < list.Count; i++)
            {
                var m = list[i];
                m.PermissionUrl = m.PermissionUrl.ToLower().Trim();
            }
            var mappings = new HashSet<RolePermissionMappingQueryModel>(list);
            return mappings;
        }

        private static void SetCache(HashSet<RolePermissionMappingQueryModel> mappings)
        {
            CacheHelper.CacheObjectLocak(mappings, RolePermissionMappingCacheKey, 60 * 12);
        }

        public static void Refresh()
        {
            SetCache(LoadData());
        }
    }

    public class RolePermissionMapping
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
    }
}