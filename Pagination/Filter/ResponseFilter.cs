using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace Pagination.Filter
{
    public class ResponseFilter:ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var flag = actionExecutedContext.Request.Properties["flag"].ToString();

            if (flag == "Pagination")
            {
                var re = actionExecutedContext.Request.Properties["X-Pagination"].ToString();
                actionExecutedContext.Response.Headers.Add("X-Pagination", re);
            }
           

        }
    }
}