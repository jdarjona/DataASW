using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web.Http;
using wsTRH.pDireccionesEnvio;

namespace wsTRH.Controllers
{
    [System.Web.Http.Authorize]
    public class DireccionesEnvioController : ApiController
    {
        DireccionesEnvio_PortClient wsDirEnvios;
        private void OpenService()
        {

            List<webServiceEmpresa> empresaConfig = configuracion.getDatosEmpresa();

            string usuario = string.Format(@"{0}\administrador", empresaConfig[0].dominio);
            string password = @"Paulagallardo2014";


            string url = @"http://{1}/DynamicsNAV/WS/{0}/Page/DireccionesEnvio";
            url = string.Format(url, empresaConfig[0].empresa, empresaConfig[0].url);

            BasicHttpBinding navisionWSBinding = new BasicHttpBinding();
            navisionWSBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            navisionWSBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            navisionWSBinding.MaxReceivedMessageSize = 2000971520;


            wsDirEnvios = new DireccionesEnvio_PortClient(navisionWSBinding, new EndpointAddress(url));
            wsDirEnvios.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;
            wsDirEnvios.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential(usuario, password);


        }


        // GET: api/DireccionesEnvio
        public List<DireccionesEnvio> Get()
        {
            try
            {
                OpenService();
                DireccionesEnvio_Filter[] filtro = new DireccionesEnvio_Filter[] { new DireccionesEnvio_Filter { Field = DireccionesEnvio_Fields.Code, Criteria = "*" } };
                var direcionesEnvio= wsDirEnvios.ReadMultiple(filtro, null, 0).ToList();
                return direcionesEnvio;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
           
        }

        // GET: api/DireccionesEnvio/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/DireccionesEnvio
        public string Post([FromBody]DireccionesEnvio direccionEnvio,string codOferta)
        {
            try
            {

                return FuncionesCodeUnit.addDireccionEnvio(direccionEnvio.Code, direccionEnvio.Customer_No, direccionEnvio.Name, direccionEnvio.Address, 
                                                           direccionEnvio.Address_2, direccionEnvio.City, direccionEnvio.Contact, direccionEnvio.Phone_No, 
                                                           direccionEnvio.Fax_No, direccionEnvio.Post_Code, direccionEnvio.Country_Region_Code, direccionEnvio.E_Mail,
                                                           direccionEnvio.Otros_datos,codOferta);

                
            }
            catch (Exception ex)
            {
                return string.Empty;
                throw;
            }
        }

        // PUT: api/DireccionesEnvio/5
        public void Put([FromBody] DireccionesEnvio value)
        {
            try
            {


            }
            catch (Exception ex)
            {

                throw;
            }

        }

        // DELETE: api/DireccionesEnvio/5
        public void Delete(int id)
        {
        }
    }
}
