namespace WEBDEV.Models.Help
{
    /// <summary>
    /// 帮助中心文章详情页面视图
    /// </summary>
    public class ArticleDetailsViewModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreatedOn { get; set; }
        /// <summary>
        /// 创建者姓名
        /// </summary>
        public string CreatedByUserName { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string UpdatedOn { get; set; }
        /// <summary>
        /// 修改者姓名
        /// </summary>
        public string UpdatedByUserName { get; set; }
        /// <summary>
        /// 子类ID
        /// </summary>
        public int SubCatalogId { get; set; }
    }
}