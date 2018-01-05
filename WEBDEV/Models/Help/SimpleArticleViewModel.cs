namespace WEBDEV.Models.Help
{
    /// <summary>
    /// 帮助中心只有文章标题的文章视图
    /// </summary>
    public class SimpleArticleViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Href
        {
            get { return "help/details/" + Id; }
        }
    }
}