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
    public partial class ErrorPage : Page {

        protected void Page_Load(object sender, EventArgs e) {
            if(RouteData.Values["ERROR"] != null) {
                errormsg.InnerText = RouteData.Values["ERROR"].ToString();
            }
        }


    }
}