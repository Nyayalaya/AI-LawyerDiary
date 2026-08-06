using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtApp.Application.Common
{
    public sealed class PaginationMeta
    {
        public int TotalCount { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public int TotalPages { get; init; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public int FirstItemIndex =>
       TotalCount == 0 ? 0 : ((PageNumber - 1) * PageSize) + 1;

        public int LastItemIndex =>
            Math.Min(PageNumber * PageSize, TotalCount);

        public static PaginationMeta Create(int totalCount, int pageNumber, int pageSize)
        {
            if (pageSize <= 0) pageSize = 10;
            return new PaginationMeta
            {
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }
    }
}
