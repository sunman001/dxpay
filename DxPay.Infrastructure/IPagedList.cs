using System.Collections.Generic;

namespace DxPay.Infrastructure
{
    /// <summary>
    /// 分页泛型接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPagedList<T> : IList<T>
    {
        int PageIndex { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
}
