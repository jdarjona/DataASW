using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Cors;
using wsTRH.cuEntrega;
using wsTRH.pEntrega;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Xml;

namespace wsTRH.Controllers
{
   [EnableCors(origins: "*", headers: "*", methods: "*")] 
    public class EntregaAlmacenController : ApiController
    {
        EntregaAlmacen_PortClient entregaAlmacenServicio;
        AlmacenRepuestos_PortClient codeUnitEntregaAlmacen;
        private void OpenService()
        {

            List<webServiceEmpresa> empresaConfig = configuracion.getDatosEmpresa();

            string usuario = string.Format(@"{0}\administrador", empresaConfig[0].dominio);
            string password =@"Paulagallardo2014" ;
            string urlWebService = empresaConfig[0].url;

            string url = @"http://{1}/DynamicsNAV/WS/{0}/Page/EntregaAlmacen";
            url = string.Format(url, empresaConfig[0].empresa, urlWebService);

            BasicHttpBinding navisionWSBinding = new BasicHttpBinding();
            navisionWSBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            navisionWSBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            navisionWSBinding.MaxReceivedMessageSize = 2000971520;


            entregaAlmacenServicio = new EntregaAlmacen_PortClient(navisionWSBinding, new EndpointAddress(url));
            entregaAlmacenServicio.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;
            entregaAlmacenServicio.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential(usuario, password);


        }

        private void OpenServiceCodeUnitEntregaAlmacen()
        {
            List<webServiceEmpresa> empresaConfig = configuracion.getDatosEmpresa();

            string usuario = string.Format(@"{0}\administrador", empresaConfig[0].dominio);
            string password = @"Paulagallardo2014";

            string url = @"http://{1}/DynamicsNAV/WS/{0}/Codeunit/AlmacenRepuestos";
            url = string.Format(url, empresaConfig[0].empresa, empresaConfig[0].url);

            BasicHttpBinding navisionWSBinding = new BasicHttpBinding();
            navisionWSBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            navisionWSBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            navisionWSBinding.MaxReceivedMessageSize = 2000971520;


            codeUnitEntregaAlmacen = new AlmacenRepuestos_PortClient(navisionWSBinding, new EndpointAddress(url));
            codeUnitEntregaAlmacen.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;
            codeUnitEntregaAlmacen.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential(usuario, password);


        }
        // GET: api/EntregaAlmacen/5
        [ResponseType(typeof(EntregaAlmacen))]
       
        public IHttpActionResult Get(string id)
        {
            OpenService();
            if (entregaAlmacenServicio != null)
            {
                EntregaAlmacen_Filter[] filtro = new EntregaAlmacen_Filter[2];

                //Filtro Empleado
                filtro[0] = new EntregaAlmacen_Filter();
                filtro[0].Field = EntregaAlmacen_Fields.Cod_Empleado;
                filtro[0].Criteria = id;
                //Filtro Empleado
                filtro[1] = new EntregaAlmacen_Filter();
                filtro[1].Field = EntregaAlmacen_Fields.Entregado;
                filtro[1].Criteria = "No";

                var entrega = entregaAlmacenServicio.ReadMultiple(filtro, null, 0).ToList();

                return Ok(entrega);

            }
            return null;
        }


       
        // POST: api/EntregaAlmacen
        [HttpPost]
        public IHttpActionResult Post([FromBody]  EntregaAlmacen[] listEntrega, string codEmpleado)
        {


            try {
            
            
            CultureInfo culture = new CultureInfo("es-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;

            //1. Actualizamos la lista de materiales
            OpenService();
                EntregaAlmacen[] entregaAlmacenArray = listEntrega;
            entregaAlmacenServicio.UpdateMultiple(ref entregaAlmacenArray);

            //2. Procedemos al registro
            OpenServiceCodeUnitEntregaAlmacen();
            this.codeUnitEntregaAlmacen.RegistrarEntrega(codEmpleado);

                ////3. Volvemos a rescatar los datos 
                //EntregaAlmacen_Filter[] filtro = new EntregaAlmacen_Filter[2];

                ////Filtro Empleado
                //filtro[0] = new EntregaAlmacen_Filter();
                //filtro[0].Field = EntregaAlmacen_Fields.Cod_Empleado;
                //filtro[0].Criteria = codEmpleado;
                ////Filtro Empleado
                //filtro[1] = new EntregaAlmacen_Filter();
                //filtro[1].Field = EntregaAlmacen_Fields.Fecha;
                //filtro[1].Criteria = DateTime.Today.ToString("d");



                // return Ok(entregaAlmacenServicio.ReadMultiple(filtro, null, 0).ToArray());
                return Ok();
            } catch (Exception ex) {

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.Content = new StringContent(ex.Message); 
                response.RequestMessage = Request;
                var error=new HttpResponseException(response);
                return (IHttpActionResult)error;

            }

            


        }

        // PUT: api/EntregaAlmacen/5
        
        public IHttpActionResult Put(string codRepusto, string codEmpleado)
        {

            try { 
            OpenService();

            if (entregaAlmacenServicio != null)
            {
                EntregaAlmacen nuevaEntrega = new EntregaAlmacen();
                nuevaEntrega.Cod_Almacen = @"PATIO";
                nuevaEntrega.Cod_Producto = codRepusto.Trim();
                    if (codEmpleado.Trim() != string.Empty)
                    {
                        nuevaEntrega.Cod_Empleado = codEmpleado.Trim();
                    }
                    else {
                        throw new Exception("No se ha introducido codigo empleado");
                    }
                entregaAlmacenServicio.Create(ref nuevaEntrega);

                return Ok(nuevaEntrega);
            }

            }
            catch (Exception ex)
            {

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.Content = new StringContent(ex.Message);
                response.RequestMessage = Request;
                var error = new HttpResponseException(response);
                return (IHttpActionResult)error;

            }
            return null;

        }

        // DELETE: api/EntregaAlmacen/5
        [HttpDelete]
        public void Delete(string key)

        {
            OpenService();

            if (entregaAlmacenServicio != null)
            {
                entregaAlmacenServicio.Delete(key);
               
            }
           ;
        }
    }
}
