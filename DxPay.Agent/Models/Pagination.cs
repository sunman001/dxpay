namespace DxPay.Agent.Models
{
    public class Pagination
    {
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int CurrentPage
        {
            get { return Page; }
        }

        public int Offset { get; set; }
    }
}