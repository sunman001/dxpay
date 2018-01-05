namespace JMP.Model.Query
{
    /// <summary>
    /// 角色-权限关系映射查询实体
    /// </summary>
    public class RolePermissionMappingQueryModel
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        public int PermissionId { get; set; }
        /// <summary>
        /// 权限路径
        /// </summary>
        public string PermissionUrl { get; set; }
    }
}
