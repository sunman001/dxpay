namespace WEBDEV.Models.Help
{
    /// <summary>
    /// 帮助中心列表页面视图
    /// </summary>
    public class ArticleListViewModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string Href
        {
            get { return "help/details/" + Id; }
        }
        /// <summary>
        /// 摘要
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreatedOn { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string UpdatedOn { get; set; }
    }
}