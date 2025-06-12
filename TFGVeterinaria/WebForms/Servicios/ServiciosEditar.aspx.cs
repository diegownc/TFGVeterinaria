using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TFGVeterinaria.Clases;

namespace TFGVeterinaria {
    public partial class ServiciosEditar : Page {

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
            string titulo = "";
            string descripcion = "";
            string ubicacion = "";
            float precio = 0.0f;
            string imageUrl = "";
            try {
                Servicios_Class.getDatosServicio(ID, ref titulo, ref descripcion, ref ubicacion, ref precio, ref imageUrl);

                txtTitulo.Text = titulo;
                txtDescripcion.Text = descripcion;
                txtUbicacion.Text = ubicacion;
                txtPrecio.Text = precio.ToString("F2", CultureInfo.InvariantCulture);
                ImagenServicio.ImageUrl = imageUrl;
            } catch (Exception ex) {
                Libreria.addLog("setDatos", ex.StackTrace, ex.Message);
                Response.RedirectToRoute("ErrorPage", new { ERROR = "Ha ocurrido un error inesperado " + ex.Message });
            }
        }

    }
}