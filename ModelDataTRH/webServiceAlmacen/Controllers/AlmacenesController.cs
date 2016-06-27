using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web.Http;
using wsTRH.pAlmacenes;

namespace wsTRH.Controllers
{
    public class AlmacenesController : ApiController
    {
        pAlmacenes.AlmacenesClientes_PortClient wsAlmacenes;
        private void OpenService()
        {

            List<webServiceEmpresa> empresaConfig = configuracion.getDatosEmpresa();

            string usuario = string.Format(@"{0}\administrador", empresaConfig[0].dominio);
            string password = @"Paulagallardo2014";


            string url = @"http://{1}/DynamicsNAV/WS/{0}/Page/AlmacenesClientes";
            url = string.Format(url, empresaConfig[0].empresa, empresaConfig[0].url);

            BasicHttpBinding navisionWSBinding = new BasicHttpBinding();
            navisionWSBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            navisionWSBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            navisionWSBinding.MaxReceivedMessageSize = 2000971520;


            wsAlmacenes = new AlmacenesClientes_PortClient(navisionWSBinding, new EndpointAddress(url));
            wsAlmacenes.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;
            wsAlmacenes.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential(usuario, password);


        }
        // GET: api/Almacenes
        public List<pAlmacenes.AlmacenesClientes> Get()
        {
            OpenService();

            AlmacenesClientes_Filter[] filtro = new AlmacenesClientes_Filter[] { new AlmacenesClientes_Filter() { Field = AlmacenesClientes_Fields.NombreAlmacen, Criteria = "*" } };
            var result= wsAlmacenes.ReadMultiple(filtro,null,0);
            return result.ToList();
           
        }

        // GET: api/Almacenes/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Almacenes
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Almacenes/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Almacenes/5
        public void Delete(int id)
        {
        }
    }
}
