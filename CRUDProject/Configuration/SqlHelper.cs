using CRUDProject.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime;

namespace CRUDProject.Configuration
{
    public class SqlHelper
    {
        public static SqlConnection DatabaseConnect(ISettings settings, ILogging logging)
        {
            try
            {
                var connString = settings.ConnectionString;

                return new SqlConnection(connString);

            }
            catch (Exception ex)
            {
                logging.TrapError(ex);

                return null;
            }
        }

        public static void ExecuteNonQuery(SqlConnection conn, string sql, ref SqlParameter[] paramList)
        {
            using (var command = new SqlCommand(sql, conn))
            {
                conn.Open();

                command.CommandType = CommandType.Text;
                command.Parameters.AddRange(paramList);
                command.ExecuteNonQuery();
            }
        }

        public static void ExecuteNonQuery(SqlConnection conn, string sql)
        {
            using (var command = new SqlCommand(sql, conn))
            {
                conn.Open();

                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();

            }
        }

        public static SqlDataReader ExecuteReader(SqlConnection conn, string sql, SqlParameter[] paramList = null)
        {
            using (var command = new SqlCommand(sql, conn))
            {
                conn.Open();

                command.CommandType = CommandType.Text;

                if (paramList != null)
                    command.Parameters.AddRange(paramList);

                var output = command.ExecuteReader();

                return output;
            }
        }
    }
}
