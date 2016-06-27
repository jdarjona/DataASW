using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using wsTRH.Providers;
using wsTRH.Models;
using Microsoft.Owin.Cors;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http;
using System.Web.Http.Cors;
using wsTRH.App_Start;

namespace wsTRH
{
   
        public partial class Startup
        {
            static Startup()
            {
                PublicClientId = "self";

               UserManagerFactory = () => new UserManager<IdentityUser>(new UserStore<IdentityUser>());

                OAuthOptions = new OAuthAuthorizationServerOptions
                {
                    TokenEndpointPath = new PathString("/Token"),
                    Provider = new ApplicationOAuthProvider(PublicClientId),
                    AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                    AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                    AllowInsecureHttp = true
                };
            }

            public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

            public static Func<UserManager<IdentityUser>> UserManagerFactory { get; set; }

            public static string PublicClientId { get; private set; }

            // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
            public void ConfigureAuth(IAppBuilder app)
            {

                app.CreatePerOwinContext(ApplicationDbContext.Create);
           
                app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
                app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

                // Enable the application to use a cookie to store information for the signed in user
                // and to use a cookie to temporarily store information about a user logging in with a third party login provider
                app.UseCookieAuthentication(new CookieAuthenticationOptions());
                app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            //app.UseCors(CorsOptions.AllowAll);
            app.Use(async (context, next) =>
           {
               IOwinRequest req = context.Request;
               IOwinResponse res = context.Response;
               // for auth2 token requests
               if (req.Path.StartsWithSegments(new PathString("/api")))
               {
                   // if there is an origin header
                   //var origin = req.Headers.Get("Origin");
                   //if (!string.IsNullOrEmpty(origin))
                   //{
                   //    // allow the cross-site request
                   //    res.Headers.Set("Access-Control-Allow-Origin", origin);
                   //}

                   // if this is pre-flight request
                   if (req.Method == "OPTIONS")
                   {
                       // respond immediately with allowed request methods and headers
                       
                       res.StatusCode = 200;
                       res.Headers.AppendCommaSeparatedValues("Access-Control-Allow-Methods", "GET", "POST","PUT");
                       res.Headers.AppendCommaSeparatedValues("Access-Control-Allow-Headers", "authorization", "content-type");
                       
                       // no further processing
                       return;
                   }
               }
               // continue executing pipeline
               await next();
           });

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

                // Uncomment the following lines to enable logging in with third party login providers
                //app.UseMicrosoftAccountAuthentication(
                //    clientId: "",
                //    clientSecret: "");

                //app.UseTwitterAuthentication(
                //    consumerKey: "",
                //    consumerSecret: "");

                //app.UseFacebookAuthentication(
                //    appId: "",
                //    appSecret: "");

                //app.UseGoogleAuthentication();
            }
        }
    }
