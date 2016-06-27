using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web.Http;
using wsTRH.pOfertas;
using wsTRH.Controllers;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace wsTRH.Controllers
{
    [System.Web.Http.Authorize]
    public class OfertasController : ApiController
    {


        pOfertas.Ofertas_PortClient wsOfertas;
        private void OpenService()
        {

            List<webServiceEmpresa> empresaConfig = configuracion.getDatosEmpresa();

            string usuario = string.Format(@"{0}\administrador", empresaConfig[0].dominio);
            string password = @"Paulagallardo2014";


            string url = @"http://{1}/DynamicsNAV/WS/{0}/Page/Ofertas";
            url = string.Format(url, empresaConfig[0].empresa, empresaConfig[0].url);

            BasicHttpBinding navisionWSBinding = new BasicHttpBinding();
            navisionWSBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            navisionWSBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            navisionWSBinding.MaxReceivedMessageSize = 2000971520;


            wsOfertas = new Ofertas_PortClient(navisionWSBinding, new EndpointAddress(url));
            wsOfertas.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;
            wsOfertas.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential(usuario, password);


        }

        // GET: api/Ofertas
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Ofertas/5
        public Ofertas Get(string codOferta)
        {
            try
            {
                OpenService();

                codOferta = codOferta.Replace("_", "/");
                Ofertas result = wsOfertas.Read(codOferta);
                return result;
            }
            catch (Exception)
            {

                return null;
            }
            
        }

        // POST: api/Ofertas
        public pOfertas.Ofertas Post(string codCliente, int codAlmacen)
        {

            try
            {
                OpenService();

                pOfertas.Ofertas oferta = new Ofertas();

                oferta.Sell_to_Customer_No = codCliente;
                oferta.IdAlmacen = codAlmacen;

                wsOfertas.Create(ref oferta);

                return oferta;
            }
            catch (Exception)
            {
                return null;

            }

        }
        // PUT: api/Ofertas/5
        public pOfertas.Ofertas Put([FromBody]Ofertas oferta)
        {
            try
            {
                OpenService();

                wsOfertas.Update(ref oferta);

                return oferta;
            }
            catch (Exception)
            {
                return null;

            }
        }


       
        public pOfertas.Ofertas Put(string codProducto, string codOferta)
        {

            try
            {
                OpenService();

                if (FuncionesCodeUnit.addProductoOferta(codProducto, codOferta)){
                    var oferta = wsOfertas.Read(codOferta);

                    return oferta;
                }
                else {
                    return null;

                }

              

                
            }
            catch (Exception)
            {
                return null;

            }

        }
        // DELETE: api/Ofertas/5
        public void Delete(int id)
        {
        }

        public string getPdfOferta(string idOferta) {
            
            string _path;
            try
            {
                
                _path = FuncionesCodeUnit.getPdfOferta(idOferta, false); //.ImprimirOferta(idOferta, true);
                return _path;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            
        }
    }
}
