using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace TFGVeterinaria
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);

            // Otros
            RouteTable.Routes.MapPageRoute("ErrorPage", "Mascotas_Detalle/{ERROR}", "~/WebForms/ErrorPage.aspx");

            //Login
            RouteTable.Routes.MapPageRoute("loginRoute", "Login", "~/WebForms/Login/Login.aspx");
            RouteTable.Routes.MapPageRoute("registroRoute", "Registro", "~/WebForms/Login/Registro.aspx");
            
            //Mascotas
            RouteTable.Routes.MapPageRoute("mascotasRoute", "Mascotas", "~/WebForms/Mascotas/Mascotas.aspx");
            RouteTable.Routes.MapPageRoute("mascotasDetalleRoute", "Mascotas_Detalle", "~/WebForms/Mascotas/Mascotas_Detalle.aspx");
            RouteTable.Routes.MapPageRoute("mascotasDetalleRouteParam", "Mascotas_Detalle/{alta}", "~/WebForms/Mascotas/Mascotas_Detalle.aspx");

            //Noticias
            RouteTable.Routes.MapPageRoute("noticiasRoute", "Noticias", "~/WebForms/Noticias/Noticias.aspx");
            RouteTable.Routes.MapPageRoute("noticiasDetalleRouteParam", "Noticias_Detalle/{ID}", "~/WebForms/Noticias/Noticias_Detalle.aspx");
            RouteTable.Routes.MapPageRoute("notencDetalleRouteParam", "Noticias_Encuesta/{ID}", "~/WebForms/Noticias/Noticias_Encuesta.aspx");

            //Logs
            RouteTable.Routes.MapPageRoute("LogsRoute", "Logs", "~/WebForms/Logs/LogSistema.aspx");

            //Usuarios
            RouteTable.Routes.MapPageRoute("UsuariosRoute", "Usuarios", "~/WebForms/Usuarios/Usuarios.aspx");

            //Servicios
            RouteTable.Routes.MapPageRoute("ServiciosRoute", "Servicios", "~/WebForms/Servicios/Servicios.aspx");
            RouteTable.Routes.MapPageRoute("ServCitaRoute", "ServiciosCita/{ID}", "~/WebForms/Servicios/ServiciosCita.aspx");
            RouteTable.Routes.MapPageRoute("ServEdRoute", "ServiciosEditar/{ID}", "~/WebForms/Servicios/ServiciosEditar.aspx");

            //Chat
            RouteTable.Routes.MapPageRoute("ChatRoute", "Chat", "~/WebForms/Chat/Chat.aspx");

            //Comunidad
            RouteTable.Routes.MapPageRoute("ComunidadRoute", "Comunidad", "~/WebForms/Comunidad/Comunidad.aspx");

        }
    }
}
