using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YogurtMvc.Pagination
{
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        public PagedList(IEnumerable<T> source, int currentPage, int pageSize, int totalCount)
            : this(source.AsQueryable(), currentPage, pageSize, totalCount)
        {
        }

        public PagedList(IQueryable<T> source, int currentPage, int pageSize, int totalCount)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
            AddRange(source.Skip((currentPage - 1) * pageSize).Take(pageSize));
        }

        public int CurrentPage { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get { return (int)Math.Ceiling((decimal)TotalCount / PageSize); } }
    }
}
