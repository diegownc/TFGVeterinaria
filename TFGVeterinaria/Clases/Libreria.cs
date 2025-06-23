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
using System.Data;

namespace TFGVeterinaria.Clases {
    public class Libreria {
        private static string connectionString = ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;

        public static void addLog(string ubicacion, string stackstraceStr, string error_messageStr) {
            try {
                using (var connection = new NpgsqlConnection(connectionString)) {
                    connection.Open();
                    string sql = "INSERT INTO log_procesos (ubicacion, stacktrace, error_message, fecha) VALUES (@ubicacion, @stacktrace, @error_message, now())";
                    using (var cmd = new NpgsqlCommand(sql, connection)) {
                        cmd.Parameters.AddWithValue("ubicacion", ubicacion);
                        cmd.Parameters.AddWithValue("stacktrace", stackstraceStr);
                        cmd.Parameters.AddWithValue("error_message", error_messageStr);
                        cmd.ExecuteNonQuery();
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void ObtenerLogs(DataTable dtLogs) {
            if (dtLogs == null) {
                dtLogs = new DataTable();
            } else {
                dtLogs.Clear();
            }

            using (var conn = new NpgsqlConnection(connectionString)) {
                conn.Open();

                string query = "SELECT ID, UBICACION, STACKTRACE, ERROR_MESSAGE, FECHA FROM LOG_PROCESOS ORDER BY ID DESC";

                using (var command = new NpgsqlCommand(query, conn)) {
                    // Crear un adaptador de datos para llenar el DataTable
                    using (var dataAdapter = new NpgsqlDataAdapter(command)) {
                        // Llenar el DataTable con los resultados de la consulta
                        dataAdapter.Fill(dtLogs);
                    }
                }
            }
        }
    }
}