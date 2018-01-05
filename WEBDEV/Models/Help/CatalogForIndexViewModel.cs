namespace WEBDEV.Models.Help
{
    /// <summary>
    /// 帮助中心首页分类视图
    /// </summary>
    public class CatalogForIndexViewModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父ID
        /// </summary>
        public int  ParentId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 文章数
        /// </summary>
        public int ArticleCount { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}