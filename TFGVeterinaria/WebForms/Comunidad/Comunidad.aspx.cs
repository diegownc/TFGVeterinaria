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
    public partial class Comunidad : Page {
        private static DataTable dtPublicaciones;

        protected void Page_Load(object sender, EventArgs e) {
            Session["PAGE"] = "NOTICIAS";
            if (!IsPostBack) {
                dtPublicaciones = new DataTable();
                LoadLecciones();
            }


            if (DELETE_FIELD.Value != "") {
                int index = 0;
                try {
                    foreach (DataRow r in dtPublicaciones.Rows) {
                        if (r["ID"].ToString() == DELETE_FIELD.Value) {
                            break;
                        }
                        index++;
                    }

                    Noticias_Class.DesactivarNoticia(Convert.ToInt32(DELETE_FIELD.Value));

                    dtPublicaciones.Rows.RemoveAt(index);
                    gvPublicaciones.DataSource = dtPublicaciones;
                    gvPublicaciones.DataBind();
                } catch (Exception ex) {
                    Libreria.addLog("Eliminando Publicación", ex.StackTrace, ex.Message);
                }
                DELETE_FIELD.Value = "";
            }
        }

        private void LoadLecciones() {
            try {
                Noticias_Class.ObtenerLecciones(dtPublicaciones);
                gvPublicaciones.DataSource = dtPublicaciones;
                gvPublicaciones.DataBind();
            } catch (Exception ex) {
                Libreria.addLog("LoadLecciones", ex.StackTrace, ex.Message);
            }
        }


        protected void gvPublicaciones_RowCommand(object sender, GridViewCommandEventArgs e) {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Acceder") {
                Response.RedirectToRoute("noticiasDetalleRouteParam", new { ID = index });

            }
        }

        protected void gvPublicaciones_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                Button btnDelete = (Button)e.Row.FindControl("btnDelete");
                if (btnDelete != null) {
                    // Usamos Eval para obtener el valor del ID y lo pasamos a OnClientClick
                    string id = DataBinder.Eval(e.Row.DataItem, "ID").ToString();
                    btnDelete.OnClientClick = $"return showDeleteConfirmation('{id}');";
                }
                btnDelete.Visible = false;
                if (Session["USR_PERFIL"] != null && (Session["USR_PERFIL"].ToString() == "VETERINARIO" || Session["USR_PERFIL"].ToString() == "ADMINISTRADOR")) {
                    btnDelete.Visible = true;
                }
            }
        }
    }
}