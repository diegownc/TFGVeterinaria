using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Npgsql;

namespace TFGVeterinaria.Clases
{
    public class Login_Class
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;

        public static bool ExisteUsuario(string username)
        {
            bool ok = false;
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT COUNT(1) FROM usuarios WHERE upper(usuario) = @username";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    // Parámetros para evitar SQL injection
                    cmd.Parameters.AddWithValue("username", username.ToUpper());

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    ok = count > 0;
                }
            }
            return ok;
        }

        public static bool ExisteUsuarioBloqueado(string username) {
            bool ok = false;
            using (var conn = new NpgsqlConnection(connectionString)) {
                conn.Open();

                string query = "SELECT COUNT(1) FROM usuarios WHERE upper(usuario) = @username and activo=0";

                using (var cmd = new NpgsqlCommand(query, conn)) {
                    // Parámetros para evitar SQL injection
                    cmd.Parameters.AddWithValue("username", username.ToUpper());

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    ok = count > 0;
                }
            }
            return ok;
        }

        public static bool CompruebaPassword(string username, string enteredPassword){
            bool ok = false;
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                
                string sql = "SELECT clave, salt FROM usuarios WHERE upper(usuario) = @username";
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("username", username.ToUpper());
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHash = reader.GetString(0);
                            string storedSalt = reader.GetString(1);

                            // Hash la contraseña ingresada con el mismo salt
                            string enteredPasswordHash = Login_Class.HashPassword(enteredPassword, storedSalt);

                            // Comparar los hashes
                            ok = storedHash == enteredPasswordHash;
                        }
                        else
                        {
                            ok = false; // Usuario no encontrado
                        }
                    }
                }
            }

            return ok;
        }

        public static bool Registrar(string usuario, string password, string nombre, string perfil, string email)
        {
            bool ok = false;

            // Generar un salt único para cada usuario
            string salt = Login_Class.GenerateSalt();

            // Generar el hash de la contraseña
            string hashedPassword = Login_Class.HashPassword(password, salt);

            // Insertar el usuario con el hash y el salt en PostgreSQL
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO usuarios (usuario, nombre, clave, salt, activo, perfil, email, verificado) VALUES (@usuario, @nombre, @passwordHash, @salt, 1, @perfil, @email, 0)";
                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("usuario", usuario);
                    cmd.Parameters.AddWithValue("nombre", nombre);
                    cmd.Parameters.AddWithValue("passwordHash", hashedPassword);
                    cmd.Parameters.AddWithValue("salt", salt);
                    cmd.Parameters.AddWithValue("perfil", perfil);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.ExecuteNonQuery();
                    ok = true;
                }
            }

            return ok;
        }
       
        private static byte[] PBKDF2(string password, string salt, int iterations, int length)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), iterations))
            {
                return pbkdf2.GetBytes(length); // Devuelve los bytes generados
            }
        }

        // PBKDF2
        public static string HashPassword(string password, string salt) {
            string res = string.Empty;
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(salt))) {
                // Generar el hash con PBKDF2
                byte[] hashBytes = PBKDF2(password, salt, 10000, 32); // 10000 es el número de iteraciones
                res = Convert.ToBase64String(hashBytes); // Devuelve el hash en formato base64
            }

            return res;
        }

        // SALT
        public static string GenerateSalt()
        {
            string res = string.Empty;
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] saltBytes = new byte[16]; 
                rng.GetBytes(saltBytes);
                res = Convert.ToBase64String(saltBytes);
            }
            return res;
        }

        public static void getDatosUsuario(ref string usuario, ref string nombre, ref string email, ref int verificado, ref string perfil) {
            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();

                string sql = "SELECT usuario, nombre, email, verificado, perfil FROM usuarios WHERE upper(usuario) = @username";
                using (var cmd = new NpgsqlCommand(sql, connection)) {
                    cmd.Parameters.AddWithValue("username", usuario.ToUpper());
                    using (var reader = cmd.ExecuteReader()) {
                        if (reader.Read()) {
                            usuario = reader.GetString(0);
                            nombre = reader.GetString(1);
                            email = reader.GetString(2);
                            verificado = reader.GetInt16(3);
                            perfil = reader.GetString(4);
                        }
                    }
                }
            }
        }
    }
}