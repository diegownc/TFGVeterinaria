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
    public class Servicios_Class {
        private static string connectionString = ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;

        public static void ObtenerServicios(DataTable dtServicios) {
            if (dtServicios == null) {
                dtServicios = new DataTable();
            } else {
                dtServicios.Clear();
            }

            using (var conn = new NpgsqlConnection(connectionString)) {
                conn.Open();
                string query = "SELECT S.ID, S.TITULO, U.NOMBRE AS VETERINARIO, S.UBICACION, S.PRECIO, S.DESCRIPCION, S.IMAGEURL FROM SERVICIOS S, USUARIOS U WHERE S.USUARIO=U.USUARIO AND S.ACTIVO = 1";
                using (var command = new NpgsqlCommand(query, conn)) {
                    using (var dataAdapter = new NpgsqlDataAdapter(command)) {
                        dataAdapter.Fill(dtServicios);
                    }
                }
            }
        }

        public static void getDatosServicio(int ID, ref string titulo, ref string descripcion, ref string ubicacion, ref float precio, ref string imageurl) {
            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();

                string sql = "SELECT S.TITULO, S.UBICACION, S.PRECIO, S.DESCRIPCION, S.IMAGEURL FROM SERVICIOS S, USUARIOS U WHERE S.USUARIO=U.USUARIO AND S.ID = @ID";
                using (var cmd = new NpgsqlCommand(sql, connection)) {
                    cmd.Parameters.AddWithValue("ID", ID);
                    using (var reader = cmd.ExecuteReader()) {
                        if (reader.Read()) {
                            titulo = reader.GetString(0);
                            ubicacion = reader.GetString(1);
                            precio = ((float)reader.GetDouble(2));
                            descripcion = reader.GetString(3);
                            imageurl = reader.GetString(4);
                        }
                    }
                }
            }
        }

        public static bool DesactivarServicio(int ID) {
            bool ok = false;

            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();
                string sql = "UPDATE SERVICIOS SET ACTIVO = 0 WHERE ID=@ID";
                using (var cmd = new NpgsqlCommand(sql, connection)) {
                    cmd.Parameters.AddWithValue("ID", ID);
                    cmd.ExecuteNonQuery();
                    ok = true;
                }
            }

            return ok;
        }

    }
}