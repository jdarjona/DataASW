using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.Security;
using wsTRH.cuAvisos;
namespace wsTRH.Controllers
{
    public class DatosUsuario
    {
        public string codComercial;
        public string emailComercial;
        public string token;
        public DatosUsuario(string _codComercial, string _emailComercial,string _token) {

            codComercial = _codComercial;
            emailComercial = _emailComercial;
            token = _token;

        }


    }

    public static class FuncionesCodeUnit
    {

        private static Avisos_PortClient wsAvisos;
        private static string  _path;
        private static string domain;
        private static string empresa;
        private static string ipDominio;
        //private string adPath = "LDAP://{0}" + ipReal + ":389/DC={1}" + companyName + ",DC={2}LIEJA";
        private static string adPath = "LDAP://{0}:389/DC=TRH,DC={1}";

        #region "Métodos Privados"
        private static void OpenService()
        {
            _path = System.Web.Hosting.HostingEnvironment.MapPath("~/PDF/");
            // Avisos_PortClient wsAvisos;
            List<webServiceEmpresa> empresaConfig = configuracion.getDatosEmpresa();

            string usuario = string.Format(@"{0}\administrador", empresaConfig[0].dominio);
            string password = @"Paulagallardo2014";
            

            domain = empresaConfig[0].dominio;
            empresa = empresaConfig[0].empresa;
            ipDominio = empresaConfig[0].ipDominio;
            adPath = string.Format(adPath, empresaConfig[0].ipDominio, empresaConfig[0].id);

            string url = @"http://{1}/DynamicsNAV/WS/{0}/Codeunit/Avisos";
            url = string.Format(url, empresaConfig[0].empresa, empresaConfig[0].url);

            BasicHttpBinding navisionWSBinding = new BasicHttpBinding();
            navisionWSBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            navisionWSBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            navisionWSBinding.MaxReceivedMessageSize = 2000971520;


            wsAvisos = new Avisos_PortClient(navisionWSBinding, new EndpointAddress(url));
            wsAvisos.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;
            wsAvisos.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential(usuario, password);

            
        }

        private static string getUsuarioPaisNavision(string usuarioDominio) {

            try
            {
                OpenService();
                return wsAvisos.ComercialPais(usuarioDominio);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        

        #endregion
        public static bool addProductoOferta(string codProducto, string codOferta) {
            
            try
            {
                OpenService();

                wsAvisos.ProductoToOferta(codProducto, codOferta);

                return true;
            }
            catch (Exception ex)
            {
                return false;

                throw;
            }

        }

        public static string addDireccionEnvio(string codeDireccionEnvio,string customer_No, string name,string direccion1, string direccion2,string ciudad,string contacto, 
                string telefono,string fax,string codigoPostal,string provincia,string email, string otrosDatos, string codOferta) {

            try
            {
                OpenService();
                return wsAvisos.CrearDireccionEnvio(codeDireccionEnvio,customer_No,name, direccion1,direccion2, 
                                             ciudad,contacto,telefono,fax,codigoPostal,provincia,email,otrosDatos,codOferta);

                
            }
            catch (Exception ex)
            {
                return string.Empty;
                throw;
            }

        }

        public static string getPdfOferta(string idOferta, bool withNombre) {
            
            try
            {
                
                OpenService();
                
                return wsAvisos.PdfOfertaDestino(idOferta.Replace("_","/"), _path, withNombre);

                

            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }


        }

        public static void sendIncidencia(string idOferta,string comentarios, string codComercial) {

            try
            {
                wsAvisos.EnviarIncidencia(idOferta, comentarios, codComercial);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        
        public static DatosUsuario authenticationAtiveDiretory(string usuario, string password) {
            
            try
            {
                OpenService();

                DatosUsuario datosUsuarios;
                LdapAuthentication adAuth = new LdapAuthentication(adPath);

                if (true == adAuth.IsAuthenticated(domain, usuario, password))
                {
                    string datosUsuario = getUsuarioPaisNavision(usuario);
                    if (string.IsNullOrEmpty(datosUsuario))
                        throw new Exception("Error al recuperar el país del usuario");
                    if (datosUsuario.Length > 2)
                    {
                        string codUsuario = datosUsuario.Split(';')[0];
                        string codPais = datosUsuario.Split(';')[1];
                        string esJefe = datosUsuario.Split(';')[2];
                        string email = datosUsuario.Split(';')[3];
                        string grupo = datosUsuario.Split(';')[4];

                        
                       
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, usuario, DateTime.Now, DateTime.Now.AddMinutes(60), false, codUsuario.ToString());
                        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        HttpContext.Current.Response.Cookies.Add(authCookie);

                        datosUsuarios = new DatosUsuario(codUsuario, email, authCookie.ToString());
                        return datosUsuarios;
                    }
                }
                else
                    throw new Exception("El Usuario y/o Contraseña son incorrectos o la cuenta está deshabilitada. ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

    }

    public class LdapAuthentication
    {
        private string _path;
        private string _filterAttribute;

        public LdapAuthentication(string path)
        {
            _path = path;
        }

        public bool IsAuthenticated(string domain, string username, string pwd)
        {
            
            string domainAndUsername = domain + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);

            try
            {
                Object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();
                if (null == result)
                {
                    return false;
                }
                _path = result.Path;
                _filterAttribute = (String)result.Properties["cn"][0];
            }
            catch (Exception ex)
            {
                throw new Exception("Error de inicio de sesion. " + ex.Message);
            }
            return true;
        }

      

    }
}