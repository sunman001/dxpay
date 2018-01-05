namespace WEB.Models
{
    public class Pagination
    {
        public Pagination()
        {
            PageSize = 10;
        }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 分布大小
        /// </summary>
        public int PageSize { get; set; }
    }
}