using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pagination.Models.Test
{
    public class PaginationParams
    {
        private const int _maxItemsPerPage = 50;
        private int itemsPerPage;
        [FromQuery]
        public int Page { get; set; } = 1;
        [FromQuery]

        public int ItemsPerPage
        {
            get => itemsPerPage;
            set =>itemsPerPage =value > _maxItemsPerPage ?  _maxItemsPerPage :value;
        }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}