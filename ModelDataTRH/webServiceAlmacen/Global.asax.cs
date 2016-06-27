using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;



namespace wsTRH
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
           
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.Formatters.Insert(0, new JsonMediaTypeFormatter());

        }
        protected void Session_Start(object sender, EventArgs e)
        {
            // Comprueba que hay un rol de administrador y un usuario asociado

            //var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            //IdentityManager identMgr = new IdentityManager();

            //if (!identMgr.RoleExists(ROLADM))
            //{
            //    identMgr.CreateRole(ROLADM);
            //    var user = new ApplicationUser() { UserName = "Administrador" };
            //    if (identMgr.CreateUser(user, "c1@v3~"))
            //    {
            //        identMgr.AddUserToRole(user.Id, ROLADM);
            //    }
            //}
        }
    }
}
