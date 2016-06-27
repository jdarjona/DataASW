using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;

using Microsoft.Owin.Cors;
namespace wsTRH
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //var cors = new EnableCorsAttribute("http://localhost:61881", "*", "*");
            config.EnableCors();
            // Configuración y servicios de Web API
            // Configure Web API para usar solo la autenticación de token de portador.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            //config.MessageHandlers.Add(new MethodOverrideHandler());

            // Rutas de Web API
            config.MapHttpAttributeRoutes();

            //var cors = new EnableCorsAttribute("*", "*", "GET,DELETE,POST,PUT,PATCH,HEAD");
            //config.EnableCors(cors);
          



            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            config.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            
        }
    }
}
