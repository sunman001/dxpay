namespace WEBDEV.Models.Help
{
    /// <summary>
    /// 带文章总数的帮助中心文章视图
    /// </summary>
    public class ArticleWithTotalCountViewModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 主分类ID
        /// </summary>
        public int MainCatalogId { get; set; }
        /// <summary>
        /// 子分类ID
        /// </summary>
        public int SubCatalogId { get; set; }
        /// <summary>
        /// 子分类名称
        /// </summary>
        public string SubCatalogName { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 子分类下的文章数
        /// </summary>
        public int SubCatalogArticleCount { get; set; }
        //链接
        public string Href
        {
            get { return "help/details/" + Id; }
        }
    }
}