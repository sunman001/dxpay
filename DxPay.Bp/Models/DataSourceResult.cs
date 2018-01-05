using DxPay.Infrastructure;
using System.Collections.Generic;

namespace DxPay.Bp.Models
{
    public class DataSourceResult<T>
    {
        public DataSourceResult(IPagedList<T> list)
        {
            TotalCount = list.TotalCount;
            TotalPages = list.TotalPages;
            PageIndex = list.PageIndex;
            Offset = list.PageSize;
        }
        public object ExtraData { get; set; }

        public IEnumerable<T> Data { get; set; }

        public object Errors { get; set; }

        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int CurrentPage {
            get { return PageIndex + 1; }
        }

        public int Offset { get; set; }
    }
}