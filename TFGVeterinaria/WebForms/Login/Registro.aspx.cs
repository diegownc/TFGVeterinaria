using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TFGVeterinaria
{
    public partial class Registro : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnRegistrarse_ServerClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text)) {
                
            }


        }


    }
}