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
    public class Usuarios_Class {
        private static string connectionString = ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;

        public static void ObtenerUsuarios(DataTable dtUsuarios) {
            if (dtUsuarios == null) {
                dtUsuarios = new DataTable();
            } else {
                dtUsuarios.Clear();
            }

            using (var conn = new NpgsqlConnection(connectionString)) {
                conn.Open();

                string query = "SELECT USUARIO, NOMBRE, EMAIL, ACTIVO, PERFIL, VERIFICADO FROM USUARIOS";
                using (var command = new NpgsqlCommand(query, conn)) {
                    using (var dataAdapter = new NpgsqlDataAdapter(command)) {
                        dataAdapter.Fill(dtUsuarios);
                    }
                }
            }
        }

        public static bool ActualizarUsuario(string usuario, string nombre, string email, string perfil, int activo, int verificado) {
            bool ok = false;

            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();
                string sql = "UPDATE USUARIOS SET NOMBRE = @nombre, EMAIL = @email, PERFIL = @perfil, ACTIVO = @activo, VERIFICADO = @verificado WHERE USUARIO = @usuario";
                using (var cmd = new NpgsqlCommand(sql, connection)) {
                    cmd.Parameters.AddWithValue("nombre", nombre);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("perfil", perfil);
                    cmd.Parameters.AddWithValue("activo", activo);
                    cmd.Parameters.AddWithValue("verificado", verificado);
                    cmd.Parameters.AddWithValue("usuario", usuario);
                    cmd.ExecuteNonQuery();
                    ok = true;
                }
            }

            return ok;
        }
    }
}