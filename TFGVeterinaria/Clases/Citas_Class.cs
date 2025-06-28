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
    public class Citas_Class {
        private static string connectionString = ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;

        public static void ObtenerCitas(DataTable dtCitas, string veterinario) {
            if (dtCitas == null) {
                dtCitas = new DataTable();
            } else {
                dtCitas.Clear();
            }

            using (var conn = new NpgsqlConnection(connectionString)) {
                conn.Open();

                string query = "SELECT C.ID, C.ESTADO, C.FECHA, C.HORA, C.DESCRIPCION,  S.DESCRIPCION AS SERVICIO, U.NOMBRE AS USUARIO, M.NOMBRE AS MASCOTA FROM CITAS  C, USUARIOS U, MASCOTAS M, SERVICIOS S WHERE C.USUARIO = U.USUARIO AND C.IDMASCOTA = M.ID AND C.IDSERVICIO = S.ID AND upper(c.VETERINARIO) = @VETERINARIO ORDER BY FECHA DESC";
                using (var command = new NpgsqlCommand(query, conn)) {
                    command.Parameters.AddWithValue("veterinario", veterinario.ToUpper());
                    using (var dataAdapter = new NpgsqlDataAdapter(command)) {
                        dataAdapter.Fill(dtCitas);
                    }
                }
            }
        }

        public static bool ActualizarCita(int idCita, int estado) {
            bool ok = false;

            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();
                string sql = "UPDATE CITAS SET ESTADO = @ESTADO WHERE ID = @ID";
                using (var cmd = new NpgsqlCommand(sql, connection)) {
                    cmd.Parameters.AddWithValue("ID", idCita);
                    cmd.Parameters.AddWithValue("ESTADO", estado);
                    cmd.ExecuteNonQuery();
                    ok = true;
                }
            }
            return ok;
        }
    }
}