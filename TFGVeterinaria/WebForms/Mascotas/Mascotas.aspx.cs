using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TFGVeterinaria.Clases;

namespace TFGVeterinaria
{
    public partial class Mascotas : Page
    {
        private static DataTable dt;

        protected void Page_Load(object sender, EventArgs e) {
            Session["PAGE"] = "MASCOTAS";
            if (!IsPostBack) {
                // Crear un DataTable y agregar columnas
                dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Nombre", typeof(string));
                dt.Columns.Add("Edad", typeof(int));

                // Agregar filas al DataTable
                dt.Rows.Add(1, "Juan", 25);
                dt.Rows.Add(2, "María", 30);
                dt.Rows.Add(3, "Pedro", 22);

                // Crear un DataView
                DataView dv = new DataView(dt);

                // Asignar el DataView al GridView
                myGridView.DataSource = dv;
                myGridView.DataBind();
            }
        }


            // Evento para manejar la selección de una fila
        protected void myGridView_RowCommand(object sender, GridViewCommandEventArgs e) {
            if (e.CommandName == "Select") {
                // Obtener el índice de la fila seleccionada
                int index = Convert.ToInt32(e.CommandArgument);
                string selectedName = dt.Rows[index]["Nombre"].ToString();
                // Mostrar algún mensaje o realizar una acción con la fila seleccionada
                Response.Write($"Seleccionaste a {selectedName}.");
            }
        } 

        // Evento para manejar la edición de una fila
        protected void myGridView_RowEditing(object sender, GridViewEditEventArgs e) {
            myGridView.EditIndex = e.NewEditIndex;
            myGridView.DataSource = dt;
            myGridView.DataBind();
        }


        // Evento para manejar la actualización de una fila editada
        protected void myGridView_RowUpdating(object sender, GridViewUpdateEventArgs e) {
            int index = e.RowIndex;
            string newName = ((TextBox)myGridView.Rows[index].Cells[1].Controls[0]).Text;
            string newAge = ((TextBox)myGridView.Rows[index].Cells[2].Controls[0]).Text;

            // Actualizar los datos en el DataTable
            dt.Rows[index]["Nombre"] = newName;
            dt.Rows[index]["Edad"] = int.Parse(newAge);

            myGridView.EditIndex = -1;
            myGridView.DataSource = dt;
            myGridView.DataBind();
        }

        // Evento para cancelar la edición
        protected void myGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) {
            myGridView.EditIndex = -1;
            myGridView.DataSource = dt;
            myGridView.DataBind();
        }

        // Evento para manejar el borrado de una fila
        protected void myGridView_RowDeleting(object sender, GridViewDeleteEventArgs e) {
            int index = e.RowIndex;

            // Eliminar la fila del DataTable
            dt.Rows.RemoveAt(index);

            myGridView.DataSource = dt;
            myGridView.DataBind();
        }
    }
}