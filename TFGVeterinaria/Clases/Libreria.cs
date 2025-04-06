using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Npgsql;
using WebGrease.Css.Ast.Selectors;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;

namespace TFGVeterinaria.Clases
{
    public class Libreria
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;

        public static void addLog(string ubicacion, string stackstraceStr, string error_messageStr)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO log_procesos (ubicacion, stacktrace, error_message, fecha) VALUES (@ubicacion, @stacktrace, @error_message, now())";
                    using (var cmd = new NpgsqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("ubicacion", ubicacion);
                        cmd.Parameters.AddWithValue("stacktrace", stackstraceStr);
                        cmd.Parameters.AddWithValue("error_message", error_messageStr);
                        cmd.ExecuteNonQuery();
                    }
                }
            }catch (Exception ex)
            {
                //TODO
                Console.WriteLine(ex.ToString());
            }
        }
    }
}