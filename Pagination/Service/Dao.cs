using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Pagination.Service
{
    public class Dao
    {

        public static SqlConnection getConnection()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
            if (con.State != ConnectionState.Open)
            {
                try
                {
                    con.Open();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return con;
        }
        public static DataTable RunSQL(string sql)
        {
            using (SqlConnection con = getConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandTimeout = 950;
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }

        }
        public static string FilterString(string str)
        {
            if (str == "" || str == null)
            {
                return "NULL";
            }
            else
            {
                str = str.Replace(";", "");
                str = str.Replace("'", "''");
                str = str.Replace("--", "");
                str = str.Replace("<script>", "");
                str = str.Replace("</script>", "");

                return "'" + str + "'";
            }

        }

        public static string CommandBuilder(string procName, string flag, Dictionary<string, string> praramNames = null)
        {

            string sqlCommand = "Execute " + procName + " @flag = " + FilterString(flag);
            if (praramNames == null)
            {
                return sqlCommand;
            }
            for (int i = 0; i < praramNames.Count; i++)
            {
                sqlCommand += ", " + praramNames.Keys.ElementAt(i) + " =" + FilterString(praramNames[praramNames.Keys.ElementAt(i)]);
            }
            return sqlCommand;
        }
    }

}