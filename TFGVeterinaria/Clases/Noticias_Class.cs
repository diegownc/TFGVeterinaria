using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Npgsql;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections;
using System.Web.DynamicData;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.CompilerServices;

namespace TFGVeterinaria.Clases
{
    public class Source {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Article {
        public Source Source { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Content { get; set; }
    }

    public class NewsResponse {
        public string Status { get; set; }
        public int TotalResults { get; set; }
        public List<Article> Articles { get; set; }
    }

    public class Noticias_Class
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

        public static void ObtenerLecciones(DataTable dtLecciones) {
            if(dtLecciones == null) {
                dtLecciones = new DataTable();
            } else {
                dtLecciones.Clear();
            }

            
            using (var conn = new NpgsqlConnection(connectionString)) {
                conn.Open();

                string query = "SELECT N.ID, N.TITULO, N.DESCRIPCION, N.CONTENIDO, COALESCE(N.AUTOR, U.NOMBRE) AUTOR, N.FECHA, N.IMAGENURL FROM NOTICIAS N LEFT JOIN USUARIOS U ON N.USUARIO = U.USUARIO ORDER BY N.ID DESC";

                using (var command = new NpgsqlCommand(query, conn)) {
                    // Crear un adaptador de datos para llenar el DataTable
                    using (var dataAdapter = new NpgsqlDataAdapter(command)) {
                        // Llenar el DataTable con los resultados de la consulta
                        dataAdapter.Fill(dtLecciones);
                    }
                }
            }
        }



        public static string LlamarAPINoticiasGet() {
            // Parámetros
            string baseUrl = "https://newsapi.org/v2/everything";
            string query = "mascota medicina"; // Términos de búsqueda
            string language = "es"; 
            string apiKey = "ff3b487005ae48bcae0b88caa43c2a07"; 

            // Codificar los parámetros en la URL
            string encodedQuery = Uri.EscapeDataString(query); // Codificar la cadena de búsqueda
            string url = $"{baseUrl}?q={encodedQuery}&language={language}&apiKey={apiKey}";

            try {
                using (HttpClient client = new HttpClient()) {
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    
                    // Realizamos la solicitud GET de manera sincrónica
                    HttpResponseMessage response = client.GetAsync(url).Result;  

                    if (response.IsSuccessStatusCode) {
                        string result = response.Content.ReadAsStringAsync().Result;  
                        return result;  
                    } else {
                        return $"Error: {response.StatusCode}";
                    }
                }
            } catch (Exception ex) {
                return $"Excepcion: {ex.Message}";
            }
        }

        public static void ActualizarDatosDesdeAPI() {
            string resultado = Noticias_Class.LlamarAPINoticiasGet();

            if(resultado.StartsWith("Error:") || resultado.StartsWith("Excepcion:")) {
                throw new Exception(resultado);
            } else {
                NewsResponse respuesta = JsonConvert.DeserializeObject<NewsResponse>(resultado);
                if (respuesta != null) {
                    foreach (var articulo in respuesta.Articles) {
                        Console.WriteLine("-----------------------------------------");
                        Console.WriteLine($"Título: {articulo.Title}");
                        Console.WriteLine($"Autor: {articulo.Author}");
                        Console.WriteLine($"Descripción: {articulo.Description}");
                        Console.WriteLine($"Fuente: {articulo.Source.Name}");
                        Console.WriteLine($"Publicado en: {articulo.PublishedAt.ToShortDateString()}");
                        Console.WriteLine($"URL: {articulo.Url}");
                        Console.WriteLine($"Imagen URL: {articulo.UrlToImage}");
                        Console.WriteLine("Contenido: " + articulo.Content);
                        Console.WriteLine("-----------------------------------------");

                        AddNoticia(articulo.Title, articulo.Description, articulo.Content, articulo.Author, "", articulo.PublishedAt, articulo.UrlToImage);
                    }
                }
            }
        }



        public static bool AddNoticia(string titulo, string descripcion, string contenido, string autor, string usuario, DateTime fecha, string ImagenURL) {
            bool ok = false;

            titulo = (titulo.Length > 200 ? titulo.Substring(0, 200) : titulo);
            descripcion = (descripcion.Length > 500 ? descripcion.Substring(0, 500) : descripcion);
            autor = (autor.Length > 500 ? autor.Substring(0, 500) : autor);
            ImagenURL = (ImagenURL.Length > 500 ? ImagenURL.Substring(0, 500) : ImagenURL);


            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();
                string sql = "INSERT INTO noticias (titulo, descripcion, contenido, autor, usuario, fecha, ImagenURL) VALUES (@titulo, @descripcion, @contenido, @autor, @usuario, @fecha, @ImagenURL)";
                using (var cmd = new NpgsqlCommand(sql, connection)) {
                    cmd.Parameters.AddWithValue("titulo", titulo);
                    cmd.Parameters.AddWithValue("descripcion", descripcion);
                    cmd.Parameters.AddWithValue("contenido", contenido);
                    cmd.Parameters.AddWithValue("autor", string.IsNullOrEmpty(autor) ? DBNull.Value : (object)autor);
                    cmd.Parameters.AddWithValue("usuario", string.IsNullOrEmpty(usuario) ? DBNull.Value : (object)usuario);
                    cmd.Parameters.AddWithValue("fecha", fecha);
                    cmd.Parameters.AddWithValue("ImagenURL", ImagenURL);
                    cmd.ExecuteNonQuery();
                    ok = true;
                }
            }

            return ok;
        }
    }
}


