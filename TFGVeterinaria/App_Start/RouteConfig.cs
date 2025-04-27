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

            //Login
            RouteTable.Routes.MapPageRoute("loginRoute", "Login", "~/WebForms/Login/Login.aspx");
            RouteTable.Routes.MapPageRoute("registroRoute", "Registro", "~/WebForms/Login/Registro.aspx");
            
            //Mascotas
            RouteTable.Routes.MapPageRoute("mascotasRoute", "Mascotas", "~/WebForms/Mascotas/Mascotas.aspx");
            RouteTable.Routes.MapPageRoute("mascotasDetalleRoute", "Mascotas_Detalle", "~/WebForms/Mascotas/Mascotas_Detalle.aspx");
            RouteTable.Routes.MapPageRoute("mascotasDetalleRouteParam", "Mascotas_Detalle/{alta}", "~/WebForms/Mascotas/Mascotas_Detalle.aspx");


            //Noticias
            RouteTable.Routes.MapPageRoute("noticiasRoute", "Noticias", "~/WebForms/Noticias/Noticias.aspx");
        }
    }
}
