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
    public partial class Noticias : Page {
        private static DataTable dtLecciones;

        protected void Page_Load(object sender, EventArgs e) {
            Session["PAGE"] = "NOTICIAS";
            if (!IsPostBack) {
                dtLecciones = new DataTable();
                LoadLecciones();
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e) {
            try {
                Noticias_Class.ActualizarDatosDesdeAPI();
                Noticias_Class.ObtenerLecciones(dtLecciones);
                gvLecciones.DataSource = dtLecciones;
                gvLecciones.DataBind();
            } catch (Exception ex) {
                Libreria.addLog("btnActualizar_Click", ex.StackTrace, ex.Message);
            }
        }

        private void LoadLecciones() {
            try {
                Noticias_Class.ObtenerLecciones(dtLecciones);
                gvLecciones.DataSource = dtLecciones;
                gvLecciones.DataBind();
            } catch (Exception ex) {
                Libreria.addLog("LoadLecciones", ex.StackTrace, ex.Message);
            }
        }


        protected void gvLecciones_RowCommand(object sender, GridViewCommandEventArgs e) {
            int index = Convert.ToInt32(e.CommandArgument); // El ID de la lección
            if (e.CommandName == "Acceder") {
                // Lógica para acceder a la lección
                Response.Redirect("~/Leccion.aspx?id=" + index);
            } else if (e.CommandName == "Añadir") {
                // Lógica para añadir una nueva lección
                Response.Redirect("~/AñadirLeccion.aspx");
            }
        }
    }
}