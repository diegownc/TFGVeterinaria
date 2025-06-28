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
    public partial class Citas : Page {
        private static DataTable dtCitas;

        protected void Page_Load(object sender, EventArgs e) {
            Session["PAGE"] = "CITAS";
            if (!IsPostBack) {
                dtCitas = new DataTable();
                LoadCitas();
                
            }
        }

        private void LoadCitas() {
            try {
                Citas_Class.ObtenerCitas(dtCitas, Session["USR_USUARIO"].ToString());
                gvCitas.DataSource = dtCitas;
                gvCitas.DataBind();
            } catch (Exception ex) {
                Libreria.addLog("LoadCitas", ex.StackTrace, ex.Message);
            }
        }


        protected void gvCitas_RowCommand(object sender, GridViewCommandEventArgs e) {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Confirmar") {
                try {
                    Citas_Class.ActualizarCita(index, 1);
                    LoadCitas();
                } catch(Exception ex) {
                    Libreria.addLog("gvCitas_RowCommand Confirmar Cita", ex.StackTrace, ex.Message);
                }
            } else if (e.CommandName == "Cancelar") {
                try {
                    Citas_Class.ActualizarCita(index, 2);
                    LoadCitas();
                } catch (Exception ex) {
                    Libreria.addLog("gvCitas_RowCommand Cancelar Cita", ex.StackTrace, ex.Message);
                }
            }
        }
    }
}