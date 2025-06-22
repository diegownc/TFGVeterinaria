using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TFGVeterinaria.Clases;

namespace TFGVeterinaria
{
    public partial class Comunidad : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnLogin_ServerClick(object sender, EventArgs e)
        {
            try{
                lblMessage.Attributes["class"] = "";
                lblMessage.InnerText = "";

                if (Login_Class.ExisteUsuario(txtUserName.Text)){
                    if (!Login_Class.ExisteUsuarioBloqueado(txtUserName.Text)) {
                        if (Login_Class.CompruebaPassword(txtUserName.Text, txtPassword.Text)) {
                            string usuario = txtUserName.Text;
                            string nombre = string.Empty;
                            string email = string.Empty;
                            int verificado = 0;
                            string perfil = string.Empty;
                            
                            Login_Class.getDatosUsuario(ref usuario, ref nombre, ref email, ref verificado, ref perfil);
                            Session["USR_USUARIO"] = usuario;
                            Session["USR_NOMBRE"] = nombre;
                            Session["USR_PERFIL"] = perfil;
                            Session["USR_EMAIL"] = email;
                            Session["USR_VERIFICADO"] = verificado;
                            Session["PAGE"] = "SERVICIOS";
                            Response.RedirectToRoute("ServiciosRoute");
                        } else {
                            lblMessage.Attributes["class"] = "error-message";
                            lblMessage.InnerText = "La contraseña no es correcta";
                        }
                    } else {
                        lblMessage.Attributes["class"] = "error-message";
                        lblMessage.InnerText = "El usuario esta bloqueado, contacte con un administrador";
                    }
                } else{
                    lblMessage.Attributes["class"] = "error-message";
                    lblMessage.InnerText = "Usuario no existe";
                }
            }catch(Exception ex){
                Libreria.addLog("btnLogin_ServerClick", ex.StackTrace, ex.Message);
            }
            
        }
    }
}