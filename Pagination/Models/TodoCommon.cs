using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pagination.Models
{
    public class TodoCommon
    {
        public string id { get; set; }
        public string Size { get; set; }
        public string Quantity { get; set; }
        public string Amount { get; set; }
        public string Category { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public string Total { get; set; }

        //internal T MapObject<T>()
        //{
        //    throw new NotImplementedException();
        //}
    }
}