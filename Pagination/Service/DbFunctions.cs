using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Pagination.Models;
using Pagination.Models.Test;

namespace Pagination.Service
{
    public class DbFunctions
    {
        public static SqlConnection GetConnection()
        {
            var connectionStrings = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            var con = new SqlConnection(connectionStrings);
            return con;
        }
        public CommonResponse GetData(PaginationModel resps)
        {
            CommonResponse res = new CommonResponse();
            string sql = string.Empty;
            DataTable dbResp = null;
            try
            {
                sql = "exec sproc_sanko @flag='p'";
                sql += ",@skip=" + Dao.FilterString(resps.CurrentPageNumber.ToString());
                sql += ",@take=" + Dao.FilterString(resps.TotalPages.ToString());

                dbResp = Dao.RunSQL(sql);

            }
            catch (Exception)
            {

                return new CommonResponse()
                {
                    code = "2",
                    message = "Exception",
                    errors = new List<Errors>()
                    {
                        new Errors()
                        {
                            error_code = "800",
                            error_message= "API Exception"
                        }
                    }
                };
            }
            if (dbResp.Rows.Count == 0)
            {
                return new CommonResponse()
                {
                    code = "1",
                    message = "Error",
                    errors = new List<Errors>()
                    {
                        new Errors()
                        {
                            error_code = "100",
                            error_message = "No  List Available"
                        }
                    }
                };
            }

            if (dbResp != null)
            {
                if (dbResp.Rows.Count > 0)
                {

                    //var data = new CommonResponse()
                    //{
                    //    code = "0",
                    //    message = "success",
                    //    data = new List<Todo()
                    //    {
                    //        id = dbResp.Rows[0]["id"].ToString(),
                    //        Quantity = dbResp.Rows[0]["quantity"].ToString(),
                    //        Amount = dbResp.Rows[0]["amount"].ToString(),
                    //        Category = dbResp.Rows[0]["category"].ToString(),
                    //        Size = dbResp.Rows[0]["size"].ToString(),
                    //        //code = dbResp.Rows[0]["code"].ToString(),
                    //        //message = dbResp.Rows[0]["message"].ToString(),

                    //    }
                    //};
                    //return data;

                    List<Todo> lst = new List<Todo>();
                    foreach (DataRow dr in dbResp.Rows)
                    {
                        Todo model = new Todo();
                        model.id = dbResp.Rows[0]["id"].ToString();
                        model.Quantity = dbResp.Rows[0]["quantity"].ToString();
                        model.Amount = dbResp.Rows[0]["amount"].ToString();
                        model.Category = dbResp.Rows[0]["category"].ToString();
                        model.Size = dbResp.Rows[0]["size"].ToString();
                        lst.Add(model);
                    }
                    res.code = "0";
                    res.message = "success";
                    res.data = lst;


                }
                else
                {
                    res.code = "1";
                    res.message = "Error";
                    res.errors = new List<Errors> { new Errors { error_code = "101", error_message = "Something Went Wrong!" } };
                }
            }
            else
            {
                res.code = "1";
                res.message = "Error";
                res.errors = new List<Errors> { new Errors { error_code = "101", error_message = "Something Went Wrong!" } };
            }
            return res;
        }


        public List<Todo> GetPaginationData(PaginationParams resps)
        {
            CommonResponse resp = new CommonResponse();
            List<Todo> todoList = new List<Todo>();

            string sql = string.Empty;
            DataTable dbResp = null;

            sql = "exec sproc_sanko @flag='p'";
            sql += ",@skip=" + Dao.FilterString(resps.Skip.ToString());
            sql += ",@take=" + Dao.FilterString(resps.Take.ToString());

            dbResp = Dao.RunSQL(sql);



            if (dbResp != null)
            {
                if (dbResp.Rows.Count > 0)
                {
                    foreach (DataRow dr in dbResp.Rows)
                    {
                        Todo model = new Todo();
                        model.id = dr["id"].ToString();
                        model.Quantity = dr["quantity"].ToString();
                        model.Amount = dr["amount"].ToString();
                        model.Category = dr["category"].ToString();
                        model.Size = dr["size"].ToString();
                        model.Total = dr["total"].ToString();
                        todoList.Add(model);
                    }

                }

            }

            return todoList;

        }


        public TodoCommon GetTotalValue()
        {
            var sql = "EXEC sproc_sanko @flag='t'";
            var dt = Dao.RunSQL(sql);
            TodoCommon todo = new TodoCommon();
            if (dt != null)
            {
                todo.Total = dt.Rows[0]["total"].ToString();
               }
            return todo;
        }
    }
}

