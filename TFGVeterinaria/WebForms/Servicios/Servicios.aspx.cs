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
    public partial class Servicios : Page {

        private static DataTable dtServicios;

        protected void Page_Load(object sender, EventArgs e) {
            Session["PAGE"] = "SERVICIOS";
            if (!Page.IsPostBack) {
                dtServicios = new DataTable();
                BindGridServicios();
            }
        }

        private void BindGridServicios() {
            try {
                Servicios_Class.ObtenerServicios(dtServicios);
                lvServicios.DataSource = dtServicios;
                lvServicios.DataBind();
            } catch (Exception ex) {
                Libreria.addLog("BindGridServicios", ex.StackTrace, ex.Message);
            }
        }
    
    }
}