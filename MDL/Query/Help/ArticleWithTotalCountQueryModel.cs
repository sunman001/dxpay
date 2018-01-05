namespace JMP.Model.Query.Help
{
    public class ArticleWithTotalCountQueryModel
    {
        public int Id { get; set; }
        public int MainCatalogId { get; set; }
        public int SubCatalogId { get; set; }
        public string SubCatalogName { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// 子分类下的文章数
        /// </summary>
        public int SubCatalogArticleCount { get; set; }

        public string Href
        {
            get { return "help/details/" + Id; }
        }
    }
}