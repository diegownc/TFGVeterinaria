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

        public static void ObtenerLecciones(DataTable dtLecciones) {
            if(dtLecciones == null) {
                dtLecciones = new DataTable();
            } else {
                dtLecciones.Clear();
            }

            
            using (var conn = new NpgsqlConnection(connectionString)) {
                conn.Open();

                string query = "SELECT N.ID, N.TITULO, N.DESCRIPCION, N.CONTENIDO, COALESCE(N.AUTOR, U.NOMBRE) AUTOR, N.FECHA, N.IMAGENURL, (SELECT COUNT(1) FROM NOTICIA_PREGUNTAS WHERE IDNOTICIA = N.ID) AS PREGUNTAS FROM NOTICIAS N LEFT JOIN USUARIOS U ON N.USUARIO = U.USUARIO WHERE N.ACTIVO=1 ORDER BY N.ID DESC";

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
                    cmd.Parameters.AddWithValue("contenido", descripcion + " " + contenido);
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

        public static bool DesactivarNoticia(int ID) {
            bool ok = false;

            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();
                string sql = "UPDATE NOTICIAS SET ACTIVO = 0 WHERE ID=@ID";
                using (var cmd = new NpgsqlCommand(sql, connection)) {
                    cmd.Parameters.AddWithValue("ID", ID);
                    cmd.ExecuteNonQuery();
                    ok = true;
                }
            }

            return ok;
        }

        public static void getDatosNoticia(int ID, ref string titulo, ref string descripcion, ref string contenido, ref string imageurl) {
            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();

                string sql = "SELECT TITULO, DESCRIPCION, CONTENIDO, IMAGENURL FROM NOTICIAS WHERE ID = @ID";
                using (var cmd = new NpgsqlCommand(sql, connection)) {
                    cmd.Parameters.AddWithValue("ID", ID);
                    using (var reader = cmd.ExecuteReader()) {
                        if (reader.Read()) {
                            titulo = reader.GetString(0);
                            descripcion = reader.GetString(1);
                            contenido = reader.GetString(2);
                            imageurl = reader.GetString(3);
                        }
                    }
                }
            }
        }


        public static bool ActualizarDatosNoticia(int ID, string titulo, string descripcion, string contenido) {
            bool ok = false;
            titulo = (titulo.Length > 200 ? titulo.Substring(0, 200) : titulo);
            descripcion = (descripcion.Length > 500 ? descripcion.Substring(0, 500) : descripcion);

            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();
                string sql = "UPDATE NOTICIAS SET titulo=@titulo, descripcion=@descripcion, contenido=@contenido WHERE ID=@ID";
                using (var cmd = new NpgsqlCommand(sql, connection)) {
                    cmd.Parameters.AddWithValue("titulo", titulo);
                    cmd.Parameters.AddWithValue("descripcion", descripcion);
                    cmd.Parameters.AddWithValue("contenido", contenido);
                    cmd.Parameters.AddWithValue("ID", ID);
                    cmd.ExecuteNonQuery();
                    ok = true;
                }
            }

            return ok;
        }


        public static void ObtenerPreguntas(DataTable dtPreguntas, int IDNoticia) {
            if (dtPreguntas == null) {
                dtPreguntas = new DataTable();
            } else {
                dtPreguntas.Clear();
            }

            using (var conn = new NpgsqlConnection(connectionString)) {
                conn.Open();

                string query = "SELECT ID, PREGUNTA, TEXTOADICIONAL, RESPUESTA_A, RESPUESTA_B, RESPUESTA_C, RESPUESTA_D, RESPUESTA FROM NOTICIA_PREGUNTAS WHERE IDNOTICIA = @IDNOTICIA ORDER BY ID DESC";

                using (var cmd = new NpgsqlCommand(query, conn)) {
                    cmd.Parameters.AddWithValue("IDNOTICIA", IDNoticia);
                    using (var dataAdapter = new NpgsqlDataAdapter(cmd)) {
                        dataAdapter.Fill(dtPreguntas);
                    }
                }
            }
        }

        public static void getPregunta(int ID, ref string pregunta, ref string textoadicional, ref string respuesta_a, ref string respuesta_b, ref string respuesta_c, ref string respuesta_d, ref string respuesta) {
            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();

                string sql = "SELECT PREGUNTA, TEXTOADICIONAL, RESPUESTA_A, RESPUESTA_B, RESPUESTA_C, RESPUESTA_D, RESPUESTA FROM NOTICIA_PREGUNTAS WHERE ID = @ID";
                using (var cmd = new NpgsqlCommand(sql, connection)) {
                    cmd.Parameters.AddWithValue("ID", ID);
                    using (var reader = cmd.ExecuteReader()) {
                        if (reader.Read()) {
                            pregunta = !reader.IsDBNull(0) ? reader.GetString(0) : string.Empty;
                            textoadicional = !reader.IsDBNull(1) ? reader.GetString(1) : string.Empty;
                            respuesta_a = !reader.IsDBNull(2) ? reader.GetString(2) : string.Empty;
                            respuesta_b = !reader.IsDBNull(3) ? reader.GetString(3) : string.Empty;
                            respuesta_c = !reader.IsDBNull(4) ? reader.GetString(4) : string.Empty;
                            respuesta_d = !reader.IsDBNull(5) ? reader.GetString(5) : string.Empty;
                            respuesta = !reader.IsDBNull(6) ? reader.GetString(6) : string.Empty;
                        }
                    }
                }
            }
        }

        public static bool ActualizarDatosPregunta(int ID, string pregunta, string textoadicional, string respuesta_a, string respuesta_b, string respuesta_c, string respuesta_d, string respuesta) {
            bool ok = false;
            pregunta = (pregunta.Length > 250 ? pregunta.Substring(0, 250) : pregunta);
            respuesta_a = (respuesta_a.Length > 100 ? respuesta_a.Substring(0, 100) : respuesta_a);
            respuesta_b = (respuesta_b.Length > 100 ? respuesta_b.Substring(0, 100) : respuesta_b);
            respuesta_c = (respuesta_c.Length > 100 ? respuesta_c.Substring(0, 100) : respuesta_c);
            respuesta_d = (respuesta_d.Length > 100 ? respuesta_d.Substring(0, 100) : respuesta_d);


            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();
                string sql = "UPDATE NOTICIA_PREGUNTAS SET pregunta=@pregunta, textoadicional=@textoadicional, respuesta_a=@respuesta_a, respuesta_b=@respuesta_b, respuesta_c=@respuesta_c, respuesta_d=@respuesta_d, respuesta=@respuesta WHERE ID=@ID";
                using (var cmd = new NpgsqlCommand(sql, connection)) {
                    cmd.Parameters.AddWithValue("ID", ID);
                    cmd.Parameters.AddWithValue("pregunta", pregunta);
                    cmd.Parameters.AddWithValue("textoadicional", textoadicional);
                    cmd.Parameters.AddWithValue("respuesta_a", respuesta_a);
                    cmd.Parameters.AddWithValue("respuesta_b", string.IsNullOrEmpty(respuesta_b) ? DBNull.Value : (object)respuesta_b);
                    cmd.Parameters.AddWithValue("respuesta_c", string.IsNullOrEmpty(respuesta_c) ? DBNull.Value : (object)respuesta_c);
                    cmd.Parameters.AddWithValue("respuesta_d", string.IsNullOrEmpty(respuesta_d) ? DBNull.Value : (object)respuesta_d);
                    cmd.Parameters.AddWithValue("respuesta", respuesta);
                    cmd.ExecuteNonQuery();
                    ok = true;
                }
            }

            return ok;
        }


        public static bool InsertarPreguntaVacia(int idnoticia) {
            bool ok = false;
            string pregunta = "";
            string textoAdicional = "";
            string respuesta_a = "";
            string respuesta = "A";


            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();
                string sql = "INSERT INTO NOTICIA_PREGUNTAS (pregunta, idnoticia, textoadicional, respuesta_a, respuesta_b, respuesta_c, respuesta_d, respuesta) VALUES (@pregunta, @idnoticia, @textoadicional, @respuesta_a, @respuesta_b, @respuesta_c, @respuesta_d, @respuesta)";
                using (var cmd = new NpgsqlCommand(sql, connection)) {
                    cmd.Parameters.AddWithValue("pregunta", pregunta);
                    cmd.Parameters.AddWithValue("idnoticia", idnoticia);
                    cmd.Parameters.AddWithValue("textoadicional", textoAdicional);
                    cmd.Parameters.AddWithValue("respuesta_a", respuesta_a);
                    cmd.Parameters.AddWithValue("respuesta_b", DBNull.Value);
                    cmd.Parameters.AddWithValue("respuesta_c", DBNull.Value);
                    cmd.Parameters.AddWithValue("respuesta_d", DBNull.Value);
                    cmd.Parameters.AddWithValue("respuesta", respuesta);
                    cmd.ExecuteNonQuery();
                    ok = true;
                }
            }

            return ok;
        }

        public static bool EliminarPregunta(int id) {
            bool ok = false;

            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();
                string sql = "DELETE FROM NOTICIA_PREGUNTAS WHERE ID=@ID";
                using (var cmd = new NpgsqlCommand(sql, connection)) {
                    cmd.Parameters.AddWithValue("ID", id);
                    cmd.ExecuteNonQuery();
                    ok = true;
                }
            }
            return ok;
        }
    }
}


