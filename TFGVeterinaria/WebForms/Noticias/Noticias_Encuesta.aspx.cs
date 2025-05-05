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
    public partial class Noticias_Encuesta : Page {

        private static DataTable dtUsuarios;

        protected void Page_Load(object sender, EventArgs e) {
            //Session["PAGE"] = "USUARIOS";
            //if (Session["USR_PERFIL"] != null) {
            //    string perfil = Session["USR_PERFIL"].ToString();
            //    if (perfil == "ADMINISTRADOR") {
            //        if (!Page.IsPostBack) {
            //            dtUsuarios = new DataTable();
            //            BindGridUsuarios();
            //            alertaExito.Visible = false;
            //        }
            //    } else {
            //        Response.RedirectToRoute("ErrorPage", new { ERROR = "Se ha intentado acceder a una ZONA que solo es accesible por un administrador" });
            //    }
            //} else {
            //    Response.RedirectToRoute("ErrorPage", new { ERROR = "Se ha intentado acceder a una ZONA que solo es accesible por un administrador" });
            //}

            if (!IsPostBack) {
                if (RouteData.Values["ID"] != null) {
                    setDatos(Convert.ToInt32(RouteData.Values["ID"]));
                } else {
                    Response.RedirectToRoute("ErrorPage", new { ERROR = "Se ha intentado acceder a una leccion que no existe" });
                }
                txtTextoAdicional.Visible = false;
                BTSiguiente.Visible = false;
                alertaFail.Visible = false;
                alertaExito.Visible = false;
            }
        }


        private void setDatos(int idnoticia) {
            try {
                DataTable dtPreguntas = new DataTable();
                Noticias_Class.ObtenerPreguntas(dtPreguntas, idnoticia);
                LISTAID.Value = "";
                ID_NOTICIA.Value = idnoticia.ToString();
                foreach (DataRow row in dtPreguntas.Rows) {
                    string ID = row["ID"].ToString();
                    LISTAID.Value += ID + ",";
                }
                if(LISTAID.Value.Length > 0) {
                    LISTAID.Value = LISTAID.Value.Remove(LISTAID.Value.Length - 1); //Eliminamos el ultimo caracter
                    setPregunta();
                } else {
                    Response.RedirectToRoute("ErrorPage", new { ERROR = "Error inesperado" });
                }
            } catch (Exception ex) {
                Libreria.addLog("BindGridPreguntas", ex.StackTrace, ex.Message);
            }
        }

        private void setPregunta() {
            try {
                List<string> listaPreguntas = LISTAID.Value.Split(',').ToList();
                int id = -1;
                string pregunta = "";
                string textoadicional = "";
                string respuesta_a = "";
                string respuesta_b = "";
                string respuesta_c = "";
                string respuesta_d = "";
                string respuesta = "";
                id = Convert.ToInt32(listaPreguntas[0]);
                Noticias_Class.getPregunta(id, ref pregunta, ref textoadicional, ref respuesta_a, ref respuesta_b, ref respuesta_c, ref respuesta_d , ref respuesta);
                listaPreguntas.RemoveAt(0);
                LISTAID.Value = string.Join(",", listaPreguntas);

                txtPregunta.Text = pregunta;
                txtTextoAdicional.Text = textoadicional;
                txtTextoAdicional.Visible = false;
                RESPUESTA.Value = respuesta;
                if (respuesta_a.Length > 0) {
                    BT_A.Text = respuesta_a;
                    BT_A.Visible = true;
                } else{
                    BT_A.Visible = false;
                }

                if (respuesta_b.Length > 0) {
                    BT_B.Text = respuesta_b;
                    BT_B.Visible = true;
                } else {
                    BT_B.Visible = false;
                }

                if (respuesta_c.Length > 0) {
                    BT_C.Text = respuesta_c;
                    BT_C.Visible = true;
                } else {
                    BT_C.Visible = false;
                }

                if (respuesta_d.Length > 0) {
                    BT_D.Text = respuesta_d;
                    BT_D.Visible = true;
                } else {
                    BT_D.Visible = false;
                }

            } catch (Exception ex) {
                Libreria.addLog("setPregunta", ex.StackTrace, ex.Message);
            }
        }


        private void checkRespuesta(string respuesta) {
            if(RESPUESTA.Value == respuesta) {
                alertaExito.Visible = true;
                alertaFail.Visible = false;
                txtTextoAdicional.Visible = true;
                if (LISTAID.Value.Length > 0) {
                    BTSiguiente.Visible = true;
                } else {
                    BTSiguiente.Visible = false;
                }

                BT_A.Visible = false;
                BT_B.Visible = false;
                BT_C.Visible = false;
                BT_D.Visible = false;
            } else {
                alertaExito.Visible = false;
                alertaFail.Visible = true;
            }
        }

        protected void BT_A_Click(object sender, EventArgs e) {
            checkRespuesta("A");
        }

        protected void BT_B_Click(object sender, EventArgs e) {
            checkRespuesta("B");
        }

        protected void BT_C_Click(object sender, EventArgs e) {
            checkRespuesta("C");
        }

        protected void BT_D_Click(object sender, EventArgs e) {
            checkRespuesta("D");
        }

        protected void BTSiguiente_Click(object sender, EventArgs e) {
            alertaExito.Visible = false;
            alertaFail.Visible = false;
            BTSiguiente.Visible = false;
            txtTextoAdicional.Visible = false;
            setPregunta();
        }

    }
}