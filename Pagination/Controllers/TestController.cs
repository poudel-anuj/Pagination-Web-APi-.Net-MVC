using Microsoft.AspNetCore.Mvc;
using Pagination.Models;
using Pagination.Models.Test;
using Pagination.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text.Json;
using Pagination.Filter;

namespace Pagination.Controllers
{
    [RoutePrefix("api/Test")]
    public class TestController : ApiController
    {
        DbFunctions db = new DbFunctions();
        [ResponseFilter]

        [System.Web.Http.Route("get")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetTask(int page, int items)
        {
            PaginationParams parm = new PaginationParams();
            TodoCommon common = new TodoCommon();
            Todo todo = new Todo();
            parm.Skip = (page - 1) * items;
            parm.Take = items;

            var lists = db.GetPaginationData(parm);
            common = db.GetTotalValue();


            todo = common.MapObject<Todo>();
            //var paginationMetaData = new PaginationMetaData(((List<Todo>)lists).Count(), page, items);
            var paginationMetaData = new PaginationMetaData(Int16.Parse(todo.Total), page, items);
            var response = Request.CreateResponse();
            Request.Properties.Add("flag", "Pagination");
            Request.Properties.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
            return Ok(lists);

        }

    }
}
