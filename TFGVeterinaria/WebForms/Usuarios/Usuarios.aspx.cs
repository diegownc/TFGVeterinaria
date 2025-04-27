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
    public partial class Usuarios : Page {

        private static DataTable dtUsuarios;

        protected void Page_Load(object sender, EventArgs e) {
            Session["PAGE"] = "USUARIOS";
            if (Session["USR_PERFIL"] != null) {
                string perfil = Session["USR_PERFIL"].ToString();
                if (perfil == "ADMINISTRADOR") {
                    if (!Page.IsPostBack) {
                        dtUsuarios = new DataTable();
                        BindGridUsuarios();
                        alertaExito.Visible = false;
                    }
                } else {
                    Response.RedirectToRoute("ErrorPage", new { ERROR = "Se ha intentado acceder a una ZONA que solo es accesible por un administrador" });
                }
            } else {
                Response.RedirectToRoute("ErrorPage", new { ERROR = "Se ha intentado acceder a una ZONA que solo es accesible por un administrador" });
            }
        }

        private void BindGridUsuarios() {
            try {
                Usuarios_Class.ObtenerUsuarios(dtUsuarios);
                GridView1.DataSource = dtUsuarios;
                GridView1.DataBind();
            } catch (Exception ex) {
                Libreria.addLog("BindGridUsuarios", ex.StackTrace, ex.Message);
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e) {
            GridView1.EditIndex = e.NewEditIndex;
            BindGridUsuarios();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) {
            GridView1.EditIndex = -1;
            BindGridUsuarios();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e) {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            try {
                Label lblUsuario = (Label)row.FindControl("lblUsuario");
                string usuario = lblUsuario.Text;

                string nombre = ((TextBox)row.Cells[2].Controls[0]).Text;
                string email = ((TextBox)row.Cells[3].Controls[0]).Text;

                HtmlInputCheckBox chkActivo = (HtmlInputCheckBox)row.FindControl("chkActivo");
                int activo = chkActivo.Checked ? 1 : 0;

                HtmlInputCheckBox chkVerificado = (HtmlInputCheckBox)row.FindControl("chkVerificado");
                int verificado = chkVerificado.Checked ? 1 : 0;

                DropDownList ddlRoles = (DropDownList)row.FindControl("ddlRoles");
                string rol = ddlRoles.SelectedValue;

                Usuarios_Class.ActualizarUsuario(usuario, nombre, email, rol, activo, verificado);
                alertaExito.Visible = true;
            } catch (Exception ex) {
                Libreria.addLog("GridView1_RowUpdating", ex.StackTrace, ex.Message);
            }  

            GridView1.EditIndex = -1;
            BindGridUsuarios();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow && GridView1.EditIndex == e.Row.RowIndex) {
                DropDownList ddlRoles = (DropDownList)e.Row.FindControl("ddlRoles");
                if (ddlRoles != null) {
                    ddlRoles.DataSource = new List<string> { "GENERICO", "VETERINARIO", "ADMINISTRADOR" };
                    ddlRoles.DataBind();
                    ddlRoles.SelectedValue = DataBinder.Eval(e.Row.DataItem, "PERFIL").ToString();
                }
            }
        }
    }
}