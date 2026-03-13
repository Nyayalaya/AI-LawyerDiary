using System.Collections.Generic;

namespace CourtApp.Web.Models
{
    public class PaginationViewModel<T>
    {
        public List<T> Data { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public long TotalCount { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}
