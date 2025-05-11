using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TFGVeterinaria.Clases;

namespace TFGVeterinaria {
    public partial class ServiciosCita : Page {

        protected void Page_Load(object sender, EventArgs e) {
            Session["PAGE"] = "SERVICIOS";

            if (!Page.IsPostBack) {
                if (RouteData.Values["ID"] != null) {
                    setDatos(Convert.ToInt32(RouteData.Values["ID"]));
                } else {
                    //Response.RedirectToRoute("ErrorPage", new { ERROR = "Se ha intentado acceder a un servicio que no existe" });
                }
            }
        }


        private void setDatos(int ID) {
            if (Session["USR_EMAIL"] != null) {
                txtEmail.Text = Session["USR_EMAIL"].ToString();
                ddlMascotas.Visible = true;
            } else {
                ddlMascotas.Visible = false;
            }

                string titulo = "";
            string descripcion = "";
            string contenido = "";
            string imageUrl = "";
            try {
                Noticias_Class.getDatosNoticia(ID, ref titulo, ref descripcion, ref contenido, ref imageUrl);
                //txtTitulo.Text = titulo;
                //txtDescripcion.Text = descripcion;
                //txtContenido.Text = contenido;
                //ImagenNoticia.ImageUrl = imageUrl;
                //ID_NOTICIA.Value = ID.ToString();
            } catch (Exception ex) {
                Libreria.addLog("setDatos", ex.StackTrace, ex.Message);
                Response.RedirectToRoute("ErrorPage", new { ERROR = "Ha ocurrido un error inesperado " + ex.Message });
            }
        }

    }
}