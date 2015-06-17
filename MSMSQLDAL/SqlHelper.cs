using System.Data;
using System.Data.SqlClient;

namespace Itcast.CMS.DAL
{
    public class SqlHelper
    {
       private readonly static string connStr = Common.ConfigurationHelper.ConnectionString("ConnStr");

       public static DataTable GetTable(string sql, CommandType type, params SqlParameter[] pars)
       {
           using (SqlConnection conn = new SqlConnection(connStr))
           {
               using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conn))
               {
                   adapter.SelectCommand.CommandType = type;
                   if (pars != null)
                   {
                       adapter.SelectCommand.Parameters.AddRange(pars);
                   }
                   DataTable da = new DataTable();
                   adapter.Fill(da);
                   return da;
               }
           }
       }

       public static int ExecuteNonQuery(string sql, CommandType type, params SqlParameter[] pars)
       {
           using (SqlConnection conn = new SqlConnection(connStr))
           {
               using (SqlCommand cmd = new SqlCommand(sql, conn))
               {
                   cmd.CommandType = type;
                   if (pars != null)
                   {
                       cmd.Parameters.AddRange(pars);
                   }
                   conn.Open();
                   return cmd.ExecuteNonQuery();
               }
           }
       }

       public static object ExecuteScalar(string sql, CommandType type, params SqlParameter[] pars)
       {
           using (SqlConnection conn = new SqlConnection(connStr))
           {
               using (SqlCommand cmd = new SqlCommand(sql, conn))
               {
                   cmd.CommandType = type;
                   if (pars != null)
                   {
                       cmd.Parameters.AddRange(pars);
                   }
                   conn.Open();
                   return cmd.ExecuteScalar();
               }
           }
       }
    }
}
