using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pagination.Models
{
    public class PaginationModel
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPageNumber { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public Todo todo { get; set; }
    }
}