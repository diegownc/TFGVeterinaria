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
    public partial class LogSistema : Page {

        private static DataTable dtLogs;

        protected void Page_Load(object sender, EventArgs e) {
            Session["PAGE"] = "LOGS";
            if (Session["USR_PERFIL"] != null) {
                string perfil = Session["USR_PERFIL"].ToString();
                if (perfil == "ADMINISTRADOR") {
                    dtLogs = new DataTable();
                    fillLogs();
                } else {
                    Response.RedirectToRoute("ErrorPage", new { ERROR = "Se ha intentado acceder a una ZONA que solo es accesible por un administrador" });
                }
            } else {
                Response.RedirectToRoute("ErrorPage", new { ERROR = "Se ha intentado acceder a una ZONA que solo es accesible por un administrador" });
            }
        }

        private void fillLogs() {
            try {
                Libreria.ObtenerLogs(dtLogs);
                myGridView.DataSource = dtLogs;
                myGridView.DataBind();
            } catch (Exception ex) {
                Libreria.addLog("fillLogs", ex.StackTrace, ex.Message);
            }
        }

        protected void myGridView_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            myGridView.PageIndex = e.NewPageIndex;
            fillLogs(); 
        }
    }
}