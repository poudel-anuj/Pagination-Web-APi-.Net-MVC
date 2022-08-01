using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pagination.Models
{
    public class CommonResponse
    {
        public string code { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        public List<Errors> errors { get; set; }
    }
    public class Errors
    {
        public string error_code { get; set; }
        public string error_message { get; set; }
    }
    public class UserDetail
    {
        public string ApiUserCode { get; set; }
        public string SecretKey { get; set; }
    }
}