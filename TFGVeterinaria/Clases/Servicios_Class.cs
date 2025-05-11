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
                string query = "SELECT S.ID, S.TITULO, U.NOMBRE AS VETERINARIO, S.UBICACION, S.PRECIO, S.DESCRIPCION, S.IMAGEURL FROM SERVICIOS S, USUARIOS U WHERE S.USUARIO=U.USUARIO";
                using (var command = new NpgsqlCommand(query, conn)) {
                    using (var dataAdapter = new NpgsqlDataAdapter(command)) {
                        dataAdapter.Fill(dtServicios);
                    }
                }
            }
        }

    }
}