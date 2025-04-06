using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TFGVeterinaria.Clases;

namespace TFGVeterinaria
{
    public partial class Registro : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnRegistrarse_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (!Login_Class.ExisteUsuario(txtUserName.Text)){
                    string perfil = "GENERICO";
                    if (chkVeterinario.Checked) {
                        perfil = "VETERINARIO";
                    } 
                    bool ok = Login_Class.Registrar(txtUserName.Text, txtPassword.Text, txtName.Text, perfil, txtEmail.Text);

                    if (ok) Response.RedirectToRoute("loginRoute");
                    

                }else{
                    lblMessage.Attributes["class"] = "error-message";
                    lblMessage.InnerText = "Ya existe el usuario '" + txtUserName.Text + "'";
                }
            }
            catch (Exception ex)
            {
                Libreria.addLog("btnRegistrarse_ServerClick", ex.StackTrace, ex.Message);
            }
        }


    }
}