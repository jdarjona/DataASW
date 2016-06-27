using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web.Http;
using wsTRH.pProductos;
using wsTRH.Controllers;

namespace wsTRH.Controllers
{
    [System.Web.Http.Authorize]
    public class ProductosController : ApiController
    {

        Productos_PortClient wsProductos;
        List<webServiceEmpresa> empresaConfig;
        private void OpenService()
        {

            empresaConfig = configuracion.getDatosEmpresa();

            string usuario = string.Format(@"{0}\administrador", empresaConfig[0].dominio);
            string password = @"Paulagallardo2014";


            string url = @"http://{1}/DynamicsNAV/WS/{0}/Page/Productos";
            url = string.Format(url, empresaConfig[0].empresa, empresaConfig[0].url);

            BasicHttpBinding navisionWSBinding = new BasicHttpBinding();
            navisionWSBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            navisionWSBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            navisionWSBinding.MaxReceivedMessageSize = 2000971520;


            wsProductos = new Productos_PortClient(navisionWSBinding, new EndpointAddress(url));
            wsProductos.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;
            wsProductos.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential(usuario, password);


        }
        // GET: api/Productos
        public List<Productos> Get()
        {
            try
            {
                OpenService();

                Productos_Filter[] filtro=new Productos_Filter[] 
                    { new Productos_Filter() { Field= Productos_Fields.Paños_x_Paquete,Criteria=">0"} };
                var result = wsProductos.ReadMultiple(filtro, null, 0);
                return result.ToList();

            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        // GET: api/Productos/5
        public List<Productos> Get(int tipoMalla)
        {
            string idTipoMalla = "*";
            try
            {
                OpenService();
                switch (tipoMalla)
                {
                    case 1:
                        idTipoMalla = empresaConfig[1].codMallaEstandar;
                        break;
                    case 2:
                        idTipoMalla = empresaConfig[1].codMallaEspecial;
                        break;
                    case 3:
                        idTipoMalla = empresaConfig[1].codMallaSegunda;
                        break;
                    case 4:
                        idTipoMalla = empresaConfig[1].codMallaCerramiento;
                        break;
                    case 5:
                        idTipoMalla = empresaConfig[1].codMallaSemiEstandar;
                        break;
                  
                }

                Productos_Filter[] filtro = new Productos_Filter[]
                    { new Productos_Filter() { Field= Productos_Fields.Paños_x_Paquete,Criteria=">0"},
                     new Productos_Filter() { Field= Productos_Fields.Product_Group_Code,Criteria=idTipoMalla}};
                var result = wsProductos.ReadMultiple(filtro, null, 0);
                return result.ToList();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }

        // POST: api/Productos
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Productos/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Productos/5
        public void Delete(int id)
        {
        }
    }
}
