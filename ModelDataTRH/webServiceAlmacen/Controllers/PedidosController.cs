using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web.Http;
using wsTRH.pPedidos;
namespace wsTRH.Controllers
{
    [System.Web.Http.Authorize]
    public class PedidosController : ApiController
    {
        pPedidos.Pedidos_PortClient wsPedidos;
        private void OpenService()
        {

            List<webServiceEmpresa> empresaConfig = configuracion.getDatosEmpresa();

            string usuario = string.Format(@"{0}\administrador", empresaConfig[0].dominio);
            string password = @"Paulagallardo2014";


            string url = @"http://{1}/DynamicsNAV/WS/{0}/Page/Pedidos";
            url = string.Format(url, empresaConfig[0].empresa, empresaConfig[0].url);

            BasicHttpBinding navisionWSBinding = new BasicHttpBinding();
            navisionWSBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            navisionWSBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            navisionWSBinding.MaxReceivedMessageSize = 2000971520;


            wsPedidos = new Pedidos_PortClient(navisionWSBinding, new EndpointAddress(url));
            wsPedidos.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;
            wsPedidos.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential(usuario, password);


        }

        // GET: api/Pedidos
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Pedidos/5
        public pPedidos.Pedidos Get(string codPedido)
        {
            OpenService();

            codPedido = codPedido.Replace("_", "/");
            Pedidos result = wsPedidos.Read(codPedido);

            return result;
        }

        // POST: api/Pedidos
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Pedidos/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Pedidos/5
        public void Delete(int id)
        {
        }
    }
}
