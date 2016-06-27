using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web.Http;
using wsTRH.pPostCode;

namespace wsTRH.Controllers
{
    [System.Web.Http.Authorize]
    public class PostCodeController : ApiController
    {
        CodigoPostales_PortClient wsPostCode;
        private void OpenService()
        {

            List<webServiceEmpresa> empresaConfig = configuracion.getDatosEmpresa();

            string usuario = string.Format(@"{0}\administrador", empresaConfig[0].dominio);
            string password = @"Paulagallardo2014";


            string url = @"http://{1}/DynamicsNAV/WS/{0}/Page/CodigoPostales";
            url = string.Format(url, empresaConfig[0].empresa, empresaConfig[0].url);

            BasicHttpBinding navisionWSBinding = new BasicHttpBinding();
            navisionWSBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            navisionWSBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            navisionWSBinding.MaxReceivedMessageSize = 2000971520;


            wsPostCode = new CodigoPostales_PortClient(navisionWSBinding, new EndpointAddress(url));
            wsPostCode.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;
            wsPostCode.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential(usuario, password);


        }
        // GET: api/PostCode
        public List<CodigoPostales> Get()
        {

            try
            {
                OpenService();

                CodigoPostales_Filter[] filtro = new CodigoPostales_Filter[] { new CodigoPostales_Filter() { Field = CodigoPostales_Fields.Code, Criteria = "*" } };

                var codigoPostales = wsPostCode.ReadMultiple(filtro, null, 0);
                return codigoPostales.ToList();

            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        // GET: api/PostCode/5
        public List<CodigoPostales> Get(string codigoPostal)
        {
            try
            {
                OpenService();

                CodigoPostales_Filter[] filtro = new CodigoPostales_Filter[] { new CodigoPostales_Filter() { Field = CodigoPostales_Fields.Code, Criteria = string.Format("{0}*", codigoPostal)} };

                var codigoPostales= wsPostCode.ReadMultiple(filtro,null,0);
                return codigoPostales.ToList();

            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        // GET: api/PostCode/5
        public List<CodigoPostales> GetByPoblacion(string poblacion)
        {
            try
            {
                OpenService();

                CodigoPostales_Filter[] filtro = new CodigoPostales_Filter[] { new CodigoPostales_Filter() { Field = CodigoPostales_Fields.City, Criteria = string.Format("{0}*", poblacion) } };

                var codigoPostales = wsPostCode.ReadMultiple(filtro, null, 0);
                return codigoPostales.ToList();

            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        // POST: api/PostCode
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/PostCode/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PostCode/5
        public void Delete(int id)
        {
        }
    }
}
