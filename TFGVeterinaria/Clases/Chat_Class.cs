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
    public class Chat_Class {
        private static string connectionString = ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;


        public static void ObtenerChat(DataTable dtChat, string usuario_uno, string usuario_dos) {
            if (dtChat == null) {
                dtChat = new DataTable();
            } else {
                dtChat.Clear();
            }

            using (var conn = new NpgsqlConnection(connectionString)) {
                conn.Open();

                string query = "SELECT UPPER(C.USUARIO_UNO) AS USR1, UPPER(C.USUARIO_DOS) AS USR2, C.ID, U.NOMBRE, U2.NOMBRE, C.MENSAJE, C.FECHA  FROM CHAT C, USUARIOS U, USUARIOS U2 WHERE C.USUARIO_UNO = U.USUARIO AND C.USUARIO_DOS = U2.USUARIO AND ((UPPER(USUARIO_UNO) = @USUARIO_UNO AND UPPER(USUARIO_DOS) = @USUARIO_DOS) OR (UPPER(USUARIO_DOS) = @USUARIO_UNO AND UPPER(USUARIO_UNO) = @USUARIO_DOS))  ORDER BY FECHA ASC";
                using (var command = new NpgsqlCommand(query, conn)) {
                    command.Parameters.AddWithValue("USUARIO_UNO", usuario_uno.ToUpper());
                    command.Parameters.AddWithValue("USUARIO_DOS", usuario_dos.ToUpper());

                    using (var dataAdapter = new NpgsqlDataAdapter(command)) {
                        dataAdapter.Fill(dtChat);
                    }
                }
            }
        }

        public static void ObtenerUsuariosChat(DataTable dtUsuarios, string usuario) {
            if (dtUsuarios == null) {
                dtUsuarios = new DataTable();
            } else {
                dtUsuarios.Clear();
            }

            using (var conn = new NpgsqlConnection(connectionString)) {
                conn.Open();

                string query = "SELECT U.USUARIO, U.NOMBRE FROM CHAT C, USUARIOS U WHERE C.USUARIO_DOS = U.USUARIO AND UPPER(USUARIO_UNO) = @USUARIO UNION SELECT U.USUARIO, U.NOMBRE FROM CHAT C, USUARIOS U WHERE C.USUARIO_UNO = U.USUARIO AND UPPER(USUARIO_DOS) = @USUARIO";
                using (var command = new NpgsqlCommand(query, conn)) {
                    command.Parameters.AddWithValue("USUARIO", usuario.ToUpper());
                    
                    using (var dataAdapter = new NpgsqlDataAdapter(command)) {
                        dataAdapter.Fill(dtUsuarios);
                    }
                }
            }
        }

        public static string ObtenerNombre(string usuario) {
            string nombre = "";
            using (var conn = new NpgsqlConnection(connectionString)) {
                conn.Open();

                string query = "SELECT NOMBRE FROM USUARIOS WHERE UPPER(USUARIO)=@USUARIO";
                using (var cmd = new NpgsqlCommand(query, conn)) {
                    cmd.Parameters.AddWithValue("USUARIO", usuario.ToUpper());
                    using (var reader = cmd.ExecuteReader()) {
                        if (reader.Read()) {
                            nombre = reader.GetString(0);
                        }
                    }
                }
            }

            return nombre;
        }




        public static bool AddMensaje(string usuario_uno, string usuario_dos, string mensaje) {
            bool ok = false;

            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();
                string sql = "INSERT INTO CHAT (USUARIO_UNO, USUARIO_DOS, MENSAJE, FECHA) VALUES (@USUARIO_UNO, @USUARIO_DOS, @MENSAJE, CURRENT_TIMESTAMP)";
                using (var cmd = new NpgsqlCommand(sql, connection)) {
                    cmd.Parameters.AddWithValue("USUARIO_UNO", usuario_uno);
                    cmd.Parameters.AddWithValue("USUARIO_DOS", usuario_dos);
                    cmd.Parameters.AddWithValue("MENSAJE", mensaje);
                    cmd.ExecuteNonQuery();
                    ok = true;
                }
            }

            return ok;
        }





    }
}