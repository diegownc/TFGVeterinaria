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
    public partial class Noticias_Detalle : Page {
        private static DataTable dtPreguntas;

        protected void Page_Load(object sender, EventArgs e) {
            Session["PAGE"] = "NOTICIAS";
            if (!IsPostBack) {
                alertaExito.Visible = false;
                if (RouteData.Values["ID"] != null) {
                    setDatos(Convert.ToInt32(RouteData.Values["ID"]));
                    setEnabled(false);
                } else {
                    Response.RedirectToRoute("ErrorPage", new { ERROR = "Se ha intentado acceder a una noticia que no existe" });
                }

                if (Session["USR_PERFIL"] != null && (Session["USR_PERFIL"].ToString() == "VETERINARIO" || Session["USR_PERFIL"].ToString() == "ADMINISTRADOR")) {
                    EditarBtn.Visible = true;
                    dtPreguntas = new DataTable();
                    BindGridPreguntas();
                } else {
                    EditarBtn.Visible = false;
                    GridPreguntas.Visible = false;
                    btnCrearPregunta.Visible = false;
                }
                seleccionarArchivo.Visible = false;
                GuardarBtn.Visible = false;

                
            }
        }


        private void setDatos(int ID) {
            string titulo = "";
            string descripcion = "";
            string contenido = "";
            string imageUrl = "";
            try {
                Noticias_Class.getDatosNoticia(ID, ref titulo, ref descripcion, ref contenido, ref imageUrl);
                txtTitulo.Text = titulo;
                txtDescripcion.Text = descripcion;
                txtContenido.Text = contenido;
                ImagenNoticia.ImageUrl = imageUrl;
                ID_NOTICIA.Value = ID.ToString();
            } catch (Exception ex) {
                Libreria.addLog("setDatos", ex.StackTrace, ex.Message);
                Response.RedirectToRoute("ErrorPage", new { ERROR = "Ha ocurrido un error inesperado " + ex.Message });
            }
        }


        private void setEnabled(bool enabled) {
            txtTitulo.Enabled = enabled;
            txtContenido.Enabled = enabled;
            txtDescripcion.Enabled = enabled;
        }

        protected void Guardar_Click(object sender, EventArgs e) {
            try {
                Noticias_Class.ActualizarDatosNoticia(Convert.ToInt32(ID_NOTICIA.Value), txtTitulo.Text, txtDescripcion.Text, txtContenido.Text);
            } catch (Exception ex) {
                Libreria.addLog("Guardar_Click", ex.StackTrace, ex.Message);
                Response.RedirectToRoute("Guardar_Click", new { ERROR = "Ha ocurrido un error inesperado " + ex.Message });
            }
            alertaExito.Visible = true;
            GuardarBtn.Visible = false;
            EditarBtn.Visible = true;
            seleccionarArchivo.Visible = false;
            setEnabled(false);
        }

        protected void Editar_Click(object sender, EventArgs e) {
            setEnabled(true);
            GuardarBtn.Visible = true;
            seleccionarArchivo.Visible = true;
            EditarBtn.Visible = false;
        }

        #region "PREGUNTAS"

        private void BindGridPreguntas() {
            try {
                Noticias_Class.ObtenerPreguntas(dtPreguntas, Convert.ToInt32(ID_NOTICIA.Value));
                GridPreguntas.DataSource = dtPreguntas;
                GridPreguntas.DataBind();
            } catch (Exception ex) {
                Libreria.addLog("BindGridPreguntas", ex.StackTrace, ex.Message);
            }
        }

        protected void GridPreguntas_RowEditing(object sender, GridViewEditEventArgs e) {
            GridPreguntas.EditIndex = e.NewEditIndex;
            BindGridPreguntas();
        }

        protected void GridPreguntas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) {
            GridPreguntas.EditIndex = -1;
            BindGridPreguntas();
        }

        protected void GridPreguntas_RowUpdating(object sender, GridViewUpdateEventArgs e) {
            GridViewRow row = GridPreguntas.Rows[e.RowIndex];
            try {
                TextBox txtPregunta = (TextBox)row.FindControl("txtPregunta");
                string pregunta = txtPregunta.Text;

                TextBox txtTextoAdicional = (TextBox)row.FindControl("txtTextoAdicional");
                string textoAdicional = txtTextoAdicional.Text;

                int id = Convert.ToInt32(((TextBox)row.Cells[2].Controls[0]).Text);
                string respuesta_a = ((TextBox)row.Cells[3].Controls[0]).Text;
                string respuesta_b = ((TextBox)row.Cells[4].Controls[0]).Text;
                string respuesta_c = ((TextBox)row.Cells[5].Controls[0]).Text;
                string respuesta_d = ((TextBox)row.Cells[6].Controls[0]).Text;

                DropDownList ddlrespuestas = (DropDownList)row.FindControl("ddlrespuestas");
                string respuesta = ddlrespuestas.SelectedValue;

                Noticias_Class.ActualizarDatosPregunta(id, pregunta, textoAdicional, respuesta_a, respuesta_b, respuesta_c, respuesta_d, respuesta);
                alertaExito.Visible = true;
            } catch (Exception ex) {
                Libreria.addLog("GridPreguntas_RowUpdating", ex.StackTrace, ex.Message);
            }

            GridPreguntas.EditIndex = -1;
            BindGridPreguntas();
        }

        protected void GridPreguntas_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow && GridPreguntas.EditIndex == e.Row.RowIndex) {
                DropDownList ddlrespuestas = (DropDownList)e.Row.FindControl("ddlrespuestas");
                if (ddlrespuestas != null) {
                    ddlrespuestas.DataSource = new List<string> { "A", "B", "C", "D" };
                    ddlrespuestas.DataBind();
                    ddlrespuestas.SelectedValue = DataBinder.Eval(e.Row.DataItem, "RESPUESTA").ToString();
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header) {
                // Ocultamos la columna del ID
                e.Row.Cells[2].Visible = false;
            }
        }

        protected void GridPreguntas_RowCommand(object sender, GridViewCommandEventArgs e) {
            if (e.CommandName == "DeletePersonalizado") {
                string id = e.CommandArgument.ToString();
                try {
                    Noticias_Class.EliminarPregunta(Convert.ToInt32(id));
                } catch (Exception ex) {
                    Libreria.addLog("GridPreguntas_RowCommand", ex.StackTrace, ex.Message);
                }
                BindGridPreguntas();
            }
        }

        protected void btnCrearPregunta_Click(object sender, EventArgs e) {
            try {
                Noticias_Class.InsertarPreguntaVacia(Convert.ToInt32(ID_NOTICIA.Value));
            } catch (Exception ex) {
                Libreria.addLog("btnCrearPregunta_Click", ex.StackTrace, ex.Message);
            }
            GridPreguntas.EditIndex = 0;
            BindGridPreguntas();
        }

        
        #endregion

    }
}