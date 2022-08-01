using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pagination.Models.Test
{
    public class PaginationMetaData
    {
        public PaginationMetaData(int totalCount , int currentPage , int itemsPerPage)
        {
            TotalCount = totalCount;
            CurrentPage = currentPage;
            TotalPage = (int)Math.Ceiling(totalCount  / (double)itemsPerPage);
        }
        public int CurrentPage { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPage { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext =>  CurrentPage < TotalPage;
     
    }
}