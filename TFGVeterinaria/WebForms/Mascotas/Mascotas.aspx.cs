using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TFGVeterinaria.Clases;

namespace TFGVeterinaria {
    public partial class Mascotas : Page {
        private static DataTable dt;

        protected void Page_Load(object sender, EventArgs e) {
            Session["PAGE"] = "MASCOTAS";
            if (!IsPostBack) {
                // Crear un DataTable y agregar columnas
                dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Dueno", typeof(string)); 
                dt.Columns.Add("ImageUrl", typeof(string));
                dt.Columns.Add("Nombre", typeof(string));
                dt.Columns.Add("Edad", typeof(int));
                dt.Columns.Add("Peso", typeof(int));

                // Agregar filas al DataTable
                dt.Rows.Add(1, "Wenceslao Diego Pacheco Guevara", "/Imagenes/Perro1.jpg", "Juanita", 25, 5);
                dt.Rows.Add(2, "Wenceslao Diego Pacheco Guevara", "/Imagenes/Gato1.jpg", "María", 30, 6);
                dt.Rows.Add(3, "Wenceslao Diego Pacheco Guevara", "/Imagenes/Perro2.jpg", "Pedrito", 22, 5);

                // Crear un DataView
                DataView dv = new DataView(dt);

                // Asignar el DataView al GridView
                myGridView.DataSource = dv;
                myGridView.DataBind();
            }

          
            if (Session["USR_PERFIL"] != null) {
                string perfil = Session["USR_PERFIL"].ToString();

                if(perfil == "ADMINISTRADOR") {
                    myGridView.Columns[3].Visible = true;
                } else {
                    myGridView.Columns[3].Visible = false;
                }
            }

            if (DELETE_FIELD.Value != "") {
                int index = 0;
                foreach(DataRow r in dt.Rows) {
                    if (r["ID"].ToString() == DELETE_FIELD.Value) {
                        break;
                    }
                    index++;
                }

                dt.Rows.RemoveAt(index);
                myGridView.DataSource = dt;
                myGridView.DataBind();
                DELETE_FIELD.Value = "";
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

                Response.RedirectToRoute("mascotasDetalleRoute");
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
        /*
        protected void myGridView_RowDeleting(object sender, GridViewDeleteEventArgs e) {
            int index = e.RowIndex;

            // Eliminar la fila del DataTable
            dt.Rows.RemoveAt(index);

            myGridView.DataSource = dt;
            myGridView.DataBind();
        }
        */

        protected void myGridView_RowDeleting(object sender, GridViewDeleteEventArgs e) {
            // Obtener el ID del registro a eliminar
            int id = Convert.ToInt32(myGridView.DataKeys[e.RowIndex].Value);

            // Aquí va la lógica para eliminar la mascota de la base de datos
            // Por ejemplo:
            // dbContext.Mascotas.Remove(id);
            // dbContext.SaveChanges();

            // Luego actualizamos el GridView para reflejar los cambios
            //BindGrid();  // Función que actualiza tu GridView después de la eliminación

        }


        protected void myGridView_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                Button btnDelete = (Button)e.Row.FindControl("btnDelete");
                if (btnDelete != null) {
                    // Usamos Eval para obtener el valor del ID y lo pasamos a OnClientClick
                    string id = DataBinder.Eval(e.Row.DataItem, "ID").ToString();
                    btnDelete.OnClientClick = $"return showDeleteConfirmation('{id}');";
                }
            }
        }


        protected void btnRedirect_Click(object sender, EventArgs e) {
            // Redirigir a la página con el parámetro en la URL
            Response.RedirectToRoute("mascotasDetalleRouteParam", new { alta = true });
        }


    }
}