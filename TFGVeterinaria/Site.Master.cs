using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TFGVeterinaria
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["PAGE"] != null) {
                string pagina = Session["PAGE"].ToString();
                if (pagina == "SERVICIOS") {
                    setSelectedMenu("navbar-brand", "nav-link", "nav-link", "nav-link", "nav-link", "nav-link", "nav-link", "nav-link");
                } else if (pagina == "COMUNIDAD") {
                    setSelectedMenu("nav-link", "navbar-brand", "nav-link", "nav-link", "nav-link", "nav-link", "nav-link", "nav-link");
                } else if (pagina == "NOTICIAS") {
                    setSelectedMenu("nav-link", "nav-link", "navbar-brand", "nav-link", "nav-link", "nav-link", "nav-link", "nav-link");
                } else if (pagina == "MASCOTAS") {
                    setSelectedMenu("nav-link", "nav-link", "nav-link", "navbar-brand", "nav-link", "nav-link", "nav-link", "nav-link");
                } else if (pagina == "CHAT") {
                    setSelectedMenu("nav-link", "nav-link", "nav-link", "nav-link", "navbar-brand", "nav-link", "nav-link", "nav-link");
                } else if (pagina == "CITAS") {
                    setSelectedMenu("nav-link", "nav-link", "nav-link", "nav-link", "nav-link", "navbar-brand", "nav-link", "nav-link");
                } else if (pagina == "USUARIOS") {
                    setSelectedMenu("nav-link", "nav-link", "nav-link", "nav-link", "nav-link", "nav-link", "navbar-brand", "nav-link");
                } else if (pagina == "LOGS") {
                    setSelectedMenu("nav-link", "nav-link", "nav-link", "nav-link", "nav-link", "nav-link", "nav-link", "navbar-brand");
                } else {
                    setSelectedMenu("navbar-brand", "nav-link", "nav-link", "nav-link", "nav-link", "nav-link", "nav-link", "nav-link");
                }
            }


            if (Session["USR_PERFIL"] != null) {
                string perfil = Session["USR_PERFIL"].ToString();

                if (perfil == "GENERICO") {
                    setVisibleMenu(true, true, true, true, true, false, false, false);
                } else if(perfil == "VETERINARIO") {
                    setVisibleMenu(true, true, true, true, true, true, false, false);
                } else if(perfil == "ADMINISTRADOR") {
                    setVisibleMenu(true, true, true, true, true, true, true, true);
                } else {
                    setVisibleMenu(true, true, true, false, false, false, false, false);
                }

                SalirBtn.Visible = true;
                LoginBtn.Visible = false;
            } else { /* Usuario no registrado */
                setVisibleMenu(true, true, true, false, false, false, false, false);
                SalirBtn.Visible = false;
                LoginBtn.Visible = true;
            }
        }

        private void setSelectedMenu(string ServiciosClass, string ComunidadClass, string NoticiasClass, string MascotasClass, string ChatClass, string CitasClass, string UsuariosClass, string LogsClass) {
            ServiciosMenu.Attributes["class"] = ServiciosClass;
            ComunidadMenu.Attributes["class"] = ComunidadClass;
            NoticiasMenu.Attributes["class"] = NoticiasClass;
            MascotasMenu.Attributes["class"] = MascotasClass;
            ChatMenu.Attributes["class"] = ChatClass;
            CitasMenu.Attributes["class"] = CitasClass;
            UsuariosMenu.Attributes["class"] = UsuariosClass;
            LogsMenu.Attributes["class"] = LogsClass;
        }

        private void setVisibleMenu(bool ServiciosVisible, bool ComunidadVisible, bool NoticiasVisible, bool MascotasVisible, bool ChatVisible, bool CitasVisible, bool UsuariosVisible, bool LogsVisible) {
            ServiciosMenu.Visible = ServiciosVisible;
            ComunidadMenu.Visible = ComunidadVisible;
            NoticiasMenu.Visible = NoticiasVisible;
            MascotasMenu.Visible = MascotasVisible;
            ChatMenu.Visible = ChatVisible;
            CitasMenu.Visible = CitasVisible;
            UsuariosMenu.Visible = UsuariosVisible;
            LogsMenu.Visible = LogsVisible;
        }

        protected void SalirBtn_Click(object sender, EventArgs e) {
            Session["USR_USUARIO"] = null;
            Session["USR_NOMBRE"] = null;
            Session["USR_PERFIL"] = null;
            Session["USR_EMAIL"] = null;
            Session["USR_VERIFICADO"] = null;
            Session["PAGE"] = "SERVICIOS";
            Response.RedirectToRoute("ServiciosRoute");
        }
    }
}
