using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TFGVeterinaria.Clases;

namespace TFGVeterinaria
{
    public partial class Login : Page
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
                    
                    if (Login_Class.CompruebaPassword(txtUserName.Text, txtPassword.Text)) {
                        Response.Redirect("~/Acercade");
                    } else {
                        lblMessage.Attributes["class"] = "error-message";
                        lblMessage.InnerText = "La contraseña no es correcta";
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