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

/*
 * 
    protected global::System.Web.UI.WebControls.GridView DTmedicamentos;
    protected global::System.Web.UI.WebControls.GridView DTtratamientos;
    protected global::System.Web.UI.WebControls.Image ImagePerro;
 * */
{
    public partial class Mascotas_Detalle : Page
    {
        protected string nombre = "Pedrito";
        protected int edad = 22;
        protected int peso = 5;

        protected string sexo = "Masculino";
        protected string medicamento = "Paracetamol";
        protected string dosis = "500 mg";
        protected string frecuenciamedicamento = "Cada 6 horas";
        protected string duracionmedicamento = "2 semanas";
        protected string obsmedicamento = "Si no quiere, aplastar el paracetamol";

        protected string tipo = "Masaje";
        protected string frecuenciatratamiento = "Cada 6 horas";
        protected string duraciontratamiento = "7 dias";
        protected string obstratamiento = "Con delicadeza";


        private static DataTable dtMedicamentos;
        private static DataTable dtTratamientos;

        protected void Page_Load(object sender, EventArgs e) {
            Session["PAGE"] = "MASCOTAS";

            txtNombre.Text = nombre;
            txtEdad.Text = edad.ToString();
            ddlSexo.SelectedValue = sexo;
            txtPeso.Text = peso.ToString();
            txtMedicamento.Text = medicamento;
            txtDosis.Text = dosis;
            txtFrecuenciaMedicamento.Text = frecuenciamedicamento;
            txtDuracionMedicamento.Text = duracionmedicamento;
            txtObsMedicamento.Text = obsmedicamento;
            txtTipo.Text = tipo;
            txtFrecuenciaTratamiento.Text = frecuenciatratamiento;
            txtDuracionTratamiento.Text = duraciontratamiento;
            txtObsTratamiento.Text = obstratamiento;

            if (!IsPostBack) {
                // Habilitar/deshabilitar controles según el valor de isEditable
                
                GuardarBtn.Visible = false;
                EditarBtn.Visible = true;
                alertaExito.Visible = false;

                EstablecerEditableMedicina(false);

                dtMedicamentos = new DataTable();
                dtMedicamentos.Columns.Add("ID", typeof(int));
                dtMedicamentos.Columns.Add("FECHA", typeof(DateTime));
                dtMedicamentos.Columns.Add("NOMBRE", typeof(string));
                dtMedicamentos.Columns.Add("DOSIS", typeof(string));
                dtMedicamentos.Columns.Add("FRECUENCIA", typeof(string));
                dtMedicamentos.Columns.Add("DURACION", typeof(string));
                dtMedicamentos.Columns.Add("OBS", typeof(string));

                dtTratamientos = new DataTable();
                dtTratamientos.Columns.Add("ID", typeof(int));
                dtTratamientos.Columns.Add("FECHA", typeof(DateTime));
                dtTratamientos.Columns.Add("TIPO", typeof(string));
                dtTratamientos.Columns.Add("FRECUENCIA", typeof(string));
                dtTratamientos.Columns.Add("DURACION", typeof(string));
                dtTratamientos.Columns.Add("OBS", typeof(string));

                if (RouteData.Values["alta"] != null) {
                    EstablecerEditable(true);
                    txtNombre.Text = "";
                    txtEdad.Text = "";
                    ddlSexo.SelectedValue = sexo;
                    txtPeso.Text = "";
                    txtMedicamento.Text = "";
                    txtDosis.Text = "";
                    txtFrecuenciaMedicamento.Text = "";
                    txtDuracionMedicamento.Text = "";
                    txtObsMedicamento.Text = "";
                    txtTipo.Text = "";
                    txtFrecuenciaTratamiento.Text = "";
                    txtDuracionTratamiento.Text = "";
                    txtObsTratamiento.Text = "";
                    ImagenPerro.Visible = false;
                } else {
                    EstablecerEditable(false);

                    // Agregar filas al DataTable
                    dtMedicamentos.Rows.Add(2, new DateTime(2025, 5, 1), medicamento, dosis, frecuenciamedicamento, duracionmedicamento, obsmedicamento);
                    dtMedicamentos.Rows.Add(2, new DateTime(2025, 4, 12), "Medicamento SIGCER", "5 gramos", "Cada 8 horas", "5 dias", "Con cuidado");
                    dtMedicamentos.Rows.Add(3, new DateTime(2025, 3, 8), "Pastilla YUN", "1", "1 vez al dia", "2 meses", "Aplicar solo una vez por dia");
                    dtMedicamentos.Rows.Add(4, new DateTime(2025, 2, 25), "Frenadol", "250 mg", "Cada 5 horas", "3 semanas", "No forzar");

                    dtTratamientos.Rows.Add(1, new DateTime(2025, 3, 8), tipo, frecuenciatratamiento, duraciontratamiento, obstratamiento);
                    dtTratamientos.Rows.Add(2, new DateTime(2025, 1, 30), "Estirar patas", "2 veces al dia", "1 semana", "Mantener en forma a la mascota");
                }


                // Crear un DataView
                DataView dv = new DataView(dtMedicamentos);
                DataView dv2 = new DataView(dtTratamientos);

                // Asignar el DataView al GridView
                DTmedicamentos.DataSource = dv;
                DTmedicamentos.DataBind();

                DTtratamientos.DataSource = dv2;
                DTtratamientos.DataBind();
            }

            
        }

        protected void Guardar(object sender, EventArgs e) {
            alertaExito.Visible = true;
            GuardarBtn.Visible = false;
            EditarBtn.Visible = true;
            EstablecerEditable(false);
        }

        protected void Editar_Click(object sender, EventArgs e) {
            EstablecerEditable(true);
            GuardarBtn.Visible = true;
            EditarBtn.Visible = false;
        }

        // Método para habilitar o deshabilitar los controles
        private void EstablecerEditable(bool editable) {
            txtNombre.Enabled = editable;
            txtEdad.Enabled = editable;
            ddlSexo.Enabled = editable;
            txtPeso.Enabled = editable;
            seleccionarArchivo.Visible = editable;

            if (Session["USR_PERFIL"] != null) {
                string perfil = Session["USR_PERFIL"].ToString();

                if (perfil == "VETERINARIO") {
                    EstablecerEditableMedicina(true);
                }
            }
        }

        private void EstablecerEditableMedicina(bool editable) {
            txtTipo.Enabled = editable;
            txtFrecuenciaTratamiento.Enabled = editable;
            txtDuracionTratamiento.Enabled = editable;
            txtObsTratamiento.Enabled = editable;

            txtMedicamento.Enabled = editable;
            txtDosis.Enabled = editable;
            txtObsMedicamento.Enabled = editable;
            txtFrecuenciaMedicamento.Enabled = editable;
            txtDuracionMedicamento.Enabled= editable;
        }

    }
}