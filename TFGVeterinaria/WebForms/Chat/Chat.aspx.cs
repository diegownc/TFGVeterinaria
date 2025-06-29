using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TFGVeterinaria.Clases;

namespace TFGVeterinaria
{
    public partial class Chat : Page
    {
        private static DataTable dtChat;
        private static DataTable dtUsuarios;

        protected void Page_Load(object sender, EventArgs e) {
            Session["PAGE"] = "CHAT";
            if (!IsPostBack) {
                dtChat = new DataTable();
                dtUsuarios = new DataTable();

                if (Session["USR_PERFIL"] == null) {
                    Response.RedirectToRoute("ErrorPage", new { ERROR = "Se ha intentado acceder a una ZONA que solo es accesible por un usuario registrado" });
                } else {
                    LoadUsuarios();
                }
            }           
        }

        private void LoadChat(string usuario_dos) {
            try {
                Chat_Class.ObtenerChat(dtChat, Session["USR_USUARIO"].ToString(), usuario_dos);
                gvChat.DataSource = dtChat;
                gvChat.DataBind();
                gvChat.HeaderRow.Cells[0].Text = Chat_Class.ObtenerNombre(usuario_dos);
                destinatario.Value = usuario_dos; 
            } catch (Exception ex) {
                Libreria.addLog("LoadChat", ex.StackTrace, ex.Message);
            }
        }

        private void LoadUsuarios() {
            try {
                Chat_Class.ObtenerUsuariosChat(dtUsuarios, Session["USR_USUARIO"].ToString());
                gvUsuariosChat.DataSource = dtUsuarios;
                gvUsuariosChat.DataBind();

                if (dtUsuarios.Rows.Count > 0) {
                    LoadChat(dtUsuarios.Rows[0]["USUARIO"].ToString());
                }

            } catch (Exception ex) {
                Libreria.addLog("LoadUsuarios", ex.StackTrace, ex.Message);
            }
        }

        protected void gvChat_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                Panel mensaje = (Panel)e.Row.FindControl("panelmensaje");
                HiddenField usr1 = (HiddenField)e.Row.FindControl("usuario_uno");
                if (mensaje != null && usr1 != null) {
                    if (usr1.Value == Session["USR_USUARIO"].ToString().ToUpper()) {
                        mensaje.CssClass= "userpanel";
                    } else {
                        mensaje.CssClass = "";
                    }
                }
            }
        }

        protected void gvUsuariosChat_RowCommand(object sender, GridViewCommandEventArgs e) {
            string usuario = Convert.ToString(e.CommandArgument);
            if (e.CommandName == "ACCEDER_CHAT") {
                LoadChat(usuario);
            }
        }

        protected void btEnviar_Click(object sender, EventArgs e) {
            try {

                if (destinatario.Value != "" && txtEnviar.Text != "") {
                    Chat_Class.AddMensaje(Session["USR_USUARIO"].ToString(), destinatario.Value, txtEnviar.Text);
                    LoadChat(destinatario.Value);
                }
            } catch(Exception ex) {
                Libreria.addLog("btEnviar_Click", ex.StackTrace, ex.Message);
            }
        }
    }
}