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

            if (DELETE_FIELD.Value != "") {
                int index = 0;
                try {
                    foreach (DataRow r in dtServicios.Rows) {
                        if (r["ID"].ToString() == DELETE_FIELD.Value) {
                            break;
                        }
                        index++;
                    }

                    Servicios_Class.DesactivarServicio(Convert.ToInt32(DELETE_FIELD.Value));
                    dtServicios.Rows.RemoveAt(index);
                    lvServicios.DataSource = dtServicios;
                    lvServicios.DataBind();
                } catch (Exception ex) {
                    Libreria.addLog("Eliminando Servicio", ex.StackTrace, ex.Message);
                }
                DELETE_FIELD.Value = "";
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

        protected void lvServicios_ItemDataBound(object sender, ListViewItemEventArgs e) {
            if (e.Item.ItemType == ListViewItemType.DataItem) {
                Button btnDelete = (Button)e.Item.FindControl("btnDelete");
                if (btnDelete != null) {
                    // Usamos Eval para obtener el valor del ID y lo pasamos a OnClientClick
                    string id = DataBinder.Eval(e.Item.DataItem, "ID").ToString();
                    btnDelete.OnClientClick = $"return showDeleteConfirmation('{id}');";
                }

                Button btnEdit = (Button)e.Item.FindControl("btnEdit");

                btnDelete.Visible = false;
                btnEdit.Visible = false;
                if (Session["USR_PERFIL"] != null && (Session["USR_PERFIL"].ToString() == "VETERINARIO" || Session["USR_PERFIL"].ToString() == "ADMINISTRADOR")) {
                    btnDelete.Visible = true;
                    btnEdit.Visible = true;
                }


            }
        }
    }
}