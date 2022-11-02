using System;
using System.Collections.Generic;
using System.Text;

namespace YogurtMvc.Pagination
{
    public interface IPagedList
    {
        int CurrentPage { get; }
        int TotalCount { get; }
        int PageSize { get; }
        int PageCount { get; }
    }
    public interface IPagedList<out T> : IPagedList, IEnumerable<T>
    {
    }
}
