using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web.Http;
using System.Web.Http.Cors;
using wsTRH.pEmpleado;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Xml;
namespace wsTRH.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmpleadoController : ApiController
    {
        private pEmpleado.Empleados_PortClient empleadoServicio;
        private void OpenService()
        {
            List<webServiceEmpresa> empresaConfig= configuracion.getDatosEmpresa();

            string usuario = string.Format(@"{0}\administrador",empresaConfig[0].dominio);
            string password = @"Paulagallardo2014";
            string urlWebService = empresaConfig[0].url;
            
            string url = @"http://{1}/DynamicsNAV/WS/{0}/Page/Empleados";
            url = string.Format(url,empresaConfig[0].empresa, urlWebService);

           
            BasicHttpBinding navisionWSBinding = new BasicHttpBinding();
            navisionWSBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            navisionWSBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            navisionWSBinding.MaxReceivedMessageSize = 2000971520;


            empleadoServicio = new Empleados_PortClient(navisionWSBinding, new EndpointAddress(url));
            empleadoServicio.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;
            empleadoServicio.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential(usuario, password);


        }
        // GET: api/Empleado
        public IHttpActionResult Get()
        {
            OpenService();
            if (empleadoServicio != null)
            {
                Empleados_Filter[] filtro = new Empleados_Filter[0];
                
                return Ok(empleadoServicio.ReadMultiple(filtro, null, 0).ToList());

            }
            return null;
        }

        // GET: api/Empleado/5
        public IHttpActionResult Get(string id)
        {
            OpenService();
            if (empleadoServicio != null)
            {
                Empleados_Filter[] filtro = new Empleados_Filter[1];

                //Filtro Empleado
                filtro[0] = new Empleados_Filter();
                filtro[0].Field = Empleados_Fields.No;
                filtro[0].Criteria = id;
               



                return Ok(empleadoServicio.ReadMultiple(filtro, null, 0).ToList());

            }
            return null;
        }

        // POST: api/Empleado
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Empleado/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Empleado/5
        public void Delete(int id)
        {
        }
    }
}
