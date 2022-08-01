using Dapper;
using Pagination.Models;
using Pagination.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Pagination.Controllers
{
    [RoutePrefix("api/Pagination")]

    public class PaginationController:ApiController
    {
        //DbFunctions db = new DbFunctions();

        //[Route("Get/{currentPageNumber:int}/{pageSize:int}")]
        [Route("Get")]
        [HttpGet]
        public IHttpActionResult Get(int currentNumber, int pageSize)
        {
            PaginationModel resps = new PaginationModel();
            DbFunctions db = new DbFunctions();


            int maxPageSize = 50;
            pageSize = pageSize < maxPageSize ? pageSize : maxPageSize;
            //int skip = (currentPageNumber - 1) * pageSize; //how many records we have to skip
            int skip = currentNumber; //how many records we have to skip
            int take = pageSize;

            resps.TotalPages = take;
            resps.CurrentPageNumber = skip;

            var dbResp = db.GetData(resps);
            return Ok(dbResp);
          
            
        }
    }
}