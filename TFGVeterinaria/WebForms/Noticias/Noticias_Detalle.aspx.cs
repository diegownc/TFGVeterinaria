using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TFGVeterinaria.Clases;

namespace TFGVeterinaria {
    public partial class Noticias_Detalle : Page {

        protected void Page_Load(object sender, EventArgs e) {
            Session["PAGE"] = "NOTICIAS";
            if (!IsPostBack) {
                alertaExito.Visible = false;
            }

            if(RouteData.Values["ID"] != null) {
                setDatos(Convert.ToInt32(RouteData.Values["ID"]));
                setEnabled(false);
            } else {
                Response.RedirectToRoute("ErrorPage", new { ERROR = "Se ha intentado acceder a una noticia que no existe" });
            }
        }


        private void setDatos(int ID) {
            string titulo = "";
            string descripcion = "";
            string contenido = "";
            try {
                Noticias_Class.getDatosNoticia(ID, ref titulo, ref descripcion, ref contenido);
                txtTitulo.Text = titulo;
                txtContenido.Text = descripcion + " " + contenido;
            } catch (Exception ex) {
                Libreria.addLog("setDatos", ex.StackTrace, ex.Message);
            }
        }


        private void setEnabled(bool enabled) {
            txtTitulo.Enabled = enabled;
            txtContenido.Enabled = enabled;
        }
    }
}