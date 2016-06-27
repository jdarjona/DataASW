using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using wsTRH.pClientes;

namespace wsTRH.Controllers
{
    //[EnableCors(origins: "http://localhost:61881", headers: "*", methods: "*",SupportsCredentials = true)]
    [System.Web.Http.Authorize]
    public class ClientesController : ApiController
    {
        pClientes.Clientes_PortClient wsClientes;
        private void OpenService()
        {

            List<webServiceEmpresa> empresaConfig = configuracion.getDatosEmpresa();

            string usuario = string.Format(@"{0}\administrador", empresaConfig[0].dominio);
            string password = @"Paulagallardo2014";


            string url = @"http://{1}/DynamicsNAV/WS/{0}/Page/Clientes";
            url = string.Format(url, empresaConfig[0].empresa, empresaConfig[0].url);

            BasicHttpBinding navisionWSBinding = new BasicHttpBinding();
            navisionWSBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            navisionWSBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            navisionWSBinding.MaxReceivedMessageSize = 2000971520;


            wsClientes = new Clientes_PortClient(navisionWSBinding, new EndpointAddress(url));
            wsClientes.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;
            wsClientes.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential(usuario, password);


        }
        // GET: api/Clientes
      
        [OutputCache(Duration = 0)]
        public List<pClientes.Clientes> Get()
        {
            OpenService();

            Clientes_Filter[] filtro = new Clientes_Filter[] { new Clientes_Filter() { Field = Clientes_Fields.No, Criteria = "*" } };
            
            pClientes.Clientes[] clientes=wsClientes.ReadMultiple(filtro, null, 0);

            return clientes.ToList();

        }

        // GET: api/Clientes/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Clientes
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Clientes/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Clientes/5
        public void Delete(int id)
        {
        }
    }
}
